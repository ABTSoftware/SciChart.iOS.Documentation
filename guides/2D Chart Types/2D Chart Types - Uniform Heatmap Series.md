# The Uniform Heatmap Series Type
Heatmaps are provided by the `SCIFastUniformHeatmapRenderableSeries`, which consumes data from a `SCIUniformHeatmapDataSeries`. 
This is designed to display **2D array of data** with real values. Every item in the 2D array is represented as a colored rectangle - **cell**. The color depends on corresponding item’s `Z-Value`.

> **_NOTE:_** For more info about `SCIUniformHeatmapDataSeries`, as well as other DataSeries types in SciChart, see the [DataSeries API](Data Series APIs.html) article.

![OHLC Series Type](img/chart-types-2d/heatmap-chart-example.png)

> **_NOTE:_** Examples for the Uniform Heatmap Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-heatmap-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-heatmap-chart-example/)

Uniform heatmaps are **extremely fast**, **lightweight** series types for rendering two dimensional data as a heatmap or spectrogram. As mentioned above - the `SCIFastUniformHeatmapRenderableSeries` type should be used in conjunction with a `SCIUniformHeatmapDataSeries`, when you simply want to specify a *Step* in the `X, Y` direction (each cell is the same size).

All `Z-Values` should fall into the range determined by the **Minimum, Maximum** values (they may go beyond it though), which are available via the following properties:
- `SCIFastUniformHeatmapRenderableSeries.minimum`;
- `SCIFastUniformHeatmapRenderableSeries.maximum`.

Also, **Uniform Heatmap Series** requires a `SCIColorMap` to be set. The **ColorMap** determines how a gradient color transition occurs and can be applied via the `SCIFastUniformHeatmapRenderableSeries.colorMap` property.

## Add a Legend onto a Heatmap Chart
There is a special view designed to be used in tandem with `SCIFastUniformHeatmapRenderableSeries` called `SCIChartHeatmapColourMap`. It is not required by a Heatmap and is **fully optional**. It can be placed anywhere on the layout.

Similarly to the `SCIFastUniformHeatmapRenderableSeries`, the `SCIChartHeatmapColourMap` type allows to set the **Minimum, Maximum** values and apply a **ColorMap**. All these can be assigned via the corresponding properties:
- `SCIChartHeatmapColourMap.minimum`;
- `SCIChartHeatmapColourMap.maximum`;
- `SCIChartHeatmapColourMap.colourMap`.

The numeric labels can be formatted by apply **TextFormatting** to them via the `SCIChartHeatmapColourMap.textFormat` property.

Finally, there is the `SCIChartHeatmapColourMap.orientation` property to specify whether the legend appears Horizontally or Vertically.

