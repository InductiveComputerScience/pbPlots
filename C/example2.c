#include "pbPlots.h"
#include "supportLib.h"

int main(){
	double xs [] = {-2, -1, 0, 1, 2};
	double ys [] = {2, -1, -2, -1, 2};

	ScatterPlotSeries *series = GetDefaultScatterPlotSeriesSettings();
	series->xs = xs;
	series->xsLength = 5;
	series->ys = ys;
	series->ysLength = 5;
	series->linearInterpolation = true;
	series->lineType = L"dashed";
	series->lineTypeLength = wcslen(series->lineType);
	series->lineThickness = 2;
	series->color = GetGray(0.3);

	ScatterPlotSettings *settings = GetDefaultScatterPlotSettings();
	settings->width = 600;
	settings->height = 400;
	settings->autoBoundaries = true;
	settings->autoPadding = true;
  settings->title = L"x^2 - 2";
  settings->titleLength = wcslen(settings->title);
  settings->xLabel = L"X axis";
  settings->xLabelLength = wcslen(settings->xLabel);
  settings->yLabel = L"Y axis";
  settings->yLabelLength = wcslen(settings->yLabel);
	ScatterPlotSeries *s [] = {series};
	settings->scatterPlotSeries = s;
	settings->scatterPlotSeriesLength = 1;

	RGBABitmapImageReference *canvasReference = CreateRGBABitmapImageReference();
	DrawScatterPlotFromSettings(canvasReference, settings);

	size_t length;
	double *pngdata = ConvertToPNG(&length, canvasReference->image);
	WriteToFile(pngdata, length, "example2.png");
	DeleteImage(canvasReference->image);

	return 0;
}
