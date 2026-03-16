# Running the .NET iOS Example (SciChart)

This guide explains how to build and run the **SciChart iOS .NET example**.

## Prerequisites

- Install **.NET 8 SDK** (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Xcode installed with iOS simulator support
- Visual Studio Code

## Setup

1. Navigate to the binding project:

```bash
cd scichart.ios.binding
```

2. install .NET workloads:

```bash
sudo dotnet workload install ios
```

3. Add the `scichart.xcframework` to the project (ensure the framework is included in the binding project).

Project structure should look similar to:

```text
scichart.ios.binding/
├── scichart.xcframework
├── SciChartBinding.csproj
└── ...
```

## Build

Build the project:

```bash
dotnet build
```

## Run the Demo App

Switch to SciChartDemoApp
```bash
cd ../SciChartDemoApp
```

Run the **SciChartDemoApp** on the iOS simulator:
 

```bash
dotnet run \
  -f net8.0-ios \
  -p:RuntimeIdentifier=iossimulator-arm64
```

## Running with a Specific Simulator (Fallback)
If the dotnet run command fails, you can manually launch the app using mlaunch.

1. List Available Simulators

```bash
xcrun simctl list devices
```
Copy the UDID of the simulator you want to use.

2. Launch the App Manually
Replace <UDID> with the simulator UDID:

```bash
 dotnet build -t:Run -f net8.0-ios -r iossimulator-arm64 \
  /p:_DeviceName=:v2:udid=<UDID>
```
OR  

```bash
/usr/local/share/dotnet/packs/Microsoft.iOS.Sdk.net8.0_18.0/18.0.8319/tools/bin/mlaunch \
  --launchsim bin/Debug/net8.0-ios/iossimulator-arm64/SciChartDemoApp.app/ \
  --device ":v2:udid=<UDID>" \
  --stdout /dev/ttys002 \
  --stderr /dev/ttys002 \
  --wait-for-exit:true
```

## Notes

- Ensure the iOS simulator is available via Xcode.
