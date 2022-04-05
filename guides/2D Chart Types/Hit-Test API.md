# Hit-Test API
The **Hit-Test API** is a set of virtual methods defined on `ISCIRenderableSeries` such as:
- `-[ISCIRenderableSeries hitTest:at:]`.
- `-[ISCIRenderableSeries hitTest:at:withHitTestRadius:]`.
- `-[ISCIRenderableSeries verticalSliceHitTest:at:]`.

This API is used by the `SCIRolloverModifier`, `SCITooltipModifier`, `SCICursorModifier` and `SCISeriesSelectionModifier` to transform touch on screen into **data-points**, and determine if touch event occurs over a point or over a series.

To call the **Hit-Test** method, use the following code:

> **_NOTE:_** You **must** transform any touch events into the coordinate space of the main chart area. Without this, all **hit-test** results will be inaccurate. You can learn more about it in the [Axis APIs - Convert Pixel to Data coordinates](axis-apis---convert-pixel-to-data-coordinates.html#transforming-pixels-to-the-inner-viewport) article.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    - (void)handleSingleTap:(UITapGestureRecognizer *)recognizer {
        // The touch point relative to the SCIChartSurface
        CGPoint location = [recognizer locationInView:recognizer.view.superview];
        // Translate the touch point relative to RenderableSeriesArea (or ModifierSurface)
        CGPoint hitTestPoint = [self.surface translatePoint:location hitTestable:self.surface.renderableSeriesArea];
        // Perform `Hit-Test` which will be stored in the `_hitTestInfo`
        [rSeries hitTest:_hitTestInfo at:hitTestPoint];
    }
</div>
<div class="code-snippet" id="swift">
    @objc fileprivate func handleSingleTap(_ recognizer: UITapGestureRecognizer) {
        // The touch point relative to the SCIChartSurface
        let location = recognizer.location(in: recognizer.view!.superview)
        // Translate the touch point relative to RenderableSeriesArea (or ModifierSurface)
        let hitTestPoint = surface.translate(location, hitTestable: surface.renderableSeriesArea)
        // Perform `Hit-Test` which will be stored in the `_hitTestInfo`
        rSeries.hitTest(_hitTestInfo, at: hitTestPoint)
    }
</div>
<div class="code-snippet" id="cs">
    private void HandleSingleTap(UITapGestureRecognizer recognizer)
    {
        // The touch point relative to the SCIChartSurface
        var location = recognizer.LocationInView(recognizer.View.Superview);
        // Translate the touch point relative to RenderableSeriesArea (or ModifierSurface)
        var hitTestPoint = Surface.TranslatePoint(location, Surface.RenderableSeriesArea);
        // Perform `Hit-Test` which will be stored in the `_hitTestInfo`
        rSeries.HitTest(_hitTestInfo, hitTestPoint);
    }
</div>

![Hit-Test API Example](img/chart-types-2d/hit-test-api-example.png)

> **_NOTE:_** You can see hit-test in action, in our full [Hit-Test API](https://www.scichart.com/example/ios-chart-chart-hit-test-api-example/) example, which be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):

## The Hit-Test Results
The `SCIHitTestInfo` which is used for **Hit-Test** contains some useful properties to determine what point was touched.

| **Property**                         | **Description**                                                                                                     |
| ------------------------------------ | ------------------------------------------------------------------------------------------------------------------- |
| `SCIHitTestInfo.hitTestPoint`        | The coordinates of a point, that is used for **Hit-Test**.                                                                     |
| `SCIHitTestInfo.hitTestRadius`       | The **Hit-Test** radius which was used for searching of nearest data point.                                         |
| `SCIHitTestInfo.dataSeriesIndex`     | If data point that was hit contains the index of the point in `ISCIDataSeries` which was hit.                            |
| `SCIHitTestInfo.pointSeriesIndex`    | If data point that was hit contains the index of hit test point in associated `ISCISeriesRenderPassData`.                |
| `SCIHitTestInfo.isHit`               | Boolean flag which tells whether or not `SCIHitTestInfo.hitTestPoint` was within a certain radius of point on a series. |
| `SCIHitTestInfo.isWithinDataBounds`  | Boolean flag which tells if `SCIHitTestInfo.hitTestPoint` lies between first and last X point on series.            |
| `SCIHitTestInfo.hitRenderableSeries` | The `ISCIRenderableSeries` which we perform **Hit-Test** on.                                                        |

## UseInterpolation Flag
The **UseInterpolation** flag increases the accuracy of the **Hit-Test** at the expense of performance. You can modify its value via the `SCITooltipModifierBase.useInterpolation` property, which is available for all the inheritors such as `SCITooltipModifier`, for example.

Consider `SCIFastLineRenderableSeries` and it's possible interpolation cases:
- `useInterpolation = NO` - hit-test will return `SCIHitTestInfo.isHit` = YES - **only** if the input mouse-point **is over a data-point**.
- `useInterpolation = YES` - hit-test will return `SCIHitTestInfo.isHit` = YES - if the input touch event **is over the line**.

This is useful say if you wanted to show a tooltip only on data-points (`useInterpolation = NO`) vs. anywhere on the line (`useInterpolation= YES`).

For other series types, such as `SCIFastCandlestickRenderableSeries`, `SCIFastColumnRenderableSeries`, `SCIFastMountainRenderableSeries`, using `useInterpolation = YES` will result in an `isHit = YES` when touch is **over the series** as opposed to **over the data-points**.

> **_NOTE:_** The interpolation is linear and linear only at the time of writing, it is not suitable for use by [Logarithmic Axis](Axis APIs.html#scilogarithmicnumericaxis).
