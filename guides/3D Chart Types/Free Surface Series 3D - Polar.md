# The Polar 3D Chart Type
In SciChart, **Polar 3D Charts** are provided by the combination of the [Free Surface 3D Series](free-surface-series-3d.html) and `SCIPolarDataSeries3D` underlying DataSeries.

The ***location*** of the `SCIPolarDataSeries3D` is defined by following properties:
- `ISCIFreeSurfaceDataSeries3DValues.offsetX` – a location of the Polar by the `X-Axis`;
- `ISCIFreeSurfaceDataSeries3DValues.offsetY` – a location of the Polar by the `Y-Axis`;
- `ISCIFreeSurfaceDataSeries3DValues.offsetZ` – a location of the Polar by the `Z-Axis`;

The ***size*** of the `SCICylindroidDataSeries3D` is defined by following properties:
- `SCIPolarDataSeries3D.a` – a distance from the origin to the ***internal*** edge of the cylindroid 3D Surface;
- `SCIPolarDataSeries3D.b` – a distance from the origin to the ***external*** edge of the cylindroid 3D Surface;

![Polar Chart 3D](img/chart-types-3d/free-surface-3d-polar.png)

> **_NOTE:_** Examples for the ***Polar Series 3D*** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-simple-polar/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-simple-polar/)

## Create a Polar 3D Chart
To create a **Polar 3D Chart**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    const int sizeU = 30;
    const int sizeV = 10;

    SCIPolarDataSeries3D *ds = [[SCIPolarDataSeries3D alloc] initWithXType:SCIDataType_Double heightType:SCIDataType_Double uSize:sizeU vSize:sizeV uMin:0.0 uMax:M_PI * 1.75];
    ds.a = @(1.0);
    ds.b = @(5.0);
    
    for (int u = 0; u < sizeU; ++u) {
        double weightU = 1.0 - ABS(2.0 * u / sizeU - 1.0);
        for (int v = 0; v < sizeV; ++v) {
            double weightV = 1.0 - ABS(2. * v / sizeV - 1.0);
            double offset = [SCDRandomUtil nextDouble];
            [ds setDisplacement:@(offset * weightU * weightV) uIndex:u vIndex:v];
        }
    }
    
    unsigned int colors[7] = { 0xFF00008B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000 };
    float stops[7] = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0};
    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7];
    
    SCIFreeSurfaceRenderableSeries3D *rs0 = [SCIFreeSurfaceRenderableSeries3D new];
    rs0.dataSeries = ds;
    rs0.drawMeshAs = SCIDrawMeshAs_SolidWireframe;
    rs0.stroke = 0x77228B22;
    rs0.contourInterval = 0.1;
    rs0.contourStroke = 0x77228B22;
    rs0.strokeThickness = 2.0;
    rs0.lightingFactor = 0.8;
    rs0.meshColorPalette = palette;
    
    rs0.paletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode_Relative;
    rs0.paletteMinimum = [[SCIVector3 alloc] initWithX:0.0 y:0.0 z:0.0];
    rs0.paletteMaximum = [[SCIVector3 alloc] initWithX:0.0 y:0.5 z:0.0];
</div>
<div class="code-snippet" id="swift">
    let SizeU: Int = 30
    let SizeV: Int = 10

    let meshDataSeries = SCIPolarDataSeries3D(xType: .double, heightType: .double, uSize: SizeU, vSize: SizeV, uMin: 0.0, uMax: Double.pi * 1.75)
    meshDataSeries.set(a: 1.0)
    meshDataSeries.set(b: 5.0)

    for u in 0 ..< SizeU {
        let weightU = 1.0 - abs(2.0 * Double(u) / Double(SizeU) - 1.0)
        for v in 0 ..< SizeV {
            let weightV = 1.0 - abs(2.0 * Double(v) / Double(SizeV) - 1.0)
            let offset =  SCDRandomUtil.nextDouble()
            meshDataSeries.setDisplacement(offset * weightU * weightV, atU: u, v: v)
        }
    }
    
    let colors: [UInt32] = [0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000]
    let stops: [Float] = [0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0]
    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7)
    
    let rs0 = SCIFreeSurfaceRenderableSeries3D()
    rs0.dataSeries = meshDataSeries
    rs0.drawMeshAs = .solidWireframe
    rs0.stroke = 0x77228B22
    rs0.contourInterval = 0.1
    rs0.contourStroke = 0x77228B22
    rs0.strokeThickness = 2.0
    rs0.lightingFactor = 0.8
    rs0.meshColorPalette = palette
    
    rs0.paletteMinMaxMode = .relative
    rs0.paletteMinimum = SCIVector3(x: 0.0, y: 0.0, z: 0.0)
    rs0.paletteMaximum = SCIVector3(x: 0.0, y: 0.5, z: 0.0)
</div>
<div class="code-snippet" id="cs">
    const int uSize = 30, vSize = 10;
    var dataSeries3D = new PolarDataSeries3D&lt;double, double&gt;(uSize, vSize, 0d, Math.PI * 1.75) { A = 1, B = 5 };

    var random = new Random();
    for (int u = 0; u < uSize; u++)
    {
        var weightU = 1d - Math.Abs(2d * u / uSize - 1d);
        for (int v = 0; v < vSize; v++)
        {
            var weightV = 1d - Math.Abs(2d * v / vSize - 1d);
            var offset = random.NextDouble();

            dataSeries3D.SetDisplacement(u, v, offset * weightU * weightV);
        }
    }

    var rSeries3D = new SCIFreeSurfaceRenderableSeries3D
    {
        DataSeries = dataSeries3D,
        DrawMeshAs = SCIDrawMeshAs.SolidWireframe,
        Stroke = 0x77228B22,
        ContourInterval = 0.1f,
        ContourStroke = 0x77228B22,
        StrokeThickness = 1f,
        MeshColorPalette = new SCIGradientColorPalette(
            new[] { ColorUtil.Sapphire, ColorUtil.Blue, ColorUtil.Cyan, ColorUtil.GreenYellow, ColorUtil.Yellow, ColorUtil.Red, ColorUtil.DarkRed },
            new[] { 0, .1f, .3f, .5f, .7f, .9f, 1}),
        PaletteMaximum = new SCIVector3(0, 0.5f, 0),
    };
</div>

> **_NOTE:_** See other [constrained](free-surface-series-3d.html#constrained-free-surface-3d-types) and [unconstrained](free-surface-series-3d.html#unconstrained-free-surface-3d-type) **Free Surface Series** types in the corresponding articles.