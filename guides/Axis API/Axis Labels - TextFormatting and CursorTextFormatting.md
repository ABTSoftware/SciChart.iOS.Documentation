# Axis Labels - TextFormatting and CursorTextFormatting
All the axis classes obey standard Cocoa formatting strings, calling methods of the [NSFormatter APIs](https://developer.apple.com/documentation/foundation/nsformatter) internally. Thus, standard [Date Formatters](https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/DataFormatting/Articles/dfDateFormatting10_4.html#//apple_ref/doc/uid/TP40002369-SW1) and [Number Formatters](https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/DataFormatting/Articles/dfNumberFormatting10_4.html#//apple_ref/doc/uid/TP40002368-SW1) strings patterns can be applied to format axis labels. There are the `ISCIAxisCore.textFormatting` and `ISCIAxisCore.cursorTextFormatting` properties for this purpose. 

> **_NOTE:_** `NSDateFormatter` and `NSNumberFormatter` relies on [Unicode Technical Standard #35](http://www.unicode.org/reports/tr35/tr35-31/tr35-numbers.html#Number_Format_Patterns)

See possible string patterns provided by [UTS#35](https://unicode.org/reports/tr35/tr35-31/)
- [Number Format Patterns](https://www.unicode.org/reports/tr35/tr35-31/tr35-numbers.html#Number_Format_Patterns)
- [Date Format Patterns](https://www.unicode.org/reports/tr35/tr35-31/tr35-dates.html#Date_Format_Patterns)

## Axis Labels Formatting
**Axis API** allows to assign a formatting string for axis labels. There is the `ISCIAxisCore.textFormatting` property for this. Such formatting can be set in code as shown below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; xAxis = [SCIDateAxis new];
    xAxis.textFormatting = @"dd.MMM";
    id&lt;ISCIAxis&gt; yAxis = [SCINumericAxis new];
    yAxis.textFormatting = @"$0.0";
</div>
<div class="code-snippet" id="swift">
    let xAxis = SCIDateAxis()
    xAxis.textFormatting = "dd.MMM"
    let yAxis = SCINumericAxis()
    yAxis.textFormatting = "$0.0"
</div>
<div class="code-snippet" id="cs">
    var xAxis = new SCIDateAxis();
    xAxis.TextFormatting = "dd.MMM";
    var yAxis = new SCINumericAxis();
    yAxis.TextFormatting = "$0.0";
</div>

![Axis Labels Formatting](img/axis-2d/text-formatting.png)

> **_NOTE:_** Axis labels formatting is also applied to `SCIAxisMarkerAnnotation` labels.

## Axis Cursor Text Formatting
**Axis API** allows to assign a formatting string for axis overlays, such as `SCICursorModifier` axis labels. Similarly to axis labels formatting, there is the `ISCIAxisCore.cursorTextFormatting` property for this. Such formatting can be set in code as shown below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; xAxis = [SCIDateAxis new];
    xAxis.cursorTextFormatting = @"'X Value:'dd.MM.yyyy";
    id&lt;ISCIAxis&gt; yAxis = [SCINumericAxis new];
    yAxis.cursorTextFormatting = @"'Price:'###.##' $'";
</div>
<div class="code-snippet" id="swift">
    let xAxis = SCIDateAxis()
    xAxis.cursorTextFormatting = "'X Value:'dd.MM.yyyy"
    let yAxis = SCINumericAxis()
    yAxis.cursorTextFormatting = "'Price:'###.##' $'"
</div>
<div class="code-snippet" id="cs">
    var xAxis = new SCIDateAxis();
    xAxis.CursorTextFormatting = "'X Value:'dd.MM.yyyy";
    var yAxis = new SCINumericAxis();
    yAxis.CursorTextFormatting = "'Price:'###.##' $'";
</div>

![Axis Cursor Text Formatting](img/axis-2d/cursor-text-formatting.png)

## Numeric Axes and Scientific Notation
It is possible to render axis labels and text in tooltips in a shortened form using **scientific notation (standard form)**. This becomes an issue when working with large numbers. In this case axis size will grow to fit axis labels inside. This is a common issue when working with `SCILogarithmicNumericAxis`.

To configure an axis to show numbers in this form, it is necessary that a [proper format string](https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/DataFormatting/Articles/dfNumberFormatting10_4.html#//apple_ref/doc/uid/TP40002368-SW1) is provided and `SCIScientificNotation` is set to the desired value. There is a `ISCINumericAxis.scientificNotation` property for this purpose. Possible options are listed below:
- `SCIScientificNotation.SCIScientificNotation_None` - the default value.
- `SCIScientificNotation.SCIScientificNotation_Normalized` - assumes 10 as base.
- `SCIScientificNotation.SCIScientificNotation_E` - assumes the number E as base.
- `SCIScientificNotation.SCIScientificNotation_LogarithmicBase` - used with `SCILogarithmicNumericAxis`. Assumes the logarithmic base of the `LogarithmicNumericAxis` as base.

An axis can be configured to use `SCIScientificNotation` like follows:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    id&lt;ISCIAxis&gt; xAxis = [SCILogarithmicNumericAxis new];
    xAxis.logarithmicBase = 10.0;
    xAxis.scientificNotation = SCIScientificNotation_LogarithmicBase;
    xAxis.textFormatting = @"#.#E+0";
    xAxis.cursorTextFormatting = @"#.#E+0";
</div>
<div class="code-snippet" id="swift">
    let xAxis = SCILogarithmicNumericAxis()
    xAxis.logarithmicBase = 10.0
    xAxis.scientificNotation = .logarithmicBase;
    xAxis.textFormatting = "#.#E+0"
    xAxis.cursorTextFormatting = "#.#E+0"
</div>
<div class="code-snippet" id="cs">
    var xAxis = new SCILogarithmicNumericAxis();
    xAxis.LogarithmicBase = 10.0;
    xAxis.ScientificNotation = SCIScientificNotation.LogarithmicBase;
    xAxis.TextFormatting = "#.#E+0";
    xAxis.CursorTextFormatting = "#.#E+0";
</div>

![Axis Cursor Text Formatting](img/axis-2d/log-text-formatting.png)

## Dynamically Changing TextFormatting
For more advanced formatting scenarios, **Axis API** provides a feature called **LabelProviders**. It grants full control over text output of **every axis label**. This can be useful if required to customize textual representation of particular axis labels or replace all of them with other strings based on some logics. Please refer to the [LabelProvider API](axis-labels---labelprovider-api.html) article for further details.