# Axis Layout - Stack Axes Vertically or Horizontally
It is possible to make **axes to stack up** vertically or horizontally with SciChart. It requires customization of the default chart layout process. Please refer to the [Central Axis](axis-layout---central-axis.html) article to learn more about the layout processes and ways to customize them.

![Default](img/axis-2d/vertically-stacked-axes-example.png)

## Stacking Axes Vertically 
In the code snippet below, a custom `ISCIAxisLayoutStrategy` is created for all the left-aligned axes that belong to a chart. Let's discuss the methods which needs to be implemented:
- `-[ISCIAxisLayoutStrategy measureAxesWithAvailableWidth:height:andChartLayoutState:]`: override the size of the left axes area is calculated, setting the corresponding field of the `SCIAxisLayoutState` object. It provides a value feedback to the `ISCILayoutManager` after the measure pass ends.

- `-[ISCIAxisLayoutStrategy layoutWithLeft:top:right:bottom:]`: override every axis is given a position at the top of the previous one.

Let's try implement that and apply newly created layout strategy to a `SCIDefaultLayoutManager.leftOuterAxesLayoutStrategy` and then - to the `SCIChartSurface`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface LeftAlignedOuterVerticallyStackedYAxisLayoutStrategy : SCIVerticalAxisLayoutStrategy
    @end
    @implementation LeftAlignedOuterVerticallyStackedYAxisLayoutStrategy

    - (void)measureAxesWithAvailableWidth:(CGFloat)width height:(CGFloat)height andChartLayoutState:(SCIChartLayoutState *)chartLayoutState {
        for (NSUInteger i = 0, count = self.axes.count; i < count; i++) {
            id<ISCIAxis> axis = self.axes[i];
            [axis updateAxisMeasurements];
            
            CGFloat requiredAxisSize = [SCIVerticalAxisLayoutStrategy getRequiredAxisSizeFrom:axis.axisLayoutState];
            chartLayoutState.leftOuterAreaSize = MAX(requiredAxisSize, chartLayoutState.leftOuterAreaSize);
        }
    }

    - (void)layoutWithLeft:(CGFloat)left top:(CGFloat)top right:(CGFloat)right bottom:(CGFloat)bottom {
        NSUInteger count = self.axes.count;
        CGFloat height = bottom - top;
        CGFloat axisSize = height / count;
        
        CGFloat topPlacement = top;
        for (int i = 0; i < count; i++) {
            id<ISCIAxis> axis = self.axes[i];
            SCIAxisLayoutState *axisLayoutState = axis.axisLayoutState;
            
            CGFloat bottomPlacement = topPlacement + axisSize;
            
            CGFloat requiredAxisSize = [SCIVerticalAxisLayoutStrategy getRequiredAxisSizeFrom:axisLayoutState];
            [axis layoutAreaWithLeft:right - requiredAxisSize + axisLayoutState.additionalLeftSize top:topPlacement right:right - axisLayoutState.additionalRightSize bottom:bottomPlacement];
            
            topPlacement = bottomPlacement;
        }
    }

    @end
    ...
    // Create and set new Layout Strategy
    SCIDefaultLayoutManager *layoutManager = [SCIDefaultLayoutManager new];
    layoutManager.leftOuterAxisLayoutStrategy = [LeftAlignedOuterVerticallyStackedYAxisLayoutStrategy new];
    self.surface.layoutManager = layoutManager;
</div>
<div class="code-snippet" id="swift">
    class LeftAlignedOuterVerticallyStackedYAxisLayoutStrategy: SCIVerticalAxisLayoutStrategy {
        
        override func measureAxes(withAvailableWidth width: CGFloat, height: CGFloat, andChartLayoutState chartLayoutState: SCIChartLayoutState!) {
            for i in 0 ..&lh; axes.count {
                let axis = axes[i] as! ISCIAxis
                axis.updateMeasurements()
                
                let requiredAxisSize = SCIVerticalAxisLayoutStrategy.getRequiredAxisSize(from: axis.axisLayoutState)
                chartLayoutState.leftOuterAreaSize = max(requiredAxisSize, chartLayoutState.leftOuterAreaSize)
            }
        }
        
        override func layout(withLeft left: CGFloat, top: CGFloat, right: CGFloat, bottom: CGFloat) {
            let count = axes.count
            let height = bottom - top
            let axisSize = height / CGFloat(count)
            
            var topPlacement = top
            for i in 0 ..&lh; count {
                let axis = axes[i] as! ISCIAxis
                let axisLayoutState = axis.axisLayoutState!
                
                let bottomPlacement = topPlacement + axisSize
                
                let requiredAxisSize = SCIVerticalAxisLayoutStrategy.getRequiredAxisSize(from: axisLayoutState)
                axis.layoutArea(withLeft: right - requiredAxisSize + axisLayoutState.additionalLeftSize, top: topPlacement, right: right - axisLayoutState.additionalRightSize, bottom: bottomPlacement)
                
                topPlacement = bottomPlacement
            }
        }
    }
    ...
    // Create and set new Layout Strategy
    let layoutManager = SCIDefaultLayoutManager()
    layoutManager.leftOuterAxisLayoutStrategy = LeftAlignedOuterVerticallyStackedYAxisLayoutStrategy()
    surface.layoutManager = layoutManager
</div>
<div class="code-snippet" id="cs">
    class LeftAlignedOuterVerticallyStackedYAxisLayoutStrategy : SCIVerticalAxisLayoutStrategy
    {
        public override void MeasureAxesWithAvailableWidth(nfloat width, nfloat height, SCIChartLayoutState chartLayoutState)
        {
            for (nuint i = 0; i < Axes.Count; i++)
            {
                var axis = Axes[i];
                axis.UpdateAxisMeasurements();

                var requiredAxisSize = GetRequiredAxisSizeFrom(axis.AxisLayoutState);
                chartLayoutState.LeftOuterAreaSize = (nfloat)Math.Max(requiredAxisSize, chartLayoutState.LeftOuterAreaSize);
            }
        }

        public override void LayoutWithLeft(nfloat left, nfloat top, nfloat right, nfloat bottom)
        {
            var count = Axes.Count;
            var height = bottom - top;
            var axisSize = height / count;

            var topPlacement = top;
            foreach (var axis in Axes)
            {
                var axisLayoutState = axis.AxisLayoutState;
                var bottomPlacement = topPlacement + axisSize;

                var requiredAxisSize = GetRequiredAxisSizeFrom(axisLayoutState);
                axis.LayoutArea(right - requiredAxisSize + axisLayoutState.AdditionalLeftSize, topPlacement, right - axisLayoutState.AdditionalLeftSize, bottomPlacement);

                topPlacement = bottomPlacement;
            }
        }
    }
    ...
    // Create and set new Layout Strategy
    var layoutManager = new SCIDefaultLayoutManager();
    layoutManager.LeftOuterAxisLayoutStrategy = LeftAlignedOuterVerticallyStackedYAxisLayoutStrategy();
    Surface.LayoutManager = layoutManager;
</div>

Please refer to the [Vertically Stacked Y Axes]() to find the complete code sample.

For more examples of **custom axes layouts**, please refer to the [Central Axis](axis-layout---central-axis.html) article.