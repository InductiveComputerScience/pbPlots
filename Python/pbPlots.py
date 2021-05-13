# Downloaded from https://repo.progsbase.com - Code Developed Using progsbase.

from math import *
class RGBABitmapImageReference:
  image = None

class Rectangle:
  x1 = None
  x2 = None
  y1 = None
  y2 = None

class ScatterPlotSeries:
  linearInterpolation = None
  pointType = None
  lineType = None
  lineThickness = None
  xs = None
  ys = None
  color = None

class ScatterPlotSettings:
  scatterPlotSeries = None
  autoBoundaries = None
  xMax = None
  xMin = None
  yMax = None
  yMin = None
  autoPadding = None
  xPadding = None
  yPadding = None
  xLabel = None
  yLabel = None
  title = None
  showGrid = None
  gridColor = None
  xAxisAuto = None
  xAxisTop = None
  xAxisBottom = None
  yAxisAuto = None
  yAxisLeft = None
  yAxisRight = None
  width = None
  height = None

class BarPlotSeries:
  ys = None
  color = None

class BarPlotSettings:
  width = None
  height = None
  autoBoundaries = None
  yMax = None
  yMin = None
  autoPadding = None
  xPadding = None
  yPadding = None
  title = None
  showGrid = None
  gridColor = None
  barPlotSeries = None
  yLabel = None
  autoColor = None
  grayscaleAutoColor = None
  autoSpacing = None
  groupSeparation = None
  barSeparation = None
  autoLabels = None
  xLabels = None
  barBorder = None

def CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax):

  x1 = x1Ref.numberValue
  y1 = y1Ref.numberValue
  x2 = x2Ref.numberValue
  y2 = y2Ref.numberValue

  p1In = x1 >= xMin and x1 <= xMax and y1 >= yMin and y1 <= yMax
  p2In = x2 >= xMin and x2 <= xMax and y2 >= yMin and y2 <= yMax

  if p1In and p2In:
    success = True
  elif  not p1In  and p2In:
    dx = x1 - x2
    dy = y1 - y2

    if dx != 0.0:
      f1 = (xMin - x2)/dx
      f2 = (xMax - x2)/dx
    else:
      f1 = 1.0
      f2 = 1.0
    if dy != 0.0:
      f3 = (yMin - y2)/dy
      f4 = (yMax - y2)/dy
    else:
      f3 = 1.0
      f4 = 1.0

    if f1 < 0.0:
      f1 = 1.0
    if f2 < 0.0:
      f2 = 1.0
    if f3 < 0.0:
      f3 = 1.0
    if f4 < 0.0:
      f4 = 1.0

    f = min(f1,min(f2,min(f3,f4)))

    x1 = x2 + f*dx
    y1 = y2 + f*dy

    success = True
  elif p1In and  not p2In :
    dx = x2 - x1
    dy = y2 - y1

    if dx != 0.0:
      f1 = (xMin - x1)/dx
      f2 = (xMax - x1)/dx
    else:
      f1 = 1.0
      f2 = 1.0
    if dy != 0.0:
      f3 = (yMin - y1)/dy
      f4 = (yMax - y1)/dy
    else:
      f3 = 1.0
      f4 = 1.0

    if f1 < 0.0:
      f1 = 1.0
    if f2 < 0.0:
      f2 = 1.0
    if f3 < 0.0:
      f3 = 1.0
    if f4 < 0.0:
      f4 = 1.0

    f = min(f1,min(f2,min(f3,f4)))

    x2 = x1 + f*dx
    y2 = y1 + f*dy

    success = True
  else:
    success = False

  x1Ref.numberValue = x1
  y1Ref.numberValue = y1
  x2Ref.numberValue = x2
  y2Ref.numberValue = y2

  return success

def IncrementFromCoordinates(x1, y1, x2, y2):
  return (x2 - x1)/(y2 - y1)

def InterceptFromCoordinates(x1, y1, x2, y2):

  a = IncrementFromCoordinates(x1, y1, x2, y2)
  b = y1 - a*x1

  return b

def Get8HighContrastColors():
  colors =  [None]*int(8.0)
  colors[int(0.0)] = CreateRGBColor(3.0/256.0, 146.0/256.0, 206.0/256.0)
  colors[int(1.0)] = CreateRGBColor(253.0/256.0, 83.0/256.0, 8.0/256.0)
  colors[int(2.0)] = CreateRGBColor(102.0/256.0, 176.0/256.0, 50.0/256.0)
  colors[int(3.0)] = CreateRGBColor(208.0/256.0, 234.0/256.0, 43.0/256.0)
  colors[int(4.0)] = CreateRGBColor(167.0/256.0, 25.0/256.0, 75.0/256.0)
  colors[int(5.0)] = CreateRGBColor(254.0/256.0, 254.0/256.0, 51.0/256.0)
  colors[int(6.0)] = CreateRGBColor(134.0/256.0, 1.0/256.0, 175.0/256.0)
  colors[int(7.0)] = CreateRGBColor(251.0/256.0, 153.0/256.0, 2.0/256.0)
  return colors

def DrawFilledRectangleWithBorder(image, x, y, w, h, borderColor, fillColor):
  if h > 0.0 and w > 0.0:
    DrawFilledRectangle(image, x, y, w, h, fillColor)
    DrawRectangle1px(image, x, y, w, h, borderColor)

def CreateRGBABitmapImageReference():

  reference = RGBABitmapImageReference()
  reference.image = RGBABitmapImage()
  reference.image.x =  [None]*int(0.0)

  return reference

def RectanglesOverlap(r1, r2):

  overlap = False

  overlap = overlap or (r2.x1 >= r1.x1 and r2.x1 <= r1.x2 and r2.y1 >= r1.y1 and r2.y1 <= r1.y2)
  overlap = overlap or (r2.x2 >= r1.x1 and r2.x2 <= r1.x2 and r2.y1 >= r1.y1 and r2.y1 <= r1.y2)
  overlap = overlap or (r2.x1 >= r1.x1 and r2.x1 <= r1.x2 and r2.y2 >= r1.y1 and r2.y2 <= r1.y2)
  overlap = overlap or (r2.x2 >= r1.x1 and r2.x2 <= r1.x2 and r2.y2 >= r1.y1 and r2.y2 <= r1.y2)

  return overlap

def CreateRectangle(x1, y1, x2, y2):
  r = Rectangle()
  r.x1 = x1
  r.y1 = y1
  r.x2 = x2
  r.y2 = y2
  return r

def CopyRectangleValues(rd, rs):
  rd.x1 = rs.x1
  rd.y1 = rs.y1
  rd.x2 = rs.x2
  rd.y2 = rs.y2

def DrawXLabelsForPriority(p, xMin, oy, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied, textOnBottom):

  r = Rectangle()
  padding = 10.0

  overlap = False
  i = 0.0
  while i < len(xLabels.stringArray):
    if xLabelPriorities.numberArray[int(i)] == p:

      x = xGridPositions[int(i)]
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)
      text = xLabels.stringArray[int(i)].string

      r.x1 = floor(px - GetTextWidth(text)/2.0)
      if textOnBottom:
        r.y1 = floor(oy + 5.0)
      else:
        r.y1 = floor(oy - 20.0)
      r.x2 = r.x1 + GetTextWidth(text)
      r.y2 = r.y1 + GetTextHeight(text)

      # Add padding
      r.x1 = r.x1 - padding
      r.y1 = r.y1 - padding
      r.x2 = r.x2 + padding
      r.y2 = r.y2 + padding

      currentOverlaps = False

      j = 0.0
      while j < nextRectangle.numberValue:
        currentOverlaps = currentOverlaps or RectanglesOverlap(r, occupied[int(j)])
        j = j + 1.0
      

      if  not currentOverlaps  and p == 1.0:
        DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor)

        CopyRectangleValues(occupied[int(nextRectangle.numberValue)], r)
        nextRectangle.numberValue = nextRectangle.numberValue + 1.0

      overlap = overlap or currentOverlaps
    i = i + 1.0
  
  if  not overlap  and p != 1.0:
    i = 0.0
    while i < len(xGridPositions):
      x = xGridPositions[int(i)]
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)

      if xLabelPriorities.numberArray[int(i)] == p:
        text = xLabels.stringArray[int(i)].string

        r.x1 = floor(px - GetTextWidth(text)/2.0)
        if textOnBottom:
          r.y1 = floor(oy + 5.0)
        else:
          r.y1 = floor(oy - 20.0)
        r.x2 = r.x1 + GetTextWidth(text)
        r.y2 = r.y1 + GetTextHeight(text)

        DrawText(canvas, r.x1, r.y1, text, gridLabelColor)

        CopyRectangleValues(occupied[int(nextRectangle.numberValue)], r)
        nextRectangle.numberValue = nextRectangle.numberValue + 1.0
      i = i + 1.0
    

def DrawYLabelsForPriority(p, yMin, ox, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, textOnLeft):

  r = Rectangle()
  padding = 10.0

  overlap = False
  i = 0.0
  while i < len(yLabels.stringArray):
    if yLabelPriorities.numberArray[int(i)] == p:

      y = yGridPositions[int(i)]
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
      text = yLabels.stringArray[int(i)].string

      if textOnLeft:
        r.x1 = floor(ox - GetTextWidth(text) - 10.0)
      else:
        r.x1 = floor(ox + 10.0)
      r.y1 = floor(py - 6.0)
      r.x2 = r.x1 + GetTextWidth(text)
      r.y2 = r.y1 + GetTextHeight(text)

      # Add padding
      r.x1 = r.x1 - padding
      r.y1 = r.y1 - padding
      r.x2 = r.x2 + padding
      r.y2 = r.y2 + padding

      currentOverlaps = False

      j = 0.0
      while j < nextRectangle.numberValue:
        currentOverlaps = currentOverlaps or RectanglesOverlap(r, occupied[int(j)])
        j = j + 1.0
      

      # Draw labels with priority 1 if they do not overlap anything else.
      if  not currentOverlaps  and p == 1.0:
        DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor)

        CopyRectangleValues(occupied[int(nextRectangle.numberValue)], r)
        nextRectangle.numberValue = nextRectangle.numberValue + 1.0

      overlap = overlap or currentOverlaps
    i = i + 1.0
  
  if  not overlap  and p != 1.0:
    i = 0.0
    while i < len(yGridPositions):
      y = yGridPositions[int(i)]
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)

      if yLabelPriorities.numberArray[int(i)] == p:
        text = yLabels.stringArray[int(i)].string

        if textOnLeft:
          r.x1 = floor(ox - GetTextWidth(text) - 10.0)
        else:
          r.x1 = floor(ox + 10.0)
        r.y1 = floor(py - 6.0)
        r.x2 = r.x1 + GetTextWidth(text)
        r.y2 = r.y1 + GetTextHeight(text)

        DrawText(canvas, r.x1, r.y1, text, gridLabelColor)

        CopyRectangleValues(occupied[int(nextRectangle.numberValue)], r)
        nextRectangle.numberValue = nextRectangle.numberValue + 1.0
      i = i + 1.0
    

def ComputeGridLinePositions(cMin, cMax, labels, priorities):

  cLength = cMax - cMin

  p = floor(log10(cLength))
  pInterval = 10.0**p
  # gives 10-1 lines for 100-10 diff
  pMin = ceil(cMin/pInterval)*pInterval
  pMax = floor(cMax/pInterval)*pInterval
  pNum = Round((pMax - pMin)/pInterval + 1.0)

  mode = 1.0

  if pNum <= 3.0:
    p = floor(log10(cLength) - 1.0)
    # gives 100-10 lines for 100-10 diff
    pInterval = 10.0**p
    pMin = ceil(cMin/pInterval)*pInterval
    pMax = floor(cMax/pInterval)*pInterval
    pNum = Round((pMax - pMin)/pInterval + 1.0)

    mode = 4.0
  elif pNum <= 6.0:
    p = floor(log10(cLength))
    pInterval = 10.0**p/4.0
    # gives 40-5 lines for 100-10 diff
    pMin = ceil(cMin/pInterval)*pInterval
    pMax = floor(cMax/pInterval)*pInterval
    pNum = Round((pMax - pMin)/pInterval + 1.0)

    mode = 3.0
  elif pNum <= 10.0:
    p = floor(log10(cLength))
    pInterval = 10.0**p/2.0
    # gives 20-3 lines for 100-10 diff
    pMin = ceil(cMin/pInterval)*pInterval
    pMax = floor(cMax/pInterval)*pInterval
    pNum = Round((pMax - pMin)/pInterval + 1.0)

    mode = 2.0

  positions =  [None]*int(pNum)
  labels.stringArray =  [None]*int(pNum)
  priorities.numberArray =  [None]*int(pNum)

  i = 0.0
  while i < pNum:
    num = pMin + pInterval*i
    positions[int(i)] = num

    # Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn.
    priority = 1.0

    # Prioritize x.25, x.5 and x.75 lower.
    if mode == 2.0 or mode == 3.0:
      rem = fabs(round(num/10.0**(p - 2.0))) % 100.0

      priority = 1.0
      if rem == 50.0:
        priority = 2.0
      elif rem == 25.0 or rem == 75.0:
        priority = 3.0

    # Prioritize x.1-x.4 and x.6-x.9 lower
    if mode == 4.0:
      rem = fabs(Round(num/10.0**p)) % 10.0

      priority = 1.0
      if rem == 1.0 or rem == 2.0 or rem == 3.0 or rem == 4.0 or rem == 6.0 or rem == 7.0 or rem == 8.0 or rem == 9.0:
        priority = 2.0

    # 0 has lowest priority.
    if EpsilonCompare(num, 0.0, 10.0**(p - 5.0)):
      priority = 3.0

    priorities.numberArray[int(i)] = priority

    # The label itself.
    labels.stringArray[int(i)] = StringReference()
    if p < 0.0:
      if mode == 2.0 or mode == 3.0:
        num = RoundToDigits(num,  -(p - 1.0))
      else:
        num = RoundToDigits(num,  -p)
    labels.stringArray[int(i)].string = CreateStringDecimalFromNumber(num)
    i = i + 1.0
  

  return positions

def MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax):

  yLength = yMax - yMin
  yPixelLength = yPixelMax - yPixelMin

  y = y - yMin
  y = y*yPixelLength/yLength
  y = yPixelLength - y
  y = y + yPixelMin
  return y

def MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax):

  xLength = xMax - xMin
  xPixelLength = xPixelMax - xPixelMin

  x = x - xMin
  x = x*xPixelLength/xLength
  x = x + xPixelMin
  return x

def MapXCoordinateAutoSettings(x, image, xs):
  return MapXCoordinate(x, GetMinimum(xs), GetMaximum(xs), GetDefaultPaddingPercentage()*ImageWidth(image), (1.0 - GetDefaultPaddingPercentage())*ImageWidth(image))

def MapYCoordinateAutoSettings(y, image, ys):
  return MapYCoordinate(y, GetMinimum(ys), GetMaximum(ys), GetDefaultPaddingPercentage()*ImageHeight(image), (1.0 - GetDefaultPaddingPercentage())*ImageHeight(image))

def MapXCoordinateBasedOnSettings(x, settings):

  boundaries = Rectangle()
  ComputeBoundariesBasedOnSettings(settings, boundaries)
  xMin = boundaries.x1
  xMax = boundaries.x2

  if settings.autoPadding:
    xPadding = floor(GetDefaultPaddingPercentage()*settings.width)
  else:
    xPadding = settings.xPadding

  xPixelMin = xPadding
  xPixelMax = settings.width - xPadding

  return MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)

def MapYCoordinateBasedOnSettings(y, settings):

  boundaries = Rectangle()
  ComputeBoundariesBasedOnSettings(settings, boundaries)
  yMin = boundaries.y1
  yMax = boundaries.y2

  if settings.autoPadding:
    yPadding = floor(GetDefaultPaddingPercentage()*settings.height)
  else:
    yPadding = settings.yPadding

  yPixelMin = yPadding
  yPixelMax = settings.height - yPadding

  return MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)

def GetDefaultPaddingPercentage():
  return 0.10

def DrawText(canvas, x, y, text, color):

  charWidth = 8.0
  spacing = 2.0

  i = 0.0
  while i < len(text):
    DrawAsciiCharacter(canvas, x + i*(charWidth + spacing), y, text[int(i)], color)
    i = i + 1.0
  

def DrawTextUpwards(canvas, x, y, text, color):

  buffer = CreateImage(GetTextWidth(text), GetTextHeight(text), GetTransparent())
  DrawText(buffer, 0.0, 0.0, text, color)
  rotated = RotateAntiClockwise90Degrees(buffer)
  DrawImageOnImage(canvas, rotated, x, y)
  DeleteImage(buffer)
  DeleteImage(rotated)

def GetDefaultScatterPlotSettings():

  settings = ScatterPlotSettings()

  settings.autoBoundaries = True
  settings.xMax = 0.0
  settings.xMin = 0.0
  settings.yMax = 0.0
  settings.yMin = 0.0
  settings.autoPadding = True
  settings.xPadding = 0.0
  settings.yPadding = 0.0
  settings.title = ""
  settings.xLabel = ""
  settings.yLabel = ""
  settings.scatterPlotSeries =  [None]*int(0.0)
  settings.showGrid = True
  settings.gridColor = GetGray(0.1)
  settings.xAxisAuto = True
  settings.xAxisTop = False
  settings.xAxisBottom = False
  settings.yAxisAuto = True
  settings.yAxisLeft = False
  settings.yAxisRight = False

  return settings

def GetDefaultScatterPlotSeriesSettings():

  series = ScatterPlotSeries()

  series.linearInterpolation = True
  series.pointType = "pixels"
  series.lineType = "solid"
  series.lineThickness = 1.0
  series.xs =  [None]*int(0.0)
  series.ys =  [None]*int(0.0)
  series.color = GetBlack()

  return series

def DrawScatterPlot(canvasReference, width, height, xs, ys):

  settings = GetDefaultScatterPlotSettings()

  settings.width = width
  settings.height = height
  settings.scatterPlotSeries =  [None]*int(1.0)
  settings.scatterPlotSeries[int(0.0)] = GetDefaultScatterPlotSeriesSettings()
  settings.scatterPlotSeries[int(0.0)].xs = None
  settings.scatterPlotSeries[int(0.0)].xs = xs
  settings.scatterPlotSeries[int(0.0)].ys = None
  settings.scatterPlotSeries[int(0.0)].ys = ys

  DrawScatterPlotFromSettings(canvasReference, settings)

