# MetadataProvider 3D API
SciChart iOS 3D features a rich **MetadataProvider API** with gives the ability to change color on a ***point-by-point*** basis.

![MetadataProvider API](img/chart-types-3d/surface-mesh-3d-with-metadata-provider.png)

> **_NOTE:_** Examples of using ***MetadataProvider API*** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-surface-mesh-palette-provider/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-surface-mesh-palette-provider/)

To enable series coloring with **MetadataProvider**, you need to create a class which conforms to one of the following protocol (or possibly to all of them):

| **Protocol**                        | **Descriptions**                                                                          |
| ----------------------------------- | ----------------------------------------------------------------------------------------- | 
| `ISCIPointMetadataProvider3D`       | allows changing the **fill** for **[PointMarkers 3D](pointmarker-3d-api.html)**.          |
| `ISCIStrokeMetadataProvider3D`      | allows painting parts of the **series' stroke**;                                          |
| `ISCIFillMetadataProvider3D`        | allows changing the **fill** color for `ISCIRenderableSeries3D`.                          |
| `ISCISurfaceMeshMetadataProvider3D` | allows overriding a specific cell or cells in the for `SCISurfaceMeshRenderableSeries3D`. |
| `ISCISelectableMetadataProvider3D`  | allows provide different colors for selected points.                                      |

> **_NOTE:_** The ***MetadataProvider API*** is very similar to the [PaletteProvider API](paletteprovider-api.html) from SciChart 2D.

A choice depends on a [RenderableSeries 3D](3D Chart Types.html) type, which **MetadataProvider 3D** is designed for.

Each **MetadataProvider** protocol declares method(s), which provides a way to update series ***colors*** for every data points. 
Mentioned methods are called every time **RenderableSeries 3D** requires a redraw, so it expects that the colors array should be updated there correspondingly.

