# Axis Ranging - Restricting VisibleRange

## Clipping the Axis VisibleRange on Zoom and Pan using the VisibleRangeLimit
Given a chart with data in the range of `[0, 10]`, when you zoom to extents, the axis will have a VisibleRange of `[0, 10]`. Sometimes this is not desirable, and you want to clip the `ISCIAxisCore.visibleRange` inside the data-range.
To do this, you can use the `ISCIAxisCore.visibleRangeLimit` property.

For example. Given an axis without any limits (`ISCIAxisCore.visibleRangeLimit = nil`). When we perform ZoomExtents on the chart, the `XAxis` gets the visible range `[0, 10]`:

![No VisibleRange Limit](img/axis-2d/no-visible-range-limit.png)
<center><sub><sup>Actual XAxis.dataRange = [0, 10]; XAxis.visibleRangeLimit = nil; XAxis.visibleRange after ZoomExtents = [0, 10]</sub></sup></center>

So let's set `ISCIAxisCore.visibleRangeLimit` = `[4.5, 5.5]`

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.visibleRangeLimit = [[SCIDoubleRange alloc] initWithMin:4.5 max:5.5];
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.visibleRangeLimit = SCIDoubleRange(min: 4.5, max: 5.5)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis();
    axis.VisibleRangeLimit = new SCIDoubleRange(4.5, 5.5);
</div>

After setting such **VisibleRangeLimit** and using ZoomExtents we now get an `XAxis.visibleRange = [4.5, 5.5]`. In other words, zooming has clipped or limited the visibleRange to `[4.5, 5.5]`

> **_NOTE:_** **VisibleRangeLimit** expects a minimum and maximum value according to the `ISCIAxisCore.visibleRange` type.

![VisibleRange Limit](img/axis-2d/visible-range-limit.png)
<center><sub><sup>Actual XAxis.dataRange = [0, 10]; XAxis.visibleRangeLimit = [4.5, 5.5]; XAxis.visibleRange after ZoomExtents = [4.5, 5.5]</sub></sup></center>

## VisibleRangeLimit Modes
Sometimes it is required to have **one end** of VisibleRange **fixed** or restrict VisibleRange either by `ISCIRange.min` or `ISCIRange.max` value. For that purposes, Axis API exposes `SCIRangeClipMode`. It allows to specify `ISCIAxisCore.visibleRangeLimitMode` property to choose a behavior which is best suitable for a particular scenario.

- `SCIRangeClipMode.SCIRangeClipMode_MinMax` – (Default) allows clipping at Min and Max.
- `SCIRangeClipMode.SCIRangeClipMode_Max` - allows clipping only at Max.
- `SCIRangeClipMode.SCIRangeClipMode_Min` - allows clipping only at Min.

Use this property if you wish to ensure that one side of the chart is always clipped, while the other side is not. For instance:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.visibleRangeLimit = [[SCIDoubleRange alloc] initWithMin:0 max:0];
    axis.visibleRangeLimitMode = SCIRangeClipMode_Min;
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.visibleRangeLimit = SCIDoubleRange(min: 0, max: 0)
    axis.visibleRangeLimitMode = .min;
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis();
    axis.VisibleRangeLimit = new SCIDoubleRange(0, 0);
    axis.VisibleRangeLimitMode = SCIRangeClipMode.Min;
</div>

Results in a chart that always sets `axis.visibleRange.min = 0` when you zoom to extents.

> **_NOTE:_** VisibleRangeLimit does not clip data range when VisibleRangeLimit is greater than data range. In this case after ZoomExtents you’ll get the actual data range.

## Advanced VisibleRange Clipping
`ISCIAxisCore.visibleRangeLimit` is a useful API to ensure the axis clips the visibleRange when zooming to extents. However, it will not stop a user from scrolling outside of that range. To achieve that, you will need to clip the visibleRange in code.

To clip the `ISCIAxisCore.visibleRange` and force a certain maximum or minimum, just use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.visibleRangeChangeListener = ^(id<ISCIAxisCore> axis, id<ISCIRange> oldRange, id<ISCIRange> newRange, BOOL isAnimating) {
        // Set the old range back on the axis if the new Min is less than 0
        if (newRange.minAsDouble < 0.0) {
            axis.visibleRange = oldRange;
        }
    };
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.visibleRangeChangeListener = { (axis, oldRange, newRange, isAnimating) in
        // Set the old range back on the axis if the new Min is less than 0
        if newRange!.minAsDouble < 0.0 {
            axis?.visibleRange = oldRange
        }
    }
