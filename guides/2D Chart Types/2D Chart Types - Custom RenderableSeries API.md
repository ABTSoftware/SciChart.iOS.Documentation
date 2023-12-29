# Custom RenderableSeries API
If the **built-in** chart types in SciChart are not enough, you can create your own **RenderableSeries**. Custom RenderableSeries should extend `SCIRenderableSeriesBase` if you want to provide some **custom data**, or one of predefined base classes if you want to display data from **one of default** `ISCIDataSeries` implementations.

| **Base class for Custom RenderableSeries** | **When to use**                                                                      |
| ------------------------------------------ | ------------------------------------------------------------------------------------ |
| `SCIXyRenderableSeriesBase`                | If you want to use `SCIXyDataSeries` as data source for custom RenderableSeries      |
| `SCIXyyRenderableSeriesBase`               | If you want to use `SCIXyyDataSeries` as data source for custom RenderableSeries     |
| `SCIHlRenderableSeriesBase`                | If you want to use `SCIHlDataSeries` as data source for custom RenderableSeries      |
| `SCIOhlcRenderableSeriesBase`              | If you want to use `SCIOhlcDataSeries` as data source for custom RenderableSeries    |
| `SCIXyzRenderableSeriesBase`               | If you want to use `SCIXyzDataSeries` as data source for custom RenderableSeries     |
| `SCIRenderableSeriesBase`                  | If default `ISCIDataSeries` implementations aren't suitable for data which should be displayed and you want to create **custom DataSeries** type |

