Within the SciChart SDK, **ChartModifiers** are the classes which can be added to a chart to give it a **certain behaviour**. For instance, all **zooming, panning operations, tooltips, legends** and even **selection** of points or lines are handled by `SCIChartModifierBase` derived classes in the SciChart codebase.

There are many different **ChartModifiers** provided by SciChart and each one deserves an article by itself! 
This article is concerned with simply giving **an overview of the modifiers** and where you can find the examples in our [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) which demonstrate them.

There are also several individual articles on the **ChartModifier's** and how to configure them in the SciChart iOS.
Those could be grouped like the following:
- [Zoom and Pan Modifiers](#zoom-and-pan-modifiers)
- [Interactivity Modifiers](#interactivity-modifiers)
- [Miscellaneous Modifiers](#miscellaneous-modifiers)
- [Custom Modifiers](custom-modifiers---the-scichartmodifierbase-api.html)

#### Zoom and Pan Modifiers
The following modifiers can be used if you want to add scrolling or zooming behavior to a chart:

| **Modifier Name**                                                    | **Description**                                                                                                                                                           |
| -------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [SCIZoomExtentsModifier](zoom-and-pan---scizoomextentsmodifier.html) | **Resets the zoom** to the data extents via double-tapping. Available almost everywhere, e.g. see the [Sync Multi Chart](https://www.scichart.com/example/ios-chart/ios-chart-example-sync-mutiple-charts/) example. |
| [SCIPinchZoomModifier](zoom-and-pan---scipinchzoommodifier.html)     | **Zooms** a chart in and out via the pinch and spread gestures correspondingly. Available almost everywhere, e.g. see the [Multiple X-Axes](https://www.scichart.com/example/ios-multiple-axis-demo/) example. |
| [SCIZoomPanModifier](zoom-and-pan---scizoompanmodifier.html)         | **Pans** the chart in X, Y or both directions with inertia via finger sliding. Available almost everywhere, e,g. see the [Multiple X-Axes](https://www.scichart.com/example/ios-multiple-axis-demo/) example. |
| [SCIXAxisDragModifier](zoom-and-pan---scixaxisdragmodifier.html)     | **Scales** or **pans an X Axis** via finger drag. See [Drag Axis to Scale a Chart](https://www.scichart.com/example/ios-chart-chart-drag-axis-to-scale-example/) example. |
| [SCIYAxisDragModifier](zoom-and-pan---sciyaxisdragmodifier.html)     | **Scales** or **pans an Y Axis** via finger drag. See [Drag Axis to Scale a Chart](https://www.scichart.com/example/ios-chart-chart-drag-axis-to-scale-example/) example. |

#### Interactivity Modifiers
These modifiers allow to interact with chart series or inspect them:

| **Modifier Name**                                                             | **Description**                                                                                                                                            |
| ----------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [SCISeriesSelectionModifier](interactivity---sciseriesselectionmodifier.html) | Provides **selection** of a series via tapping on it. See the [Series Selection](https://www.scichart.com/example/ios-chart/ios-series-selection/) example.          |
| [SCITooltipModifier](interactivity---scitooltipmodifier.html)                 | Provides a **tooltip** for the nearest point on a series under the finger. See the [Using TooltipModifier](https://www.scichart.com/example/ios-chart/ios-using-tooltip-modifier/) example. |
| [SCIRolloverModifier](interactivity---scirollovermodifier.html)               | Provides a **vertical slice** cursor **with tooltips** and markers rolling over a series. See the [Using RolloverModifier](https://www.scichart.com/example/ios-chart-tooltips-using-rollovermodifier/) example. |
| [SCICursorModifier](interactivity---scicursormodifier.html)                   | Provides a **crosshairs** with a tooltip and axis labels. See [Using CursorModifier](https://www.scichart.com/example/ios-using-cursor-modifier/) example. |

#### Miscellaneous Modifiers  
Modifiers below are used as helpers and can be a useful addition to a chart:

| **Modifier Name**                              | **Description**                                                                                                                                            |
| ---------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [SCIModifierGroup](#scimodifiergroup-features) | Can be used to **group** chart modifiers together. This can useful in **multi-chart** scenarios, to unite ModifierGroups into one **EventGroup** of modifiers. If an **Event** occurs on a chart, it will be propagated to modifiers from other charts that are in the same **EventGroup**. See the [Multi-Panel Stock Chart](https://www.scichart.com/example/ios-multi-pane-stock-chart/) example. |
| [SCILegendModifier](legend-modifier.html)    | Allows to creates and configure a **Legend** for a chart. See the [Legend Chart](https://www.scichart.com/example/ios-chart/ios-chart-legends-api-example/) example. |
| [SCISeriesValueModifier](series-value-modifier.html)    | A custom ChartModifier which places an `SCISeriesValueMarkerAnnotation` on the YAxis for each RenderableSeries in the chart, showing the current `ISCIRenderableSeries` latest Y-value. E.g. for each series, place one axis-marker with the latest Y-value of the series. See the [SeriesValueModifier Chart](https://www.scichart.com/example/ios-chart/ios-chart-legends-api-example/) example. |

> **_NOTE:_** To learn more about **ChartModifiers API**, please read the [Common ChartModifiers Features](#common-chart-modifier-features) section. 
> To find out more about a **specific** ChartModifier, please refer to a corresponding article about this Modifier type.

## Adding a Chart Modifier
All 2D **Chart Modifiers** are inherited from `SCIChartModifierBase`, conforms to the `ISCIChartModifier` and are added to the `SCIChartModifierCollection` which is stored in `ISCIChartSurface.chartModifiers` property. 
Please see the code below, to see how to add [SCIPinchZoomModifier](zoom-and-pan---scipinchzoommodifier.html) to your `SCIChartSurface`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create and add SCIPinchZoomModifier
    [self.surface.chartModifiers add:[SCIPinchZoomModifier new]];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create and add SCIPinchZoomModifier
    self.surface.chartModifiers.add(SCIPinchZoomModifier())
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create and add SCIPinchZoomModifier
    Surface.ChartModifiers.Add(new SCIPinchZoomModifier());
</div>

## Common Chart Modifier Features
As mentioned [above](#adding-a-chart-modifier) - all the **ChartModifiers** provided by SciChart conforms to the `ISCIChartModifier` protocol and derive from the `SCIChartModifierBase` class. 
These provide a **powerful API** which gives the full access to internals of a chart, axes, series, annotations. 
It is a must that Custom Modifiers implement `ISCIChartModifier`, and hence we recommend inheriting `SCIChartModifierBase` in such cases as well to get some base implementation for free.

Please see the list of common features below:

| **Feature**                              | **Description**                                                                                                             |
| ---------------------------------------- | --------------------------------------------------------------------------------------------------------------------------- |
| `ISCIChartSurfaceProvider.parentSurface` | Provides the `ISCIChartSurface` which the modifier is attached to. See the `ISCIAttachable.isAttached` method below.        |
| `ISCIChartModifierCore.modifierSurface`  | Returns the **ModifierSurface** from the parental `SCIChartSurface`. It is used to place Views like tooltips, etc. onto it. |
| `ISCIAttachable.isAttached`              | Value which indicates whether a modifier is attached to a `SCIChartSurface` or not. If it is - `ISCIChartSurfaceProvider.parentSurface` property will return the corresponding instance of `SCIChartSurface`. |
| `ISCIChartModifierCore.isEnabled`        | Allows to specify if a modifier should be **available** for interaction **or not**.                                         |
| `ISCIReceiveEvents.receiveHandledEvents` | Allows to specify whether a modifier should receive events handled by another modifier.                                     |

#### SCIModifierGroup Features
The `SCIModifierGroup` allows **grouping** of modifiers. This can be useful if modifiers create a logical group within which they are handled together. 
For example, all modifiers inside a `SCIModifierGroup` can be **enabled/disabled** together via the `ISCIChartModifierCore.isEnabled` property on the `SCIModifierGroup` itself.

Also, this is useful in **multi-chart** scenarios. Several `SCIModifierGroup`s can be united to share events between charts. 
This can be done by setting `SCIModifierGroup.eventGroup` to be the same for **ModifierGroups** which belong to different `SCIChartSurface`s.

| **Feature**                          | **Description**                                                                                                                               |
| ------------------------------------ | --------------------------------------------------------------------------------------------------------------------------------------------- |
| `SCIModifierGroup.eventGroup`        | Allows to specify which **EventGroup** this modifier goes in. It is used to share events between modifiers that belong to different surfaces. |
| `ISCIReceiveEventGroup.eventsSource` | Returns the **ModifierSurface** which is the source of the events.                                                                            |
| `SCIModifierGroup.childModifiers`    | Assigns a collection of modifiers to a **ModifierGroup**. Also a collection can be passed into the class constructor during creation.         |