# SCIExtendedLineCreationModifier
The `SCIExtendedLineCreationModifier` is a custom gesture modifier used for interactive creation of Extended Line annotations on a SciChart surface. It enables users to define a line with two anchor points using a pan gesture workflow, with options to extend the line forward, backward, or both directions beyond its anchors.

## Overview
The modifier manages the full lifecycle of extended line creation:

- Two-step gesture interaction (A → B)
- Real-time rendering while dragging points
- Automatic annotation finalization after point B is set
- Immediate reset for continuous creation of multiple extended lines
- Configurable line extension behavior (both directions, forward, backward, or none)

Once completed, the annotation is added to the chart and a completion callback is triggered.

## Interaction Behavior
The creation flow is split into two sequential pan gestures.

### Gesture Flow (A → B)

| Gesture        | Behavior                                        |
| -------------- | ----------------------------------------------- |
| **Touch Down** | Locks point **A** at touch location             |
| **Drag**       | Dynamically updates point **B**                 |
| **Touch Up**   | Finalizes A and B, transitions to waiting state |

### Completion

- After point **B** is set, a `SCIExtendedLineAnnotation` is created
- The `annotationCreationCompletionListener` callback is invoked
- The modifier automatically resets to Idle and is ready for the next line

## Retrieving Annotation Data

After completion, the resulting `SCIExtendedLineAnnotation` provides direct access to its defining anchor points in data space.

### Point Accessors
getX1() / getY1() and getX2() / getY2()  
Returns the first anchor point (A) and the second anchor point (B) in data coordinates.
Values correspond to the chart’s X‑Axis and Y‑Axis units.

## API Reference

| **Field**                              | **Description**                                                     |
| -------------------------------------- | ------------------------------------------------------------------- |
| `stroke`                               | Pen style used to draw the extended line.                           |
| `extendStart`                          | Boolean flag controlling backward extension of the line.            |
| `extendEnd`                            | Boolean flag controlling forward extension of the line.             |
| `reset()`                              | Cancels any in‑progress gesture and returns the modifier to Idle.   |
| `xAxisId`                              | ID of the X‑Axis the annotation is draw against.                    |
| `yAxisId`.                             | ID of the Y‑Axis the annotation is measured against.                |
| `tag`                                  | Custom tag identifier for the modifier.                             |
| `annotationCreationCompletionListener` | Callback invoked when a full extended line annotation is completed. |


## Usage Example

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>

<div class="code-snippet" id="objectivec">

// Create extended line creation modifier
SCIExtendedLineCreationModifier *modifier = [SCIExtendedLineCreationModifier new];

// Configure extension behavior
modifier.extendStart = YES;
modifier.extendEnd = YES; 

// Set the stroke style
modifier.stroke = [[SCISolidPenStyle alloc] initWithColorCode:0xFFE97064 thickness:2];

// Handle completion
modifier.annotationCreationCompletionListener = ^(id<ISCIAnnotation> _Nonnull createdAnnotation, SCIAnnotationCreationType type) {         
  __strong typeof(weakSelf) strongSelf = weakSelf;
  if (!strongSelf) return;
            
  NSLog(@"ExtendedLine annotation created: %@ type %@", createdAnnotation, SCIAnnotationTypeName(type));
     
  if (![createdAnnotation isKindOfClass:[SCIExtendedLineAnnotation class]]) return;
  SCIExtendedLineAnnotation *annotation = (SCIExtendedLineAnnotation *)createdAnnotation;
            
  double x1Point = annotation.x1.toDouble;
  double x2Point = annotation.x2.toDouble;
  double y1Point = annotation.y1.toDouble;
  double y2Point = annotation.y1.toDouble;
  NSLog(@"Point A: %f, %f", x1Point, y1Point);
  NSLog(@"Point B: %f, %f", x2Point, y2Point);
};
        
// Add to chart
[self.surface.chartModifiers add:modifier];

</div>

<div class="code-snippet" id="swift">

// Create extended line creation modifier
let modifier = SCIExtendedLineCreationModifier()
            
// Configure extension behavior
modifier.extendStart = true
modifier.extendEnd = true
            
// Set the stroke style
modifier.stroke = SCISolidPenStyle(color: 0xFFE97064, thickness: 2)
                        
// Handle completion
modifier.annotationCreationCompletionListener  = { [weak self] createdAnnotation, type in
  guard self != nil else { return }
                
  print("Annotation created: \(createdAnnotation), type: \(SCIAnnotationTypeName(type))")
                
  guard let annotation = createdAnnotation as? SCIExtendedLineAnnotation else { return }
  let x1Point: Double = annotation.getX1()
  let y1Point: Double = annotation.getY1()
  let x2Point: Double = annotation.getX2()
  let y2Point: Double = annotation.getY2()
                
  print("point =\(x1Point)  \(x2Point)")
  print("point =\(y1Point)  \(y2Point)")
}

</div>

## Best Practices

- Disable conflicting gesture modifiers during extended line creation for smoother interaction
- Use `annotationCreationCompletionListener` to persist or analyze completed annotations
- Call `reset()` when switching tools or exiting drawing mode
- Configure extendStart and extendEnd to match the desired line extension behavior