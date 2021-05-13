<?php
// Downloaded from https://repo.progsbase.com - Code Developed Using progsbase.


function unichr($unicode){
    return mb_convert_encoding("&#{$unicode};", 'UTF-8', 'HTML-ENTITIES');
}
function uniord($s) {
    return unpack('V', iconv('UTF-8', 'UCS-4LE', $s))[1];
}

function CropLineWithinBoundary($x1Ref, $y1Ref, $x2Ref, $y2Ref, $xMin, $xMax, $yMin, $yMax){

  $x1 = $x1Ref->numberValue;
  $y1 = $y1Ref->numberValue;
  $x2 = $x2Ref->numberValue;
  $y2 = $y2Ref->numberValue;

  $p1In = $x1 >= $xMin && $x1 <= $xMax && $y1 >= $yMin && $y1 <= $yMax;
  $p2In = $x2 >= $xMin && $x2 <= $xMax && $y2 >= $yMin && $y2 <= $yMax;

  if($p1In && $p2In){
    $success = true;
  }else if( !$p1In  && $p2In){
    $dx = $x1 - $x2;
    $dy = $y1 - $y2;

    if($dx != 0.0){
      $f1 = ($xMin - $x2)/$dx;
      $f2 = ($xMax - $x2)/$dx;
    }else{
      $f1 = 1.0;
      $f2 = 1.0;
    }
    if($dy != 0.0){
      $f3 = ($yMin - $y2)/$dy;
      $f4 = ($yMax - $y2)/$dy;
    }else{
      $f3 = 1.0;
      $f4 = 1.0;
    }

    if($f1 < 0.0){
      $f1 = 1.0;
    }
    if($f2 < 0.0){
      $f2 = 1.0;
    }
    if($f3 < 0.0){
      $f3 = 1.0;
    }
    if($f4 < 0.0){
      $f4 = 1.0;
    }

    $f = min($f1, min($f2, min($f3, $f4)));

    $x1 = $x2 + $f*$dx;
    $y1 = $y2 + $f*$dy;

    $success = true;
  }else if($p1In &&  !$p2In ){
    $dx = $x2 - $x1;
    $dy = $y2 - $y1;

    if($dx != 0.0){
      $f1 = ($xMin - $x1)/$dx;
      $f2 = ($xMax - $x1)/$dx;
    }else{
      $f1 = 1.0;
      $f2 = 1.0;
    }
    if($dy != 0.0){
      $f3 = ($yMin - $y1)/$dy;
      $f4 = ($yMax - $y1)/$dy;
    }else{
      $f3 = 1.0;
      $f4 = 1.0;
    }

    if($f1 < 0.0){
      $f1 = 1.0;
    }
    if($f2 < 0.0){
      $f2 = 1.0;
    }
    if($f3 < 0.0){
      $f3 = 1.0;
    }
    if($f4 < 0.0){
      $f4 = 1.0;
    }

    $f = min($f1, min($f2, min($f3, $f4)));

    $x2 = $x1 + $f*$dx;
    $y2 = $y1 + $f*$dy;

    $success = true;
  }else{
    $success = false;
  }

  $x1Ref->numberValue = $x1;
  $y1Ref->numberValue = $y1;
  $x2Ref->numberValue = $x2;
  $y2Ref->numberValue = $y2;

  return $success;
}
function IncrementFromCoordinates($x1, $y1, $x2, $y2){
  return ($x2 - $x1)/($y2 - $y1);
}
function InterceptFromCoordinates($x1, $y1, $x2, $y2){

  $a = IncrementFromCoordinates($x1, $y1, $x2, $y2);
  $b = $y1 - $a*$x1;

  return $b;
}
function &Get8HighContrastColors(){
  $colors = array_fill(0, 8.0, 0);
  $colors[0.0] = CreateRGBColor(3.0/256.0, 146.0/256.0, 206.0/256.0);
  $colors[1.0] = CreateRGBColor(253.0/256.0, 83.0/256.0, 8.0/256.0);
  $colors[2.0] = CreateRGBColor(102.0/256.0, 176.0/256.0, 50.0/256.0);
  $colors[3.0] = CreateRGBColor(208.0/256.0, 234.0/256.0, 43.0/256.0);
  $colors[4.0] = CreateRGBColor(167.0/256.0, 25.0/256.0, 75.0/256.0);
  $colors[5.0] = CreateRGBColor(254.0/256.0, 254.0/256.0, 51.0/256.0);
  $colors[6.0] = CreateRGBColor(134.0/256.0, 1.0/256.0, 175.0/256.0);
  $colors[7.0] = CreateRGBColor(251.0/256.0, 153.0/256.0, 2.0/256.0);
  return $colors;
}
function DrawFilledRectangleWithBorder($image, $x, $y, $w, $h, $borderColor, $fillColor){
  if($h > 0.0 && $w > 0.0){
    DrawFilledRectangle($image, $x, $y, $w, $h, $fillColor);
    DrawRectangle1px($image, $x, $y, $w, $h, $borderColor);
  }
}
function CreateRGBABitmapImageReference(){

  $reference = new stdClass();
  $reference->image = new stdClass();
  $reference->image->x = array_fill(0, 0.0, 0);

  return $reference;
}
function RectanglesOverlap($r1, $r2){

  $overlap = false;

  $overlap = $overlap || ($r2->x1 >= $r1->x1 && $r2->x1 <= $r1->x2 && $r2->y1 >= $r1->y1 && $r2->y1 <= $r1->y2);
  $overlap = $overlap || ($r2->x2 >= $r1->x1 && $r2->x2 <= $r1->x2 && $r2->y1 >= $r1->y1 && $r2->y1 <= $r1->y2);
  $overlap = $overlap || ($r2->x1 >= $r1->x1 && $r2->x1 <= $r1->x2 && $r2->y2 >= $r1->y1 && $r2->y2 <= $r1->y2);
  $overlap = $overlap || ($r2->x2 >= $r1->x1 && $r2->x2 <= $r1->x2 && $r2->y2 >= $r1->y1 && $r2->y2 <= $r1->y2);

  return $overlap;
}
function CreateRectangle($x1, $y1, $x2, $y2){
  $r = new stdClass();
  $r->x1 = $x1;
  $r->y1 = $y1;
  $r->x2 = $x2;
  $r->y2 = $y2;
  return $r;
}
function CopyRectangleValues($rd, $rs){
  $rd->x1 = $rs->x1;
  $rd->y1 = $rs->y1;
  $rd->x2 = $rs->x2;
  $rd->y2 = $rs->y2;
}
function DrawXLabelsForPriority($p, $xMin, $oy, $xMax, $xPixelMin, $xPixelMax, $nextRectangle, $gridLabelColor, $canvas, &$xGridPositions, $xLabels, $xLabelPriorities, &$occupied, $textOnBottom){

  $r = new stdClass();
  $padding = 10.0;

  $overlap = false;
  for($i = 0.0; $i < count($xLabels->stringArray); $i = $i + 1.0){
    if($xLabelPriorities->numberArray[$i] == $p){

      $x = $xGridPositions[$i];
      $px = MapXCoordinate($x, $xMin, $xMax, $xPixelMin, $xPixelMax);
      $text = $xLabels->stringArray[$i]->string;

      $r->x1 = floor($px - GetTextWidth($text)/2.0);
      if($textOnBottom){
        $r->y1 = floor($oy + 5.0);
      }else{
        $r->y1 = floor($oy - 20.0);
      }
      $r->x2 = $r->x1 + GetTextWidth($text);
      $r->y2 = $r->y1 + GetTextHeight($text);

      /* Add padding */
      $r->x1 = $r->x1 - $padding;
      $r->y1 = $r->y1 - $padding;
      $r->x2 = $r->x2 + $padding;
      $r->y2 = $r->y2 + $padding;

      $currentOverlaps = false;

      for($j = 0.0; $j < $nextRectangle->numberValue; $j = $j + 1.0){
        $currentOverlaps = $currentOverlaps || RectanglesOverlap($r, $occupied[$j]);
      }

      if( !$currentOverlaps  && $p == 1.0){
        DrawText($canvas, $r->x1 + $padding, $r->y1 + $padding, $text, $gridLabelColor);

        CopyRectangleValues($occupied[$nextRectangle->numberValue], $r);
        $nextRectangle->numberValue = $nextRectangle->numberValue + 1.0;
      }

      $overlap = $overlap || $currentOverlaps;
    }
  }
  if( !$overlap  && $p != 1.0){
    for($i = 0.0; $i < count($xGridPositions); $i = $i + 1.0){
      $x = $xGridPositions[$i];
      $px = MapXCoordinate($x, $xMin, $xMax, $xPixelMin, $xPixelMax);

      if($xLabelPriorities->numberArray[$i] == $p){
        $text = $xLabels->stringArray[$i]->string;

        $r->x1 = floor($px - GetTextWidth($text)/2.0);
        if($textOnBottom){
          $r->y1 = floor($oy + 5.0);
        }else{
          $r->y1 = floor($oy - 20.0);
        }
        $r->x2 = $r->x1 + GetTextWidth($text);
        $r->y2 = $r->y1 + GetTextHeight($text);

        DrawText($canvas, $r->x1, $r->y1, $text, $gridLabelColor);

        CopyRectangleValues($occupied[$nextRectangle->numberValue], $r);
        $nextRectangle->numberValue = $nextRectangle->numberValue + 1.0;
      }
    }
  }
}
function DrawYLabelsForPriority($p, $yMin, $ox, $yMax, $yPixelMin, $yPixelMax, $nextRectangle, $gridLabelColor, $canvas, &$yGridPositions, $yLabels, $yLabelPriorities, &$occupied, $textOnLeft){

  $r = new stdClass();
  $padding = 10.0;

  $overlap = false;
  for($i = 0.0; $i < count($yLabels->stringArray); $i = $i + 1.0){
    if($yLabelPriorities->numberArray[$i] == $p){

      $y = $yGridPositions[$i];
      $py = MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax);
      $text = $yLabels->stringArray[$i]->string;

      if($textOnLeft){
        $r->x1 = floor($ox - GetTextWidth($text) - 10.0);
      }else{
        $r->x1 = floor($ox + 10.0);
      }
      $r->y1 = floor($py - 6.0);
      $r->x2 = $r->x1 + GetTextWidth($text);
      $r->y2 = $r->y1 + GetTextHeight($text);

      /* Add padding */
      $r->x1 = $r->x1 - $padding;
      $r->y1 = $r->y1 - $padding;
      $r->x2 = $r->x2 + $padding;
      $r->y2 = $r->y2 + $padding;

      $currentOverlaps = false;

      for($j = 0.0; $j < $nextRectangle->numberValue; $j = $j + 1.0){
        $currentOverlaps = $currentOverlaps || RectanglesOverlap($r, $occupied[$j]);
      }

      /* Draw labels with priority 1 if they do not overlap anything else. */
      if( !$currentOverlaps  && $p == 1.0){
        DrawText($canvas, $r->x1 + $padding, $r->y1 + $padding, $text, $gridLabelColor);

        CopyRectangleValues($occupied[$nextRectangle->numberValue], $r);
        $nextRectangle->numberValue = $nextRectangle->numberValue + 1.0;
      }

      $overlap = $overlap || $currentOverlaps;
    }
  }
  if( !$overlap  && $p != 1.0){
    for($i = 0.0; $i < count($yGridPositions); $i = $i + 1.0){
      $y = $yGridPositions[$i];
      $py = MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax);

      if($yLabelPriorities->numberArray[$i] == $p){
        $text = $yLabels->stringArray[$i]->string;

        if($textOnLeft){
          $r->x1 = floor($ox - GetTextWidth($text) - 10.0);
        }else{
          $r->x1 = floor($ox + 10.0);
        }
        $r->y1 = floor($py - 6.0);
        $r->x2 = $r->x1 + GetTextWidth($text);
        $r->y2 = $r->y1 + GetTextHeight($text);

        DrawText($canvas, $r->x1, $r->y1, $text, $gridLabelColor);

        CopyRectangleValues($occupied[$nextRectangle->numberValue], $r);
        $nextRectangle->numberValue = $nextRectangle->numberValue + 1.0;
      }
    }
  }
}
function &ComputeGridLinePositions($cMin, $cMax, $labels, $priorities){

  $cLength = $cMax - $cMin;

  $p = floor(log10($cLength));
  $pInterval = 10.0**$p;
  /* gives 10-1 lines for 100-10 diff */
  $pMin = ceil($cMin/$pInterval)*$pInterval;
  $pMax = floor($cMax/$pInterval)*$pInterval;
  $pNum = Roundx(($pMax - $pMin)/$pInterval + 1.0);

  $mode = 1.0;

  if($pNum <= 3.0){
    $p = floor(log10($cLength) - 1.0);
    /* gives 100-10 lines for 100-10 diff */
    $pInterval = 10.0**$p;
    $pMin = ceil($cMin/$pInterval)*$pInterval;
    $pMax = floor($cMax/$pInterval)*$pInterval;
    $pNum = Roundx(($pMax - $pMin)/$pInterval + 1.0);

    $mode = 4.0;
  }else if($pNum <= 6.0){
    $p = floor(log10($cLength));
    $pInterval = 10.0**$p/4.0;
    /* gives 40-5 lines for 100-10 diff */
    $pMin = ceil($cMin/$pInterval)*$pInterval;
    $pMax = floor($cMax/$pInterval)*$pInterval;
    $pNum = Roundx(($pMax - $pMin)/$pInterval + 1.0);

    $mode = 3.0;
  }else if($pNum <= 10.0){
    $p = floor(log10($cLength));
    $pInterval = 10.0**$p/2.0;
    /* gives 20-3 lines for 100-10 diff */
    $pMin = ceil($cMin/$pInterval)*$pInterval;
    $pMax = floor($cMax/$pInterval)*$pInterval;
    $pNum = Roundx(($pMax - $pMin)/$pInterval + 1.0);

    $mode = 2.0;
  }

  $positions = array_fill(0, $pNum, 0);
  $labels->stringArray = array_fill(0, $pNum, 0);
  $priorities->numberArray = array_fill(0, $pNum, 0);

  for($i = 0.0; $i < $pNum; $i = $i + 1.0){
    $num = $pMin + $pInterval*$i;
    $positions[$i] = $num;

    /* Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn. */
    $priority = 1.0;

    /* Prioritize x.25, x.5 and x.75 lower. */
    if($mode == 2.0 || $mode == 3.0){
      $rem = abs(round($num/10.0**($p - 2.0)))%100.0;

      $priority = 1.0;
      if($rem == 50.0){
        $priority = 2.0;
      }else if($rem == 25.0 || $rem == 75.0){
        $priority = 3.0;
      }
    }

    /* Prioritize x.1-x.4 and x.6-x.9 lower */
    if($mode == 4.0){
      $rem = abs(Roundx($num/10.0**$p))%10.0;

      $priority = 1.0;
      if($rem == 1.0 || $rem == 2.0 || $rem == 3.0 || $rem == 4.0 || $rem == 6.0 || $rem == 7.0 || $rem == 8.0 || $rem == 9.0){
        $priority = 2.0;
      }
    }

    /* 0 has lowest priority. */
    if(EpsilonCompare($num, 0.0, 10.0**($p - 5.0))){
      $priority = 3.0;
    }

    $priorities->numberArray[$i] = $priority;

    /* The label itself. */
    $labels->stringArray[$i] = new stdClass();
    if($p < 0.0){
      if($mode == 2.0 || $mode == 3.0){
        $num = RoundToDigits($num, -($p - 1.0));
      }else{
        $num = RoundToDigits($num, -$p);
      }
    }
    $labels->stringArray[$i]->string = CreateStringDecimalFromNumber($num);
  }

  return $positions;
}
function MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax){

  $yLength = $yMax - $yMin;
  $yPixelLength = $yPixelMax - $yPixelMin;

  $y = $y - $yMin;
  $y = $y*$yPixelLength/$yLength;
  $y = $yPixelLength - $y;
  $y = $y + $yPixelMin;
  return $y;
}
function MapXCoordinate($x, $xMin, $xMax, $xPixelMin, $xPixelMax){

  $xLength = $xMax - $xMin;
  $xPixelLength = $xPixelMax - $xPixelMin;

  $x = $x - $xMin;
  $x = $x*$xPixelLength/$xLength;
  $x = $x + $xPixelMin;
  return $x;
}
function MapXCoordinateAutoSettings($x, $image, &$xs){
  return MapXCoordinate($x, GetMinimum($xs), GetMaximum($xs), GetDefaultPaddingPercentage()*ImageWidth($image), (1.0 - GetDefaultPaddingPercentage())*ImageWidth($image));
}
function MapYCoordinateAutoSettings($y, $image, &$ys){
  return MapYCoordinate($y, GetMinimum($ys), GetMaximum($ys), GetDefaultPaddingPercentage()*ImageHeight($image), (1.0 - GetDefaultPaddingPercentage())*ImageHeight($image));
}
function MapXCoordinateBasedOnSettings($x, $settings){

  $boundaries = new stdClass();
  ComputeBoundariesBasedOnSettings($settings, $boundaries);
  $xMin = $boundaries->x1;
  $xMax = $boundaries->x2;

  if($settings->autoPadding){
    $xPadding = floor(GetDefaultPaddingPercentage()*$settings->width);
  }else{
    $xPadding = $settings->xPadding;
  }

  $xPixelMin = $xPadding;
  $xPixelMax = $settings->width - $xPadding;

  return MapXCoordinate($x, $xMin, $xMax, $xPixelMin, $xPixelMax);
}
function MapYCoordinateBasedOnSettings($y, $settings){

  $boundaries = new stdClass();
  ComputeBoundariesBasedOnSettings($settings, $boundaries);
  $yMin = $boundaries->y1;
  $yMax = $boundaries->y2;

  if($settings->autoPadding){
    $yPadding = floor(GetDefaultPaddingPercentage()*$settings->height);
  }else{
    $yPadding = $settings->yPadding;
  }

  $yPixelMin = $yPadding;
  $yPixelMax = $settings->height - $yPadding;

  return MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax);
}
function GetDefaultPaddingPercentage(){
  return 0.10;
}
function DrawText($canvas, $x, $y, &$text, $color){

  $charWidth = 8.0;
  $spacing = 2.0;

  for($i = 0.0; $i < count($text); $i = $i + 1.0){
    DrawAsciiCharacter($canvas, $x + $i*($charWidth + $spacing), $y, $text[$i], $color);
  }
}
function DrawTextUpwards($canvas, $x, $y, &$text, $color){

  $buffer = CreateImage(GetTextWidth($text), GetTextHeight($text), GetTransparent());
  DrawText($buffer, 0.0, 0.0, $text, $color);
  $rotated = RotateAntiClockwise90Degrees($buffer);
  DrawImageOnImage($canvas, $rotated, $x, $y);
  DeleteImage($buffer);
  DeleteImage($rotated);
}
function GetDefaultScatterPlotSettings(){

  $settings = new stdClass();

  $settings->autoBoundaries = true;
  $settings->xMax = 0.0;
  $settings->xMin = 0.0;
  $settings->yMax = 0.0;
  $settings->yMin = 0.0;
  $settings->autoPadding = true;
  $settings->xPadding = 0.0;
  $settings->yPadding = 0.0;
  $settings->title = array();
  $settings->xLabel = array();
  $settings->yLabel = array();
  $settings->scatterPlotSeries = array_fill(0, 0.0, 0);
  $settings->showGrid = true;
  $settings->gridColor = GetGray(0.1);
  $settings->xAxisAuto = true;
  $settings->xAxisTop = false;
  $settings->xAxisBottom = false;
  $settings->yAxisAuto = true;
  $settings->yAxisLeft = false;
  $settings->yAxisRight = false;

  return $settings;
}
function GetDefaultScatterPlotSeriesSettings(){

  $series = new stdClass();

  $series->linearInterpolation = true;
  $series->pointType = str_split("pixels");
  $series->lineType = str_split("solid");
  $series->lineThickness = 1.0;
  $series->xs = array_fill(0, 0.0, 0);
  $series->ys = array_fill(0, 0.0, 0);
  $series->color = GetBlack();

  return $series;
}
function DrawScatterPlot($canvasReference, $width, $height, &$xs, &$ys){

  $settings = GetDefaultScatterPlotSettings();

  $settings->width = $width;
  $settings->height = $height;
  $settings->scatterPlotSeries = array_fill(0, 1.0, 0);
  $settings->scatterPlotSeries[0.0] = GetDefaultScatterPlotSeriesSettings();
  unset($settings->scatterPlotSeries[0.0]->xs);
  $settings->scatterPlotSeries[0.0]->xs = $xs;
  unset($settings->scatterPlotSeries[0.0]->ys);
  $settings->scatterPlotSeries[0.0]->ys = $ys;

  DrawScatterPlotFromSettings($canvasReference, $settings);
}
function DrawScatterPlotFromSettings($canvasReference, $settings){

  $canvas = CreateImage($settings->width, $settings->height, GetWhite());
  $patternOffset = CreateNumberReference(0.0);

  $success = ScatterPlotFromSettingsValid($settings);

  if($success){

    $boundaries = new stdClass();
    ComputeBoundariesBasedOnSettings($settings, $boundaries);
    $xMin = $boundaries->x1;
    $yMin = $boundaries->y1;
    $xMax = $boundaries->x2;
    $yMax = $boundaries->y2;

    // If zero, set to defaults.
    if($xMin - $xMax == 0){
        $xMin = 0;
        $xMax = 10;
    }

    if($yMin - $yMax == 0){
        $yMin = 0;
        $yMax = 10;
    }

    $xLength = $xMax - $xMin;
    $yLength = $yMax - $yMin;

    if($settings->autoPadding){
      $xPadding = floor(GetDefaultPaddingPercentage()*$settings->width);
      $yPadding = floor(GetDefaultPaddingPercentage()*$settings->height);
    }else{
      $xPadding = $settings->xPadding;
      $yPadding = $settings->yPadding;
    }

    /* Draw title */
    DrawText($canvas, floor($settings->width/2.0 - GetTextWidth($settings->title)/2.0), floor($yPadding/3.0), $settings->title, GetBlack());

    /* Draw grid */
    $xPixelMin = $xPadding;
    $yPixelMin = $yPadding;
    $xPixelMax = $settings->width - $xPadding;
    $yPixelMax = $settings->height - $yPadding;
    $xLengthPixels = $xPixelMax - $xPixelMin;
    $yLengthPixels = $yPixelMax - $yPixelMin;
    DrawRectangle1px($canvas, $xPixelMin, $yPixelMin, $xLengthPixels, $yLengthPixels, $settings->gridColor);

    $gridLabelColor = GetGray(0.5);

    $xLabels = new stdClass();
    $xLabelPriorities = new stdClass();
    $yLabels = new stdClass();
    $yLabelPriorities = new stdClass();
    $xGridPositions = ComputeGridLinePositions($xMin, $xMax, $xLabels, $xLabelPriorities);
    $yGridPositions = ComputeGridLinePositions($yMin, $yMax, $yLabels, $yLabelPriorities);

    if($settings->showGrid){
      /* X-grid */
      for($i = 0.0; $i < count($xGridPositions); $i = $i + 1.0){
        $x = $xGridPositions[$i];
        $px = MapXCoordinate($x, $xMin, $xMax, $xPixelMin, $xPixelMax);
        DrawLine1px($canvas, $px, $yPixelMin, $px, $yPixelMax, $settings->gridColor);
      }

      /* Y-grid */
      for($i = 0.0; $i < count($yGridPositions); $i = $i + 1.0){
        $y = $yGridPositions[$i];
        $py = MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax);
        DrawLine1px($canvas, $xPixelMin, $py, $xPixelMax, $py, $settings->gridColor);
      }
    }

    /* Compute origin information. */
    $originYInside = $yMin < 0.0 && $yMax > 0.0;
    $originY = 0.0;
    if($settings->xAxisAuto){
      if($originYInside){
        $originY = 0.0;
      }else{
        $originY = $yMin;
      }
    }else{
if($settings->xAxisTop){
        $originY = $yMax;
      }
      if($settings->xAxisBottom){
        $originY = $yMin;
      }
    }
    $originYPixels = MapYCoordinate($originY, $yMin, $yMax, $yPixelMin, $yPixelMax);

    $originXInside = $xMin < 0.0 && $xMax > 0.0;
    $originX = 0.0;
    if($settings->yAxisAuto){
      if($originXInside){
        $originX = 0.0;
      }else{
        $originX = $xMin;
      }
    }else{
if($settings->yAxisLeft){
        $originX = $xMin;
      }
      if($settings->yAxisRight){
        $originX = $xMax;
      }
    }
    $originXPixels = MapXCoordinate($originX, $xMin, $xMax, $xPixelMin, $xPixelMax);

    if($originYInside){
      $originTextY = 0.0;
    }else{
      $originTextY = $yMin + $yLength/2.0;
    }
    $originTextYPixels = MapYCoordinate($originTextY, $yMin, $yMax, $yPixelMin, $yPixelMax);

    if($originXInside){
      $originTextX = 0.0;
    }else{
      $originTextX = $xMin + $xLength/2.0;
    }
    $originTextXPixels = MapXCoordinate($originTextX, $xMin, $xMax, $xPixelMin, $xPixelMax);

    /* Labels */
    $occupied = array_fill(0, count($xLabels->stringArray) + count($yLabels->stringArray), 0);
    for($i = 0.0; $i < count($occupied); $i = $i + 1.0){
      $occupied[$i] = CreateRectangle(0.0, 0.0, 0.0, 0.0);
    }
    $nextRectangle = CreateNumberReference(0.0);

    /* x labels */
    for($i = 1.0; $i <= 5.0; $i = $i + 1.0){
      $textOnBottom = true;
      if( !$settings->xAxisAuto  && $settings->xAxisTop){
        $textOnBottom = false;
      }
      DrawXLabelsForPriority($i, $xMin, $originYPixels, $xMax, $xPixelMin, $xPixelMax, $nextRectangle, $gridLabelColor, $canvas, $xGridPositions, $xLabels, $xLabelPriorities, $occupied, $textOnBottom);
    }

    /* y labels */
    for($i = 1.0; $i <= 5.0; $i = $i + 1.0){
      $textOnLeft = true;
      if( !$settings->yAxisAuto  && $settings->yAxisRight){
        $textOnLeft = false;
      }
      DrawYLabelsForPriority($i, $yMin, $originXPixels, $yMax, $yPixelMin, $yPixelMax, $nextRectangle, $gridLabelColor, $canvas, $yGridPositions, $yLabels, $yLabelPriorities, $occupied, $textOnLeft);
    }

    /* Draw origin line axis titles. */
    $axisLabelPadding = 20.0;

    /* x origin line */
    if($originYInside){
      DrawLine1px($canvas, Roundx($xPixelMin), Roundx($originYPixels), Roundx($xPixelMax), Roundx($originYPixels), GetBlack());
    }

    /* y origin line */
    if($originXInside){
      DrawLine1px($canvas, Roundx($originXPixels), Roundx($yPixelMin), Roundx($originXPixels), Roundx($yPixelMax), GetBlack());
    }

    /* Draw origin axis titles. */
    DrawTextUpwards($canvas, 10.0, floor($originTextYPixels - GetTextWidth($settings->yLabel)/2.0), $settings->yLabel, GetBlack());
    DrawText($canvas, floor($originTextXPixels - GetTextWidth($settings->xLabel)/2.0), $yPixelMax + $axisLabelPadding, $settings->xLabel, GetBlack());

    /* X-grid-markers */
    for($i = 0.0; $i < count($xGridPositions); $i = $i + 1.0){
      $x = $xGridPositions[$i];
      $px = MapXCoordinate($x, $xMin, $xMax, $xPixelMin, $xPixelMax);
      $p = $xLabelPriorities->numberArray[$i];
      $l = 1.0;
      if($p == 1.0){
        $l = 8.0;
      }else if($p == 2.0){
        $l = 3.0;
      }
      $side = -1.0;
      if( !$settings->xAxisAuto  && $settings->xAxisTop){
        $side = 1.0;
      }
      DrawLine1px($canvas, $px, $originYPixels, $px, $originYPixels + $side*$l, GetBlack());
    }

    /* Y-grid-markers */
    for($i = 0.0; $i < count($yGridPositions); $i = $i + 1.0){
      $y = $yGridPositions[$i];
      $py = MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax);
      $p = $yLabelPriorities->numberArray[$i];
      $l = 1.0;
      if($p == 1.0){
        $l = 8.0;
      }else if($p == 2.0){
        $l = 3.0;
      }
      $side = 1.0;
      if( !$settings->yAxisAuto  && $settings->yAxisRight){
        $side = -1.0;
      }
      DrawLine1px($canvas, $originXPixels, $py, $originXPixels + $side*$l, $py, GetBlack());
    }

    /* Draw points */
    for($plot = 0.0; $plot < count($settings->scatterPlotSeries); $plot = $plot + 1.0){
      $sp = $settings->scatterPlotSeries[$plot];

      $xs = $sp->xs;
      $ys = $sp->ys;
      $linearInterpolation = $sp->linearInterpolation;

      $x1Ref = new stdClass();
      $y1Ref = new stdClass();
      $x2Ref = new stdClass();
      $y2Ref = new stdClass();
      if($linearInterpolation){
        $prevSet = false;
        $xPrev = 0.0;
        $yPrev = 0.0;
        for($i = 0.0; $i < count($xs); $i = $i + 1.0){
          $x = $xs[$i];
          $y = $ys[$i];

          if($prevSet){
            $x1Ref->numberValue = $xPrev;
            $y1Ref->numberValue = $yPrev;
            $x2Ref->numberValue = $x;
            $y2Ref->numberValue = $y;

            $success = CropLineWithinBoundary($x1Ref, $y1Ref, $x2Ref, $y2Ref, $xMin, $xMax, $yMin, $yMax);

            if($success){
              $pxPrev = floor(MapXCoordinate($x1Ref->numberValue, $xMin, $xMax, $xPixelMin, $xPixelMax));
              $pyPrev = floor(MapYCoordinate($y1Ref->numberValue, $yMin, $yMax, $yPixelMin, $yPixelMax));
              $px = floor(MapXCoordinate($x2Ref->numberValue, $xMin, $xMax, $xPixelMin, $xPixelMax));
              $py = floor(MapYCoordinate($y2Ref->numberValue, $yMin, $yMax, $yPixelMin, $yPixelMax));

              if(aStringsEqual($sp->lineType, $literal = str_split("solid")) && $sp->lineThickness == 1.0){
                DrawLine1px($canvas, $pxPrev, $pyPrev, $px, $py, $sp->color);
              }else if(aStringsEqual($sp->lineType, $literal = str_split("solid"))){
                DrawLine($canvas, $pxPrev, $pyPrev, $px, $py, $sp->lineThickness, $sp->color);
              }else if(aStringsEqual($sp->lineType, $literal = str_split("dashed"))){
                $linePattern = GetLinePattern1();
                DrawLineBresenhamsAlgorithmThickPatterned($canvas, $pxPrev, $pyPrev, $px, $py, $sp->lineThickness, $linePattern, $patternOffset, $sp->color);
              }else if(aStringsEqual($sp->lineType, $literal = str_split("dotted"))){
                $linePattern = GetLinePattern2();
                DrawLineBresenhamsAlgorithmThickPatterned($canvas, $pxPrev, $pyPrev, $px, $py, $sp->lineThickness, $linePattern, $patternOffset, $sp->color);
              }else if(aStringsEqual($sp->lineType, $literal = str_split("dotdash"))){
                $linePattern = GetLinePattern3();
                DrawLineBresenhamsAlgorithmThickPatterned($canvas, $pxPrev, $pyPrev, $px, $py, $sp->lineThickness, $linePattern, $patternOffset, $sp->color);
              }else if(aStringsEqual($sp->lineType, $literal = str_split("longdash"))){
                $linePattern = GetLinePattern4();
                DrawLineBresenhamsAlgorithmThickPatterned($canvas, $pxPrev, $pyPrev, $px, $py, $sp->lineThickness, $linePattern, $patternOffset, $sp->color);
              }else if(aStringsEqual($sp->lineType, $literal = str_split("twodash"))){
                $linePattern = GetLinePattern5();
                DrawLineBresenhamsAlgorithmThickPatterned($canvas, $pxPrev, $pyPrev, $px, $py, $sp->lineThickness, $linePattern, $patternOffset, $sp->color);
              }
            }
          }

          $prevSet = true;
          $xPrev = $x;
          $yPrev = $y;
        }
      }else{
        for($i = 0.0; $i < count($xs); $i = $i + 1.0){
          $x = $xs[$i];
          $y = $ys[$i];

          if($x > $xMin && $x < $xMax && $y > $yMin && $y < $yMax){

            $x = floor(MapXCoordinate($x, $xMin, $xMax, $xPixelMin, $xPixelMax));
            $y = floor(MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax));

            if(aStringsEqual($sp->pointType, $literal = str_split("crosses"))){
              DrawPixel($canvas, $x, $y, $sp->color);
              DrawPixel($canvas, $x + 1.0, $y, $sp->color);
              DrawPixel($canvas, $x + 2.0, $y, $sp->color);
              DrawPixel($canvas, $x - 1.0, $y, $sp->color);
              DrawPixel($canvas, $x - 2.0, $y, $sp->color);
              DrawPixel($canvas, $x, $y + 1.0, $sp->color);
              DrawPixel($canvas, $x, $y + 2.0, $sp->color);
              DrawPixel($canvas, $x, $y - 1.0, $sp->color);
              DrawPixel($canvas, $x, $y - 2.0, $sp->color);
            }else if(aStringsEqual($sp->pointType, $literal = str_split("circles"))){
              DrawCircle($canvas, $x, $y, 3.0, $sp->color);
            }else if(aStringsEqual($sp->pointType, $literal = str_split("dots"))){
              DrawFilledCircle($canvas, $x, $y, 3.0, $sp->color);
            }else if(aStringsEqual($sp->pointType, $literal = str_split("triangles"))){
              DrawTriangle($canvas, $x, $y, 3.0, $sp->color);
            }else if(aStringsEqual($sp->pointType, $literal = str_split("filled triangles"))){
              DrawFilledTriangle($canvas, $x, $y, 3.0, $sp->color);
            }else if(aStringsEqual($sp->pointType, $literal = str_split("pixels"))){
              DrawPixel($canvas, $x, $y, $sp->color);
            }
          }
        }
      }
    }

    DeleteImage($canvasReference->image);
    $canvasReference->image = $canvas;
  }

  return $success;
}
function ComputeBoundariesBasedOnSettings($settings, $boundaries){

  if(count($settings->scatterPlotSeries) >= 1.0){
    $xMin = GetMinimum($settings->scatterPlotSeries[0.0]->xs);
    $xMax = GetMaximum($settings->scatterPlotSeries[0.0]->xs);
    $yMin = GetMinimum($settings->scatterPlotSeries[0.0]->ys);
    $yMax = GetMaximum($settings->scatterPlotSeries[0.0]->ys);
  }else{
    $xMin = -10.0;
    $xMax = 10.0;
    $yMin = -10.0;
    $yMax = 10.0;
  }

  if( !$settings->autoBoundaries ){
    $xMin = $settings->xMin;
    $xMax = $settings->xMax;
    $yMin = $settings->yMin;
    $yMax = $settings->yMax;
  }else{
    for($plot = 1.0; $plot < count($settings->scatterPlotSeries); $plot = $plot + 1.0){
      $sp = $settings->scatterPlotSeries[$plot];

      $xMin = min($xMin, GetMinimum($sp->xs));
      $xMax = max($xMax, GetMaximum($sp->xs));
      $yMin = min($yMin, GetMinimum($sp->ys));
      $yMax = max($yMax, GetMaximum($sp->ys));
    }
  }

  $boundaries->x1 = $xMin;
  $boundaries->y1 = $yMin;
  $boundaries->x2 = $xMax;
  $boundaries->y2 = $yMax;
}
function ScatterPlotFromSettingsValid($settings){

  $success = true;

  /* Check axis placement. */
  if( !$settings->xAxisAuto ){
    if($settings->xAxisTop && $settings->xAxisBottom){
      $success = false;
    }
    if( !$settings->xAxisTop  &&  !$settings->xAxisBottom ){
      $success = false;
    }
  }

  if( !$settings->yAxisAuto ){
    if($settings->yAxisLeft && $settings->yAxisRight){
      $success = false;
    }
    if( !$settings->yAxisLeft  &&  !$settings->yAxisRight ){
      $success = false;
    }
  }

  /* Check series lengths. */
  for($i = 0.0; $i < count($settings->scatterPlotSeries); $i = $i + 1.0){
    $series = $settings->scatterPlotSeries[$i];
    if(count($series->xs) != count($series->ys)){
      $success = false;
    }
    if(count($series->xs) == 0.0){
      $success = false;
    }
    if($series->linearInterpolation && count($series->xs) == 1.0){
      $success = false;
    }
  }

  /* Check bounds. */
  if( !$settings->autoBoundaries ){
    if($settings->xMin >= $settings->xMax){
      $success = false;
    }
    if($settings->yMin >= $settings->yMax){
      $success = false;
    }
  }

  /* Check padding. */
  if( !$settings->autoPadding ){
    if(2.0*$settings->xPadding >= $settings->width){
      $success = false;
    }
    if(2.0*$settings->yPadding >= $settings->height){
      $success = false;
    }
  }

  /* Check width and height. */
  if($settings->width < 0.0){
    $success = false;
  }
  if($settings->height < 0.0){
    $success = false;
  }

  /* Check point types. */
  for($i = 0.0; $i < count($settings->scatterPlotSeries); $i = $i + 1.0){
    $series = $settings->scatterPlotSeries[$i];

    if($series->lineThickness < 0.0){
      $success = false;
    }

    if( !$series->linearInterpolation ){
      /* Point type. */
      $found = false;
      if(aStringsEqual($series->pointType, $literal = str_split("crosses"))){
        $found = true;
      }else if(aStringsEqual($series->pointType, $literal = str_split("circles"))){
        $found = true;
      }else if(aStringsEqual($series->pointType, $literal = str_split("dots"))){
        $found = true;
      }else if(aStringsEqual($series->pointType, $literal = str_split("triangles"))){
        $found = true;
      }else if(aStringsEqual($series->pointType, $literal = str_split("filled triangles"))){
        $found = true;
      }else if(aStringsEqual($series->pointType, $literal = str_split("pixels"))){
        $found = true;
      }
      if( !$found ){
        $success = false;
      }
    }else{
      /* Line type. */
      $found = false;
      if(aStringsEqual($series->lineType, $literal = str_split("solid"))){
        $found = true;
      }else if(aStringsEqual($series->lineType, $literal = str_split("dashed"))){
        $found = true;
      }else if(aStringsEqual($series->lineType, $literal = str_split("dotted"))){
        $found = true;
      }else if(aStringsEqual($series->lineType, $literal = str_split("dotdash"))){
        $found = true;
      }else if(aStringsEqual($series->lineType, $literal = str_split("longdash"))){
        $found = true;
      }else if(aStringsEqual($series->lineType, $literal = str_split("twodash"))){
        $found = true;
      }

      if( !$found ){
        $success = false;
      }
    }
  }

  return $success;
}
function GetDefaultBarPlotSettings(){

  $settings = new stdClass();

  $settings->width = 800.0;
  $settings->height = 600.0;
  $settings->autoBoundaries = true;
  $settings->yMax = 0.0;
  $settings->yMin = 0.0;
  $settings->autoPadding = true;
  $settings->xPadding = 0.0;
  $settings->yPadding = 0.0;
  $settings->title = array();
  $settings->yLabel = array();
  $settings->barPlotSeries = array_fill(0, 0.0, 0);
  $settings->showGrid = true;
  $settings->gridColor = GetGray(0.1);
  $settings->autoColor = true;
  $settings->grayscaleAutoColor = false;
  $settings->autoSpacing = true;
  $settings->groupSeparation = 0.0;
  $settings->barSeparation = 0.0;
  $settings->autoLabels = true;
  $settings->xLabels = array_fill(0, 0.0, 0);
  /*settings.autoLabels = false;
        settings.xLabels = new StringReference [5];
        settings.xLabels[0] = CreateStringReference("may 20".toCharArray());
        settings.xLabels[1] = CreateStringReference("jun 20".toCharArray());
        settings.xLabels[2] = CreateStringReference("jul 20".toCharArray());
        settings.xLabels[3] = CreateStringReference("aug 20".toCharArray());
        settings.xLabels[4] = CreateStringReference("sep 20".toCharArray()); */
  $settings->barBorder = false;

  return $settings;
}
function GetDefaultBarPlotSeriesSettings(){

  $series = new stdClass();

  $series->ys = array_fill(0, 0.0, 0);
  $series->color = GetBlack();

  return $series;
}
function DrawBarPlot($width, $height, &$ys){

  $settings = GetDefaultBarPlotSettings();

  $settings->barPlotSeries = array_fill(0, 1.0, 0);
  $settings->barPlotSeries[0.0] = GetDefaultBarPlotSeriesSettings();
  unset($settings->barPlotSeries[0.0]->ys);
  $settings->barPlotSeries[0.0]->ys = $ys;
  $canvasReference = new stdClass();
  $settings->width = $width;
  $settings->height = $height;

  DrawBarPlotFromSettings($canvasReference, $settings);

  return $canvasReference->image;
}
function DrawBarPlotFromSettings($canvasReference, $settings){

  $success = BarPlotSettingsIsValid($settings);

  if($success){

    $canvas = CreateImage($settings->width, $settings->height, GetWhite());

    $ss = count($settings->barPlotSeries);
    $gridLabelColor = GetGray(0.5);

    /* padding */
    if($settings->autoPadding){
      $xPadding = floor(GetDefaultPaddingPercentage()*ImageWidth($canvas));
      $yPadding = floor(GetDefaultPaddingPercentage()*ImageHeight($canvas));
    }else{
      $xPadding = $settings->xPadding;
      $yPadding = $settings->yPadding;
    }

    /* Draw title */
    DrawText($canvas, floor(ImageWidth($canvas)/2.0 - GetTextWidth($settings->title)/2.0), floor($yPadding/3.0), $settings->title, GetBlack());
    DrawTextUpwards($canvas, 10.0, floor(ImageHeight($canvas)/2.0 - GetTextWidth($settings->yLabel)/2.0), $settings->yLabel, GetBlack());

    /* min and max */
    if($settings->autoBoundaries){
      if($ss >= 1.0){
        $yMax = GetMaximum($settings->barPlotSeries[0.0]->ys);
        $yMin = min(0.0, GetMinimum($settings->barPlotSeries[0.0]->ys));

        for($s = 0.0; $s < $ss; $s = $s + 1.0){
          $yMax = max($yMax, GetMaximum($settings->barPlotSeries[$s]->ys));
          $yMin = min($yMin, GetMinimum($settings->barPlotSeries[$s]->ys));
        }
      }else{
        $yMax = 10.0;
        $yMin = 0.0;
      }
    }else{
      $yMin = $settings->yMin;
      $yMax = $settings->yMax;
    }
    $yLength = $yMax - $yMin;

    /* boundaries */
    $xPixelMin = $xPadding;
    $yPixelMin = $yPadding;
    $xPixelMax = ImageWidth($canvas) - $xPadding;
    $yPixelMax = ImageHeight($canvas) - $yPadding;
    $xLengthPixels = $xPixelMax - $xPixelMin;
    $yLengthPixels = $yPixelMax - $yPixelMin;

    /* Draw boundary. */
    DrawRectangle1px($canvas, $xPixelMin, $yPixelMin, $xLengthPixels, $yLengthPixels, $settings->gridColor);

    /* Draw grid lines. */
    $yLabels = new stdClass();
    $yLabelPriorities = new stdClass();
    $yGridPositions = ComputeGridLinePositions($yMin, $yMax, $yLabels, $yLabelPriorities);

    if($settings->showGrid){
      /* Y-grid */
      for($i = 0.0; $i < count($yGridPositions); $i = $i + 1.0){
        $y = $yGridPositions[$i];
        $py = MapYCoordinate($y, $yMin, $yMax, $yPixelMin, $yPixelMax);
        DrawLine1px($canvas, $xPixelMin, $py, $xPixelMax, $py, $settings->gridColor);
      }
    }

    /* Draw origin. */
    if($yMin < 0.0 && $yMax > 0.0){
      $py = MapYCoordinate(0.0, $yMin, $yMax, $yPixelMin, $yPixelMax);
      DrawLine1px($canvas, $xPixelMin, $py, $xPixelMax, $py, $settings->gridColor);
    }

    /* Labels */
    $occupied = array_fill(0, count($yLabels->stringArray), 0);
    for($i = 0.0; $i < count($occupied); $i = $i + 1.0){
      $occupied[$i] = CreateRectangle(0.0, 0.0, 0.0, 0.0);
    }
    $nextRectangle = CreateNumberReference(0.0);

    for($i = 1.0; $i <= 5.0; $i = $i + 1.0){
      DrawYLabelsForPriority($i, $yMin, $xPixelMin, $yMax, $yPixelMin, $yPixelMax, $nextRectangle, $gridLabelColor, $canvas, $yGridPositions, $yLabels, $yLabelPriorities, $occupied, true);
    }

    /* Draw bars. */
    if($settings->autoColor){
      if( !$settings->grayscaleAutoColor ){
        $colors = Get8HighContrastColors();
      }else{
        $colors = array_fill(0, $ss, 0);
        if($ss > 1.0){
          for($i = 0.0; $i < $ss; $i = $i + 1.0){
            $colors[$i] = GetGray(0.7 - ($i/$ss)*0.7);
          }
        }else{
          $colors[0.0] = GetGray(0.5);
        }
      }
    }else{
      $colors = array_fill(0, 0.0, 0);
    }

    /* distances */
    $bs = count($settings->barPlotSeries[0.0]->ys);

    if($settings->autoSpacing){
      $groupSeparation = ImageWidth($canvas)*0.05;
      $barSeparation = ImageWidth($canvas)*0.005;
    }else{
      $groupSeparation = $settings->groupSeparation;
      $barSeparation = $settings->barSeparation;
    }

    $barWidth = ($xLengthPixels - $groupSeparation*($bs - 1.0) - $barSeparation*($bs*($ss - 1.0)))/($bs*$ss);

    /* Draw bars. */
    $b = 0.0;
    for($n = 0.0; $n < $bs; $n = $n + 1.0){
      for($s = 0.0; $s < $ss; $s = $s + 1.0){
        $ys = $settings->barPlotSeries[$s]->ys;

        $yValue = $ys[$n];

        $yBottom = MapYCoordinate($yValue, $yMin, $yMax, $yPixelMin, $yPixelMax);
        $yTop = MapYCoordinate(0.0, $yMin, $yMax, $yPixelMin, $yPixelMax);

        $x = $xPixelMin + $n*($groupSeparation + $ss*$barWidth) + $s*($barWidth) + $b*$barSeparation;
        $w = $barWidth;

        if($yValue >= 0.0){
          $y = $yBottom;
          $h = $yTop - $y;
        }else{
          $y = $yTop;
          $h = $yBottom - $yTop;
        }

        /* Cut at boundaries. */
        if($y < $yPixelMin && $y + $h > $yPixelMax){
          $y = $yPixelMin;
          $h = $yPixelMax - $yPixelMin;
        }else if($y < $yPixelMin){
          $y = $yPixelMin;
          if($yValue >= 0.0){
            $h = $yTop - $y;
          }else{
            $h = $yBottom - $y;
          }
        }else if($y + $h > $yPixelMax){
          $h = $yPixelMax - $y;
        }

        /* Get color */
        if($settings->autoColor){
          $barColor = $colors[$s];
        }else{
          $barColor = $settings->barPlotSeries[$s]->color;
        }

        /* Draw */
        if($settings->barBorder){
          DrawFilledRectangleWithBorder($canvas, Roundx($x), Roundx($y), Roundx($w), Roundx($h), GetBlack(), $barColor);
        }else{
          DrawFilledRectangle($canvas, Roundx($x), Roundx($y), Roundx($w), Roundx($h), $barColor);
        }

        $b = $b + 1.0;
      }
      $b = $b - 1.0;
    }

    /* x-labels */
    for($n = 0.0; $n < $bs; $n = $n + 1.0){
      if($settings->autoLabels){
        $label = CreateStringDecimalFromNumber($n + 1.0);
      }else{
        $label = $settings->xLabels[$n]->string;
      }

      $textwidth = GetTextWidth($label);

      $x = $xPixelMin + ($n + 0.5)*($ss*$barWidth + ($ss - 1.0)*$barSeparation) + $n*$groupSeparation - $textwidth/2.0;

      DrawText($canvas, floor($x), ImageHeight($canvas) - $yPadding + 20.0, $label, $gridLabelColor);

      $b = $b + 1.0;
    }

    $canvasReference->image = $canvas;
  }

  return $success;
}
function BarPlotSettingsIsValid($settings){

  $success = true;

  /* Check series lengths. */
  $lengthSet = false;
  $length = 0.0;
  for($i = 0.0; $i < count($settings->barPlotSeries); $i = $i + 1.0){
    $series = $settings->barPlotSeries[$i];

    if( !$lengthSet ){
      $length = count($series->ys);
      $lengthSet = true;
    }else if($length != count($series->ys)){
      $success = false;
    }
  }

  /* Check bounds. */
  if( !$settings->autoBoundaries ){
    if($settings->yMin >= $settings->yMax){
      $success = false;
    }
  }

  /* Check padding. */
  if( !$settings->autoPadding ){
    if(2.0*$settings->xPadding >= $settings->width){
      $success = false;
    }
    if(2.0*$settings->yPadding >= $settings->height){
      $success = false;
    }
  }

  /* Check width and height. */
  if($settings->width < 0.0){
    $success = false;
  }
  if($settings->height < 0.0){
    $success = false;
  }

  /* Check spacing */
  if( !$settings->autoSpacing ){
    if($settings->groupSeparation < 0.0){
      $success = false;
    }
    if($settings->barSeparation < 0.0){
      $success = false;
    }
  }

  return $success;
}
function GetMinimum(&$data){

  $minimum = $data[0.0];
  for($i = 0.0; $i < count($data); $i = $i + 1.0){
    $minimum = min($minimum, $data[$i]);
  }

  return $minimum;
}
function GetMaximum(&$data){

  $maximum = $data[0.0];
  for($i = 0.0; $i < count($data); $i = $i + 1.0){
    $maximum = max($maximum, $data[$i]);
  }

  return $maximum;
}
function RoundToDigits($element, $digitsAfterPoint){
  return Roundx($element*10.0**$digitsAfterPoint)/10.0**$digitsAfterPoint;
}
function test(){

  $failures = CreateNumberReference(0.0);

  $imageReference = CreateRGBABitmapImageReference();

  $labels = new stdClass();
  $labelPriorities = new stdClass();

  $z = 10.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 11.0, $failures);

  $z = 9.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 19.0, $failures);

  $z = 8.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 17.0, $failures);

  $z = 7.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 15.0, $failures);

  $z = 6.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 13.0, $failures);

  $z = 5.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 21.0, $failures);

  $z = 4.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 17.0, $failures);

  $z = 3.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 31.0, $failures);

  $z = 2.0;
  $gridlines = ComputeGridLinePositions(-$z/2.0, $z/2.0, $labels, $labelPriorities);
  AssertEquals(count($gridlines), 21.0, $failures);

  $xs = array_fill(0, 5.0, 0);
  $xs[0.0] = -2.0;
  $xs[1.0] = -1.0;
  $xs[2.0] = 0.0;
  $xs[3.0] = 1.0;
  $xs[4.0] = 2.0;
  $ys = array_fill(0, 5.0, 0);
  $ys[0.0] = 2.0;
  $ys[1.0] = -1.0;
  $ys[2.0] = -2.0;
  $ys[3.0] = -1.0;
  $ys[4.0] = 2.0;
  DrawScatterPlot($imageReference, 800.0, 600.0, $xs, $ys);

  $imageReference->image = DrawBarPlot(800.0, 600.0, $ys);

  TestMapping($failures);
  TestMapping2($failures);

  return $failures->numberValue;
}
function TestMapping($failures){

  $series = GetDefaultScatterPlotSeriesSettings();

  $series->xs = array_fill(0, 5.0, 0);
  $series->xs[0.0] = -2.0;
  $series->xs[1.0] = -1.0;
  $series->xs[2.0] = 0.0;
  $series->xs[3.0] = 1.0;
  $series->xs[4.0] = 2.0;
  $series->ys = array_fill(0, 5.0, 0);
  $series->ys[0.0] = -2.0;
  $series->ys[1.0] = -1.0;
  $series->ys[2.0] = -2.0;
  $series->ys[3.0] = -1.0;
  $series->ys[4.0] = 2.0;
  $series->linearInterpolation = true;
  $series->lineType = str_split("dashed");
  $series->lineThickness = 2.0;
  $series->color = GetGray(0.3);

  $settings = GetDefaultScatterPlotSettings();
  $settings->width = 600.0;
  $settings->height = 400.0;
  $settings->autoBoundaries = true;
  $settings->autoPadding = true;
  $settings->title = str_split("x^2 - 2");
  $settings->xLabel = str_split("X axis");
  $settings->yLabel = str_split("Y axis");
  $settings->scatterPlotSeries = array_fill(0, 1.0, 0);
  $settings->scatterPlotSeries[0.0] = $series;

  $imageReference = CreateRGBABitmapImageReference();
  DrawScatterPlotFromSettings($imageReference, $settings);

  $x1 = MapXCoordinateAutoSettings(-1.0, $imageReference->image, $series->xs);
  $y1 = MapYCoordinateAutoSettings(-1.0, $imageReference->image, $series->ys);

  AssertEquals($x1, 180.0, $failures);
  AssertEquals($y1, 280.0, $failures);
}
function TestMapping2($failures){

  $points = 300.0;
  $w = 600.0*2.0;
  $h = 300.0*2.0;
  $xMin = 0.0;
  $xMax = 150.0;
  $yMin = 0.0;
  $yMax = 1.0;

  $xs = array_fill(0, $points, 0);
  $ys = array_fill(0, $points, 0);
  $xs2 = array_fill(0, $points, 0);
  $ys2 = array_fill(0, $points, 0);

  for($i = 0.0; $i < $points; $i = $i + 1.0){
    $x = $xMin + ($xMax - $xMin)/($points - 1.0)*$i;
    /* points - 1d is to ensure both extremeties are included. */
    $y = $x/($x + 7.0);

    $xs[$i] = $x;
    $ys[$i] = $y;

    $y = 1.4*$x/($x + 7.0)*(1.0 - (atan(($x/1.5 - 30.0)/5.0)/1.6 + 1.0)/2.0);

    $xs2[$i] = $x;
    $ys2[$i] = $y;
  }

  $settings = GetDefaultScatterPlotSettings();

  $settings->scatterPlotSeries = array_fill(0, 2.0, 0);
  $settings->scatterPlotSeries[0.0] = new stdClass();
  $settings->scatterPlotSeries[0.0]->xs = $xs;
  $settings->scatterPlotSeries[0.0]->ys = $ys;
  $settings->scatterPlotSeries[0.0]->linearInterpolation = true;
  $settings->scatterPlotSeries[0.0]->lineType = str_split("solid");
  $settings->scatterPlotSeries[0.0]->lineThickness = 3.0;
  $settings->scatterPlotSeries[0.0]->color = CreateRGBColor(1.0, 0.0, 0.0);
  $settings->scatterPlotSeries[1.0] = new stdClass();
  $settings->scatterPlotSeries[1.0]->xs = $xs2;
  $settings->scatterPlotSeries[1.0]->ys = $ys2;
  $settings->scatterPlotSeries[1.0]->linearInterpolation = true;
  $settings->scatterPlotSeries[1.0]->lineType = str_split("solid");
  $settings->scatterPlotSeries[1.0]->lineThickness = 3.0;
  $settings->scatterPlotSeries[1.0]->color = CreateRGBColor(0.0, 0.0, 1.0);
  $settings->autoBoundaries = false;
  $settings->xMin = $xMin;
  $settings->xMax = $xMax;
  $settings->yMin = $yMin;
  $settings->yMax = $yMax;
  $settings->yLabel = array();
  $settings->xLabel = str_split("Features");
  $settings->title = array();
  $settings->width = $w;
  $settings->height = $h;

  $canvasReference = CreateRGBABitmapImageReference();

  DrawScatterPlotFromSettings($canvasReference, $settings);

  $x1 = MapXCoordinateBasedOnSettings(27.0, $settings);
  $y1 = MapYCoordinateBasedOnSettings(1.0, $settings);

  AssertEquals(floor($x1), 292.0, $failures);
  AssertEquals($y1, 60.0, $failures);
}
function GetBlack(){
  $black = new stdClass();
  $black->a = 1.0;
  $black->r = 0.0;
  $black->g = 0.0;
  $black->b = 0.0;
  return $black;
}
function GetWhite(){
  $white = new stdClass();
  $white->a = 1.0;
  $white->r = 1.0;
  $white->g = 1.0;
  $white->b = 1.0;
  return $white;
}
function GetTransparent(){
  $transparent = new stdClass();
  $transparent->a = 0.0;
  $transparent->r = 0.0;
  $transparent->g = 0.0;
  $transparent->b = 0.0;
  return $transparent;
}
function GetGray($percentage){
  $black = new stdClass();
  $black->a = 1.0;
  $black->r = 1.0 - $percentage;
  $black->g = 1.0 - $percentage;
  $black->b = 1.0 - $percentage;
  return $black;
}
function CreateRGBColor($r, $g, $b){
  $color = new stdClass();
  $color->a = 1.0;
  $color->r = $r;
  $color->g = $g;
  $color->b = $b;
  return $color;
}
function CreateRGBAColor($r, $g, $b, $a){
  $color = new stdClass();
  $color->a = $a;
  $color->r = $r;
  $color->g = $g;
  $color->b = $b;
  return $color;
}
function CreateImage($w, $h, $color){

  $image = new stdClass();
  $image->x = array_fill(0, $w, 0);
  for($i = 0.0; $i < $w; $i = $i + 1.0){
    $image->x[$i] = new stdClass();
    $image->x[$i]->y = array_fill(0, $h, 0);
    for($j = 0.0; $j < $h; $j = $j + 1.0){
      $image->x[$i]->y[$j] = new stdClass();
      SetPixel($image, $i, $j, $color);
    }
  }

  return $image;
}
function DeleteImage($image){

  $w = ImageWidth($image);
  $h = ImageHeight($image);

  for($i = 0.0; $i < $w; $i = $i + 1.0){
    for($j = 0.0; $j < $h; $j = $j + 1.0){
      unset($image->x[$i]->y[$j]);
    }
    unset($image->x[$i]);
  }
  unset($image);
}
function ImageWidth($image){
  return count($image->x);
}
function ImageHeight($image){

  if(ImageWidth($image) == 0.0){
    $height = 0.0;
  }else{
    $height = count($image->x[0.0]->y);
  }

  return $height;
}
function SetPixel($image, $x, $y, $color){
  if($x >= 0.0 && $x < ImageWidth($image) && $y >= 0.0 && $y < ImageHeight($image)){
    $image->x[$x]->y[$y]->a = $color->a;
    $image->x[$x]->y[$y]->r = $color->r;
    $image->x[$x]->y[$y]->g = $color->g;
    $image->x[$x]->y[$y]->b = $color->b;
  }
}
function DrawPixel($image, $x, $y, $color){

  if($x >= 0.0 && $x < ImageWidth($image) && $y >= 0.0 && $y < ImageHeight($image)){
    $ra = $color->r;
    $ga = $color->g;
    $ba = $color->b;
    $aa = $color->a;

    $rb = $image->x[$x]->y[$y]->r;
    $gb = $image->x[$x]->y[$y]->g;
    $bb = $image->x[$x]->y[$y]->b;
    $ab = $image->x[$x]->y[$y]->a;

    $ao = CombineAlpha($aa, $ab);

    $ro = AlphaBlend($ra, $aa, $rb, $ab, $ao);
    $go = AlphaBlend($ga, $aa, $gb, $ab, $ao);
    $bo = AlphaBlend($ba, $aa, $bb, $ab, $ao);

    $image->x[$x]->y[$y]->r = $ro;
    $image->x[$x]->y[$y]->g = $go;
    $image->x[$x]->y[$y]->b = $bo;
    $image->x[$x]->y[$y]->a = $ao;
  }
}
function CombineAlpha($as, $ad){
  return $as + $ad*(1.0 - $as);
}
function AlphaBlend($cs, $as, $cd, $ad, $ao){
  return ($cs*$as + $cd*$ad*(1.0 - $as))/$ao;
}
function DrawHorizontalLine1px($image, $x, $y, $length, $color){

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    DrawPixel($image, $x + $i, $y, $color);
  }
}
function DrawVerticalLine1px($image, $x, $y, $height, $color){

  for($i = 0.0; $i < $height; $i = $i + 1.0){
    DrawPixel($image, $x, $y + $i, $color);
  }
}
function DrawRectangle1px($image, $x, $y, $width, $height, $color){
  DrawHorizontalLine1px($image, $x, $y, $width + 1.0, $color);
  DrawVerticalLine1px($image, $x, $y + 1.0, $height + 1.0 - 1.0, $color);
  DrawVerticalLine1px($image, $x + $width, $y + 1.0, $height + 1.0 - 1.0, $color);
  DrawHorizontalLine1px($image, $x + 1.0, $y + $height, $width + 1.0 - 2.0, $color);
}
function DrawImageOnImage($dst, $src, $topx, $topy){

  for($y = 0.0; $y < ImageHeight($src); $y = $y + 1.0){
    for($x = 0.0; $x < ImageWidth($src); $x = $x + 1.0){
      if($topx + $x >= 0.0 && $topx + $x < ImageWidth($dst) && $topy + $y >= 0.0 && $topy + $y < ImageHeight($dst)){
        DrawPixel($dst, $topx + $x, $topy + $y, $src->x[$x]->y[$y]);
      }
    }
  }
}
function DrawLine1px($image, $x0, $y0, $x1, $y1, $color){
  XiaolinWusLineAlgorithm($image, $x0, $y0, $x1, $y1, $color);
}
function XiaolinWusLineAlgorithm($image, $x0, $y0, $x1, $y1, $color){

  $olda = $color->a;

  $steep = abs($y1 - $y0) > abs($x1 - $x0);

  if($steep){
    $t = $x0;
    $x0 = $y0;
    $y0 = $t;

    $t = $x1;
    $x1 = $y1;
    $y1 = $t;
  }
  if($x0 > $x1){
    $t = $x0;
    $x0 = $x1;
    $x1 = $t;

    $t = $y0;
    $y0 = $y1;
    $y1 = $t;
  }

  $dx = $x1 - $x0;
  $dy = $y1 - $y0;
  $g = $dy/$dx;

  if($dx == 0.0){
    $g = 1.0;
  }

  $xEnd = Roundx($x0);
  $yEnd = $y0 + $g*($xEnd - $x0);
  $xGap = OneMinusFractionalPart($x0 + 0.5);
  $xpxl1 = $xEnd;
  $ypxl1 = floor($yEnd);
  if($steep){
    DrawPixel($image, $ypxl1, $xpxl1, SetBrightness($color, OneMinusFractionalPart($yEnd)*$xGap));
    DrawPixel($image, $ypxl1 + 1.0, $xpxl1, SetBrightness($color, FractionalPart($yEnd)*$xGap));
  }else{
    DrawPixel($image, $xpxl1, $ypxl1, SetBrightness($color, OneMinusFractionalPart($yEnd)*$xGap));
    DrawPixel($image, $xpxl1, $ypxl1 + 1.0, SetBrightness($color, FractionalPart($yEnd)*$xGap));
  }
  $intery = $yEnd + $g;

  $xEnd = Roundx($x1);
  $yEnd = $y1 + $g*($xEnd - $x1);
  $xGap = FractionalPart($x1 + 0.5);
  $xpxl2 = $xEnd;
  $ypxl2 = floor($yEnd);
  if($steep){
    DrawPixel($image, $ypxl2, $xpxl2, SetBrightness($color, OneMinusFractionalPart($yEnd)*$xGap));
    DrawPixel($image, $ypxl2 + 1.0, $xpxl2, SetBrightness($color, FractionalPart($yEnd)*$xGap));
  }else{
    DrawPixel($image, $xpxl2, $ypxl2, SetBrightness($color, OneMinusFractionalPart($yEnd)*$xGap));
    DrawPixel($image, $xpxl2, $ypxl2 + 1.0, SetBrightness($color, FractionalPart($yEnd)*$xGap));
  }

  if($steep){
    for($x = $xpxl1 + 1.0; $x <= $xpxl2 - 1.0; $x = $x + 1.0){
      DrawPixel($image, floor($intery), $x, SetBrightness($color, OneMinusFractionalPart($intery)));
      DrawPixel($image, floor($intery) + 1.0, $x, SetBrightness($color, FractionalPart($intery)));
      $intery = $intery + $g;
    }
  }else{
    for($x = $xpxl1 + 1.0; $x <= $xpxl2 - 1.0; $x = $x + 1.0){
      DrawPixel($image, $x, floor($intery), SetBrightness($color, OneMinusFractionalPart($intery)));
      DrawPixel($image, $x, floor($intery) + 1.0, SetBrightness($color, FractionalPart($intery)));
      $intery = $intery + $g;
    }
  }

  $color->a = $olda;
}
function OneMinusFractionalPart($x){
  return 1.0 - FractionalPart($x);
}
function FractionalPart($x){
  return $x - floor($x);
}
function SetBrightness($color, $newBrightness){
  $color->a = $newBrightness;
  return $color;
}
function DrawQuadraticBezierCurve($image, $x0, $y0, $cx, $cy, $x1, $y1, $color){

  $dx = abs($x0 - $x1);
  $dy = abs($y0 - $y1);

  $dt = 1.0/sqrt($dx**2.0 + $dy**2.0);

  $xs = new stdClass();
  $ys = new stdClass();
  $xe = new stdClass();
  $ye = new stdClass();

  QuadraticBezierPoint($x0, $y0, $cx, $cy, $x1, $y1, 0.0, $xs, $ys);
  for($t = $dt; $t <= 1.0; $t = $t + $dt){
    QuadraticBezierPoint($x0, $y0, $cx, $cy, $x1, $y1, $t, $xe, $ye);
    DrawLine1px($image, $xs->numberValue, $ys->numberValue, $xe->numberValue, $ye->numberValue, $color);
    $xs->numberValue = $xe->numberValue;
    $ys->numberValue = $ye->numberValue;
  }

  unset($xs);
  unset($ys);
  unset($xe);
  unset($ye);
}
function QuadraticBezierPoint($x0, $y0, $cx, $cy, $x1, $y1, $t, $x, $y){
  $x->numberValue = (1.0 - $t)**2.0*$x0 + (1.0 - $t)*2.0*$t*$cx + $t**2.0*$x1;
  $y->numberValue = (1.0 - $t)**2.0*$y0 + (1.0 - $t)*2.0*$t*$cy + $t**2.0*$y1;
}
function DrawCubicBezierCurve($image, $x0, $y0, $c0x, $c0y, $c1x, $c1y, $x1, $y1, $color){

  $dx = abs($x0 - $x1);
  $dy = abs($y0 - $y1);

  $dt = 1.0/sqrt($dx**2.0 + $dy**2.0);

  $xs = new stdClass();
  $ys = new stdClass();
  $xe = new stdClass();
  $ye = new stdClass();

  CubicBezierPoint($x0, $y0, $c0x, $c0y, $c1x, $c1y, $x1, $y1, 0.0, $xs, $ys);
  for($t = $dt; $t <= 1.0; $t = $t + $dt){
    CubicBezierPoint($x0, $y0, $c0x, $c0y, $c1x, $c1y, $x1, $y1, $t, $xe, $ye);
    DrawLine1px($image, $xs->numberValue, $ys->numberValue, $xe->numberValue, $ye->numberValue, $color);
    $xs->numberValue = $xe->numberValue;
    $ys->numberValue = $ye->numberValue;
  }

  unset($xs);
  unset($ys);
  unset($xe);
  unset($ye);
}
function CubicBezierPoint($x0, $y0, $c0x, $c0y, $c1x, $c1y, $x1, $y1, $t, $x, $y){
  $x->numberValue = (1.0 - $t)**3.0*$x0 + (1.0 - $t)**2.0*3.0*$t*$c0x + (1.0 - $t)*3.0*$t**2.0*$c1x + $t**3.0*$x1;

  $y->numberValue = (1.0 - $t)**3.0*$y0 + (1.0 - $t)**2.0*3.0*$t*$c0y + (1.0 - $t)*3.0*$t**2.0*$c1y + $t**3.0*$y1;
}
function CopyImage($image){

  $copyx = CreateImage(ImageWidth($image), ImageHeight($image), GetTransparent());

  for($i = 0.0; $i < ImageWidth($image); $i = $i + 1.0){
    for($j = 0.0; $j < ImageHeight($image); $j = $j + 1.0){
      SetPixel($copyx, $i, $j, $image->x[$i]->y[$j]);
    }
  }

  return $copyx;
}
function GetImagePixel($image, $x, $y){
  return $image->x[$x]->y[$y];
}
function HorizontalFlip($img){

  for($y = 0.0; $y < ImageHeight($img); $y = $y + 1.0){
    for($x = 0.0; $x < ImageWidth($img)/2.0; $x = $x + 1.0){
      $c1 = $img->x[$x]->y[$y];
      $c2 = $img->x[ImageWidth($img) - 1.0 - $x]->y[$y];

      $tmp = $c1->a;
      $c1->a = $c2->a;
      $c2->a = $tmp;

      $tmp = $c1->r;
      $c1->r = $c2->r;
      $c2->r = $tmp;

      $tmp = $c1->g;
      $c1->g = $c2->g;
      $c2->g = $tmp;

      $tmp = $c1->b;
      $c1->b = $c2->b;
      $c2->b = $tmp;
    }
  }
}
function DrawFilledRectangle($image, $x, $y, $w, $h, $color){

  for($i = 0.0; $i < $w; $i = $i + 1.0){
    for($j = 0.0; $j < $h; $j = $j + 1.0){
      SetPixel($image, $x + $i, $y + $j, $color);
    }
  }
}
function RotateAntiClockwise90Degrees($image){

  $rotated = CreateImage(ImageHeight($image), ImageWidth($image), GetBlack());

  for($y = 0.0; $y < ImageHeight($image); $y = $y + 1.0){
    for($x = 0.0; $x < ImageWidth($image); $x = $x + 1.0){
      SetPixel($rotated, $y, ImageWidth($image) - 1.0 - $x, GetImagePixel($image, $x, $y));
    }
  }

  return $rotated;
}
function DrawCircle($canvas, $xCenter, $yCenter, $radius, $color){
  DrawCircleBasicAlgorithm($canvas, $xCenter, $yCenter, $radius, $color);
}
function BresenhamsCircleDrawingAlgorithm($canvas, $xCenter, $yCenter, $radius, $color){

  $y = $radius;
  $x = 0.0;

  $delta = 3.0 - 2.0*$radius;
  for(; $y >= $x; $x = $x + 1.0){
    DrawLine1px($canvas, $xCenter + $x, $yCenter + $y, $xCenter + $x, $yCenter + $y, $color);
    DrawLine1px($canvas, $xCenter + $x, $yCenter - $y, $xCenter + $x, $yCenter - $y, $color);
    DrawLine1px($canvas, $xCenter - $x, $yCenter + $y, $xCenter - $x, $yCenter + $y, $color);
    DrawLine1px($canvas, $xCenter - $x, $yCenter - $y, $xCenter - $x, $yCenter - $y, $color);

    DrawLine1px($canvas, $xCenter - $y, $yCenter + $x, $xCenter - $y, $yCenter + $x, $color);
    DrawLine1px($canvas, $xCenter - $y, $yCenter - $x, $xCenter - $y, $yCenter - $x, $color);
    DrawLine1px($canvas, $xCenter + $y, $yCenter + $x, $xCenter + $y, $yCenter + $x, $color);
    DrawLine1px($canvas, $xCenter + $y, $yCenter - $x, $xCenter + $y, $yCenter - $x, $color);

    if($delta < 0.0){
      $delta = $delta + 4.0*$x + 6.0;
    }else{
      $delta = $delta + 4.0*($x - $y) + 10.0;
      $y = $y - 1.0;
    }
  }
}
function DrawCircleMidpointAlgorithm($canvas, $xCenter, $yCenter, $radius, $color){

  $d = floor((5.0 - $radius*4.0)/4.0);
  $x = 0.0;
  $y = $radius;

  for(; $x <= $y; $x = $x + 1.0){
    DrawPixel($canvas, $xCenter + $x, $yCenter + $y, $color);
    DrawPixel($canvas, $xCenter + $x, $yCenter - $y, $color);
    DrawPixel($canvas, $xCenter - $x, $yCenter + $y, $color);
    DrawPixel($canvas, $xCenter - $x, $yCenter - $y, $color);
    DrawPixel($canvas, $xCenter + $y, $yCenter + $x, $color);
    DrawPixel($canvas, $xCenter + $y, $yCenter - $x, $color);
    DrawPixel($canvas, $xCenter - $y, $yCenter + $x, $color);
    DrawPixel($canvas, $xCenter - $y, $yCenter - $x, $color);

    if($d < 0.0){
      $d = $d + 2.0*$x + 1.0;
    }else{
      $d = $d + 2.0*($x - $y) + 1.0;
      $y = $y - 1.0;
    }
  }
}
function DrawCircleBasicAlgorithm($canvas, $xCenter, $yCenter, $radius, $color){

  /* Place the circle in the center of the pixel. */
  $xCenter = floor($xCenter) + 0.5;
  $yCenter = floor($yCenter) + 0.5;

  $pixels = 2.0*M_PI*$radius;

  /* Below a radius of 10 pixels, over-compensate to get a smoother circle. */
  if($radius < 10.0){
    $pixels = $pixels*10.0;
  }

  $da = 2.0*M_PI/$pixels;

  for($a = 0.0; $a < 2.0*M_PI; $a = $a + $da){
    $dx = cos($a)*$radius;
    $dy = sin($a)*$radius;

    /* Floor to get the pixel coordinate. */
    DrawPixel($canvas, floor($xCenter + $dx), floor($yCenter + $dy), $color);
  }
}
function DrawFilledCircle($canvas, $x, $y, $r, $color){
  DrawFilledCircleBasicAlgorithm($canvas, $x, $y, $r, $color);
}
function DrawFilledCircleMidpointAlgorithm($canvas, $xCenter, $yCenter, $radius, $color){

  $d = floor((5.0 - $radius*4.0)/4.0);
  $x = 0.0;
  $y = $radius;

  for(; $x <= $y; $x = $x + 1.0){
    DrawLineBresenhamsAlgorithm($canvas, $xCenter + $x, $yCenter + $y, $xCenter - $x, $yCenter + $y, $color);
    DrawLineBresenhamsAlgorithm($canvas, $xCenter + $x, $yCenter - $y, $xCenter - $x, $yCenter - $y, $color);
    DrawLineBresenhamsAlgorithm($canvas, $xCenter + $y, $yCenter + $x, $xCenter - $y, $yCenter + $x, $color);
    DrawLineBresenhamsAlgorithm($canvas, $xCenter + $y, $yCenter - $x, $xCenter - $y, $yCenter - $x, $color);

    if($d < 0.0){
      $d = $d + 2.0*$x + 1.0;
    }else{
      $d = $d + 2.0*($x - $y) + 1.0;
      $y = $y - 1.0;
    }
  }
}
function DrawFilledCircleBasicAlgorithm($canvas, $xCenter, $yCenter, $radius, $color){

  /* Place the circle in the center of the pixel. */
  $xCenter = floor($xCenter) + 0.5;
  $yCenter = floor($yCenter) + 0.5;

  $pixels = 2.0*M_PI*$radius;

  /* Below a radius of 10 pixels, over-compensate to get a smoother circle. */
  if($radius < 10.0){
    $pixels = $pixels*10.0;
  }

  $da = 2.0*M_PI/$pixels;

  /* Draw lines for a half-circle to fill an entire circle. */
  for($a = 0.0; $a < M_PI; $a = $a + $da){
    $dx = cos($a)*$radius;
    $dy = sin($a)*$radius;

    /* Floor to get the pixel coordinate. */
    DrawVerticalLine1px($canvas, floor($xCenter - $dx), floor($yCenter - $dy), floor(2.0*$dy) + 1.0, $color);
  }
}
function DrawTriangle($canvas, $xCenter, $yCenter, $height, $color){

  $x1 = floor($xCenter + 0.5);
  $y1 = floor(floor($yCenter + 0.5) - $height);
  $x2 = $x1 - 2.0*$height*tan(M_PI/6.0);
  $y2 = floor($y1 + 2.0*$height);
  $x3 = $x1 + 2.0*$height*tan(M_PI/6.0);
  $y3 = floor($y1 + 2.0*$height);

  DrawLine1px($canvas, $x1, $y1, $x2, $y2, $color);
  DrawLine1px($canvas, $x1, $y1, $x3, $y3, $color);
  DrawLine1px($canvas, $x2, $y2, $x3, $y3, $color);
}
function DrawFilledTriangle($canvas, $xCenter, $yCenter, $height, $color){

  $x1 = floor($xCenter + 0.5);
  $y1 = floor(floor($yCenter + 0.5) - $height);

  for($i = 0.0; $i <= 2.0*$height; $i = $i + 1.0){
    $offset = floor($i*tan(M_PI/6.0));
    DrawHorizontalLine1px($canvas, $x1 - $offset, $y1 + $i, 2.0*$offset, $color);
  }
}
function DrawLine($canvas, $x1, $y1, $x2, $y2, $thickness, $color){
  DrawLineBresenhamsAlgorithmThick($canvas, $x1, $y1, $x2, $y2, $thickness, $color);
}
function DrawLineBresenhamsAlgorithmThick($canvas, $x1, $y1, $x2, $y2, $thickness, $color){

  $dx = $x2 - $x1;
  $dy = $y2 - $y1;

  $incX = Sign($dx);
  $incY = Sign($dy);

  $dx = abs($dx);
  $dy = abs($dy);

  if($dx > $dy){
    $pdx = $incX;
    $pdy = 0.0;
    $es = $dy;
    $el = $dx;
  }else{
    $pdx = 0.0;
    $pdy = $incY;
    $es = $dx;
    $el = $dy;
  }

  $x = $x1;
  $y = $y1;
  $err = $el/2.0;

  if($thickness >= 3.0){
    $r = $thickness/2.0;
    DrawCircle($canvas, $x, $y, $r, $color);
  }else if(floor($thickness) == 2.0){
    DrawFilledRectangle($canvas, $x, $y, 2.0, 2.0, $color);
  }else if(floor($thickness) == 1.0){
    DrawPixel($canvas, $x, $y, $color);
  }

  for($t = 0.0; $t < $el; $t = $t + 1.0){
    $err = $err - $es;
    if($err < 0.0){
      $err = $err + $el;
      $x = $x + $incX;
      $y = $y + $incY;
    }else{
      $x = $x + $pdx;
      $y = $y + $pdy;
    }

    if($thickness >= 3.0){
      $r = $thickness/2.0;
      DrawCircle($canvas, $x, $y, $r, $color);
    }else if(floor($thickness) == 2.0){
      DrawFilledRectangle($canvas, $x, $y, 2.0, 2.0, $color);
    }else if(floor($thickness) == 1.0){
      DrawPixel($canvas, $x, $y, $color);
    }
  }
}
function DrawLineBresenhamsAlgorithm($canvas, $x1, $y1, $x2, $y2, $color){

  $dx = $x2 - $x1;
  $dy = $y2 - $y1;

  $incX = Sign($dx);
  $incY = Sign($dy);

  $dx = abs($dx);
  $dy = abs($dy);

  if($dx > $dy){
    $pdx = $incX;
    $pdy = 0.0;
    $es = $dy;
    $el = $dx;
  }else{
    $pdx = 0.0;
    $pdy = $incY;
    $es = $dx;
    $el = $dy;
  }

  $x = $x1;
  $y = $y1;
  $err = $el/2.0;
  DrawPixel($canvas, $x, $y, $color);

  for($t = 0.0; $t < $el; $t = $t + 1.0){
    $err = $err - $es;
    if($err < 0.0){
      $err = $err + $el;
      $x = $x + $incX;
      $y = $y + $incY;
    }else{
      $x = $x + $pdx;
      $y = $y + $pdy;
    }

    DrawPixel($canvas, $x, $y, $color);
  }
}
function DrawLineBresenhamsAlgorithmThickPatterned($canvas, $x1, $y1, $x2, $y2, $thickness, &$pattern, $offset, $color){

  $dx = $x2 - $x1;
  $dy = $y2 - $y1;

  $incX = Sign($dx);
  $incY = Sign($dy);

  $dx = abs($dx);
  $dy = abs($dy);

  if($dx > $dy){
    $pdx = $incX;
    $pdy = 0.0;
    $es = $dy;
    $el = $dx;
  }else{
    $pdx = 0.0;
    $pdy = $incY;
    $es = $dx;
    $el = $dy;
  }

  $x = $x1;
  $y = $y1;
  $err = $el/2.0;

  $offset->numberValue = ($offset->numberValue + 1.0)%(count($pattern)*$thickness);

  if($pattern[floor($offset->numberValue/$thickness)]){
    if($thickness >= 3.0){
      $r = $thickness/2.0;
      DrawCircle($canvas, $x, $y, $r, $color);
    }else if(floor($thickness) == 2.0){
      DrawFilledRectangle($canvas, $x, $y, 2.0, 2.0, $color);
    }else if(floor($thickness) == 1.0){
      DrawPixel($canvas, $x, $y, $color);
    }
  }

  for($t = 0.0; $t < $el; $t = $t + 1.0){
    $err = $err - $es;
    if($err < 0.0){
      $err = $err + $el;
      $x = $x + $incX;
      $y = $y + $incY;
    }else{
      $x = $x + $pdx;
      $y = $y + $pdy;
    }

    $offset->numberValue = ($offset->numberValue + 1.0)%(count($pattern)*$thickness);

    if($pattern[floor($offset->numberValue/$thickness)]){
      if($thickness >= 3.0){
        $r = $thickness/2.0;
        DrawCircle($canvas, $x, $y, $r, $color);
      }else if(floor($thickness) == 2.0){
        DrawFilledRectangle($canvas, $x, $y, 2.0, 2.0, $color);
      }else if(floor($thickness) == 1.0){
        DrawPixel($canvas, $x, $y, $color);
      }
    }
  }
}
function &GetLinePattern5(){

  $pattern = array_fill(0, 19.0, 0);

  $pattern[0.0] = true;
  $pattern[1.0] = true;
  $pattern[2.0] = true;
  $pattern[3.0] = true;
  $pattern[4.0] = true;
  $pattern[5.0] = true;
  $pattern[6.0] = true;
  $pattern[7.0] = true;
  $pattern[8.0] = true;
  $pattern[9.0] = true;
  $pattern[10.0] = false;
  $pattern[11.0] = false;
  $pattern[12.0] = false;
  $pattern[13.0] = true;
  $pattern[14.0] = true;
  $pattern[15.0] = true;
  $pattern[16.0] = false;
  $pattern[17.0] = false;
  $pattern[18.0] = false;

  return $pattern;
}
function &GetLinePattern4(){

  $pattern = array_fill(0, 13.0, 0);

  $pattern[0.0] = true;
  $pattern[1.0] = true;
  $pattern[2.0] = true;
  $pattern[3.0] = true;
  $pattern[4.0] = true;
  $pattern[5.0] = true;
  $pattern[6.0] = true;
  $pattern[7.0] = true;
  $pattern[8.0] = true;
  $pattern[9.0] = true;
  $pattern[10.0] = false;
  $pattern[11.0] = false;
  $pattern[12.0] = false;

  return $pattern;
}
function &GetLinePattern3(){

  $pattern = array_fill(0, 13.0, 0);

  $pattern[0.0] = true;
  $pattern[1.0] = true;
  $pattern[2.0] = true;
  $pattern[3.0] = true;
  $pattern[4.0] = true;
  $pattern[5.0] = true;
  $pattern[6.0] = false;
  $pattern[7.0] = false;
  $pattern[8.0] = false;
  $pattern[9.0] = true;
  $pattern[10.0] = true;
  $pattern[11.0] = false;
  $pattern[12.0] = false;

  return $pattern;
}
function &GetLinePattern2(){

  $pattern = array_fill(0, 4.0, 0);

  $pattern[0.0] = true;
  $pattern[1.0] = true;
  $pattern[2.0] = false;
  $pattern[3.0] = false;

  return $pattern;
}
function &GetLinePattern1(){

  $pattern = array_fill(0, 8.0, 0);

  $pattern[0.0] = true;
  $pattern[1.0] = true;
  $pattern[2.0] = true;
  $pattern[3.0] = true;
  $pattern[4.0] = true;
  $pattern[5.0] = false;
  $pattern[6.0] = false;
  $pattern[7.0] = false;

  return $pattern;
}
function Blur($src, $pixels){

  $w = ImageWidth($src);
  $h = ImageHeight($src);
  $dst = CreateImage($w, $h, GetTransparent());

  for($x = 0.0; $x < $w; $x = $x + 1.0){
    for($y = 0.0; $y < $h; $y = $y + 1.0){
      SetPixel($dst, $x, $y, CreateBlurForPoint($src, $x, $y, $pixels));
    }
  }

  return $dst;
}
function CreateBlurForPoint($src, $x, $y, $pixels){

  $w = count($src->x);
  $h = count($src->x[0.0]->y);

  $rgba = new stdClass();
  $rgba->r = 0.0;
  $rgba->g = 0.0;
  $rgba->b = 0.0;
  $rgba->a = 0.0;

  $fromx = $x - $pixels;
  $fromx = max($fromx, 0.0);

  $tox = $x + $pixels;
  $tox = min($tox, $w - 1.0);

  $fromy = $y - $pixels;
  $fromy = max($fromy, 0.0);

  $toy = $y + $pixels;
  $toy = min($toy, $h - 1.0);

  $countColor = 0.0;
  $countTransparent = 0.0;
  for($i = $fromx; $i < $tox; $i = $i + 1.0){
    for($j = $fromy; $j < $toy; $j = $j + 1.0){
      $alpha = $src->x[$i]->y[$j]->a;
      if($alpha > 0.0){
        $rgba->r = $rgba->r + $src->x[$i]->y[$j]->r;
        $rgba->g = $rgba->g + $src->x[$i]->y[$j]->g;
        $rgba->b = $rgba->b + $src->x[$i]->y[$j]->b;
        $countColor = $countColor + 1.0;
      }
      $rgba->a = $rgba->a + $alpha;
      $countTransparent = $countTransparent + 1.0;
    }
  }

  if($countColor > 0.0){
    $rgba->r = $rgba->r/$countColor;
    $rgba->g = $rgba->g/$countColor;
    $rgba->b = $rgba->b/$countColor;
  }else{
    $rgba->r = 0.0;
    $rgba->g = 0.0;
    $rgba->b = 0.0;
  }

  if($countTransparent > 0.0){
    $rgba->a = $rgba->a/$countTransparent;
  }else{
    $rgba->a = 0.0;
  }

  return $rgba;
}
function &CreateStringScientificNotationDecimalFromNumber($decimal){

  $mantissaReference = new stdClass();
  $exponentReference = new stdClass();
  $result = array_fill(0, 0.0, 0);
  $done = false;
  $exponent = 0.0;

  if($decimal < 0.0){
    $isPositive = false;
    $decimal = -$decimal;
  }else{
    $isPositive = true;
  }

  if($decimal == 0.0){
    $done = true;
  }

  if( !$done ){
    $multiplier = 0.0;
    $inc = 0.0;

    if($decimal < 1.0){
      $multiplier = 10.0;
      $inc = -1.0;
    }else if($decimal >= 10.0){
      $multiplier = 0.1;
      $inc = 1.0;
    }else{
      $done = true;
    }

    if( !$done ){
      for(; $decimal >= 10.0 || $decimal < 1.0; ){
        $decimal = $decimal*$multiplier;
        $exponent = $exponent + $inc;
      }
    }
  }

  CreateStringFromNumberWithCheck($decimal, 10.0, $mantissaReference);

  CreateStringFromNumberWithCheck($exponent, 10.0, $exponentReference);

  if( !$isPositive ){
    $result = AppendString($result, $literal = str_split("-"));
  }

  $result = AppendString($result, $mantissaReference->string);
  $result = AppendString($result, $literal = str_split("e"));
  $result = AppendString($result, $exponentReference->string);

  return $result;
}
function &CreateStringDecimalFromNumber($decimal){

  $stringReference = new stdClass();

  /* This will succeed because base = 10. */
  CreateStringFromNumberWithCheck($decimal, 10.0, $stringReference);

  return $stringReference->string;
}
function CreateStringFromNumberWithCheck($decimal, $base, $stringReference){

  $isPositive = true;

  if($decimal < 0.0){
    $isPositive = false;
    $decimal = -$decimal;
  }

  if($decimal == 0.0){
    $stringReference->string = str_split("0");
    $success = true;
  }else{
    $characterReference = new stdClass();

    if(IsInteger($base)){
      $success = true;

      $string = array_fill(0, 0.0, 0);

      $maximumDigits = GetMaximumDigitsForBase($base);

      $digitPosition = GetFirstDigitPosition($decimal, $base);

      $decimal = round($decimal*$base**($maximumDigits - $digitPosition - 1.0));

      $hasPrintedPoint = false;

      if( !$isPositive ){
        $string = AppendCharacter($string, "-");
      }

      /* Print leading zeros. */
      if($digitPosition < 0.0){
        $string = AppendCharacter($string, "0");
        $string = AppendCharacter($string, ".");
        $hasPrintedPoint = true;
        for($i = 0.0; $i < -$digitPosition - 1.0; $i = $i + 1.0){
          $string = AppendCharacter($string, "0");
        }
      }

      /* Print number. */
      for($i = 0.0; $i < $maximumDigits && $success; $i = $i + 1.0){
        $d = floor($decimal/$base**($maximumDigits - $i - 1.0));

        if($d >= $base){
          $d = $base - 1.0;
        }

        if( !$hasPrintedPoint  && $digitPosition - $i + 1.0 == 0.0){
          if($decimal != 0.0){
            $string = AppendCharacter($string, ".");
          }
          $hasPrintedPoint = true;
        }

        if($decimal == 0.0 && $hasPrintedPoint){
        }else{
          $success = GetSingleDigitCharacterFromNumberWithCheck($d, $base, $characterReference);
          if($success){
            $c = $characterReference->characterValue;
            $string = AppendCharacter($string, $c);
          }
        }

        if($success){
          $decimal = $decimal - $d*$base**($maximumDigits - $i - 1.0);
        }
      }

      if($success){
        /* Print trailing zeros. */
        for($i = 0.0; $i < $digitPosition - $maximumDigits + 1.0; $i = $i + 1.0){
          $string = AppendCharacter($string, "0");
        }

        $stringReference->string = $string;
      }
    }else{
      $success = false;
    }
  }

  /* Done */
  return $success;
}
function GetMaximumDigitsForBase($base){

  $t = 10.0**15.0;
  return floor(log10($t)/log10($base));
}
function GetFirstDigitPosition($decimal, $base){

  $power = ceil(log10($decimal)/log10($base));

  $t = $decimal*$base**(-$power);
  if($t < $base && $t >= 1.0){
  }else if($t >= $base){
    $power = $power + 1.0;
  }else if($t < 1.0){
    $power = $power - 1.0;
  }

  return $power;
}
function GetSingleDigitCharacterFromNumberWithCheck($c, $base, $characterReference){

  $numberTable = GetDigitCharacterTable();

  if($c < $base || $c < count($numberTable)){
    $success = true;
    $characterReference->characterValue = $numberTable[$c];
  }else{
    $success = false;
  }

  return $success;
}
function &GetDigitCharacterTable(){

  $numberTable = str_split("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");

  return $numberTable;
}
function CreateNumberFromDecimalStringWithCheck(&$string, $decimalReference, $errorMessage){
  return CreateNumberFromStringWithCheck($string, 10.0, $decimalReference, $errorMessage);
}
function CreateNumberFromDecimalString(&$string){

  $doubleReference = CreateNumberReference(0.0);
  $stringReference = CreateStringReference($literal = array());
  CreateNumberFromStringWithCheck($string, 10.0, $doubleReference, $stringReference);
  $number = $doubleReference->numberValue;

  unset($doubleReference);
  unset($stringReference);

  return $number;
}
function CreateNumberFromStringWithCheck(&$string, $base, $numberReference, $errorMessage){

  $numberIsPositive = CreateBooleanReference(true);
  $exponentIsPositive = CreateBooleanReference(true);
  $beforePoint = new stdClass();
  $afterPoint = new stdClass();
  $exponent = new stdClass();

  if($base >= 2.0 && $base <= 36.0){
    $success = ExtractPartsFromNumberString($string, $base, $numberIsPositive, $beforePoint, $afterPoint, $exponentIsPositive, $exponent, $errorMessage);

    if($success){
      $numberReference->numberValue = CreateNumberFromParts($base, $numberIsPositive->booleanValue, $beforePoint->numberArray, $afterPoint->numberArray, $exponentIsPositive->booleanValue, $exponent->numberArray);
    }
  }else{
    $success = false;
    $errorMessage->string = str_split("Base must be from 2 to 36.");
  }

  return $success;
}
function CreateNumberFromParts($base, $numberIsPositive, &$beforePoint, &$afterPoint, $exponentIsPositive, &$exponent){

  $n = 0.0;

  for($i = 0.0; $i < count($beforePoint); $i = $i + 1.0){
    $p = $beforePoint[count($beforePoint) - $i - 1.0];

    $n = $n + $p*$base**$i;
  }

  for($i = 0.0; $i < count($afterPoint); $i = $i + 1.0){
    $p = $afterPoint[$i];

    $n = $n + $p*$base**(-($i + 1.0));
  }

  if(count($exponent) > 0.0){
    $e = 0.0;
    for($i = 0.0; $i < count($exponent); $i = $i + 1.0){
      $p = $exponent[count($exponent) - $i - 1.0];

      $e = $e + $p*$base**$i;
    }

    if( !$exponentIsPositive ){
      $e = -$e;
    }

    $n = $n*$base**$e;
  }

  if( !$numberIsPositive ){
    $n = -$n;
  }

  return $n;
}
function ExtractPartsFromNumberString(&$n, $base, $numberIsPositive, $beforePoint, $afterPoint, $exponentIsPositive, $exponent, $errorMessages){

  $i = 0.0;

  if($i < count($n)){
    if($n[$i] == "-"){
      $numberIsPositive->booleanValue = false;
      $i = $i + 1.0;
    }else if($n[$i] == "+"){
      $numberIsPositive->booleanValue = true;
      $i = $i + 1.0;
    }

    $success = ExtractPartsFromNumberStringFromSign($n, $base, $i, $beforePoint, $afterPoint, $exponentIsPositive, $exponent, $errorMessages);
  }else{
    $success = false;
    $errorMessages->string = str_split("Number cannot have length zero.");
  }

  return $success;
}
function ExtractPartsFromNumberStringFromSign(&$n, $base, $i, $beforePoint, $afterPoint, $exponentIsPositive, $exponent, $errorMessages){

  $done = false;
  $count = 0.0;
  for(; $i + $count < count($n) &&  !$done ; ){
    if(CharacterIsNumberCharacterInBase($n[$i + $count], $base)){
      $count = $count + 1.0;
    }else{
      $done = true;
    }
  }

  if($count >= 1.0){
    $beforePoint->numberArray = array_fill(0, $count, 0);

    for($j = 0.0; $j < $count; $j = $j + 1.0){
      $beforePoint->numberArray[$j] = GetNumberFromNumberCharacterForBase($n[$i + $j], $base);
    }

    $i = $i + $count;

    if($i < count($n)){
      $success = ExtractPartsFromNumberStringFromPointOrExponent($n, $base, $i, $afterPoint, $exponentIsPositive, $exponent, $errorMessages);
    }else{
      $afterPoint->numberArray = array_fill(0, 0.0, 0);
      $exponent->numberArray = array_fill(0, 0.0, 0);
      $success = true;
    }
  }else{
    $success = false;
    $errorMessages->string = str_split("Number must have at least one number after the optional sign.");
  }

  return $success;
}
function ExtractPartsFromNumberStringFromPointOrExponent(&$n, $base, $i, $afterPoint, $exponentIsPositive, $exponent, $errorMessages){

  if($n[$i] == "."){
    $i = $i + 1.0;

    if($i < count($n)){
      $done = false;
      $count = 0.0;
      for(; $i + $count < count($n) &&  !$done ; ){
        if(CharacterIsNumberCharacterInBase($n[$i + $count], $base)){
          $count = $count + 1.0;
        }else{
          $done = true;
        }
      }

      if($count >= 1.0){
        $afterPoint->numberArray = array_fill(0, $count, 0);

        for($j = 0.0; $j < $count; $j = $j + 1.0){
          $afterPoint->numberArray[$j] = GetNumberFromNumberCharacterForBase($n[$i + $j], $base);
        }

        $i = $i + $count;

        if($i < count($n)){
          $success = ExtractPartsFromNumberStringFromExponent($n, $base, $i, $exponentIsPositive, $exponent, $errorMessages);
        }else{
          $exponent->numberArray = array_fill(0, 0.0, 0);
          $success = true;
        }
      }else{
        $success = false;
        $errorMessages->string = str_split("There must be at least one digit after the decimal point.");
      }
    }else{
      $success = false;
      $errorMessages->string = str_split("There must be at least one digit after the decimal point.");
    }
  }else if($base <= 14.0 && ($n[$i] == "e" || $n[$i] == "E")){
    if($i < count($n)){
      $success = ExtractPartsFromNumberStringFromExponent($n, $base, $i, $exponentIsPositive, $exponent, $errorMessages);
      $afterPoint->numberArray = array_fill(0, 0.0, 0);
    }else{
      $success = false;
      $errorMessages->string = str_split("There must be at least one digit after the exponent.");
    }
  }else{
    $success = false;
    $errorMessages->string = str_split("Expected decimal point or exponent symbol.");
  }

  return $success;
}
function ExtractPartsFromNumberStringFromExponent(&$n, $base, $i, $exponentIsPositive, $exponent, $errorMessages){

  if($base <= 14.0 && ($n[$i] == "e" || $n[$i] == "E")){
    $i = $i + 1.0;

    if($i < count($n)){
      if($n[$i] == "-"){
        $exponentIsPositive->booleanValue = false;
        $i = $i + 1.0;
      }else if($n[$i] == "+"){
        $exponentIsPositive->booleanValue = true;
        $i = $i + 1.0;
      }

      if($i < count($n)){
        $done = false;
        $count = 0.0;
        for(; $i + $count < count($n) &&  !$done ; ){
          if(CharacterIsNumberCharacterInBase($n[$i + $count], $base)){
            $count = $count + 1.0;
          }else{
            $done = true;
          }
        }

        if($count >= 1.0){
          $exponent->numberArray = array_fill(0, $count, 0);

          for($j = 0.0; $j < $count; $j = $j + 1.0){
            $exponent->numberArray[$j] = GetNumberFromNumberCharacterForBase($n[$i + $j], $base);
          }

          $i = $i + $count;

          if($i == count($n)){
            $success = true;
          }else{
            $success = false;
            $errorMessages->string = str_split("There cannot be any characters past the exponent of the number.");
          }
        }else{
          $success = false;
          $errorMessages->string = str_split("There must be at least one digit after the decimal point.");
        }
      }else{
        $success = false;
        $errorMessages->string = str_split("There must be at least one digit after the exponent symbol.");
      }
    }else{
      $success = false;
      $errorMessages->string = str_split("There must be at least one digit after the exponent symbol.");
    }
  }else{
    $success = false;
    $errorMessages->string = str_split("Expected exponent symbol.");
  }

  return $success;
}
function GetNumberFromNumberCharacterForBase($c, $base){

  $numberTable = GetDigitCharacterTable();
  $position = 0.0;

  for($i = 0.0; $i < $base; $i = $i + 1.0){
    if($numberTable[$i] == $c){
      $position = $i;
    }
  }

  return $position;
}
function CharacterIsNumberCharacterInBase($c, $base){

  $numberTable = GetDigitCharacterTable();
  $found = false;

  for($i = 0.0; $i < $base; $i = $i + 1.0){
    if($numberTable[$i] == $c){
      $found = true;
    }
  }

  return $found;
}
function &StringToNumberArray(&$str){

  $numberArrayReference = new stdClass();
  $stringReference = new stdClass();

  StringToNumberArrayWithCheck($str, $numberArrayReference, $stringReference);

  $numbers = $numberArrayReference->numberArray;

  unset($numberArrayReference);
  unset($stringReference);

  return $numbers;
}
function StringToNumberArrayWithCheck(&$str, $numberArrayReference, $errorMessage){

  $numberStrings = SplitByString($str, $literal = str_split(","));

  $numbers = array_fill(0, count($numberStrings), 0);
  $success = true;
  $numberReference = new stdClass();

  for($i = 0.0; $i < count($numberStrings); $i = $i + 1.0){
    $numberString = $numberStrings[$i]->string;
    $trimmedNumberString = Trimx($numberString);
    $success = CreateNumberFromDecimalStringWithCheck($trimmedNumberString, $numberReference, $errorMessage);
    $numbers[$i] = $numberReference->numberValue;

    FreeStringReference($numberStrings[$i]);
    unset($trimmedNumberString);
  }

  unset($numberStrings);
  unset($numberReference);

  $numberArrayReference->numberArray = $numbers;

  return $success;
}
function Negate($x){
  return -$x;
}
function Positive($x){
  return +$x;
}
function Factorial($x){

  $f = 1.0;

  for($i = 2.0; $i <= $x; $i = $i + 1.0){
    $f = $f*$i;
  }

  return $f;
}
function Roundx($x){
  return floor($x + 0.5);
}
function BankersRound($x){

  if(Absolute($x - Truncate($x)) == 0.5){
    if( !DivisibleBy(Roundx($x), 2.0) ){
      $r = Roundx($x) - 1.0;
    }else{
      $r = Roundx($x);
    }
  }else{
    $r = Roundx($x);
  }

  return $r;
}
function Ceilx($x){
  return ceil($x);
}
function Floorx($x){
  return floor($x);
}
function Truncate($x){

  if($x >= 0.0){
    $t = floor($x);
  }else{
    $t = ceil($x);
  }

  return $t;
}
function Absolute($x){
  return abs($x);
}
function Logarithm($x){
  return log10($x);
}
function NaturalLogarithm($x){
  return log($x);
}
function Sinx($x){
  return sin($x);
}
function Cosx($x){
  return cos($x);
}
function Tanx($x){
  return tan($x);
}
function Asinx($x){
  return asin($x);
}
function Acosx($x){
  return acos($x);
}
function Atanx($x){
  return atan($x);
}
function Atan2x($y, $x){

  /* Atan2 is an invalid operation when x = 0 and y = 0, but this method does not return errors. */
  $a = 0.0;

  if($x > 0.0){
    $a = Atanx($y/$x);
  }else if($x < 0.0 && $y >= 0.0){
    $a = Atanx($y/$x) + M_PI;
  }else if($x < 0.0 && $y < 0.0){
    $a = Atanx($y/$x) - M_PI;
  }else if($x == 0.0 && $y > 0.0){
    $a = M_PI/2.0;
  }else if($x == 0.0 && $y < 0.0){
    $a = -M_PI/2.0;
  }

  return $a;
}
function Squareroot($x){
  return sqrt($x);
}
function Expx($x){
  return exp($x);
}
function DivisibleBy($a, $b){
  return (($a%$b) == 0.0);
}
function Combinations($n, $k){

  $c = 1.0;
  $j = 1.0;
  $i = $n - $k + 1.0;

  for(; $i <= $n; ){
    $c = $c*$i;
    $c = $c/$j;

    $i = $i + 1.0;
    $j = $j + 1.0;
  }

  return $c;
}
function Permutations($n, $k){

  $c = 1.0;

  for($i = $n - $k + 1.0; $i <= $n; $i = $i + 1.0){
    $c = $c*$i;
  }

  return $c;
}
function EpsilonCompare($a, $b, $epsilon){
  return abs($a - $b) < $epsilon;
}
function GreatestCommonDivisor($a, $b){

  for(; $b != 0.0; ){
    $t = $b;
    $b = $a%$b;
    $a = $t;
  }

  return $a;
}
function GCDWithSubtraction($a, $b){

  if($a == 0.0){
    $g = $b;
  }else{
    for(; $b != 0.0; ){
      if($a > $b){
        $a = $a - $b;
      }else{
        $b = $b - $a;
      }
    }

    $g = $a;
  }

  return $g;
}
function IsInteger($a){
  return ($a - floor($a)) == 0.0;
}
function GreatestCommonDivisorWithCheck($a, $b, $gcdReference){

  if(IsInteger($a) && IsInteger($b)){
    $gcd = GreatestCommonDivisor($a, $b);
    $gcdReference->numberValue = $gcd;
    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function LeastCommonMultiple($a, $b){

  if($a > 0.0 && $b > 0.0){
    $lcm = abs($a*$b)/GreatestCommonDivisor($a, $b);
  }else{
    $lcm = 0.0;
  }

  return $lcm;
}
function Sign($a){

  if($a > 0.0){
    $s = 1.0;
  }else if($a < 0.0){
    $s = -1.0;
  }else{
    $s = 0.0;
  }

  return $s;
}
function Maxx($a, $b){
  return max($a, $b);
}
function Minx($a, $b){
  return min($a, $b);
}
function Power($a, $b){
  return $a**$b;
}
function Gamma($x){
  return LanczosApproximation($x);
}
function LogGamma($x){
  return log(Gamma($x));
}
function LanczosApproximation($z){

  $p = array_fill(0, 8.0, 0);
  $p[0.0] = 676.5203681218851;
  $p[1.0] = -1259.1392167224028;
  $p[2.0] = 771.32342877765313;
  $p[3.0] = -176.61502916214059;
  $p[4.0] = 12.507343278686905;
  $p[5.0] = -0.13857109526572012;
  $p[6.0] = 9.9843695780195716e-6;
  $p[7.0] = 1.5056327351493116e-7;

  if($z < 0.5){
    $y = M_PI/(sin(M_PI*$z)*LanczosApproximation(1.0 - $z));
  }else{
    $z = $z - 1.0;
    $x = 0.99999999999980993;
    for($i = 0.0; $i < count($p); $i = $i + 1.0){
      $x = $x + $p[$i]/($z + $i + 1.0);
    }
    $t = $z + count($p) - 0.5;
    $y = sqrt(2.0*M_PI)*$t**($z + 0.5)*exp(-$t)*$x;
  }

  return $y;
}
function Beta($x, $y){
  return Gamma($x)*Gamma($y)/Gamma($x + $y);
}
function Sinhx($x){
  return (exp($x) - exp(-$x))/2.0;
}
function Coshx($x){
  return (exp($x) + exp(-$x))/2.0;
}
function Tanhx($x){
  return Sinhx($x)/Coshx($x);
}
function Cot($x){
  return 1.0/tan($x);
}
function Sec($x){
  return 1.0/cos($x);
}
function Csc($x){
  return 1.0/sin($x);
}
function Coth($x){
  return Coshx($x)/Sinhx($x);
}
function Sech($x){
  return 1.0/Coshx($x);
}
function Csch($x){
  return 1.0/Sinhx($x);
}
function Error($x){

  if($x == 0.0){
    $y = 0.0;
  }else if($x < 0.0){
    $y = -Error(-$x);
  }else{
    $c1 = -1.26551223;
    $c2 = +1.00002368;
    $c3 = +0.37409196;
    $c4 = +0.09678418;
    $c5 = -0.18628806;
    $c6 = +0.27886807;
    $c7 = -1.13520398;
    $c8 = +1.48851587;
    $c9 = -0.82215223;
    $c10 = +0.17087277;

    $t = 1.0/(1.0 + 0.5*abs($x));

    $tau = $t*exp(-$x**2.0 + $c1 + $t*($c2 + $t*($c3 + $t*($c4 + $t*($c5 + $t*($c6 + $t*($c7 + $t*($c8 + $t*($c9 + $t*$c10)))))))));

    $y = 1.0 - $tau;
  }

  return $y;
}
function ErrorInverse($x){

  $a = (8.0*(M_PI - 3.0))/(3.0*M_PI*(4.0 - M_PI));

  $t = 2.0/(M_PI*$a) + log(1.0 - $x**2.0)/2.0;
  $y = Sign($x)*sqrt(sqrt($t**2.0 - log(1.0 - $x**2.0)/$a) - $t);

  return $y;
}
function FallingFactorial($x, $n){

  $y = 1.0;

  for($k = 0.0; $k <= $n - 1.0; $k = $k + 1.0){
    $y = $y*($x - $k);
  }

  return $y;
}
function RisingFactorial($x, $n){

  $y = 1.0;

  for($k = 0.0; $k <= $n - 1.0; $k = $k + 1.0){
    $y = $y*($x + $k);
  }

  return $y;
}
function Hypergeometric($a, $b, $c, $z, $maxIterations, $precision){

  if(abs($z) >= 0.5){
    $y = (1.0 - $z)**(-$a)*HypergeometricDirect($a, $c - $b, $c, $z/($z - 1.0), $maxIterations, $precision);
  }else{
    $y = HypergeometricDirect($a, $b, $c, $z, $maxIterations, $precision);
  }

  return $y;
}
function HypergeometricDirect($a, $b, $c, $z, $maxIterations, $precision){

  $y = 0.0;
  $done = false;

  for($n = 0.0; $n < $maxIterations &&  !$done ; $n = $n + 1.0){
    $yp = RisingFactorial($a, $n)*RisingFactorial($b, $n)/RisingFactorial($c, $n)*$z**$n/Factorial($n);
    if(abs($yp) < $precision){
      $done = true;
    }
    $y = $y + $yp;
  }

  return $y;
}
function BernouilliNumber($n){
  return AkiyamaTanigawaAlgorithm($n);
}
function AkiyamaTanigawaAlgorithm($n){

  $A = array_fill(0, $n + 1.0, 0);

  for($m = 0.0; $m <= $n; $m = $m + 1.0){
    $A[$m] = 1.0/($m + 1.0);
    for($j = $m; $j >= 1.0; $j = $j - 1.0){
      $A[$j - 1.0] = $j*($A[$j - 1.0] - $A[$j]);
    }
  }

  $B = $A[0.0];

  unset($A);

  return $B;
}
function &aStringToNumberArray(&$string){

  $array = array_fill(0, count($string), 0);

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    $array[$i] = uniord($string[$i]);
  }
  return $array;
}
function &aNumberArrayToString(&$array){

  $string = array_fill(0, count($array), 0);

  for($i = 0.0; $i < count($array); $i = $i + 1.0){
    $string[$i] = unichr($array[$i]);
  }
  return $string;
}
function aNumberArraysEqual(&$a, &$b){

  $equal = true;
  if(count($a) == count($b)){
    for($i = 0.0; $i < count($a) && $equal; $i = $i + 1.0){
      if($a[$i] != $b[$i]){
        $equal = false;
      }
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function aBooleanArraysEqual(&$a, &$b){

  $equal = true;
  if(count($a) == count($b)){
    for($i = 0.0; $i < count($a) && $equal; $i = $i + 1.0){
      if($a[$i] != $b[$i]){
        $equal = false;
      }
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function aStringsEqual(&$a, &$b){

  $equal = true;
  if(count($a) == count($b)){
    for($i = 0.0; $i < count($a) && $equal; $i = $i + 1.0){
      if($a[$i] != $b[$i]){
        $equal = false;
      }
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function aFillNumberArray(&$a, $value){

  for($i = 0.0; $i < count($a); $i = $i + 1.0){
    $a[$i] = $value;
  }
}
function aFillString(&$a, $value){

  for($i = 0.0; $i < count($a); $i = $i + 1.0){
    $a[$i] = $value;
  }
}
function aFillBooleanArray(&$a, $value){

  for($i = 0.0; $i < count($a); $i = $i + 1.0){
    $a[$i] = $value;
  }
}
function aFillNumberArrayRange(&$a, $value, $from, $to){

  if($from >= 0.0 && $from <= count($a) && $to >= 0.0 && $to <= count($a) && $from <= $to){
    $length = $to - $from;
    for($i = 0.0; $i < $length; $i = $i + 1.0){
      $a[$from + $i] = $value;
    }

    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function aFillBooleanArrayRange(&$a, $value, $from, $to){

  if($from >= 0.0 && $from <= count($a) && $to >= 0.0 && $to <= count($a) && $from <= $to){
    $length = $to - $from;
    for($i = 0.0; $i < $length; $i = $i + 1.0){
      $a[$from + $i] = $value;
    }

    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function aFillStringRange(&$a, $value, $from, $to){

  if($from >= 0.0 && $from <= count($a) && $to >= 0.0 && $to <= count($a) && $from <= $to){
    $length = $to - $from;
    for($i = 0.0; $i < $length; $i = $i + 1.0){
      $a[$from + $i] = $value;
    }

    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function &aCopyNumberArray(&$a){

  $n = array_fill(0, count($a), 0);

  for($i = 0.0; $i < count($a); $i = $i + 1.0){
    $n[$i] = $a[$i];
  }

  return $n;
}
function &aCopyBooleanArray(&$a){

  $n = array_fill(0, count($a), 0);

  for($i = 0.0; $i < count($a); $i = $i + 1.0){
    $n[$i] = $a[$i];
  }

  return $n;
}
function &aCopyString(&$a){

  $n = array_fill(0, count($a), 0);

  for($i = 0.0; $i < count($a); $i = $i + 1.0){
    $n[$i] = $a[$i];
  }

  return $n;
}
function aCopyNumberArrayRange(&$a, $from, $to, $copyReference){

  if($from >= 0.0 && $from <= count($a) && $to >= 0.0 && $to <= count($a) && $from <= $to){
    $length = $to - $from;
    $n = array_fill(0, $length, 0);

    for($i = 0.0; $i < $length; $i = $i + 1.0){
      $n[$i] = $a[$from + $i];
    }

    $copyReference->numberArray = $n;
    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function aCopyBooleanArrayRange(&$a, $from, $to, $copyReference){

  if($from >= 0.0 && $from <= count($a) && $to >= 0.0 && $to <= count($a) && $from <= $to){
    $length = $to - $from;
    $n = array_fill(0, $length, 0);

    for($i = 0.0; $i < $length; $i = $i + 1.0){
      $n[$i] = $a[$from + $i];
    }

    $copyReference->booleanArray = $n;
    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function aCopyStringRange(&$a, $from, $to, $copyReference){

  if($from >= 0.0 && $from <= count($a) && $to >= 0.0 && $to <= count($a) && $from <= $to){
    $length = $to - $from;
    $n = array_fill(0, $length, 0);

    for($i = 0.0; $i < $length; $i = $i + 1.0){
      $n[$i] = $a[$from + $i];
    }

    $copyReference->string = $n;
    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function aIsLastElement($length, $index){
  return $index + 1.0 == $length;
}
function &aCreateNumberArray($length, $value){

  $array = array_fill(0, $length, 0);
  aFillNumberArray($array, $value);

  return $array;
}
function &aCreateBooleanArray($length, $value){

  $array = array_fill(0, $length, 0);
  aFillBooleanArray($array, $value);

  return $array;
}
function &aCreateString($length, $value){

  $array = array_fill(0, $length, 0);
  aFillString($array, $value);

  return $array;
}
function aSwapElementsOfNumberArray(&$A, $ai, $bi){

  $tmp = $A[$ai];
  $A[$ai] = $A[$bi];
  $A[$bi] = $tmp;
}
function aSwapElementsOfStringArray($A, $ai, $bi){

  $tmp = $A->stringArray[$ai];
  $A->stringArray[$ai] = $A->stringArray[$bi];
  $A->stringArray[$bi] = $tmp;
}
function aReverseNumberArray(&$array){

  for($i = 0.0; $i < count($array)/2.0; $i = $i + 1.0){
    aSwapElementsOfNumberArray($array, $i, count($array) - $i - 1.0);
  }
}
function CreateBooleanReference($value){

  $ref = new stdClass();
  $ref->booleanValue = $value;

  return $ref;
}
function CreateBooleanArrayReference(&$value){

  $ref = new stdClass();
  $ref->booleanArray = $value;

  return $ref;
}
function CreateBooleanArrayReferenceLengthValue($length, $value){

  $ref = new stdClass();
  $ref->booleanArray = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $ref->booleanArray[$i] = $value;
  }

  return $ref;
}
function FreeBooleanArrayReference($booleanArrayReference){
  unset($booleanArrayReference->booleanArray);
  unset($booleanArrayReference);
}
function CreateCharacterReference($value){

  $ref = new stdClass();
  $ref->characterValue = $value;

  return $ref;
}
function CreateNumberReference($value){

  $ref = new stdClass();
  $ref->numberValue = $value;

  return $ref;
}
function CreateNumberArrayReference(&$value){

  $ref = new stdClass();
  $ref->numberArray = $value;

  return $ref;
}
function CreateNumberArrayReferenceLengthValue($length, $value){

  $ref = new stdClass();
  $ref->numberArray = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $ref->numberArray[$i] = $value;
  }

  return $ref;
}
function FreeNumberArrayReference($numberArrayReference){
  unset($numberArrayReference->numberArray);
  unset($numberArrayReference);
}
function CreateStringReference(&$value){

  $ref = new stdClass();
  $ref->string = $value;

  return $ref;
}
function CreateStringReferenceLengthValue($length, $value){

  $ref = new stdClass();
  $ref->string = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $ref->string[$i] = $value;
  }

  return $ref;
}
function FreeStringReference($stringReference){
  unset($stringReference->string);
  unset($stringReference);
}
function CreateStringArrayReference(&$strings){

  $ref = new stdClass();
  $ref->stringArray = $strings;

  return $ref;
}
function CreateStringArrayReferenceLengthValue($length, &$value){

  $ref = new stdClass();
  $ref->stringArray = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $ref->stringArray[$i] = CreateStringReference($value);
  }

  return $ref;
}
function FreeStringArrayReference($stringArrayReference){

  for($i = 0.0; $i < count($stringArrayReference->stringArray); $i = $i + 1.0){
    unset($stringArrayReference->stringArray[$i]);
  }
  unset($stringArrayReference->stringArray);
  unset($stringArrayReference);
}
function &GetPixelFontData(){
  return str_split("0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000");
}
function DrawAsciiCharacter($image, $topx, $topy, $a, $color){

  $rowReference = new stdClass();
  $errorMessage = new stdClass();

  $index = uniord($a);
  $index = $index - 32.0;
  $allCharData = GetPixelFontData();
  $charData = Substring($allCharData, $index*2.0*13.0, ($index + 1.0)*2.0*13.0);

  for($y = 0.0; $y < 13.0; $y = $y + 1.0){
    $rowData = Substring($charData, $y*2.0, ($y + 1.0)*2.0);
    ToUpperCase($rowData);
    CreateNumberFromStringWithCheck($rowData, 16.0, $rowReference, $errorMessage);
    $row = $rowReference->numberValue;
    for($x = 0.0; $x < 8.0; $x = $x + 1.0){
      $pixel = floor($row/2.0**$x)%2.0;
      if($pixel == 1.0){
        DrawPixel($image, $topx + 8.0 - 1.0 - $x, $topy + 13.0 - 1.0 - $y, $color);
      }
    }
  }
}
function GetTextWidth(&$text){

  $charWidth = 8.0;
  $spacing = 2.0;

  if(count($text) == 0.0){
    $width = 0.0;
  }else{
    $width = count($text)*$charWidth + (count($text) - 1.0)*$spacing;
  }

  return $width;
}
function GetTextHeight(&$text){
  return 13.0;
}
function AssertFalse($b, $failures){
  if($b){
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertTrue($b, $failures){
  if( !$b ){
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertEquals($a, $b, $failures){
  if($a != $b){
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertBooleansEqual($a, $b, $failures){
  if($a != $b){
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertCharactersEqual($a, $b, $failures){
  if($a != $b){
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertStringEquals(&$a, &$b, $failures){
  if( !aStringsEqual($a, $b) ){
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertNumberArraysEqual(&$a, &$b, $failures){

  if(count($a) == count($b)){
    for($i = 0.0; $i < count($a); $i = $i + 1.0){
      AssertEquals($a[$i], $b[$i], $failures);
    }
  }else{
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertBooleanArraysEqual(&$a, &$b, $failures){

  if(count($a) == count($b)){
    for($i = 0.0; $i < count($a); $i = $i + 1.0){
      AssertBooleansEqual($a[$i], $b[$i], $failures);
    }
  }else{
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function AssertStringArraysEqual(&$a, &$b, $failures){

  if(count($a) == count($b)){
    for($i = 0.0; $i < count($a); $i = $i + 1.0){
      AssertStringEquals($a[$i]->string, $b[$i]->string, $failures);
    }
  }else{
    $failures->numberValue = $failures->numberValue + 1.0;
  }
}
function &ConvertToPNG($image){
  return ConvertToPNGWithOptions($image, 6.0, false, 0.0, 0.001);
}
function &ConvertToPNGGrayscale($image){
  return ConvertToPNGWithOptions($image, 0.0, false, 0.0, 0.001);
}
function PysicsHeader($pixelsPerMeter){

  $phys = new stdClass();

  $phys->pixelsPerMeter = $pixelsPerMeter;

  return $phys;
}
function &ConvertToPNGWithOptions($image, $colorType, $setPhys, $pixelsPerMeter, $compressionLevel){

  $png = new stdClass();

  $png->signature = PNGSignature();

  $png->ihdr = PNGHeader($image, $colorType);

  $png->physPresent = $setPhys;
  $png->phys = PysicsHeader($pixelsPerMeter);

  if($colorType == 6.0){
    $colorData = GetPNGColorData($image);
  }else{
    $colorData = GetPNGColorDataGreyscale($image);
  }
  $png->zlibStruct = ZLibCompressStaticHuffman($colorData, $compressionLevel);

  $pngData = PNGSerializeChunks($png);

  return $pngData;
}
function &PNGSerializeChunks($png){

  $length = count($png->signature) + 12.0 + PNGHeaderLength() + 12.0 + PNGIDATLength($png) + 12.0;
  if($png->physPresent){
    $length = $length + 4.0 + 4.0 + 1.0 + 12.0;
  }
  $data = array_fill(0, $length, 0);
  $position = CreateNumberReference(0.0);

  /* Signature */
  for($i = 0.0; $i < count($png->signature); $i = $i + 1.0){
    WriteByte($data, $png->signature[$i], $position);
  }

  /* Header */
  $chunkLength = PNGHeaderLength();
  Write4BytesBE($data, $chunkLength, $position);
  WriteStringBytes($data, $literal = str_split("IHDR"), $position);
  Write4BytesBE($data, $png->ihdr->Width, $position);
  Write4BytesBE($data, $png->ihdr->Height, $position);
  WriteByte($data, $png->ihdr->BitDepth, $position);
  WriteByte($data, $png->ihdr->ColourType, $position);
  WriteByte($data, $png->ihdr->CompressionMethod, $position);
  WriteByte($data, $png->ihdr->FilterMethod, $position);
  WriteByte($data, $png->ihdr->InterlaceMethod, $position);
  Write4BytesBE($data, CRC32OfInterval($data, $position->numberValue - $chunkLength - 4.0, $chunkLength + 4.0), $position);

  /* pHYs */
  if($png->physPresent){
    $chunkLength = 4.0 + 4.0 + 1.0;
    Write4BytesBE($data, $chunkLength, $position);
    WriteStringBytes($data, $literal = str_split("pHYs"), $position);

    Write4BytesBE($data, $png->phys->pixelsPerMeter, $position);
    Write4BytesBE($data, $png->phys->pixelsPerMeter, $position);
    WriteByte($data, 1.0, $position);
    /* 1 = pixels per meter */
    Write4BytesBE($data, CRC32OfInterval($data, $position->numberValue - $chunkLength - 4.0, $chunkLength + 4.0), $position);
  }

  /* IDAT */
  $chunkLength = PNGIDATLength($png);
  Write4BytesBE($data, $chunkLength, $position);
  WriteStringBytes($data, $literal = str_split("IDAT"), $position);
  WriteByte($data, $png->zlibStruct->CMF, $position);
  WriteByte($data, $png->zlibStruct->FLG, $position);
  for($i = 0.0; $i < count($png->zlibStruct->CompressedDataBlocks); $i = $i + 1.0){
    WriteByte($data, $png->zlibStruct->CompressedDataBlocks[$i], $position);
  }
  Write4BytesBE($data, $png->zlibStruct->Adler32CheckValue, $position);
  Write4BytesBE($data, CRC32OfInterval($data, $position->numberValue - $chunkLength - 4.0, $chunkLength + 4.0), $position);

  /* IEND */
  $chunkLength = 0.0;
  Write4BytesBE($data, $chunkLength, $position);
  WriteStringBytes($data, $literal = str_split("IEND"), $position);
  Write4BytesBE($data, CRC32OfInterval($data, $position->numberValue - 4.0, 4.0), $position);

  return $data;
}
function PNGIDATLength($png){
  return 2.0 + count($png->zlibStruct->CompressedDataBlocks) + 4.0;
}
function PNGHeaderLength(){
  return 4.0 + 4.0 + 1.0 + 1.0 + 1.0 + 1.0 + 1.0;
}
function &GetPNGColorData($image){

  $length = 4.0*ImageWidth($image)*ImageHeight($image) + ImageHeight($image);

  $colordata = array_fill(0, $length, 0);

  $next = 0.0;

  for($y = 0.0; $y < ImageHeight($image); $y = $y + 1.0){
    $colordata[$next] = 0.0;
    $next = $next + 1.0;
    for($x = 0.0; $x < ImageWidth($image); $x = $x + 1.0){
      $rgba = $image->x[$x]->y[$y];
      $colordata[$next] = Roundx($rgba->r*255.0);
      $next = $next + 1.0;
      $colordata[$next] = Roundx($rgba->g*255.0);
      $next = $next + 1.0;
      $colordata[$next] = Roundx($rgba->b*255.0);
      $next = $next + 1.0;
      $colordata[$next] = Roundx($rgba->a*255.0);
      $next = $next + 1.0;
    }
  }

  return $colordata;
}
function &GetPNGColorDataGreyscale($image){

  $length = ImageWidth($image)*ImageHeight($image) + ImageHeight($image);

  $colordata = array_fill(0, $length, 0);

  $next = 0.0;

  for($y = 0.0; $y < ImageHeight($image); $y = $y + 1.0){
    $colordata[$next] = 0.0;
    $next = $next + 1.0;
    for($x = 0.0; $x < ImageWidth($image); $x = $x + 1.0){
      $rgba = $image->x[$x]->y[$y];
      $colordata[$next] = Roundx($rgba->r*255.0);
      $next = $next + 1.0;
    }
  }

  return $colordata;
}
function PNGHeader($image, $colortype){

  $ihdr = new stdClass();
  $ihdr->Width = ImageWidth($image);
  $ihdr->Height = ImageHeight($image);
  /* Truecolour with alpha */
  $ihdr->BitDepth = 8.0;
  $ihdr->ColourType = $colortype;
  $ihdr->FilterMethod = 0.0;
  /* None */
  $ihdr->CompressionMethod = 0.0;
  /* zlib */
  $ihdr->InterlaceMethod = 0.0;
  /* no interlace */
  return $ihdr;
}
function &PNGSignature(){

  $s = array_fill(0, 8.0, 0);
  $s[0.0] = 137.0;
  $s[1.0] = 80.0;
  $s[2.0] = 78.0;
  $s[3.0] = 71.0;
  $s[4.0] = 13.0;
  $s[5.0] = 10.0;
  $s[6.0] = 26.0;
  $s[7.0] = 10.0;

  return $s;
}
function &PNGReadDataChunks(&$cs){

  $length = 0.0;
  for($i = 0.0; $i < count($cs); $i = $i + 1.0){
    $c = $cs[$i];
    if(aStringsEqual($c->type, $literal = str_split("IDAT"))){
      $length = $length + $c->length;
    }
  }

  $zlibData = array_fill(0, $length, 0);
  $zlibpos = 0.0;

  for($i = 0.0; $i < count($cs); $i = $i + 1.0){
    $c = $cs[$i];
    if(aStringsEqual($c->type, $literal = str_split("IDAT"))){
      for($j = 0.0; $j < $c->length; $j = $j + 1.0){
        $zlibData[$zlibpos] = $c->data[$j];
        $zlibpos = $zlibpos + 1.0;
      }
    }
  }

  return $zlibData;
}
function PNGReadHeader($image, &$cs, $errorMessages){

  $position = CreateNumberReference(0.0);
  $success = false;

  for($i = 0.0; $i < count($cs); $i = $i + 1.0){
    $c = $cs[$i];
    if(aStringsEqual($c->type, $literal = str_split("IHDR"))){
      $ihdr = new stdClass();

      $ihdr->Width = Read4bytesBE($c->data, $position);
      $ihdr->Height = Read4bytesBE($c->data, $position);
      $ihdr->BitDepth = ReadByte($c->data, $position);
      $ihdr->ColourType = ReadByte($c->data, $position);
      $ihdr->CompressionMethod = ReadByte($c->data, $position);
      $ihdr->FilterMethod = ReadByte($c->data, $position);
      $ihdr->InterlaceMethod = ReadByte($c->data, $position);

      $n = CreateImage($ihdr->Width, $ihdr->Height, GetTransparent());
      $image->x = $n->x;

      if($ihdr->ColourType == 6.0){
        if($ihdr->BitDepth == 8.0){
          if($ihdr->CompressionMethod == 0.0){
            if($ihdr->FilterMethod == 0.0){
              if($ihdr->InterlaceMethod == 0.0){
                $success = true;
              }else{
                $success = false;
                $errorMessages->string = AppendString($errorMessages->string, $literal = str_split("Interlace method not supported."));
              }
            }else{
              $success = false;
              $errorMessages->string = AppendString($errorMessages->string, $literal = str_split("Filter method not supported."));
            }
          }else{
            $success = false;
            $errorMessages->string = AppendString($errorMessages->string, $literal = str_split("Compression type not supported."));
          }
        }else{
          $success = false;
          $errorMessages->string = AppendString($errorMessages->string, $literal = str_split("Bit depth not supported."));
        }
      }else{
        $success = false;
        $errorMessages->string = AppendString($errorMessages->string, $literal = str_split("Color type not supported."));
      }
    }
  }

  return $success;
}
function &PNGReadChunks(&$data, $position){
  $done = false;
  $prepos = $position->numberValue;
  for($chunks = 0.0;  !$done ; $chunks = $chunks + 1.0){
    $c = PNGReadChunk($data, $position);
    if(aStringsEqual($c->type, $literal = str_split("IEND"))){
      $done = true;
    }
  }
  $position->numberValue = $prepos;
  $cs = array_fill(0, $chunks, 0);
  for($i = 0.0; $i < $chunks; $i = $i + 1.0){
    $cs[$i] = PNGReadChunk($data, $position);
  }

  return $cs;
}
function PNGReadChunk(&$data, $position){

  $c = new stdClass();

  $c->length = Read4bytesBE($data, $position);
  $c->type = array_fill(0, 4.0, 0);
  $c->type[0.0] = unichr(ReadByte($data, $position));
  $c->type[1.0] = unichr(ReadByte($data, $position));
  $c->type[2.0] = unichr(ReadByte($data, $position));
  $c->type[3.0] = unichr(ReadByte($data, $position));
  $c->data = ReadXbytes($data, $position, $c->length);
  $c->crc = Read4bytesBE($data, $position);

  return $c;
}
function WriteStringToStingStream(&$stream, $index, &$src){

  for($i = 0.0; $i < count($src); $i = $i + 1.0){
    $stream[$index->numberValue + $i] = $src[$i];
  }
  $index->numberValue = $index->numberValue + count($src);
}
function WriteCharacterToStingStream(&$stream, $index, $src){
  $stream[$index->numberValue] = $src;
  $index->numberValue = $index->numberValue + 1.0;
}
function WriteBooleanToStingStream(&$stream, $index, $src){
  if($src){
    WriteStringToStingStream($stream, $index, $literal = str_split("true"));
  }else{
    WriteStringToStingStream($stream, $index, $literal = str_split("false"));
  }
}
function SubstringWithCheck(&$string, $from, $to, $stringReference){

  if($from >= 0.0 && $from <= count($string) && $to >= 0.0 && $to <= count($string) && $from <= $to){
    $stringReference->string = Substring($string, $from, $to);
    $success = true;
  }else{
    $success = false;
  }

  return $success;
}
function &Substring(&$string, $from, $to){

  $length = $to - $from;

  $n = array_fill(0, $length, 0);

  for($i = $from; $i < $to; $i = $i + 1.0){
    $n[$i - $from] = $string[$i];
  }

  return $n;
}
function &AppendString(&$s1, &$s2){

  $newString = ConcatenateString($s1, $s2);

  unset($s1);

  return $newString;
}
function &ConcatenateString(&$s1, &$s2){

  $newString = array_fill(0, count($s1) + count($s2), 0);

  for($i = 0.0; $i < count($s1); $i = $i + 1.0){
    $newString[$i] = $s1[$i];
  }

  for($i = 0.0; $i < count($s2); $i = $i + 1.0){
    $newString[count($s1) + $i] = $s2[$i];
  }

  return $newString;
}
function &AppendCharacter(&$string, $c){

  $newString = ConcatenateCharacter($string, $c);

  unset($string);

  return $newString;
}
function &ConcatenateCharacter(&$string, $c){
  $newString = array_fill(0, count($string) + 1.0, 0);

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    $newString[$i] = $string[$i];
  }

  $newString[count($string)] = $c;

  return $newString;
}
function &SplitByCharacter(&$toSplit, $splitBy){

  $stringToSplitBy = array_fill(0, 1.0, 0);
  $stringToSplitBy[0.0] = $splitBy;

  $split = SplitByString($toSplit, $stringToSplitBy);

  unset($stringToSplitBy);

  return $split;
}
function IndexOfCharacter(&$string, $character, $indexReference){

  $found = false;
  for($i = 0.0; $i < count($string) &&  !$found ; $i = $i + 1.0){
    if($string[$i] == $character){
      $found = true;
      $indexReference->numberValue = $i;
    }
  }

  return $found;
}
function SubstringEqualsWithCheck(&$string, $from, &$substring, $equalsReference){

  if($from < count($string)){
    $success = true;
    $equalsReference->booleanValue = SubstringEquals($string, $from, $substring);
  }else{
    $success = false;
  }

  return $success;
}
function SubstringEquals(&$string, $from, &$substring){

  $equal = true;
  if(count($string) - $from >= count($substring)){
    for($i = 0.0; $i < count($substring) && $equal; $i = $i + 1.0){
      if($string[$from + $i] != $substring[$i]){
        $equal = false;
      }
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function IndexOfString(&$string, &$substring, $indexReference){

  $found = false;
  for($i = 0.0; $i < count($string) - count($substring) + 1.0 &&  !$found ; $i = $i + 1.0){
    if(SubstringEquals($string, $i, $substring)){
      $found = true;
      $indexReference->numberValue = $i;
    }
  }

  return $found;
}
function ContainsCharacter(&$string, $character){

  $found = false;
  for($i = 0.0; $i < count($string) &&  !$found ; $i = $i + 1.0){
    if($string[$i] == $character){
      $found = true;
    }
  }

  return $found;
}
function ContainsString(&$string, &$substring){
  return IndexOfString($string, $substring, new stdClass());
}
function ToUpperCase(&$string){

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    $string[$i] = charToUpperCase($string[$i]);
  }
}
function ToLowerCase(&$string){

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    $string[$i] = charToLowerCase($string[$i]);
  }
}
function EqualsIgnoreCase(&$a, &$b){

  if(count($a) == count($b)){
    $equal = true;
    for($i = 0.0; $i < count($a) && $equal; $i = $i + 1.0){
      if(charToLowerCase($a[$i]) != charToLowerCase($b[$i])){
        $equal = false;
      }
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function &ReplaceString(&$string, &$toReplace, &$replaceWith){

  $equalsReference = new stdClass();
  $result = array_fill(0, 0.0, 0);

  for($i = 0.0; $i < count($string); ){
    $success = SubstringEqualsWithCheck($string, $i, $toReplace, $equalsReference);
    if($success){
      $success = $equalsReference->booleanValue;
    }

    if($success && count($toReplace) > 0.0){
      $result = ConcatenateString($result, $replaceWith);
      $i = $i + count($toReplace);
    }else{
      $result = ConcatenateCharacter($result, $string[$i]);
      $i = $i + 1.0;
    }
  }

  return $result;
}
function &ReplaceCharacter(&$string, $toReplace, $replaceWith){

  $result = array_fill(0, 0.0, 0);

  for($i = 0.0; $i < count($string); $i = $i + 1.0){
    if($string[$i] == $toReplace){
      $result = ConcatenateCharacter($result, $replaceWith);
    }else{
      $result = ConcatenateCharacter($result, $string[$i]);
    }
  }

  return $result;
}
function &Trimx(&$string){

  /* Find whitepaces at the start. */
  $lastWhitespaceLocationStart = -1.0;
  $firstNonWhitespaceFound = false;
  for($i = 0.0; $i < count($string) &&  !$firstNonWhitespaceFound ; $i = $i + 1.0){
    if(charIsWhiteSpace($string[$i])){
      $lastWhitespaceLocationStart = $i;
    }else{
      $firstNonWhitespaceFound = true;
    }
  }

  /* Find whitepaces at the end. */
  $lastWhitespaceLocationEnd = count($string);
  $firstNonWhitespaceFound = false;
  for($i = count($string) - 1.0; $i >= 0.0 &&  !$firstNonWhitespaceFound ; $i = $i - 1.0){
    if(charIsWhiteSpace($string[$i])){
      $lastWhitespaceLocationEnd = $i;
    }else{
      $firstNonWhitespaceFound = true;
    }
  }

  if($lastWhitespaceLocationStart < $lastWhitespaceLocationEnd){
    $result = Substring($string, $lastWhitespaceLocationStart + 1.0, $lastWhitespaceLocationEnd);
  }else{
    $result = array_fill(0, 0.0, 0);
  }

  return $result;
}
function StartsWith(&$string, &$start){

  $startsWithString = false;
  if(count($string) >= count($start)){
    $startsWithString = SubstringEquals($string, 0.0, $start);
  }

  return $startsWithString;
}
function EndsWith(&$string, &$end){

  $endsWithString = false;
  if(count($string) >= count($end)){
    $endsWithString = SubstringEquals($string, count($string) - count($end), $end);
  }

  return $endsWithString;
}
function &SplitByString(&$toSplit, &$splitBy){

  $split = array_fill(0, 0.0, 0);

  $next = array_fill(0, 0.0, 0);
  for($i = 0.0; $i < count($toSplit); ){
    $c = $toSplit[$i];

    if(SubstringEquals($toSplit, $i, $splitBy)){
      $n = new stdClass();
      $n->string = $next;
      $split = AddString($split, $n);
      $next = array_fill(0, 0.0, 0);
      $i = $i + count($splitBy);
    }else{
      $next = AppendCharacter($next, $c);
      $i = $i + 1.0;
    }
  }

  $n = new stdClass();
  $n->string = $next;
  $split = AddString($split, $n);

  return $split;
}
function StringIsBefore(&$a, &$b){

  $before = false;
  $equal = true;
  $done = false;

  if(count($a) == 0.0 && count($b) > 0.0){
    $before = true;
  }else{
    for($i = 0.0; $i < count($a) && $i < count($b) &&  !$done ; $i = $i + 1.0){
      if($a[$i] != $b[$i]){
        $equal = false;
      }
      if(charCharacterIsBefore($a[$i], $b[$i])){
        $before = true;
      }
      if(charCharacterIsBefore($b[$i], $a[$i])){
        $done = true;
      }
    }

    if($equal){
      if(count($a) < count($b)){
        $before = true;
      }
    }
  }

  return $before;
}
function &ReadXbytes(&$data, $position, $length){

  $r = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $r[$i] = ReadByte($data, $position);
  }

  return $r;
}
function Read4bytesBE(&$data, $position){

  $r = 0.0;
  $r = $r + 2.0**24.0*ReadByte($data, $position);
  $r = $r + 2.0**16.0*ReadByte($data, $position);
  $r = $r + 2.0**8.0*ReadByte($data, $position);
  $r = $r + ReadByte($data, $position);

  return $r;
}
function Read2bytesBE(&$data, $position){

  $r = 0.0;
  $r = $r + 2.0**8.0*ReadByte($data, $position);
  $r = $r + ReadByte($data, $position);

  return $r;
}
function ReadByte(&$data, $position){

  $next = $data[$position->numberValue];
  $position->numberValue = $position->numberValue + 1.0;

  return $next;
}
function Read4bytesLE(&$data, $position){

  $r = 0.0;
  $r = $r + ReadByte($data, $position);
  $r = $r + 2.0**8.0*ReadByte($data, $position);
  $r = $r + 2.0**16.0*ReadByte($data, $position);
  $r = $r + 2.0**24.0*ReadByte($data, $position);

  return $r;
}
function WriteByte(&$data, $b, $position){
  $data[$position->numberValue] = $b;
  $position->numberValue = $position->numberValue + 1.0;
}
function Write2BytesLE(&$data, $b, $position){
  $data[$position->numberValue] = Roundx($b%2.0**8.0);
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = floor($b/2.0**8.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
}
function Write4BytesLE(&$data, $b, $position){
  $data[$position->numberValue] = Roundx($b%2.0**8.0);
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = floor($b/2.0**8.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = floor($b/2.0**16.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = floor($b/2.0**24.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
}
function Write2BytesBE(&$data, $b, $position){
  $data[$position->numberValue] = floor($b/2.0**8.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = Roundx($b%2.0**8.0);
  $position->numberValue = $position->numberValue + 1.0;
}
function Write4BytesBE(&$data, $b, $position){
  $data[$position->numberValue] = floor($b/2.0**24.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = floor($b/2.0**16.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = floor($b/2.0**8.0)%2.0**8.0;
  $position->numberValue = $position->numberValue + 1.0;
  $data[$position->numberValue] = Roundx($b%2.0**8.0);
  $position->numberValue = $position->numberValue + 1.0;
}
function WriteStringBytes(&$data, &$cs, $position){

  for($i = 0.0; $i < count($cs); $i = $i + 1.0){
    $v = uniord($cs[$i]);
    WriteByte($data, $v, $position);
  }
}
function &MakeCRC32Table(){

  $crcTable = array_fill(0, 256.0, 0);

  for($n = 0.0; $n < 256.0; $n = $n + 1.0){
    $c = $n;
    for($k = 0.0; $k < 8.0; $k = $k + 1.0){
      if( !DivisibleBy($c, 2.0) ){
        $c = Xor4Byte(3988292384.0, floor($c/2.0));
      }else{
        $c = floor($c/2.0);
      }
    }
    $crcTable[$n] = $c;
  }

  return $crcTable;
}
function UpdateCRC32($crc, &$buf, &$crc_table){

  for($n = 0.0; $n < count($buf); $n = $n + 1.0){
    $index = And4Byte(Xor4Byte($crc, $buf[$n]), 2.0**8.0 - 1.0);
    $crc = Xor4Byte($crc_table[$index], floor($crc/2.0**8.0));
  }

  return $crc;
}
function CalculateCRC32(&$buf){

  $crcTable = MakeCRC32Table();

  $b32max = 2.0**32.0 - 1.0;
  $value = UpdateCRC32($b32max, $buf, $crcTable);

  return Xor4Byte($value, $b32max);
}
function CRC32OfInterval(&$data, $from, $length){

  $crcBase = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $crcBase[$i] = $data[$from + $i];
  }

  $crc = CalculateCRC32($crcBase);

  unset($crcBase);

  return $crc;
}
function ZLibCompressNoCompression(&$data){

  $zlibStruct = new stdClass();

  $zlibStruct->CMF = 120.0;
  $zlibStruct->FLG = 1.0;
  $zlibStruct->CompressedDataBlocks = DeflateDataNoCompression($data);
  $zlibStruct->Adler32CheckValue = ComputeAdler32($data);

  return $zlibStruct;
}
function ZLibCompressStaticHuffman(&$data, $level){

  $zlibStruct = new stdClass();

  $zlibStruct->CMF = 120.0;
  $zlibStruct->FLG = 1.0;
  $zlibStruct->CompressedDataBlocks = DeflateDataStaticHuffman($data, $level);
  $zlibStruct->Adler32CheckValue = ComputeAdler32($data);

  return $zlibStruct;
}
function &AddNumber(&$list, $a){

  $newlist = array_fill(0, count($list) + 1.0, 0);
  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    $newlist[$i] = $list[$i];
  }
  $newlist[count($list)] = $a;
		
  unset($list);
		
  return $newlist;
}
function AddNumberRef($list, $i){
  $list->numberArray = AddNumber($list->numberArray, $i);
}
function &RemoveNumber(&$list, $n){

  $newlist = array_fill(0, count($list) - 1.0, 0);

  if($n >= 0.0 && $n < count($list)){
    for($i = 0.0; $i < count($list); $i = $i + 1.0){
      if($i < $n){
        $newlist[$i] = $list[$i];
      }
      if($i > $n){
        $newlist[$i - 1.0] = $list[$i];
      }
    }

    unset($list);
  }else{
    unset($newlist);
  }
		
  return $newlist;
}
function GetNumberRef($list, $i){
  return $list->numberArray[$i];
}
function RemoveNumberRef($list, $i){
  $list->numberArray = RemoveNumber($list->numberArray, $i);
}
function &AddString(&$list, $a){

  $newlist = array_fill(0, count($list) + 1.0, 0);

  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    $newlist[$i] = $list[$i];
  }
  $newlist[count($list)] = $a;
		
  unset($list);
		
  return $newlist;
}
function AddStringRef($list, $i){
  $list->stringArray = AddString($list->stringArray, $i);
}
function &RemoveString(&$list, $n){

  $newlist = array_fill(0, count($list) - 1.0, 0);

  if($n >= 0.0 && $n < count($list)){
    for($i = 0.0; $i < count($list); $i = $i + 1.0){
      if($i < $n){
        $newlist[$i] = $list[$i];
      }
      if($i > $n){
        $newlist[$i - 1.0] = $list[$i];
      }
    }

    unset($list);
  }else{
    unset($newlist);
  }
		
  return $newlist;
}
function GetStringRef($list, $i){
  return $list->stringArray[$i];
}
function RemoveStringRef($list, $i){
  $list->stringArray = RemoveString($list->stringArray, $i);
}
function &AddBoolean(&$list, $a){

  $newlist = array_fill(0, count($list) + 1.0, 0);
  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    $newlist[$i] = $list[$i];
  }
  $newlist[count($list)] = $a;
		
  unset($list);
		
  return $newlist;
}
function AddBooleanRef($list, $i){
  $list->booleanArray = AddBoolean($list->booleanArray, $i);
}
function &RemoveBoolean(&$list, $n){

  $newlist = array_fill(0, count($list) - 1.0, 0);

  if($n >= 0.0 && $n < count($list)){
    for($i = 0.0; $i < count($list); $i = $i + 1.0){
      if($i < $n){
        $newlist[$i] = $list[$i];
      }
      if($i > $n){
        $newlist[$i - 1.0] = $list[$i];
      }
    }

    unset($list);
  }else{
    unset($newlist);
  }
		
  return $newlist;
}
function GetBooleanRef($list, $i){
  return $list->booleanArray[$i];
}
function RemoveDecimalRef($list, $i){
  $list->booleanArray = RemoveBoolean($list->booleanArray, $i);
}
function CreateLinkedListString(){

  $ll = new stdClass();
  $ll->first = new stdClass();
  $ll->last = $ll->first;
  $ll->last->end = true;

  return $ll;
}
function LinkedListAddString($ll, &$value){
  $ll->last->end = false;
  $ll->last->value = $value;
  $ll->last->next = new stdClass();
  $ll->last->next->end = true;
  $ll->last = $ll->last->next;
}
function &LinkedListStringsToArray($ll){

  $node = $ll->first;

  $length = LinkedListStringsLength($ll);

  $array = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $array[$i] = new stdClass();
    $array[$i]->string = $node->value;
    $node = $node->next;
  }

  return $array;
}
function LinkedListStringsLength($ll){

  $l = 0.0;
  $node = $ll->first;
  for(;  !$node->end ; ){
    $node = $node->next;
    $l = $l + 1.0;
  }

  return $l;
}
function FreeLinkedListString($ll){

  $node = $ll->first;

  for(;  !$node->end ; ){
    $prev = $node;
    $node = $node->next;
    unset($prev);
  }

  unset($node);
}
function CreateLinkedListNumbers(){

  $ll = new stdClass();
  $ll->first = new stdClass();
  $ll->last = $ll->first;
  $ll->last->end = true;

  return $ll;
}
function &CreateLinkedListNumbersArray($length){

  $lls = array_fill(0, $length, 0);
  for($i = 0.0; $i < count($lls); $i = $i + 1.0){
    $lls[$i] = CreateLinkedListNumbers();
  }

  return $lls;
}
function LinkedListAddNumber($ll, $value){
  $ll->last->end = false;
  $ll->last->value = $value;
  $ll->last->next = new stdClass();
  $ll->last->next->end = true;
  $ll->last = $ll->last->next;
}
function LinkedListNumbersLength($ll){

  $l = 0.0;
  $node = $ll->first;
  for(;  !$node->end ; ){
    $node = $node->next;
    $l = $l + 1.0;
  }

  return $l;
}
function LinkedListNumbersIndex($ll, $index){

  $node = $ll->first;
  for($i = 0.0; $i < $index; $i = $i + 1.0){
    $node = $node->next;
  }

  return $node->value;
}
function LinkedListInsertNumber($ll, $index, $value){

  if($index == 0.0){
    $tmp = $ll->first;
    $ll->first = new stdClass();
    $ll->first->next = $tmp;
    $ll->first->value = $value;
    $ll->first->end = false;
  }else{
    $node = $ll->first;
    for($i = 0.0; $i < $index - 1.0; $i = $i + 1.0){
      $node = $node->next;
    }

    $tmp = $node->next;
    $node->next = new stdClass();
    $node->next->next = $tmp;
    $node->next->value = $value;
    $node->next->end = false;
  }
}
function LinkedListSet($ll, $index, $value){

  $node = $ll->first;
  for($i = 0.0; $i < $index; $i = $i + 1.0){
    $node = $node->next;
  }

  $node->next->value = $value;
}
function LinkedListRemoveNumber($ll, $index){

  $node = $ll->first;
  $prev = $ll->first;

  for($i = 0.0; $i < $index; $i = $i + 1.0){
    $prev = $node;
    $node = $node->next;
  }

  if($index == 0.0){
    $ll->first = $prev->next;
  }
  if( !$prev->next->end ){
    $prev->next = $prev->next->next;
  }
}
function FreeLinkedListNumbers($ll){

  $node = $ll->first;

  for(;  !$node->end ; ){
    $prev = $node;
    $node = $node->next;
    unset($prev);
  }

  unset($node);
}
function FreeLinkedListNumbersArray(&$lls){

  for($i = 0.0; $i < count($lls); $i = $i + 1.0){
    FreeLinkedListNumbers($lls[$i]);
  }
  unset($lls);
}
function &LinkedListNumbersToArray($ll){

  $node = $ll->first;

  $length = LinkedListNumbersLength($ll);

  $array = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $array[$i] = $node->value;
    $node = $node->next;
  }

  return $array;
}
function ArrayToLinkedListNumbers(&$array){

  $ll = CreateLinkedListNumbers();

  for($i = 0.0; $i < count($array); $i = $i + 1.0){
    LinkedListAddNumber($ll, $array[$i]);
  }

  return $ll;
}
function LinkedListNumbersEqual($a, $b){

  $an = $a->first;
  $bn = $b->first;

  $equal = true;
  $done = false;
  for(; $equal &&  !$done ; ){
    if($an->end == $bn->end){
      if($an->end){
        $done = true;
      }else if($an->value == $bn->value){
        $an = $an->next;
        $bn = $bn->next;
      }else{
        $equal = false;
      }
    }else{
      $equal = false;
    }
  }

  return $equal;
}
function CreateLinkedListCharacter(){

  $ll = new stdClass();
  $ll->first = new stdClass();
  $ll->last = $ll->first;
  $ll->last->end = true;

  return $ll;
}
function LinkedListAddCharacter($ll, $value){
  $ll->last->end = false;
  $ll->last->value = $value;
  $ll->last->next = new stdClass();
  $ll->last->next->end = true;
  $ll->last = $ll->last->next;
}
function &LinkedListCharactersToArray($ll){

  $node = $ll->first;

  $length = LinkedListCharactersLength($ll);

  $array = array_fill(0, $length, 0);

  for($i = 0.0; $i < $length; $i = $i + 1.0){
    $array[$i] = $node->value;
    $node = $node->next;
  }

  return $array;
}
function LinkedListCharactersLength($ll){

  $l = 0.0;
  $node = $ll->first;
  for(;  !$node->end ; ){
    $node = $node->next;
    $l = $l + 1.0;
  }

  return $l;
}
function FreeLinkedListCharacter($ll){

  $node = $ll->first;

  for(;  !$node->end ; ){
    $prev = $node;
    $node = $node->next;
    unset($prev);
  }

  unset($node);
}
function CreateDynamicArrayNumbers(){

  $da = new stdClass();
  $da->array = array_fill(0, 10.0, 0);
  $da->length = 0.0;

  return $da;
}
function CreateDynamicArrayNumbersWithInitialCapacity($capacity){

  $da = new stdClass();
  $da->array = array_fill(0, $capacity, 0);
  $da->length = 0.0;

  return $da;
}
function DynamicArrayAddNumber($da, $value){
  if($da->length == count($da->array)){
    DynamicArrayNumbersIncreaseSize($da);
  }

  $da->array[$da->length] = $value;
  $da->length = $da->length + 1.0;
}
function DynamicArrayNumbersIncreaseSize($da){

  $newLength = round(count($da->array)*3.0/2.0);
  $newArray = array_fill(0, $newLength, 0);

  for($i = 0.0; $i < count($da->array); $i = $i + 1.0){
    $newArray[$i] = $da->array[$i];
  }

  unset($da->array);

  $da->array = $newArray;
}
function DynamicArrayNumbersDecreaseSizeNecessary($da){

  $needsDecrease = false;

  if($da->length > 10.0){
    $needsDecrease = $da->length <= round(count($da->array)*2.0/3.0);
  }

  return $needsDecrease;
}
function DynamicArrayNumbersDecreaseSize($da){

  $newLength = round(count($da->array)*2.0/3.0);
  $newArray = array_fill(0, $newLength, 0);

  for($i = 0.0; $i < $newLength; $i = $i + 1.0){
    $newArray[$i] = $da->array[$i];
  }

  unset($da->array);

  $da->array = $newArray;
}
function DynamicArrayNumbersIndex($da, $index){
  return $da->array[$index];
}
function DynamicArrayNumbersLength($da){
  return $da->length;
}
function DynamicArrayInsertNumber($da, $index, $value){

  if($da->length == count($da->array)){
    DynamicArrayNumbersIncreaseSize($da);
  }

  for($i = $da->length; $i > $index; $i = $i - 1.0){
    $da->array[$i] = $da->array[$i - 1.0];
  }

  $da->array[$index] = $value;

  $da->length = $da->length + 1.0;
}
function DynamicArraySet($da, $index, $value){
  $da->array[$index] = $value;
}
function DynamicArrayRemoveNumber($da, $index){

  for($i = $index; $i < $da->length - 1.0; $i = $i + 1.0){
    $da->array[$i] = $da->array[$i + 1.0];
  }

  $da->length = $da->length - 1.0;

  if(DynamicArrayNumbersDecreaseSizeNecessary($da)){
    DynamicArrayNumbersDecreaseSize($da);
  }
}
function FreeDynamicArrayNumbers($da){
  unset($da->array);
  unset($da);
}
function &DynamicArrayNumbersToArray($da){

  $array = array_fill(0, $da->length, 0);

  for($i = 0.0; $i < $da->length; $i = $i + 1.0){
    $array[$i] = $da->array[$i];
  }

  return $array;
}
function ArrayToDynamicArrayNumbersWithOptimalSize(&$array){

  /*
         c = 10*(3/2)^n
         log(c) = log(10*(3/2)^n)
         log(c) = log(10) + log((3/2)^n)
         log(c) = 1 + log((3/2)^n)
         log(c) - 1 = log((3/2)^n)
         log(c) - 1 = n*log(3/2)
         n = (log(c) - 1)/log(3/2)
         */
  $c = count($array);
  $n = (log($c) - 1.0)/log(3.0/2.0);
  $newCapacity = floor($n) + 1.0;

  $da = CreateDynamicArrayNumbersWithInitialCapacity($newCapacity);

  for($i = 0.0; $i < count($array); $i = $i + 1.0){
    $da->array[$i] = $array[$i];
  }

  return $da;
}
function ArrayToDynamicArrayNumbers(&$array){

  $da = new stdClass();
  $da->array = aCopyNumberArray($array);
  $da->length = count($array);

  return $da;
}
function DynamicArrayNumbersEqual($a, $b){

  $equal = true;
  if($a->length == $b->length){
    for($i = 0.0; $i < $a->length && $equal; $i = $i + 1.0){
      if($a->array[$i] != $b->array[$i]){
        $equal = false;
      }
    }
  }else{
    $equal = false;
  }

  return $equal;
}
function DynamicArrayNumbersToLinkedList($da){

  $ll = CreateLinkedListNumbers();

  for($i = 0.0; $i < $da->length; $i = $i + 1.0){
    LinkedListAddNumber($ll, $da->array[$i]);
  }

  return $ll;
}
function LinkedListToDynamicArrayNumbers($ll){

  $node = $ll->first;

  $da = new stdClass();
  $da->length = LinkedListNumbersLength($ll);

  $da->array = array_fill(0, $da->length, 0);

  for($i = 0.0; $i < $da->length; $i = $i + 1.0){
    $da->array[$i] = $node->value;
    $node = $node->next;
  }

  return $da;
}
function &AddCharacter(&$list, $a){

  $newlist = array_fill(0, count($list) + 1.0, 0);
  for($i = 0.0; $i < count($list); $i = $i + 1.0){
    $newlist[$i] = $list[$i];
  }
  $newlist[count($list)] = $a;
		
  unset($list);
		
  return $newlist;
}
function AddCharacterRef($list, $i){
  $list->string = AddCharacter($list->string, $i);
}
function &RemoveCharacter(&$list, $n){

  $newlist = array_fill(0, count($list) - 1.0, 0);

  if($n >= 0.0 && $n < count($list)){
    for($i = 0.0; $i < count($list); $i = $i + 1.0){
      if($i < $n){
        $newlist[$i] = $list[$i];
      }
      if($i > $n){
        $newlist[$i - 1.0] = $list[$i];
      }
    }

    unset($list);
  }else{
    unset($newlist);
  }

  return $newlist;
}
function GetCharacterRef($list, $i){
  return $list->string[$i];
}
function RemoveCharacterRef($list, $i){
  $list->string = RemoveCharacter($list->string, $i);
}
function charToLowerCase($character){

  $toReturn = $character;
  if($character == "A"){
    $toReturn = "a";
  }else if($character == "B"){
    $toReturn = "b";
  }else if($character == "C"){
    $toReturn = "c";
  }else if($character == "D"){
    $toReturn = "d";
  }else if($character == "E"){
    $toReturn = "e";
  }else if($character == "F"){
    $toReturn = "f";
  }else if($character == "G"){
    $toReturn = "g";
  }else if($character == "H"){
    $toReturn = "h";
  }else if($character == "I"){
    $toReturn = "i";
  }else if($character == "J"){
    $toReturn = "j";
  }else if($character == "K"){
    $toReturn = "k";
  }else if($character == "L"){
    $toReturn = "l";
  }else if($character == "M"){
    $toReturn = "m";
  }else if($character == "N"){
    $toReturn = "n";
  }else if($character == "O"){
    $toReturn = "o";
  }else if($character == "P"){
    $toReturn = "p";
  }else if($character == "Q"){
    $toReturn = "q";
  }else if($character == "R"){
    $toReturn = "r";
  }else if($character == "S"){
    $toReturn = "s";
  }else if($character == "T"){
    $toReturn = "t";
  }else if($character == "U"){
    $toReturn = "u";
  }else if($character == "V"){
    $toReturn = "v";
  }else if($character == "W"){
    $toReturn = "w";
  }else if($character == "X"){
    $toReturn = "x";
  }else if($character == "Y"){
    $toReturn = "y";
  }else if($character == "Z"){
    $toReturn = "z";
  }

  return $toReturn;
}
function charToUpperCase($character){

  $toReturn = $character;
  if($character == "a"){
    $toReturn = "A";
  }else if($character == "b"){
    $toReturn = "B";
  }else if($character == "c"){
    $toReturn = "C";
  }else if($character == "d"){
    $toReturn = "D";
  }else if($character == "e"){
    $toReturn = "E";
  }else if($character == "f"){
    $toReturn = "F";
  }else if($character == "g"){
    $toReturn = "G";
  }else if($character == "h"){
    $toReturn = "H";
  }else if($character == "i"){
    $toReturn = "I";
  }else if($character == "j"){
    $toReturn = "J";
  }else if($character == "k"){
    $toReturn = "K";
  }else if($character == "l"){
    $toReturn = "L";
  }else if($character == "m"){
    $toReturn = "M";
  }else if($character == "n"){
    $toReturn = "N";
  }else if($character == "o"){
    $toReturn = "O";
  }else if($character == "p"){
    $toReturn = "P";
  }else if($character == "q"){
    $toReturn = "Q";
  }else if($character == "r"){
    $toReturn = "R";
  }else if($character == "s"){
    $toReturn = "S";
  }else if($character == "t"){
    $toReturn = "T";
  }else if($character == "u"){
    $toReturn = "U";
  }else if($character == "v"){
    $toReturn = "V";
  }else if($character == "w"){
    $toReturn = "W";
  }else if($character == "x"){
    $toReturn = "X";
  }else if($character == "y"){
    $toReturn = "Y";
  }else if($character == "z"){
    $toReturn = "Z";
  }

  return $toReturn;
}
function charIsUpperCase($character){

  $isUpper = false;
  if($character == "A"){
    $isUpper = true;
  }else if($character == "B"){
    $isUpper = true;
  }else if($character == "C"){
    $isUpper = true;
  }else if($character == "D"){
    $isUpper = true;
  }else if($character == "E"){
    $isUpper = true;
  }else if($character == "F"){
    $isUpper = true;
  }else if($character == "G"){
    $isUpper = true;
  }else if($character == "H"){
    $isUpper = true;
  }else if($character == "I"){
    $isUpper = true;
  }else if($character == "J"){
    $isUpper = true;
  }else if($character == "K"){
    $isUpper = true;
  }else if($character == "L"){
    $isUpper = true;
  }else if($character == "M"){
    $isUpper = true;
  }else if($character == "N"){
    $isUpper = true;
  }else if($character == "O"){
    $isUpper = true;
  }else if($character == "P"){
    $isUpper = true;
  }else if($character == "Q"){
    $isUpper = true;
  }else if($character == "R"){
    $isUpper = true;
  }else if($character == "S"){
    $isUpper = true;
  }else if($character == "T"){
    $isUpper = true;
  }else if($character == "U"){
    $isUpper = true;
  }else if($character == "V"){
    $isUpper = true;
  }else if($character == "W"){
    $isUpper = true;
  }else if($character == "X"){
    $isUpper = true;
  }else if($character == "Y"){
    $isUpper = true;
  }else if($character == "Z"){
    $isUpper = true;
  }

  return $isUpper;
}
function charIsLowerCase($character){

  $isLower = false;
  if($character == "a"){
    $isLower = true;
  }else if($character == "b"){
    $isLower = true;
  }else if($character == "c"){
    $isLower = true;
  }else if($character == "d"){
    $isLower = true;
  }else if($character == "e"){
    $isLower = true;
  }else if($character == "f"){
    $isLower = true;
  }else if($character == "g"){
    $isLower = true;
  }else if($character == "h"){
    $isLower = true;
  }else if($character == "i"){
    $isLower = true;
  }else if($character == "j"){
    $isLower = true;
  }else if($character == "k"){
    $isLower = true;
  }else if($character == "l"){
    $isLower = true;
  }else if($character == "m"){
    $isLower = true;
  }else if($character == "n"){
    $isLower = true;
  }else if($character == "o"){
    $isLower = true;
  }else if($character == "p"){
    $isLower = true;
  }else if($character == "q"){
    $isLower = true;
  }else if($character == "r"){
    $isLower = true;
  }else if($character == "s"){
    $isLower = true;
  }else if($character == "t"){
    $isLower = true;
  }else if($character == "u"){
    $isLower = true;
  }else if($character == "v"){
    $isLower = true;
  }else if($character == "w"){
    $isLower = true;
  }else if($character == "x"){
    $isLower = true;
  }else if($character == "y"){
    $isLower = true;
  }else if($character == "z"){
    $isLower = true;
  }

  return $isLower;
}
function charIsLetter($character){
  return charIsUpperCase($character) || charIsLowerCase($character);
}
function charIsNumber($character){

  $isNumberx = false;
  if($character == "0"){
    $isNumberx = true;
  }else if($character == "1"){
    $isNumberx = true;
  }else if($character == "2"){
    $isNumberx = true;
  }else if($character == "3"){
    $isNumberx = true;
  }else if($character == "4"){
    $isNumberx = true;
  }else if($character == "5"){
    $isNumberx = true;
  }else if($character == "6"){
    $isNumberx = true;
  }else if($character == "7"){
    $isNumberx = true;
  }else if($character == "8"){
    $isNumberx = true;
  }else if($character == "9"){
    $isNumberx = true;
  }

  return $isNumberx;
}
function charIsWhiteSpace($character){

  $isWhiteSpacex = false;
  if($character == " "){
    $isWhiteSpacex = true;
  }else if($character == "\t"){
    $isWhiteSpacex = true;
  }else if($character == "\n"){
    $isWhiteSpacex = true;
  }else if($character == "\r"){
    $isWhiteSpacex = true;
  }

  return $isWhiteSpacex;
}
function charIsSymbol($character){

  $isSymbolx = false;
  if($character == "!"){
    $isSymbolx = true;
  }else if($character == "\""){
    $isSymbolx = true;
  }else if($character == "#"){
    $isSymbolx = true;
  }else if($character == "$"){
    $isSymbolx = true;
  }else if($character == "%"){
    $isSymbolx = true;
  }else if($character == "&"){
    $isSymbolx = true;
  }else if($character == "\'"){
    $isSymbolx = true;
  }else if($character == "("){
    $isSymbolx = true;
  }else if($character == ")"){
    $isSymbolx = true;
  }else if($character == "*"){
    $isSymbolx = true;
  }else if($character == "+"){
    $isSymbolx = true;
  }else if($character == ","){
    $isSymbolx = true;
  }else if($character == "-"){
    $isSymbolx = true;
  }else if($character == "."){
    $isSymbolx = true;
  }else if($character == "/"){
    $isSymbolx = true;
  }else if($character == ":"){
    $isSymbolx = true;
  }else if($character == ";"){
    $isSymbolx = true;
  }else if($character == "<"){
    $isSymbolx = true;
  }else if($character == "="){
    $isSymbolx = true;
  }else if($character == ">"){
    $isSymbolx = true;
  }else if($character == "?"){
    $isSymbolx = true;
  }else if($character == "@"){
    $isSymbolx = true;
  }else if($character == "["){
    $isSymbolx = true;
  }else if($character == "\\"){
    $isSymbolx = true;
  }else if($character == "]"){
    $isSymbolx = true;
  }else if($character == "^"){
    $isSymbolx = true;
  }else if($character == "_"){
    $isSymbolx = true;
  }else if($character == "`"){
    $isSymbolx = true;
  }else if($character == "{"){
    $isSymbolx = true;
  }else if($character == "|"){
    $isSymbolx = true;
  }else if($character == "}"){
    $isSymbolx = true;
  }else if($character == "~"){
    $isSymbolx = true;
  }

  return $isSymbolx;
}
function charCharacterIsBefore($a, $b){

  $ad = uniord($a);
  $bd = uniord($b);

  return $ad < $bd;
}
function And4Byte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFFFFFFFF && $n2 >= 0 && $n2 <= 0xFFFFFFFF){
      return ($n1 & $n2) & 0xFFFFFFFF;
    }else{
      return 0;
    }
}
function And2Byte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFFFF && $n2 >= 0 && $n2 <= 0xFFFF){
      return ($n1 & $n2) & 0xFFFF;
    }else{
      return 0;
    }
}
function AndByte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFF && $n2 >= 0 && $n2 <= 0xFF){
      return ($n1 & $n2) & 0xFF;
    }else{
      return 0;
    }
}
function AndBytes($n1, $n2, $bytes){

  $byteVal = 1.0;
  $result = 0.0;

  if($n1 >= 0.0 && $n1 < 2.0**($bytes*8.0) && $n2 >= 0.0 && $n2 < 2.0**($bytes*8.0)){
    $n1 = Truncate($n1);
    $n2 = Truncate($n2);
    $bytes = Truncate($bytes);

    for($i = 0.0; $i < $bytes*8.0; $i = $i + 1.0){
      if($n1%2.0 == 1.0 && $n2%2.0 == 1.0){
        $result = $result + $byteVal;
      }
      $n1 = floor($n1/2.0);
      $n2 = floor($n2/2.0);
      $byteVal = $byteVal*2.0;
    }
  }

  return $result;
}
function Or4Byte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFFFFFFFF && $n2 >= 0 && $n2 <= 0xFFFFFFFF){
      return ($n1 | $n2) & 0xFFFFFFFF;
    }else{
      return 0;
    }
}
function Or2Byte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFFFF && $n2 >= 0 && $n2 <= 0xFFFF){
      return ($n1 | $n2) & 0xFFFF;
    }else{
      return 0;
    }
}
function OrByte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFF && $n2 >= 0 && $n2 <= 0xFF){
      return ($n1 | $n2) & 0xFF;
    }else{
      return 0;
    }
}
function OrBytes($n1, $n2, $bytes){

  $byteVal = 1.0;
  $result = 0.0;

  if($n1 >= 0.0 && $n1 < 2.0**($bytes*8.0) && $n2 >= 0.0 && $n2 < 2.0**($bytes*8.0)){
    $n1 = Truncate($n1);
    $n2 = Truncate($n2);
    $bytes = Truncate($bytes);

    for($i = 0.0; $i < $bytes*8.0; $i = $i + 1.0){
      if($n1%2.0 == 1.0 || $n2%2.0 == 1.0){
        $result = $result + $byteVal;
      }
      $n1 = floor($n1/2.0);
      $n2 = floor($n2/2.0);
      $byteVal = $byteVal*2.0;
    }
  }

  return $result;
}
function Xor4Byte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFFFFFFFF && $n2 >= 0 && $n2 <= 0xFFFFFFFF){
      return ($n1 ^ $n2) & 0xFFFFFFFF;
    }else{
      return 0;
    }
}
function Xor2Byte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFFFF && $n2 >= 0 && $n2 <= 0xFFFF){
      return ($n1 ^ $n2) & 0xFFFF;
    }else{
      return 0;
    }
}
function XorByte($n1, $n2){
    if($n1 >= 0 && $n1 <= 0xFF && $n2 >= 0 && $n2 <= 0xFF){
      return ($n1 ^ $n2) & 0xFF;
    }else{
      return 0;
    }
}
function XorBytes($n1, $n2, $bytes){

  $byteVal = 1.0;
  $result = 0.0;

  if($n1 >= 0.0 && $n1 < 2.0**($bytes*8.0) && $n2 >= 0.0 && $n2 < 2.0**($bytes*8.0)){
    $n1 = Truncate($n1);
    $n2 = Truncate($n2);
    $bytes = Truncate($bytes);

    for($i = 0.0; $i < $bytes*8.0; $i = $i + 1.0){
      if($n1%2.0 != $n2%2.0){
        $result = $result + $byteVal;
      }
      $n1 = floor($n1/2.0);
      $n2 = floor($n2/2.0);
      $byteVal = $byteVal*2.0;
    }
  }

  return $result;
}
function Not4Byte($b){
    if($b >= 0 && $b <= 0xFFFFFFFF){
      return ~$b & 0xFFFFFFFF;
    }else{
      return 0;
    }
}
function Not2Byte($b){
    if($b >= 0 && $b <= 0xFFFF){
      return ~$b & 0xFFFF;
    }else{
      return 0;
    }
}
function NotByte($b){
    if($b >= 0 && $b <= 0xFF){
      return ~$b & 0xFF;
    }else{
      return 0;
    }
}
function NotBytes($b, $length){

  $result = 0.0;

  if($b >= 0.0 && $b < 2.0**($length*8.0)){
    $b = Truncate($b);
    $length = Truncate($length);

    $result = 2.0**($length*8.0) - $b - 1.0;
  }

  return $result;
}
function ShiftLeft4Byte($b, $amount){
    if($b >= 0 && $b <= 0xFFFFFFFF && $amount >= 0 && $amount < 32){
      return ($b << $amount) & 0xFFFFFFFF;
    }else{
      return 0;
    }
}
function ShiftLeft2Byte($b, $amount){
    if($b >= 0 && $b <= 0xFFFF && $amount >= 0 && $amount < 16){
      return ($b << $amount) & 0xFFFF;
    }else{
      return 0;
    }
}
function ShiftLeftByte($b, $amount){
    if($b >= 0 && $b <= 0xFF && $amount >= 0 && $amount < 8){
      return ($b << $amount) & 0xFF;
    }else{
      return 0;
    }
}
function ShiftLeftBytes($b, $amount, $length){

  $result = 0.0;

  if($b >= 0.0 && $b < 2.0**($length*8.0) && $amount >= 0.0 && $amount <= $length*8.0){
    $b = Truncate($b);
    $amount = Truncate($amount);

    $result = $b*2.0**$amount;
  }

  return $result;
}
function ShiftRight4Byte($b, $amount){
    if($b >= 0 && $b <= 0xFFFFFFFF && $amount >= 0 && $amount < 32){
      return ($b >> $amount) & 0xFFFFFFFF;
    }else{
      return 0;
    }
}
function ShiftRight2Byte($b, $amount){
    if($b >= 0 && $b <= 0xFFFF && $amount >= 0 && $amount < 16){
      return ($b >> $amount) & 0xFFFF;
    }else{
      return 0;
    }
}
function ShiftRightByte($b, $amount){
    if($b >= 0 && $b <= 0xFF && $amount >= 0 && $amount < 8){
      return ($b >> $amount) & 0xFF;
    }else{
      return 0;
    }
}
function ShiftRightBytes($b, $amount, $length){

  $result = 0.0;

  if($b >= 0.0 && $b < 2.0**($length*8.0) && $amount >= 0.0 && $amount <= $length*8.0){
    $b = Truncate($b);
    $amount = Truncate($amount);

    $result = Truncate($b/2.0**$amount);
  }

  return $result;
}
function ReadNextBit(&$data, $nextbit){

  $bytenr = floor($nextbit->numberValue/8.0);
  $bitnumber = $nextbit->numberValue%8.0;

  $b = $data[$bytenr];

  $bit = floor($b/2.0**$bitnumber)%2.0;

  $nextbit->numberValue = $nextbit->numberValue + 1.0;

  return $bit;
}
function BitExtract($b, $fromInc, $toInc){
  return floor($b/2.0**$fromInc)%2.0**($toInc + 1.0 - $fromInc);
}
function ReadBitRange(&$data, $nextbit, $length){

  $number = 0.0;

  $startbyte = floor($nextbit->numberValue/8.0);
  $endbyte = floor(($nextbit->numberValue + $length)/8.0);

  $startbit = $nextbit->numberValue%8.0;
  $endbit = ($nextbit->numberValue + $length - 1.0)%8.0;

  if($startbyte == $endbyte){
    $number = BitExtract($data[$startbyte], $startbit, $endbit);
  }

  $nextbit->numberValue = $nextbit->numberValue + $length;

  return $number;
}
function SkipToBoundary($nextbit){

  $skip = 8.0 - $nextbit->numberValue%8.0;
  $nextbit->numberValue = $nextbit->numberValue + $skip;
}
function ReadNextByteBoundary(&$data, $nextbit){

  $bytenr = floor($nextbit->numberValue/8.0);
  $b = $data[$bytenr];
  $nextbit->numberValue = $nextbit->numberValue + 8.0;

  return $b;
}
function Read2bytesByteBoundary(&$data, $nextbit){

  $r = 0.0;
  $r = $r + 2.0**8.0*ReadNextByteBoundary($data, $nextbit);
  $r = $r + ReadNextByteBoundary($data, $nextbit);

  return $r;
}
function ComputeAdler32(&$data){

  $a = 1.0;
  $b = 0.0;
  $m = 65521.0;

  for($i = 0.0; $i < count($data); $i = $i + 1.0){
    $a = ($a + $data[$i])%$m;
    $b = ($b + $a)%$m;
  }

  return $b*2.0**16.0 + $a;
}
function &DeflateDataStaticHuffman(&$data, $level){

  $code = CreateNumberReference(0.0);
  $length = CreateNumberReference(0.0);
  $compressedCode = CreateNumberReference(0.0);
  $lengthAdditionLength = CreateNumberReference(0.0);
  $distanceCode = CreateNumberReference(0.0);
  $distanceReference = CreateNumberReference(0.0);
  $lengthReference = CreateNumberReference(0.0);
  $lengthAddition = CreateNumberReference(0.0);
  $distanceAdditionReference = CreateNumberReference(0.0);
  $distanceAdditionLengthReference = CreateNumberReference(0.0);
  $match = new stdClass();

  $bytes = array_fill(0, max(count($data)*2.0, 100.0), 0);
  aFillNumberArray($bytes, 0.0);
  $currentBit = CreateNumberReference(0.0);

  $bitReverseLookupTable = GenerateBitReverseLookupTable(9.0);

  /* Final block */
  AppendBitsToBytesRight($bytes, $currentBit, 1.0, 1.0);
  /* Fixed code */
  AppendBitsToBytesRight($bytes, $currentBit, 1.0, 2.0);

  for($i = 0.0; $i < count($data); ){
    FindMatch($data, $i, $distanceReference, $lengthReference, $match, $level);

    if($match->booleanValue){
      GetDeflateLengthCode($lengthReference->numberValue, $compressedCode, $lengthAddition, $lengthAdditionLength);
      GetDeflateDistanceCode($distanceReference->numberValue, $distanceCode, $distanceAdditionReference, $distanceAdditionLengthReference, $bitReverseLookupTable);
    }

    if( !$match->booleanValue ){
      GetDeflateStaticHuffmanCode($data[$i], $code, $length, $bitReverseLookupTable);
      AppendBitsToBytesRight($bytes, $currentBit, $code->numberValue, $length->numberValue);
      $i = $i + 1.0;
    }else{
      GetDeflateStaticHuffmanCode($compressedCode->numberValue, $code, $length, $bitReverseLookupTable);
      AppendBitsToBytesRight($bytes, $currentBit, $code->numberValue, $length->numberValue);
      AppendBitsToBytesRight($bytes, $currentBit, $lengthAddition->numberValue, $lengthAdditionLength->numberValue);
      AppendBitsToBytesRight($bytes, $currentBit, $distanceCode->numberValue, 5.0);
      AppendBitsToBytesRight($bytes, $currentBit, $distanceAdditionReference->numberValue, $distanceAdditionLengthReference->numberValue);
      $i = $i + $lengthReference->numberValue;
    }
  }

  /* Stop symbol */
  GetDeflateStaticHuffmanCode(256.0, $code, $length, $bitReverseLookupTable);
  AppendBitsToBytesRight($bytes, $currentBit, $code->numberValue, $length->numberValue);

  $copyx = new stdClass();
  aCopyNumberArrayRange($bytes, 0.0, ceil($currentBit->numberValue/8.0), $copyx);
  unset($bytes);
  $bytes = $copyx->numberArray;

  return $bytes;
}
function FindMatch(&$data, $pos, $distanceReference, $lengthReference, $match, $level){

  $deflateMinMength = 3.0;
  $deflateMaxLength = 258.0;

  $longest = min($pos - 1.0, $deflateMaxLength);
  $longest = min(count($data) - $pos, $longest);

  $deflateMaxDistance = floor(32768.0/10.0*$level);

  $startDistance = min($pos, $deflateMaxDistance);

  if($longest >= $deflateMinMength){
    $maxLength = 0.0;
    $distanceForMax = 0.0;

    for($i = $pos - 1.0; $i >= $pos - $startDistance && $maxLength != $longest; $i = $i - 1.0){
      $matchLength = 0.0;
      $done = false;
      for($j = 0.0; $j < $longest &&  !$done ; $j = $j + 1.0){
        if($data[$i + $j] == $data[$pos + $j]){
          $matchLength = $matchLength + 1.0;
        }else{
          $done = true;
        }
      }

      if($matchLength >= $deflateMinMength && $matchLength > $maxLength){
        $maxLength = $matchLength;
        $distanceForMax = $pos - $i;
      }
    }

    if($maxLength >= $deflateMinMength){
      $match->booleanValue = true;
      $lengthReference->numberValue = $maxLength;
      $distanceReference->numberValue = $distanceForMax;
    }else{
      $match->booleanValue = false;
    }
  }else{
    $match->booleanValue = false;
  }
}
function &GenerateBitReverseLookupTable($bits){

  $table = array_fill(0, 2.0**$bits, 0);

  for($i = 0.0; $i < count($table); $i = $i + 1.0){
    $table[$i] = ReverseBits($i, 32.0);
  }

  return $table;
}
function ReverseBits($x, $bits){

  $b = 0.0;

  for($i = 0.0; $i < $bits; $i = $i + 1.0){
    $b = ShiftLeft4Byte($b, 1.0);
    $bit = And4Byte($x, 1.0);
    $b = Or4Byte($b, $bit);
    $x = ShiftRight4Byte($x, 1.0);
  }

  return $b;
}
function &DeflateDataNoCompression(&$data){

  $maxblocksize = 2.0**16.0 - 1.0;
  $blocks = ceil(count($data)/$maxblocksize);

  $position = CreateNumberReference(0.0);

  $deflated = array_fill(0, (1.0 + 4.0)*$blocks + count($data), 0);

  for($block = 0.0; $block < $blocks; $block = $block + 1.0){
    if($block + 1.0 == $blocks){
      WriteByte($deflated, 1.0, $position);
    }else{
      WriteByte($deflated, 0.0, $position);
    }
    $blocklength = min(count($data) - $block*$maxblocksize, $maxblocksize);
    Write2BytesLE($deflated, $blocklength, $position);
    Write2BytesLE($deflated, Not2Byte($blocklength), $position);

    for($i = 0.0; $i < $blocklength; $i = $i + 1.0){
      WriteByte($deflated, $data[$block*$maxblocksize + $i], $position);
    }
  }

  return $deflated;
}
function GetDeflateStaticHuffmanCode($b, $code, $length, &$bitReverseLookupTable){

  if($b >= 0.0 && $b <= 143.0){
    $code->numberValue = 48.0 + $b;
    $length->numberValue = 8.0;
  }else if($b >= 144.0 && $b <= 255.0){
    $code->numberValue = $b - 144.0 + 400.0;
    $length->numberValue = 9.0;
  }else if($b >= 256.0 && $b <= 279.0){
    $code->numberValue = $b - 256.0 + 0.0;
    $length->numberValue = 7.0;
  }else if($b >= 280.0 && $b <= 287.0){
    $code->numberValue = $b - 280.0 + 192.0;
    $length->numberValue = 8.0;
  }

  $reversed = $bitReverseLookupTable[$code->numberValue];
  $code->numberValue = ShiftRight4Byte($reversed, 32.0 - $length->numberValue);
}
function GetDeflateLengthCode($length, $code, $lengthAddition, $lengthAdditionLength){
  if($length >= 3.0 && $length <= 10.0){
    $code->numberValue = 257.0 + $length - 3.0;
    $lengthAdditionLength->numberValue = 0.0;
  }else if($length >= 11.0 && $length <= 18.0){
    $code->numberValue = 265.0 + floor(($length - 11.0)/2.0);
    $lengthAddition->numberValue = floor(($length - 11.0)%2.0);
    $lengthAdditionLength->numberValue = 1.0;
  }else if($length >= 19.0 && $length <= 34.0){
    $code->numberValue = 269.0 + floor(($length - 19.0)/4.0);
    $lengthAddition->numberValue = floor(($length - 19.0)%4.0);
    $lengthAdditionLength->numberValue = 2.0;
  }else if($length >= 35.0 && $length <= 66.0){
    $code->numberValue = 273.0 + floor(($length - 35.0)/8.0);
    $lengthAddition->numberValue = floor(($length - 35.0)%8.0);
    $lengthAdditionLength->numberValue = 3.0;
  }else if($length >= 67.0 && $length <= 130.0){
    $code->numberValue = 277.0 + floor(($length - 67.0)/16.0);
    $lengthAddition->numberValue = floor(($length - 67.0)%16.0);
    $lengthAdditionLength->numberValue = 4.0;
  }else if($length >= 131.0 && $length <= 257.0){
    $code->numberValue = 281.0 + floor(($length - 131.0)/32.0);
    $lengthAddition->numberValue = floor(($length - 131.0)%32.0);
    $lengthAdditionLength->numberValue = 5.0;
  }else if($length == 258.0){
    $code->numberValue = 285.0;
    $lengthAdditionLength->numberValue = 0.0;
  }
}
function GetDeflateDistanceCode($distance, $code, $distanceAdditionReference, $distanceAdditionLengthReference, &$bitReverseLookupTable){

  if($distance >= 1.0 && $distance <= 4.0){
    $code->numberValue = $distance - 1.0;
    $distanceAdditionLengthReference->numberValue = 0.0;
  }else if($distance >= 5.0 && $distance <= 8.0){
    $code->numberValue = 4.0 + floor(($distance - 5.0)/2.0);
    $distanceAdditionReference->numberValue = floor(($distance - 5.0)%2.0);
    $distanceAdditionLengthReference->numberValue = 1.0;
  }else if($distance >= 9.0 && $distance <= 16.0){
    $code->numberValue = 6.0 + floor(($distance - 9.0)/4.0);
    $distanceAdditionReference->numberValue = floor(($distance - 9.0)%4.0);
    $distanceAdditionLengthReference->numberValue = 2.0;
  }else if($distance >= 17.0 && $distance <= 32.0){
    $code->numberValue = 8.0 + floor(($distance - 17.0)/8.0);
    $distanceAdditionReference->numberValue = floor(($distance - 17.0)%8.0);
    $distanceAdditionLengthReference->numberValue = 3.0;
  }else if($distance >= 33.0 && $distance <= 64.0){
    $code->numberValue = 10.0 + floor(($distance - 33.0)/16.0);
    $distanceAdditionReference->numberValue = floor(($distance - 33.0)%16.0);
    $distanceAdditionLengthReference->numberValue = 4.0;
  }else if($distance >= 65.0 && $distance <= 128.0){
    $code->numberValue = 12.0 + floor(($distance - 65.0)/32.0);
    $distanceAdditionReference->numberValue = floor(($distance - 65.0)%32.0);
    $distanceAdditionLengthReference->numberValue = 5.0;
  }else if($distance >= 129.0 && $distance <= 256.0){
    $code->numberValue = 14.0 + floor(($distance - 129.0)/64.0);
    $distanceAdditionReference->numberValue = floor(($distance - 129.0)%64.0);
    $distanceAdditionLengthReference->numberValue = 6.0;
  }else if($distance >= 257.0 && $distance <= 512.0){
    $code->numberValue = 16.0 + floor(($distance - 257.0)/128.0);
    $distanceAdditionReference->numberValue = floor(($distance - 257.0)%128.0);
    $distanceAdditionLengthReference->numberValue = 7.0;
  }else if($distance >= 513.0 && $distance <= 1024.0){
    $code->numberValue = 18.0 + floor(($distance - 513.0)/256.0);
    $distanceAdditionReference->numberValue = floor(($distance - 513.0)%256.0);
    $distanceAdditionLengthReference->numberValue = 8.0;
  }else if($distance >= 1025.0 && $distance <= 2048.0){
    $code->numberValue = 20.0 + floor(($distance - 1025.0)/2.0**9.0);
    $distanceAdditionReference->numberValue = floor(($distance - 1025.0)%2.0**9.0);
    $distanceAdditionLengthReference->numberValue = 9.0;
  }else if($distance >= 2049.0 && $distance <= 4096.0){
    $code->numberValue = 22.0 + floor(($distance - 2049.0)/2.0**10.0);
    $distanceAdditionReference->numberValue = floor(($distance - 2049.0)%2.0**10.0);
    $distanceAdditionLengthReference->numberValue = 10.0;
  }else if($distance >= 4097.0 && $distance <= 8192.0){
    $code->numberValue = 24.0 + floor(($distance - 4097.0)/2.0**11.0);
    $distanceAdditionReference->numberValue = floor(($distance - 4097.0)%2.0**11.0);
    $distanceAdditionLengthReference->numberValue = 11.0;
  }else if($distance >= 8193.0 && $distance <= 16384.0){
    $code->numberValue = 26.0 + floor(($distance - 8193.0)/2.0**12.0);
    $distanceAdditionReference->numberValue = floor(($distance - 8193.0)%2.0**12.0);
    $distanceAdditionLengthReference->numberValue = 12.0;
  }else if($distance >= 16385.0 && $distance <= 32768.0){
    $code->numberValue = 28.0 + floor(($distance - 16385.0)/2.0**13.0);
    $distanceAdditionReference->numberValue = floor(($distance - 16385.0)%2.0**13.0);
    $distanceAdditionLengthReference->numberValue = 13.0;
  }

  $reversed = $bitReverseLookupTable[$code->numberValue];
  $code->numberValue = ShiftRight4Byte($reversed, 32.0 - 5.0);
}
function AppendBitsToBytesLeft(&$bytes, $nextbit, $data, $length){

  for(; $length > 0.0; ){
    $bytePos = Truncate($nextbit->numberValue/8.0);
    $bitPos = $nextbit->numberValue%8.0;

    if($length < 8.0 - $bitPos){
      $part = ShiftLeft4Byte($data, 8.0 - $bitPos - $length);

      $bytes[$bytePos] = Or4Byte($bytes[$bytePos], $part);

      $nextbit->numberValue = $nextbit->numberValue + $length;

      $length = 0.0;
    }else{
      $segment = 8.0 - $bitPos;

      $part = ShiftRight4Byte($data, $length - $segment);
      $bytes[$bytePos] = Or4Byte($bytes[$bytePos], $part);
      $nextbit->numberValue = $nextbit->numberValue + $segment;

      $remove = ShiftLeft4Byte($part, $length - $segment);
      $data = Xor4Byte($data, $remove);

      $length = $length - $segment;
    }
  }
}
function AppendBitsToBytesRight(&$bytes, $nextbit, $data, $length){

  for(; $length > 0.0; ){
    $bytePos = Truncate($nextbit->numberValue/8.0);
    $bitPos = $nextbit->numberValue%8.0;

    if($length < 8.0 - $bitPos){
      $part = ShiftLeft4Byte($data, $bitPos);

      $bytes[$bytePos] = Or4Byte($bytes[$bytePos], $part);

      $nextbit->numberValue = $nextbit->numberValue + $length;

      $length = 0.0;
    }else{
      $segment = 8.0 - $bitPos;

      $mask = 1.0;
      $mask = ShiftLeft4Byte($mask, $segment);
      $mask = $mask - 1.0;

      $part = And4Byte($mask, $data);
      $part = ShiftLeft4Byte($part, $bitPos);
      $bytes[$bytePos] = Or4Byte($bytes[$bytePos], $part);
      $nextbit->numberValue = $nextbit->numberValue + $segment;

      $data = ShiftRight4Byte($data, $segment);

      $length = $length - $segment;
    }
  }
}

