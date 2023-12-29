# Custom Modifiers - the SCIGestureModifierBase API
The `SCIGestureModifierBase` provides an abstract base class for all **Gesture-Based Modifiers** within SciChart. 
Using this API, you can **create gestures** that you can attach to a chart to perform any gesture you like. 
Any time you want to do something to alter the behavior of any built-in gesture modifiers, you should be thinking about creating a **custom gesture-based modifier** to do it.

<video autoplay loop muted playsinline src="img/modifiers-2d/custom-gesture-based-modifier.mp4"></video>

> **_NOTE:_** Example of the Custom Gesture-Based Modifier can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-custom-gesture-modifier/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-custom-gesture-modifier/)

## Custom Gesture-Based Modifier
An example below shows how to use `SCIGestureModifierBase` API to create a chart custom gesture-based modifier.

As an example, we will implement a single finger vertical pinch-zoom gesture-based modifier to zoom our chart on X-Axis.
Also, we want to enable it by double-tap (similar to how it works in Google Maps App).

Since by default `UITapGestureRecognizer` fires callbacks on touch-up, we would need to implement custom `GestureRecognizer`, which would fire on touch-down.
So, let’s try to do just that. Please, see the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface SCDDoubleTouchDownGestureRecognizer : UITapGestureRecognizer
    @end

    @implementation SCDDoubleTouchDownGestureRecognizer

    - (instancetype)initWithTarget:(id)target action:(SEL)action {
        self = [super initWithTarget:target action:action];
        if (self) {
            self.numberOfTapsRequired = 2;
        }
        return self;
    }

    - (void)touchesBegan:(NSSet&lt;UITouch *&gt; *)touches withEvent:(UIEvent *)event {
        NSOrderedSet<UITouch *> *orderedTouches = [[NSOrderedSet alloc] initWithSet:touches];
        UITouch *firstTouch = [orderedTouches firstObject];
        
        if (firstTouch.tapCount != 2) return;
        
        if (self.state == UIGestureRecognizerStatePossible) {
            self.state = UIGestureRecognizerStateRecognized;
        }
    }

    - (void)touchesMoved:(NSSet&lt;UITouch *&gt; *)touches withEvent:(UIEvent *)event {
        self.state = UIGestureRecognizerStateFailed;
    }

    - (void)touchesEnded:(NSSet&lt;UITouch *&gt; *)touches withEvent:(UIEvent *)event {
        self.state = UIGestureRecognizerStateFailed;
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class DoubleTouchDownGestureRecognizer: UITapGestureRecognizer {
    
        override init(target: Any?, action: Selector?) {
            super.init(target: target, action: action)
            
            numberOfTapsRequired = 2
        }
        
        override func touchesBegan(_ touches: Set&lt;UITouch&gt;, with event: UIEvent) {
            guard let firstTouch = touches.first, firstTouch.tapCount == 2 else { return }
            if self.state == .possible {
                self.state = .recognized
            }
        }
        
        override func touchesMoved(_ touches: Set&lt;UITouch&gt;, with event: UIEvent) {
            self.state = .failed
        }
        
        override func touchesEnded(_ touches: Set&lt;UITouch&gt;, with event: UIEvent) {
            self.state = .failed
        }
    }
</div>
<div class="code-snippet" id="cs">
    public class DoubleTouchDownGestureRecognizer : UITapGestureRecognizer
    {
        public DoubleTouchDownGestureRecognizer(NSObject target, Selector action) : base(target, action)
        {
            NumberOfTapsRequired = 2;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            var firstTouch = (UITouch)touches.First();
            if (firstTouch.TapCount != 2) return;

            if (State == UIGestureRecognizerState.Possible)
            {
                State = UIGestureRecognizerState.Recognized;
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            State = UIGestureRecognizerState.Failed;
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            State = UIGestureRecognizerState.Failed;
        }
    }
</div>

At this point, we can get into the implementation of our **CustomGestureModifier**. We will create it as a subclass of `SCIGestureModifierBase`.

> **_NOTE:_** It's highly recommended to inherit from `SCIGestureModifierBase` since it gives you some of the base implementations for free.

Firstly, let's add our `DoubleTouchDownGestureRecognizer` to the `parentSurface`. We can do it in `-[ISCIAttachable attachTo:]` method because at that moment `parentSurface` is already initialized.

Also, we need to override the `createGestureRecognizer` method to return a gesture that will be exposed to SciChart API and the `internalHandleGesture` method to receive and handle actions from our **custom gestureRecognizer**.

Let's get into the code.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">

    // To get access to the SCIGestureModifierBase class we need to import it explicitly
    #import &lt;SciChart/SCIGestureModifierBase+Protected.h&gt;
    #import "SCDDoubleTouchDownGestureRecognizer.h"

    // Create CustomGestureModifier
    @interface SCDCustomGestureModifier : SCIGestureModifierBase
    @end

    @implementation SCDCustomGestureModifier {
        SCDDoubleTouchDownGestureRecognizer *_doubleTapGesture;
        CGPoint _initialLocation;
        CGFloat _scaleFactor;
        BOOL _canPan;
    }

    // This method is a good place to add any additional gestures to the Chart Surface
    - (void)attachTo:(id&lt;ISCIServiceContainer&gt;)services {
        [super attachTo:services];
        
        _initialLocation = CGPointZero;
        _scaleFactor = -50.0;
        _canPan = NO;
        
        _doubleTapGesture = [[SCDDoubleTouchDownGestureRecognizer alloc] initWithTarget:self action:@selector(handleDoubleTapGesture:)];
        
        if ([self.parentSurface isKindOfClass:[UIView class]]) {
            UIView *surfaceView = (UIView *)self.parentSurface;
            [surfaceView addGestureRecognizer:_doubleTapGesture];
        }
    }

    // Here we return our custom gesture which will be exposed to SciChart API
    - (UIGestureRecognizer *)createGestureRecognizer {
        return [UIPanGestureRecognizer new];
    }

    // This method fires on each gesture event, similar to gesture action
    - (void)internalHandleGesture:(UIGestureRecognizer *)gestureRecognizer {
        
        if (!_canPan) { return; }
        
        UIPanGestureRecognizer *gesture = (UIPanGestureRecognizer *)gestureRecognizer;
        UIView *parentView = self.parentSurface.modifierSurface.view;
        
        switch (gesture.state) {
            case UIGestureRecognizerStateBegan:
                _initialLocation = [gestureRecognizer locationInView:parentView];
                break;
            case UIGestureRecognizerStateChanged:
                [self performZoom:_initialLocation yScaleFactor:[gesture translationInView:parentView].y];
                [gesture setTranslation:CGPointZero inView:parentView];
                break;
            default:
                _canPan = NO;
                break;
        }
    }

    // Here, we put all our logic of calculation zoom in and out
    - (void)performZoom:(CGPoint)point yScaleFactor:(CGFloat)yScaleFactor {
        CGFloat fraction = yScaleFactor / _scaleFactor;
        for (int i = 0; i < self.xAxes.count; i++) {
            [self growAxis:self.xAxes[i] atPoint:point byFraction:fraction];
        }
    }

    - (void)growAxis:(id&lt;ISCIAxis&gt;)axis atPoint:(CGPoint)point byFraction:(CGFloat)fraction {
        CGFloat width = axis.layoutSize.width;
        CGFloat coord = width - point.x;
        
        double minFraction = (coord / width) * fraction;
        double maxFraction = (1 - coord / width) * fraction;
        
        [axis zoomByFractionMin:minFraction max:maxFraction];
    }

    - (void)handleDoubleTapGesture:(UILongPressGestureRecognizer *)gesture {
        _canPan = YES;
    }

    @end
</div>
<div class="code-snippet" id="swift">

    // To get access to the SCIGestureModifierBase class we need to import it explicitly
    import SciChart.Protected.SCIGestureModifierBase

    // Create CustomGestureModifier
    class CustomGestureModifier: SCIGestureModifierBase {
        private var initialLocation = CGPoint.zero
        private let scaleFactor: CGFloat = -50
        private var canPan = false
        
        private lazy var longPressGesture: DoubleTouchDownGestureRecognizer = {
            let gesture = DoubleTouchDownGestureRecognizer(target: self, action: #selector(handleLongPressGesture))
            gesture.numberOfTapsRequired = 2
            
            return gesture
        }()
        
        // This method is a good place to add any additional gestures to the Chart Surface
        override func attach(to services: ISCIServiceContainer!) {
            super.attach(to: services)
            
            if let parentSurface = self.parentSurface as? UIView {
                parentSurface.addGestureRecognizer(longPressGesture)
            }
        }

        // Here we return our custom gesture which will be exposed to SciChart API
        override func createGestureRecognizer() -> UIGestureRecognizer {
            return UIPanGestureRecognizer()
        }
        
        // This method fires on each gesture event, similar to gesture action
        override func internalHandleGesture(_ gestureRecognizer: UIGestureRecognizer) {
            guard canPan else {
                return
            }
            
            guard let gesture = gestureRecognizer as? UIPanGestureRecognizer else { return }
            let parentView = self.parentSurface.modifierSurface.view
            
            switch gesture.state {
            case .began:
                initialLocation = gestureRecognizer.location(in: parentView)
            case .changed:
                let translationY = gesture.translation(in: parentView).y
                performZoom(point: initialLocation, yScaleFactor: translationY)
                
                gesture.setTranslation(.zero, in: parentView)
            default:
                canPan = false
            }
        }
        
        // Here, we put all our logic of calculation zoom in and out
        private func performZoom(point: CGPoint, yScaleFactor:CGFloat) {
            let fraction = yScaleFactor / scaleFactor
            for i in 0 ..< self.xAxes.count {
                growAxis(self.xAxes[i], at: point, by: fraction)
            }
        }
        
        private func growAxis(_ axis: ISCIAxis, at point: CGPoint, by fraction: CGFloat) {
            let width = axis.layoutSize.width
            let coord = width - point.x
            
            let minFraction = (coord / width) * fraction
            let maxFraction = (1 - coord / width) * fraction
            
            axis.zoom(byFractionMin: Double(minFraction), max: Double(maxFraction))
        }

        @objc private func handleLongPressGesture(_ gesture: UILongPressGestureRecognizer) {
            canPan = true
        }
    }
</div>
<div class="code-snippet" id="cs">

    // Create CustomGestureModifier
    public class CustomGestureModifier : SCIGestureModifierBase
    {
        private DoubleTouchDownGestureRecognizer doubleTapGesture;
        private CGPoint InitialLocation = CGPoint.Empty;
        private readonly nfloat ScaleFactor = -50;
        private bool CanPan;

        public CustomGestureModifier()
        {
        }

        // This method is a good place to add any additional gestures to the Chart Surface
        public override void AttachTo(IISCIServiceContainer services)
        {
            base.AttachTo(services);

            doubleTapGesture = new DoubleTouchDownGestureRecognizer(this, new Selector("handleDoubleTapGesture:"));

            if (ParentSurface is UIView)
            {
                UIView surfaceView = (UIView)ParentSurface;
                surfaceView.AddGestureRecognizer(doubleTapGesture); 
            }
        }

        // To get access to protected methods of the SCIGestureModifierBase class we need to add 'protected' keyword

        // Here we return our custom gesture which will be exposed to SciChart API
        protected override UIGestureRecognizer CreateGestureRecognizer()
        {
            return new UIPanGestureRecognizer();
        }

        // This method fires on each gesture event, similar to gesture action
        protected override void InternalHandleGesture(UIGestureRecognizer gestureRecognizer)
        {
            if (!CanPan) return;

            UIPanGestureRecognizer gesture = (UIPanGestureRecognizer)gestureRecognizer;
            UIView parentView = ParentSurface.ModifierSurface.View;

            switch (gesture.State)
            {
                case UIGestureRecognizerState.Began:
                    InitialLocation = gestureRecognizer.LocationInView(parentView);
                    break;
                case UIGestureRecognizerState.Changed:
                    performZoom(InitialLocation, gesture.TranslationInView(parentView).Y);
                    gesture.SetTranslation(CGPoint.Empty, parentView);
                    break;
                default:
                    CanPan = false;
                    break;
            }
        }

        // Here, we put all our logic of calculation zoom in and out
        void performZoom(CGPoint point, nfloat yScaleFactor)
        {
            nfloat fraction = yScaleFactor / ScaleFactor;
            for (int i = 0; i < XAxes.Count; i++)
            {
                growAxis(XAxes[i], point, fraction);
            }
        }

        void growAxis(IISCIAxis axis, CGPoint point, nfloat fraction)
        {
            nfloat width = axis.LayoutSize.Width;
            nfloat coord = width - point.X;

            double minFraction = (coord / width) * fraction;
            double maxFraction = (1 - coord / width) * fraction;

            axis.ZoomByFractionMin(minFraction, maxFraction);
        }

        [Export("handleDoubleTapGesture:")]
        void handleDoubleTap(UILongPressGestureRecognizer gesture)
        {
            if (gesture.State == UIGestureRecognizerState.Ended)
            {
                CanPan = true;
            }
        }
    }
</div>

Add it onto your chart like any other modifier to see how it works.

## Common Gesture Modifier Properties
As mentioned [above](#custom-modifiers-the-scigesturemodifierbase-api), all **GestureModifiers** within SciChart derive from the abstract `SCIGestureModifierBase` class, which conforms to `UIGestureRecognizerDelegate` protocol.

Please see the list of common properties below:

#### The SCIGestureModifierBase Type

| **Feature**                                       | **Description**                                                                                                                                             |
| ------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `UIGestureRecognizer *gestureRecognizer`          | Read-only property, defines current gesture recognizer which is used to detect gestures.    |
| `- (nonnull UIGestureRecognizer *)createGestureRecognizer`                       | Creates gesture recognizer for current modifier which will be exposed to SciChart API. Should be overriden by inheritors.    |
| `- (void)internalHandleGesture:(nonnull UIGestureRecognizer *)gestureRecognizer` | Handles gesture provided by `UIGestureRecognizer`. Implement in derived classes to receive actions from 'gestureRecognizer'. |

> **_NOTE:_** It is a must to override `- (nonnull UIGestureRecognizer *)createGestureRecognizer` method. Otherwise you will get a `Method Not Implemented` Exception.

> **_NOTE:_** To learn more about common `ChartModifier` features - please read the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article. 
Also visit related `ISCIChartModifier` API documentation.