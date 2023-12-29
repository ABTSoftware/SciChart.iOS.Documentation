# The SCICustomAnnotation
The `SCICustomAnnotation` allows to place any [UIView](https://developer.apple.com/documentation/uikit/uiview) at a specific location on a chart:

![Custom Annotation](img/annotations/custom-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart/xamarin-chart-interaction-with-annotations-example/)

The `SCICustomAnnotation` provides the `SCICustomAnnotation.customView` property, which expects iOS [UIView](https://developer.apple.com/documentation/uikit/uiview) which later will be placed onto a `SCIChartSurface`.

Position of the `SCICustomAnnotation` is defined by the `X1` or `Y1` coordinate, depending on the axis. 
Those values can be accessed via the `ISCIAnnotation.x1` and `ISCIAnnotation.y1` properties.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

Also, because **SCICustomAnnotation** is derived from the `SCIAnchorPointAnnotation` it can be aligned relative to the `X1` or `Y1` coordinate by setting the Anchor Points. For more information about the **[Anchor Points](Annotations APIs.html#annotation-alignment-anchor-points)** - refer to the corresponding section [Annotations APIs](Annotations APIs.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

## Create a SCICustomAnnotation
A simple `SCICustomAnnotation` with an [UIImageView](https://developer.apple.com/documentation/uikit/uiimageview) can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Prepare UIImageView for the CustomAnnotation
    UIImageView *imageView = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"chart.annotation"]];
    imageView.frame = CGRectMake(0, 0, 50, 50);
    imageView.tintColor = UIColor.whiteColor;

    // Create SCICustomAnnotation instance
    SCICustomAnnotation *customAnnotation = [SCICustomAnnotation new];

    // Supply it with UIImageView
    customAnnotation.customView = imageView;

    // Specify a desired position
    customAnnotation.x1 = @(10);
    customAnnotation.y1 = @(10);

    // Allow to interact with the annotation in run-time
    customAnnotation.isEditable = YES;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    customAnnotation.xAxisId = BottomAxisId;
    customAnnotation.yAxisId = LeftAxisId;

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:customAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Prepare UIImageView for the CustomAnnotation
    let imageView = UIImageView(image:#imageLiteral(resourceName: "chart.annotation.png"))
    imageView.frame = CGRect(origin: .zero, size: CGSize(width: 50, height: 50))
    imageView.tintColor = .white

    // Create SCICustomAnnotation instance
    let customAnnotation = SCICustomAnnotation()
    
    // Supply it with UIImageView
    customAnnotation.customView = imageView

    // Specify a desired position
    customAnnotation.set(x1: 10)
    customAnnotation.set(y1: 10)

    // Allow to interact with the annotation in run-time
    customAnnotation.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    customAnnotation.xAxisId = BottomAxisId
    customAnnotation.yAxisId = LeftAxisId
    
    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(customAnnotation)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Prepare UIImageView for the CustomAnnotation
    var imageView = new UIImageView(new UIImage("chart.annotation.png"));
    imageView.Frame = new CGRect(CGPoint.Empty, new CGSize(50, 50));
    imageView.TintColor = UIColor.White;

    // Create SCICustomAnnotation instance
    var customAnnotation = new SCICustomAnnotation();

    // Supply it with UIImageView
    customAnnotation.CustomView = imageView;

    // Specify a desired position
    customAnnotation.X1Value = 10;
    customAnnotation.Y1Value = 10;

    // Allow to interact with the annotation in run-time
    customAnnotation.IsEditable = true;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    customAnnotation.XAxisId = BottomAxisId;
    customAnnotation.YAxisId = LeftAxisId;

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(customAnnotation);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.