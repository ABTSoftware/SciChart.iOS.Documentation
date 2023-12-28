# SCILegendModifier
In SciChart, the easiest way to add a **Legend** onto a chart is to use a `SCILegendModifier`

![Legend Modifier](img/modifiers-2d/legend-chart-example.png)

> **_NOTE:_** Example of the `SCILegendModifier` usage can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart/ios-chart-legends-api-example/)
> - [Xamarin Example](https://www.scichart.com/example/xamarin-chart/xamarin-chart-with-legends-example/)

The `SCILegendModifier` class exposes several **configurational** properties. Please find them explained in the table below:

| **Feature**                           | **Description**                                                                 |
| ------------------------------------- | ------------------------------------------------------------------------------- |
| `SCILegendModifier.position`          | Allows to specify **`SCIAlignment`** for the Legend.                            |
| `SCILegendModifier.margins`           | Allows to specify **margins** `UIEdgeInsets` for the Legend.                    |
| `SCILegendModifier.orientation`       | Determines **orientation** of the Legend. Can be either Horizontal or Vertical. |
| `SCILegendModifier.showLegend`        | Allows to **hide or show** the Legend.                                          |
| `SCILegendModifier.showCheckBoxes`    | Determines whether to show **visibility checkboxes** for every [RenderableSeries](renderableseries-apis.html) in the Legend or not. These allow hiding or showing their corresponding RenderableSeries. |
| `SCILegendModifier.showSeriesMarkers` | Determines whether to show **colored markers** for every [RenderableSeries](renderableseries-apis.html) in the Legend or not.                                                                           |
| `SCILegendModifier.sourceMode`        | Allows to specify which [RenderableSeries](renderableseries-apis.html) should appear in the Legend, e.g. Visible, Selected, etc. Series. Other will be ignored by the modifier. Expects a member of the `SCISourceMode` enumeration. |

## Adding the SCILegendModifier to a Chart
Any [Chart Modifier](Chart Modifier APIs.html) can be [added](Chart Modifier APIs.html#adding-a-chart-modifier)  to a `SCIChartSurface` via the`ISCIChartSurface.chartModifiers` property and `SCILegendModifier` is no difference.

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    id&lt;ISCIChartSurface&gt; surface;

    // Create a Modifier
    SCILegendModifier *legendModifier = [SCILegendModifier new];
    legendModifier.position = SCIAlignment_Top | SCIAlignment_Left;
    legendModifier.margins = UIEdgeInsetsMake(16, 16, 16, 16);
    legendModifier.sourceMode = SCISourceMode_AllSeries;
    legendModifier.orientation = SCIOrientationHorizontal;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:legendModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a Modifier
    let legendModifier = SCILegendModifier()
    legendModifier.position = [.left, .top]
    legendModifier.margins = UIEdgeInsets(top: 16, left: 16, bottom: 16, right: 16)
    legendModifier.sourceMode = .allVisibleSeries
    legendModifier.orientation = .horizontal

    // Add the modifier to the surface
    self.surface.chartModifiers.add(legendModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    IISCIChartSurface surface;

    // Create a Modifier
    var legendModifier = new SCILegendModifier();
    legendModifier.Position = SCIAlignment.Top | SCIAlignment.Left;
    legendModifier.Margins = new UIEdgeInsets(16, 16, 16, 16);
    legendModifier.SourceMode = SCISourceMode.AllSeries;
    legendModifier.Orientation = SCIOrientation.Horizontal;

    // Add the modifier to the surface
    Surface.ChartModifiers.Add(legendModifier);
</div>

> **_NOTE:_** To learn more about features available, [Chart Modifier APIs](Chart Modifier APIs.html#common-chart-modifier-features) article.

## Placing Legend outside the Chart
To place a **Legend** outside the chart you will need to create the following objects:
1. `SCIChartLegend` programmatically or using Interface Builder.
2. `SCILegendDataSource` instance.
3. `SCILegendModifier` instance with `useAutoPlacement = false` parameter using the `-[SCILegendModifier initWithLegend:dataSource:useAutoPlacement:]` initializer.

> **_NOTE:_** By passing `false` to `useAutoPlacement` argument you indicate that you want to place `SCIChartLegend` instance manually, in this case - outside the Chart Surface.

While placing a Legend you might want to use your custom constraints.
There are two most common approaches to define Legend frame:
- [Resizing according to the legend content](#resizing-according-to-the-legend-content)
- [Fill all available space defined by the constraints](#fill-all-available-space-defined-by-the-constraints)

#### Resizing according to the legend content
By default, we calculate Legend size according to its content.
To achieve this, we use some under the hood calculations and apply constraints with priority **800-801**.
If you want to allow the Legend to be resized according to its content, a priority of your width and height constraints should be lower than **800**.

As an example, let's assume that you want to place a Legend with the following requirements:
- it should be placed at the **Top** and **Leading** of the parent view
- size should be calculated according to its content.
- your `SCIChartSurface` should be placed trailing to the Legend and fill all available space.

To achieve this you need to add leading and top constraints with a priority **higher than 801**, e.g. 1000,
trailing and bottom constraints with a priority **less than 800**, e.g. 500 to the Legend.

Please see an example below where we create `SCIChartLegend` and `SCIChartSurface` programmatically:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create Chart Surface
    SCIChartSurface *surface = [SCIChartSurface new];
    [self addSubview:surface];
    
    // Create surface constraints
    surface.translatesAutoresizingMaskIntoConstraints = NO;
    NSLayoutConstraint *topSurfaceConstraint = [surface.topAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.topAnchor];
    NSLayoutConstraint *trailingSurfaceConstraint = [surface.trailingAnchor constraintEqualToAnchor: self.trailingAnchor];
    NSLayoutConstraint *bottomSurfaceConstraint = [surface.bottomAnchor constraintEqualToAnchor: self.bottomAnchor];
    
    // Create a legend
    SCIChartLegend *legend = [SCIChartLegend new];
    SCIView *container = legend.container;
    [self addSubview:container];
    
    // Create legend constraints
    container.translatesAutoresizingMaskIntoConstraints = NO;
    NSLayoutConstraint *leadingLegendConstraint = [container.leadingAnchor constraintEqualToAnchor: self.leadingAnchor];
    NSLayoutConstraint *topLegendConstraint = [container.topAnchor constraintEqualToAnchor: self.safeAreaLayoutGuide.topAnchor];
    NSLayoutConstraint *trailingLegendConstraint = [container.trailingAnchor constraintEqualToAnchor: surface.leadingAnchor];
    
    // By setting constraint priority to value lower than 800 you indicate that the legend will ignore this constraint and calculate its width or height automatically. Hence these constraints will be ignored, adding them here is optional.
    NSLayoutConstraint *bottomLegendConstraint = [container.bottomAnchor constraintEqualToAnchor: self.bottomAnchor];
    bottomLegendConstraint.priority = 500;

    NSLayoutConstraint *widthLegendConstraint = [container.widthAnchor constraintEqualToConstant:300];
    widthLegendConstraint.priority = 500;
    [container addConstraint:widthLegendConstraint];
    
    // Add constraints to a superview
    [self addConstraints:@[topSurfaceConstraint, trailingSurfaceConstraint, bottomSurfaceConstraint, leadingLegendConstraint, topLegendConstraint, trailingLegendConstraint/*, bottomLegendConstraint*/]];
    
    // Create a dataSource
    SCISeriesInfoLegendDataSource *dataSource = [[SCISeriesInfoLegendDataSource alloc] initWithLegend:legend];
    
    // Create a modifier
    SCILegendModifier *legendModifier = [[SCILegendModifier alloc] initWithLegend:legend dataSource:dataSource useAutoPlacement:NO];
    
    // Add the modifier to the surface
    [self.surface.chartModifiers add:legendModifier];
</div>
<div class="code-snippet" id="swift">
    // Create Chart Surface
    let surface = SCIChartSurface()
    self.addSubview(surface)

    // Create surface constraints
    surface.translatesAutoresizingMaskIntoConstraints = false
    let topSurfaceConstraint = surface.topAnchor.constraint(equalTo: self.safeAreaLayoutGuide.topAnchor)
    let trailingSurfaceConstraint = surface.trailingAnchor.constraint(equalTo: self.trailingAnchor)
    let bottomSurfaceConstraint = surface.bottomAnchor.constraint(equalTo: self.bottomAnchor)

    // Create a legend
    let legend = SCIChartLegend()
    guard let container = legend.container else { return }
    self.addSubview(container)

    // Create legend constraints
    container.translatesAutoresizingMaskIntoConstraints = false
    let leadingLegendConstraint = container.leadingAnchor.constraint(equalTo: self.leadingAnchor)
    let topLegendConstraint = container.topAnchor.constraint(equalTo: self.safeAreaLayoutGuide.topAnchor)
    let trailingLegendConstraint = container.trailingAnchor.constraint(equalTo: surface.leadingAnchor)

    // By setting constraint priority to value lower than 800 you indicate that the legend will ignore this constraint and calculate its width or height automatically. Hence these constraints will be ignored, adding them here is optional.
    let bottomLegendConstraint = container.bottomAnchor.constraint(equalTo: self.bottomAnchor)
    bottomLegendConstraint.priority = UILayoutPriority(rawValue: 500)

    let widthLegendConstraint = container.widthAnchor.constraint(equalToConstant: 300)
    widthLegendConstraint.priority = UILayoutPriority(rawValue: 500)
    container.addConstraint(widthLegendConstraint)

    // Add constraints to a superview
    self.addConstraints([topSurfaceConstraint, trailingSurfaceConstraint, bottomSurfaceConstraint, leadingLegendConstraint, topLegendConstraint, trailingLegendConstraint, bottomLegendConstraint])

    // Create a dataSource
    guard let dataSource = SCISeriesInfoLegendDataSource(legend: legend) else { return }

    // Create a modifier
    let legendModifier = SCILegendModifier(legend: legend, dataSource: dataSource, useAutoPlacement: false)

    // Add the modifier to the surface
    surface.chartModifiers.add(legendModifier)
</div>
<div class="code-snippet" id="cs">
    // Create Chart Surface
    var surface = new SCIChartSurface();
    View.AddSubview(surface);

    // Create surface constraints
    surface.TranslatesAutoresizingMaskIntoConstraints = false;

    var topSurfaceConstraint = surface.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor);
    var trailingSurfaceConstraint = surface.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor);
    var bottomSurfaceConstraint = surface.BottomAnchor.ConstraintEqualTo(View.BottomAnchor);

    // Create a legend
    var legend = new SCIChartLegend();
    View.AddSubview(legend);

    // Create legend constraints
    legend.TranslatesAutoresizingMaskIntoConstraints = false;
    var leadingLegendConstraint = legend.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor);
    var topLegendConstraint = legend.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor);
    var trailingLegendConstraint = legend.TrailingAnchor.ConstraintEqualTo(surface.LeadingAnchor);

    // By setting constraint priority to value lower than 800 you indicate that the legend will ignore this constraint and calculate its width or height automatically. Hence these constraints will be ignored, adding them here is optional.
    var bottomLegendConstraint = legend.BottomAnchor.ConstraintEqualTo(View.BottomAnchor);
    bottomLegendConstraint.Priority = 500;

    var widthLegendConstraint = legend.WidthAnchor.ConstraintEqualTo(300);
    widthLegendConstraint.Priority = 500;
    legend.AddConstraint(widthLegendConstraint);

    // Add constraints to a superview
    View.AddConstraints(new NSLayoutConstraint[] { topSurfaceConstraint, trailingSurfaceConstraint, bottomSurfaceConstraint, leadingLegendConstraint, topLegendConstraint, trailingLegendConstraint, bottomLegendConstraint });

    // Create a dataSource
    var dataSource = new SCISeriesInfoLegendDataSource(legend);

    // Create a modifier
    var legendModifier = new SCILegendModifier(legend: legend, dataSource: dataSource, useAutoPlacement: false);

    // Add the modifier to the surface
    surface.ChartModifiers.Add(legendModifier);
</div>

This produces the following output:
![Legend Modifier](img/modifiers-2d/legend-outside-chart-autoresizing.png)

#### Fill all available space defined by the constraints
Sometimes you might want to place a legend as a normal view with a fixed width or height.
To achieve this, the priority of all your constraints must be **higher than 801**. 

As an example, let's assume that you want to place a Legend with the following requirements:
- at the top of the parent view with leading, top, trailing constraints
- with fixed height equal to **70**.
- `SCIChartSurface` instance should be below and fill all available space.
- in case the content of a **Legend** is bigger than its height user can scroll it as a normal collection view.

Please see the code on how to achieve that below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create a legend
    SCIChartLegend *legend = [SCIChartLegend new];
    [self addSubview:legend];

    // Create legend constraints
    legend.translatesAutoresizingMaskIntoConstraints = NO;
    NSLayoutConstraint *leadingLegendConstraint = [legend.leadingAnchor constraintEqualToAnchor:self.leadingAnchor];
    NSLayoutConstraint *topLegendConstraint = [legend.topAnchor constraintEqualToAnchor: self.safeAreaLayoutGuide.topAnchor];
    NSLayoutConstraint *trailingLegendConstraint = [legend.trailingAnchor constraintEqualToAnchor: self.trailingAnchor];

    NSLayoutConstraint *heightLegendConstraint = [legend.heightAnchor constraintEqualToConstant:70];
    [legend addConstraint:heightLegendConstraint];

    // Create Chart Surface
    SCIChartSurface *surface = [SCIChartSurface new];
    [self addSubview:surface];

    // Create surface constraints
    surface.translatesAutoresizingMaskIntoConstraints = NO;
    NSLayoutConstraint *topSurfaceConstraint = [surface.topAnchor constraintEqualToAnchor: legend.bottomAnchor];
    NSLayoutConstraint *leadingSurfaceConstraint = [surface.leadingAnchor constraintEqualToAnchor: self.leadingAnchor];
    NSLayoutConstraint *trailingSurfaceConstraint = [surface.trailingAnchor constraintEqualToAnchor: self.trailingAnchor];
    NSLayoutConstraint *bottomSurfaceConstraint = [surface.bottomAnchor constraintEqualToAnchor: self.bottomAnchor];

    // Add constraints to a superview
    [self addConstraints:@[leadingLegendConstraint, topLegendConstraint, trailingLegendConstraint, topSurfaceConstraint, leadingSurfaceConstraint, trailingSurfaceConstraint, bottomSurfaceConstraint]];
    
    // Create a dataSource
    SCISeriesInfoLegendDataSource *dataSource = [[SCISeriesInfoLegendDataSource alloc] initWithLegend:legend];

    // Create a modifier
    SCILegendModifier *legendModifier = [[SCILegendModifier alloc] initWithLegend:legend dataSource:dataSource useAutoPlacement:NO];
    
    // Add the modifier to the surface
    [self.surface.chartModifiers add:legendModifier];
</div>
<div class="code-snippet" id="swift">
    // Create a legend
    let legend = SCIChartLegend()
    self.addSubview(legend)

    // Create legend constraints
    legend.translatesAutoresizingMaskIntoConstraints = false
    let leadingLegendConstraint = legend.leadingAnchor.constraint(equalTo: self.leadingAnchor)
    let topLegendConstraint = legend.topAnchor.constraint(equalTo: self.safeAreaLayoutGuide.topAnchor)
    let trailingLegendConstraint = legend.trailingAnchor.constraint(equalTo: self.trailingAnchor)

    let heightLegendConstraint = legend.heightAnchor.constraint(equalToConstant: 70)
    legend.addConstraint(heightLegendConstraint)

    // Create Chart Surface
    let surface = SCIChartSurface()
    self.addSubview(surface)

    // Create surface constraints
    surface.translatesAutoresizingMaskIntoConstraints = false
    let topSurfaceConstraint = surface.topAnchor.constraint(equalTo: legend.bottomAnchor)
    let leadingSurfaceConstraint = surface.leadingAnchor.constraint(equalTo: self.leadingAnchor)
    let trailingSurfaceConstraint = surface.trailingAnchor.constraint(equalTo: self.trailingAnchor)
    let bottomSurfaceConstraint = surface.bottomAnchor.constraint(equalTo: self.bottomAnchor)

    // Add constraints to a superview
    self.addConstraints([leadingLegendConstraint, topLegendConstraint, trailingLegendConstraint, topSurfaceConstraint, leadingSurfaceConstraint, trailingSurfaceConstraint, bottomSurfaceConstraint])

    // Create a dataSource
    guard let dataSource = SCISeriesInfoLegendDataSource(legend: legend) else {
        return
    }

    // Create a modifier
    let legendModifier = SCILegendModifier(legend: legend, dataSource: dataSource, useAutoPlacement: false)

    // Add the modifier to the surface
    surface.chartModifiers.add(legendModifier)
</div>
<div class="code-snippet" id="cs">
    // Create a legend
    var legend = new SCIChartLegend();
    View.AddSubview(legend);

    // Create legend constraints
    legend.TranslatesAutoresizingMaskIntoConstraints = false;
    var leadingLegendConstraint = legend.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor);
    var topLegendConstraint = legend.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor);
    var trailingLegendConstraint = legend.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor);

    var heightLegendConstraint = legend.HeightAnchor.ConstraintEqualTo(70);
    legend.AddConstraint(heightLegendConstraint);

    // Create Chart Surface
    var surface = new SCIChartSurface();
    View.AddSubview(surface);

    // Create surface constraints
    surface.TranslatesAutoresizingMaskIntoConstraints = false;

    var topSurfaceConstraint = surface.TopAnchor.ConstraintEqualTo(legend.BottomAnchor);
    var leadingSurfaceConstraint = surface.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor);
    var trailingSurfaceConstraint = surface.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor);
    var bottomSurfaceConstraint = surface.BottomAnchor.ConstraintEqualTo(View.BottomAnchor);

    // Add constraints to a superview
    View.AddConstraints(new NSLayoutConstraint[] { leadingLegendConstraint, topLegendConstraint, trailingLegendConstraint, topSurfaceConstraint, leadingSurfaceConstraint, trailingSurfaceConstraint, bottomSurfaceConstraint });

    // Create a dataSource
    var dataSource = new SCISeriesInfoLegendDataSource(legend);

    // Create a modifier
    var legendModifier = new SCILegendModifier(legend: legend, dataSource: dataSource, useAutoPlacement: false);

    // Add the modifier to the surface
    surface.ChartModifiers.Add(legendModifier);
