#include "pbPlots.hpp"
#include "supportLib.hpp"

using namespace std;

int main(){
	bool success;
	StringReference *errorMessage = CreateStringReferenceLengthValue(0, L' ');
	RGBABitmapImageReference *imageReference = CreateRGBABitmapImageReference();

	vector<double> xs{-2, -1, 0, 1, 2};
	vector<double> ys{2, -1, -2, -1, 2};

	success = DrawScatterPlot(imageReference, 600, 400, &xs, &ys, errorMessage);

	if(success){
		vector<double> *pngdata = ConvertToPNG(imageReference->image);
		WriteToFile(pngdata, "example1.png");
		DeleteImage(imageReference->image);
	}else{
		cerr << "Error: ";
		for(wchar_t c : *errorMessage->string){
			wcerr << c;
		}
		cerr << endl;
	}

	FreeAllocations();

	return success ? 0 : 1;
}
