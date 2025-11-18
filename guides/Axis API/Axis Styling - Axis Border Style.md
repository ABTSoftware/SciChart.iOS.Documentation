# Axis Styling - Axis Border Style

The `SCIAxisBorderStyle` class defines the visual appearance of the borders rendered around a SciChart axis.
It allows developers to customize which sides of an axis have borders, their thickness and color.
This class provides both uniform border styling (via thickness) and each-side customization (topThickness, leftThickness, bottomThickness, rightThickness).

## Overview

The `SCIAxisBorderStyle` class defines the appearance of borders drawn around an axis.

| Property                                                             | Description                                |
| -------------------------------------------------------------------- | ------------------------------------------ |
| `SCIAxisBorderStyle.color`                                                              | Defines the border color (in ARGB format). |
| `SCIAxisBorderStyle.thickness`                                                          | Uniform border thickness for all sides.    |
| `SCIAxisBorderStyle.topThickness`, `SCIAxisBorderStyle.bottomThickness`, `SCIAxisBorderStyle.leftThickness`, `SCIAxisBorderStyle.rightThickness` | Individual side thickness customization.   |
| `SCIAxisBorderStyle.antiAliasing`                                                       | Enables smoother border rendering.         |

### Usage
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    // Uniform border styling
    xAxis.axisBorderStyle = [[SCIAxisBorderStyle alloc] initWithColor:0xFFAD3D8D
                                                             thickness:axisBorderThickness];

    // Distinguish thickness for each side
    xAxis.axisBorderStyle = [[SCIAxisBorderStyle alloc] initWithColor:0xFFAD3D8D
                                                          antiAliasing:YES
                                                          topThickness:1
                                                         leftThickness:3
                                                       bottomThickness:5
                                                        rightThickness:0.5];
    
</div>
<div class="code-snippet" id="swift">
    // Uniform border styling
    xAxis.axisBorderStyle = SCIAxisBorderStyle(color: 0xFFAD3D8D, thickness: 2.0)

    // Distinguish thickness for each side
    xAxis.axisBorderStyle = SCIAxisBorderStyle(color: 0xFFAD3D8D, antiAliasing: false, topThickness: 1, leftThickness: 3, bottomThickness: 5, rightThickness: 0.5) ?? SCIAxisBorderStyle()
</div>

