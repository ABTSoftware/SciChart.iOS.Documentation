# The SCIBoxAnnotation
The `SCIBoxAnnotation` draws a rectangle at specific `X1, X2, Y1, Y2` coordinates:

![Box Annotation](img/annotations/box-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-interaction-with-annotations-example/)

The `SCIBoxAnnotation` class provides the `SCIBoxAnnotation.borderPen` and `SCIBoxAnnotation.fillBrush` properties, which are used for the annotation outline and background and expects the `SCIPenStyle` and `SCIBrushStyle` correspondingly. 
To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

A `SCIBoxAnnotation` is placed on a chart at the position determined by its `[X1, Y1]` and `[X2, Y2]` coordinates, which correspond to the **top-left** and **bottom-right** corners of the drawn rectangle. 
Those can be accessed via the following properties:
- `ISCIAnnotation.x1`
- `ISCIAnnotation.y1`
- `ISCIAnnotation.x2`
- `ISCIAnnotation.y2`

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a BoxAnnotation
A `SCIBoxAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a BoxAnnotation
    SCIBoxAnnotation *boxAnnotation = [SCIBoxAnnotation new];

    // Allow to interact with the annotation in run-time
    boxAnnotation.isEditable = YES;

    // in a multi-axis scenario, specify the XAxisId and YAxisId
    boxAnnotation.xAxisId = TopAxisId;
    boxAnnotation.yAxisId = LeftAxisId;

    // Specify a desired position by setting coordinates
    boxAnnotation.x1 = @(20);
    boxAnnotation.y1 = @(10);
    boxAnnotation.x2 = @(90);
    boxAnnotation.y2 = @(4);
    
    // Specify the border color for the annotation
    boxAnnotation.borderPen = [[SCISolidPenStyle alloc] initWithColorCode:0x99FF1919 thickness:2];
    // Specify the fill color for the annotation
    boxAnnotation.fillBrush = [[SCISolidBrushStyle alloc] initWithColorCode:0x44FF1919];

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:boxAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a BoxAnnotation
    let boxAnnotation = SCIBoxAnnotation()

    // Allow to interact with the annotation in run-time
    boxAnnotation.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    boxAnnotation.xAxisId = TopAxisId
    boxAnnotation.yAxisId = LeftAxisId

    // Specify a desired position by setting coordinates
    boxAnnotation.set(x1: 20)
    boxAnnotation.set(y1: 10)
    boxAnnotation.set(x2: 90)
    boxAnnotation.set(y2: 4)
    
    // Specify the border color for the annotation
    boxAnnotation.borderPen = SCISolidPenStyle(colorCode: 0x99FF1919, thickness: 2)
    // Specify the fill color for the annotation
    boxAnnotation.fillBrush = SCISolidBrushStyle(colorCode: 0x44FF1919)

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(boxAnnotation)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a BoxAnnotation
    var boxAnnotation = new SCIBoxAnnotation
    {
        // Allow to interact with the annotation in run-time
        IsEditable = true,

        // In a multi-axis scenario, specify the XAxisId and YAxisId
        XAxisId = TopAxisId,
        YAxisId = LeftAxisId,

        // Specify a desired position by setting coordinates
        X1Value = 20,
        Y1Value = 10,
        X2Value = 90,
        Y2Value = 4,


        // Specify the border color for the annotation
        BorderPen = new SCISolidPenStyle(0x99FF1919, 2),
        // Specify the fill color for the annotation
        FillBrush = new SCISolidBrushStyle(0x44FF1919)
    };

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(boxAnnotation);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.
