# DataSeries Types in SciChart
SciChart features a proprietary **DataSeries API**, which internally uses the fastest possible data-structures to allow fast manipulation of data bound to charts.

Our DataSeries are highly optimized data-structures for **indexing**, **searching** and **iterating** over data, enabling SciChart to achieve its record performance!

The following DataSeries types exist in SciChart iOS.

> **_NOTE:_**  Allowable types in SciChart include NSDate, Int64, Int32, Int16, Byte, Double, Float.

| **Data Series Type**                                           | **Series Applicable**                                           |
| -------------------------------------------------------------- | --------------------------------------------------------------- |
| `SCIXyDataSeries` - stores X and Y Data                        |                                                                                                        `SCIFastLineRenderableSeries`, `SCIFastMountainRenderableSeries`, `SCIXyScatterRenderableSeries`, `SCIFastColumnRenderableSeries`, `SCIFastImpulseRenderableSeries` and `SCIFastFixedErrorBarsRenderableSeries` |
| `SCIXyyDataSeries` - stores X, Y and Y1 Data                   | **`SCIFastBandRenderableSeries` (required)**.                                        Can also apply to `SCIFastLineRenderableSeries`, `SCIFastMountainRenderableSeries`, `SCIXyScatterRenderableSeries`, `SCIFastColumnRenderableSeries`, `SCIFastImpulseRenderableSeries` and `SCIFastFixedErrorBarsRenderableSeries`. In this case only the X and Y value are chosen |
| `SCIXyzDataSeries` - stores X, Y and Z Data                    | **`SCIFastBubbleRenderableSeries` (required)**.                                      Can also apply to `SCIFastLineRenderableSeries`, `SCIFastMountainRenderableSeries`, `SCIXyScatterRenderableSeries`, `SCIFastColumnRenderableSeries`, `SCIFastImpulseRenderableSeries` and `SCIFastFixedErrorBarsRenderableSeries`. In this case only the X and Z value are chosen |
| `SCIHlDataSeries` - stores X, Y, High and Low Data             | **`SCIFastErrorBarsRenderableSeries` (required)**.                                   Can also apply to `SCIFastLineRenderableSeries`, `SCIFastMountainRenderableSeries`, `SCIXyScatterRenderableSeries`, `SCIFastColumnRenderableSeries`, `SCIFastImpulseRenderableSeries` and `SCIFastFixedErrorBarsRenderableSeries`. In this case only the X and Y values are chosen |
| `SCIOhlcDataSeries` - stores X, Open, High, Low and Close Data | **`SCIFastCandlestickRenderableSeries` or `SCIFastOhlcRenderableSeries` (required)**.  Can also apply to `SCIFastLineRenderableSeries`, `SCIFastMountainRenderableSeries`, `SCIXyScatterRenderableSeries`, `SCIFastColumnRenderableSeries`, `SCIFastImpulseRenderableSeries` and `SCIFastFixedErrorBarsRenderableSeries`. In this case only the X and Close values are chosen |
| `SCIUniformHeatmapDataSeries` - stores TY values as array of TY, and TX, TZ values are computed from cell index, Start and Step values | **`SCIFastUniformHeatmapRenderableSeries` (required)**. This DataSeries type is not applicable to any other `RenderableSeries` |

