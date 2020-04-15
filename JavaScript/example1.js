var fs = require('fs');

eval(fs.readFileSync('pbPlots.js')+'');
eval(fs.readFileSync('supportLib.js')+'');

var image = CreateImage(800, 600, GetWhite());

var xs = [-2, -1, 0, 1, 2];
var ys = [2, -1, -2, -1, 2];

DrawScatterPlot(image, xs, ys);

var pngdata = ConvertToPNG(image);
WriteToFile(pngdata, "example1.png");
DeleteImage(image);
