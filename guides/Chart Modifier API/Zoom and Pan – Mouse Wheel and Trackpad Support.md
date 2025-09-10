# Zooming with Mouse Wheel, Trackpad, or Keyboard
Learn how to implement custom zooming in SciChart using the mouse wheel, trackpad, or keyboard + / - keys on macOS or Catalyst.
### Overview
In addition to built-in zoom modifiers like `SCIPinchZoomModifier` and `SCIZoomPanModifier`, you can implement custom zooming behavior using mouse wheel, trackpad gestures, or keyboard keys. This is especially useful in macOS Catalyst or full macOS (AppKit) applications where fine-grained zoom control is desired.
This article demonstrates how to override both `scrollWheel(with:)` and `keyDown(with:)` in an `NSView`-based subclass to apply a centered zoom effect on the chart surface. Both methods use the same zoom calculation logic, keeping the zoom centered around the cursor location.

> **_NOTE:_** This approach assumes a 2D chart with SCINumericAxis on both X and Y axes.
For iOS or non-macOS platforms, use built-in modifiers like SCIPinchZoomModifier.

## Zooming with Mouse Wheel or Trackpad
This code overrides `scrollWheel(with:)` to handle zooming via scroll gestures.
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
- (void)scrollWheel:(NSEvent *)event {
    id<ISCIAxis> xAxis = (id<ISCIAxis>)self->surface.xAxes.firstObject;
    id<ISCIAxis> yAxis = (id<ISCIAxis>)self->surface.yAxes.firstObject;
    
    SCIDoubleRange *xVisible = (SCIDoubleRange *)xAxis.visibleRange;
    SCIDoubleRange *yVisible = (SCIDoubleRange *)yAxis.visibleRange;
    
    if (![xAxis isKindOfClass:[SCINumericAxis class]] ||
        ![yAxis isKindOfClass:[SCINumericAxis class]] ||
        ![xVisible isKindOfClass:[SCIDoubleRange class]] ||
        ![yVisible isKindOfClass:[SCIDoubleRange class]]) {
        return;
    }

    NSPoint locationInView = [self.view convertPoint:event.locationInWindow fromView:nil];
    
    double xData = [[xAxis getDataValueFrom:locationInView.x] toDouble];
    double yData = [[xAxis getDataValueFrom:locationInView.y] toDouble];
    
    double zoomStep = 0.1;
    double scale = (event.scrollingDeltaY > 0) ? (1 - zoomStep) : (1 + zoomStep);
    
    double newXMin = xData - (xData - xVisible.minAsDouble) * scale;
    double newXMax = xData + (xVisible.maxAsDouble - xData) * scale;
    double newYMin = yData - (yData - yVisible.minAsDouble) * scale;
    double newYMax = yData + (yVisible.maxAsDouble - yData) * scale;
    
    xAxis.visibleRange = [[SCIDoubleRange alloc] initWithMin:newXMin max:newXMax];
    yAxis.visibleRange = [[SCIDoubleRange alloc] initWithMin:newYMin max:newYMax];
}
</div>
<div class="code-snippet" id="swift">
    override func scrollWheel(with event: NSEvent) {
        
        guard
            let xAxis = self.surface.xAxes.firstObject as? SCINumericAxis,
            let yAxis = self.surface.yAxes.firstObject as? SCINumericAxis,
            let xVisible = xAxis.visibleRange as? SCIDoubleRange,
            let yVisible = yAxis.visibleRange as? SCIDoubleRange else {
            return
        }
        
        // Convert mouse location to data values
        let location = view.convert(event.locationInWindow, from: nil)
        let xData = xAxis.getDataValue(Float(location.x))
        let yData = yAxis.getDataValue(Float(location.y))
        
        // Zoom factor
        let zoomStep = 0.1
        let scale = event.scrollingDeltaY > 0 ? (1 - zoomStep) : (1 + zoomStep)
        
        // Calculate new visible ranges keeping mouse point centered
        let newXMin = xData.toDouble() - (xData.toDouble() - xVisible.minAsDouble) * scale
        let newXMax = xData.toDouble() + (xVisible.maxAsDouble - xData.toDouble()) * scale
        let newYMin = yData.toDouble() - (yData.toDouble() - yVisible.minAsDouble) * scale
        let newYMax = yData.toDouble() + (yVisible.maxAsDouble - yData.toDouble()) * scale
        
        //Change visible range
        xAxis.visibleRange = SCIDoubleRange(min: newXMin, max: newXMax)
        yAxis.visibleRange = SCIDoubleRange(min: newYMin, max: newYMax)
    }
</div>

