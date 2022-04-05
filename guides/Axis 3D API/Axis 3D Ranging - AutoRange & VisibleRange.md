# Axis 3D Ranging - AutoRange & VisibleRange
**[Axes in SciChart 3D](Axis 3D APIs.html)** shares the same `SCIAxisCore` base class with **[SciChart 2D Axes](Axis APIs.html)**.
Many of the **AxisCore** features are shared. 
For your convenience, some of the documentation has been duplicated here, with some referring to other sections of the user manual.

## Axis 3D AutoRange
`SCIAxisBase3D` derived Types also have **AutoRanging** behaviour as per the 2D axis types. 
The `ISCIAxisCore.autoRange` property defines how the axis will auto-range when data is changed.

There are 3 possible auto-range modes, which are listed below:
- `SCIAutoRange.SCIAutoRange_Once` - axis will attempt to fit the data only **when you start the chart**.
- `SCIAutoRange.SCIAutoRange_Always` - axis will attempt to fit the data **every time** the chart is drawn .
- `SCIAutoRange.SCIAutoRange_Never` - axis will **never** auto-range.

In a 3D Axis, AutoRanging means, ***given a fixed size of Axis in 3D world coordinates, change the VisibleRange Max/Min to fit the data***. 
Dynamically positioning the [Camera](scichart-3d-basics---camera-3d-api.html) to view all of the 3D Chart is known as ***Zoom to Fit*** and is performed by `-[ISCICameraController zoomToFit]`.

> **_NOTE:_** The **AutoRanging** are shared between SciChart 2D and SciChart 3D. For a full walk-through, including code-samples, please see the [Axis Ranging - AutoRange](axis-ranging---autorange.html) article.

## Axis 3D VisibleRange
Every 3D Axis type can work with a specific range type that conforms to the `ISCIRange` protocol. It depends on the data type that the axis can work with. Please review the article on [3D Axis Types](Axis 3D APIs.html) to learn more.

The `ISCIAxisCore.visibleRange` is an actual axis range, measured in chart units. This is a part of a chart that is currently visible in a viewport.
This property allows you to **set or get** the `visibleRange` on the axis.

> **_NOTE:_** The **VisibleRange APIs** are shared between SciChart 2D and SciChart 3D. For a full walk-through of VisibleRange API, including code-samples, please see the following articles:
> 
> - [Axis Ranging - VisibleRange and DataRange](axis-ranging---visiblerange-and-datarange.html)
> - [Axis Ranging - Restricting VisibleRange](axis-ranging---restricting-visiblerange.html)
