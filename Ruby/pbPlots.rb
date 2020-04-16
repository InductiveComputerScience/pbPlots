
# Downloaded from https://repo.progsbase.com - Code Developed Using progsbase.

class Rectangle
	attr_accessor :x1
	attr_accessor :x2
	attr_accessor :y1
	attr_accessor :y2
end
class ScatterPlotSeries
	attr_accessor :linearInterpolation
	attr_accessor :pointType
	attr_accessor :lineType
	attr_accessor :lineThickness
	attr_accessor :xs
	attr_accessor :ys
	attr_accessor :color
end
class ScatterPlotSettings
	attr_accessor :canvas
	attr_accessor :scatterPlotSeries
	attr_accessor :autoBoundaries
	attr_accessor :xMax
	attr_accessor :xMin
	attr_accessor :yMax
	attr_accessor :yMin
	attr_accessor :autoPadding
	attr_accessor :xPadding
	attr_accessor :yPadding
	attr_accessor :yLabel
	attr_accessor :xLabel
	attr_accessor :title
	attr_accessor :showGrid
	attr_accessor :gridColor
end
class RGBA
	attr_accessor :r
	attr_accessor :g
	attr_accessor :b
	attr_accessor :a
end
class RGBABitmap
	attr_accessor :y
end
class RGBABitmapImage
	attr_accessor :x
end
class BooleanArrayReference
	attr_accessor :booleanArray
end
class BooleanReference
	attr_accessor :booleanValue
end
class CharacterReference
	attr_accessor :characterValue
end
class NumberArrayReference
	attr_accessor :numberArray
end
class NumberReference
	attr_accessor :numberValue
end
class StringArrayReference
	attr_accessor :stringArray
end
class StringReference
	attr_accessor :string
end
class Chunk
	attr_accessor :length
	attr_accessor :type
	attr_accessor :data
	attr_accessor :crc
end
class IHDR
	attr_accessor :width
	attr_accessor :height
	attr_accessor :bitDepth
	attr_accessor :colourType
	attr_accessor :compressionMethod
	attr_accessor :filterMethod
	attr_accessor :interlaceMethod
end
class PHYS
	attr_accessor :pixelsPerMeter
end
class PNGImage
	attr_accessor :signature
	attr_accessor :ihdr
	attr_accessor :zlibStruct
	attr_accessor :physPresent
	attr_accessor :phys
end
class ZLIBStruct
	attr_accessor :cMF
	attr_accessor :cM
	attr_accessor :cINFO
	attr_accessor :fLG
	attr_accessor :fCHECK
	attr_accessor :fDICT
	attr_accessor :fLEVEL
	attr_accessor :compressedDataBlocks
	attr_accessor :adler32CheckValue
end
class LinkedListNodeStrings
	attr_accessor :endx
	attr_accessor :value
	attr_accessor :nextx
end
class LinkedListStrings
	attr_accessor :first
	attr_accessor :last
end
class LinkedListNodeNumbers
	attr_accessor :nextx
	attr_accessor :endx
	attr_accessor :value
end
class LinkedListNumbers
	attr_accessor :first
	attr_accessor :last
end
class LinkedListCharacters
	attr_accessor :first
	attr_accessor :last
end
class LinkedListNodeCharacters
	attr_accessor :endx
	attr_accessor :value
	attr_accessor :nextx
end
class DynamicArrayNumbers
	attr_accessor :array
	attr_accessor :length
end
def RectanglesOverlap(r1, r2)

	overlap = false

	overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2)
	overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2)
	overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2)
	overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2)

	return overlap
end


def CreateRectangle(x1, y1, x2, y2)
	r = Rectangle.new
	r.x1 = x1
	r.y1 = y1
	r.x2 = x2
	r.y2 = y2
	return r
end


def CopyRectangleValues(rd, rs)
	rd.x1 = rs.x1
	rd.y1 = rs.y1
	rd.x2 = rs.x2
	rd.y2 = rs.y2
end


def GetDefaultScatterPlotSettings()

	settings = ScatterPlotSettings.new

	settings.autoBoundaries = true
	settings.autoPadding = true
	settings.title = "".split("")
	settings.yLabel = "".split("")
	settings.xLabel = "".split("")
	settings.scatterPlotSeries = Array.new(0)
	settings.showGrid = true
	settings.gridColor = GetGray(0.2)

	return settings
end


def GetDefaultScatterPlotSeriesSettings()

	series = ScatterPlotSeries.new

	series.linearInterpolation = true
	series.pointType = "pixels".split("")
	series.lineType = "solid".split("")
	series.lineThickness = 1.0
	series.xs = Array.new(0)
	series.ys = Array.new(0)
	series.color = GetBlack()

	return series
end


def DrawScatterPlot(canvas, xs, ys)

	settings = GetDefaultScatterPlotSettings()

	settings.canvas = canvas
	settings.scatterPlotSeries = Array.new(1)
	settings.scatterPlotSeries[0] = GetDefaultScatterPlotSeriesSettings()
	delete(settings.scatterPlotSeries[0].xs)
	settings.scatterPlotSeries[0].xs = xs
	delete(settings.scatterPlotSeries[0].ys)
	settings.scatterPlotSeries[0].ys = ys

	DrawScatterPlotFromSettings(settings)
end


def DrawScatterPlotFromSettings(settings)

	canvas = settings.canvas
	patternOffset = CreateNumberReference(0.0)

	if settings.scatterPlotSeries.length >= 1.0
		xMin = GetMinimum(settings.scatterPlotSeries[0].xs)
		xMax = GetMaximum(settings.scatterPlotSeries[0].xs)
		yMin = GetMinimum(settings.scatterPlotSeries[0].ys)
		yMax = GetMaximum(settings.scatterPlotSeries[0].ys)
	else
		xMin = -10.0
		xMax = 10.0
		yMin = -10.0
		yMax = 10.0
	end

	if !settings.autoBoundaries
		xMin = settings.xMin
		xMax = settings.xMax
		yMin = settings.yMin
		yMax = settings.yMax
	else
		plot = 1.0
		while(plot < settings.scatterPlotSeries.length)
			sp = settings.scatterPlotSeries[plot]

			xMin = [xMin, GetMinimum(sp.xs)].min
			xMax = [xMax, GetMaximum(sp.xs)].max
			yMin = [yMin, GetMinimum(sp.ys)].min
			yMax = [yMax, GetMaximum(sp.ys)].max
			plot = plot + 1.0
		end
	end

	xLength = xMax - xMin
	yLength = yMax - yMin

	if settings.autoPadding
		xPadding = 0.10*ImageWidth(canvas)
		yPadding = 0.10*ImageHeight(canvas)
	else
		xPadding = settings.xPadding
		yPadding = settings.yPadding
	end

	# Draw title
	DrawText(canvas, ImageWidth(canvas) / 2.0 - GetTextWidth(settings.title) / 2.0, yPadding / 3.0, settings.title, GetBlack())

	# Draw grid
	xPixelMin = xPadding
	yPixelMin = yPadding
	xPixelMax = ImageWidth(canvas) - xPadding
	yPixelMax = ImageHeight(canvas) - yPadding
	xLengthPixels = xPixelMax - xPixelMin
	yLengthPixels = yPixelMax - yPixelMin
	DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor)

	gridLabelColor = GetGray(0.5)

	xLabels = StringArrayReference.new
	xLabelPriorities = NumberArrayReference.new
	yLabels = StringArrayReference.new
	yLabelPriorities = NumberArrayReference.new
	xGridPositions = ComputeGridLinePositions(xMin, xMax, xLabels, xLabelPriorities)
	yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities)

	if settings.showGrid
		# X-grid
		i = 0.0
		while(i < xGridPositions.length)
			x = xGridPositions[i]
			px = MapXCoordinates(x, xMin, xLength, xPixelMin, xLengthPixels)
			DrawLine1px(canvas, px, yPixelMin, px, yPixelMax, settings.gridColor)
			i = i + 1.0
		end

		# Y-grid
		i = 0.0
		while(i < yGridPositions.length)
			y = yGridPositions[i]
			py = MapYCoordinates(y, yMin, yLength, yPixelMin, yLengthPixels)
			DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor)
			i = i + 1.0
		end
	end

	# Labels
	occupied = Array.new(xLabels.stringArray.length + yLabels.stringArray.length)
	i = 0.0
	while(i < occupied.length)
		occupied[i] = CreateRectangle(0.0, 0.0, 0.0, 0.0)
		i = i + 1.0
	end
	nextRectangle = CreateNumberReference(0.0)

	i = 1.0
	while(i <= 5.0)
		DrawXLabelsForPriority(i, xMin, yMin, yMax, yLength, yLengthPixels, xLength, xPixelMin, yPixelMin, xLengthPixels, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied)
		i = i + 1.0
	end

	i = 1.0
	while(i <= 5.0)
		DrawYLabelsForPriority(i, yMin, xMin, xMax, xLength, xLengthPixels, yLength, xPixelMin, yPixelMin, yLengthPixels, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied)
		i = i + 1.0
	end

	# Draw origin and axis titles.
	axisLabelPadding = 20.0
	if yMin < 0.0 && yMax > 0.0
		yOrigin = 0.0
	else
		yOrigin = yMin + (yMax - yMin) / 2.0
	end
	yOriginPixels = MapYCoordinates(yOrigin, yMin, yLength, yPixelMin, yLengthPixels)
	if yMin < 0.0 && yMax > 0.0
		DrawLine1px(canvas, Round(xPixelMin), Round(yOriginPixels), Round(xPixelMax), Round(yOriginPixels), GetBlack())
	end
	DrawTextUpwards(settings.xLabel, 10.0, yOriginPixels - GetTextWidth(settings.xLabel) / 2.0, canvas)

	if xMin < 0.0 && xMax > 0.0
		xOrigin = 0.0
	else
		xOrigin = xMin + (xMax - xMin) / 2.0
	end
	xOriginPixels = MapXCoordinates(xOrigin, xMin, xLength, xPixelMin, xLengthPixels)
	if xMin < 0.0 && xMax > 0.0
		DrawLine1px(canvas, Round(xOriginPixels), Round(yPixelMin), Round(xOriginPixels), Round(yPixelMax), GetBlack())
	end
	DrawText(canvas, xOriginPixels - GetTextWidth(settings.yLabel) / 2.0, yPixelMax + axisLabelPadding, settings.yLabel, GetBlack())

	# X-grid-markers
	if yMin < 0.0 && yMax > 0.0
	else
		yOrigin = yMax
		yOriginPixels = MapXCoordinates(yOrigin, yMin, yLength, yPixelMin, yLengthPixels)
	end
	i = 0.0
	while(i < xGridPositions.length)
		x = xGridPositions[i]
		px = MapXCoordinates(x, xMin, xLength, xPixelMin, xLengthPixels)
		p = xLabelPriorities.numberArray[i]
		l = 1.0
		if p == 1.0
			l = 8.0
		elsif p == 2.0
			l = 3.0
		end
		DrawLine1px(canvas, px, yOriginPixels, px, yOriginPixels - l, GetBlack())
		i = i + 1.0
	end

	# Y-grid-markers
	if xMin < 0.0 && xMax > 0.0
	else
		xOrigin = xMin
		xOriginPixels = MapXCoordinates(xOrigin, xMin, xLength, xPixelMin, xLengthPixels)
	end
	i = 0.0
	while(i < yGridPositions.length)
		y = yGridPositions[i]
		py = MapYCoordinates(y, yMin, yLength, yPixelMin, yLengthPixels)
		p = yLabelPriorities.numberArray[i]
		l = 1.0
		if p == 1.0
			l = 8.0
		elsif p == 2.0
			l = 3.0
		end
		DrawLine1px(canvas, xOriginPixels, py, xOriginPixels + l, py, GetBlack())
		i = i + 1.0
	end

	# Draw points
	plot = 0.0
	while(plot < settings.scatterPlotSeries.length)
		sp = settings.scatterPlotSeries[plot]

		xs = sp.xs
		ys = sp.ys
		linearInterpolation = sp.linearInterpolation

		x1Ref = NumberReference.new
		y1Ref = NumberReference.new
		x2Ref = NumberReference.new
		y2Ref = NumberReference.new
		if linearInterpolation
			prevSet = false
			xPrev = 0.0
			yPrev = 0.0
			i = 0.0
			while(i < xs.length)
				x = xs[i]
				y = ys[i]

				if prevSet
					x1Ref.numberValue = xPrev
					y1Ref.numberValue = yPrev
					x2Ref.numberValue = x
					y2Ref.numberValue = y

					success = CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax)

					if success
						pxPrev = MapXCoordinates(x1Ref.numberValue, xMin, xLength, xPixelMin, xLengthPixels)
						pyPrev = MapYCoordinates(y1Ref.numberValue, yMin, yLength, yPixelMin, yLengthPixels)
						px = MapXCoordinates(x2Ref.numberValue, xMin, xLength, xPixelMin, xLengthPixels)
						py = MapYCoordinates(y2Ref.numberValue, yMin, yLength, yPixelMin, yLengthPixels)

						if aStringsEqual(sp.lineType, "solid".split("")) && sp.lineThickness == 1.0
							DrawLine1px(canvas, pxPrev, pyPrev, px, py, sp.color)
						elsif aStringsEqual(sp.lineType, "solid".split(""))
							DrawLine(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, sp.color)
						elsif aStringsEqual(sp.lineType, "dashed".split(""))
							linePattern = GetLinePattern1()
							DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
						elsif aStringsEqual(sp.lineType, "dotted".split(""))
							linePattern = GetLinePattern2()
							DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
						elsif aStringsEqual(sp.lineType, "dotdash".split(""))
							linePattern = GetLinePattern3()
							DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
						elsif aStringsEqual(sp.lineType, "longdash".split(""))
							linePattern = GetLinePattern4()
							DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
						elsif aStringsEqual(sp.lineType, "twodash".split(""))
							linePattern = GetLinePattern5()
							DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
						end
					end
				end

				prevSet = true
				xPrev = x
				yPrev = y
				i = i + 1.0
			end
		else
			i = 0.0
			while(i < xs.length)
				x = xs[i]
				y = ys[i]

				if x > xMin && x < xMax && y > yMin && y < yMax

					x = MapXCoordinates(x, xMin, xLength, xPixelMin, xLengthPixels)
					y = MapYCoordinates(y, yMin, yLength, yPixelMin, yLengthPixels)

					if aStringsEqual(sp.pointType, "crosses".split(""))
						DrawPixel(canvas, x, y, sp.color)
						DrawPixel(canvas, x + 1.0, y, sp.color)
						DrawPixel(canvas, x + 2.0, y, sp.color)
						DrawPixel(canvas, x - 1.0, y, sp.color)
						DrawPixel(canvas, x - 2.0, y, sp.color)
						DrawPixel(canvas, x, y + 1.0, sp.color)
						DrawPixel(canvas, x, y + 2.0, sp.color)
						DrawPixel(canvas, x, y - 1.0, sp.color)
						DrawPixel(canvas, x, y - 2.0, sp.color)
					elsif aStringsEqual(sp.pointType, "circles".split(""))
						DrawCircle(canvas, x, y, 3.0, sp.color)
					elsif aStringsEqual(sp.pointType, "dots".split(""))
						DrawFilledCircle(canvas, x, y, 3.0, sp.color)
					elsif aStringsEqual(sp.pointType, "triangles".split(""))
						DrawTriangle(canvas, x, y, 3.0, sp.color)
					elsif aStringsEqual(sp.pointType, "filled triangles".split(""))
						DrawFilledTriangle(canvas, x, y, 3.0, sp.color)
					elsif aStringsEqual(sp.pointType, "pixels".split(""))
						DrawPixel(canvas, x, y, sp.color)
					end
				end
				i = i + 1.0
			end
		end
		plot = plot + 1.0
	end
end


def CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax)

	x1 = x1Ref.numberValue
	y1 = y1Ref.numberValue
	x2 = x2Ref.numberValue
	y2 = y2Ref.numberValue

	p1In = x1 >= xMin && x1 <= xMax && y1 >= yMin && y1 <= yMax
	p2In = x2 >= xMin && x2 <= xMax && y2 >= yMin && y2 <= yMax

	if p1In && p2In
		success = true
	elsif !p1In && p2In
		dx = x1 - x2
		dy = y1 - y2

		if dx != 0.0
			f1 = (xMin - x2) / dx
			f2 = (xMax - x2) / dx
		else
			f1 = 1.0
			f2 = 1.0
		end
		if dy != 0.0
			f3 = (yMin - y2) / dy
			f4 = (yMax - y2) / dy
		else
			f3 = 1.0
			f4 = 1.0
		end

		if f1 < 0.0
			f1 = 1.0
		end
		if f2 < 0.0
			f2 = 1.0
		end
		if f3 < 0.0
			f3 = 1.0
		end
		if f4 < 0.0
			f4 = 1.0
		end

		f = [f1, [f2, [f3, f4].min].min].min

		x1 = x2 + f*dx
		y1 = y2 + f*dy

		success = true
	elsif p1In && !p2In
		dx = x2 - x1
		dy = y2 - y1

		if dx != 0.0
			f1 = (xMin - x1) / dx
			f2 = (xMax - x1) / dx
		else
			f1 = 1.0
			f2 = 1.0
		end
		if dy != 0.0
			f3 = (yMin - y1) / dy
			f4 = (yMax - y1) / dy
		else
			f3 = 1.0
			f4 = 1.0
		end

		if f1 < 0.0
			f1 = 1.0
		end
		if f2 < 0.0
			f2 = 1.0
		end
		if f3 < 0.0
			f3 = 1.0
		end
		if f4 < 0.0
			f4 = 1.0
		end

		f = [f1, [f2, [f3, f4].min].min].min

		x2 = x1 + f*dx
		y2 = y1 + f*dy

		success = true
	else
		success = false
	end

	x1Ref.numberValue = x1
	y1Ref.numberValue = y1
	x2Ref.numberValue = x2
	y2Ref.numberValue = y2

	return success
end


def IncrementFromCoordinates(x1, y1, x2, y2)
	return (x2 - x1) / (y2 - y1)
end


def InterceptFromCoordinates(x1, y1, x2, y2)

	a = IncrementFromCoordinates(x1, y1, x2, y2)
	b = y1 - a*x1

	return b
end


def DrawXLabelsForPriority(p, xMin, yMin, yMax, yLength, yLengthPixels, xLength, xPixelMin, yPixelMin, xLengthPixels, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied)

	r = Rectangle.new

	if yMin < 0.0 && yMax > 0.0
		oy = MapYCoordinates(0.0, yMin, yLength, yPixelMin, yLengthPixels)
	else
		oy = MapYCoordinates(yMin, yMin, yLength, yPixelMin, yLengthPixels)
	end

	overlap = false
	i = 0.0
	while(i < xLabels.stringArray.length)
		if xLabelPriorities.numberArray[i] == p

			x = xGridPositions[i]
			px = MapXCoordinates(x, xMin, xLength, xPixelMin, xLengthPixels)
			text = xLabels.stringArray[i].string

			r.x1 = px - GetTextWidth(text) / 2.0
			r.y1 = oy + 5.0
			r.x2 = r.x1 + GetTextWidth(text)
			r.y2 = r.y1 + GetTextHeight(text)

			j = 0.0
			while(j < nextRectangle.numberValue)
				overlap = overlap || RectanglesOverlap(r, occupied[j])
				j = j + 1.0
			end
		end
		i = i + 1.0
	end
	if !overlap
		i = 0.0
		while(i < xGridPositions.length)
			x = xGridPositions[i]
			px = MapXCoordinates(x, xMin, xLength, xPixelMin, xLengthPixels)

			if xLabelPriorities.numberArray[i] == p
				text = xLabels.stringArray[i].string

				r.x1 = px - GetTextWidth(text) / 2.0
				r.y1 = oy + 5.0
				r.x2 = r.x1 + GetTextWidth(text)
				r.y2 = r.y1 + GetTextHeight(text)

				DrawText(canvas, r.x1, r.y1, text, gridLabelColor)

				CopyRectangleValues(occupied[nextRectangle.numberValue], r)
				nextRectangle.numberValue = nextRectangle.numberValue + 1.0
			end
			i = i + 1.0
		end
	end
end