> **_TIP:_**: If you want to zoom from the center of the chart instead of the cursor, use the chart center point instead of the cursor location for xData and yData.

## Zooming with Keyboard (`+` / `-` Keys)  
The same logic can be applied in `keyDown(with:)` to allow zooming using keyboard input.  
- Press `+` to zoom **in**  
- Press `-` to zoom **out**

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
- (void)keyDown:(NSEvent *)event {
    NSString *chars = [event charactersIgnoringModifiers];
    if (chars.length == 0) return;

    id<ISCIAxis> xAxis = (id<ISCIAxis>)self->surface.xAxes.firstObject;
    id<ISCIAxis> yAxis = (id<ISCIAxis>)self->surface.yAxes.firstObject;

    SCIDoubleRange *xVisible = (SCIDoubleRange *)xAxis.visibleRange;
    SCIDoubleRange *yVisible = (SCIDoubleRange *)yAxis.visibleRange;

    if (![xAxis isKindOfClass:[SCINumericAxis class]] ||
        ![yAxis isKindOfClass:[SCINumericAxis class]] ||
        ![xVisible isKindOfClass:[SCIDoubleRange class]] ||
        ![yVisible isKindOfClass:[SCIDoubleRange class]]) {
        return;
    }

    NSPoint locationInView = [self.view convertPoint:event.locationInWindow fromView:nil];
    double xData = [xAxis getDataValueFrom:locationInView.x].toDouble;
    double yData = [yAxis getDataValueFrom:locationInView.y].toDouble;

    double zoomStep = 0.1;
    double scale = 1.0;

    unichar keyChar = [chars characterAtIndex:0];
    if (keyChar == '+' || keyChar == '=') {
        scale = 1 - zoomStep; // Zoom in
    } else if (keyChar == '_' || keyChar == '-') {
        scale = 1 + zoomStep; // Zoom out
    } else {
        return; // Ignore other keys
    }

    double newXMin = xData - (xData - xVisible.minAsDouble) * scale;
    double newXMax = xData + (xVisible.maxAsDouble - xData) * scale;
    double newYMin = yData - (yData - yVisible.minAsDouble) * scale;
    double newYMax = yData + (yVisible.maxAsDouble - yData) * scale;

    xAxis.visibleRange = [[SCIDoubleRange alloc] initWithMin:newXMin max:newXMax];
    yAxis.visibleRange = [[SCIDoubleRange alloc] initWithMin:newYMin max:newYMax];
}
</div>
<div class="code-snippet" id="swift">
    override func keyDown(with event: NSEvent) {
        guard let chars = event.charactersIgnoringModifiers else { return }
        guard
            let xAxis = self.surface.xAxes.firstObject as? SCINumericAxis,
            let yAxis = self.surface.yAxes.firstObject as? SCINumericAxis,
            let xVisible = xAxis.visibleRange as? SCIDoubleRange,
            let yVisible = yAxis.visibleRange as? SCIDoubleRange else {
            return
        }
        
        // Convert mouse location to data values
        let location = view.convert(event.locationInWindow, from: nil)
        let xData = xAxis.getDataValue(Float(location.x))
        let yData = yAxis.getDataValue(Float(location.y))
        
        // Zoom factor
        let zoomStep = 0.1
        var scale: Double = 0.0;
        if chars == "+" || chars == "="{
            scale = 1 - zoomStep
        }
        else if chars == "_" || chars == "-" {
            scale = 1 + zoomStep
        }
        
        // Calculate new visible ranges keeping mouse point centered
        let newXMin = xData.toDouble() - (xData.toDouble() - xVisible.minAsDouble) * scale
        let newXMax = xData.toDouble() + (xVisible.maxAsDouble - xData.toDouble()) * scale
        let newYMin = yData.toDouble() - (yData.toDouble() - yVisible.minAsDouble) * scale
        let newYMax = yData.toDouble() + (yVisible.maxAsDouble - yData.toDouble()) * scale
        
        //Change visible range
        xAxis.visibleRange = SCIDoubleRange(min: newXMin, max: newXMax)
        yAxis.visibleRange = SCIDoubleRange(min: newYMin, max: newYMax)
    }
</div>

### How it Works
- `scrollWheel(with:)` and `keyDown(with:)` both convert the cursor location into data coordinates using `getDataValue(_:)`.
- A zoom scale is calculated depending on scroll direction or key pressed.
- Visible range updates are centered around the cursor location, ensuring intuitive and smooth zooming.

> **_NOTE:_** To learn more about the built-in zoom and pan modifiers, please refer to the [Zoom and Pan Modifiers](Chart Modifier APIs.html#zoom-and-pan-modifiers) article.

