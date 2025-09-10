import UIKit
struct SeriesData {
    let seriesName: String
    let xValue: Double
    let yValue: Double
}

class ChartViewModel {
    var latestSeriesData: SeriesData?

    func updateSeriesInfo(_ seriesName: String, xValue: Double, yValue: Double) {
        let data = SeriesData(seriesName: seriesName, xValue: xValue, yValue: yValue)
        latestSeriesData = data
    }
}
