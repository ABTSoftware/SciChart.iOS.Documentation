# SCIFibonacciRetracementCreationModifier
The `SCIFibonacciRetracementCreationModifier` is a custom gesture modifier used for interactive creation of **Fibonacci Retracement annotations** on a SciChart surface.

It enables users to place two key points (Start → End) using touch gestures, with real-time visual feedback during placement. Style properties set on the modifier (stroke, levels, regionColors, fillOpacity, connector line, and label options) are applied to each annotation as it is created.

## Overview
The modifier handles the full lifecycle of Fibonacci Retracement annotation creation:

- Sequential point placement via touch interaction
- Real-time dragging feedback for each point
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
- The `annotationCreationCompletionListener` callback is invoked
- The modifier resets immediately, ready for a new annotation

![Fibonacci Retracement Annotation](img/annotations/fibonacci-retracement-annotation.png)

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
// Create a new Fibonacci Retracement creation modifier instance
SCIFibonacciRetracementCreationModifier *modifier = [SCIFibonacciRetracementCreationModifier new];

// Set the stroke style for the level lines
modifier.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFFFFFFF thickness:2];

// Configure levels and region colours
modifier.levels = @[@0, @0.236, @0.382, @0.5, @0.618, @0.786, @1];
modifier.regionColors = @[
    [SCIColor fromARGBColorCode:0xFF0EA5E9],
    [SCIColor fromARGBColorCode:0xFF22C55E],
    [SCIColor fromARGBColorCode:0xFFFACC15],
    [SCIColor fromARGBColorCode:0xFFF97316],
    [SCIColor fromARGBColorCode:0xFFEF4444],
    [SCIColor fromARGBColorCode:0xFFA855F7]
];
modifier.fillOpacity = 0.2;
modifier.showConnectorLine = YES;
modifier.fibonacciLabelPlacement = SCIFibonacciLabelPlacement_Top;

// Callback triggered when the user completes placing both points (Start, End)
// Provides access to the fully created annotation object
__weak typeof(self) weakSelf = self;
modifier.annotationCreationCompletionListener = ^(id _Nonnull createdAnnotation, SCIAnnotationCreationType type) {
    __strong typeof(weakSelf) strongSelf = weakSelf;
    if (!strongSelf) return;

    NSLog(@"Fibonacci Retracement annotation created: %@ type %@", createdAnnotation, SCIAnnotationTypeName(type));

    if (![createdAnnotation isKindOfClass:[SCIFibonacciRetracementAnnotation class]]) return;

    SCIFibonacciRetracementAnnotation *fib = (SCIFibonacciRetracementAnnotation *)createdAnnotation;
    NSArray<SCIComparablePoint *> *points = [fib getBaseDataValues];

    // Get data points
    // Index mapping: 0 = Start, 1 = End
    NSLog(@"[Fibonacci] Start: date=%@  price=%@", points[0].x, points[0].y);
    NSLog(@"[Fibonacci] End: date=%@  price=%@", points[1].x, points[1].y);
};

// Add the modifier to the chart surface
[self.surface.chartModifiers add:modifier];


</div>
<div class="code-snippet" id="swift">
// Create a new Fibonacci Retracement creation modifier instance
self.fibonacciModifier = SCIFibonacciRetracementCreationModifier()

// Set the stroke style for the level lines
self.fibonacciModifier.stroke = SCISolidPenStyle(color: 0xFFFFFFFF, thickness: 2)

// Configure levels and region colours
self.fibonacciModifier.levels = [0, 0.236, 0.382, 0.5, 0.618, 0.786, 1].map { NSNumber(value: $0) }
self.fibonacciModifier.regionColors = [0xFF0EA5E9, 0xFF22C55E, 0xFFFACC15,
                           0xFFF97316, 0xFFEF4444, 0xFFA855F7].map { SCIColor.fromARGBColorCode($0) }
self.fibonacciModifier.fillOpacity = 0.2
self.fibonacciModifier.showConnectorLine = true
self.fibonacciModifier.fibonacciLabelPlacement = .top
self.fibonacciModifier.isEditable = true

// Callback triggered when the user completes placing both points (Start, End)
// Provides access to the fully created annotation object
self.fibonacciModifier.annotationCreationCompletionListener = { [weak self] createdAnnotation, type in
    guard self != nil else { return }

    print("Fibonacci Retracement annotation created: \(createdAnnotation), type: \(SCIAnnotationTypeName(type))")

    guard let fib = createdAnnotation as? SCIFibonacciRetracementAnnotation else { return }
    let arrPoints = fib.getBaseDataValues()

    // Get data points
    // Index mapping: 0 = Start, 1 = End
    print("Fibonacci point Start: \(arrPoints[0].x), \(arrPoints[0].y)")
    print("Fibonacci point End: \(arrPoints[1].x), \(arrPoints[1].y)")
}

// Add the modifier to the chart surface
self.surface.chartModifiers.add(items: self.fibonacciModifier)
</div>


The SCIFibonacciRetracementCreationModifier can be configured using the properties listed in the table below:

| **Field**                              | **Description**                                                                                                                                  |
| -------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| `stroke`                               | Defines the `SCIPenStyle` used to draw the level lines. Supplies thickness and dash pattern; the colour of each level comes from `regionColors`. |
| `levels`                               | Defines the Fibonacci ratios at which levels are drawn.                                                                                          |
| `regionColors`                         | Defines the colours used to fill the regions between levels, treated as a ramp.                                                                  |
| `fillOpacity`                          | Defines the opacity applied to the filled regions, in the range 0.0 - 1.0.                                                                       |
| `showConnectorLine`                    | Determines whether a connector line is drawn between the two base points.                                                                        |
| `connectorLineStroke`                  | Defines the `SCIPenStyle` used to draw the connector line between the two base points.                                                           |
| `fibonacciLabelPlacement`              | Determines where the level labels are placed relative to their level line: `Left`, `Top`, or `Inside`.                                           |
| `fibonacciLabelColorMode`              | Determines how the level labels are coloured: `MultiColor` (matches each level line) or `SingleColor` (uses `labelStyle`'s text colour).         |
| `labelStyle`                           | Defines the `SCIFontStyle` used by the level labels.                                                                                             |
| `annotationCreationCompletionListener` | A callback invoked when a full Fibonacci Retracement annotation (both points) is completed.                                                      |

## Best Practices
- Disable conflicting gesture modifiers during drawing for better UX
- Use the CompletionListener to validate or store annotations
- Keep `levels` and `regionColors` in sync so each region gets an intended colour
- Customize `stroke`, `fillOpacity`, and `regionColors` for better visual distinction
- Call reset when switching tools or exiting drawing mode

## Notes
- The modifier automatically resets after completing the End point
- Only one annotation is created per interaction cycle
- Designed for trend-based Fibonacci retracement analysis on financial charts