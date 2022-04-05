# Axis Styling - Grid Lines, Ticks and Axis Bands
As mentioned in previous article on Axis Styling - [Title and Labels](axis-styling---title-and-labels.html) -
**each and every** aspect of the axis can be styled. The Axis is responsible for drawing the following parts:
- [Title](axis-styling---title-and-labels.html#axis-title)
- [Axis Labels](axis-styling---title-and-labels.html#axis-labels)
- [Tick Lines](#axis-ticks) - small marks on the outside of an axis **next to labels**
- [Grid Lines](#grid-lines) - major and minor
- [Axis Bands](#axis-bands) - shading between the **major** grid-lines

In this article we are going to focus on [Tick Lines](#axis-ticks), [Gridlines](#grid-lines) and [Axis Bands](#axis-bands) styling. So let's pick up where we left off in [previous article](axis-styling---title-and-labels.html) and proceed with some styling.

> **_NOTE:_** It's important to mention, that `X-Axis` is responsible for **vertical** grid-lines and bands, whereas `Y-Axis` - for **horizontal** ones.

![Title Positioning](img/axis-2d/axis-ticks-gridlines-bands-styling.png)

> **_NOTE:_** In SciChart, almost all styling methods expect an instance of either `SCIPenStyle` or `SCIBrushStyle` to be passed in. Those that deals with text styling, expect an instance of a `SCIFontStyle`. To learn more about how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

## Axis Ticks
Axis Ticks can be styled via applying the `SCIPenStyle` onto the `ISCIAxisCore.minorTickLineStyle` property to affect **Minor** ticks, or onto the `ISCIAxisCore.majorTickLineStyle` - for the **Major** ticks. Ticks **Length** can also be controlled via `ISCIAxisCore.majorTickLineLength` and `ISCIAxisCore.minorTickLineLength` properties.

Also, ticks can be completely hidden or shown on an axis via the `ISCIAxisCore.drawMinorTicks` and `ISCIAxisCore.drawMajorTicks` properties.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    axis.minorTickLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:2];
    axis.majorTickLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:4];
</div>
<div class="code-snippet" id="swift">
    axis.minorTickLineStyle = SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 2)
    axis.majorTickLineStyle = SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 4)
</div>
<div class="code-snippet" id="cs">
    axis.MinorTickLineStyle = new SCISolidPenStyle(0xFF279B27, 2);
    axis.MajorTickLineStyle = new SCISolidPenStyle(0xFF279B27, 4);
</div>

## Grid Lines
Grid Lines are styled the same way as [Axis ticks](#axis-ticks) - use the following properties to affect gridlines rendering:
- `ISCIAxisCore.drawMajorGridLines`
- `ISCIAxisCore.drawMinorGridLines`
- `ISCIAxisCore.majorGridLineStyle`
- `ISCIAxisCore.minorGridLineStyle`

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    axis.minorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0x33279B27 thickness:1];
    axis.majorGridLineStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF279B27 thickness:3 strokeDashArray:@[@5, @15] antiAliasing:YES];
</div>
<div class="code-snippet" id="swift">
    axis.minorGridLineStyle = SCISolidPenStyle(colorCode: 0x33279B27, thickness: 1)
    axis.majorGridLineStyle = SCISolidPenStyle(colorCode: 0xFF279B27, thickness: 3, strokeDashArray: [5, 15], antiAliasing: true)
</div>
<div class="code-snippet" id="cs">
    axis.MinorGridLineStyle = new SCISolidPenStyle(0xFF279B27, 1);
    axis.MajorGridLineStyle = new SCISolidPenStyle(0xFF279B27, 3, true, new float[] { 5f, 15f });
</div>

## Axis Bands
The colored strips between **Major** grid lines are called **Axis Bands**. They can be made visible or hidden via the `ISCIAxisCore.drawMajorBands` property. The fill can be changed via the `ISCIAxisCore.axisBandsStyle` property, that expects a `SCIBrushStyle`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    axis.axisBandsStyle = [[SCISolidBrushStyle alloc] initWithColorCode:0x22279B27];
</div>
<div class="code-snippet" id="swift">
    axis.axisBandsStyle = SCISolidBrushStyle(colorCode: 0x22279B27)
</div>
<div class="code-snippet" id="cs">
    axis.AxisBandsStyle = new SCISolidBrushStyle(0x33279B27);
</div>
