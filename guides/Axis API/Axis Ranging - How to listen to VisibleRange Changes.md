# Axis Ranging - How to listen to VisibleRange Changes
It is possible to subscribe to listening to the `ISCIAxisCore.visibleRange` changes using a `SCIVisibleRangeChangeListener`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; axis = [SCINumericAxis new];
    axis.visibleRangeChangeListener = ^(id<ISCIAxisCore> axis, id<ISCIRange> oldRange, id<ISCIRange> newRange, BOOL isAnimating) {
        // TODO: handle range changes here
    };
</div>
<div class="code-snippet" id="swift">
    let axis = SCINumericAxis()
    axis.visibleRangeChangeListener = { (axis, oldRange, newRange, isAnimating) in
        // TODO: handle range changes here
    }
</div>
<div class="code-snippet" id="cs">
    var xAxis = new SCINumericAxis();
    xAxis.VisibleRangeChanged += (IISCIAxisCore axis, IISCIRange oldRange, IISCIRange newRange, bool isAnimating) =>
    {
        // TODO: handle range changes here
    };
</div>

> **_NOTE:_** You can differentiate between changes that were part of an animation by checking the **isAnimating** parameter. For example, an animated zoom to extents operation will fire the callback many times with `isAnimated = true`, then once at the end with `isAnimated = false`.

The most typical use for this callback is to perform some kind of operation when the `ISCIAxisCore.visibleRange` changes, such as updating UI.

It is also possible to use this callback to restrict the VisibleRange in some way, e.g set a bounded or clipped range onto Axis.VisibleRange when the range changes outside of a desired area.

> **_NOTE:_** We've already used this technique in [Advanced VisibleRange Clipping](axis-ranging---restricting-visiblerange.html#advanced-visiblerange-clipping)

## See Also
- [Axis Types in SciChart](Axis APIs.html)
- [Axis Ranging - AutoRange](axis-ranging---autorange.html)
- [Axis Ranging - Get or Set VisibleRange](axis-ranging---get-or-set-visiblerange.html)
- [Axis Ranging - Restricting VisibleRange](axis-ranging---restricting-visiblerange.html)
- [Axis Ranging - VisibleRange and DataRange](axis-ranging---visiblerange-and-datarange.html)