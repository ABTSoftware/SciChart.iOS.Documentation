# SciChart iOS Tutorial - Tooltips and Legends
In the previous tutorials we've showed how to [Create a Simple Chart](tutorial-01---create-a-simple-2d-chart.html) and add some [Zoom and Pan](tutorial-02---zooming-and-panning-behavior.html) interaction via the [Chart Modifiers API](Chart Modifier APIs.html).

In this SciChart iOS tutorial we're going show how to add a **Legend** and **Tooltip** to the chart.

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2003%20-%20Tooltips%20and%20Legends)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-03)

## Add a Legend
In SciChart, a chart legend can be created and configured via the `SCILegendModifier`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCILegendModifier *legendModifier = [SCILegendModifier new];
    legendModifier.orientation = SCIOrientationHorizontal;
    legendModifier.margins = UIEdgeInsetsMake(0, 0, 10, 0);
    legendModifier.position = SCIAlignment_Bottom | SCIAlignment_CenterHorizontal;

    [self.surface.chartModifiers add:tooltipModifier];
</div>
<div class="code-snippet" id="swift">
    let legendModifier = SCILegendModifier()
    legendModifier.orientation = .horizontal
    legendModifier.position = [.bottom, .centerHorizontal]
    legendModifier.margins = UIEdgeInsets(top: 0, left: 0, bottom: 10, right: 0)

    self.surface.chartModifiers.add(legendModifier)
</div>
<div class="code-snippet" id="cs">
    var legendModifier = new SCILegendModifier
    {
        Orientation = SCIOrientation.Horizontal,
        Margins = new UIEdgeInsets(0, 0, 10, 0),
        Position = SCIAlignment.Bottom | SCIAlignment.CenterHorizontal
    };

    Surface.ChartModifiers.Add(legendModifier);
</div>

Also, if you want to have your series properly named inside the **Legend**, you will need to provide **Series Names** for your DataSeries instances, like showed below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    lineDataSeries.seriesName = @"Line Series";
    scatterDataSeries.seriesName = @"Scatter Series";
</div>
<div class="code-snippet" id="swift">
    lineDataSeries.seriesName = "Line Series"
    scatterDataSeries.seriesName = "Scatter Series"
</div>
<div class="code-snippet" id="cs">
    lineDataSeries.SeriesName = "Line Series";
    scatterDataSeries.SeriesName = "Scatter Series";
</div>

> **_NOTE:_** You can find more information about legends in SciChart in the [Legend Modifier](legend-modifier.html) article.

![Legend Modifier](img/tutorials-2d/tutorials-2d-legend.png)

## Adding tooltips using SCIRolloverModifier
[Rollover Modifier](interactivity---scirollovermodifier.html) adds a vertical section onto a `SCIChartSurface`.
When you put your finger on the screen - it shows all series values at the X-Coordinate of that point.

Adding the `SCIRolloverModifier` is fairly simple with just creation new instance and adding to the `ISCIChartSurface.chartModifiers`, like below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [self.surface.chartModifiers add:[SCIRolloverModifier new]];
</div>
<div class="code-snippet" id="swift">
    self.surface.chartModifiers.add(SCIRolloverModifier())
</div>
<div class="code-snippet" id="cs">
    Surface.ChartModifiers.Add(new SCIRolloverModifier());
</div>

> **_NOTE:_** You can find more information about **RolloverModifier** in the corresponding [Rollover Modifier](interactivity---scirollovermodifier.html) article.

![Rollover Modifier](img/tutorials-2d/tutorials-2d-rollover.png)

## Where to Go From Here?
In SciChart there are a bunch of modifiers, which are able to provide information about series (inspect series), such as:
- [SCITooltipModifier](interactivity---scitooltipmodifier.html)
- [SCIRolloverModifier](interactivity---scirollovermodifier.html)
- [SCICursorModifier](interactivity---scicursormodifier.html)

Also, all of the modifiers is highly customizable, and you can find more information in the [Tooltips Customization](interactivity---tooltips-customization.html) article.

You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2003%20-%20Tooltips%20and%20Legends)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-03)

Also, you can found **next tutorial** from this series here - [SciChart iOS Tutorial - Adding Realtime Updates](tutorial-04---adding-realtime-updates.html)

Of course, this is not the maximum limit of what you can achieve with the SciChart iOS.
You can find more information about modifiers which are used in this tutorial in the articles below:
- [Zoom Extents Modifier](zoom-and-pan---scizoomextentsmodifier.html)
- [Pinch Zoom Modifier](zoom-and-pan---scipinchzoommodifier.html)
- [Zoom Pan Modifier](zoom-and-pan---scizoompanmodifier.html)
- [Rollover Modifier](interactivity---scirollovermodifier.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).
