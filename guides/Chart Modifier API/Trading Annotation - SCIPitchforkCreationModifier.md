# SCIPitchforkCreationModifier
The `SCIPitchforkCreationModifier` is a custom gesture modifier used for interactive creation of **Pitchfork annotations** on a SciChart surface.
It enables users to define a pitchfork using a two-stage pan gesture workflow, producing a `SCIPitchforkAnnotation` with real-time visual feedback during construction.

## Overview
The modifier manages the full lifecycle of pitchfork creation:
- Two-step gesture interaction (A → B → C)
- Real-time rendering while dragging points
- Automatic annotation finalization after point C is set
- Immediate reset for continuous creation of multiple pitchforks

Once completed, the annotation is added to the chart and a completion callback is triggered.

## Interaction Behavior
The creation flow is split into two sequential pan gestures.

### State Machine

| State                                  | Description                                          |
| -------------------------------------- | ---------------------------------------------------- |
| `SCIPitchforkCreationStateIdle`        | Waiting for first touch input                        |
| `SCIPitchforkCreationStateDrawingAB`   | Point A is locked, user is dragging to define B      |
| `SCIPitchforkCreationStateWaitingForC` | A and B are locked, waiting for second gesture       |
| `SCIPitchforkCreationStateDrawingC`    | User is dragging to define C; pitchfork updates live |

### Gesture Flow (A → B → C)

#### 1. First Pan Gesture (A → B)

| Gesture        | Behavior                                        |
| -------------- | ----------------------------------------------- |
| **Touch Down** | Locks point **A** at touch location             |
| **Drag**       | Dynamically updates point **B**                 |
| **Touch Up**   | Finalizes A and B, transitions to waiting state |

#### 2. Second Pan Gesture (C)

| Gesture        | Behavior                                       |
| -------------- | ---------------------------------------------- |
| **Touch Down** | Begins defining point **C**                    |
| **Drag**       | Pitchfork updates in real time with C movement |
| **Touch Up**   | Finalizes annotation and completes creation    |

### Completion

- After point **C** is set, a `SCIPitchforkAnnotation` is created
- The `annotationCreationCompletionListener` callback is invoked
- The modifier automatically resets to `Idle` and is ready for the next pitchfork

## Retrieving Annotation Data

After completion, the resulting `SCIPitchforkAnnotation` contains the defining points of the structure.

You can extract its geometry using base data methods exposed by the annotation:

### getBaseDataValues()

- Returns pitchfork anchor points in **data space**
- Values correspond to chart axis coordinates
- Best for storage, analytics, and reconstruction

### getBasePoints()

- Returns pitchfork points in **pixel space**
- Values correspond to rendered screen coordinates
- Useful for UI overlays and screen-level calculations

## API Reference

| **Field**                              | **Description**                                                     |
| -------------------------------------- | ------------------------------------------------------------------- |
| `halfWidthZoneFill`                    | Fill colour for the central (middle) polygon section.               |
| `fullWidthZoneFill`                    | Fill colour for the two outer polygon sections.                     |
| `tineStroke`                           | The pen style used to draw the four tine lines.                     |
| `mainStroke`                           | The pen style used to draw the main pivot line.                     |
| `creationState`                        | Current state of the pitchfork creation lifecycle.                  |
| `reset()`                              | Cancels any in-progress gesture and returns the modifier to `Idle`. |
| `xAxisId`                              | ID of the X‑Axis the annotation is measured against.                |
| `yAxisId`                              | ID of the Y‑Axis the annotation is measured against.                |
| `tag`                                  | Custom tag identifier for the modifier.                             |
| `annotationCreationCompletionListener` | A callback invoked when a full pitchfork annotation is completed.   |

## Usage Example

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>

<div class="code-snippet" id="objectivec">

// Create pitchfork creation modifier
SCIPitchforkCreationModifier *modifier = [SCIPitchforkCreationModifier new];

//Styling
modifier.halfWidthZoneFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x401F9FFF];
modifier.fullWidthZoneFill = [[SCISolidBrushStyle alloc] initWithColorCode:0x40F0FA00];
modifier.tineStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF007064 thickness:2];
modifier.mainStroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFF007064 thickness:2];

// Handle completion
__weak typeof(self) weakSelf = self;
modifier.annotationCreationCompletionListener = ^(id<ISCIAnnotation> _Nonnull createdAnnotation, SCIAnnotationCreationType type) {
  __strong typeof(weakSelf) strongSelf = weakSelf;
  if (!strongSelf) return;
            
  NSLog(@"PITCHFORK annotation created: %@ type %@", createdAnnotation, SCIAnnotationTypeName(type));
            
  if (![createdAnnotation isKindOfClass:[SCIPitchforkAnnotation class]]) return;
  SCIPitchforkAnnotation *annotation = (SCIPitchforkAnnotation*) createdAnnotation;
            
  /// Get data points
  NSArray<SCIComparablePoint *> *arrPoints = [annotation getBaseDataValues];
            
  NSLog(@"Point A: %@", arrPoints[0]);
  NSLog(@"Point B: %@", arrPoints[1]);
  NSLog(@"Point C: %@", arrPoints[2]);
            
};

// Add to chart
[self.surface.chartModifiers add:modifier];

</div>

<div class="code-snippet" id="swift">

// Create pitchfork creation modifier
let modifier = SCIPitchforkCreationModifier()


modifier.halfWidthZoneFill = SCISolidBrushStyle(color: 0x401F9FFF)
modifier.fullWidthZoneFill  = SCISolidBrushStyle(color: 0x40F0FA00)
modifier.tineStroke = SCISolidPenStyle(color: 0xFF007064, thickness: 2)
modifier.mainStroke = SCISolidPenStyle(color: 0xFF007064, thickness: 2)
            
// Handle completion
modifier.annotationCreationCompletionListener = { [weak self] createdAnnotation, type in
    guard self != nil else { return }
                
    print("PITCHFORK annotation created: \(createdAnnotation), type: \(SCIAnnotationTypeName(type))")
                
    if let annotation = createdAnnotation as? SCIPitchforkAnnotation {
        // Get data points
        let points = annotation.getBaseDataValues()
        print("Point A: \(points[0])")
        print("Point B: \(points[1])")
        print("Point C: \(points[2])")
    }
}

// Add to chart
surface.chartModifiers.add(modifier)

</div>

## Best Practices

- Disable conflicting gesture modifiers during pitchfork creation for smoother interaction
- Use `annotationCreationCompletionListener` to persist or analyze completed annotations
- Call `reset()` when switching tools or exiting drawing mode
- Use distinct styling for pitchfork annotations to improve chart readability

## Notes

- Only one pitchfork is created per interaction cycle
- The modifier automatically resets after completion
- Designed for financial charting tools where trend structure visualization is required
