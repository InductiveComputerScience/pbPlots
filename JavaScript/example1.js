var fs = require('fs');

eval(fs.readFileSync('pbPlots.js')+'');
eval(fs.readFileSync('supportLib.js')+'');

var imageReference = CreateRGBABitmapImageReference();

var xs = [-2, -1, 0, 1, 2];
var ys = [2, -1, -2, -1, 2];

var errorMessage = {};
var success = DrawScatterPlot(imageReference, 800, 600, xs, ys, errorMessage);

if(success){
    var pngdata = ConvertToPNG(imageReference.image);
    WriteToFile(pngdata, "example1.png");
    DeleteImage(imageReference.image);
}else{
    console.error(errorMessage.string.join(''));
}