def DrawScatterPlotFromSettings(canvasReference, settings):

  canvas = CreateImage(settings.width, settings.height, GetWhite())
  patternOffset = CreateNumberReference(0.0)

  success = ScatterPlotFromSettingsValid(settings)

  if success:

    boundaries = Rectangle()
    ComputeBoundariesBasedOnSettings(settings, boundaries)
    xMin = boundaries.x1
    yMin = boundaries.y1
    xMax = boundaries.x2
    yMax = boundaries.y2

    # If zero, set to defaults.
    if(xMin - xMax == 0):
      xMin = 0
      xMax = 10

    if(yMin - yMax == 0):
      yMin = 0
      yMax = 10

    xLength = xMax - xMin
    yLength = yMax - yMin

    if settings.autoPadding:
      xPadding = floor(GetDefaultPaddingPercentage()*settings.width)
      yPadding = floor(GetDefaultPaddingPercentage()*settings.height)
    else:
      xPadding = settings.xPadding
      yPadding = settings.yPadding

    # Draw title
    DrawText(canvas, floor(settings.width/2.0 - GetTextWidth(settings.title)/2.0), floor(yPadding/3.0), settings.title, GetBlack())

    # Draw grid
    xPixelMin = xPadding
    yPixelMin = yPadding
    xPixelMax = settings.width - xPadding
    yPixelMax = settings.height - yPadding
    xLengthPixels = xPixelMax - xPixelMin
    yLengthPixels = yPixelMax - yPixelMin
    DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor)

    gridLabelColor = GetGray(0.5)

    xLabels = StringArrayReference()
    xLabelPriorities = NumberArrayReference()
    yLabels = StringArrayReference()
    yLabelPriorities = NumberArrayReference()
    xGridPositions = ComputeGridLinePositions(xMin, xMax, xLabels, xLabelPriorities)
    yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities)

    if settings.showGrid:
      # X-grid
      i = 0.0
      while i < len(xGridPositions):
        x = xGridPositions[int(i)]
        px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)
        DrawLine1px(canvas, px, yPixelMin, px, yPixelMax, settings.gridColor)
        i = i + 1.0
      

      # Y-grid
      i = 0.0
      while i < len(yGridPositions):
        y = yGridPositions[int(i)]
        py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
        DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor)
        i = i + 1.0
      

    # Compute origin information.
    originYInside = yMin < 0.0 and yMax > 0.0
    originY = 0.0
    if settings.xAxisAuto:
      if originYInside:
        originY = 0.0
      else:
        originY = yMin
    else:
      if settings.xAxisTop:
        originY = yMax
      if settings.xAxisBottom:
        originY = yMin
    originYPixels = MapYCoordinate(originY, yMin, yMax, yPixelMin, yPixelMax)

    originXInside = xMin < 0.0 and xMax > 0.0
    originX = 0.0
    if settings.yAxisAuto:
      if originXInside:
        originX = 0.0
      else:
        originX = xMin
    else:
      if settings.yAxisLeft:
        originX = xMin
      if settings.yAxisRight:
        originX = xMax
    originXPixels = MapXCoordinate(originX, xMin, xMax, xPixelMin, xPixelMax)

    if originYInside:
      originTextY = 0.0
    else:
      originTextY = yMin + yLength/2.0
    originTextYPixels = MapYCoordinate(originTextY, yMin, yMax, yPixelMin, yPixelMax)

    if originXInside:
      originTextX = 0.0
    else:
      originTextX = xMin + xLength/2.0
    originTextXPixels = MapXCoordinate(originTextX, xMin, xMax, xPixelMin, xPixelMax)

    # Labels
    occupied =  [None]*int(len(xLabels.stringArray) + len(yLabels.stringArray))
    i = 0.0
    while i < len(occupied):
      occupied[int(i)] = CreateRectangle(0.0, 0.0, 0.0, 0.0)
      i = i + 1.0
    
    nextRectangle = CreateNumberReference(0.0)

    # x labels
    i = 1.0
    while i <= 5.0:
      textOnBottom = True
      if  not settings.xAxisAuto  and settings.xAxisTop:
        textOnBottom = False
      DrawXLabelsForPriority(i, xMin, originYPixels, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied, textOnBottom)
      i = i + 1.0
    

    # y labels
    i = 1.0
    while i <= 5.0:
      textOnLeft = True
      if  not settings.yAxisAuto  and settings.yAxisRight:
        textOnLeft = False
      DrawYLabelsForPriority(i, yMin, originXPixels, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, textOnLeft)
      i = i + 1.0
    

    # Draw origin line axis titles.
    axisLabelPadding = 20.0

    # x origin line
    if originYInside:
      DrawLine1px(canvas, Round(xPixelMin), Round(originYPixels), Round(xPixelMax), Round(originYPixels), GetBlack())

    # y origin line
    if originXInside:
      DrawLine1px(canvas, Round(originXPixels), Round(yPixelMin), Round(originXPixels), Round(yPixelMax), GetBlack())

    # Draw origin axis titles.
    DrawTextUpwards(canvas, 10.0, floor(originTextYPixels - GetTextWidth(settings.yLabel)/2.0), settings.yLabel, GetBlack())
    DrawText(canvas, floor(originTextXPixels - GetTextWidth(settings.xLabel)/2.0), yPixelMax + axisLabelPadding, settings.xLabel, GetBlack())

    # X-grid-markers
    i = 0.0
    while i < len(xGridPositions):
      x = xGridPositions[int(i)]
      px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)
      p = xLabelPriorities.numberArray[int(i)]
      l = 1.0
      if p == 1.0:
        l = 8.0
      elif p == 2.0:
        l = 3.0
      side =  -1.0
      if  not settings.xAxisAuto  and settings.xAxisTop:
        side = 1.0
      DrawLine1px(canvas, px, originYPixels, px, originYPixels + side*l, GetBlack())
      i = i + 1.0
    

    # Y-grid-markers
    i = 0.0
    while i < len(yGridPositions):
      y = yGridPositions[int(i)]
      py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
      p = yLabelPriorities.numberArray[int(i)]
      l = 1.0
      if p == 1.0:
        l = 8.0
      elif p == 2.0:
        l = 3.0
      side = 1.0
      if  not settings.yAxisAuto  and settings.yAxisRight:
        side =  -1.0
      DrawLine1px(canvas, originXPixels, py, originXPixels + side*l, py, GetBlack())
      i = i + 1.0
    

    # Draw points
    plot = 0.0
    while plot < len(settings.scatterPlotSeries):
      sp = settings.scatterPlotSeries[int(plot)]

      xs = sp.xs
      ys = sp.ys
      linearInterpolation = sp.linearInterpolation

      x1Ref = NumberReference()
      y1Ref = NumberReference()
      x2Ref = NumberReference()
      y2Ref = NumberReference()
      if linearInterpolation:
        prevSet = False
        xPrev = 0.0
        yPrev = 0.0
        i = 0.0
        while i < len(xs):
          x = xs[int(i)]
          y = ys[int(i)]

          if prevSet:
            x1Ref.numberValue = xPrev
            y1Ref.numberValue = yPrev
            x2Ref.numberValue = x
            y2Ref.numberValue = y

            success = CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax)

            if success:
              pxPrev = floor(MapXCoordinate(x1Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax))
              pyPrev = floor(MapYCoordinate(y1Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax))
              px = floor(MapXCoordinate(x2Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax))
              py = floor(MapYCoordinate(y2Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax))

              if aStringsEqual(sp.lineType, "solid") and sp.lineThickness == 1.0:
                DrawLine1px(canvas, pxPrev, pyPrev, px, py, sp.color)
              elif aStringsEqual(sp.lineType, "solid"):
                DrawLine(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, sp.color)
              elif aStringsEqual(sp.lineType, "dashed"):
                linePattern = GetLinePattern1()
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
              elif aStringsEqual(sp.lineType, "dotted"):
                linePattern = GetLinePattern2()
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
              elif aStringsEqual(sp.lineType, "dotdash"):
                linePattern = GetLinePattern3()
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
              elif aStringsEqual(sp.lineType, "longdash"):
                linePattern = GetLinePattern4()
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
              elif aStringsEqual(sp.lineType, "twodash"):
                linePattern = GetLinePattern5()
                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)

          prevSet = True
          xPrev = x
          yPrev = y
          i = i + 1.0
        
      else:
        i = 0.0
        while i < len(xs):
          x = xs[int(i)]
          y = ys[int(i)]

          if x > xMin and x < xMax and y > yMin and y < yMax:

            x = floor(MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax))
            y = floor(MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax))

            if aStringsEqual(sp.pointType, "crosses"):
              DrawPixel(canvas, x, y, sp.color)
              DrawPixel(canvas, x + 1.0, y, sp.color)
              DrawPixel(canvas, x + 2.0, y, sp.color)
              DrawPixel(canvas, x - 1.0, y, sp.color)
              DrawPixel(canvas, x - 2.0, y, sp.color)
              DrawPixel(canvas, x, y + 1.0, sp.color)
              DrawPixel(canvas, x, y + 2.0, sp.color)
              DrawPixel(canvas, x, y - 1.0, sp.color)
              DrawPixel(canvas, x, y - 2.0, sp.color)
            elif aStringsEqual(sp.pointType, "circles"):
              DrawCircle(canvas, x, y, 3.0, sp.color)
            elif aStringsEqual(sp.pointType, "dots"):
              DrawFilledCircle(canvas, x, y, 3.0, sp.color)
            elif aStringsEqual(sp.pointType, "triangles"):
              DrawTriangle(canvas, x, y, 3.0, sp.color)
            elif aStringsEqual(sp.pointType, "filled triangles"):
              DrawFilledTriangle(canvas, x, y, 3.0, sp.color)
            elif aStringsEqual(sp.pointType, "pixels"):
              DrawPixel(canvas, x, y, sp.color)
          i = i + 1.0
        
      plot = plot + 1.0
    

    DeleteImage(canvasReference.image)
    canvasReference.image = canvas

  return success

def ComputeBoundariesBasedOnSettings(settings, boundaries):

  if len(settings.scatterPlotSeries) >= 1.0:
    xMin = GetMinimum(settings.scatterPlotSeries[int(0.0)].xs)
    xMax = GetMaximum(settings.scatterPlotSeries[int(0.0)].xs)
    yMin = GetMinimum(settings.scatterPlotSeries[int(0.0)].ys)
    yMax = GetMaximum(settings.scatterPlotSeries[int(0.0)].ys)
  else:
    xMin =  -10.0
    xMax = 10.0
    yMin =  -10.0
    yMax = 10.0

  if  not settings.autoBoundaries :
    xMin = settings.xMin
    xMax = settings.xMax
    yMin = settings.yMin
    yMax = settings.yMax
  else:
    plot = 1.0
    while plot < len(settings.scatterPlotSeries):
      sp = settings.scatterPlotSeries[int(plot)]

      xMin = min(xMin,GetMinimum(sp.xs))
      xMax = max(xMax,GetMaximum(sp.xs))
      yMin = min(yMin,GetMinimum(sp.ys))
      yMax = max(yMax,GetMaximum(sp.ys))
      plot = plot + 1.0
    

  boundaries.x1 = xMin
  boundaries.y1 = yMin
  boundaries.x2 = xMax
  boundaries.y2 = yMax

def ScatterPlotFromSettingsValid(settings):

  success = True

  # Check axis placement.
  if  not settings.xAxisAuto :
    if settings.xAxisTop and settings.xAxisBottom:
      success = False
    if  not settings.xAxisTop  and  not settings.xAxisBottom :
      success = False

  if  not settings.yAxisAuto :
    if settings.yAxisLeft and settings.yAxisRight:
      success = False
    if  not settings.yAxisLeft  and  not settings.yAxisRight :
      success = False

  # Check series lengths.
  i = 0.0
  while i < len(settings.scatterPlotSeries):
    series = settings.scatterPlotSeries[int(i)]
    if len(series.xs) != len(series.ys):
      success = False
    if len(series.xs) == 0.0:
      success = False
    if series.linearInterpolation and len(series.xs) == 1.0:
      success = False
    i = i + 1.0
  

  # Check bounds.
  if  not settings.autoBoundaries :
    if settings.xMin >= settings.xMax:
      success = False
    if settings.yMin >= settings.yMax:
      success = False

  # Check padding.
  if  not settings.autoPadding :
    if 2.0*settings.xPadding >= settings.width:
      success = False
    if 2.0*settings.yPadding >= settings.height:
      success = False

  # Check width and height.
  if settings.width < 0.0:
    success = False
  if settings.height < 0.0:
    success = False

  # Check point types.
  i = 0.0
  while i < len(settings.scatterPlotSeries):
    series = settings.scatterPlotSeries[int(i)]

    if series.lineThickness < 0.0:
      success = False

    if  not series.linearInterpolation :
      # Point type.
      found = False
      if aStringsEqual(series.pointType, "crosses"):
        found = True
      elif aStringsEqual(series.pointType, "circles"):
        found = True
      elif aStringsEqual(series.pointType, "dots"):
        found = True
      elif aStringsEqual(series.pointType, "triangles"):
        found = True
      elif aStringsEqual(series.pointType, "filled triangles"):
        found = True
      elif aStringsEqual(series.pointType, "pixels"):
        found = True
      if  not found :
        success = False
    else:
      # Line type.
      found = False
      if aStringsEqual(series.lineType, "solid"):
        found = True
      elif aStringsEqual(series.lineType, "dashed"):
        found = True
      elif aStringsEqual(series.lineType, "dotted"):
        found = True
      elif aStringsEqual(series.lineType, "dotdash"):
        found = True
      elif aStringsEqual(series.lineType, "longdash"):
        found = True
      elif aStringsEqual(series.lineType, "twodash"):
        found = True

      if  not found :
        success = False
    i = i + 1.0
  

  return success

def GetDefaultBarPlotSettings():

  settings = BarPlotSettings()

  settings.width = 800.0
  settings.height = 600.0
  settings.autoBoundaries = True
  settings.yMax = 0.0
  settings.yMin = 0.0
  settings.autoPadding = True
  settings.xPadding = 0.0
  settings.yPadding = 0.0
  settings.title = ""
  settings.yLabel = ""
  settings.barPlotSeries =  [None]*int(0.0)
  settings.showGrid = True
  settings.gridColor = GetGray(0.1)
  settings.autoColor = True
  settings.grayscaleAutoColor = False
  settings.autoSpacing = True
  settings.groupSeparation = 0.0
  settings.barSeparation = 0.0
  settings.autoLabels = True
  settings.xLabels =  [None]*int(0.0)
  #settings.autoLabels = false;
  #        settings.xLabels = new StringReference [5];
  #        settings.xLabels[0] = CreateStringReference("may 20".toCharArray());
  #        settings.xLabels[1] = CreateStringReference("jun 20".toCharArray());
  #        settings.xLabels[2] = CreateStringReference("jul 20".toCharArray());
  #        settings.xLabels[3] = CreateStringReference("aug 20".toCharArray());
  #        settings.xLabels[4] = CreateStringReference("sep 20".toCharArray());
  settings.barBorder = False

  return settings

def GetDefaultBarPlotSeriesSettings():

  series = BarPlotSeries()

  series.ys =  [None]*int(0.0)
  series.color = GetBlack()

  return series

def DrawBarPlot(width, height, ys):

  settings = GetDefaultBarPlotSettings()

  settings.barPlotSeries =  [None]*int(1.0)
  settings.barPlotSeries[int(0.0)] = GetDefaultBarPlotSeriesSettings()
  settings.barPlotSeries[int(0.0)].ys = None
  settings.barPlotSeries[int(0.0)].ys = ys
  canvasReference = RGBABitmapImageReference()
  settings.width = width
  settings.height = height

  DrawBarPlotFromSettings(canvasReference, settings)

  return canvasReference.image

def DrawBarPlotFromSettings(canvasReference, settings):

  success = BarPlotSettingsIsValid(settings)

  if success:

    canvas = CreateImage(settings.width, settings.height, GetWhite())

    ss = len(settings.barPlotSeries)
    gridLabelColor = GetGray(0.5)

    # padding
    if settings.autoPadding:
      xPadding = floor(GetDefaultPaddingPercentage()*ImageWidth(canvas))
      yPadding = floor(GetDefaultPaddingPercentage()*ImageHeight(canvas))
    else:
      xPadding = settings.xPadding
      yPadding = settings.yPadding

    # Draw title
    DrawText(canvas, floor(ImageWidth(canvas)/2.0 - GetTextWidth(settings.title)/2.0), floor(yPadding/3.0), settings.title, GetBlack())
    DrawTextUpwards(canvas, 10.0, floor(ImageHeight(canvas)/2.0 - GetTextWidth(settings.yLabel)/2.0), settings.yLabel, GetBlack())

    # min and max
    if settings.autoBoundaries:
      if ss >= 1.0:
        yMax = GetMaximum(settings.barPlotSeries[int(0.0)].ys)
        yMin = min(0.0,GetMinimum(settings.barPlotSeries[int(0.0)].ys))

        s = 0.0
        while s < ss:
          yMax = max(yMax,GetMaximum(settings.barPlotSeries[int(s)].ys))
          yMin = min(yMin,GetMinimum(settings.barPlotSeries[int(s)].ys))
          s = s + 1.0
        
      else:
        yMax = 10.0
        yMin = 0.0
    else:
      yMin = settings.yMin
      yMax = settings.yMax
    yLength = yMax - yMin

    # boundaries
    xPixelMin = xPadding
    yPixelMin = yPadding
    xPixelMax = ImageWidth(canvas) - xPadding
    yPixelMax = ImageHeight(canvas) - yPadding
    xLengthPixels = xPixelMax - xPixelMin
    yLengthPixels = yPixelMax - yPixelMin

    # Draw boundary.
    DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor)

    # Draw grid lines.
    yLabels = StringArrayReference()
    yLabelPriorities = NumberArrayReference()
    yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities)

    if settings.showGrid:
      # Y-grid
      i = 0.0
      while i < len(yGridPositions):
        y = yGridPositions[int(i)]
        py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
        DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor)
        i = i + 1.0
      

    # Draw origin.
    if yMin < 0.0 and yMax > 0.0:
      py = MapYCoordinate(0.0, yMin, yMax, yPixelMin, yPixelMax)
      DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor)

    # Labels
    occupied =  [None]*int(len(yLabels.stringArray))
    i = 0.0
    while i < len(occupied):
      occupied[int(i)] = CreateRectangle(0.0, 0.0, 0.0, 0.0)
      i = i + 1.0
    
    nextRectangle = CreateNumberReference(0.0)

    i = 1.0
    while i <= 5.0:
      DrawYLabelsForPriority(i, yMin, xPixelMin, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, True)
      i = i + 1.0
    

    # Draw bars.
    if settings.autoColor:
      if  not settings.grayscaleAutoColor :
        colors = Get8HighContrastColors()
      else:
        colors =  [None]*int(ss)
        if ss > 1.0:
          i = 0.0
          while i < ss:
            colors[int(i)] = GetGray(0.7 - (i/ss)*0.7)
            i = i + 1.0
          
        else:
          colors[int(0.0)] = GetGray(0.5)
    else:
      colors =  [None]*int(0.0)

    # distances
    bs = len(settings.barPlotSeries[int(0.0)].ys)

    if settings.autoSpacing:
      groupSeparation = ImageWidth(canvas)*0.05
      barSeparation = ImageWidth(canvas)*0.005
    else:
      groupSeparation = settings.groupSeparation
      barSeparation = settings.barSeparation

    barWidth = (xLengthPixels - groupSeparation*(bs - 1.0) - barSeparation*(bs*(ss - 1.0)))/(bs*ss)

    # Draw bars.
    b = 0.0
    n = 0.0
    while n < bs:
      s = 0.0
      while s < ss:
        ys = settings.barPlotSeries[int(s)].ys

        yValue = ys[int(n)]

        yBottom = MapYCoordinate(yValue, yMin, yMax, yPixelMin, yPixelMax)
        yTop = MapYCoordinate(0.0, yMin, yMax, yPixelMin, yPixelMax)

        x = xPixelMin + n*(groupSeparation + ss*barWidth) + s*(barWidth) + b*barSeparation
        w = barWidth

        if yValue >= 0.0:
          y = yBottom
          h = yTop - y
        else:
          y = yTop
          h = yBottom - yTop

        # Cut at boundaries.
        if y < yPixelMin and y + h > yPixelMax:
          y = yPixelMin
          h = yPixelMax - yPixelMin
        elif y < yPixelMin:
          y = yPixelMin
          if yValue >= 0.0:
            h = yTop - y
          else:
            h = yBottom - y
        elif y + h > yPixelMax:
          h = yPixelMax - y

        # Get color
        if settings.autoColor:
          barColor = colors[int(s)]
        else:
          barColor = settings.barPlotSeries[int(s)].color

        # Draw
        if settings.barBorder:
          DrawFilledRectangleWithBorder(canvas, Round(x), Round(y), Round(w), Round(h), GetBlack(), barColor)
        else:
          DrawFilledRectangle(canvas, Round(x), Round(y), Round(w), Round(h), barColor)

        b = b + 1.0
        s = s + 1.0
      
      b = b - 1.0
      n = n + 1.0
    

    # x-labels
    n = 0.0
    while n < bs:
      if settings.autoLabels:
        label = CreateStringDecimalFromNumber(n + 1.0)
      else:
        label = settings.xLabels[int(n)].string

      textwidth = GetTextWidth(label)

      x = xPixelMin + (n + 0.5)*(ss*barWidth + (ss - 1.0)*barSeparation) + n*groupSeparation - textwidth/2.0

      DrawText(canvas, floor(x), ImageHeight(canvas) - yPadding + 20.0, label, gridLabelColor)

      b = b + 1.0
      n = n + 1.0
    

    canvasReference.image = canvas

  return success

def BarPlotSettingsIsValid(settings):

  success = True

  # Check series lengths.
  lengthSet = False
  length = 0.0
  i = 0.0
  while i < len(settings.barPlotSeries):
    series = settings.barPlotSeries[int(i)]

    if  not lengthSet :
      length = len(series.ys)
      lengthSet = True
    elif length != len(series.ys):
      success = False
    i = i + 1.0
  

  # Check bounds.
  if  not settings.autoBoundaries :
    if settings.yMin >= settings.yMax:
      success = False

  # Check padding.
  if  not settings.autoPadding :
    if 2.0*settings.xPadding >= settings.width:
      success = False
    if 2.0*settings.yPadding >= settings.height:
      success = False

  # Check width and height.
  if settings.width < 0.0:
    success = False
  if settings.height < 0.0:
    success = False

  # Check spacing
  if  not settings.autoSpacing :
    if settings.groupSeparation < 0.0:
      success = False
    if settings.barSeparation < 0.0:
      success = False

  return success

def GetMinimum(data):

  minimum = data[int(0.0)]
  i = 0.0
  while i < len(data):
    minimum = min(minimum,data[int(i)])
    i = i + 1.0
  

  return minimum

def GetMaximum(data):

  maximum = data[int(0.0)]
  i = 0.0
  while i < len(data):
    maximum = max(maximum,data[int(i)])
    i = i + 1.0
  

  return maximum

def RoundToDigits(element, digitsAfterPoint):
  return Round(element*10.0**digitsAfterPoint)/10.0**digitsAfterPoint

