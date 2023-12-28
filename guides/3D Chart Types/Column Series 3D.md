# The Column 3D Chart Type
The 3D Column Charts are provided by the `SCIColumnRenderableSeries3D` class.

![Column Chart 3D](img/chart-types-3d/column-chart-3d-example.png)

> **_NOTE:_** Examples for the ***Column Series 3D*** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-uniform-column/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-3d-chart-example-uniform-column/)

## Create a Column Series 3D
In SciChart you can achieve either ***[Uniform](#uniform-column-series-3d)*** or ***[Non-Uniform](#non-uniform-column-series-3d)*** **Column Series 3D**.
Both are provided by the `SCIColumnRenderableSeries3D` but underlying `ISCIDataSeries3D` is different.

The **Column Series 3D** can be configured via the following properties:

| **Property**                                    | **Description**                                                                                 |
| ----------------------------------------------- | ----------------------------------------------------------------------------------------------- |
| `SCIColumnRenderableSeries3D.fillColor`         | allows to specify the **fill color** for the column shapes.                                     |
| `SCIColumnRenderableSeries3D.dataPointWidth`    | defines the fraction of available space each column should occupy in `X and Z dimensions`. Should be in `[0.0...1.0]` range. |
| `SCIColumnRenderableSeries3D.dataPointWidthX`   | defines the fraction of available space each column should occupy in `X dimension`. Should be in `[0.0...1.0]` range. |
| `SCIColumnRenderableSeries3D.dataPointWidthZ`   | defines the fraction of available space each column should occupy in `Z dimension`. Should be in `[0.0...1.0]` range. |
| `SCIColumnRenderableSeries3D.columnSpacingMode` | allows to specify whether the column size should be ***fixed*** or ***max available*** while not overlapping with each other. Expects `SCIColumnSpacingMode` enumeration. |
| `SCIColumnRenderableSeries3D.columnFixedSize`   | allows to specify the size for column while using  the `SCIColumnSpacingMode.SCIColumnSpacingMode_FixedSize` mode. |
| `SCIColumnRenderableSeries3D.columnShape`       | allows to change the columns ***shape***. See the [3D Column Shapes](#3d-column-shapes) section. |

> **_NOTE:_** The ***dataPointWidth's*** of columns are set as a ratio of the available space between the neighboring points.

#### Uniform Column Series 3D
In order to create **Uniform** Column Series 3D - you will need to provide the `SCIUniformGridDataSeries3D` with `N x M` array of points.

The above graph is rendered with the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    const int ColumnsCount = 15;
    ...
    SCINumericAxis3D *xAxis = [SCINumericAxis3D new];
    xAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    
    SCINumericAxis3D *yAxis = [SCINumericAxis3D new];
    yAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    yAxis.visibleRange = [[SCIDoubleRange alloc] initWithMin:0.0 max:0.5];

    SCINumericAxis3D *zAxis = [SCINumericAxis3D new];
    zAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    
    SCIUniformGridDataSeries3D *ds = [[SCIUniformGridDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double xSize:ColumnsCount zSize:ColumnsCount];
    
    for (int i = 0; i < ColumnsCount; ++i) {
        for (int j = 0; j < ColumnsCount; ++j) {
            double y = sin(i * 0.25) / ((j+1) * 2);
            [ds updateYValue:@(y) atXIndex:i zIndex:j];
        }
    }
    
    SCIColumnRenderableSeries3D *rs = [SCIColumnRenderableSeries3D new];
    rs.dataSeries = ds;
    rs.fillColor = 0xFF1E90FF;
    
    [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
        self.surface.xAxis = xAxis;
        self.surface.yAxis = yAxis;
        self.surface.zAxis = zAxis;
        [self.surface.renderableSeries add:rs];
        [self.surface.chartModifiers add:ExampleViewBase.createDefault3DModifiers];
    }];
</div>
<div class="code-snippet" id="swift">
    private let Count = 15;
    ...
    let xAxis = SCINumericAxis3D()
    xAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    
    let yAxis = SCINumericAxis3D()
    yAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    yAxis.visibleRange = SCIDoubleRange(min: 0.0, max: 0.5)
    
    let zAxis = SCINumericAxis3D()
    zAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    
    let ds = SCIUniformGridDataSeries3D(xType: .double, yType: .double, zType: .double, xSize: Count, zSize: Count)
    for x in 0 ..< Count {
        for z in 0 ..< Count {
            let y = sin(Double(x) * 0.25) / Double((z + 1) * 2)
            ds.update(y: y, atX: x, z: z)
        }
    }
    
    let rs = SCIColumnRenderableSeries3D()
    rs.dataSeries = ds
    rs.fillColor = 0xFF1E90FF
    
    SCIUpdateSuspender.usingWith(surface) {
        self.surface.xAxis = xAxis
        self.surface.yAxis = yAxis
        self.surface.zAxis = zAxis
        self.surface.renderableSeries.add(rs)
        self.surface.chartModifiers.add(ExampleViewBase.createDefault3DModifiers())
    }
</div>
<div class="code-snippet" id="cs">
    const int count = 15;
    var dataSeries3D = new UniformGridDataSeries3D&lt;double, double, double&gt;(count, count);

    for (int x = 0; x < count; x++)
    {
        for (int z = 0; z < count; z++)
        {
            var y = Math.Sin(x * .2) / ((z + 1) * 2);
            dataSeries3D.UpdateYAt(x, z, y);
        }
    }

    var rSeries3D = new SCIColumnRenderableSeries3D
    {
        DataSeries = dataSeries3D,
        FillColor = ColorUtil.CornflowerBlue,
    };

    using (Surface.SuspendUpdates())
    {
        Surface.XAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.YAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.5) };
        Surface.ZAxis = new SCINumericAxis3D { GrowBy = new SCIDoubleRange(0.1, 0.1) };
        Surface.RenderableSeries.Add(rSeries3D);
        Surface.ChartModifiers.Add(CreateDefault3DModifiers());
    }
