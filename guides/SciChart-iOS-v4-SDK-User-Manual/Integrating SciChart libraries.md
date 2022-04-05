# Integrating SciChart libraries
In this article we are going to show different approaches off integrating SciChart library into your iOS Application:
- **Native iOS**
  - [Manually](#integrating-scichart-manually)
  - [CocoaPods](#integrating-scichart-using-cocoapods)
  - [Swift Package Manager](#integrating-scichart-via-swift-package-manager)
  - [Carthage](#integrating-scichart-using-carthage)
- **Xamarin.iOS**
  - [NuGet](#integrating-scichart-using-nuget-package-manager)

# Integrating SciChart manually
After you have downloaded and unzipped the [SciChart iOS Trial](https://www.scichart.com/downloads) package, you can find `SciChart.xcframework` in it. 

Now, let's link SciChart.xcframework. To do this we need to add framework file in "Frameworks, Libraries, and Embedded Content" section of your target. Follow these steps to do that:
- Click on your Project in Navigator and select its target
- In the "General" tab scroll right to the bottom
- Click on "+" in the "Frameworks, Libraries, and Embedded Content" section
- Click on "Add others..." button in appeared dialog
- Select the framework file

![Add SciChart.xcframework](img/v3-user-manual/add-scichart-xcframework.png)

> **_Keep the framework file in the same folder_**
>
> We suggest you to copy the framework file into your project's folder, and then link the framework file. This will help you to avoid the situation when you unintentionally moved or deleted the folder where the SciChart.xcframework file was placed at the beginning. 
>
> If you decide not to - don't forget to set the _"Frameworks Search Paths"_ property which you can find in _"Build Settings"_ tab.

# Integrating SciChart using CocoaPods
SciChart is available via CocoaPods. We provide a public Podspec repositories, so that you can integrate SciChart with minimum effort, either pinning to a specific version or always getting the latest stable release. If you’re new to CocoaPods, please see [CocoaPods Getting Started guide](https://guides.cocoapods.org/using/getting-started.html).

In order to get SciChart framework, you should use one of the provided Podspec repository. 
SciChart provides 2 feeds: 
- **[SciChart Official Releases](https://github.com/ABTSoftware/PodSpecs/releases)**:
  - https://github.com/ABTSoftware/PodSpecs.git
- **[SciChart Nightly Builds](https://github.com/ABTSoftware/PodSpecs-Nightly/releases)** Feed URLs:
  - https://github.com/ABTSoftware/PodSpecs-Nightly.git

To integrate SciChart using release CocoaPods feed you will need to add the following into your [Podfile](https://guides.cocoapods.org/using/the-podfile.html):

```ruby
source 'https://github.com/ABTSoftware/PodSpecs.git'
platform :ios, '8.0'

use_frameworks!
target 'YourTargetName' do
  # Use the latest available Version
  pod 'SciChart'
end
```
> **_NOTE:_** You can also use a particular version of SciChart by [Specifying pod version](https://guides.cocoapods.org/using/the-podfile.html#specifying-pod-versions), we are highly recommend you to make sure you are using the **Latest** version of SciChart though.

Once you’ve completed the steps above, run `pod install --repo-update`

When the new version of SciChart will be released you can easily update it with `pod update SciChart`

> **_NOTE:_** You can also access SciChart nightly builds feed as mentioned above via separate podspecs repository https://github.com/ABTSoftware/PodSpecs-Nightly.

# Integrating SciChart via Swift Package Manager
[Swift Package Manager](https://swift.org/package-manager/) is a tool built-in XCode used for managing and distributing third-party dependencies. 
Starting from **Swift 5.3** and **XCode 12** it gained the ability to distribute binary frameworks as Swift packages, so we finally can offer SciChart v4.x as a Swift package.

Since SwiftPM is decentralized package manager, in order to access SciChart framework, you should use the public repository: https://github.com/ABTSoftware/SciChart-SP. 

As usual, at SciChart, we provide release and nightly build feeds, but with Swift Package Manager everything it's a little bit more confusing. 
Please see the following sections to learn how to integrate release as well as nightly builds.

## Integrating SciChart **Release** builds via SwiftPM
Adding SciChart Swift package into your application is fairly simple and achieved in a few steps:
- Go to Files > Swift Packages > Add Package Dependency… and enter repository mentioned above: https://github.com/ABTSoftware/SciChart-SP.git
- Specify the rules which will be used to resolve desired version
![SciChart SwiftPM rules](img/v3-user-manual/scichart-swift-package.png)
- Wait for Xcode to finish downloading and resolving the Swift package into your project
- Choose the package products and targets and you are good to go
![SciChart SwiftPM installed](img/v3-user-manual/scichart-swift-package-installed.png)

## Integrating SciChart **Nightly** builds via SwiftPM
Since XCode support only major, minor and patch numbers **MAJOR.MINOR.PATCH**, there's no way to differentiate specific build via built-in versioning.
That's why, nightly builds can be accessed via selecting particular commit under package dependency rules during adding the SciChart package:
![SciChart SwiftPM Nightly](img/v3-user-manual/scichart-swift-package-nightly.png)

Each commit has message with exact version number, as you used to while using CocoaPods or manual integration,
so you can select proper version, and use commit hash, to checkout proper SciChart Package, e.g.:
- `[Release] SciChart(4.0.0.5436)` 
- `[Nightly] SciChart(4.0.0-nightly.5482)`
    
> **_NOTE:_** Currently, Xcode fails to sign the frameworks that are provided by SwiftPM with your app’s signing identity. It’s a known issue [SR-13343](https://bugs.swift.org/browse/SR-13343) in Xcode 12. Please, use a workaround, described [here](https://pspdfkit.com/guides/ios/current/knowledge-base/library-not-found-swiftpm/).

# Integrating SciChart using Carthage
SciChart isn't available via [Carthage](https://github.com/Carthage/Carthage/blob/master/README.md) yet.
Please see integrating via [CocoaPods](#integrating-scichart-using-cocoapods), [Swift Package Manager](#integrating-scichart-via-swift-package-manager) or [Manually](#integrating-scichart-manually).

# Integrating SciChart using NuGet Package Manager
**NuGet** is a package manager for Visual Studio and ***.NET***. It allows you to add a reference to a DLL and download it from the cloud.
There is no need to keep DLLs in version control or install them on local disk.
You can download them on-demand and link against them during your build process. 

In order to get SciChart libraries, you should connect to our [Private Feed](https://www.myget.org/gallery/abtsoftware). SciChart provides 2 feeds:
- **[SciChart Official Releases](https://www.myget.org/gallery/abtsoftware)** Feed URLs:
  - Visual Studio 2015+ - https://www.myget.org/F/abtsoftware/api/v3/index.json
  - Visual Studio 2012+ - https://www.myget.org/F/abtsoftware/api/v2
- **[SciChart Nightly Builds](https://www.myget.org/gallery/abtsoftware-bleeding-edge)** Feed URLs:
  - Visual Studio 2015+ - https://www.myget.org/F/abtsoftware-bleeding-edge/api/v3/index.json
  - Visual Studio 2012+ - https://www.myget.org/F/abtsoftware-bleeding-edge/api/v2

> **_NOTE:_** `*.nupkg` files can be unzipped if you rename them as zip. They contain the SciChart DLLs. So, even if you are not using NuGet, you can get our nightly builds by clicking the download button on the gallery page and unzipping the package.

After adding the desired feed from the above ou should have something like below in your Visual Studio:

![SciChart NuGet Feed](img/v3-user-manual/scichart-ios-nuget-feed.png)

From here do the following steps:
- Right Click on your Project in the Solution Navigator
- Click on "Manage NuGet Packages..."
- Make sure SciChart feed is selected (alternatively "All Sources" can be selected)
- Search for "SciChart"
- Find **SciChart.iOS** and click "Add Package"
- Finally - Accept License

![Add SciChart.iOS NuGet Package](img/v3-user-manual/scichart-ios-nuget-package.png)

That's it. You are good to go.