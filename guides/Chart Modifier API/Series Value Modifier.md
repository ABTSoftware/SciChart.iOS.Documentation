# SCISeriesValueModifier

The `SCISeriesValueModifier` is a custom ChartModifier that places a `SCISeriesValueMarkerAnnotation` on the YAxis for each RenderableSeries in the chart, showing the current `ISCIRenderableSeries` latest Y-value.

<video autoplay loop muted playsinline src="img/modifiers-2d/series-value-modifier.mp4"></video>

> **_NOTE:_** Examples of the `SCISeriesValueModifier` usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-series-value-modifier-example/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart-series-value-modifier-example/)

## Adding the SCISeriesValueModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added](Chart Modifier APIs.html#adding-a-chart-modifier) to a `SCIChartSurface` via the `ISCIChartSurface.chartModifiers` property and `SCISeriesValueModifier` is no different.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">

    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a modifier
    let seriesValueModifier = [SCISeriesValueModifier new];

    // Add the modifier to the surface
    [self.surface.chartModifiers add:seriesValueModifier];
</div>
<div class="code-snippet" id="swift">

    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier
    let seriesValueModifier = SCISeriesValueModifier()

    // Add the modifier to the surface
    self.surface.chartModifiers.add(seriesValueModifier)
</div>
<div class="code-snippet" id="cs">

    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a modifier
    var seriesValueModifier = new SCISeriesValueModifier();

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(seriesValueModifier);
</div>

