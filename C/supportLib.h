#include <stdlib.h>
#include <stdio.h>

unsigned char *DoubleArrayToByteArray(double *data, size_t length);
void WriteToFile(ByteArray *data, char *filename);
double *ByteArrayToDoubleArray(unsigned char *data, size_t length);
