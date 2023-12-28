# The SCIChartSurface3D Camera 3D API
In SciChart, the Camera 3D is defined by the `ISCICameraController` instance. It's accessible through `SCIChartSurface3D.camera` property. 
This interface is implemented in `SCICamera3D` class which instance is used by `SCIChartSurface3D` by default.

![Camera 3D](img/axis-3d/camera-3d.png)

> **_NOTE:_** The Above is the representation of a Camera in 3D Space. The camera is attached to the `SCIChartSurface3D.camera` property and is defined by a **position**, **target** and other properties which defines the properties of the viewport.

## Camera3D Position, Target
The camera is defined by the `ISCICameraController.position` and `ISCICameraController.target` properties, which are `XYZ vectors` in World Coordinates, as well as `ISCICameraController.projectionMode` which is defined by `SCICameraProjectionMode` enumeration.

`SCICamera3D` properties which define the viewport as seen by the camera are listed below:

| **Property**                          | **Description**                                                                                          |
| ------------------------------------- | -------------------------------------------------------------------------------------------------------- |
| `ISCICameraController.position`       | Defines the **position** of the camera as an `XYZ Vector` in the World Coordinates.                          |
| `ISCICameraController.target`         | Defines the **target** of the camera as an `XYZ Vector` in the World Coordinates.                            |
| `ISCICameraController.orbitalPitch`   | Defines the **Pitch** angle of the camera position relative to the target. Expects **degrees**.          |
| `ISCICameraController.orbitalYaw`     | Defines the **Yaw** angle of the camera position relative to the target. Expects **degrees**.            |
| `ISCICameraController.radius    `     | The **distance** of the Camera Position to the Camera Target.                                            |
| `ISCICameraController.aspectRatio`    | Defines the viewport aspect ratio.                                                                       |
| `ISCICameraController.projectionMode` | Defines whether the camera is **Perspective** or **Orthogonal** which is `SCICameraProjectionMode` enum. |
| `ISCICameraController.orthoWidth`     | Defines the **width** of the projected viewport when `projectionMode` is **Orthogonal**.                 |
| `ISCICameraController.orthoHeight`    | Defines the **height** of the projected viewport when `projectionMode` is **Orthogonal**.                |
| `ISCICameraController.fieldOfView`    | Defines the **Field of View** Angle of the Camera in **Degrees**.                                        |
| `ISCICameraController.nearClip`       | Defines the **Near** Clipping distance of the camera.                                                    |
| `ISCICameraController.farClip`        | Defines the **Far** Clipping distance of the camera.                                                     |

The **Modify Camera3D Properties** example shows how to manipulate the camera, and how to switch between perspective and orthogonal modes.

<video autoplay loop muted playsinline src="img/axis-3d/modify-camera3d-properties-example.mp4" style="width: 50%; height: 50%"></video>​

> **_NOTE:_** The **Modify Camera3D Properties** example can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-modify-camera-3d-properties/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-3d-chart-example-modify-camera-3d-properties/)
