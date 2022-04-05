There are several axis types in **SciChart iOS 3D**. The `ISCIAxis3D` are the logical representation of the `XZ`, `ZY`, `YX` planes in the ***Axis Cube***.

Axes are required to measure the `ISCIRenderableSeries3D`, for instance, an axis is responsible for the transformation between **data-values** (provided by your code) and **world coordinates** (`X`, `Y`, `Z` values in 3D Space).

> **_NOTE:_** It is necessary to declare all **X, Y and Z** Axes in code before the 3D chart will draw. They are accessible via the following properties:
>
> - `SCIChartSurface3D.xAxis`
> - `SCIChartSurface3D.yAxis`
> - `SCIChartSurface3D.zAxis`

A list of the 3D axis types are found below:

- [SCINumericAxis3D](#scinumericaxis3d)
- [SCILogarithmicNumericAxis](#scilogarithmicnumericaxis3d)
- [SCIDateAxis3D](#scidateaxis3d)

All the 3D axis types in SciChart conforms to the `ISCIAxis3D` protocol.

## SCINumericAxis3D
The `SCINumericAxis3D` is a **Value-Axis** and is suitable when the data type on that axis is numeric (e.g. **double, int, long, float, short**). It is not suitable for non-numeric data types such as `NSDate`.

To create and configure a `SCINumericAxis3D`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    id&lt;ISCIAxis3D&gt; axis = [SCINumericAxis3D new];
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    axis.visibleRange = [[SCIDoubleRange alloc] initWithMin:-7 max:7];
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis3D()
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    axis.visibleRange = SCIDoubleRange(min: -7, max: 7)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis3D
    {
        GrowBy = new SCIDoubleRange(0.1, 0.1),
        VisibleRange = new SCIDoubleRange(-7, 7)
    };
</div>

## SCILogarithmicNumericAxis3D
The `SCILogarithmicNumericAxis3D` is a **Value axis** which uses non-linear (logarithmic) scale. It is suitable when the data is numeric (e.g. **double, int, long, float, short**). It is not suitable for non-numeric data types such as `NSDate`.

> **_NOTE:_** The `SCILogarithmicNumericAxis3D` cannot render data values less than or equal to zero. Please ensure you append valid data to your DataSeries.

To create and configure a `SCILogarithmicNumericAxis3D`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    SCILogarithmicNumericAxis3D *axis = [SCILogarithmicNumericAxis3D new];
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    axis.visibleRange = [[SCIDoubleRange alloc] initWithMin:0.1 max:1000.0];

    // Specifies how labels appear on the axis
    axis.scientificNotation = SCIScientificNotation_LogarithmicBase;
    axis.textFormatting = @"#.#E+0";
    
    // Specifies the logarithm base for the logarithmic scale of the axis
    axis.logarithmicBase = 10.0;
</div>
<div class="code-snippet" id="swift">
    let axis = SCILogarithmicNumericAxis3D()
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    axis.visibleRange = SCIDoubleRange(min: 0.1, max: 1000.0)
    
    // Specifies how labels appear on the axis
    axis.scientificNotation = .logarithmicBase
    axis.textFormatting = "#.#E+0"
    
    // Specifies the logarithm base for the logarithmic scale of the axis
    axis.logarithmicBase = 10.0
</div>
<div class="code-snippet" id="cs">
    var axis = new SCILogarithmicNumericAxis3D
    {
        GrowBy = new SCIDoubleRange(0.1, 0.1),
        VisibleRange = new SCIDoubleRange(0.1, 1000.0),

        // Specifies how labels appear on the axis
        ScientificNotation = SCIScientificNotation.LogarithmicBase,
        TextFormatting = "#.#E+0",

        // Specifies the logarithm base for the logarithmic scale of the axis
        LogarithmicBase = 10.0
    };
</div>

## SCIDateAxis3D
The `SCIDateAxis3D` is a **Value axis**, which is designed to work with **dates** only.
> **_NOTE:_** The `SCIDateAxis3D` is not suitable for *numeric data types*.

To create and configure a `SCIDateAxis3D`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    id&lt;ISCIAxis&gt; axis = [SCIDateAxis3D new];
    axis.autoRange = SCIAutoRange_Always;
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    axis.visibleRange = [[SCIDoubleRange alloc] initWithMin:dateMin max:dateMax];
</div>
<div class="code-snippet" id="swift">
    let axis = SCIDateAxis3D()
    axis.autoRange = .always;
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    axis.visibleRange = SCIDoubleRange(min: dateMin, max: dateMax)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCIDateAxis3D
    {
        AutoRange = SCIAutoRange.Always,
        GrowBy = new SCIDoubleRange(0.1, 0.1),
        VisibleRange = new SCIDoubleRange(dateMin, dateMax)
    };
</div>

> **_NOTE:_** To learn more about how to convert values from Dates to Indexes and back, please refer to the [Convert Pixels to Data Coordinates](axis-apis---convert-pixel-to-data-coordinates.html) article.

## See the Axis 3D Types in action
Please take a look at the examples from the [iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) listed below to see these axis 3D types in action:
- [Custom Free Surface Chart 3D](https://www.scichart.com/example/ios-3d-chart-example-create-custom-free-surface-3d-chart/) with `SCINumericAxis3D`
- [Logarithmic Axis 3D](https://www.scichart.com/example/ios-3d-chart-example-logarithmic-axis-3d/) with `SCILogarithmicNumericAxis`
- [Date Axis 3D](https://www.scichart.com/example/ios-3d-chart-example-date-axis-3d) with `SCIDateAxis3D`