For the convenience, there is the `SCIMetadataProvider3DBase` class, which provides some basic implementation, so it's recommended to inherit from it while implementing custom **MetadataProvider**. There is also some predefined **MetadataProviders** listed below:
- `SCIDefaultSelectableMetadataProvider3D` - allows provide different colors for selected points. You can find it in our examples which demonstrate selection such as - [Waterfall Chart 3D](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-simple-waterfall/) and [Select Scatter Point 3D Chart](https://www.scichart.com/example/ios-3d-chart-example-select-scatter-point/).

## Create Custom MetadataProvider
The following code snippet demonstrates how to create a custom **MetadataProvider** which conforms to the - **Stroke** and **PointMarker** - metadata providers and colors them respectively to the `ISCICameraController.orbitalYaw` and `ISCICameraController.orbitalPitch`.

> **_NOTE:_** The below code is based on the [Point-Line Chart 3D](https://www.scichart.com/example/ios-3d-chart-example-simple-point-lines/) example which can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomMetadataProvider : SCIMetadataProvider3DBase&lt;SCIPointLineRenderableSeries3D *&gt;&lt;ISCIPointMetadataProvider3D, ISCIStrokeMetadataProvider3D&gt;
    @end
    @implementation CustomMetadataProvider

    - (instancetype)init {
        return [self initWithSeriesType:SCIPointLineRenderableSeries3D.class];
    }

    - (void)updateStrokeColors:(SCIUnsignedIntegerValues *)strokeColors defaultStroke:(unsigned int)defaultStroke {
        [self p_SCI_updateColors:strokeColors];
    }

    - (void)updatePointMetadataWithColors:(SCIUnsignedIntegerValues *)colors pointScales:(SCIFloatValues *)scales defaultColor:(unsigned int)defaultColor andDefaultScale:(float)defaultScale {
        [self p_SCI_updateColors:colors];
        
        const NSInteger pointsCount = self.renderableSeries.currentRenderPassData.pointsCount;
        scales.count = pointsCount;
        
        float *scalesArray = scales.itemsArray;
        for (NSInteger i = 0; i < pointsCount; i++) {
            scalesArray[i] = 1;
        }
    }

    - (void)p_SCI_updateColors:(SCIUnsignedIntegerValues *)colors {
        id&lt;ISCIRenderableSeries3D&gt; rSeries = self.renderableSeries;
        SCIXyzRenderPassData3D *renderPassData = (SCIXyzRenderPassData3D *)rSeries.currentRenderPassData;
        
        int upperThreshold = (int)rSeries.parentSurface.camera.orbitalYaw % 90;
        int lowerThreshold = (int)rSeries.parentSurface.camera.orbitalPitch % 90;
        const NSInteger pointsCount = renderPassData.pointsCount;
        
        colors.count = pointsCount;
        unsigned int *colorsArray = colors.itemsArray;
        
        SCIDoubleValues *yValues = renderPassData.yValues;
        for (NSInteger i = 0; i < pointsCount; i++) {
            double value = [yValues getValueAt:i];
            if (value > upperThreshold) {
                colorsArray[i] = 0xffff0000;
            } else if (value < lowerThreshold) {
                colorsArray[i] = 0xff00ff00;
            } else {
                colorsArray[i] = 0xffffff00;
            }
        }
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class CustomMetadataProvider: SCIMetadataProvider3DBase&lt;SCIPointLineRenderableSeries3D&gt;, ISCIPointMetadataProvider3D, ISCIStrokeMetadataProvider3D {

        init() {
            super.init(seriesType: SCIPointLineRenderableSeries3D.self)
        }
        
        func updateStrokeColors(_ strokeColors: SCIUnsignedIntegerValues!, defaultStroke: UInt32) {
            updateColors(colors: strokeColors)
        }
        
        func updatePointMetadata(withColors colors: SCIUnsignedIntegerValues, pointScales scales: SCIFloatValues, defaultColor: UInt32, andDefaultScale defaultScale: Float) {
            updateColors(colors: colors)
            
            let pointsCount = self.renderableSeries.currentRenderPassData.pointsCount
            scales.count = pointsCount
            
            for i in 0 ..< pointsCount {
                scales.set(1, at: i)
            }
        }
        
        fileprivate func updateColors(colors: SCIUnsignedIntegerValues) {
            let rSeries = self.renderableSeries!
            let renderPassData = rSeries.currentRenderPassData as! SCIXyzRenderPassData3D
            
            let upperThreshold = Double(rSeries.parentSurface.camera.orbitalYaw.truncatingRemainder(dividingBy: 90))
            let lowerThreshold = Double(rSeries.parentSurface.camera.orbitalPitch.truncatingRemainder(dividingBy: 90))
            
            let pointsCount = renderPassData.pointsCount
            colors.count = pointsCount;

            let yValues = renderPassData.yValues!
            for i in 0 ..< pointsCount {
                let value = yValues.getValueAt(i)
                if (value > upperThreshold) {
                    colors.set(0xffff0000, at: i)
                } else if (value < lowerThreshold) {
                    colors.set(0xff00ff00, at: i)
                } else {
                    colors.set(0xffffff00, at: i)
                }
            }
        }
    }
</div>
<div class="code-snippet" id="cs">
    class CustomMetadataProvider : SCIMetadataProvider3DBase&lt;SCIPointLineRenderableSeries3D&gt;, IISCIPointMetadataProvider3D, IISCIStrokeMetadataProvider3D
    {
        public void UpdateStrokeColors(SCIUnsignedIntegerValues strokeColors, uint defaultStroke)
        {
            UpdateColors(strokeColors);
        }

        public void UpdatePointMetadata(SCIUnsignedIntegerValues colors, SCIFloatValues scales, uint defaultColor, float defaultScale)
        {
            UpdateColors(colors);

            var pointsCount = RenderableSeries.CurrentRenderPassData.PointsCount;
            scales.Count = pointsCount;

            for (int i = 0; i < pointsCount; i++)
            {
                scales.Set(1, i);
            }
        }

        private void UpdateColors(SCIUnsignedIntegerValues colors)
        {
            var renderPassData = (SCIXyzRenderPassData3D)RenderableSeries.CurrentRenderPassData;

            var upperThreshold = RenderableSeries.ParentSurface.Camera.OrbitalYaw % 90;
            var lowerThreshold = RenderableSeries.ParentSurface.Camera.OrbitalPitch % 90;

            var pointsCount = RenderableSeries.CurrentRenderPassData.PointsCount;
            colors.Count = pointsCount;

            var yValues = renderPassData.YValues;
            for (int i = 0; i < pointsCount; i++)
            {
                var value = yValues.GetValueAt(i);
                if (value > upperThreshold)
                {
                    colors.Set(0xffff0000, i);
                }
                else if (value < lowerThreshold)
                {
                    colors.Set(0xff00ff00, i);
                }
                else
                {
                    colors.Set(0xffffff00, i);
                }
            }
        }
    }
</div>

Once the **MetadataProvider** class is ready, its instances can be used to set it for a RenderableSeries via the `ISCIRenderableSeries3D.metadataProvider` property:

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
    
    SCIXyzDataSeries3D *ds1 = [[SCIXyzDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double];
    for (int i = 0; i < 100; ++i) {
        double x = 5 * sin(i);
        double y = i;
        double z = 5 * cos(i);
        [ds1 appendX:@(x) y:@(y) z:@(z)];
    }
    
    SCISpherePointMarker3D *pointMarker = [SCISpherePointMarker3D new];
    pointMarker.size = 5.f;
    
    SCIPointLineRenderableSeries3D *rs = [SCIPointLineRenderableSeries3D new];
    rs.dataSeries = ds1;
    rs.strokeThickness = 2.0;
    rs.pointMarker = pointMarker;
    rs.metadataProvider = [CustomMetadataProvider new];
 
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
    for i in 0 ..< 100 {
        let x = 5 * sin(Double(i))
        let y = Double(i)
        let z = 5 * cos(Double(i))
        dataSeries.append(x: x, y: y, z: z)
    }
    
    let pointMarker = SCISpherePointMarker3D()
    pointMarker.size = 5.0
    
    let rs = SCIPointLineRenderableSeries3D()
    rs.dataSeries = dataSeries
    rs.strokeThickness = 2.0
    rs.pointMarker = pointMarker
    rs.metadataProvider = CustomMetadataProvider()
    
    SCIUpdateSuspender.usingWith(surface) {
        self.surface.xAxis = xAxis
        self.surface.yAxis = yAxis
        self.surface.zAxis = zAxis
        self.surface.renderableSeries.add(rs)
        self.surface.chartModifiers.add(ExampleViewBase.createDefault3DModifiers())
    }
</div>
<div class="code-snippet" id="cs">
    var dataSeries3D = new XyzDataSeries3D&lt;double, double, double&gt;();
    for (int i = 0; i < 100; i++)
    {
        double x = 5 * Math.Sin(i);
        double y = i;
        double z = 5 * Math.Cos(i);
        dataSeries3D.Append(x, y, z);
    }

    var rSeries3D = new SCIPointLineRenderableSeries3D
    {
        DataSeries = dataSeries3D,
        StrokeThickness = 2f,
        PointMarker = new SCISpherePointMarker3D { Size = 5f },
        MetadataProvider = new CustomMetadataProvider(),
    };

    using (Surface.SuspendUpdates())
    {
        Surface.XAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.YAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.ZAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.RenderableSeries.Add(rSeries3D);
        Surface.ChartModifiers.Add(CreateDefault3DModifiers());

        Surface.Camera = new SCICamera3D { Position = new SCIVector3(-350, 100, -350), Target = new SCIVector3(0, 50, 0) };
    }
</div>

Which will result with the following:

<video autoplay loop muted playsinline src="img/chart-types-3d/metadata-provider-thresholds.mp4"></video>