### Example
<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
</div>
<div class="code-snippet" id="objectivec">
    uint primaryColors[] = {
        0xFF4FBEE6, // Light blue
        0xFFAD3D8D, // Magenta
        0xFF6BBDAE, // Teal
        0xFFE76E63, // Coral red
        0xFF2C4B92  // Deep blue
    };
    
    float axisTitleSize = 12.0f;
    float labelSize = 10.0f;
    float tickSize = 8.0f;
    float tickThickness = 2.0f;
    
    // --- X Axes ---
    SCINumericAxis *xAxis1 = [SCINumericAxis new];
    xAxis1.axisId = @"xAxis1";
    xAxis1.axisTitle = @"X Axis";
    xAxis1.axisAlignment = SCIAxisAlignment_Bottom;
    xAxis1.drawMajorBands = NO;
    xAxis1.drawMajorGridLines = YES;
    xAxis1.drawMinorGridLines = NO;
    xAxis1.drawMajorTicks = YES;
    xAxis1.visibleRange = [[SCIDoubleRange alloc] initWithMin:-10 max:110];
    
    SCINumericAxis *xAxis2 = [SCINumericAxis new];
    xAxis2.axisId = @"xAxis2";
    xAxis2.axisTitle = @"Flipped X Axis";
    xAxis2.axisAlignment = SCIAxisAlignment_Bottom;
    xAxis2.flipCoordinates = YES;
    xAxis2.drawMajorBands = NO;
    xAxis2.drawMajorGridLines = NO;
    xAxis2.drawMinorGridLines = NO;
    xAxis2.drawMajorTicks = YES;
    xAxis2.visibleRange = [[SCIDoubleRange alloc] initWithMin:-10 max:110];
    // Apply only top border to distinguish this axis
    xAxis2.axisBorderStyle = [[SCIAxisBorderStyle alloc] initWithColor:primaryColors[1]
                                                          antiAliasing:YES
                                                          topThickness:0.8
                                                         leftThickness:0
                                                       bottomThickness:0
                                                        rightThickness:0];
    
    SCINumericAxis *xAxis3 = [SCINumericAxis new];
    xAxis3.axisId = @"xAxis3";
    xAxis3.axisTitle = @"Stacked X Axis";
    xAxis3.axisAlignment = SCIAxisAlignment_Right;
    xAxis3.drawMajorBands = NO;
    xAxis3.drawMajorGridLines = NO;
    xAxis3.drawMinorGridLines = NO;
    xAxis3.drawMajorTicks = YES;
    xAxis3.visibleRange = [[SCIDoubleRange alloc] initWithMin:-10 max:110];
    // Apply distinguish thickness for each side
    xAxis3.axisBorderStyle = [[SCIAxisBorderStyle alloc] initWithColor:primaryColors[2]
                                                          antiAliasing:YES
                                                          topThickness:1
                                                         leftThickness:0.5
                                                       bottomThickness:5
                                                        rightThickness:3];
    
    // --- Y Axes ---
    SCINumericAxis *yAxis1 = [SCINumericAxis new];
    yAxis1.axisId = @"yAxis1";
    yAxis1.axisTitle = @"Flipped Y Axis - Left Aligned";
    yAxis1.axisTitlePlacement = SCIAxisTitlePlacement_Right;
    yAxis1.axisAlignment = SCIAxisAlignment_Left;
    yAxis1.flipCoordinates = YES;
    yAxis1.drawMajorBands = NO;
    yAxis1.drawMajorGridLines = YES;
    yAxis1.drawMinorGridLines = NO;
    yAxis1.drawMajorTicks = YES;
    yAxis1.visibleRange = [[SCIDoubleRange alloc] initWithMin:-10 max:140];
    // Apply uniform border styling
    yAxis1.axisBorderStyle = [[SCIAxisBorderStyle alloc] initWithColor:primaryColors[0]
                                                             thickness:2.0];
    
    SCINumericAxis *yAxis2 = [SCINumericAxis new];
    yAxis2.axisId = @"yAxis2";
    yAxis2.axisTitle = @"Stacked Y Axis";
    yAxis2.axisAlignment = SCIAxisAlignment_Left;
    yAxis2.drawMajorBands = NO;
    yAxis2.drawMajorGridLines = NO;
    yAxis2.drawMinorGridLines = NO;
    yAxis2.drawMajorTicks = YES;
    yAxis2.visibleRange = [[SCIDoubleRange alloc] initWithMin:-10 max:140];
    // Apply uniform border styling
    yAxis2.axisBorderStyle = [[SCIAxisBorderStyle alloc] initWithColor:primaryColors[1]
                                                             thickness:8];
    
    SCINumericAxis *yAxis3 = [SCINumericAxis new];
    yAxis3.axisId = @"yAxis3";
    yAxis3.axisTitle = @"Y Axis - Top Aligned";
    yAxis3.axisAlignment = SCIAxisAlignment_Top;
    yAxis3.drawMajorBands = NO;
    yAxis3.drawMajorGridLines = NO;
    yAxis3.drawMinorGridLines = NO;
    yAxis3.drawMajorTicks = YES;
    yAxis3.visibleRange = [[SCIDoubleRange alloc] initWithMin:-10 max:140];
    // Apply only bottom border
    yAxis3.axisBorderStyle = [[SCIAxisBorderStyle alloc] initWithColor:primaryColors[2]
                                                          antiAliasing:YES
                                                          topThickness:0
                                                         leftThickness:0
                                                       bottomThickness:3
                                                        rightThickness:0];
    
