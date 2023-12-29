# The Line Series Type
**Line Series** can be created using the `SCIFastLineRenderableSeries` type.

![Line Series Type](img/chart-types-2d/line-chart-example.png)

> **_NOTE:_** Examples of the Line Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-line-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-line-chart-example/)

### Digital (Step) Line Series
`SCIFastLineRenderableSeries` can be configured to draw as **Digital (Step) Line**. It is achieved via the 
`SCIFastLineRenderableSeries.isDigitalLine` property.

![Digital Line Series Type](img/chart-types-2d/digital-line-chart-example.png)

## Create a Line Series
To create a `SCIFastLineRenderableSeries`, use the following code:

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

    // Create and add Line Series
    id&lt;ISCIRenderableSeries&gt; lineSeries = [SCIFastLineRenderableSeries new];
    lineSeries.dataSeries = dataSeries;
    lineSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:2.0];
    [surface.renderableSeries add:lineSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)

    // Create and add Line Series
    let lineSeries = SCIFastLineRenderableSeries()
    lineSeries.dataSeries = dataSeries
    lineSeries.strokeStyle = SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 2.0)
    surface.renderableSeries.add(lineSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new XyDataSeries<double, double>();

    // Create and add Line Series
    var lineSeries = new SCIFastLineRenderableSeries();
    lineSeries.DataSeries = dataSeries;
    lineSeries.StrokeStyle = new SCISolidPenStyle(0xFF279B27, 2.0f);
    surface.RenderableSeries.Add(lineSeries);
</div>

In the code above, a **Line Series** instance is created. It is assigned to draw the data that is provided by the `ISCIDataSeries` assigned to it. The line is drawn with a **Pen** provided by the `SCIPenStyle` instance. Finally, the **Line Series** is added to the `ISCIChartSurface.renderableSeries` property.

## Line Series Features
Line Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-line-series)
- [Draw Point Markers](#add-point-markers-onto-a-line-series)
- [Draw Series With Different Colors](#paint-line-segments-with-different-colors)

#### Render a Gap in a Line Series
It is possible to show a gap in a **Line Series** by passing a data point with a `NaN` as the Y value. Please refer to the [RenderableSeries APIs](2D Chart Types.html#adding-a-gap-onto-a-renderableseries) article for more details. The `SCIFastLineRenderableSeries`, however, allows to specify how a gap should appear. You can treat `NAN` values as **gaps** or close the line. That's defined by the `SCIRenderableSeriesBase.drawNaNAs` property (Please see `SCILineDrawMode` enumeration).

#### Add Point Markers onto a Line Series
Every data point of a **Line Series** can be marked with a `ISCIPointMarker`. To add Point Markers to the Line Series, use the following code:

![Point Markers](img/chart-types-2d/point-markers-example.png)

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create a Triangle PointMarker instance
    id&lt;ISCIPointMarker&gt; pointMarker = [SCITrianglePointMarker new];
    pointMarker.size = (CGSize){ .width = 40, .height = 40 };
    pointMarker.strokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.greenColor thickness:2.0];
    pointMarker.fillStyle = [[SCISolidBrushStyle alloc] initWithColor:UIColor.redColor];
    // Apply the PointMarker to a LineSeries
    id&lt;ISCIRenderableSeries&gt; lineSeries = [SCIFastLineRenderableSeries new];
    lineSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    // Create a Triangle PointMarker instance
    let pointMarker = SCITrianglePointMarker()
    pointMarker.size = CGSize(width: 40, height: 40)
    pointMarker.strokeStyle = SCISolidPenStyle(color: .green, thickness: 2.0)
    pointMarker.fillStyle = SCISolidBrushStyle(color: .red)
    // Apply the PointMarker to a LineSeries
    let lineSeries = SCIFastLineRenderableSeries()
    lineSeries.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    // Create a Triangle PointMarker instance
    var pointMarker = new SCITrianglePointMarker();
    pointMarker.Size = new CGSize(40, 40);
    pointMarker.StrokeStyle = new SCISolidPenStyle(UIColor.Green, 2.0f);
    pointMarker.FillStyle = new SCISolidBrushStyle(UIColor.Red);
    // Apply the PointMarker to a LineSeries
    var lineSeries = new SCIFastLineRenderableSeries();
    lineSeries.PointMarker = pointMarker;
</div>

To learn more about **Point Markers**, please refer to the [PointMarkers API](pointmarker-api.html) article.

> **_NOTE:_** This feature can be used to create a [Scatter Series](2d-chart-types---scatter-series.html), if `ISCIRenderableSeries.strokeStyle` contains a **transparent Pen**.

#### Paint Line Segments With Different Colors
In SciChart, you can draw line segments with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for **Line Series** - a custom `ISCIStrokePaletteProvider` has to be provided to the `ISCIRenderableSeries.paletteProvider` property. For more information - please refer to the [PaletteProvider API](paletteprovider-api.html) article.
