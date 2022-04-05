# SCIPenStyle, SCIBrushStyle and SCIFontStyle
In SciChart, almost all **styling** methods expect an instance of either [SCIPenStyle](#scipenstyle) or [SCIBrushStyle](#scibrushstyle) to be passed in. 
Those that deals with text styling, expect an instance of a [SCIFontStyle](#scifontstyle). 
All these classes are designed to hold a relevant information related to drawing.

SciChart also provides the category to [UIColor](https://developer.apple.com/documentation/uikit/uicolor) - [UIColor(Util)](Categories/UIColor(Util).html) - which can be used for some common conversions and retrieving partial color information.

## SCIPenStyle
There are several available `SCIPenStyle` implementations which allow us to specify how lines should be drawn in SciChart:
- `SCISolidPenStyle` - allows to draw 2D lines with **solid** color.
- `SCILinearGradientPenStyle` - allows to draw 2D lines with **linear gradient**.
- `SCIRadialGradientPenStyle` - allows to draw 2D lines with **radial gradient**.
- `SCITexturePenStyle` - allows to draw **textured** 2D lines.

Also, all `SCIPenStyle` implementations allows to specify the following parameters for a pen:
- `SCIPenStyle.color` - the pen color, which can be exposed as `SCIPenStyle.colorCode` in `ARGB` color space.
- `SCIPenStyle.antiAliasing` - is pen **anti-aliased** or not.
- `SCIPenStyle.thickness` - it's **thickness**.
- `SCIPenStyle.strokeDashArray` - used to create **dashed** pens.

To create one of the `SCIPenStyle` instance, call an appropriate type constructor, for example:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Solid Pen
    SCIPenStyle *solidPenStyle = [[SCISolidPenStyle alloc] initWithColorCode:0xFF99EE99 thickness:1.f strokeDashArray:@[@(10.f), @(3.f), @(10.f), @(3.f)] antiAliasing:YES];
    // Linear Gradient Pen requires SCILinearGradientBrushStyle
    SCILinearGradientBrushStyle *linearGradientBrush;
    SCIPenStyle *linearGradientPenStyle = [[SCILinearGradientPenStyle alloc] initWithGradientStyle:linearGradientBrush antiAliasing:YES thickness:1.f strokeDashArray:nil];
    // Radial Gradient Pen requires SCIRadialGradientBrushStyle
    SCIRadialGradientBrushStyle *radialGradientBrush;
    SCIPenStyle *radialGradientPenStyle = [[SCIRadialGradientPenStyle alloc] initWithGradientStyle:radialGradientBrush antiAliasing:YES thickness:1.f strokeDashArray:nil];
    // Texture Pen requires SCITextureBrushStyle
    SCITextureBrushStyle *textureBrush;
    SCIPenStyle *texturedPenStyle = [[SCITexturePenStyle alloc] initWithGradientStyle:textureBrush antiAliasing:YES thickness:1.f strokeDashArray:nil];
</div>
<div class="code-snippet" id="swift">
    // Solid Pen
    let solidPenStyle = SCISolidPenStyle(color: 0xFF99EE99, thickness: 1, strokeDashArray: [10, 3, 10, 3], antiAliasing: true)
    // Linear Gradient Pen requires SCILinearGradientBrushStyle
    var linearGradientBrush: SCILinearGradientBrushStyle!
    let linearGradientPenStyle = SCILinearGradientPenStyle(gradientStyle: linearGradientBrush, thickness: 1, strokeDashArray: nil, antiAliasing: true)
    // Radial Gradient Pen requires SCIRadialGradientBrushStyle
    var radialGradientBrush: SCIRadialGradientBrushStyle!
    let radialGradientPenStyle = SCIRadialGradientPenStyle(gradientStyle: radialGradientBrush, thickness: 1, strokeDashArray: nil, antiAliasing: true)
    var textureBrush: SCITextureBrushStyle!
    let texturedPenStyle = SCITexturePenStyle(textureBrushStyle: textureBrush, thickness: 1, strokeDashArray: nil, antiAliasing: true)
</div>
<div class="code-snippet" id="cs">
    // Solid Pen
    var solidPenStyle = new SCISolidPenStyle(0xFF99EE99, 1f, true, new[] { 10f, 3f, 10f, 3f });
    // Linear Gradient Pen requires SCILinearGradientBrushStyle
    SCILinearGradientBrushStyle linearGradientBrush;
    var linearGradientPenStyle = new SCILinearGradientPenStyle(linearGradientBrush, true, 1, null);
    // Radial Gradient Pen requires SCIRadialGradientBrushStyle
    SCIRadialGradientBrushStyle radialGradientBrush;
    var radialGradientPenStyle = new SCIRadialGradientPenStyle(radialGradientBrush, true, 1, null);
    // Texture Pen requires SCITextureBrushStyle
    SCITextureBrushStyle textureBrush;
    var texturedPenStyle = new SCITexturePenStyle(textureBrush, true, 1, null);
</div>

## SCIBrushStyle
As to the `SCIBrushStyle` class, it has similar implementations as the [SCIPenStyle](#scipenstyle) like the following:
- `SCISolidBrushStyle` - allows to draw 2D fills with **solid** color.
- `SCILinearGradientBrushStyle` - allows to draw 2D fills with **linear gradient**.
- `SCIRadialGradientBrushStyle` - allows to draw 2D fills with **radial gradient**.
- `SCITextureBrushStyle` - allows to draw 2D fills with **textures**.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Solid Brush
    SCIBrushStyle *solidBrushStyle = [[SCISolidBrushStyle alloc] initWithColor:UIColor.redColor];
    // Linear Gradient Brush
    SCIBrushStyle *linearGradientBrushStyle = [[SCILinearGradientBrushStyle alloc] initWithStart:CGPointZero end:CGPointMake(0, 1) startColor:UIColor.redColor endColor:UIColor.blueColor];
    // Radial Gradient Brush
    SCIBrushStyle *radialGradientBrushStyle = [[SCIRadialGradientBrushStyle alloc] initWithCenterColor:UIColor.redColor edgeColor:UIColor.blueColor];
    // Texture Brush - SCIBitmap with Texture should be provided
    SCIBitmap *textureBitmap;
    SCIBrushStyle *textureBrushStyle = [[SCITextureBrushStyle alloc] initWithTexture:textureBitmap];
</div>
<div class="code-snippet" id="swift">
    // Solid Brush
    let solidBrushStyle = SCISolidBrushStyle(color: .red)
    // Linear Gradient Brush
    let linearGradientBrushStyle = SCILinearGradientBrushStyle(start: .zero, end: CGPoint(x: 0, y: 1), startColor: .red, endColor: .blue)
    // Radial Gradient Brush
    let radialGradientBrushStyle = SCIRadialGradientBrushStyle(centerColor: .red, edgeColor: .blue)
    // Texture Brush - SCIBitmap with Texture should be provided
    var textureBitmap: SCIBitmap!
    let textureBrushStyle = SCITextureBrushStyle(texture: textureBitmap)
</div>
<div class="code-snippet" id="cs">
    // Solid Brush
    var solidBrushStyle = new SCISolidBrushStyle(UIColor.Red);
    // Linear Gradient Brush
    var linearGradientBrushStyle = new SCILinearGradientBrushStyle(CGPoint.Empty, new CGPoint(0, 1), UIColor.Red, UIColor.Blue);
    // Radial Gradient Brush
    var radialGradientBrushStyle = new SCIRadialGradientBrushStyle(UIColor.Red, UIColor.Blue);
    // Texture Brush - SCIBitmap with Texture should be provided
    SCIBitmap textureBitmap;
    var textureBrushStyle = new SCITextureBrushStyle(textureBitmap);
</div>

## SCIFontStyle
The `SCIFontStyle` type is designed to hold a relevant information related to text, such as:
- Font Size
- Text Color
- Font Descriptor - the  [UIFontDescriptor](https://developer.apple.com/documentation/uikit/uifontdescriptor) instance.

> **_NOTE:_** Since iOS 13, Apple deprecated creation of System Fonts using name. You can learn more about that in [Apple's WWDC - [Font Management] - Presentation](https://developer.apple.com/videos/play/wwdc2019/227/?time=200)

The `SCIFontStyle` type instances can be created in this way:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    UIFontDescriptor *fontDescriptor = [UIFontDescriptor fontDescriptorWithName:@"Courier-Bold" size:14];
    SCIFontStyle *fontStyle = [[SCIFontStyle alloc] initWithFontDescriptor:fontDescriptor andTextColor:UIColor.redColor];
</div>
<div class="code-snippet" id="swift">
    let fontDescriptor = UIFontDescriptor(name: "c", size: 14.0)
    let fontStyle = SCIFontStyle(fontDescriptor: fontDescriptor, andTextColor: .red)
</div>
<div class="code-snippet" id="cs">
    var fontStyle = new SCIFontStyle("SCIFontStyle *fontStyle", 14, UIColor.Red),
</div>