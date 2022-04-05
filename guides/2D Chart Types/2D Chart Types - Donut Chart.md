# The Donut Chart Type
In SciChart, the **Donut Chart** type is represented by the `SCIPieChartSurface` class.

The `SCIDonutRenderableSeries` is very similar to the [Pie Chart](2d-chart-types---pie-chart.html), except it has a round hole in its center.
A `SCIPieSegment` represents a percentage that corresponds to a particular value. 
This value appears drawn on every segment and can be set in code. 

![Donut Chart Type](img/chart-types-2d/donut-chart.example.png)

> **_NOTE:_** Examples for the Donut Chart can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-donut-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-donut-chart-example/)

The `SCIPieSegment` allows you to specify different styles to control rendering of the segments, e.g.:
- `ISCIPieSegment.fillStyle`
- `ISCIPieSegment.strokeStyle`
- `ISCIPieSegment.titleStyle`
- `ISCIPieSegment.selectedSegmentStyle`

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

Also, you can control whether to draw labels on segments or not via the `ISCIPieRenderableSeries.drawLabels` property.

## Create a Donut Chart
To create a **Donut Chart**, you have to provide a `SCIPieRenderableSeriesCollection` and assign it to `SCIPieChartSurface.renderableSeries` property. 
The data source is a collection of objects that conforms to the `ISCIPieRenderableSeries` protocol, which contains a list of `SCIPieSegment` instances to draw. 

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    SCIPieChartSurface pieChartSurface;
    pieChartSurface.holeRadius = 100;
    pieChartSurface.holeRadiusSizingMode = SCISizingMode_Absolute;

    // Create Donut Series and fill it with Segments
    SCIDonutRenderableSeries *donutSeries = [SCIDonutRenderableSeries new];
    [donutSeries.segmentsCollection add:[self segmentWithValue:40 title:@"Green" centerColor:0xff84BC3D edgeColor:0xff5B8829]];
    [donutSeries.segmentsCollection add:[self segmentWithValue:10 title:@"Red" centerColor:0xffe04a2f edgeColor:0xffB7161B]];
    [donutSeries.segmentsCollection add:[self segmentWithValue:20 title:@"Blue" centerColor:0xff4AB6C1 edgeColor:0xff2182AD]];
    [donutSeries.segmentsCollection add:[self segmentWithValue:15 title:@"Yellow" centerColor:0xffFFFF00 edgeColor:0xfffed325]];
    
    [pieChartSurface.renderableSeries add:donutSeries];
    
    - (SCIPieSegment *)segmentWithValue:(double)segmentValue title:(NSString *)title centerColor:(unsigned int)centerColor edgeColor:(unsigned int)edgeColor {
        SCIPieSegment *segment = [SCIPieSegment new];
        segment.value = segmentValue;
        segment.title = title;
        segment.fillStyle = [[SCIRadialGradientBrushStyle alloc] initWithCenterColorCode:centerColor edgeColorCode:edgeColor];
            
        return segment;
    }
    
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let pieChartSurface: SCIPieChartSurface
    pieChartSurface.holeRadius = 100
    
    // Create Donut Series and fill it with Segments
    let donutSeries = SCIDonutRenderableSeries()
    donutSeries.segmentsCollection.add(segmentWithValue(segmentValue: 40, title: "Green", centerColor: 0xff84BC3D, edgeColor: 0xff5B8829))
    donutSeries.segmentsCollection.add(segmentWithValue(segmentValue: 10, title: "Red", centerColor: 0xffe04a2f, edgeColor: 0xffB7161B))
    donutSeries.segmentsCollection.add(segmentWithValue(segmentValue: 20, title: "Blue", centerColor: 0xff4AB6C1, edgeColor: 0xff2182AD))
    donutSeries.segmentsCollection.add(segmentWithValue(segmentValue: 15, title: "Yellow", centerColor: 0xffFFFF00, edgeColor: 0xfffed325))
    
    pieChartSurface.renderableSeries.add(donutSeries)
    
    func segmentWithValue(segmentValue: Double, title: String, centerColor: UInt32, edgeColor: UInt32) -> SCIPieSegment {
        let segment = SCIPieSegment()
        segment.value = segmentValue
        segment.title = title
        segment.fillStyle = SCIRadialGradientBrushStyle(centerColorCode: centerColor, edgeColorCode: edgeColor)
        
        return segment
    }
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    SCIPieChartSurface pieChartSurface;
    pieChartSurface.HoleRadius = 100;
    pieChartSurface.HoleRadiusSizingMode = SCISizingMode.Absolute;

    // Create Donut Series and fill it with Segments
    var donutSeries = new SCIDonutRenderableSeries
    {
        SegmentsCollection = new SCIPieSegmentCollection 
        {
            new SCIPieSegment { Value = 40, Title = "Green", FillStyle = new SCIRadialGradientBrushStyle(0xff84BC3D, 0xff5B8829) },
            new SCIPieSegment { Value = 10, Title = "Red", FillStyle = new SCIRadialGradientBrushStyle(0xffe04a2f, 0xffB7161B) },
            new SCIPieSegment { Value = 20, Title = "Blue", FillStyle = new SCIRadialGradientBrushStyle(0xff4AB6C1, 0xff2182AD) },
            new SCIPieSegment { Value = 15, Title = "Yellow", FillStyle = new SCIRadialGradientBrushStyle(0xffFFFF00, 0xfffed325) },
        },
    };

    pieChartSurface.RenderableSeries.Add(donutSeries);
