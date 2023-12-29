# Tooltips Customization
In SciChart, you can fully customize tooltips for [SCITooltipModifier](interactivity---scitooltipmodifier.html), [SCIRolloverModifier](interactivity---scirollovermodifier.html) and [SCICursorModifier](interactivity---scicursormodifier.html).
Those customizations can be achieved via the `ISCISeriesInfoProvider` and `ISCISeriesTooltip` protocols.
Moreover - tooltips can be made **unique** per a **RenderableSeries** instance via the `ISCIRenderableSeries.seriesInfoProvider` property.

We have several examples (listed below) which shows how to customize tooltips for the modifiers:
- [Customization of Tooltip Modifier Tooltips](#customization-of-tooltip-modifier-tooltips)
- [Customization of Rollover Modifier Tooltips](#customization-of-rollover-modifier-tooltips)
- [Customization of Cursor Modifier Tooltips](#customization-of-cursor-modifier-tooltips)

> **_NOTE:_** All examples can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
- [Obj-C / SWIFT]().

To have fully custom tooltip for your modifier, you will need to provide **custom** `ISCISeriesInfoProvider` for your **RenderableSeries** via inheriting from `SCISeriesInfoProviderBase` which contains some base functionality.
From there - you might want to override one of the following (or both):

- `-getSeriesInfoInternal` - allows to provide custom implementation of `SCISeriesInfo`, which simply contains information about a **RenderableSeries** and should be created based on it
- `-getSeriesTooltipInternalWithSeriesInfo:modifierType:` - allows to provide **custom tooltip** for your series, based on `seriesInfo` and `modifierType`

#### Customization of Rollover Modifier Tooltips
Let's consider [Customization of Rollover Modifier Tooltips](#customization-of-rollover-modifier-tooltips) as an example, since customizations for other modifiers are nearly the same.
![Customization Rollover Modifier](img/modifiers-2d/customization-rollover-modifier.png)

> **_NOTE:_** Full example sources are available in [2D Charts -> Tooltips and Hit Test -> Customization RolloverModifier](https://www.scichart.com/example/ios-chart/ios-customization-rollover-modifier/)

First thing, we will need to create custom `ISCISeriesTooltip` and implement `-internalUpdate:` method in which we update tooltip instance based on passed in `SCISeriesInfo` instance. 
Then, in custom `ISCISeriesInfoProvider` we override `-getSeriesTooltipInternalWithSeriesInfo:modifierType` and provide our custom tooltip for `SCIRolloverModifier` type, since we want to customize tooltips only for **RolloverModifier**.
Finally, we provide our **custom** SeriesInfoProvider to our RenderableSeries instance via the corresponding property.

Let's see the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCISeriesTooltipBase+Protected.h&gt;
    #import &lt;SciChart/SCISeriesInfoProviderBase+Protected.h&gt;

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
    ...
    @interface FirstCustomRolloverSeriesInfoProvider : SCIDefaultXySeriesInfoProvider
    @end
    @implementation FirstCustomRolloverSeriesInfoProvider
    - (id<&lt;SCISeriesTooltip&gt;)getSeriesTooltipInternalWithSeriesInfo:(SCIXySeriesInfo *)seriesInfo modifierType:(Class)modifierType {
        if (modifierType == SCIRolloverModifier.class) {
            return [[FirstCustomXySeriesTooltip alloc] initWithSeriesInfo:seriesInfo];
        } else {
            return [super getSeriesTooltipInternalWithSeriesInfo:seriesInfo modifierType:modifierType];
        }
    }
    @end
    ...
    SCIFastLineRenderableSeries *line1 = [SCIFastLineRenderableSeries new];
    line1.seriesInfoProvider = [FirstCustomRolloverSeriesInfoProvider new];
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCISeriesInfoProviderBase
    import SciChart.Protected.SCISeriesTooltipBase

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
    ...
    let line1 = SCIFastLineRenderableSeries()
    line1.seriesInfoProvider = FirstCustomSeriesInfoProvider()
</div>
<div class="code-snippet" id="cs">
    class FirstCustomSeriesInfoProvider : SCIDefaultXySeriesInfoProvider
    {
        class FirstCustomXySeriesTooltip : SCIXySeriesTooltip
        {
            public FirstCustomXySeriesTooltip(SCIXySeriesInfo seriesInfo) : base(seriesInfo) { }

            protected override void InternalUpdate(SCISeriesInfo seriesInfo)
            {
                var xySeriesInfo = (SCIXySeriesInfo)seriesInfo;
                var str = string.Empty;
                str += "X: " + xySeriesInfo.FormattedXValue.RawString + "\n";
                str += "Y: " + xySeriesInfo.FormattedYValue.RawString + "\n";
                if (!string.IsNullOrEmpty(xySeriesInfo.SeriesName))
                {
                    str += xySeriesInfo.SeriesName + "\n";
                }
                str += "Rollover Modifier";

                Text = str;

                SetTooltipBackground(0xffe2460c);
                SetTooltipStroke(0xffff4500);
                SetTooltipTextColor(0xffffffff);
            }
        }

        protected override IISCISeriesTooltip GetSeriesTooltipInternal(SCISeriesInfo seriesInfo, Class modifierType)
        {
            if (modifierType == typeof(SCIRolloverModifier).ToClass())
            {
                return new FirstCustomXySeriesTooltip((SCIXySeriesInfo)seriesInfo);
            }
            else
            {
                return base.GetSeriesTooltipInternal(seriesInfo, modifierType);
            }
        }
    }
    ...
    var line1 = new SCIFastLineRenderableSeries();
    line1.SeriesInfoProvider = new FirstCustomSeriesInfoProvider();
</div>

> **_NOTE:_** A custom Tooltip has to implement the `ISCISeriesTooltip` or extend the `SCISeriesTooltipBase` class, which is derived from `UILabel`.

#### Customization of Tooltip Modifier Tooltips
![Customization Tooltip Modifier](img/modifiers-2d/customization-tooltip-modifier.png)

> **_NOTE:_** Full example source code is available in [2D Charts -> Tooltips and Hit Test -> Customization TooltipModifier](https://www.scichart.com/example/ios-customization-tooltip-modifier/)

#### Customization of Cursor Modifier Tooltips
![Customization Cursor Modifier](img/modifiers-2d/customization-cursor-modifier.png)

> **_NOTE:_** Full example sources is available in [2D Charts -> Tooltips and Hit Test -> Customization CursorModifier](https://www.scichart.com/example/ios-chart/ios-customization-cursor-modifier/)

## Axis Tooltips Customization
Axes tooltips for modifiers are customized the same way as **Series Tooltips** - via custom `SCIAxisTooltip` and `ISCIAxisInfoProvider`. Please see the code below, which is from the same [Customization RolloverModifier](https://www.scichart.com/example/ios-chart/ios-customization-rollover-modifier/) example:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCIAxisTooltip+Protected.h&gt;
    #import &lt;SciChart/SCIDefaultAxisInfoProvider+Protected.h&gt;

    @interface CustomRolloverAxisTooltip : SCIAxisTooltip
    @end
    @implementation CustomRolloverAxisTooltip
    - (BOOL)updateInternalWithAxisInfo:(SCIAxisInfo *)axisInfo {
        self.text = [NSString stringWithFormat:@"Axis ID: %@\nValue: %@", axisInfo.axisId, axisInfo.axisFormattedDataValue.rawString];
        [self setTooltipBackground:0xff6495ed];
        return YES;
    }
    @end

    @interface CustomRolloverAxisSeriesInfoProvider : SCIDefaultAxisInfoProvider
    @end
    @implementation CustomRolloverAxisSeriesInfoProvider
    - (id&lt;ISCIAxisTooltip&gt;)getAxisTooltipInternal:(SCIAxisInfo *)axisInfo modifierType:(Class)modifierType {
        if (modifierType == SCIRolloverModifier.class) {
            return [[CustomRolloverAxisTooltip alloc] initWithAxisInfo:axisInfo];
        } else {
            return [super getAxisTooltipInternal:axisInfo modifierType:modifierType];
        }
    }
    @end
    ...
    id<ISCIAxis> xAxis = [SCINumericAxis new];
    xAxis.axisInfoProvider = [CustomRolloverAxisSeriesInfoProvider new];
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCIDefaultAxisInfoProvider
    import SciChart.Protected.SCIAxisTooltip

    private class CustomAxisSeriesInfoProvider: SCIDefaultAxisInfoProvider {
        class CustomAxisTooltip: SCIAxisTooltip {            
            override func updateInternal(with axisInfo: SCIAxisInfo!) -> Bool {
                self.text = "Axis ID: \(axisInfo.axisId ?? "") \nValue: \(axisInfo.axisFormattedDataValue?.rawString ?? "")"
                setTooltipBackground(0xff6495ed)
                return true
            }
        }
        
        override func getAxisTooltipInternal(_ axisInfo: SCIAxisInfo!, modifierType: AnyClass!) -> ISCIAxisTooltip! {
            if modifierType == SCIRolloverModifier.self {
                return CustomAxisTooltip(axisInfo: axisInfo)
            } else {
                return super.getAxisTooltipInternal(axisInfo, modifierType: modifierType)
            }
        }
    }
    ...
    let xAxis = SCINumericAxis()
    xAxis.axisInfoProvider = CustomAxisSeriesInfoProvider()
</div>
<div class="code-snippet" id="cs">
    class CustomAxisSeriesInfoProvider : SCIDefaultAxisInfoProvider
    {
        class CustomAxisTooltip : SCIAxisTooltip
        {
            public CustomAxisTooltip(SCIAxisInfo axisInfo) : base(axisInfo) { }
            protected override bool UpdateInternal(SCIAxisInfo axisInfo)
            {
                Text = $"Axis ID: {axisInfo.AxisId ?? ""} \nValue: {axisInfo.AxisFormattedDataValue.RawString ?? ""}";
                SetTooltipBackground(0xff6495ed);

                return true;
            }
        }

        protected override IISCIAxisTooltip GetAxisTooltipInternal(SCIAxisInfo axisInfo, Class modifierType)
        {
            if (modifierType == typeof(SCIRolloverModifier).ToClass())
            {
                return new CustomAxisTooltip(axisInfo);
            }
            else
            {
                return base.GetAxisTooltipInternal(axisInfo, modifierType);
            }

        }
    }
    ...
    var xAxis = new SCINumericAxis();
    xAxis.AxisInfoProvider = new CustomAxisSeriesInfoProvider();
</div>

![Custom Axis Tooltip](img/modifiers-2d/custom-axis-tooltip.png)
