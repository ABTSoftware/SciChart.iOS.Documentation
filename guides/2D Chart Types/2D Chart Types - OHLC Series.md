# The OHLC Series Type
The **OHLC series** are provided by the `SCIFastOhlcRenderableSeries` type. This accepts data (`X, Open, High, Low, Close`) from a `SCIOhlcDataSeries` and renders OHLC bar at each `X-Value` coordinate.

> **_NOTE:_** For more info about `SCIOhlcDataSeries`, as well as other DataSeries types in SciChart, see the [DataSeries API](Data Series APIs.html) article.

![OHLC Series Type](img/chart-types-2d/realtime-ticking-stock-chart-example.png)

> **_NOTE:_** Examples of the OHLC Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-realtime-ticking-stock-charts/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-realtime-ticking-stock-charts-example/)

The `SCIFastOhlcRenderableSeries` is very much alike the `SCIFastCandlestickRenderableSeries` class. It allows to specify **StrokeUp** and **StrokeDown** pens as well as relative **DataPointWidth**, which will be applied to every bar. Mentioned settings can be accessed via the following properties:
- `SCIOhlcRenderableSeriesBase.strokeUpStyle` - applied to bars when **Close <= Open**.
- `SCIOhlcRenderableSeriesBase.strokeDownStyle` - applied to bars when **Close > Open**.
- `SCIOhlcRenderableSeriesBase.dataPointWidth` - specifies how much space a single bar occupies, varying from 0 to 1 (when columns are conjoined). 

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create an OHLC Series
![OHLC Chart Example](img/chart-types-2d/ohlc-chart-example.png)

To create an **OHLC Series**, use the following code:

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

    // Create OHLC Series
    SCIFastOhlcRenderableSeries *ohlcSeries = [SCIFastOhlcRenderableSeries new];
    ohlcSeries.dataSeries = dataSeries;
    ohlcSeries.strokeUpStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF00AA00 thickness:1];
    ohlcSeries.strokeDownStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFF0000 thickness:1];
    
    [surface.renderableSeries add:ohlcSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIOhlcDataSeries(xType: .date, yType: .double)

    // Create OHLC Series
    let ohlcSeries = SCIFastOhlcRenderableSeries()
    ohlcSeries.dataSeries = dataSeries
    ohlcSeries.strokeUpStyle = SCISolidPenStyle(colorCode: 0xFF00AA00, thickness: 1.0)
    ohlcSeries.strokeDownStyle = SCISolidPenStyle(colorCode: 0xFFFF0000, thickness: 1.0)

    surface.renderableSeries.add(ohlcSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new OhlcDataSeries&lt;DateTime, double&gt;();
    
    // Create Candlestick Series
    var ohlcSeries = new SCIFastOhlcRenderableSeries
    {
        DataSeries = dataSeries,
        StrokeUpStyle = new SCISolidPenStyle(0xFF00AA00, 1f),
        StrokeDownStyle = new SCISolidPenStyle(0xFFFF0000, 1f),
    };

    surface.RenderableSeries.Add(ohlcSeries);
</div>

## OHLC Series Features
OHLC Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-ohlc-series).
- [Draw Series With Different Colors](#specify-color-for-individual-bars)

#### Render a Gap in an OHLC Series
It's possible to render a Gap in **OHLC series**, by passing a data point with a `NaN` as the `Open, High, Low, Close` values. Please refer to the [RenderableSeries APIs](renderableseries-apis.html#adding-a-gap-onto-a-renderableseries) article for more details.

#### Specify Color for Individual Bars
In SciChart, you can draw each bar of the **OHLC Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To Use palette provider for OHLC Series - a custom `ISCIStrokePaletteProvider` has to be provided to the `ISCIRenderableSeries.paletteProvider` property. Please refer to the [PaletteProvider API](paletteprovider-api.html) article for more info.
