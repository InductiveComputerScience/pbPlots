#include "pbPlots.hpp"
#include "supportLib.hpp"

using namespace std;

int main(){
    bool success;
    StringReference *errorMessage = new StringReference();
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
        for(int i = 0; i < errorMessage.string->size(); i++){
            cerr << errorMessage.string[i];
        }
        cerr << endl;
	}

	return success ? 0 : 1;
}
