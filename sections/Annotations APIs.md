SciChart features a rich **Annotations API**, that allows you to place different elements over a chart:

![Annotations are Easy Example](img/annotations/annotations-are-easy-example.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-annotations-example/)

There are many different **Annotations** provided by SciChart and each one deserves an article by itself! 
This article is concerned with simply giving **an overview of the annotations** and where you can find the examples in our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which demonstrate them.

The annotations which are available out the box in SciChart are listed below:

| **Annotation Type**                                          | **Description**                                                                                                        |
| ------------------------------------------------------------ | ---------------------------------------------------------------------------------------------------------------------- |
| [SCIBoxAnnotation](boxannotation.html)                       | Draws a **rectangle** at specific `X1, X2, Y1, Y2` coordinates.                                                        |
| [SCILineAnnotation](lineannotation.html)                     | Draws a **line** between `[X1, Y1]` and `[X2, Y2]` coordinates.                                                        |
| [SCILineArrowAnnotation](linearrowannotation.html)           | Draws an **arrow** from `[X1, Y1]` to `[X2, Y2]` coordinates.                                                          |
| [SCIHorizontalLineAnnotation](horizontallineannotation.html) | Draws a **horizontal line** between `[X1, Y1]` and `[X2, Y2]` coordinates.                                             |
| [SCIVerticalLineAnnotation](verticallineannotation.html)     | Draws a **vertical line** between `[X1, Y1]` and `[X2, Y2]` coordinates.                                               |
| [SCITextAnnotation](textannotation.html)                     | Allows to place a piece of **text** at specific `[X1, Y1]` coordinates on a chart.                                     |
| [SCIAxisLabelAnnotation](axislabelannotation.html)           | Allows to place a piece of **text** at specific `X1` or  `Y1` coordinate on a chart **Axis**.                          |
| [SCIAxisMarkerAnnotation](axismarkerannotation.html)         | Allows to place **markers** with custom text onto `X or Y axes`. By default, shows the axis **value at its location**. | 
| [SCICustomAnnotation](customannotation.html)                 | Allows to place any `UIView` at a specific `[X1, Y1]` coordinates on a chart.                                          |

> **_NOTE:_** To learn more about **Annotation API**, please read the [Common Annotations Features](#common-annotations-features) section. 
> To find out more about a **specific** Annotation Type, please refer to a corresponding article about this **Annotation type**.

## Adding an Annotation Onto a Chart
The `SCIChartSurface` stores all its annotations in the internal `SCIAnnotationCollection`. 
It exposes the `ISCIChartSurface.annotations` property to access it.

The following code can be used to add the `SCILineAnnotation` to the chart:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create and configure the Line Annotation
    SCILineAnnotation *lineAnnotation = [SCILineAnnotation new];
    lineAnnotation.x1 = @(1.0);
    lineAnnotation.y1 = @(4.0);
    lineAnnotation.x2 = @(2.0);
    lineAnnotation.y2 = @(6.0);
    lineAnnotation.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF555555 thickness:2];

    // Add the annotation to the SCIAnnotationsCollection of a surface
    [self.surface.annotations add:lineAnnotation];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create and configure the Line Annotation
    let lineAnnotation = SCILineAnnotation()
    lineAnnotation.set(x1: 1.0)
    lineAnnotation.set(y1: 4.0)
    lineAnnotation.set(x2: 2.0)
    lineAnnotation.set(y2: 6.0)
    lineAnnotation.stroke = SCISolidPenStyle(colorCode: 0xFF555555, thickness: 2)

    // Add the annotation to the SCIAnnotationsCollection of a surface
    surface.annotations.add(lineAnnotation)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create and configure the Line Annotation
    var lineAnnotation = new SCILineAnnotation
    {
        X1Value = 1.0,
        Y1Value = 4.0,
        X2Value = 2.0,
        Y2Value = 6.0,
        Stroke = new SCISolidPenStyle(0xFF555555, 2f),
    };

    // Add the annotation to the SCIAnnotationsCollection of a surface
    Surface.Annotations.Add(lineAnnotation);
</div>

## Common Annotations Features
All the **Annotation** classes provided by SciChart conforms to the `ISCIAnnotation` protocol and derive from the `SCIAnnotationBase` class. 
These provide an API which allows to put them onto a chart and interact with them.

> **_NOTE:_** Please refer to the beginning of this article for the complete list of all the **Annotation types** available out of the box in SciChart.

Please see the list of common features below:

