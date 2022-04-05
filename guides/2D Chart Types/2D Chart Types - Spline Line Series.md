# The Spline Line Series Type
**Spline Line Series** can be created using the `SCISplineLineRenderableSeries` type.

![Spline Line Series Type](img/chart-types-2d/spline-line-chart.png)

> **_NOTE:_** Examples of the Spline Line Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
>
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-spline-line-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-spline-line-chart/)

## Create a Spline Line Series
To create a `SCISplineLineRenderableSeries`, use the following code:

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

    // Create and add Spline Line Series
    id&lt;ISCIRenderableSeries&gt; lineSeries = [SCISplineLineRenderableSeries new];
    lineSeries.dataSeries = dataSeries;
    lineSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:2.0];
    [surface.renderableSeries add:lineSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)

    // Create and add Spline Line Series
    let lineSeries = SCISplineLineRenderableSeries()
    lineSeries.dataSeries = dataSeries
    lineSeries.strokeStyle = SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 2.0)
    surface.renderableSeries.add(lineSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new XyDataSeries<double, double>();

    // Create and add Spline Line Series
    var lineSeries = new SCISplineLineRenderableSeries();
    lineSeries.DataSeries = dataSeries;
    lineSeries.StrokeStyle = new SCISolidPenStyle(0xFF279B27, 2.0f);
    surface.RenderableSeries.Add(lineSeries);
</div>

In the code above, a **Spline Line Series** instance is created. It is assigned to draw the data that is provided by the `ISCIDataSeries` assigned to it. The line is drawn with a **Pen** provided by the `SCIPenStyle` instance. Finally, the **Spline Line Series** is added to the `ISCIChartSurface.renderableSeries` property.

## Spline Line Series Features
Spline Line Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-spline-line-series)
- [Draw Point Markers](#add-point-markers-onto-a-spline-line-series)
- [Draw Series With Different Colors](#paint-spline-line-segments-with-different-colors)

#### Render a Gap in a Spline Line Series
It is possible to show a gap in a **Spline Line Series** by passing a data point with a `NaN` as the Y value. Please refer to the [RenderableSeries APIs](renderableseries-apis.html#adding-a-gap-onto-a-renderableseries) article for more details. The `SCISplineLineRenderableSeries`, however, allows to specify how a gap should appear. You can treat `NAN` values as **gaps** or close the line. That's defined by the `SCIRenderableSeriesBase.drawNaNAs` property (Please see `SCILineDrawMode` enumeration).

> **_NOTE:_** Please note, even though Gaps via NaN values in spline series is supported, ClosedGaps feature, which is available in [regular (non-spline)](2d-chart-types---line-series.html) series, aren't supported with splines.

#### Add Point Markers onto a Spline Line Series
Every data point of a **Spline Line Series** can be marked with a `ISCIPointMarker`. To add Point Markers to the Spline Line Series, use the following code:

![Point Markers](img/chart-types-2d/spline-point-markers-example.png)

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
    // Apply the PointMarker to a Spline Line Series
    id&lt;ISCIRenderableSeries&gt; lineSeries = [SCISplineLineRenderableSeries new];
    lineSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    // Create a Triangle PointMarker instance
    let pointMarker = SCITrianglePointMarker()
    pointMarker.size = CGSize(width: 40, height: 40)
    pointMarker.strokeStyle = SCISolidPenStyle(color: .green, thickness: 2.0)
    pointMarker.fillStyle = SCISolidBrushStyle(color: .red)
    // Apply the PointMarker to a Spline Line Series
    let lineSeries = SCISplineLineRenderableSeries()
    lineSeries.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    // Create a Triangle PointMarker instance
    var pointMarker = new SCITrianglePointMarker();
    pointMarker.Size = new CGSize(40, 40);
    pointMarker.StrokeStyle = new SCISolidPenStyle(UIColor.Green, 2.0f);
    pointMarker.FillStyle = new SCISolidBrushStyle(UIColor.Red);
    // Apply the PointMarker to a Spline Line Series
    var lineSeries = new SCISplineLineRenderableSeries();
    lineSeries.PointMarker = pointMarker;
</div>

To learn more about **Point Markers**, please refer to the [PointMarkers API](pointmarker-api.html) article.

> **_NOTE:_** This feature can be used to create a [Scatter Series](2d-chart-types---scatter-series.html), if `ISCIRenderableSeries.strokeStyle` contains a **transparent Pen**.

#### Paint Spline Line Segments With Different Colors
In SciChart, you can draw line segments with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for **Spline Line Series** - a custom `ISCIStrokePaletteProvider` has to be provided to the `ISCIRenderableSeries.paletteProvider` property. For more information - please refer to the [PaletteProvider API](palette-provider-api.html) article.
