# SCIStopLossTakeProfitCreationModifier
The `SCIStopLossTakeProfitCreationModifier` is a custom gesture modifier used for interactive creation of **Stop Loss / Take Profit annotations** on a SciChart surface.

It enables users to place two key points (Start → End) using touch gestures, with real-time visual feedback during placement. Style properties set on the modifier (takeProfitStroke, takeProfitFill, stopLossStroke, stopLossFill, showAxisLabels, axisSpanFillOpacity, labels, and formatLabel) are applied to the annotation as it is created, with the take-profit or stop-loss style pair chosen automatically depending on the direction between the two points.

## Overview
The modifier handles the full lifecycle of Stop Loss / Take Profit annotation creation:

- Sequential point placement via touch interaction
- Real-time dragging feedback for each point
- Automatic take-profit/stop-loss style selection based on the direction of the second point relative to the first
- Optional axis labels and a filled band on the X and Y axes spanning between the two points
- Automatic completion after the final point (End)
- Immediate reset for continuous drawing

Once completed, the annotation remains on the chart and a callback is triggered.

## Interaction Behavior

The creation flow follows a structured sequence:

### Per Point Interaction (Start → End)

| Gesture        | Behavior                                       |
| -------------- | ---------------------------------------------- |
| **Touch Down** | Places the current point at the touch location |
| **Drag**       | Moves the point dynamically in real-time       |
| **Touch Up**   | Locks the point and advances to the next one   |

### Completion

- After placing the **End** point, the annotation is finalized
- The direction between Start and End is resolved: if End is above Start, `takeProfitStroke`/`takeProfitFill` are applied; if End is below Start, `stopLossStroke`/`stopLossFill` are applied
- If `showAxisLabels` is enabled, labels for both points are drawn on the X and Y axes, with `axisSpanFillOpacity` applied to the band between them
- Each entry in `labels` has its text populated via the `formatLabel` block
- The `annotationCreationCompletionListener` callback is invoked
- The modifier resets immediately, ready for a new annotation

![Stop Loss / Take Profit Annotation](img/annotations/stop-loss-take-profit-annotation.png)

## Retrieving Annotation Points After Drawing Completion
After an annotation drawing is completed, you can retrieve its underlying points using:
getBaseDataValues()
getBasePoints()
Both methods return the annotation's defining points, but in different coordinate spaces.

### getBaseDataValues()
- Returns the annotation points in data space (axis values).
- X and Y values correspond to the chart's actual data coordinates
- Independent of pixel resolution or screen scaling
- Useful for storing, analysis, or reloading annotations

### getBasePoints()
- Returns annotation points in pixel space (rendered screen coordinates).
- X and Y values correspond to screen pixels
- Useful for rendering overlays or UI alignment

## Usage Example
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Create a new Stop Loss / Take Profit creation modifier instance
SCIStopLossTakeProfitCreationModifier *modifier = [SCIStopLossTakeProfitCreationModifier new];

// Set the style used for the zone when the second point is above the first (take profit)
modifier.takeProfitStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF16A34A thickness:2 strokeDashArray:@[@6, @3] antiAliasing:YES];
modifier.takeProfitFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x2E16A34A];

// Set the style used for the zone when the second point is below the first (stop loss)
modifier.stopLossStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFEF4444 thickness:2 strokeDashArray:@[@6, @3] antiAliasing:YES];
modifier.stopLossFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x2EEF4444];

// Configure the value labels and their text formatting
modifier.labels = [self makeStopLossTakeProfitLabels];
modifier.formatLabel = [self makeStopLossTakeProfitLabelFormatter];
modifier.isEditable = YES;

// Callback triggered when the user completes placing both points (Start, End)
// Provides access to the fully created annotation object
__weak typeof(self) weakSelf = self;
modifier.annotationCreationCompletionListener = ^(id _Nonnull createdAnnotation, SCIAnnotationCreationType type) {
    __strong typeof(weakSelf) strongSelf = weakSelf;
    if (!strongSelf) return;

    NSLog(@"Stop Loss / Take Profit annotation created: %@ type %@", createdAnnotation, SCIAnnotationTypeName(type));

    if (![createdAnnotation isKindOfClass:[SCIStopLossTakeProfitAnnotation class]]) return;

    SCIStopLossTakeProfitAnnotation *sltp = (SCIStopLossTakeProfitAnnotation *)createdAnnotation;
    NSArray<SCIComparablePoint *> *points = [sltp getBaseDataValues];

    // Get data points
    // Index mapping: 0 = Start, 1 = End
    NSLog(@"[StopLossTakeProfit] Start: date=%@  price=%@", points[0].x, points[0].y);
    NSLog(@"[StopLossTakeProfit] End: date=%@  price=%@", points[1].x, points[1].y);
};

