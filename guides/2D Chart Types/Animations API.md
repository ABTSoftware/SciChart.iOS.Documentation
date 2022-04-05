# Animations API
In SciChart you can use Animations API to animate **RenderableSeries**. The Animations API is based on our [Transformation API](#transformation-api), which allows to define different `ISCIRenderPassDataTransformation` to your `ISCISeriesRenderPassData` during the render pass.

We provide helper `SCIAnimations` class, which provides set of methods to create animations for `ISCIRenderableSeries`.

Let's see a simple example of using `sweep` animation on our [Line Series](2d-chart-types---line-series.html):

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // declare FastLineRenderableSeries to animate
    SCIFastLineRenderableSeries *rSeries = [SCIFastLineRenderableSeries new];

    // Animate Line Series with Sweep Transformation 
    [SCIAnimations sweepSeries:rSeries duration:3.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    // declare FastLineRenderableSeries to animate
    let rSeries = SCIFastLineRenderableSeries()

    // Animate Line Series with Sweep Transformation 
    SCIAnimations.sweep(rSeries, duration: 3.0, easingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    // declare FastLineRenderableSeries to animate
    var rSeries = new SCIFastLineRenderableSeries();

    // Animate Line Series with Sweep Transformation 
    SCIAnimations.SweepSeries(rSeries, 3, new SCICubicEase());
</div>

<video autoplay loop muted playsinline src="img/chart-types-2d/sweep-line-animation.mp4"></video>

Under the hood `SCIAnimations` methods creates `SCIValueAnimator` to animate specified series, which just animates progress of animation **from 0 to 1**. After start of the animator we just apply transformation onto `ISCIRenderableSeries` according to the current `SCIValueAnimator.animatedFraction`.

`SCIAnimations` methods accept few key parameters, such as duration, delay and easingFunction. The first two are pretty self-explanatory, but the last one - **easingFunction** - which is `ISCIEasingFunction`, allows to transform animation progress before applying transformation. There are a bunch of easing function provided out of the box in SciChart (which are all based on `SCIEasingFunctionBase`):
- `SCIBackEase`
- `SCIBounceEase`
- `SCISineEase`
- `SCIElasticEase`
- `SCICubicEase`
- `SCIQuadraticEase`
- `SCIExponentialEase`

> **_NOTE:_** All **EasingFunctions** are based on **Robert Penners easing functions** equations.

In addition to that, you can change default `SCIEasingFunctionBase.easingMode`. There are 3 possible options - **EaseIn, EaseOut, EaseInOut**.

> **_NOTE:_** You might want to use custom **EasingFunction** which isn't available out of the box in SciChart. To do that - create a class which conforms to the `ISCIEasingFunction` and implements `-[ISCIEasingFunction easeWithNormalizedTime:]` method.

## Animation Types
There are several animation types provided out of the box in SciChart:
- [Fade-In](#fade-in-animation)
- [Scale](#scale-animation)
- [Sweep](#sweep-animation)
- [Wave](#wave-animation)
- [Translate-X](#translate-x-animation)
- [Translate-Y](#translate-y-animation)

#### Fade-In Animation
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIAnimations fadeSeries:rSeries duration:2.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    SCIAnimations.fade(rSeries, duration: 2.0, andEasingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    SCIAnimations.FadeSeries(rSeries, 2, new SCICubicEase());
</div>

> **_NOTE:_** The **Fade-In** animation is implemented utilizing the `ISCIRenderableSeriesCore.opacity` property under the hood.

<video autoplay loop muted playsinline src="img/chart-types-2d/fade-in-animation.mp4"></video>

> **_NOTE:_** Examples which uses **Fade-In** animation can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-with-point-markers-example/)

#### Scale Animation
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIAnimations scaleSeries:rSeries duration:3.0 andEasingFunction:[SCIElasticEase new]];
</div>
<div class="code-snippet" id="swift">
    SCIAnimations.scale(rSeries, duration: 3.0, andEasingFunction: SCIElasticEase())
</div>
<div class="code-snippet" id="cs">
    SCIAnimations.ScaleSeries(rSeries, 3, new SCIElasticEase());
</div>

> **_NOTE:_** The **Scale** animation is implemented utilizing the [Scale Transformation](#scale-transformation) under the hood.

<video autoplay loop muted playsinline src="img/chart-types-2d/scale-animation.mp4"></video>

> **_NOTE:_** Examples which uses **Scale** animation can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-band-series-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-band-chart-example/)

#### Sweep Animation
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIAnimations sweepSeries:rSeries duration:3.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    SCIAnimations.sweep(rSeries, duration: 3.0, andEasingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    SCIAnimations.SweepSeries(rSeries, 3, new SCICubicEase());
</div>

> **_NOTE:_** The **Sweep** animation is implemented utilizing the [Sweep Transformation](#sweep-transformation) under the hood and available only for continuous series (e.g. [Line](2d-chart-types---line-series.html), [Mountain](2d-chart-types---mountain-area-series.html), [Band](2d-chart-types---band-series.html) etc...), where it's possible to interpolate points to have smooth animation.

<video autoplay loop muted playsinline src="img/chart-types-2d/sweep-animation.mp4"></video>

> **_NOTE:_** Examples which uses **Sweep** animation can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-mountain-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-mountain-chart-example/)

#### Wave Animation
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIAnimations waveSeries:rSeries duration:3.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    SCIAnimations.wave(rSeries, duration: 3.0, andEasingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    SCIAnimations.WaveSeries(rSeries, 3, new SCICubicEase());
</div>

> **_NOTE:_** The **Wave** animation is implemented utilizing the [Wave Transformation](#wave-transformation) under the hood.

<video autoplay loop muted playsinline src="img/chart-types-2d/wave-animation.mp4"></video>

> **_NOTE:_** Examples which uses **Wave** animation can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-column-chart-demo/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-column-chart-example/)

#### Translate-X Animation
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIAnimations translateXSeries:rSeries withOffset:-500 duration:3.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    SCIAnimations.translateX(rSeries, offset: -500, duration: 3.0, easingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    SCIAnimations.TranslateXSeries(rSeries, -500, 3, new SCICubicEase());
</div>

> **_NOTE:_** The **Translate-X** animation is implemented utilizing the [Translate Transformation](#translate-transformation) under the hood.

<video autoplay loop muted playsinline src="img/chart-types-2d/translate-x-animation.mp4"></video>

#### Translate-Y Animation
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIAnimations translateYSeries:rSeries withOffset:-500 duration:3.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    SCIAnimations.translateY(rSeries, offset: -500, duration: 3.0, easingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    SCIAnimations.TranslateYSeries(rSeries, -500, 3, new SCICubicEase());
</div>

> **_NOTE:_** The **Translate-Y** animation is implemented utilizing the [Translate Transformation](#translate-transformation) under the hood.

<video autoplay loop muted playsinline src="img/chart-types-2d/translate-y-animation.mp4"></video>

## Transformation API
The Transformation API is based on `ISCIRenderPassDataTransformation` protocol which allows to define different transformations for `ISCIRenderableSeries` which we apply during animation. When we start `SCIValueAnimator` created by `SCIAnimations` - it animates the **Progress** of transformation from 0 (beginning of animation) to 1 (end of animation).

There are several transformations provided out of the box which allow to animate different types of series:
- [Scale](#scale-transformation)
- [Sweep](#sweep-transformation)
- [Wave](#wave-transformation)
- [Translate-X](#translate-transformation)
- [Translate-Y](#translate-transformation)
- [Composite](#composite-transformation)

#### Scale Transformation
Scale transformation is represented by the `SCIScaleTransformationBase` and its implementors:

| **Transformation Type**           | **Applicable to:**                               |
| --------------------------------- | ------------------------------------------------ |
| `SCIScaleXyTransformation`        | `SCIXyRenderPassData` and inheritors.            |
| `SCIScaleXyyTransformation`       | `SCIXyyRenderPassData` and inheritors.           |
| `SCIScaleXyzTransformation`       | `SCIXyzRenderPassData` and inheritors.           |
| `SCIScaleHlTransformation`        | `SCIHlRenderPassData` and inheritors.            |
| `SCIScaleOhlcTransformation`      | `SCIOhlcRenderPassData` and inheritors.          |
| `SCIScaleStackedXyTransformation` | `SCIStackedSeriesRenderPassData` and inheritors. |

> **_NOTE:_** The [Scale Animation](#scale-animation) is implemented based on this transformation.

#### Sweep Transformation
Sweep transformation is represented by the `SCISweepXyTransformation` and `SCISweepXyyTransformation` which allows to transform `SCIXyRenderPassData` and `SCIXyyRenderPassData` respectively.

> **_NOTE:_** The [Sweep Animation](#sweep-animation) is implemented base on this transformation.

#### Wave Transformation
Wave transformation is represented by the `SCIWaveTransformationBase` and its implementors:

| **Transformation Type**          | **Applicable to:**                               |
| -------------------------------- | ------------------------------------------------ |
| `SCIWaveXyTransformation`        | `SCIXyRenderPassData` and inheritors.            |
| `SCIWaveXyyTransformation`       | `SCIXyyRenderPassData` and inheritors.           |
| `SCIWaveHlTransformation`        | `SCIHlRenderPassData` and inheritors.            |
| `SCIWaveOhlcTransformation`      | `SCIOhlcRenderPassData` and inheritors.          |
| `SCIWaveStackedXyTransformation` | `SCIStackedSeriesRenderPassData` and inheritors. |

> **_NOTE:_** The [Wave Animation](#wave-animation) is implemented based on this transformation.

#### Translate Transformation
Wave transformation is represented by the `SCITranslateXTransformation` as well as `SCITranslateXyTransformationBase` and its implementors:

| **Transformation Type**               | **Applicable to:**                               |
| ------------------------------------- | ------------------------------------------------ |
| `SCITranslateXTransformation`         | `SCIXSeriesRenderPassData` and inheritors.       |
| `SCITranslateXyTransformation`        | `SCIXyRenderPassData` and inheritors.            |
| `SCITranslateXyyTransformation`       | `SCIXyyRenderPassData` and inheritors.           |
| `SCITranslateHlTransformation`        | `SCIHlRenderPassData` and inheritors.            |
| `SCITranslateOhlcTransformation`      | `SCIOhlcRenderPassData` and inheritors.          |
| `SCITranslateStackedXyTransformation` | `SCIStackedSeriesRenderPassData` and inheritors. |

> **_NOTE:_** [Translate-X](#translate-x-animation) and [Translate-Y](#translate-y-animation) animations are implemented based on this transformation.

#### Composite Transformation
You might want to combine effects of several transformations at the same time without rewriting those into complex transformation. The `SCICompositeTransformation` is in SciChart to do just that. It allows to aggregate effects into one transformation (e.g. wave and translate-x)

Let's try to use [Wave](#wave-transformation) and [Translate-X](#translate-transformation) at the same time, to animate [Candlestick Series](2d-chart-types---candlestick-series.html) based on the [Candlestick Chart](https://www.scichart.com/example/ios-candlestick-chart-demo/) example which can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // declare Candlestick Series to animate
    SCIFastCandlestickRenderableSeries *rSeries = [SCIFastCandlestickRenderableSeries new];

    // Create transformations and make a Composite one out of them
    SCIWaveOhlcTransformation *wave = [[SCIWaveOhlcTransformation alloc] initWithRenderPassDataType:SCIOhlcRenderPassData.class zeroLine:0 andDurationOfStep:0.5];
    SCITranslateXTransformation *translateX = [[SCITranslateXTransformation alloc] initWithRenderPassDataType:SCIOhlcRenderPassData.class andOffset:-300];
    SCICompositeTransformation *composite = [[SCICompositeTransformation alloc] initWithTransformations:@[wave, translateX]];

    // Animate Candlestick Series with Composite Transformation 
    [SCIAnimations animateSeries:rSeries withTransformation:composite duration:3.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    // declare Candlestick Series to animate
    let rSeries = SCIFastCandlestickRenderableSeries()

    // Create transformations and make a Composite one out of them
    let wave = SCIWaveOhlcTransformation(renderPassDataType: SCIOhlcRenderPassData.self, zeroLine: 0, andDurationOfStep: 0.5)
    let translateX = SCITranslateXTransformation(renderPassDataType: SCIOhlcRenderPassData.self, andOffset: -300)
    let composite = SCICompositeTransformation(transformations: [wave, translateX])
    
    // Animate Candlestick Series with Composite Transformation 
    SCIAnimations.animate(rSeries, with: composite, duration: 3.0, andEasingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    // declare Candlestick Series to animate
    var rSeries = new SCIFastCandlestickRenderableSeries();

    // Create transformations and make a Composite one out of them
    var wave = new SCIWaveOhlcTransformation(typeof(SCIOhlcRenderPassData).ToClass(), 0, 0.5f);
    var translateX = new SCITranslateXTransformation(typeof(SCIOhlcRenderPassData).ToClass(), -300);
    var composite = new SCICompositeTransformation(new IISCIRenderPassDataTransformation[] { wave, translateX });

    // Animate Candlestick Series with Composite Transformation 
    SCIAnimations.AnimateSeries(rSeries, composite, 3.0, new SCICubicEase());
</div>

which results in the following:

<video autoplay loop muted playsinline src="img/chart-types-2d/composite-transformation-candlestick-example.mp4"></video>

> **_NOTE:_** You can create animation for your series with any transformation you want, as we just did in the example above. There are appropriate methods for that in `SCIAnimations`:
- `+[SCIAnimations animateSeries:withTransformation:duration:andEasingFunction:]`
- `+[SCIAnimations animateSeries:withTransformation:duration:startDelay:andEasingFunction:]`

## Custom Animation
To create **custom animation** we need to create a class which implements `ISCIRenderPassDataTransformation` protocol. Inside we need to add code which modifies the render pass data of the **RenderableSeries** which we need **to animate**. For example, we'll try to animate line series and create **_expand_** effect which moves points from some predefined origin point.

First we need to create a transformation for our `SCIXyRenderPassData`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCIBaseRenderPassDataTransformation+Protected.h&gt;

    @interface CustomXyTransformation&lt;T: SCIXyRenderPassData *&gt; : SCIBaseRenderPassDataTransformation&lt;T&gt;

    - (instancetype)initWithOrigin:(CGPoint)origin;

    @end

    @implementation CustomXyTransformation {
        SCIFloatValues *_originalXCoordinates;
        SCIFloatValues *_originalYCoordinates;
        
        CGPoint _origin;
    }

    - (instancetype)initWithOrigin:(CGPoint)origin {
        self = [super initWithRenderPassDataType:SCIXyRenderPassData.class];
        if (self) {
            _origin = origin;
            
            _originalXCoordinates = [SCIFloatValues new];
            _originalYCoordinates = [SCIFloatValues new];
        }
        return self;
    }

    - (void)saveOriginalData {
        NSInteger count = self.renderPassData.pointsCount;
        SCIXyRenderPassData *renderPassData = (SCIXyRenderPassData *)self.renderPassData;
        
        // clear buffers
        [_originalXCoordinates clear];
        [_originalYCoordinates clear];
        // save initial xCoords and yCoords
        [_originalXCoordinates addValues:renderPassData.xCoords.itemsArray startIndex:0 count:count];
        [_originalYCoordinates addValues:renderPassData.yCoords.itemsArray startIndex:0 count:count];
    }

    - (void)applyTransformation {
        SCIXyRenderPassData *renderPassData = (SCIXyRenderPassData *)self.renderPassData;
        // transform current render pass data
        [self transformValues:renderPassData.xCoords originalCoordinates:_originalXCoordinates originValue:_origin.x];
        [self transformValues:renderPassData.yCoords originalCoordinates:_originalYCoordinates originValue:_origin.y];
    }

    - (void)transformValues:(SCIFloatValues *)valuesToTransform originalCoordinates:(SCIFloatValues *)originalCoordinates originValue:(float)originValue {
        float transformationValue = 1 - self.currentTransformationValue;
        // calculate new values to display based on original value
        float *itemsArray = valuesToTransform.itemsArray;
        float *originalValues = originalCoordinates.itemsArray;
        for (NSInteger i = 0, count = self.renderPassData.pointsCount; i < count; i++) {
            float originalValue = originalValues[i];
            itemsArray[i] = originalValue - (originalValue - originValue) * transformationValue;
        }
    }

    - (void)discardTransformation {
        SCIXyRenderPassData *renderPassData = (SCIXyRenderPassData *)self.renderPassData;
        // clear coordinate buffers
        [renderPassData.xCoords clear];
        [renderPassData.yCoords clear];
        // save initial xCoords and yCoords
        [renderPassData.xCoords addValues:_originalXCoordinates.itemsArray startIndex:0 count:_originalXCoordinates.count];
        [renderPassData.yCoords addValues:_originalYCoordinates.itemsArray startIndex:0 count:_originalYCoordinates.count];
    }

    - (void)onInternalRenderPassDataChanged { }

    @end
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCIBaseRenderPassDataTransformation

    class CustomXyTransformation: SCIBaseRenderPassDataTransformation<SCIXyRenderPassData> {
        let originalXCoordinates = SCIFloatValues()
        let originalYCoordinates = SCIFloatValues()
        
        let origin: CGPoint
        
        init(origin: CGPoint) {
            self.origin = origin
            super.init(renderPassDataType: SCIXyRenderPassData.self)
        }
        
        override func saveOriginalData() {
            let count = self.renderPassData.pointsCount
            
            // clear buffers
            originalXCoordinates.clear()
            originalYCoordinates.clear()
            // save initial xCoords and yCoords
            originalXCoordinates.add(values: renderPassData.xCoords.itemsArray, startIndex: 0, count: count)
            originalYCoordinates.add(values: renderPassData.yCoords.itemsArray, startIndex: 0, count: count)
        }
        
        override func apply() {
            transform(values: renderPassData.xCoords, originalCoordinates: originalXCoordinates, originValue: Float(origin.x))
            transform(values: renderPassData.yCoords, originalCoordinates: originalYCoordinates, originValue: Float(origin.y))
        }
        
        fileprivate func transform(values: SCIFloatValues, originalCoordinates: SCIFloatValues, originValue: Float) {
            let transformationValue = 1 - currentTransformationValue
            // calculate new values to display based on original value
            let originalValues = originalCoordinates.itemsArray
            for i in 0 ..< renderPassData.pointsCount {
                let originalValue = originalValues[i]
                let value = originalValue - (originalValue - originValue) * transformationValue
                values.set(value, at: i)
            }
        }
        
        override func discardTransformation() {
            // clear coordinate buffers
            renderPassData.xCoords.clear()
            renderPassData.yCoords.clear()
            // save initial xCoords and yCoords
            renderPassData.xCoords.add(values: originalXCoordinates.itemsArray, startIndex: 0, count: originalXCoordinates.count)
            renderPassData.yCoords.add(values: originalYCoordinates.itemsArray, startIndex: 0, count: originalYCoordinates.count)
        }
        
        override func onInternalRenderPassDataChanged() { }
    }
</div>
<div class="code-snippet" id="cs">
    class CustomXyTransformation: SCIBaseRenderPassDataTransformation
    {
        private SCIFloatValues _originalXCoordinates = new SCIFloatValues();
        private SCIFloatValues _originalYCoordinates = new SCIFloatValues();

        private CGPoint _origin;

        public CustomXyTransformation(CGPoint origin) : base(typeof(SCIXyRenderPassData).ToClass())
        {
            _origin = origin;
        }

        protected override void SaveOriginalData()
        {
            var renderPassData = Runtime.GetNSObject<SCIXyRenderPassData>(RenderPassData.Handle);

            // clear buffers
            _originalXCoordinates.Clear();
            _originalYCoordinates.Clear();
            // save initial xCoords and yCoords
            for (int i = 0, count = RenderPassData.PointsCount; i < count; i++)
            {
                _originalXCoordinates.Add(renderPassData.XCoords.GetValueAt(i));
                _originalYCoordinates.Add(renderPassData.YCoords.GetValueAt(i));
            }
        }

        protected override void ApplyTransformation()
        {
            var renderPassData = Runtime.GetNSObject<SCIXyRenderPassData>(RenderPassData.Handle);
            // transform current render pass data
            TransformValues(renderPassData.XCoords, _originalXCoordinates, (float)_origin.X);
            TransformValues(renderPassData.YCoords, _originalYCoordinates, (float)_origin.Y);
        }

        private void TransformValues(SCIFloatValues valuesToTransform, SCIFloatValues originalCoordinates, float originValue)
        {
            float transformationValue = 1 - CurrentTransformationValue;

            // calculate new values to display based on original value
            for (int i = 0, count = RenderPassData.PointsCount; i < count; i++)
            {
                float originalCoordinate = originalCoordinates.GetValueAt(i);
                float value = originalCoordinate - (originalCoordinate - originValue) * transformationValue;
                valuesToTransform.Set(value, i);
            }
        }

        protected override void DiscardTransformation()
        {
            var renderPassData = Runtime.GetNSObject<SCIXyRenderPassData>(RenderPassData.Handle);

            // clear buffers
            renderPassData.XCoords.Clear();
            renderPassData.YCoords.Clear();
            // save initial xCoords and yCoords
            for (int i = 0, count = RenderPassData.PointsCount; i < count; i++)
            {
                renderPassData.XCoords.Add(_originalXCoordinates.GetValueAt(i));
                renderPassData.YCoords.Add(_originalYCoordinates.GetValueAt(i));
            }
        }

        protected override void OnInternalRenderPassDataChanged() { }
    }
</div>

Then we use this **custom transformation** to animate series using `SCIAnimations` APIs:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    CustomXyTransformation *customTransformation = [[CustomXyTransformation alloc] initWithOrigin:CGPointMake(150, 300)];
    [SCIAnimations animateSeries:rSeries withTransformation:customTransformation duration:1.5 andEasingFunction:[SCIQuadraticEase new]];
</div>
<div class="code-snippet" id="swift">
    let customTransformation = CustomXyTransformation(origin: CGPoint(x: 150, y: 300))
    SCIAnimations.animate(rSeries, with: customTransformation, duration: 1.5, andEasingFunction: SCIQuadraticEase())
</div>
<div class="code-snippet" id="cs">
    var customTransformation = new CustomXyTransformation(new CGPoint(150, 300));
    SCIAnimations.AnimateSeries(rSeries, customTransformation, 1.5, new SCIQuadraticEase());
</div>

The result is the following:

<video autoplay loop muted playsinline src="img/chart-types-2d/custom-animation-example.mp4"></video>
