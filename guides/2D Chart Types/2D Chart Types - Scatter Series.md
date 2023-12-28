# The Scatter Series Type
**Scatter Series** can be created using the `SCIXyScatterRenderableSeries` type.

![Scatter Series Type](img/chart-types-2d/scatter-chart-example.png)

> **_NOTE:_** Examples for the Scatter Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-scatter-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-scatter-chart-example/)

The Scatter Series requires a shape to be specified for **Point Markers**. SciChart provides several shapes out of the box:
- `SCICrossPointMarker`; 
- `SCIEllipsePointMarker`; 
- `SCISquarePointMarker`;
- `SCITrianglePointMarker`;
- `SCISpritePointMarker`.

It is also possible to define custom shapes of the Point Markers. Please refer to the [PointMarkers API](pointmarker-api.html) article to learn more. You can also override colors of the **Point Markers** individually using [The PaletteProvider API](paletteprovider-api.html).

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create a Scatter Series
To create a `SCIXyScatterRenderableSeries`, use the following code:

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
    pointMarker.size = (CGSize){ .width = 40, .height = 40 };
    pointMarker.strokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.greenColor thickness:2.0];
    pointMarker.fillStyle = [[SCISolidBrushStyle alloc] initWithColor:UIColor.redColor];
    
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    
    // Create and add Scatter Series
    id&lt;ISCIRenderableSeries&gt; scatterSeries = [SCIXyScatterRenderableSeries new];
    scatterSeries.dataSeries = dataSeries;
    scatterSeries.pointMarker = pointMarker;
    [surface.renderableSeries add:scatterSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a PointMarker instance
    let pointMarker = SCIEllipsePointMarker()
    pointMarker.size = CGSize(width: 40, height: 40)
    pointMarker.strokeStyle = SCISolidPenStyle(color: .green, thickness: 2.0)
    pointMarker.fillStyle = SCISolidBrushStyle(color: .red)
    
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)
    
    // Create and add Scatter Series
    let scatterSeries = SCIXyScatterRenderableSeries()
    scatterSeries.pointMarker = pointMarker
    scatterSeries.dataSeries = dataSeries
    surface.renderableSeries.add(scatterSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a PointMarker instance
    var pointMarker = new SCIEllipsePointMarker();
    pointMarker.Size = new CGSize(40, 40);
    pointMarker.StrokeStyle = new SCISolidPenStyle(UIColor.Green, 2.0f);
    pointMarker.FillStyle = new SCISolidBrushStyle(UIColor.Red);
    
    // Create DataSeries and fill it with some data
    var dataSeries = new XyDataSeries<double, double>();

    // Create and add Scatter Series
    var scatterSeries = new SCIFastLineRenderableSeries();
    scatterSeries.DataSeries = dataSeries;
    scatterSeries.PointMarker = pointMarker;
    surface.RenderableSeries.Add(scatterSeries);
</div>

In the code above, a **Scatter Series** instance is created. It is assigned to draw the data that is provided by the `ISCIDataSeries` assigned to it. The Scatters are drawn with a **PointMarker** provided by the `SCIEllipsePointMarker` instance. Finally, the **Scatter Series** is added to the `ISCIChartSurface.renderableSeries` property.

## Scatter Series Features
Scatter Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-scatter-series).
- [Draw Series With Different Colors](#paint-scatters-with-different-colors).

#### Render a Gap in a Scatter Series
It's possible to render a Gap in **Scatter series**, by passing a data point with a `NaN` as the `Y` value. Please refer to the [RenderableSeries APIs](renderableseries-apis.html#adding-a-gap-onto-a-renderableseries) article for more details.

#### Paint Scatters With Different Colors
In SciChart, you can draw each scatter of the **Scatter Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for scatters - a custom `ISCIPointMarkerPaletteProvider` has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.
