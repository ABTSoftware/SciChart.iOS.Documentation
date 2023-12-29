# The SCILineAnnotation
The `SCILineAnnotation` draws a line connecting the `[X1, X2]` and `[Y1, Y2]` coordinates:

![Line Annotation](img/annotations/line-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart/xamarin-chart-interaction-with-annotations-example/)

The `SCILineAnnotation` class provides the `SCILineAnnotationBase.stroke` property which is used to define the line annotation color. It expects a `SCIPenStyle` object.
To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

A `SCILineAnnotation` is placed on a chart at the position determined by its `[X1, Y1]` and `[X2, Y2]` coordinates, which specifies the two line ends.
Those can be accessed via the following properties:
- `ISCIAnnotation.x1`
- `ISCIAnnotation.y1`
- `ISCIAnnotation.x2`
- `ISCIAnnotation.y2`

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have an axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a LineAnnotation
A `SCILineAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Line Annotation
    SCILineAnnotation *lineAnnotation = [SCILineAnnotation new];

    // Allow to interact with the annotation in run-time
    lineAnnotation.isEditable = YES;

    // in a multi-axis scenario, specify the XAxisId and YAxisId
    lineAnnotation.xAxisId = TopAxisId;
    lineAnnotation.yAxisId = LeftAxisId;

    // Specify a desired position by setting coordinates
    lineAnnotation.coordinateMode = SCIAnnotationCoordinateMode_RelativeY;
    lineAnnotation.x1 = @(20);
    lineAnnotation.y1 = @(0.2);
    lineAnnotation.x2 = @(60);
    lineAnnotation.y2 = @(0.8);
    
    // Specify the stroke color for the annotation
    lineAnnotation.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0x9942AD42 thickness:4];

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:lineAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Line Annotation
    let lineAnnotation = SCILineAnnotation()

    // Allow to interact with the annotation in run-time
    lineAnnotation.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    lineAnnotation.xAxisId = TopAxisId
    lineAnnotation.yAxisId = LeftAxisId

    // Specify a desired position by setting coordinates
    lineAnnotation.coordinateMode = .relativeY
    lineAnnotation.set(x1: 20)
    lineAnnotation.set(y1: 0.2)
    lineAnnotation.set(x2: 60)
    lineAnnotation.set(y2: 0.8)
    
    // Specify the stroke color for the annotation
    lineAnnotation.stroke = SCISolidPenStyle(colorCode: 0x9942AD42, thickness: 4)

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(lineAnnotation)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Line Annotation
    var lineAnnotation = new SCILineAnnotation
    {
        // Allow to interact with the annotation in run-time
        IsEditable = true,

        // In a multi-axis scenario, specify the XAxisId and YAxisId
        XAxisId = TopAxisId,
        YAxisId = LeftAxisId,

        // Specify a desired position by setting coordinates
        CoordinateMode = SCIAnnotationCoordinateMode.RelativeY,
        X1Value = 20,
        Y1Value = 0.2,
        X2Value = 60,
        Y2Value = 0.8,

        // Specify the stroke color for the annotation
        Stroke = new SCISolidPenStyle(0x9942AD42, 4),
    };

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(lineAnnotation);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.