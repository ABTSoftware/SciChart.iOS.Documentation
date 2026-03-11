using Foundation;
using ObjCRuntime;
using UIKit;
using CoreGraphics;
using System;

namespace SciChart.iOS.Binding
{
	//AXIS
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISCIRange { }

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISCIComparable { }

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISCICoordinateCalculator { }

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISCITickProvider { }

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISCITickCoordinatesProvider { }

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISCILabelProvider { }

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISCIString { }
	//

	//Rendering series
	// // Colors
	// [BaseType(typeof(NSObject))]
	// interface SCIColor { }


	// [BaseType(typeof(NSObject))]
	// interface SCIAxisBase
	// {
	// 	[Export("axisAlignment")]
	// 	SCIAxisAlignment AxisAlignment { [Bind("axisAlignment")] get; [Bind("setAxisAlignment:")] set; }

	// 	[Export("axisId", ArgumentSemantic.Copy)]
	// 	string AxisId { get; set; }
	// }

	[BaseType(typeof(NSObject))]
	interface SCIAxisBase
	{
		[Export("axisAlignment")]
		SCIAxisAlignment AxisAlignment { [Bind("axisAlignment")] get; [Bind("setAxisAlignment:")] set; }

		[Export("axisId", ArgumentSemantic.Copy)]
		string AxisId { get; set; }

		// Visible Range
		[Export("visibleRange", ArgumentSemantic.Strong)]
		SCIDoubleRange VisibleRange { get; set; }

		[Export("growBy", ArgumentSemantic.Strong)]
		SCIDoubleRange GrowBy { get; set; }

		[Export("visibleRangeLimit", ArgumentSemantic.Strong)]
		ISCIRange VisibleRangeLimit { get; set; }

		[Export("visibleRangeLimitMode")]
		SCIRangeClipMode VisibleRangeLimitMode { get; set; }

		[Export("minimalZoomConstrain", ArgumentSemantic.Strong)]
		ISCIComparable MinimalZoomConstrain { get; set; }

		[Export("maximumZoomConstrain", ArgumentSemantic.Strong)]
		ISCIComparable MaximumZoomConstrain { get; set; }

		[Export("minorDelta", ArgumentSemantic.Strong)]
		ISCIComparable MinorDelta { get; set; }

		[Export("majorDelta", ArgumentSemantic.Strong)]
		ISCIComparable MajorDelta { get; set; }

		[Export("autoTicks")]
		bool AutoTicks { get; set; }

		[Export("maxAutoTicks")]
		uint MaxAutoTicks { get; set; }

		[Export("minorsPerMajor")]
		uint MinorsPerMajor { get; set; }

		[Export("flipCoordinates")]
		bool FlipCoordinates { get; set; }

		[Export("drawMajorTicks")]
		bool DrawMajorTicks { get; set; }

		[Export("drawMajorGridLines")]
		bool DrawMajorGridLines { get; set; }

		[Export("drawMajorBands")]
		bool DrawMajorBands { get; set; }

		[Export("drawMinorTicks")]
		bool DrawMinorTicks { get; set; }

		[Export("drawMinorGridLines")]
		bool DrawMinorGridLines { get; set; }

		[Export("drawLabels")]
		bool DrawLabels { get; set; }

		[Export("axisTitle", ArgumentSemantic.Copy)]
		string AxisTitle { get; set; }

		[Export("attributedAxisTitle", ArgumentSemantic.Copy)]
		NSAttributedString AttributedAxisTitle { get; set; }

		[Export("autoRange")]
		SCIAutoRange AutoRange { get; set; }

		[Export("tickProvider", ArgumentSemantic.Strong)]
		ISCITickProvider TickProvider { get; set; }

		[Export("tickCoordinatesProvider", ArgumentSemantic.Strong)]
		ISCITickCoordinatesProvider TickCoordinatesProvider { get; set; }

		[Export("labelProvider", ArgumentSemantic.Strong)]
		ISCILabelProvider LabelProvider { get; set; }

		[Export("textFormatting", ArgumentSemantic.Copy)]
		string TextFormatting { get; set; }

		[Export("cursorTextFormatting", ArgumentSemantic.Copy)]
		string CursorTextFormatting { get; set; }

		[Export("majorGridLineStyle", ArgumentSemantic.Strong)]
		SCIPenStyle MajorGridLineStyle { get; set; }

		[Export("minorGridLineStyle", ArgumentSemantic.Strong)]
		SCIPenStyle MinorGridLineStyle { get; set; }

