import { defineConfig } from 'vite';
import { resolve } from 'path';
import * as sass from 'sass';
import autoprefixer from 'autoprefixer';
import postcss from 'postcss';

export default defineConfig({
  // Root directory for the build
  root: '.',
  
  // Build configuration
  build: {
    // Output directory (equivalent to webpack output.path)
    outDir: 'staging/dist',
    
    // Enable source maps (equivalent to webpack devtool: 'source-map')
    sourcemap: true,
    
    // Empty output directory before build
    emptyOutDir: true,
    
    // Asset-Handling
    assetsDir: 'images',
    
    // Rollup options
    rollupOptions: {
      // Entry point for TypeScript
      input: {
        scripts: resolve(process.cwd(), 'src/ts/index.ts'),
      },
      output: {
        // Customize output file names (equivalent to webpack filename)
        entryFileNames: '[name].min.js',
        chunkFileNames: '[name].min.js',
        assetFileNames: (assetInfo) => {
          // CSS files with .min.css suffix
          if (assetInfo.name?.endsWith('.css')) {
            return 'styles.min.css';
          }
          // Images in images folder
          if (/\.(png|jpe?g|gif|svg|webp)$/i.test(assetInfo.name)) {
            return 'images/[name]-[hash][extname]';
          }
          return '[name]-[hash][extname]';
        },
      },
    },
    
    // Enable minification
    minify: 'esbuild',
    
    // Target browsers
    target: 'es2020',
    
    // Disable CSS code split
    cssCodeSplit: false,
    
    // Cache directory (equivalent to webpack cache)
    cacheDir: '.vite_cache',
  },
  
  // CSS configuration
  css: {
    preprocessorOptions: {
      scss: {
        // Source maps for SCSS
        sourceMap: true,
      },
    },
    postcss: {
      plugins: [
        autoprefixer(), // Autoprefixer for vendor prefixes
      ],
    },
  },
  
  // Resolve configuration (equivalent to webpack resolve.extensions)
  resolve: {
    extensions: ['.ts', '.tsx', '.js', '.scss', '.css'],
  },
  
  // Server configuration
  server: {
    port: 3000,
    watch: {
      usePolling: true,
    },
  },
  
  // Plugins
  plugins: [
    {
      name: 'compile-scss-to-css',
      async generateBundle(options, bundle) {
        // Compile SCSS file directly
        const scssPath = resolve(process.cwd(), 'src/styles/index.scss');
        
        try {
          // Compile SCSS to CSS
          const result = sass.compile(scssPath, {
            sourceMap: true,
            style: 'compressed',
          });
          
          // Apply PostCSS (Autoprefixer)
          const postcssResult = await postcss([autoprefixer()]).process(result.css, {
            from: scssPath,
            to: 'styles.min.css',
            map: { 
              inline: false, 
              annotation: true,
              prev: result.sourceMap ? JSON.stringify(result.sourceMap) : false,
            },
          });
          
          // Add CSS file to bundle
          this.emitFile({
            type: 'asset',
            fileName: 'styles.min.css',
            source: postcssResult.css,
          });
          
          // Add source map
          if (postcssResult.map) {
            this.emitFile({
              type: 'asset',
              fileName: 'styles.min.css.map',
              source: postcssResult.map.toString(),
            });
          }
          
          console.log('✓ CSS compiled successfully');
        } catch (error) {
          console.error('Error compiling SCSS:', error);
          throw error;
        }
      },
    },
    {
      name: 'progress-plugin',
      buildStart() {
        console.log('\nBuilding staging...');
      },
      buildEnd() {
        console.log('✓ Build complete for staging\n');
      },
    },
  ],
  
  // Optimization
  optimizeDeps: {
    include: ['typescript'],
  },
});
