# The SCIChartOverview
The `SCIChartOverview` Control is a component which can be used for navigating a 2D chart. It behaves like a minimap of the chart. The SciChartOverview is a separate chart which uses the original chart for configuration and displays the full range of it's data.

Benefits of the SCIChartOverview:

- Displays an overview of the whole chart
- Allows you to select the visible range that should be displayed by dragging & resizing an element on the overview control
- Allows instantly scrolling to a specified range by clicking on the overview
- Has an ability to transform renderable series copied from the original chart before displaying

## Adding SCIChartOverview via the .storyboard
Add UIView onto the ViewController and set it’s class to the `SCIChartOverview`. Then add an IBOutlet for your SCIChartSurface in your ViewController code to be able to manipulate with it later on.

![SCIChartOverview storyboard](img/modifiers-2d/overviewChart-storyboard.png)

## Adding SCIChartOverview purely from 
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
 To create an overview for the chart, call the method `-[SCIOverviewChart p_SCI_addOverviewChartForParentSurface:xAxisType:yAxisType:]`, passing the parent surface (an instance of the surface for which the overview needs to be drawn), along with the X and Y axis types of the parent chart.
 
  <div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

    __weak typeof(self) wSelf = self;
    dispatch_async(dispatch_get_main_queue(), ^{
        [overviewChart p_SCI_addOverviewChartForParentSurface:wSelf.surface xAxisType:SCIAxisType_CategoryDateAxis yAxisType:SCIAxisType_NumericalAxis];
    });
</div>
<div class="code-snippet" id="swift">

    DispatchQueue.main.async { [weak self] in
            guard let surface = self?.surface else {return}
            
            overviewChart1.p_SCI_addChart(forParentSurface: surface, xAxisType: .categoryDateAxis, yAxisType: .numericalAxis)
        }
</div>