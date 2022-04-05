# Installation and System Requirements

### Q: What hardware is required to run SciChart iOS?
SciChart iOS is designed to run on any iOS device capable of supporting iOS 9 and later.

### Q: What hardware is required to run SciChart macOS?
SciChart macOS is designed to run on any macOS device capable of supporting macOS 10.12 Sierra and newer.

### Q: What IDEs are recommended?
At SciChart we recommend XCode, as it is a common IDE. However, SciChart can be compiled and developed on third party IDEs such as AppCode.

### Q: Do I need a Fast iOS Device?
Not necessarily! You only need a device capable of supporting [Metal or OpenGL ES3](https://developer.apple.com/library/archive/documentation/DeviceInformation/Reference/iOSDeviceCompatibility/HardwareGPUInformation/HardwareGPUInformation.html). SciChart for iOS is very memory and CPU efficient, using fewer CPU, GPU and memory resources than competitors, resulting in longer battery life in like-for-like applications.

### Q: How much RAM do I need to run SciChart?
SciChart is actually very memory efficient. SciChart iOS uses not much more memory than required to hold the raw data, for instance, if you wish to display 1,000,000 points of XY data where X and Y types are double, you can expect to use just over 1M * 8 * 2 bytes = 16MB, plus approx. 30MB for the app to host the chart. So for 2D Charts, SciChart for iOS can be run on just about any device so long as minimum operating system requirements are met.