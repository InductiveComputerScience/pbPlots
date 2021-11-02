<?php

include("pbPlots.php");
include("supportLib.php");

$xs = array(-2, -1, 0, 1, 2);
$ys = array(2, -1, -2, -1, 2);

$imageReference = CreateRGBABitmapImageReference();
$errorMessage = new stdClass();
$success = DrawScatterPlot($imageReference, 800, 600, $xs, $ys, $errorMessage);

if($success){
    $pngdata = ConvertToPNG($imageReference->image);
    WriteToFile($pngdata, "example1.png");
    DeleteImage($imageReference->image);
}else{
    echo implode('', $errorMessage->string);
}