## Create a Heatmap Series
To create a **Uniform Heatmap Chart**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries for HeatMap and fill it with some data
    SCIUniformHeatmapDataSeries *dataSeries = [[SCIUniformHeatmapDataSeries alloc] initWithXType:SCIDataType_Int yType:SCIDataType_Int zType:SCIDataType_Double xSize:WIDTH ySize:HEIGHT];

    // Prepare colors for ColorMap
    NSArray<UIColor *> *colors = @[[UIColor fromARGBColorCode:0xFF00008B], [UIColor fromARGBColorCode:0xFF6495ED], [UIColor fromARGBColorCode:0xFF006400], [UIColor fromARGBColorCode:0xFF7FFF00], UIColor.yellowColor, UIColor.redColor];
    
    // Create Uniform Heatmap Series
    SCIFastUniformHeatmapRenderableSeries *heatmapRenderableSeries = [SCIFastUniformHeatmapRenderableSeries new];
    heatmapRenderableSeries.dataSeries = dataSeries;
    heatmapRenderableSeries.minimum = 0;
    heatmapRenderableSeries.maximum = 200;
    // Create and assign ColorMap
    heatmapRenderableSeries.colorMap = [[SCIColorMap alloc] initWithColors:colors andStops:@[@0.0, @0.2, @0.4, @0.6, @0.8, @1.0]];
    
    // Assume a SCIChartHeatmapColourMap has been created via the Storyboard
    SCIChartHeatmapColourMap *heatmapColourMap;
    heatmapColourMap.minimum = heatmapRenderableSeries.minimum;
    heatmapColourMap.maximum = heatmapRenderableSeries.maximum;
    heatmapColourMap.colourMap = heatmapRenderableSeries.colorMap;

    [surface.renderableSeries add:heatmapRenderableSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries for HeatMap and fill it with some data
    let dataSeries = SCIUniformHeatmapDataSeries(xType: .int, yType: .int, zType: .double, xSize: width, ySize: height)

    // Prepare colors for ColorMap
    let colors = [UIColor.fromARGBColorCode(0xFF00008B)!, UIColor.fromARGBColorCode(0xFF6495ED)!, UIColor.fromARGBColorCode(0xFF006400)!, UIColor.fromARGBColorCode(0xFF7FFF00)!, UIColor.yellow, UIColor.red]

    // Create Uniform Heatmap Series
    let heatmapRenderableSeries = SCIFastUniformHeatmapRenderableSeries()
    heatmapRenderableSeries.dataSeries = _dataSeries
    heatmapRenderableSeries.minimum = 0.0
    heatmapRenderableSeries.maximum = 200.0
    // Create and assign ColorMap
    heatmapRenderableSeries.colorMap = SCIColorMap(colors: colors, andStops: [0.0, 0.2, 0.4, 0.6, 0.8, 1.0])

    // Assume a SCIChartHeatmapColourMap has been created via the Storyboard
    let heatmapColourMap: SCIChartHeatmapColourMap
    heatmapColourMap.minimum = heatmapRenderableSeries.minimum
    heatmapColourMap.maximum = heatmapRenderableSeries.maximum
    heatmapColourMap.colourMap = heatmapRenderableSeries.colorMap

    surface.renderableSeries.add(heatmapRenderableSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries for HeatMap and fill it with some data
    var dataSeries = new UniformHeatmapDataSeries&lt;int, int, double&gt;(Width, Height);
    
    // Create Uniform Heatmap Series
    var heatmapRenderableSeries = new SCIFastUniformHeatmapRenderableSeries
    {
        // Create and assign ColorMap
        DataSeries = dataSeries,
        Minimum = 0,
        Maximum = 200,
        ColorMap = new SCIColorMap(
            new[] { ColorUtil.DarkBlue.ToUIColor(), ColorUtil.CornflowerBlue.ToUIColor(), ColorUtil.DarkGreen.ToUIColor(), ColorUtil.Chartreuse.ToUIColor(), ColorUtil.Yellow.ToUIColor(), ColorUtil.Red.ToUIColor() },
            new[] { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 })
    };
    
    // Assume a SCIChartHeatmapColourMap has been created via the Storyboard
    SCIChartHeatmapColourMap heatmapColourMap;
    heatmapColourMap.Minimum = heatmapRenderableSeries.Minimum;
    heatmapColourMap.Maximum = heatmapRenderableSeries.Maximum;
    heatmapColourMap.ColourMap = heatmapRenderableSeries.ColorMap;

    surface.RenderableSeries.Add(heatmapRenderableSeries);
</div>

The code above creates a **Heatmap with a ColorMap** that has gradient transitions between five colors and a `SCIChartHeatmapColourMap` **Legend** to it.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done by passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Updating Data in the Heatmap
The `SCIUniformHeatmapDataSeries` does not support **Append, Insert, Update, Remove** like the other DataSeries do. You can, however, update the data by simply updating the array passed in. There are a bunch of methods provided to update Heatmap data:
- `-[ISCIUniformHeatmapDataSeries updateZ:atX:y:]` - updates Z Value at specified xIndex and yIndex;
- `-[ISCIUniformHeatmapDataSeries updateZArray:]` - updates all Z values for this series;
- `-[ISCIUniformHeatmapDataSeries updateZArray:atX:y:]` - updates the range of Z values for this series;
- `-[ISCIUniformHeatmapDataSeries updateZValues:]` - updates all Z values for this series;
- `-[ISCIUniformHeatmapDataSeries updateZValues:atX:y:]` - updates the range of Z values for this series.

> **_NOTE:_** It is highly recommended to update `SCIUniformHeatmapDataSeries` using our `ISCIValues` which allows to omit the boxing/unboxing of values, to make sure you get the most out of performance during your updates.
