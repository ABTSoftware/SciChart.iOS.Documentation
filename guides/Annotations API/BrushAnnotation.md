# The SCIBrushAnnotation
he `SCIBrushAnnotation` allows for freehand drawing directly on the chart surface by capturing a sequence of points that form a continuous brush stroke.

![Brush Annotation](img/annotations/brush-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Brush Annotation - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-brush-annotations-example/) ̰

A `SCIBrushAnnotation` is a multi-point annotation that stores a collection of points to represent a freehand path drawn by the user.

The SCIBrushAnnotation can be configured using the properties and method listed in the table below:

| **Field**                               | **Description**                                                                                     |
| --------------------------------------- | --------------------------------------------------------------------------------------------------- |
| `SCIBrushAnnotation.stroke`           |  Defines the stroke style (color and thickness) of the brush.                                |
| `SCIBrushAnnotation.drawId`         |  A unique identifier for the annotation instance.  |
| `SCIBrushAnnotation.selectionOffset` |  Extra padding applied around the selection bounds.                                           |  

To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

## Adding Points
Points for a `SCIBrushAnnotation` are defined sequentially using the `[appendPointWithX:y:]` method, which appends each new coordinate to the existing path to form a continuous freehand stroke. 
The `clearPoints` method can be used to remove all previously added points, resetting the annotation so that it no longer renders until new points are appended.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a BrushAnnotation
A `SCIBrushAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Assume a surface has been created and configured somewhere
id<ISCIChartSurface> surface;

SCIBrushAnnotation *brush = [SCIBrushAnnotation new];

// Append points for the path
[brush appendPointWithX:@(224) y:@(11000)];
[brush appendPointWithX:@(250) y:@(12000)];
[brush appendPointWithX:@(220) y:@(11400)];
[brush appendPointWithX:@(224) y:@(11000)];

// Customize appearance
brush.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFF0000 thickness:3];

// Enable interaction
brush.isEditable = YES;

// Add to chart surface
[surface.annotations add: brush];
</div>
<div class="code-snippet" id="swift">
// Assume a surface has been created and configured somewhere
let surface: ISCIChartSurface

let brush = SCIBrushAnnotation()

// Append points for the path
brush.appendPointWith(x: NSNumber(value: 224), y: NSNumber(value: 11000))
brush.appendPointWith(x: NSNumber(value: 250), y: NSNumber(value: 12000))
brush.appendPointWith(x: NSNumber(value: 220), y: NSNumber(value: 11400))
brush.appendPointWith(x: NSNumber(value: 224), y: NSNumber(value: 11000))

// Customize appearance
brush.stroke = SCISolidPenStyle(color: SCIColor.red, thickness: 3)

// Enable interaction
brush.isEditable = true

// Add to chart surface
surface.annotations.add(brush)
</div>

> **_NOTE:_** For interactive creation of SCIBrushAnnotation, use the corresponding annotation creation modifier [SCIBrushAnnotationCreationModifier](trading-annotation---scibrushannotationcreationmodifier.html) available in SciChart iOS.

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.
