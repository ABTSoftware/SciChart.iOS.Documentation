# SciChart iOS Tutorial - Multiple Axis
So far, in our series of tutorials, all the charts have had one `X-Axis` and one `Y-Axis`.

SciChart supports unlimited, multiple X, and Y Axis on the left, right, bottom and top of the chart.
You can change Axis alignment, **rotate charts**, **mix axis** (have both XAxis/YAxis on the left), horizontally or vertically **stack axes**.
The possibilities are literally ***endless***!

In this tutorial, we are going to:
- add a second `Y-Axis` to the chart.
- show how to register annotations and line series on **the second axis**.
- ensure Axis **drag behaviors** work on both axis.

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2006%20-%20Multiple%20Axis)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-06)

First of all, make sure, you've went through the previous the tutorials:
- [Tutorial 01 - Create a simple Chart 2D](tutorial-01---create-a-simple-2d-chart.html)
- [Tutorial 05 - Annotations](tutorial-05---annotations.html)

And have at least basic understanding of how to use SciChart.

Also, you might want to read our documentation about [Axis APIs](Axis APIs.html).

## Adding a Second Y-Axis
The procedure to add a second axis to a `SCIChartSurface` is pretty much the same as with one axis with one difference.
You must assign a **unique string ID** to all axes if there is more than one.

To see the axis to appear to the either side of a chart, you set `SCIAxisAlignment` to e.g.:
- `SCIAxisAlignment.SCIAxisAlignment_Left`
- `SCIAxisAlignment.SCIAxisAlignment_Right`
- etc...

Let's add another axis and align it to the **left** side of the chart (assuming the original one is placed to the right).

To have a different scale on the secondary axis, we are going to **enlarge** its VisibleRange **by 40%** setting a `ISCIAxisCore.growBy` value:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create another numeric axis, right-aligned
    SCINumericAxis *yAxisRight = [SCINumericAxis new];
    yAxisRight.axisTitle = @"Primary Y-Axis";
    yAxisRight.axisId = @"Primary Y-Axis";
    yAxisRight.axisAlignment = SCIAxisAlignment_Right;
    
    // Create another numeric axis, left-aligned
    SCINumericAxis *yAxisLeft = [SCINumericAxis new];
    yAxisLeft.axisTitle = @"Secondary Y-Axis";
    yAxisLeft.axisId = @"Secondary Y-Axis";
    yAxisLeft.axisAlignment = SCIAxisAlignment_Left;
    yAxisLeft.growBy = [[SCIDoubleRange alloc] initWithMin:0.2 max:0.2];
    // ...
    [self.surface.xAxes add:[SCINumericAxis new]];
    [self.surface.yAxes addAll:yAxisLeft, yAxisRight, nil];
</div>
<div class="code-snippet" id="swift">
    // Create another numeric axis, right-aligned
    let yAxisRight = SCINumericAxis()
    yAxisRight.axisTitle = "Primary Y-Axis"
    yAxisRight.axisId = "Primary Y-Axis"
    yAxisRight.axisAlignment = .right
    
    // Create another numeric axis, left-aligned
    let yAxisLeft = SCINumericAxis()
    yAxisLeft.axisTitle = "Secondary Y-Axis"
    yAxisLeft.axisId = "Secondary Y-Axis"
    yAxisLeft.axisAlignment = .left
    yAxisLeft.growBy = SCIDoubleRange(min: 0.2, max: 0.2)
    // ...
    self.surface.xAxes.add(items: SCINumericAxis())
    self.surface.yAxes.add(items: yAxisRight, yAxisLeft)
</div>
<div class="code-snippet" id="cs">
    // Create another numeric axis, right-aligned
    var yAxisRight = new SCINumericAxis();
    yAxisRight.AxisTitle = "Primary Y-Axis";
    yAxisRight.AxisId = "Primary Y-Axis";
    yAxisRight.AxisAlignment = SCIAxisAlignment.Right;

    // Create another numeric axis, left-aligned
    var yAxisLeft = new SCINumericAxis();
    yAxisLeft.AxisTitle = "Secondary Y-Axis";
    yAxisLeft.AxisId = "Secondary Y-Axis";
    yAxisLeft.AxisAlignment = SCIAxisAlignment.Left;
    yAxisLeft.GrowBy = new SCIDoubleRange(0.2, 0.2);
    // ...
    Surface.XAxes.Add(new SCINumericAxis());
    Surface.YAxes.Add(yAxisLeft);
    Surface.YAxes.Add(yAxisRight);
