# Axis Ranging - AutoRange
At the outset, the [VisibleRange](axis-ranging---visiblerange-and-datarange.html) is adjusted to be equal to the [DataRange](axis-ranging---visiblerange-and-datarange.html) of an axis. However, an axis won't adjust its **VisibleRange** automatically when data changes, unless it is configured to do this. The default behavior can be changed by applying different `SCIAutoRange` modes on `ISCIAxisCore.autoRange`.

> **_NOTE!_** Setting an explicit `ISCIAxisCore.visibleRange` on the axis will override this behaviour.

## AutoRange Once
This is the **default** setting. The axis will attempt to autorange once to fit the data as you start the chart. This is a **one-time** action - the `ISCIAxisCore.visibleRange`  won't adjust to any data changes in future.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.autoRange = SCIAutoRange_Once;
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.autoRange = .once;
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis { AutoRange = SCIAutoRange.Once };
</div>

## AutoRange Always
In this mode, the axis will attempt to autorange **always** to fit the data every time the chart is drawn. The **VisibleRange** will adjust to data changes correspondingly.

> **_NOTE!_** Please be aware that this setting **will override any other ranging**, including zooming and panning by modifiers, but is useful in situations where you always want to view the extents of the data.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.autoRange = SCIAutoRange_Always;
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.autoRange = .always;
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis { AutoRange = SCIAutoRange.Always };
</div>

## AutoRange Never
The axis will **never** autorange. With this option, you would need to set the `ISCIAxisCore.visibleRange` manually. The **VisibleRange** won't adjust to any data changes.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.autoRange = SCIAutoRange_Never;
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.autoRange = .never;
</div>
<div class="code-snippet" id="cs">
    var axis = new SCINumericAxis { AutoRange = SCIAutoRange.Never };
</div>

## Using SCIViewportManager
The AutoRange behavior can be **completely overridden** if necessary. The `ISCIViewportManager` provides the full access to and control over axis ranges and viewport. The default implementation of `ISCIViewportManager` is the `SCIDefaultViewportManager` class. Custom ViewportManagers can be assigned via `ISCIChartSurface.viewportManager` property.

