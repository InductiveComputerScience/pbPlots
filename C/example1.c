#include "pbPlots.c"
#include "supportLib.c"

int main(){
	size_t length;

	RGBABitmapImage *image = CreateImage(600, 400, GetWhite());

	double xs [] = {-2, -1, 0, 1, 2};
	double ys [] = {2, -1, -2, -1, 2};

	DrawScatterPlot(image, xs, 5, ys, 5);
	double *pngdata = ConvertToPNG(&length, image);
	WriteToFile(pngdata, length, "example1.png");
	DeleteImage(image);

	return 0;
}
