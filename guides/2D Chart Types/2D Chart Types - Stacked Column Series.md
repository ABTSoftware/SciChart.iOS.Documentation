# The Stacked Column Series Type
The **Stacked Column** series can be created by using `SCIStackedColumnRenderableSeries` and one of the provided collections, which are used to stack columns **Horizontally** or **Vertically**.
Stacked Column series shares most of its properties with the regular **[Column Series](2d-chart-types---column-series.html)**, in addition the columns are automatically vertically **stacked** (the Y-Values are aggregated) or horizontally **grouped**.

There are 2 available collections to work with `SCIStackedColumnRenderableSeries`:
- `SCIVerticallyStackedColumnsCollection` - allows to stack columns vertically;
- `SCIHorizontallyStackedColumnsCollection` - allows to stack (group) columns horizontally.

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done by passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

![Stacked Column Series Type](img/chart-types-2d/stacked-column-chart-example.png)

> **_NOTE:_** Examples of the Stacked Column Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-chart-stacked-column-chart-example/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-stacked-column-chart-example/)

## How the Stacking Works for Column Series
For **Stacked Column Series**, it's possible to perform either [Vertical](#vertical-stacking) or [Horizontal](#horizontal-stacking) stacking, or even **both** at the same time.

#### Vertical Stacking
Vertical Stacking of `ISCIStackedColumnRenderableSeries` is handled by special renderableSeries - `SCIVerticallyStackedColumnsCollection`.

Basically, it's a simple `SCIObservableCollection` of **Stacked Column Series**, where the order of items in it determines how series should be stacked and drawn - the first item will be drawn as regular column series and the rest will be stacked on top of each other. 

If you want to have several sets of **Stacked Column Series** which should be stacked independently, all you need to do is to create the corresponding amount of `SCIVerticallyStackedColumnsCollection` which will hold appropriate `ISCIStackedColumnRenderableSeries`.

#### Horizontal Stacking (Grouping)
Horizontal Stacking of **Stacked Column Series** is very similar to the [Vertical](#vertical-stacking) one. The only difference is that it stacks (groups) its items horizontally. 
By default it supports stacking of any `ISCIStackedColumnRenderableSeries` implementor.

For horizontal Stacking, you can define spacing, which depends on spacingMode, as well as whole group dataPointWidth using the following properties:

- `SCIHorizontallyStackedColumnsCollection.spacing` - if you use **Absolute** mode then it accepts value in pixels, and if you use **Relative** mode - it expects value from `0 to 1`, which tells how much of the available space it should use relatively to each column size;
- `SCIHorizontallyStackedColumnsCollection.spacingMode` - Absolute or Relative modes are available through `SCISpacingMode`;
- `SCIHorizontallyStackedColumnsCollection.dataPointWidth` - specifies how much space the whole stacked group occupies, varying from 0 to 1 (when columns are conjoined). 

If you need to have both **vertical** and **horizontal** stacking at the same time, just use `SCIVerticallyStackedColumnsCollection` (which conforms to `ISCIStackedColumnRenderableSeries`) inside your `SCIHorizontallyStackedColumnsCollection`.

## Create a Stacked Column Series 
To create a **Stacked Column Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Helper method to create a SCIStackedColumnRenderableSeries instances.
    - (SCIStackedColumnRenderableSeries *)getRenderableSeriesWithDataSeries:(SCIXyDataSeries *)dataSeries FillColor:(unsigned int)fillColor {
        SCIStackedColumnRenderableSeries *rSeries = [SCIStackedColumnRenderableSeries new];
        rSeries.dataSeries = dataSeries;
        rSeries.fillBrushStyle = [[SCISolidBrushStyle alloc] initWithColorCode:fillColor];
        rSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:fillColor thickness:1.0];
        
        return rSeries;
    }
    ...
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *ds1 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    SCIXyDataSeries *ds2 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    SCIXyDataSeries *ds3 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    SCIXyDataSeries *ds4 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];
    SCIXyDataSeries *ds5 = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];

    // Create and Fill Stacked Series Collections
    SCIVerticallyStackedColumnsCollection *verticalCollection1 = [SCIVerticallyStackedColumnsCollection new];
    [verticalCollection1 add:[self getRenderableSeriesWithDataSeries:ds1 FillColor:0xff226fb7]];
    [verticalCollection1 add:[self getRenderableSeriesWithDataSeries:ds2 FillColor:0xffff9a2e]];

    SCIVerticallyStackedColumnsCollection *verticalCollection2 = [SCIVerticallyStackedColumnsCollection new];
    [verticalCollection2 add:[self getRenderableSeriesWithDataSeries:ds3 FillColor:0xffdc443f]];
    [verticalCollection2 add:[self getRenderableSeriesWithDataSeries:ds4 FillColor:0xffaad34f]];
    [verticalCollection2 add:[self getRenderableSeriesWithDataSeries:ds5 FillColor:0xff8562b4]];
    
    SCIHorizontallyStackedColumnsCollection *columnCollection = [SCIHorizontallyStackedColumnsCollection new];
    [columnCollection add:verticalCollection1];
    [columnCollection add:verticalCollection2];
    
    [surface.renderableSeries add:columnCollection];
