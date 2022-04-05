# SciChart iOS 3D Tutorial - Cursors and Tooltips
In the previous tutorials we've showed how to [Create a Simple 3D Chart](3d-tutorial-01---create-a-simple-scatter-chart-3d.html) and add some [Zoom and Rotate](3d-tutorial-02---zooming-and-rotating.html) interaction via the [Chart Modifiers 3D API](Chart Modifier 3D APIs.html).

In this SciChart iOS 3D tutorial we're going show how to add a **cursor** and **tooltip** to that chart.

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2003%20-%20Cursors%20and%20Tooltips)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-03)

Let's define what is Cursor ant Tooltip:
- **cursor** ⁠— a tablet or cell phone obviously does not have a mouse. Instead the mouse is your finer and the cursor is where you place your finger. It appears as a small x (cross).
- **tooltip** — is text that displays when you push the cursor onto an object, like a point plotted on a chart. You have to push the cursor onto the coordinate for the text to appear. In the case of the example below you probably need to use two fingers to zoom into the chart to make the points appear large enough so that you can see them.

## The TooltipModifier3D
We can add more renderable on the surface by simple adding them into `ISCIChartSurface3D.renderableSeries` collection property.
Similarly we can an add **additional modifiers**, such as a [Tooltip Modifier 3D](interactivity---tooltip-modifier-3d.html), which provides in SciChart.

In addition to modifiers we added in the previous tutorial, we are going to add `SCITooltipModifier3D` similarly, with the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCITooltipModifier3D *tooltipModifier = [SCITooltipModifier3D new];
    tooltipModifier.crosshairMode = SCICrosshairMode_Lines;

    [self.surface.chartModifiers addAll:[[SCIOrbitModifier3D alloc] initWithDefaultNumberOfTouches:2], [SCIZoomExtentsModifier3D new], [SCIPinchZoomModifier3D new], nil];
    [self.surface.chartModifiers add:tooltipModifier];
</div>
<div class="code-snippet" id="swift">
    let tooltipModifier = SCITooltipModifier3D()
    tooltipModifier.crosshairMode = .lines

    self.surface.chartModifiers.add(items: SCIOrbitModifier3D(defaultNumberOfTouches: 2), SCIZoomExtentsModifier3D(), SCIPinchZoomModifier3D())
    self.surface.chartModifiers.add(tooltipModifier)
</div>
<div class="code-snippet" id="cs">
    Surface.ChartModifiers = new SCIChartModifier3DCollection
    {
        new SCIOrbitModifier3D(2),
        new SCIZoomExtentsModifier3D(),
        new SCIPinchZoomModifier3D(),
        new SCITooltipModifier3D { CrosshairMode = SCICrosshairMode.Lines }
    };
</div>

> **_NOTE:_** We used `defaultNumberOfTouches = 2` for `SCIOrbitModifier3D` to omit conflict while using **TooltipModifier3D**

![Chart Modifiers 3D](img/tutorials-3d/tutorials-3d-tooltips.png)

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2003%20-%20Cursors%20and%20Tooltips)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-03)

Also, you can found **next tutorial** from this series here - [SciChart iOS 3D Tutorial - Plotting Realtime Data](3d-tutorial-04---plotting-realtime-data.html)

Of course, this is not the maximum limit of what you can achieve with the SciChart iOS 3D.
You can find more information about modifiers which are used in this tutorial in the articles below:
- [Zoom Extents Modifier 3D](zoom-and-pan---zoom-extents-modifier-3d.html)
- [Pinch Zoom Modifier 3D](zoom-and-pan---pinch-zoom-modifier-3d.html)
- [Orbit Modifier 3D](zoom-and-pan---orbit-modifier-3d.html)
- [Tooltip Modifier 3D](interactivity---tooltip-modifier-3d.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).
