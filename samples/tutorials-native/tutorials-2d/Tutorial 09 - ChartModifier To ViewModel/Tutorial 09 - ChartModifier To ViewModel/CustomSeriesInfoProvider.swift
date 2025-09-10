import SciChart.Protected.SCISeriesTooltipBase
import SciChart.Protected.SCISeriesInfoProviderBase

protocol SeriesInfoReceiverDelegate: AnyObject {
    func modifierInteractedWith(seriesInfo: SCIXySeriesInfo)
}

class CustomSeriesInfoProvider: SCIDefaultXySeriesInfoProvider {
    weak var delegate: SeriesInfoReceiverDelegate?
    
    class CustomXySeriesTooltip: SCIXySeriesTooltip {
        weak var tooltipDelegate: SeriesInfoReceiverDelegate?
        
        override func internalUpdate(with seriesInfo: SCIXySeriesInfo) {
            super.internalUpdate(with: seriesInfo)
            tooltipDelegate?.modifierInteractedWith(seriesInfo: seriesInfo)
        }
    }
    
    override func getSeriesTooltipInternal(seriesInfo: SCIXySeriesInfo, modifierType: AnyClass) -> ISCISeriesTooltip {
        // Replace SCITooltipModifier with the specific modifier class you are using (e.g. SCITooltipModifier)
        if (modifierType == SCIRolloverModifier.self) {
            let customXySeriesTooltip = CustomXySeriesTooltip(seriesInfo: seriesInfo)
            customXySeriesTooltip.tooltipDelegate = delegate
            return customXySeriesTooltip
        } else {
            return super.getSeriesTooltipInternal(seriesInfo: seriesInfo, modifierType: modifierType)
        }
    }
}
