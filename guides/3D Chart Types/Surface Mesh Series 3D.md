# The Surface Mesh 3D Chart Type
The ***surface mesh*** renders a two-dimensional array as a heightmap.
In SciChart it's defined by the `SCISurfaceMeshRenderableSeries3D` class and provides a number of configurable chart types in SciChart 3D, including:
- dynamic **real-time** Surfaces (terrains or height maps).
- **texturing** of surfaces or terrains or height maps.
- **Non-uniform** or **uniform** grid spacing.
- **Contour mapping** or **wireframe** on terrain or height maps.

![Surface Mesh 3D Series Type](img/chart-types-3d/uniform-surface-mesh-3d-example.png)

> **_NOTE:_** Examples of the ***Surface Mesh 3D*** Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-simple-uniform-mesh/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-simple-uniform-mesh/)

In the Surface Mesh 3D Series, the data is stored in the `SCIUniformGridDataSeries3D`.
This represents a ***2-dimensional grid***, typically of type `double`.

Some important points which is mandatory to understand while configuring the Surface Meshes:
- The **double values** which are stored in the `SCIUniformGridDataSeries3D` correspond to the **heights** on the chart (the `Y-Axis`). They are transformed into chart [World Coordinates](scichart-3d-basics---coordinates-in-3d-space.html#world-coordinates) via the `ISCIChartSurface3D.yAxis`.
- The `Z` and `X` **Data-Value** are defined by the [StartX](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)startX), [StepX](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)stepX), [StartZ](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)startZ) and [StepZ](Classes/SCIUniformGridDataSeries3D.html#/c:objc(cs)SCIUniformGridDataSeries3D(py)stepZ) properties on `SCIUniformGridDataSeries3D`. These are transformed into [World Coordinates](scichart-3d-basics---coordinates-in-3d-space.html#world-coordinates) via the `ISCIChartSurface3D.xAxis` and `ISCIChartSurface3D.zAxis` respectively.
- The **Colours** on the SurfaceMesh are defined by the `SCIMeshColorPalette`. More on this in the [following sections](#mesh-palette-modes).

The `SCISurfaceMeshRenderableSeries3D` is ***highly configurable***, so please read on the [Configuring Surface Mesh 3D Series](#configuring-surface-mesh-3d-series) section.

## Create a Surface Mesh Series 3D
In order to create **Uniform Surface Mesh 3D** - you will need to provide the `SCIUniformGridDataSeries3D` with `N x M` array of points.

The above graph is rendered with the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCINumericAxis3D *xAxis = [SCINumericAxis3D new];
    xAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];

    SCINumericAxis3D *yAxis = [SCINumericAxis3D new];
    yAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    yAxis.visibleRange = [[SCIDoubleRange alloc] initWithMin:0.0 max:0.3];
    
    SCINumericAxis3D *zAxis = [SCINumericAxis3D new];
    zAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    
    SCIUniformGridDataSeries3D *ds = [[SCIUniformGridDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double xSize:25 zSize:25];
    
    for (int x = 0; x < 25; ++x) {
        for (int z = 0; z < 25; ++z) {
            double xVal = 25 * x / 25;
            double zVal = 25 * z / 25;
            double yVal = sin(xVal * 0.2) / ((zVal + 1.0) * 2);
            [ds updateYValue:@(yVal) atXIndex:x zIndex:z];
        }
    }
    
    unsigned int colors[7] = { 0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000 };
    float stops[7] = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0};
    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7];
    
    SCISurfaceMeshRenderableSeries3D *rs0 = [SCISurfaceMeshRenderableSeries3D new];
    rs0.dataSeries = ds;
    rs0.drawMeshAs = SCIDrawMeshAs_SolidWireframe;
    rs0.stroke = 0x77228B22;
    rs0.strokeThickness = 2.0;
    rs0.drawSkirt = NO;
    rs0.meshColorPalette = palette;
    
    [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
        self.surface.xAxis = xAxis;
        self.surface.yAxis = yAxis;
        self.surface.zAxis = zAxis;
        [self.surface.renderableSeries add:rs0];
        [self.surface.chartModifiers add:ExampleViewBase.createDefault3DModifiers];
    }];
