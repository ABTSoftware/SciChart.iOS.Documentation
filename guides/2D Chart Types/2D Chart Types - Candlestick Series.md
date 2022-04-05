# The Candlestick Series type
The **Candlestick charts** are provided by the `SCIFastCandlestickRenderableSeries` type. This accepts data (`X, Open, High, Low, Close`) from a `SCIOhlcDataSeries` and renders candlesticks at each `X-Value` coordinate.

> **_NOTE:_** For more info about `SCIOhlcDataSeries`, as well as other DataSeries types in SciChart, see the [DataSeries API](Data Series APIs.html) article.

![Candlestick Series Type](img/chart-types-2d/candlestick-chart-example.png)

> **_NOTE:_** Examples of the Candlestick Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-candlestick-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-candlestick-chart-example/)

The `SCIFastCandlestickRenderableSeries` is very much alike the `SCIFastOhlcRenderableSeries` class. It allows to specify **FillUp** and **FillDown** brushes, **StrokeUp** and **StrokeDown** pens via the following properties:
- `SCIFastCandlestickRenderableSeries.fillUpBrushStyle`
- `SCIFastCandlestickRenderableSeries.fillDownBrushStyle`
- `SCIOhlcRenderableSeriesBase.strokeUpStyle`
- `SCIOhlcRenderableSeriesBase.strokeDownStyle`

**StrokeUp** and **FillUp** styles are applied to bars with **Close >= Open**, and **StrokeDown** and **FillDown** to those that have **Close < Open** respectively. 

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

Also, the `SCIOhlcRenderableSeriesBase.dataPointWidth` specifies how much space a single bar occupies, varying from 0 to 1 (when columns are conjoined). 

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done by passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create a Candlestick Series
To create a **Candlestick Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIOhlcDataSeries *dataSeries = [[SCIOhlcDataSeries alloc] initWithXType:SCIDataType_Date yType:SCIDataType_Double];

    // Create Candlestick Series
    SCIFastCandlestickRenderableSeries *candlestickSeries = [SCIFastCandlestickRenderableSeries new];
    candlestickSeries.dataSeries = dataSeries;
    candlestickSeries.strokeUpStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF00AA00 thickness:1];
    candlestickSeries.fillUpBrushStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0x9000AA00];
    candlestickSeries.strokeDownStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFF0000 thickness:1];
    candlestickSeries.fillDownBrushStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0x90FF0000];
    
    [surface.renderableSeries add:candlestickSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIOhlcDataSeries(xType: .date, yType: .double)

    // Create Candlestick Series
    let candlestickSeries = SCIFastCandlestickRenderableSeries()
    candlestickSeries.dataSeries = dataSeries
    candlestickSeries.strokeUpStyle = SCISolidPenStyle(colorCode: 0xFF00AA00, thickness: 1.0)
    candlestickSeries.fillUpBrushStyle = SCISolidBrushStyle(colorCode: 0x9000AA00)
    candlestickSeries.strokeDownStyle = SCISolidPenStyle(colorCode: 0xFFFF0000, thickness: 1.0)
    candlestickSeries.fillDownBrushStyle = SCISolidBrushStyle(colorCode: 0x90FF0000)

    surface.renderableSeries.add(candlestickSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new OhlcDataSeries&lt;DateTime, double&gt;();
    
    // Create Candlestick Series
    var candlestickSeries = new SCIFastCandlestickRenderableSeries
    {
        DataSeries = dataSeries,
        StrokeUpStyle = new SCISolidPenStyle(0xFF00AA00, 1f),
        StrokeDownStyle = new SCISolidPenStyle(0xFFFF0000, 1f),
        FillUpBrushStyle = new SCISolidBrushStyle(0x8800AA00),
        FillDownBrushStyle = new SCISolidBrushStyle(0x88FF0000)
    };

    surface.RenderableSeries.Add(candlestickSeries);
</div>

## Candlestick Series Features
Candlestick Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-candlestick-series);
- [Draw Series With Different Colors](#specify-color-for-individual-bars).

#### Render a Gap in a Candlestick Series
It's possible to render a Gap in **Candlestick series**, by passing a data point with a `NaN` as the `Open, High, Low, Close` values. Please refer to the [RenderableSeries APIs](renderableseries-apis.html#adding-a-gap-onto-a-renderableseries) article for more details.

#### Specify Color for Individual Candlesticks
In SciChart, you can draw each bar of the **Candlestick Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for Candlestick Series - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.