def test():

  failures = CreateNumberReference(0.0)

  imageReference = CreateRGBABitmapImageReference()

  labels = StringArrayReference()
  labelPriorities = NumberArrayReference()

  z = 10.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 11.0, failures)

  z = 9.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 19.0, failures)

  z = 8.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 17.0, failures)

  z = 7.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 15.0, failures)

  z = 6.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 13.0, failures)

  z = 5.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 21.0, failures)

  z = 4.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 17.0, failures)

  z = 3.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 31.0, failures)

  z = 2.0
  gridlines = ComputeGridLinePositions( -z/2.0, z/2.0, labels, labelPriorities)
  AssertEquals(len(gridlines), 21.0, failures)

  xs =  [None]*int(5.0)
  xs[int(0.0)] =  -2.0
  xs[int(1.0)] =  -1.0
  xs[int(2.0)] = 0.0
  xs[int(3.0)] = 1.0
  xs[int(4.0)] = 2.0
  ys =  [None]*int(5.0)
  ys[int(0.0)] = 2.0
  ys[int(1.0)] =  -1.0
  ys[int(2.0)] =  -2.0
  ys[int(3.0)] =  -1.0
  ys[int(4.0)] = 2.0
  DrawScatterPlot(imageReference, 800.0, 600.0, xs, ys)

  imageReference.image = DrawBarPlot(800.0, 600.0, ys)

  TestMapping(failures)
  TestMapping2(failures)

  return failures.numberValue

def TestMapping(failures):

  series = GetDefaultScatterPlotSeriesSettings()

  series.xs =  [None]*int(5.0)
  series.xs[int(0.0)] = -2.0
  series.xs[int(1.0)] = -1.0
  series.xs[int(2.0)] = 0.0
  series.xs[int(3.0)] = 1.0
  series.xs[int(4.0)] = 2.0
  series.ys =  [None]*int(5.0)
  series.ys[int(0.0)] = -2.0
  series.ys[int(1.0)] = -1.0
  series.ys[int(2.0)] = -2.0
  series.ys[int(3.0)] = -1.0
  series.ys[int(4.0)] = 2.0
  series.linearInterpolation = True
  series.lineType = "dashed"
  series.lineThickness = 2.0
  series.color = GetGray(0.3)

  settings = GetDefaultScatterPlotSettings()
  settings.width = 600.0
  settings.height = 400.0
  settings.autoBoundaries = True
  settings.autoPadding = True
  settings.title = "x^2 - 2"
  settings.xLabel = "X axis"
  settings.yLabel = "Y axis"
  settings.scatterPlotSeries =  [None]*int(1.0)
  settings.scatterPlotSeries[int(0.0)] = series

  imageReference = CreateRGBABitmapImageReference()
  DrawScatterPlotFromSettings(imageReference, settings)

  x1 = MapXCoordinateAutoSettings( -1.0, imageReference.image, series.xs)
  y1 = MapYCoordinateAutoSettings( -1.0, imageReference.image, series.ys)

  AssertEquals(x1, 180.0, failures)
  AssertEquals(y1, 280.0, failures)

def TestMapping2(failures):

  points = 300.0
  w = 600.0*2.0
  h = 300.0*2.0
  xMin = 0.0
  xMax = 150.0
  yMin = 0.0
  yMax = 1.0

  xs =  [None]*int(points)
  ys =  [None]*int(points)
  xs2 =  [None]*int(points)
  ys2 =  [None]*int(points)

  i = 0.0
  while i < points:
    x = xMin + (xMax - xMin)/(points - 1.0)*i
    # points - 1d is to ensure both extremeties are included.
    y = x/(x + 7.0)

    xs[int(i)] = x
    ys[int(i)] = y

    y = 1.4*x/(x + 7.0)*(1.0 - (atan((x/1.5 - 30.0)/5.0)/1.6 + 1.0)/2.0)

    xs2[int(i)] = x
    ys2[int(i)] = y
    i = i + 1.0
  

  settings = GetDefaultScatterPlotSettings()

  settings.scatterPlotSeries =  [None]*int(2.0)
  settings.scatterPlotSeries[int(0.0)] = ScatterPlotSeries()
  settings.scatterPlotSeries[int(0.0)].xs = xs
  settings.scatterPlotSeries[int(0.0)].ys = ys
  settings.scatterPlotSeries[int(0.0)].linearInterpolation = True
  settings.scatterPlotSeries[int(0.0)].lineType = "solid"
  settings.scatterPlotSeries[int(0.0)].lineThickness = 3.0
  settings.scatterPlotSeries[int(0.0)].color = CreateRGBColor(1.0, 0.0, 0.0)
  settings.scatterPlotSeries[int(1.0)] = ScatterPlotSeries()
  settings.scatterPlotSeries[int(1.0)].xs = xs2
  settings.scatterPlotSeries[int(1.0)].ys = ys2
  settings.scatterPlotSeries[int(1.0)].linearInterpolation = True
  settings.scatterPlotSeries[int(1.0)].lineType = "solid"
  settings.scatterPlotSeries[int(1.0)].lineThickness = 3.0
  settings.scatterPlotSeries[int(1.0)].color = CreateRGBColor(0.0, 0.0, 1.0)
  settings.autoBoundaries = False
  settings.xMin = xMin
  settings.xMax = xMax
  settings.yMin = yMin
  settings.yMax = yMax
  settings.yLabel = ""
  settings.xLabel = "Features"
  settings.title = ""
  settings.width = w
  settings.height = h

  canvasReference = CreateRGBABitmapImageReference()

  DrawScatterPlotFromSettings(canvasReference, settings)

  x1 = MapXCoordinateBasedOnSettings(27.0, settings)
  y1 = MapYCoordinateBasedOnSettings(1.0, settings)

  AssertEquals(floor(x1), 292.0, failures)
  AssertEquals(y1, 60.0, failures)

def GetBlack():
  black = RGBA()
  black.a = 1.0
  black.r = 0.0
  black.g = 0.0
  black.b = 0.0
  return black

def GetWhite():
  white = RGBA()
  white.a = 1.0
  white.r = 1.0
  white.g = 1.0
  white.b = 1.0
  return white

def GetTransparent():
  transparent = RGBA()
  transparent.a = 0.0
  transparent.r = 0.0
  transparent.g = 0.0
  transparent.b = 0.0
  return transparent

def GetGray(percentage):
  black = RGBA()
  black.a = 1.0
  black.r = 1.0 - percentage
  black.g = 1.0 - percentage
  black.b = 1.0 - percentage
  return black

def CreateRGBColor(r, g, b):
  color = RGBA()
  color.a = 1.0
  color.r = r
  color.g = g
  color.b = b
  return color

def CreateRGBAColor(r, g, b, a):
  color = RGBA()
  color.a = a
  color.r = r
  color.g = g
  color.b = b
  return color

def CreateImage(w, h, color):

  image = RGBABitmapImage()
  image.x =  [None]*int(w)
  i = 0.0
  while i < w:
    image.x[int(i)] = RGBABitmap()
    image.x[int(i)].y =  [None]*int(h)
    j = 0.0
    while j < h:
      image.x[int(i)].y[int(j)] = RGBA()
      SetPixel(image, i, j, color)
      j = j + 1.0
    
    i = i + 1.0
  

  return image

def DeleteImage(image):

  w = ImageWidth(image)
  h = ImageHeight(image)

  i = 0.0
  while i < w:
    j = 0.0
    while j < h:
      image.x[int(i)].y[int(j)] = None
      j = j + 1.0
    
    image.x[int(i)] = None
    i = i + 1.0
  
  image = None

def ImageWidth(image):
  return len(image.x)

def ImageHeight(image):

  if ImageWidth(image) == 0.0:
    height = 0.0
  else:
    height = len(image.x[int(0.0)].y)

  return height

def SetPixel(image, x, y, color):
  if x >= 0.0 and x < ImageWidth(image) and y >= 0.0 and y < ImageHeight(image):
    image.x[int(x)].y[int(y)].a = color.a
    image.x[int(x)].y[int(y)].r = color.r
    image.x[int(x)].y[int(y)].g = color.g
    image.x[int(x)].y[int(y)].b = color.b

def DrawPixel(image, x, y, color):

  if x >= 0.0 and x < ImageWidth(image) and y >= 0.0 and y < ImageHeight(image):
    ra = color.r
    ga = color.g
    ba = color.b
    aa = color.a

    rb = image.x[int(x)].y[int(y)].r
    gb = image.x[int(x)].y[int(y)].g
    bb = image.x[int(x)].y[int(y)].b
    ab = image.x[int(x)].y[int(y)].a

    ao = CombineAlpha(aa, ab)

    ro = AlphaBlend(ra, aa, rb, ab, ao)
    go = AlphaBlend(ga, aa, gb, ab, ao)
    bo = AlphaBlend(ba, aa, bb, ab, ao)

    image.x[int(x)].y[int(y)].r = ro
    image.x[int(x)].y[int(y)].g = go
    image.x[int(x)].y[int(y)].b = bo
    image.x[int(x)].y[int(y)].a = ao

def CombineAlpha(asx, ad):
  return asx + ad*(1.0 - asx)

def AlphaBlend(cs, asx, cd, ad, ao):
  return (cs*asx + cd*ad*(1.0 - asx))/ao

def DrawHorizontalLine1px(image, x, y, length, color):

  i = 0.0
  while i < length:
    DrawPixel(image, x + i, y, color)
    i = i + 1.0
  

def DrawVerticalLine1px(image, x, y, height, color):

  i = 0.0
  while i < height:
    DrawPixel(image, x, y + i, color)
    i = i + 1.0
  

def DrawRectangle1px(image, x, y, width, height, color):
  DrawHorizontalLine1px(image, x, y, width + 1.0, color)
  DrawVerticalLine1px(image, x, y + 1.0, height + 1.0 - 1.0, color)
  DrawVerticalLine1px(image, x + width, y + 1.0, height + 1.0 - 1.0, color)
  DrawHorizontalLine1px(image, x + 1.0, y + height, width + 1.0 - 2.0, color)

def DrawImageOnImage(dst, src, topx, topy):

  y = 0.0
  while y < ImageHeight(src):
    x = 0.0
    while x < ImageWidth(src):
      if topx + x >= 0.0 and topx + x < ImageWidth(dst) and topy + y >= 0.0 and topy + y < ImageHeight(dst):
        DrawPixel(dst, topx + x, topy + y, src.x[int(x)].y[int(y)])
      x = x + 1.0
    
    y = y + 1.0
  

def DrawLine1px(image, x0, y0, x1, y1, color):
  XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color)

def XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color):

  olda = color.a

  steep = fabs(y1 - y0) > fabs(x1 - x0)

  if steep:
    t = x0
    x0 = y0
    y0 = t

    t = x1
    x1 = y1
    y1 = t
  if x0 > x1:
    t = x0
    x0 = x1
    x1 = t

    t = y0
    y0 = y1
    y1 = t

  dx = x1 - x0
  dy = y1 - y0
  g = dy/dx

  if dx == 0.0:
    g = 1.0

  xEnd = Round(x0)
  yEnd = y0 + g*(xEnd - x0)
  xGap = OneMinusFractionalPart(x0 + 0.5)
  xpxl1 = xEnd
  ypxl1 = floor(yEnd)
  if steep:
    DrawPixel(image, ypxl1, xpxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
    DrawPixel(image, ypxl1 + 1.0, xpxl1, SetBrightness(color, FractionalPart(yEnd)*xGap))
  else:
    DrawPixel(image, xpxl1, ypxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
    DrawPixel(image, xpxl1, ypxl1 + 1.0, SetBrightness(color, FractionalPart(yEnd)*xGap))
  intery = yEnd + g

  xEnd = Round(x1)
  yEnd = y1 + g*(xEnd - x1)
  xGap = FractionalPart(x1 + 0.5)
  xpxl2 = xEnd
  ypxl2 = floor(yEnd)
  if steep:
    DrawPixel(image, ypxl2, xpxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
    DrawPixel(image, ypxl2 + 1.0, xpxl2, SetBrightness(color, FractionalPart(yEnd)*xGap))
  else:
    DrawPixel(image, xpxl2, ypxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
    DrawPixel(image, xpxl2, ypxl2 + 1.0, SetBrightness(color, FractionalPart(yEnd)*xGap))

  if steep:
    x = xpxl1 + 1.0
    while x <= xpxl2 - 1.0:
      DrawPixel(image, floor(intery), x, SetBrightness(color, OneMinusFractionalPart(intery)))
      DrawPixel(image, floor(intery) + 1.0, x, SetBrightness(color, FractionalPart(intery)))
      intery = intery + g
      x = x + 1.0
    
  else:
    x = xpxl1 + 1.0
    while x <= xpxl2 - 1.0:
      DrawPixel(image, x, floor(intery), SetBrightness(color, OneMinusFractionalPart(intery)))
      DrawPixel(image, x, floor(intery) + 1.0, SetBrightness(color, FractionalPart(intery)))
      intery = intery + g
      x = x + 1.0
    

  color.a = olda

def OneMinusFractionalPart(x):
  return 1.0 - FractionalPart(x)

def FractionalPart(x):
  return x - floor(x)

def SetBrightness(color, newBrightness):
  color.a = newBrightness
  return color

def DrawQuadraticBezierCurve(image, x0, y0, cx, cy, x1, y1, color):

  dx = fabs(x0 - x1)
  dy = fabs(y0 - y1)

  dt = 1.0/sqrt(dx**2.0 + dy**2.0)

  xs = NumberReference()
  ys = NumberReference()
  xe = NumberReference()
  ye = NumberReference()

  QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, 0.0, xs, ys)
  t = dt
  while t <= 1.0:
    QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, xe, ye)
    DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color)
    xs.numberValue = xe.numberValue
    ys.numberValue = ye.numberValue
    t = t + dt
  

  xs = None
  ys = None
  xe = None
  ye = None

def QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, x, y):
  x.numberValue = (1.0 - t)**2.0*x0 + (1.0 - t)*2.0*t*cx + t**2.0*x1
  y.numberValue = (1.0 - t)**2.0*y0 + (1.0 - t)*2.0*t*cy + t**2.0*y1

def DrawCubicBezierCurve(image, x0, y0, c0x, c0y, c1x, c1y, x1, y1, color):

  dx = fabs(x0 - x1)
  dy = fabs(y0 - y1)

  dt = 1.0/sqrt(dx**2.0 + dy**2.0)

  xs = NumberReference()
  ys = NumberReference()
  xe = NumberReference()
  ye = NumberReference()

  CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, 0.0, xs, ys)
  t = dt
  while t <= 1.0:
    CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, xe, ye)
    DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color)
    xs.numberValue = xe.numberValue
    ys.numberValue = ye.numberValue
    t = t + dt
  

  xs = None
  ys = None
  xe = None
  ye = None

def CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, x, y):
  x.numberValue = (1.0 - t)**3.0*x0 + (1.0 - t)**2.0*3.0*t*c0x + (1.0 - t)*3.0*t**2.0*c1x + t**3.0*x1

  y.numberValue = (1.0 - t)**3.0*y0 + (1.0 - t)**2.0*3.0*t*c0y + (1.0 - t)*3.0*t**2.0*c1y + t**3.0*y1

def CopyImage(image):

  copy = CreateImage(ImageWidth(image), ImageHeight(image), GetTransparent())

  i = 0.0
  while i < ImageWidth(image):
    j = 0.0
    while j < ImageHeight(image):
      SetPixel(copy, i, j, image.x[int(i)].y[int(j)])
      j = j + 1.0
    
    i = i + 1.0
  

  return copy

def GetImagePixel(image, x, y):
  return image.x[int(x)].y[int(y)]

def HorizontalFlip(img):

  y = 0.0
  while y < ImageHeight(img):
    x = 0.0
    while x < ImageWidth(img)/2.0:
      c1 = img.x[int(x)].y[int(y)]
      c2 = img.x[int(ImageWidth(img) - 1.0 - x)].y[int(y)]

      tmp = c1.a
      c1.a = c2.a
      c2.a = tmp

      tmp = c1.r
      c1.r = c2.r
      c2.r = tmp

      tmp = c1.g
      c1.g = c2.g
      c2.g = tmp

      tmp = c1.b
      c1.b = c2.b
      c2.b = tmp
      x = x + 1.0
    
    y = y + 1.0
  

def DrawFilledRectangle(image, x, y, w, h, color):

  i = 0.0
  while i < w:
    j = 0.0
    while j < h:
      SetPixel(image, x + i, y + j, color)
      j = j + 1.0
    
    i = i + 1.0
  

def RotateAntiClockwise90Degrees(image):

  rotated = CreateImage(ImageHeight(image), ImageWidth(image), GetBlack())

  y = 0.0
  while y < ImageHeight(image):
    x = 0.0
    while x < ImageWidth(image):
      SetPixel(rotated, y, ImageWidth(image) - 1.0 - x, GetImagePixel(image, x, y))
      x = x + 1.0
    
    y = y + 1.0
  

  return rotated

def DrawCircle(canvas, xCenter, yCenter, radius, color):
  DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color)

def BresenhamsCircleDrawingAlgorithm(canvas, xCenter, yCenter, radius, color):

  y = radius
  x = 0.0

  delta = 3.0 - 2.0*radius
  while y >= x:
    DrawLine1px(canvas, xCenter + x, yCenter + y, xCenter + x, yCenter + y, color)
    DrawLine1px(canvas, xCenter + x, yCenter - y, xCenter + x, yCenter - y, color)
    DrawLine1px(canvas, xCenter - x, yCenter + y, xCenter - x, yCenter + y, color)
    DrawLine1px(canvas, xCenter - x, yCenter - y, xCenter - x, yCenter - y, color)

    DrawLine1px(canvas, xCenter - y, yCenter + x, xCenter - y, yCenter + x, color)
    DrawLine1px(canvas, xCenter - y, yCenter - x, xCenter - y, yCenter - x, color)
    DrawLine1px(canvas, xCenter + y, yCenter + x, xCenter + y, yCenter + x, color)
    DrawLine1px(canvas, xCenter + y, yCenter - x, xCenter + y, yCenter - x, color)

    if delta < 0.0:
      delta = delta + 4.0*x + 6.0
    else:
      delta = delta + 4.0*(x - y) + 10.0
      y = y - 1.0
    x = x + 1.0
  

def DrawCircleMidpointAlgorithm(canvas, xCenter, yCenter, radius, color):

  d = floor((5.0 - radius*4.0)/4.0)
  x = 0.0
  y = radius

  while x <= y:
    DrawPixel(canvas, xCenter + x, yCenter + y, color)
    DrawPixel(canvas, xCenter + x, yCenter - y, color)
    DrawPixel(canvas, xCenter - x, yCenter + y, color)
    DrawPixel(canvas, xCenter - x, yCenter - y, color)
    DrawPixel(canvas, xCenter + y, yCenter + x, color)
    DrawPixel(canvas, xCenter + y, yCenter - x, color)
    DrawPixel(canvas, xCenter - y, yCenter + x, color)
    DrawPixel(canvas, xCenter - y, yCenter - x, color)

    if d < 0.0:
      d = d + 2.0*x + 1.0
    else:
      d = d + 2.0*(x - y) + 1.0
      y = y - 1.0
    x = x + 1.0
  

def DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color):

  # Place the circle in the center of the pixel.
  xCenter = floor(xCenter) + 0.5
  yCenter = floor(yCenter) + 0.5

  pixels = 2.0*pi*radius

  # Below a radius of 10 pixels, over-compensate to get a smoother circle.
  if radius < 10.0:
    pixels = pixels*10.0

  da = 2.0*pi/pixels

  a = 0.0
  while a < 2.0*pi:
    dx = cos(a)*radius
    dy = sin(a)*radius

    # Floor to get the pixel coordinate.
    DrawPixel(canvas, floor(xCenter + dx), floor(yCenter + dy), color)
    a = a + da
  

def DrawFilledCircle(canvas, x, y, r, color):
  DrawFilledCircleBasicAlgorithm(canvas, x, y, r, color)

def DrawFilledCircleMidpointAlgorithm(canvas, xCenter, yCenter, radius, color):

  d = floor((5.0 - radius*4.0)/4.0)
  x = 0.0
  y = radius

  while x <= y:
    DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter + y, xCenter - x, yCenter + y, color)
    DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter - y, xCenter - x, yCenter - y, color)
    DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter + x, xCenter - y, yCenter + x, color)
    DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter - x, xCenter - y, yCenter - x, color)

    if d < 0.0:
      d = d + 2.0*x + 1.0
    else:
      d = d + 2.0*(x - y) + 1.0
      y = y - 1.0
    x = x + 1.0
  

def DrawFilledCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color):

  # Place the circle in the center of the pixel.
  xCenter = floor(xCenter) + 0.5
  yCenter = floor(yCenter) + 0.5

  pixels = 2.0*pi*radius

  # Below a radius of 10 pixels, over-compensate to get a smoother circle.
  if radius < 10.0:
    pixels = pixels*10.0

  da = 2.0*pi/pixels

  # Draw lines for a half-circle to fill an entire circle.
  a = 0.0
  while a < pi:
    dx = cos(a)*radius
    dy = sin(a)*radius

    # Floor to get the pixel coordinate.
    DrawVerticalLine1px(canvas, floor(xCenter - dx), floor(yCenter - dy), floor(2.0*dy) + 1.0, color)
    a = a + da
  

def DrawTriangle(canvas, xCenter, yCenter, height, color):

  x1 = floor(xCenter + 0.5)
  y1 = floor(floor(yCenter + 0.5) - height)
  x2 = x1 - 2.0*height*tan(pi/6.0)
  y2 = floor(y1 + 2.0*height)
  x3 = x1 + 2.0*height*tan(pi/6.0)
  y3 = floor(y1 + 2.0*height)

  DrawLine1px(canvas, x1, y1, x2, y2, color)
  DrawLine1px(canvas, x1, y1, x3, y3, color)
  DrawLine1px(canvas, x2, y2, x3, y3, color)

def DrawFilledTriangle(canvas, xCenter, yCenter, height, color):

  x1 = floor(xCenter + 0.5)
  y1 = floor(floor(yCenter + 0.5) - height)

  i = 0.0
  while i <= 2.0*height:
    offset = floor(i*tan(pi/6.0))
    DrawHorizontalLine1px(canvas, x1 - offset, y1 + i, 2.0*offset, color)
    i = i + 1.0
  

def DrawLine(canvas, x1, y1, x2, y2, thickness, color):
  DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color)

def DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color):

  dx = x2 - x1
  dy = y2 - y1

  incX = Sign(dx)
  incY = Sign(dy)

  dx = fabs(dx)
  dy = fabs(dy)

  if dx > dy:
    pdx = incX
    pdy = 0.0
    es = dy
    el = dx
  else:
    pdx = 0.0
    pdy = incY
    es = dx
    el = dy

  x = x1
  y = y1
  err = el/2.0

  if thickness >= 3.0:
    r = thickness/2.0
    DrawCircle(canvas, x, y, r, color)
  elif floor(thickness) == 2.0:
    DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
  elif floor(thickness) == 1.0:
    DrawPixel(canvas, x, y, color)

  t = 0.0
  while t < el:
    err = err - es
    if err < 0.0:
      err = err + el
      x = x + incX
      y = y + incY
    else:
      x = x + pdx
      y = y + pdy

    if thickness >= 3.0:
      r = thickness/2.0
      DrawCircle(canvas, x, y, r, color)
    elif floor(thickness) == 2.0:
      DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
    elif floor(thickness) == 1.0:
      DrawPixel(canvas, x, y, color)
    t = t + 1.0
  

def DrawLineBresenhamsAlgorithm(canvas, x1, y1, x2, y2, color):

  dx = x2 - x1
  dy = y2 - y1

  incX = Sign(dx)
  incY = Sign(dy)

  dx = fabs(dx)
  dy = fabs(dy)

  if dx > dy:
    pdx = incX
    pdy = 0.0
    es = dy
    el = dx
  else:
    pdx = 0.0
    pdy = incY
    es = dx
    el = dy

  x = x1
  y = y1
  err = el/2.0
  DrawPixel(canvas, x, y, color)

  t = 0.0
  while t < el:
    err = err - es
    if err < 0.0:
      err = err + el
      x = x + incX
      y = y + incY
    else:
      x = x + pdx
      y = y + pdy

    DrawPixel(canvas, x, y, color)
    t = t + 1.0
  

def DrawLineBresenhamsAlgorithmThickPatterned(canvas, x1, y1, x2, y2, thickness, pattern, offset, color):

  dx = x2 - x1
  dy = y2 - y1

  incX = Sign(dx)
  incY = Sign(dy)

  dx = fabs(dx)
  dy = fabs(dy)

  if dx > dy:
    pdx = incX
    pdy = 0.0
    es = dy
    el = dx
  else:
    pdx = 0.0
    pdy = incY
    es = dx
    el = dy

  x = x1
  y = y1
  err = el/2.0

  offset.numberValue = (offset.numberValue + 1.0) % (len(pattern)*thickness)

  if pattern[int(floor(offset.numberValue/thickness))]:
    if thickness >= 3.0:
      r = thickness/2.0
      DrawCircle(canvas, x, y, r, color)
    elif floor(thickness) == 2.0:
      DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
    elif floor(thickness) == 1.0:
      DrawPixel(canvas, x, y, color)

  t = 0.0
  while t < el:
    err = err - es
    if err < 0.0:
      err = err + el
      x = x + incX
      y = y + incY
    else:
      x = x + pdx
      y = y + pdy

    offset.numberValue = (offset.numberValue + 1.0) % (len(pattern)*thickness)

    if pattern[int(floor(offset.numberValue/thickness))]:
      if thickness >= 3.0:
        r = thickness/2.0
        DrawCircle(canvas, x, y, r, color)
      elif floor(thickness) == 2.0:
        DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
      elif floor(thickness) == 1.0:
        DrawPixel(canvas, x, y, color)
    t = t + 1.0
  

def GetLinePattern5():

  pattern =  [None]*int(19.0)

  pattern[int(0.0)] = True
  pattern[int(1.0)] = True
  pattern[int(2.0)] = True
  pattern[int(3.0)] = True
  pattern[int(4.0)] = True
  pattern[int(5.0)] = True
  pattern[int(6.0)] = True
  pattern[int(7.0)] = True
  pattern[int(8.0)] = True
  pattern[int(9.0)] = True
  pattern[int(10.0)] = False
  pattern[int(11.0)] = False
  pattern[int(12.0)] = False
  pattern[int(13.0)] = True
  pattern[int(14.0)] = True
  pattern[int(15.0)] = True
  pattern[int(16.0)] = False
  pattern[int(17.0)] = False
  pattern[int(18.0)] = False

  return pattern

def GetLinePattern4():

  pattern =  [None]*int(13.0)

  pattern[int(0.0)] = True
  pattern[int(1.0)] = True
  pattern[int(2.0)] = True
  pattern[int(3.0)] = True
  pattern[int(4.0)] = True
  pattern[int(5.0)] = True
  pattern[int(6.0)] = True
  pattern[int(7.0)] = True
  pattern[int(8.0)] = True
  pattern[int(9.0)] = True
  pattern[int(10.0)] = False
  pattern[int(11.0)] = False
  pattern[int(12.0)] = False

  return pattern

def GetLinePattern3():

  pattern =  [None]*int(13.0)

  pattern[int(0.0)] = True
  pattern[int(1.0)] = True
  pattern[int(2.0)] = True
  pattern[int(3.0)] = True
  pattern[int(4.0)] = True
  pattern[int(5.0)] = True
  pattern[int(6.0)] = False
  pattern[int(7.0)] = False
  pattern[int(8.0)] = False
  pattern[int(9.0)] = True
  pattern[int(10.0)] = True
  pattern[int(11.0)] = False
  pattern[int(12.0)] = False

  return pattern

def GetLinePattern2():

  pattern =  [None]*int(4.0)

  pattern[int(0.0)] = True
  pattern[int(1.0)] = True
  pattern[int(2.0)] = False
  pattern[int(3.0)] = False

  return pattern

def GetLinePattern1():

  pattern =  [None]*int(8.0)

  pattern[int(0.0)] = True
  pattern[int(1.0)] = True
  pattern[int(2.0)] = True
  pattern[int(3.0)] = True
  pattern[int(4.0)] = True
  pattern[int(5.0)] = False
  pattern[int(6.0)] = False
  pattern[int(7.0)] = False

  return pattern

def Blur(src, pixels):

  w = ImageWidth(src)
  h = ImageHeight(src)
  dst = CreateImage(w, h, GetTransparent())

  x = 0.0
  while x < w:
    y = 0.0
    while y < h:
      SetPixel(dst, x, y, CreateBlurForPoint(src, x, y, pixels))
      y = y + 1.0
    
    x = x + 1.0
  

  return dst

def CreateBlurForPoint(src, x, y, pixels):

  w = len(src.x)
  h = len(src.x[int(0.0)].y)

  rgba = RGBA()
  rgba.r = 0.0
  rgba.g = 0.0
  rgba.b = 0.0
  rgba.a = 0.0

  fromxx = x - pixels
  fromxx = max(fromxx,0.0)

  tox = x + pixels
  tox = min(tox,w - 1.0)

  fromy = y - pixels
  fromy = max(fromy,0.0)

  toy = y + pixels
  toy = min(toy,h - 1.0)

  countColor = 0.0
  countTransparent = 0.0
  i = fromxx
  while i < tox:
    j = fromy
    while j < toy:
      alpha = src.x[int(i)].y[int(j)].a
      if alpha > 0.0:
        rgba.r = rgba.r + src.x[int(i)].y[int(j)].r
        rgba.g = rgba.g + src.x[int(i)].y[int(j)].g
        rgba.b = rgba.b + src.x[int(i)].y[int(j)].b
        countColor = countColor + 1.0
      rgba.a = rgba.a + alpha
      countTransparent = countTransparent + 1.0
      j = j + 1.0
    
    i = i + 1.0
  

  if countColor > 0.0:
    rgba.r = rgba.r/countColor
    rgba.g = rgba.g/countColor
    rgba.b = rgba.b/countColor
  else:
    rgba.r = 0.0
    rgba.g = 0.0
    rgba.b = 0.0

  if countTransparent > 0.0:
    rgba.a = rgba.a/countTransparent
  else:
    rgba.a = 0.0

  return rgba

def CreateStringScientificNotationDecimalFromNumber(decimal):

  mantissaReference = StringReference()
  exponentReference = StringReference()
  result =  [None]*int(0.0)
  done = False
  exponent = 0.0

  if decimal < 0.0:
    isPositive = False
    decimal =  -decimal
  else:
    isPositive = True

  if decimal == 0.0:
    done = True

  if  not done :
    multiplier = 0.0
    inc = 0.0

    if decimal < 1.0:
      multiplier = 10.0
      inc =  -1.0
    elif decimal >= 10.0:
      multiplier = 0.1
      inc = 1.0
    else:
      done = True

    if  not done :
      while decimal >= 10.0 or decimal < 1.0:
        decimal = decimal*multiplier
        exponent = exponent + inc
      

  CreateStringFromNumberWithCheck(decimal, 10.0, mantissaReference)

  CreateStringFromNumberWithCheck(exponent, 10.0, exponentReference)

  if  not isPositive :
    result = AppendString(result, "-")

  result = AppendString(result, mantissaReference.string)
  result = AppendString(result, "e")
  result = AppendString(result, exponentReference.string)

  return result

def CreateStringDecimalFromNumber(decimal):

  stringReference = StringReference()

  # This will succeed because base = 10.
  CreateStringFromNumberWithCheck(decimal, 10.0, stringReference)

  return stringReference.string

def CreateStringFromNumberWithCheck(decimal, base, stringReference):

  isPositive = True

  if decimal < 0.0:
    isPositive = False
    decimal =  -decimal

  if decimal == 0.0:
    stringReference.string = "0"
    success = True
  else:
    characterReference = CharacterReference()

    if IsInteger(base):
      success = True

      string =  [None]*int(0.0)

      maximumDigits = GetMaximumDigitsForBase(base)

      digitPosition = GetFirstDigitPosition(decimal, base)

      decimal = round(decimal*base**(maximumDigits - digitPosition - 1.0))

      hasPrintedPoint = False

      if  not isPositive :
        string = AppendCharacter(string, '-')

      # Print leading zeros.
      if digitPosition < 0.0:
        string = AppendCharacter(string, '0')
        string = AppendCharacter(string, '.')
        hasPrintedPoint = True
        i = 0.0
        while i <  -digitPosition - 1.0:
          string = AppendCharacter(string, '0')
          i = i + 1.0
        

      # Print number.
      i = 0.0
      while i < maximumDigits and success:
        d = floor(decimal/base**(maximumDigits - i - 1.0))

        if d >= base:
          d = base - 1.0

        if  not hasPrintedPoint  and digitPosition - i + 1.0 == 0.0:
          if decimal != 0.0:
            string = AppendCharacter(string, '.')
          hasPrintedPoint = True

        if decimal == 0.0 and hasPrintedPoint:
          pass
        else:
          success = GetSingleDigitCharacterFromNumberWithCheck(d, base, characterReference)
          if success:
            c = characterReference.characterValue
            string = AppendCharacter(string, c)

        if success:
          decimal = decimal - d*base**(maximumDigits - i - 1.0)
        i = i + 1.0
      

      if success:
        # Print trailing zeros.
        i = 0.0
        while i < digitPosition - maximumDigits + 1.0:
          string = AppendCharacter(string, '0')
          i = i + 1.0
        

        stringReference.string = string
    else:
      success = False

  # Done
  return success

def GetMaximumDigitsForBase(base):

  t = 10.0**15.0
  return floor(log10(t)/log10(base))

def GetFirstDigitPosition(decimal, base):

  power = ceil(log10(decimal)/log10(base))

  t = decimal*base**( -power)
  if t < base and t >= 1.0:
    pass
  elif t >= base:
    power = power + 1.0
  elif t < 1.0:
    power = power - 1.0

  return power

def GetSingleDigitCharacterFromNumberWithCheck(c, base, characterReference):

  numberTable = GetDigitCharacterTable()

  if c < base or c < len(numberTable):
    success = True
    characterReference.characterValue = numberTable[int(c)]
  else:
    success = False

  return success

def GetDigitCharacterTable():

  numberTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"

  return numberTable

def CreateNumberFromDecimalStringWithCheck(string, decimalReference, errorMessage):
  return CreateNumberFromStringWithCheck(string, 10.0, decimalReference, errorMessage)

def CreateNumberFromDecimalString(string):

  doubleReference = CreateNumberReference(0.0)
  stringReference = CreateStringReference("")
  CreateNumberFromStringWithCheck(string, 10.0, doubleReference, stringReference)
  number = doubleReference.numberValue

  doubleReference = None
  stringReference = None

  return number

def CreateNumberFromStringWithCheck(string, base, numberReference, errorMessage):

  numberIsPositive = CreateBooleanReference(True)
  exponentIsPositive = CreateBooleanReference(True)
  beforePoint = NumberArrayReference()
  afterPoint = NumberArrayReference()
  exponent = NumberArrayReference()

  if base >= 2.0 and base <= 36.0:
    success = ExtractPartsFromNumberString(string, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessage)

    if success:
      numberReference.numberValue = CreateNumberFromParts(base, numberIsPositive.booleanValue, beforePoint.numberArray, afterPoint.numberArray, exponentIsPositive.booleanValue, exponent.numberArray)
  else:
    success = False
    errorMessage.string = "Base must be from 2 to 36."

  return success

def CreateNumberFromParts(base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent):

  n = 0.0

  i = 0.0
  while i < len(beforePoint):
    p = beforePoint[int(len(beforePoint) - i - 1.0)]

    n = n + p*base**i
    i = i + 1.0
  

  i = 0.0
  while i < len(afterPoint):
    p = afterPoint[int(i)]

    n = n + p*base**( -(i + 1.0))
    i = i + 1.0
  

  if len(exponent) > 0.0:
    e = 0.0
    i = 0.0
    while i < len(exponent):
      p = exponent[int(len(exponent) - i - 1.0)]

      e = e + p*base**i
      i = i + 1.0
    

    if  not exponentIsPositive :
      e =  -e

    n = n*base**e

  if  not numberIsPositive :
    n =  -n

  return n

def ExtractPartsFromNumberString(n, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages):

  i = 0.0

  if i < len(n):
    if n[int(i)] == '-':
      numberIsPositive.booleanValue = False
      i = i + 1.0
    elif n[int(i)] == '+':
      numberIsPositive.booleanValue = True
      i = i + 1.0

    success = ExtractPartsFromNumberStringFromSign(n, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages)
  else:
    success = False
    errorMessages.string = "Number cannot have length zero."

  return success

def ExtractPartsFromNumberStringFromSign(n, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages):

  done = False
  count = 0.0
  while i + count < len(n) and  not done :
    if CharacterIsNumberCharacterInBase(n[int(i + count)], base):
      count = count + 1.0
    else:
      done = True
  

  if count >= 1.0:
    beforePoint.numberArray =  [None]*int(count)

    j = 0.0
    while j < count:
      beforePoint.numberArray[int(j)] = GetNumberFromNumberCharacterForBase(n[int(i + j)], base)
      j = j + 1.0
    

    i = i + count

    if i < len(n):
      success = ExtractPartsFromNumberStringFromPointOrExponent(n, base, i, afterPoint, exponentIsPositive, exponent, errorMessages)
    else:
      afterPoint.numberArray =  [None]*int(0.0)
      exponent.numberArray =  [None]*int(0.0)
      success = True
  else:
    success = False
    errorMessages.string = "Number must have at least one number after the optional sign."

  return success

def ExtractPartsFromNumberStringFromPointOrExponent(n, base, i, afterPoint, exponentIsPositive, exponent, errorMessages):

  if n[int(i)] == '.':
    i = i + 1.0

    if i < len(n):
      done = False
      count = 0.0
      while i + count < len(n) and  not done :
        if CharacterIsNumberCharacterInBase(n[int(i + count)], base):
          count = count + 1.0
        else:
          done = True
      

      if count >= 1.0:
        afterPoint.numberArray =  [None]*int(count)

        j = 0.0
        while j < count:
          afterPoint.numberArray[int(j)] = GetNumberFromNumberCharacterForBase(n[int(i + j)], base)
          j = j + 1.0
        

        i = i + count

        if i < len(n):
          success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages)
        else:
          exponent.numberArray =  [None]*int(0.0)
          success = True
      else:
        success = False
        errorMessages.string = "There must be at least one digit after the decimal point."
    else:
      success = False
      errorMessages.string = "There must be at least one digit after the decimal point."
  elif base <= 14.0 and (n[int(i)] == 'e' or n[int(i)] == 'E'):
    if i < len(n):
      success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages)
      afterPoint.numberArray =  [None]*int(0.0)
    else:
      success = False
      errorMessages.string = "There must be at least one digit after the exponent."
  else:
    success = False
    errorMessages.string = "Expected decimal point or exponent symbol."

  return success

def ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages):

  if base <= 14.0 and (n[int(i)] == 'e' or n[int(i)] == 'E'):
    i = i + 1.0

    if i < len(n):
      if n[int(i)] == '-':
        exponentIsPositive.booleanValue = False
        i = i + 1.0
      elif n[int(i)] == '+':
        exponentIsPositive.booleanValue = True
        i = i + 1.0

      if i < len(n):
        done = False
        count = 0.0
        while i + count < len(n) and  not done :
          if CharacterIsNumberCharacterInBase(n[int(i + count)], base):
            count = count + 1.0
          else:
            done = True
        

        if count >= 1.0:
          exponent.numberArray =  [None]*int(count)

          j = 0.0
          while j < count:
            exponent.numberArray[int(j)] = GetNumberFromNumberCharacterForBase(n[int(i + j)], base)
            j = j + 1.0
          

          i = i + count

          if i == len(n):
            success = True
          else:
            success = False
            errorMessages.string = "There cannot be any characters past the exponent of the number."
        else:
          success = False
          errorMessages.string = "There must be at least one digit after the decimal point."
      else:
        success = False
        errorMessages.string = "There must be at least one digit after the exponent symbol."
    else:
      success = False
      errorMessages.string = "There must be at least one digit after the exponent symbol."
  else:
    success = False
    errorMessages.string = "Expected exponent symbol."

  return success

def GetNumberFromNumberCharacterForBase(c, base):

  numberTable = GetDigitCharacterTable()
  position = 0.0

  i = 0.0
  while i < base:
    if numberTable[int(i)] == c:
      position = i
    i = i + 1.0
  

  return position

def CharacterIsNumberCharacterInBase(c, base):

  numberTable = GetDigitCharacterTable()
  found = False

  i = 0.0
  while i < base:
    if numberTable[int(i)] == c:
      found = True
    i = i + 1.0
  

  return found

def StringToNumberArray(str):

  numberArrayReference = NumberArrayReference()
  stringReference = StringReference()

  StringToNumberArrayWithCheck(str, numberArrayReference, stringReference)

  numbers = numberArrayReference.numberArray

  numberArrayReference = None
  stringReference = None

  return numbers

def StringToNumberArrayWithCheck(str, numberArrayReference, errorMessage):

  numberStrings = SplitByString(str, ",")

  numbers =  [None]*int(len(numberStrings))
  success = True
  numberReference = NumberReference()

  i = 0.0
  while i < len(numberStrings):
    numberString = numberStrings[int(i)].string
    trimmedNumberString = Trim(numberString)
    success = CreateNumberFromDecimalStringWithCheck(trimmedNumberString, numberReference, errorMessage)
    numbers[int(i)] = numberReference.numberValue

    FreeStringReference(numberStrings[int(i)])
    trimmedNumberString = None
    i = i + 1.0
  

  numberStrings = None
  numberReference = None

  numberArrayReference.numberArray = numbers

  return success

