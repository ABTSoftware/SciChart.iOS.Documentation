All 2D Chart types in SciChart iOS provided by the **RenderableSeries API**.

The `ISCIRenderableSeries` in SciChart are visual representations of **X,Y Numeric** or **Date** data. Some RenderableSeries render simple `X, Y` values (2D points in space), while some render additional information (such as `X, Y0, Y1` values, or `X, Y, Z` values).

All **RenderableSeries** are inherited from `SCIRenderableSeriesBase` and are added to the `SCIRenderableSeriesCollection` which is stored in `ISCIChartSurface.renderableSeries` property. This collection supports multiple **RenderableSeries** of different types. Each RenderableSeries is rendered to the screen, displaying the data from an associated `ISCIDataSeries`.

The list of **RenderableSeries** provided out of the box is available below:
- [The Line Series Type](2d-chart-types---line-series.html)
- [The Spline Line Series Type](2d-chart-types---spline-line-series.html)
- [The Scatter Series Type](2d-chart-types---scatter-series.html)
- [The Mountain (Area) Series Type](2d-chart-types---mountain-area-series.html)
- [The Spline Mountain (Area) Series Type](2d-chart-types---spline-mountain-area-series.html)
- [The Band Series Type](2d-chart-types---band-series.html)
- [The Spline Band Series Type](2d-chart-types---spline-band-series.html)
- [The Fan Chart Type](2d-chart-types---fan-chart.html)
- [The Bubble Series Type](2d-chart-types---bubble-series.html)
- [The Column Series Type](2d-chart-types---column-series.html)
- [The Impulse (Stem) Series Type](2d-chart-types---impulse-stem-series.html)
- [The Error Bars Series Type](2d-chart-types---error-bars-series.html)
- [The OHLC Series Type](2d-chart-types---ohlc-series.html)
- [The Candlestick Series Type](2d-chart-types---candlestick-series.html)
- [The Uniform Heatmap Series Type](2d-chart-types---uniform-heatmap-series.html)
- [The Stacked Column Series Type](2d-chart-types---stacked-column-series.html)
- [The Stacked Mountain Series Type](2d-chart-types---stacked-mountain-series.html)
- [The Pie Chart Type](2d-chart-types---pie-chart.html)
- [The Donut Chart Type](2d-chart-types---donut-chart.html)
- [The Custom RenderableSeries](2d-chart-types---custom-renderableseries-api.html)

