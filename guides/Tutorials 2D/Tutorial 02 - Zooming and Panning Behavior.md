# SciChart iOS Tutorial - Zooming and Panning Behavior
So far, we have created a 2D chart, added X and Y Axes, asd well as [Line](2d-chart-types---line-series.html) and [Scatter](2d-chart-types---scatter-series.html) Series.

In this SciChart iOS tutorial we're going to add some **interactivity** to a 2D chart, so at the end we should be able to **Zoom** and **Pan** a chart as well as Zooming the Chart to the Data Extents.

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2002%20-%20Zooming%20and%20Panning%20Behavior)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-02)

First of all, make sure, you've read the [Tutorial 01 - Create a simple Chart 2D](tutorial-01---create-a-simple-2d-chart.html) and have a basic understanding of how to use SciChart.

Now, let's extend previous tutorial with some [Chart Modifiers](Chart Modifier APIs.html)

## ChartModifiers
In SciChart, chart interactions are defined by the [Chart Modifiers](Chart Modifiers APIs.html).
In addition to the SciChart modifiers available out of the box, you can write [custom modifiers](custom-modifiers---the-scichartmodifierbase-api.html) or extends existing ones.

Here is the list of modifiers available out of the box in SciChart:
- [SCIZoomExtentsModifier](zoom-and-pan---scizoomextentsmodifier.html)
- [SCIPinchZoomModifier](zoom-and-pan---scipinchzoommodifier.html)
- [SCIZoomPanModifier](zoom-and-pan---scizoompanmodifier.html)
- [SCIXAxisDragModifier](zoom-and-pan---scixaxisdragmodifier.html)
- [SCIYAxisDragModifier](zoom-and-pan---sciyaxisdragmodifier.html)
- [SCISeriesSelectionModifier](interactivity---sciseriesselectionmodifier.html)
- [SCITooltipModifier](interactivity---scitooltipmodifier.html)
- [SCIRolloverModifier](interactivity---scirollovermodifier.html)
- [SCICursorModifier](interactivity---scicursormodifier.html)
- `SCIModifierGroup`
- [SCILegendModifier](legend-modifier.html)

## Adding Chart Modifiers
Now we are going to create and configure a couple of **modifiers** and add a set of them as modifier collection of the `SCIChartSurface`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [self.surface.chartModifiers addAll:[SCIPinchZoomModifier new], [SCIZoomPanModifier new], [SCIZoomExtentsModifier new], nil];
</div>
<div class="code-snippet" id="swift">
    self.surface.chartModifiers.add(items: SCIPinchZoomModifier(), SCIZoomPanModifier(), SCIZoomExtentsModifier())
</div>
<div class="code-snippet" id="cs">
    Surface.ChartModifiers = new SCIChartModifierCollection
    {
        new SCIPinchZoomModifier(),
        new SCIZoomPanModifier(),
        new SCIZoomExtentsModifier()
    };
</div>

After running the application the chart should behave like below:

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-chart-modifiers.mp4"></video>

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2002%20-%20Zooming%20and%20Panning%20Behavior)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-02)

Also, you can found **next tutorial** from this series here - [SciChart iOS Tutorial - Tooltips and Legends](tutorial-03---tooltips-and-legends.html)

Of course, this is not the maximum of what you can achieve with the SciChart iOS.
You can find more information about modifiers which are used in this tutorial in the articles below:
- [Zoom Extents Modifier](zoom-and-pan---scizoomextentsmodifier.html)
- [Pinch Zoom Modifier](zoom-and-pan---scipinchzoommodifier.html)
- [Zoom Pan Modifier](zoom-and-pan---scizoompanmodifier.html)

Also, you might want to read about Axes drag modifiers in the following articles:
- [X Axis Drag Modifier](zoom-and-pan---scixaxisdragmodifier.html)
- [Y Axis Drag Modifier](zoom-and-pan---sciyaxisdragmodifier.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples)