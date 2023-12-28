# Tooltip Modifier 3D
In SciChart iOS 3D you can add Tooltips onto `SCIChartSurface3D` using the `SCITooltipModifier3D`.
It's derived from the `SCIChartModifier3DBase` and executes on touch over the ***data-point*** and shows tooltips under the pointer.

![Tooltip Modifier 3D](img/modifiers-3d/tooltip-modifier-3d-example.png)

> **_NOTE:_** Examples of the **`SCITooltipModifier3D`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-series-tooltips/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-3d-chart-example-series-tooltips/)

## SCITooltipModifier3D Usage
The `SCITooltipModifier3D` allows inspecting [RenderableSeries 3D](3D Chart Types.html) at a touch point.
For convenience, the actual **hit-test point** is located a bit above the actual touch point. It is marked with a small "X" sign. 
Tooltips will appear to the side of it, showing the ***hit-test*** result of the topmost `ISCIRenderableSeries3D` at the "X" location:

![Tooltip Modifier Usage](img/modifiers-3d/tooltip-modifier-3d-usage.png)

For hit-testing series parts that are close to the chart boundaries, a multi-touch finger drag can be used, which makes ***hit-test*** point appear in between of two finger touches, same way as it works with [2D TooltipModifier](interactivity---scitooltipmodifier.html#scitooltipmodifier-usage). 

## SCITooltipModifier3D Features
The `SCITooltipModifier3D` has a bunch of the configuration properties listed in the table below, some of them are inherited from its base class - `SCITooltipModifierBase`:

| **Feature**                                 | **Description**                                                                                                                                                                     |
| ------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCITooltipModifierBase3D.showTooltip`      | Allows to **hide or show** modifier's Tooltips.                                                                                                                                     |
| `SCITooltipModifierBase3D.sourceMode`       | Allows to specify which `ISCIRenderableSeries` are to be inspected by a modifier, e.g. **Visible**, **Selected**, etc. Other will be ignored by the modifier. Expects a member of the `SCISourceMode` enumeration. |
| `SCITooltipModifier3D.showAxisLabels`       | Allows to **hide or show** Tooltips **axis labels**                                                                                                                                 |
| `SCITooltipModifier3D.offset`               | Specifies **how far** the hit-test point is **from** the actual **touch point**. This value will be used for either `X` or `Y` coordinate, or both, depending on `markerPlacement`. |
| `SCITooltipModifier3D.customPointOffset`    | Specifies **how far** the hit-test point is **from** the actual **touch point**. As opposed to `offset`, both `X` and `Y` coordinate will always be applied.                        |
| `SCITooltipModifier3D.markerPlacement`      | Allows to specify the **position** of the hit-test point relative to the **touch point**, e.g. Left, Top, etc... Expects a member of the `SCIPlacement` enumeration.                |
| `SCITooltipModifier3D.tooltipPointMarkerPaintStyle` | Allows to specify `SCIPenStyle` which will be used to draw **"X" marker**.                                                                                                  |
| `SCITooltipModifier3D.crosshairStrokeStyle` | Allows to specify the `SCIPenStyle` which will be used to draw ***Crosshair*** strokes                                                                                              |
| `SCITooltipModifier3D.crosshairPlanesFill`  | Allows to specify the color to draw Crosshair planes with, if the `SCICrosshairMode.SCICrosshairMode_Planes` is selected.                                                           |
| `SCITooltipModifier3D.crosshairMode`        | Allows to specify the **crosshair mode**, could be **Planes** or **Lines**. Expects `SCICrosshairMode` enumeration.                                                                 |
| `SCITooltipModifier3D.projectionMode`       | Defines the projection mode used to draw Crosshair. Expects the `SCIProjectionMode` enumeration.                                                                                    |
| `SCITooltipModifier3D.lineProjectionMode`   | Allows to specify the planes, onto which the crosshair will be projected - **XY**, **XZ**, **YZ** or any combination of planes. Expects `SCILineProjectionMode` enumeration.        |

## Adding a SCITooltipModifier3D to a Chart
Any [Chart Modifier 3D](Chart Modifier 3D APIs.html) can be [added to a `SCIChartSurface3D`](Chart Modifier 3D APIs.html#adding-a-chart-modifier-3d) via the `ISCIChartSurface3D.chartModifiers` property and `SCITooltipModifier3D` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface3D&gt; surface;

    // Create a Modifier and add modifier to the surface
    [self.surface.chartModifiers add:[SCITooltipModifier3D new]];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface3D

    // Create a Modifier and add modifier to the surface
    self.surface.chartModifiers.add(SCITooltipModifier3D())
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface3D surface;

    // Create a Modifier and add modifier to the surface
    Surface.ChartModifiers.Add(new SCITooltipModifier3D());
</div>

> **_NOTE:_** To learn more about features available, visit the [Chart Modifier 3D APIs](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) article.

## Customizing Tooltip Modifier 3D Tooltips
In SciChart, you can ***fully customize*** tooltips for `SCITooltipModifier3D`.
This customization is achieved via the `ISCISeriesInfo3DProvider` and `ISCISeriesTooltip3D` protocols.
Moreover - tooltips can be made **unique** per a **RenderableSeries** instance via the `ISCIRenderableSeries3D.seriesInfoProvider` property.

![Custom Series Tooltips 3D Example](img/modifiers-3d/custom-series-tooltips-3d-chart-example.png)

> **_NOTE:_** Examples of the `SCITooltipModifier3D` **customization** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-custom-series-tooltips/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-3d-chart-example-custom-series-tooltips/)

To have ***fully custom*** tooltip for your modifier, you will need to provide **custom** `ISCISeriesInfo3DProvider` for your **RenderableSeries** via inheriting from `SCISeriesInfo3DProviderBase` which contains some base functionality.
From there - you might want to override one of the following (or both):

- `-getSeriesInfoInternal` - allows to provide custom implementation of `SCISeriesInfo3D`, which simply contains information about a **RenderableSeries** and should be created based on it
- `-getSeriesTooltipInternalWithSeriesInfo:modifierType:` - allows to provide **custom tooltip** for your series, based on `seriesInfo` and `modifierType`

> **_NOTE:_** For more information about **SCISeriesInfo3D**, its types and place inside SciChart, you can read corresponding article from 2D Documentation - [SCISeriesInfo – Models for Tooltips and Legends](sciseriesinfo---models-for-tooltips-and-legends.html).

##### Customization SCITooltipModifier3D Example
First thing, we will need to create custom `ISCISeriesTooltip3D` and implement `-internalUpdate:` method in which we update tooltip instance based on passed in `SCISeriesInfo3D` instance. 
Then, in custom `ISCISeriesInfo3DProvider` we override `-getSeriesTooltipInternalWithSeriesInfo:modifierType` and provide our custom tooltip there.
Finally, we provide our **custom** SeriesInfo3DProvider to our `ISCIRenderableSeries3D` instance via the corresponding property.

Let's see the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCISeriesTooltip3DBase+Protected.h&gt;
    #import &lt;SciChart/SCISeriesInfo3DProviderBase+Protected.h&gt;

    @interface CustomXyzSeriesTooltip3D : SCIXyzSeriesTooltip3D
    @end
    @implementation CustomXyzSeriesTooltip3D
    - (void)internalUpdate:(SCISeriesInfo3D *)seriesInfo {
        NSMutableString *tooltipText = @"This is Custom Tooltip \n".mutableCopy;
        [tooltipText appendFormat:@"Vertex id: %d\n", ((SCIXyzSeriesInfo3D *)seriesInfo).vertexId];
        [tooltipText appendFormat:@"X: %@\n", seriesInfo.formattedXValue.rawString];
        [tooltipText appendFormat:@"Y: %@\n", seriesInfo.formattedYValue.rawString];
        [tooltipText appendFormat:@"Z: %@", seriesInfo.formattedZValue.rawString];
        self.text = tooltipText;
        
        [self setSeriesColor:seriesInfo.seriesColor.colorARGBCode];
        
        [self setTooltipBackground:0xffe2460c];
        [self setTooltipStroke:0xffff4500];
        [self setTooltipTextColor:0xffffffff];
    }
    @end
    ...
    @interface CustomSeriesInfo3DProvider : SCIDefaultXyzSeriesInfo3DProvider
    @end
    @implementation CustomSeriesInfo3DProvider

    - (id&lt;ISCISeriesTooltip3D&gt;)getSeriesTooltipInternalWithSeriesInfo:(SCISeriesInfo3D *)seriesInfo modifierType:(Class)modifierType {
        if (modifierType == SCITooltipModifier3D.class) {
            return [[CustomXyzSeriesTooltip3D alloc] initWithSeriesInfo:seriesInfo];
        } else {
            return [super getSeriesTooltipInternalWithSeriesInfo:seriesInfo modifierType:modifierType];
        }
    }
    @end
    ...
    SCIScatterRenderableSeries3D *scatterSeries3D = [SCIScatterRenderableSeries3D new];
    scatterSeries3D.seriesInfoProvider = [CustomSeriesInfo3DProvider new];
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCISeriesTooltip3DBase
    import SciChart.Protected.SCISeriesInfo3DProviderBase

    private class CustomSeriesInfo3DProvider: SCIDefaultXyzSeriesInfo3DProvider {
        
        private class CustomXyzSeriesTooltip3D: SCIXyzSeriesTooltip3D {
            override func internalUpdate(_ seriesInfo: SCISeriesInfo3D) {
                var tooltipText = "This is Custom Tooltip \n"
                tooltipText += "Vertex id: \((seriesInfo as? SCIXyzSeriesInfo3D)?.vertexId ?? 0)\n"
                tooltipText += "X: \(seriesInfo.formattedXValue()?.rawString ?? "-")\n"
                tooltipText += "Y: \(seriesInfo.formattedYValue()?.rawString ?? "-")\n"
                tooltipText += "z: \(seriesInfo.formattedZValue()?.rawString ?? "-")"
                text = tooltipText
                setSeriesColor(seriesInfo.seriesColor.colorARGBCode())
                
                setTooltipBackground(0xffe2460c);
                setTooltipStroke(0xffff4500);
                setTooltipTextColor(0xffffffff);
            }
        }
        
        override func getSeriesTooltipInternal(seriesInfo: SCISeriesInfo3D, modifierType: AnyClass) -> ISCISeriesTooltip3D {
            if modifierType == SCITooltipModifier3D.self {
                return CustomXyzSeriesTooltip3D(seriesInfo: seriesInfo)
            } else {
                return super.getSeriesTooltipInternal(seriesInfo: seriesInfo, modifierType: modifierType)
            }
        }
    }
    ...
    let scatterSeries3D = SCIScatterRenderableSeries3D()
    scatterSeries3D.seriesInfoProvider = CustomSeriesInfo3DProvider()
</div>
<div class="code-snippet" id="cs">
    class CustomSeriesInfo3DProvider : SCIDefaultXyzSeriesInfo3DProvider
    {
        class CustomXyzSeriesTooltip3D : SCIXyzSeriesTooltip3D
        {
            public CustomXyzSeriesTooltip3D(SCIXyzSeriesInfo3D seriesInfo) : base(seriesInfo) { }

            protected override void InternalUpdate(SCISeriesInfo3D seriesInfo)
            {
                var xyzSeriesInfo3D = (SCIXyzSeriesInfo3D)seriesInfo;
                var tooltipText = "This is Custom Tooltip \n";
                tooltipText += $"Vertex id: {xyzSeriesInfo3D.VertexId}\n";
                tooltipText += $"X: {xyzSeriesInfo3D.FormattedXValue}\n";
                tooltipText += $"Y: {xyzSeriesInfo3D.FormattedYValue}\n";
                tooltipText += $"Z: {xyzSeriesInfo3D.FormattedZValue}";

                Text = tooltipText;
                SetSeriesColor(xyzSeriesInfo3D.SeriesColor.ColorARGBCode());

                SetTooltipBackground(0xffe2460c);
                SetTooltipStroke(0xffff4500);
                SetTooltipTextColor(0xffffffff);
            }
        }

        protected override IISCISeriesTooltip3D GetSeriesTooltipInternal(SCISeriesInfo3D seriesInfo, Class modifierType)
        {
            if (modifierType == typeof(SCITooltipModifier3D).ToClass())
            {
                return new CustomXyzSeriesTooltip3D((SCIXyzSeriesInfo3D)seriesInfo);
            }
            else
            {
                return base.GetSeriesTooltipInternal(seriesInfo, modifierType);
            }
        }
    }
    ...
    var scatterSeries3D = new SCIScatterRenderableSeries3D();
    scatterSeries3D.SeriesInfoProvider = new CustomSeriesInfo3DProvider();
</div>

> **_NOTE:_** Full example sources are available in [3D Charts -> Tooltips and HitTest 3D Charts -> Custom Serieis Tooltips 3D Charts](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-custom-series-tooltips/)

This will result in the following:

![Custom Tooltips 3D](img/modifiers-3d/custom-tooltips-3d.png)

> **_NOTE:_** A custom Tooltip has to implement the `ISCISeriesTooltip3D` or extend the `SCISeriesTooltip3DBase` class, which is derived from `UILabel`.
