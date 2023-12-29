# SCIRolloverModifier
The `SCIRolloverModifier` can be used to show tooltips for **all the series** under the vertical line drawn at the touch position:

> **_NOTE:_** The **`SCIRolloverModifier`** is specifically suited to inspect data across many series are the same. For scatter charts, or irregular charts, please try the [SCITooltipModifier](interactivity---scitooltipmodifier.html).

![Rollover Modifier](img/modifiers-2d/rollover-modifier-example.png)

> **_NOTE:_** Examples of the **`SCIRolloverModifier`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-chart-tooltips-using-rollovermodifier/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-chart-using-rollovermodifier-tooltips-example/)

## SCIRolloverModifier Features
Besides the SCIRolloverModifier [specific features](#specific-features), there are some [common features](#common-features) which are shared between [SCITooltipModifier](interactivity---scitooltipmodifier.html), [SCIRolloverModifier](interactivity---scirollovermodifier.html) and [SCICursorModifier](interactivity---scicursormodifier.html) via common `SCITooltipModifierBase` class.

#### Common Features

| **Feature**                               | **Description**                                                                                                                                                 |
| ----------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCITooltipModifierBase.showTooltip`      | Allows to **hide or show** modifier's Tooltips.                                                                                                                 |
| `SCITooltipModifierBase.useInterpolation` | Allows to show **interpolated** values between data points. It is set to `YES` by default. If `NO` - modifier's Tooltips will report the info about **closest** data points. |
| `SCITooltipModifierBase.sourceMode`       | Allows to specify which `ISCIRenderableSeries` are to be inspected by a modifier, e.g. **Visible**, **Selected**, etc. Other will be ignored by the modifier. Expects a member of the `SCISourceMode` enumeration. |

#### Specific Features

| **Feature**                                          | **Description**                                                                       |
| ---------------------------------------------------- | ------------------------------------------------------------------------------------- |
| `SCITooltipModifierWithAxisLabelsBase.showAxisLabel` | Allows to **hide or show** Rollover's **axis label**                                  |
| `SCIRolloverModifier.drawVerticalLine`               | Allows to **hide or show** Rollover's **vertical line**.                              |
| `SCIRolloverModifier.verticalLineStyle`              | Allows to specify `SCIPenStyle` which will be used to draw rollover **vertical line** |

## Adding a SCIRolloverModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the `ISCIChartSurface.chartModifiers` property and `SCIRolloverModifier` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier and add modifier to the surface
    [self.surface.chartModifiers add:[SCIRolloverModifier new]];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier and add modifier to the surface
    self.surface.chartModifiers.add(SCIRolloverModifier())
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier and add modifier to the surface
    Surface.ChartModifiers.Add(new SCIRolloverModifier());
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.
