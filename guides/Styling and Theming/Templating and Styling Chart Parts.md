# Templating and Styling Chart Parts
Most of the parts within SciChart can be template and styled independently of [Themes](Styling and Theming.html)

![Styling Chart Example](img/styling-and-theming/styling-chart-example.png)

> **_NOTE:_** **Styling Chart** example can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-style-theme-chart-elements-in-code/)

## Styling the Chart Viewport
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // self.surface background. If you set color for chart area than it is color only for axes area
    self.surface.backgroundColor = UIColor.orangeColor;
    // chart area background fill color
    self.surface.renderableSeriesAreaFillStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0xFFFFB6C1];
    // chart area border color and thickness
    self.surface.renderableSeriesAreaBorderStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF4682b4 thickness:2];
</div>
<div class="code-snippet" id="swift">
    // surface background. If you set color for chart background than it is color only for axes area
    surface.backgroundColor = .orange
    // chart area (viewport) background fill color
    surface.renderableSeriesAreaFillStyle = SCISolidBrushStyle(colorCode: 0xFFFFB6C1)
    // chart area border color and thickness
    surface.renderableSeriesAreaBorderStyle = SCISolidPenStyle(colorCode: 0xFF4682b4, thickness: 2)
</div>
<div class="code-snippet" id="cs">
    // Surface background. If you set color for chart background than it is color only for axes area
    Surface.BackgroundColor = UIColor.Orange;
    // Chart area (viewport) background fill color
    Surface.RenderableSeriesAreaFillStyle = new SCISolidBrushStyle(colorCode: 0xFFFFB6C1);
    // Chart area border color and thickness
    Surface.RenderableSeriesAreaBorderStyle = new SCISolidPenStyle(colorCode: 0xFF4682b4, thickness: 2);
</div>

## Styling Axis
**Each and Every** aspect of the axis can be styled. The Axis is responsible for drawing the following parts:
- [Title](axis-styling---title-and-labels#axis-title)
- [Axis Labels](axis-styling---title-and-labels#axis-labels)
- [Tick Lines](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-ticks) - small marks on the outside of an axis **next to labels**
- [Gridlines](axis-styling---grid-lines-ticks-and-axis-bands.html#grid-lines) - major and minor
- [Axis Bands](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-bands) - shading between the **major** gridlines

> **_NOTE:_** You can read more about axis styling in the following articles: 
> - [Axis Styling - Title and Labels](axis-styling---title-and-labels.html) 
> - [Axis Styling - Grid Lines, Ticks and Axis Bands](axis-styling---grid-lines-ticks-and-axis-bands.html)