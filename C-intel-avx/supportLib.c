#include <stdlib.h>
#include <stddef.h>
#include <stdint.h>

typedef struct ByteArray{
  uint8_t *bytes;
  size_t bytesLength;
}ByteArray;

#include "supportLib.h"

void WriteToFile(ByteArray *data, char *filename){
	FILE* file = fopen(filename, "wb");
	fwrite(data->bytes, 1, data->bytesLength, file);
	fclose(file);
}
