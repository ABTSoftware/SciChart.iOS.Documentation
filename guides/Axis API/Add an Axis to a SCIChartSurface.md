# Add an Axis to a SCIChartSurface
While creating your [First SciChart iOS App](creating-your-first-scichart-ios-app.html) you've added *axes* to a `SCIChartSurface`. With SciChart you can also have **unlimited number of axes**, whether it's left, right, top or bottom, X or Y Axes.

![Multiple Axes Example](img/axis-2d/multiple-axes-example.png)

You can also place axes **in the centre** of the chart or **swap X and Y axes** over to create a **vertical chart**. Please see these examples from the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) for reference:
- [Multiple Axis Demo](https://www.scichart.com/example/ios-multiple-axis-demo/)
- [Shifted Axes](https://www.scichart.com/example/ios-chart/ios-shifted-axes/)
- [Vertical Charts Example](https://www.scichart.com/example/ios-chart-example-vertical-charts/)

Axes can be added to either the `ISCIChartSurface.xAxes` or `ISCIChartSurface.yAxes` collection of `SCIChartSurface`. In case of having **multiple X or Y** axes, every axis should have a **unique ID** assigned to it. All axes are positioned on a chart according to their `SCIAxisAlignment`.

## Adding an Axis
To add X and Y axes to a `SCIChartSurface`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create numeric X axis
    id&lt;ISCIAxis&gt; xAxis = [SCINumericAxis new];
    // Create numeric Y axis
    id&lt;ISCIAxis&gt; yAxis = [SCINumericAxis new];

    // Add the Y and X axes to the axis collections of the SCIChartSurface
    [surface.xAxes add:xAxis];
    [surface.yAxes add:yAxis];
</div>
<div class="code-snippet" id="swift">
    // Create numeric X axis
    let xAxis = SCINumericAxis()
    // Create numeric Y axis
    let yAxis = SCINumericAxis()

    // Add the Y and X axes to the axis collections of the SCIChartSurface
    surface.xAxes.add(xAxis)
    surface.yAxes.add(yAxis)
</div>
<div class="code-snippet" id="cs">
    // Create numeric X axis
    var xAxis = SCINumericAxis();
    // Create numeric Y axis
    var yAxis = SCINumericAxis();

    // Add the Y and X axes to the axis collections of the SCIChartSurface
    Surface.XAxes.add(xAxis);
    Surface.YAxes.add(yAxis);
</div>

Similarly, you can add **multiple X or Y axes** to your `SCIChartSurface`. 

> **_NOTE:_** As mentioned above, every axis should have a **unique ID** assigned to it. By default, each axis has a `DefaultAxisId` which isn't sufficient for multiple X or Y Axes.

## Aligning an Axis Inside a Chart
To **change the position** of an axis, set `SCIAxisAlignment` on it:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    xAxis.axisAlignment = SCIAxisAlignment_Bottom;
    yAxis.axisAlignment = SCIAxisAlignment_Left;
</div>
<div class="code-snippet" id="swift">
    xAxis.axisAlignment = .bottom
    yAxis.axisAlignment = .left
</div>
<div class="code-snippet" id="cs">
    xAxis.AxisAlignment = SCIAxisAlignment.Bottom;
    yAxis.AxisAlignment = SCIAxisAlignment.Left;
</div>

Also, it's possible pointing an axis **inward or outward** relative to the chart area. It requires setting `ISCIAxis.isCenterAxis` property on an axis. By default, it is set to `false`, thus axis labels and ticks are turned outside a chart. Setting it to `true` will provide the following output:

![Axes inside the Chart](img/axis-2d/axes-inside-the-chart.png)

> **_NOTE:_** You might want to create **Vertical(Rotated)** Charts, to learn more - refer to the [Create a Vertical Chart](axis-alignment---create-a-vertical-chart.html) article or the [Vertical Chart example](https://www.scichart.com/example/ios-chart-example-vertical-charts/).

## Changing Axis Direction
You can change **Axis Direction** via `ISCIAxisCore.flipCoordinates` property. By default it's false. Please see the difference below:

| **Default**                                       | **Flipped Y Coordinates**                                   |
| ------------------------------------------------- | ----------------------------------------------------------- |
| ![Default](img/axis-2d/impulse-chart-example.png) | ![Flipped](img/axis-2d/impulse-chart-example-y-flipped.png) |

## Central Axis
Placing an axis in the center of a chart is a bit more advanced topic. It requires changes to the layout process in `ISCILayoutManager` to specify the exact axis position inside a chart area. Please refer to the [Central Axis](axis-layout---central-axis.html) article or the [Shifted Axes example]() for more info.

![Default](img/axis-2d/shifted-axes-example.png)

## Stacking Multiple Axes Vertically or Horizontally
It is also possible to configure chart layout to have **axes** placed **one next to another** vertically or horizontally. This requires changes to the layout process in `ISCILayoutManager`. Please refer to the [Stack Axes Vertically or Horizontally](axis-layout---stack-axes-vertically-or-horizontally.html) article or the [Vertically Stacked Y Axes]() example for more info.

![Default](img/axis-2d/vertically-stacked-axes-example.png)

> **_NOTE:_** Every **RenderableSeries** (chart types e.g. `SCIFastLineRenderableSeries`, `SCIFastCandlestickRenderableSeries` etc.), every **[Annotation](Annotations APIs.html)** and some **Chart Modifiers** (e.g. `SCIPinchZoomModifier`, `SCIZoomPanModifier`) requires to be measured against **particular axis** (in other words - **attached** to it). You **must** specify the **Axis ID** for them via the `ISCIRenderableSeries.xAxisId` and `ISCIRenderableSeries.yAxisId` properties.
>
> However, If you have only a **single X and Y Axis**, setting these ID properties **isn't required**. This is **required** only for the **multiple axis** cases.
