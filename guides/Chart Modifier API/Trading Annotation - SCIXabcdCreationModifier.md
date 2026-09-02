# SCIXabcdCreationModifier
The `SCIXabcdCreationModifier` is a custom gesture modifier used for interactive creation of **XABCD annotations** on a SciChart surface.

It enables users to place five key points (X → A → B → C → D) using touch gestures, with real-time visual feedback during placement.

## Overview
The modifier handles the full lifecycle of XABCD annotation creation:

- Sequential point placement via touch interaction
- Real-time dragging feedback for each point
- Automatic completion after the final point (D)
- Immediate reset for continuous drawing

Once completed, the annotation remains on the chart and a callback is triggered.

## Interaction Behavior

The creation flow follows a structured sequence:

### Per Point Interaction (X → A → B → C → D)

| Gesture        | Behavior                                       |
| -------------- | ---------------------------------------------- |
| **Touch Down** | Places the current point at the touch location |
| **Drag**       | Moves the point dynamically in real-time       |
| **Touch Up**   | Locks the point and advances to the next one   |

### Completion

- After placing point **D**, the annotation is finalized
- The `SCIAnnotationCreationModifierBase.annotationCreationCompletionListener` callback is invoked
- The modifier resets immediately, ready for a new annotation

![Xabcd Annotation](img/annotations/xabcd-annotation.png)

## Retrieving Annotation Points After Drawing Completion
After an annotation drawing is completed, you can retrieve its underlying points using:
`-[ISCITradingAnnotation getBaseDataValues]`
`-[ISCITradingAnnotation getBasePoints]`
Both methods return the annotation’s defining points, but in different coordinate spaces.

`-[ISCITradingAnnotation getBaseDataValues]`

- Returns the annotation points in data space (axis values).
- X and Y values correspond to the chart’s actual data coordinates
- Independent of pixel resolution or screen scaling
- Useful for storing, analysis, or reloading annotations

`-[ISCITradingAnnotation getBasePoints]`

- Returns annotation points in pixel space (rendered screen coordinates).
- X and Y values correspond to screen pixels
- Useful for rendering overlays or UI alignment

## Usage Example
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
// Create a new XABCD creation modifier instance
SCIXabcdCreationModifier *modifier = [SCIXabcdCreationModifier new];

// Set the stroke (outline) style for the XABCD annotation lines
modifier.annotationStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFE97064 thickness:2];

// Set the fill style for the annotation 
modifier.annotationFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x55AAAA00];

// Callback triggered when the user completes placing all 5 points (X, A, B, C, D)
// Provides access to the fully created annotation object
__weak typeof(self) weakSelf = self;
modifier.annotationCreationCompletionListener = ^(id<ISCIAnnotation> _Nonnull         createdAnnotation, SCIAnnotationCreationType type) {
    __strong typeof(weakSelf) strongSelf = weakSelf;
    if (!strongSelf) return;
            
    NSLog(@"XABCD annotation created: %@ type %@", createdAnnotation, SCIAnnotationTypeName(type));
            
    if (![createdAnnotation isKindOfClass:[SCIXabcdAnnotation class]]) return;
            
    SCIXabcdAnnotation *xabcd = (SCIXabcdAnnotation *)createdAnnotation;
    NSArray<SCIComparablePoint *> *points = [xabcd getBaseDataValues];

    // Get data points
    // Index mapping: 0 = X, 1 = A, 2 = B, 3 = C, 4 = D
    NSLog(@"[XABCD] X: date=%@  price=%@", points[0].x, points[0].y);
    NSLog(@"[XABCD] A: date=%@  price=%@", points[1].x, points[1].y);
};

// Add the modifier to the chart surface
[self.surface.chartModifiers add:modifier];
</div>
<div class="code-snippet" id="swift">
// Create a new XABCD creation modifier instance
let modifier = SCIXabcdCreationModifier()

// Set the stroke (outline) style for the XABCD annotation lines
modifier.annotationStroke = SCISolidPenStyle(color: 0xFFE97064, thickness: 2)

// Set the fill style for the annotation
modifier.annotationFill = SCISolidBrushStyle(color: 0x55AAAA00)

// Callback triggered when all points (X, A, B, C, D) are placed
// Gives access to the completed annotation object
modifier.annotationCreationCompletionListener  = { [weak self] createdAnnotation, type in
    guard self != nil else { return }
                
    print("Annotation created: \(createdAnnotation), type: \(SCIAnnotationTypeName(type))")
                
    guard let xabcd = createdAnnotation as? SCIXabcdAnnotation else { return }
    let arrPoints = xabcd.getBaseDataValues()
                
    // Get data points
    // Index mapping: 0 = X, 1 = A, 2 = B, 3 = C, 4 = D
    print("XABCD point X: \(arrPoints[0].x), \(arrPoints[0].y)")
    print("XABCD point A: \(arrPoints[1].x), \(arrPoints[1].y)")
}

// Add the modifier to the chart surface 
surface.chartModifiers.add(modifier)
</div>


The SCIXabcdCreationModifier can be configured using the properties and method listed in the table below:

| **Field**                                   | **Description**                                           |
| ------------------------------------------- | --------------------------------------------------------- |
| `SCIXabcdCreationModifier.annotationStroke` | Defines the stroke style for newly created annotations.   |
| `SCIXabcdCreationModifier.annotationFill`   | Defines the fill style for newly created annotations.     |
| `SCIXabcdCreationModifier.isDragging`       | Indicates whether the user is actively dragging a point.  |
| `SCIXabcdCreationModifier.activePointIndex` | Represents the index of the point currently being placed. |
| `-[SCIXabcdCreationModifier reset]`         | Cancels and removes any in-progress annotation.           |

> **_NOTE:_** The **xAxisId** and **yAxisId** must be supplied if you have axis with **non-default** Axis Ids, e.g. in **multi-axis** scenario.

## Best Practices
- Disable conflicting gesture modifiers during drawing for better UX
- Use CompletionListener to validate or store annotations
- Call `-[SCIXabcdCreationModifier reset]` when switching tools or exiting drawing mode
- Customize stroke/fill for better visual distinction
- Call cancel when switching tools or modes

## Notes
- The modifier automatically resets after completing point D
- Only one annotation is created per interaction cycle 
- Designed for real-time financial charting and harmonic pattern visualization
