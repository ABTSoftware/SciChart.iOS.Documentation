# SCIMeasureCreationModifier
The `SCIMeasureCreationModifier` is a custom gesture modifier used for interactive creation of **Measure annotations** on a SciChart surface.

It enables users to place two key points (Start → End) using touch gestures, with real-time visual feedback during placement. Style properties set on the modifier (growingStroke, growingFill, decliningStroke, decliningFill, labelStyle, yValueScaleFactor, and snapToCandles) are applied to the annotation as it is created, with the growing or declining style pair chosen automatically depending on the direction between the two points.

## Overview
The modifier handles the full lifecycle of Measure annotation creation:

- Sequential point placement via touch interaction
- Real-time dragging feedback for each point
- Automatic growing/declining style selection based on the direction of the second point relative to the first
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
- The direction between Start and End is resolved: if End is above Start, `growingStroke`/`growingFill` are applied; if End is below Start, `decliningStroke`/`decliningFill` are applied
- The `annotationCreationCompletionListener` callback is invoked
- The modifier resets immediately, ready for a new annotation

![Measure Annotation](img/annotations/measure-annotation.png)

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
// Create a new Measure creation modifier instance
SCIMeasureCreationModifier *modifier = [SCIMeasureCreationModifier new];

// Set the style used when the second point is above the first (growing)
modifier.growingStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF2563EB thickness:2];
modifier.growingFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x292563EB];

// Set the style used when the second point is below the first (declining)
modifier.decliningStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFDC2626 thickness:2];
modifier.decliningFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x29DC2626];

// Scale the measured price difference shown in the label (e.g. pips, points, %)
modifier.yValueScaleFactor = 100;

// Snap the anchor points to whole candles
modifier.snapToCandles = YES;
modifier.isEditable = YES;

// Callback triggered when the user completes placing both points (Start, End)
// Provides access to the fully created annotation object
__weak typeof(self) weakSelf = self;
modifier.annotationCreationCompletionListener = ^(id _Nonnull createdAnnotation, SCIAnnotationCreationType type) {
    __strong typeof(weakSelf) strongSelf = weakSelf;
    if (!strongSelf) return;

    NSLog(@"Measure annotation created: %@ type %@", createdAnnotation, SCIAnnotationTypeName(type));

    if (![createdAnnotation isKindOfClass:[SCIMeasureAnnotation class]]) return;

    SCIMeasureAnnotation *measure = (SCIMeasureAnnotation *)createdAnnotation;
    NSArray<SCIComparablePoint *> *points = [measure getBaseDataValues];

    // Get data points
    // Index mapping: 0 = Start, 1 = End
    NSLog(@"[Measure] Start: date=%@  price=%@", points[0].x, points[0].y);
    NSLog(@"[Measure] End: date=%@  price=%@", points[1].x, points[1].y);
};

// Add the modifier to the chart surface
[self.surface.chartModifiers add:modifier];


</div>
<div class="code-snippet" id="swift">
// Create a new Measure creation modifier instance
self.measureModifier = SCIMeasureCreationModifier()

// Set the style used when the second point is above the first (growing)
self.measureModifier.growingStroke = SCISolidPenStyle(color: 0xFF2563EB, thickness: 2)
self.measureModifier.growingFill = SCISolidBrushStyle(color: 0x292563EB)

// Set the style used when the second point is below the first (declining)
self.measureModifier.decliningStroke = SCISolidPenStyle(color: 0xFFDC2626, thickness: 2)
self.measureModifier.decliningFill = SCISolidBrushStyle(color: 0x29DC2626)

// Scale the measured price difference shown in the label (e.g. pips, points, %)
self.measureModifier.yValueScaleFactor = 100
self.measureModifier.snapToCandles = true
self.measureModifier.isEditable = true

// Callback triggered when the user completes placing both points (Start, End)
// Provides access to the fully created annotation object
self.measureModifier.annotationCreationCompletionListener = { [weak self] createdAnnotation, type in
    guard self != nil else { return }

    print("Measure annotation created: \(createdAnnotation), type: \(SCIAnnotationTypeName(type))")

    guard let measure = createdAnnotation as? SCIMeasureAnnotation else { return }
    let arrPoints = measure.getBaseDataValues()

    // Get data points
    // Index mapping: 0 = Start, 1 = End
    print("Measure point Start: \(arrPoints[0].x), \(arrPoints[0].y)")
    print("Measure point End: \(arrPoints[1].x), \(arrPoints[1].y)")
}

// Add the modifier to the chart surface
self.surface.chartModifiers.add(items: self.measureModifier)
</div>


The SCIMeasureCreationModifier can be configured using the properties listed in the table below:

| **Field**                              | **Description**                                                                        |
| -------------------------------------- | -------------------------------------------------------------------------------------- |
| `growingStroke`                        | Defines the `SCIPenStyle` used when the second point is above the first.               |
| `growingFill`                          | Defines the `SCIBrushStyle` used when the second point is above the first.             |
| `decliningStroke`                      | Defines the `SCIPenStyle` used when the second point is below the first.               |
| `decliningFill`                        | Defines the `SCIBrushStyle` used when the second point is below the first.             |
| `labelStyle`                           | Defines the `SCIFontStyle` used by the measurement label.                              |
| `yValueScaleFactor`                    | The factor applied to the price change to produce the scaled value shown in the label. |
| `snapToCandles`                        | Determines whether the anchor points snap to whole candles.                            |
| `annotationCreationCompletionListener` | A callback invoked when a full Measure annotation (both points) is completed.          |

## Best Practices
- Pair `growingStroke`/`growingFill` and `decliningStroke`/`decliningFill` with visually distinct colours (e.g. green vs red) so direction is communicated at a glance
- Enable `snapToCandles` when measuring on candlestick data so endpoints align with bar open/close positions
- Use `yValueScaleFactor` to convert raw price differences into pips, points, or percentage values relevant to the instrument
- Disable conflicting gesture modifiers during drawing for better UX
- Use the CompletionListener to validate or store annotations
- Call reset when switching tools or exiting drawing mode

## Notes
- The modifier automatically resets after completing the End point
- Direction (growing vs declining) is resolved by comparing the Y value of the End point to the Start point
- Only one annotation is created per interaction cycle
- Designed for measuring price and time distance between two points on financial charts