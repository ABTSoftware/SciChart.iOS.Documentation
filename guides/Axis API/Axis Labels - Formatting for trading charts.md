# Axis Labels - Formatting for trading charts

If you develop some trading application, most likely, your chart will display some OHLC prices and indicators along with the corresponding date labels on X-Axis. In SciChart [Axis Labels - LabelProvider API](axis-labels---labelprovider-api.html#axis-labels-labelprovider-api) helps you to present these labels in the desired format. Quite a popular scenario in trading charts is when date labels formatting changes depending on the current zoom level, so the deeper zoom level is, the more detailed dates appear.

<video autoplay loop muted playsinline src="img/axis-2d/label-formatter-trade-chart.mp4"></video>

> **_NOTE:_** Example of the trading charts label formatter usage can be found in the **"Multi-Panel Stock Chart"** example in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-multi-pane-stock-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-chart-multi-pane-stock-charts-example/)

The most suitable type of X-Axis for trading charts is `SCICategoryDateAxis`. It uses `SCITradeChartAxisLabelProvider` to dynamically change its Text and Cursor Labels depending on Data-value and current zoom.

To format labels the `SCITradeChartAxisLabelProvider` class uses `SCITradeChartAxisLabelFormatter` which incapsulates two formatters: 
- `SCICalendarUnitDateFormatter` - for [axis labels](axis-styling---title-and-labels.html#axis-labels)
- `SCICursorCalendarUnitDateFormatter` - for [`SCICursorModifier` with axis labels](interactivity---scicursormodifier.html). 

Each of them formats dates depending on the current granularity using `-[ISCICalendarUnitDateFormatter formatDate:withCalendarUnit:]` method. By default, there are few predefined formatters, so you will get dynamically changing axis labels out-of-the-box. In case, the default one doesn't meet your requirements, you can provide your own and [customize your label provider](#custom-trade-chart-label-provider). Please, continue reading to find out, how to do it.

## Custom Trade Chart Label Provider
As an example of a Trade Chart Label Provider customization, let's create a custom one with some different formatters. 

First, we need to subclass `SCICalendarUnitDateFormatter` and implement a protected method `-[SCICalendarUnitDateFormatter createFormatterForCalendarUnit:]` which will return a formatter with the desired date format depending on the current calendar unit. Here is, how it might look in code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomCalendarUnitDateFormatter : SCICalendarUnitDateFormatter
    @end

    #import &lt;SciChart/SCICalendarUnitDateFormatter+Protected.h&gt;
    @implementation CustomCalendarUnitDateFormatter

    - (NSDateFormatter *)createFormatterForCalendarUnit:(NSCalendarUnit)calendarUnit {
        NSMutableString *dateFormat = [NSMutableString new];
        
        if (calendarUnit & NSCalendarUnitYear) {
            [dateFormat appendString:@" yyyy"];
        }
        if (calendarUnit & NSCalendarUnitMonth) {
            [dateFormat appendString:@" MMMM"];
        }
        if (calendarUnit & NSCalendarUnitDay) {
            [dateFormat appendString:@" EEEE, dd MMM"];
        }
        
        NSDateFormatter *formatter = [NSDateFormatter new];
        formatter.dateFormat = dateFormat;

        return formatter;
    }

    @end
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCICalendarUnitDateFormatter
    class CustomCalendarUnitDateFormatter: SCICalendarUnitDateFormatter {
        override func createFormatter(for calendarUnit: NSCalendar.Unit) -> DateFormatter {
            var dateFormat = ""
            
            if calendarUnit.contains(.year) {
                dateFormat.append(" yyyy")
            }
            
            if calendarUnit.contains(.month) {
                dateFormat.append(" MMMM")
            }
            
            if calendarUnit.contains(.day) {
                dateFormat.append(" EEEE, dd MMM")
            }
            
            let formatter = DateFormatter()
            formatter.dateFormat = dateFormat
            
            return formatter
        }
    }
</div>
<div class="code-snippet" id="cs">
        public class CustomCalendarUnitDateFormatter : SCICalendarUnitDateFormatter
        {
            protected override NSDateFormatter CreateFormatterForCalendarUnit(NSCalendarUnit calendarUnit)
            {
                var dateFormat = "";

                if ((calendarUnit & NSCalendarUnit.Year) > 0)
                {
                    dateFormat += " yyyy";
                }

                if ((calendarUnit & NSCalendarUnit.Month) > 0)
                {
                    dateFormat += " MMMM";
                }

                if ((calendarUnit & NSCalendarUnit.Day) > 0)
                {
                    dateFormat += " EEEE, dd MMM";
                }

                var dateFormatter = new NSDateFormatter()
                {
                    DateFormat = dateFormat
                };

                return dateFormatter;
            }
        }
</div>

> **_NOTE:_** In case using `createFormatterForCalendarUnit` method to return your custom formatter doesn’t fit your needs and you want to have full control on formatting your axis labels, use `-[ISCICalendarUnitDateFormatter formatDate:withCalendarUnit:]` method. Please, continue reading to see how you can use this method.

Let's also create some custom `SCICursorModifier` axis label formatter which will format cursor axis label depending on current granularity. So, for example, by default we want our Cursor to show a date in a format, like **“2018 Jun 17”** but when we zoom close enough - it should become something like **“Monday, 10 June”**.

Similar to how we create our **CustomCalendarUnitDateFormatter**, we will subclass `SCICursorCalendarUnitDateFormatter` and create two formatters with different date formats. Then, we will use `-[ISCICalendarUnitDateFormatter formatDate:withCalendarUnit:]` method to return a string for our Cursor axis label. Here is how it will look in code:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomCursorCalendarUnitDateFormatter : SCICursorCalendarUnitDateFormatter {
        NSDateFormatter *_cursorDefaultFormatter;
        NSDateFormatter *_dayCursorFormatter;
    }
        
    @end

    @implementation CustomCursorCalendarUnitDateFormatter
    - (instancetype)init {
        self = [super init];
        if (self) {
            _cursorDefaultFormatter = [NSDateFormatter new];
            _cursorDefaultFormatter.dateFormat = @"yyyy MMM dd";
            
            _dayCursorFormatter = [NSDateFormatter new];
            _dayCursorFormatter.dateFormat = @"EEEE, dd MMMM";
        }
        return self;
    }

    - (id&lt;ISCIString&gt;)formatDate:(NSDate *)date withCalendarUnit:(NSCalendarUnit)calendarUnit {
        NSDateFormatter *formatter = (calendarUnit < NSCalendarUnitDay) ? _cursorDefaultFormatter : _dayCursorFormatter;

        return [formatter stringFromDate:date];
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class CustomCursorCalendarUnitDateFormatter: SCICursorCalendarUnitDateFormatter {

        private lazy var cursorDefaultFormatter: DateFormatter = {
            let formatter = DateFormatter()
            formatter.dateFormat = "yyyy MMM dd"

            return formatter
        }()

        private lazy var dayCursorFormatter: DateFormatter = {
            let formatter = DateFormatter()
            formatter.dateFormat = "EEEE, dd MMMM"

            return formatter
        }()

        override func formatDate(_ date: Date, with calendarUnit: NSCalendar.Unit) -> ISCIString {
            let formatter = calendarUnit.rawValue < NSCalendar.Unit.day.rawValue ? cursorDefaultFormatter : dayCursorFormatter

            return  formatter.string(from: date) as ISCIString
        }
    }
</div>
<div class="code-snippet" id="cs">
    public class CustomCursorCalendarUnitDateFormatter : SCICalendarUnitDateFormatter
    {
        private NSDateFormatter cursorDefaultFormatter = new NSDateFormatter()
        {
            DateFormat = "yyyy MMM dd"
        };

        private NSDateFormatter dayCursorFormatter = new NSDateFormatter()
        {
            DateFormat = "EEEE, dd MMMM"
        };

        public override IISCIString FormatDate(NSDate date, NSCalendarUnit calendarUnit)
        {
            var formatter = (calendarUnit < NSCalendarUnit.Day) ? cursorDefaultFormatter : dayCursorFormatter;

            return formatter.ToString(date).ToSciString();
        }
    }
</div>

Next, we need to subclass `SCITradeChartAxisLabelFormatter` and pass our custom formatters to its init, like this:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomTradeChartLabelFormatter : SCITradeChartAxisLabelFormatter<SCICategoryDateAxis *>
    @end

    @implementation CustomTradeChartLabelFormatter

    - (instancetype)init {
        return [super initWithDateFormatter:[CustomCalendarUnitDateFormatter new] cursorDateFormatter:[CustomCursorCalendarUnitDateFormatter new]];
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class CustomTradeChartLabelFormatter: SCITradeChartAxisLabelFormatter<SCICategoryDateAxis> {
        init() {
            super.init(dateFormatter: CustomCalendarUnitDateFormatter(), cursorDateFormatter: CustomCursorCalendarUnitDateFormatter())
        }
    }
</div>
<div class="code-snippet" id="cs">
    public class CustomTradeChartLabelFormatter : SCITradeChartAxisLabelFormatter
    {
        public CustomTradeChartLabelFormatter() : base(new CustomCalendarUnitDateFormatter(), new CustomCursorCalendarUnitDateFormatter())
        {
        }
    }
</div>

Finally, create a `SCITradeChartAxisLabelProvider` subclass with our **CustomTradeChartLabelFormatter** and assign it to the `ISCIAxisCore.labelProvider`. Also, you need to add to your surface `SCIPinchZoomModifier` to be able to zoom and `SCICursorModifier` to see the Cursor axis labels formatting in action. See the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomTradeChartLabelProvider : SCITradeChartAxisLabelProvider
    @end

    @implementation CustomTradeChartLabelProvider

    - (instancetype)init {
        return [super initWithLabelFormatter:[CustomTradeChartLabelFormatter new]];
    }

    @end

    ...

    SCICategoryDateAxis *xAxis = [SCICategoryDateAxis new];
    xAxis.labelProvider = [CustomTradeChartLabelProvider new];

    SCIPinchZoomModifier *pinchZoomModifier = [SCIPinchZoomModifier new];
    pinchZoomModifier.direction = SCIDirection2D_XDirection;

    SCICursorModifier *cursorModifier = [SCICursorModifier new];
    cursorModifier.receiveHandledEvents = YES;

    [surface.chartModifiers addAll:pinchZoomModifier, cursorModifier, nil];

</div>
<div class="code-snippet" id="swift">
    class CustomTradeChartLabelProvider: SCITradeChartAxisLabelProvider {
        init() {
            super.init(labelFormatter: CustomTradeChartLabelFormatter())
        }
    }

    ...

    let xAxis = SCICategoryDateAxis()
    xAxis.labelProvider = CustomTradeChartLabelProvider()

    let pinchZoomModifier = SCIPinchZoomModifier()
    pinchZoomModifier.direction = .xDirection
    
    let cursorModifier = SCICursorModifier()
    cursorModifier.receiveHandledEvents = true

    surface.chartModifiers.add(items: pinchZoomModifier, cursorModifier)

</div>
<div class="code-snippet" id="cs">
    public class CustomTradeChartLabelProvider : SCITradeChartAxisLabelProvider
    {
        public CustomTradeChartLabelProvider() : base(new CustomTradeChartLabelFormatter())
        {
        }
    }

    ...

    var xAxis = new SCICategoryDateAxis {
        LabelProvider = new CustomTradeChartLabelProvider()
    }

    surface.ChartModifiers = new SCIChartModifierCollection
    {
        new SCIPinchZoomModifier { Direction = SCIDirection2D.XDirection },
        new SCICursorModifier { ReceiveHandledEvents = true }
    };

</div>

Here are the results in the normal and slightly zoomed states: 
![SCITradeChartAxisLabelProvider](img/axis-2d/label-formatter-trade-chart-cursor-default.png)
![SCITradeChartAxisLabelProvider](img/axis-2d/label-formatter-trade-chart-cursor-zoomed.png)

In case such a customization doesn't fit your needs and you need some completely different Label Provider you can always create your own. You will find more details in [Axis Labels - LabelProvider API](axis-labels---labelprovider-api.html#axis-labels-labelprovider-api) article.