</div>
<div class="code-snippet" id="swift">
    let Size: Int = 25

    let xAxis = SCINumericAxis3D()
    xAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    
    let yAxis = SCINumericAxis3D()
    yAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    yAxis.visibleRange = SCIDoubleRange(min: 0, max: 0.3)
    
    let zAxis = SCINumericAxis3D()
    zAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    
    let ds = SCIUniformGridDataSeries3D(xType: .double, yType: .double, zType: .double, xSize: Size, zSize: Size)
    
    for x in 0 ..< Size {
        for z in 0 ..< Size {
            let xVal = Double(25 * x / Size)
            let zVal = Double(25 * z / Size)
            let y = sin(xVal * 0.2) / ((zVal + 1.0) * 2.0)
            ds.update(y: y, atX: x, z: z)
        }
    }
    
    let colors: [UInt32] = [0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000]
    let stops: [Float] = [0, 0.1, 0.3, 0.5, 0.7, 0.9, 1]
    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7)
    
    let rs = SCISurfaceMeshRenderableSeries3D()
    rs.dataSeries = ds
    rs.drawMeshAs = .solidWireframe
    rs.stroke = 0x77228B22
    rs.contourStroke = 0x77228B22
    rs.strokeThickness = 2.0
    rs.drawSkirt = false
    rs.meshColorPalette = palette
    
    SCIUpdateSuspender.usingWith(surface) {
        self.surface.xAxis = xAxis
        self.surface.yAxis = yAxis
        self.surface.zAxis = zAxis
        self.surface.renderableSeries.add(rs)
        self.surface.chartModifiers.add(ExampleViewBase.createDefault3DModifiers())
    }
</div>
<div class="code-snippet" id="cs">
    const int xSize = 25;
    const int zSize = 25;

    var dataSeries3D = new UniformGridDataSeries3D<double, double, double>(xSize, zSize);

    for (int x = 0; x < xSize; x++)
    {
        for (int z = 0; z < zSize; z++)
        {
            var xVal = 25.0 * x / xSize;
            var zVal = 25.0 * z / zSize;

            var y = Math.Sin(xVal * .2) / ((zVal + 1) * 2);
            dataSeries3D.UpdateYAt(x, z, y);
        }
    }

    var rSeries3D = new SCISurfaceMeshRenderableSeries3D
    {
        DataSeries = dataSeries3D,
        DrawMeshAs = SCIDrawMeshAs.SolidWireframe,
        Stroke = 0x77228B22,
        ContourStroke = 0x77228B22,
        StrokeThickness = 2f,
        DrawSkirt = false,
        MeshColorPalette = new SCIGradientColorPalette(
            new[] { ColorUtil.Sapphire, ColorUtil.Blue, ColorUtil.Cyan, ColorUtil.GreenYellow, ColorUtil.Yellow, ColorUtil.Red, ColorUtil.DarkRed },
            new[] { 0, .1f, .3f, .5f, .7f, .9f, 1 })
    };

    using (Surface.SuspendUpdates())
    {
        Surface.XAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.YAxis = new SCINumericAxis3D { VisibleRange = new SCIDoubleRange(0, .3) };
        Surface.ZAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.RenderableSeries.Add(rSeries3D);
        Surface.ChartModifiers.Add(CreateDefault3DModifiers());

        Surface.Camera = new SCICamera3D();
    }
</div>

## Configuring Surface Mesh 3D Series
There are several properties which affect rendering of the `SCISurfaceMeshRenderableSeries3D` and those are listed below:

