# The SCIZoomPanModifier
SciChart iOS provides an inertial **scrolling / panning** behavior via the `SCIZoomPanModifier`, available out of the box.

<video autoplay loop muted playsinline src="img/modifiers-2d/zoom-pan-modifier.mp4"></video>

Besides [common features](Chart Modifier APIs.html#common-chart-modifier-features) which are inherited from the `SCIChartModifierBase` class, 
the `SCIZoomPanModifier` allows to control its specific features via the following properties:
- `SCIZoomPanModifier.direction` - allows to **restrict zooming** to the horizontal or vertical **direction** only if needed.
- `SCIZoomPanModifier.zoomExtentsY` - allows to to keep **series' peeks** always in viewport.
- `SCIZoomPanModifier.clipModeX` - allows to specify the **behavior** when scrolling **reaches data extents** in X direction via the `SCIClipMode` enumeration.

There are several modes defined by the `SCIClipMode` enumeration:
- `None` - Means you can pan right off the edge of the data into uncharted space.
- `StretchAtExtents` - Causes a zooming (stretch) action when you reach the edge of the data.
- `ClipAtMin` - Forces the panning operation to stop suddenly at the minimum of the data, but expand at the maximum.
- `ClipAtMax` - Forces the panning operation to stop suddenly at the maximum of the data, but expand at the minimum.
- `ClipAtExtents` - Forces the panning operation to stop suddenly at the extents of the data.

There are two touch events defined by the `SCIZoomState` enumeration, which can be used to get or set current interaction state of the surface:
- AtExtents indicates that the zoom level is at extents.
- UserZooming indicates User has initiated a Zoom operation.

The `allowsGestureOnAxis` property enables or disables the application of modifiers, such as pan and pinch gestures, specifically on axis elements.
This property controls whether dragging or pinching gestures performed by the user on the axis will trigger the associated modifier actions. 
- `true`- Enables the modifier actions (pan, pinch) on the axis.
- `false` - Disables the modifier actions (pan, pinch) on the axis.
The default value for allowsGestureOnAxis is `false`.

It is particularly useful in scenarios where you may want to restrict or allow user interactions specifically on the axis, while still allowing interactions elsewhere on the chart surface.

## Adding a SCIZoomPanModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added to a `SCIChartSurface`](Chart Modifier APIs.html#adding-a-chart-modifier) via the`ISCIChartSurface.chartModifiers` property and `SCIZoomPanModifier` is no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier
    SCIZoomPanModifier *zoomPanModifier = [SCIZoomPanModifier new];
    zoomPanModifier.direction = SCIDirection2D_XDirection;
    zoomPanModifier.clipModeX = SCIClipMode_StretchAtExtents;
    zoomPanModifier.clipModeY = SCIClipMode_None;
    zoomPanModifier.zoomExtentsY = YES;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:zoomPanModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier
    let zoomPanModifier = SCIZoomPanModifier()
    zoomPanModifier.direction = .xDirection
    zoomPanModifier.clipModeX = .stretchAtExtents
    zoomPanModifier.clipModeY = .none
    zoomPanModifier.zoomExtentsY = true

    // Add the modifier to the surface
    self.surface.chartModifiers.add(zoomPanModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier
    var zoomPanModifier = new SCIZoomPanModifier();
    zoomPanModifier.Direction = SCIDirection2D.XDirection;
    zoomPanModifier.ClipModeX = SCIClipMode.StretchAtExtents;
    zoomPanModifier.ClipModeY = SCIClipMode.None;
    zoomPanModifier.ZoomExtentsY = true;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(zoomPanModifier);
</div>

> **_NOTE:_** To learn more about features available, please visit the [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.

> **_NOTE:_**  The `setDisableTouchEvent:` method is used to allow or prevent userInteraction of a surface view when it is inside a scroll view. 
It accepts `disableTouchEvent`, a Boolean value that determines whether user touch events are ignored or not.
 `YES` - Touch events will be ignored (i.e., user interaction is disabled).
 `NO` - Touch events will be recognized (i.e., user interaction is enabled).