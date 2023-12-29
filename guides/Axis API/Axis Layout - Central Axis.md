# Axis Layout - Central Axis
## Axis Placement
SciChart supports axes plaсed inside the chart area, a.k.a in the center of the chart. This behavior is controlled by `ISCIAxis.isCenterAxis`. By default, `ISCIAxis.isCenterAxis` is set to `false`, and axes are drawing outside the chart. Here is the difference:

| **isCenterAxis == false**                                                 | **isCenterAxis == true**                                                |
| ------------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| ![ISCIAxis.isCenterAxis == false](img/axis-2d/axes-outside-the-chart.png) | ![ISCIAxis.isCenterAxis == true](img/axis-2d/axes-inside-the-chart.png) |

The above cases are quite simple, whereas you can create completely custom layouts and specify the exact axis position inside the chart area. It requires changes to the layout process in `ISCILayoutManager`. Please read on to learn how to do such an advanced layout.

## The Layout System
So let's dig into layout system a little bit. There is a Default implementation of `ISCILayoutManager` - `SCIDefaultLayoutManager`, and it's responsible for positioning axes on a chart. 

The layout process consists of two passes for every axis, a measure pass and a layout pass. The `ISCILayoutManager` conducts this process, doing calculations and providing all the necessary data to its `ISCIAxisLayoutStrategy` fields. Every **AxisLayoutStrategy** field handles a specific `SCIAxisAlignment` case and is responsible for placing axes with corresponding **Alignment** inside the boundaries provided by the `ISCILayoutManager`.

The layout takes place in the `-[ISCILayoutManager onLayoutChartWithAvailableSize:]` method. It orderly calls 
- `-[ISCIAxisLayoutStrategy measureAxesWithAvailableWidth:height:andChartLayoutState:]`
- `-[ISCIAxisLayoutStrategy layoutWithLeft:top:right:bottom:]`

during the measure and layout passes and returns **evaluated viewport size** as the result.

The layout process is triggered by the `SCIChartSurface` in response to different state changes. It is an essential part of a **render process**.

## Customizing Layout of Axes
Usually you don't need to create a custom `ISCILayoutManager`, but rather to provide a custom `ISCIAxisLayoutStrategy` for a specific `SCIAxisAlignment` mode, _e.g. for bottom axes layout or right axes layout, or both_. It is possible to extend an existing `ISCIAxisLayoutStrategy` class and change its behavior to better serve your purposes. Please find a list of the  strategies out of the box below:
- `SCITopAlignmentOuterAxisLayoutStrategy`
- `SCITopAlignmentInnerAxisLayoutStrategy`
- `SCIBottomAlignmentOuterAxisLayoutStrategy`
- `SCIBottomAlignmentInnerAxisLayoutStrategy`
- `SCILeftAlignmentOuterAxisLayoutStrategy`
- `SCILeftAlignmentInnerAxisLayoutStrategy`
- `SCIRightAlignmentOuterAxisLayoutStrategy`
- `SCIRightAlignmentInnerAxisLayoutStrategy`

## Pinned Axes
You might want to have your axis exactly at the center of the chart. Let's say the **X-Axis**. To do just that, we can extend the `SCITopAlignmentOuterAxisLayoutStrategy` class (or any suitable horizontal strategy) and make the stack of the **top-aligned X axes** to start exactly **at the center** of a chart.  Let's try to build that and apply the newly created layout strategy to a `SCIDefaultLayoutManager.topInnerAxisLayoutStrategy` and then - to the `SCIChartSurface`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CenteredAxisLayoutStrategy: SCITopAlignmentInnerAxisLayoutStrategy
    @end

    @implementation CenteredAxisLayoutStrategy {
        CGFloat _chartAreaWidth;
        CGFloat _chartAreaHeight;
    }

    - (void)measureAxesWithAvailableWidth:(CGFloat)width height:(CGFloat)height andChartLayoutState:(SCIChartLayoutState *)chartLayoutState {
        [super measureAxesWithAvailableWidth:width height:height andChartLayoutState:chartLayoutState];
        
        _chartAreaWidth = width;
        _chartAreaHeight = height;
    }

    - (void)layoutWithLeft:(CGFloat)left top:(CGFloat)top right:(CGFloat)right bottom:(CGFloat)bottom {
        // pin the stack of the top-aligned X Axes to the center of a chart
        CGFloat topCoord = _chartAreaHeight / 2;
        [SCIHorizontalAxisLayoutStrategy layoutAxesFromTopToBottom:self.axes withLeft:left top:topCoord right:right];
    }

    @end
    ...
    // Set place axis appropriately for "Top-Inner" Axis Strategy
    xAxis.isCenterAxis = YES;
    xAxis.axisAlignment = SCIAxisAlignment_Top;
    ...
    // Create and set new Layout Strategy
    SCIDefaultLayoutManager *layoutManager = [SCIDefaultLayoutManager new];
    layoutManager.topInnerAxisLayoutStrategy = [CenteredAxisLayoutStrategy new];
    self.surface.layoutManager = layoutManager;