</div>
This produces the following output:
![Legend Modifier](img/modifiers-2d/legend-outside-chart-fixed-height.png)

## Create Legend with a custom item
To create a `SCIChartLegend` with custom items, you will need to create `UICollectionView` inheritor and pass it to `SCILegendDataSource` so we can prepare everything else for you.
Read on to learn how to do that.

#### Create a Custom Legend Item
Your **Custom Legend Item** must inherit from `UICollectionViewCell` and conform to `ISCILegendItem` protocol. Also, it must be created in xib with `reuseIdentifier = LegendItemReuseIdentifier`.

Please see a simple example below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume that the CustomLegendItem.xib file is created somewhere in your project

    // CustomLegendItem.h
    #import &lt;UIKit/UIKit.h&gt;
    #import "ISCILegendItem.h"

    @interface CustomLegendItem : UICollectionViewCell<ISCILegendItem>
    @end

    // CustomLegendItem.m
    #import "CustomLegendItem.h"
    #import "SCISeriesInfo.h"

    @interface CustomLegendItem()

    @property (weak, nonatomic) IBOutlet UIView *markerView;
    @property (weak, nonatomic) IBOutlet UILabel *seriesNameLabel;

    @end

    @implementation CustomLegendItem

    - (void)bindSeriesInfoWith:(id)source legend:(id<ISCIChartLegend>)legend {
        SCISeriesInfoCore *seriesInfo = (SCISeriesInfoCore *)source;
        
        self.seriesNameLabel.text = seriesInfo.seriesName;
        self.markerView.backgroundColor = seriesInfo.seriesColor;
    }

    @end
