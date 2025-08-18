# Custom Annotation Grips

SciChart iOS allows you to define **custom grips** for editable annotations by overriding the resizing grip drawing and hit-test logic. This enables complete control over the appearance and behavior of grips (the draggable handles used to resize annotations).

This customization is applicable to any annotation that supports editing, such as `SCIBoxAnnotation`, `SCILineAnnotation`, `SCITextAnnotation`, and others.

## Overview

To implement custom annotation grips:

- Override `internalDrawResizingGrips(on:in:at:)` to draw your own grip visuals
- Use `resizingGrip.onDrawCustomGrip(...)` to render a grip image
- Override `getResizingGripHitIndex(at:andAnnotationCoordinates:)` to detect user interaction
- Set `isEditable = true` to enable editing
- Configure `dragDirections` to control grip movement

## Implementing Custom Grips

### Step 1: Subclassing the Annotation

Create a subclass of an editable annotation type and override its grip-related methods. In the example below, `SCIBoxAnnotation` is used.

> **_NOTE:_** Use `onDrawCustomGrip(at:imgGrip:isHorizontal:draw:)` to draw a grip image at the desired location and size.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">

</div>
<div class="code-snippet" id="swift">
class customGripBoxAnnotation: SCIBoxAnnotation {
    override func internalDrawResizingGrips(on context: CGContext, in rect: CGRect, at coordinates: SCIAnnotationCoordinates) {

        let center = (coordinates.pt1.y + coordinates.pt2.y) / 2
        drawCustomGrip(context: context, origin: CGPoint(x: coordinates.pt1.x, y: center))
    }

    private func drawCustomGrip(context: CGContext, origin: CGPoint) {
        guard let context = UIGraphicsGetCurrentContext() else { return }

        self.resizingGrip.onDrawCustomGrip(at: context, imgGrip: UIImage(named: "chart.modifier.zoomextents") ?? UIImage(), isHorizontal: true, draw: CGRect(x: origin.x - 10, y: origin.y - 10, width: 20, height: 20))
    }

}

</div>

### Step 2: Handling Grip Interaction

Implement the `getResizingGripHitIndex` method to detect when a user taps or drags a custom grip. Return a valid `SCIAnnotationPointIndex` to indicate which grip is being interacted with.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">

</div>
<div class="code-snippet" id="swift">
override func getResizingGripHitIndex(at hitPoint: CGPoint, andAnnotationCoordinates annotationCoordinates: SCIAnnotationCoordinates) -> SCIAnnotationPointIndex {
    let centerY = (annotationCoordinates.pt1.y + annotationCoordinates.pt2.y) / 2
    let gripFrame = CGRect(x: annotationCoordinates.pt1.x - 10, y: centerY - 10, width: 20, height: 20)

    let isHit = self.resizingGrip.customGripIsHit(at: hitPoint, andDrawnFrame: gripFrame)
    return isHit ? SCIAnnotationPointIndex(rawValue: 0) : SCIAnnotationPointIndex(rawValue: -1)

}

</div>

You can define multiple grip positions and return different index values for each.

## Usage Example

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">

</div>
<div class="code-snippet" id="swift">
let annotation = CustomGripBoxAnnotation()
annotation.isEditable = true
annotation.isSelected = true
annotation.dragDirections = .xDirection

annotation.set(x1: 70)
annotation.set(x2: 170)
annotation.set(y1: 36.4)
annotation.set(y2: 37.2)

surface.annotationSurface.annotations.add(annotation)

</div>

> **_NOTE:_** The annotation must be both `editable` grips to appear and respond.
