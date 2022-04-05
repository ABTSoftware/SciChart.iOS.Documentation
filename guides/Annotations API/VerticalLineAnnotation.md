# The SCIVerticalLineAnnotation
The `SCIVerticalLineAnnotation` draws the **vertical line** between `Y1` and `Y2` coordinates at `X1`:

> **_NOTE:_** You might also be interested in learning about the [SCIHorizontalLineAnnotation](horizontallineannotation.html) since it's very similar with the **Vertical** one.

![Vertical Line Annotation](img/annotations/vertical-line-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-interaction-with-annotations-example/)

The `SCIVerticalLineAnnotation` class is inherited from [SCILineAnnotation](lineannotation.htm), and hence, provides the `SCILineAnnotationBase.stroke` property which is used to define the line annotation color. It expects a `SCIPenStyle` object.
To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

In **general** case, the position of an `SCIVerticalLineAnnotation` can only be defined by the `ISCIAnnotation.x1` value, which will lead to full-height vertical line at `X1` coordinate.

Despite the above, it is possible to specify `Y1` and `Y2` coordinates for the line ends, but it will work differently while combined with different `SCIAlignment`. 
`SCIVerticalLineAnnotation.verticalAlignment` property can consume the following values:
- `SCIAlignment.SCIAlignment_Top` - the `Y1` coordinate will be applied to the **bottom** end of a line. The line appears pinned to the **top** side.
- `SCIAlignment.SCIAlignment_Bottom` - the `Y1` coordinate will be applied to the **top** end of a line. The line appears pinned to the **bottom** side.
- `SCIAlignment.SCIAlignment_CenterVertical` - both `Y1` and `Y2` coordinates **will be applied**.
- `SCIAlignment.SCIAlignment_FillVertical` - both `Y1` and `Y2` coordinates **are ignored**. The line appears vertically stretched. This is the **default value**.

