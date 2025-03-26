# The SCIChartOverview
The `SCIChartOverview` Control is a component which can be used for navigating a 2D chart. It behaves like a minimap of the chart. The SciChartOverview is a separate chart which uses the original chart for configuration and displays the full range of it's data.

Benefits of the SCIChartOverview:

- Displays an overview of the whole chart
- Allows you to select the visible range that should be displayed by dragging & resizing an element on the overview control
- Allows instantly scrolling to a specified range by tapping on the overview

## Adding SCIChartOverview via the .storyboard
Add UIView onto the ViewController and set it’s class to the `SCIChartOverview`. Then add an IBOutlet for your SCIChartSurface in your ViewController code to be able to manipulate with it later on.

![SCIChartOverview storyboard](img/modifiers-2d/overviewChart-storyboard.png)

## Adding SCIChartOverview purely from code
In your ViewController you will need to import `SciChart` and instantiate the `SCIChartOverview`. See the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

    #import &lt;SciChart/SciChart.h&gt;
    ...
    - (void)viewDidLoad {
        [super viewDidLoad];
        
        SCIChartOverview *overviewChart = [SCIChartOverview new];
        overviewChart.translatesAutoresizingMaskIntoConstraints = NO;
        [self.view addSubview:overviewChart];
        
        [overviewChart.topAnchor constraintEqualToAnchor:self.surface.bottomAnchor].active = YES;
        [overviewChart.bottomAnchor constraintEqualToAnchor:self.view.safeAreaLayoutGuide.bottomAnchor].active = YES;
        [overviewChart.leftAnchor constraintEqualToAnchor:self.surface.leftAnchor].active = YES;
        [overviewChart.rightAnchor constraintEqualToAnchor:self.surface.rightAnchor].active = YES;
        [overviewChart.heightAnchor constraintEqualToConstant:100].active = YES;

    }

</div>
<div class="code-snippet" id="swift">
    import SciChart 
    ...
    override func viewDidLoad() {
        super.viewDidLoad()
        
        let overviewChart = SCIChartOverview()
        overviewChart.translatesAutoresizingMaskIntoConstraints = false
        self.view.addSubview(overviewChart)
        overviewChart.topAnchor.constraint(equalTo: self.view.safeAreaLayoutGuide.topAnchor).isActive = true
        overviewChart.bottomAnchor.constraint(equalTo: self.view.safeAreaLayoutGuide.bottomAnchor).isActive = true
        overviewChart.leftAnchor.constraint(equalTo: self.view.leftAnchor).isActive = true
        overviewChart.rightAnchor.constraint(equalTo: self.view.rightAnchor).isActive = true
        overviewChart.heightAnchor.constraint(equalToConstant: 100).isActive = true
    }
</div>

  ## Create Overview
 To create an overview for the chart, call the method `-[SCIOverviewChart createOverviewChartForParentSurface:]`, passing the parent surface (an instance of the surface for which the overview needs to be drawn).
 
  <div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

    __weak typeof(self) wSelf = self;
    dispatch_async(dispatch_get_main_queue(), ^{
        [wSelf.overviewChart createOverviewChartForParentSurface:wSelf.mainSurface];
    });
</div>
<div class="code-snippet" id="swift">

    DispatchQueue.main.async { [weak self] in
            guard let surface = self?.surface else {return}
            self?.overviewChart.createOverviewChart(forParentSurface: surface)
        }
</div>

## Adding Zoom / Pan Modifiers to demonstrate the overview
Dragging or resizing the selection area on the overview will automatically update the visible range of the main chart, and zooming/panning the main chart will update the selection on the overview.

To demonstrate this, let's add some zoom / pan modifiers to the chart.
  <div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
...
    [self.mainSurface.chartModifiers addAll:[SCIZoomPanModifier new], [SCIPinchZoomModifier new], [SCIZoomExtentsModifier new], nil];
</div>
<div class="code-snippet" id="swift">
...
   surface.chartModifiers.add(items: SCIZoomPanModifier(), SCIPinchZoomModifier(), SCIZoomExtentsModifier())
</div>

