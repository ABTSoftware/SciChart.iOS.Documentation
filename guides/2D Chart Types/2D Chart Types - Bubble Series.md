# The Bubble Series Type
Bubble Chart is provided by the `SCIFastBubbleRenderableSeries` class. It accepts data (`X, Y, Z`) from a `SCIXyzDataSeries` and renders a **bubble** at each `[X, Y]` with `Z` bubble scale.

> **_NOTE:_** For more info about `SCIXyzDataSeries`, as well as other DataSeries types in SciChart, see the [DataSeries API](dataseries-apis.html) article.

![Bubble Series Type](img/chart-types-2d/bubble-chart-example.png)

> **_NOTE:_** Examples for the Bubble Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-bubble-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-bubble-chart-example/)

The `SCIFastBubbleRenderableSeries` class allows to specify BubbleBrush and Stroke via the following properties:
- `SCIFastBubbleRenderableSeries.bubbleBrushStyle`
- `ISCIRenderableSeries.strokeStyle`

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

Also, you can control bubbles scaling using the following properties:
- `SCIFastBubbleRenderableSeries.zScaleFactor`
- `SCIFastBubbleRenderableSeries.autoZRange`

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create a Bubble Series
To create a **Bubble Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *dataSeries = [[SCIXyzDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];

    // Create and add Mountain Series
    id&lt;ISCIRenderableSeries&gt; bubbleSeries = [SCIFastBubbleRenderableSeries new];
    bubbleSeries.dataSeries = dataSeries;
    bubbleSeries.bubbleBrushStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0x50CCCCCC];
    bubbleSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFCCCCCC thickness:1.0];
    bubbleSeries.zScaleFactor = 1.0;
    bubbleSeries.autoZRange = NO;
    [surface.renderableSeries add:bubbleSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyzDataSeries(xType: .double, yType: .double)

    // Create and add Mountain Series
    let bubbleSeries = SCIFastBubbleRenderableSeries()
    bubbleSeries.dataSeries = dataSeries
    bubbleSeries.bubbleBrushStyle = SCISolidBrushStyle(colorCode: 0x50CCCCCC)
    bubbleSeries.strokeStyle = SCISolidPenStyle(colorCode: 0xFFCCCCCC, thickness: 2.0)
    bubbleSeries.zScaleFactor = 1.0
    bubbleSeries.autoZRange = false
    surface.renderableSeries.add(bubbleSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new XyzDataSeries<double, double, double>();
    
    // Create and add Mountain Series
    var bubbleSeries = new SCIFastBubbleRenderableSeries();
    bubbleSeries.DataSeries = dataSeries;
    bubbleSeries.BubbleBrushStyle = new SCISolidBrushStyle(0x50CCCCCC);
    bubbleSeries.StrokeStyle = new SCISolidPenStyle(0xFFCCCCCC, 2f);
    bubbleSeries.ZScaleFactor = 1;
    bubbleSeries.AutoZRange = false;
    surface.RenderableSeries.Add(bubbleSeries);
</div>

## Bubble Series Features
Bubble Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-bubble-series);
- [Draw Series with Different Colors](#paint-bubbles-with-different-colors).

#### Render a Gap in a Bubble Series
It's possible to render a Gap in **Bubble series**, by passing a data point with a `NaN` as the `Y` value. Please refer to the [RenderableSeries APIs](2D Chart Types.html#adding-a-gap-onto-a-renderableseries) article for more details.

#### Paint Bubbles with Different Colors
In SciChart, you can draw each bubble of the **Bubble Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for Bubbles - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.
