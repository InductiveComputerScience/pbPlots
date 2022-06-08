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
settings.width = 800;
settings.height = 600;
settings.autoBoundaries = true;
settings.autoPadding = true;
settings.title = "x^2 - 2".split('');
settings.xLabel = "X axis".split('');
settings.yLabel = "Y axis".split('');
settings.scatterPlotSeries = [series];

var imageReference = pbPlots.CreateRGBABitmapImageReference();
var errorMessage = new pbPlots.StringReference();
var success = pbPlots.DrawScatterPlotFromSettings(imageReference, settings, errorMessage);

if(success){
    var pngdata = pbPlots.ConvertToPNG(imageReference.image);
    supportLib.WriteToFile(pngdata, "example2.png");
    pbPlots.DeleteImage(imageReference.image);
}else{
    console.error(errorMessage.stringx.join(''));
}
