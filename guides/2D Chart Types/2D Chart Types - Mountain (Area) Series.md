# The Mountain (Area) Series Type
**Mountain (Area) Series** can be created using the `SCIFastMountainRenderableSeries` type.

![Mountain Series Type](img/chart-types-2d/mountain-chart-example.png)

> **_NOTE:_** Examples of the Mountain Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-mountain-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-mountain-chart-example/)

The `SCIFastMountainRenderableSeries` class allows to specify **Stroke** pen and **Area** brush. Those values can be assigned through the corresponding properties - `ISCIRenderableSeries.strokeStyle` and `SCIBaseMountainRenderableSeries.areaStyle` accordingly.

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

It is possible to define the **ZeroLineY** baseline position for a Mountain Series via the `SCIRenderableSeriesBase.zeroLineY` property. All data points that have Y value less than **ZeroLineY** will appear downward, else - upward.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to a **particular X and Y axes**. This can be done by passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

### Digital (Step) Mountain Series
In addition to the above, `SCIFastMountainRenderableSeries` can be configured to draw as **Digital (Step) Mountain**. It is achieved via the 
`SCIBaseMountainRenderableSeries.isDigitalLine` property.

![Digital Mountain Series Type](img/chart-types-2d/digital-mountain-chart-example.png)

## Create a Mountain Series
To create a **Mountain Series**, use the following code:

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
    id&lt;ISCIRenderableSeries&gt; mountainSeries = [SCIFastMountainRenderableSeries new];
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
    let mountainSeries = SCIFastMountainRenderableSeries()
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
    var mountainSeries = new SCIFastMountainRenderableSeries();
    mountainSeries.DataSeries = dataSeries;
    mountainSeries.AreaStyle = new SCILinearGradientBrushStyle(new CGPoint(0.5, 0), new CGPoint(0.5, 1), 0xAAFF8D42, 0x88090E11),
    mountainSeries.StrokeStyle = new SCISolidPenStyle(0xFF279B27, 2.0f);
    surface.RenderableSeries.Add(mountainSeries);
</div>

## Mountain Series Features
Mountain Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-mountain-series)
- [Draw Point Markers](#add-point-markers-onto-a-mountain-series)
- [Draw Series with Different Colors](#paint-area-parts-with-different-colors)

#### Render a Gap in a Mountain Series
It's possible to render a Gap in **Mountain series**, by passing a data point with a `NaN` as the Y value. Please refer to the [RenderableSeries APIs](renderableseries-apis.html#adding-a-gap-onto-a-renderableseries) article for more details. The `SCIFastMountainRenderableSeries`, itself, allows to specify how a gap would appear. You can treat `NAN` values as a **gap** or a **close the line**. That appearance is defined by the `SCIRenderableSeriesBase.drawNaNAs` property (Please see `SCILineDrawMode` enumeration).

#### Add Point Markers onto a Mountain Series
Every data point of a **Mountain Series** can be marked with a `ISCIPointMarker`. To add Point Markers to a **Mountain Series** use the `ISCIRenderableSeries.pointMarker` property. For more information and code examples, please refer to the [PointMarkers API](pointmarker-api.html) article.

#### Paint Area Parts with Different Colors
In SciChart, you can draw Area Parts of the **Mountain Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for Mountain Area - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.
