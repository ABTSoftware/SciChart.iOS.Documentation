# SCISeriesSelectionModifier
SciChart features the `SCISeriesSelectionModifier`, which allows selection of the upmost [RenderableSeries](2D Chart Types.html) at a touch position:

![Series Selection Modifier](img/modifiers-2d/series-selection-modifier.png)

> **_NOTE:_** Examples of the `SCISeriesSelectionModifier` usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-series-selection/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-chart-series-selection-example/)

## SCISeriesSelectionModifier Usage
The `SCISeriesSelectionModifier` allows setting of **SelectedSeriesStyle** of `ISCIStyle` type, which is applied to a [RenderableSeries](2D Chart Types.html) after it has been selected by the modifier. 
Internally, the modifier modifies the `ISCIRenderableSeriesCore.isSelected` property on the topmost **RenderableSeries** to make it selected and tries to apply the `ISCIStyle` onto `ISCIRenderableSeriesCore.selectedSeriesStyle` property.

The **SelectedSeriesStyle** has to conform to the `ISCIStyle` protocol, which requires implementation of the following:

| **Method**                          | **Description**                                                                                                                                        |
| ----------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `-[ISCIStyle tryApplyStyleTo:]`     | In this method you have to **make** the desired changes to a [RenderableSeries](2D Chart Types.html). It is called when a series gets selected. |
| `-[ISCIStyle tryDiscardStyleFrom:]` | In this method you have to **discard** all the changes made in the `-[ISCIStyle tryApplyStyleTo:]` call.                                               |
| `ISCIStyle.styleableObjectType`     | Provides the type of an object (**RenderableSeries**) that this Style is to be applied to.                                                             |

