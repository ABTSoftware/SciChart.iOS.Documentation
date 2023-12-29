# SCITooltipModifier
Tooltips may be added to the `SCIChartSurface` using the `SCITooltipModifier`.

> **_NOTE:_** The **`SCITooltipModifier`** is specifically suited for scatter `X-Y` data, although it may be used for any type of data in SciChart.

![Tooltip Modifier](img/modifiers-2d/tooltip-modifier-example.png)

> **_NOTE:_** Examples of the **`SCITooltipModifier`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-using-tooltip-modifier/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-using-tooltipmodifier-tooltips-example/)

## SCITooltipModifier Usage
The `SCITooltipModifier` allows inspecting [RenderableSeries](2D Chart Types.html) at a touch point. 
For convenience, the actual **hit-test point** is located a bit upper. It is marked with a small "X" sign. 
Tooltips will appear to the side of it, showing the hit-test result for all RenderableSeries at the "X" location:

![Tooltip Modifier Usage](img/modifiers-2d/tooltip-modifier-usage.png)

For hit-testing series parts that are close to the chart boundaries, a multi-touch finger drag can be used:

![Tooltip Modifier Usage Near Edge](img/modifiers-2d/tooltip-modifier-usage-near-edge.png)

## SCITooltipModifier Features
Besides the SCITooltipModifier [specific features](#specific-features), there are some [common features](#common-features) which are shared between [SCITooltipModifier](interactivity---scitooltipmodifier.html), [SCIRolloverModifier](interactivity---scirollovermodifier.html) and [SCICursorModifier](interactivity---scicursormodifier.html) via common `SCITooltipModifierBase` class.

#### Common Features

| **Feature**                               | **Description**                                                                                                                                                 |
| ----------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCITooltipModifierBase.showTooltip`      | Allows to **hide or show** modifier's Tooltips.                                                                                                                 |
| `SCITooltipModifierBase.useInterpolation` | Allows to show **interpolated** values between data points. It is set to `YES` by default. If `NO` - modifier's Tooltips will report the info about **closest** data points. |
| `SCITooltipModifierBase.sourceMode`       | Allows to specify which `ISCIRenderableSeries` are to be inspected by a modifier, e.g. **Visible**, **Selected**, etc. Other will be ignored by the modifier. Expects a member of the `SCISourceMode` enumeration. |

#### Specific Features

| **Feature**                                       | **Description**                                                                                                                                                                           |
| ------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCITooltipModifier.offset`                       | Specifies **how far** the hit-test point is **from** the actual **touch point**. This value will be used for either `X` or `Y` coordinate, or both, depending on `markerPlacement`.       |
| `SCITooltipModifier.customPointOffset`            | Specifies **how far** the hit-test point is **from** the actual **touch point**. As opposed to `offset`, both `X` and `Y` coordinate will always be applied.                              |
| `SCITooltipModifier.markerPlacement`              | Allows to specify the **position** of the hit-test point relative to the **touch point**, e.g. Left, Top, etc... Expects a member of the `SCIPlacement` enumeration.                      |
| `SCITooltipModifier.tooltipPosition`              | Allows to specify the **position** of modifier's Tooltips relative to the **hit-test point**, e.g. TopLeft, BottomRight, etc.... Expects a member of the `SCITooltipPosition` enumeration. |
| `SCITooltipModifier.tooltipPointMarkerPaintStyle` | Allows to specify `SCIPenStyle` which will be used to draw **"X" marker**                                                                                                                 |

## Adding a SCITooltipModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the`ISCIChartSurface.chartModifiers` property and `SCITooltipModifier` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier and add modifier to the surface
    [self.surface.chartModifiers add:[SCITooltipModifier new]];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier and add modifier to the surface
    self.surface.chartModifiers.add(SCITooltipModifier())
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier and add modifier to the surface
    Surface.ChartModifiers.Add(new SCITooltipModifier());
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.
