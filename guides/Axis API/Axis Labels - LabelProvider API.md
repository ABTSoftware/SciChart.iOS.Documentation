# Axis Labels - LabelProvider API
In SciChart **Axis Label** is a representation of Values on the axes. Axis labels make it easier to read teh chart data. Our API allows to edit and customize them data in many ways to meet your needs, please read on to learn how!

All Axis Types include the `ISCIAxisCore.labelProvider` property, which allows a class to be attached to an axis for complete control over axis label output.

Use a `SCILabelProviderBase` inheritors when you want to:

- Have fine grained control over Axis Text or Cursor Labels, depending on numeric (or date) values.
- Display strings on the XAxis, e.g. “Bananas”, “Oranges”, “Apples” and not “1”, “2”, “3”.
- Dynamically change the `ISCIAxisCore.textFormatting` as you zoom in or out.
- Dynamically change the `ISCIAxisCore.textFormatting` depending on Data-value.

By default each axis has a `ISCILabelProvider` created and assigned to it. The type of LabelProvider depends on the [type of Axis](Axis APIs.html). Below is a table of the LabelProviders already defined in SciChart iOS.

| **Label Provider Type**              | **Provide labels For**      |
| ------------------------------------ | --------------------------- | 
| `SCINumericLabelProvider`            | `SCINumericAxis`            |
| `SCILogarithmicNumericLabelProvider` | `SCILogarithmicNumericAxis` |
| `SCIDateLabelProvider`               | `SCIDateAxis`               |
| `SCITradeChartAxisLabelProvider`     | `SCICategoryDateAxis`       |

The above label providers are inherited from `SCIFormatterLabelProviderBase` which format labels using `ISCILabelFormatter` provided by inheritors. So if you want to customize the default formatting of the above - provide custom `ISCILabelFormatter` into the appropriate constructors.

## Creating custom ISCILabelFormatter
To create custom `ISCILabelFormatter`, you will need to implement the following methods:
- `-[ISCILabelFormatter updateWithAxis:]` - is called from associated `SCIFormatterLabelProviderBase` to update this label formatter with values provided by axis.
- `-[SCILabelFormatterBase formatLabel:]` - is called internally for every axis tick value to get a text to show for corresponding axis label.
- `-[SCILabelFormatterBase formatCursorLabel:]` - is called to format data values for axis overlays, such as `SCICursorModifier` axis labels.

Let's create custom `ISCILabelFormatter` for `SCINumericAxis`

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface NumericLabelFormatter : SCILabelFormatterBase
    @end

    @implementation NumericLabelFormatter

    - (void)updateWithAxis:(id&lt;ISCIAxisCore&gt;)axis { }

    - (id&lt;ISCIString&gt;)formatLabel:(double)dataValue {
        // return a formatting string for tick labels
        return [NSString stringWithFormat:@"%.3f", dataValue];
    }

    - (id&lt;ISCIString&gt;)formatCursorLabel:(double)dataValue {
        // return a formatting string for modifiers' axis labels
        return [self formatLabel:dataValue];
    }

    @end
    ...
    // create a SCINumericLabelProvider with custom ISCILabelFormatter and assign it to the axis
    axis.labelProvider = [[SCINumericLabelProvider alloc] initWithLabelFormatter:[NumericLabelFormatter new]];
</div>
<div class="code-snippet" id="swift">
    class NumericLabelFormatter: SCILabelFormatterBase {
        override func update(_ axis: ISCIAxisCore) { }
        
        override func formatLabel(_ dataValue: Double) -> ISCIString {
            // return a formatting string for tick labels
            return NSString(format: "%.3f", dataValue)
        }
        
        override func formatCursorLabel(_ dataValue: Double) -> ISCIString {
            // return a formatting string for modifiers' axis labels
            return formatLabel(dataValue)
        }
    }
    ...
    // create a SCINumericLabelProvider with custom ISCILabelFormatter and assign it to the axis
    axis.labelProvider = SCINumericLabelProvider(labelFormatter: NumericLabelFormatter())
</div>
<div class="code-snippet" id="cs">
    class NumericLabelFormatter: SCILabelFormatterBase
    {
        public override void UpdateWithAxis(IISCIAxisCore axis) { }

        public override IISCIString FormatLabel(double dataValue)
        {
            // return a formatting string for tick labels
            return string.Format("{0:0.00}", dataValue).ToSciString();
        }

        public override IISCIString FormatCursorLabel(double dataValue)
        {
            // return a formatting string for modifiers' axis labels
            return FormatLabel(dataValue);
        }
    }
    ...
    // create a SCINumericLabelProvider with custom ISCILabelFormatter and assign it to the axis
    axis.LabelProvider = new SCINumericLabelProvider(new NumericLabelFormatter());
