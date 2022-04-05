# Axis Ticks - TickProvider and DeltaCalculator API
It is possible to further [customize the MajorDelta, MinorDelta](axis-ticks---majordelta-minordelta-and-autoticks.html) and affect the tick frequency of an axis, to have a **totally custom** set of axis ticks (gridlines, label intervals) on the chart. That can be achieved quite easily through [DeltaCalculator](#creating-your-own-deltacalculator) and [TickProvider](#creating-your-own-tickprovider) APIs.

## Creating your own DeltaCalculator
To have full control over the calculations of **MajorDelta** and **MinorDelta** you will need to create a custom `ISCIDeltaCalculator`. By default each `ISCITickProvider` has a DeltaCalculator created for it. The type of DeltaCalculator depends on the TickProvider. Below is a table of the DeltaCalculator already defined in SciChart iOS.

| **Delta Calculator Type**       | **Used By**                                            |
| ------------------------------- | ------------------------------------------------------ |
| `SCINumericDeltaCalculator`     | `SCINumericTickProvider` and `SCICategoryTickProvider` |
| `SCILogarithmicDeltaCalculator` | `SCILogarithmicNumericTickProvider`                    |
| `SCIDateDeltaCalculator`        | `SCIDateTickProvider`                                  |

To create a custom `ISCIDeltaCalculator`, you need to inherit from the correct class, according to the table above, then override `-[ISCIDeltaCalculator getDeltaFromRangeMin:max:minorsPerMajor:maxTicks:]` method. This method is called internally by `ISCITickProvider` for every axis when it needs to recalculate `SCIAxisDelta`.

Let's create a custom `ISCIDeltaCalculator` and use it with `SCINumericTickProvider` for `SCINumericAxis`.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCIDeltaTickProvider+Protected.h&gt;

    @interface CustomNumericDeltaCalculator : SCINumericDeltaCalculator
    @end

    @implementation CustomNumericDeltaCalculator

    - (id<ISCIAxisDelta>)getDeltaFromRangeMin:(id<ISCIComparable>)min max:(id<ISCIComparable>)max minorsPerMajor:(unsigned int)minorsPerMajor maxTicks:(unsigned int)maxTicks {
        // For the sake of simplicity, we will use simple constants.
        // But you might want to have some complex calculations here.
        NSNumber *minorDelta = @(0.07);
        NSNumber *majorDelta = @(0.1);
        return [[SCIAxisDelta alloc] initWithMinorDelta:minorDelta majorDelta:majorDelta];
    }

    @end
    ...
    // create a SCINumericTickProvider with CustomNumericDeltaCalculator and assign it to the axis
    axis.tickProvider = [[SCINumericTickProvider alloc] initWithDeltaCalculator:[CustomNumericDeltaCalculator new]];
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCIDeltaTickProvider

    class CustomNumericDeltaCalculator: SCINumericDeltaCalculator {
        override func getDeltaFromRangeMin(_ min: ISCIComparable!, max: ISCIComparable!, minorsPerMajor: UInt32, maxTicks: UInt32) -> ISCIAxisDelta! {
            // For the sake of simplicity, we will use simple constants.
            // But you might want to have some complex calculations here.
            let minorDelta = NSNumber(value: 0.07)
            let majorDelta = NSNumber(value: 0.1)
            return SCIAxisDelta(minorDelta: minorDelta, majorDelta: majorDelta)
        }
    }
    ...
    // create a SCINumericTickProvider with CustomNumericDeltaCalculator and assign it to the axis
    axis.tickProvider = SCINumericTickProvider(deltaCalculator: CustomNumericDeltaCalculator())
</div>
<div class="code-snippet" id="cs">
    class CustomNumericDeltaCalculator : SCINumericDeltaCalculator
    {
        public override IISCIAxisDelta getDeltaFromRange(IISCIComparable min, IISCIComparable max, uint minorsPerMajor, uint maxTicks)
        {
            // For the sake of simplicity, we will use simple constants.
            // But you might want to have some complex calculations here.
            var minorDelta = new NSNumber(0.07).FromComparable();
            var majorDelta = new NSNumber(0.1).FromComparable();
            return new SCIAxisDelta(minorDelta, majorDelta);
        }
    }
    ...
    // create a SCINumericTickProvider with CustomNumericDeltaCalculator and assign it to the axis
    axis.TickProvider = new SCINumericTickProvider(new CustomNumericDeltaCalculator());
</div>

![Numeric Delta Calculator](img/axis-2d/delta-calculator.png)

## Creating your own TickProvider
If you need to have **fine grained control** over axis ticks output, and custom [Delta Calculator]() isn't enough - you might want to use `ISCIAxisCore.tickProvider` property, which is available for all [Axis Types](Axis APIs.html). To use it - you will need to provide a custom `ISCITickProvider` and set it on your axis.

By default each axis has a `ISCITickProvider` created and assigned to it. The type of TickProvider depends on the [type of Axis](Axis APIs.html). Below is a table of the TickProviders already defined in SciChart iOS.

| **Tick Provider Type**              | **Provide ticks For**       |
| ----------------------------------- | --------------------------- |
| `SCINumericTickProvider`            | `SCINumericAxis`            |
| `SCILogarithmicNumericTickProvider` | `SCILogarithmicNumericAxis` |
| `SCIDateTickProvider`               | `SCIDateAxis`               |
| `SCICategoryTickProvider`           | `SCICategoryDateAxis`       |

To create a custom `SCITickProvider`, you need to inherit from the correct class, according to the [Axis Type](Axis APIs.html) you have, and override `-updateMinorTicks:andMajorTicks:` method which is called internally for every axis when it needs to recalculate major and minor ticks for drawing.

Let's create custom `SCINumericTickProvider` for `SCINumericAxis`

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCITickProvider+Protected.h&gt;

    @interface CustomNumericTickProvider : SCINumericTickProvider
    @end

    @implementation CustomNumericTickProvider

    - (void)updateMinorTicks:(SCIDoubleValues *)minorTicks andMajorTicks:(SCIDoubleValues *)majorTicks {
        double min = self.axis.visibleRange.minAsDouble;
        double max = self.axis.visibleRange.maxAsDouble;
        [majorTicks add:min];
        [majorTicks add:(min + max) / 2];
        [majorTicks add:max];
    }

    @end
    ...
    // create a CustomNumericTickProvider and assign it to the axis
    axis.tickProvider = [CustomNumericTickProvider new];
</div>
<div class="code-snippet" id="swift">
    import SciChart.Protected.SCITickProvider

    class CustomNumericTickProvider: SCINumericTickProvider {
        override func updateTicks(minorTicks: SCIDoubleValues!, majorTicks: SCIDoubleValues!) {
            let min = self.axis.visibleRange.minAsDouble
            let max = self.axis.visibleRange.maxAsDouble
            majorTicks.add(min)
            majorTicks.add((min + max) / 2)
            majorTicks.add(max)
        }
    }
    ...
    // create a CustomNumericTickProvider and assign it to the axis
    axis.tickProvider = CustomNumericTickProvider()
</div>
<div class="code-snippet" id="cs">
    class CustomNumericTickProvider: SCINumericTickProvider
    {
        public override void UpdateTicks(SCIDoubleValues minorTicks, SCIDoubleValues majorTicks)
        {
            var min = this.Axis.VisibleRange.MinAsDouble;
            var max = this.Axis.VisibleRange.MaxAsDouble;
            majorTicks.Add(min);
            majorTicks.Add((min + max) / 2);
            majorTicks.Add(max);
        }
    }
    ...
    // create a CustomNumericTickProvider and assign it to the axis
    axis.TickProvider = new CustomNumericTickProvider();
</div>

![Numeric Tick Provider](img/axis-2d/tick-provider.png)
