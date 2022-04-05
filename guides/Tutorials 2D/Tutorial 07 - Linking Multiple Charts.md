# SciChart iOS Tutorial - Linking Multiple Charts
In our ***series of tutorials***, up until now we have added a chart with two `Y-Axis`, one `X-Axis`, two series, added tooltips, legends and zooming, panning behavior, and added some annotations. All of that was with the only `SCIChartSurface`. 

In SciChart, there is **no restriction** on the number of `SCIChartSurface` you can have in an application.

In the previous tutorial - [Multiple Axis](tutorial-06---multiple-axis.html) - we've manipulated one `SCIChartSurface` instance.
In this tutorial you will learn how to:
- add a **second** `SCIChartSurface` (or potentially unlimited surfaces).
- **link** multiple **charts** and modifiers on them together

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2007%20-%20Linking%20Multiple%20Charts)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-07)

First of all, make sure, you've went through the previous the tutorials, to have a better grasp of SciChart functionality, such as:
- [Tutorial 01 - Create a simple Chart 2D](tutorial-01---create-a-simple-2d-chart.html)
- [Tutorial 05 - Annotations](tutorial-05---annotations.html)
- [Tutorial 06 - Multiple Axis](tutorial-06---multiple-axis.html)

## Adding a Second Chart
Assuming, you've already know how to add one `SCIChartSurface`, it should be fairly easy to add second surface.
Just repeat the same procedure to configure the second chart. We will leave off annotations and modifiers.
Everything else will be the same.

The code below shows how to add two `SCIChartSurface` instance into one view:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    - (void)loadView {
        self.view = [UIView new];
        _surface = [SCIChartSurface new];
        _surface.translatesAutoresizingMaskIntoConstraints = NO;
        _surface2 = [SCIChartSurface new];
        _surface2.translatesAutoresizingMaskIntoConstraints = NO;
        
        [self.view addSubview:_surface];
        [self.view addSubview:_surface2];

        NSDictionary *layoutDictionary = @{@"SciChart1" : _surface, @"SciChart2" : _surface2};
        [self.view addConstraints:[NSLayoutConstraint constraintsWithVisualFormat:@"|-(0)-[SciChart1]-(0)-|" options:0 metrics:nil views:layoutDictionary]];
        [self.view addConstraints:[NSLayoutConstraint constraintsWithVisualFormat:@"|-(0)-[SciChart2]-(0)-|" options:0 metrics:nil views:layoutDictionary]];
        [self.view addConstraints:[NSLayoutConstraint constraintsWithVisualFormat:@"V:|-(0)-[SciChart1(SciChart2)]-(0)-[SciChart2(SciChart1)]-(0)-|" options:0 metrics:nil views:layoutDictionary]];
    }
</div>
<div class="code-snippet" id="swift">
    private let surface = SCIChartSurface()
    private let surface2 = SCIChartSurface()
    
    override func loadView() {
        view = UIView()
        surface.translatesAutoresizingMaskIntoConstraints = false
        surface2.translatesAutoresizingMaskIntoConstraints = false
        
        view.addSubview(surface)
        view.addSubview(surface2)
        
        let layoutDictionary = ["SciChart1" : surface, "SciChart2" : surface2]
        view.addConstraints(NSLayoutConstraint.constraints(withVisualFormat: "|-(0)-[SciChart1]-(0)-|", options: [], metrics: nil, views: layoutDictionary))
        view.addConstraints(NSLayoutConstraint.constraints(withVisualFormat: "|-(0)-[SciChart2]-(0)-|", options: [], metrics: nil, views: layoutDictionary))
        view.addConstraints(NSLayoutConstraint.constraints(withVisualFormat: "V:|-(0)-[SciChart1(SciChart2)]-(0)-[SciChart2(SciChart1)]-(0)-|", options: [], metrics: nil, views: layoutDictionary))
    }
