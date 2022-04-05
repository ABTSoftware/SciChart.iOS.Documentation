# Axis Alignment - Setting AxisAlignment
SciChart supports **unlimited, multiple X or Y** axes which can be aligned to the **Right, Left, Top, Bottom** sides of a chart. Axis may be placed by setting the `ISCIAxisCore.axisAlignment` property. Please see the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; xTopAxis = [SCINumericAxis new];
    xTopAxis.axisId = TopAxisId;
    xTopAxis.axisAlignment = SCIAxisAlignment_Top;
    
    id&lt;ISCIAxis&gt; xBottomAxis = [SCINumericAxis new];
    xBottomAxis.axisId = BottomAxisId;
    xBottomAxis.axisAlignment = SCIAxisAlignment_Bottom;
    
    id&lt;ISCIAxis&gt; yLeftAxis = [SCINumericAxis new];
    yLeftAxis.axisId = LeftAxisId;
    yLeftAxis.axisAlignment = SCIAxisAlignment_Left;
    
    id&lt;ISCIAxis&gt; yRightAxis = [SCINumericAxis new];
    yRightAxis.axisId = RightAxisId;
    yRightAxis.axisAlignment = SCIAxisAlignment_Right;
</div>
<div class="code-snippet" id="swift">
    let xTopAxis = SCINumericAxis()
    xTopAxis.axisId = TopAxisId
    xTopAxis.axisAlignment = .top
    
    let xBottomAxis = SCINumericAxis()
    xBottomAxis.axisId = BottomAxisId
    xBottomAxis.axisAlignment = .bottom

    let yLeftAxis = SCINumericAxis()
    yLeftAxis.axisId = LeftAxisId
    yLeftAxis.axisAlignment = .left
    
    let yRightAxis = SCINumericAxis()
    yRightAxis.axisId = RightAxisId
    yRightAxis.axisAlignment = .right
</div>
<div class="code-snippet" id="cs">
    var xTopAxis = new SCINumericAxis
    {
        AxisId = "TopAxisId",
        AxisAlignment = SCIAxisAlignment.Top,
    };

    var xBottomAxis = new SCINumericAxis
    {
        AxisId = "BottomAxisId",
        AxisAlignment = SCIAxisAlignment.Bottom,
    };

    var yLeftAxis = new SCINumericAxis
    {
        AxisId = "LeftAxisId",
        AxisAlignment = SCIAxisAlignment.Left,
    };

    var yRightAxis = new SCINumericAxis
    {
        AxisId = "RightAxisId",
        AxisAlignment = SCIAxisAlignment.Right,
    };
</div>

The above code results in the following view. Also please see our [Multiple Axis Demo](https://www.scichart.com/example/ios-multiple-axis-demo/). 

![Axis Alignment](img/axis-2d/axis-alignment.png)

> **_NOTE:_** Every **RenderableSeries** (chart types e.g. `SCIFastLineRenderableSeries`, `SCIFastCandlestickRenderableSeries` etc.), every **[Annotation](Annotation API.html)** and some **Chart Modifiers** (e.g. `SCIPinchZoomModifier`, `SCIZoomPanModifier`) requires to be measured against **particular axis** (in other words - **attached** to it). You **must** specify the **Axis ID** for them via the `ISCIRenderableSeries.xAxisId` and `ISCIRenderableSeries.xAxisId` properties.
>
> However, If you have only a **single X and Y Axis** setting these ID properties **isn't required**. This is **required** only for the **multiple axis** cases.