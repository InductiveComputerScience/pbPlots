import * as fs from 'fs';

import * as supportLib from './supportLib';
import * as pbPlots from './pbPlots';

var imageReference = pbPlots.CreateRGBABitmapImageReference();

var xs = [-2, -1, 0, 1, 2];
var ys = [2, -1, -2, -1, 2];

var errorMessage = new pbPlots.StringReference();
var success = pbPlots.DrawScatterPlot(imageReference, 800, 600, xs, ys, errorMessage);

if(success){
    var pngdata = pbPlots.ConvertToPNG(imageReference.image);
    supportLib.WriteToFile(pngdata, "example1.png");
    pbPlots.DeleteImage(imageReference.image);
}else{
    console.error(errorMessage.stringx.join(''));
}
