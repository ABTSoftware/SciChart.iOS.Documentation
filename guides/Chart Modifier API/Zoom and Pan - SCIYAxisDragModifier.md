# The SCIYAxisDragModifier
SciChart iOS provides **scale or pan** an Y Axis via the `SCIYAxisDragModifier`, available out of the box.

<video autoplay loop muted playsinline src="img/modifiers-2d/y-axis-drag-modifier.mp4"></video>

Besides [common features](Chart Modifier APIs.html#common-chart-modifier-features) which are inherited from the `SCIChartModifierBase` class, 
the `SCIYAxisDragModifier` allows to control its specific features via the following properties:
- `SCIAxisDragModifierBase.dragMode` - allows to change the default axis **scaling** behavior to axis **panning** behavior - similarly to [SCIZoomPanModifier](zoom-and-pan---scizoompanmodifier.html) via the `SCIAxisDragMode` enumeration.
- `SCIAxisDragModifierBase.minTouchArea` - configures the **sensitivity** of the modifier.

## Adding a SCIYAxisDragModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the `ISCIChartSurface.chartModifiers` property and `SCIYAxisDragModifier` is no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier
    SCIYAxisDragModifier *yAxisDragModifier = [SCIYAxisDragModifier new];
    yAxisDragModifier.dragMode = SCIAxisDragMode_Pan;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:yAxisDragModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier
    let yAxisDragModifier = SCIYAxisDragModifier()
    yAxisDragModifier.dragMode = .pan

    // Add the modifier to the surface
    self.surface.chartModifiers.add(yAxisDragModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier
    var yAxisDragModifier = new SCIYAxisDragModifier();
    yAxisDragModifier.dragMode = SCIAxisDragMode.Pan;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(yAxisDragModifier);
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.