The `Y1` and `Y2` values can be accessed via the `ISCIAnnotation.y1` and `ISCIAnnotation.y2` properties.

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a VerticalLine Annotation
A `SCIVerticalLineAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Vertical Lines
    SCIVerticalLineAnnotation *verticalLine1 = [SCIVerticalLineAnnotation new];
    SCIVerticalLineAnnotation *verticalLine2 = [SCIVerticalLineAnnotation new];

    // Allow to interact with the annotation in run-time
    verticalLine1.isEditable = YES;
    verticalLine2.isEditable = YES;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    verticalLine1.xAxisId = BottomAxisId;
    verticalLine1.yAxisId = RightAxisId;
    verticalLine2.xAxisId = BottomAxisId;
    verticalLine2.yAxisId = RightAxisId;
    
    // Specify a desired position by setting coordinates and mode
    verticalLine1.coordinateMode = SCIAnnotationCoordinateMode_RelativeX;
    verticalLine2.coordinateMode = SCIAnnotationCoordinateMode_RelativeX;
    verticalLine1.x1 = @(0.1);
    verticalLine2.x1 = @(0.9);
    
    // Specify the border color for the annotation
    verticalLine1.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFF1919 thickness:2];
    verticalLine2.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:2];
    
    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations addAll:verticalLine1, verticalLine2, nil];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Vertical Lines
    let verticalLine1 = SCIVerticalLineAnnotation()
    let verticalLine2 = SCIVerticalLineAnnotation()

    // Allow to interact with the annotation in run-time
    verticalLine1.isEditable = true
    verticalLine2.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    verticalLine1.xAxisId = BottomAxisId
    verticalLine1.yAxisId = RightAxisId
    verticalLine2.xAxisId = BottomAxisId
    verticalLine2.yAxisId = RightAxisId

    // Specify a desired position by setting coordinates
    verticalLine1.coordinateMode = .relativeX
    verticalLine2.coordinateMode = .relativeX
    verticalLine1.set(x1: 0.1)
    verticalLine2.set(x1: 0.9)
    
    // Specify the stroke color for the annotation
    verticalLine1.stroke = SCISolidPenStyle(colorCode: 0xFFFF1919, thickness: 2)
    verticalLine2.stroke = SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 2)

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(verticalLine1)
    self.surface.annotations.add(verticalLine2)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Vertical Lines
    var verticalLine1 = new SCIVerticalLineAnnotation();
    var verticalLine2 = new SCIVerticalLineAnnotation();

    // Allow to interact with the annotation in run-time
    verticalLine1.IsEditable = true;
    verticalLine2.IsEditable = true;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    verticalLine1.XAxisId = BottomAxisId;
    verticalLine1.YAxisId = RightAxisId;
    verticalLine2.XAxisId = BottomAxisId;
    verticalLine2.YAxisId = RightAxisId;

    // Specify a desired position by setting coordinates
    verticalLine1.CoordinateMode = SCIAnnotationCoordinateMode.RelativeX;
    verticalLine2.CoordinateMode = SCIAnnotationCoordinateMode.RelativeX;
    verticalLine1.X1Value = 0.1;
    verticalLine2.X1Value = 0.9;

    // Specify the stroke color for the annotation
    verticalLine1.Stroke = new SCISolidPenStyle(0xFFFF1919, 2);
    verticalLine2.Stroke = new SCISolidPenStyle(0xFF279B27, 2);

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(verticalLine1);
    Surface.Annotations.Add(verticalLine2);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.

## The AnnotationLabels collection
By default, the `SCIVerticalLineAnnotation` does not show the any labels. You can show a label by adding a `SCIAnnotationLabel` to the `SCILineAnnotationWithLabelsBase.annotationLabels` collection, like below;

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIAnnotationLabel *annotationLabel = [SCIAnnotationLabel new];
    annotationLabel.labelPlacement = SCILabelPlacement_Axis;
    [verticalLine.annotationLabels add:annotationLabel];
</div>
<div class="code-snippet" id="swift">
    let annotationLabel = SCIAnnotationLabel()
    annotationLabel.labelPlacement = .axis
    verticalLine.annotationLabels.add(annotationLabel)
</div>
<div class="code-snippet" id="cs">
    var annotationLabel = new SCIAnnotationLabel();
    annotationLabel.LabelPlacement = SCILabelPlacement.Axis;
    verticalLine.AnnotationLabels.Add(annotationLabel);
</div>

The Label position can be changed by setting the `SCIAnnotationLabel.labelPlacement` property which expects one of the `SCILabelPlacement` enumeration.

> **_NOTE:_** Everything about **AnnotationLabels collection** and `SCIAnnotationLabel` can be also applied to the [SCIHorizontalLineAnnotation](horizontallineannotation.html)

#### The SCIAnnotationLabel Type
You can change appearance, position, custom value, etcetera for any annotation label which are listed below:
- `borderPen` - defines the AnnotationLabel outline.
- `backgroundBrush` - defines the AnnotationLabel background.
- `padding` - defines the padding around the label
- `rotationAngle` - allows to rotate annotation label text, expects ***degrees***
- `text` - you can set custom **text** for your label.
- `attributedText` - you can set custom **attributed text** for your label. In this case, the `text` property will be ignored. 
- `fontStyle` - applies the `SCIFontStyle` object onto the text.

> **_NOTE:_** By default, `SCIAnnotationLabel` uses it's associated axis `Y-value` to display label.
>
> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

Also, you can have more than one **AnnotationLabel** associated with `SCIVerticalLineAnnotation` by adding more than one to the `SCILineAnnotationWithLabelsBase.annotationLabels` collection.

Please see the code below, which showcases the utilization of the above settings:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIAnnotationLabel *axisAnnotationLabel = [SCIAnnotationLabel new];
    axisAnnotationLabel.labelPlacement = SCILabelPlacement_Axis;
    axisAnnotationLabel.padding = (UIEdgeInsets){ .top = 10 };
    [verticalLine1.annotationLabels add:axisAnnotationLabel];
    
    SCIAnnotationLabel *annotationLabel = [SCIAnnotationLabel new];
    annotationLabel.labelPlacement = SCILabelPlacement_TopRight;
    annotationLabel.text = @"Rotated Label";
    annotationLabel.rotationAngle = -90;
    
    verticalLine2.verticalAlignment = SCIAlignment_Bottom;
    verticalLine2.y1 = @(8);
    [verticalLine2.annotationLabels add:[SCIAnnotationLabel new]];
    [verticalLine2.annotationLabels add:annotationLabel];
</div>
<div class="code-snippet" id="swift">
    let axisAnnotationLabel = SCIAnnotationLabel()
    axisAnnotationLabel.labelPlacement = .axis
    axisAnnotationLabel.padding = UIEdgeInsets(top: 10, left: 0, bottom: 0, right: 0)
    verticalLine1.annotationLabels.add(axisAnnotationLabel)
    
    let annotationLabel = SCIAnnotationLabel()
    annotationLabel.labelPlacement = .topRight
    annotationLabel.text = "Rotated Label"
    annotationLabel.rotationAngle = -90
    
    verticalLine2.verticalAlignment = .bottom
    verticalLine2.set(y1: 8)
    verticalLine2.annotationLabels.add(SCIAnnotationLabel())
    verticalLine2.annotationLabels.add(annotationLabel)
</div>
<div class="code-snippet" id="cs">
    var axisAnnotationLabel = new SCIAnnotationLabel();
    axisAnnotationLabel.LabelPlacement = SCILabelPlacement.Axis;
    axisAnnotationLabel.Padding = new UIEdgeInsets(10, 0, 0, 0);
    verticalLine1.AnnotationLabels.Add(axisAnnotationLabel);

    var annotationLabel = new SCIAnnotationLabel();
    annotationLabel.LabelPlacement = SCILabelPlacement.TopRight;
    annotationLabel.Text = "Rotated Label";
    annotationLabel.RotationAngle = -90;

    verticalLine2.VerticalAlignment = SCIAlignment.Bottom;
    verticalLine2.Y1Value = 8;
    verticalLine2.AnnotationLabels.Add(new SCIAnnotationLabel());
    verticalLine2.AnnotationLabels.Add(annotationLabel);
</div>

This will result in the following:

![Vertical Line Annotation With Labels](img/annotations/vertical-line-annotation-with-labels.png)

#### Annotation Label Value and TextFormatting
By default, the label text is formatted by the `ISCIAxisCore.textFormatting` property. For more information, refer to the [Axis Labels - TextFormatting and CursorTextFormatting](axis-labels---textformatting-and-cursortextformatting.html) article.

But you can also override the default behaviour by providing a custom `ISCIFormattedValueProvider` for your `SCIVerticalLineAnnotation` corresponding property.

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
    // Create a Vertical Lines
    SCIVerticalLineAnnotation *verticalLine = [SCIVerticalLineAnnotation new];
    // In a multi-axis scenario, specify the XAxisId and YAxisId
    verticalLine.xAxisId = BottomAxisId;
    verticalLine.yAxisId = RightAxisId;
    // Specify a desired position by setting coordinates and mode
    verticalLine.x1 = @(65);
    // Specify the border color for the annotation
    verticalLine.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFF1919 thickness:2];
    // Provide custom ISCIFormattedValueProvider for the annotation
    verticalLine.formattedLabelValueProvider = [AnnotationValueProvider new];
    
    // Provide the Axis Label
    SCIAnnotationLabel *axisAnnotationLabel = [SCIAnnotationLabel new];
    axisAnnotationLabel.labelPlacement = SCILabelPlacement_Axis;
    [verticalLine.annotationLabels add:axisAnnotationLabel];
    
    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:verticalLine];
</div>
<div class="code-snippet" id="swift">
    // Declare custom ISCIFormattedValueProvider
    class AnnotationValueProvider: ISCIFormattedValueProvider {
        func formatValue(with axisInfo: SCIAxisInfo!) -> ISCIString! {
            return axisInfo != nil ? NSString(string: "[ --- \(axisInfo.axisFormattedDataValue!) --- ]") : nil;
        }
    }
    ...
    // Create a Vertical Lines
    let verticalLine = SCIVerticalLineAnnotation()
    // In a multi-axis scenario, specify the XAxisId and YAxisId
    verticalLine.xAxisId = BottomAxisId
    verticalLine.yAxisId = RightAxisId
    // Specify a desired position by setting coordinates and mode
    verticalLine.set(x1: 65)
    // Specify the border color for the annotation
    verticalLine.stroke = SCISolidPenStyle(colorCode: 0xFFFF1919, thickness: 2)
    // Provide custom ISCIFormattedValueProvider for the annotation
    verticalLine.formattedLabelValueProvider = AnnotationValueProvider()
    
    // Provide the Axis Label
    let axisAnnotationLabel = SCIAnnotationLabel()
    axisAnnotationLabel.labelPlacement = .axis
    verticalLine.annotationLabels.add(axisAnnotationLabel)
    
    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(verticalLine)
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
    // Create a Vertical Lines
    var verticalLine = new SCIVerticalLineAnnotation();
    // In a multi-axis scenario, specify the XAxisId and YAxisId
    verticalLine.XAxisId = TopAxisId;
    verticalLine.YAxisId = RightAxisId;
    // Specify a desired position by setting coordinates
    verticalLine.X1Value = 65;
    // Specify the stroke color for the annotation
    verticalLine.Stroke = new SCISolidPenStyle(0xFFFF1919, 2);
    // Provide custom ISCIFormattedValueProvider for the annotation
    verticalLine.FormattedLabelValueProvider = new AnnotationValueProvider();

    // Provide the Axis Label
    var axisAnnotationLabel = new SCIAnnotationLabel();
    axisAnnotationLabel.LabelPlacement = SCILabelPlacement.Axis;
    verticalLine.AnnotationLabels.Add(axisAnnotationLabel);

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(verticalLine);
</div>

This will result in the following:

![Vertical Line Annotation Label Formatting](img/annotations/vertical-line-annotation-label-formatting.png)

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.
