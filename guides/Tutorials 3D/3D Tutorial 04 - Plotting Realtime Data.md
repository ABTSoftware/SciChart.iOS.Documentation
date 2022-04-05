# SciChart iOS 3D Tutorial - Plotting Realtime Data
In Previous tutorials we've showed how to [Create a Simple 3D Chart](3d-tutorial-01---create-a-simple-scatter-chart-3d.html) and add some [Zoom and Rotate](3d-tutorial-02---zooming-and-rotating.html) interaction as well as [3D Tooltips Inspection](3d-tutorial-03---cursors-and-tooltips.html) via the [Chart Modifiers 3D API](Chart Modifier 3D APIs.html).

In this SciChart iOS 3D tutorial we're going to go a little further and show how to **update data** displayed by 3D chart in **realtime**.

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2004%20-%20Plotting%20Realtime%20Data)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-04)

Assuming you have completed previous tutorial, we will now make some changes to update the data dynamically.

## Updating Data Values
In our `ISCIDataSeries3D`, we have some static data so far. Let's update them in ***real-time*** now.

We are going to add a Timer and schedule updating the data on timer tick.
To update data in a **3D DataSeries**, we have to call one of the available `Update` methods on that DataSeries.
Since we are using `SCIXyzDataSeries3D`, we are going to use the `-[ISCIXyzDataSeries3D updateXValues:yValues:zValues:at:]` method.

But first of all, we need to adjust some previously created code and save DataSeries instance and data used in private fields, which will affect initial setup of a DataSeries a bit:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    static int const PointsCount = 200;
    // ...
    NSTimer *_timer;
    SCIXyzDataSeries3D *_dataSeries;
    SCIDoubleValues *_xValues;
    SCIDoubleValues *_yValues;
    SCIDoubleValues *_zValues;
    // ...
     _dataSeries = [[SCIXyzDataSeries3D alloc] initWithXType:SCIDataType_Double yType:SCIDataType_Double zType:SCIDataType_Double];
    for (int i = 0; i < PointsCount; ++i) {
        [_xValues add:[self getGaussianRandomNumber:5 stdDev:1.5]];
        [_yValues add:[self getGaussianRandomNumber:5 stdDev:1.5]];
        [_zValues add:[self getGaussianRandomNumber:5 stdDev:1.5]];
    }
    
    [_dataSeries appendXValues:_xValues yValues:_yValues zValues:_zValues];   
</div>
<div class="code-snippet" id="swift">
    private let pointsCount = 200
    private var timer: Timer!
    private let xValues = SCIDoubleValues()
    private let yValues = SCIDoubleValues()
    private let zValues = SCIDoubleValues()
    private let dataSeries = SCIXyzDataSeries3D(xType: .double, yType: .double, zType: .double)
    // ...
    for _ in 0 ..< pointsCount {
        xValues.add(getGaussianRandomNumber(mean: 5, stdDev: 1.5))
        yValues.add(getGaussianRandomNumber(mean: 5, stdDev: 1.5))
        zValues.add(getGaussianRandomNumber(mean: 5, stdDev: 1.5))
    }
    dataSeries.append(x: xValues, y: yValues, z: zValues)
</div>
<div class="code-snippet" id="cs">
    private int pointsCount = 200;
    private Timer timer;
    private SCIDoubleValues xValues = new SCIDoubleValues();
    private SCIDoubleValues yValues = new SCIDoubleValues();
    private SCIDoubleValues zValues = new SCIDoubleValues();
    private XyzDataSeries3D&lt;double, double, double&gt; dataSeries = new XyzDataSeries3D&lt;double, double, double&gt;();
    // ...
    for (int i = 0; i < pointsCount; i++)
    {
        xValues.Add(GetGaussianRandomNumber(5, 1.5));
        yValues.Add(GetGaussianRandomNumber(5, 1.5));
        zValues.Add(GetGaussianRandomNumber(5, 1.5));
    }
    dataSeries.AppendValues(xValues, yValues, zValues);

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
    _timer = [NSTimer scheduledTimerWithTimeInterval:0.02 target:self selector:@selector(updateData) userInfo:nil repeats:YES];
    // ...
    - (void)updateData {
        for (int i = 0; i < PointsCount; ++i) {
            
            double xValue = [_xValues getValueAt:i] + SCDRandomUtil.nextDouble - 0.5;
            double yValue = [_yValues getValueAt:i] + SCDRandomUtil.nextDouble - 0.5;
            double zValue = [_zValues getValueAt:i] + SCDRandomUtil.nextDouble - 0.5;
            
            [_xValues set:xValue at:i];
            [_yValues set:yValue at:i];
            [_zValues set:zValue at:i];
        }
        
        [SCIUpdateSuspender usingWithSuspendable:self.surface withBlock:^{
            [_dataSeries updateXValues:_xValues yValues:_yValues zValues:_zValues at:0];
        }];
    }
</div>
<div class="code-snippet" id="swift">
    timer = Timer.scheduledTimer(timeInterval: 0.01, target: self, selector: #selector(updateData), userInfo: nil, repeats: true)
    // ...
    @objc fileprivate func updateData(_ timer: Timer) {
        for i in 0 ..< pointsCount {
            let xValue = xValues.getValueAt(i) + Double.random(in: 0...1) - 0.5
            let yValue = yValues.getValueAt(i) + Double.random(in: 0...1) - 0.5
            let zValue = zValues.getValueAt(i) + Double.random(in: 0...1) - 0.5
            
            xValues.set(xValue, at: i)
            yValues.set(yValue, at: i)
            zValues.set(zValue, at: i)
        }
        
        SCIUpdateSuspender.usingWith(surface) {
            self.dataSeries.update(x: self.xValues, y: self.yValues, z: self.zValues, at: 0)
        }
    }
</div>
<div class="code-snippet" id="cs">
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

            var random = new Random();
            for (int i = 0; i < pointsCount; i++)
            {
                var xValue = xValues.GetValueAt(i) + random.NextDouble() - 0.5;
                var yValue = yValues.GetValueAt(i) + random.NextDouble() - 0.5;
                var zValue = zValues.GetValueAt(i) + random.NextDouble() - 0.5;

                xValues.Set(xValue, i);
                yValues.Set(yValue, i);
                zValues.Set(zValue, i);
            }

            using(Surface.SuspendUpdates())
            {
                dataSeries.UpdateRangeXyzAt(xValues, yValues, zValues, 0);
            }
        });
    }
</div>

Which will result in the following Chart:

> **_NOTE:_** Despite the chart is now real-time, it's still fully interactive, you can use modifiers from **[previous](3d-tutorial-03---cursors-and-tooltips.html)** tutorials with ease.

<video autoplay loop muted playsinline src="img/tutorials-3d/tutorials-3d-realtime.mp4"></video>

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-3d/3D%20Tutorial%2004%20-%20Plotting%20Realtime%20Data)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-3d/tutorial-04)

Of course, this is not the limit of what you can achieve with the SciChart iOS 3D.
Our documentation contains lots of useful information, some of the articles you might want to read are listed below:
- [3D Axis Types](Axis 3D APIs.html)
- [3D Chart Types](3D Chart Types.html)
- [3D Chart Modifiers](Chart Modifier 3D APIs.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).
