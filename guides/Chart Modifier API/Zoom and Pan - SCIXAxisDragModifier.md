# The SCIXAxisDragModifier
SciChart iOS provides **scale or pan** an X Axis via the `SCIXAxisDragModifier`, available out of the box.

<video autoplay loop muted playsinline src="img/modifiers-2d/x-axis-drag-modifier.mp4"></video>

Besides [common features](Chart Modifier APIs.html#common-chart-modifier-features) which are inherited from the `SCIChartModifierBase` class, 
the `SCIXAxisDragModifier` allows to control its specific features via the following properties:
- `SCIAxisDragModifierBase.dragMode` - allows to change the default axis **scaling** behavior to axis **panning** behavior - similarly to [SCIZoomPanModifier](zoom-and-pan---scizoompanmodifier.html) via the `SCIAxisDragMode` enumeration.
- `SCIAxisDragModifierBase.minTouchArea` - configures the **sensitivity** of the modifier.
- `SCIXAxisDragModifier.clipModeX` - allows to specify the **behavior** when scrolling **reaches data extents** in X direction via the `SCIClipMode` enumeration.
- `SCIXAxisDragModifier.clipModeTargetX` - allows to specify which target is used as limit by `clipModeX` when you reach the edge of the `X-Axis` extents.

## Adding a SCIXAxisDragModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the `ISCIChartSurface.chartModifiers` property and `SCIXAxisDragModifier` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier
    SCIXAxisDragModifier *xAxisDragModifier = [SCIXAxisDragModifier new];
    xAxisDragModifier.dragMode = SCIAxisDragMode_Pan;
    xAxisDragModifier.clipModeX = SCIClipMode_StretchAtExtents;
    xAxisDragModifier.clipModeTargetX = SCIClipModeTarget_MaximumRange;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:xAxisDragModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier
    let xAxisDragModifier = SCIXAxisDragModifier()
    xAxisDragModifier.dragMode = .pan
    xAxisDragModifier.clipModeX = .stretchAtExtents
    xAxisDragModifier.clipModeTargetX = .maximumRange

    // Add the modifier to the surface
    self.surface.chartModifiers.add(xAxisDragModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier
    var xAxisDragModifier = new SCIXAxisDragModifier();
    xAxisDragModifier.DragMode = SCIAxisDragMode.Pan;
    xAxisDragModifier.ClipModeX = SCIClipMode.StretchAtExtents;
    xAxisDragModifier.ClipModeTargetX = SCIClipModeTarget.MaximumRange;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(xAxisDragModifier);
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.
