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
- The `onCompleted` callback is invoked
- The modifier resets immediately, ready for a new annotation

![Xabcd Annotation](img/annotations/xabcd-annotation.png)

## Retrieving Annotation Points After Drawing Completion
After an annotation drawing is completed, you can retrieve its underlying points using:
getBaseDataValues()
getBasePoints()
Both methods return the annotation’s defining points, but in different coordinate spaces.

### getBaseDataValues()
- Returns the annotation points in data space (axis values).
- X and Y values correspond to the chart’s actual data coordinates
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
// Create a new XABCD creation modifier instance
SCIXabcdCreationModifier *modifier = [SCIXabcdCreationModifier new];

// Set the stroke (outline) style for the XABCD annotation lines
modifier.annotationStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFE97064 thickness:2];

// Set the fill style for the annotation 
modifier.annotationFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x55AAAA00];

// Callback triggered when the user completes placing all 5 points (X, A, B, C, D)
// Provides access to the fully created annotation object
modifier.onCompleted = ^(SCIXabcdAnnotation *annotation) {
    NSLog(@"XABCD annotation created: %@", annotation);
    NSArray<SCIComparablePoint*> *arrPoints = [annotation getBaseDataValues];

    NSLog(@"XABCD point X: %@, %@",[arrPoints[0].x toDate],arrPoints[0].y);
    NSLog(@"XABCD point A: %@, %@",arrPoints[1].x,arrPoints[1].y);
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
modifier.onCompleted = { annotation in
    print("XABCD annotation created: \(annotation)")
    let arrPoints = annotation.getBaseDataValues()
    print("XABCD point X: \(arrPoints[0].x.toDate), \(arrPoints[0].y)")
    print("XABCD point A: \(arrPoints[1].x), \(arrPoints[1].y)")
}

// Add the modifier to the chart surface 
surface.chartModifiers.add(modifier)
</div>


The SCIXabcdCreationModifier can be configured using the properties and method listed in the table below:

| **Field**                                   | **Description**                                                                  |
| ------------------------------------------- | -------------------------------------------------------------------------------- |
| `SCIXabcdCreationModifier.annotationFill`   | Defines the stroke style for newly created annotations.                          |
| `SCIXabcdCreationModifier.annotationFill`   | Defines the fill style for newly created annotations.                            |
| `SCIXabcdCreationModifier.isInSourceBounds` | Reports whether the event occurred within the **Source**.                        |
| `SCIXabcdCreationModifier.isDragging`       | Indicates whether the user is actively dragging a point.                         |
| `SCIXabcdCreationModifier.activePointIndex` | Represents the index of the point currently being placed.                        |
| `SCIXabcdCreationModifier.xAxisId`          | ID of the X‑Axis the annotation is measured against.                             |
| `SCIXabcdCreationModifier.yAxisId`          | ID of the Y‑Axis the annotation is measured against.                             |
| `SCIXabcdCreationModifier.tag`              | Custom tag identifier for the modifier.                                          |
| `SCIXabcdCreationModifier.onCompleted`      | A callback invoked on the main thread when a full XABCD annotation is completed. |

## Best Practices
- Disable conflicting gesture modifiers during drawing for better UX
- Use onCompleted to validate or store annotations
- Customize stroke/fill for better visual distinction
- Call cancel when switching tools or modes

## Notes
- The modifier automatically resets after completing point D
- Only one annotation is created per interaction cycle 
- Designed for real-time financial charting and harmonic pattern visualization
