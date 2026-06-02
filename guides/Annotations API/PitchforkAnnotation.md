# The SCIPitchforkAnnotation
The `SCIPitchforkAnnotation` (also known as Andrew’s Pitchfork) is a specialized trading annotation for iOS charts that uses three points to visualize potential support and resistance levels. It consists of a median line and two parallel outer lines (tines), forming a "pitchfork" shape. It supports interactive grips for dragging, polygon fills for highlighting regions, and customizable stroke/fill styles.

![Pitchfork Annotation](img/annotations/pitchfork-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Composite Annotation - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-composite-annotations-example/) ̰

## Structure and Points
The SCIPitchforkAnnotation is defined by three base points:
- Point 0 (Handle / Pivot): The starting point of the median line.
- Point 1 (Upper): The first point defining the outer boundary.
- Point 2 (Lower): The second point defining the outer boundary.

## Behavior
- The median line begins at Point 0 and passes through the midpoint between Point 1 and Point 2.
- Two additional lines (tines) are drawn parallel to the median line, originating from Point 1 and Point 2.
- The space between these lines can be filled to highlight trading zones.

The SCIPitchforkAnnotation can be configured using the properties and method listed in the table below:

| **Field**                                  | **Description**                                                                   |
| ------------------------------------------ | --------------------------------------------------------------------------------- |
| `SCIPitchforkAnnotation.tineStroke`        | Defines the SCIPenStyle used to draw the pitchfork lines (outer 4 tines).         |
| `SCIPitchforkAnnotation.halfWidthZoneFill` | Defines the SCIBrushStyle used to fill the central region around the median line. |
| `SCIPitchforkAnnotation.fullWidthZoneFill` | Defines the SCIBrushStyle used to fill the outer regions on either side.          |


To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

Points for a `SCIPitchforkAnnotation` are defined sequentially using the setBasePointWithX(:y) method. The median line is drawn from the first point through the midpoint between the second and third points. Two additional parallel lines (tines) extend from the second and third points, forming the characteristic pitchfork structure.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a PitchforkAnnotation
A `SCIPitchforkAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Assume a surface has been created and configured somewhere
id<ISCIChartSurface> surface;

SCIPitchforkAnnotation *pitchfork = [[SCIPitchforkAnnotation alloc] init];

// Define base points (pivot, upper, lower)
[pitchfork setBasePointWithX:@224 y:@11000];
[pitchfork setBasePointWithX:@250 y:@11300];
[pitchfork setBasePointWithX:@228 y:@11600];

// Customize appearance
pitchfork.halfWidthZoneFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x401E90FF];
pitchfork.fullWidthZoneFill  = [[SCISolidBrushStyle alloc] initWithColorCode:0x4000AA00];

// Enable interaction
pitchfork.isEditable = YES;

// Add to chart surface
[surface.annotations add: pitchfork];
</div>
<div class="code-snippet" id="swift">
// Assume a surface has been created and configured somewhere
let surface: ISCIChartSurface

let pitchfork = SCIPitchforkAnnotation()

// Define base points (pivot, upper, lower)
pitchfork.setBasePointWithX(NSNumber(value: 224), y: NSNumber(value: 11000))
pitchfork.setBasePointWithX(NSNumber(value: 250), y: NSNumber(value: 11300))
pitchfork.setBasePointWithX(NSNumber(value: 228), y: NSNumber(value: 11600))

// Customize appearance
pitchfork.halfWidthZoneFill = SCISolidBrushStyle(color: 0x401E90FF)
pitchfork.fullWidthZoneFill  = SCISolidBrushStyle(color: 0x4000AA00)

// Enable interaction
pitchfork.isEditable = true

// Add to chart surface
surface.annotations.add(pitchfork)
</div>

> **_NOTE:_** For interactive creation of SCIPitchforkAnnotation, use the corresponding annotation creation modifier [SCIPitchforkCreationModifier](trading-annotation---scipitchforkcreationmodifier.html) available in SciChart iOS.

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.
