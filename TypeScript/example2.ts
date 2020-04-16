import * as supportLib from './supportLib';
import * as pbPlots from './pbPlots';

var series = pbPlots.GetDefaultScatterPlotSeriesSettings();
series.xs = [-2, -1, 0, 1, 2];
series.ys = [2, -1, -2, -1, 2];
series.linearInterpolation = true;
series.lineType = "dashed".split('');
series.lineThickness = 2;
series.color = pbPlots.GetGray(0.3);

var settings = pbPlots.GetDefaultScatterPlotSettings();
settings.canvas = pbPlots.CreateImage(800, 600, pbPlots.GetWhite());
settings.autoBoundaries = true;
settings.autoPadding = true;
settings.title = "x^2 - 2".split('');
settings.xLabel = "X axis".split('');
settings.yLabel = "Y axis".split('');
settings.scatterPlotSeries = [series];

pbPlots.DrawScatterPlotFromSettings(settings);

var pngdata = pbPlots.ConvertToPNG(settings.canvas);
supportLib.WriteToFile(pngdata, "example2.png");
pbPlots.DeleteImage(settings.canvas);
