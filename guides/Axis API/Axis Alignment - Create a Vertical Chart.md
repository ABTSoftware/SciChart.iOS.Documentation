# Axis Alignment - Create a Vertical Chart
It is possible to create **Vertical (Rotated)** Charts with SciChart. To achieve this, simply set `ISCIAxis.axisAlignment` to Left or Right for **X Axis** and Top or Bottom for **Y Axis** using the `SCIAxisAlignment` enum. And that's it - SciChart will do everything else for you.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create X axis and position it to the left
    id&lt;ISCIAxis&gt; xAxis = [SCINumericAxis new];
    xAxis.axisAlignment = SCIAxisAlignment_Left;

    // Create Y axis and position it to the top
    id&lt;ISCIAxis&gt; yAxis = [SCINumericAxis new];
    yAxis.axisAlignment = SCIAxisAlignment_Top;
</div>
<div class="code-snippet" id="swift">
    // Create X axis and position it to the left
    let xAxis = SCINumericAxis()
    xAxis.axisAlignment = .left

    // Create Y axis and position it to the top
    let yAxis = SCINumericAxis()
    yAxis.axisAlignment = .top
</div>
<div class="code-snippet" id="cs">
    // Create X axis and position it to the left
    var xAxis = new SCINumericAxis { AxisAlignment = SCIAxisAlignment.Left };

    // Create Y axis and position it to the top
    var yAxis = new SCINumericAxis { AxisAlignment = SCIAxisAlignment.Top };
</div>

<img src="img/axis-2d/vertical-chart.png" style="width: 50%; height: 50%"/>​

## Multiple axes support
Also, SciChart supports **unlimited, multiple X or Y axes** which can be aligned to the Right, Left, Top, Bottom sides of a chart. For more information - read [Adding an Axis](add-an-axis-to-a-scichartsurface.html#adding-an-axis) article. All that applies to [Vertical(Rotated)](#axis-alignment---create-a-vertical-chart) Charts as well, so any reasonable combination of differently aligned axes is allowed. This allows to create **mixed horizontal and vertical** charts:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; xAxis = [SCINumericAxis new];
    xAxis.axisId = @"xAxis";
    xAxis.axisTitle = @"Horizontal-X";
    
    id&lt;ISCIAxis&gt; xLeftAxis = [SCINumericAxis new];
    xLeftAxis.axisId = @"xLeftAxis";
    xLeftAxis.axisAlignment = SCIAxisAlignment_Left;
    xLeftAxis.axisTitle = @"Vertical-X";

    id&lt;ISCIAxis&gt; yAxis = [SCINumericAxis new];
    yAxis.axisId = @"yAxis";
    yAxis.axisTitle = @"Vertical-Y";
    
    id&lt;ISCIAxis&gt; yTopAxis = [SCINumericAxis new];
    yTopAxis.axisId = @"yTopAxis";
    yTopAxis.axisAlignment = SCIAxisAlignment_Top;
    yTopAxis.axisTitle = @"Horizontal-Y";
</div>
<div class="code-snippet" id="swift">
    let xAxis = SCINumericAxis()
    xAxis.axisId = "xAxis"
    xAxis.axisTitle = "Horizontal-X"
    
    let xLeftAxis = SCINumericAxis()
    xLeftAxis.axisId = "xLeftAxis"
    xLeftAxis.axisAlignment = .left
    xLeftAxis.axisTitle = "Vertical-X"

    let yAxis = SCINumericAxis()
    yAxis.axisId = "yAxis"
    yAxis.axisTitle = "Vertical-Y"
    
    let yTopAxis = SCINumericAxis()
    yTopAxis.axisId = "yTopAxis"
    yTopAxis.axisAlignment = .top
    yTopAxis.axisTitle = "Horizontal-Y"
</div>
<div class="code-snippet" id="cs">
    var xAxis = new SCINumericAxis();
    xAxis.AxisId = "xAxis";
    xAxis.AxisTitle = "Horizontal-X";

    var xLeftAxis = new SCINumericAxis();
    xLeftAxis.AxisId = "xLeftAxis";
    xLeftAxis.AxisAlignment = SCIAxisAlignment.Left;
    xLeftAxis.AxisTitle = "Vertical-X";

    var yAxis = new SCINumericAxis();
    yAxis.AxisId = "yAxis";
    yAxis.AxisTitle = "Vertical-Y";

    var yTopAxis = new SCINumericAxis();
    yTopAxis.AxisId = "yTopAxis";
    yTopAxis.AxisAlignment = SCIAxisAlignment.Top;
    yTopAxis.AxisTitle = "Horizontal-Y";
</div>

![Horizontal and Vertical Chart](img/axis-2d/horizontal-and-vertical-chart.png)

> **_NOTE:_** Every **RenderableSeries** (chart types e.g. `SCIFastLineRenderableSeries`, `SCIFastCandlestickRenderableSeries` etc.), every **[Annotation](Annotation API.html)** and some **Chart Modifiers** (e.g. `SCIPinchZoomModifier`, `SCIZoomPanModifier`) requires to be measured against **particular axis** (in other words - **attached** to it). You **must** specify the **Axis ID** for them via the `ISCIRenderableSeries.xAxisId` and `ISCIRenderableSeries.yAxisId` properties.
>
> However, If you have only a **single X and Y Axis** setting these ID properties **isn't required**. This is **required** only for the **multiple axis** cases.