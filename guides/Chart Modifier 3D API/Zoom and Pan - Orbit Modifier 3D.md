# Orbit Modifier 3D
SciChart iOS 3D provides a **panning** behavior via the `SCIOrbitModifier3D`, available out of the box.

The `SCIOrbitModifier3D` performs ***orbital motion*** of the camera giving the appearance of rotating the 3D world.

<video autoplay loop muted playsinline src="img/modifiers-3d/orbit-modifier-3d.mp4"></video>

Besides [common features](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) which are inherited from the `SCIChartModifier3DBase` class, 
the `SCIOrbitModifier3D` also allows you to control **panning sensitivity** which allows you to change the orbit speed. 
It's accessible via the `SCIOrbitModifier3D.degreesPerPixelSensitivity` property.

## Adding a SCIOrbitModifier3D to a Chart
Any [Chart Modifier 3D](Chart Modifier 3D APIs.html) can be [added to a `SCIChartSurface3D`](Chart Modifier 3D APIs.html#adding-a-chart-modifier-3d) via the `ISCIChartSurface3D.chartModifiers` property and `SCIOrbitModifier3D` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface3D&gt; surface;

    // Create a Modifier
    SCIOrbitModifier3D *orbitModifier3D = [SCIOrbitModifier3D new];
    orbitModifier3D.degreesPerPixelSensitivity = 0.4;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:orbitModifier3D];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface3D

    // Create a Modifier
    let orbitModifier3D = SCIOrbitModifier3D()
    orbitModifier3D.degreesPerPixelSensitivity = 0.4

    // Add the modifier to the surface
    self.surface.chartModifiers.add(orbitModifier3D)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface3D surface;

    // Create a Modifier
    var orbitModifier3D = new SCIOrbitModifier3D();
    orbitModifier3D.degreesPerPixelSensitivity = 0.4;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(orbitModifier3D);
</div>

> **_NOTE:_** To learn more about features available, please read on the [Chart Modifier 3D APIs](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) article.
