# Vertex Selection Modifier 3D
The `SCIVertexSelectionModifier3D` allows you to perform ***selection of points*** on a 3D chart.

<video autoplay loop muted playsinline src="img/modifiers-3d/vertex-selection-modifier-3d.mp4"></video>

## Adding a SCIVertexSelectionModifier3D to a Chart
Any [Chart Modifier 3D](Chart Modifier 3D APIs.html) can be [added to a `SCIChartSurface3D`](Chart Modifier 3D APIs.html#adding-a-chart-modifier-3d) via the `ISCIChartSurface3D.chartModifiers` property and `SCIVertexSelectionModifier3D` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [self.surface.chartModifiers add:[SCIVertexSelectionModifier3D new]];
</div>
<div class="code-snippet" id="swift">
    self.surface.chartModifiers.add(SCIVertexSelectionModifier3D())
</div>
<div class="code-snippet" id="cs">
    Surface.ChartModifiers.Add(new SCIVertexSelectionModifier3D());
</div>

> **_NOTE:_** To learn more about features available, visit the [Chart Modifier 3D APIs](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) article.
