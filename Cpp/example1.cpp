#include "pbPlots.hpp"
#include "supportLib.hpp"

using namespace std;

int main(){
    bool success;
    StringReference *errorMessage = new StringReference();
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
        for(char c : errorMessage.string){
            cerr << c;
        }
        cerr << endl;
	}

	return success ? 0 : 1;
}
