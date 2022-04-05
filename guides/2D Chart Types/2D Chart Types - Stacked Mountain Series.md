# The Stacked Mountain Series Type
The **Stacked Mountain** series can be created by using `SCIStackedMountainRenderableSeries` and `SCIVerticallyStackedMountainsCollection` classes. Stacked Mountain series shares most of its properties with the regular **[Mountain (Area) Series](2d-chart-types---mountain-area-series.html)**, with the added feature that mountains are **automatically stacked** (aggregate its Y-Values).

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done by passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

![Stacked Mountain Series Type](img/chart-types-2d/stacked-mountain-chart-example.png)

> **_NOTE:_** Examples of the Stacked Mountain Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-stacked-mountain-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-stacked-mountain-chart-example/)

## How the Stacking Works for Mountain Series
Stacking Mountain series is handled by special renderableSeries - `SCIVerticallyStackedMountainsCollection`.

Basically, it's a simple `SCIObservableCollection` of **Stacked Mountain Series**, where the order of items in it determines how series should be stacked and drawn - the first item will be drawn as regular mountain series and the rest will be stacked on top of each other. 

If you want to have several sets of **Stacked Mountain Series** which should be stacked independently, all you need to do - is to create corresponding amount of `SCIVerticallyStackedMountainsCollection` which will hold appropriate `SCIStackedMountainRenderableSeries`.

## Create a Vertically Stacked Mountain Series 
To create a **Vertically Stacked Mountain Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *ds1 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    SCIXyDataSeries *ds2 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];

    // Create Stacked Mountain Series
    SCIStackedMountainRenderableSeries *rSeries1 = [SCIStackedMountainRenderableSeries new];
    rSeries1.dataSeries = ds1;
    rSeries1.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFffffff thickness:1.0];
    rSeries1.areaStyle = [[SCILinearGradientBrushStyle alloc] initWithStart:CGPointZero end:CGPointMake(0.0, 1.0) startColorCode:0xDDDBE0E1 endColorCode:0x88B6C1C3];
    
    SCIStackedMountainRenderableSeries *rSeries2 = [SCIStackedMountainRenderableSeries new];
    rSeries2.dataSeries = ds2;
    rSeries2.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFffffff thickness:1.0];
    rSeries2.areaStyle = [[SCILinearGradientBrushStyle alloc] initWithStart:CGPointZero end:CGPointMake(0.0, 1.0) startColorCode:0xDDACBCCA endColorCode:0x88439AAF];
    
    // Create and Fill Stacked Series Collection
    SCIVerticallyStackedMountainsCollection *seriesCollection = [SCIVerticallyStackedMountainsCollection new];
    [seriesCollection add:rSeries1];
    [seriesCollection add:rSeries2];
    
    [surface.renderableSeries add:seriesCollection];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let ds1 = SCIXyDataSeries(xType: .double, yType: .double)
    let ds2 = SCIXyDataSeries(xType: .double, yType: .double)

    // Create Stacked Mountain Series
    let rSeries1 = SCIStackedMountainRenderableSeries()
    rSeries1.dataSeries = ds1
    rSeries1.strokeStyle = SCISolidPenStyle(colorCode: 0xFFffffff, thickness: 1)
    rSeries1.areaStyle = SCILinearGradientBrushStyle(start: CGPoint(x: 0.0, y: 0.0), end: CGPoint(x: 1.0, y: 1.0), startColorCode: 0xDDDBE0E1, endColorCode: 0x88B6C1C3)
    
    let rSeries2 = SCIStackedMountainRenderableSeries()
    rSeries2.dataSeries = ds2
    rSeries2.strokeStyle = SCISolidPenStyle(colorCode: 0xFFffffff, thickness: 1)
    rSeries2.areaStyle = SCILinearGradientBrushStyle(start: CGPoint(x: 0.0, y: 0.0), end: CGPoint(x: 1.0, y: 1.0), startColorCode: 0xDDACBCCA, endColorCode: 0x88439AAF)

    // Create and Fill Stacked Series Collection
    let seriesCollection = SCIVerticallyStackedMountainsCollection()
    seriesCollection.add(rSeries1)
    seriesCollection.add(rSeries2)

    surface.renderableSeries.add(seriesCollection)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var ds1 = new XyDataSeries&lt;double, double&gt; { SeriesName = "data 1" };
    var ds2 = new XyDataSeries&lt;double, double&gt; { SeriesName = "data 2" };
    
    // Create Stacked Mountain Series
    var rSeries1 = new SCIStackedMountainRenderableSeries
    {
        DataSeries = ds1,
        StrokeStyle = SCISolidPenStyle(colorCode: 0xFFffffff, thickness: 1),
        AreaStyle = new SCILinearGradientBrushStyle(new CGPoint(0, 0), new CGPoint(1, 1), 0xDDDBE0E1, 0x88B6C1C3)
    };
    
    var rSeries2 = new SCIStackedMountainRenderableSeries
    {
        DataSeries = ds2,
        StrokeStyle = SCISolidPenStyle(colorCode: 0xFFffffff, thickness: 1),
        AreaStyle = new SCILinearGradientBrushStyle(new CGPoint(0, 0), new CGPoint(1, 1), 0xDDACBCCA, 0x88439AAF)
    };

    // Create and Fill Stacked Series Collection
    var seriesCollection = new SCIVerticallyStackedMountainsCollection();
    seriesCollection.Add(rSeries1);
    seriesCollection.Add(rSeries2);

    surface.RenderableSeries.Add(seriesCollection);
</div>

## 100% Stacked Mountains
Similarly to [Stacked Column Series](2d-chart-types---stacked-column-series.html) in SciChart it is possible to have **Stacked Mountains**, which fills all available vertical space. This mode is called **100% Stacked Mountains**.

To use it on your `SCIVerticallyStackedMountainsCollection`, just change the corresponding property of your collection:
- `SCIVerticallyStackedSeriesCollection.isOneHundredPercent`

![100% Stacked Mountain Series](img/chart-types-2d/stacked-100-percent-mountain-chart-example.png)
