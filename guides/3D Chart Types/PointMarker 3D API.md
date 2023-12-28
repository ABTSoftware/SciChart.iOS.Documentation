# PointMarker 3D API
SciChart features a rich **PointMarkers 3D API** to annotate the data-points of certain 3D series with markers, e.g. **Pyramid**, **Sphere**, **Ellipse**, **Quad** or even a **Custom Shape** marker. Some series types, such as **[Scatter RenderableSeries 3D](scatter-series-3d.html)** or **[Impulse RenderableSeries 3D](impulse-series-3d.html)**, require a **PointMarker3D** assigned to them unless they won't render at all.

This article is about how to configure and add PointMarkers 3D to a `ISCIRenderableSeries3D` to render markers for every data point.

![PointMarkers 3D API](img/chart-types-3d/point-cloud-chart-3d-example.png)

> **_NOTE:_** Examples of using PointMarkers API can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-3d-chart-example-simple-point-cloud-3d-chart/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-3d-chart-example-simple-point-cloud/)

## PointMarker 3D Types
SciChart provides several **PointMarker 3D** shapes out of the box, which are of 2 types: ***Flat-Texture*** and ***Mesh (Volumetric)*** which can be found below:

| **Flat-Texture PointMarkers** | **Mesh (Volumetric) PointMarkers** |
| ----------------------------- | ---------------------------------- |
| `SCITrianglePointMarker3D`    | `SCIPyramidPointMarker3D`          |
| `SCIQuadPointMarker3D`        | `SCICubePointMarker3D`             |
| `SCIEllipsePointMarker3D`     | `SCISpherePointMarker3D`           |
| `SCIPixelPointMarker3D`       | `SCICylinderPointMarker3D`         |
| `SCICustomPointMarker3D`      |

It is possible to have a custom ***Flat-Texture*** PointMarker, and there is a `SCICustomPointMarker3D` for such purpose.
It allows to render a point marker from a `SCIBitmap` (which is a simple wrapper around the `CGContext`). 
For more details, refer to the [Custom PointMarkers 3D](#custom-pointmarkers-3d) section down the page.

All the **PointMarker** types are inherited from the `SCIBasePointMarker3D`, which provides the following properties for styling point markers:

| **PointMarker property**          | **Description**                                                                                    |
| --------------------------------- | -------------------------------------------------------------------------------------------------- |
| `SCIBasePointMarker3D.size`       | Allows to specify the size of a PointMarker. PointMarkers will not appear if this value isn't set. |
| `SCIBasePointMarker3D.fillColor`  | Specifies the fill color which will be used while drawing the PointMarker instance.                |
| `SCIBasePointMarker3D.markerType` | Defines the `SCIMarkerType` for this point marker.                                                 |

## Using PointMarkers 3D
Code for creation and assigning a **PointMarker 3D** to a `ISCIRenderableSeries3D` is essentially the same regardless of a PointMarker type. 
After an instance of it has been created, it can be configured and then applied to the `ISCIRenderableSeries3D.pointMarker` property:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create a Sphere PointMarker 3D instance
    SCISpherePointMarker3D *pointMarker = [SCISpherePointMarker3D new];
    pointMarker.size = 25.0;
    pointMarker.fillColor = 0xFFFF0000;

    // Apply the PointMarker to a PointLine Series 3D
    id&lt;ISCIRenderableSeries3D&gt; rSeries = [SCIPointLineRenderableSeries3D new];
    rSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    // Create a Sphere PointMarker 3D instance
    let pointMarker = SCISpherePointMarker3D()
    pointMarker.size = 25.0
    pointMarker.fillColor = 0xFFFF0000

    // Apply the PointMarker to a PointLine Series 3D
    let rSeries = SCIPointLineRenderableSeries3D()
    rSeries.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    // Create a Sphere PointMarker 3D instance
    var pointMarker = new SCISpherePointMarker3D { Size = 25.0f, FillColor = 0xFFFF0000 };

    // Apply the PointMarker to a PointLine Series 3D
    var rSeries = new SCIPointLineRenderableSeries3D { PointMarker = pointMarker };
</div>

The code above will produce the following chart (assuming that the data has been added to the **[PointLine Series](pointline-series-3d.html)**):

![PointMarker 3D Example](img/chart-types-3d/pointmarker-3d-example.png)

## Custom PointMarkers 3D
SciChart iOS 3D provides a possibility to draw custom ***Flat-Texture*** PointMarkers via the `SCICustomPointMarker3D` class.
All you need to do - is to provide SCIBitmap texture which will be used as sprite, and then - rendered onto a 3d world.

Please see the example below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create custom PointMarker 3D
    SCICustomPointMarker3D *pointMarker = [[SCICustomPointMarker3D alloc] initWithBitmap:[UIImage imageNamed:@"image.weather.storm"].sciBitmap];
    pointMarker.size = 10.f;
    
    // Apply the point onto a Scatter Series 3D
    SCIScatterRenderableSeries3D *rs = [SCIScatterRenderableSeries3D new];
    rs.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    // Create custom PointMarker 3D
    let pointMarker = SCICustomPointMarker3D(bitmap: SCIBitmap(image: #imageLiteral(resourceName: "image.weather.storm")))
    pointMarker.size = 10.0
    
    // Apply the point onto a Scatter Series 3D
    let rs = SCIScatterRenderableSeries3D()
    rs.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    // Create custom PointMarker 3D
    var pointMarker = new SCICustomPointMarker3D(new UIImage("image.weather.storm").SciBitmap()) { Size = 10.0f };

    // Apply the point onto a Scatter Series 3D
    var rSeries = new SCIScatterRenderableSeries3D { PointMarker = pointMarker };
</div>

> **_NOTE:_** There are helper methods provided to create the `SCIBitmap` - either via it's static ctor - `+[SCIBitmap bitmapWithImage:]` or `-[UIImage(SCIBitmap) sciBitmap]` extension.

This would result in the following chart:

![Custom PointMarker 3D](img/chart-types-3d/custom-pointmarker-3d.png)