# SciChart v3 Migration guide
SciChart v3.0 is the ***latest major release*** of SciChart - **High Performance Realtime Charts** framework for iOS.

This guide is provided  to ease the transition of existing applications using SciChart v2.5 to the latest APIs, as well as explain the design and structure of new and updated functionality.

## Table of contents
- [Benefits of Upgrading](scichart-v3-migration-guide.html#benefits-of-upgrading)
- [Breaking Changes](scichart-v3-migration-guide.html#breaking-changes)
    - [Type naming  changes](scichart-v3-migration-guide.html#type-naming-changes)
    - [Removed SCIGenericType](scichart-v3-migration-guide.html#removed-scigenerictype)
    - [Drawing assets creation](scichart-v3-migration-guide.html#drawing-assets-creation)
    - [Observable collections](scichart-v3-migration-guide.html#observable-collections)
    - [Animations](scichart-v3-migration-guide.html#animations)
    - [Annotations API](scichart-v3-migration-guide.html#annotations-api)
    - [Chart Modifiers API](scichart-v3-migration-guide.html#chart-modifiers-api)

---
## Benefits of Upgrading
- **All the API was improved**, became more flexible and more consistent with other platforms 
- **Supports Metal and OpenGL** (fixed Metal - related issues from version 2.5)
- **New, more efficient** mechanism  of [Data Resampling](data-resampling.html) 
- **Improved performance** of certain renderable series types (`SCIFastBubbleRenderableSeries`, `SCIFastCandlestickRenderableSeries`, `SCIFastUniformHeatmapRenderableSeries` and some more)
- **More rich and much more flexible** [Animations](animations-api.html), [Chart Modifiers](Chart Modifier APIs.html), [Annotations](Annotations APIs.html) API
- **A lot of bug fixes**
- **Removed SCIGenericType**, so no boxing needed anymore.
- Whole SciChart API became more **Swift-friendly**
- **[New 3D Chart types](3D Chart Types.html)** were implemented
- **Better structured** [examples suite](scichart-ios-examples-suite.html)

---
## Breaking Changes
In version 3.x iOS API was fully updated to improve flexibility, swift compatibility and become consistent with other platforms.
Because of this, almost all the API has been modified in some way. There's no possibility to document every single change, so we're going to attempt to identify the most commonly used parts of SciChart and describe what have been changed there.

#### Type naming changes

To keep code cleaner and simpler we've got rid of convention to end all the protocol names with a "Protocol" suffix and use "I" prefix instead, e.g.:
- `SCIRenderableSeriesProtocol` became `ISCIRenderableSeries`.
- `SCIAxisCoreProtocol` became `ISCIAxisCore`.

Also, `SCIBubbleRenderableSeries` type was renamed to `SCIFastBubbleRenderableSeries` to match the other platforms.

So in case if, in you SciChart v2 depended code you see the errors like this:
```
- No type or protocol named 'SCIAxis2DProtocol'
- Cannot find protocol declaration for 'SCIChartSurfaceProtocol'
- No type or protocol named 'SCIRenderableSeriesProtocol'
- Unknown receiver 'SCIBubbleRenderableSeries'; did you mean 'SCIFastBubbleRenderableSeries'?
```
you'll need just update a corresponding type name according to a new convention.

#### Removed SCIGenericType

As Objective-C doesn't have generics and hence in previous version of SciChart we used to box all the native types into `SCIGenericType`, which caused code to become inaccurate and dirty. In version 3.x we **enriched our API** to get rid of `SCIGenericType`. Some of generic APIs expect object conforming `ISCIComparable` protocol, 
others just extended to work with primitive types directly. 

> **_NOTE:_** SciChart API has extensions for `NSNumber` and `NSDate` types, adding conformance to `ISCIComparable`.

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // SciChart v2.x
    id&lt;SCIAxis2DProtocol&gt; xAxis = [SCINumericAxis new];
    xAxis.growBy = [[SCIDoubleRange alloc] initWithMin:SCIGeneric(0.1) Max:SCIGeneric(0.1)];
    
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Int32 YType:SCIDataType_Double];
    [dataSeries appendX:SCIGeneric(24) Y:SCIGeneric(234.12)];

    SCILineAnnotation *lineAnnotation = [SCILineAnnotation new];
    lineAnnotation.x1 = SCIGeneric(1.0);
    lineAnnotation.y1 = SCIGeneric(4.0);
     
    // SciChart v3.x
    id&lt;ISCIAxis&gt; xAxis = [SCINumericAxis new];
    xAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    
    SCIXyDataSeries *dataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Int yType:SCIDataType_Double];
    [dataSeries appendX:@(24) y:@(234.12)];
    
    SCILineAnnotation *lineAnnotation = [SCILineAnnotation new];
    lineAnnotation.x1 = @(1.0);
    lineAnnotation.y1 = @(4.0);
</div>
<div class="code-snippet" id="swift">
    // SciChart v2.x
    let xAxis = SCINumericAxis()
    xAxis.growBy = SCIDoubleRange(min: SCIGeneric(0.1), max: SCIGeneric(0.1))

    let dataSeries = SCIXyDataSeries(xType: .int32, yType: .double)
    dataSeries.appendX(SCIGeneric(24), y: SCIGeneric(234.12))
    
    let lineAnnotation = SCILineAnnotation()
    lineAnnotation.x1 = SCIGeneric(1.0)
    lineAnnotation.y1 = SCIGeneric(4.0)
    
    // SciChart v3.x
    let xAxis = SCINumericAxis()
    xAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    
    let dataSeries = SCIXyDataSeries(xType: .int, yType: .double)
    dataSeries.append(x: 24, y: 234.12)

    let lineAnnotation = SCILineAnnotation()
    lineAnnotation.set(x1: 1.0)
    lineAnnotation.set(y1: 4.0)
</div>

Also `SCIArrayController` class (which was fully based on the `SCIGenericType`) has been removed and data structures conforming to `ISCIValues` were introduced instead. 
These changes heavily affected the [DataSeries API](dataseries-apis.html), so now the preferred way to modify your chart's data - is using methods based on `ISCIValues` under the hood.

So if you see the following errors  in your SciChart v2 depended code:
```
- Unknown type name 'SCIGenericType'
- Unknown type name 'SCIArrayController'
- Expected a type 'SCIArrayController'
- Expected a type 'SCIGenericType'
- No visible @interface for 'SCIXyzDataSeries' declares the selector 'initWithXType:YType:ZType:'
- No visible @interface for 'SCIDoubleRange' declares the selector 'initWithMin:Max:'
- Implicit conversion of 'int' to 'id<ISCIComparable>' is disallowed with ARC
```
Just use ISCIComparable conforming types instead of SCIGenericType, ISCIValues instead of SCIArrayController and remove SCIGenericWrapper.swift from your Swift project.

Please check **[Append, Insert, Update, Remove](dataseries-apis.html#append-insert-update-remove)** section, to get more details.

#### Drawing assets creation
In v3.x we introduced `ISCIAssetManager2D` protocol, which is responsible for creating all the engine-specific drawing assets (pen, brushes, sprites, textures etc.).
So in your SciChart v2 depended code, you might see errors like:
```
- No visible @interface for 'SCISolidPenStyle' declares the selector 'initWithColorCode:withThickness:'
- No visible @interface for 'SCIRadialGradientBrushStyle' declares the selector 'initWithColorCodeStart:finish:'
- No visible @interface for 'SCILinearGradientBrushStyle' declares the selector 'initWithColorCodeStart:finish:direction:'
```

You can create an asset, using corresponding style object. The styles was refactored as well, so  `SCISolidPenStyle`, `SCILinearGradientPenStyle`, `SCIRadialGradientPenStyle`, `SCISolidBrushStyle`, `SCILinearGradientBrushStyle` and `SCIRadialGradientBrushStyle` have been changed. Here is an example, how to create  pen and brush in SciChart v3.x.

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // SciChart v2.x
    SCISolidPenStyle *strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF4282B4 withThickness:1.0];   
    SCILinearGradientBrushStyle *brushStyle = [[SCILinearGradientBrushStyle alloc] initWithColorCodeStart:0xFFfc9930 finish:0xFFd17f28 direction:SCILinearGradientDirection_Vertical];
    
    id&lt;SCIPen2DProtocol&gt pen = [renderContext createPenFromStyle:strokeStyle];
    id&lt;SCIPen2DProtocol&gt brush = [renderContext createBrushFromStyle:brushStyle];

    // SciChart v3.x
    // ISCIAssetManager2D instance is passed to each drawing method, 
    // so if you override drawing code - you never create assetManager instance on your own
    SCISolidPenStyle *strokeStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF4282B4 withThickness:1.0];
    SCILinearGradientBrushStyle *brushStyle = [[SCILinearGradientBrushStyle alloc] initWithStart:CGPointMake(0.5, 0.0) end:CGPointMake(0.5, 1.0) colorValues:colorValues stopValues:stopValues];
    
    id&lt;ISCIPen2D&gt pen = [assetManager penWithStyle:strokeStyle andOpacity:opacity];
    id&lt;ISCIBrush2D&gt brush = [assetManager brushWithStyle:brushStyle];
</div>
<div class="code-snippet" id="swift">
    // SciChart v2.x
    let strokeStyle = SCISolidPenStyle(colorCode: 0xFF4282B4, withThickness: 1.0)
    let brushStyle = SCILinearGradientBrushStyle(colorCodeStart: 0xFFa9d34f, finish: 0xFF93b944, direction: .vertical)

    let pen = renderContext.createPen(fromStyle: strokeStyle)
    let brush = renderContext.createBrush(fromStyle: strokeStyle)
    
    // SciChart v3.x
    // ISCIAssetManager2D instance is passed to each drawing method, 
    // so if you override drawing code - you never create assetManager instance on your own
    let strokeStyle = SCISolidPenStyle(colorCode: 0xFF4282B4, withThickness: 1.0)
    let brushStyle = SCILinearGradientBrushStyle(start: CGPoint(x: 0.5, y: 0.0), end: CGPoint(x: 0.5, y: 1.0), startColorCode: 0xAAFF8D42, endColorCode: 0x88090E11)
    
    let pen = assetManager.pen(withStyle: strokeStyle)
    let brush = assetManager.brush(withStyle: brushStyle)
</div>

This will affect your code if you have implemented custom drawing of some SciChart visual primitives.
For example, now customization of renderable series drawing you'll need to override the following method:

```objectivec
- (void)internalDrawWithContext:(id<ISCIRenderContext2D>)renderContext 
                    assetManager:(id<ISCIAssetManager2D>)assetManager 
                    renderPassData:(id<ISCISeriesRenderPassData>)renderPassData;
```

like demonstrated below:

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    - (void)internalDrawWithContext:(id&lt;ISCIRenderContext2D&gt;)renderContext assetManager:(id&lt;ISCIAssetManager2D&gt;)assetManager renderPassData:(id&lt;ISCISeriesRenderPassData&gt;)renderPassData {
        // Don't draw transparent series
        float opacity = self.opacity;
        if (opacity == 0) return;
        
        SCIPenStyle *strokeStyle = self.strokeStyle;
        if (strokeStyle == nil || !strokeStyle.isVisible) return;
        
        SCILineRenderPassData *rpd = (SCILineRenderPassData *)renderPassData;
        id&lt;ISCIDrawingContext&gt; linesStripDrawingContext = SCIDrawingContextFactory.linesStripDrawingContext;
        
        id&lt;ISCIPen2D&gt; pen = [assetManager penWithStyle:strokeStyle andOpacity:opacity];
        [self computeSplineSeriesWithX:_splineXCoords y:_splineYCoords renderPassData:rpd isSplineEnabled:self.isSplineEnabled upSampleFactor:self.upSampleFactor];
        
        BOOL digitalLine = rpd.isDigitalLine;
        BOOL closeGaps = rpd.closeGaps;

        id&lt;ISCISeriesDrawingManager&gt; drawingManager = [self.services getServiceOfType:@protocol(ISCISeriesDrawingManager)];
        [drawingManager beginDrawWithContext:renderContext renderPassData:rpd];

        [drawingManager iterateLinesWith:linesStripDrawingContext pathColor:pen xCoords:_splineXCoords yCoords:_splineYCoords isDigitalLine:digitalLine closeGaps:closeGaps];

        [drawingManager endDraw];
        
        [self drawPointMarkersWithContext:renderContext assetManager:assetManager xCoords:rpd.xCoords yCoords:rpd.yCoords];
    }
</div>
<div class="code-snippet" id="swift">
    override func internalDraw(with renderContext: ISCIRenderContext2D!, assetManager: ISCIAssetManager2D!, renderPassData: ISCISeriesRenderPassData!) {            
        // Don't draw transparent series
        guard self.opacity == 0 else { return }
        guard let strokeStyle = self.strokeStyle else { return }
        guard strokeStyle.isVisible else { return }
        
        let rpd = renderPassData as! SCILineRenderPassData
        let linesStripDrawingContext = SCIDrawingContextFactory.linesStripDrawingContext
        
        let pen = assetManager.pen(with: strokeStyle, andOpacity: opacity)
        computeSplineSeries(splineXCoords: splineXCoords, splineYCoords: splineYCoords, renderPassData: rpd, isSplineEnabled: self.isSplineEnabled, upSampleFactor: self.upSampleFactor)
        
        let digitalLine = rpd.isDigitalLine
        let closeGaps = rpd.closeGaps
        
        let drawingManager = services.getServiceOfType(ISCISeriesDrawingManager.self) as! ISCISeriesDrawingManager
        drawingManager.beginDraw(with: renderContext, renderPassData: rpd)
        
        drawingManager.iterateLines(with: linesStripDrawingContext, pathColor: pen, xCoords: splineXCoords, yCoords: splineYCoords, isDigitalLine: digitalLine, closeGaps: closeGaps)
        
        drawingManager.endDraw()
        
        drawPointMarkers(with: renderContext, assetManager: assetManager, xCoords: rpd.xCoords, yCoords: rpd.yCoords)
    }
</div>

> **_NOTE:_** To see the full example which uses the code snippet above, please check the **SplineLineRenderableSeries** class in our [Spline Scatter Line Chart](https://www.scichart.com/example/ios-chart-custom-series-spline-line/) example which can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples/tree/SciChart_v3_Release).

#### Observable collections
`SCIObservableCollection` API was refactored - some methods of concrete classes have been removed, or partially moved to a base class.

So in your SciChart v2 depended code, you might see errors like:
```
- No visible @interface for 'SCIChartModifierCollection' declares the selector 'initWithChildModifiers:'
```

To fix this, please check the example code for setting chart modifiers to a surface: 

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // SciChart v2.x
    self.surface.chartModifiers = [[SCIChartModifierCollection alloc] initWithChildModifiers:@[[SCIPinchZoomModifier new], [SCIZoomExtentsModifier new], [SCIZoomPanModifier new]]];

    // SciChart v3.x
    self.surface.chartModifiers = [[SCIChartModifierCollection alloc] initWithCollection:@[[SCIPinchZoomModifier new], [SCIZoomExtentsModifier new], [SCIZoomPanModifier new]]];
</div>
<div class="code-snippet" id="swift">
    // SciChart v2.x
    self.surface.chartModifiers = SCIChartModifierCollection(childModifiers: [SCIPinchZoomModifier(), SCIZoomExtentsModifier(), SCIZoomPanModifier()])
    
    // SciChart v3.x
    self.surface.chartModifiers = SCIChartModifierCollection(collection: [SCIPinchZoomModifier(), SCIZoomExtentsModifier(), SCIZoomPanModifier()])
</div>

#### Animations

To become more flexible, and have better integration into SciChart run loop - [Animation API](animations-api.html) has been completely re-designed and re-written.
In SciChart v3.x we introduced `SCIAnimations` utility, which provides an interface to create basic animations for renderable series.

So in your SciChart v2 depended code, you might see errors like:
```
- Unknown receiver 'SCISweepRenderableSeriesAnimation'
- Unknown receiver 'SCIWaveRenderableSeriesAnimation'
- Unknown receiver 'SCIScaleRenderableSeriesAnimation'
```

The example below demonstrates the difference of animation creation in v2x and v3.x, end explains how to fix these errors.

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // SciChart v2.x
    [rSeries addAnimation:[[SCISweepRenderableSeriesAnimation alloc] initWithDuration:3 curveAnimation:SCIAnimationCurve_EaseOut]];
    
    // SciChart v3.x
    [SCIAnimations sweepSeries:rSeries duration:3.0 andEasingFunction:[SCICubicEase new]];
</div>
<div class="code-snippet" id="swift">
    // SciChart v2.x
    rSeries.addAnimation(SCISweepRenderableSeriesAnimation(duration: 3, curveAnimation: .easeOut))

    // SciChart v3.x
    SCIAnimations.sweep(rSeries, duration: 3.0, easingFunction: SCICubicEase())
</div>
<div class="code-snippet" id="cs">
    // SciChart v2.x
    rSeries.AddAnimation(new SCISweepRenderableSeriesAnimation(3, SCIAnimationCurve.EaseOut));
    
    // SciChart v3.x
    SCIAnimations.SweepSeries(rSeries, 3, new SCICubicEase());
</div>

To get more details about existing easing functions and animation types, please check [Animation API](animations-api.html) guide. 

#### Annotations API
All the annotations in SciChart v3.x conforms to the `ISCIAnnotation` protocol and inherits `SCIAnnotationBase` class.
To get more info about existing annotation types and common functionality of the `SCIAnnotationBase` please check the documentation for [Annotations API](Annotations APIs.html).
The most prominent changes to the [Annotations API](Annotations APIs.html) are the following:
- As the `SCIGenericType` was removed, now annotation coordinates became `ISCIComparable`.
- **AnnotationStyle** classes (e.g `SCILineAnnotationStyle`, `SCITextAnnotationStyle`) were removed, their properties either moved to corresponding annotation class, or to the `SCIAnnotationBase`.

So in your SciChart v2 depended code, you might see errors like:
```
- Property 'style' not found on object of type 'SCITextAnnotation *'
- Property 'style' not found on object of type 'SCIHorizontalLineAnnotation *'
```

Here is how the creation of `SCIHorizontalLineAnnotation` looks now:

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // SciChart v2.x
    SCIHorizontalLineAnnotation *horizontalLine = [SCIHorizontalLineAnnotation new];
    horizontalLine.x1 = SCIGeneric(5.0);
    horizontalLine.y1 = SCIGeneric(3.2);
    horizontalLine.horizontalAlignment = SCIHorizontalLineAnnotationAlignment_Right;
    horizontalLine.style.linePen = [[SCISolidPenStyle alloc] initWithColor:UIColor.orangeColor withThickness:2];

    SCILineAnnotationLabel *lineAnnotationLabel = [SCILineAnnotationLabel new];
    lineAnnotationLabel.text = @"Text";
    lineAnnotationLabel.style.labelPlacement = SCILabelPlacement_TopLeft;
    
    [horizontalLine addLabel:lineAnnotationLabel];

    // SciChart v3.x
    SCIHorizontalLineAnnotation *horizontalLine = [SCIHorizontalLineAnnotation new];
    horizontalLine.x1 = @(5.0);
    horizontalLine.y1 = @(3.2);
    horizontalLine.verticalAlignment = SCIAlignment_Right;
    horizontalLine.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFA52A2A thickness:2];

    SCIAnnotationLabel *annotationLabel = [SCIAnnotationLabel new];
    annotationLabel.text = @"Text";
    annotationLabel.labelPlacement = SCILabelPlacement_TopLeft;

    [horizontalLine.annotationLabels add:annotationLabel];
</div>
<div class="code-snippet" id="swift">
    // SciChart v2.x
    let horizontalLine = SCIHorizontalLineAnnotation()
    horizontalLine.x1 = SCIGeneric(5.0)
    horizontalLine.y1 = SCIGeneric(3.2)
    horizontalLine.horizontalAlignment = .right
    horizontalLine.style.linePen = SCISolidPenStyle(color: UIColor.orange, withThickness: 2)

    let lineAnnotationLabel = SCILineAnnotationLabel()
    lineAnnotationLabel.text = "Text"
    lineAnnotationLabel.style.labelPlacement = .topLeft

    horizontalLine.add(lineAnnotationLabel)

    // SciChart v3.x
    let horizontalLine = SCIHorizontalLineAnnotation()
    horizontalLine.set(x1: 5.0)
    horizontalLine.set(y1: 3.2)
    horizontalLine.horizontalAlignment = .right
    horizontalLine.stroke = SCISolidPenStyle(color: UIColor.orange, thickness: 2)

    SCIAnnotationLabel *annotationLabel = [SCIAnnotationLabel new];
    annotationLabel.text = @"Text";
    annotationLabel.labelPlacement = .topLeft

    horizontalLine.annotationLabels.add(annotationLabel)
</div>
<div class="code-snippet" id="cs">
    // SciChart v2.x
    var horizontalLine1 = new SCIHorizontalLineAnnotation
    {
        X1Value = 5.0,
        Y1Value = 3.2,
        HorizontalAlignment = SCIHorizontalLineAnnotationAlignment.Right,
        Style = new SCILineAnnotationStyle { LinePen = new SCISolidPenStyle(UIColor.Orange, 2f) },
    };
    horizontalLine1.AddLabel(new SCILineAnnotationLabel
    {
        Text = "Right Aligned, with text on left",
        Style = { LabelPlacement = SCILabelPlacement.TopLeft }
    });
    
    // SciChart v3.x
    var horizontalLine1 = new SCIHorizontalLineAnnotation
    {
        X1Value = 5.0,
        Y1Value = 3.2,
        HorizontalAlignment = SCIAlignment.Right,
        Stroke = new SCISolidPenStyle(UIColor.Orange, 2),
    };
       
    horizontalLine1.AnnotationLabels.Add(new SCIAnnotationLabel
    {
        LabelPlacement = SCILabelPlacement.TopLeft,
        Text = "Right Aligned, with text on left",
    });
</div>

#### Chart Modifiers API
In SciChart v3.x [Chart Modifiers API](Chart Modifier APIs.html) was ***deeply refactored***, became more flexible, easy to use and have now more clear customization points.
To get more information about all available chart modifier types, please visit the [Chart Modifier APIs](Chart Modifier APIs.html) article.

Breaking changes of the **Chart Modifiers API**, which might impact your code are listed below:
- ModifierStyle's objects have been removed and their properties moved to corresponding modifier classes (The same way like with annotations).
- All the modifiers in SciChart v3.x now conforms to `ISCIChartModifierCore` protocol and inherits `SCIChartModifierCore` base class. 
- `SCIGestureController` class was removed, and now all the chart modifiers are driven by the native [UIGestureRecognizer](https://developer.apple.com/documentation/uikit/uigesturerecognizer) 
- Gesture Modifiers are now inherited from the `SCIGestureModifierBase` or `SCIGestureModifierBase3D`. Each gesture modifier is responsible for creation its own [UIGestureRecognizer](https://developer.apple.com/documentation/uikit/uigesturerecognizer) while the `SCIGestureModifierBase` setups **target-action relationship** between gesture recognizer and modifier instance for you. So, gesture modifier now has more points for **customization**, including possibility to set your custom [UIGestureRecognizerDelegate](https://developer.apple.com/documentation/uikit/uigesturerecognizerdelegate):

```objectivec
        // Override this method to make modifier work with custom gesture recognizer
        - (UIGestureRecognizer *)createGestureRecognizer;
            
        // Override this method to handle gesture recognizers action
        - (void)internalHandleGesture:(UIPinchGestureRecognizer *)gestureRecognizer 
```

In addition to the above - `SCIRolloverModifier`, `SCITooltipModifier`, and `SCICursorModifier` are became much more flexible.
You don't need to subclass renderable series to make custom tooltip view for it.
Now you can just inject custom `ISCISeriesInfoProvider` with all needed customizations instead.

So in your SciChart v2 depended code, you might see errors like:
```
- Property 'style' not found on object of type 'SCICursorModifier *'
- Cannot find interface declaration for 'SCIGestureModifier', superclass of 'CustomModifier'; did you mean 'SCIGestureModifierBase'?
- Interface type 'SCIHitTestInfo' cannot be passed by value
- Property 'match' not found on object of type 'SCIHitTestInfo *'
```

Here is an example of `SCIRolloverModifier` customization (the rest of listed modifiers have quite the same customization logic):

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // SciChart v2.x
    @interface CustomRolloverSeriesInfo : SCIXySeriesInfo
    @end

    @implementation CustomRolloverSeriesInfo
    - (SCITooltipDataView *)createDataSeriesView {
        CustomTooltipView * view = (CustomTooltipView *)[CustomTooltipView createInstance];
        [view setData:self];
        
        return view;
    }
    @end

    @interface CustomRolloverLineSeries : SCIFastLineRenderableSeries
    @end

    @implementation CustomRolloverLineSeries
    - (SCISeriesInfo *)toSeriesInfoWithHitTest:(SCIHitTestInfo)info {
        return [[CustomRolloverSeriesInfo alloc] initWithSeries:self HitTest:info];
    }
    @end
    
    SCIFastLineRenderableSeries * line1 = [CustomRolloverLineSeries new];
  
    SCIEllipsePointMarker * pointMarker = [SCIEllipsePointMarker new];
    pointMarker.strokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.grayColor withThickness:0.5f];
    pointMarker.width = 10;
    pointMarker.height = 10;
       
    SCIRolloverModifier * rolloverModifier = [SCIRolloverModifier new];
    rolloverModifier.style.rolloverPen = [[SCISolidPenStyle alloc] initWithColor:UIColor.greenColor withThickness:0.5];
    rolloverModifier.style.axisTooltipColor = [UIColor fromARGBColorCode:0xff6495ed];
    rolloverModifier.style.pointMarker = pointMarker;
 
    [self.surface.renderableSeries add:line1];
    [self.surface.chartModifiers add:rolloverModifier];

    // SciChart v3.x
    @interface FirstCustomXySeriesTooltip : SCIXySeriesTooltip
    @end
    @implementation FirstCustomXySeriesTooltip
    - (void)internalUpdateWithSeriesInfo:(SCIXySeriesInfo *)seriesInfo {
       NSString *string = NSString.Empty;
       string = [string stringByAppendingFormat:@"X: %@\n", seriesInfo.formattedXValue.rawString];
       string = [string stringByAppendingFormat:@"Y: %@\n", seriesInfo.formattedYValue.rawString];
       if (seriesInfo.seriesName != nil) {
           string = [string stringByAppendingFormat:@"%@\n", seriesInfo.seriesName];
       }
       string = [string stringByAppendingString:@"Rollover Modifier"];
       self.text = string;
       
       [self setTooltipBackground:0xffe2460c];
       [self setTooltipStroke:0xffff4500];
       [self setTooltipTextColor:0xffffffff];
    }
    @end

    @interface FirstCustomRolloverSeriesInfoProvider : SCIDefaultXySeriesInfoProvider
    @end
    @implementation FirstCustomRolloverSeriesInfoProvider
    - (id<ISCISeriesTooltip>)getSeriesTooltipInternalWithSeriesInfo:(SCIXySeriesInfo *)seriesInfo modifierType:(Class)modifierType {
        if (modifierType == SCIRolloverModifier.class) {
            return [[FirstCustomXySeriesTooltip alloc] initWithSeriesInfo:seriesInfo];
        } else {
            return [super getSeriesTooltipInternalWithSeriesInfo:seriesInfo modifierType:modifierType];
        }
    }

    SCIFastLineRenderableSeries *line1 = [SCIFastLineRenderableSeries new];
    line1.seriesInfoProvider = [FirstCustomRolloverSeriesInfoProvider new];
    
    [self.surface.renderableSeries add:line1];
    [self.surface.chartModifiers add:[SCIRolloverModifier new]];

    @end
</div>
<div class="code-snippet" id="swift">
    // SciChart v2.x
    fileprivate class CustomSeriesInfo: SCIXySeriesInfo {
          override func createDataSeriesView() -> SCITooltipDataView! {
              let view : CustomTooltipViewSwift = CustomTooltipViewSwift.createInstance() as! CustomTooltipViewSwift
              view.setData(self)
              
              return view
          }
      }

      fileprivate class CustomLineSeries: SCIFastLineRenderableSeries {
          override func toSeriesInfo(withHitTest info: SCIHitTestInfo) -> SCISeriesInfo! {
              return CustomSeriesInfo(series: self, hitTest: info)
          }
      }

      let line1 = CustomLineSeries()

      let pointMarker = SCIEllipsePointMarker()
      pointMarker.strokeStyle = SCISolidPenStyle(color: UIColor.gray, withThickness: 0.5)
      pointMarker.width = 10
      pointMarker.height = 10

      let rolloverModifier = SCIRolloverModifier()
      rolloverModifier.style.contentPadding = 0
      rolloverModifier.style.colorMode = .default
      rolloverModifier.style.tooltipColor = UIColor.fromARGBColorCode(0xffe2460c)
      rolloverModifier.style.tooltipOpacity = 0.8
      rolloverModifier.style.tooltipBorderWidth = 1
      rolloverModifier.style.tooltipBorderColor = UIColor.fromARGBColorCode(0xff6495ed)
      rolloverModifier.style.axisTooltipColor = UIColor.fromARGBColorCode(0xff6495ed)
      rolloverModifier.style.pointMarker = pointMarker
      
      surface.renderableSeries.add(line1)
      surface.chartModifiers.add(rolloverModifier)

    // SciChart v3.x
    private class FirstCustomSeriesInfoProvider: SCIDefaultXySeriesInfoProvider {
        class FirstCustomXySeriesTooltip: SCIXySeriesTooltip {
            override func internalUpdate(with seriesInfo: SCIXySeriesInfo!) {
                var string = NSString.empty;
                string += "X: \(seriesInfo.formattedXValue.rawString!)\n"
                string += "Y: \(seriesInfo.formattedXValue.rawString!)\n"
                if let seriesName = seriesInfo.seriesName {
                    string += "\(seriesName)\n"
                }
                string += "Rollover Modifier"
                self.text = string;
                
                setTooltipBackground(0xffe2460c);
                setTooltipStroke(0xffff4500);
                setTooltipTextColor(0xffffffff);
            }
        }

        override func getSeriesTooltipInternal(with seriesInfo: SCIXySeriesInfo!, modifierType: AnyClass!) -> ISCISeriesTooltip! {
            if (modifierType == SCIRolloverModifier.self) {
                return FirstCustomXySeriesTooltip(seriesInfo: seriesInfo)
            } else {
                return super.getSeriesTooltipInternal(with: seriesInfo, modifierType: modifierType)
            }
        }
    }

    let line1 = SCIFastLineRenderableSeries()
    line1.seriesInfoProvider = FirstCustomSeriesInfoProvider()
    
    self.surface.renderableSeries.add(line1)
    self.surface.chartModifiers.add(SCIRolloverModifier())
</div>

> **_NOTE:_** The full **Customization RolloverModifier** example can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-tooltips-using-rollovermodifier/)

Also in SciChart v3 we improved a mechanism of syncing modifiers, of multiple surfaces. As a result,SCIMultiSurfaceModifier, SCIAxisAreaSizeSynchronization and SCIAxisRangeSynchronization classes got removed. 

As the result,  in your SciChart v2 depended code, you might see errors like:
```
- Unknown type name 'SCIAxisRangeSynchronization'
- Unknown type name 'SCIMultiSurfaceModifier'
```

So, now to sync ranges of two axises (from different surfaces) you'll need just assign both of them the same instance of
visibleRange. And to synchronize modifiers - you'll need just create an SCICharModifierGroup with the same event tag for both surfaces, and put in this group all the modifiers you want to be synchronized:


<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    
    // SciChart v2.x
    SCIAxisRangeSynchronization * _rangeSync = [SCIAxisRangeSynchronization new];

    SCIMultiSurfaceModifier * _zoomExtentsModifierSync  = [[SCIMultiSurfaceModifier alloc] initWithModifierType:[SCIZoomExtentsModifier class]];
    SCIMultiSurfaceModifier * _pinchZoomModifierSync = [[SCIMultiSurfaceModifier alloc] initWithModifierType:[SCIPinchZoomModifier class]];
    SCIMultiSurfaceModifier * _xDragModifierSync = [[SCIMultiSurfaceModifier alloc] initWithModifierType:[SCIXAxisDragModifier class]];
    SCIMultiSurfaceModifier * _yDragModifierSync = [[SCIMultiSurfaceModifier alloc] initWithModifierType:[SCIYAxisDragModifier class]];
    SCIMultiSurfaceModifier * _rolloverModifierSync = [[SCIMultiSurfaceModifier alloc] initWithModifierType:[SCIRolloverModifier class]];
    
    id&lt;SCIAxis2DProtocol&gt; firstSurfaceXAxis = [SCINumericAxis new];
    firstSurfaceXAxis.growBy = [[SCIDoubleRange alloc]initWithMin:SCIGeneric(0.1) Max:SCIGeneric(0.1)];
    
    id&lt;SCIAxis2DProtocol&gt; firstSurfaceYAxis = [SCINumericAxis new];
    firstSurfaceYAxis.growBy = [[SCIDoubleRange alloc]initWithMin:SCIGeneric(0.1) Max:SCIGeneric(0.1)];
    
    id&lt;SCIAxis2DProtocol&gt; secondSurfaceXAxis = [SCINumericAxis new];
    secondSurfaceXAxis.growBy = [[SCIDoubleRange alloc]initWithMin:SCIGeneric(0.1) Max:SCIGeneric(0.1)];
    
    id&lt;SCIAxis2DProtocol&gt; secondSurfaceYAxis = [SCINumericAxis new];
    secondSurfaceYAxis.growBy = [[SCIDoubleRange alloc]initWithMin:SCIGeneric(0.1) Max:SCIGeneric(0.1)];
    
    [surface1.xAxes add:firstSurfaceXAxis];
    [surface1.yAxes add:firstSurfaceYAxis];
    surface1.1chartModifiers = [[SCIChartModifierCollection alloc] initWithChildModifiers:@[_xDragModifierSync, _yDragModifierSync, _pinchZoomModifierSync, _zoomExtentsModifierSync, _rolloverModifierSync]];
    [_rangeSync attachAxis:firstSurfaceXAxis];
    
    [surface2.xAxes add:secondSurfaceXAxis];
    [surface2.yAxes add:secondSurfaceYAxis];
    surface2.1chartModifiers = [[SCIChartModifierCollection alloc] initWithChildModifiers:@[_xDragModifierSync, _yDragModifierSync, _pinchZoomModifierSync, _zoomExtentsModifierSync, _rolloverModifierSync]];
    [_rangeSync attachAxis:secondSurfaceXAxis];

    // SciChart v3.x
    SCIDoubleRange *sharedXRange = [[SCIDoubleRange alloc] initWithMin:0 max:1];
    SCIDoubleRange *sharedYRange = [[SCIDoubleRange alloc] initWithMin:0 max:1];
    
    id&lt;ISCIAxis&gt; firstSurfaceXAxis = [SCINumericAxis new];
    firstSurfaceXAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    firstSurfaceXAxis.visibleRange = sharedXRange;

    id&lt;ISCIAxis&gt; firstSurfaceYAxis = [SCINumericAxis new];
    firstSurfaceYAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    firstSurfaceYAxis.visibleRange = sharedYRange;
      
    id&lt;ISCIAxis&gt; secondSurfaceXAxis = [SCINumericAxis new];
    secondSurfaceXAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    secondSurfaceXAxis.visibleRange = sharedXRange;

    id&lt;ISCIAxis&gt; secondSurfaceYAxis = [SCINumericAxis new];
    secondSurfaceYAxis.growBy = [[SCIDoubleRange alloc] initWithMin:0.1 max:0.1];
    secondSurfaceYAxis.visibleRange = sharedYRange;
      
    SCIModifierGroup *firstSurfaceModifierGroup = [SCIModifierGroup new];
    firstSurfaceModifierGroup.eventGroup = @"SharedEventGroup";
    firstSurfaceModifierGroup.receiveHandledEvents = YES;
    [firstSurfaceModifierGroup.childModifiers addAll:[SCIZoomExtentsModifier new], [SCIPinchZoomModifier new], rolloverModifier, [SCIXAxisDragModifier new], [SCIYAxisDragModifier new], nil];
      
    SCIModifierGroup *secondSurfaceModifierGroup = [SCIModifierGroup new];
    secondSurfaceModifierGroup.eventGroup = @"SharedEventGroup";
    secondSurfaceModifierGroup.receiveHandledEvents = YES;
    [secondSurfaceModifierGroup.childModifiers addAll:[SCIZoomExtentsModifier new], [SCIPinchZoomModifier new], rolloverModifier, [SCIXAxisDragModifier new], [SCIYAxisDragModifier new], nil];
      
    [surface1.xAxes add:firstSurfaceXAxis];
    [surface1.yAxes add:firstSurfaceYAxis];
    [surface1.chartModifiers add:firstSurfaceModifierGroup];
      
    [surface2.xAxes add:secondSurfaceXAxis];
    [surface2.yAxes add:secondSurfaceYAxis];
    [surface2.chartModifiers add:secondSurfaceModifierGroup];

</div>
<div class="code-snippet" id="swift">

    // SciChart v2.x

    let _rangeSync = SCIAxisRangeSynchronization()
    let _zoomExtentsModifierSync = SCIMultiSurfaceModifier(modifierType: SCIZoomExtentsModifier.self)
    let _pinchZoomModifierSync = SCIMultiSurfaceModifier(modifierType: SCIPinchZoomModifier.self)
    let _xDragModifierSync = SCIMultiSurfaceModifier(modifierType: SCIXAxisDragModifier.self)
    let _yDragModifierSync = SCIMultiSurfaceModifier(modifierType: SCIYAxisDragModifier.self)
    let _rolloverModifierSync = SCIMultiSurfaceModifier(modifierType: SCIRolloverModifier.self)
    
    let firstSurfaceXAxis = SCINumericAxis()
    firstSurfaceXAxis.growBy = SCIDoubleRange(min: SCIGeneric(0.1), max: SCIGeneric(0.1))
    let firstSurfaceYAxis = SCINumericAxis()
    firstSurfaceYAxis.growBy = SCIDoubleRange(min: SCIGeneric(0.1), max: SCIGeneric(0.1))
    
    let secondSurfaceXAxis = SCINumericAxis()
    secondSurfaceXAxis.growBy = SCIDoubleRange(min: SCIGeneric(0.1), max: SCIGeneric(0.1))
    let secondSurfaceYAxis = SCINumericAxis()
    secondSurfaceYAxis.growBy = SCIDoubleRange(min: SCIGeneric(0.1), max: SCIGeneric(0.1))

    surface1.xAxes.add(firstSurfaceXAxis)
    surface1.yAxes.add(firstSurfaceYAxis)
    surface1.chartModifiers = SCIChartModifierCollection(childModifiers: [_xDragModifierSync, _yDragModifierSync, _pinchZoomModifierSync, _zoomExtentsModifierSync, _rolloverModifierSync])
    _rangeSync.attachAxis(firstSurfaceXAxis)
    
    surface2.xAxes.add(secondSurfaceXAxis)
    surface2.yAxes.add(secondSurfaceYAxis)
    surface2.chartModifiers = SCIChartModifierCollection(childModifiers: [_xDragModifierSync, _yDragModifierSync, _pinchZoomModifierSync, _zoomExtentsModifierSync, _rolloverModifierSync])
    _rangeSync.attachAxis(secondSurfaceXAxis)
    
    // SciChart v3.x
    
    let SharedXRange = SCIDoubleRange(min: 0, max: 1)
    let SharedYRange = SCIDoubleRange(min: 0, max: 1)
    
    let firstSurfaceXAxis = SCINumericAxis()
    firstSurfaceXAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    firstSurfaceXAxis.visibleRange = SharedXRange
          
    let firstSurfaceYAxis = SCINumericAxis()
    firstSurfaceYAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    firstSurfaceYAxis.visibleRange = SharedYRange
    
    let secondSurfaceXAxis = SCINumericAxis()
    secondSurfaceXAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    secondSurfaceXAxis.visibleRange = SharedXRange
           
    let secondSurfaceYAxis = SCINumericAxis()
    secondSurfaceYAxis.growBy = SCIDoubleRange(min: 0.1, max: 0.1)
    secondSurfaceYAxis.visibleRange = SharedYRange
    
    let firstSurfaceModifierGroup = SCIModifierGroup()
    firstSurfaceModifierGroup.eventGroup = "SharedEventGroup"
    firstSurfaceModifierGroup.receiveHandledEvents = true
    firstSurfaceModifierGroup.childModifiers.add(items: SCIZoomExtentsModifier(), SCIPinchZoomModifier(), rolloverModifier, SCIXAxisDragModifier(), SCIYAxisDragModifier())
 
    let secondSurfaceModifierGroup = SCIModifierGroup()
    secondSurfaceModifierGroup.eventGroup = "SharedEventGroup"
    secondSurfaceModifierGroup.receiveHandledEvents = true
    secondSurfaceModifierGroup.childModifiers.add(items: SCIZoomExtentsModifier(), SCIPinchZoomModifier(), rolloverModifier, SCIXAxisDragModifier(), SCIYAxisDragModifier())
    
    surface1.xAxes.add(firstSurfaceXAxis)
    surface1.yAxes.add(firstSurfaceYAxis)
    surface1.chartModifiers.add(firstSurfaceModifierGroup)
    
    surface2.xAxes.add(secondSurfaceXAxis)
    surface2.yAxes.add(secondSurfaceYAxis)
    surface2.chartModifiers.add(secondSurfaceModifierGroup)
    
</div>

> **_NOTE:_** The full **Sync Multiple Charts** example can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-multiple-surfaces-demo//)
