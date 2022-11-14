There are several axis types in SciChart iOS. Although they all differ in types of data values that can be rendered, the most fundamental difference is in their behavior. By that, the axes can be divided into two groups, Category and Value axis types. Please read the [Value Axis vs. Category Axis](value-axis-vs-category-axis.html) article elaborating on what the difference is.

A list of the axis types are found below:

| **Axis Type**                                           | **Value or Category Axis** |
| ------------------------------------------------------- | -------------------------- |
| [SCINumericAxis](#scinumericaxis)                       | Value Axis                 |
| [SCILogarithmicNumericAxis](#scilogarithmicnumericaxis) | Value Axis                 |
| [SCIDateAxis](#scidateaxis)                             | Value Axis                 |
| [SCICategoryDateAxis](#scicategorydateaxis)             | Category Axis              |

All the axis types in SciChart conforms to the `ISCIAxis` protocol.

## SCINumericAxis
The `SCINumericAxis` is a **Value-Axis** and is suitable for **X and Y Axis** when the data type on that axis is numeric (e.g. **double, int, long, float, short**). It is not suitable for non-numeric data types.

To create and configure a `SCINumericAxis`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.axisAlignment = SCIAxisAlignment_Right;
    axis.autoRange = SCIAutoRange_Once;
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    axis.visibleRange = [[SCIDoubleRange alloc] initWithMin:-45 max:300];
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.axisAlignment = .right;
    axis.autoRange = .once;
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    axis.visibleRange = SCIDoubleRange(min: -45, max: 300)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis
    {
        AxisAlignment = SCIAxisAlignment.Right,
        AutoRange = SCIAutoRange.Once,
        GrowBy = new SCIDoubleRange(0.1, 0.1),
        VisibleRange = new SCIDoubleRange(-45, 300)
    };
</div>

## SCILogarithmicNumericAxis
The `SCILogarithmicNumericAxis` is a **Value axis** which uses non-linear (logarithmic) scale. It is suitable for **X and Y Axis** when the data is numeric (e.g. **double, int, long, float, short**). It is not suitable for non-numeric data types.

> **_NOTE:_** The `SCILogarithmicNumericAxis` cannot render data values less than or equal to zero. Please ensure you append valid data to your DataSeries.

To create and configure a `SCILogarithmicNumericAxis`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    SCILogarithmicNumericAxis *axis = [SCILogarithmicNumericAxis new];
    axis.axisAlignment = SCIAxisAlignment_Right;
    axis.autoRange = SCIAutoRange_Once;
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    
    // Specifies how labels appear on the axis
    axis.scientificNotation = SCIScientificNotation_LogarithmicBase;
    axis.textFormatting = @"#.#E+0";
    
    // Specifies the logarithm base for the logarithmic scale of the axis
    axis.logarithmicBase = 10.0;
    axis.visibleRange = [[SCIDoubleRange alloc] initWithMin:0.1 max:1000.0];
</div>
<div class="code-snippet" id="swift">
    let axis = SCILogarithmicNumericAxis()
    axis.axisAlignment = .right;
    axis.autoRange = .once;
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    
    // Specifies how labels appear on the axis
    axis.scientificNotation = .logarithmicBase
    axis.textFormatting = "#.#E+0"
    
    // Specifies the logarithm base for the logarithmic scale of the axis
    axis.logarithmicBase = 10.0
    axis.visibleRange = SCIDoubleRange(min: 0.1, max: 1000.0)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCILogarithmicNumericAxis
    {
        AxisAlignment = SCIAxisAlignment.Right,
        AutoRange = SCIAutoRange.Once,
        GrowBy = new SCIDoubleRange(0.1, 0.1),

        // Specifies how labels appear on the axis
        ScientificNotation = SCIScientificNotation.LogarithmicBase,
        TextFormatting = "#.#E+0",

        // Specifies the logarithm base for the logarithmic scale of the axis
        LogarithmicBase = 10.0,
        VisibleRange = new SCIDoubleRange(0.1, 1000.0)
    };
</div>

## SCIDateAxis
The `SCIDateAxis` is a **Value axis**, which is suitable for **X and Y Axis** and is designed to work with dates only. 
> **_NOTE:_** The `SCIDateAxis` is not suitable for *numeric data types*.

To create and configure a `SCIDateAxis`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    id&lt;ISCIAxis&gt; axis = [SCIDateAxis new];
    axis.axisAlignment = SCIAxisAlignment_Bottom;
    axis.autoRange = SCIAutoRange_Once;
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    axis.visibleRange = [[SCIDateRange alloc] initWithMin:dateMin max:dateMax];
</div>
<div class="code-snippet" id="swift">
    let axis = SCIDateAxis()
    axis.axisAlignment = .bottom;
    axis.autoRange = .once;
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    axis.visibleRange = SCIDateRange(min: dateMin, max: dateMax)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCIDateAxis
    {
        AxisAlignment = SCIAxisAlignment.Bottom,
        AutoRange = SCIAutoRange.Once,
        GrowBy = new SCIDoubleRange(0.1, 0.1),
        VisibleRange = new SCIDateRange(dateMin, dateMax)
    };
</div>

## SCICategoryDateAxis
The `SCICategoryDateAxis` is a **Category axis** and is suitable for the **XAxis only**. It is designed to handle a special case when **data is discontinuous** or contains breaks at regular intervals. Unlike the other axis types, it works with with **data indices, not actual data values**.
> **_NOTE:_** The `SCICategoryDateAxis` is not suitable for *YAxis* or *numeric data types*.

To create and configure a `SCICategoryDateAxis`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    id&lt;ISCIAxis&gt; axis = [SCICategoryDateAxis new];
    axis.axisAlignment = SCIAxisAlignment_Top;
    axis.autoRange = SCIAutoRange_Once;
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    // Set visibleRange to [min data index, max data index]
    axis.visibleRange = [[SCIDoubleRange alloc] initWithMin:10 max:50];
</div>
<div class="code-snippet" id="swift">
    let axis = SCICategoryDateAxis()
    axis.axisAlignment = .top;
    axis.autoRange = .once;
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    // Set visibleRange to [min data index, max data index]
    axis.visibleRange = SCIDoubleRange(min: 10, max: 50)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCICategoryDateAxis
    {
        AxisAlignment = SCIAxisAlignment.Top,
        AutoRange = SCIAutoRange.Once,
        GrowBy = new SCIDoubleRange(0.1, 0.1),
        // Set VisibleRange to [min data index, max data index]
        VisibleRange = new SCIDoubleRange(10, 50)
    };
</div>

> **_NOTE:_** Note the `SCICategoryDateAxis` is treated as a special case. 
> Although it is intended to show Dates, it doesn't allow setting `SCIDateRange` as VisibleRange. 
> The reason for this is that this axis type works with with **data indices**, not actual **data values**. 
> So a `SCIDoubleRange` should be applied instead, with lower data index as Min and Upper data index as Max.
> 
> To learn more about how to convert values from Dates to Indexes and back, please refer to the [Convert Pixels to Data Coordinates](axis-apis---convert-pixel-to-data-coordinates.html) article.

## See the Axis Types in action
Please take a look at the examples from the iOS Examples Suite listed below to see these axis types in action:
- [Column Chart](https://www.scichart.com/example/ios-column-chart-demo/) with `SCINumericAxis`
- [Logarithmic Axis](https://www.scichart.com/example/ios-chart-example-logarithmic-axis/) with `SCILogarithmicNumericAxis`
- [Fan Chart](https://www.scichart.com/example/ios-fan-chart/) with `SCIDateAxis`
- [Candlestick Chart](https://www.scichart.com/example/ios-candlestick-chart-demo/) with `SCICategoryDateAxis`