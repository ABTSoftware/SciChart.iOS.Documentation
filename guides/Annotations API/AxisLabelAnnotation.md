# The SCIAxisLabelAnnotation
The `SCIAxisLabelAnnotation` allows to place a piece of **text** at a specific location on an [Axis](Axis APIs.html):

![Axis Label Annotation](img/annotations/axis-label-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-interaction-with-annotations-example/)

Since `SCIAxisLabelAnnotation` is nearly the same as [SCITextAnnotation](TextAnnotation.html), hence everything about configuring it is the same. Please see the [TextAnnotation](TextAnnotation.html) article to learn more.

The only difference is that `SCIAxisLabelAnnotation` can be placed on the **X-Axis** or the **Y-Axis** instead of the **Chart Surface**.
That's is specified via the `ISCIAnnotation.annotationSurface` property.
It accepts a member of the `SCIAnnotationSurfaceEnum` enumeration and it **defaults** to **`XAxis`** for the **AxisMarkerAnnotation**.

Position of the `SCIAxisMarkerAnnotation` is defined by the `X1` or `Y1` coordinate, depending on the axis. 
Those values can be accessed via the `ISCIAnnotation.x1` and `ISCIAnnotation.y1` properties.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a SCIAxisLabelAnnotation
A `SCIAxisLabelAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create SCICustomAnnotation instance
    SCIAxisLabelAnnotation *axisLabelAnnotation = [SCIAxisLabelAnnotation new];

    // Set the text
    axisLabelAnnotation.text = @"Axis Label can be Rotated";

    // Specify a SCIFontStyle for the text
    axisLabelAnnotation.fontStyle = [[SCIFontStyle alloc] initWithFontSize:20 andTextColorCode:0xFFFF1919];

    // Specify rotation Angle in Degrees if needed
    axisLabelAnnotation.rotationAngle = -30;

    // Specify a desired position by setting the X1 coordinate, since the marker is going to be located on an X axis
    axisLabelAnnotation.x1 = @(60);
    
    // Specify the desired Axis to place annotation on via SCIAnnotationSurface
    axisLabelAnnotation.annotationSurface = SCIAnnotationSurface_XAxis;

    // Allow to interact with the annotation in run-time
    axisLabelAnnotation.isEditable = YES;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    axisLabelAnnotation.xAxisId = BottomAxisId;
    axisLabelAnnotation.yAxisId = LeftAxisId;

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:axisLabelAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create SCICustomAnnotation instance
    let axisLabelAnnotation = SCIAxisLabelAnnotation()
    
    // Set the text
    axisLabelAnnotation.text = "Axis Label can be Rotated"

    // Specify a SCIFontStyle for the text
    axisLabelAnnotation.fontStyle = = SCIFontStyle(fontSize: 20, andTextColorCode: 0xFFFF1919)

    // Specify rotation Angle in Degrees if needed
    axisLabelAnnotation.rotationAngle = -30

    // Specify a desired position by setting the X1 coordinate, since the marker is going to be located on an X axis
    axisLabelAnnotation.set(x1: 60)

    // Specify the desired Axis to place annotation on via SCIAnnotationSurface
    axisLabelAnnotation.annotationSurface = .xAxis

    // Allow to interact with the annotation in run-time
    axisLabelAnnotation.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    axisLabelAnnotation.xAxisId = BottomAxisId
    axisLabelAnnotation.yAxisId = LeftAxisId
    
    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(axisLabelAnnotation)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create SCICustomAnnotation instance
    var axisLabelAnnotation = new SCIAxisLabelAnnotation();

    // Set the text
    axisLabelAnnotation.Text = "Axis Label can be Rotated";

    // Specify a SCIFontStyle for the text
    axisLabelAnnotation.FontStyle = new SCIFontStyle(20, 0xFFFF1919);

    // Specify rotation Angle in Degrees if needed
    axisLabelAnnotation.RotationAngle = -30;

    // Specify a desired position by setting the X1 coordinate, since the marker is going to be located on an X axis
    axisLabelAnnotation.Y1Value = 60;

    // Specify the desired Axis to place annotation on via SCIAnnotationSurface
    axisLabelAnnotation.AnnotationSurface = SCIAnnotationSurface.XAxis;

    // Allow to interact with the annotation in run-time
    axisLabelAnnotation.IsEditable = true;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    axisLabelAnnotation.XAxisId = BottomAxisId;
    axisLabelAnnotation.YAxisId = LeftAxisId;

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(axisLabelAnnotation);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.

## Edit SCIAxisLabelAnnotation in Run-Time
Similarly to the [TextAnnotation](TextAnnotation.html) - `SCIAxisLabelAnnotation` can be edited in run-time. To turn that on - just set `SCITextAnnotationBase.canEditText` to `YES` on your annotation.

<video autoplay loop muted playsinline src="img/annotations/axis-label-annotation-editing.mp4"></video>

> **_NOTE:_** Be aware, if you use **canEditText** (allows edit text in run-time) in conjunction with **isEditable** (allows drag annotation over that chart), you will need to perform **2 taps** - first one to select annotation, and only after that - tap to enter editing.