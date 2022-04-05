# The Bubble 3D Chart Type
The Bubble 3D Chart can be created using the `SCIScatterRenderableSeries3D` type.
It is exactly the same Chart Type that **[Scatter Chart 3D](scatter-series-3d.html)** is except it has individually colored points via the [MetadataProvider API](metadataprovider-3d-api.html).

> **_NOTE:_** Please see the [Scatter Chart 3D](scatter-series-3d.html) article for more information about `SCIScatterRenderableSeries3D`.

![Bubble Chart 3D](img/chart-types-3d/bubble-chart-3d-example.png)

> **_NOTE:_** Examples for the ***Bubble Series 3D*** can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
>
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-3d-chart-example-simple-bubble-3d-char/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-3d-chart-example-simple-bubble-3d-chart/)

There is a special class `SCIPointMetadataProvider3D` which stores collection of `SCIPointMetadata3D`.
Is expects to have metadata for each point in your `ISCIDataSeries3D`.

The `SCIPointMetadata3D` allows you to change the **Color** as well as **Scale** of your vertexes in point-by-point basis.

Please see the code below to see how to use `SCIPointMetadataProvider3D` to provide metadata for the `SCIScatterRenderableSeries3D`:


<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyzDataSeries3D *ds = [[SCIXyzDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double];
    SCIPointMetadataProvider3D *metadataProvider = [SCIPointMetadataProvider3D new];
    
    for (int i = 0; i < 250; ++i) {
        double x = [SCDDataManager getGaussianRandomNumber:15 stdDev:1.5];
        double y = [SCDDataManager getGaussianRandomNumber:15 stdDev:1.5];
        double z = [SCDDataManager getGaussianRandomNumber:15 stdDev:1.5];
        [ds appendX:@(x) y:@(y) z:@(z)];
        
        // Provide metadata for each point in DataSeries
        SCIPointMetadata3D *metaData = [[SCIPointMetadata3D alloc] initWithVertexColor:[SCDDataManager randomColor] andScale:[SCDDataManager randomScale]];
        [metadataProvider.metadata addObject:metaData];
    }
    ...
    // Apply the MetadataProvider onto the Scatter Series 3D
    SCIScatterRenderableSeries3D *rs = [SCIScatterRenderableSeries3D new];
    rs.metadataProvider = metadataProvider;
</div>
<div class="code-snippet" id="swift">
    let dataSeries = SCIXyzDataSeries3D(xType: .double, yType: .double, zType: .double)
    let pointMetaDataProvider = SCIPointMetadataProvider3D()
    
    for _ in 0 ..< 250 {
        let x = SCDDataManager.getGaussianRandomNumber(5, stdDev: 1.5)
        let y = SCDDataManager.getGaussianRandomNumber(5, stdDev: 1.5)
        let z = SCDDataManager.getGaussianRandomNumber(5, stdDev: 1.5)
        dataSeries.append(x: x, y: y, z: z);
        
        // Provide metadata for each point in DataSeries
        let metadata = SCIPointMetadata3D(vertexColor: SCDDataManager.randomColor(), andScale: SCDDataManager.randomScale())
        pointMetaDataProvider.metadata.add(metadata)
    }
    ...
    // Apply the MetadataProvider onto the Scatter Series 3D
    let rs = SCIScatterRenderableSeries3D()
    rs.metadataProvider = pointMetaDataProvider
</div>
<div class="code-snippet" id="cs">
    var dataSeries3D = new XyzDataSeries3D&lt;double, double, double&gt;();
    var metadataProvider = new SCIPointMetadataProvider3D();

    for (int i = 0; i < 250; i++)
    {
        var x = dataManager.GetGaussianRandomNumber(5, 1.5);
        var y = dataManager.GetGaussianRandomNumber(5, 1.5);
        var z = dataManager.GetGaussianRandomNumber(5, 1.5);
        dataSeries3D.Append(x, y, z);

        // Provide metadata for each point in DataSeries
        var metadata = new SCIPointMetadata3D((uint)dataManager.GetRandomColor().ToArgb(), dataManager.GetRandomScale());
        metadataProvider.Metadata.Add(metadata);
    }
    ...
    // Apply the MetadataProvider onto the Scatter Series 3D
    var rSeries3D = new SCIScatterRenderableSeries3D();
    rSeries3D.MetadataProvider = metadataProvider;
</div>