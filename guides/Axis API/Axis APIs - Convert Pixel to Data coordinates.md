# Axis APIs - Convert Pixel to Data coordinates
SciChart iOS provides a clean and simple API to transform **pixels** to **data-values** and vice versa via the following methods:
- `-[ISCIAxisCore getCoordinateFrom:]` - expects a chart `data-value` and returns the corresponding **pixel coordinate**.
- `-[ISCIAxisCore getDataValueFrom:]` - expects a `coordinate in pixels` and returns the closest **data value** to that coordinate.

It is also possible, to perform such conversion using our [CoordinateCalculator APIs](#iscicoordinatecalculator-api)

## Where Pixel Coordinates are measured from

It is important to note when converting **Pixels** to **Data Coordinates** and vice versa that pixels are measured from the top-left inside corner of the chart. So, let's see correspondence of the pixel coordinate and data-values in the table below:

| **Pixel Coordinate** | **Data-Value**                                     |
| -------------------- | -------------------------------------------------- |
| `[0, 0]`             | `[xAxis.visibleRange.min, yAxis.visibleRange.max]` |
| `[width, height]`    | `[xAxis.visibleRange.max, yAxis.visibleRange.min]` |

>**_NOTE:_** Learn more about `ISCIAxisCore.visibleRange` and how to use this property at the [Axis Ranging - VisibleRange and DataRange](axis-ranging---visiblerange-and-datarange.html) article.

![Pixels vs Data-Coordinates](img/axis-2d/pixel-vs-data-coordinates.png)

## Converting between Pixels and Data Coordinates
As mentioned [above](#axis-apis---convert-pixel-to-data-coordinates) - **data-values** are converted to **pixel coordinates** via the `-[ISCIAxisCore getCoordinateFrom:]` method. Also, Coordinates in pixels are converted back to chart data-values via the `-[ISCIAxisCore getDataValueFrom:]` method. 

You can find simple examples how to do the conversions below.

**SCINumericAxis conversions**

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    float coordinate = [xAxis getCoordinateFrom:@(1.2)];
    
    // Convert back:
    id&lt;ISCIComparable&gt; dataValue = [xAxis getDataValueFrom:coordinate];
</div>
<div class="code-snippet" id="swift">
    let coordinate = xAxis.getCoordinate(NSNumber(value: 1.2))

    // Convert back:
    let dataValue = xAxis.getDataValue(coordinate)
</div>
<div class="code-snippet" id="cs">
    var coordinate = xAxis.GetCoordinate(1.2.FromComparable());

    // Convert back:
    var dataValue = xAxis.GetDataValue(coordinate);
</div>

**SCIDateAxis conversions**

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    NSDate *date = [NSDate dateWithYear:2011 month:5 day:1];
    float coordinate = [xAxis getCoordinateFrom:date];
    
    // Convert back:
    NSDate *dataValue = [xAxis getDataValueFrom:coordinate];
</div>
<div class="code-snippet" id="swift">
    let date = NSDate(year: 2011, month: 5, day: 1)
    let coordinate = xAxis.getCoordinate(date)
    
    // Convert back:
    let dataValue = xAxis.getDataValue(coordinate)
</div>
<div class="code-snippet" id="cs">
    var date = new DateTime(2011, 5, 1);
    var coordinate = xAxis.GetCoordinate(date.FromComparable());

    // Convert back:
    var dataValue = xAxis.GetDataValue(coordinate).ToComparable();
</div>

**SCICategoryDateAxis conversions**

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    NSDate *date = [NSDate dateWithYear:2011 month:10 day:5];
    float coordinate = [xAxis getCoordinateFrom:date];
    
    // Convert back:
    NSDate *dataValue = [xAxis getDataValueFrom:coordinate];
</div>
<div class="code-snippet" id="swift">
    let date = NSDate(year: 2011, month: 10, day: 5)
    let coordinate = xAxis.getCoordinate(date)
    
    // Convert back:
    let dataValue = xAxis.getDataValue(coordinate)
</div>
<div class="code-snippet" id="cs">
    var date = new DateTime(2011, 10, 5);
    var coordinate = xAxis.GetCoordinate(date.FromComparable());

    // Convert back:
    var dataValue = xAxis.GetDataValue(coordinate).ToComparable();
</div>

## Getting a CoordinateCalculator instance
There is a `ISCIAxisCore.currentCoordinateCalculator` property, which is `readonly`, and which provides a coordinate calculator instance which is valid for the current render pass.

>**_NOTE:_** If the `ISCIAxisCore.visibleRange` changes, the data changes, or the viewport size changes - then the `ISCICoordinateCalculator` will be recreated under the hood.

## ISCICoordinateCalculator API
Like `ISCIAxisCore`, `ISCICoordinateCalculator` has the following methods:
- `-[ISCICoordinateCalculator getCoordinateFrom:]` - expects a **double** representation of `data-value` and returns the corresponding **pixel coordinate**.
- `-[ISCICoordinateCalculator getDataValueFrom:]` - expects a `coordinate in pixels` and returns the closest **data value** to that coordinate (represented in **double**).

But in addition to the above, coordinate calculators API, provides methods, to perform conversions in batches via the following:
- `-[ISCICoordinateCalculator getCoordinates:fromDataValues:count:]`
- `-[ISCICoordinateCalculator getDataValues:fromCoordinates:count:]`

As you might guess, converting Pixels to Data-Coordinates and vise versa slightly differs for different axis types due to difference in underlying **data-types**. In particular the following ones:
- [SCINumericAxis](#scinumericaxis-conversions)
- [SCIDateAxis](#scidateaxis-conversions)
- [SCICategoryDateAxis](#scicategorydateaxis-conversions)

Read on to get better understanding of such conversions.

### SCINumericAxis conversions
The simplest case is the `SCINumericAxis`. `ISCICoordinateCalculator` for NumericAxis works the **data-value** as **double**. So let's take our [Digital Line Chart](https://www.scichart.com/example/ios-chart-digital-line-chart-example/) example, and try do some conversions

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCICoordinateCalculator&gt; calculator = xAxis.currentCoordinateCalculator;
    float coordinate = [calculator getCoordinateFrom:1.2];
    
    // Convert back:
    double dataValue = [calculator getDataValueFrom:coordinate];
</div>
<div class="code-snippet" id="swift">
    let calculator = xAxis.currentCoordinateCalculator!
    let coordinate = calculator.getCoordinate(1.2)
    
    // Convert back:
    let dataValue = calculator.getDataValue(coordinate)
</div>
<div class="code-snippet" id="cs">
    var calculator = xAxis.CurrentCoordinateCalculator;
    var coordinate = calculator.GetCoordinate(1.2);

    // Convert back:
    var dataValue = calculator.GetDataValue(coordinate);
</div>
<center><sub><sup>VisibleRange = [1, 1.25], Data-Value = 1.2, Pixel-Coordinate = 272.8</sub></sup></center>

>**_NOTE:_** The exact **data-values** and **coordinates** might differ depending on your **visibleRange, viewport** etc...

### SCIDateAxis conversions
Similarly to `SCINumericAxis` - the `SCIDateAxis` is quite simple with one difference - it's `ISCICoordinateCalculator` works with **double representation** of **Date**, which is **[timeIntervalSince1970](https://developer.apple.com/documentation/foundation/nsdate/1407504-timeintervalsince1970?language=objc)**. So let's take our [Mountain Line Chart](https://www.scichart.com/example/ios-mountain-chart-demo/) as an example, and try do some conversions. 

>**_NOTE:_** Since the `ISCICoordinateCalculator` works with double representation of Date in `timeIntervalSince1970`, you will need to do all the needed conversions on your own. See the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCICoordinateCalculator&gt; calculator = xAxis.currentCoordinateCalculator;
    NSDate *date = [NSDate dateWithYear:2011 month:5 day:1];
    float coordinate = [calculator getCoordinateFrom:date.timeIntervalSince1970];
    
    // Convert back:
    double intervalSince1970 = [calculator getDataValueFrom:coordinate];
    NSDate *dateValue = [NSDate dateWithTimeIntervalSince1970:intervalSince1970];
</div>
<div class="code-snippet" id="swift">
    let calculator = xAxis.currentCoordinateCalculator!
    
    let date = NSDate(year: 2011, month: 5, day: 1)!
    let coordinate = calculator.getCoordinate(date.timeIntervalSince1970)
    
    // Convert back:
    let intervalSince1970 = calculator.getDataValue(coordinate)
    let dateValue = Date(timeIntervalSince1970: intervalSince1970)
</div>
<div class="code-snippet" id="cs">
    var calculator = xAxis.CurrentCoordinateCalculator;
    var date = new DateTime(2011, 5, 1);
    var coordinate = calculator.GetCoordinate(date.ToUnixTime());

    // Convert back:
    var intervalSince1970 = calculator.GetDataValue(coordinate);
    var dateValue = NSDate.FromTimeIntervalSince1970(intervalSince1970).FromDate();
</div>
<center><sub><sup>VisibleRange = [2010-09-28, 2011-12-09], Data-Value = "2011-05-01", Pixel-Coordinate = 374.658417</sub></sup></center>

>**_NOTE:_** The exact **data-values** and **coordinates** might differ depending on your **visibleRange, viewport** etc...

### SCICategoryDateAxis conversions
`SCICategoryDateAxis` is **slightly different**. It's `ISCICoordinateCalculator` conversion methods works with an integers, which is **the index of a data points** inside an appropriate `ISCIDataSeries`. You can convert Date to index and vice versa through the `ISCICategoryLabelProvider`. So let's work with our [Candlestick Chart](https://www.scichart.com/example/ios-candlestick-chart-demo/) as an example, since is has `SCICategoryDateAxis`. 

>**_NOTE:_** Conversion from Date to Index and vice versa can be performed using the `ISCICategoryLabelProvider`. See the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCICoordinateCalculator&gt; calculator = xAxis.currentCoordinateCalculator;
    // get ISCICategoryLabelProvider to convert Date value to Index
    id&lt;ISCICategoryLabelProvider&gt; labelProvider = (id&lt;ISCICategoryLabelProvider&gt;)xAxis.labelProvider;
    NSDate *date = [NSDate dateWithYear:2011 month:10 day:5];
    NSInteger index = [labelProvider transformDataToIndex:date];
    float coordinate = [calculator getCoordinateFrom:231];
    
    // Convert back:
    index = (NSInteger)[calculator getDataValueFrom:coordinate];
    // use the ISCICategoryLabelProvider instance to convert index to Date value
    date = [labelProvider transformIndexToData:index];

</div>
<div class="code-snippet" id="swift">
    let calculator = xAxis.currentCoordinateCalculator!
    // get ISCICategoryLabelProvider to convert Date value to Index
    let labelProvider = xAxis.labelProvider as! ISCICategoryLabelProvider
    var date = NSDate(year: 2011, month: 10, day: 5) as Date?
    var index = labelProvider.transformDataToIndex(date)
    let coordinate = calculator.getCoordinate(Double(index))
    
    // Convert back:
    index = Int(calculator.getDataValue(coordinate))
    // use the ISCICategoryLabelProvider instance to convert index to Date value
    date = labelProvider.transformIndexToData(index)
</div>
<div class="code-snippet" id="cs">
    var calculator = xAxis.CurrentCoordinateCalculator;
    // get ISCICategoryLabelProvider to convert Date value to Index
    var labelProvider = (IISCICategoryLabelProvider)xAxis.LabelProvider;
    var date = new DateTime(2011, 10, 5);
    var index = labelProvider.TransformDataToIndex(date.ToDate());
    var coordinate = calculator.GetCoordinate(index);

    // Convert back:
    index = (int)calculator.GetDataValue(coordinate);
    // use the ISCICategoryLabelProvider instance to convert index to Date value
    date = labelProvider.TransformIndexToData(index).FromDate();
</div>
<center><sub><sup>VisibleRange(IndexRange) = [223, 253], Data-Value = "2011-10-05" (index = 231), Pixel-Coordinate = 203.466675</sub></sup></center>

>**_NOTE:_** The exact **data-values** and **coordinates** might differ depending on your **visibleRange, viewport** etc...

## Transforming Pixels to the Inner Viewport
If you register a [UIGestureRecognizer](https://developer.apple.com/documentation/uikit/uigesturerecognizer) to recognize gestures or touch events on a SciChartSurface, the touch coordinates you will receive will be relative to the `SCIChartSurface` itself. Before the [Coordinate Transformation API](#converting-between-pixels-and-data-coordinates) can be used, it's important to ensure that such coordinates are transformed **relative to the viewport** (the central area). 

The view to translate relative to can be obtained calling either the `ISCIChartSurfaceBase.modifierSurface` or `ISCIChartSurface.renderableSeriesArea`, depending on the context. By default, both occupy the same area, but this can be changed if a custom `ISCILayoutManager` is provided:

![Pixels vs Data-Coordinates](img/axis-2d/hit-test-api-inner-viewport.png)

The transformation can be done via the `-[ISCIHitTestable translatePoint:hitTestable:]` method of your `SCIChartSurface` instance. For instance let's take a look at the code snippet from our [Hit-Test API](https://www.scichart.com/example/ios-chart-chart-hit-test-api-example/) example:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
- (void)handleSingleTap:(UITapGestureRecognizer *)recognizer {
    // the touch point relative to the SCIChartSurface
    CGPoint location = [recognizer locationInView:recognizer.view.superview];
    // translate the touch point relative to RenderableSeriesArea (or ModifierSurface)
    CGPoint hitTestPoint = [self.surface translatePoint:location hitTestable:self.surface.renderableSeriesArea];
    
    // the translated point can be used for looking for data values, hit-test or something else.
    ...
}
</div>
<div class="code-snippet" id="swift">
    @objc fileprivate func handleSingleTap(_ recognizer: UITapGestureRecognizer) {
        // the touch point relative to the SCIChartSurface
        let location = recognizer.location(in: recognizer.view!.superview)
        // translate the touch point relative to RenderableSeriesArea (or ModifierSurface)
        let hitTestPoint = surface.translate(location, hitTestable: surface.renderableSeriesArea)
        
        // the translated point can be used for looking for data values, hit-test or something else.
        ...
    }
</div>
<div class="code-snippet" id="cs">
    private void HandleSingleTap(UITapGestureRecognizer recognizer)
    {
        // the touch point relative to the SCIChartSurface
        var location = recognizer.LocationInView(recognizer.View.Superview);
        // translate the touch point relative to RenderableSeriesArea (or ModifierSurface)
        var hitTestPoint = Surface.TranslatePoint(location, Surface.RenderableSeriesArea);
        
        // the translated point can be used for looking for data values, hit-test or something else.
        ...
    }
</div>