</div>
<div class="code-snippet" id="cs">
    public SCIChartSurface Surface { get; } = new SCIChartSurface { TranslatesAutoresizingMaskIntoConstraints = false };
    public SCIChartSurface Surface2 { get; } = new SCIChartSurface { TranslatesAutoresizingMaskIntoConstraints = false };
    // ...
    public override void LoadView()
    {
        View = new UIView();
        View.AddSubviews(Surface, Surface2);

        var objects = new NSObject[] { Surface, Surface2 };
        var keys = new NSObject[] { new NSString("SciChart1"), new NSString("SciChart2") };
        var layoutDictionary = NSDictionary.FromObjectsAndKeys(objects, keys);

        View.AddConstraints(NSLayoutConstraint.FromVisualFormat("|-(0)-[SciChart1]-(0)-|", 0, null, layoutDictionary));
        View.AddConstraints(NSLayoutConstraint.FromVisualFormat("|-(0)-[SciChart2]-(0)-|", 0, null, layoutDictionary));
        View.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(0)-[SciChart1(SciChart2)]-(0)-[SciChart2(SciChart1)]-(0)-|", 0, null, layoutDictionary));
    }
</div>

> **_NOTE:_** You might want to add your **Surfaces** from the `.storyboard` or `.xib` or whatever else. Constrains in visual format is just for the sake of simplicity.

Now, let's add Axes onto a Surfaces as we did before, the only difference - we extracted some code to omit duplications:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    - (void)setupAxesForSurface:(SCIChartSurface *)surface {
        // Create another numeric axis, right-aligned
        SCINumericAxis *yAxisRight = [SCINumericAxis new];
        yAxisRight.axisTitle = @"Primary Y-Axis";
        yAxisRight.axisId = @"Primary Y-Axis";
        yAxisRight.axisAlignment = SCIAxisAlignment_Right;
        
        // Create another numeric axis, left-aligned
        SCINumericAxis *yAxisLeft = [SCINumericAxis new];
        yAxisLeft.axisTitle = @"Secondary Y-Axis";
        yAxisLeft.axisId = @"Secondary Y-Axis";
        yAxisLeft.axisAlignment = SCIAxisAlignment_Left;
        yAxisLeft.growBy = [[SCIDoubleRange alloc] initWithMin:0.2 max:0.2];

        [SCIUpdateSuspender usingWithSuspendable:surface withBlock:^{
            [surface.xAxes add:[SCINumericAxis new]];
            [surface.yAxes addAll:yAxisLeft, yAxisRight, nil];
        }];
    }
    // ...
    [self setupAxesForSurface:_surface];
    [self setupAxesForSurface:_surface2];
</div>
<div class="code-snippet" id="swift">
    fileprivate func setupAxesFor(surface: SCIChartSurface) {
        // Create another numeric axis, right-aligned
        let yAxisRight = SCINumericAxis()
        yAxisRight.axisTitle = "Primary Y-Axis"
        yAxisRight.axisId = "Primary Y-Axis"
        yAxisRight.axisAlignment = .right
        
        // Create another numeric axis, left-aligned
        let yAxisLeft = SCINumericAxis()
        yAxisLeft.axisTitle = "Secondary Y-Axis"
        yAxisLeft.axisId = "Secondary Y-Axis"
        yAxisLeft.axisAlignment = .left
        yAxisLeft.growBy = SCIDoubleRange(min: 0.2, max: 0.2)
        
        SCIUpdateSuspender.usingWith(surface) {
           surface.xAxes.add(items: SCINumericAxis())
           surface.yAxes.add(items: yAxisRight, yAxisLeft)
       }
    }
    // ...
    setupAxesFor(surface: surface)
    setupAxesFor(surface: surface2)
</div>
<div class="code-snippet" id="cs">
    private void SetupAxesForSurface(SCIChartSurface surface)
    {
        // Create another numeric axis, right-aligned
        var yAxisRight = new SCINumericAxis();
        yAxisRight.AxisTitle = "Primary Y-Axis";
        yAxisRight.AxisId = "Primary Y-Axis";
        yAxisRight.AxisAlignment = SCIAxisAlignment.Right;

        // Create another numeric axis, left-aligned
        var yAxisLeft = new SCINumericAxis();
        yAxisLeft.AxisTitle = "Secondary Y-Axis";
        yAxisLeft.AxisId = "Secondary Y-Axis";
        yAxisLeft.AxisAlignment = SCIAxisAlignment.Left;
        yAxisLeft.GrowBy = new SCIDoubleRange(0.2, 0.2);

        using (surface.SuspendUpdates())
        {
            surface.XAxes.Add(new SCINumericAxis());
            surface.YAxes.Add(yAxisLeft);
            surface.YAxes.Add(yAxisRight);
        }
    }
    // ...
    SetupAxesForSurface(Surface);
    SetupAxesForSurface(Surface2);
