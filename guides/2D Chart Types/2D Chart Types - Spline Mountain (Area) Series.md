# The Spline Mountain (Area) Series Type
**Spline Mountain (Area) Series** can be created using the `SCISplineMountainRenderableSeries` type.

![Spline Mountain Series Type](img/chart-types-2d/spline-mountain-chart.png)

> **_NOTE:_** Examples of the Spline Mountain Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
>
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-spline-mountain-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-spline-mountain-chart-example/)

The `SCISplineMountainRenderableSeries` class allows to specify **Stroke** pen and **Area** brush. Those values can be assigned through the corresponding properties - `ISCIRenderableSeries.strokeStyle` and `SCIBaseMountainRenderableSeries.areaStyle` accordingly.

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

It is possible to define the **ZeroLineY** baseline position for a Spline Mountain Series via the `SCIRenderableSeriesBase.zeroLineY` property. All data points that have Y value less than **ZeroLineY** will appear downward, else - upward.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to a **particular X and Y axes**. This can be done by passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create a Spline Mountain Series
To create a **Spline Mountain Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];

    // Create and add Mountain Series
    id&lt;ISCIRenderableSeries&gt; mountainSeries = [SCISplineMountainRenderableSeries new];
    mountainSeries.dataSeries = dataSeries;
    mountainSeries.areaStyle = [[SCILinearGradientBrushStyle alloc] initWithStart:CGPointZero end:CGPointMake(0, 1) startColorCode:0xAAFF8D42 endColorCode:0x88090E11];
    mountainSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:2.0];
    [surface.renderableSeries add:mountainSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)

    // Create and add Mountain Series
    let mountainSeries = SCISplineMountainRenderableSeries()
    mountainSeries.dataSeries = dataSeries
    mountainSeries.areaStyle = SCILinearGradientBrushStyle(start: CGPoint(x: 0.5, y: 0.0), end: CGPoint(x: 0.5, y: 1.0), startColorCode: 0xAAFF8D42, endColorCode: 0x88090E11)
    mountainSeries.strokeStyle = SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 2.0)
    surface.renderableSeries.add(mountainSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new XyDataSeries<double, double>();
    
    // Create and add Mountain Series
    var mountainSeries = new SCISplineMountainRenderableSeries();
    mountainSeries.DataSeries = dataSeries;
    mountainSeries.AreaStyle = new SCILinearGradientBrushStyle(new CGPoint(0.5, 0), new CGPoint(0.5, 1), 0xAAFF8D42, 0x88090E11),
    mountainSeries.StrokeStyle = new SCISolidPenStyle(0xFF279B27, 2.0f);
    surface.RenderableSeries.Add(mountainSeries);
</div>

## Spline Mountain Series Features
Spline Mountain Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-spline-mountain-series);
- [Draw Point Markers](#add-point-markers-onto-a-spline-mountain-series);
- [Draw Series with Different Colors](#paint-area-parts-with-different-colors).

#### Render a Gap in a Spline Mountain Series
It's possible to render a Gap in **Spline Mountain series**, by passing a data point with a `NaN` as the Y value. Please refer to the [RenderableSeries APIs](renderableseries-apis.html#adding-a-gap-onto-a-renderableseries) article for more details. The `SCISplineMountainRenderableSeries`, itself, allows to specify how a gap would appear. You can treat `NAN` values as a **gap** or a **close the line**. That appearance is defined by the `SCIRenderableSeriesBase.drawNaNAs` property (Please see `SCILineDrawMode` enumeration).

> **_NOTE:_** Please note, even though Gaps via NaN values in spline series is supported, ClosedGaps feature, which is available in [regular (non-spline)](2d-chart-types---mountain-area-series.html) series, aren't supported with splines.

#### Add Point Markers onto a Spline Mountain Series
Every data point of a **Mountain Series** can be marked with a `ISCIPointMarker`. To add Point Markers to a **Spline Mountain Series** use the `ISCIRenderableSeries.pointMarker` property. For more information and code examples, please refer to the [PointMarkers API](pointmarker-api.html) article.

#### Paint Area Parts with Different Colors
In SciChart, you can draw Area Parts of the **Spline Mountain Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for Spline Mountain Area - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.