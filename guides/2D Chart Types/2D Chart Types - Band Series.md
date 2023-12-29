# The Band Series Type
High-Low Fill or **Band Series** are provided by the `SCIFastBandRenderableSeries` type. This accepts data (`X, Y, Y1`) from a `SCIXyyDataSeries` and renders two lines with a polygon, which changes color depending on whether `Y > Y1` or vice versa.

> **_NOTE:_** For more info about `SCIXyyDataSeries`, as well as other DataSeries types in SciChart, see the [DataSeries API](dataseries-apis.html) article.

The **Band Series** can be used to render profit & loss (green / red above or below a zero line), shaded areas of interest, technical indicators such as MACD and Ichimoku, or to simply shade an area above or below a threshold.

![Band Series Type](img/chart-types-2d/band-chart-example.png)

> **_NOTE:_** Examples for the Band Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-band-series-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-band-chart-example/)

The `SCIFastBandRenderableSeries` class allows to specify **Fill**, **FillY1** brushes and **Stroke**, **StrokeY1** pens via the following properties:
- `SCIFastBandRenderableSeries.fillBrushStyle`
- `SCIFastBandRenderableSeries.fillY1BrushStyle`
- `ISCIRenderableSeries.strokeStyle`
- `SCIFastBandRenderableSeries.strokeY1Style`

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

### Digital (Step) Band Series
In addition to the above, `SCIFastBandRenderableSeries` can be configured to drawn as **Digital (Step) Band**. It is achieved via the 
`SCIFastBandRenderableSeries.isDigitalLine` property.

![Digital Band Series Type](img/chart-types-2d/digital-band-chart-example.png)

## Create a Band Series
To create a **Band Series**, use the following code:

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
    id&lt;ISCIRenderableSeries&gt; bandSeries = [SCIFastBandRenderableSeries new];
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
    let bandSeries = SCIFastBandRenderableSeries()
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
    var bandSeries = new SCIFastBandRenderableSeries();
    bandSeries.DataSeries = dataSeries;
    bandSeries.StrokeStyle = new SCISolidPenStyle(0xFFFF1919, 0.7f);
    bandSeries.StrokeY1Style = new SCISolidPenStyle(0xFF279B27, 0.7f;
    bandSeries.FillBrushStyle = new SCISolidBrushStyle(0x33279B27);
    bandSeries.FillY1BrushStyle = new SCISolidBrushStyle(0x33FF1919);
    surface.RenderableSeries.Add(bandSeries);
</div>

## Band Series Features
Band Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-band-series)
- [Draw Point Markers](#add-point-markers-onto-a-band-series)
- [Draw Series with Different Colors](#paint-band-area-parts-with-different-colors)

#### Render a Gap in a Band Series
It's possible to render a Gap in **Band series**, by passing a data point with a `NaN` as the `Y and Y1` value. Please refer to the [RenderableSeries APIs](2D Chart Types.html#adding-a-gap-onto-a-renderableseries) article for more details. The `SCIFastBandRenderableSeries`, however, allows to specify how a gap should appear. You can treat `NAN` values as **gaps** or close the line. That's defined by the `SCIRenderableSeriesBase.drawNaNAs` property (Please see `SCILineDrawMode` enumeration).

#### Add Point Markers onto a Band Series
Every data point of a **Band Series** can be marked with a `ISCIPointMarker`. To add Point Markers to a **Band Series** use the `ISCIRenderableSeries.pointMarker` property. For more information and code examples, please refer to the [PointMarkers API](pointmarker-api.html) article.

#### Paint Band Area Parts with Different Colors
In SciChart, you can draw **Band Series** segments with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for Band Series - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.
