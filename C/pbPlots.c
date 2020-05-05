/* Downloaded from https://repo.progsbase.com - Code Developed Using progsbase. */

#include "pbPlots.h"

#define strparam(str) (str), wcslen(str)

#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif

_Bool CropLineWithinBoundary(NumberReference *x1Ref, NumberReference *y1Ref, NumberReference *x2Ref, NumberReference *y2Ref, double xMin, double xMax, double yMin, double yMax){
  double x1, y1, x2, y2;
  _Bool success, p1In, p2In;
  double dx, dy, f1, f2, f3, f4, f;

  x1 = x1Ref->numberValue;
  y1 = y1Ref->numberValue;
  x2 = x2Ref->numberValue;
  y2 = y2Ref->numberValue;

  p1In = x1 >= xMin && x1 <= xMax && y1 >= yMin && y1 <= yMax;
  p2In = x2 >= xMin && x2 <= xMax && y2 >= yMin && y2 <= yMax;

  if(p1In && p2In){
    success = true;
  }else if( !p1In  && p2In){
    dx = x1 - x2;
    dy = y1 - y2;

    if(dx != 0.0){
      f1 = (xMin - x2)/dx;
      f2 = (xMax - x2)/dx;
    }else{
      f1 = 1.0;
      f2 = 1.0;
    }
    if(dy != 0.0){
      f3 = (yMin - y2)/dy;
      f4 = (yMax - y2)/dy;
    }else{
      f3 = 1.0;
      f4 = 1.0;
    }

    if(f1 < 0.0){
      f1 = 1.0;
    }
    if(f2 < 0.0){
      f2 = 1.0;
    }
    if(f3 < 0.0){
      f3 = 1.0;
    }
    if(f4 < 0.0){
      f4 = 1.0;
    }

    f = fmin(f1, fmin(f2, fmin(f3, f4)));

    x1 = x2 + f*dx;
    y1 = y2 + f*dy;

    success = true;
  }else if(p1In &&  !p2In ){
    dx = x2 - x1;
    dy = y2 - y1;

    if(dx != 0.0){
      f1 = (xMin - x1)/dx;
      f2 = (xMax - x1)/dx;
    }else{
      f1 = 1.0;
      f2 = 1.0;
    }
    if(dy != 0.0){
      f3 = (yMin - y1)/dy;
      f4 = (yMax - y1)/dy;
    }else{
      f3 = 1.0;
      f4 = 1.0;
    }

    if(f1 < 0.0){
      f1 = 1.0;
    }
    if(f2 < 0.0){
      f2 = 1.0;
    }
    if(f3 < 0.0){
      f3 = 1.0;
    }
    if(f4 < 0.0){
      f4 = 1.0;
    }

    f = fmin(f1, fmin(f2, fmin(f3, f4)));

    x2 = x1 + f*dx;
    y2 = y1 + f*dy;

    success = true;
  }else{
    success = false;
  }

  x1Ref->numberValue = x1;
  y1Ref->numberValue = y1;
  x2Ref->numberValue = x2;
  y2Ref->numberValue = y2;

  return success;
}
double IncrementFromCoordinates(double x1, double y1, double x2, double y2){
  return (x2 - x1)/(y2 - y1);
}
double InterceptFromCoordinates(double x1, double y1, double x2, double y2){
  double a, b;

  a = IncrementFromCoordinates(x1, y1, x2, y2);
  b = y1 - a*x1;

  return b;
}
RGBA **Get8HighContrastColors(size_t *returnArrayLength){
  RGBA **colors;
  size_t colorsLength;
  colors = (RGBA**)malloc(sizeof(RGBA) * 8.0);
  colorsLength = 8.0;
  colors[0] = CreateRGBColor(3.0/256.0, 146.0/256.0, 206.0/256.0);
  colors[1] = CreateRGBColor(253.0/256.0, 83.0/256.0, 8.0/256.0);
  colors[2] = CreateRGBColor(102.0/256.0, 176.0/256.0, 50.0/256.0);
  colors[3] = CreateRGBColor(208.0/256.0, 234.0/256.0, 43.0/256.0);
  colors[4] = CreateRGBColor(167.0/256.0, 25.0/256.0, 75.0/256.0);
  colors[5] = CreateRGBColor(254.0/256.0, 254.0/256.0, 51.0/256.0);
  colors[6] = CreateRGBColor(134.0/256.0, 1.0/256.0, 175.0/256.0);
  colors[7] = CreateRGBColor(251.0/256.0, 153.0/256.0, 2.0/256.0);
  *returnArrayLength = colorsLength;
  return colors;
}
void DrawFilledRectangleWithBorder(RGBABitmapImage *image, double x, double y, double w, double h, RGBA *borderColor, RGBA *fillColor){
  if(h > 0.0 && w > 0.0){
    DrawFilledRectangle(image, x, y, w, h, fillColor);
    DrawRectangle1px(image, x, y, w, h, borderColor);
  }
}
RGBABitmapImageReference *CreateRGBABitmapImageReference(){
  RGBABitmapImageReference *reference;

  reference = (RGBABitmapImageReference *)malloc(sizeof(RGBABitmapImageReference));
  reference->image = (RGBABitmapImage *)malloc(sizeof(RGBABitmapImage));
  reference->image->x = (RGBABitmap**)malloc(sizeof(RGBABitmap) * 0.0);
  reference->image->xLength = 0.0;

  return reference;
}
_Bool RectanglesOverlap(Rectangle *r1, Rectangle *r2){
  _Bool overlap;

  overlap = false;

  overlap = overlap || (r2->x1 >= r1->x1 && r2->x1 <= r1->x2 && r2->y1 >= r1->y1 && r2->y1 <= r1->y2);
  overlap = overlap || (r2->x2 >= r1->x1 && r2->x2 <= r1->x2 && r2->y1 >= r1->y1 && r2->y1 <= r1->y2);
  overlap = overlap || (r2->x1 >= r1->x1 && r2->x1 <= r1->x2 && r2->y2 >= r1->y1 && r2->y2 <= r1->y2);
  overlap = overlap || (r2->x2 >= r1->x1 && r2->x2 <= r1->x2 && r2->y2 >= r1->y1 && r2->y2 <= r1->y2);

  return overlap;
}
Rectangle *CreateRectangle(double x1, double y1, double x2, double y2){
  Rectangle *r;
  r = (Rectangle *)malloc(sizeof(Rectangle));
  r->x1 = x1;
  r->y1 = y1;
  r->x2 = x2;
  r->y2 = y2;
  return r;
}
void CopyRectangleValues(Rectangle *rd, Rectangle *rs){
  rd->x1 = rs->x1;
  rd->y1 = rs->y1;
  rd->x2 = rs->x2;
  rd->y2 = rs->y2;
}
void DrawXLabelsForPriority(double p, double xMin, double oy, double xMax, double xPixelMin, double xPixelMax, NumberReference *nextRectangle, RGBA *gridLabelColor, RGBABitmapImage *canvas, double *xGridPositions, size_t xGridPositionsLength, StringArrayReference *xLabels, NumberArrayReference *xLabelPriorities, Rectangle **occupied, size_t occupiedLength, _Bool textOnBottom){
  _Bool overlap, currentOverlaps;
  double i, j, x, px, padding;
  wchar_t *text;
  size_t textLength;
  Rectangle *r;

  r = (Rectangle *)malloc(sizeof(Rectangle));
  padding = 10.0;

  overlap = false;
  for(i = 0.0; i < xLabels->stringArrayLength; i = i + 1.0){
    if(xLabelPriorities->numberArray[(int)(i)] == p){

      x = xGridPositions[(int)(i)];
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
      text = xLabels->stringArray[(int)(i)]->string;
      textLength = xLabels->stringArray[(int)(i)]->stringLength;

      r->x1 = floor(px - GetTextWidth(text, textLength)/2.0);
      if(textOnBottom){
        r->y1 = floor(oy + 5.0);
      }else{
        r->y1 = floor(oy - 20.0);
      }
      r->x2 = r->x1 + GetTextWidth(text, textLength);
      r->y2 = r->y1 + GetTextHeight(text, textLength);

      /* Add padding */
      r->x1 = r->x1 - padding;
      r->y1 = r->y1 - padding;
      r->x2 = r->x2 + padding;
      r->y2 = r->y2 + padding;

      currentOverlaps = false;

      for(j = 0.0; j < nextRectangle->numberValue; j = j + 1.0){
        currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[(int)(j)]);
      }

      if( !currentOverlaps  && p == 1.0){
        DrawText(canvas, r->x1 + padding, r->y1 + padding, text, textLength, gridLabelColor);

        CopyRectangleValues(occupied[(int)(nextRectangle->numberValue)], r);
        nextRectangle->numberValue = nextRectangle->numberValue + 1.0;
      }

      overlap = overlap || currentOverlaps;
    }
  }
  if( !overlap  && p != 1.0){
    for(i = 0.0; i < xGridPositionsLength; i = i + 1.0){
      x = xGridPositions[(int)(i)];
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);

      if(xLabelPriorities->numberArray[(int)(i)] == p){
        text = xLabels->stringArray[(int)(i)]->string;
        textLength = xLabels->stringArray[(int)(i)]->stringLength;

        r->x1 = floor(px - GetTextWidth(text, textLength)/2.0);
        if(textOnBottom){
          r->y1 = floor(oy + 5.0);
        }else{
          r->y1 = floor(oy - 20.0);
        }
        r->x2 = r->x1 + GetTextWidth(text, textLength);
        r->y2 = r->y1 + GetTextHeight(text, textLength);

        DrawText(canvas, r->x1, r->y1, text, textLength, gridLabelColor);

        CopyRectangleValues(occupied[(int)(nextRectangle->numberValue)], r);
        nextRectangle->numberValue = nextRectangle->numberValue + 1.0;
      }
    }
  }
}
void DrawYLabelsForPriority(double p, double yMin, double ox, double yMax, double yPixelMin, double yPixelMax, NumberReference *nextRectangle, RGBA *gridLabelColor, RGBABitmapImage *canvas, double *yGridPositions, size_t yGridPositionsLength, StringArrayReference *yLabels, NumberArrayReference *yLabelPriorities, Rectangle **occupied, size_t occupiedLength, _Bool textOnLeft){
  _Bool overlap, currentOverlaps;
  double i, j, y, py, padding;
  wchar_t *text;
  size_t textLength;
  Rectangle *r;

  r = (Rectangle *)malloc(sizeof(Rectangle));
  padding = 10.0;

  overlap = false;
  for(i = 0.0; i < yLabels->stringArrayLength; i = i + 1.0){
    if(yLabelPriorities->numberArray[(int)(i)] == p){

      y = yGridPositions[(int)(i)];
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
      text = yLabels->stringArray[(int)(i)]->string;
      textLength = yLabels->stringArray[(int)(i)]->stringLength;

      if(textOnLeft){
        r->x1 = floor(ox - GetTextWidth(text, textLength) - 10.0);
      }else{
        r->x1 = floor(ox + 10.0);
      }
      r->y1 = floor(py - 6.0);
      r->x2 = r->x1 + GetTextWidth(text, textLength);
      r->y2 = r->y1 + GetTextHeight(text, textLength);

      /* Add padding */
      r->x1 = r->x1 - padding;
      r->y1 = r->y1 - padding;
      r->x2 = r->x2 + padding;
      r->y2 = r->y2 + padding;

      currentOverlaps = false;

      for(j = 0.0; j < nextRectangle->numberValue; j = j + 1.0){
        currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[(int)(j)]);
      }

      /* Draw labels with priority 1 if they do not overlap anything else. */
      if( !currentOverlaps  && p == 1.0){
        DrawText(canvas, r->x1 + padding, r->y1 + padding, text, textLength, gridLabelColor);

        CopyRectangleValues(occupied[(int)(nextRectangle->numberValue)], r);
        nextRectangle->numberValue = nextRectangle->numberValue + 1.0;
      }

      overlap = overlap || currentOverlaps;
    }
  }
  if( !overlap  && p != 1.0){
    for(i = 0.0; i < yGridPositionsLength; i = i + 1.0){
      y = yGridPositions[(int)(i)];
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);

      if(yLabelPriorities->numberArray[(int)(i)] == p){
        text = yLabels->stringArray[(int)(i)]->string;
        textLength = yLabels->stringArray[(int)(i)]->stringLength;

        if(textOnLeft){
          r->x1 = floor(ox - GetTextWidth(text, textLength) - 10.0);
        }else{
          r->x1 = floor(ox + 10.0);
        }
        r->y1 = floor(py - 6.0);
        r->x2 = r->x1 + GetTextWidth(text, textLength);
        r->y2 = r->y1 + GetTextHeight(text, textLength);

        DrawText(canvas, r->x1, r->y1, text, textLength, gridLabelColor);

        CopyRectangleValues(occupied[(int)(nextRectangle->numberValue)], r);
        nextRectangle->numberValue = nextRectangle->numberValue + 1.0;
      }
    }
  }
}
double *ComputeGridLinePositions(size_t *returnArrayLength, double cMin, double cMax, StringArrayReference *labels, NumberArrayReference *priorities){
  double *positions;
  size_t positionsLength;
  double cLength, p, pMin, pMax, pInterval, pNum, i, num, rem, priority, mode;

  cLength = cMax - cMin;

  p = floor(log10(cLength));
  pInterval = pow(10.0, p);
  /* gives 10-1 lines for 100-10 diff */
  pMin = ceil(cMin/pInterval)*pInterval;
  pMax = floor(cMax/pInterval)*pInterval;
  pNum = Round((pMax - pMin)/pInterval + 1.0);

  mode = 1.0;

  if(pNum <= 3.0){
    p = floor(log10(cLength) - 1.0);
    /* gives 100-10 lines for 100-10 diff */
    pInterval = pow(10.0, p);
    pMin = ceil(cMin/pInterval)*pInterval;
    pMax = floor(cMax/pInterval)*pInterval;
    pNum = Round((pMax - pMin)/pInterval + 1.0);

    mode = 4.0;
  }else if(pNum <= 6.0){
    p = floor(log10(cLength));
    pInterval = pow(10.0, p)/4.0;
    /* gives 40-5 lines for 100-10 diff */
    pMin = ceil(cMin/pInterval)*pInterval;
    pMax = floor(cMax/pInterval)*pInterval;
    pNum = Round((pMax - pMin)/pInterval + 1.0);

    mode = 3.0;
  }else if(pNum <= 10.0){
    p = floor(log10(cLength));
    pInterval = pow(10.0, p)/2.0;
    /* gives 20-3 lines for 100-10 diff */
    pMin = ceil(cMin/pInterval)*pInterval;
    pMax = floor(cMax/pInterval)*pInterval;
    pNum = Round((pMax - pMin)/pInterval + 1.0);

    mode = 2.0;
  }

  positions = (double*)malloc(sizeof(double) * (pNum));
  positionsLength = pNum;
  labels->stringArray = (StringReference**)malloc(sizeof(StringReference) * pNum);
  labels->stringArrayLength = pNum;
  priorities->numberArray = (double*)malloc(sizeof(double) * (pNum));
  priorities->numberArrayLength = pNum;

  for(i = 0.0; i < pNum; i = i + 1.0){
    num = pMin + pInterval*i;
    positions[(int)(i)] = num;

    /* Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn. */
    priority = 1.0;

    /* Prioritize x.25, x.5 and x.75 lower. */
    if(mode == 2.0 || mode == 3.0){
      rem = fmod(fabs(round(num/pow(10.0, p - 2.0))), 100.0);

      priority = 1.0;
      if(rem == 50.0){
        priority = 2.0;
      }else if(rem == 25.0 || rem == 75.0){
        priority = 3.0;
      }
    }

    /* Prioritize x.1-x.4 and x.6-x.9 lower */
    if(mode == 4.0){
      rem = fmod(fabs(Round(num/pow(10.0, p))), 10.0);

      priority = 1.0;
      if(rem == 1.0 || rem == 2.0 || rem == 3.0 || rem == 4.0 || rem == 6.0 || rem == 7.0 || rem == 8.0 || rem == 9.0){
        priority = 2.0;
      }
    }

    /* 0 has lowest priority. */
    if(EpsilonCompare(num, 0.0, pow(10.0, p - 5.0))){
      priority = 3.0;
    }

    priorities->numberArray[(int)(i)] = priority;

    /* The label itself. */
    labels->stringArray[(int)(i)] = (StringReference *)malloc(sizeof(StringReference));
    if(p < 0.0){
      if(mode == 2.0 || mode == 3.0){
        num = RoundToDigits(num,  -(p - 1.0));
      }else{
        num = RoundToDigits(num,  -p);
      }
    }
    labels->stringArray[(int)(i)]->string = CreateStringDecimalFromNumber(&labels->stringArray[(int)(i)]->stringLength, num);
  }

  *returnArrayLength = positionsLength;
  return positions;
}
double MapYCoordinate(double y, double yMin, double yMax, double yPixelMin, double yPixelMax){
  double yLength, yPixelLength;

  yLength = yMax - yMin;
  yPixelLength = yPixelMax - yPixelMin;

  y = y - yMin;
  y = y*yPixelLength/yLength;
  y = yPixelLength - y;
  y = y + yPixelMin;
  return y;
}
double MapXCoordinate(double x, double xMin, double xMax, double xPixelMin, double xPixelMax){
  double xLength, xPixelLength;

  xLength = xMax - xMin;
  xPixelLength = xPixelMax - xPixelMin;

  x = x - xMin;
  x = x*xPixelLength/xLength;
  x = x + xPixelMin;
  return x;
}
double MapXCoordinateAutoSettings(double x, RGBABitmapImage *image, double *xs, size_t xsLength){
  return MapXCoordinate(x, GetMinimum(xs, xsLength), GetMaximum(xs, xsLength) - GetMinimum(xs, xsLength), GetDefaultPaddingPercentage()*ImageWidth(image), (1.0 - GetDefaultPaddingPercentage())*ImageWidth(image));
}
double MapYCoordinateAutoSettings(double y, RGBABitmapImage *image, double *ys, size_t ysLength){
  return MapYCoordinate(y, GetMinimum(ys, ysLength), GetMaximum(ys, ysLength), GetDefaultPaddingPercentage()*ImageHeight(image), (1.0 - GetDefaultPaddingPercentage())*ImageHeight(image));
}
double GetDefaultPaddingPercentage(){
  return 0.10;
}
void DrawText(RGBABitmapImage *canvas, double x, double y, wchar_t *text, size_t textLength, RGBA *color){
  double i, charWidth, spacing;

  charWidth = 8.0;
  spacing = 2.0;

  for(i = 0.0; i < textLength; i = i + 1.0){
    DrawAsciiCharacter(canvas, x + i*(charWidth + spacing), y, text[(int)(i)], color);
  }
}
void DrawTextUpwards(RGBABitmapImage *canvas, double x, double y, wchar_t *text, size_t textLength, RGBA *color){
  RGBABitmapImage *buffer, *rotated;

  buffer = CreateImage(GetTextWidth(text, textLength), GetTextHeight(text, textLength), GetTransparent());
  DrawText(buffer, 0.0, 0.0, text, textLength, color);
  rotated = RotateAntiClockwise90Degrees(buffer);
  DrawImageOnImage(canvas, rotated, x, y);
  DeleteImage(buffer);
  DeleteImage(rotated);
}
ScatterPlotSettings *GetDefaultScatterPlotSettings(){
  ScatterPlotSettings *settings;

  settings = (ScatterPlotSettings *)malloc(sizeof(ScatterPlotSettings));

  settings->autoBoundaries = true;
  settings->xMax = 0.0;
  settings->xMin = 0.0;
  settings->yMax = 0.0;
  settings->yMin = 0.0;
  settings->autoPadding = true;
  settings->xPadding = 0.0;
  settings->yPadding = 0.0;
  settings->title = L"";
  settings->titleLength = wcslen(settings->title);
  settings->yLabel = L"";
  settings->yLabelLength = wcslen(settings->yLabel);
  settings->xLabel = L"";
  settings->xLabelLength = wcslen(settings->xLabel);
  settings->scatterPlotSeries = (ScatterPlotSeries**)malloc(sizeof(ScatterPlotSeries) * 0.0);
  settings->scatterPlotSeriesLength = 0.0;
  settings->showGrid = true;
  settings->gridColor = GetGray(0.1);
  settings->xAxisAuto = true;
  settings->xAxisTop = false;
  settings->xAxisBottom = false;
  settings->yAxisAuto = true;
  settings->yAxisLeft = false;
  settings->yAxisRight = false;

  return settings;
}
ScatterPlotSeries *GetDefaultScatterPlotSeriesSettings(){
  ScatterPlotSeries *series;

  series = (ScatterPlotSeries *)malloc(sizeof(ScatterPlotSeries));

  series->linearInterpolation = true;
  series->pointType = L"pixels";
  series->pointTypeLength = wcslen(series->pointType);
  series->lineType = L"solid";
  series->lineTypeLength = wcslen(series->lineType);
  series->lineThickness = 1.0;
  series->xs = (double*)malloc(sizeof(double) * (0.0));
  series->xsLength = 0.0;
  series->ys = (double*)malloc(sizeof(double) * (0.0));
  series->ysLength = 0.0;
  series->color = GetBlack();

  return series;
}
void DrawScatterPlot(RGBABitmapImageReference *canvasReference, double width, double height, double *xs, size_t xsLength, double *ys, size_t ysLength){
  ScatterPlotSettings *settings;

  settings = GetDefaultScatterPlotSettings();

  settings->width = width;
  settings->height = height;
  settings->scatterPlotSeries = (ScatterPlotSeries**)malloc(sizeof(ScatterPlotSeries) * 1.0);
  settings->scatterPlotSeriesLength = 1.0;
  settings->scatterPlotSeries[0] = GetDefaultScatterPlotSeriesSettings();
  free(settings->scatterPlotSeries[0]->xs);
  settings->scatterPlotSeries[0]->xs = xs;
  settings->scatterPlotSeries[0]->xsLength = xsLength;
  free(settings->scatterPlotSeries[0]->ys);
  settings->scatterPlotSeries[0]->ys = ys;
  settings->scatterPlotSeries[0]->ysLength = ysLength;

  DrawScatterPlotFromSettings(canvasReference, settings);
}
_Bool DrawScatterPlotFromSettings(RGBABitmapImageReference *canvasReference, ScatterPlotSettings *settings){
  double xMin, xMax, yMin, yMax, xLength, yLength, i, x, y, xPrev, yPrev, px, py, pxPrev, pyPrev, originX, originY, p, l, plot;
  double xPadding, yPadding, originXPixels, originYPixels;
  double xPixelMin, yPixelMin, xPixelMax, yPixelMax, xLengthPixels, yLengthPixels, axisLabelPadding;
  NumberReference *nextRectangle, *x1Ref, *y1Ref, *x2Ref, *y2Ref, *patternOffset;
  _Bool prevSet, success;
  RGBA *gridLabelColor;
  RGBABitmapImage *canvas;
  double *xs, *ys;
  size_t xsLength, ysLength;
  _Bool linearInterpolation;
  ScatterPlotSeries *sp;
  double *xGridPositions, *yGridPositions;
  size_t xGridPositionsLength, yGridPositionsLength;
  StringArrayReference *xLabels, *yLabels;
  NumberArrayReference *xLabelPriorities, *yLabelPriorities;
  Rectangle **occupied;
  size_t occupiedLength;
  _Bool *linePattern;
  size_t linePatternLength;
  _Bool originXInside, originYInside, textOnLeft, textOnBottom;
  double originTextX, originTextY, originTextXPixels, originTextYPixels, side;

  canvas = CreateImage(settings->width, settings->height, GetWhite());
  patternOffset = CreateNumberReference(0.0);

  success = ScatterPlotFromSettingsValid(settings);

  if(success){

    if(settings->scatterPlotSeriesLength >= 1.0){
      xMin = GetMinimum(settings->scatterPlotSeries[0]->xs, settings->scatterPlotSeries[0]->xsLength);
      xMax = GetMaximum(settings->scatterPlotSeries[0]->xs, settings->scatterPlotSeries[0]->xsLength);
      yMin = GetMinimum(settings->scatterPlotSeries[0]->ys, settings->scatterPlotSeries[0]->ysLength);
      yMax = GetMaximum(settings->scatterPlotSeries[0]->ys, settings->scatterPlotSeries[0]->ysLength);
    }else{
      xMin =  -10.0;
      xMax = 10.0;
      yMin =  -10.0;
      yMax = 10.0;
    }

    if( !settings->autoBoundaries ){
      xMin = settings->xMin;
      xMax = settings->xMax;
      yMin = settings->yMin;
      yMax = settings->yMax;
    }else{
      for(plot = 1.0; plot < settings->scatterPlotSeriesLength; plot = plot + 1.0){
        sp = settings->scatterPlotSeries[(int)(plot)];

        xMin = fmin(xMin, GetMinimum(sp->xs, sp->xsLength));
        xMax = fmax(xMax, GetMaximum(sp->xs, sp->xsLength));
        yMin = fmin(yMin, GetMinimum(sp->ys, sp->ysLength));
        yMax = fmax(yMax, GetMaximum(sp->ys, sp->ysLength));
      }
    }

    xLength = xMax - xMin;
    yLength = yMax - yMin;

    if(settings->autoPadding){
      xPadding = floor(GetDefaultPaddingPercentage()*ImageWidth(canvas));
      yPadding = floor(GetDefaultPaddingPercentage()*ImageHeight(canvas));
    }else{
      xPadding = settings->xPadding;
      yPadding = settings->yPadding;
    }

    /* Draw title */
    DrawText(canvas, floor(ImageWidth(canvas)/2.0 - GetTextWidth(settings->title, settings->titleLength)/2.0), floor(yPadding/3.0), settings->title, settings->titleLength, GetBlack());

    /* Draw grid */
    xPixelMin = xPadding;
    yPixelMin = yPadding;
    xPixelMax = ImageWidth(canvas) - xPadding;
    yPixelMax = ImageHeight(canvas) - yPadding;
    xLengthPixels = xPixelMax - xPixelMin;
    yLengthPixels = yPixelMax - yPixelMin;
    DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings->gridColor);

    gridLabelColor = GetGray(0.5);

    xLabels = (StringArrayReference *)malloc(sizeof(StringArrayReference));
    xLabelPriorities = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
    yLabels = (StringArrayReference *)malloc(sizeof(StringArrayReference));
    yLabelPriorities = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
    xGridPositions = ComputeGridLinePositions(&xGridPositionsLength, xMin, xMax, xLabels, xLabelPriorities);
    yGridPositions = ComputeGridLinePositions(&yGridPositionsLength, yMin, yMax, yLabels, yLabelPriorities);

    if(settings->showGrid){
      /* X-grid */
      for(i = 0.0; i < xGridPositionsLength; i = i + 1.0){
        x = xGridPositions[(int)(i)];
        px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
        DrawLine1px(canvas, px, yPixelMin, px, yPixelMax, settings->gridColor);
      }

      /* Y-grid */
      for(i = 0.0; i < yGridPositionsLength; i = i + 1.0){
        y = yGridPositions[(int)(i)];
        py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
        DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings->gridColor);
      }
    }

    /* Compute origin information. */
    originYInside = yMin < 0.0 && yMax > 0.0;
    originY = 0.0;
    if(settings->xAxisAuto){
      if(originYInside){
        originY = 0.0;
      }else{
        originY = yMin;
      }
    }else{
if(settings->xAxisTop){
        originY = yMax;
      }
      if(settings->xAxisBottom){
        originY = yMin;
      }
    }
    originYPixels = MapYCoordinate(originY, yMin, yMax, yPixelMin, yPixelMax);

    originXInside = xMin < 0.0 && xMax > 0.0;
    originX = 0.0;
    if(settings->yAxisAuto){
      if(originXInside){
        originX = 0.0;
      }else{
        originX = xMin;
      }
    }else{
if(settings->yAxisLeft){
        originX = xMin;
      }
      if(settings->yAxisRight){
        originX = xMax;
      }
    }
    originXPixels = MapXCoordinate(originX, xMin, xMax, xPixelMin, xPixelMax);

    if(originYInside){
      originTextY = 0.0;
    }else{
      originTextY = yMin + yLength/2.0;
    }
    originTextYPixels = MapYCoordinate(originTextY, yMin, yMax, yPixelMin, yPixelMax);

    if(originXInside){
      originTextX = 0.0;
    }else{
      originTextX = xMin + xLength/2.0;
    }
    originTextXPixels = MapXCoordinate(originTextX, xMin, xMax, xPixelMin, xPixelMax);

    /* Labels */
    occupied = (Rectangle**)malloc(sizeof(Rectangle) * xLabels->stringArrayLength + yLabels->stringArrayLength);
    occupiedLength = xLabels->stringArrayLength + yLabels->stringArrayLength;
    for(i = 0.0; i < occupiedLength; i = i + 1.0){
      occupied[(int)(i)] = CreateRectangle(0.0, 0.0, 0.0, 0.0);
    }
    nextRectangle = CreateNumberReference(0.0);

    /* x labels */
    for(i = 1.0; i <= 5.0; i = i + 1.0){
      textOnBottom = true;
      if( !settings->xAxisAuto  && settings->xAxisTop){
        textOnBottom = false;
      }
      DrawXLabelsForPriority(i, xMin, originYPixels, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xGridPositionsLength, xLabels, xLabelPriorities, occupied, occupiedLength, textOnBottom);
    }

    /* y labels */
    for(i = 1.0; i <= 5.0; i = i + 1.0){
      textOnLeft = true;
      if( !settings->yAxisAuto  && settings->yAxisRight){
        textOnLeft = false;
      }
      DrawYLabelsForPriority(i, yMin, originXPixels, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yGridPositionsLength, yLabels, yLabelPriorities, occupied, occupiedLength, textOnLeft);
    }

    /* Draw origin line axis titles. */
    axisLabelPadding = 20.0;

    /* x origin line */
    if(originYInside){
      DrawLine1px(canvas, Round(xPixelMin), Round(originYPixels), Round(xPixelMax), Round(originYPixels), GetBlack());
    }

    /* y origin line */
    if(originXInside){
      DrawLine1px(canvas, Round(originXPixels), Round(yPixelMin), Round(originXPixels), Round(yPixelMax), GetBlack());
    }

    /* Draw origin axis titles. */
    DrawTextUpwards(canvas, 10.0, floor(originTextYPixels - GetTextWidth(settings->xLabel, settings->xLabelLength)/2.0), settings->xLabel, settings->xLabelLength, GetBlack());
    DrawText(canvas, floor(originTextXPixels - GetTextWidth(settings->yLabel, settings->yLabelLength)/2.0), yPixelMax + axisLabelPadding, settings->yLabel, settings->yLabelLength, GetBlack());

    /* X-grid-markers */
    for(i = 0.0; i < xGridPositionsLength; i = i + 1.0){
      x = xGridPositions[(int)(i)];
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
      p = xLabelPriorities->numberArray[(int)(i)];
      l = 1.0;
      if(p == 1.0){
        l = 8.0;
      }else if(p == 2.0){
        l = 3.0;
      }
      side =  -1.0;
      if( !settings->xAxisAuto  && settings->xAxisTop){
        side = 1.0;
      }
      DrawLine1px(canvas, px, originYPixels, px, originYPixels + side*l, GetBlack());
    }

    /* Y-grid-markers */
    for(i = 0.0; i < yGridPositionsLength; i = i + 1.0){
      y = yGridPositions[(int)(i)];
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
      p = yLabelPriorities->numberArray[(int)(i)];
      l = 1.0;
      if(p == 1.0){
        l = 8.0;
      }else if(p == 2.0){
        l = 3.0;
      }
      side = 1.0;
      if( !settings->yAxisAuto  && settings->yAxisRight){
        side =  -1.0;
      }
      DrawLine1px(canvas, originXPixels, py, originXPixels + side*l, py, GetBlack());
    }

    /* Draw points */
    for(plot = 0.0; plot < settings->scatterPlotSeriesLength; plot = plot + 1.0){
      sp = settings->scatterPlotSeries[(int)(plot)];

      xs = sp->xs;
      xsLength = sp->xsLength;
      ys = sp->ys;
      ysLength = sp->ysLength;
      linearInterpolation = sp->linearInterpolation;

      x1Ref = (NumberReference *)malloc(sizeof(NumberReference));
      y1Ref = (NumberReference *)malloc(sizeof(NumberReference));
      x2Ref = (NumberReference *)malloc(sizeof(NumberReference));
      y2Ref = (NumberReference *)malloc(sizeof(NumberReference));
      if(linearInterpolation){
        prevSet = false;
        xPrev = 0.0;
        yPrev = 0.0;
        for(i = 0.0; i < xsLength; i = i + 1.0){
          x = xs[(int)(i)];
          y = ys[(int)(i)];

          if(prevSet){
            x1Ref->numberValue = xPrev;
            y1Ref->numberValue = yPrev;
            x2Ref->numberValue = x;
            y2Ref->numberValue = y;

            success = CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax);

            if(success){
              pxPrev = floor(MapXCoordinate(x1Ref->numberValue, xMin, xMax, xPixelMin, xPixelMax));
              pyPrev = floor(MapYCoordinate(y1Ref->numberValue, yMin, yMax, yPixelMin, yPixelMax));
              px = floor(MapXCoordinate(x2Ref->numberValue, xMin, xMax, xPixelMin, xPixelMax));
              py = floor(MapYCoordinate(y2Ref->numberValue, yMin, yMax, yPixelMin, yPixelMax));

              if(aStringsEqual(sp->lineType, sp->lineTypeLength, strparam(L"solid")) && sp->lineThickness == 1.0){
                DrawLine1px(canvas, pxPrev, pyPrev, px, py, sp->color);
              }else if(aStringsEqual(sp->lineType, sp->lineTypeLength, strparam(L"solid"))){
                DrawLine(canvas, pxPrev, pyPrev, px, py, sp->lineThickness, sp->color);
              }else if(aStringsEqual(sp->lineType, sp->lineTypeLength, strparam(L"dashed"))){
                linePattern = GetLinePattern1(&linePatternLength);
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp->lineThickness, linePattern, linePatternLength, patternOffset, sp->color);
              }else if(aStringsEqual(sp->lineType, sp->lineTypeLength, strparam(L"dotted"))){
                linePattern = GetLinePattern2(&linePatternLength);
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp->lineThickness, linePattern, linePatternLength, patternOffset, sp->color);
              }else if(aStringsEqual(sp->lineType, sp->lineTypeLength, strparam(L"dotdash"))){
                linePattern = GetLinePattern3(&linePatternLength);
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp->lineThickness, linePattern, linePatternLength, patternOffset, sp->color);
              }else if(aStringsEqual(sp->lineType, sp->lineTypeLength, strparam(L"longdash"))){
                linePattern = GetLinePattern4(&linePatternLength);
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp->lineThickness, linePattern, linePatternLength, patternOffset, sp->color);
              }else if(aStringsEqual(sp->lineType, sp->lineTypeLength, strparam(L"twodash"))){
                linePattern = GetLinePattern5(&linePatternLength);
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp->lineThickness, linePattern, linePatternLength, patternOffset, sp->color);
              }
            }
          }

          prevSet = true;
          xPrev = x;
          yPrev = y;
        }
      }else{
        for(i = 0.0; i < xsLength; i = i + 1.0){
          x = xs[(int)(i)];
          y = ys[(int)(i)];

          if(x > xMin && x < xMax && y > yMin && y < yMax){

            x = floor(MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax));
            y = floor(MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax));

            if(aStringsEqual(sp->pointType, sp->pointTypeLength, strparam(L"crosses"))){
              DrawPixel(canvas, x, y, sp->color);
              DrawPixel(canvas, x + 1.0, y, sp->color);
              DrawPixel(canvas, x + 2.0, y, sp->color);
              DrawPixel(canvas, x - 1.0, y, sp->color);
              DrawPixel(canvas, x - 2.0, y, sp->color);
              DrawPixel(canvas, x, y + 1.0, sp->color);
              DrawPixel(canvas, x, y + 2.0, sp->color);
              DrawPixel(canvas, x, y - 1.0, sp->color);
              DrawPixel(canvas, x, y - 2.0, sp->color);
            }else if(aStringsEqual(sp->pointType, sp->pointTypeLength, strparam(L"circles"))){
              DrawCircle(canvas, x, y, 3.0, sp->color);
            }else if(aStringsEqual(sp->pointType, sp->pointTypeLength, strparam(L"dots"))){
              DrawFilledCircle(canvas, x, y, 3.0, sp->color);
            }else if(aStringsEqual(sp->pointType, sp->pointTypeLength, strparam(L"triangles"))){
              DrawTriangle(canvas, x, y, 3.0, sp->color);
            }else if(aStringsEqual(sp->pointType, sp->pointTypeLength, strparam(L"filled triangles"))){
              DrawFilledTriangle(canvas, x, y, 3.0, sp->color);
            }else if(aStringsEqual(sp->pointType, sp->pointTypeLength, strparam(L"pixels"))){
              DrawPixel(canvas, x, y, sp->color);
            }
          }
        }
      }
    }

    DeleteImage(canvasReference->image);
    canvasReference->image = canvas;
  }

  return success;
}
_Bool ScatterPlotFromSettingsValid(ScatterPlotSettings *settings){
  _Bool success, found;
  ScatterPlotSeries *series;
  double i;

  success = true;

  /* Check axis placement. */
  if( !settings->xAxisAuto ){
    if(settings->xAxisTop && settings->xAxisBottom){
      success = false;
    }
    if( !settings->xAxisTop  &&  !settings->xAxisBottom ){
      success = false;
    }
  }

  if( !settings->yAxisAuto ){
    if(settings->yAxisLeft && settings->yAxisRight){
      success = false;
    }
    if( !settings->yAxisLeft  &&  !settings->yAxisRight ){
      success = false;
    }
  }

  /* Check series lengths. */
  for(i = 0.0; i < settings->scatterPlotSeriesLength; i = i + 1.0){
    series = settings->scatterPlotSeries[(int)(i)];
    if(series->xsLength != series->ysLength){
      success = false;
    }
    if(series->xsLength == 0.0){
      success = false;
    }
    if(series->linearInterpolation && series->xsLength == 1.0){
      success = false;
    }
  }

  /* Check bounds. */
  if( !settings->autoBoundaries ){
    if(settings->xMin >= settings->xMax){
      success = false;
    }
    if(settings->yMin >= settings->yMax){
      success = false;
    }
  }

  /* Check padding. */
  if( !settings->autoPadding ){
    if(2.0*settings->xPadding >= settings->width){
      success = false;
    }
    if(2.0*settings->yPadding >= settings->height){
      success = false;
    }
  }

  /* Check width and height. */
  if(settings->width < 0.0){
    success = false;
  }
  if(settings->height < 0.0){
    success = false;
  }

  /* Check point types. */
  for(i = 0.0; i < settings->scatterPlotSeriesLength; i = i + 1.0){
    series = settings->scatterPlotSeries[(int)(i)];

    if(series->lineThickness < 0.0){
      success = false;
    }

    if( !series->linearInterpolation ){
      /* Point type. */
      found = false;
      if(aStringsEqual(series->pointType, series->pointTypeLength, strparam(L"crosses"))){
        found = true;
      }else if(aStringsEqual(series->pointType, series->pointTypeLength, strparam(L"circles"))){
        found = true;
      }else if(aStringsEqual(series->pointType, series->pointTypeLength, strparam(L"dots"))){
        found = true;
      }else if(aStringsEqual(series->pointType, series->pointTypeLength, strparam(L"triangles"))){
        found = true;
      }else if(aStringsEqual(series->pointType, series->pointTypeLength, strparam(L"filled triangles"))){
        found = true;
      }else if(aStringsEqual(series->pointType, series->pointTypeLength, strparam(L"pixels"))){
        found = true;
      }
      if( !found ){
        success = false;
      }
    }else{
      /* Line type. */
      found = false;
      if(aStringsEqual(series->lineType, series->lineTypeLength, strparam(L"solid"))){
        found = true;
      }else if(aStringsEqual(series->lineType, series->lineTypeLength, strparam(L"dashed"))){
        found = true;
      }else if(aStringsEqual(series->lineType, series->lineTypeLength, strparam(L"dotted"))){
        found = true;
      }else if(aStringsEqual(series->lineType, series->lineTypeLength, strparam(L"dotdash"))){
        found = true;
      }else if(aStringsEqual(series->lineType, series->lineTypeLength, strparam(L"longdash"))){
        found = true;
      }else if(aStringsEqual(series->lineType, series->lineTypeLength, strparam(L"twodash"))){
        found = true;
      }

      if( !found ){
        success = false;
      }
    }
  }

  return success;
}
BarPlotSettings *GetDefaultBarPlotSettings(){
  BarPlotSettings *settings;

  settings = (BarPlotSettings *)malloc(sizeof(BarPlotSettings));

  settings->width = 800.0;
  settings->height = 600.0;
  settings->autoBoundaries = true;
  settings->yMax = 0.0;
  settings->yMin = 0.0;
  settings->autoPadding = true;
  settings->xPadding = 0.0;
  settings->yPadding = 0.0;
  settings->title = L"";
  settings->titleLength = wcslen(settings->title);
  settings->yLabel = L"";
  settings->yLabelLength = wcslen(settings->yLabel);
  settings->barPlotSeries = (BarPlotSeries**)malloc(sizeof(BarPlotSeries) * 0.0);
  settings->barPlotSeriesLength = 0.0;
  settings->showGrid = true;
  settings->gridColor = GetGray(0.1);
  settings->autoColor = true;
  settings->grayscaleAutoColor = false;
  settings->autoSpacing = true;
  settings->groupSeparation = 0.0;
  settings->barSeparation = 0.0;
  settings->autoLabels = true;
  settings->xLabels = (StringReference**)malloc(sizeof(StringReference) * 0.0);
  settings->xLabelsLength = 0.0;
  /*settings.autoLabels = false;
        settings.xLabels = new StringReference [5];
        settings.xLabels[0] = CreateStringReference("may 20".toCharArray());
        settings.xLabels[1] = CreateStringReference("jun 20".toCharArray());
        settings.xLabels[2] = CreateStringReference("jul 20".toCharArray());
        settings.xLabels[3] = CreateStringReference("aug 20".toCharArray());
        settings.xLabels[4] = CreateStringReference("sep 20".toCharArray()); */
  settings->barBorder = false;

  return settings;
}
BarPlotSeries *GetDefaultBarPlotSeriesSettings(){
  BarPlotSeries *series;

  series = (BarPlotSeries *)malloc(sizeof(BarPlotSeries));

  series->ys = (double*)malloc(sizeof(double) * (0.0));
  series->ysLength = 0.0;
  series->color = GetBlack();

  return series;
}
RGBABitmapImage *DrawBarPlot(double width, double height, double *ys, size_t ysLength){
  BarPlotSettings *settings;
  RGBABitmapImageReference *canvasReference;

  settings = GetDefaultBarPlotSettings();

  settings->barPlotSeries = (BarPlotSeries**)malloc(sizeof(BarPlotSeries) * 1.0);
  settings->barPlotSeriesLength = 1.0;
  settings->barPlotSeries[0] = GetDefaultBarPlotSeriesSettings();
  free(settings->barPlotSeries[0]->ys);
  settings->barPlotSeries[0]->ys = ys;
  settings->barPlotSeries[0]->ysLength = ysLength;
  canvasReference = (RGBABitmapImageReference *)malloc(sizeof(RGBABitmapImageReference));
  settings->width = width;
  settings->height = height;

  DrawBarPlotFromSettings(canvasReference, settings);

  return canvasReference->image;
}
_Bool DrawBarPlotFromSettings(RGBABitmapImageReference *canvasReference, BarPlotSettings *settings){
  double xPadding, yPadding;
  double xPixelMin, yPixelMin, yPixelMax, xPixelMax;
  double xLengthPixels, yLengthPixels;
  double s, n, y, x, w, h, yMin, yMax, b, i, py, yValue;
  RGBA **colors;
  size_t colorsLength;
  double *ys, *yGridPositions;
  size_t ysLength, yGridPositionsLength;
  double yTop, yBottom, ss, bs, yLength;
  double groupSeparation, barSeparation, barWidth, textwidth;
  StringArrayReference *yLabels;
  NumberArrayReference *yLabelPriorities;
  Rectangle **occupied;
  size_t occupiedLength;
  NumberReference *nextRectangle;
  RGBA *gridLabelColor, *barColor;
  wchar_t *label;
  size_t labelLength;
  _Bool success;
  RGBABitmapImage *canvas;

  success = BarPlotSettingsIsValid(settings);

  if(success){

    canvas = CreateImage(settings->width, settings->height, GetWhite());

    ss = settings->barPlotSeriesLength;
    gridLabelColor = GetGray(0.5);

    /* padding */
    if(settings->autoPadding){
      xPadding = floor(GetDefaultPaddingPercentage()*ImageWidth(canvas));
      yPadding = floor(GetDefaultPaddingPercentage()*ImageHeight(canvas));
    }else{
      xPadding = settings->xPadding;
      yPadding = settings->yPadding;
    }

    /* Draw title */
    DrawText(canvas, floor(ImageWidth(canvas)/2.0 - GetTextWidth(settings->title, settings->titleLength)/2.0), floor(yPadding/3.0), settings->title, settings->titleLength, GetBlack());
    DrawTextUpwards(canvas, 10.0, floor(ImageHeight(canvas)/2.0 - GetTextWidth(settings->yLabel, settings->yLabelLength)/2.0), settings->yLabel, settings->yLabelLength, GetBlack());

    /* min and max */
    if(settings->autoBoundaries){
      if(ss >= 1.0){
        yMax = GetMaximum(settings->barPlotSeries[0]->ys, settings->barPlotSeries[0]->ysLength);
        yMin = fmin(0.0, GetMinimum(settings->barPlotSeries[0]->ys, settings->barPlotSeries[0]->ysLength));

        for(s = 0.0; s < ss; s = s + 1.0){
          yMax = fmax(yMax, GetMaximum(settings->barPlotSeries[(int)(s)]->ys, settings->barPlotSeries[(int)(s)]->ysLength));
          yMin = fmin(yMin, GetMinimum(settings->barPlotSeries[(int)(s)]->ys, settings->barPlotSeries[(int)(s)]->ysLength));
        }
      }else{
        yMax = 10.0;
        yMin = 0.0;
      }
    }else{
      yMin = settings->yMin;
      yMax = settings->yMax;
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
    DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings->gridColor);

    /* Draw grid lines. */
    yLabels = (StringArrayReference *)malloc(sizeof(StringArrayReference));
    yLabelPriorities = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
    yGridPositions = ComputeGridLinePositions(&yGridPositionsLength, yMin, yMax, yLabels, yLabelPriorities);

    if(settings->showGrid){
      /* Y-grid */
      for(i = 0.0; i < yGridPositionsLength; i = i + 1.0){
        y = yGridPositions[(int)(i)];
        py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
        DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings->gridColor);
      }
    }

    /* Draw origin. */
    if(yMin < 0.0 && yMax > 0.0){
      py = MapYCoordinate(0.0, yMin, yMax, yPixelMin, yPixelMax);
      DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings->gridColor);
    }

    /* Labels */
    occupied = (Rectangle**)malloc(sizeof(Rectangle) * yLabels->stringArrayLength);
    occupiedLength = yLabels->stringArrayLength;
    for(i = 0.0; i < occupiedLength; i = i + 1.0){
      occupied[(int)(i)] = CreateRectangle(0.0, 0.0, 0.0, 0.0);
    }
    nextRectangle = CreateNumberReference(0.0);

    for(i = 1.0; i <= 5.0; i = i + 1.0){
      DrawYLabelsForPriority(i, yMin, xPixelMin, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yGridPositionsLength, yLabels, yLabelPriorities, occupied, occupiedLength, true);
    }

    /* Draw bars. */
    if(settings->autoColor){
      if( !settings->grayscaleAutoColor ){
        colors = Get8HighContrastColors(&colorsLength);
      }else{
        colors = (RGBA**)malloc(sizeof(RGBA) * ss);
        colorsLength = ss;
        if(ss > 1.0){
          for(i = 0.0; i < ss; i = i + 1.0){
            colors[(int)(i)] = GetGray(0.7 - (i/ss)*0.7);
          }
        }else{
          colors[0] = GetGray(0.5);
        }
      }
    }else{
      colors = (RGBA**)malloc(sizeof(RGBA) * 0.0);
      colorsLength = 0.0;
    }

    /* distances */
    bs = settings->barPlotSeries[0]->ysLength;

    if(settings->autoSpacing){
      groupSeparation = ImageWidth(canvas)*0.05;
      barSeparation = ImageWidth(canvas)*0.005;
    }else{
      groupSeparation = settings->groupSeparation;
      barSeparation = settings->barSeparation;
    }

    barWidth = (xLengthPixels - groupSeparation*(bs - 1.0) - barSeparation*(bs*(ss - 1.0)))/(bs*ss);

    /* Draw bars. */
    b = 0.0;
    for(n = 0.0; n < bs; n = n + 1.0){
      for(s = 0.0; s < ss; s = s + 1.0){
        ys = settings->barPlotSeries[(int)(s)]->ys;
        ysLength = settings->barPlotSeries[(int)(s)]->ysLength;

        yValue = ys[(int)(n)];

        yBottom = MapYCoordinate(yValue, yMin, yMax, yPixelMin, yPixelMax);
        yTop = MapYCoordinate(0.0, yMin, yMax, yPixelMin, yPixelMax);

        x = xPixelMin + n*(groupSeparation + ss*barWidth) + s*(barWidth) + b*barSeparation;
        w = barWidth;

        if(yValue >= 0.0){
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
          if(yValue >= 0.0){
            h = yTop - y;
          }else{
            h = yBottom - y;
          }
        }else if(y + h > yPixelMax){
          h = yPixelMax - y;
        }

        /* Get color */
        if(settings->autoColor){
          barColor = colors[(int)(s)];
        }else{
          barColor = settings->barPlotSeries[(int)(s)]->color;
        }

        /* Draw */
        if(settings->barBorder){
          DrawFilledRectangleWithBorder(canvas, Round(x), Round(y), Round(w), Round(h), GetBlack(), barColor);
        }else{
          DrawFilledRectangle(canvas, Round(x), Round(y), Round(w), Round(h), barColor);
        }

        b = b + 1.0;
      }
      b = b - 1.0;
    }

    /* x-labels */
    for(n = 0.0; n < bs; n = n + 1.0){
      if(settings->autoLabels){
        label = CreateStringDecimalFromNumber(&labelLength, n + 1.0);
      }else{
        label = settings->xLabels[(int)(n)]->string;
        labelLength = settings->xLabels[(int)(n)]->stringLength;
      }

      textwidth = GetTextWidth(label, labelLength);

      x = xPixelMin + (n + 0.5)*(ss*barWidth + (ss - 1.0)*barSeparation) + n*groupSeparation - textwidth/2.0;

      DrawText(canvas, floor(x), ImageHeight(canvas) - yPadding + 20.0, label, labelLength, gridLabelColor);

      b = b + 1.0;
    }

    canvasReference->image = canvas;
  }

  return success;
}
_Bool BarPlotSettingsIsValid(BarPlotSettings *settings){
  _Bool success, lengthSet;
  BarPlotSeries *series;
  double i, width, height, length;

  success = true;

  /* Check series lengths. */
  lengthSet = false;
  length = 0.0;
  for(i = 0.0; i < settings->barPlotSeriesLength; i = i + 1.0){
    series = settings->barPlotSeries[(int)(i)];

    if( !lengthSet ){
      length = series->ysLength;
      lengthSet = true;
    }else if(length != series->ysLength){
      success = false;
    }
  }

  /* Check bounds. */
  if( !settings->autoBoundaries ){
    if(settings->yMin >= settings->yMax){
      success = false;
    }
  }

  /* Check padding. */
  if( !settings->autoPadding ){
    if(2.0*settings->xPadding >= settings->width){
      success = false;
    }
    if(2.0*settings->yPadding >= settings->height){
      success = false;
    }
  }

  /* Check width and height. */
  if(settings->width < 0.0){
    success = false;
  }
  if(settings->height < 0.0){
    success = false;
  }

  /* Check spacing */
  if( !settings->autoSpacing ){
    if(settings->groupSeparation < 0.0){
      success = false;
    }
    if(settings->barSeparation < 0.0){
      success = false;
    }
  }

  return success;
}
double GetMinimum(double *data, size_t dataLength){
  double i, minimum;

  minimum = data[0];
  for(i = 0.0; i < dataLength; i = i + 1.0){
    minimum = fmin(minimum, data[(int)(i)]);
  }

  return minimum;
}
double GetMaximum(double *data, size_t dataLength){
  double i, maximum;

  maximum = data[0];
  for(i = 0.0; i < dataLength; i = i + 1.0){
    maximum = fmax(maximum, data[(int)(i)]);
  }

  return maximum;
}
double RoundToDigits(double element, double digitsAfterPoint){
  return Round(element*pow(10.0, digitsAfterPoint))/pow(10.0, digitsAfterPoint);
}
double test(){
  ScatterPlotSettings *scatterPlotSettings;
  double z;
  double *gridlines;
  size_t gridlinesLength;
  NumberReference *failures;
  StringArrayReference *labels;
  NumberArrayReference *labelPriorities;
  RGBABitmapImageReference *imageReference;
  double *xs, *ys;
  size_t xsLength, ysLength;

  failures = CreateNumberReference(0.0);

  imageReference = CreateRGBABitmapImageReference();

  scatterPlotSettings = GetDefaultScatterPlotSettings();

  labels = (StringArrayReference *)malloc(sizeof(StringArrayReference));
  labelPriorities = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));

  z = 10.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 11.0, failures);

  z = 9.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 19.0, failures);

  z = 8.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 17.0, failures);

  z = 7.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 15.0, failures);

  z = 6.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 13.0, failures);

  z = 5.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 21.0, failures);

  z = 4.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 17.0, failures);

  z = 3.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 31.0, failures);

  z = 2.0;
  gridlines = ComputeGridLinePositions(&gridlinesLength,  -z/2.0, z/2.0, labels, labelPriorities);
  AssertEquals(gridlinesLength, 21.0, failures);

  xs = (double*)malloc(sizeof(double) * (5.0));
  xsLength = 5.0;
  xs[0] =  -2.0;
  xs[1] =  -1.0;
  xs[2] = 0.0;
  xs[3] = 1.0;
  xs[4] = 2.0;
  ys = (double*)malloc(sizeof(double) * (5.0));
  ysLength = 5.0;
  ys[0] = 2.0;
  ys[1] =  -1.0;
  ys[2] =  -2.0;
  ys[3] =  -1.0;
  ys[4] = 2.0;
  DrawScatterPlot(imageReference, 800.0, 600.0, xs, xsLength, ys, ysLength);

  imageReference->image = DrawBarPlot(800.0, 600.0, ys, ysLength);

  return failures->numberValue;
}
RGBA *GetBlack(){
  RGBA *black;
  black = (RGBA *)malloc(sizeof(RGBA));
  black->a = 1.0;
  black->r = 0.0;
  black->g = 0.0;
  black->b = 0.0;
  return black;
}
RGBA *GetWhite(){
  RGBA *white;
  white = (RGBA *)malloc(sizeof(RGBA));
  white->a = 1.0;
  white->r = 1.0;
  white->g = 1.0;
  white->b = 1.0;
  return white;
}
RGBA *GetTransparent(){
  RGBA *transparent;
  transparent = (RGBA *)malloc(sizeof(RGBA));
  transparent->a = 0.0;
  transparent->r = 0.0;
  transparent->g = 0.0;
  transparent->b = 0.0;
  return transparent;
}
RGBA *GetGray(double percentage){
  RGBA *black;
  black = (RGBA *)malloc(sizeof(RGBA));
  black->a = 1.0;
  black->r = 1.0 - percentage;
  black->g = 1.0 - percentage;
  black->b = 1.0 - percentage;
  return black;
}
RGBA *CreateRGBColor(double r, double g, double b){
  RGBA *color;
  color = (RGBA *)malloc(sizeof(RGBA));
  color->a = 1.0;
  color->r = r;
  color->g = g;
  color->b = b;
  return color;
}
RGBA *CreateRGBAColor(double r, double g, double b, double a){
  RGBA *color;
  color = (RGBA *)malloc(sizeof(RGBA));
  color->a = a;
  color->r = r;
  color->g = g;
  color->b = b;
  return color;
}
RGBABitmapImage *CreateImage(double w, double h, RGBA *color){
  RGBABitmapImage *image;
  double i, j;

  image = (RGBABitmapImage *)malloc(sizeof(RGBABitmapImage));
  image->x = (RGBABitmap**)malloc(sizeof(RGBABitmap) * w);
  image->xLength = w;
  for(i = 0.0; i < w; i = i + 1.0){
    image->x[(int)(i)] = (RGBABitmap *)malloc(sizeof(RGBABitmap));
    image->x[(int)(i)]->y = (RGBA**)malloc(sizeof(RGBA) * h);
    image->x[(int)(i)]->yLength = h;
    for(j = 0.0; j < h; j = j + 1.0){
      image->x[(int)(i)]->y[(int)(j)] = (RGBA *)malloc(sizeof(RGBA));
      SetPixel(image, i, j, color);
    }
  }

  return image;
}
void DeleteImage(RGBABitmapImage *image){
  double i, j, w, h;

  w = ImageWidth(image);
  h = ImageHeight(image);

  for(i = 0.0; i < w; i = i + 1.0){
    for(j = 0.0; j < h; j = j + 1.0){
      free(image->x[(int)(i)]->y[(int)(j)]);
    }
    free(image->x[(int)(i)]);
  }
  free(image);
}
double ImageWidth(RGBABitmapImage *image){
  return image->xLength;
}
double ImageHeight(RGBABitmapImage *image){
  double height;

  if(ImageWidth(image) == 0.0){
    height = 0.0;
  }else{
    height = image->x[0]->yLength;
  }

  return height;
}
void SetPixel(RGBABitmapImage *image, double x, double y, RGBA *color){
  if(x >= 0.0 && x < ImageWidth(image) && y >= 0.0 && y < ImageHeight(image)){
    image->x[(int)(x)]->y[(int)(y)]->a = color->a;
    image->x[(int)(x)]->y[(int)(y)]->r = color->r;
    image->x[(int)(x)]->y[(int)(y)]->g = color->g;
    image->x[(int)(x)]->y[(int)(y)]->b = color->b;
  }
}
void DrawPixel(RGBABitmapImage *image, double x, double y, RGBA *color){
  double ra, ga, ba, aa;
  double rb, gb, bb, ab;
  double ro, go, bo, ao;

  if(x >= 0.0 && x < ImageWidth(image) && y >= 0.0 && y < ImageHeight(image)){
    ra = color->r;
    ga = color->g;
    ba = color->b;
    aa = color->a;

    rb = image->x[(int)(x)]->y[(int)(y)]->r;
    gb = image->x[(int)(x)]->y[(int)(y)]->g;
    bb = image->x[(int)(x)]->y[(int)(y)]->b;
    ab = image->x[(int)(x)]->y[(int)(y)]->a;

    ao = CombineAlpha(aa, ab);

    ro = AlphaBlend(ra, aa, rb, ab, ao);
    go = AlphaBlend(ga, aa, gb, ab, ao);
    bo = AlphaBlend(ba, aa, bb, ab, ao);

    image->x[(int)(x)]->y[(int)(y)]->r = ro;
    image->x[(int)(x)]->y[(int)(y)]->g = go;
    image->x[(int)(x)]->y[(int)(y)]->b = bo;
    image->x[(int)(x)]->y[(int)(y)]->a = ao;
  }
}
double CombineAlpha(double as, double ad){
  return as + ad*(1.0 - as);
}
double AlphaBlend(double cs, double as, double cd, double ad, double ao){
  return (cs*as + cd*ad*(1.0 - as))/ao;
}
void DrawHorizontalLine1px(RGBABitmapImage *image, double x, double y, double length, RGBA *color){
  double i;

  for(i = 0.0; i < length; i = i + 1.0){
    DrawPixel(image, x + i, y, color);
  }
}
void DrawVerticalLine1px(RGBABitmapImage *image, double x, double y, double height, RGBA *color){
  double i;

  for(i = 0.0; i < height; i = i + 1.0){
    DrawPixel(image, x, y + i, color);
  }
}
void DrawRectangle1px(RGBABitmapImage *image, double x, double y, double width, double height, RGBA *color){
  DrawHorizontalLine1px(image, x, y, width + 1.0, color);
  DrawVerticalLine1px(image, x, y + 1.0, height + 1.0 - 1.0, color);
  DrawVerticalLine1px(image, x + width, y + 1.0, height + 1.0 - 1.0, color);
  DrawHorizontalLine1px(image, x + 1.0, y + height, width + 1.0 - 2.0, color);
}
void DrawImageOnImage(RGBABitmapImage *dst, RGBABitmapImage *src, double topx, double topy){
  double y, x;

  for(y = 0.0; y < ImageHeight(src); y = y + 1.0){
    for(x = 0.0; x < ImageWidth(src); x = x + 1.0){
      if(topx + x >= 0.0 && topx + x < ImageWidth(dst) && topy + y >= 0.0 && topy + y < ImageHeight(dst)){
        DrawPixel(dst, topx + x, topy + y, src->x[(int)(x)]->y[(int)(y)]);
      }
    }
  }
}
void DrawLine1px(RGBABitmapImage *image, double x0, double y0, double x1, double y1, RGBA *color){
  XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color);
}
void XiaolinWusLineAlgorithm(RGBABitmapImage *image, double x0, double y0, double x1, double y1, RGBA *color){
  _Bool steep;
  double x, t, dx, dy, g, xEnd, yEnd, xGap, xpxl1, ypxl1, intery, xpxl2, ypxl2, olda;

  olda = color->a;

  steep = fabs(y1 - y0) > fabs(x1 - x0);

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

  if(dx == 0.0){
    g = 1.0;
  }

  xEnd = Round(x0);
  yEnd = y0 + g*(xEnd - x0);
  xGap = OneMinusFractionalPart(x0 + 0.5);
  xpxl1 = xEnd;
  ypxl1 = floor(yEnd);
  if(steep){
    DrawPixel(image, ypxl1, xpxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, ypxl1 + 1.0, xpxl1, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }else{
    DrawPixel(image, xpxl1, ypxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, xpxl1, ypxl1 + 1.0, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }
  intery = yEnd + g;

  xEnd = Round(x1);
  yEnd = y1 + g*(xEnd - x1);
  xGap = FractionalPart(x1 + 0.5);
  xpxl2 = xEnd;
  ypxl2 = floor(yEnd);
  if(steep){
    DrawPixel(image, ypxl2, xpxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, ypxl2 + 1.0, xpxl2, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }else{
    DrawPixel(image, xpxl2, ypxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
    DrawPixel(image, xpxl2, ypxl2 + 1.0, SetBrightness(color, FractionalPart(yEnd)*xGap));
  }

  if(steep){
    for(x = xpxl1 + 1.0; x <= xpxl2 - 1.0; x = x + 1.0){
      DrawPixel(image, floor(intery), x, SetBrightness(color, OneMinusFractionalPart(intery)));
      DrawPixel(image, floor(intery) + 1.0, x, SetBrightness(color, FractionalPart(intery)));
      intery = intery + g;
    }
  }else{
    for(x = xpxl1 + 1.0; x <= xpxl2 - 1.0; x = x + 1.0){
      DrawPixel(image, x, floor(intery), SetBrightness(color, OneMinusFractionalPart(intery)));
      DrawPixel(image, x, floor(intery) + 1.0, SetBrightness(color, FractionalPart(intery)));
      intery = intery + g;
    }
  }

  color->a = olda;
}
double OneMinusFractionalPart(double x){
  return 1.0 - FractionalPart(x);
}
double FractionalPart(double x){
  return x - floor(x);
}
RGBA *SetBrightness(RGBA *color, double newBrightness){
  color->a = newBrightness;
  return color;
}
void DrawQuadraticBezierCurve(RGBABitmapImage *image, double x0, double y0, double cx, double cy, double x1, double y1, RGBA *color){
  double t, dt, dx, dy;
  NumberReference *xs, *ys, *xe, *ye;

  dx = fabs(x0 - x1);
  dy = fabs(y0 - y1);

  dt = 1.0/sqrt(pow(dx, 2.0) + pow(dy, 2.0));

  xs = (NumberReference *)malloc(sizeof(NumberReference));
  ys = (NumberReference *)malloc(sizeof(NumberReference));
  xe = (NumberReference *)malloc(sizeof(NumberReference));
  ye = (NumberReference *)malloc(sizeof(NumberReference));

  QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, 0.0, xs, ys);
  for(t = dt; t <= 1.0; t = t + dt){
    QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, xe, ye);
    DrawLine1px(image, xs->numberValue, ys->numberValue, xe->numberValue, ye->numberValue, color);
    xs->numberValue = xe->numberValue;
    ys->numberValue = ye->numberValue;
  }

  free(xs);
  free(ys);
  free(xe);
  free(ye);
}
void QuadraticBezierPoint(double x0, double y0, double cx, double cy, double x1, double y1, double t, NumberReference *x, NumberReference *y){
  x->numberValue = pow(1.0 - t, 2.0)*x0 + (1.0 - t)*2.0*t*cx + pow(t, 2.0)*x1;
  y->numberValue = pow(1.0 - t, 2.0)*y0 + (1.0 - t)*2.0*t*cy + pow(t, 2.0)*y1;
}
void DrawCubicBezierCurve(RGBABitmapImage *image, double x0, double y0, double c0x, double c0y, double c1x, double c1y, double x1, double y1, RGBA *color){
  double t, dt, dx, dy;
  NumberReference *xs, *ys, *xe, *ye;

  dx = fabs(x0 - x1);
  dy = fabs(y0 - y1);

  dt = 1.0/sqrt(pow(dx, 2.0) + pow(dy, 2.0));

  xs = (NumberReference *)malloc(sizeof(NumberReference));
  ys = (NumberReference *)malloc(sizeof(NumberReference));
  xe = (NumberReference *)malloc(sizeof(NumberReference));
  ye = (NumberReference *)malloc(sizeof(NumberReference));

  CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, 0.0, xs, ys);
  for(t = dt; t <= 1.0; t = t + dt){
    CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, xe, ye);
    DrawLine1px(image, xs->numberValue, ys->numberValue, xe->numberValue, ye->numberValue, color);
    xs->numberValue = xe->numberValue;
    ys->numberValue = ye->numberValue;
  }

  free(xs);
  free(ys);
  free(xe);
  free(ye);
}
void CubicBezierPoint(double x0, double y0, double c0x, double c0y, double c1x, double c1y, double x1, double y1, double t, NumberReference *x, NumberReference *y){
  x->numberValue = pow(1.0 - t, 3.0)*x0 + pow(1.0 - t, 2.0)*3.0*t*c0x + (1.0 - t)*3.0*pow(t, 2.0)*c1x + pow(t, 3.0)*x1;

  y->numberValue = pow(1.0 - t, 3.0)*y0 + pow(1.0 - t, 2.0)*3.0*t*c0y + (1.0 - t)*3.0*pow(t, 2.0)*c1y + pow(t, 3.0)*y1;
}
RGBABitmapImage *CopyImage(RGBABitmapImage *image){
  RGBABitmapImage *copy;
  double i, j;

  copy = CreateImage(ImageWidth(image), ImageHeight(image), GetTransparent());

  for(i = 0.0; i < ImageWidth(image); i = i + 1.0){
    for(j = 0.0; j < ImageHeight(image); j = j + 1.0){
      SetPixel(copy, i, j, image->x[(int)(i)]->y[(int)(j)]);
    }
  }

  return copy;
}
RGBA *GetImagePixel(RGBABitmapImage *image, double x, double y){
  return image->x[(int)(x)]->y[(int)(y)];
}
void HorizontalFlip(RGBABitmapImage *img){
  double y, x;
  double tmp;
  RGBA *c1, *c2;

  for(y = 0.0; y < ImageHeight(img); y = y + 1.0){
    for(x = 0.0; x < ImageWidth(img)/2.0; x = x + 1.0){
      c1 = img->x[(int)(x)]->y[(int)(y)];
      c2 = img->x[(int)(ImageWidth(img) - 1.0 - x)]->y[(int)(y)];

      tmp = c1->a;
      c1->a = c2->a;
      c2->a = tmp;

      tmp = c1->r;
      c1->r = c2->r;
      c2->r = tmp;

      tmp = c1->g;
      c1->g = c2->g;
      c2->g = tmp;

      tmp = c1->b;
      c1->b = c2->b;
      c2->b = tmp;
    }
  }
}
void DrawFilledRectangle(RGBABitmapImage *image, double x, double y, double w, double h, RGBA *color){
  double i, j;

  for(i = 0.0; i < w; i = i + 1.0){
    for(j = 0.0; j < h; j = j + 1.0){
      SetPixel(image, x + i, y + j, color);
    }
  }
}
RGBABitmapImage *RotateAntiClockwise90Degrees(RGBABitmapImage *image){
  RGBABitmapImage *rotated;
  double x, y;

  rotated = CreateImage(ImageHeight(image), ImageWidth(image), GetBlack());

  for(y = 0.0; y < ImageHeight(image); y = y + 1.0){
    for(x = 0.0; x < ImageWidth(image); x = x + 1.0){
      SetPixel(rotated, y, ImageWidth(image) - 1.0 - x, GetImagePixel(image, x, y));
    }
  }

  return rotated;
}
void DrawCircle(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color){
  DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color);
}
void BresenhamsCircleDrawingAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color){
  double x, y, delta;

  y = radius;
  x = 0.0;

  delta = 3.0 - 2.0*radius;
  for(; y >= x; x = x + 1.0){
    DrawLine1px(canvas, xCenter + x, yCenter + y, xCenter + x, yCenter + y, color);
    DrawLine1px(canvas, xCenter + x, yCenter - y, xCenter + x, yCenter - y, color);
    DrawLine1px(canvas, xCenter - x, yCenter + y, xCenter - x, yCenter + y, color);
    DrawLine1px(canvas, xCenter - x, yCenter - y, xCenter - x, yCenter - y, color);

    DrawLine1px(canvas, xCenter - y, yCenter + x, xCenter - y, yCenter + x, color);
    DrawLine1px(canvas, xCenter - y, yCenter - x, xCenter - y, yCenter - x, color);
    DrawLine1px(canvas, xCenter + y, yCenter + x, xCenter + y, yCenter + x, color);
    DrawLine1px(canvas, xCenter + y, yCenter - x, xCenter + y, yCenter - x, color);

    if(delta < 0.0){
      delta = delta + 4.0*x + 6.0;
    }else{
      delta = delta + 4.0*(x - y) + 10.0;
      y = y - 1.0;
    }
  }
}
void DrawCircleMidpointAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color){
  double d, x, y;

  d = floor((5.0 - radius*4.0)/4.0);
  x = 0.0;
  y = radius;

  for(; x <= y; x = x + 1.0){
    DrawPixel(canvas, xCenter + x, yCenter + y, color);
    DrawPixel(canvas, xCenter + x, yCenter - y, color);
    DrawPixel(canvas, xCenter - x, yCenter + y, color);
    DrawPixel(canvas, xCenter - x, yCenter - y, color);
    DrawPixel(canvas, xCenter + y, yCenter + x, color);
    DrawPixel(canvas, xCenter + y, yCenter - x, color);
    DrawPixel(canvas, xCenter - y, yCenter + x, color);
    DrawPixel(canvas, xCenter - y, yCenter - x, color);

    if(d < 0.0){
      d = d + 2.0*x + 1.0;
    }else{
      d = d + 2.0*(x - y) + 1.0;
      y = y - 1.0;
    }
  }
}
void DrawCircleBasicAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color){
  double pixels, a, da, dx, dy;

  /* Place the circle in the center of the pixel. */
  xCenter = floor(xCenter) + 0.5;
  yCenter = floor(yCenter) + 0.5;

  pixels = 2.0*M_PI*radius;

  /* Below a radius of 10 pixels, over-compensate to get a smoother circle. */
  if(radius < 10.0){
    pixels = pixels*10.0;
  }

  da = 2.0*M_PI/pixels;

  for(a = 0.0; a < 2.0*M_PI; a = a + da){
    dx = cos(a)*radius;
    dy = sin(a)*radius;

    /* Floor to get the pixel coordinate. */
    DrawPixel(canvas, floor(xCenter + dx), floor(yCenter + dy), color);
  }
}
void DrawFilledCircle(RGBABitmapImage *canvas, double x, double y, double r, RGBA *color){
  DrawFilledCircleBasicAlgorithm(canvas, x, y, r, color);
}
void DrawFilledCircleMidpointAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color){
  double d, x, y;

  d = floor((5.0 - radius*4.0)/4.0);
  x = 0.0;
  y = radius;

  for(; x <= y; x = x + 1.0){
    DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter + y, xCenter - x, yCenter + y, color);
    DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter - y, xCenter - x, yCenter - y, color);
    DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter + x, xCenter - y, yCenter + x, color);
    DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter - x, xCenter - y, yCenter - x, color);

    if(d < 0.0){
      d = d + 2.0*x + 1.0;
    }else{
      d = d + 2.0*(x - y) + 1.0;
      y = y - 1.0;
    }
  }
}
void DrawFilledCircleBasicAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color){
  double pixels, a, da, dx, dy;

  /* Place the circle in the center of the pixel. */
  xCenter = floor(xCenter) + 0.5;
  yCenter = floor(yCenter) + 0.5;

  pixels = 2.0*M_PI*radius;

  /* Below a radius of 10 pixels, over-compensate to get a smoother circle. */
  if(radius < 10.0){
    pixels = pixels*10.0;
  }

  da = 2.0*M_PI/pixels;

  /* Draw lines for a half-circle to fill an entire circle. */
  for(a = 0.0; a < M_PI; a = a + da){
    dx = cos(a)*radius;
    dy = sin(a)*radius;

    /* Floor to get the pixel coordinate. */
    DrawVerticalLine1px(canvas, floor(xCenter - dx), floor(yCenter - dy), floor(2.0*dy) + 1.0, color);
  }
}
void DrawTriangle(RGBABitmapImage *canvas, double xCenter, double yCenter, double height, RGBA *color){
  double x1, y1, x2, y2, x3, y3;

  x1 = floor(xCenter + 0.5);
  y1 = floor(floor(yCenter + 0.5) - height);
  x2 = x1 - 2.0*height*tan(M_PI/6.0);
  y2 = floor(y1 + 2.0*height);
  x3 = x1 + 2.0*height*tan(M_PI/6.0);
  y3 = floor(y1 + 2.0*height);

  DrawLine1px(canvas, x1, y1, x2, y2, color);
  DrawLine1px(canvas, x1, y1, x3, y3, color);
  DrawLine1px(canvas, x2, y2, x3, y3, color);
}
void DrawFilledTriangle(RGBABitmapImage *canvas, double xCenter, double yCenter, double height, RGBA *color){
  double i, offset, x1, y1;

  x1 = floor(xCenter + 0.5);
  y1 = floor(floor(yCenter + 0.5) - height);

  for(i = 0.0; i <= 2.0*height; i = i + 1.0){
    offset = floor(i*tan(M_PI/6.0));
    DrawHorizontalLine1px(canvas, x1 - offset, y1 + i, 2.0*offset, color);
  }
}
void DrawLine(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, double thickness, RGBA *color){
  DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color);
}
void DrawLineBresenhamsAlgorithmThick(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, double thickness, RGBA *color){
  double x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r;

  dx = x2 - x1;
  dy = y2 - y1;

  incX = Sign(dx);
  incY = Sign(dy);

  dx = fabs(dx);
  dy = fabs(dy);

  if(dx > dy){
    pdx = incX;
    pdy = 0.0;
    es = dy;
    el = dx;
  }else{
    pdx = 0.0;
    pdy = incY;
    es = dx;
    el = dy;
  }

  x = x1;
  y = y1;
  err = el/2.0;

  if(thickness >= 3.0){
    r = thickness/2.0;
    DrawCircle(canvas, x, y, r, color);
  }else if(floor(thickness) == 2.0){
    DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color);
  }else if(floor(thickness) == 1.0){
    DrawPixel(canvas, x, y, color);
  }

  for(t = 0.0; t < el; t = t + 1.0){
    err = err - es;
    if(err < 0.0){
      err = err + el;
      x = x + incX;
      y = y + incY;
    }else{
      x = x + pdx;
      y = y + pdy;
    }

    if(thickness >= 3.0){
      r = thickness/2.0;
      DrawCircle(canvas, x, y, r, color);
    }else if(floor(thickness) == 2.0){
      DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color);
    }else if(floor(thickness) == 1.0){
      DrawPixel(canvas, x, y, color);
    }
  }
}
void DrawLineBresenhamsAlgorithm(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, RGBA *color){
  double x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t;

  dx = x2 - x1;
  dy = y2 - y1;

  incX = Sign(dx);
  incY = Sign(dy);

  dx = fabs(dx);
  dy = fabs(dy);

  if(dx > dy){
    pdx = incX;
    pdy = 0.0;
    es = dy;
    el = dx;
  }else{
    pdx = 0.0;
    pdy = incY;
    es = dx;
    el = dy;
  }

  x = x1;
  y = y1;
  err = el/2.0;
  DrawPixel(canvas, x, y, color);

  for(t = 0.0; t < el; t = t + 1.0){
    err = err - es;
    if(err < 0.0){
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
void DrawLineBresenhamsAlgorithmThickPatterned(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, double thickness, _Bool *pattern, size_t patternLength, NumberReference *offset, RGBA *color){
  double x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r;

  dx = x2 - x1;
  dy = y2 - y1;

  incX = Sign(dx);
  incY = Sign(dy);

  dx = fabs(dx);
  dy = fabs(dy);

  if(dx > dy){
    pdx = incX;
    pdy = 0.0;
    es = dy;
    el = dx;
  }else{
    pdx = 0.0;
    pdy = incY;
    es = dx;
    el = dy;
  }

  x = x1;
  y = y1;
  err = el/2.0;

  offset->numberValue = fmod(offset->numberValue + 1.0, patternLength*thickness);

  if(pattern[(int)(floor(offset->numberValue/thickness))]){
    if(thickness >= 3.0){
      r = thickness/2.0;
      DrawCircle(canvas, x, y, r, color);
    }else if(floor(thickness) == 2.0){
      DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color);
    }else if(floor(thickness) == 1.0){
      DrawPixel(canvas, x, y, color);
    }
  }

  for(t = 0.0; t < el; t = t + 1.0){
    err = err - es;
    if(err < 0.0){
      err = err + el;
      x = x + incX;
      y = y + incY;
    }else{
      x = x + pdx;
      y = y + pdy;
    }

    offset->numberValue = fmod(offset->numberValue + 1.0, patternLength*thickness);

    if(pattern[(int)(floor(offset->numberValue/thickness))]){
      if(thickness >= 3.0){
        r = thickness/2.0;
        DrawCircle(canvas, x, y, r, color);
      }else if(floor(thickness) == 2.0){
        DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color);
      }else if(floor(thickness) == 1.0){
        DrawPixel(canvas, x, y, color);
      }
    }
  }
}
_Bool *GetLinePattern5(size_t *returnArrayLength){
  _Bool *pattern;
  size_t patternLength;

  pattern = (_Bool*)malloc(sizeof(_Bool) * (19.0));
  patternLength = 19.0;

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

  *returnArrayLength = patternLength;
  return pattern;
}
_Bool *GetLinePattern4(size_t *returnArrayLength){
  _Bool *pattern;
  size_t patternLength;

  pattern = (_Bool*)malloc(sizeof(_Bool) * (13.0));
  patternLength = 13.0;

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

  *returnArrayLength = patternLength;
  return pattern;
}
_Bool *GetLinePattern3(size_t *returnArrayLength){
  _Bool *pattern;
  size_t patternLength;

  pattern = (_Bool*)malloc(sizeof(_Bool) * (13.0));
  patternLength = 13.0;

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

  *returnArrayLength = patternLength;
  return pattern;
}
_Bool *GetLinePattern2(size_t *returnArrayLength){
  _Bool *pattern;
  size_t patternLength;

  pattern = (_Bool*)malloc(sizeof(_Bool) * (4.0));
  patternLength = 4.0;

  pattern[0] = true;
  pattern[1] = true;
  pattern[2] = false;
  pattern[3] = false;

  *returnArrayLength = patternLength;
  return pattern;
}
_Bool *GetLinePattern1(size_t *returnArrayLength){
  _Bool *pattern;
  size_t patternLength;

  pattern = (_Bool*)malloc(sizeof(_Bool) * (8.0));
  patternLength = 8.0;

  pattern[0] = true;
  pattern[1] = true;
  pattern[2] = true;
  pattern[3] = true;
  pattern[4] = true;
  pattern[5] = false;
  pattern[6] = false;
  pattern[7] = false;

  *returnArrayLength = patternLength;
  return pattern;
}
RGBABitmapImage *Blur(RGBABitmapImage *src, double pixels){
  RGBABitmapImage *dst;
  double x, y, w, h;

  w = ImageWidth(src);
  h = ImageHeight(src);
  dst = CreateImage(w, h, GetTransparent());

  for(x = 0.0; x < w; x = x + 1.0){
    for(y = 0.0; y < h; y = y + 1.0){
      SetPixel(dst, x, y, CreateBlurForPoint(src, x, y, pixels));
    }
  }

  return dst;
}
RGBA *CreateBlurForPoint(RGBABitmapImage *src, double x, double y, double pixels){
  RGBA *rgba;
  double i, j, countColor, countTransparent;
  double fromx, tox, fromy, toy;
  double w, h;
  double alpha;

  w = src->xLength;
  h = src->x[0]->yLength;

  rgba = (RGBA *)malloc(sizeof(RGBA));
  rgba->r = 0.0;
  rgba->g = 0.0;
  rgba->b = 0.0;
  rgba->a = 0.0;

  fromx = x - pixels;
  fromx = fmax(fromx, 0.0);

  tox = x + pixels;
  tox = fmin(tox, w - 1.0);

  fromy = y - pixels;
  fromy = fmax(fromy, 0.0);

  toy = y + pixels;
  toy = fmin(toy, h - 1.0);

  countColor = 0.0;
  countTransparent = 0.0;
  for(i = fromx; i < tox; i = i + 1.0){
    for(j = fromy; j < toy; j = j + 1.0){
      alpha = src->x[(int)(i)]->y[(int)(j)]->a;
      if(alpha > 0.0){
        rgba->r = rgba->r + src->x[(int)(i)]->y[(int)(j)]->r;
        rgba->g = rgba->g + src->x[(int)(i)]->y[(int)(j)]->g;
        rgba->b = rgba->b + src->x[(int)(i)]->y[(int)(j)]->b;
        countColor = countColor + 1.0;
      }
      rgba->a = rgba->a + alpha;
      countTransparent = countTransparent + 1.0;
    }
  }

  if(countColor > 0.0){
    rgba->r = rgba->r/countColor;
    rgba->g = rgba->g/countColor;
    rgba->b = rgba->b/countColor;
  }else{
    rgba->r = 0.0;
    rgba->g = 0.0;
    rgba->b = 0.0;
  }

  if(countTransparent > 0.0){
    rgba->a = rgba->a/countTransparent;
  }else{
    rgba->a = 0.0;
  }

  return rgba;
}
wchar_t *CreateStringScientificNotationDecimalFromNumber(size_t *returnArrayLength, double decimal){
  StringReference *mantissaReference, *exponentReference;
  double multiplier, inc;
  double exponent;
  _Bool done, isPositive;
  wchar_t *result;
  size_t resultLength;

  mantissaReference = (StringReference *)malloc(sizeof(StringReference));
  exponentReference = (StringReference *)malloc(sizeof(StringReference));
  result = (wchar_t*)malloc(sizeof(wchar_t) * (0.0));
  resultLength = 0.0;
  done = false;
  exponent = 0.0;

  if(decimal < 0.0){
    isPositive = false;
    decimal =  -decimal;
  }else{
    isPositive = true;
  }

  if(decimal == 0.0){
    done = true;
  }

  if( !done ){
    multiplier = 0.0;
    inc = 0.0;

    if(decimal < 1.0){
      multiplier = 10.0;
      inc =  -1.0;
    }else if(decimal >= 10.0){
      multiplier = 0.1;
      inc = 1.0;
    }else{
      done = true;
    }

    if( !done ){
      for(; decimal >= 10.0 || decimal < 1.0; ){
        decimal = decimal*multiplier;
        exponent = exponent + inc;
      }
    }
  }

  CreateStringFromNumberWithCheck(decimal, 10.0, mantissaReference);

  CreateStringFromNumberWithCheck(exponent, 10.0, exponentReference);

  if( !isPositive ){
    result = AppendString(&resultLength, result, resultLength, strparam(L"-"));
  }

  result = AppendString(&resultLength, result, resultLength, mantissaReference->string, mantissaReference->stringLength);
  result = AppendString(&resultLength, result, resultLength, strparam(L"e"));
  result = AppendString(&resultLength, result, resultLength, exponentReference->string, exponentReference->stringLength);

  *returnArrayLength = resultLength;
  return result;
}
wchar_t *CreateStringDecimalFromNumber(size_t *returnArrayLength, double decimal){
  StringReference *stringReference;

  stringReference = (StringReference *)malloc(sizeof(StringReference));

  /* This will succeed because base = 10. */
  CreateStringFromNumberWithCheck(decimal, 10.0, stringReference);

  *returnArrayLength = stringReference->stringLength;
  return stringReference->string;
}
_Bool CreateStringFromNumberWithCheck(double decimal, double base, StringReference *stringReference){
  wchar_t *string;
  size_t stringLength;
  double maximumDigits;
  double digitPosition;
  _Bool hasPrintedPoint, isPositive;
  double i, d;
  _Bool success;
  CharacterReference *characterReference;
  wchar_t c;

  isPositive = true;

  if(decimal < 0.0){
    isPositive = false;
    decimal =  -decimal;
  }

  if(decimal == 0.0){
    stringReference->string = L"0";
    stringReference->stringLength = wcslen(stringReference->string);
    success = true;
  }else{
    characterReference = (CharacterReference *)malloc(sizeof(CharacterReference));

    if(IsInteger(base)){
      success = true;

      string = (wchar_t*)malloc(sizeof(wchar_t) * (0.0));
      stringLength = 0.0;

      maximumDigits = GetMaximumDigitsForBase(base);

      digitPosition = GetFirstDigitPosition(decimal, base);

      decimal = round(decimal*pow(base, maximumDigits - digitPosition - 1.0));

      hasPrintedPoint = false;

      if( !isPositive ){
        string = AppendCharacter(&stringLength, string, stringLength, '-');
      }

      /* Print leading zeros. */
      if(digitPosition < 0.0){
        string = AppendCharacter(&stringLength, string, stringLength, '0');
        string = AppendCharacter(&stringLength, string, stringLength, '.');
        hasPrintedPoint = true;
        for(i = 0.0; i <  -digitPosition - 1.0; i = i + 1.0){
          string = AppendCharacter(&stringLength, string, stringLength, '0');
        }
      }

      /* Print number. */
      for(i = 0.0; i < maximumDigits && success; i = i + 1.0){
        d = floor(decimal/pow(base, maximumDigits - i - 1.0));

        if(d >= base){
          d = base - 1.0;
        }

        if( !hasPrintedPoint  && digitPosition - i + 1.0 == 0.0){
          if(decimal != 0.0){
            string = AppendCharacter(&stringLength, string, stringLength, '.');
          }
          hasPrintedPoint = true;
        }

        if(decimal == 0.0 && hasPrintedPoint){
        }else{
          success = GetSingleDigitCharacterFromNumberWithCheck(d, base, characterReference);
          if(success){
            c = characterReference->characterValue;
            string = AppendCharacter(&stringLength, string, stringLength, c);
          }
        }

        if(success){
          decimal = decimal - d*pow(base, maximumDigits - i - 1.0);
        }
      }

      if(success){
        /* Print trailing zeros. */
        for(i = 0.0; i < digitPosition - maximumDigits + 1.0; i = i + 1.0){
          string = AppendCharacter(&stringLength, string, stringLength, '0');
        }

        stringReference->string = string;
        stringReference->stringLength = stringLength;
      }
    }else{
      success = false;
    }
  }

  /* Done */
  return success;
}
double GetMaximumDigitsForBase(double base){
  double t;

  t = pow(10.0, 15.0);
  return floor(log10(t)/log10(base));
}
double GetFirstDigitPosition(double decimal, double base){
  double power;
  double t;

  power = ceil(log10(decimal)/log10(base));

  t = decimal*pow(base,  -power);
  if(t < base && t >= 1.0){
  }else if(t >= base){
    power = power + 1.0;
  }else if(t < 1.0){
    power = power - 1.0;
  }

  return power;
}
_Bool GetSingleDigitCharacterFromNumberWithCheck(double c, double base, CharacterReference *characterReference){
  wchar_t *numberTable;
  size_t numberTableLength;
  _Bool success;

  numberTable = GetDigitCharacterTable(&numberTableLength);

  if(c < base || c < numberTableLength){
    success = true;
    characterReference->characterValue = numberTable[(int)(c)];
  }else{
    success = false;
  }

  return success;
}
wchar_t *GetDigitCharacterTable(size_t *returnArrayLength){
  wchar_t *numberTable;
  size_t numberTableLength;

  numberTable = L"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  numberTableLength = wcslen(numberTable);

  *returnArrayLength = numberTableLength;
  return numberTable;
}
_Bool CreateNumberFromDecimalStringWithCheck(wchar_t *string, size_t stringLength, NumberReference *decimalReference, StringReference *errorMessage){
  return CreateNumberFromStringWithCheck(string, stringLength, 10.0, decimalReference, errorMessage);
}
double CreateNumberFromDecimalString(wchar_t *string, size_t stringLength){
  NumberReference *doubleReference;
  StringReference *stringReference;
  double number;

  doubleReference = CreateNumberReference(0.0);
  stringReference = CreateStringReference(strparam(L""));
  CreateNumberFromStringWithCheck(string, stringLength, 10.0, doubleReference, stringReference);
  number = doubleReference->numberValue;

  free(doubleReference);
  free(stringReference);

  return number;
}
_Bool CreateNumberFromStringWithCheck(wchar_t *string, size_t stringLength, double base, NumberReference *numberReference, StringReference *errorMessage){
  _Bool success;
  BooleanReference *numberIsPositive, *exponentIsPositive;
  NumberArrayReference *beforePoint, *afterPoint, *exponent;

  numberIsPositive = CreateBooleanReference(true);
  exponentIsPositive = CreateBooleanReference(true);
  beforePoint = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
  afterPoint = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
  exponent = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));

  if(base >= 2.0 && base <= 36.0){
    success = ExtractPartsFromNumberString(string, stringLength, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessage);

    if(success){
      numberReference->numberValue = CreateNumberFromParts(base, numberIsPositive->booleanValue, beforePoint->numberArray, beforePoint->numberArrayLength, afterPoint->numberArray, afterPoint->numberArrayLength, exponentIsPositive->booleanValue, exponent->numberArray, exponent->numberArrayLength);
    }
  }else{
    success = false;
    errorMessage->string = L"Base must be from 2 to 36.";
    errorMessage->stringLength = wcslen(errorMessage->string);
  }

  return success;
}
double CreateNumberFromParts(double base, _Bool numberIsPositive, double *beforePoint, size_t beforePointLength, double *afterPoint, size_t afterPointLength, _Bool exponentIsPositive, double *exponent, size_t exponentLength){
  double n, i, p, e;

  n = 0.0;

  for(i = 0.0; i < beforePointLength; i = i + 1.0){
    p = beforePoint[(int)(beforePointLength - i - 1.0)];

    n = n + p*pow(base, i);
  }

  for(i = 0.0; i < afterPointLength; i = i + 1.0){
    p = afterPoint[(int)(i)];

    n = n + p*pow(base,  -(i + 1.0));
  }

  if(exponentLength > 0.0){
    e = 0.0;
    for(i = 0.0; i < exponentLength; i = i + 1.0){
      p = exponent[(int)(exponentLength - i - 1.0)];

      e = e + p*pow(base, i);
    }

    if( !exponentIsPositive ){
      e =  -e;
    }

    n = n*pow(base, e);
  }

  if( !numberIsPositive ){
    n =  -n;
  }

  return n;
}
_Bool ExtractPartsFromNumberString(wchar_t *n, size_t nLength, double base, BooleanReference *numberIsPositive, NumberArrayReference *beforePoint, NumberArrayReference *afterPoint, BooleanReference *exponentIsPositive, NumberArrayReference *exponent, StringReference *errorMessages){
  double i;
  _Bool success;

  i = 0.0;

  if(i < nLength){
    if(n[(int)(i)] == '-'){
      numberIsPositive->booleanValue = false;
      i = i + 1.0;
    }else if(n[(int)(i)] == '+'){
      numberIsPositive->booleanValue = true;
      i = i + 1.0;
    }

    success = ExtractPartsFromNumberStringFromSign(n, nLength, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages);
  }else{
    success = false;
    errorMessages->string = L"Number cannot have length zero.";
    errorMessages->stringLength = wcslen(errorMessages->string);
  }

  return success;
}
_Bool ExtractPartsFromNumberStringFromSign(wchar_t *n, size_t nLength, double base, double i, NumberArrayReference *beforePoint, NumberArrayReference *afterPoint, BooleanReference *exponentIsPositive, NumberArrayReference *exponent, StringReference *errorMessages){
  _Bool success, done;
  double count, j;

  done = false;
  count = 0.0;
  for(; i + count < nLength &&  !done ; ){
    if(CharacterIsNumberCharacterInBase(n[(int)(i + count)], base)){
      count = count + 1.0;
    }else{
      done = true;
    }
  }

  if(count >= 1.0){
    beforePoint->numberArray = (double*)malloc(sizeof(double) * (count));
    beforePoint->numberArrayLength = count;

    for(j = 0.0; j < count; j = j + 1.0){
      beforePoint->numberArray[(int)(j)] = GetNumberFromNumberCharacterForBase(n[(int)(i + j)], base);
    }

    i = i + count;

    if(i < nLength){
      success = ExtractPartsFromNumberStringFromPointOrExponent(n, nLength, base, i, afterPoint, exponentIsPositive, exponent, errorMessages);
    }else{
      afterPoint->numberArray = (double*)malloc(sizeof(double) * (0.0));
      afterPoint->numberArrayLength = 0.0;
      exponent->numberArray = (double*)malloc(sizeof(double) * (0.0));
      exponent->numberArrayLength = 0.0;
      success = true;
    }
  }else{
    success = false;
    errorMessages->string = L"Number must have at least one number after the optional sign.";
    errorMessages->stringLength = wcslen(errorMessages->string);
  }

  return success;
}
_Bool ExtractPartsFromNumberStringFromPointOrExponent(wchar_t *n, size_t nLength, double base, double i, NumberArrayReference *afterPoint, BooleanReference *exponentIsPositive, NumberArrayReference *exponent, StringReference *errorMessages){
  _Bool success, done;
  double count, j;

  if(n[(int)(i)] == '.'){
    i = i + 1.0;

    if(i < nLength){
      done = false;
      count = 0.0;
      for(; i + count < nLength &&  !done ; ){
        if(CharacterIsNumberCharacterInBase(n[(int)(i + count)], base)){
          count = count + 1.0;
        }else{
          done = true;
        }
      }

      if(count >= 1.0){
        afterPoint->numberArray = (double*)malloc(sizeof(double) * (count));
        afterPoint->numberArrayLength = count;

        for(j = 0.0; j < count; j = j + 1.0){
          afterPoint->numberArray[(int)(j)] = GetNumberFromNumberCharacterForBase(n[(int)(i + j)], base);
        }

        i = i + count;

        if(i < nLength){
          success = ExtractPartsFromNumberStringFromExponent(n, nLength, base, i, exponentIsPositive, exponent, errorMessages);
        }else{
          exponent->numberArray = (double*)malloc(sizeof(double) * (0.0));
          exponent->numberArrayLength = 0.0;
          success = true;
        }
      }else{
        success = false;
        errorMessages->string = L"There must be at least one digit after the decimal point.";
        errorMessages->stringLength = wcslen(errorMessages->string);
      }
    }else{
      success = false;
      errorMessages->string = L"There must be at least one digit after the decimal point.";
      errorMessages->stringLength = wcslen(errorMessages->string);
    }
  }else if(base <= 14.0 && (n[(int)(i)] == 'e' || n[(int)(i)] == 'E')){
    if(i < nLength){
      success = ExtractPartsFromNumberStringFromExponent(n, nLength, base, i, exponentIsPositive, exponent, errorMessages);
      afterPoint->numberArray = (double*)malloc(sizeof(double) * (0.0));
      afterPoint->numberArrayLength = 0.0;
    }else{
      success = false;
      errorMessages->string = L"There must be at least one digit after the exponent.";
      errorMessages->stringLength = wcslen(errorMessages->string);
    }
  }else{
    success = false;
    errorMessages->string = L"Expected decimal point or exponent symbol.";
    errorMessages->stringLength = wcslen(errorMessages->string);
  }

  return success;
}
_Bool ExtractPartsFromNumberStringFromExponent(wchar_t *n, size_t nLength, double base, double i, BooleanReference *exponentIsPositive, NumberArrayReference *exponent, StringReference *errorMessages){
  _Bool success, done;
  double count, j;

  if(base <= 14.0 && (n[(int)(i)] == 'e' || n[(int)(i)] == 'E')){
    i = i + 1.0;

    if(i < nLength){
      if(n[(int)(i)] == '-'){
        exponentIsPositive->booleanValue = false;
        i = i + 1.0;
      }else if(n[(int)(i)] == '+'){
        exponentIsPositive->booleanValue = true;
        i = i + 1.0;
      }

      if(i < nLength){
        done = false;
        count = 0.0;
        for(; i + count < nLength &&  !done ; ){
          if(CharacterIsNumberCharacterInBase(n[(int)(i + count)], base)){
            count = count + 1.0;
          }else{
            done = true;
          }
        }

        if(count >= 1.0){
          exponent->numberArray = (double*)malloc(sizeof(double) * (count));
          exponent->numberArrayLength = count;

          for(j = 0.0; j < count; j = j + 1.0){
            exponent->numberArray[(int)(j)] = GetNumberFromNumberCharacterForBase(n[(int)(i + j)], base);
          }

          i = i + count;

          if(i == nLength){
            success = true;
          }else{
            success = false;
            errorMessages->string = L"There cannot be any characters past the exponent of the number.";
            errorMessages->stringLength = wcslen(errorMessages->string);
          }
        }else{
          success = false;
          errorMessages->string = L"There must be at least one digit after the decimal point.";
          errorMessages->stringLength = wcslen(errorMessages->string);
        }
      }else{
        success = false;
        errorMessages->string = L"There must be at least one digit after the exponent symbol.";
        errorMessages->stringLength = wcslen(errorMessages->string);
      }
    }else{
      success = false;
      errorMessages->string = L"There must be at least one digit after the exponent symbol.";
      errorMessages->stringLength = wcslen(errorMessages->string);
    }
  }else{
    success = false;
    errorMessages->string = L"Expected exponent symbol.";
    errorMessages->stringLength = wcslen(errorMessages->string);
  }

  return success;
}
double GetNumberFromNumberCharacterForBase(wchar_t c, double base){
  wchar_t *numberTable;
  size_t numberTableLength;
  double i;
  double position;

  numberTable = GetDigitCharacterTable(&numberTableLength);
  position = 0.0;

  for(i = 0.0; i < base; i = i + 1.0){
    if(numberTable[(int)(i)] == c){
      position = i;
    }
  }

  return position;
}
_Bool CharacterIsNumberCharacterInBase(wchar_t c, double base){
  wchar_t *numberTable;
  size_t numberTableLength;
  double i;
  _Bool found;

  numberTable = GetDigitCharacterTable(&numberTableLength);
  found = false;

  for(i = 0.0; i < base; i = i + 1.0){
    if(numberTable[(int)(i)] == c){
      found = true;
    }
  }

  return found;
}
double *StringToNumberArray(size_t *returnArrayLength, wchar_t *str, size_t strLength){
  NumberArrayReference *numberArrayReference;
  StringReference *stringReference;
  double *numbers;
  size_t numbersLength;

  numberArrayReference = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
  stringReference = (StringReference *)malloc(sizeof(StringReference));

  StringToNumberArrayWithCheck(str, strLength, numberArrayReference, stringReference);

  numbers = numberArrayReference->numberArray;
  numbersLength = numberArrayReference->numberArrayLength;

  free(numberArrayReference);
  free(stringReference);

  *returnArrayLength = numbersLength;
  return numbers;
}
_Bool StringToNumberArrayWithCheck(wchar_t *str, size_t strLength, NumberArrayReference *numberArrayReference, StringReference *errorMessage){
  StringReference **numberStrings;
  size_t numberStringsLength;
  double *numbers;
  size_t numbersLength;
  double i;
  wchar_t *numberString, *trimmedNumberString;
  size_t numberStringLength, trimmedNumberStringLength;
  _Bool success;
  NumberReference *numberReference;

  numberStrings = SplitByString(&numberStringsLength, str, strLength, strparam(L","));

  numbers = (double*)malloc(sizeof(double) * (numberStringsLength));
  numbersLength = numberStringsLength;
  success = true;
  numberReference = (NumberReference *)malloc(sizeof(NumberReference));

  for(i = 0.0; i < numberStringsLength; i = i + 1.0){
    numberString = numberStrings[(int)(i)]->string;
    numberStringLength = numberStrings[(int)(i)]->stringLength;
    trimmedNumberString = Trim(&trimmedNumberStringLength, numberString, numberStringLength);
    success = CreateNumberFromDecimalStringWithCheck(trimmedNumberString, trimmedNumberStringLength, numberReference, errorMessage);
    numbers[(int)(i)] = numberReference->numberValue;

    FreeStringReference(numberStrings[(int)(i)]);
    free(trimmedNumberString);
  }

  free(numberStrings);
  free(numberReference);

  numberArrayReference->numberArray = numbers;
  numberArrayReference->numberArrayLength = numbersLength;

  return success;
}
double Negate(double x){
  return  -x;
}
double Positive(double x){
  return  +x;
}
double Factorial(double x){
  double i, f;

  f = 1.0;

  for(i = 2.0; i <= x; i = i + 1.0){
    f = f*i;
  }

  return f;
}
double Round(double x){
  return floor(x + 0.5);
}
double BankersRound(double x){
  double r;

  if(Absolute(x - Truncate(x)) == 0.5){
    if( !DivisibleBy(Round(x), 2.0) ){
      r = Round(x) - 1.0;
    }else{
      r = Round(x);
    }
  }else{
    r = Round(x);
  }

  return r;
}
double Ceil(double x){
  return ceil(x);
}
double Floor(double x){
  return floor(x);
}
double Truncate(double x){
  double t;

  if(x >= 0.0){
    t = floor(x);
  }else{
    t = ceil(x);
  }

  return t;
}
double Absolute(double x){
  return fabs(x);
}
double Logarithm(double x){
  return log10(x);
}
double NaturalLogarithm(double x){
  return log(x);
}
double Sin(double x){
  return sin(x);
}
double Cos(double x){
  return cos(x);
}
double Tan(double x){
  return tan(x);
}
double Asin(double x){
  return asin(x);
}
double Acos(double x){
  return acos(x);
}
double Atan(double x){
  return atan(x);
}
double Atan2(double y, double x){
  double a;

  /* Atan2 is an invalid operation when x = 0 and y = 0, but this method does not return errors. */
  a = 0.0;

  if(x > 0.0){
    a = Atan(y/x);
  }else if(x < 0.0 && y >= 0.0){
    a = Atan(y/x) + M_PI;
  }else if(x < 0.0 && y < 0.0){
    a = Atan(y/x) - M_PI;
  }else if(x == 0.0 && y > 0.0){
    a = M_PI/2.0;
  }else if(x == 0.0 && y < 0.0){
    a =  -M_PI/2.0;
  }

  return a;
}
double Squareroot(double x){
  return sqrt(x);
}
double Exp(double x){
  return exp(x);
}
_Bool DivisibleBy(double a, double b){
  return ((fmod(a, b)) == 0.0);
}
double Combinations(double n, double k){
  double i, j, c;

  c = 1.0;
  j = 1.0;
  i = n - k + 1.0;

  for(; i <= n; ){
    c = c*i;
    c = c/j;

    i = i + 1.0;
    j = j + 1.0;
  }

  return c;
}
double Permutations(double n, double k){
  double i, c;

  c = 1.0;

  for(i = n - k + 1.0; i <= n; i = i + 1.0){
    c = c*i;
  }

  return c;
}
_Bool EpsilonCompare(double a, double b, double epsilon){
  return fabs(a - b) < epsilon;
}
double GreatestCommonDivisor(double a, double b){
  double t;

  for(; b != 0.0; ){
    t = b;
    b = fmod(a, b);
    a = t;
  }

  return a;
}
double GCDWithSubtraction(double a, double b){
  double g;

  if(a == 0.0){
    g = b;
  }else{
    for(; b != 0.0; ){
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
_Bool IsInteger(double a){
  return (a - floor(a)) == 0.0;
}
_Bool GreatestCommonDivisorWithCheck(double a, double b, NumberReference *gcdReference){
  _Bool success;
  double gcd;

  if(IsInteger(a) && IsInteger(b)){
    gcd = GreatestCommonDivisor(a, b);
    gcdReference->numberValue = gcd;
    success = true;
  }else{
    success = false;
  }

  return success;
}
double LeastCommonMultiple(double a, double b){
  double lcm;

  if(a > 0.0 && b > 0.0){
    lcm = fabs(a*b)/GreatestCommonDivisor(a, b);
  }else{
    lcm = 0.0;
  }

  return lcm;
}
double Sign(double a){
  double s;

  if(a > 0.0){
    s = 1.0;
  }else if(a < 0.0){
    s =  -1.0;
  }else{
    s = 0.0;
  }

  return s;
}
double Max(double a, double b){
  return fmax(a, b);
}
double Min(double a, double b){
  return fmin(a, b);
}
double Power(double a, double b){
  return pow(a, b);
}
double Gamma(double x){
  return LanczosApproximation(x);
}
double LogGamma(double x){
  return log(Gamma(x));
}
double LanczosApproximation(double z){
  double *p;
  size_t pLength;
  double i, y, t, x;

  p = (double*)malloc(sizeof(double) * (8.0));
  pLength = 8.0;
  p[0] = 676.5203681218851;
  p[1] =  -1259.1392167224028;
  p[2] = 771.32342877765313;
  p[3] =  -176.61502916214059;
  p[4] = 12.507343278686905;
  p[5] =  -0.13857109526572012;
  p[6] = 9.9843695780195716e-6;
  p[7] = 1.5056327351493116e-7;

  if(z < 0.5){
    y = M_PI/(sin(M_PI*z)*LanczosApproximation(1.0 - z));
  }else{
    z = z - 1.0;
    x = 0.99999999999980993;
    for(i = 0.0; i < pLength; i = i + 1.0){
      x = x + p[(int)(i)]/(z + i + 1.0);
    }
    t = z + pLength - 0.5;
    y = sqrt(2.0*M_PI)*pow(t, z + 0.5)*exp( -t)*x;
  }

  return y;
}
double Beta(double x, double y){
  return Gamma(x)*Gamma(y)/Gamma(x + y);
}
double Sinh(double x){
  return (exp(x) - exp( -x))/2.0;
}
double Cosh(double x){
  return (exp(x) + exp( -x))/2.0;
}
double Tanh(double x){
  return Sinh(x)/Cosh(x);
}
double Cot(double x){
  return 1.0/tan(x);
}
double Sec(double x){
  return 1.0/cos(x);
}
double Csc(double x){
  return 1.0/sin(x);
}
double Coth(double x){
  return Cosh(x)/Sinh(x);
}
double Sech(double x){
  return 1.0/Cosh(x);
}
double Csch(double x){
  return 1.0/Sinh(x);
}
double Error(double x){
  double y, t, tau, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10;

  if(x == 0.0){
    y = 0.0;
  }else if(x < 0.0){
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

    t = 1.0/(1.0 + 0.5*fabs(x));

    tau = t*exp( -pow(x, 2.0) + c1 + t*(c2 + t*(c3 + t*(c4 + t*(c5 + t*(c6 + t*(c7 + t*(c8 + t*(c9 + t*c10)))))))));

    y = 1.0 - tau;
  }

  return y;
}
double ErrorInverse(double x){
  double y, a, t;

  a = (8.0*(M_PI - 3.0))/(3.0*M_PI*(4.0 - M_PI));

  t = 2.0/(M_PI*a) + log(1.0 - pow(x, 2.0))/2.0;
  y = Sign(x)*sqrt(sqrt(pow(t, 2.0) - log(1.0 - pow(x, 2.0))/a) - t);

  return y;
}
double FallingFactorial(double x, double n){
  double k, y;

  y = 1.0;

  for(k = 0.0; k <= n - 1.0; k = k + 1.0){
    y = y*(x - k);
  }

  return y;
}
double RisingFactorial(double x, double n){
  double k, y;

  y = 1.0;

  for(k = 0.0; k <= n - 1.0; k = k + 1.0){
    y = y*(x + k);
  }

  return y;
}
double Hypergeometric(double a, double b, double c, double z, double maxIterations, double precision){
  double y;

  if(fabs(z) >= 0.5){
    y = pow(1.0 - z,  -a)*HypergeometricDirect(a, c - b, c, z/(z - 1.0), maxIterations, precision);
  }else{
    y = HypergeometricDirect(a, b, c, z, maxIterations, precision);
  }

  return y;
}
double HypergeometricDirect(double a, double b, double c, double z, double maxIterations, double precision){
  double y, yp, n;
  _Bool done;

  y = 0.0;
  done = false;

  for(n = 0.0; n < maxIterations &&  !done ; n = n + 1.0){
    yp = RisingFactorial(a, n)*RisingFactorial(b, n)/RisingFactorial(c, n)*pow(z, n)/Factorial(n);
    if(fabs(yp) < precision){
      done = true;
    }
    y = y + yp;
  }

  return y;
}
double BernouilliNumber(double n){
  return AkiyamaTanigawaAlgorithm(n);
}
double AkiyamaTanigawaAlgorithm(double n){
  double m, j, B;
  double *A;
  size_t ALength;

  A = (double*)malloc(sizeof(double) * (n + 1.0));
  ALength = n + 1.0;

  for(m = 0.0; m <= n; m = m + 1.0){
    A[(int)(m)] = 1.0/(m + 1.0);
    for(j = m; j >= 1.0; j = j - 1.0){
      A[(int)(j - 1.0)] = j*(A[(int)(j - 1.0)] - A[(int)(j)]);
    }
  }

  B = A[0];

  free(A);

  return B;
}
double *aStringToNumberArray(size_t *returnArrayLength, wchar_t *string, size_t stringLength){
  double i;
  double *array;
  size_t arrayLength;

  array = (double*)malloc(sizeof(double) * (stringLength));
  arrayLength = stringLength;

  for(i = 0.0; i < stringLength; i = i + 1.0){
    array[(int)(i)] = string[(int)(i)];
  }
  *returnArrayLength = arrayLength;
  return array;
}
wchar_t *aNumberArrayToString(size_t *returnArrayLength, double *array, size_t arrayLength){
  double i;
  wchar_t *string;
  size_t stringLength;

  string = (wchar_t*)malloc(sizeof(wchar_t) * (arrayLength));
  stringLength = arrayLength;

  for(i = 0.0; i < arrayLength; i = i + 1.0){
    string[(int)(i)] = array[(int)(i)];
  }
  *returnArrayLength = stringLength;
  return string;
}
_Bool aNumberArraysEqual(double *a, size_t aLength, double *b, size_t bLength){
  _Bool equal;
  double i;

  equal = true;
  if(aLength == bLength){
    for(i = 0.0; i < aLength && equal; i = i + 1.0){
      if(a[(int)(i)] != b[(int)(i)]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
_Bool aBooleanArraysEqual(_Bool *a, size_t aLength, _Bool *b, size_t bLength){
  _Bool equal;
  double i;

  equal = true;
  if(aLength == bLength){
    for(i = 0.0; i < aLength && equal; i = i + 1.0){
      if(a[(int)(i)] != b[(int)(i)]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
_Bool aStringsEqual(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength){
  _Bool equal;
  double i;

  equal = true;
  if(aLength == bLength){
    for(i = 0.0; i < aLength && equal; i = i + 1.0){
      if(a[(int)(i)] != b[(int)(i)]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
void aFillNumberArray(double *a, size_t aLength, double value){
  double i;

  for(i = 0.0; i < aLength; i = i + 1.0){
    a[(int)(i)] = value;
  }
}
void aFillString(wchar_t *a, size_t aLength, wchar_t value){
  double i;

  for(i = 0.0; i < aLength; i = i + 1.0){
    a[(int)(i)] = value;
  }
}
void aFillBooleanArray(_Bool *a, size_t aLength, _Bool value){
  double i;

  for(i = 0.0; i < aLength; i = i + 1.0){
    a[(int)(i)] = value;
  }
}
_Bool aFillNumberArrayRange(double *a, size_t aLength, double value, double from, double to){
  double i, length;
  _Bool success;

  if(from >= 0.0 && from <= aLength && to >= 0.0 && to <= aLength && from <= to){
    length = to - from;
    for(i = 0.0; i < length; i = i + 1.0){
      a[(int)(from + i)] = value;
    }

    success = true;
  }else{
    success = false;
  }

  return success;
}
_Bool aFillBooleanArrayRange(_Bool *a, size_t aLength, _Bool value, double from, double to){
  double i, length;
  _Bool success;

  if(from >= 0.0 && from <= aLength && to >= 0.0 && to <= aLength && from <= to){
    length = to - from;
    for(i = 0.0; i < length; i = i + 1.0){
      a[(int)(from + i)] = value;
    }

    success = true;
  }else{
    success = false;
  }

  return success;
}
_Bool aFillStringRange(wchar_t *a, size_t aLength, wchar_t value, double from, double to){
  double i, length;
  _Bool success;

  if(from >= 0.0 && from <= aLength && to >= 0.0 && to <= aLength && from <= to){
    length = to - from;
    for(i = 0.0; i < length; i = i + 1.0){
      a[(int)(from + i)] = value;
    }

    success = true;
  }else{
    success = false;
  }

  return success;
}
double *aCopyNumberArray(size_t *returnArrayLength, double *a, size_t aLength){
  double i;
  double *n;
  size_t nLength;

  n = (double*)malloc(sizeof(double) * (aLength));
  nLength = aLength;

  for(i = 0.0; i < aLength; i = i + 1.0){
    n[(int)(i)] = a[(int)(i)];
  }

  *returnArrayLength = nLength;
  return n;
}
_Bool *aCopyBooleanArray(size_t *returnArrayLength, _Bool *a, size_t aLength){
  double i;
  _Bool *n;
  size_t nLength;

  n = (_Bool*)malloc(sizeof(_Bool) * (aLength));
  nLength = aLength;

  for(i = 0.0; i < aLength; i = i + 1.0){
    n[(int)(i)] = a[(int)(i)];
  }

  *returnArrayLength = nLength;
  return n;
}
wchar_t *aCopyString(size_t *returnArrayLength, wchar_t *a, size_t aLength){
  double i;
  wchar_t *n;
  size_t nLength;

  n = (wchar_t*)malloc(sizeof(wchar_t) * (aLength));
  nLength = aLength;

  for(i = 0.0; i < aLength; i = i + 1.0){
    n[(int)(i)] = a[(int)(i)];
  }

  *returnArrayLength = nLength;
  return n;
}
_Bool aCopyNumberArrayRange(double *a, size_t aLength, double from, double to, NumberArrayReference *copyReference){
  double i, length;
  double *n;
  size_t nLength;
  _Bool success;

  if(from >= 0.0 && from <= aLength && to >= 0.0 && to <= aLength && from <= to){
    length = to - from;
    n = (double*)malloc(sizeof(double) * (length));
    nLength = length;

    for(i = 0.0; i < length; i = i + 1.0){
      n[(int)(i)] = a[(int)(from + i)];
    }

    copyReference->numberArray = n;
    copyReference->numberArrayLength = nLength;
    success = true;
  }else{
    success = false;
  }

  return success;
}
_Bool aCopyBooleanArrayRange(_Bool *a, size_t aLength, double from, double to, BooleanArrayReference *copyReference){
  double i, length;
  _Bool *n;
  size_t nLength;
  _Bool success;

  if(from >= 0.0 && from <= aLength && to >= 0.0 && to <= aLength && from <= to){
    length = to - from;
    n = (_Bool*)malloc(sizeof(_Bool) * (length));
    nLength = length;

    for(i = 0.0; i < length; i = i + 1.0){
      n[(int)(i)] = a[(int)(from + i)];
    }

    copyReference->booleanArray = n;
    copyReference->booleanArrayLength = nLength;
    success = true;
  }else{
    success = false;
  }

  return success;
}
_Bool aCopyStringRange(wchar_t *a, size_t aLength, double from, double to, StringReference *copyReference){
  double i, length;
  wchar_t *n;
  size_t nLength;
  _Bool success;

  if(from >= 0.0 && from <= aLength && to >= 0.0 && to <= aLength && from <= to){
    length = to - from;
    n = (wchar_t*)malloc(sizeof(wchar_t) * (length));
    nLength = length;

    for(i = 0.0; i < length; i = i + 1.0){
      n[(int)(i)] = a[(int)(from + i)];
    }

    copyReference->string = n;
    copyReference->stringLength = nLength;
    success = true;
  }else{
    success = false;
  }

  return success;
}
_Bool aIsLastElement(double length, double index){
  return index + 1.0 == length;
}
double *aCreateNumberArray(size_t *returnArrayLength, double length, double value){
  double *array;
  size_t arrayLength;

  array = (double*)malloc(sizeof(double) * (length));
  arrayLength = length;
  aFillNumberArray(array, arrayLength, value);

  *returnArrayLength = arrayLength;
  return array;
}
_Bool *aCreateBooleanArray(size_t *returnArrayLength, double length, _Bool value){
  _Bool *array;
  size_t arrayLength;

  array = (_Bool*)malloc(sizeof(_Bool) * (length));
  arrayLength = length;
  aFillBooleanArray(array, arrayLength, value);

  *returnArrayLength = arrayLength;
  return array;
}
wchar_t *aCreateString(size_t *returnArrayLength, double length, wchar_t value){
  wchar_t *array;
  size_t arrayLength;

  array = (wchar_t*)malloc(sizeof(wchar_t) * (length));
  arrayLength = length;
  aFillString(array, arrayLength, value);

  *returnArrayLength = arrayLength;
  return array;
}
void aSwapElementsOfNumberArray(double *A, size_t ALength, double ai, double bi){
  double tmp;

  tmp = A[(int)(ai)];
  A[(int)(ai)] = A[(int)(bi)];
  A[(int)(bi)] = tmp;
}
void aSwapElementsOfStringArray(StringArrayReference *A, double ai, double bi){
  StringReference *tmp;

  tmp = A->stringArray[(int)(ai)];
  A->stringArray[(int)(ai)] = A->stringArray[(int)(bi)];
  A->stringArray[(int)(bi)] = tmp;
}
void aReverseNumberArray(double *array, size_t arrayLength){
  double i;

  for(i = 0.0; i < arrayLength/2.0; i = i + 1.0){
    aSwapElementsOfNumberArray(array, arrayLength, i, arrayLength - i - 1.0);
  }
}
BooleanReference *CreateBooleanReference(_Bool value){
  BooleanReference *ref;

  ref = (BooleanReference *)malloc(sizeof(BooleanReference));
  ref->booleanValue = value;

  return ref;
}
BooleanArrayReference *CreateBooleanArrayReference(_Bool *value, size_t valueLength){
  BooleanArrayReference *ref;

  ref = (BooleanArrayReference *)malloc(sizeof(BooleanArrayReference));
  ref->booleanArray = value;
  ref->booleanArrayLength = valueLength;

  return ref;
}
BooleanArrayReference *CreateBooleanArrayReferenceLengthValue(double length, _Bool value){
  BooleanArrayReference *ref;
  double i;

  ref = (BooleanArrayReference *)malloc(sizeof(BooleanArrayReference));
  ref->booleanArray = (_Bool*)malloc(sizeof(_Bool) * (length));
  ref->booleanArrayLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    ref->booleanArray[(int)(i)] = value;
  }

  return ref;
}
void FreeBooleanArrayReference(BooleanArrayReference *booleanArrayReference){
  free(booleanArrayReference->booleanArray);
  free(booleanArrayReference);
}
CharacterReference *CreateCharacterReference(wchar_t value){
  CharacterReference *ref;

  ref = (CharacterReference *)malloc(sizeof(CharacterReference));
  ref->characterValue = value;

  return ref;
}
NumberReference *CreateNumberReference(double value){
  NumberReference *ref;

  ref = (NumberReference *)malloc(sizeof(NumberReference));
  ref->numberValue = value;

  return ref;
}
NumberArrayReference *CreateNumberArrayReference(double *value, size_t valueLength){
  NumberArrayReference *ref;

  ref = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
  ref->numberArray = value;
  ref->numberArrayLength = valueLength;

  return ref;
}
NumberArrayReference *CreateNumberArrayReferenceLengthValue(double length, double value){
  NumberArrayReference *ref;
  double i;

  ref = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
  ref->numberArray = (double*)malloc(sizeof(double) * (length));
  ref->numberArrayLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    ref->numberArray[(int)(i)] = value;
  }

  return ref;
}
void FreeNumberArrayReference(NumberArrayReference *numberArrayReference){
  free(numberArrayReference->numberArray);
  free(numberArrayReference);
}
StringReference *CreateStringReference(wchar_t *value, size_t valueLength){
  StringReference *ref;

  ref = (StringReference *)malloc(sizeof(StringReference));
  ref->string = value;
  ref->stringLength = valueLength;

  return ref;
}
StringReference *CreateStringReferenceLengthValue(double length, wchar_t value){
  StringReference *ref;
  double i;

  ref = (StringReference *)malloc(sizeof(StringReference));
  ref->string = (wchar_t*)malloc(sizeof(wchar_t) * (length));
  ref->stringLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    ref->string[(int)(i)] = value;
  }

  return ref;
}
void FreeStringReference(StringReference *stringReference){
  free(stringReference->string);
  free(stringReference);
}
StringArrayReference *CreateStringArrayReference(StringReference **strings, size_t stringsLength){
  StringArrayReference *ref;

  ref = (StringArrayReference *)malloc(sizeof(StringArrayReference));
  ref->stringArray = strings;
  ref->stringArrayLength = stringsLength;

  return ref;
}
StringArrayReference *CreateStringArrayReferenceLengthValue(double length, wchar_t *value, size_t valueLength){
  StringArrayReference *ref;
  double i;

  ref = (StringArrayReference *)malloc(sizeof(StringArrayReference));
  ref->stringArray = (StringReference**)malloc(sizeof(StringReference) * length);
  ref->stringArrayLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    ref->stringArray[(int)(i)] = CreateStringReference(value, valueLength);
  }

  return ref;
}
void FreeStringArrayReference(StringArrayReference *stringArrayReference){
  double i;

  for(i = 0.0; i < stringArrayReference->stringArrayLength; i = i + 1.0){
    free(stringArrayReference->stringArray[(int)(i)]);
  }
  free(stringArrayReference->stringArray);
  free(stringArrayReference);
}
wchar_t *DigitDataBase16(size_t *returnArrayLength){
  *returnArrayLength = wcslen(L"ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe891412108153069c4ffffffffffffffffffffffffffffffffffffffff9409000000000000000049e7ffffffffffffffffffffffffffffffffff61000000000000000000000017ddffffffffffffffffffffffffffffff840000000573d3f5e5a62b00000028f0ffffffffffffffffffffffffffda04000008bcfffffffffff44200000073ffffffffffffffffffffffffff5700000088ffffffffffffffe812000008e3ffffffffffffffffffffffea02000015f9ffffffffffffffff8100000080ffffffffffffffffffffff9c00000072ffffffffffffffffffe40100002fffffffffffffffffffffff51000000b8ffffffffffffffffffff2a000000e2ffffffffffffffffffff21000001f0ffffffffffffffffffff65000000b3fffffffffffffffffff602000018ffffffffffffffffffffff8b0000008affffffffffffffffffd200000036ffffffffffffffffffffffa900000063ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffb500000057ffffffffffffffffffffffc900000046ffffffffffffffffffa90000005fffffffffffffffffffffffd20000003affffffffffffffffffa900000060ffffffffffffffffffffffd30000003affffffffffffffffffb400000057ffffffffffffffffffffffca00000046ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffd100000037ffffffffffffffffffffffa900000063fffffffffffffffffff602000019ffffffffffffffffffffff8b00000089ffffffffffffffffffff21000001f1ffffffffffffffffffff66000000b3ffffffffffffffffffff50000000b8ffffffffffffffffffff2a000000e1ffffffffffffffffffff9c00000073ffffffffffffffffffe40100002fffffffffffffffffffffffea02000015f9ffffffffffffffff8200000080ffffffffffffffffffffffff5700000088ffffffffffffffe812000008e2ffffffffffffffffffffffffda04000008bcfffffffffff44300000073ffffffffffffffffffffffffffff830000000674d3f6e6a72b00000028f0ffffffffffffffffffffffffffffff60000000000000000000000016ddfffffffffffffffffffffffffffffffffe9309000000000000000048e6ffffffffffffffffffffffffffffffffffffffe88f3f1f07132e68c3fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9d7b28e69441f02000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6a274c7095b9de64000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000affffffffffffffffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffd48b56271005142a5ea0f6ffffffffffffffffffffffffffffffffdb7c20000000000000000000001392feffffffffffffffffffffffffffff1f00000000000000000000000000004cf9ffffffffffffffffffffffffff1f0000003784c7e7f9e8b1480000000056ffffffffffffffffffffffffff1f015accffffffffffffffff9701000000b0ffffffffffffffffffffffff58caffffffffffffffffffffff770000003cfffffffffffffffffffffffffffffffffffffffffffffffffff107000002edffffffffffffffffffffffffffffffffffffffffffffffffff3a000000ccffffffffffffffffffffffffffffffffffffffffffffffffff4c000000baffffffffffffffffffffffffffffffffffffffffffffffffff32000000cbffffffffffffffffffffffffffffffffffffffffffffffffec05000002edffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffeb140000009affffffffffffffffffffffffffffffffffffffffffffffff520000002afbffffffffffffffffffffffffffffffffffffffffffffff8c00000003c7ffffffffffffffffffffffffffffffffffffffffffffffb30300000085ffffffffffffffffffffffffffffffffffffffffffffffc50a0000005dfeffffffffffffffffffffffffffffffffffffffffffffd2110000004efbffffffffffffffffffffffffffffffffffffffffffffdb1800000042f8ffffffffffffffffffffffffffffffffffffffffffffe21f00000039f3ffffffffffffffffffffffffffffffffffffffffffffe92600000030efffffffffffffffffffffffffffffffffffffffffffffee2e00000029eafffffffffffffffffffffffffffffffffffffffffffff33700000022e5fffffffffffffffffffffffffffffffffffffffffffff7410000001cdffffffffffffffffffffffffffffffffffffffffffffffb4c00000017d9fffffffffffffffffffffffffffffffffffffffffffffd5900000012d2ffffffffffffffffffffffffffffffffffffffffffffff680000000ecbffffffffffffffffffffffffffffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe2af8058392817060a1a3f74c8ffffffffffffffffffffffffffffffffeb0000000000000000000000000036cfffffffffffffffffffffffffffffeb000000000000000000000000000004a7ffffffffffffffffffffffffffeb00000f5a9dd0edfbf0ca841900000003c2ffffffffffffffffffffffffec3da8f9fffffffffffffffff0410000002bffffffffffffffffffffffffffffffffffffffffffffffffffee12000000cbffffffffffffffffffffffffffffffffffffffffffffffffff6900000090ffffffffffffffffffffffffffffffffffffffffffffffffff9600000078ffffffffffffffffffffffffffffffffffffffffffffffffff9a0000007effffffffffffffffffffffffffffffffffffffffffffffffff73000000a5fffffffffffffffffffffffffffffffffffffffffffffffff51b000009edfffffffffffffffffffffffffffffffffffffffffffffff7540000007efffffffffffffffffffffffffffffffffffffffffff3d3912400000055fcffffffffffffffffffffffffffffffffff1700000000000000001692feffffffffffffffffffffffffffffffffffff17000000000000002db8feffffffffffffffffffffffffffffffffffffff170000000000000000002bc3fffffffffffffffffffffffffffffffffffffffffffdf0cf922e00000003a5fffffffffffffffffffffffffffffffffffffffffffffffffd8700000007d1ffffffffffffffffffffffffffffffffffffffffffffffffff780000004ffffffffffffffffffffffffffffffffffffffffffffffffffff308000006f6ffffffffffffffffffffffffffffffffffffffffffffffffff3c000000d0ffffffffffffffffffffffffffffffffffffffffffffffffff4d000000c6ffffffffffffffffffffffffffffffffffffffffffffffffff35000000ddffffffffffffffffffffffffffffffffffffffffffffffffea0300000bf9ffffffffffffffffffffffffffffffffffffffffffffffff6200000054ffffffffffffffffffffff47bafefffffffffffffffffff56b00000002cbffffffffffffffffffffff0b001e71a9d7edfbf6e4ba771a000000007cffffffffffffffffffffffff0b0000000000000000000000000000017dffffffffffffffffffffffffff0b000000000000000000000000003cc8ffffffffffffffffffffffffffffe9b989593827160608162a5689dbffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffbd0100000000f3fffffffffffffffffffffffffffffffffffffffffffff3200000000000f3ffffffffffffffffffffffffffffffffffffffffffff69000000000000f3ffffffffffffffffffffffffffffffffffffffffffbf01000b0e000000f3fffffffffffffffffffffffffffffffffffffffff42100008e1f000000f3ffffffffffffffffffffffffffffffffffffffff6a000035fc1f000000f3ffffffffffffffffffffffffffffffffffffffc0010004d1ff1f000000f3fffffffffffffffffffffffffffffffffffff42200007affff1f000000f3ffffffffffffffffffffffffffffffffffff6c000026f7ffff1f000000f3ffffffffffffffffffffffffffffffffffc1010001c1ffffff1f000000f3fffffffffffffffffffffffffffffffff523000066ffffffff1f000000f3ffffffffffffffffffffffffffffffff6d000019f0ffffffff1f000000f3ffffffffffffffffffffffffffffffc2010000aeffffffffff1f000000f3fffffffffffffffffffffffffffff524000052ffffffffffff1f000000f3ffffffffffffffffffffffffffff6e00000fe6ffffffffffff1f000000f3ffffffffffffffffffffffffffc30200009affffffffffffff1f000000f3fffffffffffffffffffffffff62400003ffeffffffffffffff1f000000f3ffffffffffffffffffffffff70000008daffffffffffffffff1f000000f3fffffffffffffffffffffff602000086ffffffffffffffffff1f000000f3fffffffffffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f000008672f120514275997efffffffffffffffffffffffffffffffffff4f00000000000000000000000b73f6ffffffffffffffffffffffffffffff4f000000000000000000000000002bdeffffffffffffffffffffffffffff60538cbad2e7faf0d599370000000025ebffffffffffffffffffffffffffffffffffffffffffffffffa0090000005bffffffffffffffffffffffffffffffffffffffffffffffffffb100000001d2ffffffffffffffffffffffffffffffffffffffffffffffffff560000007effffffffffffffffffffffffffffffffffffffffffffffffffb80000003dffffffffffffffffffffffffffffffffffffffffffffffffffec00000022fffffffffffffffffffffffffffffffffffffffffffffffffffd00000011ffffffffffffffffffffffffffffffffffffffffffffffffffec00000022ffffffffffffffffffffffffffffffffffffffffffffffffffb80000003cffffffffffffffffffffffffffffffffffffffffffffffffff580000007dffffffffffffffffffffffffffffffffffffffffffffffffb301000000cfffffffffffffffffffffff4cb1fdffffffffffffffffffa40a00000058ffffffffffffffffffffffff17001a6ea9d7eefbf2d69b380000000024e8ffffffffffffffffffffffff1700000000000000000000000000002de0ffffffffffffffffffffffffff17000000000000000000000000127ef9ffffffffffffffffffffffffffffebba8a59372615050a1a3569a6f7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffca753915050d233866a3e0ffffffffffffffffffffffffffffffffffd13f0000000000000000000000f7ffffffffffffffffffffffffffffff9d07000000000000000000000000f7ffffffffffffffffffffffffffff9700000000469fdbf3f5da9e490100f7ffffffffffffffffffffffffffca0300000eb3ffffffffffffffffd84df8fffffffffffffffffffffffffa2d000007c8ffffffffffffffffffffffffffffffffffffffffffffffff9100000081ffffffffffffffffffffffffffffffffffffffffffffffffff28000010f6ffffffffffffffffffffffffffffffffffffffffffffffffc20000006affffffffffffffffffffffffffffffffffffffffffffffffff79000000b2ffffffffffffffffffffffffffffffffffffffffffffffffff43000000ebffeb903d1a0616306fc0ffffffffffffffffffffffffffffff0f000015ffa211000000000000000041dcfffffffffffffffffffffffff30000003087000000000000000000000013c6ffffffffffffffffffffffe30000000f00000055beeef7d8881000000017e6ffffffffffffffffffffd30000000000019dffffffffffffe12200000056ffffffffffffffffffffd100000000006effffffffffffffffce04000002dbffffffffffffffffffdd0000000006eaffffffffffffffffff550000008bffffffffffffffffffe90000000043ffffffffffffffffffffa90000004dfffffffffffffffffff80200000074ffffffffffffffffffffdb0000002cffffffffffffffffffff2200000088ffffffffffffffffffffef00000019ffffffffffffffffffff4d00000088ffffffffffffffffffffee0000001affffffffffffffffffff7e00000074ffffffffffffffffffffdb0000002dffffffffffffffffffffcd00000042ffffffffffffffffffffa900000052ffffffffffffffffffffff21000005e9ffffffffffffffffff5400000093ffffffffffffffffffffff8f0000006dffffffffffffffffcd04000007e6fffffffffffffffffffffff9220000019effffffffffffe1230000006cffffffffffffffffffffffffffc00600000056beeff8d888110000002af3ffffffffffffffffffffffffffffa603000000000000000000000026ddffffffffffffffffffffffffffffffffc8280000000000000000025deffffffffffffffffffffffffffffffffffffffab25a2a1106193b7ed7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff47000000000000000000000000000000000000f7ffffffffffffffffffff47000000000000000000000000000000000003faffffffffffffffffffff4700000000000000000000000000000000004afffffffffffffffffffffffffffffffffffffffffffffffffc1a000000adffffffffffffffffffffffffffffffffffffffffffffffffb300000015faffffffffffffffffffffffffffffffffffffffffffffffff5100000073ffffffffffffffffffffffffffffffffffffffffffffffffea05000000d6ffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffff2c0000009dffffffffffffffffffffffffffffffffffffffffffffffffc90000000cf3ffffffffffffffffffffffffffffffffffffffffffffffff6700000063fffffffffffffffffffffffffffffffffffffffffffffffff60f000000c6ffffffffffffffffffffffffffffffffffffffffffffffffa300000029ffffffffffffffffffffffffffffffffffffffffffffffffff410000008cffffffffffffffffffffffffffffffffffffffffffffffffdf01000005e9ffffffffffffffffffffffffffffffffffffffffffffffff7d00000052fffffffffffffffffffffffffffffffffffffffffffffffffd1e000000b5ffffffffffffffffffffffffffffffffffffffffffffffffb90000001bfcffffffffffffffffffffffffffffffffffffffffffffffff570000007bffffffffffffffffffffffffffffffffffffffffffffffffee07000001ddffffffffffffffffffffffffffffffffffffffffffffffff9300000042ffffffffffffffffffffffffffffffffffffffffffffffffff31000000a5ffffffffffffffffffffffffffffffffffffffffffffffffd000000010f7ffffffffffffffffffffffffffffffffffffffffffffffff6d0000006bfffffffffffffffffffffffffffffffffffffffffffffffff913000000ceffffffffffffffffffffffffffffffffffffffffffffffffa900000031ffffffffffffffffffffffffffffffffffffffffffffffffff4700000094ffffffffffffffffffffffffffffffffffffffffffffffffe302000008eeffffffffffffffffffffffffffffffffffffffffffffffff840000005afffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9a8602c13050c1d4882dfffffffffffffffffffffffffffffffffffffa918000000000000000000025eeeffffffffffffffffffffffffffffff780000000000000000000000000023e5ffffffffffffffffffffffffff9f0000000037a8e4faf1c66d0500000033fdfffffffffffffffffffffff81600000065fdffffffffffffc40a0000009fffffffffffffffffffffffb600000021faffffffffffffffff8d00000047ffffffffffffffffffffff820000007bffffffffffffffffffeb01000014ffffffffffffffffffffff6d000000a2ffffffffffffffffffff15000001fdffffffffffffffffffff76000000a2ffffffffffffffffffff14000007ffffffffffffffffffffffa10000007bffffffffffffffffffec01000033ffffffffffffffffffffffec08000022fbffffffffffffffff8e00000087ffffffffffffffffffffffff7d00000068fdffffffffffffc70b00001ef2fffffffffffffffffffffffffb5500000039aae5fbf2c87006000013d0fffffffffffffffffffffffffffffe93160000000000000000000153e3ffffffffffffffffffffffffffffffffffbd2e000000000000000780f0ffffffffffffffffffffffffffffffffce3500000000000000000000000e87fcffffffffffffffffffffffffffb3060000004fb2e6faf0cd82150000004ffaffffffffffffffffffffffda0b000004a9ffffffffffffffe93600000076ffffffffffffffffffffff5600000084ffffffffffffffffffe80e000005e2fffffffffffffffffff606000008f4ffffffffffffffffffff6f0000008dffffffffffffffffffcb00000039ffffffffffffffffffffffac0000005cffffffffffffffffffbc0000004affffffffffffffffffffffbe0000004dffffffffffffffffffcc00000039ffffffffffffffffffffffac0000005effffffffffffffffffea00000008f4ffffffffffffffffffff6e0000007cffffffffffffffffffff2f00000085ffffffffffffffffffe70d000000c1ffffffffffffffffffff9300000004a9ffffffffffffffe83400000028fcfffffffffffffffffffffa2d0000000050b2e7fbf2cd821400000002b8ffffffffffffffffffffffffe523000000000000000000000000000299fffffffffffffffffffffffffffff16605000000000000000000002cc5ffffffffffffffffffffffffffffffffffe88e542512040b1b3d72c1fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff8a259251008203f8be2ffffffffffffffffffffffffffffffffffffffa91d0000000000000000047ffaffffffffffffffffffffffffffffffff7b00000000000000000000000040f8ffffffffffffffffffffffffffff94000000004db9ecf7da8b1300000057ffffffffffffffffffffffffffdc050000008fffffffffffffe527000000acffffffffffffffffffffffff630000005fffffffffffffffffd406000025fbfffffffffffffffffffffb0c000002e0ffffffffffffffffff5f000000b2ffffffffffffffffffffc600000036ffffffffffffffffffffb50000005fffffffffffffffffffffa000000068ffffffffffffffffffffe700000011feffffffffffffffffff8d0000007cfffffffffffffffffffffb00000000dfffffffffffffffffff8c0000007cfffffffffffffffffffffb00000000b4ffffffffffffffffff9e00000069ffffffffffffffffffffe7000000008dffffffffffffffffffbe00000038ffffffffffffffffffffb6000000007bfffffffffffffffffff606000003e2ffffffffffffffffff62000000006fffffffffffffffffffff4f00000064ffffffffffffffffd8080000000062ffffffffffffffffffffc50000000096ffffffffffffe82b000000000064ffffffffffffffffffffff6c0000000051bbeff8dc8e1500001000000074fffffffffffffffffffffff94f0000000000000000000000288c00000084fffffffffffffffffffffffffd810b000000000000000052ea830000009fffffffffffffffffffffffffffffea8d471d090d2864c1ffff5b000000d4ffffffffffffffffffffffffffffffffffffffffffffffffff2100000dfdffffffffffffffffffffffffffffffffffffffffffffffffd900000052ffffffffffffffffffffffffffffffffffffffffffffffffff75000000b8ffffffffffffffffffffffffffffffffffffffffffffffffe30d000023fefffffffffffffffffffffffffffffffffffffffffffffff945000000b7ffffffffffffffffffffffffff7fa2fdffffffffffffffe8480000005effffffffffffffffffffffffffff63002080c4ecfae7c0740e00000034f4ffffffffffffffffffffffffffff6300000000000000000000000043f0ffffffffffffffffffffffffffffff6300000000000000000000118efdfffffffffffffffffffffffffffffffff4bb7f462b15040b25569ff4ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
  return L"ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe891412108153069c4ffffffffffffffffffffffffffffffffffffffff9409000000000000000049e7ffffffffffffffffffffffffffffffffff61000000000000000000000017ddffffffffffffffffffffffffffffff840000000573d3f5e5a62b00000028f0ffffffffffffffffffffffffffda04000008bcfffffffffff44200000073ffffffffffffffffffffffffff5700000088ffffffffffffffe812000008e3ffffffffffffffffffffffea02000015f9ffffffffffffffff8100000080ffffffffffffffffffffff9c00000072ffffffffffffffffffe40100002fffffffffffffffffffffff51000000b8ffffffffffffffffffff2a000000e2ffffffffffffffffffff21000001f0ffffffffffffffffffff65000000b3fffffffffffffffffff602000018ffffffffffffffffffffff8b0000008affffffffffffffffffd200000036ffffffffffffffffffffffa900000063ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffb500000057ffffffffffffffffffffffc900000046ffffffffffffffffffa90000005fffffffffffffffffffffffd20000003affffffffffffffffffa900000060ffffffffffffffffffffffd30000003affffffffffffffffffb400000057ffffffffffffffffffffffca00000046ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffd100000037ffffffffffffffffffffffa900000063fffffffffffffffffff602000019ffffffffffffffffffffff8b00000089ffffffffffffffffffff21000001f1ffffffffffffffffffff66000000b3ffffffffffffffffffff50000000b8ffffffffffffffffffff2a000000e1ffffffffffffffffffff9c00000073ffffffffffffffffffe40100002fffffffffffffffffffffffea02000015f9ffffffffffffffff8200000080ffffffffffffffffffffffff5700000088ffffffffffffffe812000008e2ffffffffffffffffffffffffda04000008bcfffffffffff44300000073ffffffffffffffffffffffffffff830000000674d3f6e6a72b00000028f0ffffffffffffffffffffffffffffff60000000000000000000000016ddfffffffffffffffffffffffffffffffffe9309000000000000000048e6ffffffffffffffffffffffffffffffffffffffe88f3f1f07132e68c3fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9d7b28e69441f02000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6a274c7095b9de64000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000affffffffffffffffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffd48b56271005142a5ea0f6ffffffffffffffffffffffffffffffffdb7c20000000000000000000001392feffffffffffffffffffffffffffff1f00000000000000000000000000004cf9ffffffffffffffffffffffffff1f0000003784c7e7f9e8b1480000000056ffffffffffffffffffffffffff1f015accffffffffffffffff9701000000b0ffffffffffffffffffffffff58caffffffffffffffffffffff770000003cfffffffffffffffffffffffffffffffffffffffffffffffffff107000002edffffffffffffffffffffffffffffffffffffffffffffffffff3a000000ccffffffffffffffffffffffffffffffffffffffffffffffffff4c000000baffffffffffffffffffffffffffffffffffffffffffffffffff32000000cbffffffffffffffffffffffffffffffffffffffffffffffffec05000002edffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffeb140000009affffffffffffffffffffffffffffffffffffffffffffffff520000002afbffffffffffffffffffffffffffffffffffffffffffffff8c00000003c7ffffffffffffffffffffffffffffffffffffffffffffffb30300000085ffffffffffffffffffffffffffffffffffffffffffffffc50a0000005dfeffffffffffffffffffffffffffffffffffffffffffffd2110000004efbffffffffffffffffffffffffffffffffffffffffffffdb1800000042f8ffffffffffffffffffffffffffffffffffffffffffffe21f00000039f3ffffffffffffffffffffffffffffffffffffffffffffe92600000030efffffffffffffffffffffffffffffffffffffffffffffee2e00000029eafffffffffffffffffffffffffffffffffffffffffffff33700000022e5fffffffffffffffffffffffffffffffffffffffffffff7410000001cdffffffffffffffffffffffffffffffffffffffffffffffb4c00000017d9fffffffffffffffffffffffffffffffffffffffffffffd5900000012d2ffffffffffffffffffffffffffffffffffffffffffffff680000000ecbffffffffffffffffffffffffffffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe2af8058392817060a1a3f74c8ffffffffffffffffffffffffffffffffeb0000000000000000000000000036cfffffffffffffffffffffffffffffeb000000000000000000000000000004a7ffffffffffffffffffffffffffeb00000f5a9dd0edfbf0ca841900000003c2ffffffffffffffffffffffffec3da8f9fffffffffffffffff0410000002bffffffffffffffffffffffffffffffffffffffffffffffffffee12000000cbffffffffffffffffffffffffffffffffffffffffffffffffff6900000090ffffffffffffffffffffffffffffffffffffffffffffffffff9600000078ffffffffffffffffffffffffffffffffffffffffffffffffff9a0000007effffffffffffffffffffffffffffffffffffffffffffffffff73000000a5fffffffffffffffffffffffffffffffffffffffffffffffff51b000009edfffffffffffffffffffffffffffffffffffffffffffffff7540000007efffffffffffffffffffffffffffffffffffffffffff3d3912400000055fcffffffffffffffffffffffffffffffffff1700000000000000001692feffffffffffffffffffffffffffffffffffff17000000000000002db8feffffffffffffffffffffffffffffffffffffff170000000000000000002bc3fffffffffffffffffffffffffffffffffffffffffffdf0cf922e00000003a5fffffffffffffffffffffffffffffffffffffffffffffffffd8700000007d1ffffffffffffffffffffffffffffffffffffffffffffffffff780000004ffffffffffffffffffffffffffffffffffffffffffffffffffff308000006f6ffffffffffffffffffffffffffffffffffffffffffffffffff3c000000d0ffffffffffffffffffffffffffffffffffffffffffffffffff4d000000c6ffffffffffffffffffffffffffffffffffffffffffffffffff35000000ddffffffffffffffffffffffffffffffffffffffffffffffffea0300000bf9ffffffffffffffffffffffffffffffffffffffffffffffff6200000054ffffffffffffffffffffff47bafefffffffffffffffffff56b00000002cbffffffffffffffffffffff0b001e71a9d7edfbf6e4ba771a000000007cffffffffffffffffffffffff0b0000000000000000000000000000017dffffffffffffffffffffffffff0b000000000000000000000000003cc8ffffffffffffffffffffffffffffe9b989593827160608162a5689dbffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffbd0100000000f3fffffffffffffffffffffffffffffffffffffffffffff3200000000000f3ffffffffffffffffffffffffffffffffffffffffffff69000000000000f3ffffffffffffffffffffffffffffffffffffffffffbf01000b0e000000f3fffffffffffffffffffffffffffffffffffffffff42100008e1f000000f3ffffffffffffffffffffffffffffffffffffffff6a000035fc1f000000f3ffffffffffffffffffffffffffffffffffffffc0010004d1ff1f000000f3fffffffffffffffffffffffffffffffffffff42200007affff1f000000f3ffffffffffffffffffffffffffffffffffff6c000026f7ffff1f000000f3ffffffffffffffffffffffffffffffffffc1010001c1ffffff1f000000f3fffffffffffffffffffffffffffffffff523000066ffffffff1f000000f3ffffffffffffffffffffffffffffffff6d000019f0ffffffff1f000000f3ffffffffffffffffffffffffffffffc2010000aeffffffffff1f000000f3fffffffffffffffffffffffffffff524000052ffffffffffff1f000000f3ffffffffffffffffffffffffffff6e00000fe6ffffffffffff1f000000f3ffffffffffffffffffffffffffc30200009affffffffffffff1f000000f3fffffffffffffffffffffffff62400003ffeffffffffffffff1f000000f3ffffffffffffffffffffffff70000008daffffffffffffffff1f000000f3fffffffffffffffffffffff602000086ffffffffffffffffff1f000000f3fffffffffffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f000008672f120514275997efffffffffffffffffffffffffffffffffff4f00000000000000000000000b73f6ffffffffffffffffffffffffffffff4f000000000000000000000000002bdeffffffffffffffffffffffffffff60538cbad2e7faf0d599370000000025ebffffffffffffffffffffffffffffffffffffffffffffffffa0090000005bffffffffffffffffffffffffffffffffffffffffffffffffffb100000001d2ffffffffffffffffffffffffffffffffffffffffffffffffff560000007effffffffffffffffffffffffffffffffffffffffffffffffffb80000003dffffffffffffffffffffffffffffffffffffffffffffffffffec00000022fffffffffffffffffffffffffffffffffffffffffffffffffffd00000011ffffffffffffffffffffffffffffffffffffffffffffffffffec00000022ffffffffffffffffffffffffffffffffffffffffffffffffffb80000003cffffffffffffffffffffffffffffffffffffffffffffffffff580000007dffffffffffffffffffffffffffffffffffffffffffffffffb301000000cfffffffffffffffffffffff4cb1fdffffffffffffffffffa40a00000058ffffffffffffffffffffffff17001a6ea9d7eefbf2d69b380000000024e8ffffffffffffffffffffffff1700000000000000000000000000002de0ffffffffffffffffffffffffff17000000000000000000000000127ef9ffffffffffffffffffffffffffffebba8a59372615050a1a3569a6f7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffca753915050d233866a3e0ffffffffffffffffffffffffffffffffffd13f0000000000000000000000f7ffffffffffffffffffffffffffffff9d07000000000000000000000000f7ffffffffffffffffffffffffffff9700000000469fdbf3f5da9e490100f7ffffffffffffffffffffffffffca0300000eb3ffffffffffffffffd84df8fffffffffffffffffffffffffa2d000007c8ffffffffffffffffffffffffffffffffffffffffffffffff9100000081ffffffffffffffffffffffffffffffffffffffffffffffffff28000010f6ffffffffffffffffffffffffffffffffffffffffffffffffc20000006affffffffffffffffffffffffffffffffffffffffffffffffff79000000b2ffffffffffffffffffffffffffffffffffffffffffffffffff43000000ebffeb903d1a0616306fc0ffffffffffffffffffffffffffffff0f000015ffa211000000000000000041dcfffffffffffffffffffffffff30000003087000000000000000000000013c6ffffffffffffffffffffffe30000000f00000055beeef7d8881000000017e6ffffffffffffffffffffd30000000000019dffffffffffffe12200000056ffffffffffffffffffffd100000000006effffffffffffffffce04000002dbffffffffffffffffffdd0000000006eaffffffffffffffffff550000008bffffffffffffffffffe90000000043ffffffffffffffffffffa90000004dfffffffffffffffffff80200000074ffffffffffffffffffffdb0000002cffffffffffffffffffff2200000088ffffffffffffffffffffef00000019ffffffffffffffffffff4d00000088ffffffffffffffffffffee0000001affffffffffffffffffff7e00000074ffffffffffffffffffffdb0000002dffffffffffffffffffffcd00000042ffffffffffffffffffffa900000052ffffffffffffffffffffff21000005e9ffffffffffffffffff5400000093ffffffffffffffffffffff8f0000006dffffffffffffffffcd04000007e6fffffffffffffffffffffff9220000019effffffffffffe1230000006cffffffffffffffffffffffffffc00600000056beeff8d888110000002af3ffffffffffffffffffffffffffffa603000000000000000000000026ddffffffffffffffffffffffffffffffffc8280000000000000000025deffffffffffffffffffffffffffffffffffffffab25a2a1106193b7ed7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff47000000000000000000000000000000000000f7ffffffffffffffffffff47000000000000000000000000000000000003faffffffffffffffffffff4700000000000000000000000000000000004afffffffffffffffffffffffffffffffffffffffffffffffffc1a000000adffffffffffffffffffffffffffffffffffffffffffffffffb300000015faffffffffffffffffffffffffffffffffffffffffffffffff5100000073ffffffffffffffffffffffffffffffffffffffffffffffffea05000000d6ffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffff2c0000009dffffffffffffffffffffffffffffffffffffffffffffffffc90000000cf3ffffffffffffffffffffffffffffffffffffffffffffffff6700000063fffffffffffffffffffffffffffffffffffffffffffffffff60f000000c6ffffffffffffffffffffffffffffffffffffffffffffffffa300000029ffffffffffffffffffffffffffffffffffffffffffffffffff410000008cffffffffffffffffffffffffffffffffffffffffffffffffdf01000005e9ffffffffffffffffffffffffffffffffffffffffffffffff7d00000052fffffffffffffffffffffffffffffffffffffffffffffffffd1e000000b5ffffffffffffffffffffffffffffffffffffffffffffffffb90000001bfcffffffffffffffffffffffffffffffffffffffffffffffff570000007bffffffffffffffffffffffffffffffffffffffffffffffffee07000001ddffffffffffffffffffffffffffffffffffffffffffffffff9300000042ffffffffffffffffffffffffffffffffffffffffffffffffff31000000a5ffffffffffffffffffffffffffffffffffffffffffffffffd000000010f7ffffffffffffffffffffffffffffffffffffffffffffffff6d0000006bfffffffffffffffffffffffffffffffffffffffffffffffff913000000ceffffffffffffffffffffffffffffffffffffffffffffffffa900000031ffffffffffffffffffffffffffffffffffffffffffffffffff4700000094ffffffffffffffffffffffffffffffffffffffffffffffffe302000008eeffffffffffffffffffffffffffffffffffffffffffffffff840000005afffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9a8602c13050c1d4882dfffffffffffffffffffffffffffffffffffffa918000000000000000000025eeeffffffffffffffffffffffffffffff780000000000000000000000000023e5ffffffffffffffffffffffffff9f0000000037a8e4faf1c66d0500000033fdfffffffffffffffffffffff81600000065fdffffffffffffc40a0000009fffffffffffffffffffffffb600000021faffffffffffffffff8d00000047ffffffffffffffffffffff820000007bffffffffffffffffffeb01000014ffffffffffffffffffffff6d000000a2ffffffffffffffffffff15000001fdffffffffffffffffffff76000000a2ffffffffffffffffffff14000007ffffffffffffffffffffffa10000007bffffffffffffffffffec01000033ffffffffffffffffffffffec08000022fbffffffffffffffff8e00000087ffffffffffffffffffffffff7d00000068fdffffffffffffc70b00001ef2fffffffffffffffffffffffffb5500000039aae5fbf2c87006000013d0fffffffffffffffffffffffffffffe93160000000000000000000153e3ffffffffffffffffffffffffffffffffffbd2e000000000000000780f0ffffffffffffffffffffffffffffffffce3500000000000000000000000e87fcffffffffffffffffffffffffffb3060000004fb2e6faf0cd82150000004ffaffffffffffffffffffffffda0b000004a9ffffffffffffffe93600000076ffffffffffffffffffffff5600000084ffffffffffffffffffe80e000005e2fffffffffffffffffff606000008f4ffffffffffffffffffff6f0000008dffffffffffffffffffcb00000039ffffffffffffffffffffffac0000005cffffffffffffffffffbc0000004affffffffffffffffffffffbe0000004dffffffffffffffffffcc00000039ffffffffffffffffffffffac0000005effffffffffffffffffea00000008f4ffffffffffffffffffff6e0000007cffffffffffffffffffff2f00000085ffffffffffffffffffe70d000000c1ffffffffffffffffffff9300000004a9ffffffffffffffe83400000028fcfffffffffffffffffffffa2d0000000050b2e7fbf2cd821400000002b8ffffffffffffffffffffffffe523000000000000000000000000000299fffffffffffffffffffffffffffff16605000000000000000000002cc5ffffffffffffffffffffffffffffffffffe88e542512040b1b3d72c1fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff8a259251008203f8be2ffffffffffffffffffffffffffffffffffffffa91d0000000000000000047ffaffffffffffffffffffffffffffffffff7b00000000000000000000000040f8ffffffffffffffffffffffffffff94000000004db9ecf7da8b1300000057ffffffffffffffffffffffffffdc050000008fffffffffffffe527000000acffffffffffffffffffffffff630000005fffffffffffffffffd406000025fbfffffffffffffffffffffb0c000002e0ffffffffffffffffff5f000000b2ffffffffffffffffffffc600000036ffffffffffffffffffffb50000005fffffffffffffffffffffa000000068ffffffffffffffffffffe700000011feffffffffffffffffff8d0000007cfffffffffffffffffffffb00000000dfffffffffffffffffff8c0000007cfffffffffffffffffffffb00000000b4ffffffffffffffffff9e00000069ffffffffffffffffffffe7000000008dffffffffffffffffffbe00000038ffffffffffffffffffffb6000000007bfffffffffffffffffff606000003e2ffffffffffffffffff62000000006fffffffffffffffffffff4f00000064ffffffffffffffffd8080000000062ffffffffffffffffffffc50000000096ffffffffffffe82b000000000064ffffffffffffffffffffff6c0000000051bbeff8dc8e1500001000000074fffffffffffffffffffffff94f0000000000000000000000288c00000084fffffffffffffffffffffffffd810b000000000000000052ea830000009fffffffffffffffffffffffffffffea8d471d090d2864c1ffff5b000000d4ffffffffffffffffffffffffffffffffffffffffffffffffff2100000dfdffffffffffffffffffffffffffffffffffffffffffffffffd900000052ffffffffffffffffffffffffffffffffffffffffffffffffff75000000b8ffffffffffffffffffffffffffffffffffffffffffffffffe30d000023fefffffffffffffffffffffffffffffffffffffffffffffff945000000b7ffffffffffffffffffffffffff7fa2fdffffffffffffffe8480000005effffffffffffffffffffffffffff63002080c4ecfae7c0740e00000034f4ffffffffffffffffffffffffffff6300000000000000000000000043f0ffffffffffffffffffffffffffffff6300000000000000000000118efdfffffffffffffffffffffffffffffffff4bb7f462b15040b25569ff4ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
}
void DrawDigitCharacter(RGBABitmapImage *image, double topx, double topy, double digit){
  double x, y;
  wchar_t *allCharData, *colorChars;
  size_t allCharDataLength, colorCharsLength;
  NumberReference *colorReference;
  StringReference *errorMessage;
  RGBA *color;

  colorReference = (NumberReference *)malloc(sizeof(NumberReference));
  errorMessage = (StringReference *)malloc(sizeof(StringReference));
  color = (RGBA *)malloc(sizeof(RGBA));

  colorChars = (wchar_t*)malloc(sizeof(wchar_t) * (2.0));
  colorCharsLength = 2.0;

  allCharData = DigitDataBase16(&allCharDataLength);

  for(y = 0.0; y < 37.0; y = y + 1.0){
    for(x = 0.0; x < 30.0; x = x + 1.0){
      colorChars[0] = allCharData[(int)(digit*30.0*37.0*2.0 + y*2.0*30.0 + x*2.0 + 0.0)];
      colorChars[1] = allCharData[(int)(digit*30.0*37.0*2.0 + y*2.0*30.0 + x*2.0 + 1.0)];

      ToUpperCase(colorChars, colorCharsLength);
      CreateNumberFromStringWithCheck(colorChars, colorCharsLength, 16.0, colorReference, errorMessage);
      color->r = colorReference->numberValue/255.0;
      color->g = colorReference->numberValue/255.0;
      color->b = colorReference->numberValue/255.0;
      color->a = 1.0;
      SetPixel(image, topx + x, topy + y, color);
    }
  }
}
wchar_t *GetPixelFontData(size_t *returnArrayLength){
  *returnArrayLength = wcslen(L"0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000");
  return L"0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000";
}
void DrawAsciiCharacter(RGBABitmapImage *image, double topx, double topy, wchar_t a, RGBA *color){
  double index, x, y, row, pixel;
  wchar_t *allCharData, *charData, *rowData;
  size_t allCharDataLength, charDataLength, rowDataLength;
  NumberReference *rowReference;
  StringReference *errorMessage;

  rowReference = (NumberReference *)malloc(sizeof(NumberReference));
  errorMessage = (StringReference *)malloc(sizeof(StringReference));

  index = a;
  index = index - 32.0;
  allCharData = GetPixelFontData(&allCharDataLength);
  charData = Substring(&charDataLength, allCharData, allCharDataLength, index*2.0*13.0, (index + 1.0)*2.0*13.0);

  for(y = 0.0; y < 13.0; y = y + 1.0){
    rowData = Substring(&rowDataLength, charData, charDataLength, y*2.0, (y + 1.0)*2.0);
    ToUpperCase(rowData, rowDataLength);
    CreateNumberFromStringWithCheck(rowData, rowDataLength, 16.0, rowReference, errorMessage);
    row = rowReference->numberValue;
    for(x = 0.0; x < 8.0; x = x + 1.0){
      pixel = fmod(floor(row/pow(2.0, x)), 2.0);
      if(pixel == 1.0){
        DrawPixel(image, topx + 8.0 - 1.0 - x, topy + 13.0 - 1.0 - y, color);
      }
    }
  }
}
double GetTextWidth(wchar_t *text, size_t textLength){
  double charWidth, spacing, width;

  charWidth = 8.0;
  spacing = 2.0;

  if(textLength == 0.0){
    width = 0.0;
  }else{
    width = textLength*charWidth + (textLength - 1.0)*spacing;
  }

  return width;
}
double GetTextHeight(wchar_t *text, size_t textLength){
  return 13.0;
}
void AssertFalse(_Bool b, NumberReference *failures){
  if(b){
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertTrue(_Bool b, NumberReference *failures){
  if( !b ){
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertEquals(double a, double b, NumberReference *failures){
  if(a != b){
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertBooleansEqual(_Bool a, _Bool b, NumberReference *failures){
  if(a != b){
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertCharactersEqual(wchar_t a, wchar_t b, NumberReference *failures){
  if(a != b){
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertStringEquals(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength, NumberReference *failures){
  if( !aStringsEqual(a, aLength, b, bLength) ){
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertNumberArraysEqual(double *a, size_t aLength, double *b, size_t bLength, NumberReference *failures){
  double i;

  if(aLength == bLength){
    for(i = 0.0; i < aLength; i = i + 1.0){
      AssertEquals(a[(int)(i)], b[(int)(i)], failures);
    }
  }else{
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertBooleanArraysEqual(_Bool *a, size_t aLength, _Bool *b, size_t bLength, NumberReference *failures){
  double i;

  if(aLength == bLength){
    for(i = 0.0; i < aLength; i = i + 1.0){
      AssertBooleansEqual(a[(int)(i)], b[(int)(i)], failures);
    }
  }else{
    failures->numberValue = failures->numberValue + 1.0;
  }
}
void AssertStringArraysEqual(StringReference **a, size_t aLength, StringReference **b, size_t bLength, NumberReference *failures){
  double i;

  if(aLength == bLength){
    for(i = 0.0; i < aLength; i = i + 1.0){
      AssertStringEquals(a[(int)(i)]->string, a[(int)(i)]->stringLength, b[(int)(i)]->string, b[(int)(i)]->stringLength, failures);
    }
  }else{
    failures->numberValue = failures->numberValue + 1.0;
  }
}
double *ConvertToPNG(size_t *returnArrayLength, RGBABitmapImage *image){
    return ConvertToPNGWithOptions(returnArrayLength, image, 6.0, false, 0.0, 0.001);
}
double *ConvertToPNGGrayscale(size_t *returnArrayLength, RGBABitmapImage *image){
    return ConvertToPNGWithOptions(returnArrayLength, image, 0.0, false, 0.0, 0.001);
}
PHYS *PysicsHeader(double pixelsPerMeter){
  PHYS *phys;

  phys = (PHYS *)malloc(sizeof(PHYS));

  phys->pixelsPerMeter = pixelsPerMeter;

  return phys;
}
double *ConvertToPNGWithOptions(size_t *returnArrayLength, RGBABitmapImage *image, double colorType, _Bool setPhys, double pixelsPerMeter, double compressionLevel){
  PNGImage *png;
  double *pngData, *colorData;
  size_t pngDataLength, colorDataLength;

  png = (PNGImage *)malloc(sizeof(PNGImage));

  png->signature = PNGSignature(&png->signatureLength);

  png->ihdr = PNGHeader(image, colorType);

  png->physPresent = setPhys;
  png->phys = PysicsHeader(pixelsPerMeter);

  if(colorType == 6.0){
    colorData = GetPNGColorData(&colorDataLength, image);
  }else{
    colorData = GetPNGColorDataGreyscale(&colorDataLength, image);
  }
  png->zlibStruct = ZLibCompressStaticHuffman(colorData, colorDataLength, compressionLevel);

  pngData = PNGSerializeChunks(&pngDataLength, png);

  *returnArrayLength = pngDataLength;
  return pngData;
}
double *PNGSerializeChunks(size_t *returnArrayLength, PNGImage *png){
  double length, i, chunkLength;
  double *data;
  size_t dataLength;
  NumberReference *position;

  length = png->signatureLength + 12.0 + PNGHeaderLength() + 12.0 + PNGIDATLength(png) + 12.0;
  if(png->physPresent){
    length = length + 4.0 + 4.0 + 1.0 + 12.0;
  }
  data = (double*)malloc(sizeof(double) * (length));
  dataLength = length;
  position = CreateNumberReference(0.0);

  /* Signature */
  for(i = 0.0; i < png->signatureLength; i = i + 1.0){
    WriteByte(data, dataLength, png->signature[(int)(i)], position);
  }

  /* Header */
  chunkLength = PNGHeaderLength();
  Write4BytesBE(data, dataLength, chunkLength, position);
  WriteStringBytes(data, dataLength, strparam(L"IHDR"), position);
  Write4BytesBE(data, dataLength, png->ihdr->Width, position);
  Write4BytesBE(data, dataLength, png->ihdr->Height, position);
  WriteByte(data, dataLength, png->ihdr->BitDepth, position);
  WriteByte(data, dataLength, png->ihdr->ColourType, position);
  WriteByte(data, dataLength, png->ihdr->CompressionMethod, position);
  WriteByte(data, dataLength, png->ihdr->FilterMethod, position);
  WriteByte(data, dataLength, png->ihdr->InterlaceMethod, position);
  Write4BytesBE(data, dataLength, CRC32OfInterval(data, dataLength, position->numberValue - chunkLength - 4.0, chunkLength + 4.0), position);

  /* pHYs */
  if(png->physPresent){
    chunkLength = 4.0 + 4.0 + 1.0;
    Write4BytesBE(data, dataLength, chunkLength, position);
    WriteStringBytes(data, dataLength, strparam(L"pHYs"), position);

    Write4BytesBE(data, dataLength, png->phys->pixelsPerMeter, position);
    Write4BytesBE(data, dataLength, png->phys->pixelsPerMeter, position);
    WriteByte(data, dataLength, 1.0, position);
    /* 1 = pixels per meter */
    Write4BytesBE(data, dataLength, CRC32OfInterval(data, dataLength, position->numberValue - chunkLength - 4.0, chunkLength + 4.0), position);
  }

  /* IDAT */
  chunkLength = PNGIDATLength(png);
  Write4BytesBE(data, dataLength, chunkLength, position);
  WriteStringBytes(data, dataLength, strparam(L"IDAT"), position);
  WriteByte(data, dataLength, png->zlibStruct->CMF, position);
  WriteByte(data, dataLength, png->zlibStruct->FLG, position);
  for(i = 0.0; i < png->zlibStruct->CompressedDataBlocksLength; i = i + 1.0){
    WriteByte(data, dataLength, png->zlibStruct->CompressedDataBlocks[(int)(i)], position);
  }
  Write4BytesBE(data, dataLength, png->zlibStruct->Adler32CheckValue, position);
  Write4BytesBE(data, dataLength, CRC32OfInterval(data, dataLength, position->numberValue - chunkLength - 4.0, chunkLength + 4.0), position);

  /* IEND */
  chunkLength = 0.0;
  Write4BytesBE(data, dataLength, chunkLength, position);
  WriteStringBytes(data, dataLength, strparam(L"IEND"), position);
  Write4BytesBE(data, dataLength, CRC32OfInterval(data, dataLength, position->numberValue - 4.0, 4.0), position);

  *returnArrayLength = dataLength;
  return data;
}
double PNGIDATLength(PNGImage *png){
  return 2.0 + png->zlibStruct->CompressedDataBlocksLength + 4.0;
}
double PNGHeaderLength(){
  return 4.0 + 4.0 + 1.0 + 1.0 + 1.0 + 1.0 + 1.0;
}
double *GetPNGColorData(size_t *returnArrayLength, RGBABitmapImage *image){
  double *colordata;
  size_t colordataLength;
  double length, x, y, next;
  RGBA *rgba;

  length = 4.0*ImageWidth(image)*ImageHeight(image) + ImageHeight(image);

  colordata = (double*)malloc(sizeof(double) * (length));
  colordataLength = length;

  next = 0.0;

  for(y = 0.0; y < ImageHeight(image); y = y + 1.0){
    colordata[(int)(next)] = 0.0;
    next = next + 1.0;
    for(x = 0.0; x < ImageWidth(image); x = x + 1.0){
      rgba = image->x[(int)(x)]->y[(int)(y)];
      colordata[(int)(next)] = Round(rgba->r*255.0);
      next = next + 1.0;
      colordata[(int)(next)] = Round(rgba->g*255.0);
      next = next + 1.0;
      colordata[(int)(next)] = Round(rgba->b*255.0);
      next = next + 1.0;
      colordata[(int)(next)] = Round(rgba->a*255.0);
      next = next + 1.0;
    }
  }

  *returnArrayLength = colordataLength;
  return colordata;
}
double *GetPNGColorDataGreyscale(size_t *returnArrayLength, RGBABitmapImage *image){
  double *colordata;
  size_t colordataLength;
  double length, x, y, next;
  RGBA *rgba;

  length = ImageWidth(image)*ImageHeight(image) + ImageHeight(image);

  colordata = (double*)malloc(sizeof(double) * (length));
  colordataLength = length;

  next = 0.0;

  for(y = 0.0; y < ImageHeight(image); y = y + 1.0){
    colordata[(int)(next)] = 0.0;
    next = next + 1.0;
    for(x = 0.0; x < ImageWidth(image); x = x + 1.0){
      rgba = image->x[(int)(x)]->y[(int)(y)];
      colordata[(int)(next)] = Round(rgba->r*255.0);
      next = next + 1.0;
    }
  }

  *returnArrayLength = colordataLength;
  return colordata;
}
IHDR *PNGHeader(RGBABitmapImage *image, double colortype){
  IHDR *ihdr;

  ihdr = (IHDR *)malloc(sizeof(IHDR));
  ihdr->Width = ImageWidth(image);
  ihdr->Height = ImageHeight(image);
  /* Truecolour with alpha */
  ihdr->BitDepth = 8.0;
  ihdr->ColourType = colortype;
  ihdr->FilterMethod = 0.0;
  /* None */
  ihdr->CompressionMethod = 0.0;
  /* zlib */
  ihdr->InterlaceMethod = 0.0;
  /* no interlace */
  return ihdr;
}
double *PNGSignature(size_t *returnArrayLength){
  double *s;
  size_t sLength;

  s = (double*)malloc(sizeof(double) * (8.0));
  sLength = 8.0;
  s[0] = 137.0;
  s[1] = 80.0;
  s[2] = 78.0;
  s[3] = 71.0;
  s[4] = 13.0;
  s[5] = 10.0;
  s[6] = 26.0;
  s[7] = 10.0;

  *returnArrayLength = sLength;
  return s;
}
double *PNGReadDataChunks(size_t *returnArrayLength, Chunk **cs, size_t csLength){
  double i, j, length, zlibpos;
  Chunk *c;
  double *zlibData;
  size_t zlibDataLength;

  length = 0.0;
  for(i = 0.0; i < csLength; i = i + 1.0){
    c = cs[(int)(i)];
    if(aStringsEqual(c->type, c->typeLength, strparam(L"IDAT"))){
      length = length + c->length;
    }
  }

  zlibData = (double*)malloc(sizeof(double) * (length));
  zlibDataLength = length;
  zlibpos = 0.0;

  for(i = 0.0; i < csLength; i = i + 1.0){
    c = cs[(int)(i)];
    if(aStringsEqual(c->type, c->typeLength, strparam(L"IDAT"))){
      for(j = 0.0; j < c->length; j = j + 1.0){
        zlibData[(int)(zlibpos)] = c->data[(int)(j)];
        zlibpos = zlibpos + 1.0;
      }
    }
  }

  *returnArrayLength = zlibDataLength;
  return zlibData;
}
_Bool PNGReadHeader(RGBABitmapImage *image, Chunk **cs, size_t csLength, StringReference *errorMessages){
  double i;
  IHDR *ihdr;
  Chunk *c;
  NumberReference *position;
  RGBABitmapImage *n;
  _Bool success;

  position = CreateNumberReference(0.0);
  success = false;

  for(i = 0.0; i < csLength; i = i + 1.0){
    c = cs[(int)(i)];
    if(aStringsEqual(c->type, c->typeLength, strparam(L"IHDR"))){
      ihdr = (IHDR *)malloc(sizeof(IHDR));

      ihdr->Width = Read4bytesBE(c->data, c->dataLength, position);
      ihdr->Height = Read4bytesBE(c->data, c->dataLength, position);
      ihdr->BitDepth = ReadByte(c->data, c->dataLength, position);
      ihdr->ColourType = ReadByte(c->data, c->dataLength, position);
      ihdr->CompressionMethod = ReadByte(c->data, c->dataLength, position);
      ihdr->FilterMethod = ReadByte(c->data, c->dataLength, position);
      ihdr->InterlaceMethod = ReadByte(c->data, c->dataLength, position);

      n = CreateImage(ihdr->Width, ihdr->Height, GetTransparent());
      image->x = n->x;
      image->xLength = n->xLength;

      if(ihdr->ColourType == 6.0){
        if(ihdr->BitDepth == 8.0){
          if(ihdr->CompressionMethod == 0.0){
            if(ihdr->FilterMethod == 0.0){
              if(ihdr->InterlaceMethod == 0.0){
                success = true;
              }else{
                success = false;
                errorMessages->string = AppendString(&errorMessages->stringLength, errorMessages->string, errorMessages->stringLength, strparam(L"Interlace method not supported."));
              }
            }else{
              success = false;
              errorMessages->string = AppendString(&errorMessages->stringLength, errorMessages->string, errorMessages->stringLength, strparam(L"Filter method not supported."));
            }
          }else{
            success = false;
            errorMessages->string = AppendString(&errorMessages->stringLength, errorMessages->string, errorMessages->stringLength, strparam(L"Compression type not supported."));
          }
        }else{
          success = false;
          errorMessages->string = AppendString(&errorMessages->stringLength, errorMessages->string, errorMessages->stringLength, strparam(L"Bit depth not supported."));
        }
      }else{
        success = false;
        errorMessages->string = AppendString(&errorMessages->stringLength, errorMessages->string, errorMessages->stringLength, strparam(L"Color type not supported."));
      }
    }
  }

  return success;
}
Chunk **PNGReadChunks(size_t *returnArrayLength, double *data, size_t dataLength, NumberReference *position){
  _Bool done;
  double prepos;
  double chunks;
  Chunk *c;
  Chunk **cs;
  size_t csLength;
  double i;
  done = false;
  prepos = position->numberValue;
  for(chunks = 0.0;  !done ; chunks = chunks + 1.0){
    c = PNGReadChunk(data, dataLength, position);
    if(aStringsEqual(c->type, c->typeLength, strparam(L"IEND"))){
      done = true;
    }
  }
  position->numberValue = prepos;
  cs = (Chunk**)malloc(sizeof(Chunk) * chunks);
  csLength = chunks;
  for(i = 0.0; i < chunks; i = i + 1.0){
    cs[(int)(i)] = PNGReadChunk(data, dataLength, position);
  }

  *returnArrayLength = csLength;
  return cs;
}
Chunk *PNGReadChunk(double *data, size_t dataLength, NumberReference *position){
  Chunk *c;

  c = (Chunk *)malloc(sizeof(Chunk));

  c->length = Read4bytesBE(data, dataLength, position);
  c->type = (wchar_t*)malloc(sizeof(wchar_t) * (4.0));
  c->typeLength = 4.0;
  c->type[0] = ReadByte(data, dataLength, position);
  c->type[1] = ReadByte(data, dataLength, position);
  c->type[2] = ReadByte(data, dataLength, position);
  c->type[3] = ReadByte(data, dataLength, position);
  c->data = ReadXbytes(&c->dataLength, data, dataLength, position, c->length);
  c->crc = Read4bytesBE(data, dataLength, position);

  return c;
}
void WriteStringToStingStream(wchar_t *stream, size_t streamLength, NumberReference *index, wchar_t *src, size_t srcLength){
  double i;

  for(i = 0.0; i < srcLength; i = i + 1.0){
    stream[(int)(index->numberValue + i)] = src[(int)(i)];
  }
  index->numberValue = index->numberValue + srcLength;
}
void WriteCharacterToStingStream(wchar_t *stream, size_t streamLength, NumberReference *index, wchar_t src){
  stream[(int)(index->numberValue)] = src;
  index->numberValue = index->numberValue + 1.0;
}
void WriteBooleanToStingStream(wchar_t *stream, size_t streamLength, NumberReference *index, _Bool src){
  if(src){
    WriteStringToStingStream(stream, streamLength, index, strparam(L"true"));
  }else{
    WriteStringToStingStream(stream, streamLength, index, strparam(L"false"));
  }
}
_Bool SubstringWithCheck(wchar_t *string, size_t stringLength, double from, double to, StringReference *stringReference){
  _Bool success;

  if(from >= 0.0 && from <= stringLength && to >= 0.0 && to <= stringLength && from <= to){
    stringReference->string = Substring(&stringReference->stringLength, string, stringLength, from, to);
    success = true;
  }else{
    success = false;
  }

  return success;
}
wchar_t *Substring(size_t *returnArrayLength, wchar_t *string, size_t stringLength, double from, double to){
  wchar_t *n;
  size_t nLength;
  double i, length;

  length = to - from;

  n = (wchar_t*)malloc(sizeof(wchar_t) * (length));
  nLength = length;

  for(i = from; i < to; i = i + 1.0){
    n[(int)(i - from)] = string[(int)(i)];
  }

  *returnArrayLength = nLength;
  return n;
}
wchar_t *AppendString(size_t *returnArrayLength, wchar_t *s1, size_t s1Length, wchar_t *s2, size_t s2Length){
  wchar_t *newString;
  size_t newStringLength;

  newString = ConcatenateString(&newStringLength, s1, s1Length, s2, s2Length);

  free(s1);

  *returnArrayLength = newStringLength;
  return newString;
}
wchar_t *ConcatenateString(size_t *returnArrayLength, wchar_t *s1, size_t s1Length, wchar_t *s2, size_t s2Length){
  wchar_t *newString;
  size_t newStringLength;
  double i;

  newString = (wchar_t*)malloc(sizeof(wchar_t) * (s1Length + s2Length));
  newStringLength = s1Length + s2Length;

  for(i = 0.0; i < s1Length; i = i + 1.0){
    newString[(int)(i)] = s1[(int)(i)];
  }

  for(i = 0.0; i < s2Length; i = i + 1.0){
    newString[(int)(s1Length + i)] = s2[(int)(i)];
  }

  *returnArrayLength = newStringLength;
  return newString;
}
wchar_t *AppendCharacter(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t c){
  wchar_t *newString;
  size_t newStringLength;

  newString = ConcatenateCharacter(&newStringLength, string, stringLength, c);

  free(string);

  *returnArrayLength = newStringLength;
  return newString;
}
wchar_t *ConcatenateCharacter(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t c){
  wchar_t *newString;
  size_t newStringLength;
  double i;
  newString = (wchar_t*)malloc(sizeof(wchar_t) * (stringLength + 1.0));
  newStringLength = stringLength + 1.0;

  for(i = 0.0; i < stringLength; i = i + 1.0){
    newString[(int)(i)] = string[(int)(i)];
  }

  newString[(int)(stringLength)] = c;

  *returnArrayLength = newStringLength;
  return newString;
}
StringReference **SplitByCharacter(size_t *returnArrayLength, wchar_t *toSplit, size_t toSplitLength, wchar_t splitBy){
  StringReference **split;
  size_t splitLength;
  wchar_t *stringToSplitBy;
  size_t stringToSplitByLength;

  stringToSplitBy = (wchar_t*)malloc(sizeof(wchar_t) * (1.0));
  stringToSplitByLength = 1.0;
  stringToSplitBy[0] = splitBy;

  split = SplitByString(&splitLength, toSplit, toSplitLength, stringToSplitBy, stringToSplitByLength);

  free(stringToSplitBy);

  *returnArrayLength = splitLength;
  return split;
}
_Bool IndexOfCharacter(wchar_t *string, size_t stringLength, wchar_t character, NumberReference *indexReference){
  double i;
  _Bool found;

  found = false;
  for(i = 0.0; i < stringLength &&  !found ; i = i + 1.0){
    if(string[(int)(i)] == character){
      found = true;
      indexReference->numberValue = i;
    }
  }

  return found;
}
_Bool SubstringEqualsWithCheck(wchar_t *string, size_t stringLength, double from, wchar_t *substring, size_t substringLength, BooleanReference *equalsReference){
  _Bool success;

  if(from < stringLength){
    success = true;
    equalsReference->booleanValue = SubstringEquals(string, stringLength, from, substring, substringLength);
  }else{
    success = false;
  }

  return success;
}
_Bool SubstringEquals(wchar_t *string, size_t stringLength, double from, wchar_t *substring, size_t substringLength){
  double i;
  _Bool equal;

  equal = true;
  if(stringLength - from >= substringLength){
    for(i = 0.0; i < substringLength && equal; i = i + 1.0){
      if(string[(int)(from + i)] != substring[(int)(i)]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
_Bool IndexOfString(wchar_t *string, size_t stringLength, wchar_t *substring, size_t substringLength, NumberReference *indexReference){
  double i;
  _Bool found;

  found = false;
  for(i = 0.0; i < stringLength - substringLength + 1.0 &&  !found ; i = i + 1.0){
    if(SubstringEquals(string, stringLength, i, substring, substringLength)){
      found = true;
      indexReference->numberValue = i;
    }
  }

  return found;
}
_Bool ContainsCharacter(wchar_t *string, size_t stringLength, wchar_t character){
  double i;
  _Bool found;

  found = false;
  for(i = 0.0; i < stringLength &&  !found ; i = i + 1.0){
    if(string[(int)(i)] == character){
      found = true;
    }
  }

  return found;
}
_Bool ContainsString(wchar_t *string, size_t stringLength, wchar_t *substring, size_t substringLength){
  return IndexOfString(string, stringLength, substring, substringLength, (NumberReference *)malloc(sizeof(NumberReference)));
}
void ToUpperCase(wchar_t *string, size_t stringLength){
  double i;

  for(i = 0.0; i < stringLength; i = i + 1.0){
    string[(int)(i)] = charToUpperCase(string[(int)(i)]);
  }
}
void ToLowerCase(wchar_t *string, size_t stringLength){
  double i;

  for(i = 0.0; i < stringLength; i = i + 1.0){
    string[(int)(i)] = charToLowerCase(string[(int)(i)]);
  }
}
_Bool EqualsIgnoreCase(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength){
  _Bool equal;
  double i;

  if(aLength == bLength){
    equal = true;
    for(i = 0.0; i < aLength && equal; i = i + 1.0){
      if(charToLowerCase(a[(int)(i)]) != charToLowerCase(b[(int)(i)])){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
wchar_t *ReplaceString(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t *toReplace, size_t toReplaceLength, wchar_t *replaceWith, size_t replaceWithLength){
  wchar_t *result;
  size_t resultLength;
  double i;
  BooleanReference *equalsReference;
  _Bool success;

  equalsReference = (BooleanReference *)malloc(sizeof(BooleanReference));
  result = (wchar_t*)malloc(sizeof(wchar_t) * (0.0));
  resultLength = 0.0;

  for(i = 0.0; i < stringLength; ){
    success = SubstringEqualsWithCheck(string, stringLength, i, toReplace, toReplaceLength, equalsReference);
    if(success){
      success = equalsReference->booleanValue;
    }

    if(success && toReplaceLength > 0.0){
      result = ConcatenateString(&resultLength, result, resultLength, replaceWith, replaceWithLength);
      i = i + toReplaceLength;
    }else{
      result = ConcatenateCharacter(&resultLength, result, resultLength, string[(int)(i)]);
      i = i + 1.0;
    }
  }

  *returnArrayLength = resultLength;
  return result;
}
wchar_t *ReplaceCharacter(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t toReplace, wchar_t replaceWith){
  wchar_t *result;
  size_t resultLength;
  double i;

  result = (wchar_t*)malloc(sizeof(wchar_t) * (0.0));
  resultLength = 0.0;

  for(i = 0.0; i < stringLength; i = i + 1.0){
    if(string[(int)(i)] == toReplace){
      result = ConcatenateCharacter(&resultLength, result, resultLength, replaceWith);
    }else{
      result = ConcatenateCharacter(&resultLength, result, resultLength, string[(int)(i)]);
    }
  }

  *returnArrayLength = resultLength;
  return result;
}
wchar_t *Trim(size_t *returnArrayLength, wchar_t *string, size_t stringLength){
  wchar_t *result;
  size_t resultLength;
  double i, lastWhitespaceLocationStart, lastWhitespaceLocationEnd;
  _Bool firstNonWhitespaceFound;

  /* Find whitepaces at the start. */
  lastWhitespaceLocationStart =  -1.0;
  firstNonWhitespaceFound = false;
  for(i = 0.0; i < stringLength &&  !firstNonWhitespaceFound ; i = i + 1.0){
    if(charIsWhiteSpace(string[(int)(i)])){
      lastWhitespaceLocationStart = i;
    }else{
      firstNonWhitespaceFound = true;
    }
  }

  /* Find whitepaces at the end. */
  lastWhitespaceLocationEnd = stringLength;
  firstNonWhitespaceFound = false;
  for(i = stringLength - 1.0; i >= 0.0 &&  !firstNonWhitespaceFound ; i = i - 1.0){
    if(charIsWhiteSpace(string[(int)(i)])){
      lastWhitespaceLocationEnd = i;
    }else{
      firstNonWhitespaceFound = true;
    }
  }

  if(lastWhitespaceLocationStart < lastWhitespaceLocationEnd){
    result = Substring(&resultLength, string, stringLength, lastWhitespaceLocationStart + 1.0, lastWhitespaceLocationEnd);
  }else{
    result = (wchar_t*)malloc(sizeof(wchar_t) * (0.0));
    resultLength = 0.0;
  }

  *returnArrayLength = resultLength;
  return result;
}
_Bool StartsWith(wchar_t *string, size_t stringLength, wchar_t *start, size_t startLength){
  _Bool startsWithString;

  startsWithString = false;
  if(stringLength >= startLength){
    startsWithString = SubstringEquals(string, stringLength, 0.0, start, startLength);
  }

  return startsWithString;
}
_Bool EndsWith(wchar_t *string, size_t stringLength, wchar_t *end, size_t endLength){
  _Bool endsWithString;

  endsWithString = false;
  if(stringLength >= endLength){
    endsWithString = SubstringEquals(string, stringLength, stringLength - endLength, end, endLength);
  }

  return endsWithString;
}
StringReference **SplitByString(size_t *returnArrayLength, wchar_t *toSplit, size_t toSplitLength, wchar_t *splitBy, size_t splitByLength){
  StringReference **split;
  size_t splitLength;
  wchar_t *next;
  size_t nextLength;
  double i;
  wchar_t c;
  StringReference *n;

  split = (StringReference**)malloc(sizeof(StringReference) * 0.0);
  splitLength = 0.0;

  next = (wchar_t*)malloc(sizeof(wchar_t) * (0.0));
  nextLength = 0.0;
  for(i = 0.0; i < toSplitLength; ){
    c = toSplit[(int)(i)];

    if(SubstringEquals(toSplit, toSplitLength, i, splitBy, splitByLength)){
      n = (StringReference *)malloc(sizeof(StringReference));
      n->string = next;
      n->stringLength = nextLength;
      split = AddString(&splitLength, split, splitLength, n);
      next = (wchar_t*)malloc(sizeof(wchar_t) * (0.0));
      nextLength = 0.0;
      i = i + splitByLength;
    }else{
      next = AppendCharacter(&nextLength, next, nextLength, c);
      i = i + 1.0;
    }
  }

  n = (StringReference *)malloc(sizeof(StringReference));
  n->string = next;
  n->stringLength = nextLength;
  split = AddString(&splitLength, split, splitLength, n);

  *returnArrayLength = splitLength;
  return split;
}
_Bool StringIsBefore(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength){
  _Bool before, equal, done;
  double i;

  before = false;
  equal = true;
  done = false;

  if(aLength == 0.0 && bLength > 0.0){
    before = true;
  }else{
    for(i = 0.0; i < aLength && i < bLength &&  !done ; i = i + 1.0){
      if(a[(int)(i)] != b[(int)(i)]){
        equal = false;
      }
      if(charCharacterIsBefore(a[(int)(i)], b[(int)(i)])){
        before = true;
      }
      if(charCharacterIsBefore(b[(int)(i)], a[(int)(i)])){
        done = true;
      }
    }

    if(equal){
      if(aLength < bLength){
        before = true;
      }
    }
  }

  return before;
}
double *ReadXbytes(size_t *returnArrayLength, double *data, size_t dataLength, NumberReference *position, double length){
  double *r;
  size_t rLength;
  double i;

  r = (double*)malloc(sizeof(double) * (length));
  rLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    r[(int)(i)] = ReadByte(data, dataLength, position);
  }

  *returnArrayLength = rLength;
  return r;
}
double Read4bytesBE(double *data, size_t dataLength, NumberReference *position){
  double r;

  r = 0.0;
  r = r + pow(2.0, 24.0)*ReadByte(data, dataLength, position);
  r = r + pow(2.0, 16.0)*ReadByte(data, dataLength, position);
  r = r + pow(2.0, 8.0)*ReadByte(data, dataLength, position);
  r = r + ReadByte(data, dataLength, position);

  return r;
}
double Read2bytesBE(double *data, size_t dataLength, NumberReference *position){
  double r;

  r = 0.0;
  r = r + pow(2.0, 8.0)*ReadByte(data, dataLength, position);
  r = r + ReadByte(data, dataLength, position);

  return r;
}
double ReadByte(double *data, size_t dataLength, NumberReference *position){
  double next;

  next = data[(int)(position->numberValue)];
  position->numberValue = position->numberValue + 1.0;

  return next;
}
double Read4bytesLE(double *data, size_t dataLength, NumberReference *position){
  double r;

  r = 0.0;
  r = r + ReadByte(data, dataLength, position);
  r = r + pow(2.0, 8.0)*ReadByte(data, dataLength, position);
  r = r + pow(2.0, 16.0)*ReadByte(data, dataLength, position);
  r = r + pow(2.0, 24.0)*ReadByte(data, dataLength, position);

  return r;
}
void WriteByte(double *data, size_t dataLength, double b, NumberReference *position){
  data[(int)(position->numberValue)] = b;
  position->numberValue = position->numberValue + 1.0;
}
void Write2BytesLE(double *data, size_t dataLength, double b, NumberReference *position){
  data[(int)(position->numberValue)] = Round(fmod(b, pow(2.0, 8.0)));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 8.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
}
void Write4BytesLE(double *data, size_t dataLength, double b, NumberReference *position){
  data[(int)(position->numberValue)] = Round(fmod(b, pow(2.0, 8.0)));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 8.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 16.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 24.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
}
void Write2BytesBE(double *data, size_t dataLength, double b, NumberReference *position){
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 8.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = Round(fmod(b, pow(2.0, 8.0)));
  position->numberValue = position->numberValue + 1.0;
}
void Write4BytesBE(double *data, size_t dataLength, double b, NumberReference *position){
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 24.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 16.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = fmod(floor(b/pow(2.0, 8.0)), pow(2.0, 8.0));
  position->numberValue = position->numberValue + 1.0;
  data[(int)(position->numberValue)] = Round(fmod(b, pow(2.0, 8.0)));
  position->numberValue = position->numberValue + 1.0;
}
void WriteStringBytes(double *data, size_t dataLength, wchar_t *cs, size_t csLength, NumberReference *position){
  double i, v;

  for(i = 0.0; i < csLength; i = i + 1.0){
    v = cs[(int)(i)];
    WriteByte(data, dataLength, v, position);
  }
}
double *MakeCRC32Table(size_t *returnArrayLength){
  double c, n, k;
  double *crcTable;
  size_t crcTableLength;

  crcTable = (double*)malloc(sizeof(double) * (256.0));
  crcTableLength = 256.0;

  for(n = 0.0; n < 256.0; n = n + 1.0){
    c = n;
    for(k = 0.0; k < 8.0; k = k + 1.0){
      if( !DivisibleBy(c, 2.0) ){
        c = Xor4Byte(3988292384.0, floor(c/2.0));
      }else{
        c = floor(c/2.0);
      }
    }
    crcTable[(int)(n)] = c;
  }

  *returnArrayLength = crcTableLength;
  return crcTable;
}
double UpdateCRC32(double crc, double *buf, size_t bufLength, double *crc_table, size_t crc_tableLength){
  double n, index;

  for(n = 0.0; n < bufLength; n = n + 1.0){
    index = And4Byte(Xor4Byte(crc, buf[(int)(n)]), pow(2.0, 8.0) - 1.0);
    crc = Xor4Byte(crc_table[(int)(index)], floor(crc/pow(2.0, 8.0)));
  }

  return crc;
}
double CalculateCRC32(double *buf, size_t bufLength){
  double *crcTable;
  size_t crcTableLength;
  double b32max, value;

  crcTable = MakeCRC32Table(&crcTableLength);

  b32max = pow(2.0, 32.0) - 1.0;
  value = UpdateCRC32(b32max, buf, bufLength, crcTable, crcTableLength);

  return Xor4Byte(value, b32max);
}
double CRC32OfInterval(double *data, size_t dataLength, double from, double length){
  double *crcBase;
  size_t crcBaseLength;
  double i, crc;

  crcBase = (double*)malloc(sizeof(double) * (length));
  crcBaseLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    crcBase[(int)(i)] = data[(int)(from + i)];
  }

  crc = CalculateCRC32(crcBase, crcBaseLength);

  free(crcBase);

  return crc;
}
ZLIBStruct *ZLibCompressNoCompression(double *data, size_t dataLength){
  ZLIBStruct *zlibStruct;

  zlibStruct = (ZLIBStruct *)malloc(sizeof(ZLIBStruct));

  zlibStruct->CMF = 120.0;
  zlibStruct->FLG = 1.0;
  zlibStruct->CompressedDataBlocks = DeflateDataNoCompression(&zlibStruct->CompressedDataBlocksLength, data, dataLength);
  zlibStruct->Adler32CheckValue = ComputeAdler32(data, dataLength);

  return zlibStruct;
}
ZLIBStruct *ZLibCompressStaticHuffman(double *data, size_t dataLength, double level){
  ZLIBStruct *zlibStruct;

  zlibStruct = (ZLIBStruct *)malloc(sizeof(ZLIBStruct));

  zlibStruct->CMF = 120.0;
  zlibStruct->FLG = 1.0;
  zlibStruct->CompressedDataBlocks = DeflateDataStaticHuffman(&zlibStruct->CompressedDataBlocksLength, data, dataLength, level);
  zlibStruct->Adler32CheckValue = ComputeAdler32(data, dataLength);

  return zlibStruct;
}
double *AddNumber(size_t *returnArrayLength, double *list, size_t listLength, double a){
  double *newlist;
  size_t newlistLength;
  double i;

  newlist = (double*)malloc(sizeof(double) * (listLength + 1.0));
  newlistLength = listLength + 1.0;
  for(i = 0.0; i < listLength; i = i + 1.0){
    newlist[(int)(i)] = list[(int)(i)];
  }
  newlist[(int)(listLength)] = a;
		
  free(list);
		
  *returnArrayLength = newlistLength;
  return newlist;
}
void AddNumberRef(NumberArrayReference *list, double i){
  list->numberArray = AddNumber(&list->numberArrayLength, list->numberArray, list->numberArrayLength, i);
}
double *RemoveNumber(size_t *returnArrayLength, double *list, size_t listLength, double n){
  double *newlist;
  size_t newlistLength;
  double i;

  newlist = (double*)malloc(sizeof(double) * (listLength - 1.0));
  newlistLength = listLength - 1.0;

  if(n >= 0.0 && n < listLength){
    for(i = 0.0; i < listLength; i = i + 1.0){
      if(i < n){
        newlist[(int)(i)] = list[(int)(i)];
      }
      if(i > n){
        newlist[(int)(i - 1.0)] = list[(int)(i)];
      }
    }

    free(list);
  }else{
    free(newlist);
  }
		
  *returnArrayLength = newlistLength;
  return newlist;
}
double GetNumberRef(NumberArrayReference *list, double i){
  return list->numberArray[(int)(i)];
}
void RemoveNumberRef(NumberArrayReference *list, double i){
  list->numberArray = RemoveNumber(&list->numberArrayLength, list->numberArray, list->numberArrayLength, i);
}
StringReference **AddString(size_t *returnArrayLength, StringReference **list, size_t listLength, StringReference *a){
  StringReference **newlist;
  size_t newlistLength;
  double i;

  newlist = (StringReference**)malloc(sizeof(StringReference) * listLength + 1.0);
  newlistLength = listLength + 1.0;

  for(i = 0.0; i < listLength; i = i + 1.0){
    newlist[(int)(i)] = list[(int)(i)];
  }
  newlist[(int)(listLength)] = a;
		
  free(list);
		
  *returnArrayLength = newlistLength;
  return newlist;
}
void AddStringRef(StringArrayReference *list, StringReference *i){
  list->stringArray = AddString(&list->stringArrayLength, list->stringArray, list->stringArrayLength, i);
}
StringReference **RemoveString(size_t *returnArrayLength, StringReference **list, size_t listLength, double n){
  StringReference **newlist;
  size_t newlistLength;
  double i;

  newlist = (StringReference**)malloc(sizeof(StringReference) * listLength - 1.0);
  newlistLength = listLength - 1.0;

  if(n >= 0.0 && n < listLength){
    for(i = 0.0; i < listLength; i = i + 1.0){
      if(i < n){
        newlist[(int)(i)] = list[(int)(i)];
      }
      if(i > n){
        newlist[(int)(i - 1.0)] = list[(int)(i)];
      }
    }

    free(list);
  }else{
    free(newlist);
  }
		
  *returnArrayLength = newlistLength;
  return newlist;
}
StringReference *GetStringRef(StringArrayReference *list, double i){
  return list->stringArray[(int)(i)];
}
void RemoveStringRef(StringArrayReference *list, double i){
  list->stringArray = RemoveString(&list->stringArrayLength, list->stringArray, list->stringArrayLength, i);
}
_Bool *AddBoolean(size_t *returnArrayLength, _Bool *list, size_t listLength, _Bool a){
  _Bool *newlist;
  size_t newlistLength;
  double i;

  newlist = (_Bool*)malloc(sizeof(_Bool) * (listLength + 1.0));
  newlistLength = listLength + 1.0;
  for(i = 0.0; i < listLength; i = i + 1.0){
    newlist[(int)(i)] = list[(int)(i)];
  }
  newlist[(int)(listLength)] = a;
		
  free(list);
		
  *returnArrayLength = newlistLength;
  return newlist;
}
void AddBooleanRef(BooleanArrayReference *list, _Bool i){
  list->booleanArray = AddBoolean(&list->booleanArrayLength, list->booleanArray, list->booleanArrayLength, i);
}
_Bool *RemoveBoolean(size_t *returnArrayLength, _Bool *list, size_t listLength, double n){
  _Bool *newlist;
  size_t newlistLength;
  double i;

  newlist = (_Bool*)malloc(sizeof(_Bool) * (listLength - 1.0));
  newlistLength = listLength - 1.0;

  if(n >= 0.0 && n < listLength){
    for(i = 0.0; i < listLength; i = i + 1.0){
      if(i < n){
        newlist[(int)(i)] = list[(int)(i)];
      }
      if(i > n){
        newlist[(int)(i - 1.0)] = list[(int)(i)];
      }
    }

    free(list);
  }else{
    free(newlist);
  }
		
  *returnArrayLength = newlistLength;
  return newlist;
}
_Bool GetBooleanRef(BooleanArrayReference *list, double i){
  return list->booleanArray[(int)(i)];
}
void RemoveDecimalRef(BooleanArrayReference *list, double i){
  list->booleanArray = RemoveBoolean(&list->booleanArrayLength, list->booleanArray, list->booleanArrayLength, i);
}
LinkedListStrings *CreateLinkedListString(){
  LinkedListStrings *ll;

  ll = (LinkedListStrings *)malloc(sizeof(LinkedListStrings));
  ll->first = (LinkedListNodeStrings *)malloc(sizeof(LinkedListNodeStrings));
  ll->last = ll->first;
  ll->last->end = true;

  return ll;
}
void LinkedListAddString(LinkedListStrings *ll, wchar_t *value, size_t valueLength){
  ll->last->end = false;
  ll->last->value = value;
  ll->last->valueLength = valueLength;
  ll->last->next = (LinkedListNodeStrings *)malloc(sizeof(LinkedListNodeStrings));
  ll->last->next->end = true;
  ll->last = ll->last->next;
}
StringReference **LinkedListStringsToArray(size_t *returnArrayLength, LinkedListStrings *ll){
  StringReference **array;
  size_t arrayLength;
  double length, i;
  LinkedListNodeStrings *node;

  node = ll->first;

  length = LinkedListStringsLength(ll);

  array = (StringReference**)malloc(sizeof(StringReference) * length);
  arrayLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    array[(int)(i)] = (StringReference *)malloc(sizeof(StringReference));
    array[(int)(i)]->string = node->value;
    array[(int)(i)]->stringLength = node->valueLength;
    node = node->next;
  }

  *returnArrayLength = arrayLength;
  return array;
}
double LinkedListStringsLength(LinkedListStrings *ll){
  double l;
  LinkedListNodeStrings *node;

  l = 0.0;
  node = ll->first;
  for(;  !node->end ; ){
    node = node->next;
    l = l + 1.0;
  }

  return l;
}
void FreeLinkedListString(LinkedListStrings *ll){
  LinkedListNodeStrings *node, *prev;

  node = ll->first;

  for(;  !node->end ; ){
    prev = node;
    node = node->next;
    free(prev);
  }

  free(node);
}
LinkedListNumbers *CreateLinkedListNumbers(){
  LinkedListNumbers *ll;

  ll = (LinkedListNumbers *)malloc(sizeof(LinkedListNumbers));
  ll->first = (LinkedListNodeNumbers *)malloc(sizeof(LinkedListNodeNumbers));
  ll->last = ll->first;
  ll->last->end = true;

  return ll;
}
LinkedListNumbers **CreateLinkedListNumbersArray(size_t *returnArrayLength, double length){
  LinkedListNumbers **lls;
  size_t llsLength;
  double i;

  lls = (LinkedListNumbers**)malloc(sizeof(LinkedListNumbers) * length);
  llsLength = length;
  for(i = 0.0; i < llsLength; i = i + 1.0){
    lls[(int)(i)] = CreateLinkedListNumbers();
  }

  *returnArrayLength = llsLength;
  return lls;
}
void LinkedListAddNumber(LinkedListNumbers *ll, double value){
  ll->last->end = false;
  ll->last->value = value;
  ll->last->next = (LinkedListNodeNumbers *)malloc(sizeof(LinkedListNodeNumbers));
  ll->last->next->end = true;
  ll->last = ll->last->next;
}
double LinkedListNumbersLength(LinkedListNumbers *ll){
  double l;
  LinkedListNodeNumbers *node;

  l = 0.0;
  node = ll->first;
  for(;  !node->end ; ){
    node = node->next;
    l = l + 1.0;
  }

  return l;
}
double LinkedListNumbersIndex(LinkedListNumbers *ll, double index){
  double i;
  LinkedListNodeNumbers *node;

  node = ll->first;
  for(i = 0.0; i < index; i = i + 1.0){
    node = node->next;
  }

  return node->value;
}
void LinkedListInsertNumber(LinkedListNumbers *ll, double index, double value){
  double i;
  LinkedListNodeNumbers *node, *tmp;

  if(index == 0.0){
    tmp = ll->first;
    ll->first = (LinkedListNodeNumbers *)malloc(sizeof(LinkedListNodeNumbers));
    ll->first->next = tmp;
    ll->first->value = value;
    ll->first->end = false;
  }else{
    node = ll->first;
    for(i = 0.0; i < index - 1.0; i = i + 1.0){
      node = node->next;
    }

    tmp = node->next;
    node->next = (LinkedListNodeNumbers *)malloc(sizeof(LinkedListNodeNumbers));
    node->next->next = tmp;
    node->next->value = value;
    node->next->end = false;
  }
}
void LinkedListSet(LinkedListNumbers *ll, double index, double value){
  double i;
  LinkedListNodeNumbers *node;

  node = ll->first;
  for(i = 0.0; i < index; i = i + 1.0){
    node = node->next;
  }

  node->next->value = value;
}
void LinkedListRemoveNumber(LinkedListNumbers *ll, double index){
  double i;
  LinkedListNodeNumbers *node, *prev;

  node = ll->first;
  prev = ll->first;

  for(i = 0.0; i < index; i = i + 1.0){
    prev = node;
    node = node->next;
  }

  if(index == 0.0){
    ll->first = prev->next;
  }
  if( !prev->next->end ){
    prev->next = prev->next->next;
  }
}
void FreeLinkedListNumbers(LinkedListNumbers *ll){
  LinkedListNodeNumbers *node, *prev;

  node = ll->first;

  for(;  !node->end ; ){
    prev = node;
    node = node->next;
    free(prev);
  }

  free(node);
}
void FreeLinkedListNumbersArray(LinkedListNumbers **lls, size_t llsLength){
  double i;

  for(i = 0.0; i < llsLength; i = i + 1.0){
    FreeLinkedListNumbers(lls[(int)(i)]);
  }
  free(lls);
}
double *LinkedListNumbersToArray(size_t *returnArrayLength, LinkedListNumbers *ll){
  double *array;
  size_t arrayLength;
  double length, i;
  LinkedListNodeNumbers *node;

  node = ll->first;

  length = LinkedListNumbersLength(ll);

  array = (double*)malloc(sizeof(double) * (length));
  arrayLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    array[(int)(i)] = node->value;
    node = node->next;
  }

  *returnArrayLength = arrayLength;
  return array;
}
LinkedListNumbers *ArrayToLinkedListNumbers(double *array, size_t arrayLength){
  LinkedListNumbers *ll;
  double i;

  ll = CreateLinkedListNumbers();

  for(i = 0.0; i < arrayLength; i = i + 1.0){
    LinkedListAddNumber(ll, array[(int)(i)]);
  }

  return ll;
}
_Bool LinkedListNumbersEqual(LinkedListNumbers *a, LinkedListNumbers *b){
  _Bool equal, done;
  LinkedListNodeNumbers *an, *bn;

  an = a->first;
  bn = b->first;

  equal = true;
  done = false;
  for(; equal &&  !done ; ){
    if(an->end == bn->end){
      if(an->end){
        done = true;
      }else if(an->value == bn->value){
        an = an->next;
        bn = bn->next;
      }else{
        equal = false;
      }
    }else{
      equal = false;
    }
  }

  return equal;
}
LinkedListCharacters *CreateLinkedListCharacter(){
  LinkedListCharacters *ll;

  ll = (LinkedListCharacters *)malloc(sizeof(LinkedListCharacters));
  ll->first = (LinkedListNodeCharacters *)malloc(sizeof(LinkedListNodeCharacters));
  ll->last = ll->first;
  ll->last->end = true;

  return ll;
}
void LinkedListAddCharacter(LinkedListCharacters *ll, wchar_t value){
  ll->last->end = false;
  ll->last->value = value;
  ll->last->next = (LinkedListNodeCharacters *)malloc(sizeof(LinkedListNodeCharacters));
  ll->last->next->end = true;
  ll->last = ll->last->next;
}
wchar_t *LinkedListCharactersToArray(size_t *returnArrayLength, LinkedListCharacters *ll){
  wchar_t *array;
  size_t arrayLength;
  double length, i;
  LinkedListNodeCharacters *node;

  node = ll->first;

  length = LinkedListCharactersLength(ll);

  array = (wchar_t*)malloc(sizeof(wchar_t) * (length));
  arrayLength = length;

  for(i = 0.0; i < length; i = i + 1.0){
    array[(int)(i)] = node->value;
    node = node->next;
  }

  *returnArrayLength = arrayLength;
  return array;
}
double LinkedListCharactersLength(LinkedListCharacters *ll){
  double l;
  LinkedListNodeCharacters *node;

  l = 0.0;
  node = ll->first;
  for(;  !node->end ; ){
    node = node->next;
    l = l + 1.0;
  }

  return l;
}
void FreeLinkedListCharacter(LinkedListCharacters *ll){
  LinkedListNodeCharacters *node, *prev;

  node = ll->first;

  for(;  !node->end ; ){
    prev = node;
    node = node->next;
    free(prev);
  }

  free(node);
}
DynamicArrayNumbers *CreateDynamicArrayNumbers(){
  DynamicArrayNumbers *da;

  da = (DynamicArrayNumbers *)malloc(sizeof(DynamicArrayNumbers));
  da->array = (double*)malloc(sizeof(double) * (10.0));
  da->arrayLength = 10.0;
  da->length = 0.0;

  return da;
}
DynamicArrayNumbers *CreateDynamicArrayNumbersWithInitialCapacity(double capacity){
  DynamicArrayNumbers *da;

  da = (DynamicArrayNumbers *)malloc(sizeof(DynamicArrayNumbers));
  da->array = (double*)malloc(sizeof(double) * (capacity));
  da->arrayLength = capacity;
  da->length = 0.0;

  return da;
}
void DynamicArrayAddNumber(DynamicArrayNumbers *da, double value){
  if(da->length == da->arrayLength){
    DynamicArrayNumbersIncreaseSize(da);
  }

  da->array[(int)(da->length)] = value;
  da->length = da->length + 1.0;
}
void DynamicArrayNumbersIncreaseSize(DynamicArrayNumbers *da){
  double newLength, i;
  double *newArray;
  size_t newArrayLength;

  newLength = round(da->arrayLength*3.0/2.0);
  newArray = (double*)malloc(sizeof(double) * (newLength));
  newArrayLength = newLength;

  for(i = 0.0; i < da->arrayLength; i = i + 1.0){
    newArray[(int)(i)] = da->array[(int)(i)];
  }

  free(da->array);

  da->array = newArray;
  da->arrayLength = newArrayLength;
}
_Bool DynamicArrayNumbersDecreaseSizeNecessary(DynamicArrayNumbers *da){
  _Bool needsDecrease;

  needsDecrease = false;

  if(da->length > 10.0){
    needsDecrease = da->length <= round(da->arrayLength*2.0/3.0);
  }

  return needsDecrease;
}
void DynamicArrayNumbersDecreaseSize(DynamicArrayNumbers *da){
  double newLength, i;
  double *newArray;
  size_t newArrayLength;

  newLength = round(da->arrayLength*2.0/3.0);
  newArray = (double*)malloc(sizeof(double) * (newLength));
  newArrayLength = newLength;

  for(i = 0.0; i < newLength; i = i + 1.0){
    newArray[(int)(i)] = da->array[(int)(i)];
  }

  free(da->array);

  da->array = newArray;
  da->arrayLength = newArrayLength;
}
double DynamicArrayNumbersIndex(DynamicArrayNumbers *da, double index){
  return da->array[(int)(index)];
}
double DynamicArrayNumbersLength(DynamicArrayNumbers *da){
  return da->length;
}
void DynamicArrayInsertNumber(DynamicArrayNumbers *da, double index, double value){
  double i;

  if(da->length == da->arrayLength){
    DynamicArrayNumbersIncreaseSize(da);
  }

  for(i = da->length; i > index; i = i - 1.0){
    da->array[(int)(i)] = da->array[(int)(i - 1.0)];
  }

  da->array[(int)(index)] = value;

  da->length = da->length + 1.0;
}
void DynamicArraySet(DynamicArrayNumbers *da, double index, double value){
  da->array[(int)(index)] = value;
}
void DynamicArrayRemoveNumber(DynamicArrayNumbers *da, double index){
  double i;

  for(i = index; i < da->length - 1.0; i = i + 1.0){
    da->array[(int)(i)] = da->array[(int)(i + 1.0)];
  }

  da->length = da->length - 1.0;

  if(DynamicArrayNumbersDecreaseSizeNecessary(da)){
    DynamicArrayNumbersDecreaseSize(da);
  }
}
void FreeDynamicArrayNumbers(DynamicArrayNumbers *da){
  free(da->array);
  free(da);
}
double *DynamicArrayNumbersToArray(size_t *returnArrayLength, DynamicArrayNumbers *da){
  double *array;
  size_t arrayLength;
  double i;

  array = (double*)malloc(sizeof(double) * (da->length));
  arrayLength = da->length;

  for(i = 0.0; i < da->length; i = i + 1.0){
    array[(int)(i)] = da->array[(int)(i)];
  }

  *returnArrayLength = arrayLength;
  return array;
}
DynamicArrayNumbers *ArrayToDynamicArrayNumbersWithOptimalSize(double *array, size_t arrayLength){
  DynamicArrayNumbers *da;
  double i;
  double c, n, newCapacity;

  /*
         c = 10*(3/2)^n
         log(c) = log(10*(3/2)^n)
         log(c) = log(10) + log((3/2)^n)
         log(c) = 1 + log((3/2)^n)
         log(c) - 1 = log((3/2)^n)
         log(c) - 1 = n*log(3/2)
         n = (log(c) - 1)/log(3/2)
         */
  c = arrayLength;
  n = (log(c) - 1.0)/log(3.0/2.0);
  newCapacity = floor(n) + 1.0;

  da = CreateDynamicArrayNumbersWithInitialCapacity(newCapacity);

  for(i = 0.0; i < arrayLength; i = i + 1.0){
    da->array[(int)(i)] = array[(int)(i)];
  }

  return da;
}
DynamicArrayNumbers *ArrayToDynamicArrayNumbers(double *array, size_t arrayLength){
  DynamicArrayNumbers *da;

  da = (DynamicArrayNumbers *)malloc(sizeof(DynamicArrayNumbers));
  da->array = aCopyNumberArray(&da->arrayLength, array, arrayLength);
  da->length = arrayLength;

  return da;
}
_Bool DynamicArrayNumbersEqual(DynamicArrayNumbers *a, DynamicArrayNumbers *b){
  _Bool equal;
  double i;

  equal = true;
  if(a->length == b->length){
    for(i = 0.0; i < a->length && equal; i = i + 1.0){
      if(a->array[(int)(i)] != b->array[(int)(i)]){
        equal = false;
      }
    }
  }else{
    equal = false;
  }

  return equal;
}
LinkedListNumbers *DynamicArrayNumbersToLinkedList(DynamicArrayNumbers *da){
  LinkedListNumbers *ll;
  double i;

  ll = CreateLinkedListNumbers();

  for(i = 0.0; i < da->length; i = i + 1.0){
    LinkedListAddNumber(ll, da->array[(int)(i)]);
  }

  return ll;
}
DynamicArrayNumbers *LinkedListToDynamicArrayNumbers(LinkedListNumbers *ll){
  DynamicArrayNumbers *da;
  double i;
  LinkedListNodeNumbers *node;

  node = ll->first;

  da = (DynamicArrayNumbers *)malloc(sizeof(DynamicArrayNumbers));
  da->length = LinkedListNumbersLength(ll);

  da->array = (double*)malloc(sizeof(double) * (da->length));
  da->arrayLength = da->length;

  for(i = 0.0; i < da->length; i = i + 1.0){
    da->array[(int)(i)] = node->value;
    node = node->next;
  }

  return da;
}
wchar_t *AddCharacter(size_t *returnArrayLength, wchar_t *list, size_t listLength, wchar_t a){
  wchar_t *newlist;
  size_t newlistLength;
  double i;

  newlist = (wchar_t*)malloc(sizeof(wchar_t) * (listLength + 1.0));
  newlistLength = listLength + 1.0;
  for(i = 0.0; i < listLength; i = i + 1.0){
    newlist[(int)(i)] = list[(int)(i)];
  }
  newlist[(int)(listLength)] = a;
		
  free(list);
		
  *returnArrayLength = newlistLength;
  return newlist;
}
void AddCharacterRef(StringReference *list, wchar_t i){
  list->string = AddCharacter(&list->stringLength, list->string, list->stringLength, i);
}
wchar_t *RemoveCharacter(size_t *returnArrayLength, wchar_t *list, size_t listLength, double n){
  wchar_t *newlist;
  size_t newlistLength;
  double i;

  newlist = (wchar_t*)malloc(sizeof(wchar_t) * (listLength - 1.0));
  newlistLength = listLength - 1.0;

  if(n >= 0.0 && n < listLength){
    for(i = 0.0; i < listLength; i = i + 1.0){
      if(i < n){
        newlist[(int)(i)] = list[(int)(i)];
      }
      if(i > n){
        newlist[(int)(i - 1.0)] = list[(int)(i)];
      }
    }

    free(list);
  }else{
    free(newlist);
  }

  *returnArrayLength = newlistLength;
  return newlist;
}
wchar_t GetCharacterRef(StringReference *list, double i){
  return list->string[(int)(i)];
}
void RemoveCharacterRef(StringReference *list, double i){
  list->string = RemoveCharacter(&list->stringLength, list->string, list->stringLength, i);
}
wchar_t charToLowerCase(wchar_t character){
  wchar_t toReturn;

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
wchar_t charToUpperCase(wchar_t character){
  wchar_t toReturn;

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
_Bool charIsUpperCase(wchar_t character){
  _Bool isUpper;

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
_Bool charIsLowerCase(wchar_t character){
  _Bool isLower;

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
_Bool charIsLetter(wchar_t character){
  return charIsUpperCase(character) || charIsLowerCase(character);
}
_Bool charIsNumber(wchar_t character){
  _Bool isNumberx;

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
_Bool charIsWhiteSpace(wchar_t character){
  _Bool isWhiteSpacex;

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
_Bool charIsSymbol(wchar_t character){
  _Bool isSymbolx;

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
_Bool charCharacterIsBefore(wchar_t a, wchar_t b){
  double ad, bd;

  ad = a;
  bd = b;

  return ad < bd;
}
double And4Byte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFFFFFFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFFFFFFFUL){
      return (unsigned long)n1 & (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double And2Byte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFFFUL){
      return (unsigned long)n1 & (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double AndByte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFUL){
      return (unsigned long)n1 & (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double AndBytes(double n1, double n2, double bytes){
  double byteVal, result, i;

  byteVal = 1.0;
  result = 0.0;

  if(n1 >= 0.0 && n1 < pow(2.0, bytes*8.0) && n2 >= 0.0 && n2 < pow(2.0, bytes*8.0)){
    n1 = Truncate(n1);
    n2 = Truncate(n2);
    bytes = Truncate(bytes);

    for(i = 0.0; i < bytes*8.0; i = i + 1.0){
      if(fmod(n1, 2.0) == 1.0 && fmod(n2, 2.0) == 1.0){
        result = result + byteVal;
      }
      n1 = floor(n1/2.0);
      n2 = floor(n2/2.0);
      byteVal = byteVal*2.0;
    }
  }

  return result;
}
double Or4Byte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFFFFFFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFFFFFFFUL){
      return (unsigned long)n1 | (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double Or2Byte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFFFUL){
      return (unsigned long)n1 | (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double OrByte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFUL){
      return (unsigned long)n1 | (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double OrBytes(double n1, double n2, double bytes){
  double byteVal, result, i;

  byteVal = 1.0;
  result = 0.0;

  if(n1 >= 0.0 && n1 < pow(2.0, bytes*8.0) && n2 >= 0.0 && n2 < pow(2.0, bytes*8.0)){
    n1 = Truncate(n1);
    n2 = Truncate(n2);
    bytes = Truncate(bytes);

    for(i = 0.0; i < bytes*8.0; i = i + 1.0){
      if(fmod(n1, 2.0) == 1.0 || fmod(n2, 2.0) == 1.0){
        result = result + byteVal;
      }
      n1 = floor(n1/2.0);
      n2 = floor(n2/2.0);
      byteVal = byteVal*2.0;
    }
  }

  return result;
}
double Xor4Byte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFFFFFFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFFFFFFFUL){
      return (unsigned long)n1 ^ (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double Xor2Byte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFFFUL){
      return (unsigned long)n1 ^ (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double XorByte(double n1, double n2){
    if((double)n1 >= 0.0 && (double)n1 <= (double)0xFFUL && (double)n2 >= 0.0 && (double)n2 <= (double)0xFFUL){
      return (unsigned long)n1 ^ (unsigned long)n2;
    }else{
      return 0.0;
    }
}
double XorBytes(double n1, double n2, double bytes){
  double byteVal, result, i;

  byteVal = 1.0;
  result = 0.0;

  if(n1 >= 0.0 && n1 < pow(2.0, bytes*8.0) && n2 >= 0.0 && n2 < pow(2.0, bytes*8.0)){
    n1 = Truncate(n1);
    n2 = Truncate(n2);
    bytes = Truncate(bytes);

    for(i = 0.0; i < bytes*8.0; i = i + 1.0){
      if(fmod(n1, 2.0) != fmod(n2, 2.0)){
        result = result + byteVal;
      }
      n1 = floor(n1/2.0);
      n2 = floor(n2/2.0);
      byteVal = byteVal*2.0;
    }
  }

  return result;
}
double Not4Byte(double b){
    if((double)b >= 0.0 && (double)b <= (double)0xFFFFFFFFUL){
      return ~(unsigned long)b & 0xFFFFFFFFUL;
    }else{
      return 0.0;
    }
}
double Not2Byte(double b){
    if((double)b >= 0.0 && (double)b <= (double)0xFFFFUL){
      return ~(unsigned long)b & 0xFFFFUL;
    }else{
      return 0.0;
    }
}
double NotByte(double b){
    if((double)b >= 0.0 && (double)b <= (double)0xFFUL){
      return ~(unsigned long)b & 0xFFUL;
    }else{
      return 0.0;
    }
}
double NotBytes(double b, double length){
  double result;

  result = 0.0;

  if(b >= 0.0 && b < pow(2.0, length*8.0)){
    b = Truncate(b);
    length = Truncate(length);

    result = pow(2.0, length*8.0) - b - 1.0;
  }

  return result;
}
double ShiftLeft4Byte(double b, double amount){
    if((double)b >= 0.0 && (double)b <= (double)0xFFFFFFFF && (double)amount >= 0.0 && (double)amount < (double)32){
      return (unsigned long)b << (unsigned long)amount;
    }else{
      return 0.0;
    }
}
double ShiftLeft2Byte(double b, double amount){
    if((double)b >= 0.0 && (double)b <= (double)0xFFFF && (double)amount >= 0.0 && (double)amount < (double)16){
      return (unsigned long)b << (unsigned long)amount;
    }else{
      return 0.0;
    }
}
double ShiftLeftByte(double b, double amount){
    if((double)b >= 0.0 && (double)b <= (double)0xFF && (double)amount >= 0.0 && (double)amount < (double)8){
      return (unsigned long)b << (unsigned long)amount;
    }else{
      return 0.0;
    }
}
double ShiftLeftBytes(double b, double amount, double length){
  double result;

  result = 0.0;

  if(b >= 0.0 && b < pow(2.0, length*8.0) && amount >= 0.0 && amount <= length*8.0){
    b = Truncate(b);
    amount = Truncate(amount);

    result = b*pow(2.0, amount);
  }

  return result;
}
double ShiftRight4Byte(double b, double amount){
    if((double)b >= 0.0 && (double)b <= (double)0xFFFFFFFF && (double)amount >= 0.0 && (double)amount < (double)32){
      return (unsigned long)b >> (unsigned long)amount;
    }else{
      return 0.0;
    }
}
double ShiftRight2Byte(double b, double amount){
    if((double)b >= 0.0 && (double)b <= (double)0xFFFF && (double)amount >= 0.0 && (double)amount < (double)16){
      return (unsigned long)b >> (unsigned long)amount;
    }else{
      return 0.0;
    }
}
double ShiftRightByte(double b, double amount){
    if((double)b >= 0.0 && (double)b <= (double)0xFF && (double)amount >= 0.0 && (double)amount < (double)8){
      return (unsigned long)b >> (unsigned long)amount;
    }else{
      return 0.0;
    }
}
double ShiftRightBytes(double b, double amount, double length){
  double result;

  result = 0.0;

  if(b >= 0.0 && b < pow(2.0, length*8.0) && amount >= 0.0 && amount <= length*8.0){
    b = Truncate(b);
    amount = Truncate(amount);

    result = Truncate(b/pow(2.0, amount));
  }

  return result;
}
double ReadNextBit(double *data, size_t dataLength, NumberReference *nextbit){
  double bytenr, bitnumber, bit, b;

  bytenr = floor(nextbit->numberValue/8.0);
  bitnumber = fmod(nextbit->numberValue, 8.0);

  b = data[(int)(bytenr)];

  bit = fmod(floor(b/pow(2.0, bitnumber)), 2.0);

  nextbit->numberValue = nextbit->numberValue + 1.0;

  return bit;
}
double BitExtract(double b, double fromInc, double toInc){
  return fmod(floor(b/pow(2.0, fromInc)), pow(2.0, toInc + 1.0 - fromInc));
}
double ReadBitRange(double *data, size_t dataLength, NumberReference *nextbit, double length){
  double startbyte, endbyte;
  double startbit, endbit;
  double number, i;

  number = 0.0;

  startbyte = floor(nextbit->numberValue/8.0);
  endbyte = floor((nextbit->numberValue + length)/8.0);

  startbit = fmod(nextbit->numberValue, 8.0);
  endbit = fmod(nextbit->numberValue + length - 1.0, 8.0);

  if(startbyte == endbyte){
    number = BitExtract(data[(int)(startbyte)], startbit, endbit);
  }

  nextbit->numberValue = nextbit->numberValue + length;

  return number;
}
void SkipToBoundary(NumberReference *nextbit){
  double skip;

  skip = 8.0 - fmod(nextbit->numberValue, 8.0);
  nextbit->numberValue = nextbit->numberValue + skip;
}
double ReadNextByteBoundary(double *data, size_t dataLength, NumberReference *nextbit){
  double bytenr, b;

  bytenr = floor(nextbit->numberValue/8.0);
  b = data[(int)(bytenr)];
  nextbit->numberValue = nextbit->numberValue + 8.0;

  return b;
}
double Read2bytesByteBoundary(double *data, size_t dataLength, NumberReference *nextbit){
  double r;

  r = 0.0;
  r = r + pow(2.0, 8.0)*ReadNextByteBoundary(data, dataLength, nextbit);
  r = r + ReadNextByteBoundary(data, dataLength, nextbit);

  return r;
}
double ComputeAdler32(double *data, size_t dataLength){
  double a, b, m, i;

  a = 1.0;
  b = 0.0;
  m = 65521.0;

  for(i = 0.0; i < dataLength; i = i + 1.0){
    a = fmod(a + data[(int)(i)], m);
    b = fmod(b + a, m);
  }

  return b*pow(2.0, 16.0) + a;
}
double *DeflateDataStaticHuffman(size_t *returnArrayLength, double *data, size_t dataLength, double level){
  double *bytes;
  size_t bytesLength;
  NumberReference *currentBit;
  double i;
  NumberArrayReference *copy;
  NumberReference *code, *length, *compressedCode, *lengthAdditionLength, *distanceCode;
  NumberReference *distanceReference, *lengthReference, *lengthAddition;
  NumberReference *distanceAdditionReference, *distanceAdditionLengthReference;
  double *bitReverseLookupTable;
  size_t bitReverseLookupTableLength;
  BooleanReference *match;

  code = CreateNumberReference(0.0);
  length = CreateNumberReference(0.0);
  compressedCode = CreateNumberReference(0.0);
  lengthAdditionLength = CreateNumberReference(0.0);
  distanceCode = CreateNumberReference(0.0);
  distanceReference = CreateNumberReference(0.0);
  lengthReference = CreateNumberReference(0.0);
  lengthAddition = CreateNumberReference(0.0);
  distanceAdditionReference = CreateNumberReference(0.0);
  distanceAdditionLengthReference = CreateNumberReference(0.0);
  match = (BooleanReference *)malloc(sizeof(BooleanReference));

  bytes = (double*)malloc(sizeof(double) * (fmax(dataLength*2.0, 100.0)));
  bytesLength = fmax(dataLength*2.0, 100.0);
  aFillNumberArray(bytes, bytesLength, 0.0);
  currentBit = CreateNumberReference(0.0);

  bitReverseLookupTable = GenerateBitReverseLookupTable(&bitReverseLookupTableLength, 9.0);

  /* Final block */
  AppendBitsToBytesRight(bytes, bytesLength, currentBit, 1.0, 1.0);
  /* Fixed code */
  AppendBitsToBytesRight(bytes, bytesLength, currentBit, 1.0, 2.0);

  for(i = 0.0; i < dataLength; ){
    FindMatch(data, dataLength, i, distanceReference, lengthReference, match, level);

    if(match->booleanValue){
      GetDeflateLengthCode(lengthReference->numberValue, compressedCode, lengthAddition, lengthAdditionLength);
      GetDeflateDistanceCode(distanceReference->numberValue, distanceCode, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable, bitReverseLookupTableLength);
    }

    if( !match->booleanValue ){
      GetDeflateStaticHuffmanCode(data[(int)(i)], code, length, bitReverseLookupTable, bitReverseLookupTableLength);
      AppendBitsToBytesRight(bytes, bytesLength, currentBit, code->numberValue, length->numberValue);
      i = i + 1.0;
    }else{
      GetDeflateStaticHuffmanCode(compressedCode->numberValue, code, length, bitReverseLookupTable, bitReverseLookupTableLength);
      AppendBitsToBytesRight(bytes, bytesLength, currentBit, code->numberValue, length->numberValue);
      AppendBitsToBytesRight(bytes, bytesLength, currentBit, lengthAddition->numberValue, lengthAdditionLength->numberValue);
      AppendBitsToBytesRight(bytes, bytesLength, currentBit, distanceCode->numberValue, 5.0);
      AppendBitsToBytesRight(bytes, bytesLength, currentBit, distanceAdditionReference->numberValue, distanceAdditionLengthReference->numberValue);
      i = i + lengthReference->numberValue;
    }
  }

  /* Stop symbol */
  GetDeflateStaticHuffmanCode(256.0, code, length, bitReverseLookupTable, bitReverseLookupTableLength);
  AppendBitsToBytesRight(bytes, bytesLength, currentBit, code->numberValue, length->numberValue);

  copy = (NumberArrayReference *)malloc(sizeof(NumberArrayReference));
  aCopyNumberArrayRange(bytes, bytesLength, 0.0, ceil(currentBit->numberValue/8.0), copy);
  free(bytes);
  bytes = copy->numberArray;
  bytesLength = copy->numberArrayLength;

  *returnArrayLength = bytesLength;
  return bytes;
}
void FindMatch(double *data, size_t dataLength, double pos, NumberReference *distanceReference, NumberReference *lengthReference, BooleanReference *match, double level){
  double i, j;
  double deflateMinMength, deflateMaxLength, deflateMaxDistance;
  double longest, maxLength, distanceForMax;
  double startDistance, matchLength;
  _Bool done;

  deflateMinMength = 3.0;
  deflateMaxLength = 258.0;

  longest = fmin(pos - 1.0, deflateMaxLength);
  longest = fmin(dataLength - pos, longest);

  deflateMaxDistance = floor(32768.0/10.0*level);

  startDistance = fmin(pos, deflateMaxDistance);

  if(longest >= deflateMinMength){
    maxLength = 0.0;
    distanceForMax = 0.0;

    for(i = pos - 1.0; i >= pos - startDistance && maxLength != longest; i = i - 1.0){
      matchLength = 0.0;
      done = false;
      for(j = 0.0; j < longest &&  !done ; j = j + 1.0){
        if(data[(int)(i + j)] == data[(int)(pos + j)]){
          matchLength = matchLength + 1.0;
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
      match->booleanValue = true;
      lengthReference->numberValue = maxLength;
      distanceReference->numberValue = distanceForMax;
    }else{
      match->booleanValue = false;
    }
  }else{
    match->booleanValue = false;
  }
}
double *GenerateBitReverseLookupTable(size_t *returnArrayLength, double bits){
  double *table;
  size_t tableLength;
  double i;

  table = (double*)malloc(sizeof(double) * (pow(2.0, bits)));
  tableLength = pow(2.0, bits);

  for(i = 0.0; i < tableLength; i = i + 1.0){
    table[(int)(i)] = ReverseBits(i, 32.0);
  }

  *returnArrayLength = tableLength;
  return table;
}
double ReverseBits(double x, double bits){
  double b, bit, i;

  b = 0.0;

  for(i = 0.0; i < bits; i = i + 1.0){
    b = ShiftLeft4Byte(b, 1.0);
    bit = And4Byte(x, 1.0);
    b = Or4Byte(b, bit);
    x = ShiftRight4Byte(x, 1.0);
  }

  return b;
}
double *DeflateDataNoCompression(size_t *returnArrayLength, double *data, size_t dataLength){
  double *deflated;
  size_t deflatedLength;
  NumberReference *position;
  double block, i, blocks, blocklength, maxblocksize;

  maxblocksize = pow(2.0, 16.0) - 1.0;
  blocks = ceil(dataLength/maxblocksize);

  position = CreateNumberReference(0.0);

  deflated = (double*)malloc(sizeof(double) * ((1.0 + 4.0)*blocks + dataLength));
  deflatedLength = (1.0 + 4.0)*blocks + dataLength;

  for(block = 0.0; block < blocks; block = block + 1.0){
    if(block + 1.0 == blocks){
      WriteByte(deflated, deflatedLength, 1.0, position);
    }else{
      WriteByte(deflated, deflatedLength, 0.0, position);
    }
    blocklength = fmin(dataLength - block*maxblocksize, maxblocksize);
    Write2BytesLE(deflated, deflatedLength, blocklength, position);
    Write2BytesLE(deflated, deflatedLength, Not2Byte(blocklength), position);

    for(i = 0.0; i < blocklength; i = i + 1.0){
      WriteByte(deflated, deflatedLength, data[(int)(block*maxblocksize + i)], position);
    }
  }

  *returnArrayLength = deflatedLength;
  return deflated;
}
void GetDeflateStaticHuffmanCode(double b, NumberReference *code, NumberReference *length, double *bitReverseLookupTable, size_t bitReverseLookupTableLength){
  double reversed;

  if(b >= 0.0 && b <= 143.0){
    code->numberValue = 48.0 + b;
    length->numberValue = 8.0;
  }else if(b >= 144.0 && b <= 255.0){
    code->numberValue = b - 144.0 + 400.0;
    length->numberValue = 9.0;
  }else if(b >= 256.0 && b <= 279.0){
    code->numberValue = b - 256.0 + 0.0;
    length->numberValue = 7.0;
  }else if(b >= 280.0 && b <= 287.0){
    code->numberValue = b - 280.0 + 192.0;
    length->numberValue = 8.0;
  }

  reversed = bitReverseLookupTable[(int)(code->numberValue)];
  code->numberValue = ShiftRight4Byte(reversed, 32.0 - length->numberValue);
}
void GetDeflateLengthCode(double length, NumberReference *code, NumberReference *lengthAddition, NumberReference *lengthAdditionLength){
  if(length >= 3.0 && length <= 10.0){
    code->numberValue = 257.0 + length - 3.0;
    lengthAdditionLength->numberValue = 0.0;
  }else if(length >= 11.0 && length <= 18.0){
    code->numberValue = 265.0 + floor((length - 11.0)/2.0);
    lengthAddition->numberValue = floor(fmod(length - 11.0, 2.0));
    lengthAdditionLength->numberValue = 1.0;
  }else if(length >= 19.0 && length <= 34.0){
    code->numberValue = 269.0 + floor((length - 19.0)/4.0);
    lengthAddition->numberValue = floor(fmod(length - 19.0, 4.0));
    lengthAdditionLength->numberValue = 2.0;
  }else if(length >= 35.0 && length <= 66.0){
    code->numberValue = 273.0 + floor((length - 35.0)/8.0);
    lengthAddition->numberValue = floor(fmod(length - 35.0, 8.0));
    lengthAdditionLength->numberValue = 3.0;
  }else if(length >= 67.0 && length <= 130.0){
    code->numberValue = 277.0 + floor((length - 67.0)/16.0);
    lengthAddition->numberValue = floor(fmod(length - 67.0, 16.0));
    lengthAdditionLength->numberValue = 4.0;
  }else if(length >= 131.0 && length <= 257.0){
    code->numberValue = 281.0 + floor((length - 131.0)/32.0);
    lengthAddition->numberValue = floor(fmod(length - 131.0, 32.0));
    lengthAdditionLength->numberValue = 5.0;
  }else if(length == 258.0){
    code->numberValue = 285.0;
    lengthAdditionLength->numberValue = 0.0;
  }
}
void GetDeflateDistanceCode(double distance, NumberReference *code, NumberReference *distanceAdditionReference, NumberReference *distanceAdditionLengthReference, double *bitReverseLookupTable, size_t bitReverseLookupTableLength){
  double reversed;

  if(distance >= 1.0 && distance <= 4.0){
    code->numberValue = distance - 1.0;
    distanceAdditionLengthReference->numberValue = 0.0;
  }else if(distance >= 5.0 && distance <= 8.0){
    code->numberValue = 4.0 + floor((distance - 5.0)/2.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 5.0, 2.0));
    distanceAdditionLengthReference->numberValue = 1.0;
  }else if(distance >= 9.0 && distance <= 16.0){
    code->numberValue = 6.0 + floor((distance - 9.0)/4.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 9.0, 4.0));
    distanceAdditionLengthReference->numberValue = 2.0;
  }else if(distance >= 17.0 && distance <= 32.0){
    code->numberValue = 8.0 + floor((distance - 17.0)/8.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 17.0, 8.0));
    distanceAdditionLengthReference->numberValue = 3.0;
  }else if(distance >= 33.0 && distance <= 64.0){
    code->numberValue = 10.0 + floor((distance - 33.0)/16.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 33.0, 16.0));
    distanceAdditionLengthReference->numberValue = 4.0;
  }else if(distance >= 65.0 && distance <= 128.0){
    code->numberValue = 12.0 + floor((distance - 65.0)/32.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 65.0, 32.0));
    distanceAdditionLengthReference->numberValue = 5.0;
  }else if(distance >= 129.0 && distance <= 256.0){
    code->numberValue = 14.0 + floor((distance - 129.0)/64.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 129.0, 64.0));
    distanceAdditionLengthReference->numberValue = 6.0;
  }else if(distance >= 257.0 && distance <= 512.0){
    code->numberValue = 16.0 + floor((distance - 257.0)/128.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 257.0, 128.0));
    distanceAdditionLengthReference->numberValue = 7.0;
  }else if(distance >= 513.0 && distance <= 1024.0){
    code->numberValue = 18.0 + floor((distance - 513.0)/256.0);
    distanceAdditionReference->numberValue = floor(fmod(distance - 513.0, 256.0));
    distanceAdditionLengthReference->numberValue = 8.0;
  }else if(distance >= 1025.0 && distance <= 2048.0){
    code->numberValue = 20.0 + floor((distance - 1025.0)/pow(2.0, 9.0));
    distanceAdditionReference->numberValue = floor(fmod(distance - 1025.0, pow(2.0, 9.0)));
    distanceAdditionLengthReference->numberValue = 9.0;
  }else if(distance >= 2049.0 && distance <= 4096.0){
    code->numberValue = 22.0 + floor((distance - 2049.0)/pow(2.0, 10.0));
    distanceAdditionReference->numberValue = floor(fmod(distance - 2049.0, pow(2.0, 10.0)));
    distanceAdditionLengthReference->numberValue = 10.0;
  }else if(distance >= 4097.0 && distance <= 8192.0){
    code->numberValue = 24.0 + floor((distance - 4097.0)/pow(2.0, 11.0));
    distanceAdditionReference->numberValue = floor(fmod(distance - 4097.0, pow(2.0, 11.0)));
    distanceAdditionLengthReference->numberValue = 11.0;
  }else if(distance >= 8193.0 && distance <= 16384.0){
    code->numberValue = 26.0 + floor((distance - 8193.0)/pow(2.0, 12.0));
    distanceAdditionReference->numberValue = floor(fmod(distance - 8193.0, pow(2.0, 12.0)));
    distanceAdditionLengthReference->numberValue = 12.0;
  }else if(distance >= 16385.0 && distance <= 32768.0){
    code->numberValue = 28.0 + floor((distance - 16385.0)/pow(2.0, 13.0));
    distanceAdditionReference->numberValue = floor(fmod(distance - 16385.0, pow(2.0, 13.0)));
    distanceAdditionLengthReference->numberValue = 13.0;
  }

  reversed = bitReverseLookupTable[(int)(code->numberValue)];
  code->numberValue = ShiftRight4Byte(reversed, 32.0 - 5.0);
}
void AppendBitsToBytesLeft(double *bytes, size_t bytesLength, NumberReference *nextbit, double data, double length){
  double bytePos, bitPos, segment, part, remove;

  for(; length > 0.0; ){
    bytePos = Truncate(nextbit->numberValue/8.0);
    bitPos = fmod(nextbit->numberValue, 8.0);

    if(length < 8.0 - bitPos){
      part = ShiftLeft4Byte(data, 8.0 - bitPos - length);

      bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);

      nextbit->numberValue = nextbit->numberValue + length;

      length = 0.0;
    }else{
      segment = 8.0 - bitPos;

      part = ShiftRight4Byte(data, length - segment);
      bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);
      nextbit->numberValue = nextbit->numberValue + segment;

      remove = ShiftLeft4Byte(part, length - segment);
      data = Xor4Byte(data, remove);

      length = length - segment;
    }
  }
}
void AppendBitsToBytesRight(double *bytes, size_t bytesLength, NumberReference *nextbit, double data, double length){
  double bytePos, bitPos, segment, part;
  double mask;

  for(; length > 0.0; ){
    bytePos = Truncate(nextbit->numberValue/8.0);
    bitPos = fmod(nextbit->numberValue, 8.0);

    if(length < 8.0 - bitPos){
      part = ShiftLeft4Byte(data, bitPos);

      bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);

      nextbit->numberValue = nextbit->numberValue + length;

      length = 0.0;
    }else{
      segment = 8.0 - bitPos;

      mask = 1.0;
      mask = ShiftLeft4Byte(mask, segment);
      mask = mask - 1.0;

      part = And4Byte(mask, data);
      part = ShiftLeft4Byte(part, bitPos);
      bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);
      nextbit->numberValue = nextbit->numberValue + segment;

      data = ShiftRight4Byte(data, segment);

      length = length - segment;
    }
  }
}