def DrawYLabelsForPriority(p, yMin, xMin, xMax, xLength, xLengthPixels, yLength, xPixelMin, yPixelMin, yLengthPixels, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied)

	r = Rectangle.new

	if xMin < 0.0 && xMax > 0.0
		ox = MapXCoordinates(0.0, xMin, xLength, xPixelMin, xLengthPixels)
	else
		ox = MapXCoordinates(xMin, xMin, xLength, xPixelMin, xLengthPixels)
	end

	overlap = false
	i = 0.0
	while(i < yLabels.stringArray.length)
		if yLabelPriorities.numberArray[i] == p

			y = yGridPositions[i]
			py = MapYCoordinates(y, yMin, yLength, yPixelMin, yLengthPixels)
			text = yLabels.stringArray[i].string

			r.x1 = ox - GetTextWidth(text) - 10.0
			r.y1 = py - 6.0
			r.x2 = r.x1 + GetTextWidth(text)
			r.y2 = r.y1 + GetTextHeight(text)

			j = 0.0
			while(j < nextRectangle.numberValue)
				overlap = overlap || RectanglesOverlap(r, occupied[j])
				j = j + 1.0
			end
		end
		i = i + 1.0
	end
	if !overlap
		i = 0.0
		while(i < yGridPositions.length)
			y = yGridPositions[i]
			py = MapYCoordinates(y, yMin, yLength, yPixelMin, yLengthPixels)

			if yLabelPriorities.numberArray[i] == p
				text = yLabels.stringArray[i].string

				r.x1 = ox - GetTextWidth(text) - 10.0
				r.y1 = py - 6.0
				r.x2 = r.x1 + GetTextWidth(text)
				r.y2 = r.y1 + GetTextHeight(text)

				DrawText(canvas, r.x1, r.y1, text, gridLabelColor)

				CopyRectangleValues(occupied[nextRectangle.numberValue], r)
				nextRectangle.numberValue = nextRectangle.numberValue + 1.0
			end
			i = i + 1.0
		end
	end
end


def DrawTextUpwards(text, x, y, canvas)
	buffer = CreateImage(GetTextWidth(text), GetTextHeight(text), GetTransparent())
	DrawText(buffer, 0.0, 0.0, text, GetBlack())
	rotated = RotateAntiClockwise90Degrees(buffer)
	DrawImageOnImage(canvas, rotated, x, y)
	DeleteImage(buffer)
	DeleteImage(rotated)
end


def RoundToDigits(element, digitsAfterPoint)
	return Round(element*10.0**digitsAfterPoint) / 10.0**digitsAfterPoint
end


def ComputeGridLinePositions(cMin, cMax, labels, priorities)

	cLength = cMax - cMin

	p = (Math.log10(cLength)).floor
	pInterval = 10.0**p
	# gives 10-1 lines for 100-10 diff
	pMin = (cMin / pInterval).ceil*pInterval
	pMax = (cMax / pInterval).floor*pInterval
	pNum = Round((pMax - pMin) / pInterval + 1.0)

	mode = 1.0

	if pNum <= 3.0
		p = (Math.log10(cLength) - 1.0).floor
		# gives 100-10 lines for 100-10 diff
		pInterval = 10.0**p
		pMin = (cMin / pInterval).ceil*pInterval
		pMax = (cMax / pInterval).floor*pInterval
		pNum = Round((pMax - pMin) / pInterval + 1.0)

		mode = 4.0
	elsif pNum <= 6.0
		p = (Math.log10(cLength)).floor
		pInterval = 10.0**p / 4.0
		# gives 40-5 lines for 100-10 diff
		pMin = (cMin / pInterval).ceil*pInterval
		pMax = (cMax / pInterval).floor*pInterval
		pNum = Round((pMax - pMin) / pInterval + 1.0)

		mode = 3.0
	elsif pNum <= 10.0
		p = (Math.log10(cLength)).floor
		pInterval = 10.0**p / 2.0
		# gives 20-3 lines for 100-10 diff
		pMin = (cMin / pInterval).ceil*pInterval
		pMax = (cMax / pInterval).floor*pInterval
		pNum = Round((pMax - pMin) / pInterval + 1.0)

		mode = 2.0
	end

	positions = Array.new(pNum)
	labels.stringArray = Array.new(pNum)
	priorities.numberArray = Array.new(pNum)

	i = 0.0
	while(i < pNum)
		num = pMin + pInterval*i
		positions[i] = num

		# Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn.
		priority = 1.0

		# Prioritize x.25, x.5 and x.75 lower.
		if mode == 2.0 || mode == 3.0
			rem = ((num / 10.0**(p - 2.0)).round).abs%100.0

			priority = 1.0
			if rem == 50.0
				priority = 2.0
			elsif rem == 25.0 || rem == 75.0
				priority = 3.0
			end
		end

		# Prioritize x.1-x.4 and x.6-x.9 lower
		if mode == 4.0
			rem = (Round(num / 10.0**p)).abs%10.0

			priority = 1.0
			if rem == 1.0 || rem == 2.0 || rem == 3.0 || rem == 4.0 || rem == 6.0 || rem == 7.0 || rem == 8.0 || rem == 9.0
				priority = 2.0
			end
		end

		# 0 has lowest priority.
		if EpsilonCompare(num, 0.0, 10.0**(p - 5.0))
			priority = 10.0
		end

		priorities.numberArray[i] = priority

		# The label itself.
		labels.stringArray[i] = StringReference.new
		if p < 0.0
			if mode == 2.0 || mode == 3.0
				num = RoundToDigits(num, -(p - 1.0))
			else
				num = RoundToDigits(num, -p)
			end
		end
		labels.stringArray[i].string = CreateStringDecimalFromNumber(num)
		i = i + 1.0
	end

	return positions
end


def DrawText(canvas, x, y, text, color)

	charWidth = 8.0
	spacing = 2.0

	i = 0.0
	while(i < text.length)
		DrawAsciiCharacter(canvas, x + i*(charWidth + spacing), y, text[i], color)
		i = i + 1.0
	end
end


def MapYCoordinates(y, ymin, yLength, yPixelMin, yPixelLength)
	y = y - ymin
	y = y*yPixelLength / yLength
	y = yPixelLength - y
	y = y + yPixelMin
	return y
end


def MapXCoordinates(x, xmin, xLength, xPixelMin, xPixelLength)
	x = x - xmin
	x = x*xPixelLength / xLength
	x = x + xPixelMin
	return x
end


def GetMinimum(data)

	minimum = data[0]
	i = 0.0
	while(i < data.length)
		minimum = [minimum, data[i]].min
		i = i + 1.0
	end

	return minimum
end


def GetMaximum(data)

	maximum = data[0]
	i = 0.0
	while(i < data.length)
		maximum = [maximum, data[i]].max
		i = i + 1.0
	end

	return maximum
end


def test()

	failures = CreateNumberReference(0.0)

	scatterPlotSettings = GetDefaultScatterPlotSettings()

	labels = StringArrayReference.new
	labelPriorities = NumberArrayReference.new

	z = 10.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 11.0, failures)

	z = 9.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 19.0, failures)

	z = 8.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 17.0, failures)

	z = 7.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 15.0, failures)

	z = 6.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 13.0, failures)

	z = 5.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 21.0, failures)

	z = 4.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 17.0, failures)

	z = 3.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 31.0, failures)

	z = 2.0
	gridlines = ComputeGridLinePositions(-z / 2.0, z / 2.0, labels, labelPriorities)
	AssertEquals(gridlines.length, 21.0, failures)

	return failures.numberValue
end


def GetBlack()
	black = RGBA.new
	black.a = 1.0
	black.r = 0.0
	black.g = 0.0
	black.b = 0.0
	return black
end


def GetWhite()
	white = RGBA.new
	white.a = 1.0
	white.r = 1.0
	white.g = 1.0
	white.b = 1.0
	return white
end


def GetTransparent()
	transparent = RGBA.new
	transparent.a = 0.0
	transparent.r = 0.0
	transparent.g = 0.0
	transparent.b = 0.0
	return transparent
end


def GetGray(percentage)
	black = RGBA.new
	black.a = 1.0
	black.r = 1.0 - percentage
	black.g = 1.0 - percentage
	black.b = 1.0 - percentage
	return black
end


def CreateRGBColor(r, g, b)
	color = RGBA.new
	color.a = 1.0
	color.r = r
	color.g = g
	color.b = b
	return color
end


def CreateRGBAColor(r, g, b, a)
	color = RGBA.new
	color.a = a
	color.r = r
	color.g = g
	color.b = b
	return color
end


def CreateImage(w, h, color)

	image = RGBABitmapImage.new
	image.x = Array.new(w)
	i = 0.0
	while(i < w)
		image.x[i] = RGBABitmap.new
		image.x[i].y = Array.new(h)
		j = 0.0
		while(j < h)
			image.x[i].y[j] = RGBA.new
			SetPixel(image, i, j, color)
			j = j + 1.0
		end
		i = i + 1.0
	end

	return image
end


def DeleteImage(image)

	w = ImageWidth(image)
	h = ImageHeight(image)

	i = 0.0
	while(i < w)
		j = 0.0
		while(j < h)
			delete(image.x[i].y[j])
			j = j + 1.0
		end
		delete(image.x[i])
		i = i + 1.0
	end
	delete(image)
end


def ImageWidth(image)
	return image.x.length
end


def ImageHeight(image)

	if ImageWidth(image) == 0.0
		height = 0.0
	else
		height = image.x[0].y.length
	end

	return height
end


def SetPixel(image, x, y, color)
	if x >= 0.0 && x < ImageWidth(image) && y >= 0.0 && y < ImageHeight(image)
		image.x[x].y[y].a = color.a
		image.x[x].y[y].r = color.r
		image.x[x].y[y].g = color.g
		image.x[x].y[y].b = color.b
	end
end


def DrawPixel(image, x, y, color)

	if x >= 0.0 && x < ImageWidth(image) && y >= 0.0 && y < ImageHeight(image)
		ra = color.r
		ga = color.g
		ba = color.b
		aa = color.a

		rb = image.x[x].y[y].r
		gb = image.x[x].y[y].g
		bb = image.x[x].y[y].b
		ab = image.x[x].y[y].a

		ao = CombineAlpha(aa, ab)

		ro = AlphaBlend(ra, aa, rb, ab, ao)
		go = AlphaBlend(ga, aa, gb, ab, ao)
		bo = AlphaBlend(ba, aa, bb, ab, ao)

		image.x[x].y[y].r = ro
		image.x[x].y[y].g = go
		image.x[x].y[y].b = bo
		image.x[x].y[y].a = ao
	end
end


def CombineAlpha(as, ad)
	return as + ad*(1.0 - as)
end


def AlphaBlend(cs, as, cd, ad, ao)
	return (cs*as + cd*ad*(1.0 - as)) / ao
end


def DrawHorizontalLine1px(image, x, y, length, color)

	i = 0.0
	while(i < length)
		DrawPixel(image, x + i, y, color)
		i = i + 1.0
	end
end


def DrawVerticalLine1px(image, x, y, height, color)

	i = 0.0
	while(i < height)
		DrawPixel(image, x, y + i, color)
		i = i + 1.0
	end
end


def DrawRectangle1px(image, x, y, width, height, color)
	DrawHorizontalLine1px(image, x, y, width + 1.0, color)
	DrawVerticalLine1px(image, x, y + 1.0, height + 1.0 - 1.0, color)
	DrawVerticalLine1px(image, x + width, y + 1.0, height + 1.0 - 1.0, color)
	DrawHorizontalLine1px(image, x + 1.0, y + height, width + 1.0 - 2.0, color)
end


def DrawImageOnImage(dst, src, topx, topy)

	y = 0.0
	while(y < ImageHeight(src))
		x = 0.0
		while(x < ImageWidth(src))
			if topx + x >= 0.0 && topx + x < ImageWidth(dst) && topy + y >= 0.0 && topy + y < ImageHeight(dst)
				DrawPixel(dst, topx + x, topy + y, src.x[x].y[y])
			end
			x = x + 1.0
		end
		y = y + 1.0
	end
end


def DrawLine1px(image, x0, y0, x1, y1, color)
	XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color)
end


def XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color)

	olda = color.a

	steep = (y1 - y0).abs > (x1 - x0).abs

	if steep
		t = x0
		x0 = y0
		y0 = t

		t = x1
		x1 = y1
		y1 = t
	end
	if x0 > x1
		t = x0
		x0 = x1
		x1 = t

		t = y0
		y0 = y1
		y1 = t
	end

	dx = x1 - x0
	dy = y1 - y0
	g = dy / dx

	if dx == 0.0
		g = 1.0
	end

	xEnd = Round(x0)
	yEnd = y0 + g*(xEnd - x0)
	xGap = OneMinusFractionalPart(x0 + 0.5)
	xpxl1 = xEnd
	ypxl1 = (yEnd).floor
	if steep
		DrawPixel(image, ypxl1, xpxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
		DrawPixel(image, ypxl1 + 1.0, xpxl1, SetBrightness(color, FractionalPart(yEnd)*xGap))
	else
		DrawPixel(image, xpxl1, ypxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
		DrawPixel(image, xpxl1, ypxl1 + 1.0, SetBrightness(color, FractionalPart(yEnd)*xGap))
	end
	intery = yEnd + g

	xEnd = Round(x1)
	yEnd = y1 + g*(xEnd - x1)
	xGap = FractionalPart(x1 + 0.5)
	xpxl2 = xEnd
	ypxl2 = (yEnd).floor
	if steep
		DrawPixel(image, ypxl2, xpxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
		DrawPixel(image, ypxl2 + 1.0, xpxl2, SetBrightness(color, FractionalPart(yEnd)*xGap))
	else
		DrawPixel(image, xpxl2, ypxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
		DrawPixel(image, xpxl2, ypxl2 + 1.0, SetBrightness(color, FractionalPart(yEnd)*xGap))
	end

	if steep
		x = xpxl1 + 1.0
		while(x <= xpxl2 - 1.0)
			DrawPixel(image, (intery).floor, x, SetBrightness(color, OneMinusFractionalPart(intery)))
			DrawPixel(image, (intery).floor + 1.0, x, SetBrightness(color, FractionalPart(intery)))
			intery = intery + g
			x = x + 1.0
		end
	else
		x = xpxl1 + 1.0
		while(x <= xpxl2 - 1.0)
			DrawPixel(image, x, (intery).floor, SetBrightness(color, OneMinusFractionalPart(intery)))
			DrawPixel(image, x, (intery).floor + 1.0, SetBrightness(color, FractionalPart(intery)))
			intery = intery + g
			x = x + 1.0
		end
	end

	color.a = olda
end


def OneMinusFractionalPart(x)
	return 1.0 - FractionalPart(x)
end


def FractionalPart(x)
	return x - (x).floor
end


def SetBrightness(color, newBrightness)
	color.a = newBrightness
	return color
end


def DrawQuadraticBezierCurve(image, x0, y0, cx, cy, x1, y1, color)

	dx = (x0 - x1).abs
	dy = (y0 - y1).abs

	dt = 1.0 / Math.sqrt(dx**2.0 + dy**2.0)

	xs = NumberReference.new
	ys = NumberReference.new
	xe = NumberReference.new
	ye = NumberReference.new

	QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, 0.0, xs, ys)
	t = dt
	while(t <= 1.0)
		QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, xe, ye)
		DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color)
		xs.numberValue = xe.numberValue
		ys.numberValue = ye.numberValue
		t = t + dt
	end

	delete(xs)
	delete(ys)
	delete(xe)
	delete(ye)
end


def QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, x, y)
	x.numberValue = (1.0 - t)**2.0*x0 + (1.0 - t)*2.0*t*cx + t**2.0*x1
	y.numberValue = (1.0 - t)**2.0*y0 + (1.0 - t)*2.0*t*cy + t**2.0*y1
end


def DrawCubicBezierCurve(image, x0, y0, c0x, c0y, c1x, c1y, x1, y1, color)

	dx = (x0 - x1).abs
	dy = (y0 - y1).abs

	dt = 1.0 / Math.sqrt(dx**2.0 + dy**2.0)

	xs = NumberReference.new
	ys = NumberReference.new
	xe = NumberReference.new
	ye = NumberReference.new

	CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, 0.0, xs, ys)
	t = dt
	while(t <= 1.0)
		CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, xe, ye)
		DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color)
		xs.numberValue = xe.numberValue
		ys.numberValue = ye.numberValue
		t = t + dt
	end

	delete(xs)
	delete(ys)
	delete(xe)
	delete(ye)
end


def CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, x, y)
	x.numberValue = (1.0 - t)**3.0*x0 + (1.0 - t)**2.0*3.0*t*c0x + (1.0 - t)*3.0*t**2.0*c1x + t**3.0*x1

	y.numberValue = (1.0 - t)**3.0*y0 + (1.0 - t)**2.0*3.0*t*c0y + (1.0 - t)*3.0*t**2.0*c1y + t**3.0*y1
end


def CopyImage(image)

	copy = CreateImage(ImageWidth(image), ImageHeight(image), GetTransparent())

	i = 0.0
	while(i < ImageWidth(image))
		j = 0.0
		while(j < ImageHeight(image))
			SetPixel(copy, i, j, image.x[i].y[j])
			j = j + 1.0
		end
		i = i + 1.0
	end

	return copy
end


def GetImagePixel(image, x, y)
	return image.x[x].y[y]
end


def HorizontalFlip(img)

	y = 0.0
	while(y < ImageHeight(img))
		x = 0.0
		while(x < ImageWidth(img) / 2.0)
			c1 = img.x[x].y[y]
			c2 = img.x[ImageWidth(img) - 1.0 - x].y[y]

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
		end
		y = y + 1.0
	end
end


def DrawFilledRectangle(image, x, y, w, h, color)

	i = 0.0
	while(i < w)
		j = 0.0
		while(j < h)
			SetPixel(image, x + i, y + j, color)
			j = j + 1.0
		end
		i = i + 1.0
	end
end


def RotateAntiClockwise90Degrees(image)

	rotated = CreateImage(ImageHeight(image), ImageWidth(image), GetBlack())

	y = 0.0
	while(y < ImageHeight(image))
		x = 0.0
		while(x < ImageWidth(image))
			SetPixel(rotated, y, ImageWidth(image) - 1.0 - x, GetImagePixel(image, x, y))
			x = x + 1.0
		end
		y = y + 1.0
	end

	return rotated
end


def DrawCircle(canvas, xCenter, yCenter, radius, color)
	DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color)
end


def BresenhamsCircleDrawingAlgorithm(canvas, xCenter, yCenter, radius, color)

	y = radius
	x = 0.0

	delta = 3.0 - 2.0*radius
	while(y >= x)
		DrawLine1px(canvas, xCenter + x, yCenter + y, xCenter + x, yCenter + y, color)
		DrawLine1px(canvas, xCenter + x, yCenter - y, xCenter + x, yCenter - y, color)
		DrawLine1px(canvas, xCenter - x, yCenter + y, xCenter - x, yCenter + y, color)
		DrawLine1px(canvas, xCenter - x, yCenter - y, xCenter - x, yCenter - y, color)

		DrawLine1px(canvas, xCenter - y, yCenter + x, xCenter - y, yCenter + x, color)
		DrawLine1px(canvas, xCenter - y, yCenter - x, xCenter - y, yCenter - x, color)
		DrawLine1px(canvas, xCenter + y, yCenter + x, xCenter + y, yCenter + x, color)
		DrawLine1px(canvas, xCenter + y, yCenter - x, xCenter + y, yCenter - x, color)

		if delta < 0.0
			delta = delta + 4.0*x + 6.0
		else
			delta = delta + 4.0*(x - y) + 10.0
			y = y - 1.0
		end
		x = x + 1.0
	end
end


def DrawCircleMidpointAlgorithm(canvas, xCenter, yCenter, radius, color)

	d = ((5.0 - radius*4.0) / 4.0).floor
	x = 0.0
	y = radius

	while(x <= y)
		DrawPixel(canvas, xCenter + x, yCenter + y, color)
		DrawPixel(canvas, xCenter + x, yCenter - y, color)
		DrawPixel(canvas, xCenter - x, yCenter + y, color)
		DrawPixel(canvas, xCenter - x, yCenter - y, color)
		DrawPixel(canvas, xCenter + y, yCenter + x, color)
		DrawPixel(canvas, xCenter + y, yCenter - x, color)
		DrawPixel(canvas, xCenter - y, yCenter + x, color)
		DrawPixel(canvas, xCenter - y, yCenter - x, color)

		if d < 0.0
			d = d + 2.0*x + 1.0
		else
			d = d + 2.0*(x - y) + 1.0
			y = y - 1.0
		end
		x = x + 1.0
	end
end


def DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color)

	# Place the circle in the center of the pixel.
	xCenter = (xCenter).floor + 0.5
	yCenter = (yCenter).floor + 0.5

	pixels = 2.0*Math::PI*radius

	# Below a radius of 10 pixels, over-compensate to get a smoother circle.
	if radius < 10.0
		pixels = pixels*10.0
	end

	da = 2.0*Math::PI / pixels

	a = 0.0
	while(a < 2.0*Math::PI)
		dx = Math.cos(a)*radius
		dy = Math.sin(a)*radius

		# Floor to get the pixel coordinate.
		DrawPixel(canvas, (xCenter + dx).floor, (yCenter + dy).floor, color)
		a = a + da
	end
end


def DrawFilledCircle(canvas, x, y, r, color)
	DrawFilledCircleBasicAlgorithm(canvas, x, y, r, color)
end


def DrawFilledCircleMidpointAlgorithm(canvas, xCenter, yCenter, radius, color)

	d = ((5.0 - radius*4.0) / 4.0).floor
	x = 0.0
	y = radius

	while(x <= y)
		DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter + y, xCenter - x, yCenter + y, color)
		DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter - y, xCenter - x, yCenter - y, color)
		DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter + x, xCenter - y, yCenter + x, color)
		DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter - x, xCenter - y, yCenter - x, color)

		if d < 0.0
			d = d + 2.0*x + 1.0
		else
			d = d + 2.0*(x - y) + 1.0
			y = y - 1.0
		end
		x = x + 1.0
	end
end


def DrawFilledCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color)

	# Place the circle in the center of the pixel.
	xCenter = (xCenter).floor + 0.5
	yCenter = (yCenter).floor + 0.5

	pixels = 2.0*Math::PI*radius

	# Below a radius of 10 pixels, over-compensate to get a smoother circle.
	if radius < 10.0
		pixels = pixels*10.0
	end

	da = 2.0*Math::PI / pixels

	# Draw lines for a half-circle to fill an entire circle.
	a = 0.0
	while(a < Math::PI)
		dx = Math.cos(a)*radius
		dy = Math.sin(a)*radius

		# Floor to get the pixel coordinate.
		DrawVerticalLine1px(canvas, (xCenter - dx).floor, (yCenter - dy).floor, (2.0*dy).floor + 1.0, color)
		a = a + da
	end
end


def DrawTriangle(canvas, xCenter, yCenter, height, color)

	x1 = (xCenter + 0.5).floor
	y1 = ((yCenter + 0.5).floor - height).floor
	x2 = x1 - 2.0*height*Math.tan(Math::PI / 6.0)
	y2 = (y1 + 2.0*height).floor
	x3 = x1 + 2.0*height*Math.tan(Math::PI / 6.0)
	y3 = (y1 + 2.0*height).floor

	DrawLine1px(canvas, x1, y1, x2, y2, color)
	DrawLine1px(canvas, x1, y1, x3, y3, color)
	DrawLine1px(canvas, x2, y2, x3, y3, color)
end


def DrawFilledTriangle(canvas, xCenter, yCenter, height, color)

	x1 = (xCenter + 0.5).floor
	y1 = ((yCenter + 0.5).floor - height).floor

	i = 0.0
	while(i <= 2.0*height)
		offset = (i*Math.tan(Math::PI / 6.0)).floor
		DrawHorizontalLine1px(canvas, x1 - offset, y1 + i, 2.0*offset, color)
		i = i + 1.0
	end
end


def DrawLine(canvas, x1, y1, x2, y2, thickness, color)
	DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color)
end


def DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color)

	dx = x2 - x1
	dy = y2 - y1

	incX = Sign(dx)
	incY = Sign(dy)

	dx = (dx).abs
	dy = (dy).abs

	if dx > dy
		pdx = incX
		pdy = 0.0
		es = dy
		el = dx
	else
		pdx = 0.0
		pdy = incY
		es = dx
		el = dy
	end

	x = x1
	y = y1
	err = el / 2.0

	if thickness >= 3.0
		r = thickness / 2.0
		DrawCircle(canvas, x, y, r, color)
	elsif (thickness).floor == 2.0
		DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
	elsif (thickness).floor == 1.0
		DrawPixel(canvas, x, y, color)
	end

	t = 0.0
	while(t < el)
		err = err - es
		if err < 0.0
			err = err + el
			x = x + incX
			y = y + incY
		else
			x = x + pdx
			y = y + pdy
		end

		if thickness >= 3.0
			r = thickness / 2.0
			DrawCircle(canvas, x, y, r, color)
		elsif (thickness).floor == 2.0
			DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
		elsif (thickness).floor == 1.0
			DrawPixel(canvas, x, y, color)
		end
		t = t + 1.0
	end
end


def DrawLineBresenhamsAlgorithm(canvas, x1, y1, x2, y2, color)

	dx = x2 - x1
	dy = y2 - y1

	incX = Sign(dx)
	incY = Sign(dy)

	dx = (dx).abs
	dy = (dy).abs

	if dx > dy
		pdx = incX
		pdy = 0.0
		es = dy
		el = dx
	else
		pdx = 0.0
		pdy = incY
		es = dx
		el = dy
	end

	x = x1
	y = y1
	err = el / 2.0
	DrawPixel(canvas, x, y, color)

	t = 0.0
	while(t < el)
		err = err - es
		if err < 0.0
			err = err + el
			x = x + incX
			y = y + incY
		else
			x = x + pdx
			y = y + pdy
		end

		DrawPixel(canvas, x, y, color)
		t = t + 1.0
	end
end


def DrawLineBresenhamsAlgorithmThickPatterned(canvas, x1, y1, x2, y2, thickness, pattern, offset, color)

	dx = x2 - x1
	dy = y2 - y1

	incX = Sign(dx)
	incY = Sign(dy)

	dx = (dx).abs
	dy = (dy).abs

	if dx > dy
		pdx = incX
		pdy = 0.0
		es = dy
		el = dx
	else
		pdx = 0.0
		pdy = incY
		es = dx
		el = dy
	end

	x = x1
	y = y1
	err = el / 2.0

	offset.numberValue = (offset.numberValue + 1.0)%(pattern.length*thickness)

	if pattern[(offset.numberValue / thickness).floor]
		if thickness >= 3.0
			r = thickness / 2.0
			DrawCircle(canvas, x, y, r, color)
		elsif (thickness).floor == 2.0
			DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
		elsif (thickness).floor == 1.0
			DrawPixel(canvas, x, y, color)
		end
	end

	t = 0.0
	while(t < el)
		err = err - es
		if err < 0.0
			err = err + el
			x = x + incX
			y = y + incY
		else
			x = x + pdx
			y = y + pdy
		end

		offset.numberValue = (offset.numberValue + 1.0)%(pattern.length*thickness)

		if pattern[(offset.numberValue / thickness).floor]
			if thickness >= 3.0
				r = thickness / 2.0
				DrawCircle(canvas, x, y, r, color)
			elsif (thickness).floor == 2.0
				DrawFilledRectangle(canvas, x, y, 2.0, 2.0, color)
			elsif (thickness).floor == 1.0
				DrawPixel(canvas, x, y, color)
			end
		end
		t = t + 1.0
	end
end


def GetLinePattern5()

	pattern = Array.new(19)

	pattern[0] = true
	pattern[1] = true
	pattern[2] = true
	pattern[3] = true
	pattern[4] = true
	pattern[5] = true
	pattern[6] = true
	pattern[7] = true
	pattern[8] = true
	pattern[9] = true
	pattern[10] = false
	pattern[11] = false
	pattern[12] = false
	pattern[13] = true
	pattern[14] = true
	pattern[15] = true
	pattern[16] = false
	pattern[17] = false
	pattern[18] = false

	return pattern
end


def GetLinePattern4()

	pattern = Array.new(13)

	pattern[0] = true
	pattern[1] = true
	pattern[2] = true
	pattern[3] = true
	pattern[4] = true
	pattern[5] = true
	pattern[6] = true
	pattern[7] = true
	pattern[8] = true
	pattern[9] = true
	pattern[10] = false
	pattern[11] = false
	pattern[12] = false

	return pattern
end


def GetLinePattern3()

	pattern = Array.new(13)

	pattern[0] = true
	pattern[1] = true
	pattern[2] = true
	pattern[3] = true
	pattern[4] = true
	pattern[5] = true
	pattern[6] = false
	pattern[7] = false
	pattern[8] = false
	pattern[9] = true
	pattern[10] = true
	pattern[11] = false
	pattern[12] = false

	return pattern
end


def GetLinePattern2()

	pattern = Array.new(4)

	pattern[0] = true
	pattern[1] = true
	pattern[2] = false
	pattern[3] = false

	return pattern
end


def GetLinePattern1()

	pattern = Array.new(8)

	pattern[0] = true
	pattern[1] = true
	pattern[2] = true
	pattern[3] = true
	pattern[4] = true
	pattern[5] = false
	pattern[6] = false
	pattern[7] = false

	return pattern
end


def Blur(src, pixels)

	w = ImageWidth(src)
	h = ImageHeight(src)
	dst = CreateImage(w, h, GetTransparent())

	x = 0.0
	while(x < w)
		y = 0.0
		while(y < h)
			SetPixel(dst, x, y, CreateBlurForPoint(src, x, y, pixels))
			y = y + 1.0
		end
		x = x + 1.0
	end

	return dst
end


def CreateBlurForPoint(src, x, y, pixels)

	w = src.x.length
	h = src.x[0].y.length

	rgba = RGBA.new
	rgba.r = 0.0
	rgba.g = 0.0
	rgba.b = 0.0
	rgba.a = 0.0

	fromx = x - pixels
	fromx = [fromx, 0.0].max

	tox = x + pixels
	tox = [tox, w - 1.0].min

	fromy = y - pixels
	fromy = [fromy, 0.0].max

	toy = y + pixels
	toy = [toy, h - 1.0].min

	countColor = 0.0
	countTransparent = 0.0
	i = fromx
	while(i < tox)
		j = fromy
		while(j < toy)
			alpha = src.x[i].y[j].a
			if alpha > 0.0
				rgba.r = rgba.r + src.x[i].y[j].r
				rgba.g = rgba.g + src.x[i].y[j].g
				rgba.b = rgba.b + src.x[i].y[j].b
				countColor = countColor + 1.0
			end
			rgba.a = rgba.a + alpha
			countTransparent = countTransparent + 1.0
			j = j + 1.0
		end
		i = i + 1.0
	end

	if countColor > 0.0
		rgba.r = rgba.r / countColor
		rgba.g = rgba.g / countColor
		rgba.b = rgba.b / countColor
	else
		rgba.r = 0.0
		rgba.g = 0.0
		rgba.b = 0.0
	end

	if countTransparent > 0.0
		rgba.a = rgba.a / countTransparent
	else
		rgba.a = 0.0
	end

	return rgba
end


def CreateStringScientificNotationDecimalFromNumber(decimal)

	mantissaReference = StringReference.new
	exponentReference = StringReference.new
	result = Array.new(0)
	done = false
	exponent = 0.0

	if decimal < 0.0
		isPositive = false
		decimal = -decimal
	else
		isPositive = true
	end

	if decimal == 0.0
		done = true
	end

	if !done
		multiplier = 0.0
		inc = 0.0

		if decimal < 1.0
			multiplier = 10.0
			inc = -1.0
		elsif decimal >= 10.0
			multiplier = 0.1
			inc = 1.0
		else
			done = true
		end

		if !done
			while(decimal >= 10.0 || decimal < 1.0)
				decimal = decimal*multiplier
				exponent = exponent + inc
			end
		end
	end

	CreateStringFromNumberWithCheck(decimal, 10.0, mantissaReference)

	CreateStringFromNumberWithCheck(exponent, 10.0, exponentReference)

	if !isPositive
		result = AppendString(result, "-".split(""))
	end

	result = AppendString(result, mantissaReference.string)
	result = AppendString(result, "e".split(""))
	result = AppendString(result, exponentReference.string)

	return result
end


def CreateStringDecimalFromNumber(decimal)

	stringReference = StringReference.new

	# This will succeed because base = 10.
	CreateStringFromNumberWithCheck(decimal, 10.0, stringReference)

	return stringReference.string
end


def CreateStringFromNumberWithCheck(decimal, base, stringReference)

	isPositive = true

	if decimal < 0.0
		isPositive = false
		decimal = -decimal
	end

	if decimal == 0.0
		stringReference.string = "0".split("")
		success = true
	else
		characterReference = CharacterReference.new

		if IsInteger(base)
			success = true

			string = Array.new(0)

			maximumDigits = GetMaximumDigitsForBase(base)

			digitPosition = GetFirstDigitPosition(decimal, base)

			decimal = (decimal*base**(maximumDigits - digitPosition - 1.0)).round

			hasPrintedPoint = false

			if !isPositive
				string = AppendCharacter(string, "-")
			end

			# Print leading zeros.
			if digitPosition < 0.0
				string = AppendCharacter(string, "0")
				string = AppendCharacter(string, ".")
				hasPrintedPoint = true
				i = 0.0
				while(i < -digitPosition - 1.0)
					string = AppendCharacter(string, "0")
					i = i + 1.0
				end
			end

			# Print number.
			i = 0.0
			while(i < maximumDigits && success)
				d = (decimal / base**(maximumDigits - i - 1.0)).floor

				if d >= base
					d = base - 1.0
				end

				if !hasPrintedPoint && digitPosition - i + 1.0 == 0.0
					if decimal != 0.0
						string = AppendCharacter(string, ".")
					end
					hasPrintedPoint = true
				end

				if decimal == 0.0 && hasPrintedPoint
				else
					success = GetSingleDigitCharacterFromNumberWithCheck(d, base, characterReference)
					if success
						c = characterReference.characterValue
						string = AppendCharacter(string, c)
					end
				end

				if success
					decimal = decimal - d*base**(maximumDigits - i - 1.0)
				end
				i = i + 1.0
			end

			if success
				# Print trailing zeros.
				i = 0.0
				while(i < digitPosition - maximumDigits + 1.0)
					string = AppendCharacter(string, "0")
					i = i + 1.0
				end

				stringReference.string = string
			end
		else
			success = false
		end
	end

	# Done
	return success
end


def GetMaximumDigitsForBase(base)

	t = 10.0**15.0
	return (Math.log10(t) / Math.log10(base)).floor
end


def GetFirstDigitPosition(decimal, base)

	power = (Math.log10(decimal) / Math.log10(base)).ceil

	t = decimal*base**(-power)
	if t < base && t >= 1.0
	elsif t >= base
		power = power + 1.0
	elsif t < 1.0
		power = power - 1.0
	end

	return power
end


def GetSingleDigitCharacterFromNumberWithCheck(c, base, characterReference)

	numberTable = GetDigitCharacterTable()

	if c < base || c < numberTable.length
		success = true
		characterReference.characterValue = numberTable[c]
	else
		success = false
	end

	return success
end


def GetDigitCharacterTable()

	numberTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".split("")

	return numberTable
end


def CreateNumberFromDecimalStringWithCheck(string, decimalReference, errorMessage)
	return CreateNumberFromStringWithCheck(string, 10.0, decimalReference, errorMessage)
end


def CreateNumberFromDecimalString(string)

	doubleReference = CreateNumberReference(0.0)
	stringReference = CreateStringReference("".split(""))
	CreateNumberFromStringWithCheck(string, 10.0, doubleReference, stringReference)
	number = doubleReference.numberValue

	delete(doubleReference)
	delete(stringReference)

	return number
end


def CreateNumberFromStringWithCheck(string, base, numberReference, errorMessage)

	numberIsPositive = CreateBooleanReference(true)
	exponentIsPositive = CreateBooleanReference(true)
	beforePoint = NumberArrayReference.new
	afterPoint = NumberArrayReference.new
	exponent = NumberArrayReference.new

	if base >= 2.0 && base <= 36.0
		success = ExtractPartsFromNumberString(string, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessage)

		if success
			numberReference.numberValue = CreateNumberFromParts(base, numberIsPositive.booleanValue, beforePoint.numberArray, afterPoint.numberArray, exponentIsPositive.booleanValue, exponent.numberArray)
		end
	else
		success = false
		errorMessage.string = "Base must be from 2 to 36.".split("")
	end

	return success
end


def CreateNumberFromParts(base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent)

	n = 0.0

	i = 0.0
	while(i < beforePoint.length)
		p = beforePoint[beforePoint.length - i - 1.0]

		n = n + p*base**i
		i = i + 1.0
	end

	i = 0.0
	while(i < afterPoint.length)
		p = afterPoint[i]

		n = n + p*base**(-(i + 1.0))
		i = i + 1.0
	end

	if exponent.length > 0.0
		e = 0.0
		i = 0.0
		while(i < exponent.length)
			p = exponent[exponent.length - i - 1.0]

			e = e + p*base**i
			i = i + 1.0
		end

		if !exponentIsPositive
			e = -e
		end

		n = n*base**e
	end

	if !numberIsPositive
		n = -n
	end

	return n
end


def ExtractPartsFromNumberString(n, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages)

	i = 0.0

	if i < n.length
		if n[i] == "-"
			numberIsPositive.booleanValue = false
			i = i + 1.0
		elsif n[i] == "+"
			numberIsPositive.booleanValue = true
			i = i + 1.0
		end

		success = ExtractPartsFromNumberStringFromSign(n, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages)
	else
		success = false
		errorMessages.string = "Number cannot have length zero.".split("")
	end

	return success
end


def ExtractPartsFromNumberStringFromSign(n, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages)

	done = false
	count = 0.0
	while(i + count < n.length && !done)
		if CharacterIsNumberCharacterInBase(n[i + count], base)
			count = count + 1.0
		else
			done = true
		end
	end

	if count >= 1.0
		beforePoint.numberArray = Array.new(count)

		j = 0.0
		while(j < count)
			beforePoint.numberArray[j] = GetNumberFromNumberCharacterForBase(n[i + j], base)
			j = j + 1.0
		end

		i = i + count

		if i < n.length
			success = ExtractPartsFromNumberStringFromPointOrExponent(n, base, i, afterPoint, exponentIsPositive, exponent, errorMessages)
		else
			afterPoint.numberArray = Array.new(0)
			exponent.numberArray = Array.new(0)
			success = true
		end
	else
		success = false
		errorMessages.string = "Number must have at least one number after the optional sign.".split("")
	end

	return success
end


def ExtractPartsFromNumberStringFromPointOrExponent(n, base, i, afterPoint, exponentIsPositive, exponent, errorMessages)

	if n[i] == "."
		i = i + 1.0

		if i < n.length
			done = false
			count = 0.0
			while(i + count < n.length && !done)
				if CharacterIsNumberCharacterInBase(n[i + count], base)
					count = count + 1.0
				else
					done = true
				end
			end

			if count >= 1.0
				afterPoint.numberArray = Array.new(count)

				j = 0.0
				while(j < count)
					afterPoint.numberArray[j] = GetNumberFromNumberCharacterForBase(n[i + j], base)
					j = j + 1.0
				end

				i = i + count

				if i < n.length
					success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages)
				else
					exponent.numberArray = Array.new(0)
					success = true
				end
			else
				success = false
				errorMessages.string = "There must be at least one digit after the decimal point.".split("")
			end
		else
			success = false
			errorMessages.string = "There must be at least one digit after the decimal point.".split("")
		end
	elsif base <= 14.0 && (n[i] == "e" || n[i] == "E")
		if i < n.length
			success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages)
			afterPoint.numberArray = Array.new(0)
		else
			success = false
			errorMessages.string = "There must be at least one digit after the exponent.".split("")
		end
	else
		success = false
		errorMessages.string = "Expected decimal point or exponent symbol.".split("")
	end

	return success
end


def ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages)

	if base <= 14.0 && (n[i] == "e" || n[i] == "E")
		i = i + 1.0

		if i < n.length
			if n[i] == "-"
				exponentIsPositive.booleanValue = false
				i = i + 1.0
			elsif n[i] == "+"
				exponentIsPositive.booleanValue = true
				i = i + 1.0
			end

			if i < n.length
				done = false
				count = 0.0
				while(i + count < n.length && !done)
					if CharacterIsNumberCharacterInBase(n[i + count], base)
						count = count + 1.0
					else
						done = true
					end
				end

				if count >= 1.0
					exponent.numberArray = Array.new(count)

					j = 0.0
					while(j < count)
						exponent.numberArray[j] = GetNumberFromNumberCharacterForBase(n[i + j], base)
						j = j + 1.0
					end

					i = i + count

					if i == n.length
						success = true
					else
						success = false
						errorMessages.string = "There cannot be any characters past the exponent of the number.".split("")
					end
				else
					success = false
					errorMessages.string = "There must be at least one digit after the decimal point.".split("")
				end
			else
				success = false
				errorMessages.string = "There must be at least one digit after the exponent symbol.".split("")
			end
		else
			success = false
			errorMessages.string = "There must be at least one digit after the exponent symbol.".split("")
		end
	else
		success = false
		errorMessages.string = "Expected exponent symbol.".split("")
	end

	return success
