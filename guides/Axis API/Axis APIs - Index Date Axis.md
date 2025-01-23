There are several axis types in SciChart iOS which could be **Value**, **Category** or **Index Date Axis**. 

`SCIIndexDateAxis` is a value-based date axis capable of rendering date ticks based on indices, similar to the `SCICategoryDateAxis`.

Given the data:

| **Date (X-Axis)** | **Open** | **High** | **Low** | **Close** |
| ---------------- | :------: | :------: | :------: | :------: |
| 2-1-2025         | 243.99   | 244.54   | 242.13   | 243.04   |
| 3-1-2025         | 242.91   | 242.63   | 242.08   | 242.84   |
| 6-1-2025         | 241.83   | 247.24   | 241.75   | 246.75   |
| 7-1-2025         | 246.89   | 248.21   | 245.34   | 247.77   |

A Value X-Axis and Index Date X-Axis would display the data differently:

| **Index Date X-Axis**                        | **Date X-Axis**                        |
| ------------------------------------------ | --------------------------------------- |
| ![IndexDate](img/axis-2d/index_date_axis_candlestick.png) | ![Date](img/axis-2d/date_axis_candlestick.png) |


**SCIIndexDateAxis** is a special type of hybrid axis for financial stock charts, forex charts, futures charts and cryptocurrency charts. 
 
**Date Axis** and **Category Date Axis** behaviours have been combined in order to achieve a more streamlined approach, allowing for faster / easier development of trading applications with fewer workarounds
 
**SCIDateAxis**
 
Uses dates to measure X-values. This cannot be used in forex, stocks or futures which have gaps for overnight trading, weekends where markets are closed. Using a DateAxis would show gaps at weekends or overnight in trading which is not desirable in financial applcations
 
**SCICategoryDateAxis**
 
Seeks to solve the problem by using the index (i=0, i=1) of the financial data to space candles and bars. This neatly solves the issue of collapsing gaps in financial data such as stocks, forex however it leaves two problems
 
all series on the axis must have the same number of points 
all annotations and markers on the chart must be placed by index

This problem is apparently when using a Moving Average on a chart with CategoryDateAxis. A moving average has fewer points than the main candlestick series and the user must pre-pend NaN (null points) into the moving average 
 
Annotations when placed have to be placed using index, not date. A simple trade marker requires calculating what index to place the annotation at 
 
Changing timeframe (e.g. from Daily chart to Hourly) requires recalculation of all indexes and re-placement of all annotations at the new correct indexes
 
Even setting the visibleRange of the axis requires a calculation of date to index. this is a lot of work for the user application which must continually convert between index and date. 
 
**SCIIndexDateAxis**
 
This new axis type solves the problems in financial charts by internally using Category calculations (by index) to collapse gaps, but performs the transform between indexes and dates internally
 
The first series (candlestick OHLC data) is used as a scale by which all other series are measured against. Using this axis type you can
 
- Set xAxis.VisibleRange as dates, not indexes
- Place annotations using dates on x-value, not indexes
- not worry about repositioning annotations when changing timeframe e.g. from daily to hourly financial data
- Place series on the chart with different numbers of points: e.g. moving averages no longer need to have NaN prepended
- Indicators such as ZigZag or oscillators with fewer or more points than the main OHLC series can be placed on the chart

All this happens transparently under the hood. A single axis can now be used for Forex, Cryptocurrency, Stock market or futures data without unnecessary complication to the user

## See the SCIIndexDateAxis Types in action
Please take a look at the examples from the iOS Examples Suite listed below to see these axis types in action:
- [Easy Stock Chart with IndexDateAxis](https://www.scichart.com/example/ios-easy-stock-chart-with-indexDateAxis/) with `SCIIndexDateAxis` 