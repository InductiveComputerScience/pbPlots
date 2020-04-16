import * as fs from 'fs';

import * as supportLib from './supportLib';
import * as pbPlots from './pbPlots';

var image = pbPlots.CreateImage(800, 600, pbPlots.GetWhite());

var xs = [-2, -1, 0, 1, 2];
var ys = [2, -1, -2, -1, 2];

pbPlots.DrawScatterPlot(image, xs, ys);

var pngdata = pbPlots.ConvertToPNG(image);
supportLib.WriteToFile(pngdata, "example1.png");
pbPlots.DeleteImage(image);
