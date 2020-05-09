#include "pbPlots.h"
#include "supportLib.h"

#define points 50

int main(){
	double x [points];
	double y [points];

	for(int i = 0; i < points; i++){
		x[i] = i/10.0 - 2.5;
		y[i] = sin(x[i]);
	}

	RGBABitmapImageReference *imageRef = CreateRGBABitmapImageReference();
	DrawScatterPlot(imageRef, 800, 600, x, points, y, points);

	size_t length;
	double *pngdata = ConvertToPNG(&length, imageRef->image);
	DeleteImage(imageRef->image);

	WriteToFile(pngdata, length, "example3.png");

	return 0;
}
