# What is Data Resampling and how does it work?
By default, SciChart uses **resampling (culling)** of data to ensure the minimum viable data-set is displayed on the screen. Resampling is intended to be **lossless, and automatic**. It occurs for every [RenderableSeries](2D Chart Types.html) before the series is rendered, if required.

Resampling methods make assumptions about the data in order to produce a valid output. SciChart provides variety of the `SCIResamplingMode`, and auto detects the most suitable one. 

However, there are cases when data input **cannot be resampled accurately**. Good examples could be plotting unsorted data or using logarithmic scale on an axis. We recommend using `SCIResamplingMode.SCIResamplingMode_None` in such situations, which in other word means **switching Resampling off**.

> **_NOTE:_** Please be aware that if you disable resampling you will experience a degradation in performance.

## Resampling Modes
There are several `SCIResamplingMode` available in SciChart:

| **Resampling Modes**                                          | **Description**                                                                                                                            |
| ------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------ |
| `SCIResamplingMode.SCIResamplingMode_Auto`                    | This is the **default** mode. It auto-detects the most suitable resampling algorithm - **fastest and most accurate** - for the type of data appended. |
| `SCIResamplingMode.SCIResamplingMode_None`                    | **switches off** Resampling on a `ISCIRenderableSeries`.                                                                                   |
| `SCIResamplingMode.SCIResamplingMode_MinMax`                  | suitable for evenly-spaced data. Resamples by taking **the min-max** of oversampled data.                                                  |
| `SCIResamplingMode.SCIResamplingMode_MinOrMax`                | suitable for evenly-spaced. Resamples by taking **the min or max** of oversampled data.                                                    |
| `SCIResamplingMode.SCIResamplingMode_Min`                     | suitable for evenly-spaced data. Resamples by taking the **minimum point** of oversampled data.                                            |
| `SCIResamplingMode.SCIResamplingMode_Max`                     | suitable for evenly-spaced data. Resamples by taking the **maximum point** of oversampled data.                                            |
| `SCIResamplingMode.SCIResamplingMode_Mid`                     | suitable for evenly-spaced data. Resamples by taking the **median point** of oversampled data.                                             |

## Setting Resampling Mode
Most of the time, you don't need to set `SCIResamplingMode` manually. SciChart auto detects the best one for a given data and uses it internally. However, when it is necessary, the **ResamplingMode** can be set explicitly via the `ISCIRenderableSeries.resamplingMode` property:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create a DataSeries with unsorted data
    SCIXyDataSeries *unsortedDataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    unsortedDataSeries.acceptsUnsortedData = YES;
    
    SCIFastLineRenderableSeries *rSeries = [SCIFastLineRenderableSeries new];
    // Set a DataSeries with unsorted data
    rSeries.dataSeries = unsortedDataSeries;
    
    // Switch off Resampling because the DataSeries is unsorted
    rSeries.resamplingMode = SCIResamplingMode_None;
</div>
<div class="code-snippet" id="swift">
    // Create a DataSeries with unsorted data
    let unsortedDataSeries = SCIXyDataSeries(xType: .double, yType: .double)
    unsortedDataSeries.acceptsUnsortedData = true
    
    let rSeries = SCIFastLineRenderableSeries()
    // Set a DataSeries with unsorted data
    rSeries.dataSeries = unsortedDataSeries
    
    // Switch off Resampling because the DataSeries is unsorted
    rSeries.resamplingMode = SCIResamplingMode_None
</div>
<div class="code-snippet" id="cs">
    // Create a DataSeries with unsorted data
    var unsortedDataSeries = new XyDataSeries&lt;double, double&gt;();
    unsortedDataSeries.AcceptsUnsortedData = true;

    var rSeries = new SCIFastLineRenderableSeries();
    // Set a DataSeries with unsorted data
    rSeries.DataSeries = unsortedDataSeries;

    // Switch off Resampling because the DataSeries is unsorted
    rSeries.ResamplingMode = SCIResamplingMode.None;
</div>

## Resampling Performance
Resampling makes drawing many millions of points possible with SciChart. For instance, in the [Performance Demo](https://www.scichart.com/example/ios-chart/ios-3x-series-perfomance-demo/) example, we push 1000 points every 10ms to three series on a chart. The point count quickly rises to the millions of points, and SciChart is still rendering at interactive rates. Also, the example allows to play around with different `SCIResamplingMode` and see their impact on performance.

In addition, we compared performance of the most popular iOS charting packages with SciChart. The results can be found in the [Performance Comparison](https://www.scichart.com/ios-chart-metal-opengl-performance/) article.

