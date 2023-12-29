# SCICursorModifier
SciChart features a **Cursor** or **Crosshair** modifier provided by the `SCICursorModifier`. 
It allows to display a crosshair at the **touch-point** and `X-Axis` and/or `Y-Axis` labels. 
The `SCICursorModifier` is also able to display an aggregated tooltip (all series in one tooltip).

![Cursor Modifier](img/modifiers-2d/cursor-modifier-example.png)

> **_NOTE:_** Examples of the **`SCICursorModifier`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-using-cursor-modifier/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-using-cursormodifier-tooltips-example/)

## SCICursorModifier Usage
The `SCICursorModifier` allows inspecting [RenderableSeries](2D Chart Types.html) at a touch point. 
For convenience, the actual **hit-test point** is located a bit upper.
Tooltips will appear to the side of it, showing the hit-test result for all RenderableSeries above the cursor horizontal line.
Also, the `SCICursorModifier` shows labels on axes for its horizontal, vertical lines.

![Cursor Modifier Usage](img/modifiers-2d/cursor-modifier-usage.png)

For hit-testing series parts that are close to the chart boundaries, a multi-touch finger drag can be used:

![Cursor Modifier Usage Near Edge](img/modifiers-2d/cursor-modifier-usage-near-edge.png)

## SCICursorModifier Features
Besides the SCICursorModifier [specific features](#specific-features), there are some [common features](#common-features) which are shared between [SCITooltipModifier](interactivity---scitooltipmodifier.html), [SCIRolloverModifier](interactivity---scirollovermodifier.html) and [SCICursorModifier](interactivity---scicursormodifier.html) via common `SCITooltipModifierBase` class.

#### Common Features

| **Feature**                               | **Description**                                                                                                                                                 |
| ----------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCITooltipModifierBase.showTooltip`      | Allows to **hide or show** modifier's Tooltips.                                                                                                                 |
| `SCITooltipModifierBase.useInterpolation` | Allows to show **interpolated** values between data points. It is a `YES` by default. If `NO` - modifier's Tooltips will report the info about **closest** data points. |
| `SCITooltipModifierBase.sourceMode`       | Allows to specify which `ISCIRenderableSeries` are to be inspected by a modifier, e.g. **Visible**, **Selected**, etc. Other will be ignored by the modifier. Expects a member of the `SCISourceMode` enumeration. |

#### Specific Features

| **Feature**                           | **Description**                                                                                                                                                                           |
| ------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCICursorModifier.offset`            | Specifies **how far** the hit-test point is **from** the actual **touch point**. This value will be used for either `X` or `Y` coordinate, or both, depending on `markerPlacement`.       |
| `SCICursorModifier.customPointOffset` | Specifies **how far** the hit-test point is **from** the actual **touch point**. As opposed to `offset`, both `X` and `Y` coordinate will always be applied.                              |
| `SCICursorModifier.markerPlacement`   | Allows to specify the **position** of the hit-test point relative to the **touch point**, e.g. Left, Top, etc... Expects a member of the `SCIPlacement` enumeration.                      |
| `SCICursorModifier.tooltipPosition`   | Allows to specify the **position** of modifier's Tooltips relative to the **hit-test point**, e.g. TopLeft, BottomRight, etc.... Expects a member of the `SCITooltipPosition` enumeration. |
| `SCICursorModifier.crosshairPenStyle` | Allows to specify `SCIPenStyle` which will be used to draw cursor **crosshair** lines.

## Adding a SCICursorModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the `ISCIChartSurface.chartModifiers` property and `SCICursorModifier` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier and add modifier to the surface
    [self.surface.chartModifiers add:[SCICursorModifier new]];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier and add modifier to the surface
    self.surface.chartModifiers.add(SCICursorModifier())
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier and add modifier to the surface
    Surface.ChartModifiers.Add(new SCICursorModifier());
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.
