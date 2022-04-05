# The SCILineArrowAnnotation
The `SCILineArrowAnnotation` draws an **arrow** from `[X1, X2]` to `[Y1, Y2]` coordinates:

![Line Arrow Annotation](img/annotations/line-arrow-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-interaction-with-annotations-example/)

The `SCILineArrowAnnotation` class provides the `SCILineAnnotationBase.stroke` property which is used to define the line annotation color. It expects a `SCIPenStyle` object.
To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

A `SCILineArrowAnnotation` is placed on a chart at the position determined by its `[X1, Y1]` and `[X2, Y2]` coordinates, which specifies the start and end of the **arrow**.
Those can be accessed via the following properties:
- `ISCIAnnotation.x1`
- `ISCIAnnotation.y1`
- `ISCIAnnotation.x2`
- `ISCIAnnotation.y2`

The arrow's head is placed at `[X2, Y2]` coordinates, its size is determined by the following properties:
- `SCILineArrowAnnotation.headLength`
- `SCILineArrowAnnotation.headWidth`

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have an axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a LineArrow Annotation
A `SCILineArrowAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a LineArrow Annotation
    SCILineArrowAnnotation *lineArrowAnnotation = [SCILineArrowAnnotation new];

    // Allow to interact with the annotation in run-time
    lineArrowAnnotation.isEditable = YES;

    // in a multi-axis scenario, specify the XAxisId and YAxisId
    lineArrowAnnotation.xAxisId = TopAxisId;
    lineArrowAnnotation.yAxisId = LeftAxisId;
        
    // Specify size for the arrow's head
    lineArrowAnnotation.headLength = 4;
    lineArrowAnnotation.headWidth = 8;

    // Specify a desired position by setting coordinates
    lineArrowAnnotation.coordinateMode = SCIAnnotationCoordinateMode_RelativeY;
    lineArrowAnnotation.x1 = @(40);
    lineArrowAnnotation.y1 = @(0.8);
    lineArrowAnnotation.x2 = @(100);
    lineArrowAnnotation.y2 = @(0.2);
    
    // Specify the stroke color for the annotation
    lineArrowAnnotation.stroke = [[SCISolidPenStyle alloc] initWithColor:UIColor.yellowColor thickness:2];

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:lineArrowAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a LineArrow Annotation
    let lineArrowAnnotation = SCILineArrowAnnotation()

    // Allow to interact with the annotation in run-time
    lineArrowAnnotation.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    lineArrowAnnotation.xAxisId = TopAxisId
    lineArrowAnnotation.yAxisId = LeftAxisId

    // Specify size for the arrow's head
    lineArrowAnnotation.headLength = 4
    lineArrowAnnotation.headWidth = 8

    // Specify a desired position by setting coordinates
    lineArrowAnnotation.coordinateMode = .relativeY
    lineArrowAnnotation.set(x1: 40)
    lineArrowAnnotation.set(y1: 0.8)
    lineArrowAnnotation.set(x2: 100)
    lineArrowAnnotation.set(y2: 0.2)
    
    // Specify the stroke color for the annotation
    lineArrowAnnotation.stroke = SCISolidPenStyle(color: .yellowColor, thickness: 2)

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(lineArrowAnnotation)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a LineArrow Annotation
    var lineArrowAnnotation = new SCILineArrowAnnotation
    {
        // Allow to interact with the annotation in run-time
        IsEditable = true,

        // In a multi-axis scenario, specify the XAxisId and YAxisId
        XAxisId = TopAxisId,
        YAxisId = LeftAxisId,

        // Specify size for the arrow's head
        HeadLength = 4,
        HeadWidth = 8,

        // Specify a desired position by setting coordinates
        CoordinateMode = SCIAnnotationCoordinateMode.RelativeY,
        X1Value = 20,
        Y1Value = 0.2,
        X2Value = 60,
        Y2Value = 0.8,

        // Specify the stroke color for the annotation
        Stroke = new SCISolidPenStyle(UIColor.Yellow, 2),
    };

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(lineArrowAnnotation);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.