For convenience purposes, there is a base implementation provided in `SCIStyleBase` class. 
You can **derive** from it then you will only need to implement the `-applyStyleInternalTo:` and `-discardStyleInternalFrom:` methods instead. 
Please find a code sample [below](#adding-a-sciseriesselectionmodifier-to-a-chart).

> **_NOTE:_** If you need to be able **to select** multiple RenderableSeries of **different types**, you can return the `ISCIRenderableSeries` as the RenderableSeries type, as it is shown in the code sample below. 
> Alternatively, it is possible to have several `SCISeriesSelectionModifier`s instances with `ISCIStyle`s for different RenderableSeries types with the `receiveHandledEvents = YES`.

## Adding a SCISeriesSelectionModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the `ISCIChartSurface.chartModifiers` property and `SCISeriesSelectionModifier` with no difference.

In the example below, we will create **SelectedSeriesStyle** and use it with `SCISeriesSelectionModifier`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCIStyleBase+Protected.h&gt;

    @interface SelectedSeriesStyle : SCIStyleBase&lt;id&lt;ISCIRenderableSeries&gt;&gt;
    @end
    @implementation SelectedSeriesStyle {
        SCISolidPenStyle *_selectedStrokeStyle;
        id&lt;ISCIPointMarker&gt; _selectedPointMarker;
    }

    - (instancetype)init {
        self = [super initWithStyleableType:SCIRenderableSeriesBase.class];
        if (self) {
            _selectedPointMarker = [SCIEllipsePointMarker new];
            _selectedPointMarker.size = CGSizeMake(10, 10);
            _selectedPointMarker.fillStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0xFFFF00DC];
            _selectedPointMarker.strokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.whiteColor thickness:1];
            
            _selectedStrokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.whiteColor thickness:4];
        }
        return self;
    }

    - (void)applyStyleInternalTo:(id&lt;ISCIRenderableSeries&gt;)styleableObject {
        [self putProperty:Stroke value:styleableObject.strokeStyle intoObject:styleableObject];
        [self putProperty:PointMarker value:styleableObject.pointMarker intoObject:styleableObject];
        
        styleableObject.strokeStyle = _selectedStrokeStyle;
        styleableObject.pointMarker = _selectedPointMarker;
    }

    - (void)discardStyleInternalFrom:(id&lt;ISCIRenderableSeries&gt;)styleableObject {
        SCIPenStyle *penStyle = [self getValueFromProperty:Stroke ofType:SCISolidPenStyle.class fromObject:styleableObject];
        id&lt;ISCIPointMarker&gt; pointMarker = [self getValueFromProperty:PointMarker ofType:SCIEllipsePointMarker.class fromObject:styleableObject];
        
        styleableObject.strokeStyle = penStyle;
        styleableObject.pointMarker = pointMarker;
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class SelectedSeriesStyle: SCIStyleBase&lt;ISCIRenderableSeries&gt; {

        let Stroke = "Stroke"
        let selectedStrokeStyle = SCISolidPenStyle(color: .white, thickness: 4)
        let PointMarker = "PointMarker"
        let selectedPointMarker: ISCIPointMarker
        
        init() {
            selectedPointMarker = SCIEllipsePointMarker()
            selectedPointMarker.size = CGSize(width: 10, height: 10)
            selectedPointMarker.fillStyle = SCISolidBrushStyle(colorCode: 0xFFFF00DC)
            selectedPointMarker.strokeStyle = SCISolidPenStyle(color: .white, thickness: 1)
            
            super.init(styleableType: SCIRenderableSeriesBase.self)
        }
        
        override func applyStyleInternal(to styleableObject: ISCIRenderableSeries!) {
            putProperty(Stroke, value: styleableObject.strokeStyle, intoObject: styleableObject)
            putProperty(PointMarker, value: styleableObject.pointMarker, intoObject: styleableObject)
            
            styleableObject.strokeStyle = selectedStrokeStyle
            styleableObject.pointMarker = selectedPointMarker
        }
        
        override func discardStyleInternal(from styleableObject: ISCIRenderableSeries!) {
            let penStyle = getValueFromProperty(Stroke, ofType: SCISolidPenStyle.self, fromObject: styleableObject)
            let pointMarker = getValueFromProperty(PointMarker, ofType: ISCIPointMarker.self, fromObject: styleableObject)
            
            styleableObject.strokeStyle = penStyle as? SCIPenStyle
            styleableObject.pointMarker = pointMarker as? ISCIPointMarker
        }
    }
</div>
<div class="code-snippet" id="cs">
    private class SelectedSeriesStyle : SCIStyleBase&lt;SCIRenderableSeriesBase&gt;
    {
        private readonly SCIPenStyle _selectedStrokeStyle = new SCISolidPenStyle(UIColor.White, 4f);
        private readonly SCIEllipsePointMarker _selectedPointMarker;

        private const string Stroke = "Stroke";
        private const string PointMarker = "PointMarker";

        public SelectedSeriesStyle()
        {
            _selectedPointMarker = new SCIEllipsePointMarker
            {
                Size = new CGSize(10, 10),
                FillStyle = new SCISolidBrushStyle(0xFFFF00DC),
                StrokeStyle = new SCISolidPenStyle(UIColor.White, 1f)
            };
        }

        protected override void ApplyStyle(SCIRenderableSeriesBase styleableObject)
        {
            PutPropertyValue(styleableObject, Stroke, styleableObject.StrokeStyle);
            PutPropertyValue(styleableObject, PointMarker, styleableObject.PointMarker);

            styleableObject.StrokeStyle = _selectedStrokeStyle;
            styleableObject.PointMarker = _selectedPointMarker;
        }

        protected override void DiscardStyle(SCIRenderableSeriesBase styleableObject)
        {
            styleableObject.StrokeStyle = GetPropertyValue<SCIPenStyle>(styleableObject, Stroke);
            styleableObject.PointMarker = GetPropertyValue<SCIEllipsePointMarker>(styleableObject, PointMarker);
        }
    }
</div>

Now, create and add `SCISeriesSelectionModifier` onto your `SCIChartSurface`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a SCISeriesSelectionModifier
    SCISeriesSelectionModifier *seriesSelectionModifier = [SCISeriesSelectionModifier new];

    // Set a style which will be applied to a RenderableSeries when selected
    seriesSelectionModifier.selectedSeriesStyle = [SelectedSeriesStyle new];

    // Add the modifier to the surface
    [self.surface.chartModifiers add:seriesSelectionModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a SCISeriesSelectionModifier
    let seriesSelectionModifier = SCISeriesSelectionModifier()

    // Set a style which will be applied to a RenderableSeries when selected
    seriesSelectionModifier.selectedSeriesStyle = SelectedSeriesStyle()

    // Add the modifier to the surface
    self.surface.chartModifiers.add(seriesSelectionModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a SCISeriesSelectionModifier with a style which will be applied to a RenderableSeries when selected
    var seriesSelectionModifier = new SCISeriesSelectionModifier { SelectedSeriesStyle = new SelectedSeriesStyle() };

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(seriesSelectionModifier);
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.