## Customizing the Selection and Range Annotations
SciChart Overview also allows you to specify a custom view for the selection control by initializing the overview chart with gripView. And selected range color can be modified using the `fillBrush` propety of the `SCIChartOverview`.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
...
    __weak typeof(self) wSelf = self;
    dispatch_async(dispatch_get_main_queue(), ^{
        [wSelf.overviewChart createOverviewChartForParentSurface:wSelf.mainSurface gripView:customView options:[SCIOverviewOptions new]];
        wSelf.overviewChart.fillBrush = [[SCISolidBrushStyle alloc] initWithColorCode:0x33A4EBC6];
    });
</div>
<div class="code-snippet" id="swift">
...
   DispatchQueue.main.async { [weak self] in
            guard let surface = self?.mainSurface else {return}
            self?.overviewChart.createOverviewChart(forParentSurface: surface, grip: customView ?? UIView(), options: SCIOverviewOptions())
            self?.overviewChart.fillBrush = SCISolidBrushStyle(color: 0x33A4EBC6)
        }
</div>

## Optional Parameters for creating SciChartOverview
`-[SCIOverviewChart createOverviewChartForParentSurface:options:]` method accepts optional params object described in `SCIOverviewOptions`. These params allow to specify axis ids and renderanle series which should be used for binding overview chart.

> **_NOTE:_** specifying the SCIOverviewOptions.xAxisId and SCIOverviewOptions.yAxisId is required when you are using custom axis ids (as in case when you have multiple X or Y axes).

Another important parameter is SCIOverviewOptions.renderableSeries, which is used to set a renderable series.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
...
    __weak typeof(self) wSelf = self;
    dispatch_async(dispatch_get_main_queue(), ^{
       
        SCIFastMountainRenderableSeries *rSeries = [SCIFastMountainRenderableSeries new];
        rSeries.dataSeries = self->_ohlcDataSeries;
        
        SCIOverviewOptions *options = [SCIOverviewOptions new];
        options.renderableSeries = rSeries;
        options.xAxisId = @"axis";
        options.yAxisId = @"axis";
        [wSelf.overviewChart createOverviewChartForParentSurface:wSelf.mainSurface gripView:customView options:options];
        
        wSelf.overviewChart.fillBrush = [[SCISolidBrushStyle alloc] initWithColorCode:0x33A4EBC6];
        wSelf.overviewChart.theme = wSelf.mainSurface.theme;
    });
</div>
<div class="code-snippet" id="swift">
...
   DispatchQueue.main.async { [weak self] in
            guard let surface = self?.mainSurface else {return}
            
            let rSeries = SCIFastMountainRenderableSeries()
            rSeries.dataSeries = self?._ohlcDataSeries
            
            let options = SCIOverviewOptions()
            let renderableSeries = rSeries
            options.renderableSeries = renderableSeries
            options.xAxisId = "axis"
            options.yAxisId = "axis"
            
            self?.overviewChart.createOverviewChart(forParentSurface: surface, grip: customView ?? UIView(), options: options)
            self?.overviewChart.fillBrush = SCISolidBrushStyle(color: 0x33A4EBC6)
            self?.overviewChart.theme = surface.theme
        }
</div>

![Overview Optional Param](img/modifiers-2d/overviewChartOptionalParam.png)

## Update the chart overview
To refresh or replace the current chart overview, you can use the method `-[SCIChartOverview updateOverviewChartForParentSurface:series:]`. This method updates the existing overview by setting a new SCIChartSurface and associated renderable series.

If you also need to update the grip view in addition to the chart surface and series, use the method `-[SCIChartOverview updateOverviewChartForParentSurface:series:gripView:]`. This version of the method allows you to pass an updated grip view along with the chart surface and series, ensuring that all relevant components of the chart overview are refreshed.

## Using the SciChartOverview in a Vertical Chart
To create a vertical chart, set the axis alignment as follows: align the xAxis to the left and the yAxis to the bottom of the chart. Additionally, you may need to adjust the positioning of the overview container view to fit your specific layout requirements. These adjustments will convert the chart into a vertical orientation, enabling the overview to be resizable and movable in the vertical direction.

![Overview Vertical Chart](img/modifiers-2d/overviewVerticalChart.png)
