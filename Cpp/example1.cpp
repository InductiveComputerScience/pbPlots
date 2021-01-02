#include "pbPlots.hpp"
#include "supportLib.hpp"

using namespace std;

int main(){
	RGBABitmapImageReference *imageReference = CreateRGBABitmapImageReference();

	vector<double> xs{-2, -1, 0, 1, 2};
	vector<double> ys{2, -1, -2, -1, 2};

	DrawScatterPlot(imageReference, 600, 400, &xs, &ys);

	vector<double> *pngdata = ConvertToPNG(imageReference->image);
	WriteToFile(pngdata, "example1.png");
	DeleteImage(imageReference->image);

	return 0;
}
