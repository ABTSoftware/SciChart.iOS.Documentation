# Axis 3D Styling - Labels and GridLines
Many aspects of the axis 3D can be styled. Same way, like in SciChart 2D the Axis 3D is responsible for drawing the following parts, most of which is same as in 2D and shared through `ISCIAxisCore`:

| **Axis Parts**    | **Description**                                          | **Related 2D Article**                                                                   |
| ----------------- | -------------------------------------------------------- | ---------------------------------------------------------------------------------------  |
| Labels and Titles | all 3D labels are styled via the `ISCIAxis3D.textColor`, `ISCIAxis3D.textSize` and `ISCIAxis3D.textFont` properties | ----------------------------- |
| Tick Lines        | small marks on the outside of an axis **next to labels** | [2D Tick Lines Styling](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-ticks)  |
| Grid Lines        | **major** and **minor** grid lines                       | [2D Grid Lines Styling](axis-styling---grid-lines-ticks-and-axis-bands.html#grid-lines)  |
| Axis Bands        | shading between the **major** grid-lines                 | [2D Axis Bands Styling](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-bands)  |

> **_NOTE:_** In SciChart, almost all styling methods expect an instance of either `SCIPenStyle` or `SCIBrushStyle` to be passed in. Those that deals with text styling, expect an instance of a `SCIFontStyle`. To learn more about how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

![Style 3D Chart](img/axis-3d/style-3d-chart-example.png)

> **_NOTE:_** The **Style 3D Chart** example can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-style-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-3d-chart-example-style-chart/)

Please take a look at the code snippet from the [Style 3D Chart](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-style-chart/) example which showcase some `ISCIAxis3D` styling:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCINumericAxis3D *xAxis = [SCINumericAxis3D new];
    xAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    xAxis.minorsPerMajor = 5;
    xAxis.maxAutoTicks = 7;
    xAxis.textSize = 13;
    xAxis.textColor = 0xFF00FF00;
    xAxis.textFont = @"RobotoCondensed-BoldItalic";
    xAxis.axisBandsStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0xFF556B2F];
    xAxis.majorTickLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF00FF00 thickness:1.0];
    xAxis.majorTickLineLength = 8.0;
    xAxis.minorTickLineStyle =  [[SCISolidPenStyle alloc] initWithColorCode:0xFFC71585 thickness:1.0];
    xAxis.minorTickLineLength = 4.0;
    xAxis.majorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF00FF00 thickness:1.0];
    xAxis.minorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF9400D3 thickness:1.0];
    
    SCINumericAxis3D *yAxis = [SCINumericAxis3D new];
    yAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    yAxis.minorsPerMajor = 5;
    yAxis.maxAutoTicks = 7;
    yAxis.textSize = 13;
    yAxis.textColor = 0xFFB22222;
    yAxis.textFont = @"RobotoCondensed-BoldItalic";
    yAxis.axisBandsStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0xFFFF6347];
    yAxis.majorTickLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFB22222 thickness:1.0];
    yAxis.majorTickLineLength = 8.0;
    yAxis.minorTickLineStyle =  [[SCISolidPenStyle alloc] initWithColorCode:0xFFCD5C5C thickness:1.0];
    yAxis.minorTickLineLength = 4.0;
    yAxis.majorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF006400 thickness:1.0];
    yAxis.minorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF8CBED6 thickness:1.0];
    
    SCINumericAxis3D *zAxis = [SCINumericAxis3D new];
    zAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    zAxis.minorsPerMajor = 5;
    zAxis.maxAutoTicks = 7;
    zAxis.textSize = 13;
    zAxis.textColor = 0xFFDB7093;
    zAxis.textFont = @"RobotoCondensed-BoldItalic";
    zAxis.axisBandsStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0xFFADFF2F];
    zAxis.majorTickLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFDB7093 thickness:1.0];
    zAxis.majorTickLineLength = 8.0;
    zAxis.minorTickLineStyle =  [[SCISolidPenStyle alloc] initWithColorCode:0xFF7FFF00 thickness:1.0];
    zAxis.minorTickLineLength = 4.0;
    zAxis.majorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFF5F5DC thickness:1.0];
    zAxis.minorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFA52A2A thickness:1.0];
