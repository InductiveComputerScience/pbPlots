#include "pbPlots.h"
#include "supportLib.h"

#define points 50

int main(){
    _Bool success;
    StringReference *errorMessage;
	double x [points];
	double y [points];

	for(int i = 0; i < points; i++){
		x[i] = i/10.0 - 2.5;
		y[i] = sin(x[i]);
	}

	RGBABitmapImageReference *imageRef = CreateRGBABitmapImageReference();
    errorMessage = (StringReference *)malloc(sizeof(StringReference));
	success = DrawScatterPlot(imageRef, 800, 600, x, points, y, points, errorMessage);

    if(success){
        size_t length;
        double *pngdata = ConvertToPNG(&length, imageRef->image);
        DeleteImage(imageRef->image);

        WriteToFile(pngdata, length, "example3.png");
        DeleteImage(imageRef->image);
	}else{
        fprintf(stderr, "Error: ", errorMessage.string);
        for(int i = 0; i < errorMessage.stringLength; i++){
            fprintf(stderr, "%c", errorMessage.string[i]);
        }
        fprintf(stderr, "\n");
    }

	return success ? 0 : 1;
}
