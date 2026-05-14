# The SCIFreehandDrawingModifier
The `SCIFreehandDrawingModifier` is a gesture modifier that enables freehand drawing directly on a SciChartSurface.

![Freehand Drawing Modifier](img/annotations/freehand-drawing-modifier.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Freehand Drawing Modifier - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-freehand-drawing-modifier-example/) ̰

## Overview
When this modifier is attached to a SciChartSurface, users can draw freehand strokes on the chart using touch gestures. As the user drags across the surface, a freehand drawing annotation is dynamically created and populated with a series of points representing the path of the gesture.

This is useful for scenarios such as:
- Marking regions of interest
- Freeform technical analysis
- Drawing custom overlays on charts

## API Reference

| **Field**                               | **Description**                                                                                     |
| --------------------------------------- | --------------------------------------------------------------------------------------------------- |
| `SCIFreehandDrawingModifier.stroke`           |  The pen style used to draw the annotation                               |
| `SCIFreehandDrawingModifier.selectionOffset`         |  Specifies extra padding around the selection bounds of the annotation..  |
| `SCIFreehandDrawingModifier.deleteSelectedAnnotations()` |  Removes all currently selected freehand drawing annotations from the chart surface.  |
 
To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Usage Example
A `SCIFreehandDrawingModifier` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
id<SCIChartSurface> surface = self.surface;

SCIFreehandDrawingModifier *freehandModifier = [SCIFreehandDrawingModifier new];
freehandModifier.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFF0000 thickness:2];

// Add to chart modifiers collection
[surface.chartModifiers add:freehandModifier];
</div>
<div class="code-snippet" id="swift">
// Assume a surface has been created and configured somewhere
let surface: ISCIChartSurface

let freehandModifier = SCIFreehandDrawingModifier()
freehandModifier.stroke = SCISolidPenStyle(color: SCIColor.red, thickness: 2.0)

// Add to chart modifiers collection
surface.chartModifiers.add(freehandModifier)
</div>

## Behavior
- Touch down begins a new freehand drawing annotation.
- Moving the finger continuously adds points to the annotation path.
- Releasing the touch finalizes the annotation.
- Annotations can be selected and removed using deleteSelectedAnnotations().

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.