</div>
<div class="code-snippet" id="swift">
    class CenteredAxisLayoutStrategy: SCITopAlignmentInnerAxisLayoutStrategy {
        private var chartAreaWidth: CGFloat!
        private var chartAreaHeight: CGFloat!
        
        override func measureAxes(withAvailableWidth width: CGFloat, height: CGFloat, andChartLayoutState chartLayoutState: SCIChartLayoutState!) {
            super.measureAxes(withAvailableWidth: width, height: height, andChartLayoutState: chartLayoutState)
            
            self.chartAreaWidth = width
            self.chartAreaHeight = height
        }
        
        override func layout(withLeft left: CGFloat, top: CGFloat, right: CGFloat, bottom: CGFloat) {
            // pin the stack of the top-aligned X Axes to the center of a chart
            let topCoord = chartAreaHeight / 2
            SCIHorizontalAxisLayoutStrategy.layoutAxesFromTop(toBottom: self.axes as? [ISCIAxis], withLeft: left, top: topCoord, right: right)
        }
    }
    ...
    // Set place axis appropriately for "Top-Inner" Axis Strategy
    xAxis.isCenterAxis = true
    xAxis.axisAlignment = .top
    ...
    // Create and set new Layout Strategy
    let layoutManager = SCIDefaultLayoutManager()
    layoutManager.topInnerAxisLayoutStrategy = CenteredAxisLayoutStrategy()
    surface.layoutManager = layoutManager
</div>
<div class="code-snippet" id="cs">
    class CenteredAxisLayoutStrategy : SCITopAlignmentInnerAxisLayoutStrategy
    {
        private nfloat chartAreaWidth;
        private nfloat chartAreaHeight;

        public override void MeasureAxesWithAvailableWidth(nfloat width, nfloat height, SCIChartLayoutState chartLayoutState)
        {
            base.MeasureAxesWithAvailableWidth(width, height, chartLayoutState);

            chartAreaWidth = width;
            chartAreaHeight = height;
        }

        public override void LayoutWithLeft(nfloat left, nfloat top, nfloat right, nfloat bottom)
        {
            // pin the stack of the top-aligned X Axes to the center of a chart
            nfloat topCoord = chartAreaHeight / 2;
            SCIHorizontalAxisLayoutStrategy.LayoutAxesFromTopToBottom(Axes, left, topCoord, right);
        }
    }
    ...
    // Set place axis appropriately for "Top-Inner" Axis Strategy
    xAxis.IsCenterAxis = true;
    xAxis.AxisAlignment = SCIAxisAlignment.Top;
    ...
    // Create and set new Layout Strategy
    var layoutManager = new SCIDefaultLayoutManager();
    layoutManager.TopInnerAxisLayoutStrategy = new CenteredAxisLayoutStrategy();
    Surface.LayoutManager = layoutManager;
</div>

As the result, the **X Axis** should be placed **at the center** of a chart. It should look very similar to this:

![Pinned Central Axis](img/axis-2d/pinned-central-axis.png)

