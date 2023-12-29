# The Column Series Type
Column Chart is provided by the `SCIFastColumnRenderableSeries` class. It accepts data (`X, Y`) from a `SCIXyDataSeries` and renders a **column** at each `[X, Y]` value.

> **_NOTE:_** Examples for the Column Series can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-column-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-stacked-column-chart-example/)

The `SCIFastColumnRenderableSeries` class allows to specify **Fill** brush, **Stroke** pen and relative **DataPointWidth** which will be applied to every column. You can also choose the `SCITextureMappingMode` for Fill brush which defines how columns are filled when a gradient is used. The **DataPointWidth** specifies how much space a single column occupies, varying from 0 to 1 (when columns are conjoined). 

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

Also, it is possible to define the **ZeroLineY** baseline position via the `SCIRenderableSeriesBase.zeroLineY` property. All data points that have Y value less than **ZeroLineY** will appear downward, else - upward.

All those values can be assigned via the corresponding properties:
- `SCIBaseColumnRenderableSeries.fillBrushStyle`
- `SCIBaseColumnRenderableSeries.fillBrushMappingMode`
- `ISCIRenderableSeries.strokeStyle`
- `SCIBaseColumnRenderableSeries.dataPointWidth`
- `SCIRenderableSeriesBase.zeroLineY`

> **_NOTE:_** In multi axis scenarios, a series has to be assigned to **particular X and Y axes**. This can be done passing the axes IDs to the `ISCIRenderableSeries.xAxisId`, `ISCIRenderableSeries.yAxisId` properties.

## Create a Column Series
![Column Series Type](img/chart-types-2d/column-chart-example.png)

To create a **Column Series**, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;
    // Create DataSeries and fill it with some data
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double];

    // Create and add Mountain Series
    id&lt;ISCIRenderableSeries&gt; columnSeries = [SCIFastColumnRenderableSeries new];
    columnSeries.dataSeries = dataSeries;
    columnSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFFA99A8A thickness:1.0];
    columnSeries.fillBrushStyle = [[SCILinearGradientBrushStyle alloc] initWithStart:CGPointMake(0, 0) end:CGPointMake(0, 1) startColorCode:0xFF4682B4 endColorCode:0xFFB0C4DE];
    [surface.renderableSeries add:columnSeries];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface
    // Create DataSeries and fill it with some data
    let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)

    // Create and add Mountain Series
    let columnSeries = SCIFastColumnRenderableSeries()
    columnSeries.dataSeries = dataSeries
    columnSeries.strokeStyle = SCISolidPenStyle(colorCode: 0xFFA99A8A, thickness: 1)
    columnSeries.fillBrushStyle = SCILinearGradientBrushStyle(start: CGPoint(x: 0, y: 0), end: CGPoint(x: 0, y: 1), startColorCode: 0xFF4682B4, endColorCode: 0xFFB0C4DE)
    surface.renderableSeries.add(columnSeries)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;
    // Create DataSeries and fill it with some data
    var dataSeries = new XyDataSeries&lt;double, double&gt;();
    
    // Create and add Mountain Series
    var columnSeries = new SCIFastColumnRenderableSeries();
    columnSeries.DataSeries = dataSeries;
    columnSeries.StrokeStyle = new SCISolidPenStyle(0xFFA99A8A, 1f);
    columnSeries.FillBrushStyle = new SCILinearGradientBrushStyle(new CGPoint(0, 0), new CGPoint(0, 1), 0xFF4682B4, 0xFFB0C4DE);
    surface.RenderableSeries.Add(columnSeries);
</div>

