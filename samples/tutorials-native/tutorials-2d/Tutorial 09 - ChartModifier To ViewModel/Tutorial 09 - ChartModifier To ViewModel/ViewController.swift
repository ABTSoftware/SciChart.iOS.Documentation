//
//  ViewController.swift
//  ChartModifierToViewModel
//
//  Created by CN10 on 29/08/25.
//

import UIKit
import SciChart

class ViewController: UIViewController {
    
    private let viewModel = ChartViewModel()
    
    private var surface: SCIChartSurface {
        return view as! SCIChartSurface
    }
    
    override func loadView() {
        viewRespectsSystemMinimumLayoutMargins = false
        view = SCIChartSurface()
    }
    
    override func viewDidLoad() {
        initExample()
    }
    
    func initExample() {
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
        dataSeries.seriesName = "test"
        
        let renderableSeries = SCIFastLineRenderableSeries()
        renderableSeries.dataSeries = dataSeries
        let customSeriesInfoProvider = CustomSeriesInfoProvider()
        customSeriesInfoProvider.delegate = self
        renderableSeries.seriesInfoProvider = customSeriesInfoProvider
        
        
        SCIUpdateSuspender.usingWith(self.surface) {
            self.surface.xAxes.add(xAxis)
            self.surface.yAxes.add(yAxis)
            self.surface.renderableSeries.add(renderableSeries)
            self.surface.chartModifiers.add(items: SCIRolloverModifier())
        }
    }
}

extension ViewController: SeriesInfoReceiverDelegate {
    
    func modifierInteractedWith(seriesInfo: SCIXySeriesInfo) {
        guard let xValue = seriesInfo.xValue?.toDouble(), let yValue = seriesInfo.yValue?.toDouble() else { return }
        viewModel.updateSeriesInfo(seriesInfo.seriesName ?? "Unknown", xValue: xValue, yValue: yValue)
        
        guard let seriesData = viewModel.latestSeriesData else { return }
        print("Series Name: \(seriesData.seriesName), X: \(seriesData.xValue), Y: \(seriesData.yValue)")
    }
}
