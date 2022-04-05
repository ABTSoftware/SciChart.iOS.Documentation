# Zoom Extents Modifier 3D
SciChart iOS 3D provides the ability to **zoom-to-fit** the entire 3D chart by ***double-tapping*** it via the `SCIZoomExtentsModifier3D`, available out of the box.

<video autoplay loop muted playsinline src="img/modifiers-3d/zoom-extents-modifier-3d.mp4"></video>

Besides [common features](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) which are inherited from the `SCIChartModifier3DBase` class, 
the `SCIZoomExtentsModifier3D` allows to control its specific features via the following properties:
- `SCIZoomExtentsModifier3D.resetTarget` - defines the `SCIVector3D` target where the `ISCICameraController.target` is moved on reset.
- `SCIZoomExtentsModifier3D.resetPosition` - defines the `SCIVector3D` target where the `ISCICameraController.position` is moved on reset.
- `SCIZoomExtentsModifier3D.executeOn` - allows to specify the trigger action for the modifier via the `SCIExecuteOn` enumeration.
- `SCIZoomExtentsModifier3D.autoFitRadius` - When `YES`, attempts to ***auto-fit*** the camera radius to fit the scene. When `NO` - uses the `resetPosition` and `resetTarget` instead.
- `SCIZoomExtentsModifier3D.animationDuration` - defines the animation duration in `NSTimeInterval` for any zoom operations.

> **_NOTE:_** There are several modes defined by the `SCIExecuteOn` enumeration, such as **Single Tap, Double Tap, Long Press**, and **Fling**.

There are two modes of operation for the `SCIZoomExtentsModifier3D` - ***AutoFit*** and ***Manual***:

| **Operation Mode**    | **Description**                                                                                                                                      |
| --------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- |
| `autoFitRadius = YES` | Tries to automatically fit the 3D scene into the viewport. `ISCICameraController.target` and `ISCICameraController.position` properties are ignored. |
| `autoFitRadius = NO`  | You should specify the precise coordinates for the `ISCICameraController.target` and `ISCICameraController.position` properties.                    |

## Adding a SCIZoomExtentsModifier to a Chart
Any [Chart Modifier 3D](Chart Modifier 3D APIs.html) can be [added to a `SCIChartSurface3D`](Chart Modifier 3D APIs.html#adding-a-chart-modifier-3d) via the `ISCIChartSurface3D.chartModifiers` property and `SCIZoomExtentsModifier3D` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface 3D has been created and configured somewhere
    id&lt;ISCIChartSurface3D&gt; surface;

    // Create a Modifier
    SCIZoomExtentsModifier3D *zoomExtentsModifier3D = [SCIZoomExtentsModifier3D new];
    zoomExtentsModifier3D.executeOn = SCIExecuteOn_DoubleTap;
    zoomExtentsModifier3D.autoFitRadius = NO;
    zoomExtentsModifier3D.resetPosition = [[SCIVector3 alloc] initWithX:200 y:200 z:200];
    zoomExtentsModifier3D.resetTarget = [[SCIVector3 alloc] initWithX:0 y:0 z:0];

    // Add the modifier to the surface
    [self.surface.chartModifiers add:zoomExtentsModifier3D];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface3D

    // Create a Modifier
    let zoomExtentsModifier3D = SCIZoomExtentsModifier3D()
    zoomExtentsModifier3D.executeOn = .doubleTap
    zoomExtentsModifier3D.autoFitRadius = false
    zoomExtentsModifier3D.resetPosition = SCIVector3(x: 200, y: 200, z: 200)
    zoomExtentsModifier3D.resetTarget = SCIVector3(x: 0, y: 0, z: 0)

    // Add the modifier to the surface
    self.surface.chartModifiers.add(zoomExtentsModifier3D)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface 3D has been created and configured somewhere
    IISCIChartSurface3D surface;

    // Create a Modifier
    var zoomExtentsModifier3D = new SCIZoomExtentsModifier3D();
    zoomExtentsModifier3D.ExecuteOn = SCIExecuteOn.DoubleTap;
    zoomExtentsModifier3D.AutoFitRadius = false;
    zoomExtentsModifier3D.ResetPosition = new SCIVector3(200, 200, 200);
    zoomExtentsModifier3D.ResetTarget = new SCIVector3(0, 0, 0);

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(zoomExtentsModifier3D);
</div>

> **_NOTE:_** To learn more about features available, please read on the [Chart Modifier 3D APIs](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) article.

## Programmatically Zoom to Extents
You can also run **Zoom to Extents** functionality programmatically without adding `SCIZoomExtentsModifier3D`.
The `SCICamera3D` object which is associated with `SCIChartSurface3D` provides the following methods which you can call whenever you need to zoom the chart to fit:
- `-[ISCICameraController zoomToFit]`
- `-[ISCICameraController animateZoomToFitWithDuration:]`
- `-[ISCICameraController zoomToFitWithResetPosition:]`
- `-[ISCICameraController animateZoomToFitWithResetPosition:andDuration:]`
- `-[ISCICameraController animatePosition:andTarget:withDuration:]`