> **_NOTE:_** To learn more about features available, [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.

## Excluding a Series from the SeriesValueModifier

By default, all RenderableSeries from `ISCIChartSurface.renderableSeries` collection are included in the `SCISeriesValueModifier` and thus will have an axis marker placed on YAxis. This behavior can be controlled and to do so you’ll need to create a `SCIDefaultSeriesValueMarkerFactory` instance with a predicate to decide which series would you like to include or exclude. 

Imagine you have three `ISCIRenderableSeries` and you want to show marker only for a series with the name "Blue Series". Please see the code below, which shows how to do just that:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">

    // Assume renderable series with name "Blue Series" has been created and configured somewhere

    // Create a factory with a predicate
    SCIDefaultSeriesValueMarkerFactory *factory = [[SCIDefaultSeriesValueMarkerFactory alloc] initWithPredicate:^BOOL(id<ISCIRenderableSeries> series) {
        return [series.dataSeries.seriesName isEqualToString:@"Blue Series"];
    }];

    // Create seriesValueModifier with this factory
    SCISeriesValueModifier *seriesValueModifier = [[SCISeriesValueModifier alloc] initWithMarkerFactory:factory];

    // Add the modifier to the surface
    [self.surface.chartModifiers add:seriesValueModifier];
    
</div>
<div class="code-snippet" id="swift">

    // Assume renderable series with name "Blue Series" has been created and configured somewhere

    // Create a factory with a predicate
    let factory = SCIDefaultSeriesValueMarkerFactory { series -> Bool in
        guard let series = series as? ISCIRenderableSeries else { return true }
        
        return series.dataSeries?.seriesName == "Blue Series"
    }
    
    // Create seriesValueModifier with this factory
    let seriesValueModifier = SCISeriesValueModifier(markerFactory: factory)

    // Add the modifier to the surface
    self.surface.chartModifiers.add(seriesValueModifier)

</div>
<div class="code-snippet" id="cs">

    // Assume renderable series with name "Blue Series" has been created and configured somewhere

    // Create a factory with a predicate
    var factory = new SCIDefaultSeriesValueMarkerFactory(obj => ((IISCIRenderableSeries)obj).DataSeries.SeriesName.Equals("Blue Series"));
    
    // Create seriesValueModifier with this factory
    var seriesValueModifier = new SCISeriesValueModifier(factory);
    
    // Add the modifier to the surface
    Surface.ChartModifiers.Add(seriesValueModifier);
</div>

The result is that only the blue series has a marker:

![Series Value Modifier](img/modifiers-2d/series-value-modifier-exclude-series.png)

## Styling the SCISeriesValueModifier Axis Markers

By default, `SCISeriesValueModifier` uses `SCIDefaultSeriesValueMarker` to create `SCISeriesValueMarkerAnnotation`. You can customize a marker annotation style or provide some complete custom annotation. 

> **_NOTE:_** To learn more about `SCIAxisMarkerAnnotation` API, follow [SCIAxisMarkerAnnotation API](axismarkerannotation.html#) article.

As an example of such customization, let’s place a `SCIHorizontalLineAnnotation` with an axis label. You will need to write few classes with some implementations to inject the desired marker into the `SCISeriesValueModifier` pipeline. Those are:

 - Subclass `SCIHorizontalLineAnnotation` with `SCISeriesValueMarkerAnnotationHelper` as a initializer parameter to call `-[SCIDefaultSeriesValueMarkerAnnotationHelper tryUpdateAnnotation:]` method when needed:
 
 <div class="code-snippet-tabs">
   <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
   <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
 </div>
 <div class="code-snippet" id="objectivec">
    
    @interface SCDHorizontalLineSeriesValueMarkerAnnotation : SCIHorizontalLineAnnotation

    - (instancetype)initWithSeriesValueHelper:(SCIDefaultSeriesValueMarkerAnnotationHelper *)seriesValueHelper;

    @end

    @implementation SCDHorizontalLineSeriesValueMarkerAnnotation {
        SCIDefaultSeriesValueMarkerAnnotationHelper *_seriesValueHelper;
    }

    - (instancetype)initWithSeriesValueHelper:(SCIDefaultSeriesValueMarkerAnnotationHelper<SCDHorizontalLineSeriesValueMarkerAnnotation *> *)seriesValueHelper {
        self = [super init];
        if (self) {
            _seriesValueHelper = seriesValueHelper;
        }
        return self;
    }

    - (void)attachTo:(id<ISCIServiceContainer>)services {
        id<ISCIRenderableSeries> renderableSeries = _seriesValueHelper.renderableSeries;
        
        self.xAxisId = renderableSeries.xAxisId;
        self.yAxisId = renderableSeries.yAxisId;
        
        [super attachTo:services];
    }

    - (void)updateWithXAxis:(id<ISCIAxis>)xAxis yAxis:(id<ISCIAxis>)yAxis {
        [_seriesValueHelper tryUpdateAnnotation:self];
        
        [super updateWithXAxis:xAxis yAxis:yAxis];
    }

    @end

 </div>
 <div class="code-snippet" id="swift">

    class HorizontalLineSeriesValueMarkerAnnotation: SCIHorizontalLineAnnotation {
        private let seriesValueHelper: SCIDefaultSeriesValueMarkerAnnotationHelper<HorizontalLineSeriesValueMarkerAnnotation>
        
        init(seriesValueHelper: SCIDefaultSeriesValueMarkerAnnotationHelper<HorizontalLineSeriesValueMarkerAnnotation>) {
            self.seriesValueHelper = seriesValueHelper
        }
        
        override func attach(to services: ISCIServiceContainer) {
            let renderableSeries = seriesValueHelper.renderableSeries

            xAxisId = renderableSeries.xAxisId
            yAxisId = renderableSeries.yAxisId

            super.attach(to:services)
        }
        
        override func update(withXAxis xAxis: ISCIAxis, yAxis: ISCIAxis) {
            seriesValueHelper.tryUpdateAnnotation(self)
            
            super.update(withXAxis: xAxis, yAxis: yAxis)
        }
    }
 </div>

 - Subclass `SCISeriesValueMarkerAnnotationHelper` where we will style our annotation and annotation labels:
 
 <div class="code-snippet-tabs">
   <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
   <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
 </div>
 <div class="code-snippet" id="objectivec">

    #import &lt;SciChart/SCIDefaultSeriesValueMarkerAnnotationHelper+Protected.h&gt;

    @interface SCDHorizontalLineSeriesValueMarkerAnnotationHelper : SCIDefaultSeriesValueMarkerAnnotationHelper<SCDHorizontalLineSeriesValueMarkerAnnotation *>

    @end

    @implementation SCDHorizontalLineSeriesValueMarkerAnnotationHelper {
        float _lineThickness;
        NSArray<NSNumber *> *_dashPattern;
    }

    - (instancetype)initWithRenderableSeries:(id<ISCIRenderableSeries>)renderableSeries predicate:(SCIPredicate)isValidRenderableSeriesPredicate {
        self = [super initWithRenderableSeries:renderableSeries predicate:isValidRenderableSeriesPredicate];
        if (self) {
            _lineThickness = 1;
            _dashPattern = [NSArray arrayWithObjects:@4, @4, nil];
        }
        return self;
    }

    - (void)updateAnnotation:(SCDHorizontalLineSeriesValueMarkerAnnotation *)annotation lastValue:(id<ISCIComparable>)lastValue lastColor:(UIColor *)lastColor {
        [super updateAnnotation:annotation lastValue:lastValue lastColor:lastColor];
        
        annotation.stroke = [[SCISolidPenStyle alloc] initWithColor:lastColor thickness:_lineThickness strokeDashArray:_dashPattern];
        
        NSInteger count = annotation.annotationLabels.count;
        for (NSInteger i = 0; i <count; i++) {
            SCIAnnotationLabel *label = annotation.annotationLabels[i];
            label.backgroundBrush = [[SCISolidBrushStyle alloc] initWithColor:lastColor];
            label.fontStyle = [[SCIFontStyle alloc] initWithFontSize:12.0 andTextColorCode:[UIColor getInvertedColor:[lastColor colorARGBCode]]];
        }
    }

    @end
 </div>
 <div class="code-snippet" id="swift">

    import SciChart.Protected.SCIDefaultSeriesValueMarkerAnnotationHelper

    class HorizontalLineSeriesValueMarkerAnnotationHelper: SCIDefaultSeriesValueMarkerAnnotationHelper<HorizontalLineSeriesValueMarkerAnnotation> {
        private let lineThickness: Float = 1
        private let dashPattern: [NSNumber] = [4, 4]
        
        override func updateAnnotation(annotation: HorizontalLineSeriesValueMarkerAnnotation, lastValue: ISCIComparable, lastColor: UIColor) {
            super.updateAnnotation(annotation: annotation, lastValue: lastValue, lastColor: lastColor)
            
            annotation.stroke = SCISolidPenStyle(color: lastColor, thickness: lineThickness, strokeDashArray: dashPattern)
            for i in 0..<annotation.annotationLabels.count {
                let label = annotation.annotationLabels[i]
                label.backgroundBrush = SCISolidBrushStyle(color: lastColor)
                label.fontStyle = SCIFontStyle(fontSize: 12, andTextColorCode: UIColor.getInvertedColor(lastColor.colorARGBCode()))
            }
        }
    }
     
 </div>

 - Subclass `SCISeriesValueMarkerBase`, where we will create, destroy, add or remove our custom **markerAnnotation** when needed:
 
 <div class="code-snippet-tabs">
   <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
   <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
 </div>
 <div class="code-snippet" id="objectivec">

    #import &lt;SciChart/SCISeriesValueMarkerBase+Protected.h&gt;

    @interface SCDHorizontalLineSeriesValueMarker : SCISeriesValueMarkerBase
        
    @end

    @implementation SCDHorizontalLineSeriesValueMarker {
        SCDHorizontalLineSeriesValueMarkerAnnotation *_markerAnnotation;
    }

    - (void)tryRemoveMarkerAnnotation:(id<ISCIChartSurface>)parentSurface {
        if (_markerAnnotation != nil) {
            [parentSurface.annotations remove:_markerAnnotation];
        }
    }

    - (void)tryAddMarkerAnnotation:(id<ISCIChartSurface>)parentSurface {
        if (_markerAnnotation != nil) {
            [parentSurface.annotations safeAdd:_markerAnnotation];
        }
    }

    - (void)createMarkerAnnotation {
        _markerAnnotation = [[SCDHorizontalLineSeriesValueMarkerAnnotation alloc] initWithSeriesValueHelper:[[SCDHorizontalLineSeriesValueMarkerAnnotationHelper alloc] initWithRenderableSeries:self.renderableSeries predicate:self.isValidRenderableSeriesPredicate]];
        
        SCIAnnotationLabel *label = [SCIAnnotationLabel new];
        label.labelPlacement = SCILabelPlacement_Axis;
        [_markerAnnotation.annotationLabels add:label];
    }

    - (void)destroyMarkerAnnotation {
        _markerAnnotation = nil;
    }

    @end
     
 </div>
 <div class="code-snippet" id="swift">

    import SciChart.Protected.SCISeriesValueMarkerBase
    class HorizontalLineSeriesValueMarker: SCISeriesValueMarkerBase {
        private var markerAnnotation: HorizontalLineSeriesValueMarkerAnnotation?
        
        override func tryRemoveMarkerAnnotation(_ parentSurface: ISCIChartSurface) {
            if let markerAnnotation = markerAnnotation {
                parentSurface.annotations.remove(markerAnnotation)
            }
        }
        
        override func tryAddMarkerAnnotation(_ parentSurface: ISCIChartSurface) {
            if let markerAnnotation = markerAnnotation {
                parentSurface.annotations.safeAdd(markerAnnotation)
            }
        }
        
        override func createMarkerAnnotation() {
            markerAnnotation = HorizontalLineSeriesValueMarkerAnnotation(
                seriesValueHelper: HorizontalLineSeriesValueMarkerAnnotationHelper(
                    renderableSeries: renderableSeries,
                    predicate: isValidRenderableSeriesPredicate
                )
            )
            
            let label = SCIAnnotationLabel()
            label.labelPlacement = .axis
            markerAnnotation?.annotationLabels.add(label)
        }
        
        override func destroyMarkerAnnotation() {
            markerAnnotation = nil
        }
    }

 </div>
 
 - Create a custom `ISCISeriesValueMarkerFactory` and implement `CreateMarkerFunc` **projectionFunction** which will return our custom marker:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

    @interface SCDHorizontalLineSeriesValueMarkerFactory : NSObject<ISCISeriesValueMarkerFactory>

    @end

    @implementation SCDHorizontalLineSeriesValueMarkerFactory

    @synthesize projectionFunction = _projectionFunction;

    - (instancetype)initWithPredicate:(SCIPredicate)isValidRenderableSeriesPredicate {
        self = [super init];
        if (self) {
            _projectionFunction = ^id<ISCISeriesValueMarker>(id<ISCIRenderableSeries> renderableSeries) {
                return [[SCDHorizontalLineSeriesValueMarker alloc] initWithRenderableSeries:renderableSeries predicate:isValidRenderableSeriesPredicate];
            };
        }
        return self;
    }

    @end
    
</div>
<div class="code-snippet" id="swift">

    class HorizontalLineSeriesValueMarkerFactory: NSObject, ISCISeriesValueMarkerFactory {
        var projectionFunction: CreateMarkerFunc
        
        init(predicate: @escaping SCIPredicate = { _ in return true }) {
            projectionFunction = { series in
                return HorizontalLineSeriesValueMarker(renderableSeries: series, predicate: predicate)
            }
        }
    }
    
</div>

 - Create a `SCISeriesValueModifier` with your custom factory and add it to the surface:

 <div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

    SCISeriesValueModifier *seriesValueModifier = [[SCISeriesValueModifier alloc] initWithMarkerFactory:[[SCDHorizontalLineSeriesValueMarkerFactory alloc] initWithPredicate:^BOOL(id item) { return true; }]];

    [self.surface.chartModifiers add:seriesValueModifier];
    
</div>
<div class="code-snippet" id="swift">

    let seriesValueModifier = SCISeriesValueModifier(markerFactory: HorizontalLineSeriesValueMarkerFactory(predicate: { _ in true }))

    surface.chartModifiers.add(seriesValueModifier)
    
</div>

This produces the following output:

![Series Value Modifier Custom Marker](img/modifiers-2d/series-value-modifier-custom-marker.png)
