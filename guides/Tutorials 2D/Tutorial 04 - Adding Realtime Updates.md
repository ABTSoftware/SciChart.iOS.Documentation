# SciChart iOS Tutorial - Adding Realtime Updates
In the previous tutorials we've showed how to [Create a Simple Chart](tutorial-01---create-a-simple-2d-chart.html), add some [Zoom and Pan](tutorial-02---zooming-and-panning-behavior.html) interaction as well as [Tooltips Inspection](tutorial-03---tooltips-and-legends.html) + [Legends](legend-modifier.html) via the [Chart Modifiers API](Chart Modifier APIs.html).

In this SciChart iOS tutorial we're going to go a little further and show how to **update data** displayed by a chart in **real-time**.

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2004%20-%20Adding%20Realtime%20Updates)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-04)

Assuming you have completed the [previous](tutorial-03---tooltips-and-legends.html) tutorial, we will now make some changes to update the data dynamically.

## Updating Data Values
In our `ISCIDataSeries`, we have some static data so far. Let's update them in ***real-time*** now.

We are going to add a Timer and schedule updating the data on timer tick.
To update data in a **DataSeries**, we will need to call one of the available `Update` methods on that DataSeries.
Since we are using `SCIXyDataSeries`, we are going to use the `-[ISCIXyDataSeries updateValuesX:y:at:]` method.

More information about Updating DataSeries can be found in the [Manipulating DataSeries Data](dataseries-apis.html#manipulating-dataseries-data) article. 

But first of all, we need to adjust some previously created code and save DataSeries instances to be able update them later.
And since we are going to change a DataSeries setup, it worth mentioning that the code from the previous tutorials works, but it wasn't very efficient:
- Calling any of the **[Update methods](dataseries-apis.html#append-insert-update-remove)** triggers a chart update, which redraws the entire chart.
- The values are passed in as `NSNumber` objects, which require ***boxing/unboxing***, which slows down the process as well

No worries, in SciChart there is an easy way to improve that:
- Make sure to always ***Append*** or ***Update*** data in a DataSeries in **batches** instead of one at a time.
- Suspend updates on a `SCIChartSurface` using `-[ISCISuspendable suspendUpdates]` to prevent redrawing until you have updated the whole DataSeries.
- Use one of the `ISCIValues` implementation such as `SCIDoubleValues`. It stores data in an ***primitive array internally*** and doesn't requires ***boxing/unboxing***.

So we updated the code as follows:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    static int const PointsCount = 200;
    // ...
    NSTimer *_timer;
    
    SCIDoubleValues *_lineData;
    SCIXyDataSeries *_lineDataSeries;
    SCIDoubleValues *_scatterData;
    SCIXyDataSeries *_scatterDataSeries;
    // ...
    _lineData = [SCIDoubleValues new];
    _lineDataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Int yType:SCIDataType_Double];
    _scatterData = [SCIDoubleValues new];
    _scatterDataSeries = [[SCIXyDataSeries alloc] initWithXType:SCIDataType_Int yType:SCIDataType_Double];
    
    SCIIntegerValues *xValues = [SCIIntegerValues new];
    for (int i = 0; i < 200; i++) {
        [xValues add:i];
        [_lineData add:sin(i * 0.1)];
        [_scatterData add:cos(i * 0.1)];
    }
    [_lineDataSeries appendValuesX:xValues y:_lineData];
    [_scatterDataSeries appendValuesX:xValues y:_scatterData];
</div>
<div class="code-snippet" id="swift">
    private let pointsCount = 200
    private var timer: Timer!
    
    private let lineData = SCIDoubleValues()
    private lazy var lineDataSeries: SCIXyDataSeries = {
        let lineDataSeries = SCIXyDataSeries(xType: .int, yType: .double)
        lineDataSeries.seriesName = "Line Series"
        return lineDataSeries
    }()
    private let scatterData = SCIDoubleValues()
    private lazy var scatterDataSeries: SCIXyDataSeries = {
        let scatterDataSeries = SCIXyDataSeries(xType: .int, yType: .double)
        scatterDataSeries.seriesName = "Scatter Series"
        return scatterDataSeries
    }()
    // ...
    let xValues = SCIIntegerValues()
    for i in 0 ..< 200 {
        xValues.add(Int32(i))
        lineData.add(sin(Double(i) * 0.1))
        scatterData.add(cos(Double(i) * 0.1))
    }
    lineDataSeries.append(x: xValues, y: lineData)
    scatterDataSeries.append(x: xValues, y: scatterData)
</div>
<div class="code-snippet" id="cs">
    private int pointsCount = 200;
    private Timer timer;
    
    private readonly SCIDoubleValues lineData = new SCIDoubleValues();
    private readonly SCIDoubleValues scatterData = new SCIDoubleValues();
    private readonly XyDataSeries&lt;int, double&gt; lineDataSeries = new XyDataSeries&lt;int, double&gt; { SeriesName = "Line Series " };
    private readonly XyDataSeries&lt;int, double&gt; scatterDataSeries = new XyDataSeries&lt;int, double&gt; { SeriesName = "Scatter Series" };
    // ...
    var xValues = new SCIIntegerValues();
    for (int i = 0; i < 200; i++)
    {
        xValues.Add(i);
        lineData.Add(Math.Sin(i * 0.1));
        scatterData.Add(Math.Cos(i * 0.1));
    }
    lineDataSeries.AppendValues(xValues, lineData);
    scatterDataSeries.AppendValues(xValues, scatterData);

    // start timer here
    Start();
</div>

From here, we can initialize our Timer and create an `updateData` selector, with real-time updates, like follows:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    double _phase;
    // ...
    _timer = [NSTimer scheduledTimerWithTimeInterval:0.02 target:self selector:@selector(updateData) userInfo:nil repeats:YES];
    // ...
    - (void)updateData {
        for (int i = 0; i < PointsCount; ++i) {
            [_lineData set:sin(i * 0.1 + _phase) at:i];
            [_scatterData set:cos(i * 0.1 + _phase) at:i];
        }
        
        [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
            [_lineDataSeries updateValuesY:_lineData at:0];
            [_scatterDataSeries updateValuesY:_scatterData at:0];
        }];
        
        _phase += 0.01;
    }
