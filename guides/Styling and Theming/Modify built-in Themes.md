# Modify built-in Themes
Maybe you don’t want to create an entire [custom theme](create-a-custom-theme.html), but just want to **override one color or brush** from one of our standard themes or your custom theme. 

This can be achieved by adding a special **`basedOn`** Property list key to your new theme with the **"parent theme name"** as a string value. 

> **_NOTE:_** The full list of our standard themes can be found in [Styling and Theming](Styling%20and%20Theming.html) article.

As an example let's take our `SCIChartThemeV4Dark` theme as a parent one and change few properties there.

First, what you need to do is to create a new Property list file and add **`basedOn`** key with your **"parent theme name"** as a string value.

Next, add the keys you want to override, like this:

**MyModifiedTheme.plist**

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
	<key>basedOn</key>
	<string>SCIChartThemeV4Dark</string>
	<key>sciChartBackground</key>
	<dict>
		<key>solid</key>
		<string>#FF2F313E</string>
	</dict>
	<key>renderableSeriesAreaFillColor</key>
	<string>#FF2A2738</string>
	<key>minorGridLineColor</key>
	<string>#00000000</string>
	<key>axisBandsColor</key>
	<string>#00000000</string>
	<key>lineSeriesColor</key>
	<string>#FF4DB7F3</string>
	<key>tickTextColor</key>
	<string>#FF4DB7F3</string>
</dict>
</plist>
```

Now, you can add and apply your newly created theme as you would do with any [Custom Theme](create-a-custom-theme.html):

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">

    static SCIChartTheme const MyModifiedTheme = @"MyModifiedTheme";

    [SCIThemeManager addTheme:MyModifiedTheme fromBundle:[NSBundle mainBundle]];
    [SCIThemeManager applyTheme:MyModifiedTheme toThemeable:self.surface];
</div>
<div class="code-snippet" id="swift">

    extension SCIChartTheme {
        static let modifiedTheme: SCIChartTheme = SCIChartTheme(rawValue: "MyModifiedTheme")
    }

    SCIThemeManager.addTheme(.modifiedTheme, from: Bundle.main)
    SCIThemeManager.applyTheme(.modifiedTheme, to: self.surface)
</div>
<div class="code-snippet" id="cs">
    private const string MyModifiedTheme = "MyModifiedTheme";

    SCIThemeManager.AddTheme(MyModifiedTheme, Foundation.NSBundle.MainBundle);
    SCIThemeManager.ApplyTheme(MyModifiedTheme, Surface);
</div>

Here is the result:

![Modified Theme](img/styling-and-theming/modified-theme-example.png)