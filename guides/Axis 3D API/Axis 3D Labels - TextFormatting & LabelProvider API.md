# Axis 3D Labels - TextFormatting & LabelProvider API
**[Axes in SciChart 3D](Axis 3D APIs.html)** shares the same `SCIAxisCore` base class with **[SciChart 2D Axes](Axis APIs.html)**.
Many of the **AxisCore** features are shared. 
For your convenience, some of the documentation has been duplicated here, with some referring to other sections of the user manual.

## Axis 3D TextFormatting
All the axis classes obey standard Cocoa formatting strings, calling methods of the [NSFormatter APIs](https://developer.apple.com/documentation/foundation/nsformatter) internally. Thus, standard [Date Formatters](https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/DataFormatting/Articles/dfDateFormatting10_4.html#//apple_ref/doc/uid/TP40002369-SW1) and [Number Formatters](https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/DataFormatting/Articles/dfNumberFormatting10_4.html#//apple_ref/doc/uid/TP40002368-SW1) strings patterns can be applied to format axis labels. There are the `ISCIAxisCore.textFormatting` and `ISCIAxisCore.cursorTextFormatting` properties for this purpose. 

> **_NOTE:_** `NSDateFormatter` and `NSNumberFormatter` relies on [Unicode Technical Standard #35](http://www.unicode.org/reports/tr35/tr35-31/tr35-numbers.html#Number_Format_Patterns)

See possible string patterns provided by [UTS#35](https://unicode.org/reports/tr35/tr35-31/)
- [Number Format Patterns](https://www.unicode.org/reports/tr35/tr35-31/tr35-numbers.html#Number_Format_Patterns)
- [Date Format Patterns](https://www.unicode.org/reports/tr35/tr35-31/tr35-dates.html#Date_Format_Patterns)

> **_NOTE:_** The **TextFormatting** is a shared API between SciChart 2D and SciChart 3D. For a full walk-through including code-samples, please see the [Axis Labels - TextFormatting and CursorTextFormatting](axis-labels---textformatting-and-cursortextformatting.html) article.

## Axis 3D LabelProvider API
The [LabelProvider API](axis-labels---labelprovider-api.html) allows full control over the formatting of `SCIAxisCore` text labels, over and above what you can achieve using [TextFormatting](axis-3d-labels---textformatting--labelprovider-api.html).

Use a **LabelProvider** when you want to:

- Have fine grained control over Axis Text or Cursor Labels, depending on numeric (or date) values.
- Display strings on the XAxis, e.g. “Bananas”, “Oranges”, “Apples” and not “1”, “2”, “3”.
- Dynamically change the `ISCIAxisCore.textFormatting` as you zoom in or out.
- Dynamically change the `ISCIAxisCore.textFormatting` depending on Data-value.

> **_NOTE:_** The **LabelProvider API** is a shared API between SciChart 2D and SciChart 3D. For a full walk-through of the AxisCore LabelProvider API, including code-samples, please see [Axis Labels - LabelProvider API](axis-labels---labelprovider-api.html) article.