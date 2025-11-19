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

## Additional Properties

### Restricting Interaction to a Single Direction (X or Y)
Interaction for this modifier can be limited to a specific axis direction by configuring the direction property.
This feature is especially useful in multiple-axis charts, where you may want to zoom only selected axes while keeping others fixed.
Set direction to one of the SCIDirection2D values to restrict the modifier to the X-axis, Y-axis, or both.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
pinchZoomModifier.direction = SCIDirection2D_XyDirection; // Allows interaction in both directions
pinchZoomModifier.direction = SCIDirection2D_XDirection;  // X-axis only
pinchZoomModifier.direction = SCIDirection2D_YDirection;  // Y-axis only
</div>
<div class="code-snippet" id="swift">
pinchZoomModifier.direction = .xyDirection    // Allows interaction in both directions
pinchZoomModifier.direction = .xDirection     // X-axis only
pinchZoomModifier.direction = .yDirection     // Y-axis only
</div>

### Include/Exclude Certain Axis from Pinch Zoom
The `SCIPinchZoomModifier` allows you to include or exclude certain axis from the pinch zoom operation.
By default all axis are included, to exclude one or more X or Y axis, set the following property:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Exclude a specific axis from the pan zoom operation
[pinchZoomModifier includeXAxis:xAxis isIncluded:NO];
[pinchZoomModifier includeYAxis:yAxis isIncluded:NO];

// Include a specific axis from the pan zoom operation
[pinchZoomModifier includeXAxis:xAxis isIncluded:YES];
[pinchZoomModifier includeYAxis:yAxis isIncluded:YES];

// Reset flags
[pinchZoomModifier includeAll];
[pinchZoomModifier excludeAll];
</div>
<div class="code-snippet" id="swift">
// Exclude a specific axis from the pan zoom operation
pinchZoomModifier.includeXAxis(xAxis, isIncluded: false)
pinchZoomModifier.includeYAxis(yAxis, isIncluded: false)

// Include a specific axis from the pan zoom operation
pinchZoomModifier.includeXAxis(xAxis, isIncluded: true)
pinchZoomModifier.includeYAxis(yAxis, isIncluded: true)

// Reset flags
pinchZoomModifier.includeAll()
pinchZoomModifier.excludeAll()
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.

## FAQ
**Q: How to keyboard zoom on +/- keys in macOS?**          
A: To implement zooming via keyboard on macOS using the + and - keys, override the `keyDown(with:)` method in your chart-hosting view or responder, detect the appropriate key input, compute a zoom scale, and update the visibleRange of your SCINumericAxis accordingly.
For more details, see the documentation:
[Zoom and Pan – Mouse Wheel and Trackpad Support](#zoom-and-pan--mouse-wheel-and-trackpad-support.html)

**Q: Whether chart content supports mouse-wheel zooming** 
A: We don't have a built in scroll modifier for macOS, but it would done by overriding `scrollWheel(with event: NSEvent)` method.
For more details, see the documentation:
[Zoom and Pan – Mouse Wheel and Trackpad Support](#zoom-and-pan--mouse-wheel-and-trackpad-support.html)