</div>
<div class="code-snippet" id="swift">
    private var phase: Double = 0
    // ...
    timer = Timer.scheduledTimer(timeInterval: 0.01, target: self, selector: #selector(updateData), userInfo: nil, repeats: true)
    // ...
    @objc fileprivate func updateData(_ timer: Timer) {
        for i in 0 ..< pointsCount {
            lineData.set(sin(Double(i) * 0.1 + phase), at: i)
            scatterData.set(cos(Double(i) * 0.1 + phase), at: i)
        }

        SCIUpdateSuspender.usingWith(surface) {
            self.lineDataSeries.update(y: self.lineData, at: 0)
            self.scatterDataSeries.update(y: self.scatterData, at: 0)
        }
            
        phase += 0.01
    }
</div>
<div class="code-snippet" id="cs">
    private double phase = 0;
    private const int TimerInterval = 10;
    private volatile bool _isRunning = false;
    // ...
    private void Start()
    {
        if (_isRunning) return;

        _isRunning = true;
        timer = new Timer(TimerInterval);
        timer.Elapsed += UpdateData;
        timer.AutoReset = true;
        timer.Start();
    }

    private void UpdateData(object sender, ElapsedEventArgs e)
    {
        InvokeOnMainThread(() =>
        {
            if (!_isRunning) return;

            for (int i = 0; i < pointsCount; i++)
            {
                lineData.Set(Math.Sin(i * 0.1 + phase), i);
                scatterData.Set(Math.Cos(i * 0.1 + phase), i);
            }

            using (Surface.SuspendUpdates())
            {
                lineDataSeries.UpdateRangeYAt(lineData, 0);
                scatterDataSeries.UpdateRangeYAt(scatterData, 0);
            }

            phase += 0.01;
        });
    }
</div>

Which will result in the following Chart:

> **_NOTE:_** Despite the chart is now real-time, it's still fully interactive, you can use modifiers from **[previous](tutorial-03---tooltips-and-legends.html)** tutorials with ease.

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-realtime-update.mp4"></video>

## Adding New Data Values
As well as using `-[ISCIXyDataSeries updateValuesX:y:at:]`, you can also use `-[ISCIXyDataSeries appendX:y:]` (or any other available **Append** method) to add new **data-values** to a DataSeries.

The code from above can be updated as follows to **append** new data constantly to the dataSeries:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    - (void)updateData {
        NSInteger x = _lineDataSeries.count;
        [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
            [_lineDataSeries appendX:@(x) y:@(sin(x * 0.1))];
            [_scatterDataSeries appendX:@(x) y:@(cos(x * 0.1))];
            
            // zoom series to fit viewport size into X-Axis direction
            [self.surface zoomExtents];
        }];
    }