end


def GetNumberFromNumberCharacterForBase(c, base)

	numberTable = GetDigitCharacterTable()
	position = 0.0

	i = 0.0
	while(i < base)
		if numberTable[i] == c
			position = i
		end
		i = i + 1.0
	end

	return position
end


def CharacterIsNumberCharacterInBase(c, base)

	numberTable = GetDigitCharacterTable()
	found = false

	i = 0.0
	while(i < base)
		if numberTable[i] == c
			found = true
		end
		i = i + 1.0
	end

	return found
end


def StringToNumberArray(str)

	numberArrayReference = NumberArrayReference.new
	stringReference = StringReference.new

	StringToNumberArrayWithCheck(str, numberArrayReference, stringReference)

	numbers = numberArrayReference.numberArray

	delete(numberArrayReference)
	delete(stringReference)

	return numbers
end


def StringToNumberArrayWithCheck(str, numberArrayReference, errorMessage)

	numberStrings = SplitByString(str, ",".split(""))

	numbers = Array.new(numberStrings.length)
	success = true
	numberReference = NumberReference.new

	i = 0.0
	while(i < numberStrings.length)
		numberString = numberStrings[i].string
		trimmedNumberString = Trim(numberString)
		success = CreateNumberFromDecimalStringWithCheck(trimmedNumberString, numberReference, errorMessage)
		numbers[i] = numberReference.numberValue

		FreeStringReference(numberStrings[i])
		delete(trimmedNumberString)
		i = i + 1.0
	end

	delete(numberStrings)
	delete(numberReference)

	numberArrayReference.numberArray = numbers

	return success
end


def Negate(x)
	return -x
end


def Positive(x)
	return +x
end


def Factorial(x)

	f = 1.0

	i = 2.0
	while(i <= x)
		f = f*i
		i = i + 1.0
	end

	return f
end


def Round(x)
	return (x + 0.5).floor
end


def BankersRound(x)

	if Absolute(x - Truncate(x)) == 0.5
		if !DivisibleBy(Round(x), 2.0)
			r = Round(x) - 1.0
		else
			r = Round(x)
		end
	else
		r = Round(x)
	end

	return r
end


def Ceil(x)
	return (x).ceil
end


def Floor(x)
	return (x).floor
end


def Truncate(x)

	if x >= 0.0
		t = (x).floor
	else
		t = (x).ceil
	end

	return t
end


def Absolute(x)
	return (x).abs
end


def Logarithm(x)
	return Math.log10(x)
end


def NaturalLogarithm(x)
	return Math.log(x)
end


def Sin(x)
	return Math.sin(x)
end


def Cos(x)
	return Math.cos(x)
end


def Tan(x)
	return Math.tan(x)
end


def Asin(x)
	return Math.asin(x)
end


def Acos(x)
	return Math.acos(x)
end


def Atan(x)
	return Math.atan(x)
end


def Atan2(y, x)

	# Atan2 is an invalid operation when x = 0 and y = 0, but this method does not return errors.
	a = 0.0

	if x > 0.0
		a = Atan(y / x)
	elsif x < 0.0 && y >= 0.0
		a = Atan(y / x) + Math::PI
	elsif x < 0.0 && y < 0.0
		a = Atan(y / x) - Math::PI
	elsif x == 0.0 && y > 0.0
		a = Math::PI / 2.0
	elsif x == 0.0 && y < 0.0
		a = -Math::PI / 2.0
	end

	return a
end


def Squareroot(x)
	return Math.sqrt(x)
end


def Exp(x)
	return Math.exp(x)
end


def DivisibleBy(a, b)
	return ((a%b) == 0.0)
end


def Combinations(n, k)

	c = 1.0
	j = 1.0
	i = n - k + 1.0

	while(i <= n)
		c = c*i
		c = c / j

		i = i + 1.0
		j = j + 1.0
	end

	return c
end


def Permutations(n, k)

	c = 1.0

	i = n - k + 1.0
	while(i <= n)
		c = c*i
		i = i + 1.0
	end

	return c
end


def EpsilonCompare(a, b, epsilon)
	return (a - b).abs < epsilon
end


def GreatestCommonDivisor(a, b)

	while(b != 0.0)
		t = b
		b = a%b
		a = t
	end

	return a
end


def GCDWithSubtraction(a, b)

	if a == 0.0
		g = b
	else
		while(b != 0.0)
			if a > b
				a = a - b
			else
				b = b - a
			end
		end

		g = a
	end

	return g
end


def IsInteger(a)
	return (a - (a).floor) == 0.0
end


def GreatestCommonDivisorWithCheck(a, b, gcdReference)

	if IsInteger(a) && IsInteger(b)
		gcd = GreatestCommonDivisor(a, b)
		gcdReference.numberValue = gcd
		success = true
	else
		success = false
	end

	return success
end


def LeastCommonMultiple(a, b)

	if a > 0.0 && b > 0.0
		lcm = (a*b).abs / GreatestCommonDivisor(a, b)
	else
		lcm = 0.0
	end

	return lcm
end


def Sign(a)

	if a > 0.0
		s = 1.0
	elsif a < 0.0
		s = -1.0
	else
		s = 0.0
	end

	return s
end


def Max(a, b)
	return [a, b].max
end


def Min(a, b)
	return [a, b].min
end


def Power(a, b)
	return a**b
end


def Gamma(x)
	return LanczosApproximation(x)
end


def LogGamma(x)
	return Math.log(Gamma(x))
end


def LanczosApproximation(z)

	p = Array.new(8)
	p[0] = 676.5203681218851
	p[1] = -1259.1392167224028
	p[2] = 771.32342877765313
	p[3] = -176.61502916214059
	p[4] = 12.507343278686905
	p[5] = -0.13857109526572012
	p[6] = 9.9843695780195716e-6
	p[7] = 1.5056327351493116e-7

	if z < 0.5
		y = Math::PI / (Math.sin(Math::PI*z)*LanczosApproximation(1.0 - z))
	else
		z = z - 1.0
		x = 0.99999999999980993
		i = 0.0
		while(i < p.length)
			x = x + p[i] / (z + i + 1.0)
			i = i + 1.0
		end
		t = z + p.length - 0.5
		y = Math.sqrt(2.0*Math::PI)*t**(z + 0.5)*Math.exp(-t)*x
	end

	return y
end


def Beta(x, y)
	return Gamma(x)*Gamma(y) / Gamma(x + y)
end


def Sinh(x)
	return (Math.exp(x) - Math.exp(-x)) / 2.0
end


def Cosh(x)
	return (Math.exp(x) + Math.exp(-x)) / 2.0
end


def Tanh(x)
	return Sinh(x) / Cosh(x)
end


def Cot(x)
	return 1.0 / Math.tan(x)
end


def Sec(x)
	return 1.0 / Math.cos(x)
end


def Csc(x)
	return 1.0 / Math.sin(x)
end


def Coth(x)
	return Cosh(x) / Sinh(x)
end


def Sech(x)
	return 1.0 / Cosh(x)
end


def Csch(x)
	return 1.0 / Sinh(x)
end


def Error(x)

	if x == 0.0
		y = 0.0
	elsif x < 0.0
		y = -Error(-x)
	else
		c1 = -1.26551223
		c2 = +1.00002368
		c3 = +0.37409196
		c4 = +0.09678418
		c5 = -0.18628806
		c6 = +0.27886807
		c7 = -1.13520398
		c8 = +1.48851587
		c9 = -0.82215223
		c10 = +0.17087277

		t = 1.0 / (1.0 + 0.5*(x).abs)

		tau = t*Math.exp(-x**2.0 + c1 + t*(c2 + t*(c3 + t*(c4 + t*(c5 + t*(c6 + t*(c7 + t*(c8 + t*(c9 + t*c10)))))))))

		y = 1.0 - tau
	end

	return y
end


def ErrorInverse(x)

	a = (8.0*(Math::PI - 3.0)) / (3.0*Math::PI*(4.0 - Math::PI))

	t = 2.0 / (Math::PI*a) + Math.log(1.0 - x**2.0) / 2.0
	y = Sign(x)*Math.sqrt(Math.sqrt(t**2.0 - Math.log(1.0 - x**2.0) / a) - t)

	return y
end


def FallingFactorial(x, n)

	y = 1.0

	k = 0.0
	while(k <= n - 1.0)
		y = y*(x - k)
		k = k + 1.0
	end

	return y
end


def RisingFactorial(x, n)

	y = 1.0

	k = 0.0
	while(k <= n - 1.0)
		y = y*(x + k)
		k = k + 1.0
	end

	return y
end


def Hypergeometric(a, b, c, z, maxIterations, precision)

	if (z).abs >= 0.5
		y = (1.0 - z)**(-a)*HypergeometricDirect(a, c - b, c, z / (z - 1.0), maxIterations, precision)
	else
		y = HypergeometricDirect(a, b, c, z, maxIterations, precision)
	end

	return y
end


def HypergeometricDirect(a, b, c, z, maxIterations, precision)

	y = 0.0
	done = false

	n = 0.0
	while(n < maxIterations && !done)
		yp = RisingFactorial(a, n)*RisingFactorial(b, n) / RisingFactorial(c, n)*z**n / Factorial(n)
		if (yp).abs < precision
			done = true
		end
		y = y + yp
		n = n + 1.0
	end

	return y
end


def BernouilliNumber(n)
	return AkiyamaTanigawaAlgorithm(n)
end


def AkiyamaTanigawaAlgorithm(n)

	a = Array.new(n + 1.0)

	m = 0.0
	while(m <= n)
		a[m] = 1.0 / (m + 1.0)
		j = m
		while(j >= 1.0)
			a[j - 1.0] = j*(a[j - 1.0] - a[j])
			j = j - 1.0
		end
		m = m + 1.0
	end

	b = a[0]

	delete(a)

	return b
end


def aStringToNumberArray(string)

	array = Array.new(string.length)

	i = 0.0
	while(i < string.length)
		array[i] = (string[i]).ord
		i = i + 1.0
	end
	return array
end


def aNumberArrayToString(array)

	string = Array.new(array.length)

	i = 0.0
	while(i < array.length)
		string[i] = (array[i]).truncate.chr('UTF-8')
		i = i + 1.0
	end
	return string
end


def aNumberArraysEqual(a, b)

	equal = true
	if a.length == b.length
		i = 0.0
		while(i < a.length && equal)
			if a[i] != b[i]
				equal = false
			end
			i = i + 1.0
		end
	else
		equal = false
	end

	return equal
end


def aBooleanArraysEqual(a, b)

	equal = true
	if a.length == b.length
		i = 0.0
		while(i < a.length && equal)
			if a[i] != b[i]
				equal = false
			end
			i = i + 1.0
		end
	else
		equal = false
	end

	return equal
end


def aStringsEqual(a, b)

	equal = true
	if a.length == b.length
		i = 0.0
		while(i < a.length && equal)
			if a[i] != b[i]
				equal = false
			end
			i = i + 1.0
		end
	else
		equal = false
	end

	return equal
end


def aFillNumberArray(a, value)

	i = 0.0
	while(i < a.length)
		a[i] = value
		i = i + 1.0
	end
end


def aFillString(a, value)

	i = 0.0
	while(i < a.length)
		a[i] = value
		i = i + 1.0
	end
end


def aFillBooleanArray(a, value)

	i = 0.0
	while(i < a.length)
		a[i] = value
		i = i + 1.0
	end
end


def aFillNumberArrayRange(a, value, from, to)

	if from >= 0.0 && from <= a.length && to >= 0.0 && to <= a.length && from <= to
		length = to - from
		i = 0.0
		while(i < length)
			a[from + i] = value
			i = i + 1.0
		end

		success = true
	else
		success = false
	end

	return success
end


def aFillBooleanArrayRange(a, value, from, to)

	if from >= 0.0 && from <= a.length && to >= 0.0 && to <= a.length && from <= to
		length = to - from
		i = 0.0
		while(i < length)
			a[from + i] = value
			i = i + 1.0
		end

		success = true
	else
		success = false
	end

	return success
end


def aFillStringRange(a, value, from, to)

	if from >= 0.0 && from <= a.length && to >= 0.0 && to <= a.length && from <= to
		length = to - from
		i = 0.0
		while(i < length)
			a[from + i] = value
			i = i + 1.0
		end

		success = true
	else
		success = false
	end

	return success
end


def aCopyNumberArray(a)

	n = Array.new(a.length)

	i = 0.0
	while(i < a.length)
		n[i] = a[i]
		i = i + 1.0
	end

	return n
end


def aCopyBooleanArray(a)

	n = Array.new(a.length)

	i = 0.0
	while(i < a.length)
		n[i] = a[i]
		i = i + 1.0
	end

	return n
end


def aCopyString(a)

	n = Array.new(a.length)

	i = 0.0
	while(i < a.length)
		n[i] = a[i]
		i = i + 1.0
	end

	return n
end


def aCopyNumberArrayRange(a, from, to, copyReference)

	if from >= 0.0 && from <= a.length && to >= 0.0 && to <= a.length && from <= to
		length = to - from
		n = Array.new(length)

		i = 0.0
		while(i < length)
			n[i] = a[from + i]
			i = i + 1.0
		end

		copyReference.numberArray = n
		success = true
	else
		success = false
	end

	return success
end


def aCopyBooleanArrayRange(a, from, to, copyReference)

	if from >= 0.0 && from <= a.length && to >= 0.0 && to <= a.length && from <= to
		length = to - from
		n = Array.new(length)

		i = 0.0
		while(i < length)
			n[i] = a[from + i]
			i = i + 1.0
		end

		copyReference.booleanArray = n
		success = true
	else
		success = false
	end

	return success
end


def aCopyStringRange(a, from, to, copyReference)

	if from >= 0.0 && from <= a.length && to >= 0.0 && to <= a.length && from <= to
		length = to - from
		n = Array.new(length)

		i = 0.0
		while(i < length)
			n[i] = a[from + i]
			i = i + 1.0
		end

		copyReference.string = n
		success = true
	else
		success = false
	end

	return success
end


def aIsLastElement(length, index)
	return index + 1.0 == length
end


def aCreateNumberArray(length, value)

	array = Array.new(length)
	aFillNumberArray(array, value)

	return array
end


def aCreateBooleanArray(length, value)

	array = Array.new(length)
	aFillBooleanArray(array, value)

	return array
end


def aCreateString(length, value)

	array = Array.new(length)
	aFillString(array, value)

	return array
end


def aSwapElementsOfNumberArray(a, ai, bi)

	tmp = a[ai]
	a[ai] = a[bi]
	a[bi] = tmp
end


def aSwapElementsOfStringArray(a, ai, bi)

	tmp = a.stringArray[ai]
	a.stringArray[ai] = a.stringArray[bi]
	a.stringArray[bi] = tmp
end


def aReverseNumberArray(array)

	i = 0.0
	while(i < array.length / 2.0)
		aSwapElementsOfNumberArray(array, i, array.length - i - 1.0)
		i = i + 1.0
	end
end


def CreateBooleanReference(value)

	ref = BooleanReference.new
	ref.booleanValue = value

	return ref
end


def CreateBooleanArrayReference(value)

	ref = BooleanArrayReference.new
	ref.booleanArray = value

	return ref
end


def CreateBooleanArrayReferenceLengthValue(length, value)

	ref = BooleanArrayReference.new
	ref.booleanArray = Array.new(length)

	i = 0.0
	while(i < length)
		ref.booleanArray[i] = value
		i = i + 1.0
	end

	return ref
end


def FreeBooleanArrayReference(booleanArrayReference)
	delete(booleanArrayReference.booleanArray)
	delete(booleanArrayReference)
end


def CreateCharacterReference(value)

	ref = CharacterReference.new
	ref.characterValue = value

	return ref
end


def CreateNumberReference(value)

	ref = NumberReference.new
	ref.numberValue = value

	return ref
end


def CreateNumberArrayReference(value)

	ref = NumberArrayReference.new
	ref.numberArray = value

	return ref
end


def CreateNumberArrayReferenceLengthValue(length, value)

	ref = NumberArrayReference.new
	ref.numberArray = Array.new(length)

	i = 0.0
	while(i < length)
		ref.numberArray[i] = value
		i = i + 1.0
	end

	return ref
end


def FreeNumberArrayReference(numberArrayReference)
	delete(numberArrayReference.numberArray)
	delete(numberArrayReference)
end


def CreateStringReference(value)

	ref = StringReference.new
	ref.string = value

	return ref
end


def CreateStringReferenceLengthValue(length, value)

	ref = StringReference.new
	ref.string = Array.new(length)

	i = 0.0
	while(i < length)
		ref.string[i] = value
		i = i + 1.0
	end

	return ref
end


def FreeStringReference(stringReference)
	delete(stringReference.string)
	delete(stringReference)
end


def CreateStringArrayReference(strings)

	ref = StringArrayReference.new
	ref.stringArray = strings

	return ref
end


def CreateStringArrayReferenceLengthValue(length, value)

	ref = StringArrayReference.new
	ref.stringArray = Array.new(length)

	i = 0.0
	while(i < length)
		ref.stringArray[i] = CreateStringReference(value)
		i = i + 1.0
	end

	return ref
end


def FreeStringArrayReference(stringArrayReference)

	i = 0.0
	while(i < stringArrayReference.stringArray.length)
		delete(stringArrayReference.stringArray[i])
		i = i + 1.0
	end
	delete(stringArrayReference.stringArray)
	delete(stringArrayReference)
end