| **Feature**                                                                        | **Description**                                                                                                                  |
| ---------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------- |
| `ISCIAnnotation.x1`, `ISCIAnnotation.y1`, `ISCIAnnotation.x2`, `ISCIAnnotation.y2` | [X1, X2, Y1, Y2] coordinates specifies the position of an annotation on a `SCIChartSurface`. These are chart coordinates, not device (screen or pixel) coordinates. To find out about coordinate transformation in SciChart, please refer to the [Convert Pixels to Data Coordinates article](axis-apis---convert-pixel-to-data-coordinates.html). |
| `ISCIAnnotation.xAxisId`, `ISCIAnnotation.yAxisId`                                 | Specifies the ID of **X-Axis** and **Y-Axis** that this annotation is measured against.                                          |
| `ISCIAnnotation.isSelected`                                                        | Defines whether the annotation is **Selected**. When Selected - round **resizing markers** appears at the ends or corners of the annotation which can be used to resize it. Resizing markers can be accessed via the `SCIAnnotationBase.resizingGrip` property |
| `ISCIAnnotation.isEditable`                                                        | Specifies whether an annotation is **available for interaction**. When Editable, the annotation can be **Selected**, **Moved** and **Resized** by a user with a touch interaction. |
| `ISCIAnnotation.isHidden`                                                          | **Hides** or **shows** an annotation.                                                                                            |
| `ISCIAnnotation.dragDirections`                                                    | Allows to **constrain drag directions** for an annotation. Accepts a member of the `SCIDirection2D` enumeration.                 |
| `ISCIAnnotation.resizeDirections`                                                  | Allows to **constrain resize directions** for an annotation. Accepts a member of the `SCIDirection2D` enumeration.               |
| `SCIAnnotationBase.coordinateMode`                                                 | Determines whether [X1, X2, Y1, Y2] coordinates are **in chart** coordinates or in **relative** screen coordinates. **Relative** coordinates range from **[0 to 1]**, where 1 refers to the full Width or Height of the canvas. **Absolute** coordinates are the **data-values** which must correspond to the [Axis Type](Axis APIs.html). Defined by the `SCIAnnotationCoordinateMode` enumeration. |
| `SCIAnnotationBase.resizingGrip`                                                   | Determines what **resizing markers** look like. Custom ones have to implement `ISCIResizingGrip`.                                |
| `ISCIAnnotation.annotationSurface`                                                 | Specifies a **surface** to place an annotation on. Possible options are declared by the `SCIAnnotationSurfaceEnum` enumeration.  |

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids.

Below is the list of some methods and listeners:

| **Feature**                                                                        | **Description**                                                                                                                  |
| ---------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------- |
| `-[ISCIAnnotation updateWithXAxis:yAxis:]`                                         | **Redraws** an annotation without invalidating the `parentSurface` of the `ISCIAnnotation` instance.                             |
| `-[ISCIAnnotation moveAnnotationByXDelta:yDelta:]`                                 | **Moves** an annotation by the specified X and Y **delta** in horizontal and vertical directions.                                |
|                                                                                    |                                                                                                                                  |
| `ISCIAnnotation.annotationDragListener`                                            | Allows to appoint `SCIAnnotationDragListener` to receive notifications about **user interactions** with an annotation.           |
| `ISCIAnnotation.annotationSelectionChangedListener`                                | Allows to appoint `SCIAnnotationSelectionChangedListener` to receive notifications when the annotation **selection has changed** |
| `ISCIAnnotation.annotationIsHiddenChangedListener`                                 | Allows to appoint `SCIAnnotationIsHiddenChangedListener` to receive notifications when an annotation **gets hidden or visible**. |

> To find out more about a **specific** Annotation Type, please refer to a corresponding article about this **Annotation type**.

## Annotation Alignment (Anchor Points)
There is the subset of classes among all the **annotation types** that allows their instances to be aligned **relative to the [X1, Y1]** **_control point_**. 
The base class responsible for this behavior is called `SCIAnchorPointAnnotation`. 
It has two properties, which accepts the `SCIHorizontalAnchorPoint` and `SCIVerticalAnchorPoint` enumerations correspondingly:
- `SCIAnchorPointAnnotation.horizontalAnchorPoint`
- `SCIAnchorPointAnnotation.verticalAnchorPoint`

The image below illustrates how those works:

![Annotations Anchor Points](img/annotations/annotations-anchor-points.png)

The annotation types that derive from AnchorPointAnnotation are the following:
- [SCITextAnnotation](textannotation.html)
- [SCICustomAnnotation](customannotation.html)
- [SCIAxisMarkerAnnotation](axismarkerannotation.html)
- [SCIAxisLabelAnnotation](axismarkerannotation.html)

As the information above implies, annotations of these types are controlled by the **[X1, Y1]** coordinates **only**. 
The **[X2, Y2]** coordinates have no impact on placement and are **simply ignored**.

## Editing Annotations via User Drag
In SciChart - **All Annotation Types** are supporting dragging and repositioning via touch. 
As mentioned [above](#common-annotations-features) - that's can be controlled via the `ISCIAnnotation.isEditable`. 

<video autoplay loop muted playsinline src="img/annotations/interaction-with-annotations-example.mp4"></video>

This is demonstrated in the **Interaction with Annotations** example which can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
- [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-chart-interaction-with-annotations-example/)
- [Xamarin Example](https://www.scichart.com/example/xamarin-chart-interaction-with-annotations-example/)