		[Export("axisBandsStyle", ArgumentSemantic.Strong)]
		SCIBrushStyle AxisBandsStyle { get; set; }

		[Export("majorTickLineStyle", ArgumentSemantic.Strong)]
		SCIPenStyle MajorTickLineStyle { get; set; }

		[Export("minorTickLineStyle", ArgumentSemantic.Strong)]
		SCIPenStyle MinorTickLineStyle { get; set; }

		[Export("majorTickLineLength")]
		float MajorTickLineLength { get; set; }

		[Export("minorTickLineLength")]
		float MinorTickLineLength { get; set; }

		[Export("titleStyle", ArgumentSemantic.Strong)]
		SCIFontStyle TitleStyle { get; set; }

		[Export("tickLabelStyle", ArgumentSemantic.Strong)]
		SCIFontStyle TickLabelStyle { get; set; }

		[Export("currentCoordinateCalculator", ArgumentSemantic.Strong)]
		ISCICoordinateCalculator CurrentCoordinateCalculator { get; }

		[Export("axisThickness")]
		nfloat AxisThickness { get; set; }

		[Export("isVisible")]
		bool IsVisible { get; set; }

		[Export("axisViewportDimension")]
		nfloat AxisViewportDimension { get; }

		[Export("hasValidVisibleRange")]
		bool HasValidVisibleRange { get; }

		[Export("hasDefaultVisibleRange")]
		bool HasDefaultVisibleRange { get; }

		[Export("defaultNonZeroRange", ArgumentSemantic.Strong)]
		ISCIRange DefaultNonZeroRange { get; }

		[Export("maximumRange", ArgumentSemantic.Strong)]
		ISCIRange MaximumRange { get; }

		[Export("dataRange", ArgumentSemantic.Strong)]
		ISCIRange DataRange { get; }

		[Export("visibleRangeChangeListener", ArgumentSemantic.Strong)]
		SCIVisibleRangeChangeListener VisibleRangeChangeListener { get; set; }

		[Export("dataRangeChangeListener", ArgumentSemantic.Strong)]
		SCIDataRangeChangeListener DataRangeChangeListener { get; set; }

		// Methods

		[Export("createCoordinateCalculatorFromRange:")]
		ISCICoordinateCalculator CreateCoordinateCalculatorFromRange(ISCIRange visibleRange);

		[Export("animateVisibleRangeTo:withDuration:")]
		void AnimateVisibleRangeTo(ISCIRange range, float duration);

		[Export("isValidRange:")]
		bool IsValidRange(ISCIRange range);

		[Export("onDataRangeChanged")]
		void OnDataRangeChanged();

		[Export("getDataValueFrom:")]
		ISCIComparable GetDataValue(float pixelCoordinate);

		[Export("getCoordinateFrom:")]
		float GetCoordinate(ISCIComparable value);

		[Export("formatText:")]
		ISCIString FormatText(ISCIComparable value);

		[Export("formatCursorText:")]
		ISCIString FormatCursorText(ISCIComparable value);

		[Export("getMaximumRange:")]
		ISCIRange GetMaximumRange(bool forceCacheUpdate);

		[Export("getDataRange:")]
		ISCIRange GetDataRange(bool forceCacheUpdate);
	}

	delegate void SCIVisibleRangeChangeListener(ISCIRange oldRange, ISCIRange newRange);
	delegate void SCIDataRangeChangeListener(ISCIRange oldRange, ISCIRange newRange);

	[BaseType(typeof(SCIAxisBase))]
	interface SCINumericAxis
	{
		[Static]
		[Export("new")]
		SCINumericAxis Create();

		// [Export("visibleRange", ArgumentSemantic.Strong)]
		// SCIDoubleRange VisibleRange { get; set; }
	}

	[BaseType(typeof(NSObject))]
	interface SCIRenderableSeriesBase
	{
		[Export("dataSeries", ArgumentSemantic.Strong)]
		SCIXyDataSeries DataSeries { get; set; }

		[Export("strokeStyle", ArgumentSemantic.Strong)]
		SCIPenStyle StrokeStyle { get; set; }
		[Export("areaStyle", ArgumentSemantic.Strong)]
		SCIBrushStyle AreaStyle { get; set; }
	}

	[BaseType(typeof(SCIBrushStyle))]
	interface SCISolidBrushStyle : SCIBrushStyle
	{
		// UIColor initializer
		[Export("initWithColor:")]
		IntPtr Constructor(UIKit.UIColor color);

