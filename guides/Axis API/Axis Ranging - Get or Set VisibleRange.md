# Axis Ranging - Get or Set VisibleRange

## How to modify VisibleRange for different Axis Types
To programmatically range an axis, set the property `ISCIAxisCore.visibleRange`.

> **_NOTE:_** The type of object that needs to be applied to the Axis VisibleRange depends on the axis type.

For more information about setting VisibleRange on particular Axis type - please see the following:
- [SCINumericAxis](Axis APIs.html#scinumericaxis)
- [SCILogarithmicNumericAxis](Axis APIs.html#scilogarithmicnumericaxis)
- [SCIDateAxis](Axis APIs.html#scidateaxis)
- [SCICategoryDateAxis](Axis APIs.html#scicategorydateaxis)

## Adding Padding or Spacing with GrowBy
Also, it is possible to **add spacing** to the VisibleRange via the `ISCIAxisCore.growBy` property. It allows to specify two fractions which will be always applied to the `ISCIRange.min`, `ISCIRange.max` values of the axes VisibleRange:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    axis.visibleRange = [[SCIDoubleRange alloc] initWithMin:0 max:10];
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    axis.visibleRange = SCIDoubleRange(min: 0, max: 10)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis
    {
        GrowBy = new SCIDoubleRange(0.1, 0.1),
        VisibleRange = new SCIDoubleRange(0, 10)
    };
</div>

In the code snippet above, the GrowBy of 0.1 will be applied to the VisibleRange resulting in actual VisibleRange increased by a fraction of 0.1 (10%), i.e. `Min = -1, Max = 11`.

![GrowBy](img/axis-2d/impulse-growby.png)

## Zooming to fit all the Data
Sometimes it is required to make an axis to show the **full extent of the data** associated with it. There are several ways to achieve this in code:
- set the `ISCIAxisCore.visibleRange` to the `ISCIAxisCore.dataRange` value.
- configure the axis to auto adjust correspondingly to data changes. See the [AutoRange](axis-ranging---autorange.html) article.
- call the `ISCIChartController` methods from `SCIChartSurface` such as `-[ISCIChartController zoomExtents]`.
- call the `ISCIChartController` methods from `ISCIViewportManager` such as `-[ISCIChartController zoomExtents]`.

To change the `VisibleRange` by touch interaction with a chart, please read about our [ChartModifier API](Chart Modifier APIs.html).
