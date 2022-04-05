# SciChart iOS 3D Tutorial - Zooming and Rotating
So far, we have created a 3D chart, added XAxis, YAxis and ZAxis and 3D scatter series.

In this SciChart iOS 3D tutorial we're going to add some **interactivity** for 3D chart, that at the end we should be able to **zoom** and **rotate** camera around the chart.

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2002%20-%20Zooming%20and%20Rotating)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-02)

First of all, make sure, you've read the [Tutorial 01 - Create a simple Scatter Chart 3D](3d-tutorial-01---create-a-simple-scatter-chart-3d.html) and have a basic understanding of how to use SciChart 3D.

Now, let's give some definition for Zooming and Rotation:
- **zoom** — means to enlarge the chart by zooming in on a section. You use two fingers to do this. But it's more than just zooming into a 2D drawing, the **perspective** changes as you move throughout the 3D space, creating the illusion that you are moving inside the cube which is the chart.
- **rotate** — means to move rotate the camera and move it up and down. Remember that the **camera** is your perspective, or the projection from your eye of the chart onto the 2D surface of the screen.

## 3D ChartModifiers
In SciChart iOS 3D, chart interactions are defined by [3D ChartModifiers](Chart Modifier 3D APIs.html).
In addition to the default SciChart modifiers you can write custom modifiers or extends existing ones.

Here is the list of modifiers available out of the box in SciChart:
- [SCIZoomExtentsModifier3D](zoom-and-pan---zoom-extents-modifier-3d.html)
- [SCIPinchZoomModifier3D](zoom-and-pan---pinch-zoom-modifier-3d.html)
- [SCIOrbitModifier3D](zoom-and-pan---orbit-modifier-3d.html)
- [SCIFreeLookModifier3D](zoom-and-pan---free-look-modifier-3d.html)
- [SCIVertexSelectionModifier3D](interactivity---vertex-selection-modifier-3d.html)
- [SCITooltipModifier3D](interactivity---tooltip-modifier-3d.html)
- `SCILegendModifier3D`
- `SCIModifierGroup3D`

## Adding 3D Chart Modifiers
Now we are going to create and configure a couple of **modifiers** and add a set of them as modifier collection of the `SCIChartSurface3D`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [self.surface.chartModifiers addAll:[SCIZoomExtentsModifier3D new], [SCIPinchZoomModifier3D new], [SCIOrbitModifier3D new], nil];
</div>
<div class="code-snippet" id="swift">
    self.surface.chartModifiers.add(items: SCIZoomExtentsModifier3D(), SCIPinchZoomModifier3D(), SCIOrbitModifier3D())
</div>
<div class="code-snippet" id="cs">
    Surface.ChartModifiers = new SCIChartModifier3DCollection
    {
        new SCIZoomExtentsModifier3D(),
        new SCIPinchZoomModifier3D(),
        new SCIOrbitModifier3D()
    };
</div>

<video autoplay loop muted playsinline src="img/tutorials-3d/tutorials-3d-chart-modifiers.mp4"></video>

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2002%20-%20Zooming%20and%20Rotating)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-02)

Also, you can found **next tutorial** from this series here - [SciChart iOS 3D Tutorial - Cursors and Tooltips](3d-tutorial-03---cursors-and-tooltips.html)

Of course, this is not the maximum of what you can achieve with the SciChart iOS 3D.
You can find more information about modifiers which are used in this tutorial in the articles below:
- [Zoom Extents Modifier 3D](zoom-and-pan---zoom-extents-modifier-3d.html)
- [Pinch Zoom Modifier 3D](zoom-and-pan---pinch-zoom-modifier-3d.html)
- [Orbit Modifier 3D](zoom-and-pan---orbit-modifier-3d.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples)
