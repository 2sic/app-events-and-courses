const path = require('path');
const webpack = require('webpack');
const FixStyleOnlyEntriesPlugin = require("webpack-fix-style-only-entries");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = (env) => {
  return {
    entry: {
      styles: `./src/styles/index.scss`,      
      scripts: './src/ts/index.ts',
    },
    output: {
      path: path.resolve(__dirname, `staging/dist`),
      filename: '[name].min.js',
      assetModuleFilename: 'images/[hash][ext][query]'
    },
    mode: 'production',
    devtool: 'source-map',
    watch: true,
    stats: {
      warnings: false,
      cachedModules: false,
      groupModulesByCacheStatus: false
    },
    cache: {
      type: 'filesystem',
      cacheDirectory: path.resolve(__dirname, '.temp_cache'),
      compression: 'gzip',
    },
    resolve: {
      extensions: ['.ts', '.tsx', '.js', '.scss']
    },    
    plugins: [
      new FixStyleOnlyEntriesPlugin(),
      new MiniCssExtractPlugin({
        filename: '[name].min.css',
      }),
      new webpack.ProgressPlugin(),
    ],
    module: {
      rules: [{
          test: /\.scss$/,
          exclude: /node_modules/,
          use: [
            MiniCssExtractPlugin.loader,
            {
              loader: 'css-loader',
              options: {
                sourceMap: true
              }
            }, {
              loader: 'postcss-loader',
              options: {
                sourceMap: true,
                postcssOptions: { 
                  plugins: [
                    require('autoprefixer')
                  ] 
                }
              }
            }, {
              loader: 'sass-loader',
              options: {
                sourceMap: true
              }
            }
          ],
        }, {
          test: /\.ts$/,
          exclude: /node_modules/,
          use: {
            loader: 'ts-loader'
          }
        }
      ],
    },
  }
};