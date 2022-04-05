# Custom Modifiers - the SCIChartModifierBase API
The **`SCIChartModifierBase` API** is by far the **most powerful** API in the SciChart library. 
The `SCIChartModifierBase` provides an abstract base class for all of the 2D **ChartModifiers** within SciChart, all of our built-in 2D modifiers inherit from it. 
Using this API, you can **create behaviours** which you can attach to a chart to perform custom Zooming, Panning, Annotation & Markers, Legend output and much much more.
Any time you want to do something to alter the behaviour of a `SCIChartSurface` you should be thinking about creating **a custom modifier** to do it.

## Custom Chart Rotation Modifier
A simple example below shows how you can use `SCIChartModifierBase` API to create a chart rotation modifier. 
Add it onto your chart like any other modifier to see how it works.

Let's get to the code:

> **_NOTE:_** It's highly recommended to inherit from `SCIChartModifierBase` since it gives you some of the base implementation for free.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomRotateChartModifier : SCIChartModifierBase

    - (void)rotateChart;

    @end

    @implementation CustomRotateChartModifier

    - (void)rotateChart {
        if (self.isAttached) {
            id&lt;ISCIChartSurface&gt; parentSurface = self.parentSurface;
            id&lt;ISCIUpdateSuspender&gt; updateSuspender = [parentSurface suspendUpdates];
            
            @try {
                [self p_SCI_rotateAxes:parentSurface.xAxes];
                [self p_SCI_rotateAxes:parentSurface.yAxes];
            } @finally {
                updateSuspender = nil;
            }
        }
    }

    - (void)p_SCI_rotateAxes:(SCIAxisCollection *)axes {
        for (NSInteger i = 0, count = axes.count; i < count; i++) {
            id&lt;ISCIAxis&gt; axis = axes[i];
            SCIAxisAlignment axisAlignment = axis.axisAlignment;
            switch (axisAlignment) {
                case SCIAxisAlignment_Right:
                    axis.axisAlignment = SCIAxisAlignment_Bottom;
                    break;
                case SCIAxisAlignment_Left:
                    axis.axisAlignment = SCIAxisAlignment_Top;
                    break;
                case SCIAxisAlignment_Top:
                    axis.axisAlignment = SCIAxisAlignment_Right;
                    break;
                case SCIAxisAlignment_Bottom:
                    axis.axisAlignment = SCIAxisAlignment_Left;
                    break;
                case SCIAxisAlignment_Auto:
                    axis.axisAlignment = axis.isXAxis ? SCIAxisAlignment_Left : SCIAxisAlignment_Bottom;
                    break;
            }
        }
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class CustomRotateChartModifier: SCIChartModifierBase {
        public func rotateChart() {
            if self.isAttached {
                let parentSurface = self.parentSurface!
                let updateSuspender = parentSurface.suspendUpdates()
                defer {
                    updateSuspender.dispose()
                }
                
                rotateAxes(axes: parentSurface.xAxes)
                rotateAxes(axes: parentSurface.yAxes)
            }
        }
        
        fileprivate func rotateAxes(axes: SCIAxisCollection) {
            for i in 0 ..< axes.count {
                let axis = axes.item(at: i)
                let axisAlignment = axis.axisAlignment
                switch axisAlignment {
                case .right:
                    axis.axisAlignment = .bottom
                    break
                case .left:
                    axis.axisAlignment = .top
                    break
                case .top:
                    axis.axisAlignment = .right
                    break
                case .bottom:
                    axis.axisAlignment = .left
                    break
                case .auto:
                    axis.axisAlignment = axis.isXAxis ? .left : .bottom
                }
            }
        }
    }
</div>
<div class="code-snippet" id="cs">
    class CustomRotateChartModifier : SCIChartModifierBase
    {
        public void RotateChart()
        {
            if (IsAttached) {
                using (ParentSurface.SuspendUpdates())
                {
                    RotateAxes(ParentSurface.XAxes);
                    RotateAxes(ParentSurface.YAxes);
                }
            }
        }

        private void RotateAxes(SCIAxisCollection axes)
        {
            foreach (var axis in axes)
            {
                var axisAlignment = axis.AxisAlignment;
                switch (axisAlignment)
                {
                    case SCIAxisAlignment.Right:
                        axis.AxisAlignment = SCIAxisAlignment.Bottom;
                        break;
                    case SCIAxisAlignment.Left:
                        axis.AxisAlignment = SCIAxisAlignment.Top;
                        break;
                    case SCIAxisAlignment.Top:
                        axis.AxisAlignment = SCIAxisAlignment.Right;
                        break;
                    case SCIAxisAlignment.Bottom:
                        axis.AxisAlignment = SCIAxisAlignment.Left;
                        break;
                    case SCIAxisAlignment.Auto:
                        axis.AxisAlignment = axis.IsXAxis ? SCIAxisAlignment.Left : SCIAxisAlignment.Bottom;
                        break;
                }
            }
        }
    }
</div>

 The **modifier** above allows to rotate a chart when added to its `SCIChartModifierCollection`.

 The common methods that are currently implemented and are not likely to be replaced can be found in the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article. 
 For more information - please read the `ISCIChartModifier` API documentation.

> **_NOTE:_** If you want to handle gestures in your custom **ChartModifier**, you could derive it from the `SCIGestureModifierBase` class, which provides base infrastructure for **`UIGestureRecognizer`** usage.

## The SCIModifierEventArgs Type
If your custom **ChartModifier** requires to handle touch events or gestures, you might need to override the `-[ISCIReceiveEvents onEvent:]` method. 
It receive the only parameter of the `SCIModifierEventArgs` type. This type exposes the following information about an event that occurred:

| **Field**                               | **Description**                                                                                     |
| --------------------------------------- | --------------------------------------------------------------------------------------------------- |
| `SCIModifierEventArgs.source`           |  Provides the source of the event, of the `ISCIReceiveEvents` type.                                 |
| `SCIModifierEventArgs.isMaster`         |  Gets whether the event occurred on the **ParentSurface** or was propagated through **EventGroup**.  |
| `SCIModifierEventArgs.isInSourceBounds` |  Reports whether the event occurred within the **Source**.                                           |
| `SCIModifierEventArgs.isHandled`        |  Reports whether the event **has been already** passed to any other modifier and **handled** by it. |
| `SCIModifierEventArgs.e`                |  Returns the `ISCIEvent` instance.                                                                  |

> **_NOTE:_** To receive handled events, set `ISCIReceiveEvents.receiveHandledEvents` on a modifier to **`YES`**.
