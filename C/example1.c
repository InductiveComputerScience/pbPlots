#include "pbPlots.h"
#include "supportLib.h"

int main(){
	double xs [] = {-2, -1, 0, 1, 2};
	double ys [] = {2, -1, -2, -1, 2};
	_Bool success;

	RGBABitmapImageReference *canvasReference = CreateRGBABitmapImageReference();
	StringReference *errorMessage = (StringReference *)malloc(sizeof(StringReference));
	success = DrawScatterPlot(canvasReference, 600, 400, xs, 5, ys, 5, errorMessage);

    if(success){
        size_t length;
        double *pngdata = ConvertToPNG(&length, canvasReference->image);
        WriteToFile(pngdata, length, "example1.png");
        DeleteImage(canvasReference->image);
	}else{
	    fprintf(stderr, "Error: ", errorMessage.string);
	    for(int i = 0; i < errorMessage.stringLength; i++){
            fprintf(stderr, "%c", errorMessage.string[i]);
        }
        fprintf(stderr, "\n");
    }

	return success ? 0 : 1;
}
