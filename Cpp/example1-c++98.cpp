#include "pbPlots.hpp"
#include "supportLib.hpp"

using namespace std;

int main(){
	bool success;
	StringReference *errorMessage = CreateStringReferenceLengthValue(0, L' ');
	RGBABitmapImageReference *imageReference = CreateRGBABitmapImageReference();

	double xsa[] = {-2, -1, 0, 1, 2};
	vector<double> xs(xsa, xsa+sizeof(xsa)/sizeof(double));
	double ysa[] = {2, -1, -2, -1, 2};
	vector<double> ys(ysa, ysa+sizeof(ysa)/sizeof(double));

	success = DrawScatterPlot(imageReference, 600, 400, &xs, &ys, errorMessage);

	if(success){
		vector<double> *pngdata = ConvertToPNG(imageReference->image);
		WriteToFile(pngdata, "example1.png");
		DeleteImage(imageReference->image);
	}else{
		cerr << "Error: ";
		for(int i = 0; i < errorMessage->string->size(); i++){
			wcerr << errorMessage->string->at(i);
		}
		cerr << endl;
	}

	FreeAllocations();

	return success ? 0 : 1;
}