</div>

## Changing the size of the Donut Chart
If you want to change the size of the **Donut Chart** you can use a combination of the following properties:
- `ISCIPieRenderableSeries.height`
- `ISCIPieRenderableSeries.heightSizingMode`

The above allows you to specify how much space the **Donut Chart** should use for its rendering. 
If you use **Absolute** mode then `ISCIPieRenderableSeries.height` accepts size in pixels, and if you use **Relative** mode, it expects value from 0 to 1, which tells how much of the available space it should use for rendering (e.g. 0.5 will tell Donut series to use 50% of available space).

Also, you can control the center hole size via the `ISCIPieChartSurface.holeRadius` property.

## SCIPieChartSurface Modifiers
The `SCIPieChartSurface` supports modifiers like [Legend](#donut-chart-legend), [Selection](#donut-chart-selection), and [Tooltip](#donut-chart-tooltip).

![Donut Chart Modifiers](img/chart-types-2d/donut-chart-modifiers-example.png)

**Legend** and **Selection** modifiers are synced if both are added. A `SCIPieSegment` can be selected by clicking on either of them (via the [Selection Modifier](#donut-chart-selection)) or the corresponding item in the [Legend](#donut-chart-legend). 
This action provides visual feedback on the chart and the Legend.

#### Donut Chart Legend
To add the `SCIPieChartLegendModifier`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIPieChartLegendModifier *legendModifier = [SCIPieChartLegendModifier new];
    legendModifier.sourceSeries = donutSeries;
    legendModifier.margins = UIEdgeInsetsMake(17, 17, 17, 17);
    legendModifier.position = SCIAlignment_Bottom | SCIAlignment_CenterHorizontal;

    [pieChartSurface.chartModifiers add:legendModifier];
</div>
<div class="code-snippet" id="swift">
    let legendModifier = SCIPieChartLegendModifier()
    legendModifier.sourceSeries = donutSeries;
    legendModifier.margins = UIEdgeInsets(top: 17, left: 17, bottom: 17, right: 17)
    legendModifier.position = [.bottom, .centerHorizontal]

    pieChartSurface.chartModifiers.add(legendModifier)`
</div>
<div class="code-snippet" id="cs">
    pieChartSurface.ChartModifiers.Add(new SCIPieChartLegendModifier
    {
        SourceSeries = donutSeries,
        Position = SCIAlignment.Bottom | SCIAlignment.CenterHorizontal,
        Margins = new UIEdgeInsets(17, 17, 17, 17),
    });
</div>

> **_NOTE:_** `SCIPieChartLegendModifier` works similar to `SCILegendModifier` and has a similar API. To learn more, please visit the [SCILegendModifier usage](legend-modifier.html) article.

#### Donut Chart Selection
To add the `SCIPieSegmentSelectionModifier`, please use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIPieSegmentSelectionModifier *selectionModifier = [SCIPieSegmentSelectionModifier new];
    [pieChartSurface.chartModifiers add:selectionModifier];
</div>
<div class="code-snippet" id="swift">
    let selectionModifier = SCIPieSelectionModifier()
    pieChartSurface.chartModifiers.add(selectionModifier)
</div>
<div class="code-snippet" id="cs">
    pieChartSurface.ChartModifiers.Add(new SCIPieSegmentSelectionModifier());
</div>

> **_NOTE:_** `SCIPieSegmentSelectionModifier` works similar to `SCISeriesSelectionModifier` and has a similar API. To learn more, please visit the [SCISeriesSelectionModifier usage](interactivity---sciseriesselectionmodifier.html) article.

#### Donut Chart Tooltip

The `SCIPieChartTooltipModifier` allows inspecting `SCIPieDonutRenderableSeriesBase.segmentsCollection` at a touch point. To add the `SCIPieChartTooltipModifier`, use the following code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [pieChartSurface.chartModifiers add:[SCIPieChartTooltipModifier new]];
</div>
<div class="code-snippet" id="swift">
    pieChartSurface.chartModifiers.add(SCIPieChartTooltipModifier())
</div>
<div class="code-snippet" id="cs">
    pieChartSurface.ChartModifiers.Add(new SCIPieChartTooltipModifier());
</div>

> **_NOTE:_** `SCIPieChartTooltipModifier` works similar to `SCITooltipModifier` and has a similar API. To learn more, please visit [SCITooltipModifier Usage](interactivity---scitooltipmodifier.html) article.

## Pie Segment Label Formatter
By default, the Pie Segment Label displays a relative percentage calculated on values of all segments in `SCIPieDonutRenderableSeriesBase.segmentsCollection`. This behavior can be controlled and to do so you’ll need to subclass `SCIPieSegmentLabelFormatterBase` and provide your custom data in `-[ISCIPieSegmentLabelFormatter formatLabelForPieSegment:]` method. As an example, let's create a label that displays a segment absolute value. Assume, we create a donutSeries and add four segments with values 40, 10, 20, and 15. Here is the code sample, how to do this:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // CustomPieSegmentLabelFormatter.h
    @interface CustomPieSegmentLabelFormatter : SCIPieSegmentLabelFormatterBase
    @end

    // CustomPieSegmentLabelFormatter.m
    @implementation CustomPieSegmentLabelFormatter

    - (NSString *)formatLabelForPieSegment:(id<ISCIPieSegment>)pieSegment {
        return [NSString stringWithFormat:@"%f", pieSegment.value];
    }

    @end
    
    // Assume a donutSeries has been created somewhere
    SCIDonutRenderableSeries *donutSeries = [SCIDonutRenderableSeries new];
    donutSeries.pieSegmentLabelFormatter = [CustomPieSegmentLabelFormatter new];
    
</div>
<div class="code-snippet" id="swift">
    class CustomPieSegmentLabelFormatter: SCIPieSegmentLabelFormatterBase {
        override func formatLabel(for pieSegment: ISCIPieSegment!) -> String! {
            return "\(pieSegment.value)"
        }
    }
    
    // Assume a donutSeries has been created somewhere
    let donutSeries = SCIDonutRenderableSeries()
    donutSeries.pieSegmentLabelFormatter = CustomPieSegmentLabelFormatter()
</div>
<div class="code-snippet" id="cs">
    class CustomPieSegmentLabelFormatter : SCIPieSegmentLabelFormatterBase
    {
        public override string FormatLabel(IISCIPieSegment pieSegment)
        {
            return pieSegment.Value.ToString();
        }
    }
    
    // Assume a donutSeries has been created somewhere
    var donutSeries = new SCIDonutRenderableSeries;
    donutSeries.PieSegmentLabelFormatter = new CustomPieSegmentLabelFormatter();
    
</div>

This produces the following output:

![Donut Series with custom label formatter](img/chart-types-2d/donut-chart-custom-formatter.png)

## Multi Pie Donut Chart
In SciChart you can have both [Pie Chart]() and [Donut Chart]() placed inside the `SCIPieChartSurface` at the same time.

![Multi Pie Donut](img/chart-types-2d/pie-donut-modifiers-example.png)

> **_NOTE:_** Examples of the Multi Pie Donut Chart can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-nested-pie-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-nested-pie-chart-example/)
