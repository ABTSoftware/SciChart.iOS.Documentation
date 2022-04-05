# The Waterfall 3D Chart Type
The **Waterfall Chart** renders a two-dimensional array as a series of slices.
In SciChart it's defined by the `SCIWaterfallRenderableSeries3D` class and provides a number of configurable chart types in SciChart 3D.
As an example, it can be used for the following:
- **dynamic** updating slices for **visualizing** spectra (Acoustic or radio frequency domain data)
- **volumetric** slices
- optional **PointMarkers** at data-points.
- and more...

![Waterfall 3D Series Type](img/chart-types-3d/waterfall-series-3d-example.png)

> **_NOTE:_** Examples of the ***Waterfall 3D*** Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-simple-waterfall/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-simple-waterfall/)

In the Surface Mesh 3D Series, the data is stored in the `SCIWaterfallDataSeries3D`.
This represents a ***2-dimensional grid***, typically of type `double`.

Some important points which is must to know while configuring the **Waterfall Series**:
- The **double values** which are stored in the `SCIWaterfallDataSeries3D` correspond to the **heights** on the chart [`Y-Axis`]. They are transformed into chart [World Coordinates](scichart-3d-basics---coordinates-in-3d-space.html#world-coordinates) via the `ISCIChartSurface3D.yAxis`.
- The `Z` and `X` **Data-Value** are defined by the [StartX](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)startX), [StepX](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)stepX), [StartZ](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)startZ) and [StepZ](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)stepZ) properties on `SCIUniformGridDataSeries3D`. These are transformed into [World Coordinates](scichart-3d-basics---coordinates-in-3d-space.html#world-coordinates) via the `ISCIChartSurface3D.xAxis` and `ISCIChartSurface3D.zAxis` respectively.
- The **Color** of each ***slice*** and ***outlines*** are define by the `SCIMeshColorPalette` via the following properties:
    - `SCIWaterfallRenderableSeries3D.selectedColorMapping`
    - `SCIWaterfallRenderableSeries3D.yColorMapping`
    - `SCIWaterfallRenderableSeries3D.zColorMapping`
    - `SCIWaterfallRenderableSeries3D.yStrokeColorMapping`
    - `SCIWaterfallRenderableSeries3D.zStrokeColorMapping`

Read on to learn more about [Applying Palettes to the Waterfall](#applying-palettes-to-the-waterfall)

## Create a Surface Mesh Series 3D
In order to create **Waterfall Series** - you will need to provide the `SCIWaterfallDataSeries3D` with `N x M` array of points, which is an array of slices.

See the code below, which shows how to create the above chart:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    const int PointsPerSlice = 128;
    const int SliceCount = 20;

    SCIWaterfallDataSeries3D *ds = [[SCIWaterfallDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double xSize:PointsPerSlice zSize:SliceCount];
    ds.startX = @(10.0);
    ds.startZ = @(1.0);

    [self fill:ds];

    unsigned int fillColors[5] = { 0xFFFF0000, 0xFFFFA500, 0xFFFFFF00, 0xFFADFF2F, 0xFF006400 };
    float fillStops[5] = { 0.0, 0.25, 0.5, 0.75, 1.0 };
    SCIGradientColorPalette *fillColorPalette = [[SCIGradientColorPalette alloc] initWithColors:fillColors stops:fillStops count:5];

    unsigned int strokeColors[4] = { 0xFFDC143C, 0xFFFF8C00, 0xFF32CD32, 0xFF32CD32 };
    float strokeStops[4] = { 0.0, 0.3, 0.67, 1.0 };
    SCIGradientColorPalette *strokeColorPalette = [[SCIGradientColorPalette alloc] initWithColors:strokeColors stops:strokeStops count:4];

    SCIWaterfallRenderableSeries3D *rs = [SCIWaterfallRenderableSeries3D new];
    rs.dataSeries = ds;
    rs.stroke = 0xFF0000FF;
    rs.strokeThickness = 1.0;
    rs.sliceThickness = 0.0;
    rs.yColorMapping = fillColorPalette;
    rs.yStrokeColorMapping = strokeColorPalette;
    rs.opacity = 0.8;
</div>
<div class="code-snippet" id="swift">
    let PointsPerSlice = 128;
    let SliceCount = 20;
    
    private let fillColorPalette = SCIGradientColorPalette(colors: [0xFFFF0000, 0xFFFFA500, 0xFFFFFF00, 0xFFADFF2F, 0xFF006400], stops: [0.0, 0.25, 0.5, 0.75, 1.0], count: 5)
    private let strokeColorPalette = SCIGradientColorPalette(colors: [0xFFDC143C, 0xFFFF8C00, 0xFF32CD32, 0xFF32CD32], stops: [0.0, 0.33, 0.67, 1.0], count: 4)

    let ds = SCIWaterfallDataSeries3D(xType: .double, yType: .double, zType: .double, xSize: PointsPerSlice, zSize: SliceCount)
    ds.set(startX: 10.0)
    ds.set(startZ: 1.0)
    
    fill(dataSeries: ds)
    
    let rs = SCIWaterfallRenderableSeries3D()
    rs.dataSeries = ds
    rs.stroke = 0xFF0000FF
    rs.strokeThickness = 1.0
    rs.sliceThickness = 0.0
    rs.yColorMapping = fillColorPalette
    rs.yStrokeColorMapping = strokeColorPalette
    rs.opacity = 0.8;
</div>
<div class="code-snippet" id="cs">
    var dataSeries3D = new WaterfallDataSeries3D&lt;double, double, double&gt;(pointsPerSlice, sliceCount) { StartX = 10d, StepX = 1d, StartZ = 1d };
    for (int i = 0; i < sliceCount; i++)
    {
        dataSeries3D.SetRowAt(i, data[i]);
    }

    var rSeries3D = new SCIWaterfallRenderableSeries3D
    {
        DataSeries = dataSeries3D,
        StrokeThickness = 1f,
        SliceThickness = 0f,
        YColorMapping = new SCIGradientColorPalette(
            new[] { ColorUtil.Red, ColorUtil.Orange, ColorUtil.Yellow, ColorUtil.GreenYellow, ColorUtil.DarkGreen },
            new[] { 0, .25f, .5f, .75f, 1 }),
        YStrokeColorMapping = new SCIGradientColorPalette(
            new[] { ColorUtil.Crimson, ColorUtil.DarkOrange, ColorUtil.LimeGreen, ColorUtil.LimeGreen },
            new[] { 0, 0.33f, 0.67f, 1 }),
        Opacity = 0.8f,
    };
</div>

#### Applying Palettes to the Waterfall
The ***Waterfall*** chart obeys Palette rules similar to that of the [3D SurfaceMesh Chart](surface-mesh-series-3d.html).
To learn more about the types of palette available and how to declare them - please see the [Applying Palettes](surface-mesh-series-3d.html#applying-palettes-to-the-3d-surface-meshes) section of the [3D SurfaceMesh Chart](surface-mesh-series-3d.html) article.

Palettes which may be applied to the `SCIWaterfallRenderableSeries3D` chart include:
- `SCISolidColorBrushPalette` - applies a ***solid color*** the waterfall slice or stroke.
- `SCIGradientColorPalette` - maps a ***linear gradient*** to the slice or stroke, where heights map to successive colors.

The properties which allow colouring the Waterfall **slices** and **outlines** are available for the `Z-Direction` and `Y-Direction`.
Those are mutually exclusive, and you should choose one direction at a time.

Read on to see some examples of applying Palettes to `SCIWaterfallRenderableSeries3D`.

##### Applying Solid Palettes to Waterfall Slices
To apply **Solid color** to the Waterfall Slices, please use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    rSeries.yColorMapping = [[SCISolidColorBrushPalette alloc] initWithColor:0xFF6495ED];
    rSeries.yStrokeColorMapping = [[SCISolidColorBrushPalette alloc] initWithColor:0xFF6495ED];
</div>
<div class="code-snippet" id="swift">
    rSeries.yColorMapping = SCISolidColorBrushPalette(color: 0xFF6495ED)
    rSeries.yStrokeColorMapping = SCISolidColorBrushPalette(color: 0xFF6495ED)
</div>
<div class="code-snippet" id="cs">
    rSeries.YColorMapping = new SCISolidColorBrushPalette(0xFF6495ED);
    rSeries.YStrokeColorMapping = new SCISolidColorBrushPalette(0xFF6495ED);
</div>

| **Solid Fill**         | **Solid Outline**                         |
| ------------------------- | ----------------------------------------- |
| ![Waterfall Solid Fill](img/chart-types-3d/waterfall-solid-fill.png) | ![Waterfall Solid Outline](img/chart-types-3d/waterfall-solid-outline.png) |

##### Applying Linear Gradient Palettes to Waterfall Slice Fill
To apply **Linear Gradient** to the Waterfall Slices, first prepare the SCIGradientColorPalette for the upcoming steps:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    unsigned int colors[5] = { 0xFFFF0000, 0xFFFFA500, 0xFFFFFF00, 0xFFADFF2F, 0xFF006400 };
    float stops[5] = { 0.0, 0.25, 0.5, 0.75, 1.0 };
    SCIGradientColorPalette *colorPalette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:5];
</div>
<div class="code-snippet" id="swift">
    let colorPalette = SCIGradientColorPalette(colors: [0xFFFF0000, 0xFFFFA500, 0xFFFFFF00, 0xFFADFF2F, 0xFF006400], stops: [0.0, 0.25, 0.5, 0.75, 1.0], count: 5)
</div>
<div class="code-snippet" id="cs">
    var colorPalette = new SCIGradientColorPalette(
        new[] { ColorUtil.Red, ColorUtil.Orange, ColorUtil.Yellow, ColorUtil.GreenYellow, ColorUtil.DarkGreen },
        new[] { 0, .25f, .5f, .75f, 1 });
</div>

From here, we can apply it to the Slice ***Fill***, ***Outline*** or ***Both***.

Applying a **Color Palette** onto Slice ***Fill*** in a `Y` or `Z` direction:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Z-Direction
    rSeries.zColorMapping = colorPalette;
    rSeries.zStrokeColorMapping = [[SCISolidColorBrushPalette alloc] initWithColor:0x00FFFFFF];

    // or Y-Direction
    rSeries.yColorMapping = colorPalette;
    rSeries.yStrokeColorMapping = [[SCISolidColorBrushPalette alloc] initWithColor:0x00FFFFFF];
</div>
<div class="code-snippet" id="swift">
    // Z-Direction
    rSeries.zColorMapping = colorPalette
    rSeries.zStrokeColorMapping = SCISolidColorBrushPalette(color: 0x00FFFFFF)

    // or Y-Direction
    rSeries.yColorMapping = colorPalette
    rSeries.yStrokeColorMapping = SCISolidColorBrushPalette(color: 0x00FFFFFF)
</div>
<div class="code-snippet" id="cs">
    // Z-Direction
    rSeries.ZColorMapping = colorPalette;
    rSeries.ZStrokeColorMapping = new SCISolidColorBrushPalette(0x00FFFFFF);

    // or Y-Direction
    rSeries.YColorMapping = colorPalette;
    rSeries.YStrokeColorMapping = new SCISolidColorBrushPalette(0x00FFFFFF);
</div>

| **Z-Direction Fill**      | **Y-Direction Fill**                      |
| ------------------------- | ----------------------------------------- |
| ![Waterfall Gradient Fill Z Direction](img/chart-types-3d/waterfall-gradient-fill-z-direction.png) | ![Waterfall Gradient Fill Y Direction](img/chart-types-3d/waterfall-gradient-fill-y-direction.png) |

Applying a **Color Palette** onto Slice ***Stroke*** in a `Y` or `Z` direction:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Z-Direction
    rSeries.zColorMapping = [[SCISolidColorBrushPalette alloc] initWithColor:0x00FFFFFF];
    rSeries.zStrokeColorMapping = colorPalette;

    // or Y-Direction
    rSeries.yColorMapping = [[SCISolidColorBrushPalette alloc] initWithColor:0x00FFFFFF];
    rSeries.yStrokeColorMapping = colorPalette;
</div>
<div class="code-snippet" id="swift">
    // Z-Direction
    rSeries.zColorMapping = SCISolidColorBrushPalette(color: 0x00FFFFFF)
    rSeries.zStrokeColorMapping = colorPalette

    // or Y-Direction
    rSeries.yColorMapping = SCISolidColorBrushPalette(color: 0x00FFFFFF)
    rSeries.yStrokeColorMapping = colorPalette
</div>
<div class="code-snippet" id="cs">
    // Z-Direction
    rSeries.ZColorMapping = new SCISolidColorBrushPalette(0x00FFFFFF);
    rSeries.ZStrokeColorMapping = colorPalette;

    // or Y-Direction
    rSeries.YColorMapping = new SCISolidColorBrushPalette(0x00FFFFFF);
    rSeries.YStrokeColorMapping = colorPalette;
</div>

| **Z-Direction Stroke**    | **Y-Direction Stroke**                    |
| ------------------------- | ----------------------------------------- |
| ![Waterfall Gradient Stroke Z Direction](img/chart-types-3d/waterfall-gradient-stroke-z-direction.png) | ![Waterfall Gradient Stroke Y Direction](img/chart-types-3d/waterfall-gradient-stroke-y-direction.png) |

#### Volumetric Waterfall 3D
A Waterfall Chart can be made volumetric by setting the property `SCIWaterfallRenderableSeries3D.sliceThickness` (default value = 0), e.g.:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIWaterfallRenderableSeries3D *rSeries = [SCIWaterfallRenderableSeries3D new];
    rSeries.sliceThickness = 10.0;
</div>
<div class="code-snippet" id="swift">
    let rSeries = SCIWaterfallRenderableSeries3D()
    rSeries.sliceThickness = 10.0

</div>
<div class="code-snippet" id="cs">
    var rSeries = new SCIWaterfallRenderableSeries3D();
    rSeries.SliceThickness = 10.0;
</div>

![Volumetric Waterfall 3D](img/chart-types-3d/waterfall-series-3d-volumetric-example.png)

#### PointMarkers on Waterfall 3D
A Waterfall Chart Slice data-points can be marked with a [Point Markers](pointmarker-3d-api.html).
That's achieved by providing the **Point Marker 3D** for the `SCIWaterfallRenderableSeries3D`.
See the code snipped below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCISpherePointMarker3D *pointMarker = [SCISpherePointMarker3D new];
    pointMarker.fillColor = 0xFFFFA500;
    pointMarker.size = 10.0;

    SCIWaterfallRenderableSeries3D *rSeries = [SCIWaterfallRenderableSeries3D new];
    rSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    let pointMarker = SCISpherePointMarker3D()
    pointMarker.fillColor = 0xFFFFA500;
    pointMarker.size = 10.0

    let rSeries = SCIWaterfallRenderableSeries3D()
    rSeries.pointMarker = pointMarker

</div>
<div class="code-snippet" id="cs">
    var rSeries = new SCIWaterfallRenderableSeries3D();
    rSeries.PointMarker = new SCISpherePointMarker3D { FillColor = 0xFFFFA500, Size = 10.0f };
</div>

![Waterfall 3D With PointMarkers](img/chart-types-3d/waterfall-series-3d-with-pointmarkers.png)

> **_NOTE:_** For more information - please refer to the [PointMarker API](pointmarker-3d-api.html) article.

## Real-time Waterfall 3D Example
In SciChart, it's possible to create real-time **Waterfall 3D Charts** which is shown below:

<video autoplay loop muted playsinline src="img/chart-types-3d/waterfall-series-3d-realtime-example.mp4"></video>

> **_NOTE:_** Examples of the ***Real-Time Waterfall 3D*** Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-realtime-waterfall/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-realtime-waterfall/)