</div>

![Numeric LabelFormatter](img/axis-2d/label-formatter-numeric.png)

> **_NOTE:_** The other axis types require [different LabelProvider types](#axis-labels-labelprovider-api)

## Creating your own LabelProvider
You might want to create a your own, fully custom, **LabelProvider**.  To do so, - we simply create a class that inherits `SCILabelProviderBase` and provide the proper `ISCIAxis` `@protocol()`, which should correspond to the axis which will use your label provider. From there, wou can override the following, similarly to the [Label Formatter](#creating-custom-iscilabelformatters):
- `-[ISCILabelProvider formatLabel:]`
- Optional `-[ISCILabelProvider formatCursorLabel:]`

The **first one** is called internally for every axis tick value to get a text to show for corresponding axis label. The **latter one** is called to format data values for axis overlays, such as `SCICursorModifier` axis labels.

As mentioned above - the **LabelProvider** can be assigned to an axis via the `ISCIAxisCore.labelProvider` property.

Let's create custom **LabelProvider** for `SCIDateAxis`

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCILabelProviderBase+Protected.h&gt;

    @interface DateLabelProvider : SCILabelProviderBase<id&lt;ISCIDateAxis&gt;>
    @end

    @implementation DateLabelProvider {
        NSDateFormatter *_dateFormatter;
    }

    - (instancetype)init {
        self = [super initWithAxisType:@protocol(ISCIDateAxis)];
        if (self) {
            _dateFormatter = [NSDateFormatter createWithFormat:@"hh:mm dd/mm/yyyy"];
        }
        return self;
    }

    - (id&lt;ISCIString&gt;)formatLabel:(id&lt;ISCIComparable&gt;)dataValue {
        return [_dateFormatter stringFromDate:dataValue.toDate];
    }

    - (id&lt;ISCIString&gt;)formatCursorLabel:(id&lt;ISCIComparable&gt;)dataValue {
        return [self formatLabel:dataValue];
    }

    @end
    ...
    axis.labelProvider = [DateLabelProvider new];
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCILabelProviderBase

    class DateLabelProvider : SCILabelProviderBase<ISCIDateAxis> {
        let dateFormatter = DateFormatter.create(withFormat: "hh:mm dd/mm/yyyy")

        init() {
            super.init(axisType: ISCIDateAxis.self)
        }
        
        override func formatLabel(_ dataValue: ISCIComparable) -> ISCIString {
            return NSString(string: dateFormatter.string(from: dataValue.toDate()))
        }
        
        override func formatCursorLabel(_ dataValue: ISCIComparable) -> ISCIString {
            return formatLabel(dataValue)
        }
    }
    ...
    axis.labelProvider = DateLabelProvider()
</div>
<div class="code-snippet" id="cs">
    class DateLabelProvider: SCILabelProviderBase<IISCIDateAxis>
    {
        public override IISCIString FormatLabel(IISCIComparable dataValue)
        {
            var date = dataValue.ToDate();
            var dateTime = date.FromDate();
            return dateTime.ToString("hh:mm dd/mm/yyyy").ToSciString();
        }

        public override IISCIString FormatCursorLabel(IISCIComparable dataValue)
        {
            return FormatLabel(dataValue);
        }
    }
    ...
    axis.LabelProvider = new DateLabelProvider();
</div>

![Date LabelProvider](img/axis-2d/label-provider-date.png)

> **_NOTE:_** dataValue parameter in - `-[ISCILabelProvider formatLabel:]` and `-[ISCILabelProvider formatCursorLabel:]` is always a double. It is different for different axis types:
> - For a `SCINumericAxis` - the double-representation of the data.
> - For a `SCIDateAxis` - the [timeIntervalSince1970](https://developer.apple.com/documentation/foundation/nsdate/1407504-timeintervalsince1970?language=objc)
> - For a `SCICategoryDateAxis` - dataValue is the index to the data-series.

## More examples of LabelProvider usage
Several of the SciChart iOS Chart Examples use the **LabelProvider**, including the following:
- [iOS Stacked Column Side By Side](https://www.scichart.com/example/ios-chart-stacked-column-chart-grouped-side-by-side-example/)
- [iOS Custom Theme](https://www.scichart.com/example/ios-chart-example-custom-theme/)

## See Also
- [Axis Labels - TextFormatting and CursorTextFormatting](axis-labels---textformatting-and-cursortextformatting.html)
