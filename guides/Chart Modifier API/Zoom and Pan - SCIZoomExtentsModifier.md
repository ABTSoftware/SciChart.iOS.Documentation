# The SCIZoomExtentsModifier
SciChart iOS provides the ability to **zoom-to-fit** the entire chart double-tapping it via the `SCIZoomExtentsModifier`, available out of the box.

<video autoplay loop muted playsinline src="img/modifiers-2d/zoom-extents-modifier.mp4"></video>

Besides [common features](Chart Modifier APIs.html#common-chart-modifier-features) which are inherited from the `SCIChartModifierBase` class, 
the `SCIZoomExtentsModifier` allows to control its specific features via the following properties:
- `SCIZoomExtentsModifier.direction` - allows to **restrict zooming** to the horizontal or vertical **direction** only if needed.
- `SCIZoomExtentsModifier.isAnimated` - allows to switch **on/off** the animation on zoom out.
- `SCIZoomExtentsModifier.executeOn` - allows to specify the trigger action for the modifier via the `SCIExecuteOn` enumeration.

> **_NOTE:_** There are several modes defined by the `SCIExecuteOn` enumeration, such as **Single Tap, Double Tap, Long Press**, and **Fling**.

## Adding a SCIZoomExtentsModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the `ISCIChartSurface.chartModifiers` property and `SCIZoomExtentsModifier` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier
    SCIZoomExtentsModifier *zoomExtentsModifier = [SCIZoomExtentsModifier new];
    zoomExtentsModifier.direction = SCIDirection2D_XDirection;
    zoomExtentsModifier.executeOn = SCIExecuteOn_DoubleTap;
    zoomExtentsModifier.isAnimated = YES;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:zoomExtentsModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier
    let zoomExtentsModifier = SCIZoomExtentsModifier()
    zoomExtentsModifier.direction = .xDirection
    zoomExtentsModifier.executeOn = .doubleTap
    zoomExtentsModifier.isAnimated = true

    // Add the modifier to the surface
    self.surface.chartModifiers.add(zoomExtentsModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier
    var zoomExtentsModifier = new SCIZoomExtentsModifier();
    zoomExtentsModifier.Direction = SCIDirection2D.XDirection;
    zoomExtentsModifier.ExecuteOn = SCIExecuteOn.DoubleTap;
    zoomExtentsModifier.IsAnimated = true;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(zoomExtentsModifier);
</div>

## Additional Properties

### Restricting Interaction to a Single Direction (X or Y)
Interaction for this modifier can be limited to a specific axis direction by configuring the direction property.
Set direction to one of the SCIDirection2D values to restrict the modifier to the X-axis, Y-axis, or both.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
zoomExtentsModifier.direction = SCIDirection2D_XyDirection; // Allows interaction in both directions
zoomExtentsModifier.direction = SCIDirection2D_XDirection;  // X-axis only
zoomExtentsModifier.direction = SCIDirection2D_YDirection;  // Y-axis only
</div>
<div class="code-snippet" id="swift">
zoomExtentsModifier.direction = .xyDirection    // Allows interaction in both directions
zoomExtentsModifier.direction = .xDirection     // X-axis only
zoomExtentsModifier.direction = .yDirection     // Y-axis only
</div>

### Include/Exclude Certain Axis from Zoom Extent
The `SCIZoomExtentsModifier` allows you to include or exclude certain axis from the zoom entent operation.
This feature is especially useful in charts with multiple axes, where you may want to zoom only selected axes while keeping others unchanged.
By default all axis are included, to exclude one or more X or Y axis, set the following property:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Exclude a specific axis from the pan zoom operation
[zoomExtentsModifier includeXAxis:xAxis isIncluded:NO];
[zoomExtentsModifier includeYAxis:yAxis isIncluded:NO];

// Include a specific axis from the pan zoom operation
[zoomExtentsModifier includeXAxis:xAxis isIncluded:YES];
[zoomExtentsModifier includeYAxis:yAxis isIncluded:YES];

// Reset flags
[zoomExtentsModifier includeAll];
[zoomExtentsModifier excludeAll];
</div>
<div class="code-snippet" id="swift">
// Exclude a specific axis from the pan zoom operation
zoomExtentsModifier.includeXAxis(xAxis, isIncluded: false)
zoomExtentsModifier.includeYAxis(yAxis, isIncluded: false)

// Include a specific axis from the pan zoom operation
zoomExtentsModifier.includeXAxis(xAxis, isIncluded: true)
zoomExtentsModifier.includeYAxis(yAxis, isIncluded: true)

// Reset flags
zoomExtentsModifier.includeAll()
zoomExtentsModifier.excludeAll()
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.

## Programmatically Zoom to Extents
You can also run **Zoom to Extents** functionality programmatically without adding `SCIZoomExtentsModifier`. 
`SCIChartSurface` supports the following methods which you can call whenever you need to zoom the chart to fit:
- `-[ISCIChartController zoomExtents]`
- `-[ISCIChartController animateZoomExtentsWithDuration:]`
- `-[ISCIChartController zoomExtentsY]`
- `-[ISCIChartController animateZoomExtentsYWithDuration:]`
- `-[ISCIChartController zoomExtentsX]`
- `-[ISCIChartController animateZoomExtentsXWithDuration:]`