## Floating Axes
Another possible scenario, **floating axes**, can be found in the example called [Shifted Axes](https://www.scichart.com/example/ios-chart/ios-shifted-axes/) from the [SciChart iOS Example Suite](https://www.scichart.com/examples/ios-chart/).

In this case each axis is pinned to 0 value of the other axis. To achieve this, the pixel coordinate of the `0` data value is looked for during every layout pass, using the [Coordinate Transformation API](axis-apis---convert-pixel-to-data-coordinates.html):

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CenteredTopAlignmentInnerAxisLayoutStrategy : SCITopAlignmentInnerAxisLayoutStrategy
    - (instancetype)initWithYAxis:(id<ISCIAxis>)yAxis;
    @end
    @implementation CenteredTopAlignmentInnerAxisLayoutStrategy {
        id<ISCIAxis> _yAxis;
    }

    - (instancetype)initWithYAxis:(id<ISCIAxis>)yAxis {
        self = [super init];
        if (self) {
            _yAxis = yAxis;
        }
        return self;
    }

    - (void)layoutWithLeft:(CGFloat)left top:(CGFloat)top right:(CGFloat)right bottom:(CGFloat)bottom {
        // find the coordinate of 0 on the Y Axis in pixels
        // place the stack of the top-aligned X Axes at this coordinate
        CGFloat topCoord = [_yAxis.currentCoordinateCalculator getCoordinate:0];
        [SCIHorizontalAxisLayoutStrategy layoutAxesFromTopToBottom:self.axes withLeft:left top:topCoord right:right];
    }
    @end
</div>
<div class="code-snippet" id="swift">
    class CenteredTopAlignmentInnerAxisLayoutStrategy: SCITopAlignmentInnerAxisLayoutStrategy {
        let yAxis: ISCIAxis;

        init(yAxis: ISCIAxis) {
            self.yAxis = yAxis
        }
        
        override func layout(withLeft left: CGFloat, top: CGFloat, right: CGFloat, bottom: CGFloat) {
            // find the coordinate of 0 on the Y Axis in pixels
            // place the stack of the top-aligned X Axes at this coordinate
            let topCoord = CGFloat(yAxis.currentCoordinateCalculator.getCoordinateFrom(0))
            SCIHorizontalAxisLayoutStrategy.layoutAxesFromTop(toBottom: self.axes as? [ISCIAxis], withLeft: left, top: topCoord, right: right)
        }
    }
</div>
<div class="code-snippet" id="cs">
    class CenteredTopAlignmentInnerAxisLayoutStrategy : SCITopAlignmentInnerAxisLayoutStrategy
    {
        private IISCIAxis yAxis;

        CenteredTopAlignmentInnerAxisLayoutStrategy(IISCIAxis yAxis)
        {
            this.yAxis = yAxis;
        }

        public override void LayoutWithLeft(nfloat left, nfloat top, nfloat right, nfloat bottom)
        {
            // find the coordinate of 0 on the Y Axis in pixels
            // place the stack of the top-aligned X Axes at this coordinate
            var topCoord = yAxis.CurrentCoordinateCalculator.GetCoordinate(0);
            SCIHorizontalAxisLayoutStrategy.LayoutAxesFromTopToBottom(Axes, left, topCoord, right);
        }
    }
</div>

As it can be seen in the example, both axes are **pinned to a given data value**, not to a specific position inside the chart area. Thus they will adjust their positions accordingly to VisibleRange changes. Please refer to the [Shifted Axes Example](https://www.scichart.com/example/ios-chart/ios-shifted-axes/) to find the complete code sample.

![Floating Central Axis](img/axis-2d/floating-central-axis.png)

For more examples of **custom axes layouts**, please refer to the [Stack Axes Vertically or Horizontally](axis-layout---stack-axes-vertically-or-horizontally.html) article.

## Advanced Axes Layout Customization
For more advanced layout **customization** it is possible to override **the whole layout process**. To achieve this, a custom `ISCILayoutManager` is required with a custom layout process implemented in the `-[ISCILayoutManager onLayoutChartWithAvailableSize:]` method.
