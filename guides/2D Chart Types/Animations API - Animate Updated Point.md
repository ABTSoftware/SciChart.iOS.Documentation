
# Animations API - Animate Updated Point
SciChart library has several built-in animations which you can use to animate your Renderable Series.
> **_NOTE:_** Please refer to the [Animations API](animations-api.html) article for more details.

Also, you can create a custom animation and have complete control over data appearing on the screen. This tutorial shows how to animate Y-Value changes of `SCIStackedColumnRenderableSeries` in real-time.

To achieve that we'd need to perform 2 steps:
- [Create a new transformation](#create-transformation) - a class that implements a `ISCIRenderPassDataTransformation` protocol.
- [Animate your series](#animate-series) using previously created transformation

<video autoplay loop muted playsinline src="img/chart-types-2d/animating-stacked-column-example.mp4"></video>

> **_NOTE:_** A complete project of the Animated Stacked Column Series example you can find in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-chart-animating-stacked-column-chart-example/)

## Create transformation
Creating transformation is fairly simple. We have to create a class that implements an `ISCIRenderPassDataTransformation` protocol and pass an `ISCISeriesRenderPassData` type suitable for your Renderable Series. In our case we will subclass an abstract class `SCIBaseRenderPassDataTransformation`, pass `SCIStackedColumnRenderPassData` type and implement few required methods.
The code would look like follows:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    
#import &lt;SciChart/SCIBaseRenderPassDataTransformation+Protected.h&gt;

@interface SCDUpdatedPointTransformation : SCIBaseRenderPassDataTransformation<SCIStackedColumnRenderPassData *>

- (instancetype)init;

@end

@implementation SCDUpdatedPointTransformation {
    SCIFloatValues *_startYCoordinates;
    SCIFloatValues *_startPrevSeriesYCoordinates;

    SCIFloatValues *_originalYCoordinates;
    SCIFloatValues *_originalPrevSeriesYCoordinates;
}

- (instancetype)init {
    self = [super initWithRenderPassDataType:SCIStackedColumnRenderPassData.class];
    if (self) {
        _startYCoordinates = [SCIFloatValues new];
        _startPrevSeriesYCoordinates = [SCIFloatValues new];
        
        _originalYCoordinates = [SCIFloatValues new];
        _originalPrevSeriesYCoordinates = [SCIFloatValues new];
    }
    return self;
}

// this method is called before the animation starts
- (void)saveOriginalData {
    if (!self.renderPassData.isValid) return;
    
    // save initial yCoords and prevSeriesYCoords
    [SCITransformationHelpers copyDataFromSource:self.renderPassData.yCoords toDest:_originalYCoordinates];
    [SCITransformationHelpers copyDataFromSource:self.renderPassData.prevSeriesYCoords toDest:_originalPrevSeriesYCoordinates];
}

// this method is called multiple times in a loop while the animation is in progress.
- (void)applyTransformation {
    if (!self.renderPassData.isValid) return;
    
    NSInteger count = self.renderPassData.pointsCount;
    
    if (_startPrevSeriesYCoordinates.count != count ||
        _startYCoordinates.count != count ||
        _originalYCoordinates.count != count ||
        _originalPrevSeriesYCoordinates.count != count) {
        return;
    }
    
    // calculate new values for a renderPassData based on original values and the current animator fraction
    float currentTransformationValue = self.currentTransformationValue;

    for (NSInteger i = 0; i < count; i++) {
        float startYCoord = [_startYCoordinates getValueAt:i];
        float originalYCoordinate = [_originalYCoordinates getValueAt:i];
        float additionalY = startYCoord + (originalYCoordinate - startYCoord) * currentTransformationValue;
        
        float startPrevSeriesYCoords = [_startPrevSeriesYCoordinates getValueAt:i];
        float originalPrevSeriesYCoordinate = [_originalPrevSeriesYCoordinates getValueAt:i];
        float additionalPrevSeriesY = startPrevSeriesYCoords + (originalPrevSeriesYCoordinate - startPrevSeriesYCoords) * currentTransformationValue;

        [self.renderPassData.yCoords set:additionalY at:i];
        [self.renderPassData.prevSeriesYCoords set:additionalPrevSeriesY at:i];
    }
}

// this method is called on clean up after animation end
- (void)discardTransformation {
    // reset renderPassData to initial yCoords and prevSeriesYCoords
    [SCITransformationHelpers copyDataFromSource:_originalYCoordinates toDest:self.renderPassData.yCoords];
    [SCITransformationHelpers copyDataFromSource:_originalPrevSeriesYCoordinates toDest:self.renderPassData.prevSeriesYCoords];
}

- (void)onInternalRenderPassDataChanged {
    [self applyTransformation];
}

- (void)onAnimationEnd {
    [super onAnimationEnd];
    
    // save start values for future animation
    [SCITransformationHelpers copyDataFromSource:_originalYCoordinates toDest:_startYCoordinates];
    [SCITransformationHelpers copyDataFromSource:_originalPrevSeriesYCoordinates toDest:_startPrevSeriesYCoordinates];
}

@end
    
</div>
<div class="code-snippet" id="swift">
    
import SciChart.Protected.SCIBaseRenderPassDataTransformation

class UpdatedPointTransformation: SCIBaseRenderPassDataTransformation<SCIStackedColumnRenderPassData> {
    private let startYCoordinates = SCIFloatValues()
    private let startPrevSeriesYCoordinates = SCIFloatValues()

    private let originalYCoordinates = SCIFloatValues()
    private let originalPrevSeriesYCoordinates = SCIFloatValues()
    
    init() {
        super.init(renderPassDataType: SCIStackedColumnRenderPassData.self)
    }
    
    // this method is called before the animation starts
    override func saveOriginalData() {
        guard
            let renderPassData = self.renderPassData,
            renderPassData.isValid
        else { return }
        
        // save initial yCoords and prevSeriesYCoords
        SCITransformationHelpers.copyData(fromSource: renderPassData.yCoords, toDest: originalYCoordinates)
        SCITransformationHelpers.copyData(fromSource: renderPassData.prevSeriesYCoords, toDest: originalPrevSeriesYCoordinates)
    }
    
    // this method is called multiple times in a loop while the animation is in progress.
    override func applyTransformation() {
        guard
            let renderPassData = self.renderPassData,
                renderPassData.isValid
        else { return }

        // calculate new values for a renderPassData based on original values and the current animator fraction
        let count = renderPassData.pointsCount
        
        if startPrevSeriesYCoordinates.count != count ||
            startYCoordinates.count != count ||
            originalYCoordinates.count != count ||
            originalPrevSeriesYCoordinates.count != count {
            return
        }

        for i in 0..&lt;count {
            let startYCoord = startYCoordinates.getValueAt(i)
            let originalYCoordinate = originalYCoordinates.getValueAt(i)
            let additionalY = startYCoord + (originalYCoordinate - startYCoord) * currentTransformationValue
            
            let startPrevSeriesYCoords = startPrevSeriesYCoordinates.getValueAt(i)
            let originalPrevSeriesYCoordinate = originalPrevSeriesYCoordinates.getValueAt(i)
            let additionalPrevSeriesY = startPrevSeriesYCoords + (originalPrevSeriesYCoordinate - startPrevSeriesYCoords) * currentTransformationValue

            renderPassData.yCoords.set(additionalY, at: i)
            renderPassData.prevSeriesYCoords.set(additionalPrevSeriesY, at: i)
        }
    }
    
    // this method is called on clean up after animation end
    override func discardTransformation() {
        guard let renderPassData = self.renderPassData else { return }
        
        // reset renderPassData to initial yCoords and prevSeriesYCoords
        SCITransformationHelpers.copyData(fromSource: originalYCoordinates, toDest: renderPassData.yCoords)
        SCITransformationHelpers.copyData(fromSource: originalPrevSeriesYCoordinates, toDest: renderPassData.prevSeriesYCoords)
    }
    
    override func onInternalRenderPassDataChanged() {
        applyTransformation()
    }
    
    override func onAnimationEnd() {
        super.onAnimationEnd()
        
        // save start values for future animation
        SCITransformationHelpers.copyData(fromSource: originalYCoordinates, toDest: startYCoordinates)
        SCITransformationHelpers.copyData(fromSource: originalPrevSeriesYCoordinates, toDest: startPrevSeriesYCoordinates)
    }
}
    
</div>

## Animate series
With the transformation created above, all we need to do is just animate our series.
It's easily achievable with `SCIAnimations` APIs like below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

// Since we have two Renderable Series in our `SCIVerticallyStackedColumnsCollection`, we need to create separate animators for each series. Please refer to a complete example for more details.
SCIValueAnimator *_animator1 = [self p_SCD_createAnimatorForSeries:_rSeries1];
SCIValueAnimator *_animator2 = [self p_SCD_createAnimatorForSeries:_rSeries2];

- (SCIValueAnimator *)p_SCD_createAnimatorForSeries:(id<ISCIRenderableSeries>)rSeries {
    SCIValueAnimator *animator = [SCIAnimations createAnimatorForSeries:rSeries withTransformation:[SCDUpdatedPointTransformation new]];
    animator.easingFunction = [SCICubicEase new];
    
    return animator;
}

// this method is called in real time based on timer.
- (void)p_SCD_refreshData {
    //cancel animators in case they are in progress 
    [_animator1 cancel];
    [_animator2 cancel];
    
    // update our Data Series
    __weak typeof(self) wSelf = self;
    [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
        for (NSInteger i = 0, count = xValuesCount; i < count; i++) {
            [self->_dataSeries1 updateY:@([wSelf p_SCD_getRandomYValue]) at:i];
            [self->_dataSeries2 updateY:@([wSelf p_SCD_getRandomYValue]) at:i];
        }
    }];
    
    // start animation
    [_animator1 startWithDuration:animationDuration];
    [_animator2 startWithDuration:animationDuration];
}

