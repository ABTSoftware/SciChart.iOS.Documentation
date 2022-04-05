All 3D Chart types in SciChart iOS provided by the **RenderableSeries 3D API**.

The `ISCIRenderableSeries3D` is the visual representation of the `[X, Y, Z]` underlying data provided by the corresponding `ISCIDataSeries3D`.

All **RenderableSeries** are inherited from `SCIBaseRenderableSeries3D` and are added to the `SCIRenderableSeriesCollection` which is stored in `ISCIChartSurface3D.renderableSeries` property. This collection supports multiple **RenderableSeries** instances of different types. Each RenderableSeries is rendered to the screen, displaying the data from an associated `ISCIDataSeries3D`.

The list of **3D RenderableSeries** provided out of the box is available below:
- [The PointLine Series 3D](pointline-series-3d.html)
- [The Scatter Series 3D](scatter-series-3d.html)
- [The Bubble Series 3D](bubble-series-3d.html)
- [The Column Series 3D](column-series-3d.html)
- [The Impulse Series 3D](impulse-series-3d.html)
- [The Waterfall Series 3D](waterfall-series-3d.html)
- [The Free Surface Series 3D](free-surface-series-3d.html)
- [The Surface Mesh Series 3D](surface-mesh-series-3d.html)

Read on to learn more about the features, that all [RenderableSeries 3D have in common](#common-renderableseries-3d-features). For specific features of any **RenderableSeries** - please refer to a corresponding article on this series type.

## Common RenderableSeries 3D Features
As mentioned above - all 3D series types in SciChart iOS conforms to the `ISCIRenderableSeries3D` protocol. The list of some common features shared by all the series types can be found below:

| **Feature**                                  | **Description**                                                                            |
| -------------------------------------------- | ------------------------------------------------------------------------------------------ |
| `ISCIRenderableSeries3D.dataSeries`          | A DataSeries is the ***data-source*** for a RenderableSeries 3D. Please see the `ISCIDataSeries3D` API documentation for more info. |
| `ISCIRenderableSeriesCore.isVisible`         | Allows to hide or show a series.                                                           |
| `ISCIRenderableSeriesCore.isSelected`        | A series can be made Selected to be drawn on the top of other RenderableSeries.            |
| `ISCIRenderableSeries3D.pointMarker`         | This feature lets you set an ***optional marker*** on data points, e.g. Ellipse, Square, Cube, etc.. Its usage is described minutely in the [PointMarker API](pointmarker-3d-api.html) article. |
| `ISCIRenderableSeries3D.metadataProvider`    | The MetadataProvider API allows changing the color of a series on a ***per-point*** basis. Please see the [MetadataProvider 3D API](metadataprovider-3d-api.html) article for more information. |
| `ISCIRenderableSeries3D.seriesInfoProvider`  | Allows to customize the result of inspection of a series by [Chart Modifiers 3D](Chart Modifier 3D APIs.html). Also can be used to specify how modifiers tooltips have to appear for this series. Please refer to the [Cursors and Tooltips](interactivity---tooltip-modifier-3d.html#customizing-tooltip-modifier-3d-tooltips) section for more info. |
| `ISCIRenderableSeries3D.seriesColor`         | Allows to specify the **color** which will be used while drawing this series.              |
| `ISCIRenderableSeries3D.selectedVertexColor` | Allows to specify the **color** for **selected vertexes**. Please refer to the [Vertex Selection Modifier 3D](interactivity---vertex-selection-modifier-3d.html) article for more details. |
| `ISCIRenderableSeries3D.shininess`           | Defines how much the surface material is shining. Expect value in `[0.0f...1024.0]` range. |
| `ISCIRenderableSeries3D.specularStrength`    | Defines how bright and visible is the shining spot.                                        |
| `ISCIRenderableSeries3D.specularColor`       | Defines the material specular color.                                                       |
| `ISCIRenderableSeries3D.diffuseColor`        | Defines the material diffuse color.                                                        |

> **_NOTE:_** You might want to visit our API documentation for the above properties, which is available on the `ISCIRenderableSeries3D` page.

For more comprehensive walkthrough into any feature or specifics of any series type please refer to articles on that series type. The list of the **series types available** out of the box can be found at the beginning of this article.