def DigitDataBase16()
	return "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe891412108153069c4ffffffffffffffffffffffffffffffffffffffff9409000000000000000049e7ffffffffffffffffffffffffffffffffff61000000000000000000000017ddffffffffffffffffffffffffffffff840000000573d3f5e5a62b00000028f0ffffffffffffffffffffffffffda04000008bcfffffffffff44200000073ffffffffffffffffffffffffff5700000088ffffffffffffffe812000008e3ffffffffffffffffffffffea02000015f9ffffffffffffffff8100000080ffffffffffffffffffffff9c00000072ffffffffffffffffffe40100002fffffffffffffffffffffff51000000b8ffffffffffffffffffff2a000000e2ffffffffffffffffffff21000001f0ffffffffffffffffffff65000000b3fffffffffffffffffff602000018ffffffffffffffffffffff8b0000008affffffffffffffffffd200000036ffffffffffffffffffffffa900000063ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffb500000057ffffffffffffffffffffffc900000046ffffffffffffffffffa90000005fffffffffffffffffffffffd20000003affffffffffffffffffa900000060ffffffffffffffffffffffd30000003affffffffffffffffffb400000057ffffffffffffffffffffffca00000046ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffd100000037ffffffffffffffffffffffa900000063fffffffffffffffffff602000019ffffffffffffffffffffff8b00000089ffffffffffffffffffff21000001f1ffffffffffffffffffff66000000b3ffffffffffffffffffff50000000b8ffffffffffffffffffff2a000000e1ffffffffffffffffffff9c00000073ffffffffffffffffffe40100002fffffffffffffffffffffffea02000015f9ffffffffffffffff8200000080ffffffffffffffffffffffff5700000088ffffffffffffffe812000008e2ffffffffffffffffffffffffda04000008bcfffffffffff44300000073ffffffffffffffffffffffffffff830000000674d3f6e6a72b00000028f0ffffffffffffffffffffffffffffff60000000000000000000000016ddfffffffffffffffffffffffffffffffffe9309000000000000000048e6ffffffffffffffffffffffffffffffffffffffe88f3f1f07132e68c3fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9d7b28e69441f02000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6a274c7095b9de64000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000affffffffffffffffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffd48b56271005142a5ea0f6ffffffffffffffffffffffffffffffffdb7c20000000000000000000001392feffffffffffffffffffffffffffff1f00000000000000000000000000004cf9ffffffffffffffffffffffffff1f0000003784c7e7f9e8b1480000000056ffffffffffffffffffffffffff1f015accffffffffffffffff9701000000b0ffffffffffffffffffffffff58caffffffffffffffffffffff770000003cfffffffffffffffffffffffffffffffffffffffffffffffffff107000002edffffffffffffffffffffffffffffffffffffffffffffffffff3a000000ccffffffffffffffffffffffffffffffffffffffffffffffffff4c000000baffffffffffffffffffffffffffffffffffffffffffffffffff32000000cbffffffffffffffffffffffffffffffffffffffffffffffffec05000002edffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffeb140000009affffffffffffffffffffffffffffffffffffffffffffffff520000002afbffffffffffffffffffffffffffffffffffffffffffffff8c00000003c7ffffffffffffffffffffffffffffffffffffffffffffffb30300000085ffffffffffffffffffffffffffffffffffffffffffffffc50a0000005dfeffffffffffffffffffffffffffffffffffffffffffffd2110000004efbffffffffffffffffffffffffffffffffffffffffffffdb1800000042f8ffffffffffffffffffffffffffffffffffffffffffffe21f00000039f3ffffffffffffffffffffffffffffffffffffffffffffe92600000030efffffffffffffffffffffffffffffffffffffffffffffee2e00000029eafffffffffffffffffffffffffffffffffffffffffffff33700000022e5fffffffffffffffffffffffffffffffffffffffffffff7410000001cdffffffffffffffffffffffffffffffffffffffffffffffb4c00000017d9fffffffffffffffffffffffffffffffffffffffffffffd5900000012d2ffffffffffffffffffffffffffffffffffffffffffffff680000000ecbffffffffffffffffffffffffffffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe2af8058392817060a1a3f74c8ffffffffffffffffffffffffffffffffeb0000000000000000000000000036cfffffffffffffffffffffffffffffeb000000000000000000000000000004a7ffffffffffffffffffffffffffeb00000f5a9dd0edfbf0ca841900000003c2ffffffffffffffffffffffffec3da8f9fffffffffffffffff0410000002bffffffffffffffffffffffffffffffffffffffffffffffffffee12000000cbffffffffffffffffffffffffffffffffffffffffffffffffff6900000090ffffffffffffffffffffffffffffffffffffffffffffffffff9600000078ffffffffffffffffffffffffffffffffffffffffffffffffff9a0000007effffffffffffffffffffffffffffffffffffffffffffffffff73000000a5fffffffffffffffffffffffffffffffffffffffffffffffff51b000009edfffffffffffffffffffffffffffffffffffffffffffffff7540000007efffffffffffffffffffffffffffffffffffffffffff3d3912400000055fcffffffffffffffffffffffffffffffffff1700000000000000001692feffffffffffffffffffffffffffffffffffff17000000000000002db8feffffffffffffffffffffffffffffffffffffff170000000000000000002bc3fffffffffffffffffffffffffffffffffffffffffffdf0cf922e00000003a5fffffffffffffffffffffffffffffffffffffffffffffffffd8700000007d1ffffffffffffffffffffffffffffffffffffffffffffffffff780000004ffffffffffffffffffffffffffffffffffffffffffffffffffff308000006f6ffffffffffffffffffffffffffffffffffffffffffffffffff3c000000d0ffffffffffffffffffffffffffffffffffffffffffffffffff4d000000c6ffffffffffffffffffffffffffffffffffffffffffffffffff35000000ddffffffffffffffffffffffffffffffffffffffffffffffffea0300000bf9ffffffffffffffffffffffffffffffffffffffffffffffff6200000054ffffffffffffffffffffff47bafefffffffffffffffffff56b00000002cbffffffffffffffffffffff0b001e71a9d7edfbf6e4ba771a000000007cffffffffffffffffffffffff0b0000000000000000000000000000017dffffffffffffffffffffffffff0b000000000000000000000000003cc8ffffffffffffffffffffffffffffe9b989593827160608162a5689dbffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffbd0100000000f3fffffffffffffffffffffffffffffffffffffffffffff3200000000000f3ffffffffffffffffffffffffffffffffffffffffffff69000000000000f3ffffffffffffffffffffffffffffffffffffffffffbf01000b0e000000f3fffffffffffffffffffffffffffffffffffffffff42100008e1f000000f3ffffffffffffffffffffffffffffffffffffffff6a000035fc1f000000f3ffffffffffffffffffffffffffffffffffffffc0010004d1ff1f000000f3fffffffffffffffffffffffffffffffffffff42200007affff1f000000f3ffffffffffffffffffffffffffffffffffff6c000026f7ffff1f000000f3ffffffffffffffffffffffffffffffffffc1010001c1ffffff1f000000f3fffffffffffffffffffffffffffffffff523000066ffffffff1f000000f3ffffffffffffffffffffffffffffffff6d000019f0ffffffff1f000000f3ffffffffffffffffffffffffffffffc2010000aeffffffffff1f000000f3fffffffffffffffffffffffffffff524000052ffffffffffff1f000000f3ffffffffffffffffffffffffffff6e00000fe6ffffffffffff1f000000f3ffffffffffffffffffffffffffc30200009affffffffffffff1f000000f3fffffffffffffffffffffffff62400003ffeffffffffffffff1f000000f3ffffffffffffffffffffffff70000008daffffffffffffffff1f000000f3fffffffffffffffffffffff602000086ffffffffffffffffff1f000000f3fffffffffffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f000008672f120514275997efffffffffffffffffffffffffffffffffff4f00000000000000000000000b73f6ffffffffffffffffffffffffffffff4f000000000000000000000000002bdeffffffffffffffffffffffffffff60538cbad2e7faf0d599370000000025ebffffffffffffffffffffffffffffffffffffffffffffffffa0090000005bffffffffffffffffffffffffffffffffffffffffffffffffffb100000001d2ffffffffffffffffffffffffffffffffffffffffffffffffff560000007effffffffffffffffffffffffffffffffffffffffffffffffffb80000003dffffffffffffffffffffffffffffffffffffffffffffffffffec00000022fffffffffffffffffffffffffffffffffffffffffffffffffffd00000011ffffffffffffffffffffffffffffffffffffffffffffffffffec00000022ffffffffffffffffffffffffffffffffffffffffffffffffffb80000003cffffffffffffffffffffffffffffffffffffffffffffffffff580000007dffffffffffffffffffffffffffffffffffffffffffffffffb301000000cfffffffffffffffffffffff4cb1fdffffffffffffffffffa40a00000058ffffffffffffffffffffffff17001a6ea9d7eefbf2d69b380000000024e8ffffffffffffffffffffffff1700000000000000000000000000002de0ffffffffffffffffffffffffff17000000000000000000000000127ef9ffffffffffffffffffffffffffffebba8a59372615050a1a3569a6f7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffca753915050d233866a3e0ffffffffffffffffffffffffffffffffffd13f0000000000000000000000f7ffffffffffffffffffffffffffffff9d07000000000000000000000000f7ffffffffffffffffffffffffffff9700000000469fdbf3f5da9e490100f7ffffffffffffffffffffffffffca0300000eb3ffffffffffffffffd84df8fffffffffffffffffffffffffa2d000007c8ffffffffffffffffffffffffffffffffffffffffffffffff9100000081ffffffffffffffffffffffffffffffffffffffffffffffffff28000010f6ffffffffffffffffffffffffffffffffffffffffffffffffc20000006affffffffffffffffffffffffffffffffffffffffffffffffff79000000b2ffffffffffffffffffffffffffffffffffffffffffffffffff43000000ebffeb903d1a0616306fc0ffffffffffffffffffffffffffffff0f000015ffa211000000000000000041dcfffffffffffffffffffffffff30000003087000000000000000000000013c6ffffffffffffffffffffffe30000000f00000055beeef7d8881000000017e6ffffffffffffffffffffd30000000000019dffffffffffffe12200000056ffffffffffffffffffffd100000000006effffffffffffffffce04000002dbffffffffffffffffffdd0000000006eaffffffffffffffffff550000008bffffffffffffffffffe90000000043ffffffffffffffffffffa90000004dfffffffffffffffffff80200000074ffffffffffffffffffffdb0000002cffffffffffffffffffff2200000088ffffffffffffffffffffef00000019ffffffffffffffffffff4d00000088ffffffffffffffffffffee0000001affffffffffffffffffff7e00000074ffffffffffffffffffffdb0000002dffffffffffffffffffffcd00000042ffffffffffffffffffffa900000052ffffffffffffffffffffff21000005e9ffffffffffffffffff5400000093ffffffffffffffffffffff8f0000006dffffffffffffffffcd04000007e6fffffffffffffffffffffff9220000019effffffffffffe1230000006cffffffffffffffffffffffffffc00600000056beeff8d888110000002af3ffffffffffffffffffffffffffffa603000000000000000000000026ddffffffffffffffffffffffffffffffffc8280000000000000000025deffffffffffffffffffffffffffffffffffffffab25a2a1106193b7ed7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff47000000000000000000000000000000000000f7ffffffffffffffffffff47000000000000000000000000000000000003faffffffffffffffffffff4700000000000000000000000000000000004afffffffffffffffffffffffffffffffffffffffffffffffffc1a000000adffffffffffffffffffffffffffffffffffffffffffffffffb300000015faffffffffffffffffffffffffffffffffffffffffffffffff5100000073ffffffffffffffffffffffffffffffffffffffffffffffffea05000000d6ffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffff2c0000009dffffffffffffffffffffffffffffffffffffffffffffffffc90000000cf3ffffffffffffffffffffffffffffffffffffffffffffffff6700000063fffffffffffffffffffffffffffffffffffffffffffffffff60f000000c6ffffffffffffffffffffffffffffffffffffffffffffffffa300000029ffffffffffffffffffffffffffffffffffffffffffffffffff410000008cffffffffffffffffffffffffffffffffffffffffffffffffdf01000005e9ffffffffffffffffffffffffffffffffffffffffffffffff7d00000052fffffffffffffffffffffffffffffffffffffffffffffffffd1e000000b5ffffffffffffffffffffffffffffffffffffffffffffffffb90000001bfcffffffffffffffffffffffffffffffffffffffffffffffff570000007bffffffffffffffffffffffffffffffffffffffffffffffffee07000001ddffffffffffffffffffffffffffffffffffffffffffffffff9300000042ffffffffffffffffffffffffffffffffffffffffffffffffff31000000a5ffffffffffffffffffffffffffffffffffffffffffffffffd000000010f7ffffffffffffffffffffffffffffffffffffffffffffffff6d0000006bfffffffffffffffffffffffffffffffffffffffffffffffff913000000ceffffffffffffffffffffffffffffffffffffffffffffffffa900000031ffffffffffffffffffffffffffffffffffffffffffffffffff4700000094ffffffffffffffffffffffffffffffffffffffffffffffffe302000008eeffffffffffffffffffffffffffffffffffffffffffffffff840000005afffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9a8602c13050c1d4882dfffffffffffffffffffffffffffffffffffffa918000000000000000000025eeeffffffffffffffffffffffffffffff780000000000000000000000000023e5ffffffffffffffffffffffffff9f0000000037a8e4faf1c66d0500000033fdfffffffffffffffffffffff81600000065fdffffffffffffc40a0000009fffffffffffffffffffffffb600000021faffffffffffffffff8d00000047ffffffffffffffffffffff820000007bffffffffffffffffffeb01000014ffffffffffffffffffffff6d000000a2ffffffffffffffffffff15000001fdffffffffffffffffffff76000000a2ffffffffffffffffffff14000007ffffffffffffffffffffffa10000007bffffffffffffffffffec01000033ffffffffffffffffffffffec08000022fbffffffffffffffff8e00000087ffffffffffffffffffffffff7d00000068fdffffffffffffc70b00001ef2fffffffffffffffffffffffffb5500000039aae5fbf2c87006000013d0fffffffffffffffffffffffffffffe93160000000000000000000153e3ffffffffffffffffffffffffffffffffffbd2e000000000000000780f0ffffffffffffffffffffffffffffffffce3500000000000000000000000e87fcffffffffffffffffffffffffffb3060000004fb2e6faf0cd82150000004ffaffffffffffffffffffffffda0b000004a9ffffffffffffffe93600000076ffffffffffffffffffffff5600000084ffffffffffffffffffe80e000005e2fffffffffffffffffff606000008f4ffffffffffffffffffff6f0000008dffffffffffffffffffcb00000039ffffffffffffffffffffffac0000005cffffffffffffffffffbc0000004affffffffffffffffffffffbe0000004dffffffffffffffffffcc00000039ffffffffffffffffffffffac0000005effffffffffffffffffea00000008f4ffffffffffffffffffff6e0000007cffffffffffffffffffff2f00000085ffffffffffffffffffe70d000000c1ffffffffffffffffffff9300000004a9ffffffffffffffe83400000028fcfffffffffffffffffffffa2d0000000050b2e7fbf2cd821400000002b8ffffffffffffffffffffffffe523000000000000000000000000000299fffffffffffffffffffffffffffff16605000000000000000000002cc5ffffffffffffffffffffffffffffffffffe88e542512040b1b3d72c1fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff8a259251008203f8be2ffffffffffffffffffffffffffffffffffffffa91d0000000000000000047ffaffffffffffffffffffffffffffffffff7b00000000000000000000000040f8ffffffffffffffffffffffffffff94000000004db9ecf7da8b1300000057ffffffffffffffffffffffffffdc050000008fffffffffffffe527000000acffffffffffffffffffffffff630000005fffffffffffffffffd406000025fbfffffffffffffffffffffb0c000002e0ffffffffffffffffff5f000000b2ffffffffffffffffffffc600000036ffffffffffffffffffffb50000005fffffffffffffffffffffa000000068ffffffffffffffffffffe700000011feffffffffffffffffff8d0000007cfffffffffffffffffffffb00000000dfffffffffffffffffff8c0000007cfffffffffffffffffffffb00000000b4ffffffffffffffffff9e00000069ffffffffffffffffffffe7000000008dffffffffffffffffffbe00000038ffffffffffffffffffffb6000000007bfffffffffffffffffff606000003e2ffffffffffffffffff62000000006fffffffffffffffffffff4f00000064ffffffffffffffffd8080000000062ffffffffffffffffffffc50000000096ffffffffffffe82b000000000064ffffffffffffffffffffff6c0000000051bbeff8dc8e1500001000000074fffffffffffffffffffffff94f0000000000000000000000288c00000084fffffffffffffffffffffffffd810b000000000000000052ea830000009fffffffffffffffffffffffffffffea8d471d090d2864c1ffff5b000000d4ffffffffffffffffffffffffffffffffffffffffffffffffff2100000dfdffffffffffffffffffffffffffffffffffffffffffffffffd900000052ffffffffffffffffffffffffffffffffffffffffffffffffff75000000b8ffffffffffffffffffffffffffffffffffffffffffffffffe30d000023fefffffffffffffffffffffffffffffffffffffffffffffff945000000b7ffffffffffffffffffffffffff7fa2fdffffffffffffffe8480000005effffffffffffffffffffffffffff63002080c4ecfae7c0740e00000034f4ffffffffffffffffffffffffffff6300000000000000000000000043f0ffffffffffffffffffffffffffffff6300000000000000000000118efdfffffffffffffffffffffffffffffffff4bb7f462b15040b25569ff4ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff".split("")
end


def DrawDigitCharacter(image, topx, topy, digit)

	colorReference = NumberReference.new
	errorMessage = StringReference.new
	color = RGBA.new

	colorChars = Array.new(2)

	allCharData = DigitDataBase16()

	y = 0.0
	while(y < 37.0)
		x = 0.0
		while(x < 30.0)
			colorChars[0] = allCharData[digit*30.0*37.0*2.0 + y*2.0*30.0 + x*2.0 + 0.0]
			colorChars[1] = allCharData[digit*30.0*37.0*2.0 + y*2.0*30.0 + x*2.0 + 1.0]

			ToUpperCase(colorChars)
			CreateNumberFromStringWithCheck(colorChars, 16.0, colorReference, errorMessage)
			color.r = colorReference.numberValue / 255.0
			color.g = colorReference.numberValue / 255.0
			color.b = colorReference.numberValue / 255.0
			color.a = 1.0
			SetPixel(image, topx + x, topy + y, color)
			x = x + 1.0
		end
		y = y + 1.0
	end
end


def GetPixelFontData()
	return "0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000".split("")
end


def DrawAsciiCharacter(image, topx, topy, a, color)

	rowReference = NumberReference.new
	errorMessage = StringReference.new

	index = (a).ord
	index = index - 32.0
	allCharData = GetPixelFontData()
	charData = Substring(allCharData, index*2.0*13.0, (index + 1.0)*2.0*13.0)

	y = 0.0
	while(y < 13.0)
		rowData = Substring(charData, y*2.0, (y + 1.0)*2.0)
		ToUpperCase(rowData)
		CreateNumberFromStringWithCheck(rowData, 16.0, rowReference, errorMessage)
		row = rowReference.numberValue
		x = 0.0
		while(x < 8.0)
			pixel = (row / 2.0**x).floor%2.0
			if pixel == 1.0
				DrawPixel(image, topx + 8.0 - 1.0 - x, topy + 13.0 - 1.0 - y, color)
			end
			x = x + 1.0
		end
		y = y + 1.0
	end
end


def GetTextWidth(text)

	charWidth = 8.0
	spacing = 2.0

	if text.length == 0.0
		width = 0.0
	else
		width = text.length*charWidth + (text.length - 1.0)*spacing
	end

	return width
end


def GetTextHeight(text)
	return 13.0
end


def AssertFalse(b, failures)
	if b
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertTrue(b, failures)
	if !b
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertEquals(a, b, failures)
	if a != b
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertBooleansEqual(a, b, failures)
	if a != b
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertCharactersEqual(a, b, failures)
	if a != b
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertStringEquals(a, b, failures)
	if !aStringsEqual(a, b)
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertNumberArraysEqual(a, b, failures)

	if a.length == b.length
		i = 0.0
		while(i < a.length)
			AssertEquals(a[i], b[i], failures)
			i = i + 1.0
		end
	else
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertBooleanArraysEqual(a, b, failures)

	if a.length == b.length
		i = 0.0
		while(i < a.length)
			AssertBooleansEqual(a[i], b[i], failures)
			i = i + 1.0
		end
	else
		failures.numberValue = failures.numberValue + 1.0
	end
end


def AssertStringArraysEqual(a, b, failures)

	if a.length == b.length
		i = 0.0
		while(i < a.length)
			AssertStringEquals(a[i].string, b[i].string, failures)
			i = i + 1.0
		end
	else
		failures.numberValue = failures.numberValue + 1.0
	end
end


def ConvertToPNG(image)
	return ConvertToPNGWithOptions(image, 6.0, false, 0.0, 0.001)
end


def ConvertToPNGGrayscale(image)
	return ConvertToPNGWithOptions(image, 0.0, false, 0.0, 0.001)
end


def PysicsHeader(pixelsPerMeter)

	phys = PHYS.new

	phys.pixelsPerMeter = pixelsPerMeter

	return phys
end


def ConvertToPNGWithOptions(image, colorType, setPhys, pixelsPerMeter, compressionLevel)

	png = PNGImage.new

	png.signature = PNGSignature()

	png.ihdr = PNGHeader(image, colorType)

	png.physPresent = setPhys
	png.phys = PysicsHeader(pixelsPerMeter)

	if colorType == 6.0
		colorData = GetPNGColorData(image)
	else
		colorData = GetPNGColorDataGreyscale(image)
	end
	png.zlibStruct = ZLibCompressStaticHuffman(colorData, compressionLevel)

	pngData = PNGSerializeChunks(png)

	return pngData
end


def PNGSerializeChunks(png)

	length = png.signature.length + 12.0 + PNGHeaderLength() + 12.0 + PNGIDATLength(png) + 12.0
	if png.physPresent
		length = length + 4.0 + 4.0 + 1.0 + 12.0
	end
	data = Array.new(length)
	position = CreateNumberReference(0.0)

	# Signature
	i = 0.0
	while(i < png.signature.length)
		WriteByte(data, png.signature[i], position)
		i = i + 1.0
	end

	# Header
	chunkLength = PNGHeaderLength()
	Write4BytesBE(data, chunkLength, position)
	WriteStringBytes(data, "IHDR".split(""), position)
	Write4BytesBE(data, png.ihdr.width, position)
	Write4BytesBE(data, png.ihdr.height, position)
	WriteByte(data, png.ihdr.bitDepth, position)
	WriteByte(data, png.ihdr.colourType, position)
	WriteByte(data, png.ihdr.compressionMethod, position)
	WriteByte(data, png.ihdr.filterMethod, position)
	WriteByte(data, png.ihdr.interlaceMethod, position)
	Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4.0, chunkLength + 4.0), position)

	# pHYs
	if png.physPresent
		chunkLength = 4.0 + 4.0 + 1.0
		Write4BytesBE(data, chunkLength, position)
		WriteStringBytes(data, "pHYs".split(""), position)

		Write4BytesBE(data, png.phys.pixelsPerMeter, position)
		Write4BytesBE(data, png.phys.pixelsPerMeter, position)
		WriteByte(data, 1.0, position)
		# 1 = pixels per meter
		Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4.0, chunkLength + 4.0), position)
	end

	# IDAT
	chunkLength = PNGIDATLength(png)
	Write4BytesBE(data, chunkLength, position)
	WriteStringBytes(data, "IDAT".split(""), position)
	WriteByte(data, png.zlibStruct.cMF, position)
	WriteByte(data, png.zlibStruct.fLG, position)
	i = 0.0
	while(i < png.zlibStruct.compressedDataBlocks.length)
		WriteByte(data, png.zlibStruct.compressedDataBlocks[i], position)
		i = i + 1.0
	end
	Write4BytesBE(data, png.zlibStruct.adler32CheckValue, position)
	Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4.0, chunkLength + 4.0), position)

	# IEND
	chunkLength = 0.0
	Write4BytesBE(data, chunkLength, position)
	WriteStringBytes(data, "IEND".split(""), position)
	Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - 4.0, 4.0), position)

	return data
