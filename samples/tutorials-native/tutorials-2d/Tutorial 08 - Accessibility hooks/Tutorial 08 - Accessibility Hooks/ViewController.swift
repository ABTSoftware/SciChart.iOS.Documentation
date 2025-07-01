import UIKit
import SciChart

class ViewController: UIViewController {
    
    private var surface: SCIChartSurface {
        return view as! SCIChartSurface
    }
    
    override func loadView() {
        viewRespectsSystemMinimumLayoutMargins = false
        view = SCIChartSurface()
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        let xAxis = SCINumericAxis()
        xAxis.growBy = SCIDoubleRange(min: 0.05, max: 0.05)
        let yAxis = SCINumericAxis()
        yAxis.growBy = SCIDoubleRange(min: 0.05, max: 0.05)
        
        let count = 1000
        let xValues = SCIDoubleValues(capacity: count)
        let yValues = SCIDoubleValues(capacity: count)
        for i in 0 ..< count {
            let x: Double = 10.0 * Double(i) / Double(count)
            let y: Double = sin(2 * x)
            xValues.add(x)
            yValues.add(y)
        }
        
        let dataSeries = SCIXyDataSeries(xType: .double, yType: .double)
        dataSeries.append(x: xValues, y: yValues)
        
        let renderableSeries = SCIFastLineRenderableSeries()
        renderableSeries.dataSeries = dataSeries
        
        SCIUpdateSuspender.usingWith(self.surface) {
            self.surface.xAxes.add(xAxis)
            self.surface.yAxes.add(yAxis)
            self.surface.renderableSeries.add(renderableSeries)
            self.surface.chartModifiers.add(items: SCIZoomPanModifier(), SCIPinchZoomModifier(), SCIZoomExtentsModifier())
        }
        
        let currentStyle = traitCollection.userInterfaceStyle
        if currentStyle == .dark {
            SCIThemeManager.applyTheme(.v4Dark, to: surface)
        } else {
            SCIThemeManager.applyTheme(.brightSpark, to: surface)
        }
        
        NotificationCenter.default.addObserver(
            self,
            selector: #selector(handleContentSizeCategoryChange),
            name: UIContentSizeCategory.didChangeNotification,
            object: nil
        )
        
        xAxis.visibleRangeChangeListener = { (_, _, range, _) in
            let strRange = String.init(format: "%0.2f to %0.2f", range.minAsDouble, range.maxAsDouble)
            let announcement = "X Axis range changed, now it's \(strRange)"
            DispatchQueue.main.async {
                UIAccessibility.post(notification: .announcement, argument: announcement)
            }
        }
        
        //Chart announcement
        configureAccessibility()
    }
    
    override var accessibilityCustomActions: [UIAccessibilityCustomAction]? {
        get {
            return [
                UIAccessibilityCustomAction(
                    name: "Zoom to Extents",
                    target: self,
                    selector: #selector(zoomExtentsCustomAction)
                )
            ]
        }
        set {
            super.accessibilityCustomActions = newValue
        }
    }
    
    @objc func zoomExtentsCustomAction() -> Bool {
        surface.zoomExtents()
        return true
    }
    
    override func traitCollectionDidChange(_ previousTraitCollection: UITraitCollection?) {
        if traitCollection.hasDifferentColorAppearance(comparedTo: previousTraitCollection) {
            
            if traitCollection.userInterfaceStyle == .dark {
                SCIThemeManager.applyTheme(.v4Dark, to: surface)
            } else {
                SCIThemeManager.applyTheme(.brightSpark, to: surface)
            }
        }
    }
    
    @objc func handleContentSizeCategoryChange() {
        // Update fonts if necessary
        if let primaryXAxis = surface.xAxes.primaryAxis {
            primaryXAxis.tickLabelStyle = scaledFontStyle(for: primaryXAxis.tickLabelStyle)
        }
        if let primaryYAxis = surface.yAxes.primaryAxis {
            primaryYAxis.tickLabelStyle = scaledFontStyle(for: primaryYAxis.tickLabelStyle)
        }
    }
    
    func scaledFontStyle(for fontStyle: SCIFontStyle) -> SCIFontStyle {
        let fontDescriptor = fontStyle.fontDescriptor
        let fontMetrics = UIFontMetrics(forTextStyle: UIFont.TextStyle.body)
        let font = fontMetrics.scaledFont(for: UIFont(descriptor: fontDescriptor, size: fontDescriptor.pointSize))
        
        return SCIFontStyle(fontDescriptor: font.fontDescriptor, andTextColor: fontStyle.color)
    }
    
    private func configureAccessibility() {
        // Make the chart surface accessible
        surface.isAccessibilityElement = true
        
        // Descriptive label
        surface.accessibilityLabel = "Sine Wave Line Chart"
        
        // Hint about what the chart shows
        surface.accessibilityHint = "Displays a sine wave with 1000 data points from X equals 0 to 10."
        
        // Additional value information
        surface.accessibilityValue = "Y values range from negative one to one, with smooth curve transitions."
    }
}

