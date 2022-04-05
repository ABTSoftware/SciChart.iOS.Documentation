# Axis 3D Labels - Labels Configuration
In SciChart 3D you can configure showing the **Axis Labels** differently on the **Axis Cube** Planes. 
Possible options are defined by the `SCIAxisPlaneDrawLabelsMode` enumeration.

But first, let's explain **Axis Cube** planes properly.

#### Axis Cube Planes
![Axis Cube Planes](img/axis-3d/axis-cube-planes.png)

As you might guess, there are three possible planes in axis cube, and those are defined as follows:

| **AxisCube Plane** | **Description**                                     |
| ------------------ | --------------------------------------------------- |
| **XY** Plane       | perpendicular to the **Z-Axis** (Left by default).  |
| **ZY** Plane       | perpendicular to the **X-Axis** (Right by default). |
| **ZX** Plane       | perpendicular to the **Y-Axis** (up).               |

Let's see the ZY Plane on the image below:

![ZY Plane Explanation](img/axis-3d/zy-plane-explanation.png)

#### Axis Cube Labels Configuration
As mentioned [above](#axis-3d-labels---labels-configuration), drawing labels are controlled by the `SCIAxisPlaneDrawLabelsMode` enumeration, which has the following options:

| `SCIAxisPlaneDrawLabelsMode`    | **Description**                                                                                                                    |
| ------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| Both                            | axis labels are drawn on both sides of the axis cube. This is the **default** mode.                                                |
| Hidden                          | hides the axis labels for this plane, for example, when applied to the XyAxisPlane, the labels drawn by this plane will be hidden. |
| LocalX                          | draws the labels on one side - the ***local X*** - of the plane.                                                                   |
| LocalY                          | draws the labels on the other side - the ***local Y*** - of the plane.                                                             |

The above modes can be applied to one of the AxisCube's planes which are accessible through the following properties on the `SCIChartSurface3D`:
- `SCIChartSurface3D.xyAxisPlaneDrawLabelsMode`
- `SCIChartSurface3D.zyAxisPlaneDrawLabelsMode`
- `SCIChartSurface3D.zxAxisPlaneDrawLabelsMode`

See how it works in the code snippet below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    self.surface.xyAxisPlaneDrawLabelsMode = SCIAxisPlaneDrawLabelsMode_Both;
    self.surface.zyAxisPlaneDrawLabelsMode = SCIAxisPlaneDrawLabelsMode_LocalX;
    self.surface.zxAxisPlaneDrawLabelsMode = SCIAxisPlaneDrawLabelsMode_Hidden;
</div>
<div class="code-snippet" id="swift">
    surface.xyAxisPlaneDrawLabelsMode = .both
    surface.zyAxisPlaneDrawLabelsMode = .localX
    surface.zxAxisPlaneDrawLabelsMode = .hidden
</div>
<div class="code-snippet" id="cs">
    Surface.XyAxisPlaneDrawLabelsMode = SCIAxisPlaneDrawLabelsMode.Both;
    Surface.ZyAxisPlaneDrawLabelsMode = SCIAxisPlaneDrawLabelsMode.LocalX;
    Surface.ZxAxisPlaneDrawLabelsMode = SCIAxisPlaneDrawLabelsMode.Hidden;
</div>

Which results with the following:

![Axis Plane Draw Labels Mode](img/axis-3d/axis-plane-draw-labels-mode.png)
