#include "pbPlots.h"
#include "supportLib.h"

void Repeat();

int main(){
	double xs [] = {-2, -1, 0, 1, 2};
	double ys [] = {2, -1, -2, -1, 2};
	_Bool success;

	StartArenaAllocator();

	RGBABitmapImageReference *canvasReference = CreateRGBABitmapImageReference();
	StringReference *errorMessage = CreateStringReference(L"", 0);
	success = DrawScatterPlot(canvasReference, 600, 400, xs, 5, ys, 5, errorMessage);

	if(success){
		size_t length;
		ByteArray *pngdata = ConvertToPNG(canvasReference->image);
		WriteToFile(pngdata, "example1.png");
		DeleteImage(canvasReference->image);
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