end


def PNGIDATLength(png)
	return 2.0 + png.zlibStruct.compressedDataBlocks.length + 4.0
end


def PNGHeaderLength()
	return 4.0 + 4.0 + 1.0 + 1.0 + 1.0 + 1.0 + 1.0
end


def GetPNGColorData(image)

	length = 4.0*ImageWidth(image)*ImageHeight(image) + ImageHeight(image)

	colordata = Array.new(length)

	nextx = 0.0

	y = 0.0
	while(y < ImageHeight(image))
		colordata[nextx] = 0.0
		nextx = nextx + 1.0
		x = 0.0
		while(x < ImageWidth(image))
			rgba = image.x[x].y[y]
			colordata[nextx] = Round(rgba.r*255.0)
			nextx = nextx + 1.0
			colordata[nextx] = Round(rgba.g*255.0)
			nextx = nextx + 1.0
			colordata[nextx] = Round(rgba.b*255.0)
			nextx = nextx + 1.0
			colordata[nextx] = Round(rgba.a*255.0)
			nextx = nextx + 1.0
			x = x + 1.0
		end
		y = y + 1.0
	end

	return colordata
end


def GetPNGColorDataGreyscale(image)

	length = ImageWidth(image)*ImageHeight(image) + ImageHeight(image)

	colordata = Array.new(length)

	nextx = 0.0

	y = 0.0
	while(y < ImageHeight(image))
		colordata[nextx] = 0.0
		nextx = nextx + 1.0
		x = 0.0
		while(x < ImageWidth(image))
			rgba = image.x[x].y[y]
			colordata[nextx] = Round(rgba.r*255.0)
			nextx = nextx + 1.0
			x = x + 1.0
		end
		y = y + 1.0
	end

	return colordata
end


def PNGHeader(image, colortype)

	ihdr = IHDR.new
	ihdr.width = ImageWidth(image)
	ihdr.height = ImageHeight(image)
	# Truecolour with alpha
	ihdr.bitDepth = 8.0
	ihdr.colourType = colortype
	ihdr.filterMethod = 0.0
	# None
	ihdr.compressionMethod = 0.0
	# zlib
	ihdr.interlaceMethod = 0.0
	# no interlace
	return ihdr
end


def PNGSignature()

	s = Array.new(8)
	s[0] = 137.0
	s[1] = 80.0
	s[2] = 78.0
	s[3] = 71.0
	s[4] = 13.0
	s[5] = 10.0
	s[6] = 26.0
	s[7] = 10.0

	return s
end


def PNGReadDataChunks(cs)

	length = 0.0
	i = 0.0
	while(i < cs.length)
		c = cs[i]
		if aStringsEqual(c.type, "IDAT".split(""))
			length = length + c.length
		end
		i = i + 1.0
	end

	zlibData = Array.new(length)
	zlibpos = 0.0

	i = 0.0
	while(i < cs.length)
		c = cs[i]
		if aStringsEqual(c.type, "IDAT".split(""))
			j = 0.0
			while(j < c.length)
				zlibData[zlibpos] = c.data[j]
				zlibpos = zlibpos + 1.0
				j = j + 1.0
			end
		end
		i = i + 1.0
	end

	return zlibData
end


def PNGReadHeader(image, cs, errorMessages)

	position = CreateNumberReference(0.0)
	success = false

	i = 0.0
	while(i < cs.length)
		c = cs[i]
		if aStringsEqual(c.type, "IHDR".split(""))
			ihdr = IHDR.new

			ihdr.width = Read4bytesBE(c.data, position)
			ihdr.height = Read4bytesBE(c.data, position)
			ihdr.bitDepth = ReadByte(c.data, position)
			ihdr.colourType = ReadByte(c.data, position)
			ihdr.compressionMethod = ReadByte(c.data, position)
			ihdr.filterMethod = ReadByte(c.data, position)
			ihdr.interlaceMethod = ReadByte(c.data, position)

			n = CreateImage(ihdr.width, ihdr.height, GetTransparent())
			image.x = n.x

			if ihdr.colourType == 6.0
				if ihdr.bitDepth == 8.0
					if ihdr.compressionMethod == 0.0
						if ihdr.filterMethod == 0.0
							if ihdr.interlaceMethod == 0.0
								success = true
							else
								success = false
								errorMessages.string = AppendString(errorMessages.string, "Interlace method not supported.".split(""))
							end
						else
							success = false
							errorMessages.string = AppendString(errorMessages.string, "Filter method not supported.".split(""))
						end
					else
						success = false
						errorMessages.string = AppendString(errorMessages.string, "Compression type not supported.".split(""))
					end
				else
					success = false
					errorMessages.string = AppendString(errorMessages.string, "Bit depth not supported.".split(""))
				end
			else
				success = false
				errorMessages.string = AppendString(errorMessages.string, "Color type not supported.".split(""))
			end
		end
		i = i + 1.0
	end

	return success
end


def PNGReadChunks(data, position)
	done = false
	prepos = position.numberValue
	chunks = 0.0
	while(!done)
		c = PNGReadChunk(data, position)
		if aStringsEqual(c.type, "IEND".split(""))
			done = true
		end
		chunks = chunks + 1.0
	end
	position.numberValue = prepos
	cs = Array.new(chunks)
	i = 0.0
	while(i < chunks)
		cs[i] = PNGReadChunk(data, position)
		i = i + 1.0
	end

	return cs
end


def PNGReadChunk(data, position)

	c = Chunk.new

	c.length = Read4bytesBE(data, position)
	c.type = Array.new(4)
	c.type[0] = (ReadByte(data, position)).truncate.chr('UTF-8')
	c.type[1] = (ReadByte(data, position)).truncate.chr('UTF-8')
	c.type[2] = (ReadByte(data, position)).truncate.chr('UTF-8')
	c.type[3] = (ReadByte(data, position)).truncate.chr('UTF-8')
	c.data = ReadXbytes(data, position, c.length)
	c.crc = Read4bytesBE(data, position)

	return c
end


def WriteStringToStingStream(stream, index, src)

	i = 0.0
	while(i < src.length)
		stream[index.numberValue + i] = src[i]
		i = i + 1.0
	end
	index.numberValue = index.numberValue + src.length
end


def WriteCharacterToStingStream(stream, index, src)
	stream[index.numberValue] = src
	index.numberValue = index.numberValue + 1.0
end


def WriteBooleanToStingStream(stream, index, src)
	if src
		WriteStringToStingStream(stream, index, "true".split(""))
	else
		WriteStringToStingStream(stream, index, "false".split(""))
	end
end


def SubstringWithCheck(string, from, to, stringReference)

	if from >= 0.0 && from <= string.length && to >= 0.0 && to <= string.length && from <= to
		stringReference.string = Substring(string, from, to)
		success = true
	else
		success = false
	end

	return success
end


def Substring(string, from, to)

	length = to - from

	n = Array.new(length)

	i = from
	while(i < to)
		n[i - from] = string[i]
		i = i + 1.0
	end

	return n
end


def AppendString(s1, s2)

	newString = ConcatenateString(s1, s2)

	delete(s1)

	return newString
end


def ConcatenateString(s1, s2)

	newString = Array.new(s1.length + s2.length)

	i = 0.0
	while(i < s1.length)
		newString[i] = s1[i]
		i = i + 1.0
	end

	i = 0.0
	while(i < s2.length)
		newString[s1.length + i] = s2[i]
		i = i + 1.0
	end

	return newString
end


def AppendCharacter(string, c)

	newString = ConcatenateCharacter(string, c)

	delete(string)

	return newString
end


def ConcatenateCharacter(string, c)
	newString = Array.new(string.length + 1.0)

	i = 0.0
	while(i < string.length)
		newString[i] = string[i]
		i = i + 1.0
	end

	newString[string.length] = c

	return newString
end


def SplitByCharacter(toSplit, splitBy)

	stringToSplitBy = Array.new(1)
	stringToSplitBy[0] = splitBy

	split = SplitByString(toSplit, stringToSplitBy)

	delete(stringToSplitBy)

	return split
end


def IndexOfCharacter(string, character, indexReference)

	found = false
	i = 0.0
	while(i < string.length && !found)
		if string[i] == character
			found = true
			indexReference.numberValue = i
		end
		i = i + 1.0
	end

	return found
end


def SubstringEqualsWithCheck(string, from, substring, equalsReference)

	if from < string.length
		success = true
		equalsReference.booleanValue = SubstringEquals(string, from, substring)
	else
		success = false
	end

	return success
end


def SubstringEquals(string, from, substring)

	equal = true
	if string.length - from >= substring.length
		i = 0.0
		while(i < substring.length && equal)
			if string[from + i] != substring[i]
				equal = false
			end
			i = i + 1.0
		end
	else
		equal = false
	end

	return equal
end


def IndexOfString(string, substring, indexReference)

	found = false
	i = 0.0
	while(i < string.length - substring.length + 1.0 && !found)
		if SubstringEquals(string, i, substring)
			found = true
			indexReference.numberValue = i
		end
		i = i + 1.0
	end

	return found
end


def ContainsCharacter(string, character)

	found = false
	i = 0.0
	while(i < string.length && !found)
		if string[i] == character
			found = true
		end
		i = i + 1.0
	end

	return found
end


def ContainsString(string, substring)
	return IndexOfString(string, substring, NumberReference.new)
end


def ToUpperCase(string)

	i = 0.0
	while(i < string.length)
		string[i] = charToUpperCase(string[i])
		i = i + 1.0
	end
end


def ToLowerCase(string)

	i = 0.0
	while(i < string.length)
		string[i] = charToLowerCase(string[i])
		i = i + 1.0
	end
end


def EqualsIgnoreCase(a, b)

	if a.length == b.length
		equal = true
		i = 0.0
		while(i < a.length && equal)
			if charToLowerCase(a[i]) != charToLowerCase(b[i])
				equal = false
			end
			i = i + 1.0
		end
	else
		equal = false
	end

	return equal
end


def ReplaceString(string, toReplace, replaceWith)

	equalsReference = BooleanReference.new
	result = Array.new(0)

	i = 0.0
	while(i < string.length)
		success = SubstringEqualsWithCheck(string, i, toReplace, equalsReference)
		if success
			success = equalsReference.booleanValue
		end

		if success && toReplace.length > 0.0
			result = ConcatenateString(result, replaceWith)
			i = i + toReplace.length
		else
			result = ConcatenateCharacter(result, string[i])
			i = i + 1.0
		end
	end

	return result
end


def ReplaceCharacter(string, toReplace, replaceWith)

	result = Array.new(0)

	i = 0.0
	while(i < string.length)
		if string[i] == toReplace
			result = ConcatenateCharacter(result, replaceWith)
		else
			result = ConcatenateCharacter(result, string[i])
		end
		i = i + 1.0
	end

	return result
end


def Trim(string)

	# Find whitepaces at the start.
	lastWhitespaceLocationStart = -1.0
	firstNonWhitespaceFound = false
	i = 0.0
	while(i < string.length && !firstNonWhitespaceFound)
		if charIsWhiteSpace(string[i])
			lastWhitespaceLocationStart = i
		else
			firstNonWhitespaceFound = true
		end
		i = i + 1.0
	end

	# Find whitepaces at the end.
	lastWhitespaceLocationEnd = string.length
	firstNonWhitespaceFound = false
	i = string.length - 1.0
	while(i >= 0.0 && !firstNonWhitespaceFound)
		if charIsWhiteSpace(string[i])
			lastWhitespaceLocationEnd = i
		else
			firstNonWhitespaceFound = true
		end
		i = i - 1.0
	end

	if lastWhitespaceLocationStart < lastWhitespaceLocationEnd
		result = Substring(string, lastWhitespaceLocationStart + 1.0, lastWhitespaceLocationEnd)
	else
		result = Array.new(0)
	end

	return result
end


def StartsWith(string, start)

	startsWithString = false
	if string.length >= start.length
		startsWithString = SubstringEquals(string, 0.0, start)
	end

	return startsWithString
end


def EndsWith(string, endx)

	endsWithString = false
	if string.length >= endx.length
		endsWithString = SubstringEquals(string, string.length - endx.length, endx)
	end

	return endsWithString
end


def SplitByString(toSplit, splitBy)

	split = Array.new(0)

	nextx = Array.new(0)
	i = 0.0
	while(i < toSplit.length)
		c = toSplit[i]

		if SubstringEquals(toSplit, i, splitBy)
			n = StringReference.new
			n.string = nextx
			split = AddString(split, n)
			nextx = Array.new(0)
			i = i + splitBy.length
		else
			nextx = AppendCharacter(nextx, c)
			i = i + 1.0
		end
	end

	n = StringReference.new
	n.string = nextx
	split = AddString(split, n)

	return split
end


def StringIsBefore(a, b)

	before = false
	equal = true
	done = false

	if a.length == 0.0 && b.length > 0.0
		before = true
	else
		i = 0.0
		while(i < a.length && i < b.length && !done)
			if a[i] != b[i]
				equal = false
			end
			if charCharacterIsBefore(a[i], b[i])
				before = true
			end
			if charCharacterIsBefore(b[i], a[i])
				done = true
			end
			i = i + 1.0
		end

		if equal
			if a.length < b.length
				before = true
			end
		end
	end

	return before
end


def ReadXbytes(data, position, length)

	r = Array.new(length)

	i = 0.0
	while(i < length)
		r[i] = ReadByte(data, position)
		i = i + 1.0
	end

	return r
end


def Read4bytesBE(data, position)

	r = 0.0
	r = r + 2.0**24.0*ReadByte(data, position)
	r = r + 2.0**16.0*ReadByte(data, position)
	r = r + 2.0**8.0*ReadByte(data, position)
	r = r + ReadByte(data, position)

	return r
end


def Read2bytesBE(data, position)

	r = 0.0
	r = r + 2.0**8.0*ReadByte(data, position)
	r = r + ReadByte(data, position)

	return r
end


def ReadByte(data, position)

	nextx = data[position.numberValue]
	position.numberValue = position.numberValue + 1.0

	return nextx
end


def Read4bytesLE(data, position)

	r = 0.0
	r = r + ReadByte(data, position)
	r = r + 2.0**8.0*ReadByte(data, position)
	r = r + 2.0**16.0*ReadByte(data, position)
	r = r + 2.0**24.0*ReadByte(data, position)

	return r
end


def WriteByte(data, b, position)
	data[position.numberValue] = b
	position.numberValue = position.numberValue + 1.0
end


