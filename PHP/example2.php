<?php

include("pbPlots.php");
include("supportLib.php");

$series = GetDefaultScatterPlotSeriesSettings();
$series->xs = array(-2, -1, 0, 1, 2);
$series->ys = array(2, -1, -2, -1, 2);
$series->linearInterpolation = true;
$series->lineType = str_split("dashed");
$series->lineThickness = 2;
$series->color = GetGray(0.3);

$settings = GetDefaultScatterPlotSettings();
$settings->canvas = CreateImage(800, 600, GetWhite());
$settings->autoBoundaries = true;
$settings->autoPadding = true;
$settings->title = str_split("x^2 - 2");
$settings->xLabel = str_split("X axis");
$settings->yLabel = str_split("Y axis");
$settings->scatterPlotSeries = array($series);

DrawScatterPlotFromSettings($settings);

$pngdata = ConvertToPNG($settings->canvas);
WriteToFile($pngdata, "example2.png");
DeleteImage($settings->canvas);
