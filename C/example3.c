#include "pbPlots.h"
#include "supportLib.h"

#define points 50

int main(){
	_Bool success;
	StringReference *errorMessage;
	double x [points];
	double y [points];

	StartArenaAllocator();

	for(int i = 0; i < points; i++){
		x[i] = i/10.0 - 2.5;
		y[i] = sin(x[i]);
	}

	RGBABitmapImageReference *imageRef = CreateRGBABitmapImageReference();
	errorMessage = (StringReference *)malloc(sizeof(StringReference));
	success = DrawScatterPlot(imageRef, 800, 600, x, points, y, points, errorMessage);

	if(success){
		ByteArray *pngdata = ConvertToPNG(imageRef->image);
		DeleteImage(imageRef->image);

		WriteToFile(pngdata, "example3.png");
	}else{
		fprintf(stderr, "Error: ");
		for(int i = 0; i < errorMessage->stringLength; i++){
			fprintf(stderr, "%c", errorMessage->string[i]);
		}
		fprintf(stderr, "\n");
  }

	FreeAllocations();

	return success ? 0 : 1;
}