</div>

#### Single Row Column 3D Charts
You might also want to create a ***Single-Row*** of 3D Columns.
It's easily achievable via providing `SCIUniformGridDataSeries3D` with size of `1` in `Z-Direction` and update the `worldDimensions` like below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIUniformGridDataSeries3D *ds = [[SCIUniformGridDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double xSize:ColumnsCount zSize:1];
    for (int x = 0; x < ColumnsCount; x++) {
        for (int z = 0; z < 1; z++) {
            double y = sin(x * 0.25) / ((z + 1) * 2);
            [ds updateYValue:@(y) atXIndex:x zIndex:z];
        }
    }
    ...
    [self.surface.worldDimensions assignX:200 y:100 z:20];
</div>
<div class="code-snippet" id="swift">
    let ds = SCIUniformGridDataSeries3D(xType: .double, yType: .double, zType: .double, xSize: Count, zSize: 1)
    for x in 0 ..< Count {
        for z in 0 ..< 1 {
            let y = sin(Double(x) * 0.25) / Double((z + 1) * 2)
            ds.update(y: y, atX: x, z: z)
        }
    }
    ...
    self.surface.worldDimensions.assignX(200, y: 100, z: 20)
</div>
<div class="code-snippet" id="cs">
    var dataSeries3D = new UniformGridDataSeries3D&lt;double, double, double&gt;(count, 1);
    for (int x = 0; x < count; x++)
    {
        for (int z = 0; z < 1; z++)
        {
            var y = Math.Sin(x * .2) / ((z + 1) * 2);
            dataSeries3D.UpdateYAt(x, z, y);
        }
    }
    ...
    Surface.WorldDimensions.AssignX(200, 100, 20);
</div>

and results in the following chart:

![Single Row Column Chart 3D](img/chart-types-3d/column-chart-3d-single-row-example.png)

