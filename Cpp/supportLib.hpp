#include <vector>
#include <string>
#include <iostream>
#include <fstream>

using namespace std;

unsigned char *DoubleArrayToByteArray(vector<double> *data);
void WriteToFile(vector<double> *data, string filename);
vector<double> *ByteArrayToDoubleArray(vector<unsigned char> *data);
