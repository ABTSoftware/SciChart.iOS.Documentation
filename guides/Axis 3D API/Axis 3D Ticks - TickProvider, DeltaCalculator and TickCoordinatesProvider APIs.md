# Axis 3D Ticks - TickProvider, DeltaCalculator and TickCoordinatesProvider APIs
**[Axes in SciChart 3D](Axis 3D APIs.html)** shares the same `SCIAxisCore` base class with **[SciChart 2D Axes](Axis APIs.html)**.
Many of the **AxisCore** features are shared. 
For your convenience, some of the documentation has been duplicated here, with some references to other sections of the user manual.

## Axis 3D Ticks, Labels and Grid Lines
In SciChart, the **Ticks** are small marks around the chart on an axis. There are **Minor** and **Major** Ticks, where Minor Ticks are placed in between Major ones. By default, Major Ticks are longer and thicker than Minor Ticks.

**Axis Labels** appears for every Major Tick, if there is enough space around.

**Grid Lines** correspond to **Ticks** on an axis. Likewise, there are Minor and Major Grid lines. In SciChart, **axes are responsible** not only for drawing **[Ticks](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-ticks)** and **[Labels](axis-styling---title-and-labels.html#axis-labels)**, but also for the **[Chart Grid](axis-styling---grid-lines-ticks-and-axis-bands.html#grid-lines)** as well as **[Axis Bands](axis-styling---grid-lines-ticks-and-axis-bands.html#axis-bands)**.

![Ticks and Deltas](img/axis-3d/major-minor-ticks.png)
<center><sub><sup>majorDelta = 2; minorDelta = 0.4; autoTicks = NO</sub></sup></center>

> **_NOTE:_** The **Axis Ticks** configuration is shared between SciChart 2D and SciChart 3D. For a full walk-through, including code-samples, please see the **[Axis Ticks - MajorDelta, MinorDelta and AutoTicks](axis-ticks---majordelta-minordelta-and-autoticks.html)** article.

## Axis 3D TickProvider and DeltaCalculator
The [TickProvider](axis-ticks---tickprovider-and-deltacalculator-api.html#creating-your-own-tickprovider) and [DeltaCalculator](axis-ticks---tickprovider-and-deltacalculator-api.html#creating-your-own-deltacalculator) APIs allows to further customize [Axis Ticks, Labels and GridLines](#axis-3d-ticks-labels-and-grid-lines) the [MajorDelta, MinorDelta], above and beyond of what you can achieve by setting `ISCIAxisCore.majorDelta` and `ISCIAxisCore.minorDelta`. That allows you to have a **totally custom** set of axis ticks (grid-lines, label intervals) on the chart. 

> **_NOTE:_** This is a shared API between SciChart 2D and SciChart 3D. For a full walk-through of the `ISCITickProvider` and `ISCIDeltaCalculator` APIs, including code-samples, please see the **[Axis Ticks - TickProvider and DeltaCalculator API](axis-ticks---tickprovider-and-deltacalculator-api.html)** article.

In addition to the above, you can also provide custom `ISCIAxisCore.tickCoordinatesProvider`, which is also described in SciChart 2D Documentation. Please refer to the - **[Axis Ticks - TickCoordinatesProvider API](axis-ticks---tickcoordinatesprovider-api.html)** - for more information on that topic.