def Negate(x):
  return  -x

def Positive(x):
  return  +x

def Factorial(x):

  f = 1.0

  i = 2.0
  while i <= x:
    f = f*i
    i = i + 1.0
  

  return f

def Round(x):
  return floor(x + 0.5)

def BankersRound(x):

  if Absolute(x - Truncate(x)) == 0.5:
    if  not DivisibleBy(Round(x), 2.0) :
      r = Round(x) - 1.0
    else:
      r = Round(x)
  else:
    r = Round(x)

  return r

def Ceil(x):
  return ceil(x)

def Floor(x):
  return floor(x)

def Truncate(x):

  if x >= 0.0:
    t = floor(x)
  else:
    t = ceil(x)

  return t

def Absolute(x):
  return fabs(x)

def Logarithm(x):
  return log10(x)

def NaturalLogarithm(x):
  return log(x)

def Sin(x):
  return sin(x)

def Cos(x):
  return cos(x)

def Tan(x):
  return tan(x)

def Asin(x):
  return asin(x)

def Acos(x):
  return acos(x)

def Atan(x):
  return atan(x)

def Atan2(y, x):

  # Atan2 is an invalid operation when x = 0 and y = 0, but this method does not return errors.
  a = 0.0

  if x > 0.0:
    a = Atan(y/x)
  elif x < 0.0 and y >= 0.0:
    a = Atan(y/x) + pi
  elif x < 0.0 and y < 0.0:
    a = Atan(y/x) - pi
  elif x == 0.0 and y > 0.0:
    a = pi/2.0
  elif x == 0.0 and y < 0.0:
    a =  -pi/2.0

  return a

def Squareroot(x):
  return sqrt(x)

def Exp(x):
  return exp(x)

def DivisibleBy(a, b):
  return ((a % b) == 0.0)

def Combinations(n, k):

  c = 1.0
  j = 1.0
  i = n - k + 1.0

  while i <= n:
    c = c*i
    c = c/j

    i = i + 1.0
    j = j + 1.0
  

  return c

def Permutations(n, k):

  c = 1.0

  i = n - k + 1.0
  while i <= n:
    c = c*i
    i = i + 1.0
  

  return c

def EpsilonCompare(a, b, epsilon):
  return fabs(a - b) < epsilon

def GreatestCommonDivisor(a, b):

  while b != 0.0:
    t = b
    b = a % b
    a = t
  

  return a

def GCDWithSubtraction(a, b):

  if a == 0.0:
    g = b
  else:
    while b != 0.0:
      if a > b:
        a = a - b
      else:
        b = b - a
    

    g = a

  return g

def IsInteger(a):
  return (a - floor(a)) == 0.0

def GreatestCommonDivisorWithCheck(a, b, gcdReference):

  if IsInteger(a) and IsInteger(b):
    gcd = GreatestCommonDivisor(a, b)
    gcdReference.numberValue = gcd
    success = True
  else:
    success = False

  return success

def LeastCommonMultiple(a, b):

  if a > 0.0 and b > 0.0:
    lcm = fabs(a*b)/GreatestCommonDivisor(a, b)
  else:
    lcm = 0.0

  return lcm

def Sign(a):

  if a > 0.0:
    s = 1.0
  elif a < 0.0:
    s =  -1.0
  else:
    s = 0.0

  return s

def Max(a, b):
  return max(a,b)

def Min(a, b):
  return min(a,b)

def Power(a, b):
  return a**b

def Gamma(x):
  return LanczosApproximation(x)

def LogGamma(x):
  return log(Gamma(x))

def LanczosApproximation(z):

  p =  [None]*int(8.0)
  p[int(0.0)] = 676.5203681218851
  p[int(1.0)] =  -1259.1392167224028
  p[int(2.0)] = 771.32342877765313
  p[int(3.0)] =  -176.61502916214059
  p[int(4.0)] = 12.507343278686905
  p[int(5.0)] =  -0.13857109526572012
  p[int(6.0)] = 9.9843695780195716e-6
  p[int(7.0)] = 1.5056327351493116e-7

  if z < 0.5:
    y = pi/(sin(pi*z)*LanczosApproximation(1.0 - z))
  else:
    z = z - 1.0
    x = 0.99999999999980993
    i = 0.0
    while i < len(p):
      x = x + p[int(i)]/(z + i + 1.0)
      i = i + 1.0
    
    t = z + len(p) - 0.5
    y = sqrt(2.0*pi)*t**(z + 0.5)*exp( -t)*x

  return y

def Beta(x, y):
  return Gamma(x)*Gamma(y)/Gamma(x + y)

def Sinh(x):
  return (exp(x) - exp( -x))/2.0

def Cosh(x):
  return (exp(x) + exp( -x))/2.0

def Tanh(x):
  return Sinh(x)/Cosh(x)

def Cot(x):
  return 1.0/tan(x)

def Sec(x):
  return 1.0/cos(x)

def Csc(x):
  return 1.0/sin(x)

def Coth(x):
  return Cosh(x)/Sinh(x)

def Sech(x):
  return 1.0/Cosh(x)

def Csch(x):
  return 1.0/Sinh(x)

def Error(x):

  if x == 0.0:
    y = 0.0
  elif x < 0.0:
    y =  -Error( -x)
  else:
    c1 =  -1.26551223
    c2 =  +1.00002368
    c3 =  +0.37409196
    c4 =  +0.09678418
    c5 =  -0.18628806
    c6 =  +0.27886807
    c7 =  -1.13520398
    c8 =  +1.48851587
    c9 =  -0.82215223
    c10 =  +0.17087277

    t = 1.0/(1.0 + 0.5*fabs(x))

    tau = t*exp( -x**2.0 + c1 + t*(c2 + t*(c3 + t*(c4 + t*(c5 + t*(c6 + t*(c7 + t*(c8 + t*(c9 + t*c10)))))))))

    y = 1.0 - tau

  return y

def ErrorInverse(x):

  a = (8.0*(pi - 3.0))/(3.0*pi*(4.0 - pi))

  t = 2.0/(pi*a) + log(1.0 - x**2.0)/2.0
  y = Sign(x)*sqrt(sqrt(t**2.0 - log(1.0 - x**2.0)/a) - t)

  return y

def FallingFactorial(x, n):

  y = 1.0

  k = 0.0
  while k <= n - 1.0:
    y = y*(x - k)
    k = k + 1.0
  

  return y

def RisingFactorial(x, n):

  y = 1.0

  k = 0.0
  while k <= n - 1.0:
    y = y*(x + k)
    k = k + 1.0
  

  return y

def Hypergeometric(a, b, c, z, maxIterations, precision):

  if fabs(z) >= 0.5:
    y = (1.0 - z)**( -a)*HypergeometricDirect(a, c - b, c, z/(z - 1.0), maxIterations, precision)
  else:
    y = HypergeometricDirect(a, b, c, z, maxIterations, precision)

  return y

def HypergeometricDirect(a, b, c, z, maxIterations, precision):

  y = 0.0
  done = False

  n = 0.0
  while n < maxIterations and  not done :
    yp = RisingFactorial(a, n)*RisingFactorial(b, n)/RisingFactorial(c, n)*z**n/Factorial(n)
    if fabs(yp) < precision:
      done = True
    y = y + yp
    n = n + 1.0
  

  return y

def BernouilliNumber(n):
  return AkiyamaTanigawaAlgorithm(n)

def AkiyamaTanigawaAlgorithm(n):

  A =  [None]*int(n + 1.0)

  m = 0.0
  while m <= n:
    A[int(m)] = 1.0/(m + 1.0)
    j = m
    while j >= 1.0:
      A[int(j - 1.0)] = j*(A[int(j - 1.0)] - A[int(j)])
      j = j - 1.0
    
    m = m + 1.0
  

  B = A[int(0.0)]

  A = None

  return B

def aStringToNumberArray(string):

  array =  [None]*int(len(string))

  i = 0.0
  while i < len(string):
    array[int(i)] = ord(string[int(i)])
    i = i + 1.0
  
  return array

def aNumberArrayToString(array):

  string =  [None]*int(len(array))

  i = 0.0
  while i < len(array):
    string[int(i)] = unichr(int(array[int(i)]))
    i = i + 1.0
  
  return string

def aNumberArraysEqual(a, b):

  equal = True
  if len(a) == len(b):
    i = 0.0
    while i < len(a) and equal:
      if a[int(i)] != b[int(i)]:
        equal = False
      i = i + 1.0
    
  else:
    equal = False

  return equal

def aBooleanArraysEqual(a, b):

  equal = True
  if len(a) == len(b):
    i = 0.0
    while i < len(a) and equal:
      if a[int(i)] != b[int(i)]:
        equal = False
      i = i + 1.0
    
  else:
    equal = False

  return equal

def aStringsEqual(a, b):

  equal = True
  if len(a) == len(b):
    i = 0.0
    while i < len(a) and equal:
      if a[int(i)] != b[int(i)]:
        equal = False
      i = i + 1.0
    
  else:
    equal = False

  return equal

def aFillNumberArray(a, value):

  i = 0.0
  while i < len(a):
    a[int(i)] = value
    i = i + 1.0
  

def aFillString(a, value):

  i = 0.0
  while i < len(a):
    a[int(i)] = value
    i = i + 1.0
  

def aFillBooleanArray(a, value):

  i = 0.0
  while i < len(a):
    a[int(i)] = value
    i = i + 1.0
  

def aFillNumberArrayRange(a, value, fromx, to):

  if fromx >= 0.0 and fromx <= len(a) and to >= 0.0 and to <= len(a) and fromx <= to:
    length = to - fromx
    i = 0.0
    while i < length:
      a[int(fromx + i)] = value
      i = i + 1.0
    

    success = True
  else:
    success = False

  return success

def aFillBooleanArrayRange(a, value, fromx, to):

  if fromx >= 0.0 and fromx <= len(a) and to >= 0.0 and to <= len(a) and fromx <= to:
    length = to - fromx
    i = 0.0
    while i < length:
      a[int(fromx + i)] = value
      i = i + 1.0
    

    success = True
  else:
    success = False

  return success

def aFillStringRange(a, value, fromx, to):

  if fromx >= 0.0 and fromx <= len(a) and to >= 0.0 and to <= len(a) and fromx <= to:
    length = to - fromx
    i = 0.0
    while i < length:
      a[int(fromx + i)] = value
      i = i + 1.0
    

    success = True
  else:
    success = False

  return success

def aCopyNumberArray(a):

  n =  [None]*int(len(a))

  i = 0.0
  while i < len(a):
    n[int(i)] = a[int(i)]
    i = i + 1.0
  

  return n

def aCopyBooleanArray(a):

  n =  [None]*int(len(a))

  i = 0.0
  while i < len(a):
    n[int(i)] = a[int(i)]
    i = i + 1.0
  

  return n

def aCopyString(a):

  n =  [None]*int(len(a))

  i = 0.0
  while i < len(a):
    n[int(i)] = a[int(i)]
    i = i + 1.0
  

  return n

def aCopyNumberArrayRange(a, fromx, to, copyReference):

  if fromx >= 0.0 and fromx <= len(a) and to >= 0.0 and to <= len(a) and fromx <= to:
    length = to - fromx
    n =  [None]*int(length)

    i = 0.0
    while i < length:
      n[int(i)] = a[int(fromx + i)]
      i = i + 1.0
    

    copyReference.numberArray = n
    success = True
  else:
    success = False

  return success

def aCopyBooleanArrayRange(a, fromx, to, copyReference):

  if fromx >= 0.0 and fromx <= len(a) and to >= 0.0 and to <= len(a) and fromx <= to:
    length = to - fromx
    n =  [None]*int(length)

    i = 0.0
    while i < length:
      n[int(i)] = a[int(fromx + i)]
      i = i + 1.0
    

    copyReference.booleanArray = n
    success = True
  else:
    success = False

  return success

def aCopyStringRange(a, fromx, to, copyReference):

  if fromx >= 0.0 and fromx <= len(a) and to >= 0.0 and to <= len(a) and fromx <= to:
    length = to - fromx
    n =  [None]*int(length)

    i = 0.0
    while i < length:
      n[int(i)] = a[int(fromx + i)]
      i = i + 1.0
    

    copyReference.string = n
    success = True
  else:
    success = False

  return success

def aIsLastElement(length, index):
  return index + 1.0 == length

def aCreateNumberArray(length, value):

  array =  [None]*int(length)
  aFillNumberArray(array, value)

  return array

def aCreateBooleanArray(length, value):

  array =  [None]*int(length)
  aFillBooleanArray(array, value)

  return array

def aCreateString(length, value):

  array =  [None]*int(length)
  aFillString(array, value)

  return array

def aSwapElementsOfNumberArray(A, ai, bi):

  tmp = A[int(ai)]
  A[int(ai)] = A[int(bi)]
  A[int(bi)] = tmp

def aSwapElementsOfStringArray(A, ai, bi):

  tmp = A.stringArray[int(ai)]
  A.stringArray[int(ai)] = A.stringArray[int(bi)]
  A.stringArray[int(bi)] = tmp

def aReverseNumberArray(array):

  i = 0.0
  while i < len(array)/2.0:
    aSwapElementsOfNumberArray(array, i, len(array) - i - 1.0)
    i = i + 1.0
  

class RGBA:
  r = None
  g = None
  b = None
  a = None

class RGBABitmap:
  y = None

class RGBABitmapImage:
  x = None

class BooleanArrayReference:
  booleanArray = None

class BooleanReference:
  booleanValue = None

class CharacterReference:
  characterValue = None

class NumberArrayReference:
  numberArray = None

class NumberReference:
  numberValue = None

class StringArrayReference:
  stringArray = None

class StringReference:
  string = None

def CreateBooleanReference(value):

  ref = BooleanReference()
  ref.booleanValue = value

  return ref

def CreateBooleanArrayReference(value):

  ref = BooleanArrayReference()
  ref.booleanArray = value

  return ref

def CreateBooleanArrayReferenceLengthValue(length, value):

  ref = BooleanArrayReference()
  ref.booleanArray =  [None]*int(length)

  i = 0.0
  while i < length:
    ref.booleanArray[int(i)] = value
    i = i + 1.0
  

  return ref

def FreeBooleanArrayReference(booleanArrayReference):
  booleanArrayReference.booleanArray = None
  booleanArrayReference = None

def CreateCharacterReference(value):

  ref = CharacterReference()
  ref.characterValue = value

  return ref

def CreateNumberReference(value):

  ref = NumberReference()
  ref.numberValue = value

  return ref

def CreateNumberArrayReference(value):

  ref = NumberArrayReference()
  ref.numberArray = value

  return ref

def CreateNumberArrayReferenceLengthValue(length, value):

  ref = NumberArrayReference()
  ref.numberArray =  [None]*int(length)

  i = 0.0
  while i < length:
    ref.numberArray[int(i)] = value
    i = i + 1.0
  

  return ref

def FreeNumberArrayReference(numberArrayReference):
  numberArrayReference.numberArray = None
  numberArrayReference = None

def CreateStringReference(value):

  ref = StringReference()
  ref.string = value

  return ref

def CreateStringReferenceLengthValue(length, value):

  ref = StringReference()
  ref.string =  [None]*int(length)

  i = 0.0
  while i < length:
    ref.string[int(i)] = value
    i = i + 1.0
  

  return ref

def FreeStringReference(stringReference):
  stringReference.string = None
  stringReference = None

def CreateStringArrayReference(strings):

  ref = StringArrayReference()
  ref.stringArray = strings

  return ref

def CreateStringArrayReferenceLengthValue(length, value):

  ref = StringArrayReference()
  ref.stringArray =  [None]*int(length)

  i = 0.0
  while i < length:
    ref.stringArray[int(i)] = CreateStringReference(value)
    i = i + 1.0
  

  return ref

def FreeStringArrayReference(stringArrayReference):

  i = 0.0
  while i < len(stringArrayReference.stringArray):
    stringArrayReference.stringArray[int(i)] = None
    i = i + 1.0
  
  stringArrayReference.stringArray = None
  stringArrayReference = None
  

def GetPixelFontData():
  return "0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000"

def DrawAsciiCharacter(image, topx, topy, a, color):

  rowReference = NumberReference()
  errorMessage = StringReference()

  index = ord(a)
  index = index - 32.0
  allCharData = GetPixelFontData()
  charData = Substring(allCharData, index*2.0*13.0, (index + 1.0)*2.0*13.0)

  y = 0.0
  while y < 13.0:
    rowData = Substring(charData, y*2.0, (y + 1.0)*2.0)
    ToUpperCase(rowData)
    CreateNumberFromStringWithCheck(rowData, 16.0, rowReference, errorMessage)
    row = rowReference.numberValue
    x = 0.0
    while x < 8.0:
      pixel = floor(row/2.0**x) % 2.0
      if pixel == 1.0:
        DrawPixel(image, topx + 8.0 - 1.0 - x, topy + 13.0 - 1.0 - y, color)
      x = x + 1.0
    
    y = y + 1.0
  

def GetTextWidth(text):

  charWidth = 8.0
  spacing = 2.0

  if len(text) == 0.0:
    width = 0.0
  else:
    width = len(text)*charWidth + (len(text) - 1.0)*spacing

  return width

def GetTextHeight(text):
  return 13.0

def AssertFalse(b, failures):
  if b:
    failures.numberValue = failures.numberValue + 1.0

def AssertTrue(b, failures):
  if  not b :
    failures.numberValue = failures.numberValue + 1.0

def AssertEquals(a, b, failures):
  if a != b:
    failures.numberValue = failures.numberValue + 1.0

def AssertBooleansEqual(a, b, failures):
  if a != b:
    failures.numberValue = failures.numberValue + 1.0

def AssertCharactersEqual(a, b, failures):
  if a != b:
    failures.numberValue = failures.numberValue + 1.0

def AssertStringEquals(a, b, failures):
  if  not aStringsEqual(a, b) :
    failures.numberValue = failures.numberValue + 1.0

def AssertNumberArraysEqual(a, b, failures):

  if len(a) == len(b):
    i = 0.0
    while i < len(a):
      AssertEquals(a[int(i)], b[int(i)], failures)
      i = i + 1.0
    
  else:
    failures.numberValue = failures.numberValue + 1.0

def AssertBooleanArraysEqual(a, b, failures):

  if len(a) == len(b):
    i = 0.0
    while i < len(a):
      AssertBooleansEqual(a[int(i)], b[int(i)], failures)
      i = i + 1.0
    
  else:
    failures.numberValue = failures.numberValue + 1.0

def AssertStringArraysEqual(a, b, failures):

  if len(a) == len(b):
    i = 0.0
    while i < len(a):
      AssertStringEquals(a[int(i)].string, b[int(i)].string, failures)
      i = i + 1.0
    
  else:
    failures.numberValue = failures.numberValue + 1.0

class Chunk:
  length = None
  type = None
  data = None
  crc = None

class IHDR:
  Width = None
  Height = None
  BitDepth = None
  ColourType = None
  CompressionMethod = None
  FilterMethod = None
  InterlaceMethod = None

class PHYS:
  pixelsPerMeter = None

class PNGImage:
  signature = None
  ihdr = None
  zlibStruct = None
  physPresent = None
  phys = None

def ConvertToPNG(image):
  return ConvertToPNGWithOptions(image, 6.0, False, 0.0, 0.001)

def ConvertToPNGGrayscale(image):
  return ConvertToPNGWithOptions(image, 0.0, False, 0.0, 0.001)

def PysicsHeader(pixelsPerMeter):

  phys = PHYS()

  phys.pixelsPerMeter = pixelsPerMeter

  return phys

def ConvertToPNGWithOptions(image, colorType, setPhys, pixelsPerMeter, compressionLevel):

  png = PNGImage()

  png.signature = PNGSignature()

  png.ihdr = PNGHeader(image, colorType)

  png.physPresent = setPhys
  png.phys = PysicsHeader(pixelsPerMeter)

  if colorType == 6.0:
    colorData = GetPNGColorData(image)
  else:
    colorData = GetPNGColorDataGreyscale(image)
  png.zlibStruct = ZLibCompressStaticHuffman(colorData, compressionLevel)

  pngData = PNGSerializeChunks(png)

  return pngData

