# The SCIPinchZoomModifier
SciChart iOS provides **pinch zooming** via the `SCIPinchZoomModifier`, available out of the box.

<video autoplay loop muted playsinline src="img/modifiers-2d/pinch-zoom-modifier.mp4"></video>

Besides [common features](Chart Modifier APIs.html#common-chart-modifier-features) which are inherited from the `SCIChartModifierBase` class, 
the `SCIPinchZoomModifier` allows to control its specific features via the following properties:
- `SCIPinchZoomModifier.scaleFactor` - allows to set **ScaleFactor** to change zooming speed.
- `SCIPinchZoomModifier.direction` - allows to **restrict zooming** to the horizontal or vertical **direction** only if needed.

## Adding a SCIPinchZoomModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the `ISCIChartSurface.chartModifiers` property and `SCIPinchZoomModifier` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier
    SCIPinchZoomModifier *pinchZoomModifier = [SCIPinchZoomModifier new];
    pinchZoomModifier.direction = SCIDirection2D_XDirection;
    pinchZoomModifier.scaleFactor = 1.5;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:pinchZoomModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier
    let pinchZoomModifier = SCIPinchZoomModifier()
    pinchZoomModifier.direction = .xDirection
    pinchZoomModifier.scaleFactor = 1.5

    // Add the modifier to the surface
    self.surface.chartModifiers.add(pinchZoomModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier
    var pinchZoomModifier = new SCIPinchZoomModifier();
    pinchZoomModifier.Direction = SCIDirection2D.XDirection;
    pinchZoomModifier.scaleFactor = 1.5;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(pinchZoomModifier);
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.
