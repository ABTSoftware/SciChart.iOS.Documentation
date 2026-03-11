# Running the .NET iOS Example (SciChart)

This guide explains how to build and run the **SciChart iOS .NET example**.

## Prerequisites

- Install **.NET 10 SDK**
- Xcode installed with iOS simulator support
- Visual Studio Code

## Setup

1. Navigate to the binding project:

```bash
cd scichart.ios.binding
```

2. Restore .NET workloads:

```bash
sudo dotnet workload restore
```

3. Add the `scichart.xcframework` to the project (ensure the framework is included in the binding project).

## Build

Build the project:

```bash
dotnet build
```

## Run the Demo App

Run the **SciChartDemoApp** on the iOS simulator:

```bash
dotnet run \
  -f net10.0-ios \
  -p:RuntimeIdentifier=iossimulator-arm64
```

## Notes

- Ensure the iOS simulator is available via Xcode.
- The runtime identifier `iossimulator-arm64` is required for Apple Silicon Macs.