def PNGSerializeChunks(png):

  length = len(png.signature) + 12.0 + PNGHeaderLength() + 12.0 + PNGIDATLength(png) + 12.0
  if png.physPresent:
    length = length + 4.0 + 4.0 + 1.0 + 12.0
  data =  [None]*int(length)
  position = CreateNumberReference(0.0)

  # Signature
  i = 0.0
  while i < len(png.signature):
    WriteByte(data, png.signature[int(i)], position)
    i = i + 1.0
  

  # Header
  chunkLength = PNGHeaderLength()
  Write4BytesBE(data, chunkLength, position)
  WriteStringBytes(data, "IHDR", position)
  Write4BytesBE(data, png.ihdr.Width, position)
  Write4BytesBE(data, png.ihdr.Height, position)
  WriteByte(data, png.ihdr.BitDepth, position)
  WriteByte(data, png.ihdr.ColourType, position)
  WriteByte(data, png.ihdr.CompressionMethod, position)
  WriteByte(data, png.ihdr.FilterMethod, position)
  WriteByte(data, png.ihdr.InterlaceMethod, position)
  Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4.0, chunkLength + 4.0), position)

  # pHYs
  if png.physPresent:
    chunkLength = 4.0 + 4.0 + 1.0
    Write4BytesBE(data, chunkLength, position)
    WriteStringBytes(data, "pHYs", position)

    Write4BytesBE(data, png.phys.pixelsPerMeter, position)
    Write4BytesBE(data, png.phys.pixelsPerMeter, position)
    WriteByte(data, 1.0, position)
    # 1 = pixels per meter
    Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4.0, chunkLength + 4.0), position)

  # IDAT
  chunkLength = PNGIDATLength(png)
  Write4BytesBE(data, chunkLength, position)
  WriteStringBytes(data, "IDAT", position)
  WriteByte(data, png.zlibStruct.CMF, position)
  WriteByte(data, png.zlibStruct.FLG, position)
  i = 0.0
  while i < len(png.zlibStruct.CompressedDataBlocks):
    WriteByte(data, png.zlibStruct.CompressedDataBlocks[int(i)], position)
    i = i + 1.0
  
  Write4BytesBE(data, png.zlibStruct.Adler32CheckValue, position)
  Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4.0, chunkLength + 4.0), position)

  # IEND
  chunkLength = 0.0
  Write4BytesBE(data, chunkLength, position)
  WriteStringBytes(data, "IEND", position)
  Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - 4.0, 4.0), position)

  return data

def PNGIDATLength(png):
  return 2.0 + len(png.zlibStruct.CompressedDataBlocks) + 4.0

def PNGHeaderLength():
  return 4.0 + 4.0 + 1.0 + 1.0 + 1.0 + 1.0 + 1.0

def GetPNGColorData(image):

  length = 4.0*ImageWidth(image)*ImageHeight(image) + ImageHeight(image)

  colordata =  [None]*int(length)

  next = 0.0

  y = 0.0
  while y < ImageHeight(image):
    colordata[int(next)] = 0.0
    next = next + 1.0
    x = 0.0
    while x < ImageWidth(image):
      rgba = image.x[int(x)].y[int(y)]
      colordata[int(next)] = Round(rgba.r*255.0)
      next = next + 1.0
      colordata[int(next)] = Round(rgba.g*255.0)
      next = next + 1.0
      colordata[int(next)] = Round(rgba.b*255.0)
      next = next + 1.0
      colordata[int(next)] = Round(rgba.a*255.0)
      next = next + 1.0
      x = x + 1.0
    
    y = y + 1.0
  

  return colordata

def GetPNGColorDataGreyscale(image):

  length = ImageWidth(image)*ImageHeight(image) + ImageHeight(image)

  colordata =  [None]*int(length)

  next = 0.0

  y = 0.0
  while y < ImageHeight(image):
    colordata[int(next)] = 0.0
    next = next + 1.0
    x = 0.0
    while x < ImageWidth(image):
      rgba = image.x[int(x)].y[int(y)]
      colordata[int(next)] = Round(rgba.r*255.0)
      next = next + 1.0
      x = x + 1.0
    
    y = y + 1.0
  

  return colordata

def PNGHeader(image, colortype):

  ihdr = IHDR()
  ihdr.Width = ImageWidth(image)
  ihdr.Height = ImageHeight(image)
  # Truecolour with alpha
  ihdr.BitDepth = 8.0
  ihdr.ColourType = colortype
  ihdr.FilterMethod = 0.0
  # None
  ihdr.CompressionMethod = 0.0
  # zlib
  ihdr.InterlaceMethod = 0.0
  # no interlace
  return ihdr

def PNGSignature():

  s =  [None]*int(8.0)
  s[int(0.0)] = 137.0
  s[int(1.0)] = 80.0
  s[int(2.0)] = 78.0
  s[int(3.0)] = 71.0
  s[int(4.0)] = 13.0
  s[int(5.0)] = 10.0
  s[int(6.0)] = 26.0
  s[int(7.0)] = 10.0

  return s

def PNGReadDataChunks(cs):

  length = 0.0
  i = 0.0
  while i < len(cs):
    c = cs[int(i)]
    if aStringsEqual(c.type, "IDAT"):
      length = length + c.length
    i = i + 1.0
  

  zlibData =  [None]*int(length)
  zlibpos = 0.0

  i = 0.0
  while i < len(cs):
    c = cs[int(i)]
    if aStringsEqual(c.type, "IDAT"):
      j = 0.0
      while j < c.length:
        zlibData[int(zlibpos)] = c.data[int(j)]
        zlibpos = zlibpos + 1.0
        j = j + 1.0
      
    i = i + 1.0
  

  return zlibData

def PNGReadHeader(image, cs, errorMessages):

  position = CreateNumberReference(0.0)
  success = False

  i = 0.0
  while i < len(cs):
    c = cs[int(i)]
    if aStringsEqual(c.type, "IHDR"):
      ihdr = IHDR()

      ihdr.Width = Read4bytesBE(c.data, position)
      ihdr.Height = Read4bytesBE(c.data, position)
      ihdr.BitDepth = ReadByte(c.data, position)
      ihdr.ColourType = ReadByte(c.data, position)
      ihdr.CompressionMethod = ReadByte(c.data, position)
      ihdr.FilterMethod = ReadByte(c.data, position)
      ihdr.InterlaceMethod = ReadByte(c.data, position)

      n = CreateImage(ihdr.Width, ihdr.Height, GetTransparent())
      image.x = n.x

      if ihdr.ColourType == 6.0:
        if ihdr.BitDepth == 8.0:
          if ihdr.CompressionMethod == 0.0:
            if ihdr.FilterMethod == 0.0:
              if ihdr.InterlaceMethod == 0.0:
                success = True
              else:
                success = False
                errorMessages.string = AppendString(errorMessages.string, "Interlace method not supported.")
            else:
              success = False
              errorMessages.string = AppendString(errorMessages.string, "Filter method not supported.")
          else:
            success = False
            errorMessages.string = AppendString(errorMessages.string, "Compression type not supported.")
        else:
          success = False
          errorMessages.string = AppendString(errorMessages.string, "Bit depth not supported.")
      else:
        success = False
        errorMessages.string = AppendString(errorMessages.string, "Color type not supported.")
    i = i + 1.0
  

  return success

def PNGReadChunks(data, position):
  done = False
  prepos = position.numberValue
  chunks = 0.0
  while  not done :
    c = PNGReadChunk(data, position)
    if aStringsEqual(c.type, "IEND"):
      done = True
    chunks = chunks + 1.0
  
  position.numberValue = prepos
  cs =  [None]*int(chunks)
  i = 0.0
  while i < chunks:
    cs[int(i)] = PNGReadChunk(data, position)
    i = i + 1.0
  

  return cs

def PNGReadChunk(data, position):

  c = Chunk()

  c.length = Read4bytesBE(data, position)
  c.type =  [None]*int(4.0)
  c.type[int(0.0)] = unichr(int(ReadByte(data, position)))
  c.type[int(1.0)] = unichr(int(ReadByte(data, position)))
  c.type[int(2.0)] = unichr(int(ReadByte(data, position)))
  c.type[int(3.0)] = unichr(int(ReadByte(data, position)))
  c.data = ReadXbytes(data, position, c.length)
  c.crc = Read4bytesBE(data, position)

  return c

def WriteStringToStingStream(stream, index, src):

  i = 0.0
  while i < len(src):
    stream[int(index.numberValue + i)] = src[int(i)]
    i = i + 1.0
  
  index.numberValue = index.numberValue + len(src)

def WriteCharacterToStingStream(stream, index, src):
  stream[int(index.numberValue)] = src
  index.numberValue = index.numberValue + 1.0

def WriteBooleanToStingStream(stream, index, src):
  if src:
    WriteStringToStingStream(stream, index, "true")
  else:
    WriteStringToStingStream(stream, index, "false")

def SubstringWithCheck(string, fromx, to, stringReference):

  if fromx >= 0.0 and fromx <= len(string) and to >= 0.0 and to <= len(string) and fromx <= to:
    stringReference.string = Substring(string, fromx, to)
    success = True
  else:
    success = False

  return success

def Substring(string, fromx, to):

  length = to - fromx

  n =  [None]*int(length)

  i = fromx
  while i < to:
    n[int(i - fromx)] = string[int(i)]
    i = i + 1.0
  

  return n

def AppendString(s1, s2):

  newString = ConcatenateString(s1, s2)

  s1 = None

  return newString

def ConcatenateString(s1, s2):

  newString =  [None]*int(len(s1) + len(s2))

  i = 0.0
  while i < len(s1):
    newString[int(i)] = s1[int(i)]
    i = i + 1.0
  

  i = 0.0
  while i < len(s2):
    newString[int(len(s1) + i)] = s2[int(i)]
    i = i + 1.0
  

  return newString

def AppendCharacter(string, c):

  newString = ConcatenateCharacter(string, c)

  string = None

  return newString

def ConcatenateCharacter(string, c):
  newString =  [None]*int(len(string) + 1.0)

  i = 0.0
  while i < len(string):
    newString[int(i)] = string[int(i)]
    i = i + 1.0
  

  newString[int(len(string))] = c

  return newString

def SplitByCharacter(toSplit, splitBy):

  stringToSplitBy =  [None]*int(1.0)
  stringToSplitBy[int(0.0)] = splitBy

  split = SplitByString(toSplit, stringToSplitBy)

  stringToSplitBy = None

  return split

def IndexOfCharacter(string, character, indexReference):

  found = False
  i = 0.0
  while i < len(string) and  not found :
    if string[int(i)] == character:
      found = True
      indexReference.numberValue = i
    i = i + 1.0
  

  return found

def SubstringEqualsWithCheck(string, fromx, substring, equalsReference):

  if fromx < len(string):
    success = True
    equalsReference.booleanValue = SubstringEquals(string, fromx, substring)
  else:
    success = False

  return success

def SubstringEquals(string, fromx, substring):

  equal = True
  if len(string) - fromx >= len(substring):
    i = 0.0
    while i < len(substring) and equal:
      if string[int(fromx + i)] != substring[int(i)]:
        equal = False
      i = i + 1.0
    
  else:
    equal = False

  return equal

def IndexOfString(string, substring, indexReference):

  found = False
  i = 0.0
  while i < len(string) - len(substring) + 1.0 and  not found :
    if SubstringEquals(string, i, substring):
      found = True
      indexReference.numberValue = i
    i = i + 1.0
  

  return found

def ContainsCharacter(string, character):

  found = False
  i = 0.0
  while i < len(string) and  not found :
    if string[int(i)] == character:
      found = True
    i = i + 1.0
  

  return found

def ContainsString(string, substring):
  return IndexOfString(string, substring, NumberReference())

def ToUpperCase(string):

  i = 0.0
  while i < len(string):
    string[int(i)] = charToUpperCase(string[int(i)])
    i = i + 1.0
  

def ToLowerCase(string):

  i = 0.0
  while i < len(string):
    string[int(i)] = charToLowerCase(string[int(i)])
    i = i + 1.0
  

def EqualsIgnoreCase(a, b):

  if len(a) == len(b):
    equal = True
    i = 0.0
    while i < len(a) and equal:
      if charToLowerCase(a[int(i)]) != charToLowerCase(b[int(i)]):
        equal = False
      i = i + 1.0
    
  else:
    equal = False

  return equal

def ReplaceString(string, toReplace, replaceWith):

  equalsReference = BooleanReference()
  result =  [None]*int(0.0)

  i = 0.0
  while i < len(string):
    success = SubstringEqualsWithCheck(string, i, toReplace, equalsReference)
    if success:
      success = equalsReference.booleanValue

    if success and len(toReplace) > 0.0:
      result = ConcatenateString(result, replaceWith)
      i = i + len(toReplace)
    else:
      result = ConcatenateCharacter(result, string[int(i)])
      i = i + 1.0
  

  return result

def ReplaceCharacter(string, toReplace, replaceWith):

  result =  [None]*int(0.0)

  i = 0.0
  while i < len(string):
    if string[int(i)] == toReplace:
      result = ConcatenateCharacter(result, replaceWith)
    else:
      result = ConcatenateCharacter(result, string[int(i)])
    i = i + 1.0
  

  return result

def Trim(string):

  # Find whitepaces at the start.
  lastWhitespaceLocationStart =  -1.0
  firstNonWhitespaceFound = False
  i = 0.0
  while i < len(string) and  not firstNonWhitespaceFound :
    if charIsWhiteSpace(string[int(i)]):
      lastWhitespaceLocationStart = i
    else:
      firstNonWhitespaceFound = True
    i = i + 1.0
  

  # Find whitepaces at the end.
  lastWhitespaceLocationEnd = len(string)
  firstNonWhitespaceFound = False
  i = len(string) - 1.0
  while i >= 0.0 and  not firstNonWhitespaceFound :
    if charIsWhiteSpace(string[int(i)]):
      lastWhitespaceLocationEnd = i
    else:
      firstNonWhitespaceFound = True
    i = i - 1.0
  

  if lastWhitespaceLocationStart < lastWhitespaceLocationEnd:
    result = Substring(string, lastWhitespaceLocationStart + 1.0, lastWhitespaceLocationEnd)
  else:
    result =  [None]*int(0.0)

  return result

def StartsWith(string, start):

  startsWithString = False
  if len(string) >= len(start):
    startsWithString = SubstringEquals(string, 0.0, start)

  return startsWithString

def EndsWith(string, end):

  endsWithString = False
  if len(string) >= len(end):
    endsWithString = SubstringEquals(string, len(string) - len(end), end)

  return endsWithString

def SplitByString(toSplit, splitBy):

  split =  [None]*int(0.0)

  next =  [None]*int(0.0)
  i = 0.0
  while i < len(toSplit):
    c = toSplit[int(i)]

    if SubstringEquals(toSplit, i, splitBy):
      n = StringReference()
      n.string = next
      split = AddString(split, n)
      next =  [None]*int(0.0)
      i = i + len(splitBy)
    else:
      next = AppendCharacter(next, c)
      i = i + 1.0
  

  n = StringReference()
  n.string = next
  split = AddString(split, n)

  return split

def StringIsBefore(a, b):

  before = False
  equal = True
  done = False

  if len(a) == 0.0 and len(b) > 0.0:
    before = True
  else:
    i = 0.0
    while i < len(a) and i < len(b) and  not done :
      if a[int(i)] != b[int(i)]:
        equal = False
      if charCharacterIsBefore(a[int(i)], b[int(i)]):
        before = True
      if charCharacterIsBefore(b[int(i)], a[int(i)]):
        done = True
      i = i + 1.0
    

    if equal:
      if len(a) < len(b):
        before = True

  return before

def ReadXbytes(data, position, length):

  r =  [None]*int(length)

  i = 0.0
  while i < length:
    r[int(i)] = ReadByte(data, position)
    i = i + 1.0
  

  return r

def Read4bytesBE(data, position):

  r = 0.0
  r = r + 2.0**24.0*ReadByte(data, position)
  r = r + 2.0**16.0*ReadByte(data, position)
  r = r + 2.0**8.0*ReadByte(data, position)
  r = r + ReadByte(data, position)

  return r

def Read2bytesBE(data, position):

  r = 0.0
  r = r + 2.0**8.0*ReadByte(data, position)
  r = r + ReadByte(data, position)

  return r

def ReadByte(data, position):

  next = data[int(position.numberValue)]
  position.numberValue = position.numberValue + 1.0

  return next

def Read4bytesLE(data, position):

  r = 0.0
  r = r + ReadByte(data, position)
  r = r + 2.0**8.0*ReadByte(data, position)
  r = r + 2.0**16.0*ReadByte(data, position)
  r = r + 2.0**24.0*ReadByte(data, position)

  return r

def WriteByte(data, b, position):
  data[int(position.numberValue)] = b
  position.numberValue = position.numberValue + 1.0

