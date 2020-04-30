#include "pbPlots.cpp"
#include "supportLib.cpp"

int main(){
	size_t length;
	RGBABitmapImageReference *imageReference = CreateRGBABitmapImageReference();

	double xsa[] = {-2, -1, 0, 1, 2};
	vector<double> xs(xsa, xsa+sizeof(xsa)/sizeof(double));
	double ysa[] = {2, -1, -2, -1, 2};
	vector<double> ys(ysa, ysa+sizeof(ysa)/sizeof(double));

	ScatterPlotSeries *series = GetDefaultScatterPlotSeriesSettings();
	series->xs = &xs;
	series->ys = &ys;
	series->linearInterpolation = true;
	series->lineType = toVector(L"dashed");
	series->lineThickness = 2;
	series->color = GetGray(0.3);

	ScatterPlotSettings *settings = GetDefaultScatterPlotSettings();
	settings->width = 600;
	settings->height = 400;
	settings->autoBoundaries = true;
	settings->autoPadding = true;
	settings->title = toVector(L"x^2 - 2");
	settings->xLabel = toVector(L"X axis");
	settings->yLabel = toVector(L"Y axis");
	settings->scatterPlotSeries->push_back(series);

	DrawScatterPlotFromSettings(imageReference, settings);

	vector<double> *pngdata = ConvertToPNG(imageReference->image);
	WriteToFile(pngdata, "example2.png");
	DeleteImage(imageReference->image);

	return 0;
}
