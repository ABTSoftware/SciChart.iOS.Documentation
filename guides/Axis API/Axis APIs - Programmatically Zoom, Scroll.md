# Axis APIs - Programmatically Zoom, Scroll

## Programmatically Scrolling an Axis
SciChart provides a clean and [simple APIs](Chart Modifier APIs.html) to interact with a single Axis programmatically, e.g. to **Zoom**, **Scroll** or **Pan** an axis. This can come in useful when creating custom `SCIChartModifierBase` derived classes e.g. [custom zooming or panning](custom-modifiers---the-scichartmodifierbase-api.html) of the chart.

## ISCIAxis Interactivity Methods
The following methods can be found on `ISCIAxis` and `SCIAxisBase`, a shared base-class for 2D Axis types in SciChart:
- `-[ISCIAxis scrollByPixels:clipMode:]`
- `-[ISCIAxis scrollByPixels:clipMode:duration:]`
- `-[ISCIAxis scrollByPixels:clipMode:clipTarget:]`
- `-[ISCIAxis scrollByPixels:clipMode:clipTarget:duration:]`
- `-[ISCIAxis zoomFrom:to:]`
- `-[ISCIAxis zoomFrom:to:duration:]`
- `-[ISCIAxis zoomByFractionMin:max:]`
- `-[ISCIAxis zoomByFractionMin:max:duration:]`

Using these methods you can programmatically **zoom, scroll (pan)** a specific axis.  You can read more information about the above methods in [API documentation](Protocols/ISCIAxis.html).