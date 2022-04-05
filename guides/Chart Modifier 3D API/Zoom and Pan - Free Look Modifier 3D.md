# Free Look Modifier 3D
If you want to add simple movement of the camera (imagine ***free-look*** in a computer game) then you can do so using our [Chart Modifier 3D API](Chart Modifier 3D APIs.html). 
The `SCIFreeLookModifier3D` performs `SCICamera3D` movement in the Left/Right/Up/Down direction giving the appearance of moving through the 3D World

<video autoplay loop muted playsinline src="img/modifiers-3d/free-look-modifier-3d.mp4"></video>

Besides [common features](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) which are inherited from the `SCIChartModifier3DBase` class, 
the `SCIFreeLookModifier3D` also allows you to control movement **sensitivity** via the `SCIFreeLookModifier3D.degreesPerPixelSensitivity` property.

## Adding a SCIFreeLookModifier3D to a Chart
Any [Chart Modifier 3D](Chart Modifier 3D APIs.html) can be [added to a `SCIChartSurface3D`](Chart Modifier 3D APIs.html#adding-a-chart-modifier-3d) via the `ISCIChartSurface3D.chartModifiers` property and `SCIFreeLookModifier3D` with no difference:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface3D&gt; surface;

    // Create a Modifier
    SCIFreeLookModifier3D *freeLookModifier3D = [[SCIFreeLookModifier3D alloc] initWithDefaultNumberOfTouches:2];
    freeLookModifier3D.degreesPerPixelSensitivity = 0.4;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:freeLookModifier3D];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface3D

    // Create a Modifier
    let freeLookModifier3D = SCIFreeLookModifier3D(defaultNumberOfTouches: 2)
    freeLookModifier3D.degreesPerPixelSensitivity = 0.4

    // Add the modifier to the surface
    self.surface.chartModifiers.add(freeLookModifier3D)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface3D surface;

    // Create a Modifier
    var freeLookModifier3D = new SCIFreeLookModifier3D(2);
    freeLookModifier3D.degreesPerPixelSensitivity = 0.4;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(freeLookModifier3D);
</div>

> **_NOTE:_** To learn more about features available, please read on the [Chart Modifier 3D APIs](Chart Modifier 3D APIs.html#common-chart-modifier-3d-features) article.
