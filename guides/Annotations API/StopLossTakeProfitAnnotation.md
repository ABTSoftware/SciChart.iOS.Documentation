# The SCIStopLossTakeProfitAnnotation
The SCIStopLossTakeProfitAnnotation is a trade planning tool placed on the chart using two anchor points, which marks the zone between an entry level and a target level. It is commonly used to visualize a planned trade's risk and reward before it is placed.

![Stop Loss / Take Profit Annotation](img/annotations/stoplosstakeprofit-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
>
> - Trading Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-macos-trading-annotation-example/)

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

## Structure and Points
The SCIStopLossTakeProfitAnnotation is defined by two base points, added sequentially using the setBasePointWithX(:y) method:
- Point 0: The entry level of the planned trade
- Point 1: The target level of the planned trade

## Behavior
- The zone between the two points is bounded by two level lines, one at each anchor point.
- The styling used depends on the relative position of the second point:
    * If the second point is **above** the first, the zone is coloured as a take profit, using `takeProfitStroke` and `takeProfitFill`.
    * If the second point is **below** the first, the zone is coloured as a stop loss, using `stopLossStroke` and `stopLossFill`.
- When `showAxisLabels` is enabled, price badges are drawn on the Y-Axis for the two levels, and badges are drawn on the X-Axis at the two anchor points. A band is also filled between the Y-Axis badges, with opacity controlled by `axisSpanFillOpacity`.
- Additional point or segment labels can be drawn via the `labels` property, with their text optionally overridden dynamically via `formatLabel`.

The SCIStopLossTakeProfitAnnotation can be configured using the properties listed in the table below:

| **Field**                                             | **Description**                                                                                                                                                                                                                                                                                  |
| ----------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `SCIStopLossTakeProfitAnnotation.takeProfitStroke`    | The `SCIPenStyle` used for the level lines when the second point is above the first.                                                                                                                                                                                                             |
| `SCIStopLossTakeProfitAnnotation.takeProfitFill`      | The `SCIBrushStyle` used to fill the zone when the second point is above the first.                                                                                                                                                                                                              |
| `SCIStopLossTakeProfitAnnotation.stopLossStroke`      | The `SCIPenStyle` used for the level lines when the second point is below the first.                                                                                                                                                                                                             |
| `SCIStopLossTakeProfitAnnotation.stopLossFill`        | The `SCIBrushStyle` used to fill the zone when the second point is below the first.                                                                                                                                                                                                              |
| `SCIStopLossTakeProfitAnnotation.showAxisLabels`      | Determines whether axis labels are shown - price badges on the Y-Axis for the two levels, and badges on the X-Axis at the two anchor points.                                                                                                                                                     |
| `SCIStopLossTakeProfitAnnotation.axisSpanFillOpacity` | The opacity of the band filled on the axes between the two axis labels. Set to `0` to hide the band. Defaults to `0.2`.                                                                                                                                                                          |
| `SCIStopLossTakeProfitAnnotation.labels`              | The labels drawn by this annotation. Empty by default, in which case no point or segment labels are drawn. Axis-anchored labels with an `X` or `Both` draw mode add badges on the X-Axis; the Y-Axis badges are always derived from the two levels and are governed by `showAxisLabels` instead. |
| `SCIStopLossTakeProfitAnnotation.formatLabel`         | A block which supplies the complete text of each label, re-evaluated whenever the annotation changes. Takes precedence over `SCIMultiPointLabel.text`. Returning `nil`, an empty, or a whitespace-only string falls back to that text; when neither yields text the label is not drawn.          |

## Define Points
Points are added sequentially using `[annotation setBasePointWithX: y:];`. Each call adds the next point in order (Point 0 → Point 1).
The annotation is fully formed once both points are provided.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a StopLossTakeProfitAnnotation
A `SCIStopLossTakeProfitAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Assume a surface has been created and configured somewhere
id<ISCIChartSurface> surface;

// Create Stop Loss / Take Profit annotation with predefined points
SCIStopLossTakeProfitAnnotation *stopLossAnnotation = [SCIStopLossTakeProfitAnnotation new];
stopLossAnnotation.takeProfitStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF16A34A
                                                                        thickness:2
                                                                  strokeDashArray:@[@6, @3]
                                                                     antiAliasing:false];
stopLossAnnotation.takeProfitFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x2E16A34A];
stopLossAnnotation.stopLossStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFEF4444
                                                                      thickness:2
                                                                strokeDashArray:@[@6, @3]
                                                                   antiAliasing:false];
stopLossAnnotation.stopLossFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x2EEF4444];
stopLossAnnotation.labels = [self makeStopLossTakeProfitLabels];
stopLossAnnotation.formatLabel = [self makeStopLossTakeProfitLabelFormatter];

// Define points
[stopLossAnnotation setBasePointWithX:@200 y:@12300];
[stopLossAnnotation setBasePointWithX:@252 y:@11700];

// Enable interaction
stopLossAnnotation.isEditable = YES;

// Add to chart surface
[self.surface.annotations add:stopLossAnnotation];
</div>
<div class="code-snippet" id="swift">
// Assume a surface has been created and configured somewhere
let surface: ISCIChartSurface

// Create Stop Loss / Take Profit annotation with predefined points
let stopLossAnnotation = SCIStopLossTakeProfitAnnotation()
stopLossAnnotation.takeProfitStroke = SCISolidPenStyle(color: 0xFF16A34A, thickness: 2, strokeDashArray: [6, 3])
stopLossAnnotation.takeProfitFill = SCISolidBrushStyle(color: 0x2E16A34A)
stopLossAnnotation.stopLossStroke = SCISolidPenStyle(color: 0xFFEF4444, thickness: 2, strokeDashArray: [6, 3])
stopLossAnnotation.stopLossFill = SCISolidBrushStyle(color: 0x2EEF4444)
stopLossAnnotation.labels = self.makeStopLossTakeProfitLabels()
stopLossAnnotation.formatLabel = self.makeStopLossTakeProfitLabelFormatter()

// Define points
stopLossAnnotation.setBasePointWithX(NSNumber(value: 200), y: NSNumber(value: 12300))
stopLossAnnotation.setBasePointWithX(NSNumber(value: 252), y: NSNumber(value: 11700))

// Enable interaction
stopLossAnnotation.isEditable = true

// Add to chart surface
self.surface.annotations.add(items: stopLossAnnotation)
</div>

> **_NOTE:_** For interactive creation of SCIStopLossTakeProfitAnnotation, use the corresponding annotation creation modifier [SCIStopLossTakeProfitCreationModifier](trading-annotation---scistoplosstakeprofitcreationmodifier.html) available in SciChart iOS.

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.