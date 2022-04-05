# Pinch Zoom Modifier 3D
SciChart iOS 3D provides **pinch zooming** via the `SCIPinchZoomModifier3D`, which is available out of the box.

The `SCIPinchZoomModifier3D` performs movement of the `SCICamera3D` ***forwards/backwards*** when the user pinches a touch screen giving the appearance of zooming the 3D world.

<video autoplay loop muted playsinline src="img/modifiers-3d/pinch-zoom-modifier-3d.mp4"></video>

Besides [common features](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) which are inherited from the `SCIChartModifier3DBase` class, 
the `SCIPinchZoomModifier3D` also allows you to change **ScaleFactor** which allows you to change zooming speed. It is accessible via the `SCIPinchZoomModifier3D.scaleFactor` property.

## Adding a SCIPinchZoomModifier3D to a Chart
Any [Chart Modifier 3D](Chart Modifier 3D APIs.html) can be [added to a `SCIChartSurface3D`](Chart Modifier 3D APIs.html#adding-a-chart-modifier-3d) via the `ISCIChartSurface3D.chartModifiers` property and `SCIPinchZoomModifier3D` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface3D&gt; surface;

    // Create a Modifier
    SCIPinchZoomModifier3D *pinchZoomModifier3D = [SCIPinchZoomModifier3D new];
    pinchZoomModifier3D.scaleFactor = 1.5;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:pinchZoomModifier3D];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface3D

    // Create a Modifier
    let pinchZoomModifier3D = SCIPinchZoomModifier3D()
    pinchZoomModifier3D.scaleFactor = 1.5

    // Add the modifier to the surface
    self.surface.chartModifiers.add(pinchZoomModifier3D)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface3D surface;

    // Create a Modifier
    var pinchZoomModifier3D = new SCIPinchZoomModifier3D();
    pinchZoomModifier3D.scaleFactor = 1.5;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(pinchZoomModifier3D);
</div>

> **_NOTE:_** To learn more about features available, please read on the [Chart Modifier 3D APIs](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) article.