</div>
<div class="code-snippet" id="swift">
    // Helper method to create a SCIStackedColumnRenderableSeries instances.
    fileprivate func getRenderableSeriesWith(dataSeries: SCIXyDataSeries, fillColor: UInt32) -> SCIStackedColumnRenderableSeries {
        let rSeries = SCIStackedColumnRenderableSeries()
        rSeries.dataSeries = dataSeries
        rSeries.fillBrushStyle = SCISolidBrushStyle(colorCode: fillColor)
        rSeries.strokeStyle = SCISolidPenStyle(colorCode: fillColor, thickness: 1.0)
        
        return rSeries
    }
    ...
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let ds1 = SCIXyDataSeries(xType: .double, yType: .double)
    let ds2 = SCIXyDataSeries(xType: .double, yType: .double)
    let ds3 = SCIXyDataSeries(xType: .double, yType: .double)
    let ds4 = SCIXyDataSeries(xType: .double, yType: .double)
    let ds5 = SCIXyDataSeries(xType: .double, yType: .double)

    // Create and Fill Stacked Series Collection
    let verticalCollection1 = SCIVerticallyStackedColumnsCollection()
    verticalCollection1.add(getRenderableSeriesWith(dataSeries: ds1, fillColor: 0xff226fb7))
    verticalCollection1.add(getRenderableSeriesWith(dataSeries: ds2, fillColor: 0xffff9a2e))
    
    let verticalCollection2 = SCIVerticallyStackedColumnsCollection()
    verticalCollection2.add(getRenderableSeriesWith(dataSeries: ds3, fillColor: 0xffdc443f))
    verticalCollection2.add(getRenderableSeriesWith(dataSeries: ds4, fillColor: 0xffaad34f))
    verticalCollection2.add(getRenderableSeriesWith(dataSeries: ds5, fillColor: 0xff8562b4))
    
    let columnCollection = SCIHorizontallyStackedColumnsCollection()
    columnCollection.add(verticalCollection1)
    columnCollection.add(verticalCollection2)

    surface.renderableSeries.add(columnCollection)
</div>
<div class="code-snippet" id="cs">
    // Helper method to create a SCIStackedColumnRenderableSeries instances.
    private SCIStackedColumnRenderableSeries GetRenderableSeries(IDataSeries dataSeries, uint strokeColor, uint fillColor)
    {
        return new SCIStackedColumnRenderableSeries
        {
            DataSeries = dataSeries,
            FillBrushStyle = new SCISolidBrushStyle(fillColor),
            StrokeStyle = new SCISolidPenStyle(strokeColor, 1f)
        };
    }
    ...
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var ds1 = new XyDataSeries&lt;double, double&gt; { SeriesName = "Pork Series" };
    var ds2 = new XyDataSeries&lt;double, double&gt; { SeriesName = "Veal Series" };
    var ds3 = new XyDataSeries&lt;double, double&gt; { SeriesName = "Tomato Series" };
    var ds4 = new XyDataSeries&lt;double, double&gt; { SeriesName = "Cucumber Series" };
    var ds5 = new XyDataSeries&lt;double, double&gt; { SeriesName = "Pepper Series" };

    // Create and Fill Stacked Series Collection
    var verticalCollection1 = new SCIVerticallyStackedColumnsCollection();
    verticalCollection1.Add(porkSeries);
    verticalCollection1.Add(vealSeries);

    var verticalCollection2 = new SCIVerticallyStackedColumnsCollection();
    verticalCollection2.Add(tomatoSeries);
    verticalCollection2.Add(cucumberSeries);
    verticalCollection2.Add(pepperSeries);

    var columnsCollection = new SCIHorizontallyStackedColumnsCollection();
    columnsCollection.Add(verticalCollection1);
    columnsCollection.Add(verticalCollection2);

    surface.RenderableSeries.Add(columnCollection);
</div>

## 100% Stacked Columns
Similarly to [Stacked Mountain Series](2d-chart-types---stacked-mountain-series.html) in SciChart - it is possible to have **Stacked Columns**, that fills in all available vertical space. This mode is called **100% Stacked Columns**.

To use it on your `SCIVerticallyStackedColumnsCollection`, just change the corresponding property of your collection to `SCIVerticallyStackedSeriesCollection.isOneHundredPercent`

![100% Stacked Column Series](img/chart-types-2d/stacked-100-percent-column-chart-example.png)
