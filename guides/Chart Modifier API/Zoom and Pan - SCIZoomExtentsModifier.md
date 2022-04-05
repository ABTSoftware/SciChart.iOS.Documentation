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