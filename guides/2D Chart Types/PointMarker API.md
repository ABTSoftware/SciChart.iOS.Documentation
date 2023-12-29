# PointMarker API
SciChart features a rich **PointMarkers API** to annotate the data-points of certain series with markers, e.g. **Ellipse**, **Square**, **Triangle**, **Cross** or a **Custom Shape** marker. Some series types, such as `SCIXyScatterRenderableSeries` or `SCIFastImpulseRenderableSeries`, require a PointMarker assigned to them unless they won't render at all.

This article is about how to configure and add PointMarkers to a `ISCIRenderableSeries` to render markers for every data point.

![PointMarkers API](img/chart-types-2d/using-pointmarkers-example.png)

> **_NOTE:_** Examples of using PointMarkers API can be found in the [SciChart iOS Examples Suite](https://www.scichart.com/examples/ios-chart/) as well as on [GitHub](https://github.com/ABTSoftware/SciChart.iOS.Examples):
> 
> - [Obj-C/Swift Example](https://www.scichart.com/example/ios-chart-with-point-markers-example/)

## PointMarker Types
SciChart provides several **PointMarker** shapes out of the box which can be found below:
- `SCIEllipsePointMarker`
- `SCITrianglePointMarker`
- `SCISquarePointMarker`
- `SCICrossPointMarker`
- `SCISpritePointMarker`

It is possible to change how point markers appears by extending any of the above classes. The `SCISpritePointMarker` allows to render point markers on a `SCIBitmap` (which is a simple wrapper around the `CGContext`). For more details, refer to the [Custom PointMarkers](#custom-pointmarkers) section down the page.

All the **PointMarker** types conforms to the `ISCIPointMarker` protocol, which provides the following properties for styling point markers:

| **`ISCIPointMarker` property** | **Description**                                                                                                                                  |
| ------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| `ISCIPointMarker.size`         | Allows to specify the size of a PointMarker. PointMarkers will not appear if this value isn't set. The units are in user space coordinate system |
| `ISCIPointMarker.strokeStyle`  | Specifies a stroke pen of the `SCIPenStyle` type. It contains information about the **Color, Stroke Thickness**, etc.                            |
| `ISCIPointMarker.fillStyle`    | Specifies a fill brush of the `SCIBrushStyle` type. It contains information about the fill Color and the desired type of visual output.          |

> **_NOTE:_** To learn more about **Pens** and **Brushes** and how to utilize them, please refer to the [SCIPenStyle, SCIBrushStyle and SCIFontStyle](scipenstyle-scibrushstyle-and-scifontstyle.html) article.

## Using PointMarkers
Code for creation and assigning a **PointMarker** to a `ISCIRenderableSeries` is essentially the same regardless of a PointMarker type. 
After an instance of it has been created, it can be configured and then applied to the `ISCIRenderableSeries.pointMarker` property:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    // Create an Ellipse PointMarker instance
    id&lt;ISCIPointMarker&gt; pointMarker = [SCIEllipsePointMarker new];
    pointMarker.size = (CGSize){ .width = 40, .height = 40 };
    pointMarker.strokeStyle = [[SCISolidPenStyle alloc] initWithColor:UIColor.greenColor thickness:2.0];
    pointMarker.fillStyle = [[SCISolidBrushStyle alloc] initWithColor:UIColor.redColor];
    
    // Apply the PointMarker to a LineSeries
    id&lt;ISCIRenderableSeries&gt; lineSeries = [SCIFastLineRenderableSeries new];
    lineSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    // Create an Ellipse PointMarker instance
    let pointMarker = SCIEllipsePointMarker()
    pointMarker.size = CGSize(width: 40, height: 40)
    pointMarker.strokeStyle = SCISolidPenStyle(color: .green, thickness: 2.0)
    pointMarker.fillStyle = SCISolidBrushStyle(color: .red)
    
    // Apply the PointMarker to a LineSeries
    let lineSeries = SCIFastLineRenderableSeries()
    lineSeries.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    // Create an Ellipse PointMarker instance
    var pointMarker = new SCIEllipsePointMarker();
    pointMarker.Size = new CGSize(40, 40);
    pointMarker.StrokeStyle = new SCISolidPenStyle(UIColor.Green, 2.0f);
    pointMarker.FillStyle = new SCISolidBrushStyle(UIColor.Red);
    
    // Apply the PointMarker to a LineSeries
    var lineSeries = new SCIFastLineRenderableSeries();
    lineSeries.PointMarker = pointMarker;
</div>

The code above will produce the following chart (assuming that the data has been added to the **[Line Series](2d-chart-types---line-series.html)**):

![PointMarker Example](img/chart-types-2d/pointmarker-example.jpg)

## Custom PointMarkers
There are two ways of creating custom **PointMarkers** in SciChart. The [first one](#extend-scidrawablepointmarker) involves using our **RenderContext2D API** for drawing, and [the second](#implement-iscispritepointmarkerdrawer) allows to use CoreGraphics drawing capabilities.

#### Extend SCIDrawablePointMarker
This technique requires extending the `SCIDrawablePointMarker` class and overriding the its `-[SCIDrawablePointMarker internalDrawWithContext:at:withStrokePen:andFillBrush:]` method which is called for every data point in a series. 
In the implementation, a **PointMarker** can be drawn calling methods from `ISCIRenderContext2D`. 
The code below demonstrates how custom **EllipsePointMarker** can be created using this approach:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface EllipsePointMarker : SCIDrawablePointMarker
    @end

    @implementation EllipsePointMarker

    - (void)internalDrawWithContext:(id<ISCIRenderContext2D>)renderContext at:(CGPoint)point withStrokePen:(id<ISCIPen2D>)pen andFillBrush:(id<ISCIBrush2D>)brush {
        [renderContext drawEllipseWithPen:pen brush:brush andSize:self.size at:point];
    }

    @end
</div>
<div class="code-snippet" id="swift">
    class EllipsePointMarker: SCIDrawablePointMarker {
        override func internalDraw(with renderContext: ISCIRenderContext2D!, at point: CGPoint, withStroke pen: ISCIPen2D!, andFill brush: ISCIBrush2D!) {
            renderContext.drawEllipse(with: pen, brush: brush, andSize: self.size, at: point)
        }
    }
</div>
<div class="code-snippet" id="cs">
    class EllipsePointMarker: SCIDrawablePointMarker
    {
        public override void InternalDraw(IISCIRenderContext2D renderContext, CGPoint point, IISCIPen2D pen, IISCIBrush2D brush)
        {
            renderContext.DrawEllipseAt(point, this.Size, pen, brush);
        }
    }
</div>

However, the **RenderContext2D API** has its own limitations. It isn't suitable for drawing complex custom shapes. 
Besides, calling drawing methods for every data point is redundant and is an overkill. So the second technique, described in the following paragraph, is **better suited** for most cases.

#### Implement ISCISpritePointMarkerDrawer 
This approach is rather different. It allows to draw a shape on CoreGraphics [CGContext](https://developer.apple.com/documentation/coregraphics/cgcontextref).
Then, a sprite is created out of it, which is rendered for every data point in a series. 

This requires creating an object which conforms to `ISCISpritePointMarkerDrawer` protocol and pass it's instance into the `SCISpritePointMarker` initializer, like shown below:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface SCICustomPointMarkerDrawer: NSObject&lt;ISCISpritePointMarkerDrawer&gt;
    - (nonnull instancetype)initWithImage:(UIImage *)image;
    @end

    @implementation SCICustomPointMarkerDrawer {
        UIImage *_image;
    }

    - (instancetype)initWithImage:(UIImage *)image {
        self = [super init];
        if (self) {
            _image = image;
        }
        return self;
    }

    - (void)onDrawBitmap:(SCIBitmap *)bitmap withPenStyle:(SCIPenStyle *)penStyle andBrushStyle:(SCIBrushStyle *)brushStyle {
        CGContextSaveGState(bitmap.context);
        CGRect rect = CGRectMake(0, 0, bitmap.width, bitmap.height);
        CGContextTranslateCTM(bitmap.context, 0.0, bitmap.height); // bitmap.context.height
        CGContextScaleCTM(bitmap.context, 1.0, -1.0);
        CGContextDrawImage(bitmap.context, rect, _image.CGImage);
        CGContextRestoreGState(bitmap.context);
    }

    @end
    ...
    SCICustomPointMarkerDrawer *drawer = [[SCICustomPointMarkerDrawer alloc] initWithImage:[UIImage imageNamed:@"image.weather.storm"]];
    SCISpritePointMarker *pointMarker = [[SCISpritePointMarker alloc] initWithDrawer:drawer];
    pointMarker.size = CGSizeMake(40, 40);

    // Apply the PointMarker to a LineSeries
    id&lt;ISCIRenderableSeries&gt; lineSeries = [SCIFastLineRenderableSeries new];
    lineSeries.pointMarker = pointMarker;
</div>
<div class="code-snippet" id="swift">
    class CustomPointMarkerDrawer: ISCISpritePointMarkerDrawer {
        
        let image: UIImage
        
        init(image: UIImage) {
            self.image = image
        }
        
        func onDraw(_ bitmap: SCIBitmap!, with penStyle: SCIPenStyle!, andBrushStyle brushStyle: SCIBrushStyle!) {
            bitmap.context.saveGState()
            let rect = CGRect(origin: .zero, size: CGSize(width: CGFloat(bitmap.width), height: CGFloat(bitmap.height)))
            bitmap.context.translateBy(x: 0.0, y: CGFloat(bitmap.context.height))
            bitmap.context.scaleBy(x: 1.0, y: -1.0)
            bitmap.context.draw(image.cgImage!, in: rect)
            bitmap.context.restoreGState()
        }
    }
    ...
    let pointMarker = SCISpritePointMarker(drawer: CustomPointMarkerDrawer(image: #imageLiteral(resourceName: "image.weather.storm")))
    pointMarker.size = CGSize(width: 40, height: 40)

    // Apply the PointMarker to a LineSeries
    let lineSeries = SCIFastLineRenderableSeries()
    lineSeries.pointMarker = pointMarker
</div>
<div class="code-snippet" id="cs">
    class CustomPointMarkerDrawer : IISCISpritePointMarkerDrawer
    {
        public UIImage image { get; }

        public IntPtr Handle => throw new NotImplementedException();

        public CustomPointMarkerDrawer(UIImage image)
        {
            this.image = image;
        }

        public void onDraw(SCIBitmap bitmap, SCIPenStyle penStyle, SCIBrushStyle brushStyle)
        {
            bitmap.Context.SaveState();
            var rect = new CGRect(CGPoint.Empty, new CGSize(bitmap.Width, bitmap.Height));
            bitmap.Context.TranslateCTM((nfloat)0.0, bitmap.Height);
            bitmap.Context.ScaleCTM((nfloat)1.0, (nfloat)(-1.0));
            bitmap.Context.DrawImage(rect, image.CGImage);
            bitmap.Context.RestoreState();
        }
    }
    ...
    var pointMarker = new SCISpritePointMarker(new CustomPointMarkerDrawer(new UIImage("image.weather.storm")));
    pointMarker.Size = new CGSize(40, 40);

    // Apply the PointMarker to a LineSeries
    var lineSeries = new SCIFastLineRenderableSeries();
    lineSeries.PointMarker = pointMarker;
</div>

This would result in the following chart:

![Custom PointMarker Drawer](img/chart-types-2d/custom-pointmarker-drawer.png)
