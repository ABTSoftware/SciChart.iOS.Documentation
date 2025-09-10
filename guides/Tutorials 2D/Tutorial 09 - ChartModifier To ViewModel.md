# SciChart iOS Tutorial - ChartModifier To ViewModel
This is key when you're trying to detect tap/click under the mouse/touch and get datapoints or SeriesInfo out from the modifier and into another ViewModel.

To extract data point information when a tooltip is displayed or updated, implement a custom SeriesInfoProvider. This approach enables you to intercept tooltip updates and access formatted values such as X and Y, which can then be forwarded to your chart view, model or another handler.

This approach allows you to:
- Intercept updates when the tooltip is shown or refreshed.
- Extract the formatted X/Y values of the touched data point.
- Forward that information to your chart view or any other handler.

> **_NOTE:_** This approach works with all tooltip-based modifiers, including SCITooltipModifier, SCIRolloverModifier, and SCICursorModifier.

To begin, implement the required protocols and subclass SCIDefaultXySeriesInfoProvider and SCIXySeriesTooltip:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// CustomSeriesInfoProvider.h file
#import &lt;SciChart/SCISeriesTooltipBase+Protected.h&gt;
#import &lt;SciChart/SCISeriesInfoProviderBase+Protected.h&gt;
#import &lt;SciChart/SCIDefaultXySeriesInfoProvider.h&gt;
#import &lt;SciChart/SCIXySeriesTooltip.h&gt;

@protocol SeriesInfoReceiverDelegate &lt;NSObject&gt;
- (void)modifierInteractedWith:(SCIXySeriesInfo *)seriesInfo;
@end

@interface CustomSeriesInfoProvider : SCIDefaultXySeriesInfoProvider
@property (nonatomic, weak) id&lt;SeriesInfoReceiverDelegate&gt; delegate;
@end

// CustomSeriesInfoProvider.m file
@interface CustomXySeriesTooltip : SCIXySeriesTooltip
@property(nonatomic, weak) id  tooltipDelegate;
@end

@implementation CustomXySeriesTooltip
@synthesize tooltipDelegate;

- (void)internalUpdateWithSeriesInfo:(SCIXySeriesInfo *)seriesInfo {
    [super internalUpdateWithSeriesInfo:seriesInfo];
    [tooltipDelegate modifierInteractedWith: seriesInfo];
}
@end

@implementation CustomSeriesInfoProvider
@synthesize delegate;

- (id)getSeriesTooltipInternalWithSeriesInfo:(SCIXySeriesInfo *)seriesInfo modifierType:(Class)modifierType {
    if (modifierType == SCIRolloverModifier.class) {
        CustomXySeriesTooltip *customXySeriesTooltip = [[CustomXySeriesTooltip alloc] initWithSeriesInfo:seriesInfo];
        customXySeriesTooltip.tooltipDelegate = delegate;
        return customXySeriesTooltip;
    } else {
        return [super getSeriesTooltipInternalWithSeriesInfo:seriesInfo modifierType:modifierType];
    }
}

- (void)getSeriesInfo:(SCIXySeriesInfo *)seriesInfo {
    [delegate modifierInteractedWith: seriesInfo];
}
</div>
<div class="code-snippet" id="swift">
import SciChart.Protected.SCISeriesTooltipBase
import SciChart.Protected.SCISeriesInfoProviderBase

protocol SeriesInfoReceiverDelegate: AnyObject {
    func modifierInteractedWith(seriesInfo: SCIXySeriesInfo)
}

class CustomSeriesInfoProvider: SCIDefaultXySeriesInfoProvider {
    weak var delegate: SeriesInfoReceiverDelegate?
    
    class CustomXySeriesTooltip: SCIXySeriesTooltip {
        weak var tooltipDelegate: SeriesInfoReceiverDelegate?
        
        override func internalUpdate(with seriesInfo: SCIXySeriesInfo) {
            super.internalUpdate(with: seriesInfo)
            tooltipDelegate?.modifierInteractedWith(seriesInfo: seriesInfo)
        }
    }
    
    override func getSeriesTooltipInternal(seriesInfo: SCIXySeriesInfo, modifierType: AnyClass) -> ISCISeriesTooltip {
        // Replace SCITooltipModifier with the specific modifier class you are using (e.g. SCITooltipModifier)
        if (modifierType == SCIRolloverModifier.self) {
            let customXySeriesTooltip = CustomXySeriesTooltip(seriesInfo: seriesInfo)
            customXySeriesTooltip.tooltipDelegate = delegate
            return customXySeriesTooltip
        } else {
            return super.getSeriesTooltipInternal(seriesInfo: seriesInfo, modifierType: modifierType)
        }
    }
    
    func getTouchDataSeriesIndex(seriesInfo: SCIXySeriesInfo) {
        delegate?.modifierInteractedWith(seriesInfo: seriesInfo)
    }
    
}
</div>

Implement the SeriesInfoReceiverDelegate in your chart view to receive and handle the extracted data:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
@interface RolloverModifierChartView : UIViewController &lt;SeriesInfoReceiverDelegate&gt;
...
-(void)modifierInteractedWith:(SCIXySeriesInfo *)seriesInfo {
    NSString *string = NSString.empty;
    string = [string stringByAppendingFormat:@"X: %@\n", seriesInfo.formattedXValue.rawString];
    string = [string stringByAppendingFormat:@"Y: %@", seriesInfo.formattedYValue.rawString];
    
    NSLog(@"Rollover touch => %@", string);
    NSLog(@"Series name => %@", seriesInfo.seriesName);
}
</div>
<div class="code-snippet" id="swift">
extension UsingRolloverModifierChartView: SeriesInfoReceiverDelegate {
    func modifierInteractedWith(seriesInfo: SCIXySeriesInfo) {
        var string = NSString.empty;
        string += "X: \(seriesInfo.formattedXValue.rawString) "
        string += "Y: \(seriesInfo.formattedYValue.rawString)"
        print("Rollover touch => \(string)")
        print("Series name =>", seriesInfo.seriesName ?? "")
    }
}
</div>

To use your custom info provider, assign it to each renderable series:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    CustomSeriesInfoProvider *customSeriesInfoProvider = [CustomSeriesInfoProvider new];
    customSeriesInfoProvider.delegate = self;
    rSeries.seriesInfoProvider = customSeriesInfoProvider;
</div>
<div class="code-snippet" id="swift">
    let customSeriesInfoProvider = CustomSeriesInfoProvider()
    customSeriesInfoProvider.delegate = self
    rSeries.seriesInfoProvider = customSeriesInfoProvider
</div>

> **_NOTE:_** If you are using multiple series, create a separate instance of CustomSeriesInfoProvider for each series.

## Where to Go From Here?

You can download the final project from our GitHub Repository:

- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2009%20-%20ChartModifier%20To%20ViewModel)

Of course, this is not the limit of what you can achieve with the SciChart iOS. You might want to read some of the following articles:

- [Axis APIs](Axis APIs.html)
- [Annotations API](Annotations APIs.html)
- [2D Chart Types](2D Chart Types.html)
- [Chart Modifiers](Chart Modifier APIs.html)

Finally, start exploring. The SciChart iOS is quite extensive.
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).

For instance - take a look at our **Sync Multi Chart** example, which can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):

![Sync Multi Chart Example](img/tutorials-2d/tutorials-2d-sync-multi-chart-example.png)
