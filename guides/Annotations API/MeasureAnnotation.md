# The SCIMeasureAnnotation
The SCIMeasureAnnotation measures the change between two points. It renders a rectangle, horizontal and vertical arrows and a dedicated measurement label. It is commonly used to analyze the magnitude and duration of a price swing, drawing a center line with arrowheads, a filled region indicating the measured range, and a label reporting the calculated change.

![Measure Annotation](img/annotations/measure-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
>
> - Trading Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-macos-trading-annotation-example/)

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

## Structure and Points
The SCIMeasureAnnotation is defined by two base points, added sequentially using the setBasePointWithX(:y) method:
- Point 0: The starting anchor point of the measurement
- Point 1: The end point of the measurement

## Behavior
- A center line is drawn between the two points, capped with an arrow head pointing toward the second point.
- A filled region is drawn around the measured range, bounded by the outline defined by the active stroke.
- The styling used depends on the relative position of the second point:
    * If the second point is **above** the first, the `growingStroke` and `growingFill` are used.
    * If the second point is **below** the first, the `decliningStroke` and `decliningFill` are used.
- A label is drawn showing the measured change, scaled by `yValueScaleFactor`. The label background is taken from the currently active stroke colour (growing or declining).

The SCIMeasureAnnotation can be configured using the properties listed in the table below:

| **Field**                                | **Description**                                                                                                                                      |
| ---------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCIMeasureAnnotation.growingStroke`     | The `SCIPenStyle` used for the outline and centre line when the second point is above the first.                                                     |
| `SCIMeasureAnnotation.growingFill`       | The `SCIBrushStyle` used to fill the measured area when the second point is above the first.                                                         |
| `SCIMeasureAnnotation.decliningStroke`   | The `SCIPenStyle` used for the outline and centre line when the second point is below the first.                                                     |
| `SCIMeasureAnnotation.decliningFill`     | The `SCIBrushStyle` used to fill the measured area when the second point is below the first.                                                         |
| `SCIMeasureAnnotation.arrowHeadLength`   | The length of the centre line arrow head, in points.                                                                                                 |
| `SCIMeasureAnnotation.arrowHeadWidth`    | The width of the centre line arrow head, in points.                                                                                                  |
| `SCIMeasureAnnotation.labelStyle`        | The `SCIFontStyle` used by the measurement label. The label background is taken from the active stroke colour.                                       |
| `SCIMeasureAnnotation.labelPadding`      | The padding applied inside the measurement label.                                                                                                    |
| `SCIMeasureAnnotation.yValueScaleFactor` | The factor applied to the price change to produce the scaled value shown in the label. Defaults to `100`, reporting the change in basis-point style. |
| `SCIMeasureAnnotation.snapToCandles`     | Determines whether the anchor points snap to whole candles. Only takes effect when the X-Axis is a category axis, where a data value is a bar index. |

## Define Points
Points are added sequentially using `[annotation setBasePointWithX: y:];`. Each call adds the next point in order (Point 0 → Point 1).
The annotation is fully formed once both points are provided.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a MeasureAnnotation
A `SCIMeasureAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Assume a surface has been created and configured somewhere
id<ISCIChartSurface> surface;

SCIMeasureAnnotation *measureAnn = [[SCIMeasureAnnotation alloc] init];

// Define points
[measureAnn setBasePointWithX:@150 y:@11577.05];
[measureAnn setBasePointWithX:@220 y:@10500];

// Customize appearance for a growing move (point 2 above point 1)
measureAnn.growingStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF2563EB thickness:2.0];
measureAnn.growingFill   = [[SCISolidBrushStyle alloc] initWithColorCode:0x292563EB];

// Customize appearance for a declining move (point 2 below point 1)
measureAnn.decliningStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFDC2626 thickness:2.0];
measureAnn.decliningFill   = [[SCISolidBrushStyle alloc] initWithColorCode:0x29DC2626];

// Configure label scaling and candle snapping
measureAnn.yValueScaleFactor = 100;
measureAnn.snapToCandles = YES;

// Enable interaction
measureAnn.isEditable = YES;

// Add to chart surface
[self.surface.annotations add:measureAnn];
</div>
<div class="code-snippet" id="swift">
// Assume a surface has been created and configured somewhere
let surface: ISCIChartSurface

let measureAnnotation = SCIMeasureAnnotation()

// Define points
measureAnnotation.setBasePointWithX(NSNumber(value: 150), y: NSNumber(value: 11577.05))
measureAnnotation.setBasePointWithX(NSNumber(value: 220), y: NSNumber(value: 10500))

// Customize appearance for a growing move (point 2 above point 1)
measureAnnotation.growingStroke = SCISolidPenStyle(color: 0xFF2563EB, thickness: 2)

// Customize appearance for a declining move (point 2 below point 1)
measureAnnotation.growingFill = SCISolidBrushStyle(color: 0x292563EB)
measureAnnotation.decliningStroke = SCISolidPenStyle(color: 0xFFDC2626, thickness: 2)
measureAnnotation.decliningFill = SCISolidBrushStyle(color: 0x29DC2626)

// Configure label scaling and candle snapping
measureAnnotation.yValueScaleFactor = 100
measureAnnotation.snapToCandles = true

// Enable interaction
measureAnnotation.isEditable = true

// Add to chart surface
self.surface.annotations.add(items: measureAnnotation)
</div>

> **_NOTE:_** For interactive creation of SCIMeasureAnnotation, use the corresponding annotation creation modifier [SCIMeasureCreationModifier](trading-annotation---scimeasurecreationmodifier.html) available in SciChart iOS.

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.