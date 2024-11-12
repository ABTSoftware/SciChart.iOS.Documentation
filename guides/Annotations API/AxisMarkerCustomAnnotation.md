# The SCIAxisMarkerCustomAnnotation
The `SCIAxisMarkerCustomAnnotation` type allows to place any [UIView](https://developer.apple.com/documentation/uikit/uiview) onto **X or Y axes**. 

![Axis Marker Custom Annotation](img/annotations/axis-marker-custom-annotation-formatting.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-chart-chart-interaction-with-annotations-example/)

The `SCIAxisMarkerCustomAnnotation` provides the `SCIAxisMarkerCustomAnnotation.customView` property, which expects iOS [UIView](https://developer.apple.com/documentation/uikit/uiview) which later will be placed onto a **X or Y axes**.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

The `SCIAxisMarkerCustomAnnotation` can be placed on the **X-Axis** or the **Y-Axis** which is specified via the `ISCIAnnotation.annotationSurface` property.
It accepts a member of the `SCIAnnotationSurfaceEnum` enumeration and it **defaults** to **`YAxis`** for the **AxisMarkerCustomAnnotation**.

Position of the `SCIAxisMarkerCustomAnnotation` is defined by the `X1` or `Y1` coordinate, depending on the axis. 
Those values can be accessed via the `ISCIAnnotation.x1` and `ISCIAnnotation.y1` properties.

Also, **AxisMarkerAnnotation** can be aligned relative to the `X1` or `Y1` coordinate by setting Anchor Points. 
For more information about the **[Anchor Points](Annotations APIs.html#annotation-alignment-anchor-points)** - refer to the corresponding section [Annotations APIs](Annotations APIs.html) article.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create an AxisMarkerCustomAnnotation
A simple `SCIAxisMarkerCustomAnnotation` with an [UIImageView](https://developer.apple.com/documentation/uikit/uiimageview) can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Prepare UIImageView for the AxisMarkerCustomAnnotation
    UIImageView *imageView = [[UIImageView alloc] initWithFrame:CGRectMake(0, 0, 20, 20)];
    imageView.image = [UIImage imageNamed:@"image.arrow.right"];
    imageView.contentMode = UIViewContentModeScaleAspectFit;

    // Create an AxisMarkerAnnotation
    SCIAxisMarkerCustomAnnotation *rightMarker = [SCIAxisMarkerCustomAnnotation new];

    // Supply it with UIImageView
    rightMarker.customView = imageView;
    
    // Specify a desired position by setting the Y1 coordinate, since the marker is going to be located on an Y axis
    rightMarker.y1 = @(9);

    // Allow to interact with the annotation in run-time
    rightMarker.isEditable = YES;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    rightMarker.xAxisId = BottomAxisId;
    rightMarker.yAxisId = RightAxisId;

    // Specify a desirable axis to place the annotation
    rightMarker.annotationSurface = SCIAnnotationSurface_YAxis;
    
    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:rightMarker];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Prepare UIImageView for the AxisMarkerCustomAnnotation
    let imageView = UIImageView.init(frame: CGRect(origin: CGPoint(x: 0, y: 0), size: CGSize.init(width: 20, height: 20)))
    imageView.image = UIImage(named: "image.arrow.right")
    imageView.contentMode = .scaleAspectFit

    // Create an AxisMarkerAnnotation
    let rightMarker = SCIAxisMarkerCustomAnnotation()

    // Supply it with UIImageView
    rightMarker.customView = imageView
    
    // Specify a desired position by setting the Y1 coordinate, since the marker is going to be located on an Y axis
    rightMarker.set(y1: 9)

    // Allow to interact with the annotation in run-time
    rightMarker.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    rightMarker.xAxisId = BottomAxisId
    rightMarker.yAxisId = RightAxisId

    // Specify a desirable axis to place the annotation
    rightMarker.annotationSurface = .yAxis

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(rightMarker)
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.