# Coordinates in 3D Space
In SciChart, there is a possibility to configure the **coordinate system** which will be used by renderers. Could be one of the following;
- Left Handed Coordinate System
- Right Handed Coordinate System

Be default `SCIChartSurface3D` renders 3D world using the **Left-Handed** coordinate system - **LHS**. 
In the Left-Handed coordinates **X and Z Axes** form the horizontal plane, and **Y Axis** is always up - `YDirection = [0, 1, 0]`. 
It is helpful to think of the 3D world as a 2D Chart with `X and Y axes` plus addition of **Z-Axis** which goes ***"into the screen"***.

![LHS rule 3D](img/axis-3d/lhs-rule-3d.png)

![Left-Handed Coordinates](img/axis-3d/left-handed-coordinates.png)

SciChart also allows you to render `SCIChartSurface3D` world in **Right-Handed** coordinate system - **RHS**. 
It is easily switchable via the `ISCIViewport3D.isLeftHandedCoordinateSystem` property:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    self.surface.viewport.isLeftHandedCoordinateSystem = NO;
</div>
<div class="code-snippet" id="swift">
    self.surface.viewport.isLeftHandedCoordinateSystem = false
</div>
<div class="code-snippet" id="cs">
    Surface.Viewport.IsLeftHandedCoordinateSystem = false;
</div>

## World Coordinates
World Coordinates is the term used to describe coordinates in the 3D World. These are the raw `[X, Y, Z]` coordinates of a vertex.
By default, the origin `[0, 0, 0]` is in the center of the bottom plane of the chart.

The box in the chart is called the **AxisCube**. The AxisCube size is defined by the `SCIChartSurface3D.worldDimensions` property, 
which is a single `SCIVector3` with `[X, Y, Z]` sizes and defines the size of a cube as follows:

![World Coordinates](img/axis-3d/world-coordinates.png)

Therefore, size of the ***AxisCube*** in `[X, Y, Z]` is extends like below:

| **Direction** | **Extends like**                                  |
| ------------- | ------------------------------------------------- |
| `X-Direction` | `[-worldDimensions.x / 2; worldDimensions.x / 2]` |
| `Y-Direction` | `[0; worldDimensions.y]`                          |
| `Z-Direction` | `[-worldDimensions.z / 2; worldDimensions.z / 2]` |

> **_NOTE:_** By **default** the `SCIChartSurface3D.worldDimensions` is initialized with `[X = 300, Y = 200, Z = 300]`

To change the `SCIChartSurface3D.worldDimensions` property, simply provide it with the `SCIVector3` (3-component vector) like below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec" >
    [self.surface.worldDimensions assignX:200 y:200 z:200];
</div>
<div class="code-snippet" id="swift">
    self.surface.worldDimensions.assignX(200, y: 200, z: 200)
</div>
<div class="code-snippet" id="cs">
    Surface.WorldDimensions = new SCIVector3(200, 200, 200);
</div>

## Data Coordinates
By contrast to the [WorldCoordinates](#world-coordinates), which are **absolute** coordinates in the 3D World, in SciChart 3D there is the concept of **Data Coordinates**.

Data Coordinates are measured on an `ISCIAxis3D` instance, for example, the `Y-Axis` (which is UP) might have a size of 200 in the World Coordinates, but might display a VisibleRange of `[0...10]`. 
Therefore, Data which falls int he range `[0...10]` will be spaced on this axis from `[0...200]` World Coordinates.

The difference between ***World*** and ***Data*** Coordinates is shown in the following example:

![World Coordinates](img/axis-3d/data-vs-world-coordinates.png)

## Converting from World to Data Coordinates
The conversion between ***Data Coordinates*** and ***World Coordinates*** is done by the `ISCIAxis3D`. 
It's no different to 2D conversions which is described in the [Axis APIs - Convert Pixel to Data coordinates](axis-apis---convert-pixel-to-data-coordinates.html) article.

You can find simple example how to do the conversions for `SCINumericAxis3D` below.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCICoordinateCalculator&gt; calculator = xAxis.currentCoordinateCalculator;
    float coordinate = [calculator getCoordinateFrom:1.2];
    
    // Convert back:
    double dataValue = [calculator getDataValueFrom:coordinate];
</div>
<div class="code-snippet" id="swift">
    let calculator = xAxis.currentCoordinateCalculator!
    let coordinate = calculator.getCoordinate(1.2)
    
    // Convert back:
    let dataValue = calculator.getDataValue(coordinate)
</div>
<div class="code-snippet" id="cs">
    var coordinate = xAxis.GetCoordinate(1.2.FromComparable());

    // Convert back:
    var dataValue = xAxis.GetDataValue(coordinate);
</div>
