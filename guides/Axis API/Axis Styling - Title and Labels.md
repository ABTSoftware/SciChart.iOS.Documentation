# Axis Styling - Title and Labels
**Each and Every** aspect of the axis can be styled. The Axis is responsible for drawing the following parts:
- [Title](#axis-title)
- [Axis Labels](#axis-labels)
- [Tick Lines](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-ticks) - small marks on the outside of an axis **next to labels**
- [Gridlines](axis-styling---grid-lines-ticks-and-axis-bands.html#grid-lines) - major and minor
- [Axis Bands](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-bands) - shading between the **major** gridlines

In this article we are going to focus on [Axis Title](#axis-title) and [Axis Labels](#axis-labels) styling.

> **_NOTE:_** In SciChart, almost all styling methods expect an instance of either `SCIPenStyle` or `SCIBrushStyle` to be passed in. Those that deal with text styling, expect an instance of a `SCIFontStyle`. To learn more about how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

## Axis Title
The `SCIFontStyle` object can be applied to axis labels via the `ISCIAxisCore.titleStyle` property.

In addition to font style, there are several options available for positioning of the title, including inside the axis area:
- `ISCIAxis.axisTitlePlacement` - allows placing a title inside or outside the axis area via the `SCIAxisTitlePlacement`.
- `ISCIAxis.axisTitleOrientation` - changes the orientation of the title, making it horizontal or vertical via the `SCIAxisTitleOrientation` enum.
- `ISCIAxis.axisTitleAlignment` - the desired alignment of the title via the `SCIAlignment`.
- `ISCIAxis.axisTitleMargins` - margins could be applied to omit overlapping with labels, while using `SCIAxisTitlePlacement.SCIAxisTitlePlacement_Inside` mode.

## Axis Labels
The tick labels can be hidden or shown on an axis via the `ISCIAxisCore.drawLabels` property. To make the labels at the edges to always appear inside the axis area, use the `ISCIAxis.autoFitMarginalLabels` property.

The `SCIFontStyle` object can be applied to axis labels via the `ISCIAxisCore.tickLabelStyle` property.

Also, there is `SCIAxisTickLabelStyle` available for controlling **alignment**, **margin** and **rotation angle** of each Label during rendering.

Let's try to modify our [Multiple Axes Example](https://www.scichart.com/example/ios-multiple-axis-demo/) with the following code, and see what we can achieve:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; xTopAxis = [SCINumericAxis new];
    xTopAxis.axisId = TopAxisId;
    xTopAxis.axisAlignment = SCIAxisAlignment_Top;
    xTopAxis.axisTitle = @"Top Axis";
    xTopAxis.axisTitlePlacement = SCIAxisTitlePlacement_Bottom;
    xTopAxis.titleStyle = [[SCIFontStyle alloc] initWithFontSize:18 andTextColorCode:0xFF279B27];
    xTopAxis.tickLabelStyle = [[SCIFontStyle alloc] initWithFontSize:12 andTextColorCode:0xFF279B27];
    
    id&lt;ISCIAxis&gt; xBottomAxis = [SCINumericAxis new];
    xBottomAxis.axisId = BottomAxisId;
    xBottomAxis.axisAlignment = SCIAxisAlignment_Bottom;
    xBottomAxis.axisTitle = @"Bottom Axis";
    xBottomAxis.titleStyle = [[SCIFontStyle alloc] initWithFontSize:18 andTextColorCode:0xFFFF1919];
    xBottomAxis.tickLabelStyle = [[SCIFontStyle alloc] initWithFontSize:12 andTextColorCode:0xFFFF1919];
    
    id&lt;ISCIAxis&gt; yLeftAxis = [SCINumericAxis new];
    yLeftAxis.axisId = LeftAxisId;
    yLeftAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    yLeftAxis.axisAlignment = SCIAxisAlignment_Left;
    yLeftAxis.axisTitle = @"Left Axis";
    yLeftAxis.axisTitleAlignment = SCIAlignment_Top;
    yLeftAxis.axisTitlePlacement = SCIAxisTitlePlacement_Inside;
    yLeftAxis.axisTitleMargins = (UIEdgeInsets){ .top = 5, .left = 20 };
    yLeftAxis.titleStyle = [[SCIFontStyle alloc] initWithFontSize:18 andTextColorCode:0xFFFC9C29];
    yLeftAxis.tickLabelStyle = [[SCIFontStyle alloc] initWithFontSize:12 andTextColorCode:0xFFFC9C29];
    yLeftAxis.axisTickLabelStyle = [[SCIAxisTickLabelStyle alloc] initWithAlignment:SCIAlignment_Center margins:UIEdgeInsetsZero andRotationAngle:45];
    
    id&lt;ISCIAxis&gt; yRightAxis = [SCINumericAxis new];
    yRightAxis.axisId = RightAxisId;
    yRightAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    yRightAxis.axisAlignment = SCIAxisAlignment_Right;
    yRightAxis.isCenterAxis = YES;
    yRightAxis.axisTitle = @"Right Axis";
    yRightAxis.axisTitleAlignment = SCIAlignment_Bottom;
    yRightAxis.axisTitlePlacement = SCIAxisTitlePlacement_Left;
    yRightAxis.tickLabelStyle = [[SCIFontStyle alloc] initWithFontSize:12 andTextColorCode:0xFF4083B7];
    yRightAxis.titleStyle = [[SCIFontStyle alloc] initWithFontSize:18 andTextColorCode:0xFF4083B7];
</div>
<div class="code-snippet" id="swift">
    let xTopAxis = SCINumericAxis()
    xTopAxis.axisId = TopAxisId
    xTopAxis.axisAlignment = .top
    xTopAxis.axisTitle = "Top Axis"
    xTopAxis.axisTitlePlacement = .bottom
    xTopAxis.titleStyle = SCIFontStyle(fontSize: 18, andTextColorCode: 0xFF279B27)
    xTopAxis.tickLabelStyle = SCIFontStyle(fontSize: 12, andTextColorCode: 0xFF279B27)
    
    let xBottomAxis = SCINumericAxis()
    xBottomAxis.axisId = BottomAxisId
    xBottomAxis.axisAlignment = .bottom
    xBottomAxis.axisTitle = "Bottom Axis"
    xBottomAxis.titleStyle = SCIFontStyle(fontSize: 18, andTextColorCode: 0xFFFF1919)
    xBottomAxis.tickLabelStyle = SCIFontStyle(fontSize: 12, andTextColorCode: 0xFFFF1919)
    
    let yLeftAxis = SCINumericAxis()
    yLeftAxis.axisId = LeftAxisId
    yLeftAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    yLeftAxis.axisAlignment = .left
    yLeftAxis.axisTitle = "Left Axis"
    yLeftAxis.axisTitleAlignment = .top
    yLeftAxis.axisTitlePlacement = .inside
    yLeftAxis.axisTitleMargins = UIEdgeInsets(top: 5, left: 20, bottom: 0, right: 0)
    yLeftAxis.titleStyle = SCIFontStyle(fontSize: 18, andTextColorCode: 0xFFFC9C29)
    yLeftAxis.tickLabelStyle = SCIFontStyle(fontSize: 12, andTextColorCode: 0xFFFC9C29)
    yLeftAxis.axisTickLabelStyle = SCIAxisTickLabelStyle(alignment: .center, margins: .zero, andRotationAngle: 45)

    let yRightAxis = SCINumericAxis()
    yRightAxis.axisId = RightAxisId
    yRightAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    yRightAxis.axisAlignment = .right
    yRightAxis.isCenterAxis = true
    yRightAxis.axisTitle = "Right Axis"
    yRightAxis.axisTitleAlignment = .bottom
    yRightAxis.axisTitlePlacement = .left
    yRightAxis.titleStyle = SCIFontStyle(fontSize: 18, andTextColorCode: 0xFF4083B7)
    yRightAxis.tickLabelStyle = SCIFontStyle(fontSize: 12, andTextColorCode: 0xFF4083B7)
</div>
<div class="code-snippet" id="cs">
    var xTopAxis = new SCINumericAxis();
    xTopAxis.AxisId = TopAxisId;
    xTopAxis.AxisAlignment = SCIAxisAlignment.Top;
    xTopAxis.AxisTitle = "Top Axis";
    xTopAxis.AxisTitlePlacement = SCIAxisTitlePlacement.Bottom;
    xTopAxis.TitleStyle = new SCIFontStyle(18, 0xFF279B27);
    xTopAxis.TickLabelStyle = new SCIFontStyle(12, 0xFF279B27);

    var xBottomAxis = new SCINumericAxis();
    xBottomAxis.AxisId = BottomAxisId
    xBottomAxis.AxisAlignment = SCIAxisAlignment.Bottom;
    xBottomAxis.AxisTitle = "Bottom Axis";
    xBottomAxis.TitleStyle = new SCIFontStyle(18, 0xFFFF1919);
    xBottomAxis.TickLabelStyle = new SCIFontStyle(12, 0xFFFF1919);

    var yLeftAxis = new SCINumericAxis();
    yLeftAxis.AxisId = LeftAxisId
    yLeftAxis.GrowBy = new SCIDoubleRange(0.1, 0.1);
    yLeftAxis.AxisAlignment = SCIAxisAlignment.Left;
    yLeftAxis.AxisTitle = "Left Axis";
    yLeftAxis.AxisTitleAlignment = SCIAlignment.Top;
    yLeftAxis.AxisTitlePlacement = SCIAxisTitlePlacement.Inside;
    yLeftAxis.AxisTitleMargins = new UIEdgeInsets(5, 20, 0, 0);
    yLeftAxis.TitleStyle = new SCIFontStyle(18, 0xFFFC9C29);
    yLeftAxis.TickLabelStyle = new SCIFontStyle(12, 0xFFFC9C29);
    yLeftAxis.AxisTickLabelStyle = new SCIAxisTickLabelStyle(SCIAlignment.Center, UIEdgeInsets.Zero, 45);

    var yRightAxis = new SCINumericAxis();
    yRightAxis.AxisId = RightAxisId
    yRightAxis.GrowBy = new SCIDoubleRange(0.1, 0.1);
    yRightAxis.AxisAlignment = SCIAxisAlignment.Right;
    yRightAxis.IsCenterAxis = true;
    yRightAxis.AxisTitle = "Right Axis";
    yRightAxis.AxisTitleAlignment = SCIAlignment.Bottom;
    yRightAxis.AxisTitlePlacement = SCIAxisTitlePlacement.Left;
    yRightAxis.TitleStyle = new SCIFontStyle(18, 0xFF4083B7);
    yRightAxis.TickLabelStyle = new SCIFontStyle(12, 0xFF4083B7);
</div>

![Title Positioning](img/axis-2d/axis-title-labels-styling.png)

> **_NOTE:_** You might notice, that **Right Axis** title, labels and ticks are placed inside the chart area. This behaviour is controlled by `ISCIAxis.isCenterAxis`. To learn more, please refer to [Axis Placement](axis-layout---central-axis.html#axis-placement) article.