		// Hex initializer
		[Export("initWithColorCode:")]
		IntPtr Constructor(uint colorCode);
	}

	[BaseType(typeof(SCIPenStyle))]
	interface SCISolidPenStyle : SCIPenStyle
	{
		// UIColor initializer
		[Export("initWithColor:thickness:")]
		IntPtr Constructor(UIKit.UIColor color, float thickness);

		// Hex initializer
		[Export("initWithColorCode:thickness:")]
		IntPtr Constructor(uint colorCode, float thickness);
	}


	[BaseType(typeof(SCIRenderableSeriesBase))]
	interface SCIFastLineRenderableSeries
	{
		[Static]
		[Export("new")]
		SCIFastLineRenderableSeries Create();
	}

	[BaseType(typeof(NSObject))]
	interface SCIAxisCollection
	{
		[Export("add:")] void Add(NSObject axis);
		[Export("count")]
		nint Count { get; }
	}

	[BaseType(typeof(NSObject))]
	interface SCIDoubleRange : ISCIRange
	{
		[Export("initWithMin:max:")]
		IntPtr Constructor(NSNumber min, NSNumber max);

		[Export("min")]
		NSNumber Min { get; set; }

		[Export("max")]
		NSNumber Max { get; set; }
	}


	[BaseType(typeof(UIView))]
	interface SCIChartSurface
	{
		[Export("initWithFrame:")]
		IntPtr Constructor(CGRect frame);

		[Static]
		[Export("setRuntimeLicenseKey:")]
		void SetRuntimeLicenseKey(string licenseKey);

		[Export("xAxes")]
		SCIAxisCollection XAxes { get; }
		[Export("yAxes")]
		SCIAxisCollection YAxes { get; }
		[Export("renderableSeries")]
		SCIRenderableSeriesCollection RenderableSeries { get; }

		[Export("annotations")]
		SCIAnnotationCollection Annotations { get; }

		[Export("invalidateElement")]
		void InvalidateElement();
	}

	[BaseType(typeof(NSObject))]
	interface SCIXyDataSeries
	{
		// Bind the Objective-C initializer
		[Export("initWithXType:yType:")]
		IntPtr Constructor(SCIDataType xType, SCIDataType yType);

		// Bind the append method
		[Export("appendX:y:")]
		void Append(NSNumber x, NSNumber y);
	}

	[BaseType(typeof(NSObject))]
	interface SCIRenderableSeriesCollection
	{
		[Export("add:")]
		void Add(NSObject series);
		[Export("count")]
		nint Count { get; }
	}

	[BaseType(typeof(SCIRenderableSeriesBase))]
	interface SCIFastMountainRenderableSeries
	{
		[Static]
		[Export("new")]
		SCIFastMountainRenderableSeries Create();
	}

	[BaseType(typeof(NSObject))]
	interface SCIAnnotationCollection
	{
		[Export("add:")]
		void Add(NSObject annotation);
	}

	[BaseType(typeof(NSObject))]
	interface SCIAnnotationBase
	{
		[Export("x1")]
		NSNumber X1 { get; set; }

		[Export("y1")]
		NSNumber Y1 { get; set; }
	}

	[BaseType(typeof(SCIAnnotationBase))]
	interface SCIVerticalLineAnnotation
	{
		[Static]
		[Export("new")]
		SCIVerticalLineAnnotation Create();
	}

	[BaseType(typeof(SCIAnnotationBase))]
	interface SCIHorizontalLineAnnotation
	{
		[Static]
		[Export("new")]
		SCIHorizontalLineAnnotation Create();
	}

	[BaseType(typeof(SCIAnnotationBase))]
	interface SCICustomAnnotation
	{
		// [Export("init")]
		// IntPtr Constructor();

		[Export("customView", ArgumentSemantic.Strong)]
		UIView CustomView { get; set; }

		[Export("horizontalAnchorPoint")]
		SCIHorizontalAnchorPoint HorizontalAnchorPoint { get; set; }

		[Export("verticalAnchorPoint")]
		SCIVerticalAnchorPoint VerticalAnchorPoint { get; set; }
	}

	[BaseType(typeof(NSObject))]
	interface SCIPenStyle
	{
	}

	[BaseType(typeof(NSObject))]
	interface SCIBrushStyle
	{
	}

	[BaseType(typeof(NSObject))]
	interface SCIFontStyle
	{
	}
}
