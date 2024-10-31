# The SCIImageAnnotation
The `SCIImageAnnotation` type allows to place **image** at a specific location on a chart.

![Image Annotation](img/annotations/image-annotation-demo.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/)

A `SCIImageAnnotation` is placed on a chart at the position determined by its `[X1, Y1]` and `[X2, Y2]` coordinates, which correspond to the **top-left** and **bottom-right** corners of the drawn rectangle. 
Those can be accessed via the following properties:
- `ISCIAnnotation.x1`
- `ISCIAnnotation.y1`
- `ISCIAnnotation.x2`
- `ISCIAnnotation.y2`

A `SCIImageAnnotation` can also be placed on a chart at the position determined by its `[X1, Y1]` coordinates, which correspond to the **top-left** corners, and `ISCIAnnotation.desiredSize` which specifies the image dimensions.

A `SCIImageAnnotation` can be positioned as a background image on a chart by setting the `ISCIAnnotation.annotationType` property to `SCIAnnotationType_Background`.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

The `SCIImageAnnotation` can be configured using the properties listed in the table below:

| **Feature**                                           | **Description**                                                    | 
| ----------------------------------------------------- | ------------------------------------------------------------------ |
| `SCIImageAnnotation.image`              | Allows to specify the **image** that will appear **on the surface**. |
| `SCIImageAnnotation.annotationPosition` | Allows to specify `[X1, Y1]` coordinates correspond to the which corner of the image. It accepts a member of the `SCIAlignment` enumeration for vertical and horizontal position and it **defaults** to **top-left** for the **SCIImageAnnotation**.<br />**_NOTE:_** The annotation will not be affected by this property when a bounding box with four coordinates is defined.|
| `SCIImageAnnotation.contentMode`                   | Determines the **layout** i.e **content mode** of the image. It accepts a member of the `SCIContentModeEnum` enumeration and it **defaults** to **`SCIContentMode_ScaleToFill`** for the **SCIImageAnnotation**.|
| `SCIImageAnnotation.annotationType`             | Allows to specify how the image will be rendered on the chart, as **image annotation** OR as **background image**. It accepts a member of the `SCIAnnotationTypeEnum` enumeration and it **defaults** to **`SCIAnnotationType_Annotation`** for the **SCIImageAnnotation**.|
| `SCIImageAnnotation.alpha`                   | Allows to specify the opacity of the image.|
| `SCIImageAnnotation.desiredSize`             | Allows to specify the **reuired size** of the image.<br />**_NOTE:_** The annotation will not be affected by this property when a bounding box with four coordinates is defined.|

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

## Create an ImageAnnotation using `[X1, Y1]` and `[X2, Y2]` coordinates 
A simple `SCIImageAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create an ImageAnnotation
    SCIImageAnnotation *boxImageAnnotation = [SCIImageAnnotation new];
        
    // Specify the image resource
    SCIImage *image = [SCIImage imageNamed:@"testLandscape"];
    if (image) {
        boxImageAnnotation.image = image;
    }

    // Specify a desired position by setting X1, Y1, X2, Y2
    boxImageAnnotation.x1 = @(-0.8);
    boxImageAnnotation.y1 = @(0.8);
    boxImageAnnotation.x2 = @(2.5);
    boxImageAnnotation.y2 = @(-5.0);

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    boxImageAnnotation.xAxisId = BottomAxisId;
    boxImageAnnotation.yAxisId = LeftAxisId;

    // Specify the desired layer to place the annotation
    boxImageAnnotation.annotationSurface = SCIAnnotationSurface_BelowChart;

    // Specify image aspect ratio
    boxImageAnnotation.contentMode = SCIContentMode_AspectFit;
    
    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:boxImageAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create an ImageAnnotation
    let boxImageAnnotation = SCIImageAnnotation()
    
    // Specify the image resource
    if let image = SCIImage(named: "testLandscape") {
        boxImageAnnotation.image = image
    }

    // Specify a desired position by setting X1, Y1, X2, Y2
    boxImageAnnotation.set(x1: -0.8)
    boxImageAnnotation.set(y1: 0.8)
    boxImageAnnotation.set(x2: 2.5)
    boxImageAnnotation.set(y2: -5.0)

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    boxImageAnnotation.xAxisId = BottomAxisId
    boxImageAnnotation.yAxisId = RightAxisId

    // Specify the desired layer to place the annotation
    boxImageAnnotation.annotationSurface = .belowChart

    // Specify image aspect ratio
    boxImageAnnotation.contentMode = .aspectFit

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(boxImageAnnotation)
</div>

## Create an ImageAnnotation using `[X1, Y1]` and Desier size
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create an ImageAnnotation
    SCIImageAnnotation *pointImageAnnotation = [SCIImageAnnotation new];
        
    // Specify the image resource
    SCIImage *image = [SCIImage imageNamed:@"testLandscape"];
    if (pointImage) {
        pointImageAnnotation.image = pointImage;
    }

    // Specify the desired size for the annotation
    pointImageAnnotation.desiredSize = CGSizeMake(100, 100);

    // Specify a desired position by setting X1, Y1
    pointImageAnnotation.x1 = @(6.3);
    pointImageAnnotation.y1 = @(2.5);
    
    // Specify a desired placement
    pointImageAnnotation.annotationPosition = SCIAlignment_Left | SCIAlignment_Top;

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:pointImageAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create an ImageAnnotation
    let pointImageAnnotation = SCIImageAnnotation()
            
    // Specify the image resource
    if let image = SCIImage(named: "testLandscape") {
        pointImageAnnotation.image = image
    }
            
    // Specify the desired size for the annotation
    pointImageAnnotation.desiredSize = CGSize.init(width: 100, height: 100)

    // Specify a desired position by setting X1, Y1
    pointImageAnnotation.set(x1: 6.3)
    pointImageAnnotation.set(y1: 2.5)

    // Specify a desired placement
    pointImageAnnotation.annotationPosition = [.left, .top];

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(pointImageAnnotation)
</div>

## ImageAnnotation as background image
Setting the `SCIAnnotationType_Background` value in the `SCIImageAnnotation.annotationType` property designates the image as a **fixed background** for the chart. This means that the image will remain stationary and unaffected by any panning or zooming actions performed on the chart.

Let's see a short example which shows how to use the above:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
        
        backgroundImageAnnotation = [SCIImageAnnotation new];
        SCIImage *image = [SCIImage imageNamed:@"image.background.moving"];
        if (image) {
            backgroundImageAnnotation.image = image;
        }
        backgroundImageAnnotation.annotationType = SCIAnnotationType_Background;
        backgroundImageAnnotation.alpha = 0.4;
        backgroundImageAnnotation.annotationSurface = SCIAnnotationSurface_AboveGrid;
        backgroundImageAnnotation.contentMode = SCIContentMode_AspectFill;
</div>
<div class="code-snippet" id="swift">

        backgroundImageAnnotation = SCIImageAnnotation()
        if let image = SCIImage(named: "image.background.moving") {
            backgroundImageAnnotation.image = image
        }
        backgroundImageAnnotation.annotationType = .background
        backgroundImageAnnotation.alpha = 0.4
        backgroundImageAnnotation.annotationSurface = .aboveGrid
        backgroundImageAnnotation.contentMode = .aspectFill
</div>

which will result in the following:

![Background Image Annotation](img/annotations/background-Image-Annotation.png)

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.