</div>

Now we can see the second axis in our application:

![Second Y-Axis](img/tutorials-2d/tutorials-2d-second-y-axis.png)

> **_NOTE:_** **Annotations** and **RenderableSeries** don't get rendered now

## Registering RenderableSeries on the Second Y-Axis
If there are ***several Y or X axes***, you need to register other chart parts, like **RenderableSeries** and **Annotations**, on a particular axis to be measured against its scale.

From the tutorial, we are going to attach one series to the right axis and the other to the left axis, passing corresponding IDs to the **RenderableSeries**:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIFastLineRenderableSeries *lineSeries = [SCIFastLineRenderableSeries new];
    lineSeries.yAxisId = @"Primary Y-Axis";

    SCIXyScatterRenderableSeries *scatterSeries = [SCIXyScatterRenderableSeries new];
    scatterSeries.yAxisId = @"Secondary Y-Axis";
</div>
<div class="code-snippet" id="swift">
    let lineSeries = SCIFastLineRenderableSeries()
    lineSeries.yAxisId = "Primary Y-Axis"

    let scatterSeries = SCIXyScatterRenderableSeries()
    scatterSeries.yAxisId = "Secondary Y-Axis"
</div>
<div class="code-snippet" id="cs">
    var lineSeries = new SCIFastLineRenderableSeries { DataSeries = lineDataSeries, YAxisId = "Primary Y-Axis" };
    var scatterSeries = new SCIXyScatterRenderableSeries
    {
        YAxisId = "Secondary Y-Axis",
        DataSeries = scatterDataSeries,
        PointMarker = new SCIEllipsePointMarker { FillStyle = new SCISolidBrushStyle(0xFF32CD32), Size = new CGSize(10, 10) }
    };
</div>

Which results in the following:

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-two-axis-two-series.mp4"></video>

## Registering Annotations on the Second Y-Axis
Annotations also need to be registered on a certain axis in a **multi-axis** scenario.
So we are going to **alternate** the axis IDs to annotations in on our chart for the left or right axes in the following way:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCITextAnnotation *label = [SCITextAnnotation new];
    label.yAxisId = x % 200 == 0 ? @"Primary Y-Axis" : @"Secondary Y-Axis";
</div>
<div class="code-snippet" id="swift">
    let label = SCITextAnnotation()
    label.yAxisId = x % 200 == 0 ? "Primary Y-Axis" : "Secondary Y-Axis"
</div>
<div class="code-snippet" id="cs">
    var label = new SCITextAnnotation();
    label.YAxisId = x % 200 == 0 ? "Primary Y-Axis" : "Secondary Y-Axis";
</div>

If you now add `SCIYAxisDragModifier` you can see which series and annotation are registered on which `Y-Axis`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [self.surface.chartModifiers add:[SCIYAxisDragModifier new]];
</div>
<div class="code-snippet" id="swift">
    self.surface.chartModifiers.add(items: SCIYAxisDragModifier())
</div>
<div class="code-snippet" id="cs">
    Surface.ChartModifiers.Add(new SCIYAxisDragModifier());
</div>

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-two-axis-annotations.mp4"></video>

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2006%20-%20Multiple%20Axis)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-06)

Also, you can found **next tutorial** from this series here - [SciChart iOS Tutorial - Linking Multiple Charts](tutorial-06---multiple-axis.html)

Of course, this is not the limit of what you can achieve with the SciChart iOS. You might want to read some of the following articles:
- [Axis APIs](Axis APIs.html)
- [Annotations API](Annotations APIs.html)
- [2D Chart Types](2D Chart Types.html)
- [Chart Modifiers](Chart Modifier APIs.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).