// Add the modifier to the chart surface
[self.surface.chartModifiers add:modifier];


</div>
<div class="code-snippet" id="swift">
// Create a new Stop Loss / Take Profit creation modifier instance
self.stopLossTakeProfitModifier = SCIStopLossTakeProfitCreationModifier()

// Set the style used for the zone when the second point is above the first (take profit)
self.stopLossTakeProfitModifier.takeProfitStroke = SCISolidPenStyle(color: 0xFF16A34A, thickness: 2, strokeDashArray: [6, 3])
self.stopLossTakeProfitModifier.takeProfitFill = SCISolidBrushStyle(color: 0x2E16A34A)

// Set the style used for the zone when the second point is below the first (stop loss)
self.stopLossTakeProfitModifier.stopLossStroke = SCISolidPenStyle(color: 0xFFEF4444, thickness: 2, strokeDashArray: [6, 3])
self.stopLossTakeProfitModifier.stopLossFill = SCISolidBrushStyle(color: 0x2EEF4444)

// Configure the value labels and their text formatting
self.stopLossTakeProfitModifier.labels = self.makeStopLossTakeProfitLabels()
self.stopLossTakeProfitModifier.formatLabel = self.makeStopLossTakeProfitLabelFormatter()
self.stopLossTakeProfitModifier.isEditable = true

// Callback triggered when the user completes placing both points (Start, End)
// Provides access to the fully created annotation object
self.stopLossTakeProfitModifier.annotationCreationCompletionListener = { [weak self] createdAnnotation, type in
    guard self != nil else { return }

    print("Stop Loss / Take Profit annotation created: \(createdAnnotation), type: \(SCIAnnotationTypeName(type))")

    guard let sltp = createdAnnotation as? SCIStopLossTakeProfitAnnotation else { return }
    let arrPoints = sltp.getBaseDataValues()

    // Get data points
    // Index mapping: 0 = Start, 1 = End
    print("Stop Loss / Take Profit point Start: \(arrPoints[0].x), \(arrPoints[0].y)")
    print("Stop Loss / Take Profit point End: \(arrPoints[1].x), \(arrPoints[1].y)")
}

// Add the modifier to the chart surface
self.surface.chartModifiers.add(items: self.stopLossTakeProfitModifier)
</div>


The SCIStopLossTakeProfitCreationModifier can be configured using the properties listed in the table below:

| **Field**                              | **Description**                                                                               |
| -------------------------------------- | --------------------------------------------------------------------------------------------- |
| `takeProfitStroke`                     | Defines the `SCIPenStyle` used for the level lines when the second point is above the first.  |
| `takeProfitFill`                       | Defines the `SCIBrushStyle` used to fill the zone when the second point is above the first.   |
| `stopLossStroke`                       | Defines the `SCIPenStyle` used for the level lines when the second point is below the first.  |
| `stopLossFill`                         | Defines the `SCIBrushStyle` used to fill the zone when the second point is below the first.   |
| `showAxisLabels`                       | Determines whether axis labels are shown on both the X and Y axes.                            |
| `axisSpanFillOpacity`                  | Defines the opacity of the band filled on the axes between the two axis labels.               |
| `labels`                               | Defines the labels applied to each new annotation.                                            |
| `formatLabel`                          | Defines the block which supplies the complete text of each label.                             |
| `annotationCreationCompletionListener` | A callback invoked when a full Stop Loss / Take Profit annotation (both points) is completed. |

## Best Practices
- Pair `takeProfitStroke`/`takeProfitFill` and `stopLossStroke`/`stopLossFill` with clearly distinct colours (e.g. green vs red) so risk and reward zones are identifiable at a glance
- Use a dashed `strokeDashArray` on the level lines to visually distinguish them from solid trend or measure lines
- Enable `showAxisLabels` with a moderate `axisSpanFillOpacity` so the price band reads clearly directly on the axis
- Use `formatLabel` to surface distance, pip count, or risk-reward ratio alongside each entry in `labels`
- Disable conflicting gesture modifiers during drawing for better UX
- Use the CompletionListener to validate or store annotations
- Call reset when switching tools or exiting drawing mode

## Notes
- The modifier automatically resets after completing the End point
- Direction (take profit vs stop loss) is resolved by comparing the Y value of the End point to the Start point
- Only one annotation is created per interaction cycle
- Designed for marking risk/reward zones relative to an entry price on financial charts