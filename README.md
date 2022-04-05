# SciChart.iOS.Documentation
This repository contains official documentation for [SciChart.iOS & macOS](https://www.scichart.com/): High Performance Realtime [iOS & macOS Chart Library](https://www.scichart.com/ios-chart-features/).

[SciChart.iOS & macOS Chart Examples](https://github.com/ABTSoftware/SciChart.iOS.Examples) are provided in `Swift` & `Objective-C`. If you are looking for other platforms then please see here:

* [Android Charts](https://github.com/ABTSoftware/SciChart.Android.Examples) (Java / Kotlin)
* [WPF Charts](https://github.com/ABTSoftware/SciChart.WPF.Examples) (C# / WPF)
* [Xamarin Charts](https://github.com/ABTSoftware/SciChart.Xamarin.Examples) (C#)
  
***

## Documentation Samples
Here you can also find [Examples and Tutorials used in documentation articles](samples):
<!-- TODO: - [Creating Your First SciChart Android Appliation](samples/first-app) -->
<!-- TODO: - [Documentation Samples Sandbox](samples/sandbox) -->
- [SciChart iOS Native Tutorials](samples/tutorials-native)
- [SciChart iOS Xamarin Tutorials](samples/tutorials-xamarin)

## How to Build Documentation 
- [Required dependencies](#required-dependencies)
- [Project structure](#project-structure)
- [Build process](#build-process)
- [Development process](#development-process)
  - [Adding new article/section](#adding-new-articlesection)
  - [Alternative way to add article with new subcategory](#alternative-way-to-add-article-with-new-subcategory)

### **Required dependencies**
- [Jazzy](https://github.com/realm/jazzy) - CLI tool that generates documentation for `Swift` or `Objective-C`. 
On Mac it can be installed from rubygems: [\[`sudo`\] `gem install jazzy`](https://github.com/realm/jazzy#installation)
Latest stable version at the moment of writing was v0.14.2.

- [SourceKitten](https://github.com/jpsim/SourceKitten) - CLI tool for interacting with SourceKit. 
On mac is installed via `Homebrew` - [`brew install sourcekitten`](https://github.com/jpsim/SourceKitten#installation)

### **Project structure**
- The main configuration is stored at `.jazzy.yaml`.
- The homepage is generated from `index.md` file
- Primary table of content (TOC), that descibes main categories lies in `.jazzy.yml`
- Documentation articles are stored at `sections` and `guides` folders.
- Images used by articles should be stored at `scichart-theme/assets/img/**` folder
- Style and template overrides for documentation website can be found is at `scichart-theme` folder

### **Build process**
Run one of the following commands from the `build/build_docs.sh` script:
| **Command**       | **Description**                                                                                         |
| ----------------- | ------------------------------------------------------------------------------------------------------- |
| generate-api-docs | generates API documentation from pre-built \`SciChart.framework\` or \`SciChart.xcframework\`.          |
| generate-site     | generates full documentation site from \`.md\` articles and \`SourceKit\` API docs metadata.            |
| build             | build full documentation from \`.md\` articles and SciChart.iOS API docs comments.                      |

e.g `sh build/build_docs.sh generate-site`

### **Development process**
1. To build documentation at first we need to prepare library headers to extract API documentation. Those can be extract from `SciChart.framework`.

2. We also need to resolve common jazzy problem, when your directory structure does not reflect the include/import statements, 
e.g. you have `#import <MyFile/MyFile.h>` but the directory containing `MyFile.h` is called `Sources/Headers`.
Then whatever `-I,path` pair you pass the compile fails complaining about not finding files. 

Since there's no neat solution we are manually create folder filled with headers from `SciChart.framework` required by umbrella in needed place to run jazzy against.
More informatiion about workarounds:
- https://github.com/realm/jazzy/issues/667
- https://github.com/realm/jazzy/issues/1068

Long story short - `SciChart.h` must be on the same level, that it's headers `SciChart/*.h`

`.jazzy.yaml` should contain the following lines:

```yaml
    framework_root: path_to_headers/
    umbrella_header: path_to_headers/SciChart.h
```

3. (Optional). You can cache your SourceKit `.json` to speed up iterative documentation generation.
With the `SciChart.h` and other headers in `SciChart/*.h` in place, you can run the following:

```sh
sourcekitten doc --objc "SciChart.h" -- -x objective-c \
        -isysroot $(xcrun --sdk iphonesimulator --show-sdk-path) \
        -I "./" > api/scichart-sourcekit-api-docs
```

This will output your API documentation `.json` in `api/scichart-sourcekit-api-docs` file.
From here you just need to specify where to get prepared the above `.json` in `.jazzy.yaml`:

```yaml
    sourcekitten_sourcefile: api/scichart-sourcekit-api-docs
```

4. Then to build documentation (assuming you have your `.jazzy.yaml` in place) you just use

```sh
jazzy
```

This will build documentation and place it in the root directory

### Adding new article/section
1. Create `*.md` file at `sections` folder, to create a new section, e.g `SciChart iOS v4 SDK User Manual.md`.
2. Or create `*.md` file at `guides` folder, to create separate article, e.g. `SciChart-iOS-v4-SDK-User-Manual/Installation and System Requirements.md`.
2. Edit `.jazzy.yaml` file add new item(s), that has name that is used in tree and path to `*.md` file:

```yaml
custom_categories:
  - name: SciChart iOS v4 SDK User Manual
    children:
      - Installation and System Requirements
```