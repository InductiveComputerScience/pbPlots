// Downloaded from https://repo.progsbase.com - Code Developed Using progsbase.

function CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax){
  var x1, y1, x2, y2;
  var success, p1In, p2In;
  var dx, dy, f1, f2, f3, f4, f;

  x1 = x1Ref.numberValue;
  y1 = y1Ref.numberValue;
  x2 = x2Ref.numberValue;
  y2 = y2Ref.numberValue;

  p1In = x1 >= xMin && x1 <= xMax && y1 >= yMin && y1 <= yMax;
  p2In = x2 >= xMin && x2 <= xMax && y2 >= yMin && y2 <= yMax;

  if(p1In && p2In){
    success = true;
  }else if( !p1In  && p2In){
    dx = x1 - x2;
    dy = y1 - y2;

    if(dx != 0){
      f1 = (xMin - x2)/dx;
      f2 = (xMax - x2)/dx;
    }else{
      f1 = 1;
      f2 = 1;
    }
    if(dy != 0){
      f3 = (yMin - y2)/dy;
      f4 = (yMax - y2)/dy;
    }else{
      f3 = 1;
      f4 = 1;
    }

    if(f1 < 0){
      f1 = 1;
    }
    if(f2 < 0){
      f2 = 1;
    }
    if(f3 < 0){
      f3 = 1;
    }
    if(f4 < 0){
      f4 = 1;
    }

    f = Math.min(f1, Math.min(f2, Math.min(f3, f4)));

    x1 = x2 + f*dx;
    y1 = y2 + f*dy;

    success = true;
  }else if(p1In &&  !p2In ){
    dx = x2 - x1;
    dy = y2 - y1;

    if(dx != 0){
      f1 = (xMin - x1)/dx;
      f2 = (xMax - x1)/dx;
    }else{
      f1 = 1;
      f2 = 1;
    }
    if(dy != 0){
      f3 = (yMin - y1)/dy;
      f4 = (yMax - y1)/dy;
    }else{
      f3 = 1;
      f4 = 1;
    }

    if(f1 < 0){
      f1 = 1;
    }
    if(f2 < 0){
      f2 = 1;
    }
    if(f3 < 0){
      f3 = 1;
    }
    if(f4 < 0){
      f4 = 1;
    }

    f = Math.min(f1, Math.min(f2, Math.min(f3, f4)));

    x2 = x1 + f*dx;
    y2 = y1 + f*dy;

    success = true;
  }else{
    success = false;
  }

  x1Ref.numberValue = x1;
  y1Ref.numberValue = y1;
  x2Ref.numberValue = x2;
  y2Ref.numberValue = y2;

  return success;
}
function IncrementFromCoordinates(x1, y1, x2, y2){
  return (x2 - x1)/(y2 - y1);
}
function InterceptFromCoordinates(x1, y1, x2, y2){
  var a, b;

  a = IncrementFromCoordinates(x1, y1, x2, y2);
  b = y1 - a*x1;

  return b;
}
function Get8HighContrastColors(){
  var colors;
  colors = [];
  colors.length = 8;
  colors[0] = CreateRGBColor(3/256, 146/256, 206/256);
  colors[1] = CreateRGBColor(253/256, 83/256, 8/256);
  colors[2] = CreateRGBColor(102/256, 176/256, 50/256);
  colors[3] = CreateRGBColor(208/256, 234/256, 43/256);
  colors[4] = CreateRGBColor(167/256, 25/256, 75/256);
  colors[5] = CreateRGBColor(254/256, 254/256, 51/256);
  colors[6] = CreateRGBColor(134/256, 1/256, 175/256);
  colors[7] = CreateRGBColor(251/256, 153/256, 2/256);
  return colors;
}
function DrawFilledRectangleWithBorder(image, x, y, w, h, borderColor, fillColor){
  if(h > 0 && w > 0){
    DrawFilledRectangle(image, x, y, w, h, fillColor);
    DrawRectangle1px(image, x, y, w, h, borderColor);
  }
}
function CreateRGBABitmapImageReference(){
  var reference;

  reference = {};
  reference.image = {};
  reference.image.x = [];
  reference.image.x.length = 0;

  return reference;
}
function RectanglesOverlap(r1, r2){
  var overlap;

  overlap = false;

  overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2);
  overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2);
  overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2);
  overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2);

  return overlap;
}
function CreateRectangle(x1, y1, x2, y2){
  var r;
  r = {};
  r.x1 = x1;
  r.y1 = y1;
  r.x2 = x2;
  r.y2 = y2;
  return r;
}
function CopyRectangleValues(rd, rs){
  rd.x1 = rs.x1;
  rd.y1 = rs.y1;
  rd.x2 = rs.x2;
  rd.y2 = rs.y2;
}
function DrawXLabelsForPriority(p, xMin, oy, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied, textOnBottom){
  var overlap, currentOverlaps;
  var i, j, x, px, padding;
  var text;
  var r;

  r = {};
  padding = 10;

  overlap = false;
  for(i = 0; i < xLabels.stringArray.length; i = i + 1){
    if(xLabelPriorities.numberArray[i] == p){

      x = xGridPositions[i];
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
      text = xLabels.stringArray[i].string;

      r.x1 = Math.floor(px - GetTextWidth(text)/2);
      if(textOnBottom){
        r.y1 = Math.floor(oy + 5);
      }else{
        r.y1 = Math.floor(oy - 20);
      }
      r.x2 = r.x1 + GetTextWidth(text);
      r.y2 = r.y1 + GetTextHeight(text);

      /* Add padding */
      r.x1 = r.x1 - padding;
      r.y1 = r.y1 - padding;
      r.x2 = r.x2 + padding;
      r.y2 = r.y2 + padding;

      currentOverlaps = false;

      for(j = 0; j < nextRectangle.numberValue; j = j + 1){
        currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[j]);
      }

      if( !currentOverlaps  && p == 1){
        DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor);

        CopyRectangleValues(occupied[nextRectangle.numberValue], r);
        nextRectangle.numberValue = nextRectangle.numberValue + 1;
      }

      overlap = overlap || currentOverlaps;
    }
  }
  if( !overlap  && p != 1){
    for(i = 0; i < xGridPositions.length; i = i + 1){
      x = xGridPositions[i];
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);

      if(xLabelPriorities.numberArray[i] == p){
        text = xLabels.stringArray[i].string;

        r.x1 = Math.floor(px - GetTextWidth(text)/2);
        if(textOnBottom){
          r.y1 = Math.floor(oy + 5);
        }else{
          r.y1 = Math.floor(oy - 20);
        }
        r.x2 = r.x1 + GetTextWidth(text);
        r.y2 = r.y1 + GetTextHeight(text);

        DrawText(canvas, r.x1, r.y1, text, gridLabelColor);

        CopyRectangleValues(occupied[nextRectangle.numberValue], r);
        nextRectangle.numberValue = nextRectangle.numberValue + 1;
      }
    }
  }
}
function DrawYLabelsForPriority(p, yMin, ox, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, textOnLeft){
  var overlap, currentOverlaps;
  var i, j, y, py, padding;
  var text;
  var r;

  r = {};
  padding = 10;

  overlap = false;
  for(i = 0; i < yLabels.stringArray.length; i = i + 1){
    if(yLabelPriorities.numberArray[i] == p){

      y = yGridPositions[i];
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
      text = yLabels.stringArray[i].string;

      if(textOnLeft){
        r.x1 = Math.floor(ox - GetTextWidth(text) - 10);
      }else{
        r.x1 = Math.floor(ox + 10);
      }
      r.y1 = Math.floor(py - 6);
      r.x2 = r.x1 + GetTextWidth(text);
      r.y2 = r.y1 + GetTextHeight(text);

      /* Add padding */
      r.x1 = r.x1 - padding;
      r.y1 = r.y1 - padding;
      r.x2 = r.x2 + padding;
      r.y2 = r.y2 + padding;

      currentOverlaps = false;

      for(j = 0; j < nextRectangle.numberValue; j = j + 1){
        currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[j]);
      }

      /* Draw labels with priority 1 if they do not overlap anything else. */
      if( !currentOverlaps  && p == 1){
        DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor);

        CopyRectangleValues(occupied[nextRectangle.numberValue], r);
        nextRectangle.numberValue = nextRectangle.numberValue + 1;
      }

      overlap = overlap || currentOverlaps;
    }
  }
  if( !overlap  && p != 1){
    for(i = 0; i < yGridPositions.length; i = i + 1){
      y = yGridPositions[i];
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);

      if(yLabelPriorities.numberArray[i] == p){
        text = yLabels.stringArray[i].string;

        if(textOnLeft){
          r.x1 = Math.floor(ox - GetTextWidth(text) - 10);
        }else{
          r.x1 = Math.floor(ox + 10);
        }
        r.y1 = Math.floor(py - 6);
        r.x2 = r.x1 + GetTextWidth(text);
        r.y2 = r.y1 + GetTextHeight(text);

        DrawText(canvas, r.x1, r.y1, text, gridLabelColor);

        CopyRectangleValues(occupied[nextRectangle.numberValue], r);
        nextRectangle.numberValue = nextRectangle.numberValue + 1;
      }
    }
  }
}
function ComputeGridLinePositions(cMin, cMax, labels, priorities){
  var positions;
  var cLength, p, pMin, pMax, pInterval, pNum, i, num, rem, priority, mode;

  cLength = cMax - cMin;

  p = Math.floor(Math.log10(cLength));
  pInterval = Math.pow(10, p);
  /* gives 10-1 lines for 100-10 diff */
  pMin = Math.ceil(cMin/pInterval)*pInterval;
  pMax = Math.floor(cMax/pInterval)*pInterval;
  pNum = Round((pMax - pMin)/pInterval + 1);

  mode = 1;

  if(pNum <= 3){
    p = Math.floor(Math.log10(cLength) - 1);
    /* gives 100-10 lines for 100-10 diff */
    pInterval = Math.pow(10, p);
    pMin = Math.ceil(cMin/pInterval)*pInterval;
    pMax = Math.floor(cMax/pInterval)*pInterval;
    pNum = Round((pMax - pMin)/pInterval + 1);

    mode = 4;
  }else if(pNum <= 6){
    p = Math.floor(Math.log10(cLength));
    pInterval = Math.pow(10, p)/4;
    /* gives 40-5 lines for 100-10 diff */
    pMin = Math.ceil(cMin/pInterval)*pInterval;
    pMax = Math.floor(cMax/pInterval)*pInterval;
    pNum = Round((pMax - pMin)/pInterval + 1);

    mode = 3;
  }else if(pNum <= 10){
    p = Math.floor(Math.log10(cLength));
    pInterval = Math.pow(10, p)/2;
    /* gives 20-3 lines for 100-10 diff */
    pMin = Math.ceil(cMin/pInterval)*pInterval;
    pMax = Math.floor(cMax/pInterval)*pInterval;
    pNum = Round((pMax - pMin)/pInterval + 1);

    mode = 2;
  }

  positions = [];
  positions.length = pNum;
  labels.stringArray = [];
  labels.stringArray.length = pNum;
  priorities.numberArray = [];
  priorities.numberArray.length = pNum;

  for(i = 0; i < pNum; i = i + 1){
    num = pMin + pInterval*i;
    positions[i] = num;

    /* Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn. */
    priority = 1;

    /* Prioritize x.25, x.5 and x.75 lower. */
    if(mode == 2 || mode == 3){
      rem = Math.abs(Math.round(num/Math.pow(10, p - 2)))%100;

      priority = 1;
      if(rem == 50){
        priority = 2;
      }else if(rem == 25 || rem == 75){
        priority = 3;
      }
    }

    /* Prioritize x.1-x.4 and x.6-x.9 lower */
    if(mode == 4){
      rem = Math.abs(Round(num/Math.pow(10, p)))%10;

      priority = 1;
      if(rem == 1 || rem == 2 || rem == 3 || rem == 4 || rem == 6 || rem == 7 || rem == 8 || rem == 9){
        priority = 2;
      }
    }

    /* 0 has lowest priority. */
    if(EpsilonCompare(num, 0, Math.pow(10, p - 5))){
      priority = 3;
    }

    priorities.numberArray[i] = priority;

    /* The label itself. */
    labels.stringArray[i] = {};
    if(p < 0){
      if(mode == 2 || mode == 3){
        num = RoundToDigits(num,  -(p - 1));
      }else{
        num = RoundToDigits(num,  -p);
      }
    }
    labels.stringArray[i].string = CreateStringDecimalFromNumber(num);
  }

  return positions;
}
function MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax){
  var yLength, yPixelLength;

  yLength = yMax - yMin;
  yPixelLength = yPixelMax - yPixelMin;

  y = y - yMin;
  y = y*yPixelLength/yLength;
  y = yPixelLength - y;
  y = y + yPixelMin;
  return y;
}
function MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax){
  var xLength, xPixelLength;

  xLength = xMax - xMin;
  xPixelLength = xPixelMax - xPixelMin;

  x = x - xMin;
  x = x*xPixelLength/xLength;
  x = x + xPixelMin;
  return x;
}
function MapXCoordinateAutoSettings(x, image, xs){
  return MapXCoordinate(x, GetMinimum(xs), GetMaximum(xs), GetDefaultPaddingPercentage()*ImageWidth(image), (1 - GetDefaultPaddingPercentage())*ImageWidth(image));
}
function MapYCoordinateAutoSettings(y, image, ys){
  return MapYCoordinate(y, GetMinimum(ys), GetMaximum(ys), GetDefaultPaddingPercentage()*ImageHeight(image), (1 - GetDefaultPaddingPercentage())*ImageHeight(image));
}
function MapXCoordinateBasedOnSettings(x, settings){
  var xMin, xMax, xPadding, xPixelMin, xPixelMax;
  var boundaries;

  boundaries = {};
  ComputeBoundariesBasedOnSettings(settings, boundaries);
  xMin = boundaries.x1;
  xMax = boundaries.x2;

  if(settings.autoPadding){
    xPadding = Math.floor(GetDefaultPaddingPercentage()*settings.width);
  }else{
    xPadding = settings.xPadding;
  }

  xPixelMin = xPadding;
  xPixelMax = settings.width - xPadding;

  return MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
}
function MapYCoordinateBasedOnSettings(y, settings){
  var yMin, yMax, yPadding, yPixelMin, yPixelMax;
  var boundaries;

  boundaries = {};
  ComputeBoundariesBasedOnSettings(settings, boundaries);
  yMin = boundaries.y1;
  yMax = boundaries.y2;

  if(settings.autoPadding){
    yPadding = Math.floor(GetDefaultPaddingPercentage()*settings.height);
  }else{
    yPadding = settings.yPadding;
  }

  yPixelMin = yPadding;
  yPixelMax = settings.height - yPadding;

  return MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
}
function GetDefaultPaddingPercentage(){
  return 0.10;
}
function DrawText(canvas, x, y, text, color){
  var i, charWidth, spacing;

  charWidth = 8;
  spacing = 2;

  for(i = 0; i < text.length; i = i + 1){
    DrawAsciiCharacter(canvas, x + i*(charWidth + spacing), y, text[i], color);
  }
}
function DrawTextUpwards(canvas, x, y, text, color){
  var buffer, rotated;

  buffer = CreateImage(GetTextWidth(text), GetTextHeight(text), GetTransparent());
  DrawText(buffer, 0, 0, text, color);
  rotated = RotateAntiClockwise90Degrees(buffer);
  DrawImageOnImage(canvas, rotated, x, y);
  DeleteImage(buffer);
  DeleteImage(rotated);
}
function GetDefaultScatterPlotSettings(){
  var settings;

  settings = {};

  settings.autoBoundaries = true;
  settings.xMax = 0;
  settings.xMin = 0;
  settings.yMax = 0;
  settings.yMin = 0;
  settings.autoPadding = true;
  settings.xPadding = 0;
  settings.yPadding = 0;
  settings.title = "".split('');
  settings.xLabel = "".split('');
  settings.yLabel = "".split('');
  settings.scatterPlotSeries = [];
  settings.scatterPlotSeries.length = 0;
  settings.showGrid = true;
  settings.gridColor = GetGray(0.1);
  settings.xAxisAuto = true;
  settings.xAxisTop = false;
  settings.xAxisBottom = false;
  settings.yAxisAuto = true;
  settings.yAxisLeft = false;
  settings.yAxisRight = false;

  return settings;
}
function GetDefaultScatterPlotSeriesSettings(){
  var series;

  series = {};

  series.linearInterpolation = true;
  series.pointType = "pixels".split('');
  series.lineType = "solid".split('');
  series.lineThickness = 1;
  series.xs = [];
  series.xs.length = 0;
  series.ys = [];
  series.ys.length = 0;
  series.color = GetBlack();

  return series;
}
function DrawScatterPlot(canvasReference, width, height, xs, ys){
  var settings;

  settings = GetDefaultScatterPlotSettings();

  settings.width = width;
  settings.height = height;
  settings.scatterPlotSeries = [];
  settings.scatterPlotSeries.length = 1;
  settings.scatterPlotSeries[0] = GetDefaultScatterPlotSeriesSettings();
  delete(settings.scatterPlotSeries[0].xs);
  settings.scatterPlotSeries[0].xs = xs;
  delete(settings.scatterPlotSeries[0].ys);
  settings.scatterPlotSeries[0].ys = ys;

  DrawScatterPlotFromSettings(canvasReference, settings);
}
function DrawScatterPlotFromSettings(canvasReference, settings){
  var xMin, xMax, yMin, yMax, xLength, yLength, i, x, y, xPrev, yPrev, px, py, pxPrev, pyPrev, originX, originY, p, l, plot;
  var boundaries;
  var xPadding, yPadding, originXPixels, originYPixels;
  var xPixelMin, yPixelMin, xPixelMax, yPixelMax, xLengthPixels, yLengthPixels, axisLabelPadding;
  var nextRectangle, x1Ref, y1Ref, x2Ref, y2Ref, patternOffset;
  var prevSet, success;
  var gridLabelColor;
  var canvas;
  var xs, ys;
  var linearInterpolation;
  var sp;
  var xGridPositions, yGridPositions;
  var xLabels, yLabels;
  var xLabelPriorities, yLabelPriorities;
  var occupied;
  var linePattern;
  var originXInside, originYInside, textOnLeft, textOnBottom;
  var originTextX, originTextY, originTextXPixels, originTextYPixels, side;

  canvas = CreateImage(settings.width, settings.height, GetWhite());
  patternOffset = CreateNumberReference(0);

  success = ScatterPlotFromSettingsValid(settings);

  if(success){

    boundaries = {};
    ComputeBoundariesBasedOnSettings(settings, boundaries);
    xMin = boundaries.x1;
    yMin = boundaries.y1;
    xMax = boundaries.x2;
    yMax = boundaries.y2;

    xLength = xMax - xMin;
    yLength = yMax - yMin;

    if(settings.autoPadding){
      xPadding = Math.floor(GetDefaultPaddingPercentage()*settings.width);
      yPadding = Math.floor(GetDefaultPaddingPercentage()*settings.height);
    }else{
      xPadding = settings.xPadding;
      yPadding = settings.yPadding;
    }

    /* Draw title */
    DrawText(canvas, Math.floor(settings.width/2 - GetTextWidth(settings.title)/2), Math.floor(yPadding/3), settings.title, GetBlack());

    /* Draw grid */
    xPixelMin = xPadding;
    yPixelMin = yPadding;
    xPixelMax = settings.width - xPadding;
    yPixelMax = settings.height - yPadding;
    xLengthPixels = xPixelMax - xPixelMin;
    yLengthPixels = yPixelMax - yPixelMin;
    DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor);

    gridLabelColor = GetGray(0.5);

    xLabels = {};
    xLabelPriorities = {};
    yLabels = {};
    yLabelPriorities = {};
    xGridPositions = ComputeGridLinePositions(xMin, xMax, xLabels, xLabelPriorities);
    yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities);

    if(settings.showGrid){
      /* X-grid */
      for(i = 0; i < xGridPositions.length; i = i + 1){
        x = xGridPositions[i];
        px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
        DrawLine1px(canvas, px, yPixelMin, px, yPixelMax, settings.gridColor);
      }

      /* Y-grid */
      for(i = 0; i < yGridPositions.length; i = i + 1){
        y = yGridPositions[i];
        py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
        DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
      }
    }

    /* Compute origin information. */
    originYInside = yMin < 0 && yMax > 0;
    originY = 0;
    if(settings.xAxisAuto){
      if(originYInside){
        originY = 0;
      }else{
        originY = yMin;
      }
    }else{
if(settings.xAxisTop){
        originY = yMax;
      }
      if(settings.xAxisBottom){
        originY = yMin;
      }
    }
    originYPixels = MapYCoordinate(originY, yMin, yMax, yPixelMin, yPixelMax);

    originXInside = xMin < 0 && xMax > 0;
    originX = 0;
    if(settings.yAxisAuto){
      if(originXInside){
        originX = 0;
      }else{
        originX = xMin;
      }
    }else{
if(settings.yAxisLeft){
        originX = xMin;
      }
      if(settings.yAxisRight){
        originX = xMax;
      }
    }
    originXPixels = MapXCoordinate(originX, xMin, xMax, xPixelMin, xPixelMax);

    if(originYInside){
      originTextY = 0;
    }else{
      originTextY = yMin + yLength/2;
    }
    originTextYPixels = MapYCoordinate(originTextY, yMin, yMax, yPixelMin, yPixelMax);

    if(originXInside){
      originTextX = 0;
    }else{
      originTextX = xMin + xLength/2;
    }
    originTextXPixels = MapXCoordinate(originTextX, xMin, xMax, xPixelMin, xPixelMax);

    /* Labels */
    occupied = [];
    occupied.length = xLabels.stringArray.length + yLabels.stringArray.length;
    for(i = 0; i < occupied.length; i = i + 1){
      occupied[i] = CreateRectangle(0, 0, 0, 0);
    }
    nextRectangle = CreateNumberReference(0);

    /* x labels */
    for(i = 1; i <= 5; i = i + 1){
      textOnBottom = true;
      if( !settings.xAxisAuto  && settings.xAxisTop){
        textOnBottom = false;
      }
      DrawXLabelsForPriority(i, xMin, originYPixels, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied, textOnBottom);
    }

    /* y labels */
    for(i = 1; i <= 5; i = i + 1){
      textOnLeft = true;
      if( !settings.yAxisAuto  && settings.yAxisRight){
        textOnLeft = false;
      }
      DrawYLabelsForPriority(i, yMin, originXPixels, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, textOnLeft);
    }

    /* Draw origin line axis titles. */
    axisLabelPadding = 20;

    /* x origin line */
    if(originYInside){
      DrawLine1px(canvas, Round(xPixelMin), Round(originYPixels), Round(xPixelMax), Round(originYPixels), GetBlack());
    }

    /* y origin line */
    if(originXInside){
      DrawLine1px(canvas, Round(originXPixels), Round(yPixelMin), Round(originXPixels), Round(yPixelMax), GetBlack());
    }

    /* Draw origin axis titles. */
    DrawTextUpwards(canvas, 10, Math.floor(originTextYPixels - GetTextWidth(settings.yLabel)/2), settings.yLabel, GetBlack());
    DrawText(canvas, Math.floor(originTextXPixels - GetTextWidth(settings.xLabel)/2), yPixelMax + axisLabelPadding, settings.xLabel, GetBlack());

    /* X-grid-markers */
    for(i = 0; i < xGridPositions.length; i = i + 1){
      x = xGridPositions[i];
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
      p = xLabelPriorities.numberArray[i];
      l = 1;
      if(p == 1){
        l = 8;
      }else if(p == 2){
        l = 3;
      }
      side =  -1;
      if( !settings.xAxisAuto  && settings.xAxisTop){
        side = 1;
      }
      DrawLine1px(canvas, px, originYPixels, px, originYPixels + side*l, GetBlack());
    }

    /* Y-grid-markers */
    for(i = 0; i < yGridPositions.length; i = i + 1){
      y = yGridPositions[i];
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
      p = yLabelPriorities.numberArray[i];
      l = 1;
      if(p == 1){
        l = 8;
      }else if(p == 2){
        l = 3;
      }
      side = 1;
      if( !settings.yAxisAuto  && settings.yAxisRight){
        side =  -1;
      }
      DrawLine1px(canvas, originXPixels, py, originXPixels + side*l, py, GetBlack());
    }

    /* Draw points */
    for(plot = 0; plot < settings.scatterPlotSeries.length; plot = plot + 1){
      sp = settings.scatterPlotSeries[plot];

      xs = sp.xs;
      ys = sp.ys;
      linearInterpolation = sp.linearInterpolation;

      x1Ref = {};
      y1Ref = {};
      x2Ref = {};
      y2Ref = {};
      if(linearInterpolation){
        prevSet = false;
        xPrev = 0;
        yPrev = 0;
        for(i = 0; i < xs.length; i = i + 1){
          x = xs[i];
          y = ys[i];

          if(prevSet){
            x1Ref.numberValue = xPrev;
            y1Ref.numberValue = yPrev;
            x2Ref.numberValue = x;
            y2Ref.numberValue = y;

            success = CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax);

            if(success){
              pxPrev = Math.floor(MapXCoordinate(x1Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax));
              pyPrev = Math.floor(MapYCoordinate(y1Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax));
              px = Math.floor(MapXCoordinate(x2Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax));
              py = Math.floor(MapYCoordinate(y2Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax));

              if(aStringsEqual(sp.lineType, "solid".split('')) && sp.lineThickness == 1){
                DrawLine1px(canvas, pxPrev, pyPrev, px, py, sp.color);
              }else if(aStringsEqual(sp.lineType, "solid".split(''))){
                DrawLine(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, sp.color);
              }else if(aStringsEqual(sp.lineType, "dashed".split(''))){
                linePattern = GetLinePattern1();
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
              }else if(aStringsEqual(sp.lineType, "dotted".split(''))){
                linePattern = GetLinePattern2();
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
              }else if(aStringsEqual(sp.lineType, "dotdash".split(''))){
                linePattern = GetLinePattern3();
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
              }else if(aStringsEqual(sp.lineType, "longdash".split(''))){
                linePattern = GetLinePattern4();
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
              }else if(aStringsEqual(sp.lineType, "twodash".split(''))){
                linePattern = GetLinePattern5();
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
              }
            }
          }

          prevSet = true;
          xPrev = x;
          yPrev = y;
        }
      }else{
        for(i = 0; i < xs.length; i = i + 1){
          x = xs[i];
          y = ys[i];

          if(x > xMin && x < xMax && y > yMin && y < yMax){

            x = Math.floor(MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax));
            y = Math.floor(MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax));

            if(aStringsEqual(sp.pointType, "crosses".split(''))){
              DrawPixel(canvas, x, y, sp.color);
              DrawPixel(canvas, x + 1, y, sp.color);
              DrawPixel(canvas, x + 2, y, sp.color);
              DrawPixel(canvas, x - 1, y, sp.color);
              DrawPixel(canvas, x - 2, y, sp.color);
              DrawPixel(canvas, x, y + 1, sp.color);
              DrawPixel(canvas, x, y + 2, sp.color);
              DrawPixel(canvas, x, y - 1, sp.color);
              DrawPixel(canvas, x, y - 2, sp.color);
            }else if(aStringsEqual(sp.pointType, "circles".split(''))){
              DrawCircle(canvas, x, y, 3, sp.color);
            }else if(aStringsEqual(sp.pointType, "dots".split(''))){
              DrawFilledCircle(canvas, x, y, 3, sp.color);
            }else if(aStringsEqual(sp.pointType, "triangles".split(''))){
              DrawTriangle(canvas, x, y, 3, sp.color);
            }else if(aStringsEqual(sp.pointType, "filled triangles".split(''))){
              DrawFilledTriangle(canvas, x, y, 3, sp.color);
            }else if(aStringsEqual(sp.pointType, "pixels".split(''))){
              DrawPixel(canvas, x, y, sp.color);
            }
          }
        }
      }
    }

    DeleteImage(canvasReference.image);
    canvasReference.image = canvas;
  }

  return success;
}
function ComputeBoundariesBasedOnSettings(settings, boundaries){
  var sp;
  var plot, xMin, xMax, yMin, yMax;

  if(settings.scatterPlotSeries.length >= 1){
    xMin = GetMinimum(settings.scatterPlotSeries[0].xs);
    xMax = GetMaximum(settings.scatterPlotSeries[0].xs);
    yMin = GetMinimum(settings.scatterPlotSeries[0].ys);
    yMax = GetMaximum(settings.scatterPlotSeries[0].ys);
  }else{
    xMin =  -10;
    xMax = 10;
    yMin =  -10;
    yMax = 10;
  }

  if( !settings.autoBoundaries ){
    xMin = settings.xMin;
    xMax = settings.xMax;
    yMin = settings.yMin;
    yMax = settings.yMax;
  }else{
    for(plot = 1; plot < settings.scatterPlotSeries.length; plot = plot + 1){
      sp = settings.scatterPlotSeries[plot];

      xMin = Math.min(xMin, GetMinimum(sp.xs));
      xMax = Math.max(xMax, GetMaximum(sp.xs));
      yMin = Math.min(yMin, GetMinimum(sp.ys));
      yMax = Math.max(yMax, GetMaximum(sp.ys));
    }
  }

  boundaries.x1 = xMin;
  boundaries.y1 = yMin;
  boundaries.x2 = xMax;
  boundaries.y2 = yMax;
}
function ScatterPlotFromSettingsValid(settings){
  var success, found;
  var series;
  var i;

  success = true;

  /* Check axis placement. */
  if( !settings.xAxisAuto ){
    if(settings.xAxisTop && settings.xAxisBottom){
      success = false;
    }
    if( !settings.xAxisTop  &&  !settings.xAxisBottom ){
      success = false;
    }
  }

  if( !settings.yAxisAuto ){
    if(settings.yAxisLeft && settings.yAxisRight){
      success = false;
    }
    if( !settings.yAxisLeft  &&  !settings.yAxisRight ){
      success = false;
    }
  }

  /* Check series lengths. */
  for(i = 0; i < settings.scatterPlotSeries.length; i = i + 1){
    series = settings.scatterPlotSeries[i];
    if(series.xs.length != series.ys.length){
      success = false;
    }
    if(series.xs.length == 0){
      success = false;
    }
    if(series.linearInterpolation && series.xs.length == 1){
      success = false;
    }
  }

  /* Check bounds. */
  if( !settings.autoBoundaries ){
    if(settings.xMin >= settings.xMax){
      success = false;
    }
    if(settings.yMin >= settings.yMax){
      success = false;
    }
  }

  /* Check padding. */
  if( !settings.autoPadding ){
    if(2*settings.xPadding >= settings.width){
      success = false;
    }
    if(2*settings.yPadding >= settings.height){
      success = false;
    }
  }

  /* Check width and height. */
  if(settings.width < 0){
    success = false;
  }
  if(settings.height < 0){
    success = false;
  }

  /* Check point types. */
  for(i = 0; i < settings.scatterPlotSeries.length; i = i + 1){
    series = settings.scatterPlotSeries[i];

    if(series.lineThickness < 0){
      success = false;
    }

    if( !series.linearInterpolation ){
      /* Point type. */
      found = false;
      if(aStringsEqual(series.pointType, "crosses".split(''))){
        found = true;
      }else if(aStringsEqual(series.pointType, "circles".split(''))){
        found = true;
      }else if(aStringsEqual(series.pointType, "dots".split(''))){
        found = true;
      }else if(aStringsEqual(series.pointType, "triangles".split(''))){
        found = true;
      }else if(aStringsEqual(series.pointType, "filled triangles".split(''))){
        found = true;
      }else if(aStringsEqual(series.pointType, "pixels".split(''))){
        found = true;
      }
      if( !found ){
        success = false;
      }
    }else{
      /* Line type. */
      found = false;
      if(aStringsEqual(series.lineType, "solid".split(''))){
        found = true;
      }else if(aStringsEqual(series.lineType, "dashed".split(''))){
        found = true;
      }else if(aStringsEqual(series.lineType, "dotted".split(''))){
        found = true;
      }else if(aStringsEqual(series.lineType, "dotdash".split(''))){
        found = true;
      }else if(aStringsEqual(series.lineType, "longdash".split(''))){
        found = true;
      }else if(aStringsEqual(series.lineType, "twodash".split(''))){
        found = true;
      }

      if( !found ){
        success = false;
      }
    }
  }

  return success;
}
function GetDefaultBarPlotSettings(){
  var settings;

  settings = {};

  settings.width = 800;
  settings.height = 600;
  settings.autoBoundaries = true;
  settings.yMax = 0;
  settings.yMin = 0;
  settings.autoPadding = true;
  settings.xPadding = 0;
  settings.yPadding = 0;
  settings.title = "".split('');
  settings.yLabel = "".split('');
  settings.barPlotSeries = [];
  settings.barPlotSeries.length = 0;
  settings.showGrid = true;
  settings.gridColor = GetGray(0.1);
  settings.autoColor = true;
  settings.grayscaleAutoColor = false;
  settings.autoSpacing = true;
  settings.groupSeparation = 0;
  settings.barSeparation = 0;
  settings.autoLabels = true;
  settings.xLabels = [];
  settings.xLabels.length = 0;
  /*settings.autoLabels = false;
        settings.xLabels = new StringReference [5];
        settings.xLabels[0] = CreateStringReference("may 20".toCharArray());
        settings.xLabels[1] = CreateStringReference("jun 20".toCharArray());
        settings.xLabels[2] = CreateStringReference("jul 20".toCharArray());
        settings.xLabels[3] = CreateStringReference("aug 20".toCharArray());
        settings.xLabels[4] = CreateStringReference("sep 20".toCharArray()); */
  settings.barBorder = false;

  return settings;
}
function GetDefaultBarPlotSeriesSettings(){
  var series;

  series = {};

  series.ys = [];
  series.ys.length = 0;
  series.color = GetBlack();

  return series;
}
function DrawBarPlot(width, height, ys){
  var settings;
  var canvasReference;

  settings = GetDefaultBarPlotSettings();

  settings.barPlotSeries = [];
  settings.barPlotSeries.length = 1;
  settings.barPlotSeries[0] = GetDefaultBarPlotSeriesSettings();
  delete(settings.barPlotSeries[0].ys);
  settings.barPlotSeries[0].ys = ys;
  canvasReference = {};
  settings.width = width;
  settings.height = height;

  DrawBarPlotFromSettings(canvasReference, settings);

  return canvasReference.image;
}
function DrawBarPlotFromSettings(canvasReference, settings){
  var xPadding, yPadding;
  var xPixelMin, yPixelMin, yPixelMax, xPixelMax;
  var xLengthPixels, yLengthPixels;
  var s, n, y, x, w, h, yMin, yMax, b, i, py, yValue;
  var colors;
  var ys, yGridPositions;
  var yTop, yBottom, ss, bs, yLength;
  var groupSeparation, barSeparation, barWidth, textwidth;
  var yLabels;
  var yLabelPriorities;
  var occupied;
  var nextRectangle;
  var gridLabelColor, barColor;
  var label;
  var success;
  var canvas;

  success = BarPlotSettingsIsValid(settings);

  if(success){

    canvas = CreateImage(settings.width, settings.height, GetWhite());

    ss = settings.barPlotSeries.length;
    gridLabelColor = GetGray(0.5);

    /* padding */
    if(settings.autoPadding){
      xPadding = Math.floor(GetDefaultPaddingPercentage()*ImageWidth(canvas));
      yPadding = Math.floor(GetDefaultPaddingPercentage()*ImageHeight(canvas));
    }else{
      xPadding = settings.xPadding;
      yPadding = settings.yPadding;
    }

    /* Draw title */
    DrawText(canvas, Math.floor(ImageWidth(canvas)/2 - GetTextWidth(settings.title)/2), Math.floor(yPadding/3), settings.title, GetBlack());
    DrawTextUpwards(canvas, 10, Math.floor(ImageHeight(canvas)/2 - GetTextWidth(settings.yLabel)/2), settings.yLabel, GetBlack());

    /* min and max */
    if(settings.autoBoundaries){
      if(ss >= 1){
        yMax = GetMaximum(settings.barPlotSeries[0].ys);
        yMin = Math.min(0, GetMinimum(settings.barPlotSeries[0].ys));

        for(s = 0; s < ss; s = s + 1){
          yMax = Math.max(yMax, GetMaximum(settings.barPlotSeries[s].ys));
          yMin = Math.min(yMin, GetMinimum(settings.barPlotSeries[s].ys));
        }
      }else{
        yMax = 10;
        yMin = 0;
      }
    }else{
      yMin = settings.yMin;
      yMax = settings.yMax;
    }
    yLength = yMax - yMin;

    /* boundaries */
    xPixelMin = xPadding;
    yPixelMin = yPadding;
    xPixelMax = ImageWidth(canvas) - xPadding;
    yPixelMax = ImageHeight(canvas) - yPadding;
    xLengthPixels = xPixelMax - xPixelMin;
    yLengthPixels = yPixelMax - yPixelMin;

    /* Draw boundary. */
    DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor);

    /* Draw grid lines. */
    yLabels = {};
    yLabelPriorities = {};
    yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities);

    if(settings.showGrid){
      /* Y-grid */
      for(i = 0; i < yGridPositions.length; i = i + 1){
        y = yGridPositions[i];
        py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
        DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
      }
    }

    /* Draw origin. */
    if(yMin < 0 && yMax > 0){
      py = MapYCoordinate(0, yMin, yMax, yPixelMin, yPixelMax);
      DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
    }

    /* Labels */
    occupied = [];
    occupied.length = yLabels.stringArray.length;
    for(i = 0; i < occupied.length; i = i + 1){
      occupied[i] = CreateRectangle(0, 0, 0, 0);
    }
    nextRectangle = CreateNumberReference(0);

    for(i = 1; i <= 5; i = i + 1){
      DrawYLabelsForPriority(i, yMin, xPixelMin, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, true);
    }

    /* Draw bars. */
    if(settings.autoColor){
      if( !settings.grayscaleAutoColor ){
        colors = Get8HighContrastColors();
      }else{
        colors = [];
        colors.length = ss;
        if(ss > 1){
          for(i = 0; i < ss; i = i + 1){
            colors[i] = GetGray(0.7 - (i/ss)*0.7);
          }
        }else{
          colors[0] = GetGray(0.5);
        }
      }
    }else{
      colors = [];
      colors.length = 0;
    }

    /* distances */
    bs = settings.barPlotSeries[0].ys.length;

    if(settings.autoSpacing){
      groupSeparation = ImageWidth(canvas)*0.05;
      barSeparation = ImageWidth(canvas)*0.005;
    }else{
      groupSeparation = settings.groupSeparation;
      barSeparation = settings.barSeparation;
    }

    barWidth = (xLengthPixels - groupSeparation*(bs - 1) - barSeparation*(bs*(ss - 1)))/(bs*ss);

    /* Draw bars. */
    b = 0;
    for(n = 0; n < bs; n = n + 1){
      for(s = 0; s < ss; s = s + 1){
        ys = settings.barPlotSeries[s].ys;

        yValue = ys[n];

        yBottom = MapYCoordinate(yValue, yMin, yMax, yPixelMin, yPixelMax);
        yTop = MapYCoordinate(0, yMin, yMax, yPixelMin, yPixelMax);

        x = xPixelMin + n*(groupSeparation + ss*barWidth) + s*(barWidth) + b*barSeparation;
        w = barWidth;

        if(yValue >= 0){
          y = yBottom;
          h = yTop - y;
        }else{
          y = yTop;
          h = yBottom - yTop;
        }

        /* Cut at boundaries. */
        if(y < yPixelMin && y + h > yPixelMax){
          y = yPixelMin;
          h = yPixelMax - yPixelMin;
        }else if(y < yPixelMin){
          y = yPixelMin;
          if(yValue >= 0){
            h = yTop - y;
          }else{
            h = yBottom - y;
          }
        }else if(y + h > yPixelMax){
          h = yPixelMax - y;
        }

        /* Get color */
        if(settings.autoColor){
          barColor = colors[s];
        }else{
          barColor = settings.barPlotSeries[s].color;
        }

        /* Draw */
        if(settings.barBorder){
          DrawFilledRectangleWithBorder(canvas, Round(x), Round(y), Round(w), Round(h), GetBlack(), barColor);
        }else{
          DrawFilledRectangle(canvas, Round(x), Round(y), Round(w), Round(h), barColor);
        }

        b = b + 1;
      }
      b = b - 1;
    }

    /* x-labels */
    for(n = 0; n < bs; n = n + 1){
      if(settings.autoLabels){
        label = CreateStringDecimalFromNumber(n + 1);
      }else{
        label = settings.xLabels[n].string;
      }

      textwidth = GetTextWidth(label);

      x = xPixelMin + (n + 0.5)*(ss*barWidth + (ss - 1)*barSeparation) + n*groupSeparation - textwidth/2;

      DrawText(canvas, Math.floor(x), ImageHeight(canvas) - yPadding + 20, label, gridLabelColor);

      b = b + 1;
    }

    canvasReference.image = canvas;
  }

  return success;
}
function BarPlotSettingsIsValid(settings){
  var success, lengthSet;
  var series;
  var i, width, height, length;

  success = true;

  /* Check series lengths. */
  lengthSet = false;
  length = 0;
  for(i = 0; i < settings.barPlotSeries.length; i = i + 1){
    series = settings.barPlotSeries[i];

    if( !lengthSet ){
      length = series.ys.length;
      lengthSet = true;
    }else if(length != series.ys.length){
      success = false;
    }
  }

  /* Check bounds. */
  if( !settings.autoBoundaries ){
    if(settings.yMin >= settings.yMax){
      success = false;
    }
  }

  /* Check padding. */
  if( !settings.autoPadding ){
    if(2*settings.xPadding >= settings.width){
      success = false;
    }
    if(2*settings.yPadding >= settings.height){
      success = false;
    }
  }

  /* Check width and height. */
  if(settings.width < 0){
    success = false;
  }
  if(settings.height < 0){
    success = false;
  }

  /* Check spacing */
  if( !settings.autoSpacing ){
    if(settings.groupSeparation < 0){
      success = false;
    }
    if(settings.barSeparation < 0){
      success = false;
    }
  }

  return success;
}
function GetMinimum(data){
  var i, minimum;

  minimum = data[0];
  for(i = 0; i < data.length; i = i + 1){
    minimum = Math.min(minimum, data[i]);
  }

  return minimum;
}
function GetMaximum(data){
  var i, maximum;

  maximum = data[0];
  for(i = 0; i < data.length; i = i + 1){
    maximum = Math.max(maximum, data[i]);
  }

  return maximum;
}
function RoundToDigits(element, digitsAfterPoint){
  return Round(element*Math.pow(10, digitsAfterPoint))/Math.pow(10, digitsAfterPoint);
}
function test(){
  var z;
  var gridlines;
  var failures;
  var labels;
  var labelPriorities;
  var imageReference;
  var xs, ys;

  failures = CreateNumberReference(0);

  imageReference = CreateRGBABitmapImageReference();

  labels = {};
  labelPriorities = {};

  z = 10;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 11, failures);

  z = 9;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 19, failures);

  z = 8;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 17, failures);

  z = 7;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 15, failures);

  z = 6;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 13, failures);

  z = 5;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 21, failures);

  z = 4;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 17, failures);

  z = 3;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 31, failures);

  z = 2;
  gridlines = ComputeGridLinePositions( -z/2, z/2, labels, labelPriorities);
  AssertEquals(gridlines.length, 21, failures);

  xs = [];
  xs.length = 5;
  xs[0] =  -2;
  xs[1] =  -1;
  xs[2] = 0;
  xs[3] = 1;
  xs[4] = 2;
  ys = [];
  ys.length = 5;
  ys[0] = 2;
  ys[1] =  -1;
  ys[2] =  -2;
  ys[3] =  -1;
  ys[4] = 2;
  DrawScatterPlot(imageReference, 800, 600, xs, ys);

  imageReference.image = DrawBarPlot(800, 600, ys);

  TestMapping(failures);
  TestMapping2(failures);

  return failures.numberValue;
}
function TestMapping(failures){
  var series;
  var settings;
  var imageReference;
  var x1, y1;

  series = GetDefaultScatterPlotSeriesSettings();

  series.xs = [];
  series.xs.length = 5;
  series.xs[0] = -2;
  series.xs[1] = -1;
  series.xs[2] = 0;
  series.xs[3] = 1;
  series.xs[4] = 2;
  series.ys = [];
  series.ys.length = 5;
  series.ys[0] = -2;
  series.ys[1] = -1;
  series.ys[2] = -2;
  series.ys[3] = -1;
  series.ys[4] = 2;
  series.linearInterpolation = true;
  series.lineType = "dashed".split('');
  series.lineThickness = 2;
  series.color = GetGray(0.3);

  settings = GetDefaultScatterPlotSettings();
  settings.width = 600;
  settings.height = 400;
  settings.autoBoundaries = true;
  settings.autoPadding = true;
  settings.title = "x^2 - 2".split('');
  settings.xLabel = "X axis".split('');
  settings.yLabel = "Y axis".split('');
  settings.scatterPlotSeries = [];
  settings.scatterPlotSeries.length = 1;
  settings.scatterPlotSeries[0] = series;

  imageReference = CreateRGBABitmapImageReference();
  DrawScatterPlotFromSettings(imageReference, settings);

  x1 = MapXCoordinateAutoSettings( -1, imageReference.image, series.xs);
  y1 = MapYCoordinateAutoSettings( -1, imageReference.image, series.ys);

  AssertEquals(x1, 180, failures);
  AssertEquals(y1, 280, failures);
}
function TestMapping2(failures){
  var xs, ys, xs2, ys2;
  var i, x, y, w, h, xMin, xMax, yMin, yMax;
  var canvasReference;
  var settings;
  var points;
  var x1, y1;

  points = 300;
  w = 600*2;
  h = 300*2;
  xMin = 0;
  xMax = 150;
  yMin = 0;
  yMax = 1;

  xs = [];
  xs.length = points;
  ys = [];
  ys.length = points;
  xs2 = [];
  xs2.length = points;
  ys2 = [];
  ys2.length = points;

  for(i = 0; i < points; i = i + 1){
    x = xMin + (xMax - xMin)/(points - 1)*i;
    /* points - 1d is to ensure both extremeties are included. */
    y = x/(x + 7);

    xs[i] = x;
    ys[i] = y;

    y = 1.4*x/(x + 7)*(1 - (Math.atan((x/1.5 - 30)/5)/1.6 + 1)/2);

    xs2[i] = x;
    ys2[i] = y;
  }

  settings = GetDefaultScatterPlotSettings();

  settings.scatterPlotSeries = [];
  settings.scatterPlotSeries.length = 2;
  settings.scatterPlotSeries[0] = {};
  settings.scatterPlotSeries[0].xs = xs;
  settings.scatterPlotSeries[0].ys = ys;
  settings.scatterPlotSeries[0].linearInterpolation = true;
  settings.scatterPlotSeries[0].lineType = "solid".split('');
  settings.scatterPlotSeries[0].lineThickness = 3;
  settings.scatterPlotSeries[0].color = CreateRGBColor(1, 0, 0);
  settings.scatterPlotSeries[1] = {};
  settings.scatterPlotSeries[1].xs = xs2;
  settings.scatterPlotSeries[1].ys = ys2;
  settings.scatterPlotSeries[1].linearInterpolation = true;
  settings.scatterPlotSeries[1].lineType = "solid".split('');
  settings.scatterPlotSeries[1].lineThickness = 3;
  settings.scatterPlotSeries[1].color = CreateRGBColor(0, 0, 1);
  settings.autoBoundaries = false;
  settings.xMin = xMin;
  settings.xMax = xMax;
  settings.yMin = yMin;
  settings.yMax = yMax;
  settings.yLabel = "".split('');
  settings.xLabel = "Features".split('');
  settings.title = "".split('');
  settings.width = w;
  settings.height = h;

  canvasReference = CreateRGBABitmapImageReference();

  DrawScatterPlotFromSettings(canvasReference, settings);

  x1 = MapXCoordinateBasedOnSettings(27, settings);
  y1 = MapYCoordinateBasedOnSettings(1, settings);

  AssertEquals(Math.floor(x1), 292, failures);
  AssertEquals(y1, 60, failures);
}
function GetBlack(){
  var black;
  black = {};
  black.a = 1;
  black.r = 0;
  black.g = 0;
  black.b = 0;
  return black;
}
function GetWhite(){
  var white;
  white = {};
  white.a = 1;
  white.r = 1;
  white.g = 1;
  white.b = 1;
  return white;
}
function GetTransparent(){
  var transparent;
  transparent = {};
  transparent.a = 0;
  transparent.r = 0;
  transparent.g = 0;
  transparent.b = 0;
  return transparent;
}
function GetGray(percentage){
  var black;
  black = {};
  black.a = 1;
  black.r = 1 - percentage;
  black.g = 1 - percentage;
  black.b = 1 - percentage;
  return black;
}
function CreateRGBColor(r, g, b){
  var color;
  color = {};
  color.a = 1;
  color.r = r;
  color.g = g;
  color.b = b;
  return color;
}
function CreateRGBAColor(r, g, b, a){
  var color;
  color = {};
  color.a = a;
  color.r = r;
  color.g = g;
  color.b = b;
  return color;
}
function CreateImage(w, h, color){
  var image;
  var i, j;

  image = {};
  image.x = [];
  image.x.length = w;
  for(i = 0; i < w; i = i + 1){
    image.x[i] = {};
    image.x[i].y = [];
    image.x[i].y.length = h;
    for(j = 0; j < h; j = j + 1){
      image.x[i].y[j] = {};
      SetPixel(image, i, j, color);
    }
  }

  return image;
}
function DeleteImage(image){
  var i, j, w, h;

  w = ImageWidth(image);
  h = ImageHeight(image);

  for(i = 0; i < w; i = i + 1){
    for(j = 0; j < h; j = j + 1){
      delete(image.x[i].y[j]);
    }
    delete(image.x[i]);
  }
  delete(image);
}
function ImageWidth(image){
  return image.x.length;
}
function ImageHeight(image){
  var height;

  if(ImageWidth(image) == 0){
    height = 0;
  }else{
    height = image.x[0].y.length;
  }

  return height;
}
function SetPixel(image, x, y, color){
  if(x >= 0 && x < ImageWidth(image) && y >= 0 && y < ImageHeight(image)){
    image.x[x].y[y].a = color.a;
    image.x[x].y[y].r = color.r;
    image.x[x].y[y].g = color.g;
    image.x[x].y[y].b = color.b;
  }
}
function DrawPixel(image, x, y, color){
  var ra, ga, ba, aa;
  var rb, gb, bb, ab;
  var ro, go, bo, ao;

  if(x >= 0 && x < ImageWidth(image) && y >= 0 && y < ImageHeight(image)){
    ra = color.r;
    ga = color.g;
    ba = color.b;
    aa = color.a;

    rb = image.x[x].y[y].r;
    gb = image.x[x].y[y].g;
    bb = image.x[x].y[y].b;
    ab = image.x[x].y[y].a;

    ao = CombineAlpha(aa, ab);

    ro = AlphaBlend(ra, aa, rb, ab, ao);
    go = AlphaBlend(ga, aa, gb, ab, ao);
    bo = AlphaBlend(ba, aa, bb, ab, ao);

    image.x[x].y[y].r = ro;
    image.x[x].y[y].g = go;
    image.x[x].y[y].b = bo;
    image.x[x].y[y].a = ao;
  }
}
function CombineAlpha(as, ad){
  return as + ad*(1 - as);
}
function AlphaBlend(cs, as, cd, ad, ao){
  return (cs*as + cd*ad*(1 - as))/ao;
}
function DrawHorizontalLine1px(image, x, y, length, color){
  var i;

  for(i = 0; i < length; i = i + 1){
    DrawPixel(image, x + i, y, color);
  }
}
function DrawVerticalLine1px(image, x, y, height, color){
  var i;

  for(i = 0; i < height; i = i + 1){
    DrawPixel(image, x, y + i, color);
  }
}
function DrawRectangle1px(image, x, y, width, height, color){
  DrawHorizontalLine1px(image, x, y, width + 1, color);
  DrawVerticalLine1px(image, x, y + 1, height + 1 - 1, color);
  DrawVerticalLine1px(image, x + width, y + 1, height + 1 - 1, color);
  DrawHorizontalLine1px(image, x + 1, y + height, width + 1 - 2, color);
}
function DrawImageOnImage(dst, src, topx, topy){
  var y, x;

  for(y = 0; y < ImageHeight(src); y = y + 1){
    for(x = 0; x < ImageWidth(src); x = x + 1){
      if(topx + x >= 0 && topx + x < ImageWidth(dst) && topy + y >= 0 && topy + y < ImageHeight(dst)){
        DrawPixel(dst, topx + x, topy + y, src.x[x].y[y]);
      }
    }
  }
}
function DrawLine1px(image, x0, y0, x1, y1, color){
  XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color);
}
function XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color){
  var steep;
  var x, t, dx, dy, g, xEnd, yEnd, xGap, xpxl1, ypxl1, intery, xpxl2, ypxl2, olda;

  olda = color.a;

  steep = Math.abs(y1 - y0) > Math.abs(x1 - x0);

  if(steep){
    t = x0;
    x0 = y0;
    y0 = t;

    t = x1;
    x1 = y1;
    y1 = t;
  }
  if(x0 > x1){
    t = x0;
    x0 = x1;
    x1 = t;

    t = y0;
    y0 = y1;
    y1 = t;
  }

  dx = x1 - x0;
  dy = y1 - y0;
  g = dy/dx;

  if(dx == 0){
    g = 1;
  }

  xEnd = Round(x0);
  yEnd = y0 + g*(xEnd - x0);
  xGap = OneMinusFractionalPart(x0 + 0.5);
  xpxl1 = xEnd;
  ypxl1 = Math.floor(yEnd);
  if(steep){
    DrawPixel(image, ypxl1, xpxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, ypxl1 + 1, xpxl1, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }else{
    DrawPixel(image, xpxl1, ypxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, xpxl1, ypxl1 + 1, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }
  intery = yEnd + g;

  xEnd = Round(x1);
  yEnd = y1 + g*(xEnd - x1);
  xGap = FractionalPart(x1 + 0.5);
  xpxl2 = xEnd;
  ypxl2 = Math.floor(yEnd);
  if(steep){
    DrawPixel(image, ypxl2, xpxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, ypxl2 + 1, xpxl2, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }else{
    DrawPixel(image, xpxl2, ypxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, xpxl2, ypxl2 + 1, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }

  if(steep){
    for(x = xpxl1 + 1; x <= xpxl2 - 1; x = x + 1){
      DrawPixel(image, Math.floor(intery), x, SetBrightness(color, OneMinusFractionalPart(intery)));
      DrawPixel(image, Math.floor(intery) + 1, x, SetBrightness(color, FractionalPart(intery)));
      intery = intery + g;
    }
  }else{
    for(x = xpxl1 + 1; x <= xpxl2 - 1; x = x + 1){
      DrawPixel(image, x, Math.floor(intery), SetBrightness(color, OneMinusFractionalPart(intery)));
      DrawPixel(image, x, Math.floor(intery) + 1, SetBrightness(color, FractionalPart(intery)));
      intery = intery + g;
    }
  }

  color.a = olda;
}
function OneMinusFractionalPart(x){
  return 1 - FractionalPart(x);
}
function FractionalPart(x){
  return x - Math.floor(x);
}
function SetBrightness(color, newBrightness){
  color.a = newBrightness;
  return color;
}
function DrawQuadraticBezierCurve(image, x0, y0, cx, cy, x1, y1, color){
  var t, dt, dx, dy;
  var xs, ys, xe, ye;

  dx = Math.abs(x0 - x1);
  dy = Math.abs(y0 - y1);

  dt = 1/Math.sqrt(Math.pow(dx, 2) + Math.pow(dy, 2));

  xs = {};
  ys = {};
  xe = {};
  ye = {};

  QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, 0, xs, ys);
  for(t = dt; t <= 1; t = t + dt){
    QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, xe, ye);
    DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color);
    xs.numberValue = xe.numberValue;
    ys.numberValue = ye.numberValue;
  }

  delete(xs);
  delete(ys);
  delete(xe);
  delete(ye);
}
function QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, x, y){
  x.numberValue = Math.pow(1 - t, 2)*x0 + (1 - t)*2*t*cx + Math.pow(t, 2)*x1;
  y.numberValue = Math.pow(1 - t, 2)*y0 + (1 - t)*2*t*cy + Math.pow(t, 2)*y1;
}
function DrawCubicBezierCurve(image, x0, y0, c0x, c0y, c1x, c1y, x1, y1, color){
  var t, dt, dx, dy;
  var xs, ys, xe, ye;

  dx = Math.abs(x0 - x1);
  dy = Math.abs(y0 - y1);

  dt = 1/Math.sqrt(Math.pow(dx, 2) + Math.pow(dy, 2));

  xs = {};
  ys = {};
  xe = {};
  ye = {};

  CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, 0, xs, ys);
  for(t = dt; t <= 1; t = t + dt){
    CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, xe, ye);
    DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color);
    xs.numberValue = xe.numberValue;
    ys.numberValue = ye.numberValue;
  }

  delete(xs);
  delete(ys);
  delete(xe);
  delete(ye);
}
function CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, x, y){
  x.numberValue = Math.pow(1 - t, 3)*x0 + Math.pow(1 - t, 2)*3*t*c0x + (1 - t)*3*Math.pow(t, 2)*c1x + Math.pow(t, 3)*x1;

  y.numberValue = Math.pow(1 - t, 3)*y0 + Math.pow(1 - t, 2)*3*t*c0y + (1 - t)*3*Math.pow(t, 2)*c1y + Math.pow(t, 3)*y1;
}
function CopyImage(image){
  var copy;
  var i, j;

  copy = CreateImage(ImageWidth(image), ImageHeight(image), GetTransparent());

  for(i = 0; i < ImageWidth(image); i = i + 1){
    for(j = 0; j < ImageHeight(image); j = j + 1){
      SetPixel(copy, i, j, image.x[i].y[j]);
    }
  }

  return copy;
}
function GetImagePixel(image, x, y){
  return image.x[x].y[y];
}
function HorizontalFlip(img){
  var y, x;
  var tmp;
  var c1, c2;

  for(y = 0; y < ImageHeight(img); y = y + 1){
    for(x = 0; x < ImageWidth(img)/2; x = x + 1){
      c1 = img.x[x].y[y];
      c2 = img.x[ImageWidth(img) - 1 - x].y[y];

      tmp = c1.a;
      c1.a = c2.a;
      c2.a = tmp;

      tmp = c1.r;
      c1.r = c2.r;
      c2.r = tmp;

      tmp = c1.g;
      c1.g = c2.g;
      c2.g = tmp;

      tmp = c1.b;
      c1.b = c2.b;
      c2.b = tmp;
    }
  }
}
function DrawFilledRectangle(image, x, y, w, h, color){
  var i, j;

  for(i = 0; i < w; i = i + 1){
    for(j = 0; j < h; j = j + 1){
      SetPixel(image, x + i, y + j, color);
    }
  }
}
function RotateAntiClockwise90Degrees(image){
  var rotated;
  var x, y;

  rotated = CreateImage(ImageHeight(image), ImageWidth(image), GetBlack());

  for(y = 0; y < ImageHeight(image); y = y + 1){
    for(x = 0; x < ImageWidth(image); x = x + 1){
      SetPixel(rotated, y, ImageWidth(image) - 1 - x, GetImagePixel(image, x, y));
    }
  }

  return rotated;
}
function DrawCircle(canvas, xCenter, yCenter, radius, color){
  DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color);
}
function BresenhamsCircleDrawingAlgorithm(canvas, xCenter, yCenter, radius, color){
  var x, y, delta;

  y = radius;
  x = 0;

  delta = 3 - 2*radius;
  for(; y >= x; x = x + 1){
    DrawLine1px(canvas, xCenter + x, yCenter + y, xCenter + x, yCenter + y, color);
    DrawLine1px(canvas, xCenter + x, yCenter - y, xCenter + x, yCenter - y, color);
    DrawLine1px(canvas, xCenter - x, yCenter + y, xCenter - x, yCenter + y, color);
    DrawLine1px(canvas, xCenter - x, yCenter - y, xCenter - x, yCenter - y, color);

    DrawLine1px(canvas, xCenter - y, yCenter + x, xCenter - y, yCenter + x, color);
    DrawLine1px(canvas, xCenter - y, yCenter - x, xCenter - y, yCenter - x, color);
    DrawLine1px(canvas, xCenter + y, yCenter + x, xCenter + y, yCenter + x, color);
    DrawLine1px(canvas, xCenter + y, yCenter - x, xCenter + y, yCenter - x, color);

    if(delta < 0){
      delta = delta + 4*x + 6;
    }else{
      delta = delta + 4*(x - y) + 10;
      y = y - 1;
    }
  }
}
function DrawCircleMidpointAlgorithm(canvas, xCenter, yCenter, radius, color){
  var d, x, y;

  d = Math.floor((5 - radius*4)/4);
  x = 0;
  y = radius;

  for(; x <= y; x = x + 1){
    DrawPixel(canvas, xCenter + x, yCenter + y, color);
    DrawPixel(canvas, xCenter + x, yCenter - y, color);
    DrawPixel(canvas, xCenter - x, yCenter + y, color);
    DrawPixel(canvas, xCenter - x, yCenter - y, color);
    DrawPixel(canvas, xCenter + y, yCenter + x, color);
    DrawPixel(canvas, xCenter + y, yCenter - x, color);
    DrawPixel(canvas, xCenter - y, yCenter + x, color);
    DrawPixel(canvas, xCenter - y, yCenter - x, color);

    if(d < 0){
      d = d + 2*x + 1;
    }else{
      d = d + 2*(x - y) + 1;
      y = y - 1;
    }
  }
}
function DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color){
  var pixels, a, da, dx, dy;

  /* Place the circle in the center of the pixel. */
  xCenter = Math.floor(xCenter) + 0.5;
  yCenter = Math.floor(yCenter) + 0.5;

  pixels = 2*Math.PI*radius;

  /* Below a radius of 10 pixels, over-compensate to get a smoother circle. */
  if(radius < 10){
    pixels = pixels*10;
  }

  da = 2*Math.PI/pixels;

  for(a = 0; a < 2*Math.PI; a = a + da){
    dx = Math.cos(a)*radius;
    dy = Math.sin(a)*radius;

    /* Floor to get the pixel coordinate. */
    DrawPixel(canvas, Math.floor(xCenter + dx), Math.floor(yCenter + dy), color);
  }
}
function DrawFilledCircle(canvas, x, y, r, color){
  DrawFilledCircleBasicAlgorithm(canvas, x, y, r, color);
}
function DrawFilledCircleMidpointAlgorithm(canvas, xCenter, yCenter, radius, color){
  var d, x, y;

  d = Math.floor((5 - radius*4)/4);
  x = 0;
  y = radius;

  for(; x <= y; x = x + 1){
    DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter + y, xCenter - x, yCenter + y, color);
    DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter - y, xCenter - x, yCenter - y, color);
    DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter + x, xCenter - y, yCenter + x, color);
    DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter - x, xCenter - y, yCenter - x, color);

    if(d < 0){
      d = d + 2*x + 1;
    }else{
      d = d + 2*(x - y) + 1;
      y = y - 1;
    }
  }
}
function DrawFilledCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color){
  var pixels, a, da, dx, dy;

  /* Place the circle in the center of the pixel. */
  xCenter = Math.floor(xCenter) + 0.5;
  yCenter = Math.floor(yCenter) + 0.5;

  pixels = 2*Math.PI*radius;

  /* Below a radius of 10 pixels, over-compensate to get a smoother circle. */
  if(radius < 10){
    pixels = pixels*10;
  }

  da = 2*Math.PI/pixels;

  /* Draw lines for a half-circle to fill an entire circle. */
  for(a = 0; a < Math.PI; a = a + da){
    dx = Math.cos(a)*radius;
    dy = Math.sin(a)*radius;

    /* Floor to get the pixel coordinate. */
    DrawVerticalLine1px(canvas, Math.floor(xCenter - dx), Math.floor(yCenter - dy), Math.floor(2*dy) + 1, color);
  }
}
function DrawTriangle(canvas, xCenter, yCenter, height, color){
  var x1, y1, x2, y2, x3, y3;

  x1 = Math.floor(xCenter + 0.5);
  y1 = Math.floor(Math.floor(yCenter + 0.5) - height);
  x2 = x1 - 2*height*Math.tan(Math.PI/6);
  y2 = Math.floor(y1 + 2*height);
  x3 = x1 + 2*height*Math.tan(Math.PI/6);
  y3 = Math.floor(y1 + 2*height);

  DrawLine1px(canvas, x1, y1, x2, y2, color);
  DrawLine1px(canvas, x1, y1, x3, y3, color);
  DrawLine1px(canvas, x2, y2, x3, y3, color);
}
function DrawFilledTriangle(canvas, xCenter, yCenter, height, color){
  var i, offset, x1, y1;

  x1 = Math.floor(xCenter + 0.5);
  y1 = Math.floor(Math.floor(yCenter + 0.5) - height);

  for(i = 0; i <= 2*height; i = i + 1){
    offset = Math.floor(i*Math.tan(Math.PI/6));
    DrawHorizontalLine1px(canvas, x1 - offset, y1 + i, 2*offset, color);
  }
}
function DrawLine(canvas, x1, y1, x2, y2, thickness, color){
  DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color);
}
function DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color){
  var x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r;

  dx = x2 - x1;
  dy = y2 - y1;

  incX = Sign(dx);
  incY = Sign(dy);

  dx = Math.abs(dx);
  dy = Math.abs(dy);

  if(dx > dy){
    pdx = incX;
    pdy = 0;
    es = dy;
    el = dx;
  }else{
    pdx = 0;
    pdy = incY;
    es = dx;
    el = dy;
  }

  x = x1;
  y = y1;
  err = el/2;

  if(thickness >= 3){
    r = thickness/2;
    DrawCircle(canvas, x, y, r, color);
  }else if(Math.floor(thickness) == 2){
    DrawFilledRectangle(canvas, x, y, 2, 2, color);
  }else if(Math.floor(thickness) == 1){
    DrawPixel(canvas, x, y, color);
  }

  for(t = 0; t < el; t = t + 1){
    err = err - es;
    if(err < 0){
      err = err + el;
      x = x + incX;
      y = y + incY;
    }else{
      x = x + pdx;
      y = y + pdy;
    }

    if(thickness >= 3){
      r = thickness/2;
      DrawCircle(canvas, x, y, r, color);
    }else if(Math.floor(thickness) == 2){
      DrawFilledRectangle(canvas, x, y, 2, 2, color);
    }else if(Math.floor(thickness) == 1){
      DrawPixel(canvas, x, y, color);
    }
  }
}
function DrawLineBresenhamsAlgorithm(canvas, x1, y1, x2, y2, color){
  var x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t;

  dx = x2 - x1;
  dy = y2 - y1;

  incX = Sign(dx);
  incY = Sign(dy);

  dx = Math.abs(dx);
  dy = Math.abs(dy);

  if(dx > dy){
    pdx = incX;
    pdy = 0;
    es = dy;
    el = dx;
  }else{
    pdx = 0;
    pdy = incY;
    es = dx;
    el = dy;
  }

  x = x1;
  y = y1;
  err = el/2;
  DrawPixel(canvas, x, y, color);

  for(t = 0; t < el; t = t + 1){
    err = err - es;
    if(err < 0){
      err = err + el;
      x = x + incX;
      y = y + incY;
    }else{
      x = x + pdx;
      y = y + pdy;
    }

    DrawPixel(canvas, x, y, color);
  }
}
function DrawLineBresenhamsAlgorithmThickPatterned(canvas, x1, y1, x2, y2, thickness, pattern, offset, color){
  var x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r;

  dx = x2 - x1;
  dy = y2 - y1;

  incX = Sign(dx);
  incY = Sign(dy);

  dx = Math.abs(dx);
  dy = Math.abs(dy);

  if(dx > dy){
    pdx = incX;
    pdy = 0;
    es = dy;
    el = dx;
  }else{
    pdx = 0;
    pdy = incY;
    es = dx;
    el = dy;
  }

  x = x1;
  y = y1;
  err = el/2;

  offset.numberValue = (offset.numberValue + 1)%(pattern.length*thickness);

  if(pattern[Math.floor(offset.numberValue/thickness)]){
    if(thickness >= 3){
      r = thickness/2;
      DrawCircle(canvas, x, y, r, color);
    }else if(Math.floor(thickness) == 2){
      DrawFilledRectangle(canvas, x, y, 2, 2, color);
    }else if(Math.floor(thickness) == 1){
      DrawPixel(canvas, x, y, color);
    }
  }

  for(t = 0; t < el; t = t + 1){
    err = err - es;
    if(err < 0){
      err = err + el;
      x = x + incX;
      y = y + incY;
    }else{
      x = x + pdx;
      y = y + pdy;
    }

    offset.numberValue = (offset.numberValue + 1)%(pattern.length*thickness);

    if(pattern[Math.floor(offset.numberValue/thickness)]){
      if(thickness >= 3){
        r = thickness/2;
        DrawCircle(canvas, x, y, r, color);
      }else if(Math.floor(thickness) == 2){
        DrawFilledRectangle(canvas, x, y, 2, 2, color);
      }else if(Math.floor(thickness) == 1){
        DrawPixel(canvas, x, y, color);
      }
    }
  }
}
function GetLinePattern5(){
  var pattern;

  pattern = [];
  pattern.length = 19;

  pattern[0] = true;
  pattern[1] = true;
  pattern[2] = true;
  pattern[3] = true;
  pattern[4] = true;
  pattern[5] = true;
  pattern[6] = true;
  pattern[7] = true;
  pattern[8] = true;
  pattern[9] = true;
  pattern[10] = false;
  pattern[11] = false;
  pattern[12] = false;
  pattern[13] = true;
  pattern[14] = true;
  pattern[15] = true;
  pattern[16] = false;
  pattern[17] = false;
  pattern[18] = false;

  return pattern;
}
function GetLinePattern4(){
  var pattern;

  pattern = [];
  pattern.length = 13;

  pattern[0] = true;
  pattern[1] = true;
  pattern[2] = true;
  pattern[3] = true;
  pattern[4] = true;
  pattern[5] = true;
  pattern[6] = true;
  pattern[7] = true;
  pattern[8] = true;
  pattern[9] = true;
  pattern[10] = false;
  pattern[11] = false;
  pattern[12] = false;

  return pattern;
}
function GetLinePattern3(){
  var pattern;

  pattern = [];
  pattern.length = 13;

  pattern[0] = true;
  pattern[1] = true;
  pattern[2] = true;
  pattern[3] = true;
  pattern[4] = true;
  pattern[5] = true;
  pattern[6] = false;
  pattern[7] = false;
  pattern[8] = false;
  pattern[9] = true;
  pattern[10] = true;
  pattern[11] = false;
  pattern[12] = false;

  return pattern;
}
function GetLinePattern2(){
  var pattern;

  pattern = [];
  pattern.length = 4;

  pattern[0] = true;
  pattern[1] = true;
  pattern[2] = false;
  pattern[3] = false;

  return pattern;
}
function GetLinePattern1(){
  var pattern;

  pattern = [];
  pattern.length = 8;

  pattern[0] = true;
  pattern[1] = true;
  pattern[2] = true;
  pattern[3] = true;
  pattern[4] = true;
  pattern[5] = false;
  pattern[6] = false;
  pattern[7] = false;

  return pattern;
}
function Blur(src, pixels){
  var dst;
  var x, y, w, h;

  w = ImageWidth(src);
  h = ImageHeight(src);
  dst = CreateImage(w, h, GetTransparent());

  for(x = 0; x < w; x = x + 1){
    for(y = 0; y < h; y = y + 1){
      SetPixel(dst, x, y, CreateBlurForPoint(src, x, y, pixels));
    }
  }

  return dst;
}
function CreateBlurForPoint(src, x, y, pixels){
  var rgba;
  var i, j, countColor, countTransparent;
  var fromx, tox, fromy, toy;
  var w, h;
  var alpha;

  w = src.x.length;
  h = src.x[0].y.length;

  rgba = {};
  rgba.r = 0;
  rgba.g = 0;
  rgba.b = 0;
  rgba.a = 0;

  fromx = x - pixels;
  fromx = Math.max(fromx, 0);

  tox = x + pixels;
  tox = Math.min(tox, w - 1);

  fromy = y - pixels;
  fromy = Math.max(fromy, 0);

  toy = y + pixels;
  toy = Math.min(toy, h - 1);

  countColor = 0;
  countTransparent = 0;
  for(i = fromx; i < tox; i = i + 1){
    for(j = fromy; j < toy; j = j + 1){
      alpha = src.x[i].y[j].a;
      if(alpha > 0){
        rgba.r = rgba.r + src.x[i].y[j].r;
        rgba.g = rgba.g + src.x[i].y[j].g;
        rgba.b = rgba.b + src.x[i].y[j].b;
        countColor = countColor + 1;
      }
      rgba.a = rgba.a + alpha;
      countTransparent = countTransparent + 1;
    }
  }

  if(countColor > 0){
    rgba.r = rgba.r/countColor;
    rgba.g = rgba.g/countColor;
    rgba.b = rgba.b/countColor;
  }else{
    rgba.r = 0;
    rgba.g = 0;
    rgba.b = 0;
  }

  if(countTransparent > 0){
    rgba.a = rgba.a/countTransparent;
  }else{
    rgba.a = 0;
  }

  return rgba;
}
function CreateStringScientificNotationDecimalFromNumber(decimal){
  var mantissaReference, exponentReference;
  var multiplier, inc;
  var exponent;
  var done, isPositive;
  var result;

  mantissaReference = {};
  exponentReference = {};
  result = [];
  result.length = 0;
  done = false;
  exponent = 0;

  if(decimal < 0){
    isPositive = false;
    decimal =  -decimal;
  }else{
    isPositive = true;
  }

  if(decimal == 0){
    done = true;
  }

  if( !done ){
    multiplier = 0;
    inc = 0;

    if(decimal < 1){
      multiplier = 10;
      inc =  -1;
    }else if(decimal >= 10){
      multiplier = 0.1;
      inc = 1;
    }else{
      done = true;
    }

    if( !done ){
      for(; decimal >= 10 || decimal < 1; ){
        decimal = decimal*multiplier;
        exponent = exponent + inc;
      }
    }
  }

  CreateStringFromNumberWithCheck(decimal, 10, mantissaReference);

  CreateStringFromNumberWithCheck(exponent, 10, exponentReference);

  if( !isPositive ){
    result = AppendString(result, "-".split(''));
  }

  result = AppendString(result, mantissaReference.string);
  result = AppendString(result, "e".split(''));
  result = AppendString(result, exponentReference.string);

  return result;
}
function CreateStringDecimalFromNumber(decimal){
  var stringReference;

  stringReference = {};

  /* This will succeed because base = 10. */
  CreateStringFromNumberWithCheck(decimal, 10, stringReference);

  return stringReference.string;
}
function CreateStringFromNumberWithCheck(decimal, base, stringReference){
  var string;
  var maximumDigits;
  var digitPosition;
  var hasPrintedPoint, isPositive;
  var i, d;
  var success;
  var characterReference;
  var c;

  isPositive = true;

  if(decimal < 0){
    isPositive = false;
    decimal =  -decimal;
  }

  if(decimal == 0){
    stringReference.string = "0".split('');
    success = true;
  }else{
    characterReference = {};

    if(IsInteger(base)){
      success = true;

      string = [];
      string.length = 0;

      maximumDigits = GetMaximumDigitsForBase(base);

      digitPosition = GetFirstDigitPosition(decimal, base);

      decimal = Math.round(decimal*Math.pow(base, maximumDigits - digitPosition - 1));

      hasPrintedPoint = false;

      if( !isPositive ){
        string = AppendCharacter(string, '-');
      }

      /* Print leading zeros. */
      if(digitPosition < 0){
        string = AppendCharacter(string, '0');
        string = AppendCharacter(string, '.');
        hasPrintedPoint = true;
        for(i = 0; i <  -digitPosition - 1; i = i + 1){
          string = AppendCharacter(string, '0');
        }
      }

      /* Print number. */
      for(i = 0; i < maximumDigits && success; i = i + 1){
        d = Math.floor(decimal/Math.pow(base, maximumDigits - i - 1));

        if(d >= base){
          d = base - 1;
        }

        if( !hasPrintedPoint  && digitPosition - i + 1 == 0){
          if(decimal != 0){
            string = AppendCharacter(string, '.');
          }
          hasPrintedPoint = true;
        }

        if(decimal == 0 && hasPrintedPoint){
        }else{
          success = GetSingleDigitCharacterFromNumberWithCheck(d, base, characterReference);
          if(success){
            c = characterReference.characterValue;
            string = AppendCharacter(string, c);
          }
        }

        if(success){
          decimal = decimal - d*Math.pow(base, maximumDigits - i - 1);
        }
      }

      if(success){
        /* Print trailing zeros. */
        for(i = 0; i < digitPosition - maximumDigits + 1; i = i + 1){
          string = AppendCharacter(string, '0');
        }

        stringReference.string = string;
      }
    }else{
      success = false;
    }
  }

  /* Done */
  return success;
}
function GetMaximumDigitsForBase(base){
  var t;

  t = Math.pow(10, 15);
  return Math.floor(Math.log10(t)/Math.log10(base));
}
function GetFirstDigitPosition(decimal, base){
  var power;
  var t;

  power = Math.ceil(Math.log10(decimal)/Math.log10(base));

  t = decimal*Math.pow(base,  -power);
  if(t < base && t >= 1){
  }else if(t >= base){
    power = power + 1;
  }else if(t < 1){
    power = power - 1;
  }

  return power;
}
function GetSingleDigitCharacterFromNumberWithCheck(c, base, characterReference){
  var numberTable;
  var success;

  numberTable = GetDigitCharacterTable();

  if(c < base || c < numberTable.length){
    success = true;
    characterReference.characterValue = numberTable[c];
  }else{
    success = false;
  }

  return success;
}
function GetDigitCharacterTable(){
  var numberTable;

  numberTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".split('');

  return numberTable;
}
function CreateNumberFromDecimalStringWithCheck(string, decimalReference, errorMessage){
  return CreateNumberFromStringWithCheck(string, 10, decimalReference, errorMessage);
}
function CreateNumberFromDecimalString(string){
  var doubleReference;
  var stringReference;
  var number;

  doubleReference = CreateNumberReference(0);
  stringReference = CreateStringReference("".split(''));
  CreateNumberFromStringWithCheck(string, 10, doubleReference, stringReference);
  number = doubleReference.numberValue;

  delete(doubleReference);
  delete(stringReference);

  return number;
}
function CreateNumberFromStringWithCheck(string, base, numberReference, errorMessage){
  var success;
  var numberIsPositive, exponentIsPositive;
  var beforePoint, afterPoint, exponent;

  numberIsPositive = CreateBooleanReference(true);
  exponentIsPositive = CreateBooleanReference(true);
  beforePoint = {};
  afterPoint = {};
  exponent = {};

  if(base >= 2 && base <= 36){
    success = ExtractPartsFromNumberString(string, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessage);

    if(success){
      numberReference.numberValue = CreateNumberFromParts(base, numberIsPositive.booleanValue, beforePoint.numberArray, afterPoint.numberArray, exponentIsPositive.booleanValue, exponent.numberArray);
    }
  }else{
    success = false;
    errorMessage.string = "Base must be from 2 to 36.".split('');
  }

  return success;
}
function CreateNumberFromParts(base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent){
  var n, i, p, e;

  n = 0;

  for(i = 0; i < beforePoint.length; i = i + 1){
    p = beforePoint[beforePoint.length - i - 1];

    n = n + p*Math.pow(base, i);
  }

  for(i = 0; i < afterPoint.length; i = i + 1){
    p = afterPoint[i];

    n = n + p*Math.pow(base,  -(i + 1));
  }

  if(exponent.length > 0){
    e = 0;
    for(i = 0; i < exponent.length; i = i + 1){
      p = exponent[exponent.length - i - 1];

      e = e + p*Math.pow(base, i);
    }

    if( !exponentIsPositive ){
      e =  -e;
    }

    n = n*Math.pow(base, e);
  }

  if( !numberIsPositive ){
    n =  -n;
  }

  return n;
}
function ExtractPartsFromNumberString(n, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages){
  var i;
  var success;

  i = 0;

  if(i < n.length){
    if(n[i] == '-'){
      numberIsPositive.booleanValue = false;
      i = i + 1;
    }else if(n[i] == '+'){
      numberIsPositive.booleanValue = true;
      i = i + 1;
    }

    success = ExtractPartsFromNumberStringFromSign(n, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages);
  }else{
    success = false;
    errorMessages.string = "Number cannot have length zero.".split('');
  }

  return success;
}
function ExtractPartsFromNumberStringFromSign(n, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages){
  var success, done;
  var count, j;

  done = false;
  count = 0;
  for(; i + count < n.length &&  !done ; ){
    if(CharacterIsNumberCharacterInBase(n[i + count], base)){
      count = count + 1;
    }else{
      done = true;
    }
  }

  if(count >= 1){
    beforePoint.numberArray = [];
    beforePoint.numberArray.length = count;

    for(j = 0; j < count; j = j + 1){
      beforePoint.numberArray[j] = GetNumberFromNumberCharacterForBase(n[i + j], base);
    }

    i = i + count;

    if(i < n.length){
      success = ExtractPartsFromNumberStringFromPointOrExponent(n, base, i, afterPoint, exponentIsPositive, exponent, errorMessages);
    }else{
      afterPoint.numberArray = [];
      afterPoint.numberArray.length = 0;
      exponent.numberArray = [];
      exponent.numberArray.length = 0;
      success = true;
    }
  }else{
    success = false;
    errorMessages.string = "Number must have at least one number after the optional sign.".split('');
  }

  return success;
}
function ExtractPartsFromNumberStringFromPointOrExponent(n, base, i, afterPoint, exponentIsPositive, exponent, errorMessages){
  var success, done;
  var count, j;

  if(n[i] == '.'){
    i = i + 1;

    if(i < n.length){
      done = false;
      count = 0;
      for(; i + count < n.length &&  !done ; ){
        if(CharacterIsNumberCharacterInBase(n[i + count], base)){
          count = count + 1;
        }else{
          done = true;
        }
      }

      if(count >= 1){
        afterPoint.numberArray = [];
        afterPoint.numberArray.length = count;

        for(j = 0; j < count; j = j + 1){
          afterPoint.numberArray[j] = GetNumberFromNumberCharacterForBase(n[i + j], base);
        }

        i = i + count;

        if(i < n.length){
          success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages);
        }else{
          exponent.numberArray = [];
          exponent.numberArray.length = 0;
          success = true;
        }
      }else{
        success = false;
        errorMessages.string = "There must be at least one digit after the decimal point.".split('');
      }
    }else{
      success = false;
      errorMessages.string = "There must be at least one digit after the decimal point.".split('');
    }
  }else if(base <= 14 && (n[i] == 'e' || n[i] == 'E')){
    if(i < n.length){
      success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages);
      afterPoint.numberArray = [];
      afterPoint.numberArray.length = 0;
    }else{
      success = false;
      errorMessages.string = "There must be at least one digit after the exponent.".split('');
    }
  }else{
    success = false;
    errorMessages.string = "Expected decimal point or exponent symbol.".split('');
  }

  return success;
}
function ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages){
  var success, done;
  var count, j;

  if(base <= 14 && (n[i] == 'e' || n[i] == 'E')){
    i = i + 1;

    if(i < n.length){
      if(n[i] == '-'){
        exponentIsPositive.booleanValue = false;
        i = i + 1;
      }else if(n[i] == '+'){
        exponentIsPositive.booleanValue = true;
        i = i + 1;
      }

      if(i < n.length){
        done = false;
        count = 0;
        for(; i + count < n.length &&  !done ; ){
          if(CharacterIsNumberCharacterInBase(n[i + count], base)){
            count = count + 1;
          }else{
            done = true;
          }
        }

        if(count >= 1){
          exponent.numberArray = [];
          exponent.numberArray.length = count;

          for(j = 0; j < count; j = j + 1){
            exponent.numberArray[j] = GetNumberFromNumberCharacterForBase(n[i + j], base);
          }

          i = i + count;

          if(i == n.length){
            success = true;
          }else{
            success = false;
            errorMessages.string = "There cannot be any characters past the exponent of the number.".split('');
          }
        }else{
          success = false;
          errorMessages.string = "There must be at least one digit after the decimal point.".split('');
        }
      }else{
        success = false;
        errorMessages.string = "There must be at least one digit after the exponent symbol.".split('');
      }
    }else{
      success = false;
      errorMessages.string = "There must be at least one digit after the exponent symbol.".split('');
    }
  }else{
    success = false;
    errorMessages.string = "Expected exponent symbol.".split('');
  }

  return success;
}
function GetNumberFromNumberCharacterForBase(c, base){
  var numberTable;
  var i;
  var position;

  numberTable = GetDigitCharacterTable();
  position = 0;

  for(i = 0; i < base; i = i + 1){
    if(numberTable[i] == c){
      position = i;
    }
  }

  return position;
}
function CharacterIsNumberCharacterInBase(c, base){
  var numberTable;
  var i;
  var found;

  numberTable = GetDigitCharacterTable();
  found = false;

  for(i = 0; i < base; i = i + 1){
    if(numberTable[i] == c){
      found = true;
    }
  }

  return found;
}
function StringToNumberArray(str){
  var numberArrayReference;
  var stringReference;
  var numbers;

  numberArrayReference = {};
  stringReference = {};

  StringToNumberArrayWithCheck(str, numberArrayReference, stringReference);

  numbers = numberArrayReference.numberArray;

  delete(numberArrayReference);
  delete(stringReference);

  return numbers;
}
function StringToNumberArrayWithCheck(str, numberArrayReference, errorMessage){
  var numberStrings;
  var numbers;
  var i;
  var numberString, trimmedNumberString;
  var success;
  var numberReference;

  numberStrings = SplitByString(str, ",".split(''));

  numbers = [];
  numbers.length = numberStrings.length;
  success = true;
  numberReference = {};

  for(i = 0; i < numberStrings.length; i = i + 1){
    numberString = numberStrings[i].string;
    trimmedNumberString = Trim(numberString);
    success = CreateNumberFromDecimalStringWithCheck(trimmedNumberString, numberReference, errorMessage);
    numbers[i] = numberReference.numberValue;

    FreeStringReference(numberStrings[i]);
    delete(trimmedNumberString);
  }

  delete(numberStrings);
  delete(numberReference);

  numberArrayReference.numberArray = numbers;

  return success;
}
function Negate(x){
  return  -x;
}
function Positive(x){
  return  +x;
}
function Factorial(x){
  var i, f;

  f = 1;

  for(i = 2; i <= x; i = i + 1){
    f = f*i;
  }

  return f;
}
function Round(x){
  return Math.floor(x + 0.5);
}
function BankersRound(x){
  var r;

  if(Absolute(x - Truncate(x)) == 0.5){
    if( !DivisibleBy(Round(x), 2) ){
      r = Round(x) - 1;
    }else{
      r = Round(x);
    }
  }else{
    r = Round(x);
  }

  return r;
}
function Ceil(x){
  return Math.ceil(x);
}
function Floor(x){
  return Math.floor(x);
}
function Truncate(x){
  var t;

  if(x >= 0){
    t = Math.floor(x);
  }else{
    t = Math.ceil(x);
  }

  return t;
}
function Absolute(x){
  return Math.abs(x);
}
function Logarithm(x){
  return Math.log10(x);
}
function NaturalLogarithm(x){
  return Math.log(x);
}
function Sin(x){
  return Math.sin(x);
}
function Cos(x){
  return Math.cos(x);
}
function Tan(x){
  return Math.tan(x);
}
function Asin(x){
  return Math.asin(x);
}
function Acos(x){
  return Math.acos(x);
}
function Atan(x){
  return Math.atan(x);
}
function Atan2(y, x){
  var a;

  /* Atan2 is an invalid operation when x = 0 and y = 0, but this method does not return errors. */
  a = 0;

  if(x > 0){
    a = Atan(y/x);
  }else if(x < 0 && y >= 0){
    a = Atan(y/x) + Math.PI;
  }else if(x < 0 && y < 0){
    a = Atan(y/x) - Math.PI;
  }else if(x == 0 && y > 0){
    a = Math.PI/2;
  }else if(x == 0 && y < 0){
    a =  -Math.PI/2;
  }

  return a;
}
function Squareroot(x){
  return Math.sqrt(x);
}
function Exp(x){
  return Math.exp(x);
}
function DivisibleBy(a, b){
  return ((a%b) == 0);
}
function Combinations(n, k){
  var i, j, c;

  c = 1;
  j = 1;
  i = n - k + 1;

  for(; i <= n; ){
    c = c*i;
    c = c/j;

    i = i + 1;
    j = j + 1;
  }

  return c;
}
function Permutations(n, k){
  var i, c;

  c = 1;

  for(i = n - k + 1; i <= n; i = i + 1){
    c = c*i;
  }

  return c;
}
function EpsilonCompare(a, b, epsilon){
  return Math.abs(a - b) < epsilon;
}
function GreatestCommonDivisor(a, b){
  var t;

  for(; b != 0; ){
    t = b;
    b = a%b;
    a = t;
  }

  return a;
}
function GCDWithSubtraction(a, b){
  var g;

  if(a == 0){
    g = b;
  }else{
    for(; b != 0; ){
      if(a > b){
        a = a - b;
      }else{
        b = b - a;
      }
    }

    g = a;
  }

  return g;
}
function IsInteger(a){
  return (a - Math.floor(a)) == 0;
}
function GreatestCommonDivisorWithCheck(a, b, gcdReference){
  var success;
  var gcd;

  if(IsInteger(a) && IsInteger(b)){
    gcd = GreatestCommonDivisor(a, b);
    gcdReference.numberValue = gcd;
    success = true;
  }else{
    success = false;
  }

  return success;
}
function LeastCommonMultiple(a, b){
  var lcm;

  if(a > 0 && b > 0){
    lcm = Math.abs(a*b)/GreatestCommonDivisor(a, b);
  }else{
    lcm = 0;
  }

  return lcm;
}
function Sign(a){
  var s;

  if(a > 0){
    s = 1;
  }else if(a < 0){
    s =  -1;
  }else{
    s = 0;
  }

  return s;
}
function Max(a, b){
  return Math.max(a, b);
}
function Min(a, b){
  return Math.min(a, b);
}
function Power(a, b){
  return Math.pow(a, b);
}
function Gamma(x){
  return LanczosApproximation(x);
}
function LogGamma(x){
  return Math.log(Gamma(x));
}
function LanczosApproximation(z){
  var p;
  var i, y, t, x;

  p = [];
  p.length = 8;
  p[0] = 676.5203681218851;
  p[1] =  -1259.1392167224028;
  p[2] = 771.32342877765313;
  p[3] =  -176.61502916214059;
  p[4] = 12.507343278686905;
  p[5] =  -0.13857109526572012;
  p[6] = 9.9843695780195716e-6;
  p[7] = 1.5056327351493116e-7;

  if(z < 0.5){
    y = Math.PI/(Math.sin(Math.PI*z)*LanczosApproximation(1 - z));
  }else{
    z = z - 1;
    x = 0.99999999999980993;
    for(i = 0; i < p.length; i = i + 1){
      x = x + p[i]/(z + i + 1);
    }
    t = z + p.length - 0.5;
    y = Math.sqrt(2*Math.PI)*Math.pow(t, z + 0.5)*Math.exp( -t)*x;
  }

  return y;
}
function Beta(x, y){
  return Gamma(x)*Gamma(y)/Gamma(x + y);
}
function Sinh(x){
  return (Math.exp(x) - Math.exp( -x))/2;
}
function Cosh(x){
  return (Math.exp(x) + Math.exp( -x))/2;
}
function Tanh(x){
  return Sinh(x)/Cosh(x);
}
function Cot(x){
  return 1/Math.tan(x);
}
function Sec(x){
  return 1/Math.cos(x);
}
function Csc(x){
  return 1/Math.sin(x);
}
function Coth(x){
  return Cosh(x)/Sinh(x);
}
function Sech(x){
  return 1/Cosh(x);
}
function Csch(x){
  return 1/Sinh(x);
}
function Error(x){
  var y, t, tau, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10;

  if(x == 0){
    y = 0;
  }else if(x < 0){
    y =  -Error( -x);
  }else{
    c1 =  -1.26551223;
    c2 =  +1.00002368;
    c3 =  +0.37409196;
    c4 =  +0.09678418;
    c5 =  -0.18628806;
    c6 =  +0.27886807;
    c7 =  -1.13520398;
    c8 =  +1.48851587;
    c9 =  -0.82215223;
    c10 =  +0.17087277;

    t = 1/(1 + 0.5*Math.abs(x));

    tau = t*Math.exp( -(Math.pow(x, 2)) + c1 + t*(c2 + t*(c3 + t*(c4 + t*(c5 + t*(c6 + t*(c7 + t*(c8 + t*(c9 + t*c10)))))))));

    y = 1 - tau;
  }

  return y;
}
function ErrorInverse(x){
  var y, a, t;

  a = (8*(Math.PI - 3))/(3*Math.PI*(4 - Math.PI));

  t = 2/(Math.PI*a) + Math.log(1 - Math.pow(x, 2))/2;
  y = Sign(x)*Math.sqrt(Math.sqrt(Math.pow(t, 2) - Math.log(1 - Math.pow(x, 2))/a) - t);

  return y;
}
function FallingFactorial(x, n){
  var k, y;

  y = 1;

  for(k = 0; k <= n - 1; k = k + 1){
    y = y*(x - k);
  }

  return y;
}
function RisingFactorial(x, n){
  var k, y;

  y = 1;

  for(k = 0; k <= n - 1; k = k + 1){
    y = y*(x + k);
  }

  return y;
}
function Hypergeometric(a, b, c, z, maxIterations, precision){
  var y;

  if(Math.abs(z) >= 0.5){
    y = Math.pow(1 - z,  -a)*HypergeometricDirect(a, c - b, c, z/(z - 1), maxIterations, precision);
  }else{
    y = HypergeometricDirect(a, b, c, z, maxIterations, precision);
  }

  return y;
}
function HypergeometricDirect(a, b, c, z, maxIterations, precision){
  var y, yp, n;
  var done;

  y = 0;
  done = false;

  for(n = 0; n < maxIterations &&  !done ; n = n + 1){
    yp = RisingFactorial(a, n)*RisingFactorial(b, n)/RisingFactorial(c, n)*Math.pow(z, n)/Factorial(n);
    if(Math.abs(yp) < precision){
      done = true;
    }
    y = y + yp;
  }

  return y;
}
function BernouilliNumber(n){
  return AkiyamaTanigawaAlgorithm(n);
}
function AkiyamaTanigawaAlgorithm(n){
  var m, j, B;
  var A;

  A = [];
  A.length = n + 1;

  for(m = 0; m <= n; m = m + 1){
    A[m] = 1/(m + 1);
    for(j = m; j >= 1; j = j - 1){
      A[j - 1] = j*(A[j - 1] - A[j]);
    }
  }

  B = A[0];

  delete(A);

  return B;
}
function aStringToNumberArray(string){
  var i;
  var array;

  array = [];
  array.length = string.length;

  for(i = 0; i < string.length; i = i + 1){
    array[i] = string[i].charCodeAt(0);
  }
  return array;
}
function aNumberArrayToString(array){
  var i;
  var string;

  string = [];
  string.length = array.length;

  for(i = 0; i < array.length; i = i + 1){
    string[i] = String.fromCharCode(array[i]);
  }
  return string;
}
function aNumberArraysEqual(a, b){
  var equal;
  var i;

  equal = true;
  if(a.length == b.length){
    for(i = 0; i < a.length && equal; i = i + 1){
      if(a[i] != b[i]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
function aBooleanArraysEqual(a, b){
  var equal;
  var i;

  equal = true;
  if(a.length == b.length){
    for(i = 0; i < a.length && equal; i = i + 1){
      if(a[i] != b[i]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
function aStringsEqual(a, b){
  var equal;
  var i;

  equal = true;
  if(a.length == b.length){
    for(i = 0; i < a.length && equal; i = i + 1){
      if(a[i] != b[i]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
function aFillNumberArray(a, value){
  var i;

  for(i = 0; i < a.length; i = i + 1){
    a[i] = value;
  }
}
function aFillString(a, value){
  var i;

  for(i = 0; i < a.length; i = i + 1){
    a[i] = value;
  }
}
function aFillBooleanArray(a, value){
  var i;

  for(i = 0; i < a.length; i = i + 1){
    a[i] = value;
  }
}
function aFillNumberArrayRange(a, value, from, to){
  var i, length;
  var success;

  if(from >= 0 && from <= a.length && to >= 0 && to <= a.length && from <= to){
    length = to - from;
    for(i = 0; i < length; i = i + 1){
      a[from + i] = value;
    }

    success = true;
  }else{
    success = false;
  }

  return success;
}
function aFillBooleanArrayRange(a, value, from, to){
  var i, length;
  var success;

  if(from >= 0 && from <= a.length && to >= 0 && to <= a.length && from <= to){
    length = to - from;
    for(i = 0; i < length; i = i + 1){
      a[from + i] = value;
    }

    success = true;
  }else{
    success = false;
  }

  return success;
}
function aFillStringRange(a, value, from, to){
  var i, length;
  var success;

  if(from >= 0 && from <= a.length && to >= 0 && to <= a.length && from <= to){
    length = to - from;
    for(i = 0; i < length; i = i + 1){
      a[from + i] = value;
    }

    success = true;
  }else{
    success = false;
  }

  return success;
}
function aCopyNumberArray(a){
  var i;
  var n;

  n = [];
  n.length = a.length;

  for(i = 0; i < a.length; i = i + 1){
    n[i] = a[i];
  }

  return n;
}
function aCopyBooleanArray(a){
  var i;
  var n;

  n = [];
  n.length = a.length;

  for(i = 0; i < a.length; i = i + 1){
    n[i] = a[i];
  }

  return n;
}
function aCopyString(a){
  var i;
  var n;

  n = [];
  n.length = a.length;

  for(i = 0; i < a.length; i = i + 1){
    n[i] = a[i];
  }

  return n;
}
function aCopyNumberArrayRange(a, from, to, copyReference){
  var i, length;
  var n;
  var success;

  if(from >= 0 && from <= a.length && to >= 0 && to <= a.length && from <= to){
    length = to - from;
    n = [];
    n.length = length;

    for(i = 0; i < length; i = i + 1){
      n[i] = a[from + i];
    }

    copyReference.numberArray = n;
    success = true;
  }else{
    success = false;
  }

  return success;
}
function aCopyBooleanArrayRange(a, from, to, copyReference){
  var i, length;
  var n;
  var success;

  if(from >= 0 && from <= a.length && to >= 0 && to <= a.length && from <= to){
    length = to - from;
    n = [];
    n.length = length;

    for(i = 0; i < length; i = i + 1){
      n[i] = a[from + i];
    }

    copyReference.booleanArray = n;
    success = true;
  }else{
    success = false;
  }

  return success;
}
function aCopyStringRange(a, from, to, copyReference){
  var i, length;
  var n;
  var success;

  if(from >= 0 && from <= a.length && to >= 0 && to <= a.length && from <= to){
    length = to - from;
    n = [];
    n.length = length;

    for(i = 0; i < length; i = i + 1){
      n[i] = a[from + i];
    }

    copyReference.string = n;
    success = true;
  }else{
    success = false;
  }

  return success;
}
function aIsLastElement(length, index){
  return index + 1 == length;
}
function aCreateNumberArray(length, value){
  var array;

  array = [];
  array.length = length;
  aFillNumberArray(array, value);

  return array;
}
function aCreateBooleanArray(length, value){
  var array;

  array = [];
  array.length = length;
  aFillBooleanArray(array, value);

  return array;
}
function aCreateString(length, value){
  var array;

  array = [];
  array.length = length;
  aFillString(array, value);

  return array;
}
function aSwapElementsOfNumberArray(A, ai, bi){
  var tmp;

  tmp = A[ai];
  A[ai] = A[bi];
  A[bi] = tmp;
}
function aSwapElementsOfStringArray(A, ai, bi){
  var tmp;

  tmp = A.stringArray[ai];
  A.stringArray[ai] = A.stringArray[bi];
  A.stringArray[bi] = tmp;
}
function aReverseNumberArray(array){
  var i;

  for(i = 0; i < array.length/2; i = i + 1){
    aSwapElementsOfNumberArray(array, i, array.length - i - 1);
  }
}
function CreateBooleanReference(value){
  var ref;

  ref = {};
  ref.booleanValue = value;

  return ref;
}
function CreateBooleanArrayReference(value){
  var ref;

  ref = {};
  ref.booleanArray = value;

  return ref;
}
function CreateBooleanArrayReferenceLengthValue(length, value){
  var ref;
  var i;

  ref = {};
  ref.booleanArray = [];
  ref.booleanArray.length = length;

  for(i = 0; i < length; i = i + 1){
    ref.booleanArray[i] = value;
  }

  return ref;
}
function FreeBooleanArrayReference(booleanArrayReference){
  delete(booleanArrayReference.booleanArray);
  delete(booleanArrayReference);
}
function CreateCharacterReference(value){
  var ref;

  ref = {};
  ref.characterValue = value;

  return ref;
}
function CreateNumberReference(value){
  var ref;

  ref = {};
  ref.numberValue = value;

  return ref;
}
function CreateNumberArrayReference(value){
  var ref;

  ref = {};
  ref.numberArray = value;

  return ref;
}
function CreateNumberArrayReferenceLengthValue(length, value){
  var ref;
  var i;

  ref = {};
  ref.numberArray = [];
  ref.numberArray.length = length;

  for(i = 0; i < length; i = i + 1){
    ref.numberArray[i] = value;
  }

  return ref;
}
function FreeNumberArrayReference(numberArrayReference){
  delete(numberArrayReference.numberArray);
  delete(numberArrayReference);
}
function CreateStringReference(value){
  var ref;

  ref = {};
  ref.string = value;

  return ref;
}
function CreateStringReferenceLengthValue(length, value){
  var ref;
  var i;

  ref = {};
  ref.string = [];
  ref.string.length = length;

  for(i = 0; i < length; i = i + 1){
    ref.string[i] = value;
  }

  return ref;
}
function FreeStringReference(stringReference){
  delete(stringReference.string);
  delete(stringReference);
}
function CreateStringArrayReference(strings){
  var ref;

  ref = {};
  ref.stringArray = strings;

  return ref;
}
function CreateStringArrayReferenceLengthValue(length, value){
  var ref;
  var i;

  ref = {};
  ref.stringArray = [];
  ref.stringArray.length = length;

  for(i = 0; i < length; i = i + 1){
    ref.stringArray[i] = CreateStringReference(value);
  }

  return ref;
}
function FreeStringArrayReference(stringArrayReference){
  var i;

  for(i = 0; i < stringArrayReference.stringArray.length; i = i + 1){
    delete(stringArrayReference.stringArray[i]);
  }
  delete(stringArrayReference.stringArray);
  delete(stringArrayReference);
}
function DigitDataBase16(){
  return "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe891412108153069c4ffffffffffffffffffffffffffffffffffffffff9409000000000000000049e7ffffffffffffffffffffffffffffffffff61000000000000000000000017ddffffffffffffffffffffffffffffff840000000573d3f5e5a62b00000028f0ffffffffffffffffffffffffffda04000008bcfffffffffff44200000073ffffffffffffffffffffffffff5700000088ffffffffffffffe812000008e3ffffffffffffffffffffffea02000015f9ffffffffffffffff8100000080ffffffffffffffffffffff9c00000072ffffffffffffffffffe40100002fffffffffffffffffffffff51000000b8ffffffffffffffffffff2a000000e2ffffffffffffffffffff21000001f0ffffffffffffffffffff65000000b3fffffffffffffffffff602000018ffffffffffffffffffffff8b0000008affffffffffffffffffd200000036ffffffffffffffffffffffa900000063ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffb500000057ffffffffffffffffffffffc900000046ffffffffffffffffffa90000005fffffffffffffffffffffffd20000003affffffffffffffffffa900000060ffffffffffffffffffffffd30000003affffffffffffffffffb400000057ffffffffffffffffffffffca00000046ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffd100000037ffffffffffffffffffffffa900000063fffffffffffffffffff602000019ffffffffffffffffffffff8b00000089ffffffffffffffffffff21000001f1ffffffffffffffffffff66000000b3ffffffffffffffffffff50000000b8ffffffffffffffffffff2a000000e1ffffffffffffffffffff9c00000073ffffffffffffffffffe40100002fffffffffffffffffffffffea02000015f9ffffffffffffffff8200000080ffffffffffffffffffffffff5700000088ffffffffffffffe812000008e2ffffffffffffffffffffffffda04000008bcfffffffffff44300000073ffffffffffffffffffffffffffff830000000674d3f6e6a72b00000028f0ffffffffffffffffffffffffffffff60000000000000000000000016ddfffffffffffffffffffffffffffffffffe9309000000000000000048e6ffffffffffffffffffffffffffffffffffffffe88f3f1f07132e68c3fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9d7b28e69441f02000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6a274c7095b9de64000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000affffffffffffffffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffd48b56271005142a5ea0f6ffffffffffffffffffffffffffffffffdb7c20000000000000000000001392feffffffffffffffffffffffffffff1f00000000000000000000000000004cf9ffffffffffffffffffffffffff1f0000003784c7e7f9e8b1480000000056ffffffffffffffffffffffffff1f015accffffffffffffffff9701000000b0ffffffffffffffffffffffff58caffffffffffffffffffffff770000003cfffffffffffffffffffffffffffffffffffffffffffffffffff107000002edffffffffffffffffffffffffffffffffffffffffffffffffff3a000000ccffffffffffffffffffffffffffffffffffffffffffffffffff4c000000baffffffffffffffffffffffffffffffffffffffffffffffffff32000000cbffffffffffffffffffffffffffffffffffffffffffffffffec05000002edffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffeb140000009affffffffffffffffffffffffffffffffffffffffffffffff520000002afbffffffffffffffffffffffffffffffffffffffffffffff8c00000003c7ffffffffffffffffffffffffffffffffffffffffffffffb30300000085ffffffffffffffffffffffffffffffffffffffffffffffc50a0000005dfeffffffffffffffffffffffffffffffffffffffffffffd2110000004efbffffffffffffffffffffffffffffffffffffffffffffdb1800000042f8ffffffffffffffffffffffffffffffffffffffffffffe21f00000039f3ffffffffffffffffffffffffffffffffffffffffffffe92600000030efffffffffffffffffffffffffffffffffffffffffffffee2e00000029eafffffffffffffffffffffffffffffffffffffffffffff33700000022e5fffffffffffffffffffffffffffffffffffffffffffff7410000001cdffffffffffffffffffffffffffffffffffffffffffffffb4c00000017d9fffffffffffffffffffffffffffffffffffffffffffffd5900000012d2ffffffffffffffffffffffffffffffffffffffffffffff680000000ecbffffffffffffffffffffffffffffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe2af8058392817060a1a3f74c8ffffffffffffffffffffffffffffffffeb0000000000000000000000000036cfffffffffffffffffffffffffffffeb000000000000000000000000000004a7ffffffffffffffffffffffffffeb00000f5a9dd0edfbf0ca841900000003c2ffffffffffffffffffffffffec3da8f9fffffffffffffffff0410000002bffffffffffffffffffffffffffffffffffffffffffffffffffee12000000cbffffffffffffffffffffffffffffffffffffffffffffffffff6900000090ffffffffffffffffffffffffffffffffffffffffffffffffff9600000078ffffffffffffffffffffffffffffffffffffffffffffffffff9a0000007effffffffffffffffffffffffffffffffffffffffffffffffff73000000a5fffffffffffffffffffffffffffffffffffffffffffffffff51b000009edfffffffffffffffffffffffffffffffffffffffffffffff7540000007efffffffffffffffffffffffffffffffffffffffffff3d3912400000055fcffffffffffffffffffffffffffffffffff1700000000000000001692feffffffffffffffffffffffffffffffffffff17000000000000002db8feffffffffffffffffffffffffffffffffffffff170000000000000000002bc3fffffffffffffffffffffffffffffffffffffffffffdf0cf922e00000003a5fffffffffffffffffffffffffffffffffffffffffffffffffd8700000007d1ffffffffffffffffffffffffffffffffffffffffffffffffff780000004ffffffffffffffffffffffffffffffffffffffffffffffffffff308000006f6ffffffffffffffffffffffffffffffffffffffffffffffffff3c000000d0ffffffffffffffffffffffffffffffffffffffffffffffffff4d000000c6ffffffffffffffffffffffffffffffffffffffffffffffffff35000000ddffffffffffffffffffffffffffffffffffffffffffffffffea0300000bf9ffffffffffffffffffffffffffffffffffffffffffffffff6200000054ffffffffffffffffffffff47bafefffffffffffffffffff56b00000002cbffffffffffffffffffffff0b001e71a9d7edfbf6e4ba771a000000007cffffffffffffffffffffffff0b0000000000000000000000000000017dffffffffffffffffffffffffff0b000000000000000000000000003cc8ffffffffffffffffffffffffffffe9b989593827160608162a5689dbffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffbd0100000000f3fffffffffffffffffffffffffffffffffffffffffffff3200000000000f3ffffffffffffffffffffffffffffffffffffffffffff69000000000000f3ffffffffffffffffffffffffffffffffffffffffffbf01000b0e000000f3fffffffffffffffffffffffffffffffffffffffff42100008e1f000000f3ffffffffffffffffffffffffffffffffffffffff6a000035fc1f000000f3ffffffffffffffffffffffffffffffffffffffc0010004d1ff1f000000f3fffffffffffffffffffffffffffffffffffff42200007affff1f000000f3ffffffffffffffffffffffffffffffffffff6c000026f7ffff1f000000f3ffffffffffffffffffffffffffffffffffc1010001c1ffffff1f000000f3fffffffffffffffffffffffffffffffff523000066ffffffff1f000000f3ffffffffffffffffffffffffffffffff6d000019f0ffffffff1f000000f3ffffffffffffffffffffffffffffffc2010000aeffffffffff1f000000f3fffffffffffffffffffffffffffff524000052ffffffffffff1f000000f3ffffffffffffffffffffffffffff6e00000fe6ffffffffffff1f000000f3ffffffffffffffffffffffffffc30200009affffffffffffff1f000000f3fffffffffffffffffffffffff62400003ffeffffffffffffff1f000000f3ffffffffffffffffffffffff70000008daffffffffffffffff1f000000f3fffffffffffffffffffffff602000086ffffffffffffffffff1f000000f3fffffffffffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f000008672f120514275997efffffffffffffffffffffffffffffffffff4f00000000000000000000000b73f6ffffffffffffffffffffffffffffff4f000000000000000000000000002bdeffffffffffffffffffffffffffff60538cbad2e7faf0d599370000000025ebffffffffffffffffffffffffffffffffffffffffffffffffa0090000005bffffffffffffffffffffffffffffffffffffffffffffffffffb100000001d2ffffffffffffffffffffffffffffffffffffffffffffffffff560000007effffffffffffffffffffffffffffffffffffffffffffffffffb80000003dffffffffffffffffffffffffffffffffffffffffffffffffffec00000022fffffffffffffffffffffffffffffffffffffffffffffffffffd00000011ffffffffffffffffffffffffffffffffffffffffffffffffffec00000022ffffffffffffffffffffffffffffffffffffffffffffffffffb80000003cffffffffffffffffffffffffffffffffffffffffffffffffff580000007dffffffffffffffffffffffffffffffffffffffffffffffffb301000000cfffffffffffffffffffffff4cb1fdffffffffffffffffffa40a00000058ffffffffffffffffffffffff17001a6ea9d7eefbf2d69b380000000024e8ffffffffffffffffffffffff1700000000000000000000000000002de0ffffffffffffffffffffffffff17000000000000000000000000127ef9ffffffffffffffffffffffffffffebba8a59372615050a1a3569a6f7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffca753915050d233866a3e0ffffffffffffffffffffffffffffffffffd13f0000000000000000000000f7ffffffffffffffffffffffffffffff9d07000000000000000000000000f7ffffffffffffffffffffffffffff9700000000469fdbf3f5da9e490100f7ffffffffffffffffffffffffffca0300000eb3ffffffffffffffffd84df8fffffffffffffffffffffffffa2d000007c8ffffffffffffffffffffffffffffffffffffffffffffffff9100000081ffffffffffffffffffffffffffffffffffffffffffffffffff28000010f6ffffffffffffffffffffffffffffffffffffffffffffffffc20000006affffffffffffffffffffffffffffffffffffffffffffffffff79000000b2ffffffffffffffffffffffffffffffffffffffffffffffffff43000000ebffeb903d1a0616306fc0ffffffffffffffffffffffffffffff0f000015ffa211000000000000000041dcfffffffffffffffffffffffff30000003087000000000000000000000013c6ffffffffffffffffffffffe30000000f00000055beeef7d8881000000017e6ffffffffffffffffffffd30000000000019dffffffffffffe12200000056ffffffffffffffffffffd100000000006effffffffffffffffce04000002dbffffffffffffffffffdd0000000006eaffffffffffffffffff550000008bffffffffffffffffffe90000000043ffffffffffffffffffffa90000004dfffffffffffffffffff80200000074ffffffffffffffffffffdb0000002cffffffffffffffffffff2200000088ffffffffffffffffffffef00000019ffffffffffffffffffff4d00000088ffffffffffffffffffffee0000001affffffffffffffffffff7e00000074ffffffffffffffffffffdb0000002dffffffffffffffffffffcd00000042ffffffffffffffffffffa900000052ffffffffffffffffffffff21000005e9ffffffffffffffffff5400000093ffffffffffffffffffffff8f0000006dffffffffffffffffcd04000007e6fffffffffffffffffffffff9220000019effffffffffffe1230000006cffffffffffffffffffffffffffc00600000056beeff8d888110000002af3ffffffffffffffffffffffffffffa603000000000000000000000026ddffffffffffffffffffffffffffffffffc8280000000000000000025deffffffffffffffffffffffffffffffffffffffab25a2a1106193b7ed7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff47000000000000000000000000000000000000f7ffffffffffffffffffff47000000000000000000000000000000000003faffffffffffffffffffff4700000000000000000000000000000000004afffffffffffffffffffffffffffffffffffffffffffffffffc1a000000adffffffffffffffffffffffffffffffffffffffffffffffffb300000015faffffffffffffffffffffffffffffffffffffffffffffffff5100000073ffffffffffffffffffffffffffffffffffffffffffffffffea05000000d6ffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffff2c0000009dffffffffffffffffffffffffffffffffffffffffffffffffc90000000cf3ffffffffffffffffffffffffffffffffffffffffffffffff6700000063fffffffffffffffffffffffffffffffffffffffffffffffff60f000000c6ffffffffffffffffffffffffffffffffffffffffffffffffa300000029ffffffffffffffffffffffffffffffffffffffffffffffffff410000008cffffffffffffffffffffffffffffffffffffffffffffffffdf01000005e9ffffffffffffffffffffffffffffffffffffffffffffffff7d00000052fffffffffffffffffffffffffffffffffffffffffffffffffd1e000000b5ffffffffffffffffffffffffffffffffffffffffffffffffb90000001bfcffffffffffffffffffffffffffffffffffffffffffffffff570000007bffffffffffffffffffffffffffffffffffffffffffffffffee07000001ddffffffffffffffffffffffffffffffffffffffffffffffff9300000042ffffffffffffffffffffffffffffffffffffffffffffffffff31000000a5ffffffffffffffffffffffffffffffffffffffffffffffffd000000010f7ffffffffffffffffffffffffffffffffffffffffffffffff6d0000006bfffffffffffffffffffffffffffffffffffffffffffffffff913000000ceffffffffffffffffffffffffffffffffffffffffffffffffa900000031ffffffffffffffffffffffffffffffffffffffffffffffffff4700000094ffffffffffffffffffffffffffffffffffffffffffffffffe302000008eeffffffffffffffffffffffffffffffffffffffffffffffff840000005afffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9a8602c13050c1d4882dfffffffffffffffffffffffffffffffffffffa918000000000000000000025eeeffffffffffffffffffffffffffffff780000000000000000000000000023e5ffffffffffffffffffffffffff9f0000000037a8e4faf1c66d0500000033fdfffffffffffffffffffffff81600000065fdffffffffffffc40a0000009fffffffffffffffffffffffb600000021faffffffffffffffff8d00000047ffffffffffffffffffffff820000007bffffffffffffffffffeb01000014ffffffffffffffffffffff6d000000a2ffffffffffffffffffff15000001fdffffffffffffffffffff76000000a2ffffffffffffffffffff14000007ffffffffffffffffffffffa10000007bffffffffffffffffffec01000033ffffffffffffffffffffffec08000022fbffffffffffffffff8e00000087ffffffffffffffffffffffff7d00000068fdffffffffffffc70b00001ef2fffffffffffffffffffffffffb5500000039aae5fbf2c87006000013d0fffffffffffffffffffffffffffffe93160000000000000000000153e3ffffffffffffffffffffffffffffffffffbd2e000000000000000780f0ffffffffffffffffffffffffffffffffce3500000000000000000000000e87fcffffffffffffffffffffffffffb3060000004fb2e6faf0cd82150000004ffaffffffffffffffffffffffda0b000004a9ffffffffffffffe93600000076ffffffffffffffffffffff5600000084ffffffffffffffffffe80e000005e2fffffffffffffffffff606000008f4ffffffffffffffffffff6f0000008dffffffffffffffffffcb00000039ffffffffffffffffffffffac0000005cffffffffffffffffffbc0000004affffffffffffffffffffffbe0000004dffffffffffffffffffcc00000039ffffffffffffffffffffffac0000005effffffffffffffffffea00000008f4ffffffffffffffffffff6e0000007cffffffffffffffffffff2f00000085ffffffffffffffffffe70d000000c1ffffffffffffffffffff9300000004a9ffffffffffffffe83400000028fcfffffffffffffffffffffa2d0000000050b2e7fbf2cd821400000002b8ffffffffffffffffffffffffe523000000000000000000000000000299fffffffffffffffffffffffffffff16605000000000000000000002cc5ffffffffffffffffffffffffffffffffffe88e542512040b1b3d72c1fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff8a259251008203f8be2ffffffffffffffffffffffffffffffffffffffa91d0000000000000000047ffaffffffffffffffffffffffffffffffff7b00000000000000000000000040f8ffffffffffffffffffffffffffff94000000004db9ecf7da8b1300000057ffffffffffffffffffffffffffdc050000008fffffffffffffe527000000acffffffffffffffffffffffff630000005fffffffffffffffffd406000025fbfffffffffffffffffffffb0c000002e0ffffffffffffffffff5f000000b2ffffffffffffffffffffc600000036ffffffffffffffffffffb50000005fffffffffffffffffffffa000000068ffffffffffffffffffffe700000011feffffffffffffffffff8d0000007cfffffffffffffffffffffb00000000dfffffffffffffffffff8c0000007cfffffffffffffffffffffb00000000b4ffffffffffffffffff9e00000069ffffffffffffffffffffe7000000008dffffffffffffffffffbe00000038ffffffffffffffffffffb6000000007bfffffffffffffffffff606000003e2ffffffffffffffffff62000000006fffffffffffffffffffff4f00000064ffffffffffffffffd8080000000062ffffffffffffffffffffc50000000096ffffffffffffe82b000000000064ffffffffffffffffffffff6c0000000051bbeff8dc8e1500001000000074fffffffffffffffffffffff94f0000000000000000000000288c00000084fffffffffffffffffffffffffd810b000000000000000052ea830000009fffffffffffffffffffffffffffffea8d471d090d2864c1ffff5b000000d4ffffffffffffffffffffffffffffffffffffffffffffffffff2100000dfdffffffffffffffffffffffffffffffffffffffffffffffffd900000052ffffffffffffffffffffffffffffffffffffffffffffffffff75000000b8ffffffffffffffffffffffffffffffffffffffffffffffffe30d000023fefffffffffffffffffffffffffffffffffffffffffffffff945000000b7ffffffffffffffffffffffffff7fa2fdffffffffffffffe8480000005effffffffffffffffffffffffffff63002080c4ecfae7c0740e00000034f4ffffffffffffffffffffffffffff6300000000000000000000000043f0ffffffffffffffffffffffffffffff6300000000000000000000118efdfffffffffffffffffffffffffffffffff4bb7f462b15040b25569ff4ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff".split('');
}
function DrawDigitCharacter(image, topx, topy, digit){
  var x, y;
  var allCharData, colorChars;
  var colorReference;
  var errorMessage;
  var color;

  colorReference = {};
  errorMessage = {};
  color = {};

  colorChars = [];
  colorChars.length = 2;

  allCharData = DigitDataBase16();

  for(y = 0; y < 37; y = y + 1){
    for(x = 0; x < 30; x = x + 1){
      colorChars[0] = allCharData[digit*30*37*2 + y*2*30 + x*2 + 0];
      colorChars[1] = allCharData[digit*30*37*2 + y*2*30 + x*2 + 1];

      ToUpperCase(colorChars);
      CreateNumberFromStringWithCheck(colorChars, 16, colorReference, errorMessage);
      color.r = colorReference.numberValue/255;
      color.g = colorReference.numberValue/255;
      color.b = colorReference.numberValue/255;
      color.a = 1;
      SetPixel(image, topx + x, topy + y, color);
    }
  }
}
function GetPixelFontData(){
  return "0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000".split('');
}
function DrawAsciiCharacter(image, topx, topy, a, color){
  var index, x, y, row, pixel;
  var allCharData, charData, rowData;
  var rowReference;
  var errorMessage;

  rowReference = {};
  errorMessage = {};

  index = a.charCodeAt(0);
  index = index - 32;
  allCharData = GetPixelFontData();
  charData = Substring(allCharData, index*2*13, (index + 1)*2*13);

  for(y = 0; y < 13; y = y + 1){
    rowData = Substring(charData, y*2, (y + 1)*2);
    ToUpperCase(rowData);
    CreateNumberFromStringWithCheck(rowData, 16, rowReference, errorMessage);
    row = rowReference.numberValue;
    for(x = 0; x < 8; x = x + 1){
      pixel = Math.floor(row/Math.pow(2, x))%2;
      if(pixel == 1){
        DrawPixel(image, topx + 8 - 1 - x, topy + 13 - 1 - y, color);
      }
    }
  }
}
function GetTextWidth(text){
  var charWidth, spacing, width;

  charWidth = 8;
  spacing = 2;

  if(text.length == 0){
    width = 0;
  }else{
    width = text.length*charWidth + (text.length - 1)*spacing;
  }

  return width;
}
function GetTextHeight(text){
  return 13;
}
function AssertFalse(b, failures){
  if(b){
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertTrue(b, failures){
  if( !b ){
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertEquals(a, b, failures){
  if(a != b){
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertBooleansEqual(a, b, failures){
  if(a != b){
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertCharactersEqual(a, b, failures){
  if(a != b){
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertStringEquals(a, b, failures){
  if( !aStringsEqual(a, b) ){
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertNumberArraysEqual(a, b, failures){
  var i;

  if(a.length == b.length){
    for(i = 0; i < a.length; i = i + 1){
      AssertEquals(a[i], b[i], failures);
    }
  }else{
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertBooleanArraysEqual(a, b, failures){
  var i;

  if(a.length == b.length){
    for(i = 0; i < a.length; i = i + 1){
      AssertBooleansEqual(a[i], b[i], failures);
    }
  }else{
    failures.numberValue = failures.numberValue + 1;
  }
}
function AssertStringArraysEqual(a, b, failures){
  var i;

  if(a.length == b.length){
    for(i = 0; i < a.length; i = i + 1){
      AssertStringEquals(a[i].string, b[i].string, failures);
    }
  }else{
    failures.numberValue = failures.numberValue + 1;
  }
}
function ConvertToPNG(image){
  return ConvertToPNGWithOptions(image, 6, false, 0, 0.001);
}
function ConvertToPNGGrayscale(image){
  return ConvertToPNGWithOptions(image, 0, false, 0, 0.001);
}
function PysicsHeader(pixelsPerMeter){
  var phys;

  phys = {};

  phys.pixelsPerMeter = pixelsPerMeter;

  return phys;
}
function ConvertToPNGWithOptions(image, colorType, setPhys, pixelsPerMeter, compressionLevel){
  var png;
  var pngData, colorData;

  png = {};

  png.signature = PNGSignature();

  png.ihdr = PNGHeader(image, colorType);

  png.physPresent = setPhys;
  png.phys = PysicsHeader(pixelsPerMeter);

  if(colorType == 6){
    colorData = GetPNGColorData(image);
  }else{
    colorData = GetPNGColorDataGreyscale(image);
  }
  png.zlibStruct = ZLibCompressStaticHuffman(colorData, compressionLevel);

  pngData = PNGSerializeChunks(png);

  return pngData;
}
function PNGSerializeChunks(png){
  var length, i, chunkLength;
  var data;
  var position;

  length = png.signature.length + 12 + PNGHeaderLength() + 12 + PNGIDATLength(png) + 12;
  if(png.physPresent){
    length = length + 4 + 4 + 1 + 12;
  }
  data = [];
  data.length = length;
  position = CreateNumberReference(0);

  /* Signature */
  for(i = 0; i < png.signature.length; i = i + 1){
    WriteByte(data, png.signature[i], position);
  }

  /* Header */
  chunkLength = PNGHeaderLength();
  Write4BytesBE(data, chunkLength, position);
  WriteStringBytes(data, "IHDR".split(''), position);
  Write4BytesBE(data, png.ihdr.Width, position);
  Write4BytesBE(data, png.ihdr.Height, position);
  WriteByte(data, png.ihdr.BitDepth, position);
  WriteByte(data, png.ihdr.ColourType, position);
  WriteByte(data, png.ihdr.CompressionMethod, position);
  WriteByte(data, png.ihdr.FilterMethod, position);
  WriteByte(data, png.ihdr.InterlaceMethod, position);
  Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4, chunkLength + 4), position);

  /* pHYs */
  if(png.physPresent){
    chunkLength = 4 + 4 + 1;
    Write4BytesBE(data, chunkLength, position);
    WriteStringBytes(data, "pHYs".split(''), position);

    Write4BytesBE(data, png.phys.pixelsPerMeter, position);
    Write4BytesBE(data, png.phys.pixelsPerMeter, position);
    WriteByte(data, 1, position);
    /* 1 = pixels per meter */
    Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4, chunkLength + 4), position);
  }

  /* IDAT */
  chunkLength = PNGIDATLength(png);
  Write4BytesBE(data, chunkLength, position);
  WriteStringBytes(data, "IDAT".split(''), position);
  WriteByte(data, png.zlibStruct.CMF, position);
  WriteByte(data, png.zlibStruct.FLG, position);
  for(i = 0; i < png.zlibStruct.CompressedDataBlocks.length; i = i + 1){
    WriteByte(data, png.zlibStruct.CompressedDataBlocks[i], position);
  }
  Write4BytesBE(data, png.zlibStruct.Adler32CheckValue, position);
  Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4, chunkLength + 4), position);

  /* IEND */
  chunkLength = 0;
  Write4BytesBE(data, chunkLength, position);
  WriteStringBytes(data, "IEND".split(''), position);
  Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - 4, 4), position);

  return data;
}
function PNGIDATLength(png){
  return 2 + png.zlibStruct.CompressedDataBlocks.length + 4;
}
function PNGHeaderLength(){
  return 4 + 4 + 1 + 1 + 1 + 1 + 1;
}
function GetPNGColorData(image){
  var colordata;
  var length, x, y, next;
  var rgba;

  length = 4*ImageWidth(image)*ImageHeight(image) + ImageHeight(image);

  colordata = [];
  colordata.length = length;

  next = 0;

  for(y = 0; y < ImageHeight(image); y = y + 1){
    colordata[next] = 0;
    next = next + 1;
    for(x = 0; x < ImageWidth(image); x = x + 1){
      rgba = image.x[x].y[y];
      colordata[next] = Round(rgba.r*255);
      next = next + 1;
      colordata[next] = Round(rgba.g*255);
      next = next + 1;
      colordata[next] = Round(rgba.b*255);
      next = next + 1;
      colordata[next] = Round(rgba.a*255);
      next = next + 1;
    }
  }

  return colordata;
}
function GetPNGColorDataGreyscale(image){
  var colordata;
  var length, x, y, next;
  var rgba;

  length = ImageWidth(image)*ImageHeight(image) + ImageHeight(image);

  colordata = [];
  colordata.length = length;

  next = 0;

  for(y = 0; y < ImageHeight(image); y = y + 1){
    colordata[next] = 0;
    next = next + 1;
    for(x = 0; x < ImageWidth(image); x = x + 1){
      rgba = image.x[x].y[y];
      colordata[next] = Round(rgba.r*255);
      next = next + 1;
    }
  }

  return colordata;
}
function PNGHeader(image, colortype){
  var ihdr;

  ihdr = {};
  ihdr.Width = ImageWidth(image);
  ihdr.Height = ImageHeight(image);
  /* Truecolour with alpha */
  ihdr.BitDepth = 8;
  ihdr.ColourType = colortype;
  ihdr.FilterMethod = 0;
  /* None */
  ihdr.CompressionMethod = 0;
  /* zlib */
  ihdr.InterlaceMethod = 0;
  /* no interlace */
  return ihdr;
}
function PNGSignature(){
  var s;

  s = [];
  s.length = 8;
  s[0] = 137;
  s[1] = 80;
  s[2] = 78;
  s[3] = 71;
  s[4] = 13;
  s[5] = 10;
  s[6] = 26;
  s[7] = 10;

  return s;
}
function PNGReadDataChunks(cs){
  var i, j, length, zlibpos;
  var c;
  var zlibData;

  length = 0;
  for(i = 0; i < cs.length; i = i + 1){
    c = cs[i];
    if(aStringsEqual(c.type, "IDAT".split(''))){
      length = length + c.length;
    }
  }

  zlibData = [];
  zlibData.length = length;
  zlibpos = 0;

  for(i = 0; i < cs.length; i = i + 1){
    c = cs[i];
    if(aStringsEqual(c.type, "IDAT".split(''))){
      for(j = 0; j < c.length; j = j + 1){
        zlibData[zlibpos] = c.data[j];
        zlibpos = zlibpos + 1;
      }
    }
  }

  return zlibData;
}
function PNGReadHeader(image, cs, errorMessages){
  var i;
  var ihdr;
  var c;
  var position;
  var n;
  var success;

  position = CreateNumberReference(0);
  success = false;

  for(i = 0; i < cs.length; i = i + 1){
    c = cs[i];
    if(aStringsEqual(c.type, "IHDR".split(''))){
      ihdr = {};

      ihdr.Width = Read4bytesBE(c.data, position);
      ihdr.Height = Read4bytesBE(c.data, position);
      ihdr.BitDepth = ReadByte(c.data, position);
      ihdr.ColourType = ReadByte(c.data, position);
      ihdr.CompressionMethod = ReadByte(c.data, position);
      ihdr.FilterMethod = ReadByte(c.data, position);
      ihdr.InterlaceMethod = ReadByte(c.data, position);

      n = CreateImage(ihdr.Width, ihdr.Height, GetTransparent());
      image.x = n.x;

      if(ihdr.ColourType == 6){
        if(ihdr.BitDepth == 8){
          if(ihdr.CompressionMethod == 0){
            if(ihdr.FilterMethod == 0){
              if(ihdr.InterlaceMethod == 0){
                success = true;
              }else{
                success = false;
                errorMessages.string = AppendString(errorMessages.string, "Interlace method not supported.".split(''));
              }
            }else{
              success = false;
              errorMessages.string = AppendString(errorMessages.string, "Filter method not supported.".split(''));
            }
          }else{
            success = false;
            errorMessages.string = AppendString(errorMessages.string, "Compression type not supported.".split(''));
          }
        }else{
          success = false;
          errorMessages.string = AppendString(errorMessages.string, "Bit depth not supported.".split(''));
        }
      }else{
        success = false;
        errorMessages.string = AppendString(errorMessages.string, "Color type not supported.".split(''));
      }
    }
  }

  return success;
}
function PNGReadChunks(data, position){
  var done;
  var prepos;
  var chunks;
  var c;
  var cs;
  var i;
  done = false;
  prepos = position.numberValue;
  for(chunks = 0;  !done ; chunks = chunks + 1){
    c = PNGReadChunk(data, position);
    if(aStringsEqual(c.type, "IEND".split(''))){
      done = true;
    }
  }
  position.numberValue = prepos;
  cs = [];
  cs.length = chunks;
  for(i = 0; i < chunks; i = i + 1){
    cs[i] = PNGReadChunk(data, position);
  }

  return cs;
}
function PNGReadChunk(data, position){
  var c;

  c = {};

  c.length = Read4bytesBE(data, position);
  c.type = [];
  c.type.length = 4;
  c.type[0] = String.fromCharCode(ReadByte(data, position));
  c.type[1] = String.fromCharCode(ReadByte(data, position));
  c.type[2] = String.fromCharCode(ReadByte(data, position));
  c.type[3] = String.fromCharCode(ReadByte(data, position));
  c.data = ReadXbytes(data, position, c.length);
  c.crc = Read4bytesBE(data, position);

  return c;
}
function WriteStringToStingStream(stream, index, src){
  var i;

  for(i = 0; i < src.length; i = i + 1){
    stream[index.numberValue + i] = src[i];
  }
  index.numberValue = index.numberValue + src.length;
}
function WriteCharacterToStingStream(stream, index, src){
  stream[index.numberValue] = src;
  index.numberValue = index.numberValue + 1;
}
function WriteBooleanToStingStream(stream, index, src){
  if(src){
    WriteStringToStingStream(stream, index, "true".split(''));
  }else{
    WriteStringToStingStream(stream, index, "false".split(''));
  }
}
function SubstringWithCheck(string, from, to, stringReference){
  var success;

  if(from >= 0 && from <= string.length && to >= 0 && to <= string.length && from <= to){
    stringReference.string = Substring(string, from, to);
    success = true;
  }else{
    success = false;
  }

  return success;
}
function Substring(string, from, to){
  var n;
  var i, length;

  length = to - from;

  n = [];
  n.length = length;

  for(i = from; i < to; i = i + 1){
    n[i - from] = string[i];
  }

  return n;
}
function AppendString(s1, s2){
  var newString;

  newString = ConcatenateString(s1, s2);

  delete(s1);

  return newString;
}
function ConcatenateString(s1, s2){
  var newString;
  var i;

  newString = [];
  newString.length = s1.length + s2.length;

  for(i = 0; i < s1.length; i = i + 1){
    newString[i] = s1[i];
  }

  for(i = 0; i < s2.length; i = i + 1){
    newString[s1.length + i] = s2[i];
  }

  return newString;
}
function AppendCharacter(string, c){
  var newString;

  newString = ConcatenateCharacter(string, c);

  delete(string);

  return newString;
}
function ConcatenateCharacter(string, c){
  var newString;
  var i;
  newString = [];
  newString.length = string.length + 1;

  for(i = 0; i < string.length; i = i + 1){
    newString[i] = string[i];
  }

  newString[string.length] = c;

  return newString;
}
function SplitByCharacter(toSplit, splitBy){
  var split;
  var stringToSplitBy;

  stringToSplitBy = [];
  stringToSplitBy.length = 1;
  stringToSplitBy[0] = splitBy;

  split = SplitByString(toSplit, stringToSplitBy);

  delete(stringToSplitBy);

  return split;
}
function IndexOfCharacter(string, character, indexReference){
  var i;
  var found;

  found = false;
  for(i = 0; i < string.length &&  !found ; i = i + 1){
    if(string[i] == character){
      found = true;
      indexReference.numberValue = i;
    }
  }

  return found;
}
function SubstringEqualsWithCheck(string, from, substring, equalsReference){
  var success;

  if(from < string.length){
    success = true;
    equalsReference.booleanValue = SubstringEquals(string, from, substring);
  }else{
    success = false;
  }

  return success;
}
function SubstringEquals(string, from, substring){
  var i;
  var equal;

  equal = true;
  if(string.length - from >= substring.length){
    for(i = 0; i < substring.length && equal; i = i + 1){
      if(string[from + i] != substring[i]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
function IndexOfString(string, substring, indexReference){
  var i;
  var found;

  found = false;
  for(i = 0; i < string.length - substring.length + 1 &&  !found ; i = i + 1){
    if(SubstringEquals(string, i, substring)){
      found = true;
      indexReference.numberValue = i;
    }
  }

  return found;
}
function ContainsCharacter(string, character){
  var i;
  var found;

  found = false;
  for(i = 0; i < string.length &&  !found ; i = i + 1){
    if(string[i] == character){
      found = true;
    }
  }

  return found;
}
function ContainsString(string, substring){
  return IndexOfString(string, substring, {});
}
function ToUpperCase(string){
  var i;

  for(i = 0; i < string.length; i = i + 1){
    string[i] = charToUpperCase(string[i]);
  }
}
function ToLowerCase(string){
  var i;

  for(i = 0; i < string.length; i = i + 1){
    string[i] = charToLowerCase(string[i]);
  }
}
function EqualsIgnoreCase(a, b){
  var equal;
  var i;

  if(a.length == b.length){
    equal = true;
    for(i = 0; i < a.length && equal; i = i + 1){
      if(charToLowerCase(a[i]) != charToLowerCase(b[i])){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
function ReplaceString(string, toReplace, replaceWith){
  var result;
  var i;
  var equalsReference;
  var success;

  equalsReference = {};
  result = [];
  result.length = 0;

  for(i = 0; i < string.length; ){
    success = SubstringEqualsWithCheck(string, i, toReplace, equalsReference);
    if(success){
      success = equalsReference.booleanValue;
    }

    if(success && toReplace.length > 0){
      result = ConcatenateString(result, replaceWith);
      i = i + toReplace.length;
    }else{
      result = ConcatenateCharacter(result, string[i]);
      i = i + 1;
    }
  }

  return result;
}
function ReplaceCharacter(string, toReplace, replaceWith){
  var result;
  var i;

  result = [];
  result.length = 0;

  for(i = 0; i < string.length; i = i + 1){
    if(string[i] == toReplace){
      result = ConcatenateCharacter(result, replaceWith);
    }else{
      result = ConcatenateCharacter(result, string[i]);
    }
  }

  return result;
}
function Trim(string){
  var result;
  var i, lastWhitespaceLocationStart, lastWhitespaceLocationEnd;
  var firstNonWhitespaceFound;

  /* Find whitepaces at the start. */
  lastWhitespaceLocationStart =  -1;
  firstNonWhitespaceFound = false;
  for(i = 0; i < string.length &&  !firstNonWhitespaceFound ; i = i + 1){
    if(charIsWhiteSpace(string[i])){
      lastWhitespaceLocationStart = i;
    }else{
      firstNonWhitespaceFound = true;
    }
  }

  /* Find whitepaces at the end. */
  lastWhitespaceLocationEnd = string.length;
  firstNonWhitespaceFound = false;
  for(i = string.length - 1; i >= 0 &&  !firstNonWhitespaceFound ; i = i - 1){
    if(charIsWhiteSpace(string[i])){
      lastWhitespaceLocationEnd = i;
    }else{
      firstNonWhitespaceFound = true;
    }
  }

  if(lastWhitespaceLocationStart < lastWhitespaceLocationEnd){
    result = Substring(string, lastWhitespaceLocationStart + 1, lastWhitespaceLocationEnd);
  }else{
    result = [];
    result.length = 0;
  }

  return result;
}
function StartsWith(string, start){
  var startsWithString;

  startsWithString = false;
  if(string.length >= start.length){
    startsWithString = SubstringEquals(string, 0, start);
  }

  return startsWithString;
}
function EndsWith(string, end){
  var endsWithString;

  endsWithString = false;
  if(string.length >= end.length){
    endsWithString = SubstringEquals(string, string.length - end.length, end);
  }

  return endsWithString;
}
function SplitByString(toSplit, splitBy){
  var split;
  var next;
  var i;
  var c;
  var n;

  split = [];
  split.length = 0;

  next = [];
  next.length = 0;
  for(i = 0; i < toSplit.length; ){
    c = toSplit[i];

    if(SubstringEquals(toSplit, i, splitBy)){
      n = {};
      n.string = next;
      split = AddString(split, n);
      next = [];
      next.length = 0;
      i = i + splitBy.length;
    }else{
      next = AppendCharacter(next, c);
      i = i + 1;
    }
  }

  n = {};
  n.string = next;
  split = AddString(split, n);

  return split;
}
function StringIsBefore(a, b){
  var before, equal, done;
  var i;

  before = false;
  equal = true;
  done = false;

  if(a.length == 0 && b.length > 0){
    before = true;
  }else{
    for(i = 0; i < a.length && i < b.length &&  !done ; i = i + 1){
      if(a[i] != b[i]){
        equal = false;
      }
      if(charCharacterIsBefore(a[i], b[i])){
        before = true;
      }
      if(charCharacterIsBefore(b[i], a[i])){
        done = true;
      }
    }

    if(equal){
      if(a.length < b.length){
        before = true;
      }
    }
  }

  return before;
}
function ReadXbytes(data, position, length){
  var r;
  var i;

  r = [];
  r.length = length;

  for(i = 0; i < length; i = i + 1){
    r[i] = ReadByte(data, position);
  }

  return r;
}
function Read4bytesBE(data, position){
  var r;

  r = 0;
  r = r + Math.pow(2, 24)*ReadByte(data, position);
  r = r + Math.pow(2, 16)*ReadByte(data, position);
  r = r + Math.pow(2, 8)*ReadByte(data, position);
  r = r + ReadByte(data, position);

  return r;
}
function Read2bytesBE(data, position){
  var r;

  r = 0;
  r = r + Math.pow(2, 8)*ReadByte(data, position);
  r = r + ReadByte(data, position);

  return r;
}
function ReadByte(data, position){
  var next;

  next = data[position.numberValue];
  position.numberValue = position.numberValue + 1;

  return next;
}
function Read4bytesLE(data, position){
  var r;

  r = 0;
  r = r + ReadByte(data, position);
  r = r + Math.pow(2, 8)*ReadByte(data, position);
  r = r + Math.pow(2, 16)*ReadByte(data, position);
  r = r + Math.pow(2, 24)*ReadByte(data, position);

  return r;
}
function WriteByte(data, b, position){
  data[position.numberValue] = b;
  position.numberValue = position.numberValue + 1;
}
function Write2BytesLE(data, b, position){
  data[position.numberValue] = Round(b%Math.pow(2, 8));
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Math.floor(b/Math.pow(2, 8))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
}
function Write4BytesLE(data, b, position){
  data[position.numberValue] = Round(b%Math.pow(2, 8));
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Math.floor(b/Math.pow(2, 8))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Math.floor(b/Math.pow(2, 16))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Math.floor(b/Math.pow(2, 24))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
}
function Write2BytesBE(data, b, position){
  data[position.numberValue] = Math.floor(b/Math.pow(2, 8))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Round(b%Math.pow(2, 8));
  position.numberValue = position.numberValue + 1;
}
function Write4BytesBE(data, b, position){
  data[position.numberValue] = Math.floor(b/Math.pow(2, 24))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Math.floor(b/Math.pow(2, 16))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Math.floor(b/Math.pow(2, 8))%Math.pow(2, 8);
  position.numberValue = position.numberValue + 1;
  data[position.numberValue] = Round(b%Math.pow(2, 8));
  position.numberValue = position.numberValue + 1;
}
function WriteStringBytes(data, cs, position){
  var i, v;

  for(i = 0; i < cs.length; i = i + 1){
    v = cs[i].charCodeAt(0);
    WriteByte(data, v, position);
  }
}
function MakeCRC32Table(){
  var c, n, k;
  var crcTable;

  crcTable = [];
  crcTable.length = 256;

  for(n = 0; n < 256; n = n + 1){
    c = n;
    for(k = 0; k < 8; k = k + 1){
      if( !DivisibleBy(c, 2) ){
        c = Xor4Byte(3988292384, Math.floor(c/2));
      }else{
        c = Math.floor(c/2);
      }
    }
    crcTable[n] = c;
  }

  return crcTable;
}
function UpdateCRC32(crc, buf, crc_table){
  var n, index;

  for(n = 0; n < buf.length; n = n + 1){
    index = And4Byte(Xor4Byte(crc, buf[n]), Math.pow(2, 8) - 1);
    crc = Xor4Byte(crc_table[index], Math.floor(crc/Math.pow(2, 8)));
  }

  return crc;
}
function CalculateCRC32(buf){
  var crcTable;
  var b32max, value;

  crcTable = MakeCRC32Table();

  b32max = Math.pow(2, 32) - 1;
  value = UpdateCRC32(b32max, buf, crcTable);

  return Xor4Byte(value, b32max);
}
function CRC32OfInterval(data, from, length){
  var crcBase;
  var i, crc;

  crcBase = [];
  crcBase.length = length;

  for(i = 0; i < length; i = i + 1){
    crcBase[i] = data[from + i];
  }

  crc = CalculateCRC32(crcBase);

  delete(crcBase);

  return crc;
}
function ZLibCompressNoCompression(data){
  var zlibStruct;

  zlibStruct = {};

  zlibStruct.CMF = 120;
  zlibStruct.FLG = 1;
  zlibStruct.CompressedDataBlocks = DeflateDataNoCompression(data);
  zlibStruct.Adler32CheckValue = ComputeAdler32(data);

  return zlibStruct;
}
function ZLibCompressStaticHuffman(data, level){
  var zlibStruct;

  zlibStruct = {};

  zlibStruct.CMF = 120;
  zlibStruct.FLG = 1;
  zlibStruct.CompressedDataBlocks = DeflateDataStaticHuffman(data, level);
  zlibStruct.Adler32CheckValue = ComputeAdler32(data);

  return zlibStruct;
}
function AddNumber(list, a){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length + 1;
  for(i = 0; i < list.length; i = i + 1){
    newlist[i] = list[i];
  }
  newlist[list.length] = a;
		
  delete(list);
		
  return newlist;
}
function AddNumberRef(list, i){
  list.numberArray = AddNumber(list.numberArray, i);
}
function RemoveNumber(list, n){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length - 1;

  if(n >= 0 && n < list.length){
    for(i = 0; i < list.length; i = i + 1){
      if(i < n){
        newlist[i] = list[i];
      }
      if(i > n){
        newlist[i - 1] = list[i];
      }
    }

    delete(list);
  }else{
    delete(newlist);
  }
		
  return newlist;
}
function GetNumberRef(list, i){
  return list.numberArray[i];
}
function RemoveNumberRef(list, i){
  list.numberArray = RemoveNumber(list.numberArray, i);
}
function AddString(list, a){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length + 1;

  for(i = 0; i < list.length; i = i + 1){
    newlist[i] = list[i];
  }
  newlist[list.length] = a;
		
  delete(list);
		
  return newlist;
}
function AddStringRef(list, i){
  list.stringArray = AddString(list.stringArray, i);
}
function RemoveString(list, n){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length - 1;

  if(n >= 0 && n < list.length){
    for(i = 0; i < list.length; i = i + 1){
      if(i < n){
        newlist[i] = list[i];
      }
      if(i > n){
        newlist[i - 1] = list[i];
      }
    }

    delete(list);
  }else{
    delete(newlist);
  }
		
  return newlist;
}
function GetStringRef(list, i){
  return list.stringArray[i];
}
function RemoveStringRef(list, i){
  list.stringArray = RemoveString(list.stringArray, i);
}
function AddBoolean(list, a){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length + 1;
  for(i = 0; i < list.length; i = i + 1){
    newlist[i] = list[i];
  }
  newlist[list.length] = a;
		
  delete(list);
		
  return newlist;
}
function AddBooleanRef(list, i){
  list.booleanArray = AddBoolean(list.booleanArray, i);
}
function RemoveBoolean(list, n){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length - 1;

  if(n >= 0 && n < list.length){
    for(i = 0; i < list.length; i = i + 1){
      if(i < n){
        newlist[i] = list[i];
      }
      if(i > n){
        newlist[i - 1] = list[i];
      }
    }

    delete(list);
  }else{
    delete(newlist);
  }
		
  return newlist;
}
function GetBooleanRef(list, i){
  return list.booleanArray[i];
}
function RemoveDecimalRef(list, i){
  list.booleanArray = RemoveBoolean(list.booleanArray, i);
}
function CreateLinkedListString(){
  var ll;

  ll = {};
  ll.first = {};
  ll.last = ll.first;
  ll.last.end = true;

  return ll;
}
function LinkedListAddString(ll, value){
  ll.last.end = false;
  ll.last.value = value;
  ll.last.next = {};
  ll.last.next.end = true;
  ll.last = ll.last.next;
}
function LinkedListStringsToArray(ll){
  var array;
  var length, i;
  var node;

  node = ll.first;

  length = LinkedListStringsLength(ll);

  array = [];
  array.length = length;

  for(i = 0; i < length; i = i + 1){
    array[i] = {};
    array[i].string = node.value;
    node = node.next;
  }

  return array;
}
function LinkedListStringsLength(ll){
  var l;
  var node;

  l = 0;
  node = ll.first;
  for(;  !node.end ; ){
    node = node.next;
    l = l + 1;
  }

  return l;
}
function FreeLinkedListString(ll){
  var node, prev;

  node = ll.first;

  for(;  !node.end ; ){
    prev = node;
    node = node.next;
    delete(prev);
  }

  delete(node);
}
function CreateLinkedListNumbers(){
  var ll;

  ll = {};
  ll.first = {};
  ll.last = ll.first;
  ll.last.end = true;

  return ll;
}
function CreateLinkedListNumbersArray(length){
  var lls;
  var i;

  lls = [];
  lls.length = length;
  for(i = 0; i < lls.length; i = i + 1){
    lls[i] = CreateLinkedListNumbers();
  }

  return lls;
}
function LinkedListAddNumber(ll, value){
  ll.last.end = false;
  ll.last.value = value;
  ll.last.next = {};
  ll.last.next.end = true;
  ll.last = ll.last.next;
}
function LinkedListNumbersLength(ll){
  var l;
  var node;

  l = 0;
  node = ll.first;
  for(;  !node.end ; ){
    node = node.next;
    l = l + 1;
  }

  return l;
}
function LinkedListNumbersIndex(ll, index){
  var i;
  var node;

  node = ll.first;
  for(i = 0; i < index; i = i + 1){
    node = node.next;
  }

  return node.value;
}
function LinkedListInsertNumber(ll, index, value){
  var i;
  var node, tmp;

  if(index == 0){
    tmp = ll.first;
    ll.first = {};
    ll.first.next = tmp;
    ll.first.value = value;
    ll.first.end = false;
  }else{
    node = ll.first;
    for(i = 0; i < index - 1; i = i + 1){
      node = node.next;
    }

    tmp = node.next;
    node.next = {};
    node.next.next = tmp;
    node.next.value = value;
    node.next.end = false;
  }
}
function LinkedListSet(ll, index, value){
  var i;
  var node;

  node = ll.first;
  for(i = 0; i < index; i = i + 1){
    node = node.next;
  }

  node.next.value = value;
}
function LinkedListRemoveNumber(ll, index){
  var i;
  var node, prev;

  node = ll.first;
  prev = ll.first;

  for(i = 0; i < index; i = i + 1){
    prev = node;
    node = node.next;
  }

  if(index == 0){
    ll.first = prev.next;
  }
  if( !prev.next.end ){
    prev.next = prev.next.next;
  }
}
function FreeLinkedListNumbers(ll){
  var node, prev;

  node = ll.first;

  for(;  !node.end ; ){
    prev = node;
    node = node.next;
    delete(prev);
  }

  delete(node);
}
function FreeLinkedListNumbersArray(lls){
  var i;

  for(i = 0; i < lls.length; i = i + 1){
    FreeLinkedListNumbers(lls[i]);
  }
  delete(lls);
}
function LinkedListNumbersToArray(ll){
  var array;
  var length, i;
  var node;

  node = ll.first;

  length = LinkedListNumbersLength(ll);

  array = [];
  array.length = length;

  for(i = 0; i < length; i = i + 1){
    array[i] = node.value;
    node = node.next;
  }

  return array;
}
function ArrayToLinkedListNumbers(array){
  var ll;
  var i;

  ll = CreateLinkedListNumbers();

  for(i = 0; i < array.length; i = i + 1){
    LinkedListAddNumber(ll, array[i]);
  }

  return ll;
}
function LinkedListNumbersEqual(a, b){
  var equal, done;
  var an, bn;

  an = a.first;
  bn = b.first;

  equal = true;
  done = false;
  for(; equal &&  !done ; ){
    if(an.end == bn.end){
      if(an.end){
        done = true;
      }else if(an.value == bn.value){
        an = an.next;
        bn = bn.next;
      }else{
        equal = false;
      }
    }else{
      equal = false;
    }
  }

  return equal;
}
function CreateLinkedListCharacter(){
  var ll;

  ll = {};
  ll.first = {};
  ll.last = ll.first;
  ll.last.end = true;

  return ll;
}
function LinkedListAddCharacter(ll, value){
  ll.last.end = false;
  ll.last.value = value;
  ll.last.next = {};
  ll.last.next.end = true;
  ll.last = ll.last.next;
}
function LinkedListCharactersToArray(ll){
  var array;
  var length, i;
  var node;

  node = ll.first;

  length = LinkedListCharactersLength(ll);

  array = [];
  array.length = length;

  for(i = 0; i < length; i = i + 1){
    array[i] = node.value;
    node = node.next;
  }

  return array;
}
function LinkedListCharactersLength(ll){
  var l;
  var node;

  l = 0;
  node = ll.first;
  for(;  !node.end ; ){
    node = node.next;
    l = l + 1;
  }

  return l;
}
function FreeLinkedListCharacter(ll){
  var node, prev;

  node = ll.first;

  for(;  !node.end ; ){
    prev = node;
    node = node.next;
    delete(prev);
  }

  delete(node);
}
function CreateDynamicArrayNumbers(){
  var da;

  da = {};
  da.array = [];
  da.array.length = 10;
  da.length = 0;

  return da;
}
function CreateDynamicArrayNumbersWithInitialCapacity(capacity){
  var da;

  da = {};
  da.array = [];
  da.array.length = capacity;
  da.length = 0;

  return da;
}
function DynamicArrayAddNumber(da, value){
  if(da.length == da.array.length){
    DynamicArrayNumbersIncreaseSize(da);
  }

  da.array[da.length] = value;
  da.length = da.length + 1;
}
function DynamicArrayNumbersIncreaseSize(da){
  var newLength, i;
  var newArray;

  newLength = Math.round(da.array.length*3/2);
  newArray = [];
  newArray.length = newLength;

  for(i = 0; i < da.array.length; i = i + 1){
    newArray[i] = da.array[i];
  }

  delete(da.array);

  da.array = newArray;
}
function DynamicArrayNumbersDecreaseSizeNecessary(da){
  var needsDecrease;

  needsDecrease = false;

  if(da.length > 10){
    needsDecrease = da.length <= Math.round(da.array.length*2/3);
  }

  return needsDecrease;
}
function DynamicArrayNumbersDecreaseSize(da){
  var newLength, i;
  var newArray;

  newLength = Math.round(da.array.length*2/3);
  newArray = [];
  newArray.length = newLength;

  for(i = 0; i < newLength; i = i + 1){
    newArray[i] = da.array[i];
  }

  delete(da.array);

  da.array = newArray;
}
function DynamicArrayNumbersIndex(da, index){
  return da.array[index];
}
function DynamicArrayNumbersLength(da){
  return da.length;
}
function DynamicArrayInsertNumber(da, index, value){
  var i;

  if(da.length == da.array.length){
    DynamicArrayNumbersIncreaseSize(da);
  }

  for(i = da.length; i > index; i = i - 1){
    da.array[i] = da.array[i - 1];
  }

  da.array[index] = value;

  da.length = da.length + 1;
}
function DynamicArraySet(da, index, value){
  da.array[index] = value;
}
function DynamicArrayRemoveNumber(da, index){
  var i;

  for(i = index; i < da.length - 1; i = i + 1){
    da.array[i] = da.array[i + 1];
  }

  da.length = da.length - 1;

  if(DynamicArrayNumbersDecreaseSizeNecessary(da)){
    DynamicArrayNumbersDecreaseSize(da);
  }
}
function FreeDynamicArrayNumbers(da){
  delete(da.array);
  delete(da);
}
function DynamicArrayNumbersToArray(da){
  var array;
  var i;

  array = [];
  array.length = da.length;

  for(i = 0; i < da.length; i = i + 1){
    array[i] = da.array[i];
  }

  return array;
}
function ArrayToDynamicArrayNumbersWithOptimalSize(array){
  var da;
  var i;
  var c, n, newCapacity;

  /*
         c = 10*(3/2)^n
         log(c) = log(10*(3/2)^n)
         log(c) = log(10) + log((3/2)^n)
         log(c) = 1 + log((3/2)^n)
         log(c) - 1 = log((3/2)^n)
         log(c) - 1 = n*log(3/2)
         n = (log(c) - 1)/log(3/2)
         */
  c = array.length;
  n = (Math.log(c) - 1)/Math.log(3/2);
  newCapacity = Math.floor(n) + 1;

  da = CreateDynamicArrayNumbersWithInitialCapacity(newCapacity);

  for(i = 0; i < array.length; i = i + 1){
    da.array[i] = array[i];
  }

  return da;
}
function ArrayToDynamicArrayNumbers(array){
  var da;

  da = {};
  da.array = aCopyNumberArray(array);
  da.length = array.length;

  return da;
}
function DynamicArrayNumbersEqual(a, b){
  var equal;
  var i;

  equal = true;
  if(a.length == b.length){
    for(i = 0; i < a.length && equal; i = i + 1){
      if(a.array[i] != b.array[i]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
function DynamicArrayNumbersToLinkedList(da){
  var ll;
  var i;

  ll = CreateLinkedListNumbers();

  for(i = 0; i < da.length; i = i + 1){
    LinkedListAddNumber(ll, da.array[i]);
  }

  return ll;
}
function LinkedListToDynamicArrayNumbers(ll){
  var da;
  var i;
  var node;

  node = ll.first;

  da = {};
  da.length = LinkedListNumbersLength(ll);

  da.array = [];
  da.array.length = da.length;

  for(i = 0; i < da.length; i = i + 1){
    da.array[i] = node.value;
    node = node.next;
  }

  return da;
}
function AddCharacter(list, a){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length + 1;
  for(i = 0; i < list.length; i = i + 1){
    newlist[i] = list[i];
  }
  newlist[list.length] = a;
		
  delete(list);
		
  return newlist;
}
function AddCharacterRef(list, i){
  list.string = AddCharacter(list.string, i);
}
function RemoveCharacter(list, n){
  var newlist;
  var i;

  newlist = [];
  newlist.length = list.length - 1;

  if(n >= 0 && n < list.length){
    for(i = 0; i < list.length; i = i + 1){
      if(i < n){
        newlist[i] = list[i];
      }
      if(i > n){
        newlist[i - 1] = list[i];
      }
    }

    delete(list);
  }else{
    delete(newlist);
  }

  return newlist;
}
function GetCharacterRef(list, i){
  return list.string[i];
}
function RemoveCharacterRef(list, i){
  list.string = RemoveCharacter(list.string, i);
}
function charToLowerCase(character){
  var toReturn;

  toReturn = character;
  if(character == 'A'){
    toReturn = 'a';
  }else if(character == 'B'){
    toReturn = 'b';
  }else if(character == 'C'){
    toReturn = 'c';
  }else if(character == 'D'){
    toReturn = 'd';
  }else if(character == 'E'){
    toReturn = 'e';
  }else if(character == 'F'){
    toReturn = 'f';
  }else if(character == 'G'){
    toReturn = 'g';
  }else if(character == 'H'){
    toReturn = 'h';
  }else if(character == 'I'){
    toReturn = 'i';
  }else if(character == 'J'){
    toReturn = 'j';
  }else if(character == 'K'){
    toReturn = 'k';
  }else if(character == 'L'){
    toReturn = 'l';
  }else if(character == 'M'){
    toReturn = 'm';
  }else if(character == 'N'){
    toReturn = 'n';
  }else if(character == 'O'){
    toReturn = 'o';
  }else if(character == 'P'){
    toReturn = 'p';
  }else if(character == 'Q'){
    toReturn = 'q';
  }else if(character == 'R'){
    toReturn = 'r';
  }else if(character == 'S'){
    toReturn = 's';
  }else if(character == 'T'){
    toReturn = 't';
  }else if(character == 'U'){
    toReturn = 'u';
  }else if(character == 'V'){
    toReturn = 'v';
  }else if(character == 'W'){
    toReturn = 'w';
  }else if(character == 'X'){
    toReturn = 'x';
  }else if(character == 'Y'){
    toReturn = 'y';
  }else if(character == 'Z'){
    toReturn = 'z';
  }

  return toReturn;
}
function charToUpperCase(character){
  var toReturn;

  toReturn = character;
  if(character == 'a'){
    toReturn = 'A';
  }else if(character == 'b'){
    toReturn = 'B';
  }else if(character == 'c'){
    toReturn = 'C';
  }else if(character == 'd'){
    toReturn = 'D';
  }else if(character == 'e'){
    toReturn = 'E';
  }else if(character == 'f'){
    toReturn = 'F';
  }else if(character == 'g'){
    toReturn = 'G';
  }else if(character == 'h'){
    toReturn = 'H';
  }else if(character == 'i'){
    toReturn = 'I';
  }else if(character == 'j'){
    toReturn = 'J';
  }else if(character == 'k'){
    toReturn = 'K';
  }else if(character == 'l'){
    toReturn = 'L';
  }else if(character == 'm'){
    toReturn = 'M';
  }else if(character == 'n'){
    toReturn = 'N';
  }else if(character == 'o'){
    toReturn = 'O';
  }else if(character == 'p'){
    toReturn = 'P';
  }else if(character == 'q'){
    toReturn = 'Q';
  }else if(character == 'r'){
    toReturn = 'R';
  }else if(character == 's'){
    toReturn = 'S';
  }else if(character == 't'){
    toReturn = 'T';
  }else if(character == 'u'){
    toReturn = 'U';
  }else if(character == 'v'){
    toReturn = 'V';
  }else if(character == 'w'){
    toReturn = 'W';
  }else if(character == 'x'){
    toReturn = 'X';
  }else if(character == 'y'){
    toReturn = 'Y';
  }else if(character == 'z'){
    toReturn = 'Z';
  }

  return toReturn;
}
function charIsUpperCase(character){
  var isUpper;

  isUpper = false;
  if(character == 'A'){
    isUpper = true;
  }else if(character == 'B'){
    isUpper = true;
  }else if(character == 'C'){
    isUpper = true;
  }else if(character == 'D'){
    isUpper = true;
  }else if(character == 'E'){
    isUpper = true;
  }else if(character == 'F'){
    isUpper = true;
  }else if(character == 'G'){
    isUpper = true;
  }else if(character == 'H'){
    isUpper = true;
  }else if(character == 'I'){
    isUpper = true;
  }else if(character == 'J'){
    isUpper = true;
  }else if(character == 'K'){
    isUpper = true;
  }else if(character == 'L'){
    isUpper = true;
  }else if(character == 'M'){
    isUpper = true;
  }else if(character == 'N'){
    isUpper = true;
  }else if(character == 'O'){
    isUpper = true;
  }else if(character == 'P'){
    isUpper = true;
  }else if(character == 'Q'){
    isUpper = true;
  }else if(character == 'R'){
    isUpper = true;
  }else if(character == 'S'){
    isUpper = true;
  }else if(character == 'T'){
    isUpper = true;
  }else if(character == 'U'){
    isUpper = true;
  }else if(character == 'V'){
    isUpper = true;
  }else if(character == 'W'){
    isUpper = true;
  }else if(character == 'X'){
    isUpper = true;
  }else if(character == 'Y'){
    isUpper = true;
  }else if(character == 'Z'){
    isUpper = true;
  }

  return isUpper;
}
function charIsLowerCase(character){
  var isLower;

  isLower = false;
  if(character == 'a'){
    isLower = true;
  }else if(character == 'b'){
    isLower = true;
  }else if(character == 'c'){
    isLower = true;
  }else if(character == 'd'){
    isLower = true;
  }else if(character == 'e'){
    isLower = true;
  }else if(character == 'f'){
    isLower = true;
  }else if(character == 'g'){
    isLower = true;
  }else if(character == 'h'){
    isLower = true;
  }else if(character == 'i'){
    isLower = true;
  }else if(character == 'j'){
    isLower = true;
  }else if(character == 'k'){
    isLower = true;
  }else if(character == 'l'){
    isLower = true;
  }else if(character == 'm'){
    isLower = true;
  }else if(character == 'n'){
    isLower = true;
  }else if(character == 'o'){
    isLower = true;
  }else if(character == 'p'){
    isLower = true;
  }else if(character == 'q'){
    isLower = true;
  }else if(character == 'r'){
    isLower = true;
  }else if(character == 's'){
    isLower = true;
  }else if(character == 't'){
    isLower = true;
  }else if(character == 'u'){
    isLower = true;
  }else if(character == 'v'){
    isLower = true;
  }else if(character == 'w'){
    isLower = true;
  }else if(character == 'x'){
    isLower = true;
  }else if(character == 'y'){
    isLower = true;
  }else if(character == 'z'){
    isLower = true;
  }

  return isLower;
}
function charIsLetter(character){
  return charIsUpperCase(character) || charIsLowerCase(character);
}
function charIsNumber(character){
  var isNumberx;

  isNumberx = false;
  if(character == '0'){
    isNumberx = true;
  }else if(character == '1'){
    isNumberx = true;
  }else if(character == '2'){
    isNumberx = true;
  }else if(character == '3'){
    isNumberx = true;
  }else if(character == '4'){
    isNumberx = true;
  }else if(character == '5'){
    isNumberx = true;
  }else if(character == '6'){
    isNumberx = true;
  }else if(character == '7'){
    isNumberx = true;
  }else if(character == '8'){
    isNumberx = true;
  }else if(character == '9'){
    isNumberx = true;
  }

  return isNumberx;
}
function charIsWhiteSpace(character){
  var isWhiteSpacex;

  isWhiteSpacex = false;
  if(character == ' '){
    isWhiteSpacex = true;
  }else if(character == '\t'){
    isWhiteSpacex = true;
  }else if(character == '\n'){
    isWhiteSpacex = true;
  }else if(character == '\r'){
    isWhiteSpacex = true;
  }

  return isWhiteSpacex;
}
function charIsSymbol(character){
  var isSymbolx;

  isSymbolx = false;
  if(character == '!'){
    isSymbolx = true;
  }else if(character == '\"'){
    isSymbolx = true;
  }else if(character == '#'){
    isSymbolx = true;
  }else if(character == '$'){
    isSymbolx = true;
  }else if(character == '%'){
    isSymbolx = true;
  }else if(character == '&'){
    isSymbolx = true;
  }else if(character == '\''){
    isSymbolx = true;
  }else if(character == '('){
    isSymbolx = true;
  }else if(character == ')'){
    isSymbolx = true;
  }else if(character == '*'){
    isSymbolx = true;
  }else if(character == '+'){
    isSymbolx = true;
  }else if(character == ','){
    isSymbolx = true;
  }else if(character == '-'){
    isSymbolx = true;
  }else if(character == '.'){
    isSymbolx = true;
  }else if(character == '/'){
    isSymbolx = true;
  }else if(character == ':'){
    isSymbolx = true;
  }else if(character == ';'){
    isSymbolx = true;
  }else if(character == '<'){
    isSymbolx = true;
  }else if(character == '='){
    isSymbolx = true;
  }else if(character == '>'){
    isSymbolx = true;
  }else if(character == '?'){
    isSymbolx = true;
  }else if(character == '@'){
    isSymbolx = true;
  }else if(character == '['){
    isSymbolx = true;
  }else if(character == '\\'){
    isSymbolx = true;
  }else if(character == ']'){
    isSymbolx = true;
  }else if(character == '^'){
    isSymbolx = true;
  }else if(character == '_'){
    isSymbolx = true;
  }else if(character == '`'){
    isSymbolx = true;
  }else if(character == '{'){
    isSymbolx = true;
  }else if(character == '|'){
    isSymbolx = true;
  }else if(character == '}'){
    isSymbolx = true;
  }else if(character == '~'){
    isSymbolx = true;
  }

  return isSymbolx;
}
function charCharacterIsBefore(a, b){
  var ad, bd;

  ad = a.charCodeAt(0);
  bd = b.charCodeAt(0);

  return ad < bd;
}
function And4Byte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFFFFFFFF && n2 >= 0 && n2 <= 0xFFFFFFFF){
    var a = n1 & n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function And2Byte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFFFF && n2 >= 0 && n2 <= 0xFFFF){
    var a = n1 & n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function AndByte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFF && n2 >= 0 && n2 <= 0xFF){
    var a = n1 & n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function AndBytes(n1, n2, bytes){
  var byteVal, result, i;

  byteVal = 1;
  result = 0;

  if(n1 >= 0 && n1 < Math.pow(2, bytes*8) && n2 >= 0 && n2 < Math.pow(2, bytes*8)){
    n1 = Truncate(n1);
    n2 = Truncate(n2);
    bytes = Truncate(bytes);

    for(i = 0; i < bytes*8; i = i + 1){
      if(n1%2 == 1 && n2%2 == 1){
        result = result + byteVal;
      }
      n1 = Math.floor(n1/2);
      n2 = Math.floor(n2/2);
      byteVal = byteVal*2;
    }
  }

  return result;
}
function Or4Byte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFFFFFFFF && n2 >= 0 && n2 <= 0xFFFFFFFF){
    var a = n1 | n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function Or2Byte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFFFF && n2 >= 0 && n2 <= 0xFFFF){
    var a = n1 | n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function OrByte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFF && n2 >= 0 && n2 <= 0xFF){
    var a = n1 | n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function OrBytes(n1, n2, bytes){
  var byteVal, result, i;

  byteVal = 1;
  result = 0;

  if(n1 >= 0 && n1 < Math.pow(2, bytes*8) && n2 >= 0 && n2 < Math.pow(2, bytes*8)){
    n1 = Truncate(n1);
    n2 = Truncate(n2);
    bytes = Truncate(bytes);

    for(i = 0; i < bytes*8; i = i + 1){
      if(n1%2 == 1 || n2%2 == 1){
        result = result + byteVal;
      }
      n1 = Math.floor(n1/2);
      n2 = Math.floor(n2/2);
      byteVal = byteVal*2;
    }
  }

  return result;
}
function Xor4Byte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFFFFFFFF && n2 >= 0 && n2 <= 0xFFFFFFFF){
    var a = n1 ^ n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function Xor2Byte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFFFF && n2 >= 0 && n2 <= 0xFFFF){
    var a = n1 ^ n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function XorByte(n1, n2){
  if(n1 >= 0 && n1 <= 0xFF && n2 >= 0 && n2 <= 0xFF){
    var a = n1 ^ n2;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function XorBytes(n1, n2, bytes){
  var byteVal, result, i;

  byteVal = 1;
  result = 0;

  if(n1 >= 0 && n1 < Math.pow(2, bytes*8) && n2 >= 0 && n2 < Math.pow(2, bytes*8)){
    n1 = Truncate(n1);
    n2 = Truncate(n2);
    bytes = Truncate(bytes);

    for(i = 0; i < bytes*8; i = i + 1){
      if(n1%2 != n2%2){
        result = result + byteVal;
      }
      n1 = Math.floor(n1/2);
      n2 = Math.floor(n2/2);
      byteVal = byteVal*2;
    }
  }

  return result;
}
function Not4Byte(b){
  if(b >= 0 && b <= 0xFFFFFFFF){
    var a = ~b & 0xFFFFFFFF;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function Not2Byte(b){
  if(b >= 0 && b <= 0xFFFF){
    var a = ~b & 0xFFFF;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function NotByte(b){
  if(b >= 0 && b <= 0xFF){
    var a = ~b & 0xFF;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function NotBytes(b, length){
  var result;

  result = 0;

  if(b >= 0 && b < Math.pow(2, length*8)){
    b = Truncate(b);
    length = Truncate(length);

    result = Math.pow(2, length*8) - b - 1;
  }

  return result;
}
function ShiftLeft4Byte(b, amount){
  if(b >= 0 && b <= 0xFFFFFFFF && amount >= 0 && amount < 32){
    var a = b << amount;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function ShiftLeft2Byte(b, amount){
  if(b >= 0 && b <= 0xFFFF && amount >= 0 && amount < 16){
    var a = b << amount;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function ShiftLeftByte(b, amount){
  if(b >= 0 && b <= 0xFF && amount >= 0 && amount < 8){
    var a = b << amount;
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function ShiftLeftBytes(b, amount, length){
  var result;

  result = 0;

  if(b >= 0 && b < Math.pow(2, length*8) && amount >= 0 && amount <= length*8){
    b = Truncate(b);
    amount = Truncate(amount);

    result = b*Math.pow(2, amount);
  }

  return result;
}
function ShiftRight4Byte(b, amount){
  if(b >= 0 && b <= 0xFFFFFFFF && amount >= 0 && amount < 32){
    var a;
    if(b > (1 << 30)){
      b = b - 2147483648;
      a = b >> amount;
      a += 1 << (31 - amount);
    }else{
      a = b >> amount;
    }
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function ShiftRight2Byte(b, amount){
  if(b >= 0 && b <= 0xFFFF && amount >= 0 && amount < 16){
    var a;
    if(b > (1 << 30)){
      b = b - 2147483648;
      a = b >> amount;
      a += 1 << (31 - amount);
    }else{
      a = b >> amount;
    }
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function ShiftRightByte(b, amount){
  if(b >= 0 && b <= 0xFF && amount >= 0 && amount < 8){
    var a;
    if(b > (1 << 30)){
      b = b - 2147483648;
      a = b >> amount;
      a += 1 << (31 - amount);
    }else{
      a = b >> amount;
    }
    if(a >= 0){
      return a;
    }else{
      return 4294967296 + a;
    }
  }else{
    return 0;
  }
}
function ShiftRightBytes(b, amount, length){
  var result;

  result = 0;

  if(b >= 0 && b < Math.pow(2, length*8) && amount >= 0 && amount <= length*8){
    b = Truncate(b);
    amount = Truncate(amount);

    result = Truncate(b/Math.pow(2, amount));
  }

  return result;
}
function ReadNextBit(data, nextbit){
  var bytenr, bitnumber, bit, b;

  bytenr = Math.floor(nextbit.numberValue/8);
  bitnumber = nextbit.numberValue%8;

  b = data[bytenr];

  bit = Math.floor(b/Math.pow(2, bitnumber))%2;

  nextbit.numberValue = nextbit.numberValue + 1;

  return bit;
}
function BitExtract(b, fromInc, toInc){
  return Math.floor(b/Math.pow(2, fromInc))%Math.pow(2, toInc + 1 - fromInc);
}
function ReadBitRange(data, nextbit, length){
  var startbyte, endbyte;
  var startbit, endbit;
  var number, i;

  number = 0;

  startbyte = Math.floor(nextbit.numberValue/8);
  endbyte = Math.floor((nextbit.numberValue + length)/8);

  startbit = nextbit.numberValue%8;
  endbit = (nextbit.numberValue + length - 1)%8;

  if(startbyte == endbyte){
    number = BitExtract(data[startbyte], startbit, endbit);
  }

  nextbit.numberValue = nextbit.numberValue + length;

  return number;
}
function SkipToBoundary(nextbit){
  var skip;

  skip = 8 - nextbit.numberValue%8;
  nextbit.numberValue = nextbit.numberValue + skip;
}
function ReadNextByteBoundary(data, nextbit){
  var bytenr, b;

  bytenr = Math.floor(nextbit.numberValue/8);
  b = data[bytenr];
  nextbit.numberValue = nextbit.numberValue + 8;

  return b;
}
function Read2bytesByteBoundary(data, nextbit){
  var r;

  r = 0;
  r = r + Math.pow(2, 8)*ReadNextByteBoundary(data, nextbit);
  r = r + ReadNextByteBoundary(data, nextbit);

  return r;
}
function ComputeAdler32(data){
  var a, b, m, i;

  a = 1;
  b = 0;
  m = 65521;

  for(i = 0; i < data.length; i = i + 1){
    a = (a + data[i])%m;
    b = (b + a)%m;
  }

  return b*Math.pow(2, 16) + a;
}
function DeflateDataStaticHuffman(data, level){
  var bytes;
  var currentBit;
  var i;
  var copy;
  var code, length, compressedCode, lengthAdditionLength, distanceCode;
  var distanceReference, lengthReference, lengthAddition;
  var distanceAdditionReference, distanceAdditionLengthReference;
  var bitReverseLookupTable;
  var match;

  code = CreateNumberReference(0);
  length = CreateNumberReference(0);
  compressedCode = CreateNumberReference(0);
  lengthAdditionLength = CreateNumberReference(0);
  distanceCode = CreateNumberReference(0);
  distanceReference = CreateNumberReference(0);
  lengthReference = CreateNumberReference(0);
  lengthAddition = CreateNumberReference(0);
  distanceAdditionReference = CreateNumberReference(0);
  distanceAdditionLengthReference = CreateNumberReference(0);
  match = {};

  bytes = [];
  bytes.length = Math.max(data.length*2, 100);
  aFillNumberArray(bytes, 0);
  currentBit = CreateNumberReference(0);

  bitReverseLookupTable = GenerateBitReverseLookupTable(9);

  /* Final block */
  AppendBitsToBytesRight(bytes, currentBit, 1, 1);
  /* Fixed code */
  AppendBitsToBytesRight(bytes, currentBit, 1, 2);

  for(i = 0; i < data.length; ){
    FindMatch(data, i, distanceReference, lengthReference, match, level);

    if(match.booleanValue){
      GetDeflateLengthCode(lengthReference.numberValue, compressedCode, lengthAddition, lengthAdditionLength);
      GetDeflateDistanceCode(distanceReference.numberValue, distanceCode, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable);
    }

    if( !match.booleanValue ){
      GetDeflateStaticHuffmanCode(data[i], code, length, bitReverseLookupTable);
      AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue);
      i = i + 1;
    }else{
      GetDeflateStaticHuffmanCode(compressedCode.numberValue, code, length, bitReverseLookupTable);
      AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue);
      AppendBitsToBytesRight(bytes, currentBit, lengthAddition.numberValue, lengthAdditionLength.numberValue);
      AppendBitsToBytesRight(bytes, currentBit, distanceCode.numberValue, 5);
      AppendBitsToBytesRight(bytes, currentBit, distanceAdditionReference.numberValue, distanceAdditionLengthReference.numberValue);
      i = i + lengthReference.numberValue;
    }
  }

  /* Stop symbol */
  GetDeflateStaticHuffmanCode(256, code, length, bitReverseLookupTable);
  AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue);

  copy = {};
  aCopyNumberArrayRange(bytes, 0, Math.ceil(currentBit.numberValue/8), copy);
  delete(bytes);
  bytes = copy.numberArray;

  return bytes;
}
function FindMatch(data, pos, distanceReference, lengthReference, match, level){
  var i, j;
  var deflateMinMength, deflateMaxLength, deflateMaxDistance;
  var longest, maxLength, distanceForMax;
  var startDistance, matchLength;
  var done;

  deflateMinMength = 3;
  deflateMaxLength = 258;

  longest = Math.min(pos - 1, deflateMaxLength);
  longest = Math.min(data.length - pos, longest);

  deflateMaxDistance = Math.floor(32768/10*level);

  startDistance = Math.min(pos, deflateMaxDistance);

  if(longest >= deflateMinMength){
    maxLength = 0;
    distanceForMax = 0;

    for(i = pos - 1; i >= pos - startDistance && maxLength != longest; i = i - 1){
      matchLength = 0;
      done = false;
      for(j = 0; j < longest &&  !done ; j = j + 1){
        if(data[i + j] == data[pos + j]){
          matchLength = matchLength + 1;
        }else{
          done = true;
        }
      }

      if(matchLength >= deflateMinMength && matchLength > maxLength){
        maxLength = matchLength;
        distanceForMax = pos - i;
      }
    }

    if(maxLength >= deflateMinMength){
      match.booleanValue = true;
      lengthReference.numberValue = maxLength;
      distanceReference.numberValue = distanceForMax;
    }else{
      match.booleanValue = false;
    }
  }else{
    match.booleanValue = false;
  }
}
function GenerateBitReverseLookupTable(bits){
  var table;
  var i;

  table = [];
  table.length = Math.pow(2, bits);

  for(i = 0; i < table.length; i = i + 1){
    table[i] = ReverseBits(i, 32);
  }

  return table;
}
function ReverseBits(x, bits){
  var b, bit, i;

  b = 0;

  for(i = 0; i < bits; i = i + 1){
    b = ShiftLeft4Byte(b, 1);
    bit = And4Byte(x, 1);
    b = Or4Byte(b, bit);
    x = ShiftRight4Byte(x, 1);
  }

  return b;
}
function DeflateDataNoCompression(data){
  var deflated;
  var position;
  var block, i, blocks, blocklength, maxblocksize;

  maxblocksize = Math.pow(2, 16) - 1;
  blocks = Math.ceil(data.length/maxblocksize);

  position = CreateNumberReference(0);

  deflated = [];
  deflated.length = (1 + 4)*blocks + data.length;

  for(block = 0; block < blocks; block = block + 1){
    if(block + 1 == blocks){
      WriteByte(deflated, 1, position);
    }else{
      WriteByte(deflated, 0, position);
    }
    blocklength = Math.min(data.length - block*maxblocksize, maxblocksize);
    Write2BytesLE(deflated, blocklength, position);
    Write2BytesLE(deflated, Not2Byte(blocklength), position);

    for(i = 0; i < blocklength; i = i + 1){
      WriteByte(deflated, data[block*maxblocksize + i], position);
    }
  }

  return deflated;
}
function GetDeflateStaticHuffmanCode(b, code, length, bitReverseLookupTable){
  var reversed;

  if(b >= 0 && b <= 143){
    code.numberValue = 48 + b;
    length.numberValue = 8;
  }else if(b >= 144 && b <= 255){
    code.numberValue = b - 144 + 400;
    length.numberValue = 9;
  }else if(b >= 256 && b <= 279){
    code.numberValue = b - 256 + 0;
    length.numberValue = 7;
  }else if(b >= 280 && b <= 287){
    code.numberValue = b - 280 + 192;
    length.numberValue = 8;
  }

  reversed = bitReverseLookupTable[code.numberValue];
  code.numberValue = ShiftRight4Byte(reversed, 32 - length.numberValue);
}
function GetDeflateLengthCode(length, code, lengthAddition, lengthAdditionLength){
  if(length >= 3 && length <= 10){
    code.numberValue = 257 + length - 3;
    lengthAdditionLength.numberValue = 0;
  }else if(length >= 11 && length <= 18){
    code.numberValue = 265 + Math.floor((length - 11)/2);
    lengthAddition.numberValue = Math.floor((length - 11)%2);
    lengthAdditionLength.numberValue = 1;
  }else if(length >= 19 && length <= 34){
    code.numberValue = 269 + Math.floor((length - 19)/4);
    lengthAddition.numberValue = Math.floor((length - 19)%4);
    lengthAdditionLength.numberValue = 2;
  }else if(length >= 35 && length <= 66){
    code.numberValue = 273 + Math.floor((length - 35)/8);
    lengthAddition.numberValue = Math.floor((length - 35)%8);
    lengthAdditionLength.numberValue = 3;
  }else if(length >= 67 && length <= 130){
    code.numberValue = 277 + Math.floor((length - 67)/16);
    lengthAddition.numberValue = Math.floor((length - 67)%16);
    lengthAdditionLength.numberValue = 4;
  }else if(length >= 131 && length <= 257){
    code.numberValue = 281 + Math.floor((length - 131)/32);
    lengthAddition.numberValue = Math.floor((length - 131)%32);
    lengthAdditionLength.numberValue = 5;
  }else if(length == 258){
    code.numberValue = 285;
    lengthAdditionLength.numberValue = 0;
  }
}
function GetDeflateDistanceCode(distance, code, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable){
  var reversed;

  if(distance >= 1 && distance <= 4){
    code.numberValue = distance - 1;
    distanceAdditionLengthReference.numberValue = 0;
  }else if(distance >= 5 && distance <= 8){
    code.numberValue = 4 + Math.floor((distance - 5)/2);
    distanceAdditionReference.numberValue = Math.floor((distance - 5)%2);
    distanceAdditionLengthReference.numberValue = 1;
  }else if(distance >= 9 && distance <= 16){
    code.numberValue = 6 + Math.floor((distance - 9)/4);
    distanceAdditionReference.numberValue = Math.floor((distance - 9)%4);
    distanceAdditionLengthReference.numberValue = 2;
  }else if(distance >= 17 && distance <= 32){
    code.numberValue = 8 + Math.floor((distance - 17)/8);
    distanceAdditionReference.numberValue = Math.floor((distance - 17)%8);
    distanceAdditionLengthReference.numberValue = 3;
  }else if(distance >= 33 && distance <= 64){
    code.numberValue = 10 + Math.floor((distance - 33)/16);
    distanceAdditionReference.numberValue = Math.floor((distance - 33)%16);
    distanceAdditionLengthReference.numberValue = 4;
  }else if(distance >= 65 && distance <= 128){
    code.numberValue = 12 + Math.floor((distance - 65)/32);
    distanceAdditionReference.numberValue = Math.floor((distance - 65)%32);
    distanceAdditionLengthReference.numberValue = 5;
  }else if(distance >= 129 && distance <= 256){
    code.numberValue = 14 + Math.floor((distance - 129)/64);
    distanceAdditionReference.numberValue = Math.floor((distance - 129)%64);
    distanceAdditionLengthReference.numberValue = 6;
  }else if(distance >= 257 && distance <= 512){
    code.numberValue = 16 + Math.floor((distance - 257)/128);
    distanceAdditionReference.numberValue = Math.floor((distance - 257)%128);
    distanceAdditionLengthReference.numberValue = 7;
  }else if(distance >= 513 && distance <= 1024){
    code.numberValue = 18 + Math.floor((distance - 513)/256);
    distanceAdditionReference.numberValue = Math.floor((distance - 513)%256);
    distanceAdditionLengthReference.numberValue = 8;
  }else if(distance >= 1025 && distance <= 2048){
    code.numberValue = 20 + Math.floor((distance - 1025)/Math.pow(2, 9));
    distanceAdditionReference.numberValue = Math.floor((distance - 1025)%Math.pow(2, 9));
    distanceAdditionLengthReference.numberValue = 9;
  }else if(distance >= 2049 && distance <= 4096){
    code.numberValue = 22 + Math.floor((distance - 2049)/Math.pow(2, 10));
    distanceAdditionReference.numberValue = Math.floor((distance - 2049)%Math.pow(2, 10));
    distanceAdditionLengthReference.numberValue = 10;
  }else if(distance >= 4097 && distance <= 8192){
    code.numberValue = 24 + Math.floor((distance - 4097)/Math.pow(2, 11));
    distanceAdditionReference.numberValue = Math.floor((distance - 4097)%Math.pow(2, 11));
    distanceAdditionLengthReference.numberValue = 11;
  }else if(distance >= 8193 && distance <= 16384){
    code.numberValue = 26 + Math.floor((distance - 8193)/Math.pow(2, 12));
    distanceAdditionReference.numberValue = Math.floor((distance - 8193)%Math.pow(2, 12));
    distanceAdditionLengthReference.numberValue = 12;
  }else if(distance >= 16385 && distance <= 32768){
    code.numberValue = 28 + Math.floor((distance - 16385)/Math.pow(2, 13));
    distanceAdditionReference.numberValue = Math.floor((distance - 16385)%Math.pow(2, 13));
    distanceAdditionLengthReference.numberValue = 13;
  }

  reversed = bitReverseLookupTable[code.numberValue];
  code.numberValue = ShiftRight4Byte(reversed, 32 - 5);
}
function AppendBitsToBytesLeft(bytes, nextbit, data, length){
  var bytePos, bitPos, segment, part, remove;

  for(; length > 0; ){
    bytePos = Truncate(nextbit.numberValue/8);
    bitPos = nextbit.numberValue%8;

    if(length < 8 - bitPos){
      part = ShiftLeft4Byte(data, 8 - bitPos - length);

      bytes[bytePos] = Or4Byte(bytes[bytePos], part);

      nextbit.numberValue = nextbit.numberValue + length;

      length = 0;
    }else{
      segment = 8 - bitPos;

      part = ShiftRight4Byte(data, length - segment);
      bytes[bytePos] = Or4Byte(bytes[bytePos], part);
      nextbit.numberValue = nextbit.numberValue + segment;

      remove = ShiftLeft4Byte(part, length - segment);
      data = Xor4Byte(data, remove);

      length = length - segment;
    }
  }
}
function AppendBitsToBytesRight(bytes, nextbit, data, length){
  var bytePos, bitPos, segment, part;
  var mask;

  for(; length > 0; ){
    bytePos = Truncate(nextbit.numberValue/8);
    bitPos = nextbit.numberValue%8;

    if(length < 8 - bitPos){
      part = ShiftLeft4Byte(data, bitPos);

      bytes[bytePos] = Or4Byte(bytes[bytePos], part);

      nextbit.numberValue = nextbit.numberValue + length;

      length = 0;
    }else{
      segment = 8 - bitPos;

      mask = 1;
      mask = ShiftLeft4Byte(mask, segment);
      mask = mask - 1;

      part = And4Byte(mask, data);
      part = ShiftLeft4Byte(part, bitPos);
      bytes[bytePos] = Or4Byte(bytes[bytePos], part);
      nextbit.numberValue = nextbit.numberValue + segment;

      data = ShiftRight4Byte(data, segment);

      length = length - segment;
    }
  }
}