| **Property**                                            | **Description**                                             |
| ------------------------------------------------------- | ----------------------------------------------------------- |
| `SCIContourMeshRenderableSeries3DBase.highlight` | Changes the lighting algorithm to make cells appear ***lighter***. |
| `SCIContourMeshRenderableSeries3DBase.meshColorPalette` | defines the `SCIMeshColorPalette` which is used to calculate color from data value. See the [Applying Palettes to the 3D Surface Meshes](#applying-palettes-to-the-3d-surface-meshes) section for more information. |
| `SCIContourMeshRenderableSeries3DBase.meshPaletteMode`  | Changes how the ***heightmap*** is applied. See the [Applying Palettes to the 3D Surface Meshes](#applying-palettes-to-the-3d-surface-meshes) section for more information. |
| `SCISurfaceMeshRenderableSeries3D.heightScaleFactor`    | Applies a constant ***scaling factor*** to the heights, e.g. setting to 0 will make the surface mesh flat. |
| `SCISurfaceMeshRenderableSeries3D.yOffset`              | Applies a constant ***offset heights***, e.g. setting to 1 will move the SurfaceMesh `1-Data-Value` in the `Y-direction`. |

The effect of these properties are demonstrated in the images below.

| **Highlight = 0**         | **Highlight = 1**                         |
| ------------------------- | ----------------------------------------- |
| ![Surface Mesh Highlight = 0](img/chart-types-3d/surface-mesh-3d-highlight-0.png) | ![Surface Mesh Highlight = 1](img/chart-types-3d/surface-mesh-3d-highlight-1.png) |
| **HeightScaleFactor = 0** | **meshPaletteMode = HeightMapSolidCells** |
| ![Surface Mesh HeightScaleFactor = 0](img/chart-types-3d/surface-mesh-3d-heightScaleFactor-0.png) | ![Surface Mesh MeshPaletteMode = HeightMapSolidCells](img/chart-types-3d/surface-mesh-3d-solid-cells.png) |

#### Applying Palettes to the 3D Surface Meshes
The `SCISurfaceMeshRenderableSeries3D` accepts color palettes via the `SCIContourMeshRenderableSeries3DBase.meshColorPalette` property.
There are several palettes available out of the box, including the following:
- [SCISolidColorBrushPalette](#solid-color-palette) - applies a solid color to all cells in the mesh
- [SCIGradientColorPalette](#gradient-color-palette) - maps a linear gradient to the mesh.
- [SCIBrushColorPalette](#brush-color-palette) - maps a `SCIBrushStyle` to the mesh. For example, this can be used to map an image via the `SCITextureBrushStyle` type.
- [Custom Palette](#create-a-custom-palette) - maps a custom Texture to the mesh.

Read on to learn more about each of these options.

In addition, rendering all of the above palettes can be affected by the `SCIMeshPaletteMode`.
See the [Mesh Palette Modes](#mesh-palette-modes) section below for more information. 

##### Mesh Palette Modes
The `SCIContourMeshRenderableSeries3DBase.meshPaletteMode` property changes how the palette is applied to the Mesh.
Possible `SCIMeshPaletteMode` are listed in the table below:

| **Mode**                | **Description**                                                                                                    |
| ----------------------- | ------------------------------------------------------------------------------------------------------------------ |
| `heightMapInterpolated` | the palette is applied in the `Y-direction` (vertically).                                                          |
| `heightMapSolidCells`   | same as `heightMapInterpolated` but no ***interpolation***. Use this if you want each cell have a separate colour. |
| `textured`              | palette is applied to the mesh in the **[XZ plane](axis-3d-labels---labels-configuration.html#axis-cube-planes)**. Imagine the palette is stretched over the mesh itself. |
| `texturedSolidCells`    | same as `textured` but no ***interpolation***. Use this if you want each cell have a separate colour.              |

##### Solid Color Palette
The Solid palette if provided by the `SCISolidColorBrushPalette` class. It's quite simple, let's dig into declaration:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCISurfaceMeshRenderableSeries3D *rSeries = [SCISurfaceMeshRenderableSeries3D new];
    rSeries.meshColorPalette = [[SCISolidColorBrushPalette alloc] initWithColor:0xFF006400];
</div>
<div class="code-snippet" id="swift">
    let rSeries = SCISurfaceMeshRenderableSeries3D()
    rSeries.meshColorPalette = SCISolidColorBrushPalette(color: 0xFF006400)
</div>
<div class="code-snippet" id="cs">
    var rSeries = new SCISurfaceMeshRenderableSeries3D();
    rSeries.MeshColorPalette = new SCISolidColorBrushPalette(0xFF006400);
</div>

![Surface Mesh Solid Palette](img/chart-types-3d/surface-mesh-3d-solid-palette.png)

##### Gradient Color Palette
The `SCIGradientColorPalette` can be used to map a ***Gradient Brush*** set of colors to heights in the **Surface Mesh**.
The mapping is similar to that performed by the [Heatmap Series](2d-chart-types---uniform-heatmap-series.html) in 2D Charts.

Given the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    unsigned int colors[7] = { 0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000 };
    float stops[7] = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0};

    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7];
</div>
<div class="code-snippet" id="swift">
    let colors: [UInt32] = [0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000]
    let stops: [Float] = [0, 0.1, 0.3, 0.5, 0.7, 0.9, 1]

    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7)
</div>
<div class="code-snippet" id="cs">
    var colors = new[] { ColorUtil.Sapphire, ColorUtil.Blue, ColorUtil.Cyan, ColorUtil.GreenYellow, ColorUtil.Yellow, ColorUtil.Red, ColorUtil.DarkRed };
    var stops = new[] { 0, .1f, .3f, .5f, .7f, .9f, 1 };

    var palette = new SCIGradientColorPalette(colors, stops);
</div>

Colors are mapped onto `Y-values` as follows:
- `SCISurfaceMeshRenderableSeries3D.minimum` are drawn with the gradient stop color at offset 0.
- `SCISurfaceMeshRenderableSeries3D.maximum` are drawn with the gradient stop color at offset 1.
- All other values are linearly interpolated (including `Y-Values` outside of ***minimum*** and ***maximum*** values).

It's also possible to specify whether gradient is ***stepped*** or not. See code which creates ***stepped*** palette below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7 isStepped:YES];
</div>
<div class="code-snippet" id="swift">
    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7, isStepped: true)
</div>
<div class="code-snippet" id="cs">
    var palette = new SCIGradientColorPalette(colors, stops, true);
</div>

And the difference is showed below:

| **Linear Gradient**       | **Stepped Gradient (isStepped = YES)**  |
| ------------------------- | --------------------------------------- |
| ![Surface Mesh Gradient Palette](img/chart-types-3d/surface-mesh-3d-gradient-palette.png) | ![Surface Mesh Stepped Gradient Palette](img/chart-types-3d/surface-mesh-3d-stepped-gradient-palette.png)

##### Brush Color Palette
A texture can be applied to the SurfaceMesh and mapped over it in the [XZ plane](axis-3d-labels---labels-configuration.html#axis-cube-planes) plane by using a ***combination*** of the following:
- `SCIBrushColorPalette`
- `SCIMeshPaletteMode.SCIMeshPaletteMode_Textured`
- `SCIContourMeshRenderableSeries3DBase.meshColorPaletteSize`

See the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIBitmap *bitmap = [UIImage imageNamed:@"image.weather.storm"].sciBitmap;
    SCITextureBrushStyle *brushStyle = [[SCITextureBrushStyle alloc] initWithTexture:bitmap];
    SCIBrushColorPalette *palette = [[SCIBrushColorPalette alloc] initWithBrushStyle:brushStyle];

    rSeries.meshColorPalette = palette;
    rSeries.meshPaletteMode = SCIMeshPaletteMode_Textured;
    rSeries.meshColorPaletteSize = CGSizeMake(bitmap.width, bitmap.height);
</div>
<div class="code-snippet" id="swift">
    let bitmap = #imageLiteral(resourceName: "image.weather.storm").sciBitmap()
    let brushStyle = SCITextureBrushStyle(texture: bitmap)
    let palette = SCIBrushColorPalette(brushStyle: brushStyle)
    
    rSeries.meshColorPalette = palette
    rSeries.meshPaletteMode = .textured
    rSeries.meshColorPaletteSize = CGSize(width: CGFloat(bitmap.width), height: CGFloat(bitmap.height))
</div>
<div class="code-snippet" id="cs">
    var bitmap = new UIImage("image.weather.storm").SciBitmap();
    var brushStyle = new SCITextureBrushStyle(bitmap);
    var palette = new SCIBrushColorPalette(brushStyle);

    rSeries.MeshColorPalette = palette;
    rSeries.MeshPaletteMode = SCIMeshPaletteMode.Textured;
    rSeries.MeshColorPaletteSize = new CGSize(bitmap.Width, bitmap.Height);
</div>

![Surface Mesh Textured Palette](img/chart-types-3d/surface-mesh-3d-textured-palette.png)

##### Create a Custom Palette
In addition to all of the above, you can create your own ***custom*** Color Palette by inheriting `SCIMeshColorPalette` and overriding the `-[SCIMeshColorPalette getTextureWithWidth:height:]` method. 

For example, see the following code snippet:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomPalette : SCIMeshColorPalette
    @end

    @implementation CustomPalette

    - (id&lt;ISCITexture2D&gt;)getTextureWithWidth:(int)width height:(int)height {
        SCIBitmap *bitmap = [[SCIBitmap alloc] initWithSize:CGSizeMake(width, height)];
        
        // TODO: Fill Bitmap
        return [bitmap createTexture];
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class CustomPalette: SCIMeshColorPalette {
        override func getTextureWithWidth(_ width: Int32, height: Int32) -> ISCITexture2D! {
            let bitmap = SCIBitmap(size: CGSize(width: CGFloat(width), height: CGFloat(height)))
            
            // TODO: FIll bitmap
            return bitmap.createTexture()
        }
    }
</div>
<div class="code-snippet" id="cs">
    class CustomPalette : SCIMeshColorPalette
    {
        public override IISCITexture2D GetTexture(int width, int height)
        {
            var bitmap = new SCIBitmap(new CGSize(width, height));

            // TODO: FIll bitmap
            return bitmap.CreateTexture();
        }
    }
</div>

The palette is applied to a `SCISurfaceMeshRenderableSeries3D` as in the above examples.

> **_NOTE:_** You might noticed helped extension, which allows to create `ISCITexture2D` directly from your `SCIBitmap` - `-[SCIBitmap createTexture]`

#### Surface Mesh 3D Wireframe and Contours
In SciChart, the **3D Surface Mesh** can be configured to draw with ***Contours*** and/or ***Wireframe***.
That is added optionally ans configured via the `SCIContourMeshRenderableSeries3DBase.drawMeshAs` property.
See the possible options in the table below:

| **Option**                   | **Surface** | **Wireframe** | **Contours** |
| ---------------------------- | ----------- | ------------- | ------------ |
| `SolidMesh`                  | +           |               |              |
| `Wireframe`                  |             | +             |              |
| `Contours`                   |             |               | +            |
| `SolidWireframe`             | +           | +             |              |
| `SolidWithContours`          | +           |               | +            |
| `SolidWireframeWithContours` | +           | +             | +            |
 
Wireframe and Contours can be configured via the following properties:
- `SCIContourMeshRenderableSeries3DBase.contourStrokeThickness` - defines the **thickness** of the **contour line**.
- `SCIContourMeshRenderableSeries3DBase.contourStroke` - defines the **stroke color** of the **contours**, which may optionally include opacity.
- `SCIContourMeshRenderableSeries3DBase.contourOffset` - defines the **offset** of contours from `Y-values`, defaults to 0.
- `SCIContourMeshRenderableSeries3DBase.contourInterval` - defines the `Y-value` intervals between contours.
- `SCIContourMeshRenderableSeries3DBase.strokeThickness` - defines the **thickness** of the **wireframe line**.
- `SCIContourMeshRenderableSeries3DBase.stroke` - defines the **stroke color** of the **wireframe**, which may optionally include opacity.

Some examples are shown below:

| **Bare Wireframe**             | **Solid Surface With Contours**    |
| ------------------------- | --------------------------------------- |
| ![Surface Mesh Gradient Palette](img/chart-types-3d/surface-mesh-3d-wireframe.png) | ![Surface Mesh Stepped Gradient Palette](img/chart-types-3d/surface-mesh-3d-solid-with-contours.png)

#### Overriding Surface Mesh 3D Specific Cell Colors
In addition to the [custom palettes](#applying-palettes-to-the-3d-surface-meshes), [heightmaps](#gradient-color-palette), [textures](#brush-color-palette) - you can **override a specific cell** or cells in the `SCISurfaceMeshRenderableSeries3D` by using the [MetadataProvider API](metadataprovider-3d-api.html).

For example it can be used for one of the following:
- **remove specific cells** or mark them as NULL by overriding the cell color to be **Transparent**.
- **mark regions** of interest, say certain **cells** in a value range, or with index must be **colored differently**.
- with higher resolution meshes, you can **change the shape** of the mesh to **circular** (approx) by removing cells outside of a region.

It is showcased in our **Surface mesh 3D with Metadata provider** example. Let's see a code snippet from it, which shows how to implement the custom [MetadataProvider](metadataprovider-3d-api.html):

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface SurfaceMeshMetaDataProvider3D : SCIMetadataProvider3DBase&lt;SCISurfaceMeshRenderableSeries3D *&gt;&lt;ISCISurfaceMeshMetadataProvider3D&gt;
    @end

    @implementation SurfaceMeshMetaDataProvider3D

    - (instancetype)init {
        return [super initWithSeriesType:SCISurfaceMeshRenderableSeries3D.class];
    }

    - (void)updateMeshColors:(SCIUnsignedIntegerValues *)cellColors {
        SCISurfaceMeshRenderPassData3D *currentRenderPassData = (SCISurfaceMeshRenderPassData3D *)self.renderableSeries.currentRenderPassData;
        
        NSInteger countX = currentRenderPassData.countX - 1;
        NSInteger countZ = currentRenderPassData.countZ - 1;
        
        cellColors.count = currentRenderPassData.pointsCount;
        
        unsigned int *items = cellColors.itemsArray;
        for (int x = 0; x < countX; ++x) {
            for (int z = 0; z < countZ; ++z) {
                NSInteger index = x * countZ + z;
                unsigned int color;
                if  ((x >= 20 && x <= 26 && z > 0 && z < 47) || (z >= 20 && z <= 26 && x > 0 && x < 47)) {
                    color = 0x00FFFFFF;
                } else {
                    color = SCDDataManager.randomColor;
                }
                items[index] = color;
            }
        }
    }

    @end
</div>
<div class="code-snippet" id="swift">
    private class SurfaceMeshMetaDataProvider3D: SCIMetadataProvider3DBase<SCISurfaceMeshRenderableSeries3D>, ISCISurfaceMeshMetadataProvider3D {
        
        init() {
            super.init(seriesType: SCISurfaceMeshRenderableSeries3D.self)
        }
        
        override init(seriesType: AnyClass) {
            super.init(seriesType: seriesType)
        }
        
        func updateMeshColors(_ cellColors: SCIUnsignedIntegerValues!) {
            guard let currentRenderPassData = self.renderableSeries.currentRenderPassData as? SCISurfaceMeshRenderPassData3D else {
                return
            }
            
            let countX = currentRenderPassData.countX - 1
            let countZ = currentRenderPassData.countZ - 1
            
            cellColors.count = currentRenderPassData.pointsCount
            for x in 0 ..< countX {
                for z in 0 ..< countX {
                    let index = x * countZ + z;
                    var color: UInt32 = 0;
                    if  ((x >= 20 && x <= 26 && z > 0 && z < 47) || (z >= 20 && z <= 26 && x > 0 && x < 47)) {
                        color = 0x00FFFFFF;
                    } else {
                        color = SCDDataManager.randomColor()
                    }
                    
                    cellColors.set(color, at: index)
                }
            }
        }
    }
</div>
<div class="code-snippet" id="cs">
    class SurfaceMeshMetadataProvider3D : SCIMetadataProvider3DBase&lt;SCISurfaceMeshRenderableSeries3D&gt;, IISCISurfaceMeshMetadataProvider3D
    {
        public void UpdateMeshColors(SCIUnsignedIntegerValues cellColors)
        {
            var currentRenderPassData = Runtime.GetNSObject&lt;SCISurfaceMeshRenderPassData3D&gt;(RenderableSeries.CurrentRenderPassData.Handle);
            var dataManager = DataManager.Instance;

            var countX = currentRenderPassData.CountX - 1;
            var countZ = currentRenderPassData.CountZ - 1;
            cellColors.Count = currentRenderPassData.PointsCount;

            for (int x = 0; x < countX; x++)
            {
                for (int z = 0; z < countX; z++)
                {
                    int index = x * countZ + z;

                    uint color = ((x >= 20 && x <= 26 && z > 0 && z < 47) || (z >= 20 && z <= 26 && x > 0 && x < 47))
                        ? ColorUtil.Red
                        : (uint)dataManager.GetRandomColor().ToArgb();
                    
                    cellColors.Set(color, index);
                }
            }
        }
    }
</div>

> **_NOTE:_** For more information about custom **MetadataProviders** - please refer to the [MetadataProvider API](metadataprovider-3d-api.html) article.

![MetadataProvider API](img/chart-types-3d/surface-mesh-3d-gaps.png)

> **_NOTE:_** Examples of using ***MetadataProvider API*** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-surface-mesh-palette-provider/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-surface-mesh-palette-provider/)