## Creating your Own Series
For example, let's try to create a **RenderableSeries**, which draws [PointMarker](pointmarker-api.html) at specified `[x, y]` coordinates. Since, `SCIXyDataSeries` is enough to define [x, y] data we will extend `SCIXyRenderableSeriesBase`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    #import &lt;SciChart/SCIRenderableSeriesBase+Protected.h&gt;

    @interface CustomRenderableSeries : SCIXyRenderableSeriesBase
    @end

    @implementation CustomRenderableSeries

    - (instancetype)init {
        // In default constructor we will use:
        // - SCIXyRenderPassData which will store points to draw ( If you need to store some additional data for drawing you can extend it and add additional fields )
        // - SCIPointMarkerHitProvider which performs hit checks on points rendered by series. In our case we just check if point marker is hit
        // - SCINearestXyPointProvider allows to locate nearest (x, y) point to specified point on screen
        return [super initWithRenderPassData:[SCIXyRenderPassData new] hitProvider:[SCIPointMarkerHitProvider new] nearestPointProvider:[SCINearestXyPointProvider new]];
    }

    - (void)internalDrawWithContext:(id&lt;ISCIRenderContext2D&gt;)renderContext assetManager:(id&lt;ISCIAssetManager2D&gt;)assetManager renderPassData:(id&lt;ISCISeriesRenderPassData&gt;)renderPassData {
        // here we cast render pass data to type of render pass data which we created in constructor
        SCIXyRenderPassData *renderPassDataToDraw = (SCIXyRenderPassData *)renderPassData;

        // get the x, y coordinates provided by the SCIXyRenderPassData
        SCIFloatValues *xCoords = renderPassDataToDraw.xCoords;
        SCIFloatValues *yCoords = renderPassDataToDraw.yCoords;
        
        // here we can draw something using ISCIRenderContext2D
        // as an example, we will use helper method from base class to draw data as point markers
        [super drawPointMarkersWithContext:renderContext assetManager:assetManager xCoords:xCoords yCoords:yCoords];
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class CustomRenderableSeries: SCIXyRenderableSeriesBase {

        // In default constructor we will use:
        // - SCIXyRenderPassData which will store points to draw ( If you need to store some additional data for drawing you can extend it and add additional fields )
        // - SCIPointMarkerHitProvider which performs hit checks on points rendered by series. In our case we just check if point marker is hit
        // - SCINearestXyPointProvider allows to locate nearest (x, y) point to specified point on screen
        override init() {
            super.init(renderPassData: SCIXyRenderPassData(), hitProvider: SCIPointMarkerHitProvider(), nearestPointProvider: SCINearestXyPointProvider())
        }
        
        override func internalDraw(with renderContext: ISCIRenderContext2D!, assetManager: ISCIAssetManager2D!, renderPassData: ISCISeriesRenderPassData!) {
            // here we cast render pass data to type of render pass data which we created in constructor
            let renderPassDataToDraw = renderPassData as! SCIXyRenderPassData

            // get the x, y coordinates provided by the SCIXyRenderPassData
            let xCoords = renderPassDataToDraw.xCoords
            let yCoords = renderPassDataToDraw.yCoords
            
            // here we can draw something using ISCIRenderContext2D
            // as an example, we will use helper method from base class to draw data as point markers
            super.drawPointMarkers(with: renderContext, assetManager: assetManager, xCoords: xCoords, yCoords: yCoords)
        }
    }
</div>
<div class="code-snippet" id="cs">
    class CustomRenderableSeries : SCIXyRenderableSeriesBase
    {
        // In default constructor we will use:
        // - SCIXyRenderPassData which will store points to draw ( If you need to store some additional data for drawing you can extend it and add additional fields )
        // - SCIPointMarkerHitProvider which performs hit checks on points rendered by series. In our case we just check if point marker is hit
        // - SCINearestXyPointProvider allows to locate nearest (x, y) point to specified point on screen
        public CustomRenderableSeries()
            : base(new SCIXyRenderPassData(), new SCIPointMarkerHitProvider(), new SCINearestXyPointProvider())
        {
        }

        protected override void InternalDraw(IISCIRenderContext2D renderContext, IISCIAssetManager2D assetManager, IISCISeriesRenderPassData renderPassData)
        {
            // here we cast render pass data to type of render pass data which we created in constructor
            var renderPassDataToDraw = renderPassData as SCIXyRenderPassData;


            // get the x, y coordinates provided by the SCIXyRenderPassData
            var xCoords = renderPassDataToDraw.XCoords;
            var yCoords = renderPassDataToDraw.YCoords;

            // here we can draw something using ISCIRenderContext2D
            // as an example, we will use helper method from base class to draw data as point markers

            base.DrawPointMarkers(renderContext, assetManager, xCoords, yCoords);
        }
    }
</div>

As showed in the code above, the main method, which has to be implemented is the `internalDraw`, which allows you to perform any **custom drawings** you want. The `ISCIRenderContext2D` and `ISCIAssetManager2D` are passed into it, which should be used to draw to the screen and which are the parts of the graphics context for this render pass.

## The current Data to Draw - ISCISeriesRenderPassData protocol
The data to draw is contained in the `ISCISeriesRenderPassData` passed in to the `internalDraw` method. Using the **RenderPassData** object you can access the **data values** and **coordinates** to draw, the **xPointRange** (the indices of the data to draw, inclusive), the **X and Y Coordinate Calculators**, that transforms data to pixel coordinates. All the above accessed through the following properties:
- `SCIXyRenderPassData.yValues` (other **RenderPassData** types might have other properties)
- `SCIXyRenderPassData.yCoords` (other **RenderPassData** types might have other properties)
- `SCISeriesRenderPassData.xPointRange`
- `ISCISeriesRenderPassData.xCoordinateCalculator`
- `ISCISeriesRenderPassData.yCoordinateCalculator`

Depending on **DataSeries** type you can have a different `ISCISeriesRenderPassData` type and different ways to access the data to draw.

| **Series RenderPassData Type**    | **DataSeries type used**                                                                    |
| --------------------------------- | ------------------------------------------------------------------------------------------- |
| `SCIXyRenderPassData`             | If you want to use `SCIXyDataSeries` as data source for custom RenderableSeries             |
| `SCIXyyRenderPassData`            | If you want to use `SCIXyyDataSeries` as data source for custom RenderableSeries            |
| `SCIXyzRenderPassData`            | If you want to use `SCIXyzDataSeries` as data source for custom RenderableSeries            |
| `SCIOhlcRenderPassData`           | If you want to use `SCIOhlcDataSeries` as data source for custom RenderableSeries           |
| `SCIHlRenderPassData`             | If you want to use `SCIHlDataSeries` as data source for custom RenderableSeries             |
| `SCIUniformHeatmapRenderPassData` | If you want to use `SCIUniformHeatmapDataSeries` as data source for custom RenderableSeries |

The types above could be extended to add some additional information which is required for rendering, e.g. `SCIColumnRenderPassData` extends `SCIXyRenderPassData` and adds fields for caching of **column width** in pixels and coordinate of **zero line**.

## Example: RoundedColumnRenderableSeries
We have an example which shows how to create a rounded column series with this **powerful API**.

![Rounded Column Chart Example](img/chart-types-2d/rounded-column-chart-example.png)

> **_NOTE:_** The full **Rounded Column Chart** example can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
>
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-custom-series-rounded-column/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-custom-series-rounded-column-example/)
