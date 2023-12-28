# SciChart iOS Tutorial - Create a simple 2D Chart
In this SciChart iOS 3D tutorial, you will learn to:
- create a **2D Chart**
- add `X and Y` [Axes](Axis APIs.html) to a Chart;
- render a Simple [Line](2d-chart-types---line-series.html) and [Scatter](2d-chart-types---scatter-series.html) Series.

> **_NOTE:_** This ***tutorial*** assumes that you’ve already know how to [Link SciChart iOS](integrating-scichart-libraries.html) and [Add SCIChartSurface instance](creating-your-first-scichart-ios-app.html#adding-axes-to-the-scichartsurface) into your `ViewController`. If you need more information - please read the following articles:
>
> - [Integrating SciChart.framework](integrating-scichart-libraries.html)
> - [The SCIChartSurface Type](creating-your-first-scichart-ios-app.html#the-scichartsurface-type)

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2001%20-%20Create%20a%20simple%202D%20Chart)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-01)

## Adding Axes to the SCIChartSurface
Once you have added a `SCIChartSurface` into your ViewController, you will not see anything drawn because you need to add axes. 
This is important thing here - **two axes X and Y** has to be added to your surface. This is a bare minimum to see drawn grid on your device.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [self.surface.xAxes add:[SCINumericAxis new]];
    [self.surface.yAxes add:[SCINumericAxis new]];
</div>
<div class="code-snippet" id="swift">
    self.surface.xAxes.add(items: SCINumericAxis())
    self.surface.yAxes.add(items: SCINumericAxis())
</div>
<div class="code-snippet" id="cs">
    Surface.XAxes.Add(new SCINumericAxis());
    Surface.YAxes.Add(new SCINumericAxis());
</div>

![Empty SCIChartSurface](img/tutorials-2d/tutorials-2d-empty-chart.png)

## Adding Series to the Chart
Now, we would like to see something more than just empty grid, e.g. Some **Scatter Chart** or a **Line Chart**.
To draw some data in chart we need to create 3D DataSeries and [RenderableSeries](2D Chart Types.html).

The **DataSeries** is a class which is responsible for storing data which should be displayed.
For more information about the DataSeries types available in SciChart - refer to the [DataSeries Types](dataseries-apis.html) article.

The **RenderableSeries** on the other hand are the special classes in SciChart, that determines how data should be visualized by chart. 
You can find more information about RenderableSeries in the [2D Chart Types](2D Chart Types.html).

In this tutorial, we are going to add a [Line](2d-chart-types---line-series.html) and a [Scatter](2d-chart-types---scatter-series.html) series onto the chart.
First, let's declare the DataSeries for both and generate some data for them below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIXyDataSeries *lineDataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Int yType:SCIDataType_Double];
    SCIXyDataSeries *scatterDataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Int yType:SCIDataType_Double];
    for (int i = 0; i < 200; i++) {
        [lineDataSeries appendX:@(i) y:@(sin(i * 0.1))];
        [scatterDataSeries appendX:@(i) y:@(cos(i * 0.1))];
    }
</div>
<div class="code-snippet" id="swift">
    let lineDataSeries = SCIXyDataSeries(xType: .int, yType: .double)
    let scatterDataSeries = SCIXyDataSeries(xType: .int, yType: .double)
    for i in 0 ..< 200 {
        lineDataSeries.append(x: i, y: sin(Double(i) * 0.1))
        scatterDataSeries.append(x: i, y: cos(Double(i) * 0.1))
    }
</div>
<div class="code-snippet" id="cs">
    var lineDataSeries = new XyDataSeries&lt;int, double&gt;();
    var scatterDataSeries = new XyDataSeries&lt;int, double&gt;();
    for (int i = 0; i < 200; i++)
    {
        lineDataSeries.Append(i, Math.Sin(i * 0.1));
        scatterDataSeries.Append(i, Math.Cos(i * 0.1));
    }
</div>

The next step is to create **Line** and **Scatter** [RenderableSeries](2D Chart Types.html) and provide previously created DataSeries to it.
Please note, that [Scatter Series](2d-chart-types---scatter-series.html) requires a [PointMarker](pointmarker-api.html) to be drawn.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIFastLineRenderableSeries *lineSeries = [SCIFastLineRenderableSeries new];
    lineSeries.dataSeries = lineDataSeries;
    
    SCIEllipsePointMarker *pointMarker = [SCIEllipsePointMarker new];
    pointMarker.fillStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0xFF32CD32];
    pointMarker.size = CGSizeMake(10, 10);
    
    SCIXyScatterRenderableSeries *scatterSeries = [SCIXyScatterRenderableSeries new];
    scatterSeries.dataSeries = scatterDataSeries;
    scatterSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    let lineSeries = SCIFastLineRenderableSeries()
    lineSeries.dataSeries = lineDataSeries
    
    let pointMarker = SCIEllipsePointMarker()
    pointMarker.fillStyle = SCISolidBrushStyle(colorCode: 0xFF32CD32)
    pointMarker.size = CGSize(width: 10, height: 10)
    
    let scatterSeries = SCIXyScatterRenderableSeries()
    scatterSeries.dataSeries = scatterDataSeries
    scatterSeries.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    var lineSeries = new SCIFastLineRenderableSeries { DataSeries = lineDataSeries };
    var scatterSeries = new SCIXyScatterRenderableSeries
    {
        DataSeries = scatterDataSeries,
        PointMarker = new SCIEllipsePointMarker { FillStyle = new SCISolidBrushStyle(0xFF32CD32), Size = new CGSize(10, 10) }
    };
</div>

Finally, we need to add newly created series into the `ISCIChartSurface.renderableSeries` collection, like so:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
        [self.surface.xAxes add:xAxis];
        [self.surface.yAxes add:yAxis];
        [self.surface.renderableSeries addAll:lineSeries, scatterSeries, nil];
    }];
</div>
<div class="code-snippet" id="swift">
    SCIUpdateSuspender.usingWith(self.surface) {
        self.surface.xAxes.add(items: SCINumericAxis())
        self.surface.yAxes.add(items: SCINumericAxis())
        self.surface.renderableSeries.add(items: lineSeries, scatterSeries)
    }
</div>
<div class="code-snippet" id="cs">
    using (Surface.SuspendUpdates())
    {
        Surface.XAxes.Add(new SCINumericAxis());
        Surface.YAxes.Add(new SCINumericAxis());
        Surface.RenderableSeries.Add(lineSeries);
        Surface.RenderableSeries.Add(scatterSeries);
    }
</div>

Which will render the following Chart:

![Simple Chart](img/tutorials-2d/tutorials-2d-simple-chart.png)

> **_NOTE:_** Please note that we've added axes and renderableSeries to `SCIChartSurface` inside `+[SCIUpdateSuspender usingWithSuspendable:withBlock:]` block. This allows to suspend surface instance and refresh it only one time after you finished all needed operations. That's **highly recommended** technique, if you want to omit performance decrease due to triggering refreshes on every operation which could be performed in one batch.

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2001%20-%20Create%20a%20simple%202D%20Chart)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-01)

Also, you can found **next tutorial** from this series here - [SciChart iOS Tutorial - Zooming and Panning Behavior](tutorial-02---zooming-and-panning-behavior.html)

Of course, this is not the limit of what you can achieve with the SciChart iOS.
Our documentation contains lots of useful information, some of the articles you might want to read are listed below:
- [Axis Types](Axis APIs.html)
- [2D Chart Types](2D Chart Types.html)
- [Chart Modifiers](Chart Modifier APIs.html)

Finally, start exploring. The SciChart iOS library and functionality is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples)
