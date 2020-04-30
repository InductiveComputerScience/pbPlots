var fs = require('fs');

eval(fs.readFileSync('pbPlots.js')+'');
eval(fs.readFileSync('supportLib.js')+'');

var imageReference = CreateRGBABitmapImageReference();

var series = GetDefaultScatterPlotSeriesSettings();
series.xs = [-2, -1, 0, 1, 2];
series.ys = [2, -1, -2, -1, 2];
series.linearInterpolation = true;
series.lineType = "dashed";
series.lineThickness = 2;
series.color = GetGray(0.3);

var settings = GetDefaultScatterPlotSettings();
settings.width = 800;
settings.height = 600;
settings.autoBoundaries = true;
settings.autoPadding = true;
settings.title = "x^2 - 2";
settings.xLabel = "X axis";
settings.yLabel = "Y axis";
settings.scatterPlotSeries = [series];

DrawScatterPlotFromSettings(imageReference, settings);

var pngdata = ConvertToPNG(imageReference.image);
WriteToFile(pngdata, "example2.png");
DeleteImage(imageReference.image);