## Manipulating DataSeries Data
Data in `ISCIDataSeries` may be manipulated using the **[Append, Insert, Update, Remove](#append-insert-update-remove)** functions. 

In addition, [xRange and yRange](#x-and-y-ranges) may be accessed as well as any of the [underlying data may be directly accessed](#accessing-x-y-other-values).

Also, DataSeries feature two modes: standard and [FIFO (First in first out)](#fifo-first-in-first-out-dataseries). In FIFO mode data may be streamed and old data-points discarded as new arrive.

Finally, you can control [Data Distribution](#dataseries-data-distribution) using `ISCIDataDistributionCalculator`.

The following sections show how you can manipulate data in the **DataSeries** types in SciChart.

### Append, Insert, Update, Remove
All DataSeries types include **Append, Insert, Update, Remove** methods. Many of these also have overloads which accept a range of data, e.g. the `ISCIXyDataSeries` protocol has the following:
- `-[ISCIXyDataSeries appendX:y:]`
- `-[ISCIXyDataSeries appendArrayX:y:]`
- `-[ISCIXyDataSeries appendValuesX:y:]`
- `-[ISCIXyDataSeries insertX:y:at:]`
- `-[ISCIXyDataSeries insertArrayX:y:at:]`
- `-[ISCIXyDataSeries insertValuesX:y:at:]`
- `-[ISCIXyDataSeries updateX:at:]`
- `-[ISCIXyDataSeries updateY:at:]`
- `-[ISCIXyDataSeries updateX:y:at:]`
- `-[ISCIXyDataSeries updateArrayX:at:]`
- `-[ISCIXyDataSeries updateArrayY:at:]`
- `-[ISCIXyDataSeries updateArrayX:y:at:]`
- `-[ISCIXyDataSeries updateValuesX:at:]`
- `-[ISCIXyDataSeries updateValuesY:at:]`
- `-[ISCIXyDataSeries updateValuesX:y:at:]`

Other DataSeries have similar methods appropriate to underlying data which they hold.

> **_NOTE:_** It is highly recommended to use set of methods, which works with our `ISCIValues` data-structures, to achieve better performance and omit boxing/unboxing into Cocoa native types.

### X and Y Ranges
All DataSeries types expose the XRange and YRange of the underlying DataSeries as well as corresponding Max and Min values.
See the following methods:
- `ISCIDataSeries.xMin`
- `ISCIDataSeries.xMax`
- `ISCIDataSeries.xRange`
- `ISCIDataSeries.yMin`
- `ISCIDataSeries.yMax`
- `ISCIDataSeries.yRange`

> **_NOTE:_** These perform a calculation every time the property is accessed, so should be used sparingly.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    // Append some data here
    id&lt;ISCIRange&gt; xRange = dataSeries.xRange;
    id&lt;ISCIRange&gt; yRange = dataSeries.yRange;
    // You can access Min/Max directly as Double
    double min = xRange.minAsDouble;
    double max = xRange.maxAsDouble;
</div>
<div class="code-snippet" id="swift">
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)
    // Append some data here
    let xRange = dataSeries.xRange;
    let yRange = dataSeries.yRange;
    // You can access Min/Max directly as Double
    let min = xRange.minAsDouble;
    let max = xRange.maxAsDouble;
</div>
<div class="code-snippet" id="cs">
    var dataSeries = new XyDataSeries&lt;double, double&gt;();
    // Append some data here
    var xRange = dataSeries.XRange;
    var yRange = dataSeries.YRange;
    // You can access Min/Max directly as Double
    var min = xRange.MinAsDouble;
    var max = xRange.MaxAsDouble;
</div>

> **_NOTE:_**  SciChart **Xamarin.iOS** has generic wrappers to our native DataSeries classes. These generic classes can be constructed and used without the need of the **SCIDataType**. It will be inferred automatically.

### Accessing X, Y, [other] Values
All DataSeries types expose the lists of underlying data. There is a set of protocols, which provides an access for the underlying data, which names has a pattern as follows: `ISCI[Something]DataSeriesValues`, e.g.:
- `ISCIXDataSeriesValues` - provides an access to the `xValues`.
- `ISCIXyDataSeriesValues` - provides an access to the `yValues` (in addition to `xValues`).

Those are read-only `ISCIList`s. Data can be accessed by casting to the underlying list type, e.g. `ISCIListDouble`.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    id&lt;ISCIList&gt; xValues = dataSeries.xValues;

    // You can cast each value separately
    double value = [[xValues valueAt:0] toDouble];

    // Or you can cast whole list to needed one, and work with its primitive `itemsArray`
    id&lt;ISCIListDouble&gt; doubleValues = (id&lt;ISCIListDouble&gt;)xValues;
    double doubleValue = doubleValues.itemsArray[0];
</div>
<div class="code-snippet" id="swift">
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)
    let xValues = dataSeries.xValues!

    // You can cast each value separately
    let value = xValues.value(at: 0).toDouble()

    // Or you can cast whole list to needed one, and work with it's primitive `itemsArray`
    let doubleValues = xValues as! ISCIListDouble
    let doubleValue = doubleValues.itemsArray[0]
</div>
<div class="code-snippet" id="cs">
    var dataSeries = new XyDataSeries&lt;double, double&gt;();
    
    var xValues = dataSeries.XValues;
    // You can cast each value separately
    var value = xValues.ValueAt(0).ToDouble();
</div>


### Fifo (First-In-First-Out) DataSeries
DataSeries allow **First-In-First-Out** behaviour, where a **maximum capacity** is set, once reached, old data-points **are discarded**. To declare a Fifo dataseries, simply set the `ISCIDataSeries.fifoCapacity` property.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    dataSeries.fifoCapacity = 1000;
</div>
<div class="code-snippet" id="swift">
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)
    dataSeries.fifoCapacity = 1000
</div>
<div class="code-snippet" id="cs">
    var dataSeries = new XyDataSeries&lt;double, double&gt;();
    dataSeries.FifoCapacity = 1000;
</div>

Considering the code above, the behaviour will be the following. Once the **1001-st** point have been added, the **very first** point will be **discarded**. Appending **another 5 points**, the next **oldest 5 points will be discarded**. The window continues to scroll no matter how many points are appended and **memory never increases beyond 1000 points**.

Fifo DataSeries are a very memory and CPU efficient way of scrolling and discarding old data and creating scrolling, streaming charts.

### DataSeries Data Distribution
The `ISCIDataDistributionCalculator` is a protocol which determines the distribution of your data (sorted in X or not, evenly spaced in X or not) and the flags are used to determine the correct algorithm(s) for [resampling](data-resampling.html), [hit-testing](hit-test-api.html) and indexing of data.

By default, this all works automatically, however, if you want to **save a few CPU cycles** and you know in advance the distribution of your data, you can override the flags as follows:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIUserDefinedDistributionCalculator *dataDistributionCalculator = [SCIUserDefinedDistributionCalculator new];
    dataDistributionCalculator.isDataSortedAscending = YES;
    dataDistributionCalculator.isDataEvenlySpaced = YES;
    dataDistributionCalculator.isDataContainsNaN = NO;
    
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double dataDistributionCalculator:dataDistributionCalculator];
</div>
<div class="code-snippet" id="swift">
    let dataDistributionCalculator = SCIUserDefinedDistributionCalculator()
    dataDistributionCalculator.isDataSortedAscending = true
    dataDistributionCalculator.isDataEvenlySpaced = true
    dataDistributionCalculator.isDataContainsNaN = false
    
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double, dataDistributionCalculator: dataDistributionCalculator)
</div>

#### DataSeries AcceptsUnsortedData
By default, DataSeries are designed to throw an exception if data is appended unsorted in X. This is because unsorted data is detrimental to performance, and many people were unintentionally appending data unsorted in the X-direction.

SciChart can however accept unsorted data, you just need to specify the flag `ISCIDataSeries.acceptsUnsortedData` = true. This will signal to SciChart that your appending of unsorted data was intentional and the chart will then draw it.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    dataSeries.acceptsUnsortedData = YES;
</div>
<div class="code-snippet" id="swift">
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)
    dataSeries.acceptsUnsortedData = true
</div>
<div class="code-snippet" id="cs">
    var dataSeries = new XyDataSeries&lt;double, double&gt;();
    dataSeries.AcceptsUnsortedData = true;
</div>
