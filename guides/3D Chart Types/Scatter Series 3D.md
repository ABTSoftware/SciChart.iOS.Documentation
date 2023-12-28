# The Scatter 3D Chart Type
3D Scatter Charts can be created using the `SCIScatterRenderableSeries3D` type.

![Scatter Chart 3D](img/chart-types-3d/scatter-chart-3d-example.png)

> **_NOTE:_** Examples for the ***Scatter Series 3D*** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-simple-scatter-3d-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-3d-chart-example-simple-scatter/)

The Scatter Series 3D requires a shape to be specified using the **Point Markers 3D**. SciChart provides several shapes out of the box:

| **Flat-Texture PointMarkers** | **Mesh (Volumetric) PointMarkers** |
| ----------------------------- | ---------------------------------- |
| `SCITrianglePointMarker3D`    | `SCIPyramidPointMarker3D`          |
| `SCIQuadPointMarker3D`        | `SCICubePointMarker3D`             |
| `SCIEllipsePointMarker3D`     | `SCISpherePointMarker3D`           |
| `SCIPixelPointMarker3D`       | `SCICylinderPointMarker3D`         |
| `SCICustomPointMarker3D`      |

It is also possible to [define custom texture](pointmarker-3d-api.html#custom-pointmarkers-3d) for the Point Markers via the `SCICustomPointMarker3D`.
Please refer to the [PointMarkers 3D API](pointmarker-3d-api.html) article to learn more.
You can also [override colors](#paint-scatters-with-different-colors) of the **Point Markers 3D** individually using [The MetadataProvider API](metadataprovider-3d-api.html).

## Create a 3D Scatter Series
To create a `SCIScatterRenderableSeries3D`, use the following code:

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
    SCINumericAxis3D *zAxis = [SCINumericAxis3D new];
    zAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    
    SCIXyzDataSeries3D *ds = [[SCIXyzDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double];
    for (int i = 0; i < 250; ++i) {
        double x = [SCDDataManager getGaussianRandomNumber:15 stdDev:1.5];
        double y = [SCDDataManager getGaussianRandomNumber:15 stdDev:1.5];
        double z = [SCDDataManager getGaussianRandomNumber:15 stdDev:1.5];
        
        [ds appendX:@(x) y:@(y) z:@(z)];
    }
    
    SCISpherePointMarker3D *pointMarker = [SCISpherePointMarker3D new];
    pointMarker.fillColor = 0xFF32CD32;
    pointMarker.size = 10.0;
    
    SCIScatterRenderableSeries3D *rs = [SCIScatterRenderableSeries3D new];
    rs.dataSeries = ds;
    rs.pointMarker = pointMarker;
    
    [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
        self.surface.xAxis = xAxis;
        self.surface.yAxis = yAxis;
        self.surface.zAxis = zAxis;
        [self.surface.renderableSeries add:rs];
        [self.surface.chartModifiers add:ExampleViewBase.createDefault3DModifiers];
    }];
</div>
<div class="code-snippet" id="swift">
    let xAxis = SCINumericAxis3D()
    xAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    let yAxis = SCINumericAxis3D()
    yAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    let zAxis = SCINumericAxis3D()
    zAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    
    let dataSeries = SCIXyzDataSeries3D(xType: .double, yType: .double, zType: .double)
    for _ in 0 ..< 200 {
        let x = SCDDataManager.getGaussianRandomNumber(5, stdDev: 1.5)
        let y = SCDDataManager.getGaussianRandomNumber(5, stdDev: 1.5)
        let z = SCDDataManager.getGaussianRandomNumber(5, stdDev: 1.5)
        dataSeries.append(x: x, y: y, z: z)
    }
    
    let pointMarker = SCISpherePointMarker3D()
    pointMarker.fillColor = 0xFF32CD32;
    pointMarker.size = 10.0
    
    let rs = SCIScatterRenderableSeries3D()
    rs.dataSeries = dataSeries
    rs.pointMarker = pointMarker
    rs.selectedVertexColor = 0xFF00FF00
    rs.metadataProvider = SCIDefaultSelectableMetadataProvider3D(seriesType: SCIScatterRenderableSeries3D.self)
    
    let orbitModifier = SCIOrbitModifier3D()
    orbitModifier.gestureRecognizer?.minimumNumberOfTouches = 2
    
    SCIUpdateSuspender.usingWith(surface) {
        self.surface.xAxis = xAxis
        self.surface.yAxis = yAxis
        self.surface.zAxis = zAxis
        self.surface.renderableSeries.add(rs)
        self.surface.chartModifiers.add(ExampleViewBase.createDefault3DModifiers())
    }
</div>
<div class="code-snippet" id="cs">
    var dataManager = DataManager.Instance;
    var dataSeries3D = new XyzDataSeries3D&lt;double, double, double&gt;();

    for (int i = 0; i < 100; i++)
    {
        double x = dataManager.GetGaussianRandomNumber(5, 1.5);
        double y = dataManager.GetGaussianRandomNumber(5, 1.5);
        double z = dataManager.GetGaussianRandomNumber(5, 1.5);

        dataSeries3D.Append(x, y, z);
    }

    var rSeries3D = new SCIScatterRenderableSeries3D
    {
        DataSeries = dataSeries3D,
        PointMarker = new SCISpherePointMarker3D { FillColor = ColorUtil.Lime, Size = 10.0f },
    };

    using (Surface.SuspendUpdates())
    {
        Surface.XAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.YAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.ZAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.RenderableSeries.Add(rSeries3D);
        Surface.ChartModifiers.Add(CreateDefault3DModifiers());

        Surface.Camera = new SCICamera3D();
    }
</div>

In the code above, a **Scatter Series** instance is created and assigned to draw the data provided by the `ISCIDataSeries3D` assigned to it.
The ***Scatters 3D*** are drawn with a **PointMarker** provided by the `SCISpherePointMarker3D` instance.
Finally, the **3D Scatter Series** is added to the `ISCIChartSurface3D.renderableSeries` collection.

#### Paint Scatters With Different Colors
Is SciChart, you can draw each scatter of the **Scatter Series 3D** with the different color using the [MetadataProvider API](metadataprovider-3d-api.html).
To use **MetadataProvider** for scatters 3D - a custom `ISCIPointMetadataProvider3D` has to be provided to the `ISCIRenderableSeries3D.metadataProvider` property. Also, there is `SCIPointMetadataProvider3D` provided out of the box, as a general case for **Scatter Series 3D**.

Individually colored **Scatter Series 3D** are also known as [Bubble 3D Chart](bubble-series-3d.html). Please visit the corresponding article for more information about it.

> **_NOTE:_** To learn more about coloring `ISCIRenderableSeries3D` points individually - refer to the [MetadataProvider API](metadataprovider-3d-api.html) article for more info.