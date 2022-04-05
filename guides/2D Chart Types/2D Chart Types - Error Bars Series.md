# The Error Bars Type
In SciChart, Error Bar Series is represented by the `SCIFastErrorBarsRenderableSeries` and `SCIFastFixedErrorBarsRenderableSeries` types. Those accepts (`X, Y, ErrorHigh, ErrorLow`) data from a `SCIHlDataSeries` and (`X, Y`) data from `SCIXyDataSeries` respectively. Then it renders error bars above and below the **Y-value**.

> **_NOTE:_** For more info about `SCIHlDataSeries` or `SCIXyDataSeries`, as well as other DataSeries types in SciChart, see the [DataSeries API](Data Series APIs.html) article.

![Error Bars Type](img/chart-types-2d/error-bars-chart-example.png)

> **_NOTE:_** Examples for the Error Bars Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-fixed-error-bars/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-error-bars-chart-example/)

**Error Bar Series** allow to render error bars for every data point. There are few main properties which allow to control how Error Bars are rendered:
- `errorDirection` - allows rendering either Vertical or Horizontal error via the `SCIErrorDirection` enumeration.
- `errorMode` - choose which error to show - **High**, **Low** or **Both** - via the `SCIErrorMode` enumeration.
- `errorType` - specifies whether error value is `SCIErrorType.SCIErrorType_Relative` (a fraction between 0 and 1) or `SCIErrorType.SCIErrorType_Absolute`.
- `dataPointWidth` - used to determine how much space a single bar occupies. This value can vary from 0 to 1 (when bars are conjoined)

In addition to the above **Error Bars** can be colored differently by providing desired **pens** to the `strokeLowStyle` and `strokeHighStyle` properties.

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

All of the above properties are available on both - `SCIFastErrorBarsRenderableSeries` and `SCIFastFixedErrorBarsRenderableSeries` types.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create an Error Bars Series
![Error Bars Horizontal Vertical](img/chart-types-2d/error-bars-horizontal-vertical-example.png)

> **_NOTE:_** This series type can be used in tandem with other series types, such as `SCIXyScatterRenderableSeries`. Error Bars Series can **share a DataSeries** with other series, to avoid data duplication.

To Create **Error Bars** Series, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *dataSeries0 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    SCIHlDataSeries *dataSeries1 = [[SCIHlDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];

    uint color = 0xFFC6E6FF;

    // Create Fixed ErrorBars Series
    SCIFastFixedErrorBarsRenderableSeries *errorBars0 = [SCIFastFixedErrorBarsRenderableSeries new];
    errorBars0.dataSeries = dataSeries0;
    errorBars0.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:color thickness:1.f];
    errorBars0.errorDirection = SCIErrorDirection_Horizontal;
    errorBars0.errorLow = 0.001;
    errorBars0.errorHigh = 0.002;
    errorBars0.errorType = SCIErrorType_Absolute;

    // Create ErrorBars Series
    SCIFastErrorBarsRenderableSeries *errorBars1 = [SCIFastErrorBarsRenderableSeries new];
    errorBars1.dataSeries = dataSeries1;
    errorBars1.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:color thickness:1.f];
    errorBars1.errorDirection = SCIErrorDirection_Vertical;
    errorBars1.errorType = SCIErrorType_Absolute;

    [surface.renderableSeries addAll:errorBars0, errorBars1, nil];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries0 = SCIXyDataSeries(xType: .double, yType: .double)
    let dataSeries1 = SCIHlDataSeries(xType: .double, yType: .double)

    let color: uint = 0xFFC6E6FF

    // Create Fixed ErrorBars Series
    let errorBars0 = SCIFastFixedErrorBarsRenderableSeries()
    errorBars0.dataSeries = dataSeries0
    errorBars0.strokeStyle = SCISolidPenStyle(colorCode: color, thickness: 1.0)
    errorBars0.errorDirection = .horizontal
    errorBars0.errorLow = 0.001
    errorBars0.errorHigh = 0.002
    errorBars0.errorType = .absolute

    // Create ErrorBars Series
    let errorBars1 = SCIFastErrorBarsRenderableSeries()
    errorBars1.dataSeries = dataSeries1
    errorBars1.strokeStyle = SCISolidPenStyle(colorCode: color, thickness: 1.0)
    errorBars1.errorDirection = .vertical
    errorBars1.errorType = .absolute

    surface.renderableSeries.add(items: errorBars0, errorBars1)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries0 = new XyDataSeries&lt;double, double&gt;();
    var dataSeries1 = new HlDataSeries&lt;double, double&gt;();

    const uint color = 0xFFC6E6FF;

    // Create Fixed ErrorBars Series
    var errorBars0 = new SCIFastFixedErrorBarsRenderableSeries
    {
        DataSeries = dataSeries0,
        StrokeStyle = new SCISolidPenStyle(color, 1f),
        ErrorDirection = SCIErrorDirection.Horizontal,
        ErrorLow = 0.001,
        ErrorHigh = 0.002,
        ErrorType = SCIErrorType.Absolute
    };

    // Create ErrorBars Series
    var errorBars1 = new SCIFastErrorBarsRenderableSeries
    {
        DataSeries = dataSeries1,
        StrokeStyle = new SCISolidPenStyle(color, 1f),
        ErrorDirection = SCIErrorDirection.Vertical,
        ErrorType = SCIErrorType.Absolute
    };

    surface.RenderableSeries = new SCIRenderableSeriesCollection { errorBars0, errorBars1};
</div>

## Render a Gap in a ErrorBars Series
It's possible to render a Gap in **ErrorBars series**, by passing a data point with a `NaN` as the `Y` value. Please refer to the [RenderableSeries APIs](renderableseries-apis.html#adding-a-gap-onto-a-renderableseries) article for more details.
