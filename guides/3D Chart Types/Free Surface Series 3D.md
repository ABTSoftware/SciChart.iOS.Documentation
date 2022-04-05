# The Free Surface 3D Chart Type
The Free Surface 3D Chart types are a set of 3D Charts that represent the data by plotting the surface in a **custom shape in 3D space**.

It's represented by the `SCIFreeSurfaceRenderableSeries3D` in SciChart, and can be configured with different `SCIFreeSurfaceDataSeries3D`.

The **Free Surface 3D** Chart Types can be divided into a two groups:
- [constrained](#constrained-free-surface-3d-types) to its ***basic 3D primitive***
- [unconstrained](#unconstrained-free-surface-3d-type) - ***free surface***.

You can find more information about individual Free Surface types in the corresponding sections of this article.

![Free Surface 3D Collage](img/chart-types-3d/free-surface-3d-collage.png)

## Constrained Free Surface 3D Types
Each of constrained chart type represents its basic primitive. 
There is a 3 constrained free-surface types available out of the box in SciChart: a **sphere**, a **cylinder**, or a **disk**.

The ***Surface*** of such charts can be modified by the `2D offset array`. 
Each particular type of the constrained 3D chart specifies how the surface can be offset:
- [The Ellipsoid Chart](free-surface-series-3d---ellipsoid.html) - **offsets** its points on the surface in each of `XYZ axes` **proportionality**, based on the location of its origin.
- [The Cylindroid Chart](free-surface-series-3d---cylindroid.html) - **offsets** the surface in `XZ axes`, **based** on its **Y-axis** aligned origin line.
- [The Polar 3D Chart](free-surface-series-3d---polar.html) - offsets its surface in `Y-axis`, **based** on **XZ axes** origin plane.

## Unconstrained Free Surface 3D Type
In contrast to the [constrained](#constrained-free-surface-3d-types) chart types, [The Custom Free Surface 3D Chart](free-surface-series-3d---custom-surface.html) isn’t based on any geometric primitive.
The shape of its surface is defined by a set of **user-defined functions**, injected in the constructor during the instantiation.
This approach allows the surface to obtain ***any possible shape***.

## Configuring Free Surface 3D Series
Most of the configuration options follow the same approach as it is in [The SurfaceMesh 3D Chart Type](surface-mesh-series-3d.html) so all of the following are also applicable to ***Free Surface Meshes***:
- [Applying Palettes to the Surface Meshes](surface-mesh-series-3d.html#applying-palettes-to-the-3d-surface-meshes)
- [Configuring Wireframe and Contours](surface-mesh-series-3d.html#surface-mesh-3d-wireframe-and-contours)
- [Overriding Specific Cell Colors](surface-mesh-series-3d.html#overriding-surface-mesh-3d-specific-cell-colors)

Despite the similarity of configuration to other 3D charts, Free Surface 3D Charts have some unique options.
One of them is the **Palette Mode**, determining which color is picked from the Palette based on its four components:
- [The Axial Palette Component](#the-axial-palette-component)
- [The Radial Palette Component](#the-radial-palette-component)
- [The Azimuthal Palette Component](#the-azimuthal-palette-component)
- [The Polar Palette Component](#the-polar-palette-component)

Each of components can be used separately or blended together, based on values of corresponding **Factor** properties.
Below is the formula that determines the color of the Palette:

![Free Surface Palette Formula](img/chart-types-3d/free-surface-3d-palette-formula.png)
- P - is the coordinate that determines ***color*** being picked from the Palette by its value in the ange `[0..1]`;
- W<sub>ax</sub> - the **axial** weight `3D vector`;
- F<sub>ax</sub> - the ***axial factor*** specified by the `SCIFreeSurfaceRenderableSeries3D.paletteAxialFactor` **3D vector**.
- W<sub>r</sub> - the **radial** weight;
- F<sub>r</sub> - the ***radial factor*** specified by the `SCIFreeSurfaceRenderableSeries3D.paletteRadialFactor` property.
- W<sub>az</sub> - the **azimuthal** weight;
- F<sub>az</sub> - the ***azimuthal factor*** specified by the `SCIFreeSurfaceRenderableSeries3D.paletteAzimuthalFactor` property.
- W<sub>p</sub> - the **polar** weight;
- F<sub>p</sub> - the ***polar factor*** specified by the `SCIFreeSurfaceRenderableSeries3D.palettePolarFactor` property.

All of the components are `0` by default, except **Radial** which is equal to `1`.

To learn more about each of the component - read on the following section of this article.

#### The Axial Palette Component
In this mode, **palette color** is determined by the position of a particular point on the surface that ***linearly interpolates*** between the user-specified ***minimum*** and ***maximum*** values. The ***axial*** component is controlled by the `SCIFreeSurfaceRenderableSeries3D.paletteAxialFactor` vector.

The weight of the **Axial Palette Component** is calculated by following formula:

![Free Surface Axial Palette Formula](img/chart-types-3d/free-surface-3d-palette-formula-axial.png)
- P<sub>s</sub> - is the **position** of a particular point on the surface;
- P<sub>min</sub> - is specified by the `SCIFreeSurfaceRenderableSeries3D.paletteMinimum` property;
- P<sub>max</sub> - is specified by the `SCIFreeSurfaceRenderableSeries3D.paletteMaximum` property;

See how it works in the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    unsigned int colors[7] = { 0xFF00008B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000 };
    float stops[7] = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0};
    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7];

    SCIFreeSurfaceRenderableSeries3D *rSeries = [SCIFreeSurfaceRenderableSeries3D new];
    rSeries.meshColorPalette = palette;
    rSeries.paletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode_Absolute;
    rSeries.paletteMinimum = [[SCIVector3 alloc] initWithX:0.0 y:-4.0 z:0.0];
    rSeries.paletteMaximum = [[SCIVector3 alloc] initWithX:0.0 y:4.0 z:0.0];
    rSeries.paletteAxialFactor = [[SCIVector3 alloc] initWithX:0.0 y:1.0 z:0.0];
</div>
<div class="code-snippet" id="swift">
    let colors: [UInt32] = [0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000]
    let stops: [Float] = [0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0]
    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7)
    
    let rSeries = SCIFreeSurfaceRenderableSeries3D()
    rSeries.meshColorPalette = palette
    rSeries.paletteMinMaxMode = .absolute
    rSeries.paletteMinimum = SCIVector3(x: 0.0, y: -4.0, z: 0.0)
    rSeries.paletteMaximum = SCIVector3(x: 0.0, y: 4.0, z: 0.0)
    rSeries.paletteAxialFactor = SCIVector3(x: 0.0, y: 1.0, z: 0.0)
</div>
<div class="code-snippet" id="cs">
    var palette = new SCIGradientColorPalette(
            new[] { ColorUtil.Sapphire, ColorUtil.Blue, ColorUtil.Cyan, ColorUtil.GreenYellow, ColorUtil.Yellow, ColorUtil.Red, ColorUtil.DarkRed },
            new[] { 0, .1f, .3f, .5f, .7f, .9f, 1 });

    var rSeries = new SCIFreeSurfaceRenderableSeries3D();
    rSeries.MeshColorPalette = palette;
    rSeries.PaletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode.Absolute;
    rSeries.PaletteMinimum = new SCIVector3(0.0f, -4.0f, 0.0f);
    rSeries.PaletteMaximum = new SCIVector3(0.0f, 4.0f, 0.0f);
    rSeries.PaletteAxialFactor = new SCIVector3(0.0f, 1.0f, 0.0f);
</div>

![Free Surface Axial Palette](img/chart-types-3d/free-surface-3d-axial-palette.png)

#### The Radial Palette Component
The ***Radial*** component is controlled by the `SCIFreeSurfaceRenderableSeries3D.paletteRadialFactor` property.
It is quite simple, similar to the 2d heatmaps, see the example below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    unsigned int colors[7] = { 0xFF00008B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000 };
    float stops[7] = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0};
    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7];

    SCIFreeSurfaceRenderableSeries3D *rSeries = [SCIFreeSurfaceRenderableSeries3D new];
    rSeries.meshColorPalette = palette;
    rSeries.paletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode_Relative;
    rSeries.paletteMinimum = [[SCIVector3 alloc] initWithX:0.0 y:6.0 z:0.0];
    rSeries.paletteMaximum = [[SCIVector3 alloc] initWithX:0.0 y:7.0 z:0.0];
    rSeries.paletteRadialFactor = 1;
</div>
<div class="code-snippet" id="swift">
    let colors: [UInt32] = [0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000]
    let stops: [Float] = [0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0]
    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7)
    
    let rSeries = SCIFreeSurfaceRenderableSeries3D()
    rSeries.meshColorPalette = palette
    rSeries.paletteMinMaxMode = .relative
    rSeries.paletteMinimum = SCIVector3(x: 0.0, y: 6.0, z: 0.0)
    rSeries.paletteMaximum = SCIVector3(x: 0.0, y: 7.0, z: 0.0)
    rSeries.paletteRadialFactor = 1
</div>
<div class="code-snippet" id="cs">
    var palette = new SCIGradientColorPalette(
            new[] { ColorUtil.Sapphire, ColorUtil.Blue, ColorUtil.Cyan, ColorUtil.GreenYellow, ColorUtil.Yellow, ColorUtil.Red, ColorUtil.DarkRed },
            new[] { 0, .1f, .3f, .5f, .7f, .9f, 1 });

    var rSeries = new SCIFreeSurfaceRenderableSeries3D();
    rSeries.MeshColorPalette = palette;
    rSeries.PaletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode.Relative;
    rSeries.PaletteMinimum = new SCIVector3(0.0f, 6.0f, 0.0f);
    rSeries.PaletteMaximum = new SCIVector3(0.0f, 7.0f, 0.0f);
    rSeries.PaletteRadialFactor = 1;
</div>

![Free Surface Radial Palette](img/chart-types-3d/free-surface-3d-radial-palette.png)

#### The Azimuthal Palette Component
In this mode palette color is determined by **cos angle** between the unit vector of the `X-Axis` and vector from the origin point to the particular point on the surface, projected onto the [XZ plane](axis-3d-labels---labels-configuration.html#axis-cube-planes). The ***azimuthal*** component is controlled by the `SCIFreeSurfaceRenderableSeries3D.paletteAzimuthalFactor` property.

The weight of the **Azimuthal Palette Component** is calculated by the following formula:

![Free Surface Azimuthal Palette Formula](img/chart-types-3d/free-surface-3d-palette-formula-azimuthal.png)
- X - is the **unit vector** of the `X-Axis`;
- P<sub>xz</sub> - is a **particular point** on the surface, ***projected*** onto the [XZ plane](axis-3d-labels---labels-configuration.html#axis-cube-planes);

See how it works in the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    unsigned int colors[7] = { 0xFF00008B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000 };
    float stops[7] = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0};
    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7];

    SCIFreeSurfaceRenderableSeries3D *rSeries = [SCIFreeSurfaceRenderableSeries3D new];
    rSeries.meshColorPalette = palette;
    rSeries.paletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode_Relative;
    rSeries.paletteAzimuthalFactor = 1;
</div>
<div class="code-snippet" id="swift">
    let colors: [UInt32] = [0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000]
    let stops: [Float] = [0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0]
    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7)
    
    let rSeries = SCIFreeSurfaceRenderableSeries3D()
    rSeries.meshColorPalette = palette
    rSeries.paletteMinMaxMode = .relative
    rSeries.paletteAzimuthalFactor = 1
</div>
<div class="code-snippet" id="cs">
    var palette = new SCIGradientColorPalette(
            new[] { ColorUtil.Sapphire, ColorUtil.Blue, ColorUtil.Cyan, ColorUtil.GreenYellow, ColorUtil.Yellow, ColorUtil.Red, ColorUtil.DarkRed },
            new[] { 0, .1f, .3f, .5f, .7f, .9f, 1 });

    var rSeries = new SCIFreeSurfaceRenderableSeries3D();
    rSeries.MeshColorPalette = palette;
    rSeries.PaletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode.Relative;
    rSeries.PaletteAzimuthalFactor = 1;
</div>

<video autoplay loop muted playsinline src="img/chart-types-3d/free-surface-3d-azimuthal-palette.mp4"></video>

#### The Polar Palette Component
In this mode, palette color is determined by **cos angle** between the unit vector of the `Y-Axis` and vector from the origin point to the particular point on the surface. The ***polar*** component is controlled by the `SCIFreeSurfaceRenderableSeries3D.palettePolarFactor` property.

The weight of the ***Polar Palette Component*** is calculated by following formula:

![Free Surface Polar Palette Formula](img/chart-types-3d/free-surface-3d-palette-formula-polar.png)
- Y - is the **unit vector** of the `Y-Axis`;
- P - is a **particular point** on the surface.

See how it works in the code below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    unsigned int colors[7] = { 0xFF00008B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000 };
    float stops[7] = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0};
    SCIGradientColorPalette *palette = [[SCIGradientColorPalette alloc] initWithColors:colors stops:stops count:7];

    SCIFreeSurfaceRenderableSeries3D *rSeries = [SCIFreeSurfaceRenderableSeries3D new];
    rSeries.meshColorPalette = palette;
    rSeries.paletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode_Relative;
    rSeries.palettePolarFactor = 1;
</div>
<div class="code-snippet" id="swift">
    let colors: [UInt32] = [0xFF1D2C6B, 0xFF0000FF, 0xFF00FFFF, 0xFFADFF2F, 0xFFFFFF00, 0xFFFF0000, 0xFF8B0000]
    let stops: [Float] = [0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0]
    let palette = SCIGradientColorPalette(colors: colors, stops: stops, count: 7)
    
    let rSeries = SCIFreeSurfaceRenderableSeries3D()
    rSeries.meshColorPalette = palette
    rSeries.paletteMinMaxMode = .relative
    rSeries.palettePolarFactor = 1
</div>
<div class="code-snippet" id="cs">
    var palette = new SCIGradientColorPalette(
            new[] { ColorUtil.Sapphire, ColorUtil.Blue, ColorUtil.Cyan, ColorUtil.GreenYellow, ColorUtil.Yellow, ColorUtil.Red, ColorUtil.DarkRed },
            new[] { 0, .1f, .3f, .5f, .7f, .9f, 1 });

    var rSeries = new SCIFreeSurfaceRenderableSeries3D();
    rSeries.MeshColorPalette = palette;
    rSeries.PaletteMinMaxMode = SCIFreeSurfacePaletteMinMaxMode.Relative;
    rSeries.PalettePolarFactor = 1;
</div>

![Free Surface Polar Palette](img/chart-types-3d/free-surface-3d-polar-palette.png)