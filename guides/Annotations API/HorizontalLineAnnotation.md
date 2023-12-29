# The SCIHorizontalLineAnnotation
The `SCIHorizontalLineAnnotation` draws the **horizontal line** between `X1` and `X2` coordinates at `Y1`:

> **_NOTE:_** You might find it useful to learn about the [SCIVerticalLineAnnotation](verticallineannotation.html) as well since it's very similar with the **Horizontal** one.

![Horizontal Line Annotation](img/annotations/horizontal-line-annotation.png)

> **_NOTE:_** Examples of the **`Annotations`** usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - Annotations are Easy - [Obj-C/Swift](https://www.scichart.com/example/ios-chart-chart-annotations-are-easy-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart-annotations-example/)
> - Interaction with Annotations - [Obj-C/Swift](https://www.scichart.com/example/ios-chart/ios-chart-chart-interaction-with-annotations-example/), [Xamarin.iOS](https://www.scichart.com/example/xamarin-chart/xamarin-chart-interaction-with-annotations-example/)

The `SCIHorizontalLineAnnotation` class is inherited from [SCILineAnnotation](lineannotation.html), and hence, provides the `SCILineAnnotationBase.stroke` property which is used to define the line annotation color. It expects a `SCIPenStyle` object.
To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

> **_NOTE:_** To learn more about **Annotations** in general - please see the [Common Annotation Features](Annotations APIs.html#common-annotations-features) article.

In **general** case, the position of an `SCIHorizontalLineAnnotation` can be defined by the `ISCIAnnotation.y1` value only, which will lead to full-width horizontal line at `Y1` coordinate.

Despite the above, it is possible to specify `X1` and `X2` coordinates for the line ends, but it will work differently while combined with different `SCIAlignment`. 
`SCIHorizontalLineAnnotation.horizontalAlignment` property can consume the following values:
- `SCIAlignment.SCIAlignment_Left` - the `X1` coordinate will be applied to the **right** end of a line. The line appears pinned to the **left** side.
- `SCIAlignment.SCIAlignment_Right` - the `X1` coordinate will be applied to the **left** end of a line. The line appears pinned to the **right** side.
- `SCIAlignment.SCIAlignment_CenterHorizontal` - both `X1` and `X2` coordinates **will be applied**.
- `SCIAlignment.SCIAlignment_FillHorizontal` - both `X1` and `X2` coordinates **are ignored**. The line appears horizontally stretched. This is the **default value**.

The `X1` and `X2` values can be accessed via the `ISCIAnnotation.x1` and `ISCIAnnotation.x2` properties

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have an axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Create a HorizontalLine Annotation
A `SCIHorizontalLineAnnotation` can be added onto a chart using the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a HorizontalLine Annotation
    SCIHorizontalLineAnnotation *horizontalLine = [SCIHorizontalLineAnnotation new];

    // Allow to interact with the annotation in run-time
    horizontalLine.isEditable = YES;

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    horizontalLine.xAxisId = TopAxisId;
    horizontalLine.yAxisId = RightAxisId;
    
    // Specify a desired position by setting coordinates and mode
    horizontalLine.coordinateMode = SCIAnnotationCoordinateMode_RelativeY;
    horizontalLine.y1 = @(0.1);
    
    // Specify the border color for the annotation
    horizontalLine.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFC9C29 thickness:2];

    // Add the annotation to the Annotations collection of the surface
    [self.surface.annotations add:horizontalLine];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a HorizontalLine Annotation
    let horizontalLine = SCIHorizontalLineAnnotation()

    // Allow to interact with the annotation in run-time
    horizontalLine.isEditable = true

    // In a multi-axis scenario, specify the XAxisId and YAxisId
    horizontalLine.xAxisId = TopAxisId
    horizontalLine.yAxisId = RightAxisId

    // Specify a desired position by setting coordinates
    horizontalLine.coordinateMode = .relativeY
    horizontalLine.set(y1: 0.1)
    
    // Specify the stroke color for the annotation
    horizontalLine.stroke = SCISolidPenStyle(colorCode: 0xFFFC9C29, thickness: 2)

    // Add the annotation to the Annotations collection of the surface
    self.surface.annotations.add(horizontalLine)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a HorizontalLine Annotation
    var horizontalLine = new SCIHorizontalLineAnnotation
    {
        // Allow to interact with the annotation in run-time
        IsEditable = true,

        // In a multi-axis scenario, specify the XAxisId and YAxisId
        XAxisId = TopAxisId,
        YAxisId = RightAxisId,

        // Specify a desired position by setting coordinates
        CoordinateMode = SCIAnnotationCoordinateMode.RelativeY,
        Y1Value = 0.1,

        // Specify the stroke color for the annotation
        Stroke = new SCISolidPenStyle(0xFFFC9C29, 2),
    };

    // Add the annotation to the Annotations collection of the surface
    Surface.Annotations.Add(horizontalLine);
</div>

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.

## The AnnotationLabels collection
By default, the `SCIHorizontalLineAnnotation` does not show any labels. You can show a label by adding a `SCIAnnotationLabel` to the `SCILineAnnotationWithLabelsBase.annotationLabels` collection, like below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIAnnotationLabel *annotationLabel = [SCIAnnotationLabel new];
    annotationLabel.labelPlacement = SCILabelPlacement_Axis;
    [horizontalLine.annotationLabels add:annotationLabel];
</div>
<div class="code-snippet" id="swift">
    let annotationLabel = SCIAnnotationLabel()
    annotationLabel.labelPlacement = .axis
    horizontalLine.annotationLabels.add(annotationLabel)
</div>
<div class="code-snippet" id="cs">
    var annotationLabel = new SCIAnnotationLabel();
    annotationLabel.LabelPlacement = SCILabelPlacement.Axis;
    horizontalLine.AnnotationLabels.Add(annotationLabel);
</div>

The Label position can be changed by setting the `SCIAnnotationLabel.labelPlacement` property which expects one of the `SCILabelPlacement` enumeration.

> **_NOTE:_** Everything about **AnnotationLabels collection** and `SCIAnnotationLabel` can be also applied to the [SCIVerticalLineAnnotation](verticallineannotation.html)

#### The SCIAnnotationLabel Type
You can change appearance, position, custom value, etcetera for any annotation label which are listed below:
- `borderPen` - defines the AnnotationLabel outline.
- `backgroundBrush` - defines the AnnotationLabel background.
- `padding` - defines the padding around the label
- `rotationAngle` - allows to rotate annotation label text, expects ***degrees***
- `text` - you can set custom **text** for your label.
- `attributedText` - you can set custom **attributed text** for your label. In this case, the `text` property will be ignored. 
- `fontStyle` - applies the `SCIFontStyle` object onto the text.

> **_NOTE:_** By default, `SCIAnnotationLabel` uses its associated axis `Y-value` to display a label.
>
> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

Also, you can have more than one **AnnotationLabel** associated with `SCIHorizontalLineAnnotation` by adding more than one to the `SCILineAnnotationWithLabelsBase.annotationLabels` collection.

Please see the code below, which showcases the utilization of the above settings:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIHorizontalLineAnnotation *horizontalLine = [SCIHorizontalLineAnnotation new];
    horizontalLine.x1 = @(10);
    horizontalLine.y1 = @(34.512);
    horizontalLine.isEditable = YES;
    horizontalLine.horizontalAlignment = SCIAlignment_Right;
    horizontalLine.stroke = [[SCISolidPenStyle alloc] initWithColor:UIColor.redColor thickness:2];
    
    SCIAnnotationLabel *axisAnnotationLabel = [SCIAnnotationLabel new];
    axisAnnotationLabel.labelPlacement = SCILabelPlacement_Axis;
    axisAnnotationLabel.rotationAngle = -10;
    axisAnnotationLabel.padding = (UIEdgeInsets){ .left = 10 };
    
    SCIAnnotationLabel *annotationLabel = [SCIAnnotationLabel new];
    annotationLabel.labelPlacement = SCILabelPlacement_TopLeft;
    annotationLabel.text = @"Whatever Label";
    annotationLabel.fontStyle = [[SCIFontStyle alloc] initWithFontSize:25 andTextColor:UIColor.yellowColor];
    
    [horizontalLine.annotationLabels addAll:annotationLabel, axisAnnotationLabel, nil];
</div>
<div class="code-snippet" id="swift">
    let horizontalLine = SCIHorizontalLineAnnotation()
    horizontalLine.set(x1: 10)
    horizontalLine.set(y1: 34.512)
    horizontalLine.isEditable = true
    horizontalLine.horizontalAlignment = .right
    horizontalLine.stroke = SCISolidPenStyle(color: .red, thickness: 2)
    
    let axisAnnotationLabel = SCIAnnotationLabel()
    axisAnnotationLabel.labelPlacement = .axis
    axisAnnotationLabel.rotationAngle = -10;
    axisAnnotationLabel.padding = UIEdgeInsets(top: 0, left: -10, bottom: 0, right: 0)
    
    let annotationLabel = SCIAnnotationLabel()
    annotationLabel.labelPlacement = .topLeft
    annotationLabel.text = "Whatever Label"
    annotationLabel.fontStyle = SCIFontStyle(fontSize: 25, andTextColor: .yellow)
    
    horizontalLine.annotationLabels.add(annotationLabel)
    horizontalLine.annotationLabels.add(axisAnnotationLabel)
</div>
<div class="code-snippet" id="cs">
    var horizontalLine = new SCIHorizontalLineAnnotation();
    horizontalLine.X1Value = 10;
    horizontalLine.Y1Value = 34.512;
    horizontalLine.IsEditable = true;
    horizontalLine.HorizontalAlignment = SCIAlignment.Right;
    horizontalLine.Stroke = new SCISolidPenStyle(UIColor.Red, 2);

    var axisAnnotationLabel = new SCIAnnotationLabel();
    axisAnnotationLabel.LabelPlacement = SCILabelPlacement.Axis;
    axisAnnotationLabel.RotationAngle = -10;
    axisAnnotationLabel.Padding = new UIEdgeInsets(0, -10, 0, 0);

    var annotationLabel = new SCIAnnotationLabel();
    annotationLabel.LabelPlacement = SCILabelPlacement.TopLeft;
    annotationLabel.Text = "Whatever Label";
    annotationLabel.FontStyle = new SCIFontStyle(25, UIColor.Yellow);

    horizontalLine.AnnotationLabels.Add(annotationLabel);
    horizontalLine.AnnotationLabels.Add(axisAnnotationLabel);
</div>

The result will be the following:

![Horizontal Line Annotation With Labels](img/annotations/horizontal-line-annotation-with-labels.png)

#### Annotation Label Value and TextFormatting
By default, the label text is formatted by the `ISCIAxisCore.textFormatting` property. For more information, refer to the [Axis Labels - TextFormatting and CursorTextFormatting](axis-labels---textformatting-and-cursortextformatting.html) article.

But you can also override the default behaviour by providing a custom `ISCIFormattedValueProvider` for your `SCIHorizontalLineAnnotation` corresponding property.

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
        return axisInfo != nil ? [NSString stringWithFormat:@"$ %@ $", axisInfo.axisFormattedDataValue] : nil;
    }

    @end
    ...
    SCIHorizontalLineAnnotation *horizontalLine = [SCIHorizontalLineAnnotation new];
    horizontalLine.x1 = @(10);
    horizontalLine.y1 = @(34.512);
    horizontalLine.isEditable = YES;
    horizontalLine.horizontalAlignment = SCIAlignment_Right;
    // Provide custom ISCIFormattedValueProvider for the annotation
    horizontalLine.formattedLabelValueProvider = [AnnotationValueProvider new];
    
    SCIAnnotationLabel *axisAnnotationLabel = [SCIAnnotationLabel new];
    axisAnnotationLabel.labelPlacement = SCILabelPlacement_Axis;

    [horizontalLine.annotationLabels add:axisAnnotationLabel];
</div>
<div class="code-snippet" id="swift">
    // Declare custom ISCIFormattedValueProvider
    class AnnotationValueProvider: ISCIFormattedValueProvider {
        func formatValue(with axisInfo: SCIAxisInfo!) -> ISCIString! {
            return axisInfo != nil ? NSString(string: "$ \(axisInfo.axisFormattedDataValue!) $") : nil;
        }
    }
    ...
    let horizontalLine = SCIHorizontalLineAnnotation()
    horizontalLine.set(x1: 10)
    horizontalLine.set(y1: 34.512)
    horizontalLine.isEditable = true
    horizontalLine.horizontalAlignment = .right
    // Provide custom ISCIFormattedValueProvider for the annotation
    horizontalLine.formattedLabelValueProvider = AnnotationValueProvider()
    
    let axisAnnotationLabel = SCIAnnotationLabel()
    axisAnnotationLabel.labelPlacement = .axis

    horizontalLine.annotationLabels.add(axisAnnotationLabel)
</div>
<div class="code-snippet" id="cs">
    // Declare custom ISCIFormattedValueProvider
    class AnnotationValueProvider : ISCIFormattedValueProvider
    {
        public override IISCIString FormatValueWithAxisInfo(SCIAxisInfo axisInfo)
        {
            return axisInfo != null ? $"$ {axisInfo.AxisFormattedDataValue} $".ToSciString() : null;
        }
    }
    ...
    var horizontalLine = new SCIHorizontalLineAnnotation();
    horizontalLine.X1Value = 10;
    horizontalLine.Y1Value = 34.512;
    horizontalLine.IsEditable = true;
    horizontalLine.HorizontalAlignment = SCIAlignment.Right;
    // Provide custom ISCIFormattedValueProvider for the annotation
    horizontalLine.FormattedLabelValueProvider = new AnnotationValueProvider();
    
    var axisAnnotationLabel = new SCIAnnotationLabel();
    axisAnnotationLabel.LabelPlacement = SCILabelPlacement.Axis;

    horizontalLine.AnnotationLabels.Add(axisAnnotationLabel);
</div>

The result will be the following:

![Horizontal Line Annotation Label Formatting](img/annotations/horizontal-line-annotation-label-formatting.png)

> **_NOTE:_** To learn more about other **Annotation Types**, available out of the box in SciChart, please find the comprehensive list in the [Annotation APIs](Annotations APIs.html) article.