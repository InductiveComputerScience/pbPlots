#include <stdlib.h>
#include <stddef.h>

typedef struct ByteArray{
  double *bytes;
  size_t bytesLength;
}ByteArray;

#include "supportLib.h"

unsigned char *DoubleArrayToByteArray(double *data, size_t length){
	unsigned char *out;
	size_t i;

	out = (unsigned char*)malloc(sizeof(unsigned char)*length);

	for(i = 0; i < length; i++){
		out[i] = data[i];
	}

	return out;
}

void WriteToFile(ByteArray *data, char *filename){
	unsigned char *bytes;

	bytes = DoubleArrayToByteArray(data->bytes, data->bytesLength);

	FILE* file = fopen(filename, "wb");
	fwrite(bytes, 1, data->bytesLength, file);
	fclose(file);

	free(bytes);
}

double *ByteArrayToDoubleArray(unsigned char *data, size_t length){
	double *out;
	size_t i;

	out = (double*)malloc(sizeof(double)*length);

	for(i = 0; i < length; i++){
		out[i] = data[i];
	}

	return out;
}
