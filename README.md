<img src="app-icon.png" align="right" width="200px">

# Events and Courses 6 App for .net CMSs

> This is a [2sxc](https://2sxc.org) App for [DNN ☢️](https://www.dnnsoftware.com/) and [Oqtane 💧](https://www.oqtane.org/)

A standard Events and Courses app to use with 2sxc

| Aspect              | Status | Comments or Version |
| ------------------- | :----: | ------------------- |
| 2sxc                | ✅    | requires 2sxc v15.04
| Dnn                 | ✅    | For v7, v8 and v9
| Oqtane 2            | ✅    | Requires v2.02
| No jQuery           | ✅    | 
| Live Demo           | ➖    |
| Install Checklist   | ✅    | See [Installation](https://azing.org/2sxc/r/j3DB7UTx) on [azing.org](https://azing.org/2sxc)
| Source & License    | ✅    | included, ISC/MIT
| App Catalog         | ✅    | See [app catalog](https://2sxc.org/en/apps/app/course-and-events-v6-for-dnn-and-oqtane)
| Screenshots         | ✅    | See [app catalog](https://2sxc.org/en/apps/app/course-and-events-v6-for-dnn-and-oqtane)
| Best Practices      | ✅    | Uses v13.10 conventions
| Bootstrap 3         | ✔️    | Works, but not optimized 
| Bootstrap 4         | ✅    | 
| Bootstrap 5         | ✅    | 


## Customize the App

If you want to customize the CSS, you will usually follow the ["Create Custom Styles in a Standard 2sxc App" checklist](https://azing.org/2sxc/r/gg_aB9FD)

## History

* v1.0 2017-03
* v2.0 2017-05
* ...
* v6.0 2021-11 for 2sxc 12.08
  * Now hybrid, runs on Dnn and Oqtane
  * Updated to v12 best-practices
  * Improved code, clean-up etc.
  * No more jQuery
* v6.1 2022-02
  * Enabled dataoptimizations
* v6.1.2 2022-03
  * Improvements toolbar
* v6.2.0 2022-04  
  * Changed all access to Services to ToSic.Sxc.Services
  * Changed all images to use the IImageService
  * Replaced data-enableoptimizations with IPageService.AssetAttributes()
  * Updated JS to use new webapi methods (fetch -> fetchRaw)
* v6.3.0 2022-06
  * Changed all base classes to their 2sxc 14 equivalents
  * Removed all GetService<> and replaced it with the ServiceKit14
  * Changed all toolbar configurations to use the IToolbarService
  * Updated webpack
* v6.4.0 2022-06
  <!-- * Replaced Tag.Custom with `Kit.HtmlTags` -->
  * Enhanced Kit.Image with `imgAltFallback`
  * Replaced turnOn Tag with `Kit.Page.TurnOn`
  * Removed _ from Filenames
