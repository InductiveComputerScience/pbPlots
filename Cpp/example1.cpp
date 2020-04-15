#include "pbPlots.cpp"
#include "supportLib.cpp"

int main(){
	size_t length;

	RGBABitmapImage *image = CreateImage(600, 400, GetWhite());

	double xsa[] = {-2, -1, 0, 1, 2};
	vector<double> xs(xsa, xsa+sizeof(xsa)/sizeof(double));
	double ysa[] = {2, -1, -2, -1, 2};
	vector<double> ys(ysa, ysa+sizeof(ysa)/sizeof(double));

	DrawScatterPlot(image, &xs, &ys);
	vector<double> *pngdata = ConvertToPNG(image);
	WriteToFile(pngdata, "example1.png");
	DeleteImage(image);

	return 0;
}
