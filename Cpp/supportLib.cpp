#include <cstdio>
#include <vector>

using namespace std;

unsigned char *DoubleArrayToByteArray(vector<double> *data){
	unsigned char *out;
	size_t i;

	out = (unsigned char *)malloc(sizeof(unsigned char) * data->size());

	for(i = 0; i < data->size(); i++){
		out[i] = data->at(i);
	}

	return out;
}

void WriteToFile(vector<double> *data, char *filename){
	unsigned char *bytes;

	bytes = DoubleArrayToByteArray(data);

	FILE* file = fopen(filename, "wb");
	fwrite(bytes, 1, data->size(), file);

	free(bytes);
}

vector<double> *ByteArrayToDoubleArray(vector<unsigned char> *data){
	vector<double> *out;
	size_t i;

	out = new vector<double>(data->size());

	for(i = 0; i < data->size(); i++){
		out->at(i) = data->at(i);
	}

	return out;
}
