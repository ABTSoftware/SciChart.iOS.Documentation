# The Impulse (Stem) Series Type
Impulse Charts are provided by the `SCIFastImpulseRenderableSeries` class and is a hybrid of [Column Series](2d-chart-types---column-series.html) and [Scatter Series](2d-chart-types---scatter-series.html). It accepts data (`X, Y`) from a `SCIXyDataSeries` and draws a single line from **zeroLineY** to the `[X, Y]` value with an optional `ISCIPointMarker`.

> **_NOTE:_** Examples for the Impulse Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-impulse-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-impulse-stem-chart-example/)

Like the `SCIXyScatterRenderableSeries` type, `SCIFastImpulseRenderableSeries` requires a shape to be specified for the **Point Markers**. There are several shapes available out of the box, as well as it is possible to define custom shapes of the Point Markers. 

> **_NOTE:_** For the detailed description of **Point Markers**, please see the [PointMarkers API](pointmarker-api.html) article.

Similarly to the `SCIFastColumnRenderableSeries` type, it is possible to specify the **baseline** position for series' bars via the `SCIRenderableSeriesBase.zeroLineY` property. All data points that have Y value less than **ZeroLineY (baseline)** will be drawn downwards, else - upwards:

![Impulse Series Baseline](img/chart-types-2d/impulse-chart-baseline-example.png)

Finally, the stem color can be changed via the `ISCIRenderableSeries.strokeStyle` property.

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create an Impulse Series
To create an **Impulse Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a PointMarker instance
    id&lt;ISCIPointMarker&gt; pointMarker = [SCIEllipsePointMarker new];
    pointMarker.size = (CGSize){ .width = 15, .height = 15 };
    pointMarker.strokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.yellowColor thickness:2.0];
    pointMarker.fillStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0xFF0066FF];
    
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    
    // Create and add Scatter Series
    id&lt;ISCIRenderableSeries&gt; impulseSeries = [SCIXyScatterRenderableSeries new];
    impulseSeries.dataSeries = dataSeries;
    impulseSeries.pointMarker = pointMarker;
    impulseSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.yellowColor thickness:1.0];
    impulseSeries.zeroLineY = 0.2;
    [surface.renderableSeries add:impulseSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a PointMarker instance
    let pointMarker = SCIEllipsePointMarker()
    pointMarker.size = CGSize(width: 15, height: 15)
    pointMarker.strokeStyle = SCISolidPenStyle(color: .yellow, thickness: 2.0)
    pointMarker.fillStyle = SCISolidBrushStyle(colorCode: 0xFF0066FF)
    
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)
    
    // Create and add Scatter Series
    let impulseSeries = SCIFastImpulseRenderableSeries()
    impulseSeries.dataSeries = dataSeries
    impulseSeries.pointMarker = pointMarker
    impulseSeries.strokeStyle = SCISolidPenStyle(color: .yellow, thickness: 1.0)
    impulseSeries.zeroLineY = 0.2
    surface.renderableSeries.add(impulseSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a PointMarker instance
    var pointMarker = new SCIEllipsePointMarker();
    pointMarker.Size = new CGSize(15, 15);
    pointMarker.StrokeStyle = new SCISolidPenStyle(UIColor.Yellow, 2.0f);
    pointMarker.FillStyle = new SCISolidBrushStyle(0xFF0066FF);
    
    // Create DataSeries and fill it with some data
    var dataSeries = new XyDataSeries<double, double>();

    // Create and add Scatter Series
    var impulseSeries = new SCIFastLineRenderableSeries();
    impulseSeries.DataSeries = dataSeries;
    impulseSeries.PointMarker = pointMarker;
    impulseSeries.StrokeStyle = new SCISolidPenStyle(UIColor.Yellow, 1.0f);
    impulseSeries.ZeroLineY = 0.2;
    surface.RenderableSeries.Add(impulseSeries);
</div>

The result should be:

![Impulse Series Type](img/chart-types-2d/impulse-chart-example.png)

## Impulse Series Features
Impulse Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-impulse-series);
- [Draw Series With Different Colors](#specify-color-for-individual-bars).

#### Render a Gap in a Impulse Series
It's possible to render a Gap in an **Impulse series**, by passing a data point with a `NaN` as the `Y` value. Please refer to the [RenderableSeries APIs](2D Chart Types.html#adding-a-gap-onto-a-renderableseries) article for more details.

#### Specify Color for Individual Bars
In SciChart, you can draw each bar of the **Impulse Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for Impulse Series - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.