</div>
<div class="code-snippet" id="swift">
    let xAxis = SCINumericAxis3D()
    xAxis.minorsPerMajor = 5
    xAxis.maxAutoTicks = 7
    xAxis.textSize = 13
    xAxis.textColor = 0xFF00FF00
    xAxis.textFont = "RobotoCondensed-BoldItalic"
    xAxis.axisBandsStyle = SCISolidBrushStyle(colorCode: 0xFF556B2F)
    xAxis.majorTickLineStyle = SCISolidPenStyle(colorCode: 0xFF00FF00, thickness: 1.0)
    xAxis.majorTickLineLength = 8.0
    xAxis.minorTickLineStyle = SCISolidPenStyle(colorCode: 0xFFC71585, thickness: 1.0)
    xAxis.minorTickLineLength = 4.0
    xAxis.majorGridLineStyle = SCISolidPenStyle(colorCode: 0xFF00FF00, thickness: 1.0)
    xAxis.minorGridLineStyle = SCISolidPenStyle(colorCode: 0xFF9400D3, thickness: 1.0)

    let yAxis = SCINumericAxis3D()
    yAxis.minorsPerMajor = 5
    yAxis.maxAutoTicks = 7
    yAxis.textSize = 13
    yAxis.textColor = 0xFFB22222
    yAxis.textFont = "RobotoCondensed-BoldItalic"
    yAxis.axisBandsStyle = SCISolidBrushStyle(colorCode: 0xFFFF6347)
    yAxis.majorTickLineStyle = SCISolidPenStyle(colorCode: 0xFFB22222, thickness: 1.0)
    yAxis.majorTickLineLength = 8.0
    yAxis.minorTickLineStyle = SCISolidPenStyle(colorCode: 0xFFCD5C5C, thickness: 1.0)
    yAxis.minorTickLineLength = 4.0
    yAxis.majorGridLineStyle = SCISolidPenStyle(colorCode: 0xFF006400, thickness: 1.0)
    yAxis.minorGridLineStyle = SCISolidPenStyle(colorCode: 0xFF8CBED6, thickness: 1.0)
    
    let zAxis = SCINumericAxis3D()
    zAxis.minorsPerMajor = 5
    zAxis.maxAutoTicks = 7
    zAxis.textSize = 13
    zAxis.textColor = 0xFFDB7093
    zAxis.textFont = "RobotoCondensed-BoldItalic"
    zAxis.axisBandsStyle = SCISolidBrushStyle(colorCode: 0xFFADFF2F)
    zAxis.majorTickLineStyle = SCISolidPenStyle(colorCode: 0xFFDB7093, thickness: 1.0)
    zAxis.majorTickLineLength = 8.0
    zAxis.minorTickLineStyle = SCISolidPenStyle(colorCode: 0xFF7FFF00, thickness: 1.0)
    zAxis.minorTickLineLength = 4.0
    zAxis.majorGridLineStyle = SCISolidPenStyle(colorCode: 0xFFF5F5DC, thickness: 1.0)
    zAxis.minorGridLineStyle = SCISolidPenStyle(colorCode: 0xFFA52A2A, thickness: 1.0)
</div>
<div class="code-snippet" id="cs">
    Surface.XAxis = new SCINumericAxis3D
    {   
        MinorsPerMajor = 5,
        MaxAutoTicks = 7,
        TextSize = 13f,
        TextColor = 0xFF00FF00,
        TextFont = font,
        AxisBandsStyle = new SCISolidBrushStyle(0xFF556B2F),
        MajorTickLineStyle = new SCISolidPenStyle(0xFF00FF00, 1),
        MajorTickLineLength = 8f,
        MinorTickLineStyle = new SCISolidPenStyle(0xFFC71585),
        MinorTickLineLength = 4f,
        MajorGridLineStyle = new SCISolidPenStyle(0xFF00FF00),
        MinorGridLineStyle = new SCISolidPenStyle(0xFF9400D3),
    };
    Surface.YAxis = new SCINumericAxis3D
    {
        MinorsPerMajor = 5,
        MaxAutoTicks = 7,
        TextSize = 13f,
        TextColor = 0xFFB22222,
        TextFont = font,
        AxisBandsStyle = new SCISolidBrushStyle(0xFFFF6347),
        MajorTickLineStyle = new SCISolidPenStyle(0xFFB22222),
        MajorTickLineLength = 8f,
        MinorTickLineStyle = new SCISolidPenStyle(0xFFCD5C5C),
        MinorTickLineLength = 4f,
        MajorGridLineStyle = new SCISolidPenStyle(0xFF006400),
        MinorGridLineStyle = new SCISolidPenStyle(0xFF8CBED6)
    };
    Surface.ZAxis = new SCINumericAxis3D
    {
        MinorsPerMajor = 5,
        MaxAutoTicks = 7,
        TextSize = 13f,
        TextColor = 0xFFDB7093,
        TextFont = font,
        AxisBandsStyle = new SCISolidBrushStyle(0xFFADFF2F),
        MajorTickLineStyle = new SCISolidPenStyle(0xFFDB7093),
        MajorTickLineLength = 8f,
        MinorTickLineStyle = new SCISolidPenStyle(0xFF7FFF00),
        MinorTickLineLength = 4f,
        MajorGridLineStyle = new SCISolidPenStyle(0xFFF5F5DC),
        MinorGridLineStyle = new SCISolidPenStyle(0xFFA52A2A)
    };
</div>

> **_NOTE:_** For more information on Styling, please refer to the [Styling and Theming](Styling and Theming.html) article.
