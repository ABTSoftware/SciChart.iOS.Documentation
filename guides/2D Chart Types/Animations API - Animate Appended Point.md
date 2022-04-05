# Animations API - Animate Appended Point
SciChart library has several built-in animations which you can use to animate your Renderable Series.
> **_NOTE:_** Please refer to the [Animations API](animations-api.html) article for more details.

Also, you can create a custom animation and have complete control over data appearing on the screen. This tutorial shows how to animate the last appended point to a `SCIFastLineRenderableSeries` in real-time.

To achieve that we'd need to perform 2 steps:
- [Create a new transformation](#create-transformation) - a class that implements a `ISCIRenderPassDataTransformation` protocol.
- [Animate your series](#animate-series) using previously created transformation

<video autoplay loop muted playsinline src="img/chart-types-2d/animating-line-chart-example.mp4"></video>

> **_NOTE:_** A complete project of the Animated Line Series example you can find in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-animating-line-chart-example/)

## Create transformation
Creating transformation is fairly simple. We have to create a class that implements an `ISCIRenderPassDataTransformation` protocol and pass an `ISCISeriesRenderPassData` type suitable for your Renderable Series. In our case we will subclass an abstract class `SCIBaseRenderPassDataTransformation`, pass `SCILineRenderPassData` type and implement few required methods.
The code would look like follows:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

#import &lt;SciChart/SCIBaseRenderPassDataTransformation+Protected.h&gt;

@interface SCDAppendedPointTransformation : SCIBaseRenderPassDataTransformation<SCILineRenderPassData *>

- (instancetype)init;

@end

@implementation SCDAppendedPointTransformation {
    SCIFloatValues *_originalXCoordinates;
    SCIFloatValues *_originalYCoordinates;
}

- (instancetype)init {
    self = [super initWithRenderPassDataType:SCILineRenderPassData.class];
    if (self) {
        _originalXCoordinates = [SCIFloatValues new];
        _originalYCoordinates = [SCIFloatValues new];
    }
    return self;
}

// this method is called before the animation starts
- (void)saveOriginalData {
    if (!self.renderPassData.isValid) return;
    
    // save initial xCoords and yCoords
    [SCITransformationHelpers copyDataFromSource:self.renderPassData.xCoords toDest:_originalXCoordinates];
    [SCITransformationHelpers copyDataFromSource:self.renderPassData.yCoords toDest:_originalYCoordinates];
}

// this method is called multiple times in a loop while the animation is in progress.
- (void)applyTransformation {
    if (!self.renderPassData.isValid) return;

    id<ISCICoordinateCalculator> xCalculator = self.renderPassData.xCoordinateCalculator;
    id<ISCICoordinateCalculator> yCalculator = self.renderPassData.yCoordinateCalculator;

    NSInteger count = self.renderPassData.pointsCount;

    // calculate new values for a renderPassData based on original values and the current animator fraction
    float firstXStart = [xCalculator getCoordinateFrom:0];
    float xStart = count <= 1 ? firstXStart : [_originalXCoordinates getValueAt:count - 2];
    float xFinish = [_originalXCoordinates getValueAt:count - 1];
    float additionalX = xStart + (xFinish - xStart) * self.currentTransformationValue;
    [self.renderPassData.xCoords set:additionalX at:count - 1];

    float firstYStart = [yCalculator getCoordinateFrom:0];
    float yStart = count <= 1 ? firstYStart : [_originalYCoordinates getValueAt:count - 2];
    float yFinish = [_originalYCoordinates getValueAt:count - 1];
    float additionalY = yStart + (yFinish - yStart) * self.currentTransformationValue;
    [self.renderPassData.yCoords set:additionalY at:count - 1];
}

// this method is called on clean up after animation end
- (void)discardTransformation {
    // reset renderPassData to initial xCoords and yCoords
    [SCITransformationHelpers copyDataFromSource:_originalXCoordinates toDest:self.renderPassData.xCoords];
    [SCITransformationHelpers copyDataFromSource:_originalYCoordinates toDest:self.renderPassData.yCoords];
}

- (void)onInternalRenderPassDataChanged {
    [self applyTransformation];
}

@end

</div>
<div class="code-snippet" id="swift">
    
import SciChart.Protected.SCIBaseRenderPassDataTransformation

class AppendedPointTransformation: SCIBaseRenderPassDataTransformation<SCILineRenderPassData> {
    private let originalXCoordinates = SCIFloatValues()
    private let originalYCoordinates = SCIFloatValues()
    
    init() {
        super.init(renderPassDataType: SCILineRenderPassData.self)
    }
    
    // this method is called before the animation starts
    override func saveOriginalData() {
        guard let renderPassData = self.renderPassData, renderPassData.isValid else { return }
        
        // save initial xCoords and yCoords
        SCITransformationHelpers.copyData(fromSource: renderPassData.xCoords, toDest: originalXCoordinates)
        SCITransformationHelpers.copyData(fromSource: renderPassData.yCoords, toDest: originalYCoordinates)
    }
    
    // this method is called multiple times in a loop while the animation is in progress.
    override func applyTransformation() {
        guard
            let renderPassData = self.renderPassData,
                renderPassData.isValid,
            let xCalculator = renderPassData.xCoordinateCalculator,
            let yCalculator = renderPassData.yCoordinateCalculator
        else { return }
            
        let count = renderPassData.pointsCount

        // calculate new values for a renderPassData based on original values and the current animator fraction
        let firstXStart = xCalculator.getCoordinate(0)
        let xStart = count <= 1 ? firstXStart : originalXCoordinates.getValueAt(count - 2)
        let xFinish = originalXCoordinates.getValueAt(count - 1)
        let additionalX = xStart + (xFinish - xStart) * currentTransformationValue
        renderPassData.xCoords.set(additionalX, at: count - 1)
        
        let firstYStart = yCalculator.getCoordinate(0)
        let yStart = count <= 1 ? firstYStart : originalYCoordinates.getValueAt(count - 2)
        let yFinish = originalYCoordinates.getValueAt(count - 1)
        let additionalY = yStart + (yFinish - yStart) * currentTransformationValue
        renderPassData.yCoords.set(additionalY, at: count - 1)
    }
    
    // this method is called on clean up after animation end
    override func discardTransformation() {
        guard let renderPassData = self.renderPassData else { return }

        // reset renderPassData to initial xCoords and yCoords
        SCITransformationHelpers.copyData(fromSource: originalXCoordinates, toDest: renderPassData.xCoords)
        SCITransformationHelpers.copyData(fromSource: originalYCoordinates, toDest: renderPassData.yCoords)
    }
    
    override func onInternalRenderPassDataChanged() {
        applyTransformation()
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

    // Append new value to our dataSeries in real-time
    double randomYValue = SCDRandomUtil.nextDouble() * self.maxYValue
    [self->_dataSeries appendX:@(self->_currentXValue) y:@(randomYValue)];

    // Animate renderable series with our custom transformation
    [SCIAnimations animateSeries:_rSeries withTransformation:[SCDAppendedPointTransformation new] duration:animationDuration andEasingFunction:[SCICubicEase new]];

</div>
<div class="code-snippet" id="swift">
    
    // Append new value to our dataSeries in real-time
    let randomYValue = SCDRandomUtil.nextDouble() * self.maxYValue
    self.dataSeries.append(x: self.currentXValue, y: randomYValue)

    // Animate renderable series with our custom transformation
    SCIAnimations.animate(rSeries, with: AppendedPointTransformation(), duration: animationDuration, andEasingFunction: SCICubicEase())

</div>

> **_NOTE:_** You may also take a look at the [Animations API - Animate Updated Point](animations-api---animate-updated-point.html) article to find out how to animate Series after updating existing data points.