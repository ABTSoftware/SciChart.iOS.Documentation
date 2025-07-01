# SciChart iOS Tutorial - Accessibility hooks
SciChart for iOS provides accessibility hooks to enhance usability for users relying on assistive technologies like VoiceOver. This guide shows how to enable and customize accessibility on a SCIChartSurface.

#### Use-cases:
- VoiceOver announces axis range when the visible range changes.
- VoiceOver reads out the x/y values when a user selects a data point.
- Tick labels respect Dynamic Type settings.
- Themes adapt to system appearance (Dark/Light Mode).

### Announcing Axis Range Changes
When the user interacts with the chart and the visible range of an axis changes, a VoiceOver announcement is triggered.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
       xAxis.visibleRangeChangeListener = ^(id<ISCIAxisCore> axis, id<ISCIRange> oldRange, id<ISCIRange> newRange, BOOL isAnimating) {
            
            NSString *strRange = [NSString stringWithFormat:@"%.2f to %.2f", newRange.minAsDouble, newRange.maxAsDouble];
            NSString *announcement = [NSString stringWithFormat:@"X Axis range changed, now it's %@", strRange];
            UIAccessibilityPostNotification(UIAccessibilityAnnouncementNotification, announcement);
        };
</div>
<div class="code-snippet" id="swift">
       xAxis.visibleRangeChangeListener = { (_, _, range, _) in
            let strRange = String.init(format: "%0.2f to %0.2f", range.minAsDouble, range.maxAsDouble)
            let announcement = "X Axis range changed, now it's \(strRange)"
            UIAccessibility.post(notification: .announcement, argument: announcement)
        }
</div>

### Making Data Points Accessible
Data points can be exposed to VoiceOver by creating UIAccessibilityElements and assigning them to the chart surface.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
        [self.surface setRenderedListener:^(id surface, id _Nullable unused) {
        SCIChartSurface *chartSurface = (SCIChartSurface*)surface;

        if (![dataSeries isKindOfClass:[SCIXyDataSeries class]]) return;

        NSMutableArray<UIAccessibilityElement *> *accessibilityElements = [NSMutableArray array];

        id<ISCICoordinateCalculator> xCoordCalc = xAxis.currentCoordinateCalculator;

        for (int i = 0; i < dataSeries.count; i++) {
            double xValue = [[dataSeries.xValues valueAt:i] toDouble];
            double yValue = [[dataSeries.yValues valueAt:i] toDouble];
            double xCoord = [xCoordCalc getCoordinateFrom:xValue];

            UIAccessibilityElement *accessibilityElement = [[UIAccessibilityElement alloc] initWithAccessibilityContainer:chartSurface.renderSurface.view];

            NSString *label = [NSString stringWithFormat:@"x value is %.2f and y value is %.2f", xValue, yValue];
            accessibilityElement.accessibilityLabel = label;

            CGRect frame = CGRectMake(xCoord - 15.0, 0.0, 30.0, chartSurface.renderableSeriesArea.view.frame.size.height);
            CGRect screenFrame = [chartSurface.view convertRect:frame toView:nil];
            accessibilityElement.accessibilityFrame = screenFrame;

            [accessibilityElements addObject:accessibilityElement];
        }

        chartSurface.renderSurface.view.accessibilityElements = accessibilityElements;
    }];

</div>
<div class="code-snippet" id="swift">
        surface.setRenderedListener { (surface, _) in
            guard let surface = surface else { return }

            var accessibilityElements = [UIAccessibilityElement]()
            let xCoordCalc = xAxis.currentCoordinateCalculator

            for i in 0 ..< dataSeries.count {
                let xValue = dataSeries.xValues.value(at: i).toDouble()
                let yValue = dataSeries.yValues.value(at: i).toDouble()
                let xCoord = xCoordCalc.getCoordinate(xValue)

                let accessibilityElement = UIAccessibilityElement(accessibilityContainer: surface.renderSurface.view)
                let strRange = String.init(format: "x value is %0.2f and y value is %0.2f", xValue, yValue)
                accessibilityElement.accessibilityLabel = strRange

                let frame = CGRect(
                    x: Double(xCoord - 15),
                    y: 0,
                    width: 30.0,
                    height: Double(surface.renderableSeriesArea.view.frame.size.height)
                )
                accessibilityElement.accessibilityFrame = UIAccessibility.convertToScreenCoordinates(frame, in: surface.view)
                accessibilityElements.append(accessibilityElement)
            }

            surface.renderSurface.view.accessibilityElements = accessibilityElements
        } 
</div>

### Supporting Dynamic Type
Tick label fonts scale automatically with system text size preferences.
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
- (void)viewDidLoad {
    [super viewDidLoad];
    ...
    [[NSNotificationCenter defaultCenter] addObserver:self
                                             selector:@selector(handleContentSizeCategoryChange)
                                                 name:UIContentSizeCategoryDidChangeNotification
                                               object:nil];
}

- (void)handleContentSizeCategoryChange {
    id<ISCIAxis> primaryXAxis = self.surface.xAxes.primaryAxis;
    if (primaryXAxis) {
        primaryXAxis.tickLabelStyle = [self scaledFontStyleFor:primaryXAxis.tickLabelStyle];
    }
    id<ISCIAxis> primaryYAxis = self.surface.yAxes.primaryAxis;
    if (primaryYAxis) {
        primaryYAxis.tickLabelStyle = [self scaledFontStyleFor:primaryYAxis.tickLabelStyle];
    }
}

- (SCIFontStyle *)scaledFontStyleFor:(SCIFontStyle *)fontStyle {
    UIFontDescriptor *fontDescriptor = fontStyle.fontDescriptor;
    UIFontMetrics *fontMetrics = [UIFontMetrics metricsForTextStyle:UIFontTextStyleBody];
    UIFont *scaledFont = [fontMetrics scaledFontForFont:[UIFont fontWithDescriptor:fontDescriptor size:fontDescriptor.pointSize]];

    return [[SCIFontStyle alloc] initWithFontDescriptor:scaledFont.fontDescriptor andTextColor:fontStyle.color];
}
</div>
<div class="code-snippet" id="swift">
    override func viewDidLoad() {
    ...
    NotificationCenter.default.addObserver(
            self,
            selector: #selector(handleContentSizeCategoryChange),
            name: UIContentSizeCategory.didChangeNotification,
            object: nil
        )
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

</div>

### Adapting to System Theme (Dark/Light Mode)
Change chart theme based on system appearance:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    - (void)traitCollectionDidChange:(UITraitCollection *)previousTraitCollection {
    if ([self.traitCollection hasDifferentColorAppearanceComparedToTraitCollection:previousTraitCollection]) {
        if (self.traitCollection.userInterfaceStyle == UIUserInterfaceStyleDark) {
            [SCIThemeManager applyTheme:SCIChartThemeV4Dark toThemeable:self.surface];
        } else {
            [SCIThemeManager applyTheme:SCIChartThemeBrightSpark toThemeable:self.surface];
        }
    }
}
</div>
<div class="code-snippet" id="swift">
    override func traitCollectionDidChange(_ previousTraitCollection: UITraitCollection?) {
        if traitCollection.hasDifferentColorAppearance(comparedTo: previousTraitCollection) {
            
            if traitCollection.userInterfaceStyle == .dark {
                SCIThemeManager.applyTheme(.v4Dark, to: surface)
            } else {
                SCIThemeManager.applyTheme(.brightSpark, to: surface)
            }
        }
    }
</div>