#### Non-Uniform Column Series 3D
In order to create **Non-Uniform** Column Series 3D - you will need to provide the `SCIXyzDataSeries3D` with points.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyzDataSeries3D *ds = [[SCIXyzDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double];
    SCIPointMetadataProvider3D *metadataProvider = [SCIPointMetadataProvider3D new];
    
    for (int i = 0; i < Count; ++i) {
        for (int j = 0; j < Count; ++j) {
            if (i != j && (i % 3) == 0 && (j % 3) == 0) {
                double y = [SCDDataManager getGaussianRandomNumber:15 stdDev:1.5];
                [ds appendX:@(i) y:@(y) z:@(j)];
                
                SCIPointMetadata3D *metaData = [[SCIPointMetadata3D alloc] initWithVertexColor:[SCDDataManager randomColor] andScale:[SCDDataManager randomScale]];
                [metadataProvider.metadata addObject:metaData];
            }
        }
    }
    
    SCIColumnRenderableSeries3D *rs = [SCIColumnRenderableSeries3D new];
    rs.dataSeries = ds;
    rs.metadataProvider = metadataProvider;
</div>
<div class="code-snippet" id="swift">
    let dataSeries = SCIXyzDataSeries3D(xType: .double, yType: .double, zType: .double)
    let pointMetaDataProvider = SCIPointMetadataProvider3D()
    
    for i in 1 ..< Count {
        for j in 1 ..< Count {
            if (i != j) && (i % 3) == 0 && (j % 3) == 0 {
                let y = SCDDataManager.getGaussianRandomNumber(5, stdDev: 1.5)
                dataSeries.append(x: i, y: y, z: j)

                let metadata = SCIPointMetadata3D(vertexColor: SCDDataManager.randomColor(), andScale: SCDDataManager.randomScale())
                pointMetaDataProvider.metadata.add(metadata)
            }
        }
    }
    
    let rs = SCIColumnRenderableSeries3D()
    rs.dataSeries = dataSeries
    rs.metadataProvider = pointMetaDataProvider
</div>
<div class="code-snippet" id="cs">
    var dataSeries3D = new XyzDataSeries3D&lt;double, double, double&gt;();
    var metadataProvider = new SCIPointMetadataProvider3D();

    for (int i = 0; i < count; i++)
    {
        for (int j = 0; j < count; j++)
        {
            if (i != j && i % 2 == 0 && j % 2 == 0)
            {
                var y = dataManager.GetGaussianRandomNumber(5, 1.5);
                dataSeries3D.Append(i, y, j);

                var metadata = new SCIPointMetadata3D((uint)dataManager.GetRandomColor().ToArgb());
                metadataProvider.Metadata.Add(metadata);
            }
        }
    }

    var rSeries3D = new SCIColumnRenderableSeries3D
    {
        DataSeries = dataSeries3D,
        MetadataProvider = metadataProvider
    };
</div>

which will result in the following chart:

![Non-Uniform Column Chart 3D](img/chart-types-3d/column-chart-3d-non-uniform-example.png)

> **_NOTE:_** Full example code for the ***Sparse Column Series 3D*** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-sparse-column/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-sparse-column/)

#### 3D Column Shapes
There are several shapes provided out of the box for **3D Column Series** via the `SCIColumnRenderableSeries3D.columnShape`, 
and expects type from the `SCIChartMeshTemplate` enumeration.

The possible ***Shapes*** are showed in the table below:

| **Shape**                                            | **Output**                                                                             |
| ---------------------------------------------------- | -------------------------------------------------------------------------------------- |
| `SCIChartMeshTemplate.SCIChartMeshTemplate_Cube`     | ![Cube Column 3D Shape](img/chart-types-3d/column-3d-cube-shape.png)                   |
| `SCIChartMeshTemplate.SCIChartMeshTemplate_Sphere`   | ![Sphere Column 3D Shape](img/chart-types-3d/column-3d-sphere-shape.png)               |
| `SCIChartMeshTemplate.SCIChartMeshTemplate_Pyramid`  | ![Pyramid Column 3D Shape](img/chart-types-3d/column-3d-pyramid-shape.png)             |
| `SCIChartMeshTemplate.SCIChartMeshTemplate_Cylinder` | ![Cylinder Column 3D Shape](img/chart-types-3d/column-chart-3d-single-row-example.png) |