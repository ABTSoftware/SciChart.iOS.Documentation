# The SCIXabcdAnnotation
The `SCIXabcdAnnotation` is a specialized multi-point trading annotation used to draw harmonic patterns such as Gartley, Butterfly, Bat, and Crab. These patterns consist of five key points: X, A, B, C, and D, which help identify potential reversal zones in financial charts.

![Xabcd Annotation](img/annotations/xabcd-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Trading Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-macos-trading-annotation-example/) ̰

The `SCICompositeAnnotation` class provides the `SCICompositeAnnotation.borderPen` and `SCICompositeAnnotation.fillBrush` properties, which are used for the annotation outline and background and expects the `SCIPenStyle` and `SCIBrushStyle` correspondingly. 
To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

## Structure and Points
The SCIXabcdAnnotation is defined by five base points, added sequentially using the setBasePointWithX(:y) method:
- Point 0 (X): Starting point of the pattern
- Point 1 (A): First leg of the pattern
- Point 2 (B): Retracement from A
- Point 3 (C): Extension from B
- Point 4 (D): Final point completing the harmonic structure
kDPointIndex = 4 indicates the index of the final D point.

## Behavior
- Lines are drawn between XA, AB, BC, and CD.
- Additional helper (often dashed) lines may connect XB, AC, BD, and XD.
- Two filled regions are typically formed:
    * Triangle XAB
    * Triangle BCD

The SCIXabcdAnnotation can be configured using the properties and method listed in the table below:

| **Field**                       | **Description**                                                           |
| ------------------------------- | ------------------------------------------------------------------------- |
| `SCIXabcdAnnotation.showRatios` | Determines whether calculated harmonic ratios are displayed on the chart. |


## Define Points
Points are added sequentially using `[annotation setBasePointWithX: y:];`. Each call adds the next point in order (X → A → B → C → D).
The annotation is fully formed once all five points are provided.


> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a XabcdAnnotation
A `SCIXabcdAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Assume a surface has been created and configured somewhere
id<ISCIChartSurface> surface;

SCIXabcdAnnotation *xAbcdAnn = [[SCIXabcdAnnotation alloc] init];

// Define points (X, A, B, C, D)
[xAbcdAnn setBasePointWithX:@224 y:@7];   // X
[xAbcdAnn setBasePointWithX:@230 y:@4];   // A
[xAbcdAnn setBasePointWithX:@235 y:@5];   // B
[xAbcdAnn setBasePointWithX:@240 y:@6];   // C
[xAbcdAnn setBasePointWithX:@245 y:@7];   // D

// Customize appearance
xAbcdAnn.fillBrush = [[SCISolidBrushStyle alloc] initWithColorCode:0x55AAAA00];
xAbcdAnn.stroke    = [[SCISolidPenStyle alloc] initWithColorCode:0xFFe97064 thickness:2.0];

// Enable interaction and ratios
xAbcdAnn.isEditable = YES;
xAbcdAnn.showRatios = YES;

// Add to chart surface
[self.surface.annotations add:xAbcdAnn];
</div>
<div class="code-snippet" id="swift">
// Assume a surface has been created and configured somewhere
let surface: ISCIChartSurface

let xAbcdAnn = SCIXabcdAnnotation()

// Add points
xAbcdAnn.setBasePointWithX(NSNumber(value: 224), y: NSNumber(value: 7))   // X
xAbcdAnn.setBasePointWithX(NSNumber(value: 230), y: NSNumber(value: 4))   // A
xAbcdAnn.setBasePointWithX(NSNumber(value: 235), y: NSNumber(value: 5))   // B
xAbcdAnn.setBasePointWithX(NSNumber(value: 240), y: NSNumber(value: 6))   // C
xAbcdAnn.setBasePointWithX(NSNumber(value: 245), y: NSNumber(value: 7))   // D

// Customize appearance
xAbcdAnn.fillBrush = SCISolidBrushStyle(color: 0x55AAAA00)
xAbcdAnn.stroke = SCISolidPenStyle(color: 0xFFe97064, thickness: 2)

// Enable interaction and ratios
xAbcdAnn.isEditable = true
xAbcdAnn.showRatios = true

// Add to chart surface
self.surface.annotations.add(items: xAbcdAnn)
</div>

> **_NOTE:_** For interactive creation of SCIXabcdAnnotation, use the corresponding annotation creation modifier [SCIXabcdCreationModifier](trading-annotation---scixabcdcreationmodifier.html) available in SciChart iOS.

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.
