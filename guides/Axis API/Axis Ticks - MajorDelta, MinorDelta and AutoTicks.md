# Axis Ticks - MajorDelta, MinorDelta and AutoTicks

## Axis Ticks, Labels and Grid Lines
In SciChart, the **Ticks** are small marks around the chart on an axis. There are **Minor** and **Major** Ticks, where Minor Ticks are placed in between Major ones. By default, Major Ticks are longer and thicker than Minor Ticks.

**Axis Labels** appears for every Major Tick, if there is enough space around. If Axis Labels are placed too tightly, some of them **get culled** to make more space. If necessary, labels ***culling can be switched off*** via `ISCIAxis.isLabelCullingEnabled` property.

**Grid Lines** correspond to **Ticks** on an axis. Likewise, there are Minor and Major Grid lines. In SciChart, **axes are responsible** not only for drawing Ticks and Labels, but also **for the chart grid**. An axis draws only those Grid Lines that can be measured against it, i.e. a horizontal axis draws vertical grid lines and vice versa.

![Ticks and Deltas](img/axis-2d/major-minor-ticks.png)
<center><sub><sup>majorDelta = 2; minorDelta = 0.4; autoTicks = NO</sub></sup></center>

To learn more about possible options for Axis Ticks, Gridlines and Labels, please refer to the [Styling Gridlines, Ticks and Axis Bands](axis-styling---styling-grid-lines-tick-lines-and-axis-bands.html) and [Styling Title and Axis Labels](styling-the-axis-title-and-labels.html) articles.

## Automatic Tick Spacing
In SciChart, the **difference** between two Major Ticks **is called MajorDelta**, and that between two minor Ticks - **MinorDelta**. By default, both delta values, and therefore the Tick spacing, are calculated automatically according to the VisibleRange and size of an axis. As zoom level changes, the `ISCIAxisCore.majorDelta`, `ISCIAxisCore.minorDelta` of every axis will be updated correspondingly, and the tick frequency may change.

There are two additional options used to control the auto ticking behavior. The `ISCIAxisCore.maxAutoTicks` property allows to limit the number of generated ticks to the desired number, and the `ISCIAxisCore.minorsPerMajor` - specifies how many Minor Ticks are drawn between two Major Ticks (the default is 5).

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    // change max possible auto ticks amount (the default is 10)
    axis.maxAutoTicks = 20;
    // specify that there should be 10 Minor Ticks between two Major Ticks (the default is 5)
    axis.minorsPerMajor = 10;
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    // change max possible auto ticks amount (the default is 10)
    axis.maxAutoTicks = 20
    // specify that there should be 10 Minor Ticks between two Major Ticks (the default is 5)
    axis.minorsPerMajor = 10
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis()
    // change max possible auto ticks amount (the default is 10)
    axis.MaxAutoTicks = 20;
    // specify that there should be 10 Minor Ticks between two Major Ticks (the default is 5)
    axis.MinorsPerMajor = 10;
</div>

## Altering Tick Spacing
It is also possible to set **MinorDelta** and **MajorDelta** manually. To change them, automatic calculation must be **switched off** via the `ISCIAxisCore.autoTicks` property. To set delta values, call the `ISCIAxisCore.minorDelta` and `ISCIAxisCore.majorDelta` properties. Please note that both delta values need to be set in this case:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.autoTicks = NO;
    axis.minorDelta = @(2);
    axis.majorDelta = @(10);
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.autoTicks = false
    axis.minorDelta = NSNumber(value: 2)
    axis.majorDelta = NSNumber(value: 10)
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis()
    axis.AutoTicks = false;
    axis.MinorDelta = 2.FromComparable();
    axis.MajorDelta = 10.FromComparable();
</div>

## Altering Tick Spacing for the Logarithmic Axis
`SCILogarithmicNumericAxis` is a special case that need to be mentioning here. Due to the exponential nature of this axis type, **MajorDelta** represents the **difference between exponents** of neighbouring major ticks, not between their actual values. For instance, having `ISCIAxisCore.majorDelta` = 3 and `ISCILogarithmicNumericAxis.logarithmicBase` = 10 on a `SCILogarithmicNumericAxis` specifies that major ticks and gridlines will be spaced at 10<sup>3</sup> intervals (exponents will be divisors of MajorDelta):

![SCILogarithmicNumericAxis Tick Spacing](img/axis-2d/log-axis-major-tick-spacing.png)
<center><sub><sup>LogarithmicAxis, MajorDelta = 3, minor ticks/gridlines hidden</sub></sup></center>

On the contrary, the difference between neighbouring minor ticks is not exponential. Therefore, **MinorDelta** represents the **difference between the actual values** of neighbouring minor ticks. 

For instance, having `ISCIAxisCore.minorDelta` = 100 results in minor ticks and gridlines appearing with the increment of 100x10<sup>E</sup>, where `E` is the exponent of the nearest major tick to the left. So, the interval will be like the following:

| **Range**                            | **Interval**       | **Raw Interval** |
| ------------------------------------ | ------------------ | ---------------- |
| [1x10<sup>0</sup>, 1x10<sup>3</sup>] | 100x10<sup>0</sup> | 100              |
| [1x10<sup>3</sup>, 1x10<sup>6</sup>] | 100x10<sup>3</sup> | 100000           |
| [1x10<sup>6</sup>, 1x10<sup>9</sup>] | 100x10<sup>6</sup> | 100000000        |

![SCILogarithmicNumericAxis Tick Spacing](img/axis-2d/log-axis-minor-tick-spacing.png)
<center><sub><sup>LogarithmicAxis, MajorDelta = 3, MinorDelta = 100</sub></sup></center>