</div>

At this stage, you should have something similar to the shown below:

![Multi Chart Empty](img/tutorials-2d/tutorials-2d-multi-chart-empty.png)

## Adding a Series to the Second Chart
Now we are ready to add a [RenderableSeries](2D Chart Types.html) to the second chart.
To try something new, let's add a [Mountain Series](2d-chart-types---mountain-area-series.html)

We are going to attach an **existing DataSeries** instance, so it scrolls just like the series on the first chart.
Also, we will attach the **RenderableSeries** to the right axis.

> **_NOTE:_** Remember, since there are two `Y-Axes`, they both must have **unique IDs** assigned to them. Those IDs can be used to register **RenderableSeries** and **Annotations** on a corresponding axis.

So just add these lines of code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIFastMountainRenderableSeries *mountainSeries = [SCIFastMountainRenderableSeries new];
    mountainSeries.yAxisId = @"Primary Y-Axis";
    mountainSeries.dataSeries = _scatterDataSeries;
    mountainSeries.strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF0271B1 thickness:2];
    mountainSeries.areaStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0x990271B1];
    
    [SCIUpdateSuspender usingWithSuspendable:self.surface2 withBlock:^{
        [self.surface2.renderableSeries add:mountainSeries];
    }];
</div>
<div class="code-snippet" id="swift">
    let mountainSeries = SCIFastMountainRenderableSeries()
    mountainSeries.yAxisId = "Primary Y-Axis"
    mountainSeries.dataSeries = scatterDataSeries
    mountainSeries.strokeStyle = SCISolidPenStyle(colorCode: 0xFF0271B1, thickness: 2)
    mountainSeries.areaStyle = SCISolidBrushStyle(colorCode: 0x990271B1)
    
    SCIUpdateSuspender.usingWith(surface2) {
        self.surface2.renderableSeries.add(mountainSeries)
    }
</div>
<div class="code-snippet" id="cs">
    using (Surface2.SuspendUpdates())
    {
        Surface2.RenderableSeries.Add(new SCIFastMountainRenderableSeries
        {
            YAxisId = "Primary Y-Axis",
            DataSeries = scatterDataSeries,
            StrokeStyle = new SCISolidPenStyle(0xFF0271B1, 2),
            AreaStyle = new SCISolidBrushStyle(0x990271B1)
        });
    }
</div>

Now you should see something like this:

![Multi Chart](img/tutorials-2d/tutorials-2d-multi-chart.mp4"></video>

## Synchronizing Multiple Charts
In SciChart, you can synchronize **VisibleRanges**, chart **Sizes**, Modifiers, tooltips and more!

#### Synchronizing VisibleRanges on Axes
To make both charts show the same **VisibleRanges** on both axes, you **share** the same `ISCIRange` instance across the axes.

In this particular case, there is no need to do that because both charts use the same data. But if it was different, we would need to **synchronize** VisibleRanges like this:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create an ISCIRange instance that will be shared across multiple charts
    SCIDoubleRange *sharedXRange = [SCIDoubleRange new];

    // Create an X axis and apply sharedXRange
    SCINumericAxis *xAxis = SCINumericAxis()
    xAxis.visibleRange = sharedXRange;

    // Create another X axis and apply sharedXRange
    SCINumericAxis *xAxis2 = SCINumericAxis()
    xAxis2.visibleRange = sharedXRange;
</div>
<div class="code-snippet" id="swift">
    // Create an ISCIRange instance that will be shared across multiple charts
    let sharedXRange = SCIDoubleRange()

    // Create an X axis and apply sharedXRange
    let xAxis = SCINumericAxis()
    xAxis.visibleRange = sharedXRange

    // Create another X axis and apply sharedXRange
    let xAxis2 = SCINumericAxis()
    xAxis2.visibleRange = sharedXRange
</div>
<div class="code-snippet" id="cs">
    // Create an ISCIRange instance that will be shared across multiple charts
    var sharedXRange = new SCIDoubleRange();

    // Create an X axis and apply sharedXRange
    var xAxis = new SCINumericAxis();
    xAxis.VisibleRange = sharedXRange;

    // Create another X axis and apply sharedXRange
    var xAxis2 = new SCINumericAxis();
    xAxis2.VisibleRange = sharedXRange;
</div>

#### Synchronizing Chart Widths
Imagine a situation when you have a two charts with Y axes on opposite sides, or values on your Y-Axes differs, and width of them are different.
It will cause one of the chart areas stick out. There is a helper class called `SCIChartVerticalGroup` which is used in situations like this.
It's just line charts up. See the code below which showcases how to use it:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIChartVerticalGroup *_verticalGroup = [SCIChartVerticalGroup new];
    [_verticalGroup addSurfaceToGroup:surface];
    [_verticalGroup addSurfaceToGroup:surface2];
</div>
<div class="code-snippet" id="swift">
    let verticalGroup = SCIChartVerticalGroup()
    verticalGroup.addSurface(toGroup: surface)
    verticalGroup.addSurface(toGroup: surface2)
</div>
<div class="code-snippet" id="cs">
    var verticalGroup = new SCIChartVerticalGroup();
    verticalGroup.AddSurfaceToGroup(surface);
    verticalGroup.AddSurfaceToGroup(surface2);
</div>

We used this technique in our **Multi-Pane Stock Chart** example, which can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-multi-pane-stock-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-multi-pane-stock-charts-example/)