## Column Series Features
Column Series also has some features similar to other series, such as:
- [Render a Gap](#render-a-gap-in-a-column-series);
- [Draw Series with Different Colors](#paint-column-area-parts-with-different-colors).

#### Render a Gap in a Column Series
It's possible to render a Gap in **Column series**, by passing a data point with a `NaN` as the `Y` value. Please refer to the [RenderableSeries APIs](2D Chart Types.html#adding-a-gap-onto-a-renderableseries) article for more details.

#### Paint Column Area Parts With Different Colors
![Paletted Column Series Type](img/chart-types-2d/paletted-column-chart-example.png)

In SciChart, you can draw each column of the **Column Series** with different colors using the [PaletteProvider API](paletteprovider-api.html). 
To use palette provider for Columns - a custom `ISCIFillPaletteProvider` (or `ISCIStrokePaletteProvider`) has to be provided to the `ISCIRenderableSeries.paletteProvider` property. 

Please see the code snippet from our [Column Chart Example](https://www.scichart.com/example/ios-column-chart-demo/) below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCIPaletteProviderBase+Protected.h&gt;

    @interface ColumnsTripleColorPalette : SCIPaletteProviderBase&lt;SCIFastColumnRenderableSeries *&gt;&lt;ISCIFillPaletteProvider&gt;
    @end

    @implementation ColumnsTripleColorPalette {
        SCIUnsignedIntegerValues *_colors;
    }

    static unsigned int desiredColors[] = {0xFFa9d34f, 0xFFfc9930, 0xFFd63b3f};

    - (instancetype)init {
        self = [super initWithRenderableSeriesType:SCIFastColumnRenderableSeries.class];
        if (self) {
            _colors = [SCIUnsignedIntegerValues new];
        }
        return self;
    }

    - (void)update {
        NSInteger count = self.renderableSeries.currentRenderPassData.pointsCount;
        _colors.count = count;
        
        unsigned int *colorsArray = _colors.itemsArray;
        for (NSInteger i = 0; i < count; i++) {
            colorsArray[i] = desiredColors[i % 3];
        }
    }

    - (SCIUnsignedIntegerValues *)fillColors {
        return _colors;
    }

    @end
    ...
    columnSeries.paletteProvider = [ColumnsTripleColorPalette new];
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCIPaletteProviderBase

    class ColumnsTripleColorPalette : SCIPaletteProviderBase&lt;SCIFastColumnRenderableSeries&gt;, ISCIFillPaletteProvider {
        
        let colors = SCIUnsignedIntegerValues()
        let desiredColors:[UInt32] = [0xffa9d34f, 0xfffc9930, 0xffd63b3f]
        
        override init!(renderableSeriesType: AnyClass!) {
            super.init(renderableSeriesType: renderableSeriesType)
        }
        
        init() {
            super.init(renderableSeriesType: SCIFastColumnRenderableSeries.self)
        }
        
        override func update() {
            let count = renderableSeries.currentRenderPassData.pointsCount
            colors.count = count
            
            for i in 0 ..< count {
                colors.set(desiredColors[i % 3], at: i)
            }
        }
        
        var fillColors: SCIUnsignedIntegerValues! { return colors }
    }
    ...
    columnSeries.paletteProvider = ColumnsTripleColorPalette()
</div>
<div class="code-snippet" id="cs">
    private class ColumnPaletteProvider : SCIPaletteProviderBase&lt;SCIFastColumnRenderableSeries&gt;, IISCIFillPaletteProvider
    {
        private readonly uint[] _desiredColors = { 0xFFa9d34f, 0xFFfc9930, 0xFFd63b3f };

        public override void Update()
        {
            FillColors.Clear();
            var pointsCount = RenderableSeries.CurrentRenderPassData.PointsCount;
            for (int i = 0; i < pointsCount; i++)
            {
                FillColors.Add(_desiredColors[i % 3]);
            }
        }

        public SCIUnsignedIntegerValues FillColors { get; } = new SCIUnsignedIntegerValues();
    }
    ...
    columnSeries.PaletteProvider = new ColumnPaletteProvider();
</div>

For more information - please refer to the [PaletteProvider API](paletteprovider-api.html) article.
