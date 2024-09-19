# Axis Thickness - Setting axisThickness
The `ISCIAxisCore.axisThickness` property is used to customize **the thickness** of the chart's axes. The visual representation of charts can be enhanced by controlling the width or height of the axes, depending on their orientation.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; xTopAxis = [SCINumericAxis new];
    xTopAxis.axisId = TopAxisId;
    xTopAxis.axisThickness = 50;
    
    id&lt;ISCIAxis&gt; xBottomAxis = [SCINumericAxis new];
    xBottomAxis.axisId = BottomAxisId;
    xBottomAxis.axisThickness = 50;
    
    id&lt;ISCIAxis&gt; yLeftAxis = [SCINumericAxis new];
    yLeftAxis.axisId = LeftAxisId;
    yLeftAxis.axisThickness = 50;
    
    id&lt;ISCIAxis&gt; yRightAxis = [SCINumericAxis new];
    yRightAxis.axisId = RightAxisId;
    yRightAxis.axisThickness = 50;
</div>
<div class="code-snippet" id="swift">
    let xTopAxis = SCINumericAxis()
    xTopAxis.axisId = TopAxisId
    xTopAxis.axisThickness = 50
    
    let xBottomAxis = SCINumericAxis()
    xBottomAxis.axisId = BottomAxisId
    xBottomAxis.axisThickness = 50

    let yLeftAxis = SCINumericAxis()
    yLeftAxis.axisId = LeftAxisId
    yLeftAxis.axisThickness = 50
    
    let yRightAxis = SCINumericAxis()
    yRightAxis.axisId = RightAxisId
    yRightAxis.axisThickness = 50
</div>

The above code results in the following view. 

![Axis Alignment](img/axis-2d/axis-thickness.png)

> **_NOTE:_** Every **RenderableSeries** (chart types e.g. `SCIFastLineRenderableSeries`, `SCIFastCandlestickRenderableSeries` etc.), every **[Annotation](Annotations APIs.html)** and some **Chart Modifiers** (e.g. `SCIPinchZoomModifier`, `SCIZoomPanModifier`) requires to be measured against **particular axis** (in other words - **attached** to it). You **must** specify the **Axis ID** for them via the `ISCIRenderableSeries.xAxisId` and `ISCIRenderableSeries.xAxisId` properties.
>
> However, If you have only a **single X and Y Axis** setting these ID properties **isn't required**. This is **required** only for the **multiple axis** cases.