</div>
<div class="code-snippet" id="swift">
    // Assume that the CustomLegendItem.xib file is created somewhere in your project

    import UIKit

    final class CustomLegendItem: UICollectionViewCell, ISCILegendItem {
        @IBOutlet private weak var markerView: UIView!
        @IBOutlet private weak var seriesNameLabel: UILabel!
        
        override var reuseIdentifier: String? {
            return LegendItemReuseIdentifier
        }
        
        // MARK: ISCILegendItem
        func bindSeriesInfo(with source: Any!, legend: ISCIChartLegend!) {
            guard let seriesInfo = source as? SCISeriesInfoCore else {
                return
            }
            markerView.backgroundColor = seriesInfo.seriesColor
            seriesNameLabel.text = seriesInfo.seriesName
        }
    }
</div>
<div class="code-snippet" id="cs">
    // Assume that the CustomLegendItem.xib file is created somewhere in your project

    using System;
    using Foundation;
    using UIKit;
    using SciChart.iOS.Charting;

    namespace Xamarin.Examples.Demo.iOS
    {
        public partial class CustomLegendItem : UICollectionViewCell, IISCILegendItem
        {
            public static readonly NSString Key = new NSString("CustomLegendItem");
            public static readonly UINib Nib;

            static CustomLegendItem()
            {
                Nib = UINib.FromName("CustomLegendItem", NSBundle.MainBundle);
            }

            protected CustomLegendItem(IntPtr handle) : base(handle)
            {
                // Note: this .ctor should not contain any initialization logic.
            }

            public override NSString ReuseIdentifier => SCIChartLegend.LegendItemReuseIdentifier;

            public void bindSeriesInfo(NSObject source, IISCIChartLegend legend)
            {
                var seriesInfo = source as SCISeriesInfoCore;
                if (seriesInfo != null)
                {
                    markerView.BackgroundColor = seriesInfo.SeriesColor;
                    seriesNameLabel.Text = seriesInfo.SeriesName;
                }
            }
        }
    }