![Multi-Pane Stock Chart](img/tutorials-2d/tutorials-2d-multi-pane-stock-chart-example.png)

#### Linking Cursor and Other Modifiers
Next we are going to **link chart modifiers**.

The first chart has an array of ChartModifiers set up to handle zooming, panning and tooltips.
We are going to add some of these modifiers to the second chart.

To sync those modifier, you should add the modifiers through the `SCIModifierGroup` collection, and share the **same group** via the `SCIModifierGroup.eventGroup` property, like showcased below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    SCIRolloverModifier *rolloverModifier = [SCIRolloverModifier new];
    rolloverModifier.receiveHandledEvents = YES;

    SCIModifierGroup *modifierGroup = [SCIModifierGroup new];
    modifierGroup.eventGroup = @"SharedEventGroup";
    modifierGroup.receiveHandledEvents = YES;
    [modifierGroup.childModifiers addAll:[SCIZoomExtentsModifier new], [SCIPinchZoomModifier new], rolloverModifier, [SCIXAxisDragModifier new], [SCIYAxisDragModifier new], nil];
</div>
<div class="code-snippet" id="swift">
    let rolloverModifier = SCIRolloverModifier()
    rolloverModifier.receiveHandledEvents = true
    
    let modifierGroup = SCIModifierGroup()
    modifierGroup.eventGroup = "SharedEventGroup"
    modifierGroup.receiveHandledEvents = true
    modifierGroup.childModifiers.add(items: SCIZoomExtentsModifier(), SCIPinchZoomModifier(), rolloverModifier, SCIXAxisDragModifier(), SCIYAxisDragModifier())
</div>
<div class="code-snippet" id="cs">
    var modifierGroup = new SCIModifierGroup { EventGroup = "SharedEventGroup" };
    modifierGroup.ChildModifiers.Add(new SCIZoomExtentsModifier());
    modifierGroup.ChildModifiers.Add(new SCIPinchZoomModifier());
    modifierGroup.ChildModifiers.Add(new SCIRolloverModifier { ReceiveHandledEvents = true });
    modifierGroup.ChildModifiers.Add(new SCIXAxisDragModifier());
    modifierGroup.ChildModifiers.Add(new SCIYAxisDragModifier());
</div>

> **_NOTE_** You can use a **eventGroup** on more than two charts.

Run the application again. The Cursors and Tooltips are now **synchronized** across the charts:

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-multi-chart-sync.mp4"></video>

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2007%20-%20Linking%20Multiple%20Charts)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-07)

Of course, this is not the limit of what you can achieve with the SciChart iOS. You might want to read some of the following articles:
- [Axis APIs](Axis APIs.html)
- [Annotations API](Annotations APIs.html)
- [2D Chart Types](2D Chart Types.html)
- [Chart Modifiers](Chart Modifier APIs.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).

For instance - take a look at our **Sync Multi Chart** example, which can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):

![Sync Multi Chart Example](img/tutorials-2d/tutorials-2d-sync-multi-chart-example.png)