def Write2BytesLE(data, b, position)
	data[position.numberValue] = Round(b%2.0**8.0)
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = (b / 2.0**8.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
end


def Write4BytesLE(data, b, position)
	data[position.numberValue] = Round(b%2.0**8.0)
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = (b / 2.0**8.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = (b / 2.0**16.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = (b / 2.0**24.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
end


def Write2BytesBE(data, b, position)
	data[position.numberValue] = (b / 2.0**8.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = Round(b%2.0**8.0)
	position.numberValue = position.numberValue + 1.0
end


def Write4BytesBE(data, b, position)
	data[position.numberValue] = (b / 2.0**24.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = (b / 2.0**16.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = (b / 2.0**8.0).floor%2.0**8.0
	position.numberValue = position.numberValue + 1.0
	data[position.numberValue] = Round(b%2.0**8.0)
	position.numberValue = position.numberValue + 1.0
end


def WriteStringBytes(data, cs, position)

	i = 0.0
	while(i < cs.length)
		v = (cs[i]).ord
		WriteByte(data, v, position)
		i = i + 1.0
	end
end


def MakeCRC32Table()

	crcTable = Array.new(256)

	n = 0.0
	while(n < 256.0)
		c = n
		k = 0.0
		while(k < 8.0)
			if !DivisibleBy(c, 2.0)
				c = Xor4Byte(3988292384.0, (c / 2.0).floor)
			else
				c = (c / 2.0).floor
			end
			k = k + 1.0
		end
		crcTable[n] = c
		n = n + 1.0
	end

	return crcTable
end


def UpdateCRC32(crc, buf, crc_table)

	n = 0.0
	while(n < buf.length)
		index = And4Byte(Xor4Byte(crc, buf[n]), 2.0**8.0 - 1.0)
		crc = Xor4Byte(crc_table[index], (crc / 2.0**8.0).floor)
		n = n + 1.0
	end

	return crc
end


def CalculateCRC32(buf)

	crcTable = MakeCRC32Table()

	b32max = 2.0**32.0 - 1.0
	value = UpdateCRC32(b32max, buf, crcTable)

	return Xor4Byte(value, b32max)
end


def CRC32OfInterval(data, from, length)

	crcBase = Array.new(length)

	i = 0.0
	while(i < length)
		crcBase[i] = data[from + i]
		i = i + 1.0
	end

	crc = CalculateCRC32(crcBase)

	delete(crcBase)

	return crc
end


def ZLibCompressNoCompression(data)

	zlibStruct = ZLIBStruct.new

	zlibStruct.cMF = 120.0
	zlibStruct.fLG = 1.0
	zlibStruct.compressedDataBlocks = DeflateDataNoCompression(data)
	zlibStruct.adler32CheckValue = ComputeAdler32(data)

	return zlibStruct
end


def ZLibCompressStaticHuffman(data, level)

	zlibStruct = ZLIBStruct.new

	zlibStruct.cMF = 120.0
	zlibStruct.fLG = 1.0
	zlibStruct.compressedDataBlocks = DeflateDataStaticHuffman(data, level)
	zlibStruct.adler32CheckValue = ComputeAdler32(data)

	return zlibStruct
end


def AddNumber(list, a)

	newlist = Array.new(list.length + 1.0)
	i = 0.0
	while(i < list.length)
		newlist[i] = list[i]
		i = i + 1.0
	end
	newlist[list.length] = a
		
	delete(list)
		
	return newlist
end


def AddNumberRef(list, i)
	list.numberArray = AddNumber(list.numberArray, i)
end


def RemoveNumber(list, n)

	newlist = Array.new(list.length - 1.0)

	if n >= 0.0 && n < list.length
		i = 0.0
		while(i < list.length)
			if i < n
				newlist[i] = list[i]
			end
			if i > n
				newlist[i - 1.0] = list[i]
			end
			i = i + 1.0
		end

		delete(list)
	else
		delete(newlist)
	end
		
	return newlist
end


def GetNumberRef(list, i)
	return list.numberArray[i]
end


def RemoveNumberRef(list, i)
	list.numberArray = RemoveNumber(list.numberArray, i)
end


def AddString(list, a)

	newlist = Array.new(list.length + 1.0)

	i = 0.0
	while(i < list.length)
		newlist[i] = list[i]
		i = i + 1.0
	end
	newlist[list.length] = a
		
	delete(list)
		
	return newlist
end


def AddStringRef(list, i)
	list.stringArray = AddString(list.stringArray, i)
end


def RemoveString(list, n)

	newlist = Array.new(list.length - 1.0)

	if n >= 0.0 && n < list.length
		i = 0.0
		while(i < list.length)
			if i < n
				newlist[i] = list[i]
			end
			if i > n
				newlist[i - 1.0] = list[i]
			end
			i = i + 1.0
		end

		delete(list)
	else
		delete(newlist)
	end
		
	return newlist
end


def GetStringRef(list, i)
	return list.stringArray[i]
end


def RemoveStringRef(list, i)
	list.stringArray = RemoveString(list.stringArray, i)
end


def AddBoolean(list, a)

	newlist = Array.new(list.length + 1.0)
	i = 0.0
	while(i < list.length)
		newlist[i] = list[i]
		i = i + 1.0
	end
	newlist[list.length] = a
		
	delete(list)
		
	return newlist
end


def AddBooleanRef(list, i)
	list.booleanArray = AddBoolean(list.booleanArray, i)
end


def RemoveBoolean(list, n)

	newlist = Array.new(list.length - 1.0)

	if n >= 0.0 && n < list.length
		i = 0.0
		while(i < list.length)
			if i < n
				newlist[i] = list[i]
			end
			if i > n
				newlist[i - 1.0] = list[i]
			end
			i = i + 1.0
		end

		delete(list)
	else
		delete(newlist)
	end
		
	return newlist
end


def GetBooleanRef(list, i)
	return list.booleanArray[i]
end


def RemoveDecimalRef(list, i)
	list.booleanArray = RemoveBoolean(list.booleanArray, i)
end


def CreateLinkedListString()

	ll = LinkedListStrings.new
	ll.first = LinkedListNodeStrings.new
	ll.last = ll.first
	ll.last.endx = true

	return ll
end


def LinkedListAddString(ll, value)
	ll.last.endx = false
	ll.last.value = value
	ll.last.nextx = LinkedListNodeStrings.new
	ll.last.nextx.endx = true
	ll.last = ll.last.nextx
end


def LinkedListStringsToArray(ll)

	node = ll.first

	length = LinkedListStringsLength(ll)

	array = Array.new(length)

	i = 0.0
	while(i < length)
		array[i] = StringReference.new
		array[i].string = node.value
		node = node.nextx
		i = i + 1.0
	end

	return array
end


def LinkedListStringsLength(ll)

	l = 0.0
	node = ll.first
	while(!node.endx)
		node = node.nextx
		l = l + 1.0
	end

	return l
end


def FreeLinkedListString(ll)

	node = ll.first

	while(!node.endx)
		prev = node
		node = node.nextx
		delete(prev)
	end

	delete(node)
end


def CreateLinkedListNumbers()

	ll = LinkedListNumbers.new
	ll.first = LinkedListNodeNumbers.new
	ll.last = ll.first
	ll.last.endx = true

	return ll
end


def CreateLinkedListNumbersArray(length)

	lls = Array.new(length)
	i = 0.0
	while(i < lls.length)
		lls[i] = CreateLinkedListNumbers()
		i = i + 1.0
	end

	return lls
end


def LinkedListAddNumber(ll, value)
	ll.last.endx = false
	ll.last.value = value
	ll.last.nextx = LinkedListNodeNumbers.new
	ll.last.nextx.endx = true
	ll.last = ll.last.nextx
end


def LinkedListNumbersLength(ll)

	l = 0.0
	node = ll.first
	while(!node.endx)
		node = node.nextx
		l = l + 1.0
	end

	return l
end


def LinkedListNumbersIndex(ll, index)

	node = ll.first
	i = 0.0
	while(i < index)
		node = node.nextx
		i = i + 1.0
	end

	return node.value
end


def LinkedListInsertNumber(ll, index, value)

	if index == 0.0
		tmp = ll.first
		ll.first = LinkedListNodeNumbers.new
		ll.first.nextx = tmp
		ll.first.value = value
		ll.first.endx = false
	else
		node = ll.first
		i = 0.0
		while(i < index - 1.0)
			node = node.nextx
			i = i + 1.0
		end

		tmp = node.nextx
		node.nextx = LinkedListNodeNumbers.new
		node.nextx.nextx = tmp
		node.nextx.value = value
		node.nextx.endx = false
	end
end


def LinkedListSet(ll, index, value)

	node = ll.first
	i = 0.0
	while(i < index)
		node = node.nextx
		i = i + 1.0
	end

	node.nextx.value = value
end


def LinkedListRemoveNumber(ll, index)

	node = ll.first
	prev = ll.first

	i = 0.0
	while(i < index)
		prev = node
		node = node.nextx
		i = i + 1.0
	end

	if index == 0.0
		ll.first = prev.nextx
	end
	if !prev.nextx.endx
		prev.nextx = prev.nextx.nextx
	end
end


def FreeLinkedListNumbers(ll)

	node = ll.first

	while(!node.endx)
		prev = node
		node = node.nextx
		delete(prev)
	end

	delete(node)
end


def FreeLinkedListNumbersArray(lls)

	i = 0.0
	while(i < lls.length)
		FreeLinkedListNumbers(lls[i])
		i = i + 1.0
	end
	delete(lls)
end


def LinkedListNumbersToArray(ll)

	node = ll.first

	length = LinkedListNumbersLength(ll)

	array = Array.new(length)

	i = 0.0
	while(i < length)
		array[i] = node.value
		node = node.nextx
		i = i + 1.0
	end

	return array
end


def ArrayToLinkedListNumbers(array)

	ll = CreateLinkedListNumbers()

	i = 0.0
	while(i < array.length)
		LinkedListAddNumber(ll, array[i])
		i = i + 1.0
	end

	return ll
end


def LinkedListNumbersEqual(a, b)

	an = a.first
	bn = b.first

	equal = true
	done = false
	while(equal && !done)
		if an.endx == bn.endx
			if an.endx
				done = true
			elsif an.value == bn.value
				an = an.nextx
				bn = bn.nextx
			else
				equal = false
			end
		else
			equal = false
		end
	end

	return equal
end


def CreateLinkedListCharacter()

	ll = LinkedListCharacters.new
	ll.first = LinkedListNodeCharacters.new
	ll.last = ll.first
	ll.last.endx = true

	return ll
end


def LinkedListAddCharacter(ll, value)
	ll.last.endx = false
	ll.last.value = value
	ll.last.nextx = LinkedListNodeCharacters.new
	ll.last.nextx.endx = true
	ll.last = ll.last.nextx
end


def LinkedListCharactersToArray(ll)

	node = ll.first

	length = LinkedListCharactersLength(ll)

	array = Array.new(length)

	i = 0.0
	while(i < length)
		array[i] = node.value
		node = node.nextx
		i = i + 1.0
	end

	return array
end


def LinkedListCharactersLength(ll)

	l = 0.0
	node = ll.first
	while(!node.endx)
		node = node.nextx
		l = l + 1.0
	end

	return l
end


def FreeLinkedListCharacter(ll)

	node = ll.first

	while(!node.endx)
		prev = node
		node = node.nextx
		delete(prev)
	end

	delete(node)
end


def CreateDynamicArrayNumbers()

	da = DynamicArrayNumbers.new
	da.array = Array.new(10)
	da.length = 0.0

	return da
end


def CreateDynamicArrayNumbersWithInitialCapacity(capacity)

	da = DynamicArrayNumbers.new
	da.array = Array.new(capacity)
	da.length = 0.0

	return da
end


def DynamicArrayAddNumber(da, value)
	if da.length == da.array.length
		DynamicArrayNumbersIncreaseSize(da)
	end

	da.array[da.length] = value
	da.length = da.length + 1.0
end


def DynamicArrayNumbersIncreaseSize(da)

	newLength = (da.array.length*3.0 / 2.0).round
	newArray = Array.new(newLength)

	i = 0.0
	while(i < da.array.length)
		newArray[i] = da.array[i]
		i = i + 1.0
	end

	delete(da.array)

	da.array = newArray
end


def DynamicArrayNumbersDecreaseSizeNecessary(da)

	needsDecrease = false

	if da.length > 10.0
		needsDecrease = da.length <= (da.array.length*2.0 / 3.0).round
	end

	return needsDecrease
end


def DynamicArrayNumbersDecreaseSize(da)

	newLength = (da.array.length*2.0 / 3.0).round
	newArray = Array.new(newLength)

	i = 0.0
	while(i < newLength)
		newArray[i] = da.array[i]
		i = i + 1.0
	end

	delete(da.array)

	da.array = newArray
end


def DynamicArrayNumbersIndex(da, index)
	return da.array[index]
end


def DynamicArrayNumbersLength(da)
	return da.length
end


def DynamicArrayInsertNumber(da, index, value)

	if da.length == da.array.length
		DynamicArrayNumbersIncreaseSize(da)
	end

	i = da.length
	while(i > index)
		da.array[i] = da.array[i - 1.0]
		i = i - 1.0
	end

	da.array[index] = value

	da.length = da.length + 1.0
end


def DynamicArraySet(da, index, value)
	da.array[index] = value
end


def DynamicArrayRemoveNumber(da, index)

	i = index
	while(i < da.length - 1.0)
		da.array[i] = da.array[i + 1.0]
		i = i + 1.0
	end

	da.length = da.length - 1.0

	if DynamicArrayNumbersDecreaseSizeNecessary(da)
		DynamicArrayNumbersDecreaseSize(da)
	end
end


def FreeDynamicArrayNumbers(da)
	delete(da.array)
	delete(da)
end


def DynamicArrayNumbersToArray(da)

	array = Array.new(da.length)

	i = 0.0
	while(i < da.length)
		array[i] = da.array[i]
		i = i + 1.0
	end

	return array
end


def ArrayToDynamicArrayNumbersWithOptimalSize(array)

=begin

         c = 10*(3/2)^n
         log(c) = log(10*(3/2)^n)
         log(c) = log(10) + log((3/2)^n)
         log(c) = 1 + log((3/2)^n)
         log(c) - 1 = log((3/2)^n)
         log(c) - 1 = n*log(3/2)
         n = (log(c) - 1)/log(3/2)
        
=end

	c = array.length
	n = (Math.log(c) - 1.0) / Math.log(3.0 / 2.0)
	newCapacity = (n).floor + 1.0

	da = CreateDynamicArrayNumbersWithInitialCapacity(newCapacity)

	i = 0.0
	while(i < array.length)
		da.array[i] = array[i]
		i = i + 1.0
	end

	return da
end


def ArrayToDynamicArrayNumbers(array)

	da = DynamicArrayNumbers.new
	da.array = aCopyNumberArray(array)
	da.length = array.length

	return da
end


def DynamicArrayNumbersEqual(a, b)

	equal = true
	if a.length == b.length
		i = 0.0
		while(i < a.length && equal)
			if a.array[i] != b.array[i]
				equal = false
			end
			i = i + 1.0
		end
	else
		equal = false
	end

	return equal
end


def DynamicArrayNumbersToLinkedList(da)

	ll = CreateLinkedListNumbers()

	i = 0.0
	while(i < da.length)
		LinkedListAddNumber(ll, da.array[i])
		i = i + 1.0
	end

	return ll
end


def LinkedListToDynamicArrayNumbers(ll)

	node = ll.first

	da = DynamicArrayNumbers.new
	da.length = LinkedListNumbersLength(ll)

	da.array = Array.new(da.length)

	i = 0.0
	while(i < da.length)
		da.array[i] = node.value
		node = node.nextx
		i = i + 1.0
	end

	return da
end


def AddCharacter(list, a)

	newlist = Array.new(list.length + 1.0)
	i = 0.0
	while(i < list.length)
		newlist[i] = list[i]
		i = i + 1.0
	end
	newlist[list.length] = a
		
	delete(list)
		
	return newlist
end


def AddCharacterRef(list, i)
	list.string = AddCharacter(list.string, i)
end


def RemoveCharacter(list, n)

	newlist = Array.new(list.length - 1.0)

	if n >= 0.0 && n < list.length
		i = 0.0
		while(i < list.length)
			if i < n
				newlist[i] = list[i]
			end
			if i > n
				newlist[i - 1.0] = list[i]
			end
			i = i + 1.0
		end

		delete(list)
	else
		delete(newlist)
	end

	return newlist
end


def GetCharacterRef(list, i)
	return list.string[i]
end


def RemoveCharacterRef(list, i)
	list.string = RemoveCharacter(list.string, i)
end


def charToLowerCase(character)

	toReturn = character
	if character == "A"
		toReturn = "a"
	elsif character == "B"
		toReturn = "b"
	elsif character == "C"
		toReturn = "c"
	elsif character == "D"
		toReturn = "d"
	elsif character == "E"
		toReturn = "e"
	elsif character == "F"
		toReturn = "f"
	elsif character == "G"
		toReturn = "g"
	elsif character == "H"
		toReturn = "h"
	elsif character == "I"
		toReturn = "i"
	elsif character == "J"
		toReturn = "j"
	elsif character == "K"
		toReturn = "k"
	elsif character == "L"
		toReturn = "l"
	elsif character == "M"
		toReturn = "m"
	elsif character == "N"
		toReturn = "n"
	elsif character == "O"
		toReturn = "o"
	elsif character == "P"
		toReturn = "p"
	elsif character == "Q"
		toReturn = "q"
	elsif character == "R"
		toReturn = "r"
	elsif character == "S"
		toReturn = "s"
	elsif character == "T"
		toReturn = "t"
	elsif character == "U"
		toReturn = "u"
	elsif character == "V"
		toReturn = "v"
	elsif character == "W"
		toReturn = "w"
	elsif character == "X"
		toReturn = "x"
	elsif character == "Y"
		toReturn = "y"
	elsif character == "Z"
		toReturn = "z"
	end

	return toReturn
end


def charToUpperCase(character)

	toReturn = character
	if character == "a"
		toReturn = "A"
	elsif character == "b"
		toReturn = "B"
	elsif character == "c"
		toReturn = "C"
	elsif character == "d"
		toReturn = "D"
	elsif character == "e"
		toReturn = "E"
	elsif character == "f"
		toReturn = "F"
	elsif character == "g"
		toReturn = "G"
	elsif character == "h"
		toReturn = "H"
	elsif character == "i"
		toReturn = "I"
	elsif character == "j"
		toReturn = "J"
	elsif character == "k"
		toReturn = "K"
	elsif character == "l"
		toReturn = "L"
	elsif character == "m"
		toReturn = "M"
	elsif character == "n"
		toReturn = "N"
	elsif character == "o"
		toReturn = "O"
	elsif character == "p"
		toReturn = "P"
	elsif character == "q"
		toReturn = "Q"
	elsif character == "r"
		toReturn = "R"
	elsif character == "s"
		toReturn = "S"
	elsif character == "t"
		toReturn = "T"
	elsif character == "u"
		toReturn = "U"
	elsif character == "v"
		toReturn = "V"
	elsif character == "w"
		toReturn = "W"
	elsif character == "x"
		toReturn = "X"
	elsif character == "y"
		toReturn = "Y"
	elsif character == "z"
		toReturn = "Z"
	end

	return toReturn
end


def charIsUpperCase(character)

	isUpper = false
	if character == "A"
		isUpper = true
	elsif character == "B"
		isUpper = true
	elsif character == "C"
		isUpper = true
	elsif character == "D"
		isUpper = true
	elsif character == "E"
		isUpper = true
	elsif character == "F"
		isUpper = true
	elsif character == "G"
		isUpper = true
	elsif character == "H"
		isUpper = true
	elsif character == "I"
		isUpper = true
	elsif character == "J"
		isUpper = true
	elsif character == "K"
		isUpper = true
	elsif character == "L"
		isUpper = true
	elsif character == "M"
		isUpper = true
	elsif character == "N"
		isUpper = true
	elsif character == "O"
		isUpper = true
	elsif character == "P"
		isUpper = true
	elsif character == "Q"
		isUpper = true
	elsif character == "R"
		isUpper = true
	elsif character == "S"
		isUpper = true
	elsif character == "T"
		isUpper = true
	elsif character == "U"
		isUpper = true
	elsif character == "V"
		isUpper = true
	elsif character == "W"
		isUpper = true
	elsif character == "X"
		isUpper = true
	elsif character == "Y"
		isUpper = true
	elsif character == "Z"
		isUpper = true
	end

	return isUpper
end


def charIsLowerCase(character)

	isLower = false
	if character == "a"
		isLower = true
	elsif character == "b"
		isLower = true
	elsif character == "c"
		isLower = true
	elsif character == "d"
		isLower = true
	elsif character == "e"
		isLower = true
	elsif character == "f"
		isLower = true
	elsif character == "g"
		isLower = true
	elsif character == "h"
		isLower = true
	elsif character == "i"
		isLower = true
	elsif character == "j"
		isLower = true
	elsif character == "k"
		isLower = true
	elsif character == "l"
		isLower = true
	elsif character == "m"
		isLower = true
	elsif character == "n"
		isLower = true
	elsif character == "o"
		isLower = true
	elsif character == "p"
		isLower = true
	elsif character == "q"
		isLower = true
	elsif character == "r"
		isLower = true
	elsif character == "s"
		isLower = true
	elsif character == "t"
		isLower = true
	elsif character == "u"
		isLower = true
	elsif character == "v"
		isLower = true
	elsif character == "w"
		isLower = true
	elsif character == "x"
		isLower = true
	elsif character == "y"
		isLower = true
	elsif character == "z"
		isLower = true
	end

	return isLower
end


def charIsLetter(character)
	return charIsUpperCase(character) || charIsLowerCase(character)
end


def charIsNumber(character)

	isNumberx = false
	if character == "0"
		isNumberx = true
	elsif character == "1"
		isNumberx = true
	elsif character == "2"
		isNumberx = true
	elsif character == "3"
		isNumberx = true
	elsif character == "4"
		isNumberx = true
	elsif character == "5"
		isNumberx = true
	elsif character == "6"
		isNumberx = true
	elsif character == "7"
		isNumberx = true
	elsif character == "8"
		isNumberx = true
	elsif character == "9"
		isNumberx = true
	end

	return isNumberx
end


def charIsWhiteSpace(character)

	isWhiteSpacex = false
	if character == " "
		isWhiteSpacex = true
	elsif character == "\t"
		isWhiteSpacex = true
	elsif character == "\n"
		isWhiteSpacex = true
	elsif character == "\r"
		isWhiteSpacex = true
	end

	return isWhiteSpacex
end


def charIsSymbol(character)

	isSymbolx = false
	if character == "!"
		isSymbolx = true
	elsif character == "\""
		isSymbolx = true
	elsif character == "#"
		isSymbolx = true
	elsif character == "$"
		isSymbolx = true
	elsif character == "%"
		isSymbolx = true
	elsif character == "&"
		isSymbolx = true
	elsif character == "\'"
		isSymbolx = true
	elsif character == "("
		isSymbolx = true
	elsif character == ")"
		isSymbolx = true
	elsif character == "*"
		isSymbolx = true
	elsif character == "+"
		isSymbolx = true
	elsif character == ","
		isSymbolx = true
	elsif character == "-"
		isSymbolx = true
	elsif character == "."
		isSymbolx = true
	elsif character == "/"
		isSymbolx = true
	elsif character == ":"
		isSymbolx = true
	elsif character == ";"
		isSymbolx = true
	elsif character == "<"
		isSymbolx = true
	elsif character == "="
		isSymbolx = true
	elsif character == ">"
		isSymbolx = true
	elsif character == "?"
		isSymbolx = true
	elsif character == "@"
		isSymbolx = true
	elsif character == "["
		isSymbolx = true
	elsif character == "\\"
		isSymbolx = true
	elsif character == "]"
		isSymbolx = true
	elsif character == "^"
		isSymbolx = true
	elsif character == "_"
		isSymbolx = true
	elsif character == "`"
		isSymbolx = true
	elsif character == "{"
		isSymbolx = true
	elsif character == "|"
		isSymbolx = true
	elsif character == "}"
		isSymbolx = true
	elsif character == "~"
		isSymbolx = true
	end

	return isSymbolx
end


def charCharacterIsBefore(a, b)

	ad = (a).ord
	bd = (b).ord

	return ad < bd
end


def And4Byte(n1, n2)
		if n1 >= 0 and n1 <= 0xFFFFFFFF and n2 >= 0 and n2 <= 0xFFFFFFFF
			return (n1.to_int & n2.to_int) & 0xFFFFFFFF
		else
			return 0
		end
end


def And2Byte(n1, n2)
		if n1 >= 0 and n1 <= 0xFFFF and n2 >= 0 and n2 <= 0xFFFF
			return (n1.to_int & n2.to_int) & 0xFFFF
		else
			return 0
		end
end


def AndByte(n1, n2)
		if n1 >= 0 and n1 <= 0xFF and n2 >= 0 and n2 <= 0xFF
			return (n1.to_int & n2.to_int) & 0xFF
		else
			return 0
		end
end


def AndBytes(n1, n2, bytes)

	byteVal = 1.0
	result = 0.0

	if n1 >= 0.0 && n1 < 2.0**(bytes*8.0) && n2 >= 0.0 && n2 < 2.0**(bytes*8.0)
		n1 = Truncate(n1)
		n2 = Truncate(n2)
		bytes = Truncate(bytes)

		i = 0.0
		while(i < bytes*8.0)
			if n1%2.0 == 1.0 && n2%2.0 == 1.0
				result = result + byteVal
			end
			n1 = (n1 / 2.0).floor
			n2 = (n2 / 2.0).floor
			byteVal = byteVal*2.0
			i = i + 1.0
		end
	end

	return result
end


def Or4Byte(n1, n2)
		if n1 >= 0 and n1 <= 0xFFFFFFFF and n2 >= 0 and n2 <= 0xFFFFFFFF
			return (n1.to_int | n2.to_int) & 0xFFFFFFFF
		else
			return 0
		end
end


def Or2Byte(n1, n2)
		if n1 >= 0 and n1 <= 0xFFFF and n2 >= 0 and n2 <= 0xFFFF
			return (n1.to_int | n2.to_int) & 0xFFFF
		else
			return 0
		end
end


def OrByte(n1, n2)
		if n1 >= 0 and n1 <= 0xFF and n2 >= 0 and n2 <= 0xFF
			return (n1.to_int | n2.to_int) & 0xFF
		else
			return 0
		end
end


def OrBytes(n1, n2, bytes)

	byteVal = 1.0
	result = 0.0

	if n1 >= 0.0 && n1 < 2.0**(bytes*8.0) && n2 >= 0.0 && n2 < 2.0**(bytes*8.0)
		n1 = Truncate(n1)
		n2 = Truncate(n2)
		bytes = Truncate(bytes)

		i = 0.0
		while(i < bytes*8.0)
			if n1%2.0 == 1.0 || n2%2.0 == 1.0
				result = result + byteVal
			end
			n1 = (n1 / 2.0).floor
			n2 = (n2 / 2.0).floor
			byteVal = byteVal*2.0
			i = i + 1.0
		end
	end

	return result
end


def Xor4Byte(n1, n2)
		if n1 >= 0 and n1 <= 0xFFFFFFFF and n2 >= 0 and n2 <= 0xFFFFFFFF
			return (n1.to_int ^ n2.to_int) & 0xFFFFFFFF
		else
			return 0
		end
end


def Xor2Byte(n1, n2)
		if n1 >= 0 and n1 <= 0xFFFF and n2 >= 0 and n2 <= 0xFFFF
			return (n1.to_int ^ n2.to_int) & 0xFFFF
		else
			return 0
		end
end


def XorByte(n1, n2)
		if n1 >= 0 and n1 <= 0xFF and n2 >= 0 and n2 <= 0xFF
			return (n1.to_int ^ n2.to_int) & 0xFF
		else
			return 0
		end
end


def XorBytes(n1, n2, bytes)

	byteVal = 1.0
	result = 0.0

	if n1 >= 0.0 && n1 < 2.0**(bytes*8.0) && n2 >= 0.0 && n2 < 2.0**(bytes*8.0)
		n1 = Truncate(n1)
		n2 = Truncate(n2)
		bytes = Truncate(bytes)

		i = 0.0
		while(i < bytes*8.0)
			if n1%2.0 != n2%2.0
				result = result + byteVal
			end
			n1 = (n1 / 2.0).floor
			n2 = (n2 / 2.0).floor
			byteVal = byteVal*2.0
			i = i + 1.0
		end
	end

	return result
end


def Not4Byte(b)
		if b >= 0 and b <= 0xFFFFFFFF
			return ~(b.to_int) & 0xFFFFFFFF
		else
			return 0
		end
end


def Not2Byte(b)
		if b >= 0 and b <= 0xFFFF
			return ~(b.to_int) & 0xFFFF
		else
			return 0
		end
end


def NotByte(b)
		if b >= 0 and b <= 0xFF
			return ~(b.to_int) & 0xFF
		else
			return 0
		end
end


def NotBytes(b, length)

	result = 0.0

	if b >= 0.0 && b < 2.0**(length*8.0)
		b = Truncate(b)
		length = Truncate(length)

		result = 2.0**(length*8.0) - b - 1.0
	end

	return result
end


def ShiftLeft4Byte(b, amount)
		if b >= 0 and b <= 0xFFFFFFFF and amount >= 0 and amount < 32
			return (b.to_int << amount.to_int) & 0xFFFFFFFF
		else
			return 0
		end
end


def ShiftLeft2Byte(b, amount)
		if b >= 0 and b <= 0xFFFF and amount >= 0 and amount < 16
			return (b.to_int << amount.to_int) & 0xFFFF
		else
			return 0
		end
end


def ShiftLeftByte(b, amount)
		if b >= 0 and b <= 0xFF and amount >= 0 and amount < 8
			return (b.to_int << amount.to_int) & 0xFF
		else
			return 0
		end
end


def ShiftLeftBytes(b, amount, length)

	result = 0.0

	if b >= 0.0 && b < 2.0**(length*8.0) && amount >= 0.0 && amount <= length*8.0
		b = Truncate(b)
		amount = Truncate(amount)

		result = b*2.0**amount
	end

	return result
end


def ShiftRight4Byte(b, amount)
		if b >= 0 and b <= 0xFFFFFFFF and amount >= 0 and amount < 32
			return (b.to_int >> amount.to_int) & 0xFFFFFFFF
		else
			return 0
		end
end


def ShiftRight2Byte(b, amount)
		if b >= 0 and b <= 0xFFFF and amount >= 0 and amount < 16
			return (b.to_int >> amount.to_int) & 0xFFFF
		else
			return 0
		end
end


def ShiftRightByte(b, amount)
		if b >= 0 and b <= 0xFF and amount >= 0 and amount < 8
			return (b.to_int >> amount.to_int) & 0xFF
		else
			return 0
		end
end


def ShiftRightBytes(b, amount, length)

	result = 0.0

	if b >= 0.0 && b < 2.0**(length*8.0) && amount >= 0.0 && amount <= length*8.0
		b = Truncate(b)
		amount = Truncate(amount)

		result = Truncate(b / 2.0**amount)
	end

	return result
end


def ReadNextBit(data, nextbit)

	bytenr = (nextbit.numberValue / 8.0).floor
	bitnumber = nextbit.numberValue%8.0

	b = data[bytenr]

	bit = (b / 2.0**bitnumber).floor%2.0

	nextbit.numberValue = nextbit.numberValue + 1.0

	return bit
end


def BitExtract(b, fromInc, toInc)
	return (b / 2.0**fromInc).floor%2.0**(toInc + 1.0 - fromInc)
end


def ReadBitRange(data, nextbit, length)

	number = 0.0

	startbyte = (nextbit.numberValue / 8.0).floor
	endbyte = ((nextbit.numberValue + length) / 8.0).floor

	startbit = nextbit.numberValue%8.0
	endbit = (nextbit.numberValue + length - 1.0)%8.0

	if startbyte == endbyte
		number = BitExtract(data[startbyte], startbit, endbit)
	end

	nextbit.numberValue = nextbit.numberValue + length

	return number
end


def SkipToBoundary(nextbit)

	skip = 8.0 - nextbit.numberValue%8.0
	nextbit.numberValue = nextbit.numberValue + skip
end


def ReadNextByteBoundary(data, nextbit)

	bytenr = (nextbit.numberValue / 8.0).floor
	b = data[bytenr]
	nextbit.numberValue = nextbit.numberValue + 8.0

	return b
end


def Read2bytesByteBoundary(data, nextbit)

	r = 0.0
	r = r + 2.0**8.0*ReadNextByteBoundary(data, nextbit)
	r = r + ReadNextByteBoundary(data, nextbit)

	return r
end


def ComputeAdler32(data)

	a = 1.0
	b = 0.0
	m = 65521.0

	i = 0.0
	while(i < data.length)
		a = (a + data[i])%m
		b = (b + a)%m
		i = i + 1.0
	end

	return b*2.0**16.0 + a
end


def DeflateDataStaticHuffman(data, level)

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
	match = BooleanReference.new

	bytes = Array.new([data.length*2.0, 100.0].max)
	aFillNumberArray(bytes, 0.0)
	currentBit = CreateNumberReference(0.0)

	bitReverseLookupTable = GenerateBitReverseLookupTable(9.0)

	# Final block
	AppendBitsToBytesRight(bytes, currentBit, 1.0, 1.0)
	# Fixed code
	AppendBitsToBytesRight(bytes, currentBit, 1.0, 2.0)

	i = 0.0
	while(i < data.length)
		FindMatch(data, i, distanceReference, lengthReference, match, level)

		if match.booleanValue
			GetDeflateLengthCode(lengthReference.numberValue, compressedCode, lengthAddition, lengthAdditionLength)
			GetDeflateDistanceCode(distanceReference.numberValue, distanceCode, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable)
		end

		if !match.booleanValue
			GetDeflateStaticHuffmanCode(data[i], code, length, bitReverseLookupTable)
			AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)
			i = i + 1.0
		else
			GetDeflateStaticHuffmanCode(compressedCode.numberValue, code, length, bitReverseLookupTable)
			AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)
			AppendBitsToBytesRight(bytes, currentBit, lengthAddition.numberValue, lengthAdditionLength.numberValue)
			AppendBitsToBytesRight(bytes, currentBit, distanceCode.numberValue, 5.0)
			AppendBitsToBytesRight(bytes, currentBit, distanceAdditionReference.numberValue, distanceAdditionLengthReference.numberValue)
			i = i + lengthReference.numberValue
		end
	end

	# Stop symbol
	GetDeflateStaticHuffmanCode(256.0, code, length, bitReverseLookupTable)
	AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)

	copy = NumberArrayReference.new
	aCopyNumberArrayRange(bytes, 0.0, (currentBit.numberValue / 8.0).ceil, copy)
	delete(bytes)
	bytes = copy.numberArray

	return bytes
end


def FindMatch(data, pos, distanceReference, lengthReference, match, level)

	deflateMinMength = 3.0
	deflateMaxLength = 258.0

	longest = [pos - 1.0, deflateMaxLength].min
	longest = [data.length - pos, longest].min

	deflateMaxDistance = (32768.0 / 10.0*level).floor

	startDistance = [pos, deflateMaxDistance].min

	if longest >= deflateMinMength
		maxLength = 0.0
		distanceForMax = 0.0

		i = pos - 1.0
		while(i >= pos - startDistance && maxLength != longest)
			matchLength = 0.0
			done = false
			j = 0.0
			while(j < longest && !done)
				if data[i + j] == data[pos + j]
					matchLength = matchLength + 1.0
				else
					done = true
				end
				j = j + 1.0
			end

			if matchLength >= deflateMinMength && matchLength > maxLength
				maxLength = matchLength
				distanceForMax = pos - i
			end
			i = i - 1.0
		end

		if maxLength >= deflateMinMength
			match.booleanValue = true
			lengthReference.numberValue = maxLength
			distanceReference.numberValue = distanceForMax
		else
			match.booleanValue = false
		end
	else
		match.booleanValue = false
	end
end


def GenerateBitReverseLookupTable(bits)

	table = Array.new(2.0**bits)

	i = 0.0
	while(i < table.length)
		table[i] = ReverseBits(i, 32.0)
		i = i + 1.0
	end

	return table
end


def ReverseBits(x, bits)

	b = 0.0

	i = 0.0
	while(i < bits)
		b = ShiftLeft4Byte(b, 1.0)
		bit = And4Byte(x, 1.0)
		b = Or4Byte(b, bit)
		x = ShiftRight4Byte(x, 1.0)
		i = i + 1.0
	end

	return b
end


def DeflateDataNoCompression(data)

	maxblocksize = 2.0**16.0 - 1.0
	blocks = (data.length / maxblocksize).ceil

	position = CreateNumberReference(0.0)

	deflated = Array.new((1.0 + 4.0)*blocks + data.length)

	block = 0.0
	while(block < blocks)
		if block + 1.0 == blocks
			WriteByte(deflated, 1.0, position)
		else
			WriteByte(deflated, 0.0, position)
		end
		blocklength = [data.length - block*maxblocksize, maxblocksize].min
		Write2BytesLE(deflated, blocklength, position)
		Write2BytesLE(deflated, Not2Byte(blocklength), position)

		i = 0.0
		while(i < blocklength)
			WriteByte(deflated, data[block*maxblocksize + i], position)
			i = i + 1.0
		end
		block = block + 1.0
	end

	return deflated
end


def GetDeflateStaticHuffmanCode(b, code, length, bitReverseLookupTable)

	if b >= 0.0 && b <= 143.0
		code.numberValue = 48.0 + b
		length.numberValue = 8.0
	elsif b >= 144.0 && b <= 255.0
		code.numberValue = b - 144.0 + 400.0
		length.numberValue = 9.0
	elsif b >= 256.0 && b <= 279.0
		code.numberValue = b - 256.0 + 0.0
		length.numberValue = 7.0
	elsif b >= 280.0 && b <= 287.0
		code.numberValue = b - 280.0 + 192.0
		length.numberValue = 8.0
	end

	reversed = bitReverseLookupTable[code.numberValue]
	code.numberValue = ShiftRight4Byte(reversed, 32.0 - length.numberValue)
end


def GetDeflateLengthCode(length, code, lengthAddition, lengthAdditionLength)
	if length >= 3.0 && length <= 10.0
		code.numberValue = 257.0 + length - 3.0
		lengthAdditionLength.numberValue = 0.0
	elsif length >= 11.0 && length <= 18.0
		code.numberValue = 265.0 + ((length - 11.0) / 2.0).floor
		lengthAddition.numberValue = ((length - 11.0)%2.0).floor
		lengthAdditionLength.numberValue = 1.0
	elsif length >= 19.0 && length <= 34.0
		code.numberValue = 269.0 + ((length - 19.0) / 4.0).floor
		lengthAddition.numberValue = ((length - 19.0)%4.0).floor
		lengthAdditionLength.numberValue = 2.0
	elsif length >= 35.0 && length <= 66.0
		code.numberValue = 273.0 + ((length - 35.0) / 8.0).floor
		lengthAddition.numberValue = ((length - 35.0)%8.0).floor
		lengthAdditionLength.numberValue = 3.0
	elsif length >= 67.0 && length <= 130.0
		code.numberValue = 277.0 + ((length - 67.0) / 16.0).floor
		lengthAddition.numberValue = ((length - 67.0)%16.0).floor
		lengthAdditionLength.numberValue = 4.0
	elsif length >= 131.0 && length <= 257.0
		code.numberValue = 281.0 + ((length - 131.0) / 32.0).floor
		lengthAddition.numberValue = ((length - 131.0)%32.0).floor
		lengthAdditionLength.numberValue = 5.0
	elsif length == 258.0
		code.numberValue = 285.0
		lengthAdditionLength.numberValue = 0.0
	end
end


def GetDeflateDistanceCode(distance, code, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable)

	if distance >= 1.0 && distance <= 4.0
		code.numberValue = distance - 1.0
		distanceAdditionLengthReference.numberValue = 0.0
	elsif distance >= 5.0 && distance <= 8.0
		code.numberValue = 4.0 + ((distance - 5.0) / 2.0).floor
		distanceAdditionReference.numberValue = ((distance - 5.0)%2.0).floor
		distanceAdditionLengthReference.numberValue = 1.0
	elsif distance >= 9.0 && distance <= 16.0
		code.numberValue = 6.0 + ((distance - 9.0) / 4.0).floor
		distanceAdditionReference.numberValue = ((distance - 9.0)%4.0).floor
		distanceAdditionLengthReference.numberValue = 2.0
	elsif distance >= 17.0 && distance <= 32.0
		code.numberValue = 8.0 + ((distance - 17.0) / 8.0).floor
		distanceAdditionReference.numberValue = ((distance - 17.0)%8.0).floor
		distanceAdditionLengthReference.numberValue = 3.0
	elsif distance >= 33.0 && distance <= 64.0
		code.numberValue = 10.0 + ((distance - 33.0) / 16.0).floor
		distanceAdditionReference.numberValue = ((distance - 33.0)%16.0).floor
		distanceAdditionLengthReference.numberValue = 4.0
	elsif distance >= 65.0 && distance <= 128.0
		code.numberValue = 12.0 + ((distance - 65.0) / 32.0).floor
		distanceAdditionReference.numberValue = ((distance - 65.0)%32.0).floor
		distanceAdditionLengthReference.numberValue = 5.0
	elsif distance >= 129.0 && distance <= 256.0
		code.numberValue = 14.0 + ((distance - 129.0) / 64.0).floor
		distanceAdditionReference.numberValue = ((distance - 129.0)%64.0).floor
		distanceAdditionLengthReference.numberValue = 6.0
	elsif distance >= 257.0 && distance <= 512.0
		code.numberValue = 16.0 + ((distance - 257.0) / 128.0).floor
		distanceAdditionReference.numberValue = ((distance - 257.0)%128.0).floor
		distanceAdditionLengthReference.numberValue = 7.0
	elsif distance >= 513.0 && distance <= 1024.0
		code.numberValue = 18.0 + ((distance - 513.0) / 256.0).floor
		distanceAdditionReference.numberValue = ((distance - 513.0)%256.0).floor
		distanceAdditionLengthReference.numberValue = 8.0
	elsif distance >= 1025.0 && distance <= 2048.0
		code.numberValue = 20.0 + ((distance - 1025.0) / 2.0**9.0).floor
		distanceAdditionReference.numberValue = ((distance - 1025.0)%2.0**9.0).floor
		distanceAdditionLengthReference.numberValue = 9.0
	elsif distance >= 2049.0 && distance <= 4096.0
		code.numberValue = 22.0 + ((distance - 2049.0) / 2.0**10.0).floor
		distanceAdditionReference.numberValue = ((distance - 2049.0)%2.0**10.0).floor
		distanceAdditionLengthReference.numberValue = 10.0
	elsif distance >= 4097.0 && distance <= 8192.0
		code.numberValue = 24.0 + ((distance - 4097.0) / 2.0**11.0).floor
		distanceAdditionReference.numberValue = ((distance - 4097.0)%2.0**11.0).floor
		distanceAdditionLengthReference.numberValue = 11.0
	elsif distance >= 8193.0 && distance <= 16384.0
		code.numberValue = 26.0 + ((distance - 8193.0) / 2.0**12.0).floor
		distanceAdditionReference.numberValue = ((distance - 8193.0)%2.0**12.0).floor
		distanceAdditionLengthReference.numberValue = 12.0
	elsif distance >= 16385.0 && distance <= 32768.0
		code.numberValue = 28.0 + ((distance - 16385.0) / 2.0**13.0).floor
		distanceAdditionReference.numberValue = ((distance - 16385.0)%2.0**13.0).floor
		distanceAdditionLengthReference.numberValue = 13.0
	end

	reversed = bitReverseLookupTable[code.numberValue]
	code.numberValue = ShiftRight4Byte(reversed, 32.0 - 5.0)
end


def AppendBitsToBytesLeft(bytes, nextbit, data, length)

	while(length > 0.0)
		bytePos = Truncate(nextbit.numberValue / 8.0)
		bitPos = nextbit.numberValue%8.0

		if length < 8.0 - bitPos
			part = ShiftLeft4Byte(data, 8.0 - bitPos - length)

			bytes[bytePos] = Or4Byte(bytes[bytePos], part)

			nextbit.numberValue = nextbit.numberValue + length

			length = 0.0
		else
			segment = 8.0 - bitPos

			part = ShiftRight4Byte(data, length - segment)
			bytes[bytePos] = Or4Byte(bytes[bytePos], part)
			nextbit.numberValue = nextbit.numberValue + segment

			remove = ShiftLeft4Byte(part, length - segment)
			data = Xor4Byte(data, remove)

			length = length - segment
		end
	end
end


def AppendBitsToBytesRight(bytes, nextbit, data, length)

	while(length > 0.0)
		bytePos = Truncate(nextbit.numberValue / 8.0)
		bitPos = nextbit.numberValue%8.0

		if length < 8.0 - bitPos
			part = ShiftLeft4Byte(data, bitPos)

			bytes[bytePos] = Or4Byte(bytes[bytePos], part)

			nextbit.numberValue = nextbit.numberValue + length

			length = 0.0
		else
			segment = 8.0 - bitPos

			mask = 1.0
			mask = ShiftLeft4Byte(mask, segment)
			mask = mask - 1.0

			part = And4Byte(mask, data)
			part = ShiftLeft4Byte(part, bitPos)
			bytes[bytePos] = Or4Byte(bytes[bytePos], part)
			nextbit.numberValue = nextbit.numberValue + segment

			data = ShiftRight4Byte(data, segment)

			length = length - segment
		end
	end
end


def delete(x)
	# Ruby has garbage collection.
end