</div>
<div class="code-snippet" id="swift">
    @objc fileprivate func updateData(_ timer: Timer) {
        let x = lineDataSeries.count
        SCIUpdateSuspender.usingWith(surface) {
            self.lineDataSeries.append(x: x, y: sin(Double(x) * 0.1))
            self.scatterDataSeries.append(x: x, y: cos(Double(x) * 0.1))
            
            // zoom series to fit viewport size into X-Axis direction
            self.surface.zoomExtents()
        }
    }
</div>
<div class="code-snippet" id="cs">
    private void UpdateData(object sender, ElapsedEventArgs e)
    {
        InvokeOnMainThread(() =>
        {
            if (!_isRunning) return;

            var x = lineDataSeries.Count;
            using (Surface.SuspendUpdates())
            {
                lineDataSeries.Append(x, Math.Sin(x * 0.1));
                scatterDataSeries.Append(x, Math.Cos(x * 0.1));

                // zoom series to fit viewport size into X-Axis direction
                Surface.ZoomExtents();
            }
        });
    }
</div>

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-realtime-append.mp4"></video>

## Scrolling Realtime Charts
What if you wanted to **scroll** as new data was appended? You have a few choices.
- If you want to be memory efficient, and you don't mind if you **discard** old data, you can use our **FIFO** (first-in-first-out) functionality.
- If you want to **preserve** old data, you can simply update the `ISCIAxisCore.visibleRange`. 

Since updating **VisibleRange** is fairly self-explanatory, we are going to explain the **FIFO** method.

#### Discarding Data when Scrolling using FifoCapacity
The most **memory efficient** way to achieve scrolling is to use `ISCIDataSeries.fifoCapacity` to set the maximum size of a DataSeries before old points are ***discarded***.
DataSeries in FIFO mode act as a circular - ***first-in-first-out*** - buffer. Once the capacity is exceeded, old points are discarded.
You cannot zoom back to see the old points, **once they are lost, they are lost**.

To make a **DataSeries** use the FIFO buffer, all you need to do is just set fifo capacity on the DataSeries, e.g.:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    _lineDataSeries.fifoCapacity = 300;
    _scatterDataSeries.fifoCapacity = 300;
</div>
<div class="code-snippet" id="swift">
    lineDataSeries.fifoCapacity = 300
    scatterDataSeries.fifoCapacity = 300
</div>
<div class="code-snippet" id="cs">
    lineDataSeries.FifoCapacity = 300;
    scatterDataSeries.FifoCapacity = 300;
</div>

> **_NOTE:_** After appending new data we call `zoomExtents` to make series to fit the viewport.

The following should be the result when you run the application:

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-realtime-fifo.mp4"></video>

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2004%20-%20Adding%20Realtime%20Updates)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-04)

Also, you can found **next tutorial** from this series here - [SciChart iOS Tutorial - Annotations](tutorial-05---annotations.html)

Of course, this is not the limit of what you can achieve with the SciChart iOS.
Our documentation contains lots of useful information, some of the articles you might want to read are listed below:
- [Axis Types](Axis APIs.html)
- [2D Chart Types](2D Chart Types.html)
- [Chart Modifiers](Chart Modifier APIs.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).

In particular, you might want to take a look at our [Fifo Scrolling Chart](https://www.scichart.com/example/ios-fifo-scrolling-chart/):

<video autoplay loop muted playsinline src="img/tutorials-2d/fifo-scrolling-chart-example.mp4"></video>
