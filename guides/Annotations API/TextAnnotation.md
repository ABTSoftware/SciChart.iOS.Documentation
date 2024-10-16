# The SCITextAnnotation
The `SCITextAnnotation` allows to place a piece of **text** at a specific location on a chart:

![Text Annotation](img/annotations/text-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart/xamarin-chart-interaction-with-annotations-example/)

The `SCITextAnnotation` can be configured using the properties listed in the table below:

| **Property**                           | **Description**                                                                                      |
| -------------------------------------- | ---------------------------------------------------------------------------------------------------- |
| `SCITextAnnotationBase.text`           | Specifies the **text** for an annotation.                                                            |
| `SCITextAnnotationBase.attributedText` | Specifies the **attributed text** for your label. In this case, the `text` property will be ignored. |
| `SCITextAnnotationBase.fontStyle`      | Determines the **appearance** of the text via the `SCIFontStyle` object. Please refer to the [Styling and Theming](scipenstyle-scibrushstyle-and-scifontstyle.html) article to learn more. |
| `SCITextAnnotationBase.alignment`      | Specifies a text alignment with `SCIAlignment`.                                                      |
| `SCITextAnnotationBase.padding`        | Defines the **padding** around the text                                                              |
| `SCITextAnnotationBase.canEditText`    | When set to `YES` - allows to **modify the text in run-time** after selecting an annotation. See the [Edit SCITextAnnotation in Run-Time](#edit-scitextannotation-in-run-time) section. |
| `SCITextAnnotationBase.rotationAngle`  | **Rotates** an annotation through the specified angle in **degrees**.                                 |
| `SCITextAnnotationBase.backgroundColor`  |Sets the **background color** for the annotation text.                                        |

Position of the `SCITextAnnotation` is defined by the `X1` or `Y1` coordinate, depending on the axis. 
Those values can be accessed via the `ISCIAnnotation.x1` and `ISCIAnnotation.y1` properties.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

Also, because **SCITextAnnotation** is derived from the `SCIAnchorPointAnnotation` it can be aligned relative to the `X1` or `Y1` coordinate by setting the Anchor Points. For more information about the **[Anchor Points](Annotations APIs.html#annotation-alignment-anchor-points)** - refer to the corresponding section [Annotations APIs](Annotations APIs.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

## Create a SCITextAnnotation
A `SCITextAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create SCICustomAnnotation instance
    SCITextAnnotation *textAnnotation = [SCITextAnnotation new];

    // Set the text
    textAnnotation.text = @"Text can be Rotated";
 
    // Specify desired padding
    textAnnotation.padding = SCIEdgeInsetsMake(20, 20, 20, 20);

    // Specify a desired background color
    textAnnotation.backgroundColor = [SCIColor whiteColor];

    // Specify a SCIFontStyle for the text
    textAnnotation.fontStyle = [[SCIFontStyle alloc] initWithFontSize:20 andTextColorCode:0xBBFC9C29];

    // Specify rotation Angle in Degrees if needed
    textAnnotation.rotationAngle = -30;

    // Specify a desired position
    textAnnotation.x1 = @(20);
    textAnnotation.y1 = @(14);

    // Allow to interact with the annotation in run-time
    textAnnotation.isEditable = YES;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    textAnnotation.xAxisId = BottomAxisId;
    textAnnotation.yAxisId = LeftAxisId;

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:textAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create SCICustomAnnotation instance
    let textAnnotation = SCITextAnnotation()
    
    // Set the text
    textAnnotation.text = "Text can be Rotated"

    // Specify desired padding
    textAnnotation.padding = SCIEdgeInsets(top: 20, left: 20, bottom: 20, right: 20)

    // Specify a desired background color
    textAnnotation1.backgroundColor = SCIColor.white

    // Specify a SCIFontStyle for the text
    textAnnotation.fontStyle = = SCIFontStyle(fontSize: 20, andTextColorCode: 0xBBFC9C29)

    // Specify rotation Angle in Degrees if needed
    textAnnotation.rotationAngle = -30

    // Specify a desired position
    textAnnotation.set(x1: 20)
    textAnnotation.set(y1: 14)

    // Allow to interact with the annotation in run-time
    textAnnotation.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    textAnnotation.xAxisId = BottomAxisId
    textAnnotation.yAxisId = LeftAxisId
    
    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(textAnnotation)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create SCICustomAnnotation instance
    var textAnnotation = new SCITextAnnotation();

    // Set the text
    textAnnotation.Text = "Text can be Rotated";

    // Specify a SCIFontStyle for the text
    textAnnotation.FontStyle = new SCIFontStyle(20, 0xBBFC9C29);

    // Specify rotation Angle in Degrees if needed
    textAnnotation.RotationAngle = -30;

    // Specify a desired position
    textAnnotation.X1Value = 20;
    textAnnotation.Y1Value = 14;

    // Allow to interact with the annotation in run-time
    textAnnotation.IsEditable = true;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    textAnnotation.XAxisId = BottomAxisId;
    textAnnotation.YAxisId = LeftAxisId;

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(textAnnotation);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.

## Edit SCITextAnnotation in Run-Time
The `SCITextAnnotation` can be edited in run-time. To turn that on - just set `SCITextAnnotationBase.canEditText` to `YES` on your annotation.

Please see the code snippet and result below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCITextAnnotation *editTextAnnotation = [SCITextAnnotation new];
    editTextAnnotation.isEditable = YES;
    editTextAnnotation.canEditText = YES;
    editTextAnnotation.xAxisId = BottomAxisId;
    editTextAnnotation.yAxisId = LeftAxisId;
    editTextAnnotation.x1 = @(80);
    editTextAnnotation.y1 = @(14);
    editTextAnnotation.text = @"and edited ...";
    editTextAnnotation.fontStyle = [[SCIFontStyle alloc] initWithFontSize:20 andTextColor:UIColor.yellowColor];
</div>
<div class="code-snippet" id="swift">
    let editTextAnnotation = SCITextAnnotation()
    editTextAnnotation.isEditable = true
    editTextAnnotation.canEditText = true
    editTextAnnotation.xAxisId = BottomAxisId
    editTextAnnotation.yAxisId = LeftAxisId
    editTextAnnotation.set(x1: 80)
    editTextAnnotation.set(y1: 14)
    editTextAnnotation.text = "and edited ..."
    editTextAnnotation.fontStyle = SCIFontStyle(fontSize: 20, andTextColor: .yellow)
</div>
<div class="code-snippet" id="cs">
    var editTextAnnotation = new SCITextAnnotation();
    editTextAnnotation.IsEditable = true;
    editTextAnnotation.CanEditText = true;
    editTextAnnotation.XAxisId = BottomAxisId;
    editTextAnnotation.YAxisId = LeftAxisId;
    editTextAnnotation.X1Value = 80;
    editTextAnnotation.Y1Value = 14;
    editTextAnnotation.Text = "and edited ...";
    editTextAnnotation.FontStyle = new SCIFontStyle(20, UIColor.Yellow);
</div>

<video autoplay loop muted playsinline src="img/annotations/text-annotation-editing.mp4"></video>

> **_NOTE:_** Be aware, if you use **canEditText** (allows edit text in run-time) in conjunction with **isEditable** (allows drag annotation over that chart), you will need to perform **2 taps** - first one to select annotation, and only after that - tap to enter editing.