</div>
<div class="code-snippet" id="swift">

// Since we have two Renderable Series in our `SCIVerticallyStackedColumnsCollection`, we need to create separate animators for each series. Please refer to a complete example for more details.
var animator1: SCIValueAnimator = createAnimator(series: rSeries1)
var animator2: SCIValueAnimator = createAnimator(series: rSeries2)

// create animator for renderable series with our custom transformation
func createAnimator(series: ISCIRenderableSeries) -> SCIValueAnimator {
    let animator = SCIAnimations.createAnimator(for: series, with: UpdatedPointTransformation())
    animator.easingFunction = SCICubicEase()
    
    return animator
}

// this method is called in real time based on timer.
func refreshData() {
    //cancel animators in case they are in progress 
    animator1.cancel()
    animator2.cancel()
    
    // update our Data Series
    SCIUpdateSuspender.usingWith(surface) { [weak self] in
        guard let self = self else { return }
        
        for i in 0..&lt;self.xValuesCount {
            self.dataSeries1.update(y: self.getRandomYValue(), at: i)
            self.dataSeries2.update(y: self.getRandomYValue(), at: i)
        }
    }
    
    // start animation
    animator1.start(withDuration: animationDuration)
    animator2.start(withDuration: animationDuration)
}

</div>

> **_NOTE:_** You may also take a look at the [Animations API - Animate Appended Point](animations-api---animate-appended-point.html) article to find out how to animate an appended point.