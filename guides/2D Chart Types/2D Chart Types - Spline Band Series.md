# The Spline Band Series Type
**Spline Band Series** are provided by the `SCISplineBandRenderableSeries` type. This accepts data (`X, Y, Y1`) from a `SCIXyyDataSeries` and renders two lines with a polygon, which changes color depending on whether `Y > Y1` or vice versa.

> **_NOTE:_** For more info about `SCIXyyDataSeries`, as well as other DataSeries types in SciChart, see the [DataSeries API](dataseries-apis.html) article.

The **Spline Band Series** can be used to render profit & loss (green / red above or below a zero line), shaded areas of interest, technical indicators such as MACD and Ichimoku, or to simply shade an area above or below a threshold.

![Spline Band Series Type](img/chart-types-2d/spline-band-chart.png)

> **_NOTE:_** Examples for the Spline Band Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-spline-band-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-spline-band-chart-example/)

The `SCISplineBandRenderableSeries` class allows to specify **Fill**, **FillY1** brushes and **Stroke**, **StrokeY1** pens via the following properties:
- `SCISplineBandRenderableSeries.fillBrushStyle`
- `SCISplineBandRenderableSeries.fillY1BrushStyle`
- `ISCIRenderableSeries.strokeStyle`
- `SCISplineBandRenderableSeries.strokeY1Style`

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create a Spline Band Series
To create a **Spline Band Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *dataSeries = [[SCIXyyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];

    // Create and add Mountain Series
    id&lt;ISCIRenderableSeries&gt; bandSeries = [SCISplineBandRenderableSeries new];
    bandSeries.dataSeries = dataSeries;
    bandSeries.fillBrushStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0x33279B27];
    bandSeries.fillY1BrushStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0x33FF1919];
    bandSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:1.0];
    bandSeries.strokeY1Style = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFF1919 thickness:1.0];
    [surface.renderableSeries add:bandSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyyDataSeries(xType: .double, yType: .double)

    // Create and add Mountain Series
    let bandSeries = SCISplineBandRenderableSeries()
    bandSeries.dataSeries = dataSeries
    bandSeries.fillBrushStyle = SCISolidBrushStyle(colorCode: 0x33279B27)
    bandSeries.fillY1BrushStyle = SCISolidBrushStyle(colorCode: 0x33FF1919)
    bandSeries.strokeStyle =  SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 1.0)
    bandSeries.strokeY1Style = SCISolidPenStyle(colorCode: 0xFFFF1919, thickness: 1.0)
    surface.renderableSeries.add(bandSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new XyyDataSeries<double, double>();
    
    // Create and add Mountain Series
    var bandSeries = new SCISplineBandRenderableSeries();
    bandSeries.DataSeries = dataSeries;
    bandSeries.StrokeStyle = new SCISolidPenStyle(0xFFFF1919, 0.7f);
    bandSeries.StrokeY1Style = new SCISolidPenStyle(0xFF279B27, 0.7f;
    bandSeries.FillBrushStyle = new SCISolidBrushStyle(0x33279B27);
    bandSeries.FillY1BrushStyle = new SCISolidBrushStyle(0x33FF1919);
    surface.RenderableSeries.Add(bandSeries);
</div>

## Spline Band Series Features
Spline Band Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-spline-band-series)
- [Draw Point Markers](#add-point-markers-onto-a-spline-band-series)
- [Draw Series with Different Colors](#paint-spline-band-area-parts-with-different-colors)

#### Render a Gap in a Spline Band Series
It's possible to render a Gap in **Spline Band series**, by passing a data point with a `NaN` as the `Y and Y1` value. Please refer to the [RenderableSeries APIs](2D Chart Types.html#adding-a-gap-onto-a-renderableseries) article for more details. The `SCISplineBandRenderableSeries`, however, allows to specify how a gap should appear. You can treat `NAN` values as **gaps** or close the line. That's defined by the `SCIRenderableSeriesBase.drawNaNAs` property (Please see `SCILineDrawMode` enumeration).

> **_NOTE:_** Please note, even though Gaps via NaN values in spline series is supported, ClosedGaps feature, which is available in [regular (non-spline)](2d-chart-types---band-series.html) series, aren't supported with splines.

#### Add Point Markers onto a Spline Band Series
Every data point of a **Spline Band Series** can be marked with a `ISCIPointMarker`. To add Point Markers to a **Spline Band Series** use the `ISCIRenderableSeries.pointMarker` property. For more information and code examples, please refer to the [PointMarkers API](pointmarker-api.html) article.

#### Paint Spline Band Area Parts with Different Colors
In SciChart, you can draw **Spline Band Series** segments with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for Spline Band Series - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.