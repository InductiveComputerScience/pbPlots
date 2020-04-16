<?php

include("pbPlots.php");
include("supportLib.php");

$image = CreateImage(800, 600, GetWhite());

$xs = array(-2, -1, 0, 1, 2);
$ys = array(2, -1, -2, -1, 2);

DrawScatterPlot($image, $xs, $ys);

$pngdata = ConvertToPNG($image);
WriteToFile($pngdata, "example1.png");
DeleteImage($image);
