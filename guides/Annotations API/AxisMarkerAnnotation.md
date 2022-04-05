# The SCIAxisMarkerAnnotation
The `SCIAxisMarkerAnnotation` type allows to place **markers** with custom text onto **X or Y axes**. 
They show the **axis value** at their location **by default**:

![Axis Marker Annotation](img/annotations/axis-marker-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-interaction-with-annotations-example/)

The `SCIAxisMarkerAnnotation` can be configured using the properties listed in the table below:

| **Feature**                                           | **Description**                                                    | 
| ----------------------------------------------------- | ------------------------------------------------------------------ |
| `SCIAxisMarkerAnnotation.formattedValue`              | Allows to specify the **text** that will appear **on the marker**. |
| `SCIAxisMarkerAnnotation.formattedLabelValueProvider` | Allows to override the **default formatted value**, which comes from an axis via an `SCIAxisInfo` object. Please refer to the [AxisMarkerAnnotation TextFormatting](#axismarkerannotation-textformatting) section to learn more.|
| `SCIAxisMarkerAnnotation.fontStyle`                   | Determines the **appearance of the text** on the marker via the `SCIFontStyle` object. Please refer to the [Styling and Theming](scipenstyle-scibrushstyle-and-scifontstyle.html) article to learn more. |
| `SCIAxisMarkerAnnotation.markerPointSize`             | Allows to specify the **length of the pointed end** of the marker. |
| `SCIAxisMarkerAnnotation.borderPen`                   | Allows to specify the **outline color** of the marker.             |
| `SCIAxisMarkerAnnotation.backgroundBrush`             | Allows to specify the **background brush** of the marker.          |

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

The `SCIAxisMarkerAnnotation` can be placed on the **X-Axis** or the **Y-Axis** which is specified via the `ISCIAnnotation.annotationSurface` property.
It accepts a member of the `SCIAnnotationSurfaceEnum` enumeration and it **defaults** to **`XAxis`** for the **AxisMarkerAnnotation**.

Position of the `SCIAxisMarkerAnnotation` is defined by the `X1` or `Y1` coordinate, depending on the axis. 
Those values can be accessed via the `ISCIAnnotation.x1` and `ISCIAnnotation.y1` properties.

Also, **AxisMarkerAnnotation** can be aligned relative to the `X1` or `Y1` coordinate by setting Anchor Points. 
For more information about the **[Anchor Points](Annotations APIs.html#annotation-alignment-anchor-points)** - refer to the corresponding section [Annotations APIs](Annotations APIs.html) article.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create an AxisMarkerAnnotation
A simple `SCIAxisMarkerAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create an AxisMarkerAnnotation
    SCIAxisMarkerAnnotation *rightMarker = [SCIAxisMarkerAnnotation new];
    
    // Specify a desired position by setting the Y1 coordinate, since the marker is going to be located on an Y axis
    rightMarker.y1 = @(8);

    // Allow to interact with the annotation in run-time
    rightMarker.isEditable = YES;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    rightMarker.xAxisId = BottomAxisId;
    rightMarker.yAxisId = RightAxisId;
    
    // Specify the outline color for the marker
    rightMarker.borderPen = [[SCISolidPenStyle alloc] initWithColorCode:0xFF4083B7 thickness:1];

    // Specify the background color for the marker
    rightMarker.backgroundBrush = [[SCISolidBrushStyle alloc] initWithColorCode:0xAA4083B7];
    
    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:rightMarker];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create an AxisMarkerAnnotation
    let rightMarker = SCIAxisMarkerAnnotation()
    
    // Specify a desired position by setting the Y1 coordinate, since the marker is going to be located on an Y axis
    rightMarker.set(y1: 8)

    // Allow to interact with the annotation in run-time
    rightMarker.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    rightMarker.xAxisId = BottomAxisId
    rightMarker.yAxisId = RightAxisId
    
    // Specify the outline color for the marker
    rightMarker.borderPen = SCISolidPenStyle(colorCode: 0xFF4083B7, thickness: 1)

    // Specify the background color for the marker
    rightMarker.backgroundBrush = SCISolidBrushStyle(colorCode: 0xAA4083B7)

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(rightMarker)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create an AxisMarkerAnnotation
    var rightMarker = new SCIAxisMarkerAnnotation();

    // Specify a desired position by setting the Y1 coordinate, since the marker is going to be located on an Y axis
    rightMarker.Y1Value = 8;

    // Allow to interact with the annotation in run-time
    rightMarker.IsEditable = true;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    rightMarker.XAxisId = BottomAxisId;
    rightMarker.YAxisId = RightAxisId;

    // Specify the outline color for the marker
    rightMarker.BorderPen = new SCISolidPenStyle(0xFF4083B7, 1);

    // Specify the background color for the marker
    rightMarker.BackgroundBrush = new SCISolidBrushStyle(0xAA4083B7);

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(rightMarker);
</div>

#### AxisMarkerAnnotation TextFormatting
By default, the **axis marker** text is formatted by the `ISCIAxisCore.textFormatting` property. For more information, refer to the [Axis Labels - TextFormatting and CursorTextFormatting](axis-labels---textformatting-and-cursortextformatting.html) article.

But you can also override the default behaviour by using textformatting by providing custom the `ISCIFormattedValueProvider` for your `SCIAxisMarkerAnnotation` corresponding property.

Let's see a short example which shows how to use the above:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Declare custom ISCIFormattedValueProvider
    @interface AnnotationValueProvider: NSObject&lt;ISCIFormattedValueProvider&gt;
    @end
    @implementation AnnotationValueProvider

    - (id<ISCIString>)formatValueWithAxisInfo:(SCIAxisInfo *)axisInfo {
        return axisInfo != nil ? [NSString stringWithFormat:@"[ --- %@ --- ]", axisInfo.axisFormattedDataValue] : nil;
    }

    @end
    ...
    // Provide custom ISCIFormattedValueProvider for the annotation
    rightMarker.formattedLabelValueProvider = [AnnotationValueProvider new];
</div>
<div class="code-snippet" id="swift">
    // Declare custom ISCIFormattedValueProvider
    class AnnotationValueProvider: ISCIFormattedValueProvider {
        func formatValue(with axisInfo: SCIAxisInfo!) -> ISCIString! {
            return axisInfo != nil ? NSString(string: "[ --- \(axisInfo.axisFormattedDataValue!) --- ]") : nil;
        }
    }
    ...
    // Provide custom ISCIFormattedValueProvider for the annotation
    rightMarker.formattedLabelValueProvider = AnnotationValueProvider()
</div>
<div class="code-snippet" id="cs">
    // Declare custom ISCIFormattedValueProvider
    class AnnotationValueProvider : ISCIFormattedValueProvider
    {
        public override IISCIString FormatValueWithAxisInfo(SCIAxisInfo axisInfo)
        {
            return axisInfo != null ? $"[ --- {axisInfo.AxisFormattedDataValue} --- ]".ToSciString() : null;
        }
    }
    ...
    // Provide custom ISCIFormattedValueProvider for the annotation
    rightMarker.FormattedLabelValueProvider = new AnnotationValueProvider();
</div>

which will result in the following:

![Axis Marker Annotation Formatting](img/annotations/axis-marker-annotation-formatting.png)

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.