</div>
<div class="code-snippet" id="cs">
    var xAxis = new SCINumericAxis();
    xAxis.VisibleRangeChanged += (IISCIAxisCore axis, IISCIRange oldRange, IISCIRange newRange, bool isAnimating) =>
    {
        // Set the old range back on the axis if the new Min is less than 0
        if (newRange.MinAsDouble < 0.0)
        {
            axis.VisibleRange = oldRange;
        }
    };
</div>

## Minimum or Maximum Zoom Level
If you want to constrain zoom depth in your application, the `ISCIAxisCore.minimalZoomConstrain` allows you to specify the minimal difference between `Min` and `Max` values of axis VisibleRange. If the difference becomes less than MinimalZoomConstrain value - then VisibleRange will not change.

It is also possible to specify the `ISCIAxisCore.maximumZoomConstrain` which defines the maximal difference between `Min` and `Max` values of axis VisibleRange. If the difference becomes more than MaximumZoomConstrain value - then VisibleRange will not change.

Read on to learn how to apply Minimum or Maximum Zoom Level for different Axis Types.

### Specifying ZoomConstrains for SCINumericAxis
In the following code we are going to specify the visibleRange for `SCINumericAxis`. It should never become `less than 10` and `greater than 100`. In other words - always be in range of `[10, 100]`.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.minimalZoomConstrain = @(10);
    axis.maximumZoomConstrain = @(100);
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.minimalZoomConstrain = NSNumber(value: 10)
    axis.maximumZoomConstrain = NSNumber(value: 100)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis();
    axis.MinimalZoomConstrain = new NSNumber(10).FromComparable();
    axis.MaximumZoomConstrain = new NSNumber(100).FromComparable();
</div>

### Specifying ZoomConstrains for SCICategoryDateAxis
ZoomConstrains works differently if set on `SCICategoryDateAxis`. It determines the min/max zoom level on an axis which is possible to show by the specifying the amount of data points. 

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCICategoryDateAxis new];
    axis.minimalZoomConstrain = @(10);
    axis.maximumZoomConstrain = @(100);
</div>
<div class="code-snippet" id="swift">
    let axis = SCICategoryDateAxis()
    axis.minimalZoomConstrain = NSNumber(value: 10)
    axis.maximumZoomConstrain = NSNumber(value: 100)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCICategoryDateAxis();
    axis.MinimalZoomConstrain = new NSNumber(10).FromComparable();
    axis.MaximumZoomConstrain = new NSNumber(100).FromComparable();
</div>

In the code above, VisibleRange should show `no less than 10` data points as well as `no more that 100` data points.

### Specifying ZoomConstrains for SCIDateAxis
`SCIDateAxis` has its specifics as well. It's VisibleRange is of `SCIDateRange` type, so the Zoom Constraints is designed to specify the difference between two dates in seconds. Setting Zoom Constraints on a SCIDateRange, you ensure that your `axis.visibleRange.diff` will never become less than the `ISCIAxisCore.minimalZoomConstrain` value and more than `ISCIAxisCore.maximumZoomConstrain`.

> **_NOTE:_** For convenience, SciChart provides a bunch of helper methods in the `SCIDateIntervalUtil` class:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCIDateAxis new];
    double min = [SCIDateIntervalUtil fromMonths:2];
    double max = [SCIDateIntervalUtil fromMonths:10];
    axis.minimalZoomConstrain = @(min);
    axis.maximumZoomConstrain = @(max);
</div>
<div class="code-snippet" id="swift">
    let axis = SCIDateAxis()
    let min = SCIDateIntervalUtil.fromMonths(2)
    let max = SCIDateIntervalUtil.fromMonths(10)
    axis.minimalZoomConstrain = NSNumber(value: min)
    axis.maximumZoomConstrain = NSNumber(value: max)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCIDateAxis();
    var min = SCIDateIntervalUtil.FromMonths(2);
    var max = SCIDateIntervalUtil.FromMonths(10);
    axis.MinimalZoomConstrain = new NSNumber(min).FromComparable();
    axis.MaximumZoomConstrain = new NSNumber(max).FromComparable();
</div>

In the code above, the VisibleRange will satisfy the  following equation: `2 months < VisibleRange < 10 month`

## See Also
- [Axis Types in SciChart](Axis APIs.html)
- [Axis Ranging - AutoRange](axis-ranging---autorange.html)
- [Axis Ranging - Get or Set VisibleRange](axis-ranging---get-or-set-visiblerange.html)
- [Axis Ranging - How to listen to VisibleRange Changes](axis-ranging---how-to-listen-to-visiblerange-changes.html)
- [Axis Ranging - VisibleRange and DataRange](axis-ranging---visiblerange-and-datarange.html)
