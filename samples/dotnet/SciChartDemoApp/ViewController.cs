using UIKit;
using CoreGraphics;
using Foundation;
using SciChart.iOS.Binding;
using System;
using System.Security.Cryptography;

namespace SciChartDemoApp
{
    public class ViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var plusButton = new UIButton(new CGRect(20, 70, 40, 40));
            plusButton.SetTitle("+", UIControlState.Normal);
            plusButton.BackgroundColor = UIColor.SystemBlue;

            View.AddSubview(plusButton);

            var minusButton = new UIButton(new CGRect(70, 70, 40, 40));
            minusButton.SetTitle("-", UIControlState.Normal);
            minusButton.BackgroundColor = UIColor.SystemBlue;

            View.AddSubview(minusButton);


            InvokeOnMainThread(() =>
            {
                string licenseKey = "YOUR_LICENSE_KEY_HERE";
                SCIChartSurface.SetRuntimeLicenseKey(licenseKey);

                var surface = new SCIChartSurface(new CGRect(20, 120, 300, 300));
                surface.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                View.AddSubview(surface);

                var xAxis = new SCINumericAxis();
                xAxis.AxisTitle = "X Axis";
                xAxis.DrawMajorGridLines = true;
                xAxis.DrawMinorGridLines = true;
                xAxis.MinorsPerMajor = 5;
                xAxis.MajorTickLineLength = 10;
                xAxis.MinorTickLineLength = 5;
                xAxis.FlipCoordinates = false;

                var yAxis = new SCINumericAxis();
                yAxis.AxisAlignment = SCIAxisAlignment.Left;
                yAxis.AxisTitle = "Y Axis";
                yAxis.DrawMajorGridLines = true;
                yAxis.DrawMinorGridLines = true;
                yAxis.MinorsPerMajor = 5;
                yAxis.MajorTickLineLength = 10;
                yAxis.MinorTickLineLength = 5;
                yAxis.FlipCoordinates = false;
                yAxis.GrowBy = new SCIDoubleRange(2, 2);


                surface.XAxes.Add(xAxis);
                surface.YAxes.Add(yAxis);

                var dataSeries = new SCIXyDataSeries(SCIDataType.Double, SCIDataType.Double);

                for (int i = 0; i <= 50; i++)
                {
                    var y = 40 + Math.Sin(i * 0.25) * 20; // stays in 20–60
                    dataSeries.Append(new NSNumber(i), new NSNumber(y));
                }

                var mountainSeries = SCIFastMountainRenderableSeries.Create();
                mountainSeries.DataSeries = dataSeries;

                // Using UIColor
                mountainSeries.AreaStyle = new SCISolidBrushStyle(0x5500FF00);
                // mountainSeries.StrokeStyle = new SCISolidPenStyle(0xFF00FF00, 2.0f); // opaque green


                surface.RenderableSeries.Add(mountainSeries);

                var verticalLine = new SCIVerticalLineAnnotation();
                verticalLine.X1 = new NSNumber(25);

                var horizontalLine = new SCIHorizontalLineAnnotation();
                horizontalLine.Y1 = new NSNumber(40);

                surface.Annotations.Add(verticalLine);
                surface.Annotations.Add(horizontalLine);

                // var intersectionView = new UIView(new CGRect(0, 0, 12, 12));
                // intersectionView.Layer.CornerRadius = 6;
                // intersectionView.BackgroundColor = UIColor.Red;

                var img = UIImage.FromBundle("marker.png");
                // Create an image view
                var imageView = new UIImageView(new CGRect(0, 0, 20, 20));
                if (img == null)
                {
                    Console.WriteLine("IMAGE NOT FOUND");
                }
                else
                {
                    Console.WriteLine("IMAGE FOUND");
                }

                Console.WriteLine(NSBundle.MainBundle.BundlePath);

                imageView.Image = img;
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;


                var customAnnotation = new SCICustomAnnotation();
                customAnnotation.CustomView = imageView;

                customAnnotation.X1 = new NSNumber(25);
                customAnnotation.Y1 = new NSNumber(40);
                customAnnotation.HorizontalAnchorPoint = SCIHorizontalAnchorPoint.Center;
                customAnnotation.VerticalAnchorPoint = SCIVerticalAnchorPoint.Center;

                surface.Annotations.Add(customAnnotation);

                plusButton.TouchUpInside += (sender, e) =>
                {
                    var currentY = horizontalLine.Y1.DoubleValue + 1.0;
                    var currentX = verticalLine.X1.DoubleValue + 1.0;
                    customAnnotation.X1 = new NSNumber(currentX);
                    customAnnotation.Y1 = new NSNumber(currentY);
                    horizontalLine.Y1 = new NSNumber(currentY);
                    verticalLine.X1 = new NSNumber(currentX);
                    surface.InvalidateElement(); // redraw chart
                };

                minusButton.TouchUpInside += (sender, e) =>
                {
                    var currentY = horizontalLine.Y1.DoubleValue - 1.0;
                    var currentX = verticalLine.X1.DoubleValue - 1.0;
                    customAnnotation.X1 = new NSNumber(currentX);
                    customAnnotation.Y1 = new NSNumber(currentY);
                    horizontalLine.Y1 = new NSNumber(currentY);
                    verticalLine.X1 = new NSNumber(currentX);
                    surface.InvalidateElement(); // redraw chart
                };

                surface.InvalidateElement();
            });

        }
    }
}