def Write2BytesLE(data, b, position):
  data[int(position.numberValue)] = Round(b % 2.0**8.0)
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = floor(b/2.0**8.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0

def Write4BytesLE(data, b, position):
  data[int(position.numberValue)] = Round(b % 2.0**8.0)
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = floor(b/2.0**8.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = floor(b/2.0**16.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = floor(b/2.0**24.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0

def Write2BytesBE(data, b, position):
  data[int(position.numberValue)] = floor(b/2.0**8.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = Round(b % 2.0**8.0)
  position.numberValue = position.numberValue + 1.0

def Write4BytesBE(data, b, position):
  data[int(position.numberValue)] = floor(b/2.0**24.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = floor(b/2.0**16.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = floor(b/2.0**8.0) % 2.0**8.0
  position.numberValue = position.numberValue + 1.0
  data[int(position.numberValue)] = Round(b % 2.0**8.0)
  position.numberValue = position.numberValue + 1.0

def WriteStringBytes(data, cs, position):

  i = 0.0
  while i < len(cs):
    v = ord(cs[int(i)])
    WriteByte(data, v, position)
    i = i + 1.0
  

def MakeCRC32Table():

  crcTable =  [None]*int(256.0)

  n = 0.0
  while n < 256.0:
    c = n
    k = 0.0
    while k < 8.0:
      if  not DivisibleBy(c, 2.0) :
        c = Xor4Byte(3988292384.0, floor(c/2.0))
      else:
        c = floor(c/2.0)
      k = k + 1.0
    
    crcTable[int(n)] = c
    n = n + 1.0
  

  return crcTable

def UpdateCRC32(crc, buf, crc_table):

  n = 0.0
  while n < len(buf):
    index = And4Byte(Xor4Byte(crc, buf[int(n)]), 2.0**8.0 - 1.0)
    crc = Xor4Byte(crc_table[int(index)], floor(crc/2.0**8.0))
    n = n + 1.0
  

  return crc

def CalculateCRC32(buf):

  crcTable = MakeCRC32Table()

  b32max = 2.0**32.0 - 1.0
  value = UpdateCRC32(b32max, buf, crcTable)

  return Xor4Byte(value, b32max)

def CRC32OfInterval(data, fromx, length):

  crcBase =  [None]*int(length)

  i = 0.0
  while i < length:
    crcBase[int(i)] = data[int(fromx + i)]
    i = i + 1.0
  

  crc = CalculateCRC32(crcBase)

  crcBase = None

  return crc

class ZLIBStruct:
  CMF = None
  CM = None
  CINFO = None
  FLG = None
  FCHECK = None
  FDICT = None
  FLEVEL = None
  CompressedDataBlocks = None
  Adler32CheckValue = None

def ZLibCompressNoCompression(data):

  zlibStruct = ZLIBStruct()

  zlibStruct.CMF = 120.0
  zlibStruct.FLG = 1.0
  zlibStruct.CompressedDataBlocks = DeflateDataNoCompression(data)
  zlibStruct.Adler32CheckValue = ComputeAdler32(data)

  return zlibStruct

def ZLibCompressStaticHuffman(data, level):

  zlibStruct = ZLIBStruct()

  zlibStruct.CMF = 120.0
  zlibStruct.FLG = 1.0
  zlibStruct.CompressedDataBlocks = DeflateDataStaticHuffman(data, level)
  zlibStruct.Adler32CheckValue = ComputeAdler32(data)

  return zlibStruct

class LinkedListNodeStrings:
  end = None
  value = None
  next = None

class LinkedListStrings:
  first = None
  last = None

class LinkedListNodeNumbers:
  next = None
  end = None
  value = None

class LinkedListNumbers:
  first = None
  last = None

class LinkedListCharacters:
  first = None
  last = None

class LinkedListNodeCharacters:
  end = None
  value = None
  next = None

class DynamicArrayNumbers:
  array = None
  length = None

def AddNumber(list, a):

  newlist =  [None]*int(len(list) + 1.0)
  i = 0.0
  while i < len(list):
    newlist[int(i)] = list[int(i)]
    i = i + 1.0
  
  newlist[int(len(list))] = a
		
  list = None
		
  return newlist

def AddNumberRef(list, i):
  list.numberArray = AddNumber(list.numberArray, i)

def RemoveNumber(list, n):

  newlist =  [None]*int(len(list) - 1.0)

  if n >= 0.0 and n < len(list):
    i = 0.0
    while i < len(list):
      if i < n:
        newlist[int(i)] = list[int(i)]
      if i > n:
        newlist[int(i - 1.0)] = list[int(i)]
      i = i + 1.0
    

    list = None
  else:
    newlist = None
		
  return newlist

def GetNumberRef(list, i):
  return list.numberArray[int(i)]

def RemoveNumberRef(list, i):
  list.numberArray = RemoveNumber(list.numberArray, i)

def AddString(list, a):

  newlist =  [None]*int(len(list) + 1.0)

  i = 0.0
  while i < len(list):
    newlist[int(i)] = list[int(i)]
    i = i + 1.0
  
  newlist[int(len(list))] = a
		
  list = None
		
  return newlist

def AddStringRef(list, i):
  list.stringArray = AddString(list.stringArray, i)

def RemoveString(list, n):

  newlist =  [None]*int(len(list) - 1.0)

  if n >= 0.0 and n < len(list):
    i = 0.0
    while i < len(list):
      if i < n:
        newlist[int(i)] = list[int(i)]
      if i > n:
        newlist[int(i - 1.0)] = list[int(i)]
      i = i + 1.0
    

    list = None
  else:
    newlist = None
		
  return newlist

def GetStringRef(list, i):
  return list.stringArray[int(i)]

def RemoveStringRef(list, i):
  list.stringArray = RemoveString(list.stringArray, i)

def AddBoolean(list, a):

  newlist =  [None]*int(len(list) + 1.0)
  i = 0.0
  while i < len(list):
    newlist[int(i)] = list[int(i)]
    i = i + 1.0
  
  newlist[int(len(list))] = a
		
  list = None
		
  return newlist

def AddBooleanRef(list, i):
  list.booleanArray = AddBoolean(list.booleanArray, i)

def RemoveBoolean(list, n):

  newlist =  [None]*int(len(list) - 1.0)

  if n >= 0.0 and n < len(list):
    i = 0.0
    while i < len(list):
      if i < n:
        newlist[int(i)] = list[int(i)]
      if i > n:
        newlist[int(i - 1.0)] = list[int(i)]
      i = i + 1.0
    

    list = None
  else:
    newlist = None
		
  return newlist

def GetBooleanRef(list, i):
  return list.booleanArray[int(i)]

def RemoveDecimalRef(list, i):
  list.booleanArray = RemoveBoolean(list.booleanArray, i)

def CreateLinkedListString():

  ll = LinkedListStrings()
  ll.first = LinkedListNodeStrings()
  ll.last = ll.first
  ll.last.end = True

  return ll

def LinkedListAddString(ll, value):
  ll.last.end = False
  ll.last.value = value
  ll.last.next = LinkedListNodeStrings()
  ll.last.next.end = True
  ll.last = ll.last.next

def LinkedListStringsToArray(ll):

  node = ll.first

  length = LinkedListStringsLength(ll)

  array =  [None]*int(length)

  i = 0.0
  while i < length:
    array[int(i)] = StringReference()
    array[int(i)].string = node.value
    node = node.next
    i = i + 1.0
  

  return array

def LinkedListStringsLength(ll):

  l = 0.0
  node = ll.first
  while  not node.end :
    node = node.next
    l = l + 1.0
  

  return l

def FreeLinkedListString(ll):

  node = ll.first

  while  not node.end :
    prev = node
    node = node.next
    prev = None
  

  node = None

def CreateLinkedListNumbers():

  ll = LinkedListNumbers()
  ll.first = LinkedListNodeNumbers()
  ll.last = ll.first
  ll.last.end = True

  return ll

def CreateLinkedListNumbersArray(length):

  lls =  [None]*int(length)
  i = 0.0
  while i < len(lls):
    lls[int(i)] = CreateLinkedListNumbers()
    i = i + 1.0
  

  return lls

def LinkedListAddNumber(ll, value):
  ll.last.end = False
  ll.last.value = value
  ll.last.next = LinkedListNodeNumbers()
  ll.last.next.end = True
  ll.last = ll.last.next

def LinkedListNumbersLength(ll):

  l = 0.0
  node = ll.first
  while  not node.end :
    node = node.next
    l = l + 1.0
  

  return l

def LinkedListNumbersIndex(ll, index):

  node = ll.first
  i = 0.0
  while i < index:
    node = node.next
    i = i + 1.0
  

  return node.value

def LinkedListInsertNumber(ll, index, value):

  if index == 0.0:
    tmp = ll.first
    ll.first = LinkedListNodeNumbers()
    ll.first.next = tmp
    ll.first.value = value
    ll.first.end = False
  else:
    node = ll.first
    i = 0.0
    while i < index - 1.0:
      node = node.next
      i = i + 1.0
    

    tmp = node.next
    node.next = LinkedListNodeNumbers()
    node.next.next = tmp
    node.next.value = value
    node.next.end = False

def LinkedListSet(ll, index, value):

  node = ll.first
  i = 0.0
  while i < index:
    node = node.next
    i = i + 1.0
  

  node.next.value = value

def LinkedListRemoveNumber(ll, index):

  node = ll.first
  prev = ll.first

  i = 0.0
  while i < index:
    prev = node
    node = node.next
    i = i + 1.0
  

  if index == 0.0:
    ll.first = prev.next
  if  not prev.next.end :
    prev.next = prev.next.next

def FreeLinkedListNumbers(ll):

  node = ll.first

  while  not node.end :
    prev = node
    node = node.next
    prev = None
  

  node = None

def FreeLinkedListNumbersArray(lls):

  i = 0.0
  while i < len(lls):
    FreeLinkedListNumbers(lls[int(i)])
    i = i + 1.0
  
  lls = None

def LinkedListNumbersToArray(ll):

  node = ll.first

  length = LinkedListNumbersLength(ll)

  array =  [None]*int(length)

  i = 0.0
  while i < length:
    array[int(i)] = node.value
    node = node.next
    i = i + 1.0
  

  return array

def ArrayToLinkedListNumbers(array):

  ll = CreateLinkedListNumbers()

  i = 0.0
  while i < len(array):
    LinkedListAddNumber(ll, array[int(i)])
    i = i + 1.0
  

  return ll

def LinkedListNumbersEqual(a, b):

  an = a.first
  bn = b.first

  equal = True
  done = False
  while equal and  not done :
    if an.end == bn.end:
      if an.end:
        done = True
      elif an.value == bn.value:
        an = an.next
        bn = bn.next
      else:
        equal = False
    else:
      equal = False
  

  return equal

def CreateLinkedListCharacter():

  ll = LinkedListCharacters()
  ll.first = LinkedListNodeCharacters()
  ll.last = ll.first
  ll.last.end = True

  return ll

def LinkedListAddCharacter(ll, value):
  ll.last.end = False
  ll.last.value = value
  ll.last.next = LinkedListNodeCharacters()
  ll.last.next.end = True
  ll.last = ll.last.next

def LinkedListCharactersToArray(ll):

  node = ll.first

  length = LinkedListCharactersLength(ll)

  array =  [None]*int(length)

  i = 0.0
  while i < length:
    array[int(i)] = node.value
    node = node.next
    i = i + 1.0
  

  return array

def LinkedListCharactersLength(ll):

  l = 0.0
  node = ll.first
  while  not node.end :
    node = node.next
    l = l + 1.0
  

  return l

def FreeLinkedListCharacter(ll):

  node = ll.first

  while  not node.end :
    prev = node
    node = node.next
    prev = None
  

  node = None

def CreateDynamicArrayNumbers():

  da = DynamicArrayNumbers()
  da.array =  [None]*int(10.0)
  da.length = 0.0

  return da

def CreateDynamicArrayNumbersWithInitialCapacity(capacity):

  da = DynamicArrayNumbers()
  da.array =  [None]*int(capacity)
  da.length = 0.0

  return da

def DynamicArrayAddNumber(da, value):
  if da.length == len(da.array):
    DynamicArrayNumbersIncreaseSize(da)

  da.array[int(da.length)] = value
  da.length = da.length + 1.0

def DynamicArrayNumbersIncreaseSize(da):

  newLength = round(len(da.array)*3.0/2.0)
  newArray =  [None]*int(newLength)

  i = 0.0
  while i < len(da.array):
    newArray[int(i)] = da.array[int(i)]
    i = i + 1.0
  

  da.array = None

  da.array = newArray

def DynamicArrayNumbersDecreaseSizeNecessary(da):

  needsDecrease = False

  if da.length > 10.0:
    needsDecrease = da.length <= round(len(da.array)*2.0/3.0)

  return needsDecrease

def DynamicArrayNumbersDecreaseSize(da):

  newLength = round(len(da.array)*2.0/3.0)
  newArray =  [None]*int(newLength)

  i = 0.0
  while i < newLength:
    newArray[int(i)] = da.array[int(i)]
    i = i + 1.0
  

  da.array = None

  da.array = newArray

def DynamicArrayNumbersIndex(da, index):
  return da.array[int(index)]

def DynamicArrayNumbersLength(da):
  return da.length

def DynamicArrayInsertNumber(da, index, value):

  if da.length == len(da.array):
    DynamicArrayNumbersIncreaseSize(da)

  i = da.length
  while i > index:
    da.array[int(i)] = da.array[int(i - 1.0)]
    i = i - 1.0
  

  da.array[int(index)] = value

  da.length = da.length + 1.0

def DynamicArraySet(da, index, value):
  da.array[int(index)] = value

def DynamicArrayRemoveNumber(da, index):

  i = index
  while i < da.length - 1.0:
    da.array[int(i)] = da.array[int(i + 1.0)]
    i = i + 1.0
  

  da.length = da.length - 1.0

  if DynamicArrayNumbersDecreaseSizeNecessary(da):
    DynamicArrayNumbersDecreaseSize(da)

def FreeDynamicArrayNumbers(da):
  da.array = None
  da = None

def DynamicArrayNumbersToArray(da):

  array =  [None]*int(da.length)

  i = 0.0
  while i < da.length:
    array[int(i)] = da.array[int(i)]
    i = i + 1.0
  

  return array

def ArrayToDynamicArrayNumbersWithOptimalSize(array):

  #
  #         c = 10*(3/2)^n
  #         log(c) = log(10*(3/2)^n)
  #         log(c) = log(10) + log((3/2)^n)
  #         log(c) = 1 + log((3/2)^n)
  #         log(c) - 1 = log((3/2)^n)
  #         log(c) - 1 = n*log(3/2)
  #         n = (log(c) - 1)/log(3/2)
  #        
  c = len(array)
  n = (log(c) - 1.0)/log(3.0/2.0)
  newCapacity = floor(n) + 1.0

  da = CreateDynamicArrayNumbersWithInitialCapacity(newCapacity)

  i = 0.0
  while i < len(array):
    da.array[int(i)] = array[int(i)]
    i = i + 1.0
  

  return da

def ArrayToDynamicArrayNumbers(array):

  da = DynamicArrayNumbers()
  da.array = aCopyNumberArray(array)
  da.length = len(array)

  return da

def DynamicArrayNumbersEqual(a, b):

  equal = True
  if a.length == b.length:
    i = 0.0
    while i < a.length and equal:
      if a.array[int(i)] != b.array[int(i)]:
        equal = False
      i = i + 1.0
    
  else:
    equal = False

  return equal

def DynamicArrayNumbersToLinkedList(da):

  ll = CreateLinkedListNumbers()

  i = 0.0
  while i < da.length:
    LinkedListAddNumber(ll, da.array[int(i)])
    i = i + 1.0
  

  return ll

def LinkedListToDynamicArrayNumbers(ll):

  node = ll.first

  da = DynamicArrayNumbers()
  da.length = LinkedListNumbersLength(ll)

  da.array =  [None]*int(da.length)

  i = 0.0
  while i < da.length:
    da.array[int(i)] = node.value
    node = node.next
    i = i + 1.0
  

  return da

def AddCharacter(list, a):

  newlist =  [None]*int(len(list) + 1.0)
  i = 0.0
  while i < len(list):
    newlist[int(i)] = list[int(i)]
    i = i + 1.0
  
  newlist[int(len(list))] = a
		
  list = None
		
  return newlist

def AddCharacterRef(list, i):
  list.string = AddCharacter(list.string, i)

def RemoveCharacter(list, n):

  newlist =  [None]*int(len(list) - 1.0)

  if n >= 0.0 and n < len(list):
    i = 0.0
    while i < len(list):
      if i < n:
        newlist[int(i)] = list[int(i)]
      if i > n:
        newlist[int(i - 1.0)] = list[int(i)]
      i = i + 1.0
    

    list = None
  else:
    newlist = None

  return newlist

def GetCharacterRef(list, i):
  return list.string[int(i)]

def RemoveCharacterRef(list, i):
  list.string = RemoveCharacter(list.string, i)

def charToLowerCase(character):

  toReturn = character
  if character == 'A':
    toReturn = 'a'
  elif character == 'B':
    toReturn = 'b'
  elif character == 'C':
    toReturn = 'c'
  elif character == 'D':
    toReturn = 'd'
  elif character == 'E':
    toReturn = 'e'
  elif character == 'F':
    toReturn = 'f'
  elif character == 'G':
    toReturn = 'g'
  elif character == 'H':
    toReturn = 'h'
  elif character == 'I':
    toReturn = 'i'
  elif character == 'J':
    toReturn = 'j'
  elif character == 'K':
    toReturn = 'k'
  elif character == 'L':
    toReturn = 'l'
  elif character == 'M':
    toReturn = 'm'
  elif character == 'N':
    toReturn = 'n'
  elif character == 'O':
    toReturn = 'o'
  elif character == 'P':
    toReturn = 'p'
  elif character == 'Q':
    toReturn = 'q'
  elif character == 'R':
    toReturn = 'r'
  elif character == 'S':
    toReturn = 's'
  elif character == 'T':
    toReturn = 't'
  elif character == 'U':
    toReturn = 'u'
  elif character == 'V':
    toReturn = 'v'
  elif character == 'W':
    toReturn = 'w'
  elif character == 'X':
    toReturn = 'x'
  elif character == 'Y':
    toReturn = 'y'
  elif character == 'Z':
    toReturn = 'z'

  return toReturn

def charToUpperCase(character):

  toReturn = character
  if character == 'a':
    toReturn = 'A'
  elif character == 'b':
    toReturn = 'B'
  elif character == 'c':
    toReturn = 'C'
  elif character == 'd':
    toReturn = 'D'
  elif character == 'e':
    toReturn = 'E'
  elif character == 'f':
    toReturn = 'F'
  elif character == 'g':
    toReturn = 'G'
  elif character == 'h':
    toReturn = 'H'
  elif character == 'i':
    toReturn = 'I'
  elif character == 'j':
    toReturn = 'J'
  elif character == 'k':
    toReturn = 'K'
  elif character == 'l':
    toReturn = 'L'
  elif character == 'm':
    toReturn = 'M'
  elif character == 'n':
    toReturn = 'N'
  elif character == 'o':
    toReturn = 'O'
  elif character == 'p':
    toReturn = 'P'
  elif character == 'q':
    toReturn = 'Q'
  elif character == 'r':
    toReturn = 'R'
  elif character == 's':
    toReturn = 'S'
  elif character == 't':
    toReturn = 'T'
  elif character == 'u':
    toReturn = 'U'
  elif character == 'v':
    toReturn = 'V'
  elif character == 'w':
    toReturn = 'W'
  elif character == 'x':
    toReturn = 'X'
  elif character == 'y':
    toReturn = 'Y'
  elif character == 'z':
    toReturn = 'Z'

  return toReturn

def charIsUpperCase(character):

  isUpper = False
  if character == 'A':
    isUpper = True
  elif character == 'B':
    isUpper = True
  elif character == 'C':
    isUpper = True
  elif character == 'D':
    isUpper = True
  elif character == 'E':
    isUpper = True
  elif character == 'F':
    isUpper = True
  elif character == 'G':
    isUpper = True
  elif character == 'H':
    isUpper = True
  elif character == 'I':
    isUpper = True
  elif character == 'J':
    isUpper = True
  elif character == 'K':
    isUpper = True
  elif character == 'L':
    isUpper = True
  elif character == 'M':
    isUpper = True
  elif character == 'N':
    isUpper = True
  elif character == 'O':
    isUpper = True
  elif character == 'P':
    isUpper = True
  elif character == 'Q':
    isUpper = True
  elif character == 'R':
    isUpper = True
  elif character == 'S':
    isUpper = True
  elif character == 'T':
    isUpper = True
  elif character == 'U':
    isUpper = True
  elif character == 'V':
    isUpper = True
  elif character == 'W':
    isUpper = True
  elif character == 'X':
    isUpper = True
  elif character == 'Y':
    isUpper = True
  elif character == 'Z':
    isUpper = True

  return isUpper

def charIsLowerCase(character):

  isLower = False
  if character == 'a':
    isLower = True
  elif character == 'b':
    isLower = True
  elif character == 'c':
    isLower = True
  elif character == 'd':
    isLower = True
  elif character == 'e':
    isLower = True
  elif character == 'f':
    isLower = True
  elif character == 'g':
    isLower = True
  elif character == 'h':
    isLower = True
  elif character == 'i':
    isLower = True
  elif character == 'j':
    isLower = True
  elif character == 'k':
    isLower = True
  elif character == 'l':
    isLower = True
  elif character == 'm':
    isLower = True
  elif character == 'n':
    isLower = True
  elif character == 'o':
    isLower = True
  elif character == 'p':
    isLower = True
  elif character == 'q':
    isLower = True
  elif character == 'r':
    isLower = True
  elif character == 's':
    isLower = True
  elif character == 't':
    isLower = True
  elif character == 'u':
    isLower = True
  elif character == 'v':
    isLower = True
  elif character == 'w':
    isLower = True
  elif character == 'x':
    isLower = True
  elif character == 'y':
    isLower = True
  elif character == 'z':
    isLower = True

  return isLower

def charIsLetter(character):
  return charIsUpperCase(character) or charIsLowerCase(character)

def charIsNumber(character):

  isNumberx = False
  if character == '0':
    isNumberx = True
  elif character == '1':
    isNumberx = True
  elif character == '2':
    isNumberx = True
  elif character == '3':
    isNumberx = True
  elif character == '4':
    isNumberx = True
  elif character == '5':
    isNumberx = True
  elif character == '6':
    isNumberx = True
  elif character == '7':
    isNumberx = True
  elif character == '8':
    isNumberx = True
  elif character == '9':
    isNumberx = True

  return isNumberx

def charIsWhiteSpace(character):

  isWhiteSpacex = False
  if character == ' ':
    isWhiteSpacex = True
  elif character == '\t':
    isWhiteSpacex = True
  elif character == '\n':
    isWhiteSpacex = True
  elif character == '\r':
    isWhiteSpacex = True

  return isWhiteSpacex

def charIsSymbol(character):

  isSymbolx = False
  if character == '!':
    isSymbolx = True
  elif character == '\"':
    isSymbolx = True
  elif character == '#':
    isSymbolx = True
  elif character == '$':
    isSymbolx = True
  elif character == '%':
    isSymbolx = True
  elif character == '&':
    isSymbolx = True
  elif character == '\'':
    isSymbolx = True
  elif character == '(':
    isSymbolx = True
  elif character == ')':
    isSymbolx = True
  elif character == '*':
    isSymbolx = True
  elif character == '+':
    isSymbolx = True
  elif character == ',':
    isSymbolx = True
  elif character == '-':
    isSymbolx = True
  elif character == '.':
    isSymbolx = True
  elif character == '/':
    isSymbolx = True
  elif character == ':':
    isSymbolx = True
  elif character == ';':
    isSymbolx = True
  elif character == '<':
    isSymbolx = True
  elif character == '=':
    isSymbolx = True
  elif character == '>':
    isSymbolx = True
  elif character == '?':
    isSymbolx = True
  elif character == '@':
    isSymbolx = True
  elif character == '[':
    isSymbolx = True
  elif character == '\\':
    isSymbolx = True
  elif character == ']':
    isSymbolx = True
  elif character == '^':
    isSymbolx = True
  elif character == '_':
    isSymbolx = True
  elif character == '`':
    isSymbolx = True
  elif character == '{':
    isSymbolx = True
  elif character == '|':
    isSymbolx = True
  elif character == '}':
    isSymbolx = True
  elif character == '~':
    isSymbolx = True

  return isSymbolx

def charCharacterIsBefore(a, b):

  ad = ord(a)
  bd = ord(b)

  return ad < bd

def And4Byte(n1, n2):
    if n1 >= 0 and n1 <= 0xFFFFFFFF and n2 >= 0 and n2 <= 0xFFFFFFFF:
      return (int(n1) & int(n2)) & 0xFFFFFFFF
    else:
      return 0

def And2Byte(n1, n2):
    if n1 >= 0 and n1 <= 0xFFFF and n2 >= 0 and n2 <= 0xFFFF:
      return (int(n1) & int(n2)) & 0xFFFF
    else:
      return 0

def AndByte(n1, n2):
    if n1 >= 0 and n1 <= 0xFF and n2 >= 0 and n2 <= 0xFF:
      return (int(n1) & int(n2)) & 0xFF
    else:
      return 0

def AndBytes(n1, n2, bytes):

  byteVal = 1.0
  result = 0.0

  if n1 >= 0.0 and n1 < 2.0**(bytes*8.0) and n2 >= 0.0 and n2 < 2.0**(bytes*8.0):
    n1 = Truncate(n1)
    n2 = Truncate(n2)
    bytes = Truncate(bytes)

    i = 0.0
    while i < bytes*8.0:
      if n1 % 2.0 == 1.0 and n2 % 2.0 == 1.0:
        result = result + byteVal
      n1 = floor(n1/2.0)
      n2 = floor(n2/2.0)
      byteVal = byteVal*2.0
      i = i + 1.0
    

  return result

def Or4Byte(n1, n2):
    if n1 >= 0 and n1 <= 0xFFFFFFFF and n2 >= 0 and n2 <= 0xFFFFFFFF:
      return (int(n1) | int(n2)) & 0xFFFFFFFF
    else:
      return 0

def Or2Byte(n1, n2):
    if n1 >= 0 and n1 <= 0xFFFF and n2 >= 0 and n2 <= 0xFFFF:
      return (int(n1) | int(n2)) & 0xFFFF
    else:
      return 0

def OrByte(n1, n2):
    if n1 >= 0 and n1 <= 0xFF and n2 >= 0 and n2 <= 0xFF:
      return (int(n1) | int(n2)) & 0xFF
    else:
      return 0

def OrBytes(n1, n2, bytes):

  byteVal = 1.0
  result = 0.0

  if n1 >= 0.0 and n1 < 2.0**(bytes*8.0) and n2 >= 0.0 and n2 < 2.0**(bytes*8.0):
    n1 = Truncate(n1)
    n2 = Truncate(n2)
    bytes = Truncate(bytes)

    i = 0.0
    while i < bytes*8.0:
      if n1 % 2.0 == 1.0 or n2 % 2.0 == 1.0:
        result = result + byteVal
      n1 = floor(n1/2.0)
      n2 = floor(n2/2.0)
      byteVal = byteVal*2.0
      i = i + 1.0
    

  return result

def Xor4Byte(n1, n2):
    if n1 >= 0 and n1 <= 0xFFFFFFFF and n2 >= 0 and n2 <= 0xFFFFFFFF:
      return (int(n1) ^ int(n2)) & 0xFFFFFFFF
    else:
      return 0

def Xor2Byte(n1, n2):
    if n1 >= 0 and n1 <= 0xFFFF and n2 >= 0 and n2 <= 0xFFFF:
      return (int(n1) ^ int(n2)) & 0xFFFF
    else:
      return 0

def XorByte(n1, n2):
    if n1 >= 0 and n1 <= 0xFF and n2 >= 0 and n2 <= 0xFF:
      return (int(n1) ^ int(n2)) & 0xFF
    else:
      return 0

def XorBytes(n1, n2, bytes):

  byteVal = 1.0
  result = 0.0

  if n1 >= 0.0 and n1 < 2.0**(bytes*8.0) and n2 >= 0.0 and n2 < 2.0**(bytes*8.0):
    n1 = Truncate(n1)
    n2 = Truncate(n2)
    bytes = Truncate(bytes)

    i = 0.0
    while i < bytes*8.0:
      if n1 % 2.0 != n2 % 2.0:
        result = result + byteVal
      n1 = floor(n1/2.0)
      n2 = floor(n2/2.0)
      byteVal = byteVal*2.0
      i = i + 1.0
    

  return result

def Not4Byte(b):
    if b >= 0 and b <= 0xFFFFFFFF:
      return ~int(b) & 0xFFFFFFFF
    else:
      return 0

def Not2Byte(b):
    if b >= 0 and b <= 0xFFFF:
      return ~int(b) & 0xFFFF
    else:
      return 0

def NotByte(b):
    if b >= 0 and b <= 0xFF:
      return ~int(b) & 0xFF
    else:
      return 0

def NotBytes(b, length):

  result = 0.0

  if b >= 0.0 and b < 2.0**(length*8.0):
    b = Truncate(b)
    length = Truncate(length)

    result = 2.0**(length*8.0) - b - 1.0

  return result

def ShiftLeft4Byte(b, amount):
    if b >= 0 and b <= 0xFFFFFFFF and amount >= 0 and amount < 32:
      return (int(b) << int(amount)) & 0xFFFFFFFF
    else:
      return 0

def ShiftLeft2Byte(b, amount):
    if b >= 0 and b <= 0xFFFF and amount >= 0 and amount < 16:
      return (int(b) << int(amount)) & 0xFFFF
    else:
      return 0

def ShiftLeftByte(b, amount):
    if b >= 0 and b <= 0xFF and amount >= 0 and amount < 8:
      return (int(b) << int(amount)) & 0xFF
    else:
      return 0

def ShiftLeftBytes(b, amount, length):

  result = 0.0

  if b >= 0.0 and b < 2.0**(length*8.0) and amount >= 0.0 and amount <= length*8.0:
    b = Truncate(b)
    amount = Truncate(amount)

    result = b*2.0**amount

  return result

def ShiftRight4Byte(b, amount):
    if b >= 0 and b <= 0xFFFFFFFF and amount >= 0 and amount < 32:
      return (int(b) >> int(amount)) & 0xFFFFFFFF
    else:
      return 0

def ShiftRight2Byte(b, amount):
    if b >= 0 and b <= 0xFFFF and amount >= 0 and amount < 16:
      return (int(b) >> int(amount)) & 0xFFFF
    else:
      return 0

def ShiftRightByte(b, amount):
    if b >= 0 and b <= 0xFF and amount >= 0 and amount < 8:
      return (int(b) >> int(amount)) & 0xFF
    else:
      return 0

def ShiftRightBytes(b, amount, length):

  result = 0.0

  if b >= 0.0 and b < 2.0**(length*8.0) and amount >= 0.0 and amount <= length*8.0:
    b = Truncate(b)
    amount = Truncate(amount)

    result = Truncate(b/2.0**amount)

  return result

def ReadNextBit(data, nextbit):

  bytenr = floor(nextbit.numberValue/8.0)
  bitnumber = nextbit.numberValue % 8.0

  b = data[int(bytenr)]

  bit = floor(b/2.0**bitnumber) % 2.0

  nextbit.numberValue = nextbit.numberValue + 1.0

  return bit

def BitExtract(b, fromInc, toInc):
  return floor(b/2.0**fromInc) % 2.0**(toInc + 1.0 - fromInc)

def ReadBitRange(data, nextbit, length):

  number = 0.0

  startbyte = floor(nextbit.numberValue/8.0)
  endbyte = floor((nextbit.numberValue + length)/8.0)

  startbit = nextbit.numberValue % 8.0
  endbit = (nextbit.numberValue + length - 1.0) % 8.0

  if startbyte == endbyte:
    number = BitExtract(data[int(startbyte)], startbit, endbit)

  nextbit.numberValue = nextbit.numberValue + length

  return number

def SkipToBoundary(nextbit):

  skip = 8.0 - nextbit.numberValue % 8.0
  nextbit.numberValue = nextbit.numberValue + skip

def ReadNextByteBoundary(data, nextbit):

  bytenr = floor(nextbit.numberValue/8.0)
  b = data[int(bytenr)]
  nextbit.numberValue = nextbit.numberValue + 8.0

  return b

def Read2bytesByteBoundary(data, nextbit):

  r = 0.0
  r = r + 2.0**8.0*ReadNextByteBoundary(data, nextbit)
  r = r + ReadNextByteBoundary(data, nextbit)

  return r

def ComputeAdler32(data):

  a = 1.0
  b = 0.0
  m = 65521.0

  i = 0.0
  while i < len(data):
    a = (a + data[int(i)]) % m
    b = (b + a) % m
    i = i + 1.0
  

  return b*2.0**16.0 + a

def DeflateDataStaticHuffman(data, level):

  code = CreateNumberReference(0.0)
  length = CreateNumberReference(0.0)
  compressedCode = CreateNumberReference(0.0)
  lengthAdditionLength = CreateNumberReference(0.0)
  distanceCode = CreateNumberReference(0.0)
  distanceReference = CreateNumberReference(0.0)
  lengthReference = CreateNumberReference(0.0)
  lengthAddition = CreateNumberReference(0.0)
  distanceAdditionReference = CreateNumberReference(0.0)
  distanceAdditionLengthReference = CreateNumberReference(0.0)
  match = BooleanReference()

  bytes =  [None]*int(max(len(data)*2.0,100.0))
  aFillNumberArray(bytes, 0.0)
  currentBit = CreateNumberReference(0.0)

  bitReverseLookupTable = GenerateBitReverseLookupTable(9.0)

  # Final block
  AppendBitsToBytesRight(bytes, currentBit, 1.0, 1.0)
  # Fixed code
  AppendBitsToBytesRight(bytes, currentBit, 1.0, 2.0)

  i = 0.0
  while i < len(data):
    FindMatch(data, i, distanceReference, lengthReference, match, level)

    if match.booleanValue:
      GetDeflateLengthCode(lengthReference.numberValue, compressedCode, lengthAddition, lengthAdditionLength)
      GetDeflateDistanceCode(distanceReference.numberValue, distanceCode, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable)

    if  not match.booleanValue :
      GetDeflateStaticHuffmanCode(data[int(i)], code, length, bitReverseLookupTable)
      AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)
      i = i + 1.0
    else:
      GetDeflateStaticHuffmanCode(compressedCode.numberValue, code, length, bitReverseLookupTable)
      AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)
      AppendBitsToBytesRight(bytes, currentBit, lengthAddition.numberValue, lengthAdditionLength.numberValue)
      AppendBitsToBytesRight(bytes, currentBit, distanceCode.numberValue, 5.0)
      AppendBitsToBytesRight(bytes, currentBit, distanceAdditionReference.numberValue, distanceAdditionLengthReference.numberValue)
      i = i + lengthReference.numberValue
  

  # Stop symbol
  GetDeflateStaticHuffmanCode(256.0, code, length, bitReverseLookupTable)
  AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)

  copy = NumberArrayReference()
  aCopyNumberArrayRange(bytes, 0.0, ceil(currentBit.numberValue/8.0), copy)
  bytes = None
  bytes = copy.numberArray

  return bytes

def FindMatch(data, pos, distanceReference, lengthReference, match, level):

  deflateMinMength = 3.0
  deflateMaxLength = 258.0

  longest = min(pos - 1.0,deflateMaxLength)
  longest = min(len(data) - pos,longest)

  deflateMaxDistance = floor(32768.0/10.0*level)

  startDistance = min(pos,deflateMaxDistance)

  if longest >= deflateMinMength:
    maxLength = 0.0
    distanceForMax = 0.0

    i = pos - 1.0
    while i >= pos - startDistance and maxLength != longest:
      matchLength = 0.0
      done = False
      j = 0.0
      while j < longest and  not done :
        if data[int(i + j)] == data[int(pos + j)]:
          matchLength = matchLength + 1.0
        else:
          done = True
        j = j + 1.0
      

      if matchLength >= deflateMinMength and matchLength > maxLength:
        maxLength = matchLength
        distanceForMax = pos - i
      i = i - 1.0
    

    if maxLength >= deflateMinMength:
      match.booleanValue = True
      lengthReference.numberValue = maxLength
      distanceReference.numberValue = distanceForMax
    else:
      match.booleanValue = False
  else:
    match.booleanValue = False

def GenerateBitReverseLookupTable(bits):

  table =  [None]*int(2.0**bits)

  i = 0.0
  while i < len(table):
    table[int(i)] = ReverseBits(i, 32.0)
    i = i + 1.0
  

  return table

def ReverseBits(x, bits):

  b = 0.0

  i = 0.0
  while i < bits:
    b = ShiftLeft4Byte(b, 1.0)
    bit = And4Byte(x, 1.0)
    b = Or4Byte(b, bit)
    x = ShiftRight4Byte(x, 1.0)
    i = i + 1.0
  

  return b

def DeflateDataNoCompression(data):

  maxblocksize = 2.0**16.0 - 1.0
  blocks = ceil(len(data)/maxblocksize)

  position = CreateNumberReference(0.0)

  deflated =  [None]*int((1.0 + 4.0)*blocks + len(data))

  block = 0.0
  while block < blocks:
    if block + 1.0 == blocks:
      WriteByte(deflated, 1.0, position)
    else:
      WriteByte(deflated, 0.0, position)
    blocklength = min(len(data) - block*maxblocksize,maxblocksize)
    Write2BytesLE(deflated, blocklength, position)
    Write2BytesLE(deflated, Not2Byte(blocklength), position)

    i = 0.0
    while i < blocklength:
      WriteByte(deflated, data[int(block*maxblocksize + i)], position)
      i = i + 1.0
    
    block = block + 1.0
  

  return deflated

def GetDeflateStaticHuffmanCode(b, code, length, bitReverseLookupTable):

  if b >= 0.0 and b <= 143.0:
    code.numberValue = 48.0 + b
    length.numberValue = 8.0
  elif b >= 144.0 and b <= 255.0:
    code.numberValue = b - 144.0 + 400.0
    length.numberValue = 9.0
  elif b >= 256.0 and b <= 279.0:
    code.numberValue = b - 256.0 + 0.0
    length.numberValue = 7.0
  elif b >= 280.0 and b <= 287.0:
    code.numberValue = b - 280.0 + 192.0
    length.numberValue = 8.0

  reversed = bitReverseLookupTable[int(code.numberValue)]
  code.numberValue = ShiftRight4Byte(reversed, 32.0 - length.numberValue)

def GetDeflateLengthCode(length, code, lengthAddition, lengthAdditionLength):
  if length >= 3.0 and length <= 10.0:
    code.numberValue = 257.0 + length - 3.0
    lengthAdditionLength.numberValue = 0.0
  elif length >= 11.0 and length <= 18.0:
    code.numberValue = 265.0 + floor((length - 11.0)/2.0)
    lengthAddition.numberValue = floor((length - 11.0) % 2.0)
    lengthAdditionLength.numberValue = 1.0
  elif length >= 19.0 and length <= 34.0:
    code.numberValue = 269.0 + floor((length - 19.0)/4.0)
    lengthAddition.numberValue = floor((length - 19.0) % 4.0)
    lengthAdditionLength.numberValue = 2.0
  elif length >= 35.0 and length <= 66.0:
    code.numberValue = 273.0 + floor((length - 35.0)/8.0)
    lengthAddition.numberValue = floor((length - 35.0) % 8.0)
    lengthAdditionLength.numberValue = 3.0
  elif length >= 67.0 and length <= 130.0:
    code.numberValue = 277.0 + floor((length - 67.0)/16.0)
    lengthAddition.numberValue = floor((length - 67.0) % 16.0)
    lengthAdditionLength.numberValue = 4.0
  elif length >= 131.0 and length <= 257.0:
    code.numberValue = 281.0 + floor((length - 131.0)/32.0)
    lengthAddition.numberValue = floor((length - 131.0) % 32.0)
    lengthAdditionLength.numberValue = 5.0
  elif length == 258.0:
    code.numberValue = 285.0
    lengthAdditionLength.numberValue = 0.0

def GetDeflateDistanceCode(distance, code, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable):

  if distance >= 1.0 and distance <= 4.0:
    code.numberValue = distance - 1.0
    distanceAdditionLengthReference.numberValue = 0.0
  elif distance >= 5.0 and distance <= 8.0:
    code.numberValue = 4.0 + floor((distance - 5.0)/2.0)
    distanceAdditionReference.numberValue = floor((distance - 5.0) % 2.0)
    distanceAdditionLengthReference.numberValue = 1.0
  elif distance >= 9.0 and distance <= 16.0:
    code.numberValue = 6.0 + floor((distance - 9.0)/4.0)
    distanceAdditionReference.numberValue = floor((distance - 9.0) % 4.0)
    distanceAdditionLengthReference.numberValue = 2.0
  elif distance >= 17.0 and distance <= 32.0:
    code.numberValue = 8.0 + floor((distance - 17.0)/8.0)
    distanceAdditionReference.numberValue = floor((distance - 17.0) % 8.0)
    distanceAdditionLengthReference.numberValue = 3.0
  elif distance >= 33.0 and distance <= 64.0:
    code.numberValue = 10.0 + floor((distance - 33.0)/16.0)
    distanceAdditionReference.numberValue = floor((distance - 33.0) % 16.0)
    distanceAdditionLengthReference.numberValue = 4.0
  elif distance >= 65.0 and distance <= 128.0:
    code.numberValue = 12.0 + floor((distance - 65.0)/32.0)
    distanceAdditionReference.numberValue = floor((distance - 65.0) % 32.0)
    distanceAdditionLengthReference.numberValue = 5.0
  elif distance >= 129.0 and distance <= 256.0:
    code.numberValue = 14.0 + floor((distance - 129.0)/64.0)
    distanceAdditionReference.numberValue = floor((distance - 129.0) % 64.0)
    distanceAdditionLengthReference.numberValue = 6.0
  elif distance >= 257.0 and distance <= 512.0:
    code.numberValue = 16.0 + floor((distance - 257.0)/128.0)
    distanceAdditionReference.numberValue = floor((distance - 257.0) % 128.0)
    distanceAdditionLengthReference.numberValue = 7.0
  elif distance >= 513.0 and distance <= 1024.0:
    code.numberValue = 18.0 + floor((distance - 513.0)/256.0)
    distanceAdditionReference.numberValue = floor((distance - 513.0) % 256.0)
    distanceAdditionLengthReference.numberValue = 8.0
  elif distance >= 1025.0 and distance <= 2048.0:
    code.numberValue = 20.0 + floor((distance - 1025.0)/2.0**9.0)
    distanceAdditionReference.numberValue = floor((distance - 1025.0) % 2.0**9.0)
    distanceAdditionLengthReference.numberValue = 9.0
  elif distance >= 2049.0 and distance <= 4096.0:
    code.numberValue = 22.0 + floor((distance - 2049.0)/2.0**10.0)
    distanceAdditionReference.numberValue = floor((distance - 2049.0) % 2.0**10.0)
    distanceAdditionLengthReference.numberValue = 10.0
  elif distance >= 4097.0 and distance <= 8192.0:
    code.numberValue = 24.0 + floor((distance - 4097.0)/2.0**11.0)
    distanceAdditionReference.numberValue = floor((distance - 4097.0) % 2.0**11.0)
    distanceAdditionLengthReference.numberValue = 11.0
  elif distance >= 8193.0 and distance <= 16384.0:
    code.numberValue = 26.0 + floor((distance - 8193.0)/2.0**12.0)
    distanceAdditionReference.numberValue = floor((distance - 8193.0) % 2.0**12.0)
    distanceAdditionLengthReference.numberValue = 12.0
  elif distance >= 16385.0 and distance <= 32768.0:
    code.numberValue = 28.0 + floor((distance - 16385.0)/2.0**13.0)
    distanceAdditionReference.numberValue = floor((distance - 16385.0) % 2.0**13.0)
    distanceAdditionLengthReference.numberValue = 13.0

  reversed = bitReverseLookupTable[int(code.numberValue)]
  code.numberValue = ShiftRight4Byte(reversed, 32.0 - 5.0)

def AppendBitsToBytesLeft(bytes, nextbit, data, length):

  while length > 0.0:
    bytePos = Truncate(nextbit.numberValue/8.0)
    bitPos = nextbit.numberValue % 8.0

    if length < 8.0 - bitPos:
      part = ShiftLeft4Byte(data, 8.0 - bitPos - length)

      bytes[int(bytePos)] = Or4Byte(bytes[int(bytePos)], part)

      nextbit.numberValue = nextbit.numberValue + length

      length = 0.0
    else:
      segment = 8.0 - bitPos

      part = ShiftRight4Byte(data, length - segment)
      bytes[int(bytePos)] = Or4Byte(bytes[int(bytePos)], part)
      nextbit.numberValue = nextbit.numberValue + segment

      remove = ShiftLeft4Byte(part, length - segment)
      data = Xor4Byte(data, remove)

      length = length - segment
  

def AppendBitsToBytesRight(bytes, nextbit, data, length):

  while length > 0.0:
    bytePos = Truncate(nextbit.numberValue/8.0)
    bitPos = nextbit.numberValue % 8.0

    if length < 8.0 - bitPos:
      part = ShiftLeft4Byte(data, bitPos)

      bytes[int(bytePos)] = Or4Byte(bytes[int(bytePos)], part)

      nextbit.numberValue = nextbit.numberValue + length

      length = 0.0
    else:
      segment = 8.0 - bitPos

      mask = 1.0
      mask = ShiftLeft4Byte(mask, segment)
      mask = mask - 1.0

      part = And4Byte(mask, data)
      part = ShiftLeft4Byte(part, bitPos)
      bytes[int(bytePos)] = Or4Byte(bytes[int(bytePos)], part)
      nextbit.numberValue = nextbit.numberValue + segment

      data = ShiftRight4Byte(data, segment)

      length = length - segment
  