</div>
<div class="code-snippet" id="swift">
    let primaryColors: [UInt32] = [
        0xFF4FBEE6, // Light blue
        0xFFAD3D8D, // Magenta
        0xFF6BBDAE, // Teal
        0xFFE76E63, // Coral red
        0xFF2C4B92  // Deep blue
    ]
    
    let axisTitleSize: Float = 12.0
    let labelSize: Float = 10.0
    let tickSize: Float = 8.0
    let tickThickness: Float = 2.0
        
    let xAxis1 = SCINumericAxis()
    xAxis1.axisId = "xAxis1"
    xAxis1.axisTitle = "X Axis"
    xAxis1.axisAlignment = .bottom
    xAxis1.drawMajorBands = false
    xAxis1.drawMajorGridLines = true
    xAxis1.drawMinorGridLines = false
    xAxis1.drawMajorTicks = true
    xAxis1.visibleRange = SCIDoubleRange(min: -10.0, max: 110.0)
        
    let xAxis2 = SCINumericAxis()
    xAxis2.axisId = "xAxis2"
    xAxis2.axisTitle = "Flipped X Axis"
    xAxis2.axisAlignment = .bottom
    xAxis2.flipCoordinates = true
    xAxis2.drawMajorBands = false
    xAxis2.drawMajorGridLines = false
    xAxis2.drawMinorGridLines = false
    xAxis2.drawMajorTicks = true
    xAxis2.visibleRange = SCIDoubleRange(min: -10.0, max: 110.0)
    // Apply only top border to distinguish this axis
    xAxis2.axisBorderStyle = SCIAxisBorderStyle(color: primaryColors[1], antiAliasing: true, topThickness: 0.8, leftThickness: 0, bottomThickness: 0, rightThickness: 0) ?? SCIAxisBorderStyle()
        
    let xAxis3 = SCINumericAxis()
    xAxis3.axisId = "xAxis3"
    xAxis3.axisTitle = "Stacked X Axis"
    xAxis3.axisAlignment = .right
    xAxis3.drawMajorBands = false
    xAxis3.drawMajorGridLines = false
    xAxis3.drawMinorGridLines = false
    xAxis3.drawMajorTicks = true
    xAxis3.visibleRange = SCIDoubleRange(min: -10.0, max: 110.0)
    // Apply distinguish thickness for each side
    xAxis3.axisBorderStyle = SCIAxisBorderStyle(color: primaryColors[2], antiAliasing: false, topThickness: 1, leftThickness: 0.5, bottomThickness: 5, rightThickness: 3) ?? SCIAxisBorderStyle()
        
    let yAxis1 = SCINumericAxis()
    yAxis1.axisId = "yAxis1"
    yAxis1.axisTitle = "Flipped Y Axis - Left Aligned"
    yAxis1.axisTitlePlacement = .right
    yAxis1.axisAlignment = .left
    yAxis1.flipCoordinates = true
    yAxis1.drawMajorBands = false
    yAxis1.drawMajorGridLines = true
    yAxis1.drawMinorGridLines = false
    yAxis1.drawMajorTicks = true
    yAxis1.visibleRange = SCIDoubleRange(min: -10.0, max: 140.0)
    // Apply uniform border styling
    yAxis1.axisBorderStyle = SCIAxisBorderStyle(color: primaryColors[0], thickness: 2.0)
        
        
    let yAxis2 = SCINumericAxis()
    yAxis2.axisId = "yAxis2"
    yAxis2.axisTitle = "Stacked Y Axis"
    yAxis2.axisAlignment = .left
    yAxis2.drawMajorBands = false
    yAxis2.drawMajorGridLines = false
    yAxis2.drawMinorGridLines = false
    yAxis2.drawMajorTicks = true
    yAxis2.visibleRange = SCIDoubleRange(min: -10.0, max: 140.0)
    // Apply uniform border styling
    yAxis2.axisBorderStyle = SCIAxisBorderStyle(color: primaryColors[1], thickness: 8)
        
    let yAxis3 = SCINumericAxis()
    yAxis3.axisId = "yAxis3"
    yAxis3.axisTitle = "Y Axis - Top Aligned"
    yAxis3.axisAlignment = .top
    yAxis3.drawMajorBands = false
    yAxis3.drawMajorGridLines = false
    yAxis3.drawMinorGridLines = false
    yAxis3.drawMajorTicks = true
    yAxis3.visibleRange = SCIDoubleRange(min: -10.0, max: 140.0)
    // Apply only bottom border
    yAxis3.axisBorderStyle = SCIAxisBorderStyle(color: primaryColors[2], antiAliasing: true, topThickness: 0, leftThickness: 0, bottomThickness: 3, rightThickness: 0) ?? SCIAxisBorderStyle()
        
</div>

![Axis Border](img/axis-2d/axis-border-styling.png)