</div>

#### Adding a Legend with CustomItem to a Chart
To add a Legend with previously created Custom Legend Item you will need to do the following:
- create `SCIChartLegend` instance.
- use the `-[SCILegendDataSource initWithLegend:legendItemXibName:]` initializer to create a legend `DataSource`. Pass legend created in the previous step and your **CustomLegendItem.xib** name string.
- use the `-[SCILegendModifier initWithLegend:dataSource:useAutoPlacement:]` initializer to create `SCILegendModifier` instance. Pass previously created **Legend** and **DataSource** objects.

> **_NOTE:_** By passing `true` or `false` to `useAutoPlacement` argument you indicate whether you want to place your `SCIChartLegend` instance manually, e.g. - [inside](#adding-the-scilegendmodifier-to-a-chart) or [outside](#placing-legend-outside-the-chart) the Chart Surface.

Please see the code sample on how to do that below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Assume a surface has been created and configured somewhere
    ISCIChartSurface surface;

    // Create a legend
    SCIChartLegend *legend = [SCIChartLegend new];

    // Create a dataSource with your custom legend item xib name. Assume a CustomLegendItem class has been created and configured somewhere    
    SCISeriesInfoLegendDataSource *dataSource = [[SCISeriesInfoLegendDataSource alloc] initWithLegend:legend legendItemXibName:@"CustomLegendItem"];

    // Create a modifier.
    // By passing `YES` to `useAutoPlacement` argument you indicate that you want to place Legend inside the Chart Surface.
    SCILegendModifier *legendModifier = [[SCILegendModifier alloc] initWithLegend:legend dataSource:dataSource useAutoPlacement:YES];
    legendModifier.margins = UIEdgeInsetsMake(16, 16, 16, 16);
    legendModifier.position = SCIAlignment_Top | SCIAlignment_Left;

    // Add the modifier to the surface
    [self.surface.chartModifiers add:legendModifier];
</div>
<div class="code-snippet" id="swift">
    // Assume a surface has been created and configured somewhere
    let surface: ISCIChartSurface

    // Create a legend
    let legend = SCIChartLegend()

    // Create a dataSource with your custom legend item xib name. Assume a CustomLegendItem class has been created and configured somewhere
    let dataSource = SCISeriesInfoLegendDataSource(legend: legend, legendItemXibName: "CustomLegendItem")

    // Create a modifier.
    // By passing `true` to `useAutoPlacement` argument you indicate that you want to place Legend inside the Chart Surface.
    let legendModifier = SCILegendModifier(legend: legend, dataSource: dataSource, useAutoPlacement: true)
    legendModifier.position = [.left, .top]
    legendModifier.margins = UIEdgeInsets(top: 16, left: 16, bottom: 16, right: 16)

    // Add the modifier to the surface
    surface.chartModifiers.add(legendModifier)
</div>
<div class="code-snippet" id="cs">
    // Assume a surface has been created and configured somewhere
    SCIChartSurface surface;

    // Create a legend
    var legend = new SCIChartLegend();

    // Create a dataSource with your custom legend item xib name. Assume a CustomLegendItem class has been created and configured somewhere
    var dataSource = new SCISeriesInfoLegendDataSource(legend: legend, legendItemXibName: "CustomLegendItem");

    // Create a modifier.
    // By passing `true` to `useAutoPlacement` argument you indicate that you want to place Legend inside the Chart Surface.
    var legendModifier = new SCILegendModifier(legend: legend, dataSource: dataSource, useAutoPlacement: true);
    legendModifier.Position = SCIAlignment.Top | SCIAlignment.Left;
    legendModifier.Margins = new UIEdgeInsets(16, 16, 16, 16);

    // Add the modifier to the surface
    surface.ChartModifiers.Add(legendModifier);
</div>

This produces the following output:
![Legend Modifier](img/modifiers-2d/legend-custom-item.png)