Read on to learn more about the features, that all [RenderableSeries have in common](#common-renderableseries-features). For specific features of any RenderableSeries please refer to a corresponding article on this series type.

> **_NOTE:_** For more information about which DataSeries are used for which RenderableSeries, see the [DataSeries Types](dataseries-apis.html) page.

## Common RenderableSeries Features
As mentioned above - all 2D series types in SciChart iOS conforms to the `ISCIRenderableSeries` protocol. One of the main features of RenderableSeries API is its **extensibility**. New series types can be created via the [Custom RenderableSeries API](2d-chart-types---custom-renderableseries-api.html). The list of other features shared by all the series types can be found below:

| **ISCIRenderableSeries property**                             | **Description**                                                            |
| ------------------------------------------------------------- | -------------------------------------------------------------------------- |
| `ISCIRenderableSeries.dataSeries`                             | A DataSeries is the data-source for a RenderableSeries. Please see the [DataSeries API](dataseries-apis.html) section for a complete walkthrough of the DataSeries API. |
| `ISCIRenderableSeriesCore.isVisible`                          | Allows to hide or show a series.                                           |
| `ISCIRenderableSeriesCore.isSelected`                         | A series can be made Selected to be drawn on the top of other RenderableSeries. Also, the series can alter its appearance in response to changes in the selection state. Please refer to the [SeriesSelectionModifier](interactivity---sciseriesselectionmodifier.html) article for more details. |
| `ISCIRenderableSeries.pointMarker`                            | This feature lets you set an optional marker on data points, e.g. Ellipse, Square, Triangle or a custom shape. Its usage is described minutely in the [PointMarker API](pointmarker-api.html) article. |
| `ISCIRenderableSeries.resamplingMode`                         | Allows to choose a particular **ResamplingMode** for a series, which specifies a way to reduce the DataSeries for drawing. For more details, see the [Data Resampling](data-resampling.html) article. |
| `ISCIRenderableSeries.paletteProvider`                        | The PaletteProvider API allows changing the color of a series on a per-point basis. Please see the [PaletteProvider API](paletteprovider-api.html) article for more information. |
| `ISCIRenderableSeries.strokeStyle`                            | Allows to assign an `SCIPenStyle` object to determine how the series' outline should be drawn. Please refer to the [Styling and Theming](Styling and Theming.html) section for more details. |
| `ISCIRenderableSeries.seriesInfoProvider`                     | Allows to customize the result of inspection of a series by [Chart Modifiers](Chart Modifier APIs.html). Also can be used to specify how modifiers tooltips have to appear for this series. Please refer to the [Cursors and Tooltips](interactivity---tooltips-customization.html) section for more info. |
| `ISCIRenderableSeriesCore.selectedSeriesStyle`                | Allows to change the appearance of a series when its selection status changes. Please refer to the [SeriesSelectionModifier](interactivity---sciseriesselectionmodifier.html) article to learn more about this. |
| `ISCIRenderableSeries.xAxisId`                                | In case of multi axis, allows to attach a series to a specific **X Axis**. |
| `ISCIRenderableSeries.yAxisId`                                | In case of multi axis, allows to attach a series to a specific **Y Axis**. |
| **SCIRenderableSeriesBase property**                          |                                                                            |
| `SCIRenderableSeriesBase.zeroLineY`                           | Specifies the minimum Y value which is considered as an up value. Any part of a series which contains values that are above this value will be drawn upwards. Any part of the series with values below it will be drawn downwards. |
| `SCIRenderableSeriesBase.drawNaNAs`                           | Specifies how data points with **NaN** Y values are rendered within a given series type. In SciChart, **NaN values** is treated as a special value which designates an empty point. It is rendered as a gap within a series. For some chart types, though, the ends of the gap can be connected with a line. See the next paragraph for more details. |
| **IRenderableSeries Event Listeners**                         |                                                                            |
| `-[ISCIRenderableSeriesCore addIsVisibleChangeListener:]`     | Allows to attach a `SCIRenderableSeriesChangeListener` to a series which will be called when its **visibility** changes. This can occur when the `ISCIRenderableSeriesCore.isVisible` property changes. |
| `-[ISCIRenderableSeriesCore removeIsVisibleChangeListener:]`  | Allows to unsubscribe from the series **visibility** changes.              |
| `-[ISCIRenderableSeriesCore addIsSelectedChangeListener:]`    | Allows to attach a `SCIRenderableSeriesChangeListener` to a series which will be called when it becomes **selected/deselected**. This can occur when the `ISCIRenderableSeriesCore.isSelected` property changes. |
| `-[ISCIRenderableSeriesCore removeIsSelectedChangeListener:]` | Allows to unsubscribe from the series **selected/deselected** changes.     |

For more comprehensive walkthrough into any feature or specifics of any series type please refer to articles on that series type. The list of the ***series types available*** out of the box can be found at the beginning of this article.

## Adding a Gap onto a RenderableSeries
There is a special value reserved for this purpose in SciChart - `NaN`. If the Y value of a data point is equal to the `NaN` value, a series will render an empty space at this place, equal to the width of a single series bar:

![Gap in RenderableSeries](img/chart-types-2d/candlestick-series-gaps.png)

Some chart types allow to configure how gaps appear on a series via the `SCIRenderableSeriesBase.drawNaNAs` property, e.g.:
- [The Line Series Type](2d-chart-types---line-series.html)
- [The Spline Line Series Type](2d-chart-types---spline-line-series.html)
- [The Mountain (Area) Series Type](2d-chart-types---mountain-area-series.html)
- [The Spline Mountain (Area) Series Type](2d-chart-types---spline-mountain-area-series.html)
- [The Band Series Type](2d-chart-types---band-series.html)
- [The Spline Band Series Type](2d-chart-types---spline-band-series.html)

Please see the `SCILineDrawMode` enumeration for more info.
