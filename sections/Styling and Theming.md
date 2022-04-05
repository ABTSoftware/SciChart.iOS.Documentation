SciChart ships with **8 stunning themes** which you can select and apply to the charts in your application. Most of the components of SciChart are also stylable, so you can truly customize the chart to fit your application.

The 8 built-in themes are shown below. You can also create your own [custom theme](create-a-custom-theme.html), or [modify ours](modify-built-in-themes.html) to meet your needs.

| **Theme Name**           | **Associated theme object**           | **Result when Applied**                                                         |
| ------------------------ | ---------------------------------- | ------------------------------------------------------------------------------- |
| v4 Dark            | Obj-C: `SCIChartThemeV4Dark` <br /> Swift: [`SCIChartTheme.v4Dark`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeV4Dark)   | <img src="img/styling-and-theming/v4-dark-theme.png" alt="v4 Dark Theme" width="500"/> | Oscilloscope       | Obj-C: `SCIChartThemeOscilloscope` <br /> Swift: [`SCIChartTheme.oscilloscope`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeOscilloscope)   | <img src="img/styling-and-theming/oscilloscope-theme.png" alt="Oscilloscope Theme" width="500"/> |
| Expression Light   | Obj-C: `SCIChartThemeExpressionLight` <br /> Swift: [`SCIChartTheme.expressionLight`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeExpressionLight) | <img src="img/styling-and-theming/expression-light-theme.png" alt="Expression Light Theme" width="500"/>  |
| Expression Dark    | Obj-C: `SCIChartThemeExpressionDark` <br /> Swift: [`SCIChartTheme.expressionDark`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeExpressionDark)  | <img src="img/styling-and-theming/expression-dark-theme.png" alt="Expression Dark Theme" width="500"/> |
| Electric           | Obj-C: `SCIChartThemeElectric` <br /> Swift: [`SCIChartTheme.electric`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeElectric)        | <img src="img/styling-and-theming/electric-theme.png" alt="Electric Theme" width="500"/> |
| Chrome             | Obj-C: `SCIChartThemeChrome` <br /> Swift: [`SCIChartTheme.chrome`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeChrome)          | <img src="img/styling-and-theming/chrome-theme.png" alt="Chrome Theme" width="500"/> |
| Bright Spark       | Obj-C: `SCIChartThemeBrightSpark` <br /> Swift: [`SCIChartTheme.brightSpark`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeBrightSpark)    | <img src="img/styling-and-theming/bright-spark-theme.png" alt="Bright Spark Theme" width="500"/> |
| Black Steel        | Obj-C: `SCIChartThemeBlackSteel` <br /> Swift: [`SCIChartTheme.blackSteel`](Other%20Constants.html#/c:SCIThemeManager.h@SCIChartThemeBlackSteel)      | <img src="img/styling-and-theming/black-steel-theme.png" alt="Black Steel Theme" width="500"/> |

#### Applying a Theme to the SciChartSurface
To apply a theme to a `SCIChartSurface`, simply use the following code. Allowable theme objects are available in the table above:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    [SCIThemeManager applyTheme:SCIChartThemeV4Dark toThemeable:self.surface];
</div>
<div class="code-snippet" id="swift">
    SCIThemeManager.applyTheme(.v4Dark, to: self.surface)
</div>
<div class="code-snippet" id="cs">
    SCIThemeManager.ApplyTheme(SCIThemeManager.V4Dark, Surface);
</div>

## See Also
- [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html)
- [Templating and Styling Chart Parts](templating-and-styling-chart-parts.html)
- [Modify built-in Themes](modify-built-in-themes.html)
- [Create a Custom Theme](create-a-custom-theme.html)
