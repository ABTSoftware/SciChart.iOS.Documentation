# SciChart iOS Tutorial - Annotations
In the [prior tutorials](tutorial-04---adding-realtime-updates.html) we have familiarized with a great and powerful SciChart functionality.
In this one we are going to lean about another powerful SciChart API - [Annotations API](Annotations APIs.html) which allows placing different elements to the Chart.

Annotation available out of the box in SciChart are listed below:
- [SCIBoxAnnotation](boxannotation.html)
- [SCILineAnnotation](lineannotation.html)
- [SCILineArrowAnnotation](linearrowannotation.html)
- [SCIHorizontalLineAnnotation](horizontallineannotation.html)
- [SCIVerticalLineAnnotation](verticallineannotation.html)
- [SCITextAnnotation](textannotation.html)
- [SCIAxisLabelAnnotation](axislabelannotation.html)
- [SCIAxisMarkerAnnotation](axismarkerannotation.html)
- [SCICustomAnnotation](customannotation.html)

## Getting Started
This tutorial is suitable for **Objective-C**, **Swift** and **C#** with Xamarin.iOS.

> **_NOTE:_** Source code for this tutorial can be found at our Github Repository:
>
> - [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2005%20-%20Annotations)
> - [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-05)

In this tutorial, we are going to build on top of the [FIFO scrolling chart](tutorial-04---adding-realtime-updates.html#discarding-data-when-scrolling-using-fifocapacity) that was implemented in the previous [Adding Realtime Updates](tutorial-04---adding-realtime-updates.html) tutorial.

Please make corresponding amendments to your code if required.

## Adding Annotations to the Chart
Lets add a kind of news bulletin markers to the chart, one per 100 data points.
To achieve this, we are going to modify our `updateData` handler.
We want to create a `SCITextAnnotation` with some **text** and **background** and add it to the `ISCIChartSurface.annotations` collection.

See the modified code below:

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

            [self tryAddAnnotationAt:x];

            // zoom series to fit viewport size into X-Axis direction
            [self.surface zoomExtents];
        }];
    }

    - (void)tryAddAnnotationAt:(NSInteger)x {
        // add label every 100 data points
        if (x % 100 == 0) {
            SCITextAnnotation *label = [SCITextAnnotation new];
            label.text = @"N";
            label.x1 = @(x);
            label.y1 = @(0);
            label.horizontalAnchorPoint = SCIHorizontalAnchorPoint_Center;
            label.verticalAnchorPoint = SCIVerticalAnchorPoint_Center;
            label.fontStyle = [[SCIFontStyle alloc] initWithFontSize:30 andTextColor:UIColor.whiteColor];
            
            // add label into annotation collection
            [self.surface.annotations add:label];
            
            // if we add annotation and x > fifoCapacity
            // then we need to remove annotation which goes out of the screen
            if (x > _fifoCapacity) {
                [self.surface.annotations removeAt:0];
            }
        }
    }
</div>
<div class="code-snippet" id="swift">
    @objc fileprivate func updateData(_ timer: Timer) {
        let x = count
        SCIUpdateSuspender.usingWith(surface) {
            self.lineDataSeries.append(x: x, y: sin(Double(x) * 0.1))
            self.scatterDataSeries.append(x: x, y: cos(Double(x) * 0.1))
            
            self.tryAddAnnotation(at: x)
            
            // zoom series to fit viewport size into X-Axis direction
            self.surface.zoomExtents()
            self.count += 1
        }
    }
    
    fileprivate func tryAddAnnotation(at x: Int) {
        // add label every 100 data points
        if (x % 100 == 0) {
            let label = SCITextAnnotation()
            label.text = "N"
            label.set(x1: x)
            label.set(y1: 0)
            label.horizontalAnchorPoint = .center
            label.verticalAnchorPoint = .center
            label.fontStyle = SCIFontStyle(fontSize: 30, andTextColor: .white)
            
            // add label into annotation collection
            self.surface.annotations.add(label)
            
            // if we add annotation and x > fifoCapacity
            // then we need to remove annotation which goes out of the screen
            if (x > fifoCapacity) {
                self.surface.annotations.remove(at: 0)
            }
        }
    }
</div>
<div class="code-snippet" id="cs">
    private void UpdateData(object sender, ElapsedEventArgs e)
    {
        InvokeOnMainThread(() =>
        {
            if (!_isRunning) return;

            var x = count;
            using (Surface.SuspendUpdates())
            {
                lineDataSeries.Append(x, Math.Sin(x * 0.1));
                scatterDataSeries.Append(x, Math.Cos(x * 0.1));

                TryAddAnnotationAt(x);

                // zoom series to fit viewport size into X-Axis direction
                Surface.ZoomExtents();
                count++;
            }
        });
    }

    private void TryAddAnnotationAt(int x) 
    {
        // add label every 100 data points
        if (x % 100 == 0)
        {
            var label = new SCITextAnnotation
            {
                Text = "N",
                X1Value = x,
                Y1Value = 0,
                HorizontalAnchorPoint = SCIHorizontalAnchorPoint.Center,
                VerticalAnchorPoint = SCIVerticalAnchorPoint.Center,
                FontStyle = new SCIFontStyle(30, 0xFFFFFFFF)
            };

            // add label into annotation collection
            Surface.Annotations.Add(label);

            // if we add annotation and x > fifoCapacity
            // then we need to remove annotation which goes out of the screen
            if (x > fifoCapacity)
            {
                Surface.Annotations.RemoveAt(0);
            }
        }
    }
</div>

> **_NOTE:_** After appending new data we call `zoomExtents` to make series to fit the viewport.

Which will result in the following:

<video autoplay loop muted playsinline src="img/tutorials-2d/tutorials-2d-annotations.mp4"></video>

## Where to Go From Here?
You can download the final project from our GitHub Repository:
- [Swift](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-native/tutorials-2d/Tutorial%2005%20-%20Annotations)
- [Xamarin](https://github.com/ABTSoftware/SciChart.iOS.Documentation/tree/release_v4/samples/tutorials-xamarin/tutorials-2d/tutorial-05)

Also, you can found **next tutorial** from this series here - [SciChart iOS Tutorial - Multiple Axis](tutorial-06---multiple-axis.html)

Of course, this is not the limit of what you can achieve with the SciChart iOS. You might want to read the article about **annotations**:
- [Annotations API](Annotations APIs.html)

as well as some other general articles listed below:
- [Axis Types](Axis APIs.html)
- [2D Chart Types](2D Chart Types.html)
- [Chart Modifiers](Chart Modifier APIs.html)

Finally, start exploring. The SciChart iOS is quite extensive. 
You can look into our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which are full of 2D and 3D examples, which are also available on our [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples).