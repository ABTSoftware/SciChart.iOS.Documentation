# SciChart v4 Migration guide
SciChart v4.0 is the ***latest major release*** of SciChart - **High-Performance Real-time Charts** framework for iOS.

This guide is provided to ease the transition of existing applications using SciChart v3.0 to the latest APIs, as well as explain the design and structure of new and updated functionality.

## Table of contents
- [Benefits of Upgrading](scichart-v4-migration-guide.html#benefits-of-upgrading)
- [Breaking Changes](scichart-v4-migration-guide.html#breaking-changes)

---
## Benefits of Upgrading
- SciChart library starts to support **macOS**
- **All the API was improved**, became more flexible and more consistent with other platforms
- **A lot of bug fixes**
- Whole SciChart API became even more **Swift-friendly**

---
## Breaking Changes
In version 4.x iOS SciChart library has started to support macOS. API hasn't changed dramatically except **Pie/Donut** API which has been rewritten completely to match other platforms. Also, SciChart API has been slightly updated to improve flexibility, Swift compatibility, and become consistent with other platforms.
There's no possibility to document every single change, so we're going to attempt to identify the most commonly used parts of SciChart and describe what has been changed there.

#### Pie/Donut API
In version 4.x **Pie/Donut API** has been rewritten completely to match other platforms, for example:

- `SCIPieRenderableSeries` and `SCIDonutRenderableSeries` `segments` became `segmentsCollection`
- `SCIPieLegendModifier` became `SCIPieChartLegendModifier`
- `SCIPieSelectionModifier` became `SCIPieSegmentSelectionModifier`
- `SCIPieSelectionModifier.selectionMode` removed
- `SCIPieTooltipModifier` became `SCIPieChartTooltipModifier`

#### Custom theme applying
In version 4.x you can to add your custom themes from different bundles like this:
<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

    // SciChart v4.x
    [SCIThemeManager addThemeByThemeKey:@"customThemeId" fromBundle:[NSBundle bundleWithIdentifier:@"bundleIdentifier"]];
    [SCIThemeManager applyThemeToThemeable:self.surface withThemeKey:@"customThemeId"];
    
</div>
<div class="code-snippet" id="swift">

    // SciChart v4.x
    if let exampleBundle = Bundle(identifier: "bundleIdentifier") {
        SCIThemeManager.addTheme(byThemeKey: "customThemeId", from: exampleBundle)
        SCIThemeManager.applyTheme(to: self.surface, withThemeKey: "customThemeId")
    }

</div>

#### Minor API changes
- Some methods and properties have been changed to non-optional:
    - SciChart v3.x: `UIColor.fromARGBColorCode(0xffff4500)?.cgColor` 
    - SciChart v4.x: `UIColor.fromARGBColorCode(0xffff4500).cgColor`
- `SCIAnnotationDragListener` has been changed to `ISCIAnnotationDragListener
- `ISCIAxis` methods parameters have been changed from `double` to `CGFloat` like this:
    - SciChart v3.x: `- (void)zoomFrom:(double)fromCoord to:(double)toCoord`
    - SciChart v4.x: `- (void)zoomFrom:(CGFloat)fromCoord to:(CGFloat)toCoord`
- `SCIModifierBehavior+Protected.lastUpdatePoint` has been removed. Instead `SCIGestureModifierEventArgs.lastUpdateArgs` has been added. 
So, now you can use `lastUpdateArgs.location` to get a location at which the associated gesture occurred.
- `SCIModifierGroup.eventGroup` has been removed. Instead `ISCIChartModifierCore.eventsGroupTag` preperty has been added. 
So, in case you want to sync your modifier between charts you set `eventsGroupTag` to some string like this:

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // SciChart v3.x
    SCIModifierGroup *modifierGroup = [SCIModifierGroup new];
    modifierGroup.eventGroup = @"SharedEventGroup";
    modifierGroup.receiveHandledEvents = YES;
    [modifierGroup.childModifiers add:rolloverModifier];
    [surface.chartModifiers add:modifierGroup];
    
    // SciChart v4.x
    SCIRolloverModifier *rolloverModifier = [SCIRolloverModifier new];
    rolloverModifier.receiveHandledEvents = YES;
    rolloverModifier.eventsGroupTag = @"SharedEventGroup";
    [surface.chartModifiers add:rolloverModifier];
</div>
<div class="code-snippet" id="swift">
    // SciChart v3.x
    let rolloverModifier = SCIRolloverModifier()
    modifierGroup.eventGroup = "SharedEventGroup"
    modifierGroup.childModifiers.add(rolloverModifier)
    surface.chartModifiers.add(modifierGroup)
    
    // SciChart v4.x
    let rolloverModifier = SCIRolloverModifier()
    rolloverModifier.receiveHandledEvents = true
    rolloverModifier.eventsGroupTag = "SharedEventGroup"
    surface.chartModifiers.add(rolloverModifier)   
</div>

- `[SCIGestureModifierBase+Protected internalHandleGesture:]` method has been replaced with four separate methods which handle each gesture state separately like this:

<div class="code-snippet-tabs">
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
    <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">

    // SciChart v3.x
    - (void)internalHandleGesture:(UIGestureRecognizer *)gestureRecognizer {
        // some code here
    }
    
    // SciChart v4.x
    - (void)onGestureBeganWithArgs:(SCIGestureModifierEventArgs *)args {
        // some code here
    }

    - (void)onGestureChangedWithArgs:(SCIGestureModifierEventArgs *)args {
        // some code here
    }

    - (void)onGestureEndedWithArgs:(SCIGestureModifierEventArgs *)args {
        // some code here
    }

    - (void)onGestureCancelledWithArgs:(SCIGestureModifierEventArgs *)args {
        // some code here
    }
</div>
<div class="code-snippet" id="swift">
    
    // SciChart v3.x
    override func internalHandleGesture(_ gestureRecognizer: UIGestureRecognizer) {
        // some code here
    }
    
    // SciChart v4.x
    override func onGestureBegan(with args: SCIGestureModifierEventArgs) {
        // some code here
    }
    
    override func onGestureChanged(with args: SCIGestureModifierEventArgs) {
        // some code here
    }
    
    override func onGestureEnded(with args: SCIGestureModifierEventArgs) {
        // some code here
    }
    
    override func onGestureCancelled(with args: SCIGestureModifierEventArgs) {
        // some code here
    }
</div>

> **_NOTE:_** In case you want to handle all gesture recognizer states in one plase, you can override `- (void)onEvent:(SCIGestureModifierEventArgs *)args` method;
