# SciChart iOS 3D Tutorial - Create a simple Scatter Chart 3D
In this SciChart iOS 3D tutorial, you’ll learn to:
- create a **3D Chart**
- add `X, Y and Z` [Axes](Axis 3D APIs.html) to a Chart;
- render a [Simple Scatter 3D Series](scatter-series-3d.html);

> **_NOTE:_** This ***tutorial*** assumes that you’ve already know how to [Link SciChart iOS](integrating-scichart-framework.html) and [Add SCIChartSurface3D instance](creating-your-first-scichart-ios-app.html#adding-3d-axes-to-the-scichartsurface3d) into your `ViewController`. If you need more information - please read the following articles:
>
> - [Integrating SciChart.framework](integrating-scichart-libraries.html)
> - [The SCIChartSurface3D Type](creating-your-first-scichart-ios-app.html#the-scichartsurface3d-type)

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2001%20-%20Create%20a%20simple%20Scatter%20Chart%203D)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-01)

## Adding 3D Axes to the SCIChartSurface3D
Once you have added a `SCIChartSurface3D` into your ViewController, you will not see anything drawn because you need to add axes. 
This is important: that **three axes X, Y and Z** has to be added to your surface.This is a bare minimum to see drawn grid on your device.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    self.surface.xAxis = [SCINumericAxis3D new];
    self.surface.yAxis = [SCINumericAxis3D new];
    self.surface.zAxis = [SCINumericAxis3D new];
</div>
<div class="code-snippet" id="swift">
    self.surface.xAxis = SCINumericAxis3D()
    self.surface.yAxis = SCINumericAxis3D()
    self.surface.zAxis = SCINumericAxis3D()
</div>
<div class="code-snippet" id="cs">
    Surface.XAxis = new SCINumericAxis3D();
    Surface.YAxis = new SCINumericAxis3D();
    Surface.ZAxis = new SCINumericAxis3D();
</div>

![Empty SCIChartSurface3D](img/tutorials-3d/tutorials-3d-empty-chart.png)

## Adding 3D Renderable Series
Now, we would like to see something more than just empty grid, e.g. **Scatter 3D Chart**. 
To draw some data in chart we need to create 3D DataSeries and [RenderableSeries](3D Chart Types.html).
The DataSeries is a class which is responsible for storing data which should be displayed and RenderableSeries is a class that determines how data should be visualized by chart.

First, let's declare the 3D DataSeries and generate some data for it:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyzDataSeries3D *ds = [[SCIXyzDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double];
    for (int i = 0; i < 200; ++i) {
        double x = [self getGaussianRandomNumber:5 stdDev:1.5];
        double y = [self getGaussianRandomNumber:5 stdDev:1.5];
        double z = [self getGaussianRandomNumber:5 stdDev:1.5];

        [ds appendX:@(x) y:@(y) z:@(z)];
    }
</div>
<div class="code-snippet" id="swift">
    let dataSeries = SCIXyzDataSeries3D(xType: .double, yType: .double, zType: .double)
    for _ in 0 ..< 200 {
        let x = getGaussianRandomNumber(mean: 5, stdDev: 1.5)
        let y = getGaussianRandomNumber(mean: 5, stdDev: 1.5)
        let z = getGaussianRandomNumber(mean: 5, stdDev: 1.5)

        dataSeries.append(x: x, y: y, z: z)
    }
</div>
<div class="code-snippet" id="cs">
    var dataSeries3D = new XyzDataSeries3D&lt;double, double, double&gt;();
    for (int i = 0; i < 200; i++)
    {
        double x = GetGaussianRandomNumber(5, 1.5);
        double y = GetGaussianRandomNumber(5, 1.5);
        double z = GetGaussianRandomNumber(5, 1.5);

        dataSeries3D.Append(x, y, z);
    }
</div>

The next step is to create [3D RenderableSeries](3D Chart Types.html).
In our case we're going to display data as a point cloud using [Scatter Series 3D](scatter-series-3d.html), which is represented by the `SCIScatterRenderableSeries3D`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCISpherePointMarker3D *pointMarker = [SCISpherePointMarker3D new];
    pointMarker.fillColor = 0xFF32CD32;
    pointMarker.size = 10.0;
    
    SCIScatterRenderableSeries3D *rSeries = [SCIScatterRenderableSeries3D new];
    rSeries.dataSeries = dataSeries;
    rSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    let pointMarker = SCISpherePointMarker3D()
    pointMarker.fillColor = 0xFF32CD32;
    pointMarker.size = 10.0
    
    let rSeries = SCIScatterRenderableSeries3D()
    rSeries.dataSeries = dataSeries
    rSeries.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    var rSeries = new SCIScatterRenderableSeries3D
    {
        DataSeries = dataSeries,
        PointMarker = new SCISpherePointMarker3D { FillColor = 0xFF32CD32, Size = 10.0f },
    };
</div>

Finally, we need to add newly created series into the `ISCIChartSurface3D.renderableSeries` collection, like so:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
        self.surface.xAxis = [SCINumericAxis3D new];
        self.surface.yAxis = [SCINumericAxis3D new];
        self.surface.zAxis = [SCINumericAxis3D new];
        [self.surface.renderableSeries add:rSeries];
    }];
</div>
<div class="code-snippet" id="swift">
    SCIUpdateSuspender.usingWith(self.surface) {
        self.surface.xAxis = SCINumericAxis3D()
        self.surface.yAxis = SCINumericAxis3D()
        self.surface.zAxis = SCINumericAxis3D()
        self.surface.renderableSeries.add(rSeries)
    }
</div>
<div class="code-snippet" id="cs">
    using (Surface.SuspendUpdates())
    {
        Surface.XAxis = new SCINumericAxis3D();
        Surface.YAxis = new SCINumericAxis3D();
        Surface.ZAxis = new SCINumericAxis3D();
        Surface.RenderableSeries.Add(rSeries);
    }
</div>

Which will render the following Scatter Chart 3D:

![Empty SCIChartSurface3D](img/tutorials-3d/tutorials-3d-scatter-chart.png)

> **_NOTE:_** Please note that we've added axes and renderableSeries to `SCIChartSurface3D` inside `+[SCIUpdateSuspender usingWithSuspendable:withBlock:]` block. This allows to suspend surface instance and refresh it only one time after you finished all needed operations. That's **highly recommended** technique, if you want to omit performance decrease due to triggering refreshes on every operation which could be performed in one batch.

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2001%20-%20Create%20a%20simple%20Scatter%20Chart%203D)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-01)

Also, you can found **next tutorial** from this series here - [SciChart iOS 3D Tutorial - Zooming and Rotating](3d-tutorial-02---zooming-and-rotating.html)

Of course, this is not the limit of what you can achieve with the SciChart iOS 3D.
Our documentation contains lots of useful information, some of the articles you might want to read are listed below:
- [3D Axis Types](Axis 3D APIs.html)
- [3D Chart Types](3D Chart Types.html)
- [3D Chart Modifiers](Chart Modifier 3D APIs.html)

Finally, start exploring. The SciChart iOS library and functionality is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples)
