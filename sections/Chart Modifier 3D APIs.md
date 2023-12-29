# Chart Modifier 3D APIs

Within the SciChart iOS SDK, **ChartModifiers** are the classes which can be added to a chart to give it a **certain behaviour**.
In SciChart 3D, for instance, all **zooming, panning operations, tooltips, legends** and even **selection** of points or lines are handled by `SCIChartModifier3DBase` derived classes in the SciChart codebase.

There are many different **3D Chart Modifiers** provided by SciChart and each one deserves an article by itself! 
This article is concerned with simply giving **an overview of the modifiers** and where you can find the examples in our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which demonstrate them.

There are also several individual articles on the **ChartModifier's** and how to configure them in the SciChart iOS.
Those could be grouped like the following:
- [Zoom and Pan Modifiers 3D](#zoom-and-pan-modifiers-3d)
- [Interactivity Modifiers 3D](#interactivity-modifiers-3d)
- [Miscellaneous Modifiers 3D](#miscellaneous-modifiers-3d)

#### Zoom and Pan Modifiers 3D
The following modifiers can be used if you want to add zooming or panning behavior to a chart:

| **Modifier Name**                                                        | **Description**                                                                                                                                                           |
| ------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [SCIZoomExtentsModifier3D](zoom-and-pan---zoom-extents-modifier-3d.html) | **Resets the zoom** to the data extents via the ***double-tapping***. Available almost everywhere, e.g. see the [Surface Mesh 3D Floor and Ceiling](https://www.scichart.com/example/ios-3d-chart-example-surface-mesh-floor-and-ceiling/) example. |
| [SCIPinchZoomModifier3D](zoom-and-pan---pinch-zoom-modifier-3d.html)     | **Zooms** a chart in and out via the ***pinch and spread gestures*** correspondingly. Available almost everywhere, e.g. see the [Logarithmic Axis 3D](https://www.scichart.com/example/ios-3d-chart-example-logarithmic-axis-3d/) example. |
| [SCIOrbitModifier3D](zoom-and-pan---orbit-modifier-3d.html)              | Performs the **orbital motion** of the camera performs giving the appearance of ***rotating*** the 3D world. Available almost everywhere, e,g. see the [Ellipsoid Free Surface Chart 3D](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-simple-ellipsoid/) example. |
| [SCIFreeLookModifier3D](zoom-and-pan---free-look-modifier-3d.html)      | Allows simple movement - Left, Right, Up or Down - of the chart camera imitating the ***free-look*** motion. Available almost everywhere, e.g. see the [Bubble Chart 3D](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-simple-bubble/) example. |

#### Interactivity Modifiers 3D
These modifiers allow to interact with chart series or inspect them:

| **Modifier Name**                                                                 | **Description**                                                                                                                                            |
| --------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [SCIVertexSelectionModifier3D](interactivity---vertex-selection-modifier-3d.html) | Provides **selection** of points via tapping on them. See the [Select Scatter Point 3D Chart](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-select-scatter-point/) example. |
| [SCITooltipModifier3D](interactivity---tooltip-modifier-3d.html)                  | Provides a **tooltip** for the nearest point on a series under the finger. See the [Series Tooltips 3D Chart](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-series-tooltips/) example. |

#### Miscellaneous Modifiers 3D
Modifiers below are used as helpers and can be a useful addition to a chart:

| **Modifier Name**                                  | **Description**                                                                                                                                            |
| -------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [SCIModifierGroup3D](#scimodifiergroup3d-features) | Can be used to **group** chart modifiers together. This can useful in **multi-chart** scenarios, to unite ModifierGroups into one **EventGroup** of modifiers. If an **Event** occurs on a chart, it will be propagated to modifiers from other charts that are in the same **EventGroup**. |
| `SCILegendModifier3D`                              | Allows to creates and configure a **Legend** for a 3D chart. See the [Add Remove Series](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-add-remove-series/) example. |

> **_NOTE:_** To learn more about **ChartModifiers 3D API**, please read the [Common ChartModifiers 3D Features](#common-chart-modifier-3d-features) section. 
> To find out more about a **specific** 3D ChartModifier, please refer to a corresponding article about this Modifier type.

## Adding a Chart Modifier 3D
All 3D **Chart Modifiers** are inherited from `SCIChartModifier3DBase`, conforms to the `ISCIChartModifier3D` and are added to the `SCIChartModifier3DCollection` which is stored in `ISCIChartSurface3D.chartModifiers` property. 
Please see the code below, to see how to add [SCIPinchZoomModifier3D](zoom-and-pan---pinch-zoom-modifier-3d.html) to your `SCIChartSurface3D`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface3D&gt; surface;

    // Create and add SCIPinchZoomModifier3D
    [self.surface.chartModifiers add:[SCIPinchZoomModifier3D new]];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface3D

    // Create and add SCIPinchZoomModifier3D
    self.surface.chartModifiers.add(SCIPinchZoomModifier3D())
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface3D surface;

    // Create and add SCIPinchZoomModifier3D
    Surface.ChartModifiers.Add(new SCIPinchZoomModifier3D());
</div>

## Common Chart Modifier 3D Features
As mentioned [above](#adding-a-chart-modifier-3d) - all the **3D ChartModifiers** provided by SciChart conforms to the `ISCIChartModifier3D` protocol and derive from the `SCIChartModifier3DBase` class. 
These provide a **powerful API** which gives the full access to internals of a ***chart***, ***axes*** and ***renderable series***. 
It is a must that Custom Modifier 3D implement `ISCIChartModifier3D`, and hence we recommend inheriting `SCIChartModifier3DBase` in such cases as well to get some base implementation for free.

Please see the list of common features below:

| **Feature**                                | **Description**                                                                                                               |
| ------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------- |
| `ISCIChartSurface3DProvider.parentSurface` | Provides the `ISCIChartSurface3D` which the modifier is attached to. See the `ISCIAttachable.isAttached` method below.        |
| `ISCIChartModifierCore.modifierSurface`    | Returns the **ModifierSurface** from the parental `SCIChartSurface3D`. It is used to place Views like tooltips, etc. onto it. |
| `ISCIAttachable.isAttached`                | Value which indicates whether a modifier is attached to a `SCIChartSurface3D` or not. If it is - `ISCIChartSurface3DProvider.parentSurface` property will return the corresponding instance of `SCIChartSurface3D`. |
| `ISCIChartModifierCore.isEnabled`          | Allows to specify if a modifier should be **available** for interaction **or not**.                                           |
| `ISCIReceiveEvents.receiveHandledEvents`   | Allows to specify whether a modifier should receive events handled by another modifier.                                       |

#### SCIModifierGroup3D Features
The `SCIModifierGroup3D` allows **grouping** of modifiers. This can be useful if modifiers create a logical group within which they are handled together. 
For example, all modifiers inside a `SCIModifierGroup3D` can be **enabled/disabled** together via the `ISCIChartModifierCore.isEnabled` property on the `SCIModifierGroup3D` itself.

Also, this is useful in **multi-chart** scenarios. Several `SCIModifierGroup3D`s can be united to share events between charts. 
This can be done by setting `SCIModifierGroup3D.eventGroup` to be the same for **ModifierGroups** which belong to different `SCIChartSurface3D`s.

| **Feature**                          | **Description**                                                                                                                               |
| ------------------------------------ | --------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCIModifierGroup3D.eventGroup`      | Allows to specify which **EventGroup** this modifier goes in. It is used to share events between modifiers that belong to different surfaces. |
| `ISCIReceiveEventGroup.eventsSource` | Returns the **ModifierSurface** which is the source of the events.                                                                            |
| `SCIModifierGroup3D.childModifiers`  | Assigns a collection of modifiers to a **ModifierGroup 3D**. Also a collection can be passed into the class constructor during creation.      |