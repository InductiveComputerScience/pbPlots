#include "pbPlots.h"
#include "supportLib.h"

int main(){
	double xs [] = {-2, -1, 0, 1, 2};
	double ys [] = {2, -1, -2, -1, 2};

	RGBABitmapImageReference *canvasReference = CreateRGBABitmapImageReference();
	DrawScatterPlot(canvasReference, 600, 400, xs, 5, ys, 5);

	size_t length;
	double *pngdata = ConvertToPNG(&length, canvasReference->image);
	WriteToFile(pngdata, length, "example1.png");
	DeleteImage(canvasReference->image);

	return 0;
}
