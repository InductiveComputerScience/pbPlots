' Downloaded from https://repo.progsbase.com - Code Developed Using progsbase.

Imports System.Math

Public Class RGBABitmapImageReference
	Public image As RGBABitmapImage
End Class

Public Class Rectangle
	Public x1 As Double
	Public x2 As Double
	Public y1 As Double
	Public y2 As Double
End Class

Public Class ScatterPlotSeries
	Public linearInterpolation As Boolean
	Public pointType As Char ()
	Public lineType As Char ()
	Public lineThickness As Double
	Public xs As Double ()
	Public ys As Double ()
	Public color As RGBA
End Class

Public Class ScatterPlotSettings
	Public scatterPlotSeries As ScatterPlotSeries ()
	Public autoBoundaries As Boolean
	Public xMax As Double
	Public xMin As Double
	Public yMax As Double
	Public yMin As Double
	Public autoPadding As Boolean
	Public xPadding As Double
	Public yPadding As Double
	Public yLabel As Char ()
	Public xLabel As Char ()
	Public title As Char ()
	Public showGrid As Boolean
	Public gridColor As RGBA
	Public xAxisAuto As Boolean
	Public xAxisTop As Boolean
	Public xAxisBottom As Boolean
	Public yAxisAuto As Boolean
	Public yAxisLeft As Boolean
	Public yAxisRight As Boolean
	Public width As Double
	Public height As Double
End Class

Public Class BarPlotSeries
	Public ys As Double ()
	Public color As RGBA
End Class

Public Class BarPlotSettings
	Public width As Double
	Public height As Double
	Public autoBoundaries As Boolean
	Public yMax As Double
	Public yMin As Double
	Public autoPadding As Boolean
	Public xPadding As Double
	Public yPadding As Double
	Public title As Char ()
	Public showGrid As Boolean
	Public gridColor As RGBA
	Public barPlotSeries As BarPlotSeries ()
	Public yLabel As Char ()
	Public autoColor As Boolean
	Public grayscaleAutoColor As Boolean
	Public autoSpacing As Boolean
	Public groupSeparation As Double
	Public barSeparation As Double
	Public autoLabels As Boolean
	Public xLabels As StringReference ()
	Public barBorder As Boolean
End Class

Public Class RGBA
	Public r As Double
	Public g As Double
	Public b As Double
	Public a As Double
End Class

Public Class RGBABitmap
	Public y As RGBA ()
End Class

Public Class RGBABitmapImage
	Public x As RGBABitmap ()
End Class

Public Class BooleanArrayReference
	Public booleanArray As Boolean ()
End Class

Public Class BooleanReference
	Public booleanValue As Boolean
End Class

Public Class CharacterReference
	Public characterValue As Char
End Class

Public Class NumberArrayReference
	Public numberArray As Double ()
End Class

Public Class NumberReference
	Public numberValue As Double
End Class

Public Class StringArrayReference
	Public stringArray As StringReference ()
End Class

Public Class StringReference
	Public stringx As Char ()
End Class

Public Class Chunk
	Public length As Double
	Public type As Char ()
	Public data As Double ()
	Public crc As Double
End Class

Public Class IHDR
	Public Width As Double
	Public Height As Double
	Public BitDepth As Double
	Public ColourType As Double
	Public CompressionMethod As Double
	Public FilterMethod As Double
	Public InterlaceMethod As Double
End Class

Public Class PHYS
	Public pixelsPerMeter As Double
End Class

Public Class PNGImage
	Public signature As Double ()
	Public ihdr As IHDR
	Public zlibStruct As ZLIBStruct
	Public physPresent As Boolean
	Public phys As PHYS
End Class

Public Class ZLIBStruct
	Public CMF As Double
	Public CM As Double
	Public CINFO As Double
	Public FLG As Double
	Public FCHECK As Double
	Public FDICT As Double
	Public FLEVEL As Double
	Public CompressedDataBlocks As Double ()
	Public Adler32CheckValue As Double
End Class

Public Class LinkedListNodeStrings
	Public endx As Boolean
	Public value As Char ()
	Public nextx As LinkedListNodeStrings
End Class

Public Class LinkedListStrings
	Public first As LinkedListNodeStrings
	Public last As LinkedListNodeStrings
End Class

Public Class LinkedListNodeNumbers
	Public nextx As LinkedListNodeNumbers
	Public endx As Boolean
	Public value As Double
End Class

Public Class LinkedListNumbers
	Public first As LinkedListNodeNumbers
	Public last As LinkedListNodeNumbers
End Class

Public Class LinkedListCharacters
	Public first As LinkedListNodeCharacters
	Public last As LinkedListNodeCharacters
End Class

Public Class LinkedListNodeCharacters
	Public endx As Boolean
	Public value As Char
	Public nextx As LinkedListNodeCharacters
End Class

Public Class DynamicArrayNumbers
	Public arrayx As Double ()
	Public length As Double
End Class

Module Plots
	Public Function CropLineWithinBoundary(ByRef x1Ref As NumberReference, ByRef y1Ref As NumberReference, ByRef x2Ref As NumberReference, ByRef y2Ref As NumberReference, xMin As Double, xMax As Double, yMin As Double, yMax As Double) As Boolean
		Dim x1, y1, x2, y2 As Double
		Dim success, p1In, p2In As Boolean
		Dim dx, dy, f1, f2, f3, f4, f As Double

		x1 = x1Ref.numberValue
		y1 = y1Ref.numberValue
		x2 = x2Ref.numberValue
		y2 = y2Ref.numberValue

		p1In = x1 >= xMin And x1 <= xMax And y1 >= yMin And y1 <= yMax
		p2In = x2 >= xMin And x2 <= xMax And y2 >= yMin And y2 <= yMax

		If p1In And p2In
			success = true
		ElseIf Not p1In And p2In
			dx = x1 - x2
			dy = y1 - y2

			If dx <> 0
				f1 = (xMin - x2)/dx
				f2 = (xMax - x2)/dx
			Else
				f1 = 1
				f2 = 1
			End If
			If dy <> 0
				f3 = (yMin - y2)/dy
				f4 = (yMax - y2)/dy
			Else
				f3 = 1
				f4 = 1
			End If

			If f1 < 0
				f1 = 1
			End If
			If f2 < 0
				f2 = 1
			End If
			If f3 < 0
				f3 = 1
			End If
			If f4 < 0
				f4 = 1
			End If

			f = Min(f1, Min(f2, Min(f3, f4)))

			x1 = x2 + f*dx
			y1 = y2 + f*dy

			success = true
		ElseIf p1In And Not p2In
			dx = x2 - x1
			dy = y2 - y1

			If dx <> 0
				f1 = (xMin - x1)/dx
				f2 = (xMax - x1)/dx
			Else
				f1 = 1
				f2 = 1
			End If
			If dy <> 0
				f3 = (yMin - y1)/dy
				f4 = (yMax - y1)/dy
			Else
				f3 = 1
				f4 = 1
			End If

			If f1 < 0
				f1 = 1
			End If
			If f2 < 0
				f2 = 1
			End If
			If f3 < 0
				f3 = 1
			End If
			If f4 < 0
				f4 = 1
			End If

			f = Min(f1, Min(f2, Min(f3, f4)))

			x2 = x1 + f*dx
			y2 = y1 + f*dy

			success = true
		Else
			success = false
		End If

		x1Ref.numberValue = x1
		y1Ref.numberValue = y1
		x2Ref.numberValue = x2
		y2Ref.numberValue = y2

		Return success
	End Function


	Public Function IncrementFromCoordinates(x1 As Double, y1 As Double, x2 As Double, y2 As Double) As Double
		Return (x2 - x1)/(y2 - y1)
	End Function


	Public Function InterceptFromCoordinates(x1 As Double, y1 As Double, x2 As Double, y2 As Double) As Double
		Dim a, b As Double

		a = IncrementFromCoordinates(x1, y1, x2, y2)
		b = y1 - a*x1

		Return b
	End Function


	Public Function Get8HighContrastColors() As RGBA ()
		Dim colors As RGBA ()
		colors = New RGBA (8 - 1){}
		colors(0) = CreateRGBColor(3/256, 146/256, 206/256)
		colors(1) = CreateRGBColor(253/256, 83/256, 8/256)
		colors(2) = CreateRGBColor(102/256, 176/256, 50/256)
		colors(3) = CreateRGBColor(208/256, 234/256, 43/256)
		colors(4) = CreateRGBColor(167/256, 25/256, 75/256)
		colors(5) = CreateRGBColor(254/256, 254/256, 51/256)
		colors(6) = CreateRGBColor(134/256, 1/256, 175/256)
		colors(7) = CreateRGBColor(251/256, 153/256, 2/256)
		Return colors
	End Function


	Public Sub DrawFilledRectangleWithBorder(ByRef image As RGBABitmapImage, x As Double, y As Double, w As Double, h As Double, ByRef borderColor As RGBA, ByRef fillColor As RGBA)
		If h > 0 And w > 0
			Call DrawFilledRectangle(image, x, y, w, h, fillColor)
			Call DrawRectangle1px(image, x, y, w, h, borderColor)
		End If
	End Sub


	Public Function CreateRGBABitmapImageReference() As RGBABitmapImageReference
		Dim reference As RGBABitmapImageReference

		reference = New RGBABitmapImageReference()
		reference.image = New RGBABitmapImage()
		reference.image.x = New RGBABitmap (0 - 1){}

		Return reference
	End Function


	Public Function RectanglesOverlap(ByRef r1 As Rectangle, ByRef r2 As Rectangle) As Boolean
		Dim overlap As Boolean

		overlap = false

		overlap = overlap Or (r2.x1 >= r1.x1 And r2.x1 <= r1.x2 And r2.y1 >= r1.y1 And r2.y1 <= r1.y2)
		overlap = overlap Or (r2.x2 >= r1.x1 And r2.x2 <= r1.x2 And r2.y1 >= r1.y1 And r2.y1 <= r1.y2)
		overlap = overlap Or (r2.x1 >= r1.x1 And r2.x1 <= r1.x2 And r2.y2 >= r1.y1 And r2.y2 <= r1.y2)
		overlap = overlap Or (r2.x2 >= r1.x1 And r2.x2 <= r1.x2 And r2.y2 >= r1.y1 And r2.y2 <= r1.y2)

		Return overlap
	End Function


	Public Function CreateRectangle(x1 As Double, y1 As Double, x2 As Double, y2 As Double) As Rectangle
		Dim r As Rectangle
		r = New Rectangle()
		r.x1 = x1
		r.y1 = y1
		r.x2 = x2
		r.y2 = y2
		Return r
	End Function


	Public Sub CopyRectangleValues(ByRef rd As Rectangle, ByRef rs As Rectangle)
		rd.x1 = rs.x1
		rd.y1 = rs.y1
		rd.x2 = rs.x2
		rd.y2 = rs.y2
	End Sub


	Public Sub DrawXLabelsForPriority(p As Double, xMin As Double, oy As Double, xMax As Double, xPixelMin As Double, xPixelMax As Double, ByRef nextRectangle As NumberReference, ByRef gridLabelColor As RGBA, ByRef canvas As RGBABitmapImage, ByRef xGridPositions As Double (), ByRef xLabels As StringArrayReference, ByRef xLabelPriorities As NumberArrayReference, ByRef occupied As Rectangle (), textOnBottom As Boolean)
		Dim overlap, currentOverlaps As Boolean
		Dim i, j, x, px, padding As Double
		Dim text As Char ()
		Dim r As Rectangle

		r = New Rectangle()
		padding = 10

		overlap = false
		i = 0
		While i < xLabels.stringArray.Length
			If xLabelPriorities.numberArray(i) = p

				x = xGridPositions(i)
				px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)
				text = xLabels.stringArray(i).stringx

				r.x1 = Floor(px - GetTextWidth(text)/2)
				If textOnBottom
					r.y1 = Floor(oy + 5)
				Else
					r.y1 = Floor(oy - 20)
				End If
				r.x2 = r.x1 + GetTextWidth(text)
				r.y2 = r.y1 + GetTextHeight(text)

				' Add padding
				r.x1 = r.x1 - padding
				r.y1 = r.y1 - padding
				r.x2 = r.x2 + padding
				r.y2 = r.y2 + padding

				currentOverlaps = false

				j = 0
				While j < nextRectangle.numberValue
					currentOverlaps = currentOverlaps Or RectanglesOverlap(r, occupied(j))
					j = j + 1
				End While

				If Not currentOverlaps And p = 1
					Call DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor)

					Call CopyRectangleValues(occupied(nextRectangle.numberValue), r)
					nextRectangle.numberValue = nextRectangle.numberValue + 1
				End If

				overlap = overlap Or currentOverlaps
			End If
			i = i + 1
		End While
		If Not overlap And p <> 1
			i = 0
			While i < xGridPositions.Length
				x = xGridPositions(i)
				px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)

				If xLabelPriorities.numberArray(i) = p
					text = xLabels.stringArray(i).stringx

					r.x1 = Floor(px - GetTextWidth(text)/2)
					If textOnBottom
						r.y1 = Floor(oy + 5)
					Else
						r.y1 = Floor(oy - 20)
					End If
					r.x2 = r.x1 + GetTextWidth(text)
					r.y2 = r.y1 + GetTextHeight(text)

					Call DrawText(canvas, r.x1, r.y1, text, gridLabelColor)

					Call CopyRectangleValues(occupied(nextRectangle.numberValue), r)
					nextRectangle.numberValue = nextRectangle.numberValue + 1
				End If
				i = i + 1
			End While
		End If
	End Sub


	Public Sub DrawYLabelsForPriority(p As Double, yMin As Double, ox As Double, yMax As Double, yPixelMin As Double, yPixelMax As Double, ByRef nextRectangle As NumberReference, ByRef gridLabelColor As RGBA, ByRef canvas As RGBABitmapImage, ByRef yGridPositions As Double (), ByRef yLabels As StringArrayReference, ByRef yLabelPriorities As NumberArrayReference, ByRef occupied As Rectangle (), textOnLeft As Boolean)
		Dim overlap, currentOverlaps As Boolean
		Dim i, j, y, py, padding As Double
		Dim text As Char ()
		Dim r As Rectangle

		r = New Rectangle()
		padding = 10

		overlap = false
		i = 0
		While i < yLabels.stringArray.Length
			If yLabelPriorities.numberArray(i) = p

				y = yGridPositions(i)
				py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
				text = yLabels.stringArray(i).stringx

				If textOnLeft
					r.x1 = Floor(ox - GetTextWidth(text) - 10)
				Else
					r.x1 = Floor(ox + 10)
				End If
				r.y1 = Floor(py - 6)
				r.x2 = r.x1 + GetTextWidth(text)
				r.y2 = r.y1 + GetTextHeight(text)

				' Add padding
				r.x1 = r.x1 - padding
				r.y1 = r.y1 - padding
				r.x2 = r.x2 + padding
				r.y2 = r.y2 + padding

				currentOverlaps = false

				j = 0
				While j < nextRectangle.numberValue
					currentOverlaps = currentOverlaps Or RectanglesOverlap(r, occupied(j))
					j = j + 1
				End While

				' Draw labels with priority 1 if they do not overlap anything else.
				If Not currentOverlaps And p = 1
					Call DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor)

					Call CopyRectangleValues(occupied(nextRectangle.numberValue), r)
					nextRectangle.numberValue = nextRectangle.numberValue + 1
				End If

				overlap = overlap Or currentOverlaps
			End If
			i = i + 1
		End While
		If Not overlap And p <> 1
			i = 0
			While i < yGridPositions.Length
				y = yGridPositions(i)
				py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)

				If yLabelPriorities.numberArray(i) = p
					text = yLabels.stringArray(i).stringx

					If textOnLeft
						r.x1 = Floor(ox - GetTextWidth(text) - 10)
					Else
						r.x1 = Floor(ox + 10)
					End If
					r.y1 = Floor(py - 6)
					r.x2 = r.x1 + GetTextWidth(text)
					r.y2 = r.y1 + GetTextHeight(text)

					Call DrawText(canvas, r.x1, r.y1, text, gridLabelColor)

					Call CopyRectangleValues(occupied(nextRectangle.numberValue), r)
					nextRectangle.numberValue = nextRectangle.numberValue + 1
				End If
				i = i + 1
			End While
		End If
	End Sub


	Public Function ComputeGridLinePositions(cMin As Double, cMax As Double, ByRef labels As StringArrayReference, ByRef priorities As NumberArrayReference) As Double ()
		Dim positions As Double ()
		Dim cLength, p, pMin, pMax, pInterval, pNum, i, num, remx, priority, mode As Double

		cLength = cMax - cMin

		p = Floor(Log10(cLength))
		pInterval = 10 ^ p
		' gives 10-1 lines for 100-10 diff
		pMin = Ceiling(cMin/pInterval)*pInterval
		pMax = Floor(cMax/pInterval)*pInterval
		pNum = Roundx((pMax - pMin)/pInterval + 1)

		mode = 1

		If pNum <= 3
			p = Floor(Log10(cLength) - 1)
			' gives 100-10 lines for 100-10 diff
			pInterval = 10 ^ p
			pMin = Ceiling(cMin/pInterval)*pInterval
			pMax = Floor(cMax/pInterval)*pInterval
			pNum = Roundx((pMax - pMin)/pInterval + 1)

			mode = 4
		ElseIf pNum <= 6
			p = Floor(Log10(cLength))
			pInterval = 10 ^ p/4
			' gives 40-5 lines for 100-10 diff
			pMin = Ceiling(cMin/pInterval)*pInterval
			pMax = Floor(cMax/pInterval)*pInterval
			pNum = Roundx((pMax - pMin)/pInterval + 1)

			mode = 3
		ElseIf pNum <= 10
			p = Floor(Log10(cLength))
			pInterval = 10 ^ p/2
			' gives 20-3 lines for 100-10 diff
			pMin = Ceiling(cMin/pInterval)*pInterval
			pMax = Floor(cMax/pInterval)*pInterval
			pNum = Roundx((pMax - pMin)/pInterval + 1)

			mode = 2
		End If

		positions = New Double (pNum - 1){}
		labels.stringArray = New StringReference (pNum - 1){}
		priorities.numberArray = New Double (pNum - 1){}

		i = 0
		While i < pNum
			num = pMin + pInterval*i
			positions(i) = num

			' Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn.
			priority = 1

			' Prioritize x.25, x.5 and x.75 lower.
			If mode = 2 Or mode = 3
				remx = Abs(Round(num/10 ^ (p - 2))) Mod 100

				priority = 1
				If remx = 50
					priority = 2
				ElseIf remx = 25 Or remx = 75
					priority = 3
				End If
			End If

			' Prioritize x.1-x.4 and x.6-x.9 lower
			If mode = 4
				remx = Abs(Roundx(num/10 ^ p)) Mod 10

				priority = 1
				If remx = 1 Or remx = 2 Or remx = 3 Or remx = 4 Or remx = 6 Or remx = 7 Or remx = 8 Or remx = 9
					priority = 2
				End If
			End If

			' 0 has lowest priority.
			If EpsilonCompare(num, 0, 10 ^ (p - 5))
				priority = 3
			End If

			priorities.numberArray(i) = priority

			' The label itself.
			labels.stringArray(i) = New StringReference()
			If p < 0
				If mode = 2 Or mode = 3
					num = RoundToDigits(num, -(p - 1))
				Else
					num = RoundToDigits(num, -p)
				End If
			End If
			labels.stringArray(i).stringx = CreateStringDecimalFromNumber(num)
			i = i + 1
		End While

		Return positions
	End Function


	Public Function MapYCoordinate(y As Double, yMin As Double, yMax As Double, yPixelMin As Double, yPixelMax As Double) As Double
		Dim yLength, yPixelLength As Double

		yLength = yMax - yMin
		yPixelLength = yPixelMax - yPixelMin

		y = y - yMin
		y = y*yPixelLength/yLength
		y = yPixelLength - y
		y = y + yPixelMin
		Return y
	End Function


	Public Function MapXCoordinate(x As Double, xMin As Double, xMax As Double, xPixelMin As Double, xPixelMax As Double) As Double
		Dim xLength, xPixelLength As Double

		xLength = xMax - xMin
		xPixelLength = xPixelMax - xPixelMin

		x = x - xMin
		x = x*xPixelLength/xLength
		x = x + xPixelMin
		Return x
	End Function


	Public Function MapXCoordinateAutoSettings(x As Double, ByRef image As RGBABitmapImage, ByRef xs As Double ()) As Double
		Return MapXCoordinate(x, GetMinimum(xs), GetMaximum(xs) - GetMinimum(xs), GetDefaultPaddingPercentage()*ImageWidth(image), (1 - GetDefaultPaddingPercentage())*ImageWidth(image))
	End Function


	Public Function MapYCoordinateAutoSettings(y As Double, ByRef image As RGBABitmapImage, ByRef ys As Double ()) As Double
		Return MapYCoordinate(y, GetMinimum(ys), GetMaximum(ys), GetDefaultPaddingPercentage()*ImageHeight(image), (1 - GetDefaultPaddingPercentage())*ImageHeight(image))
	End Function


	Public Function GetDefaultPaddingPercentage() As Double
		Return 0.10
	End Function


	Public Sub DrawText(ByRef canvas As RGBABitmapImage, x As Double, y As Double, ByRef text As Char (), ByRef color As RGBA)
		Dim i, charWidth, spacing As Double

		charWidth = 8
		spacing = 2

		i = 0
		While i < text.Length
			Call DrawAsciiCharacter(canvas, x + i*(charWidth + spacing), y, text(i), color)
			i = i + 1
		End While
	End Sub


	Public Sub DrawTextUpwards(ByRef canvas As RGBABitmapImage, x As Double, y As Double, ByRef text As Char (), ByRef color As RGBA)
		Dim buffer, rotated As RGBABitmapImage

		buffer = CreateImage(GetTextWidth(text), GetTextHeight(text), GetTransparent())
		Call DrawText(buffer, 0, 0, text, color)
		rotated = RotateAntiClockwise90Degrees(buffer)
		Call DrawImageOnImage(canvas, rotated, x, y)
		Call DeleteImage(buffer)
		Call DeleteImage(rotated)
	End Sub


	Public Function GetDefaultScatterPlotSettings() As ScatterPlotSettings
		Dim settings As ScatterPlotSettings

		settings = New ScatterPlotSettings()

		settings.autoBoundaries = true
		settings.xMax = 0
		settings.xMin = 0
		settings.yMax = 0
		settings.yMin = 0
		settings.autoPadding = true
		settings.xPadding = 0
		settings.yPadding = 0
		settings.title = "".ToCharArray()
		settings.yLabel = "".ToCharArray()
		settings.xLabel = "".ToCharArray()
		settings.scatterPlotSeries = New ScatterPlotSeries (0 - 1){}
		settings.showGrid = true
		settings.gridColor = GetGray(0.1)
		settings.xAxisAuto = true
		settings.xAxisTop = false
		settings.xAxisBottom = false
		settings.yAxisAuto = true
		settings.yAxisLeft = false
		settings.yAxisRight = false

		Return settings
	End Function


	Public Function GetDefaultScatterPlotSeriesSettings() As ScatterPlotSeries
		Dim series As ScatterPlotSeries

		series = New ScatterPlotSeries()

		series.linearInterpolation = true
		series.pointType = "pixels".ToCharArray()
		series.lineType = "solid".ToCharArray()
		series.lineThickness = 1
		series.xs = New Double (0 - 1){}
		series.ys = New Double (0 - 1){}
		series.color = GetBlack()

		Return series
	End Function


	Public Sub DrawScatterPlot(ByRef canvasReference As RGBABitmapImageReference, width As Double, height As Double, ByRef xs As Double (), ByRef ys As Double ())
		Dim settings As ScatterPlotSettings

		settings = GetDefaultScatterPlotSettings()

		settings.width = width
		settings.height = height
		settings.scatterPlotSeries = New ScatterPlotSeries (1 - 1){}
		settings.scatterPlotSeries(0) = GetDefaultScatterPlotSeriesSettings()
		Erase settings.scatterPlotSeries(0).xs 
		settings.scatterPlotSeries(0).xs = xs
		Erase settings.scatterPlotSeries(0).ys 
		settings.scatterPlotSeries(0).ys = ys

		DrawScatterPlotFromSettings(canvasReference, settings)
	End Sub


	Public Function DrawScatterPlotFromSettings(ByRef canvasReference As RGBABitmapImageReference, ByRef settings As ScatterPlotSettings) As Boolean
		Dim xMin, xMax, yMin, yMax, xLength, yLength, i, x, y, xPrev, yPrev, px, py, pxPrev, pyPrev, originX, originY, p, l, plot As Double
		Dim xPadding, yPadding, originXPixels, originYPixels As Double
		Dim xPixelMin, yPixelMin, xPixelMax, yPixelMax, xLengthPixels, yLengthPixels, axisLabelPadding As Double
		Dim nextRectangle, x1Ref, y1Ref, x2Ref, y2Ref, patternOffset As NumberReference
		Dim prevSet, success As Boolean
		Dim gridLabelColor As RGBA
		Dim canvas As RGBABitmapImage
		Dim xs, ys As Double ()
		Dim linearInterpolation As Boolean
		Dim sp As ScatterPlotSeries
		Dim xGridPositions, yGridPositions As Double ()
		Dim xLabels, yLabels As StringArrayReference
		Dim xLabelPriorities, yLabelPriorities As NumberArrayReference
		Dim occupied As Rectangle ()
		Dim linePattern As Boolean ()
		Dim originXInside, originYInside, textOnLeft, textOnBottom As Boolean
		Dim originTextX, originTextY, originTextXPixels, originTextYPixels, side As Double

		canvas = CreateImage(settings.width, settings.height, GetWhite())
		patternOffset = CreateNumberReference(0)

		success = ScatterPlotFromSettingsValid(settings)

		If success

			If settings.scatterPlotSeries.Length >= 1
				xMin = GetMinimum(settings.scatterPlotSeries(0).xs)
				xMax = GetMaximum(settings.scatterPlotSeries(0).xs)
				yMin = GetMinimum(settings.scatterPlotSeries(0).ys)
				yMax = GetMaximum(settings.scatterPlotSeries(0).ys)
			Else
				xMin = -10
				xMax = 10
				yMin = -10
				yMax = 10
			End If

			If Not settings.autoBoundaries
				xMin = settings.xMin
				xMax = settings.xMax
				yMin = settings.yMin
				yMax = settings.yMax
			Else
				plot = 1
				While plot < settings.scatterPlotSeries.Length
					sp = settings.scatterPlotSeries(plot)

					xMin = Min(xMin, GetMinimum(sp.xs))
					xMax = Max(xMax, GetMaximum(sp.xs))
					yMin = Min(yMin, GetMinimum(sp.ys))
					yMax = Max(yMax, GetMaximum(sp.ys))
					plot = plot + 1
				End While
			End If

			xLength = xMax - xMin
			yLength = yMax - yMin

			If settings.autoPadding
				xPadding = Floor(GetDefaultPaddingPercentage()*ImageWidth(canvas))
				yPadding = Floor(GetDefaultPaddingPercentage()*ImageHeight(canvas))
			Else
				xPadding = settings.xPadding
				yPadding = settings.yPadding
			End If

			' Draw title
			Call DrawText(canvas, Floor(ImageWidth(canvas)/2 - GetTextWidth(settings.title)/2), Floor(yPadding/3), settings.title, GetBlack())

			' Draw grid
			xPixelMin = xPadding
			yPixelMin = yPadding
			xPixelMax = ImageWidth(canvas) - xPadding
			yPixelMax = ImageHeight(canvas) - yPadding
			xLengthPixels = xPixelMax - xPixelMin
			yLengthPixels = yPixelMax - yPixelMin
			Call DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor)

			gridLabelColor = GetGray(0.5)

			xLabels = New StringArrayReference()
			xLabelPriorities = New NumberArrayReference()
			yLabels = New StringArrayReference()
			yLabelPriorities = New NumberArrayReference()
			xGridPositions = ComputeGridLinePositions(xMin, xMax, xLabels, xLabelPriorities)
			yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities)

			If settings.showGrid
				' X-grid
				i = 0
				While i < xGridPositions.Length
					x = xGridPositions(i)
					px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)
					Call DrawLine1px(canvas, px, yPixelMin, px, yPixelMax, settings.gridColor)
					i = i + 1
				End While

				' Y-grid
				i = 0
				While i < yGridPositions.Length
					y = yGridPositions(i)
					py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
					Call DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor)
					i = i + 1
				End While
			End If

			' Compute origin information.
			originYInside = yMin < 0 And yMax > 0
			originY = 0
			If settings.xAxisAuto
				If originYInside
					originY = 0
				Else
					originY = yMin
				End If
			Else
				If settings.xAxisTop
					originY = yMax
				End If
				If settings.xAxisBottom
					originY = yMin
				End If
			End If
			originYPixels = MapYCoordinate(originY, yMin, yMax, yPixelMin, yPixelMax)

			originXInside = xMin < 0 And xMax > 0
			originX = 0
			If settings.yAxisAuto
				If originXInside
					originX = 0
				Else
					originX = xMin
				End If
			Else
				If settings.yAxisLeft
					originX = xMin
				End If
				If settings.yAxisRight
					originX = xMax
				End If
			End If
			originXPixels = MapXCoordinate(originX, xMin, xMax, xPixelMin, xPixelMax)

			If originYInside
				originTextY = 0
			Else
				originTextY = yMin + yLength/2
			End If
			originTextYPixels = MapYCoordinate(originTextY, yMin, yMax, yPixelMin, yPixelMax)

			If originXInside
				originTextX = 0
			Else
				originTextX = xMin + xLength/2
			End If
			originTextXPixels = MapXCoordinate(originTextX, xMin, xMax, xPixelMin, xPixelMax)

			' Labels
			occupied = New Rectangle (xLabels.stringArray.Length + yLabels.stringArray.Length - 1){}
			i = 0
			While i < occupied.Length
				occupied(i) = CreateRectangle(0, 0, 0, 0)
				i = i + 1
			End While
			nextRectangle = CreateNumberReference(0)

			' x labels
			i = 1
			While i <= 5
				textOnBottom = true
				If Not settings.xAxisAuto And settings.xAxisTop
					textOnBottom = false
				End If
				Call DrawXLabelsForPriority(i, xMin, originYPixels, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied, textOnBottom)
				i = i + 1
			End While

			' y labels
			i = 1
			While i <= 5
				textOnLeft = true
				If Not settings.yAxisAuto And settings.yAxisRight
					textOnLeft = false
				End If
				Call DrawYLabelsForPriority(i, yMin, originXPixels, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, textOnLeft)
				i = i + 1
			End While

			' Draw origin line axis titles.
			axisLabelPadding = 20

			' x origin line
			If originYInside
				Call DrawLine1px(canvas, Roundx(xPixelMin), Roundx(originYPixels), Roundx(xPixelMax), Roundx(originYPixels), GetBlack())
			End If

			' y origin line
			If originXInside
				Call DrawLine1px(canvas, Roundx(originXPixels), Roundx(yPixelMin), Roundx(originXPixels), Roundx(yPixelMax), GetBlack())
			End If

			' Draw origin axis titles.
			Call DrawTextUpwards(canvas, 10, Floor(originTextYPixels - GetTextWidth(settings.xLabel)/2), settings.xLabel, GetBlack())
			Call DrawText(canvas, Floor(originTextXPixels - GetTextWidth(settings.yLabel)/2), yPixelMax + axisLabelPadding, settings.yLabel, GetBlack())

			' X-grid-markers
			i = 0
			While i < xGridPositions.Length
				x = xGridPositions(i)
				px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax)
				p = xLabelPriorities.numberArray(i)
				l = 1
				If p = 1
					l = 8
				ElseIf p = 2
					l = 3
				End If
				side = -1
				If Not settings.xAxisAuto And settings.xAxisTop
					side = 1
				End If
				Call DrawLine1px(canvas, px, originYPixels, px, originYPixels + side*l, GetBlack())
				i = i + 1
			End While

			' Y-grid-markers
			i = 0
			While i < yGridPositions.Length
				y = yGridPositions(i)
				py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
				p = yLabelPriorities.numberArray(i)
				l = 1
				If p = 1
					l = 8
				ElseIf p = 2
					l = 3
				End If
				side = 1
				If Not settings.yAxisAuto And settings.yAxisRight
					side = -1
				End If
				Call DrawLine1px(canvas, originXPixels, py, originXPixels + side*l, py, GetBlack())
				i = i + 1
			End While

			' Draw points
			plot = 0
			While plot < settings.scatterPlotSeries.Length
				sp = settings.scatterPlotSeries(plot)

				xs = sp.xs
				ys = sp.ys
				linearInterpolation = sp.linearInterpolation

				x1Ref = New NumberReference()
				y1Ref = New NumberReference()
				x2Ref = New NumberReference()
				y2Ref = New NumberReference()
				If linearInterpolation
					prevSet = false
					xPrev = 0
					yPrev = 0
					i = 0
					While i < xs.Length
						x = xs(i)
						y = ys(i)

						If prevSet
							x1Ref.numberValue = xPrev
							y1Ref.numberValue = yPrev
							x2Ref.numberValue = x
							y2Ref.numberValue = y

							success = CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax)

							If success
								pxPrev = Floor(MapXCoordinate(x1Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax))
								pyPrev = Floor(MapYCoordinate(y1Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax))
								px = Floor(MapXCoordinate(x2Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax))
								py = Floor(MapYCoordinate(y2Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax))

								If aStringsEqual(sp.lineType, "solid".ToCharArray()) And sp.lineThickness = 1
									Call DrawLine1px(canvas, pxPrev, pyPrev, px, py, sp.color)
								ElseIf aStringsEqual(sp.lineType, "solid".ToCharArray())
									Call DrawLine(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, sp.color)
								ElseIf aStringsEqual(sp.lineType, "dashed".ToCharArray())
									linePattern = GetLinePattern1()
									Call DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
								ElseIf aStringsEqual(sp.lineType, "dotted".ToCharArray())
									linePattern = GetLinePattern2()
									Call DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
								ElseIf aStringsEqual(sp.lineType, "dotdash".ToCharArray())
									linePattern = GetLinePattern3()
									Call DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
								ElseIf aStringsEqual(sp.lineType, "longdash".ToCharArray())
									linePattern = GetLinePattern4()
									Call DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
								ElseIf aStringsEqual(sp.lineType, "twodash".ToCharArray())
									linePattern = GetLinePattern5()
									Call DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color)
								End If
							End If
						End If

						prevSet = true
						xPrev = x
						yPrev = y
						i = i + 1
					End While
				Else
					i = 0
					While i < xs.Length
						x = xs(i)
						y = ys(i)

						If x > xMin And x < xMax And y > yMin And y < yMax

							x = Floor(MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax))
							y = Floor(MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax))

							If aStringsEqual(sp.pointType, "crosses".ToCharArray())
								Call DrawPixel(canvas, x, y, sp.color)
								Call DrawPixel(canvas, x + 1, y, sp.color)
								Call DrawPixel(canvas, x + 2, y, sp.color)
								Call DrawPixel(canvas, x - 1, y, sp.color)
								Call DrawPixel(canvas, x - 2, y, sp.color)
								Call DrawPixel(canvas, x, y + 1, sp.color)
								Call DrawPixel(canvas, x, y + 2, sp.color)
								Call DrawPixel(canvas, x, y - 1, sp.color)
								Call DrawPixel(canvas, x, y - 2, sp.color)
							ElseIf aStringsEqual(sp.pointType, "circles".ToCharArray())
								Call DrawCircle(canvas, x, y, 3, sp.color)
							ElseIf aStringsEqual(sp.pointType, "dots".ToCharArray())
								Call DrawFilledCircle(canvas, x, y, 3, sp.color)
							ElseIf aStringsEqual(sp.pointType, "triangles".ToCharArray())
								Call DrawTriangle(canvas, x, y, 3, sp.color)
							ElseIf aStringsEqual(sp.pointType, "filled triangles".ToCharArray())
								Call DrawFilledTriangle(canvas, x, y, 3, sp.color)
							ElseIf aStringsEqual(sp.pointType, "pixels".ToCharArray())
								Call DrawPixel(canvas, x, y, sp.color)
							End If
						End If
						i = i + 1
					End While
				End If
				plot = plot + 1
			End While

			Call DeleteImage(canvasReference.image)
			canvasReference.image = canvas
		End If

		Return success
	End Function


	Public Function ScatterPlotFromSettingsValid(ByRef settings As ScatterPlotSettings) As Boolean
		Dim success, found As Boolean
		Dim series As ScatterPlotSeries
		Dim i As Double

		success = true

		' Check axis placement.
		If Not settings.xAxisAuto
			If settings.xAxisTop And settings.xAxisBottom
				success = false
			End If
			If Not settings.xAxisTop And Not settings.xAxisBottom
				success = false
			End If
		End If

		If Not settings.yAxisAuto
			If settings.yAxisLeft And settings.yAxisRight
				success = false
			End If
			If Not settings.yAxisLeft And Not settings.yAxisRight
				success = false
			End If
		End If

		' Check series lengths.
		i = 0
		While i < settings.scatterPlotSeries.Length
			series = settings.scatterPlotSeries(i)
			If series.xs.Length <> series.ys.Length
				success = false
			End If
			If series.xs.Length = 0
				success = false
			End If
			If series.linearInterpolation And series.xs.Length = 1
				success = false
			End If
			i = i + 1
		End While

		' Check bounds.
		If Not settings.autoBoundaries
			If settings.xMin >= settings.xMax
				success = false
			End If
			If settings.yMin >= settings.yMax
				success = false
			End If
		End If

		' Check padding.
		If Not settings.autoPadding
			If 2*settings.xPadding >= settings.width
				success = false
			End If
			If 2*settings.yPadding >= settings.height
				success = false
			End If
		End If

		' Check width and height.
		If settings.width < 0
			success = false
		End If
		If settings.height < 0
			success = false
		End If

		' Check point types.
		i = 0
		While i < settings.scatterPlotSeries.Length
			series = settings.scatterPlotSeries(i)

			If series.lineThickness < 0
				success = false
			End If

			If Not series.linearInterpolation
				' Point type.
				found = false
				If aStringsEqual(series.pointType, "crosses".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.pointType, "circles".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.pointType, "dots".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.pointType, "triangles".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.pointType, "filled triangles".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.pointType, "pixels".ToCharArray())
					found = true
				End If
				If Not found
					success = false
				End If
			Else
				' Line type.
				found = false
				If aStringsEqual(series.lineType, "solid".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.lineType, "dashed".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.lineType, "dotted".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.lineType, "dotdash".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.lineType, "longdash".ToCharArray())
					found = true
				ElseIf aStringsEqual(series.lineType, "twodash".ToCharArray())
					found = true
				End If

				If Not found
					success = false
				End If
			End If
			i = i + 1
		End While

		Return success
	End Function


	Public Function GetDefaultBarPlotSettings() As BarPlotSettings
		Dim settings As BarPlotSettings

		settings = New BarPlotSettings()

		settings.width = 800
		settings.height = 600
		settings.autoBoundaries = true
		settings.yMax = 0
		settings.yMin = 0
		settings.autoPadding = true
		settings.xPadding = 0
		settings.yPadding = 0
		settings.title = "".ToCharArray()
		settings.yLabel = "".ToCharArray()
		settings.barPlotSeries = New BarPlotSeries (0 - 1){}
		settings.showGrid = true
		settings.gridColor = GetGray(0.1)
		settings.autoColor = true
		settings.grayscaleAutoColor = false
		settings.autoSpacing = true
		settings.groupSeparation = 0
		settings.barSeparation = 0
		settings.autoLabels = true
		settings.xLabels = New StringReference (0 - 1){}
		'settings.autoLabels = false;
		'        settings.xLabels = new StringReference [5];
		'        settings.xLabels[0] = CreateStringReference("may 20".toCharArray());
		'        settings.xLabels[1] = CreateStringReference("jun 20".toCharArray());
		'        settings.xLabels[2] = CreateStringReference("jul 20".toCharArray());
		'        settings.xLabels[3] = CreateStringReference("aug 20".toCharArray());
		'        settings.xLabels[4] = CreateStringReference("sep 20".toCharArray());
		settings.barBorder = false

		Return settings
	End Function


	Public Function GetDefaultBarPlotSeriesSettings() As BarPlotSeries
		Dim series As BarPlotSeries

		series = New BarPlotSeries()

		series.ys = New Double (0 - 1){}
		series.color = GetBlack()

		Return series
	End Function


	Public Function DrawBarPlot(width As Double, height As Double, ByRef ys As Double ()) As RGBABitmapImage
		Dim settings As BarPlotSettings
		Dim canvasReference As RGBABitmapImageReference

		settings = GetDefaultBarPlotSettings()

		settings.barPlotSeries = New BarPlotSeries (1 - 1){}
		settings.barPlotSeries(0) = GetDefaultBarPlotSeriesSettings()
		Erase settings.barPlotSeries(0).ys 
		settings.barPlotSeries(0).ys = ys
		canvasReference = New RGBABitmapImageReference()
		settings.width = width
		settings.height = height

		DrawBarPlotFromSettings(canvasReference, settings)

		Return canvasReference.image
	End Function


	Public Function DrawBarPlotFromSettings(ByRef canvasReference As RGBABitmapImageReference, ByRef settings As BarPlotSettings) As Boolean
		Dim xPadding, yPadding As Double
		Dim xPixelMin, yPixelMin, yPixelMax, xPixelMax As Double
		Dim xLengthPixels, yLengthPixels As Double
		Dim s, n, y, x, w, h, yMin, yMax, b, i, py, yValue As Double
		Dim colors As RGBA ()
		Dim ys, yGridPositions As Double ()
		Dim yTop, yBottom, ss, bs, yLength As Double
		Dim groupSeparation, barSeparation, barWidth, textwidth As Double
		Dim yLabels As StringArrayReference
		Dim yLabelPriorities As NumberArrayReference
		Dim occupied As Rectangle ()
		Dim nextRectangle As NumberReference
		Dim gridLabelColor, barColor As RGBA
		Dim label As Char ()
		Dim success As Boolean
		Dim canvas As RGBABitmapImage

		success = BarPlotSettingsIsValid(settings)

		If success

			canvas = CreateImage(settings.width, settings.height, GetWhite())

			ss = settings.barPlotSeries.Length
			gridLabelColor = GetGray(0.5)

			' padding
			If settings.autoPadding
				xPadding = Floor(GetDefaultPaddingPercentage()*ImageWidth(canvas))
				yPadding = Floor(GetDefaultPaddingPercentage()*ImageHeight(canvas))
			Else
				xPadding = settings.xPadding
				yPadding = settings.yPadding
			End If

			' Draw title
			Call DrawText(canvas, Floor(ImageWidth(canvas)/2 - GetTextWidth(settings.title)/2), Floor(yPadding/3), settings.title, GetBlack())
			Call DrawTextUpwards(canvas, 10, Floor(ImageHeight(canvas)/2 - GetTextWidth(settings.yLabel)/2), settings.yLabel, GetBlack())

			' min and max
			If settings.autoBoundaries
				If ss >= 1
					yMax = GetMaximum(settings.barPlotSeries(0).ys)
					yMin = Min(0, GetMinimum(settings.barPlotSeries(0).ys))

					s = 0
					While s < ss
						yMax = Max(yMax, GetMaximum(settings.barPlotSeries(s).ys))
						yMin = Min(yMin, GetMinimum(settings.barPlotSeries(s).ys))
						s = s + 1
					End While
				Else
					yMax = 10
					yMin = 0
				End If
			Else
				yMin = settings.yMin
				yMax = settings.yMax
			End If
			yLength = yMax - yMin

			' boundaries
			xPixelMin = xPadding
			yPixelMin = yPadding
			xPixelMax = ImageWidth(canvas) - xPadding
			yPixelMax = ImageHeight(canvas) - yPadding
			xLengthPixels = xPixelMax - xPixelMin
			yLengthPixels = yPixelMax - yPixelMin

			' Draw boundary.
			Call DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor)

			' Draw grid lines.
			yLabels = New StringArrayReference()
			yLabelPriorities = New NumberArrayReference()
			yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities)

			If settings.showGrid
				' Y-grid
				i = 0
				While i < yGridPositions.Length
					y = yGridPositions(i)
					py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax)
					Call DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor)
					i = i + 1
				End While
			End If

			' Draw origin.
			If yMin < 0 And yMax > 0
				py = MapYCoordinate(0, yMin, yMax, yPixelMin, yPixelMax)
				Call DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor)
			End If

			' Labels
			occupied = New Rectangle (yLabels.stringArray.Length - 1){}
			i = 0
			While i < occupied.Length
				occupied(i) = CreateRectangle(0, 0, 0, 0)
				i = i + 1
			End While
			nextRectangle = CreateNumberReference(0)

			i = 1
			While i <= 5
				Call DrawYLabelsForPriority(i, yMin, xPixelMin, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, true)
				i = i + 1
			End While

			' Draw bars.
			If settings.autoColor
				If Not settings.grayscaleAutoColor
					colors = Get8HighContrastColors()
				Else
					colors = New RGBA (ss - 1){}
					If ss > 1
						i = 0
						While i < ss
							colors(i) = GetGray(0.7 - (i/ss)*0.7)
							i = i + 1
						End While
					Else
						colors(0) = GetGray(0.5)
					End If
				End If
			Else
				colors = New RGBA (0 - 1){}
			End If

			' distances
			bs = settings.barPlotSeries(0).ys.Length

			If settings.autoSpacing
				groupSeparation = ImageWidth(canvas)*0.05
				barSeparation = ImageWidth(canvas)*0.005
			Else
				groupSeparation = settings.groupSeparation
				barSeparation = settings.barSeparation
			End If

			barWidth = (xLengthPixels - groupSeparation*(bs - 1) - barSeparation*(bs*(ss - 1)))/(bs*ss)

			' Draw bars.
			b = 0
			n = 0
			While n < bs
				s = 0
				While s < ss
					ys = settings.barPlotSeries(s).ys

					yValue = ys(n)

					yBottom = MapYCoordinate(yValue, yMin, yMax, yPixelMin, yPixelMax)
					yTop = MapYCoordinate(0, yMin, yMax, yPixelMin, yPixelMax)

					x = xPixelMin + n*(groupSeparation + ss*barWidth) + s*(barWidth) + b*barSeparation
					w = barWidth

					If yValue >= 0
						y = yBottom
						h = yTop - y
					Else
						y = yTop
						h = yBottom - yTop
					End If

					' Cut at boundaries.
					If y < yPixelMin And y + h > yPixelMax
						y = yPixelMin
						h = yPixelMax - yPixelMin
					ElseIf y < yPixelMin
						y = yPixelMin
						If yValue >= 0
							h = yTop - y
						Else
							h = yBottom - y
						End If
					ElseIf y + h > yPixelMax
						h = yPixelMax - y
					End If

					' Get color
					If settings.autoColor
						barColor = colors(s)
					Else
						barColor = settings.barPlotSeries(s).color
					End If

					' Draw
					If settings.barBorder
						Call DrawFilledRectangleWithBorder(canvas, Roundx(x), Roundx(y), Roundx(w), Roundx(h), GetBlack(), barColor)
					Else
						Call DrawFilledRectangle(canvas, Roundx(x), Roundx(y), Roundx(w), Roundx(h), barColor)
					End If

					b = b + 1
					s = s + 1
				End While
				b = b - 1
				n = n + 1
			End While

			' x-labels
			n = 0
			While n < bs
				If settings.autoLabels
					label = CreateStringDecimalFromNumber(n + 1)
				Else
					label = settings.xLabels(n).stringx
				End If

				textwidth = GetTextWidth(label)

				x = xPixelMin + (n + 0.5)*(ss*barWidth + (ss - 1)*barSeparation) + n*groupSeparation - textwidth/2

				Call DrawText(canvas, Floor(x), ImageHeight(canvas) - yPadding + 20, label, gridLabelColor)

				b = b + 1
				n = n + 1
			End While

			canvasReference.image = canvas
		End If

		Return success
	End Function


	Public Function BarPlotSettingsIsValid(ByRef settings As BarPlotSettings) As Boolean
		Dim success, lengthSet As Boolean
		Dim series As BarPlotSeries
		Dim i, width, height, length As Double

		success = true

		' Check series lengths.
		lengthSet = false
		length = 0
		i = 0
		While i < settings.barPlotSeries.Length
			series = settings.barPlotSeries(i)

			If Not lengthSet
				length = series.ys.Length
				lengthSet = true
			ElseIf length <> series.ys.Length
				success = false
			End If
			i = i + 1
		End While

		' Check bounds.
		If Not settings.autoBoundaries
			If settings.yMin >= settings.yMax
				success = false
			End If
		End If

		' Check padding.
		If Not settings.autoPadding
			If 2*settings.xPadding >= settings.width
				success = false
			End If
			If 2*settings.yPadding >= settings.height
				success = false
			End If
		End If

		' Check width and height.
		If settings.width < 0
			success = false
		End If
		If settings.height < 0
			success = false
		End If

		' Check spacing
		If Not settings.autoSpacing
			If settings.groupSeparation < 0
				success = false
			End If
			If settings.barSeparation < 0
				success = false
			End If
		End If

		Return success
	End Function


	Public Function GetMinimum(ByRef data As Double ()) As Double
		Dim i, minimum As Double

		minimum = data(0)
		i = 0
		While i < data.Length
			minimum = Min(minimum, data(i))
			i = i + 1
		End While

		Return minimum
	End Function


	Public Function GetMaximum(ByRef data As Double ()) As Double
		Dim i, maximum As Double

		maximum = data(0)
		i = 0
		While i < data.Length
			maximum = Max(maximum, data(i))
			i = i + 1
		End While

		Return maximum
	End Function


	Public Function RoundToDigits(element As Double, digitsAfterPoint As Double) As Double
		Return Roundx(element*10 ^ digitsAfterPoint)/10 ^ digitsAfterPoint
	End Function


	Public Function test() As Double
		Dim scatterPlotSettings As ScatterPlotSettings
		Dim z As Double
		Dim gridlines As Double ()
		Dim failures As NumberReference
		Dim labels As StringArrayReference
		Dim labelPriorities As NumberArrayReference
		Dim imageReference As RGBABitmapImageReference
		Dim xs, ys As Double ()

		failures = CreateNumberReference(0)

		imageReference = CreateRGBABitmapImageReference()

		scatterPlotSettings = GetDefaultScatterPlotSettings()

		labels = New StringArrayReference()
		labelPriorities = New NumberArrayReference()

		z = 10
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 11, failures)

		z = 9
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 19, failures)

		z = 8
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 17, failures)

		z = 7
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 15, failures)

		z = 6
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 13, failures)

		z = 5
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 21, failures)

		z = 4
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 17, failures)

		z = 3
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 31, failures)

		z = 2
		gridlines = ComputeGridLinePositions(-z/2, z/2, labels, labelPriorities)
		Call AssertEquals(gridlines.Length, 21, failures)

		xs = New Double (5 - 1){}
		xs(0) = -2
		xs(1) = -1
		xs(2) = 0
		xs(3) = 1
		xs(4) = 2
		ys = New Double (5 - 1){}
		ys(0) = 2
		ys(1) = -1
		ys(2) = -2
		ys(3) = -1
		ys(4) = 2
		Call DrawScatterPlot(imageReference, 800, 600, xs, ys)

		imageReference.image = DrawBarPlot(800, 600, ys)

		Return failures.numberValue
	End Function


	Public Function GetBlack() As RGBA
		Dim black As RGBA
		black = New RGBA()
		black.a = 1
		black.r = 0
		black.g = 0
		black.b = 0
		Return black
	End Function


	Public Function GetWhite() As RGBA
		Dim white As RGBA
		white = New RGBA()
		white.a = 1
		white.r = 1
		white.g = 1
		white.b = 1
		Return white
	End Function


	Public Function GetTransparent() As RGBA
		Dim transparent As RGBA
		transparent = New RGBA()
		transparent.a = 0
		transparent.r = 0
		transparent.g = 0
		transparent.b = 0
		Return transparent
	End Function


	Public Function GetGray(percentage As Double) As RGBA
		Dim black As RGBA
		black = New RGBA()
		black.a = 1
		black.r = 1 - percentage
		black.g = 1 - percentage
		black.b = 1 - percentage
		Return black
	End Function


	Public Function CreateRGBColor(r As Double, g As Double, b As Double) As RGBA
		Dim color As RGBA
		color = New RGBA()
		color.a = 1
		color.r = r
		color.g = g
		color.b = b
		Return color
	End Function


	Public Function CreateRGBAColor(r As Double, g As Double, b As Double, a As Double) As RGBA
		Dim color As RGBA
		color = New RGBA()
		color.a = a
		color.r = r
		color.g = g
		color.b = b
		Return color
	End Function


	Public Function CreateImage(w As Double, h As Double, ByRef color As RGBA) As RGBABitmapImage
		Dim image As RGBABitmapImage
		Dim i, j As Double

		image = New RGBABitmapImage()
		image.x = New RGBABitmap (w - 1){}
		i = 0
		While i < w
			image.x(i) = New RGBABitmap()
			image.x(i).y = New RGBA (h - 1){}
			j = 0
			While j < h
				image.x(i).y(j) = New RGBA()
				Call SetPixel(image, i, j, color)
				j = j + 1
			End While
			i = i + 1
		End While

		Return image
	End Function


	Public Sub DeleteImage(ByRef image As RGBABitmapImage)
		Dim i, j, w, h As Double

		w = ImageWidth(image)
		h = ImageHeight(image)

		i = 0
		While i < w
			j = 0
			While j < h
				image.x(i).y(j) = Nothing
				j = j + 1
			End While
			image.x(i) = Nothing
			i = i + 1
		End While
		image = Nothing
	End Sub


	Public Function ImageWidth(ByRef image As RGBABitmapImage) As Double
		Return image.x.Length
	End Function


	Public Function ImageHeight(ByRef image As RGBABitmapImage) As Double
		Dim height As Double

		If ImageWidth(image) = 0
			height = 0
		Else
			height = image.x(0).y.Length
		End If

		Return height
	End Function


	Public Sub SetPixel(ByRef image As RGBABitmapImage, x As Double, y As Double, ByRef color As RGBA)
		If x >= 0 And x < ImageWidth(image) And y >= 0 And y < ImageHeight(image)
			image.x(x).y(y).a = color.a
			image.x(x).y(y).r = color.r
			image.x(x).y(y).g = color.g
			image.x(x).y(y).b = color.b
		End If
	End Sub


	Public Sub DrawPixel(ByRef image As RGBABitmapImage, x As Double, y As Double, ByRef color As RGBA)
		Dim ra, ga, ba, aa As Double
		Dim rb, gb, bb, ab As Double
		Dim ro, go, bo, ao As Double

		If x >= 0 And x < ImageWidth(image) And y >= 0 And y < ImageHeight(image)
			ra = color.r
			ga = color.g
			ba = color.b
			aa = color.a

			rb = image.x(x).y(y).r
			gb = image.x(x).y(y).g
			bb = image.x(x).y(y).b
			ab = image.x(x).y(y).a

			ao = CombineAlpha(aa, ab)

			ro = AlphaBlend(ra, aa, rb, ab, ao)
			go = AlphaBlend(ga, aa, gb, ab, ao)
			bo = AlphaBlend(ba, aa, bb, ab, ao)

			image.x(x).y(y).r = ro
			image.x(x).y(y).g = go
			image.x(x).y(y).b = bo
			image.x(x).y(y).a = ao
		End If
	End Sub


	Public Function CombineAlpha(asx As Double, ad As Double) As Double
		Return asx + ad*(1 - asx)
	End Function


	Public Function AlphaBlend(cs As Double, asx As Double, cd As Double, ad As Double, ao As Double) As Double
		Return (cs*asx + cd*ad*(1 - asx))/ao
	End Function


	Public Sub DrawHorizontalLine1px(ByRef image As RGBABitmapImage, x As Double, y As Double, length As Double, ByRef color As RGBA)
		Dim i As Double

		i = 0
		While i < length
			Call DrawPixel(image, x + i, y, color)
			i = i + 1
		End While
	End Sub


	Public Sub DrawVerticalLine1px(ByRef image As RGBABitmapImage, x As Double, y As Double, height As Double, ByRef color As RGBA)
		Dim i As Double

		i = 0
		While i < height
			Call DrawPixel(image, x, y + i, color)
			i = i + 1
		End While
	End Sub


	Public Sub DrawRectangle1px(ByRef image As RGBABitmapImage, x As Double, y As Double, width As Double, height As Double, ByRef color As RGBA)
		Call DrawHorizontalLine1px(image, x, y, width + 1, color)
		Call DrawVerticalLine1px(image, x, y + 1, height + 1 - 1, color)
		Call DrawVerticalLine1px(image, x + width, y + 1, height + 1 - 1, color)
		Call DrawHorizontalLine1px(image, x + 1, y + height, width + 1 - 2, color)
	End Sub


	Public Sub DrawImageOnImage(ByRef dst As RGBABitmapImage, ByRef src As RGBABitmapImage, topx As Double, topy As Double)
		Dim y, x As Double

		y = 0
		While y < ImageHeight(src)
			x = 0
			While x < ImageWidth(src)
				If topx + x >= 0 And topx + x < ImageWidth(dst) And topy + y >= 0 And topy + y < ImageHeight(dst)
					Call DrawPixel(dst, topx + x, topy + y, src.x(x).y(y))
				End If
				x = x + 1
			End While
			y = y + 1
		End While
	End Sub


	Public Sub DrawLine1px(ByRef image As RGBABitmapImage, x0 As Double, y0 As Double, x1 As Double, y1 As Double, ByRef color As RGBA)
		Call XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color)
	End Sub


	Public Sub XiaolinWusLineAlgorithm(ByRef image As RGBABitmapImage, x0 As Double, y0 As Double, x1 As Double, y1 As Double, ByRef color As RGBA)
		Dim steep As Boolean
		Dim x, t, dx, dy, g, xEnd, yEnd, xGap, xpxl1, ypxl1, intery, xpxl2, ypxl2, olda As Double

		olda = color.a

		steep = Abs(y1 - y0) > Abs(x1 - x0)

		If steep
			t = x0
			x0 = y0
			y0 = t

			t = x1
			x1 = y1
			y1 = t
		End If
		If x0 > x1
			t = x0
			x0 = x1
			x1 = t

			t = y0
			y0 = y1
			y1 = t
		End If

		dx = x1 - x0
		dy = y1 - y0
		g = dy/dx

		If dx = 0
			g = 1
		End If

		xEnd = Roundx(x0)
		yEnd = y0 + g*(xEnd - x0)
		xGap = OneMinusFractionalPart(x0 + 0.5)
		xpxl1 = xEnd
		ypxl1 = Floor(yEnd)
		If steep
			Call DrawPixel(image, ypxl1, xpxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
			Call DrawPixel(image, ypxl1 + 1, xpxl1, SetBrightness(color, FractionalPart(yEnd)*xGap))
		Else
			Call DrawPixel(image, xpxl1, ypxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
			Call DrawPixel(image, xpxl1, ypxl1 + 1, SetBrightness(color, FractionalPart(yEnd)*xGap))
		End If
		intery = yEnd + g

		xEnd = Roundx(x1)
		yEnd = y1 + g*(xEnd - x1)
		xGap = FractionalPart(x1 + 0.5)
		xpxl2 = xEnd
		ypxl2 = Floor(yEnd)
		If steep
			Call DrawPixel(image, ypxl2, xpxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
			Call DrawPixel(image, ypxl2 + 1, xpxl2, SetBrightness(color, FractionalPart(yEnd)*xGap))
		Else
			Call DrawPixel(image, xpxl2, ypxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap))
			Call DrawPixel(image, xpxl2, ypxl2 + 1, SetBrightness(color, FractionalPart(yEnd)*xGap))
		End If

		If steep
			x = xpxl1 + 1
			While x <= xpxl2 - 1
				Call DrawPixel(image, Floor(intery), x, SetBrightness(color, OneMinusFractionalPart(intery)))
				Call DrawPixel(image, Floor(intery) + 1, x, SetBrightness(color, FractionalPart(intery)))
				intery = intery + g
				x = x + 1
			End While
		Else
			x = xpxl1 + 1
			While x <= xpxl2 - 1
				Call DrawPixel(image, x, Floor(intery), SetBrightness(color, OneMinusFractionalPart(intery)))
				Call DrawPixel(image, x, Floor(intery) + 1, SetBrightness(color, FractionalPart(intery)))
				intery = intery + g
				x = x + 1
			End While
		End If

		color.a = olda
	End Sub


	Public Function OneMinusFractionalPart(x As Double) As Double
		Return 1 - FractionalPart(x)
	End Function


	Public Function FractionalPart(x As Double) As Double
		Return x - Floor(x)
	End Function


	Public Function SetBrightness(ByRef color As RGBA, newBrightness As Double) As RGBA
		color.a = newBrightness
		Return color
	End Function


	Public Sub DrawQuadraticBezierCurve(ByRef image As RGBABitmapImage, x0 As Double, y0 As Double, cx As Double, cy As Double, x1 As Double, y1 As Double, ByRef color As RGBA)
		Dim t, dt, dx, dy As Double
		Dim xs, ys, xe, ye As NumberReference

		dx = Abs(x0 - x1)
		dy = Abs(y0 - y1)

		dt = 1/Sqrt(dx ^ 2 + dy ^ 2)

		xs = New NumberReference()
		ys = New NumberReference()
		xe = New NumberReference()
		ye = New NumberReference()

		Call QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, 0, xs, ys)
		t = dt
		While t <= 1
			Call QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, xe, ye)
			Call DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color)
			xs.numberValue = xe.numberValue
			ys.numberValue = ye.numberValue
			t = t + dt
		End While

		xs = Nothing
		ys = Nothing
		xe = Nothing
		ye = Nothing
	End Sub


	Public Sub QuadraticBezierPoint(x0 As Double, y0 As Double, cx As Double, cy As Double, x1 As Double, y1 As Double, t As Double, ByRef x As NumberReference, ByRef y As NumberReference)
		x.numberValue = (1 - t) ^ 2*x0 + (1 - t)*2*t*cx + t ^ 2*x1
		y.numberValue = (1 - t) ^ 2*y0 + (1 - t)*2*t*cy + t ^ 2*y1
	End Sub


	Public Sub DrawCubicBezierCurve(ByRef image As RGBABitmapImage, x0 As Double, y0 As Double, c0x As Double, c0y As Double, c1x As Double, c1y As Double, x1 As Double, y1 As Double, ByRef color As RGBA)
		Dim t, dt, dx, dy As Double
		Dim xs, ys, xe, ye As NumberReference

		dx = Abs(x0 - x1)
		dy = Abs(y0 - y1)

		dt = 1/Sqrt(dx ^ 2 + dy ^ 2)

		xs = New NumberReference()
		ys = New NumberReference()
		xe = New NumberReference()
		ye = New NumberReference()

		Call CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, 0, xs, ys)
		t = dt
		While t <= 1
			Call CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, xe, ye)
			Call DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color)
			xs.numberValue = xe.numberValue
			ys.numberValue = ye.numberValue
			t = t + dt
		End While

		xs = Nothing
		ys = Nothing
		xe = Nothing
		ye = Nothing
	End Sub


	Public Sub CubicBezierPoint(x0 As Double, y0 As Double, c0x As Double, c0y As Double, c1x As Double, c1y As Double, x1 As Double, y1 As Double, t As Double, ByRef x As NumberReference, ByRef y As NumberReference)
		x.numberValue = (1 - t) ^ 3*x0 + (1 - t) ^ 2*3*t*c0x + (1 - t)*3*t ^ 2*c1x + t ^ 3*x1

		y.numberValue = (1 - t) ^ 3*y0 + (1 - t) ^ 2*3*t*c0y + (1 - t)*3*t ^ 2*c1y + t ^ 3*y1
	End Sub


	Public Function CopyImage(ByRef image As RGBABitmapImage) As RGBABitmapImage
		Dim copy As RGBABitmapImage
		Dim i, j As Double

		copy = CreateImage(ImageWidth(image), ImageHeight(image), GetTransparent())

		i = 0
		While i < ImageWidth(image)
			j = 0
			While j < ImageHeight(image)
				Call SetPixel(copy, i, j, image.x(i).y(j))
				j = j + 1
			End While
			i = i + 1
		End While

		Return copy
	End Function


	Public Function GetImagePixel(ByRef image As RGBABitmapImage, x As Double, y As Double) As RGBA
		Return image.x(x).y(y)
	End Function


	Public Sub HorizontalFlip(ByRef img As RGBABitmapImage)
		Dim y, x As Double
		Dim tmp As Double
		Dim c1, c2 As RGBA

		y = 0
		While y < ImageHeight(img)
			x = 0
			While x < ImageWidth(img)/2
				c1 = img.x(x).y(y)
				c2 = img.x(ImageWidth(img) - 1 - x).y(y)

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
				x = x + 1
			End While
			y = y + 1
		End While
	End Sub


	Public Sub DrawFilledRectangle(ByRef image As RGBABitmapImage, x As Double, y As Double, w As Double, h As Double, ByRef color As RGBA)
		Dim i, j As Double

		i = 0
		While i < w
			j = 0
			While j < h
				Call SetPixel(image, x + i, y + j, color)
				j = j + 1
			End While
			i = i + 1
		End While
	End Sub


	Public Function RotateAntiClockwise90Degrees(ByRef image As RGBABitmapImage) As RGBABitmapImage
		Dim rotated As RGBABitmapImage
		Dim x, y As Double

		rotated = CreateImage(ImageHeight(image), ImageWidth(image), GetBlack())

		y = 0
		While y < ImageHeight(image)
			x = 0
			While x < ImageWidth(image)
				Call SetPixel(rotated, y, ImageWidth(image) - 1 - x, GetImagePixel(image, x, y))
				x = x + 1
			End While
			y = y + 1
		End While

		Return rotated
	End Function


	Public Sub DrawCircle(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, radius As Double, ByRef color As RGBA)
		Call DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color)
	End Sub


	Public Sub BresenhamsCircleDrawingAlgorithm(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, radius As Double, ByRef color As RGBA)
		Dim x, y, delta As Double

		y = radius
		x = 0

		delta = 3 - 2*radius
		
		While y >= x
			Call DrawLine1px(canvas, xCenter + x, yCenter + y, xCenter + x, yCenter + y, color)
			Call DrawLine1px(canvas, xCenter + x, yCenter - y, xCenter + x, yCenter - y, color)
			Call DrawLine1px(canvas, xCenter - x, yCenter + y, xCenter - x, yCenter + y, color)
			Call DrawLine1px(canvas, xCenter - x, yCenter - y, xCenter - x, yCenter - y, color)

			Call DrawLine1px(canvas, xCenter - y, yCenter + x, xCenter - y, yCenter + x, color)
			Call DrawLine1px(canvas, xCenter - y, yCenter - x, xCenter - y, yCenter - x, color)
			Call DrawLine1px(canvas, xCenter + y, yCenter + x, xCenter + y, yCenter + x, color)
			Call DrawLine1px(canvas, xCenter + y, yCenter - x, xCenter + y, yCenter - x, color)

			If delta < 0
				delta = delta + 4*x + 6
			Else
				delta = delta + 4*(x - y) + 10
				y = y - 1
			End If
			x = x + 1
		End While
	End Sub


	Public Sub DrawCircleMidpointAlgorithm(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, radius As Double, ByRef color As RGBA)
		Dim d, x, y As Double

		d = Floor((5 - radius*4)/4)
		x = 0
		y = radius

		
		While x <= y
			Call DrawPixel(canvas, xCenter + x, yCenter + y, color)
			Call DrawPixel(canvas, xCenter + x, yCenter - y, color)
			Call DrawPixel(canvas, xCenter - x, yCenter + y, color)
			Call DrawPixel(canvas, xCenter - x, yCenter - y, color)
			Call DrawPixel(canvas, xCenter + y, yCenter + x, color)
			Call DrawPixel(canvas, xCenter + y, yCenter - x, color)
			Call DrawPixel(canvas, xCenter - y, yCenter + x, color)
			Call DrawPixel(canvas, xCenter - y, yCenter - x, color)

			If d < 0
				d = d + 2*x + 1
			Else
				d = d + 2*(x - y) + 1
				y = y - 1
			End If
			x = x + 1
		End While
	End Sub


	Public Sub DrawCircleBasicAlgorithm(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, radius As Double, ByRef color As RGBA)
		Dim pixels, a, da, dx, dy As Double

		' Place the circle in the center of the pixel.
		xCenter = Floor(xCenter) + 0.5
		yCenter = Floor(yCenter) + 0.5

		pixels = 2*Pi*radius

		' Below a radius of 10 pixels, over-compensate to get a smoother circle.
		If radius < 10
			pixels = pixels*10
		End If

		da = 2*Pi/pixels

		a = 0
		While a < 2*Pi
			dx = Cos(a)*radius
			dy = Sin(a)*radius

			' Floor to get the pixel coordinate.
			Call DrawPixel(canvas, Floor(xCenter + dx), Floor(yCenter + dy), color)
			a = a + da
		End While
	End Sub


	Public Sub DrawFilledCircle(ByRef canvas As RGBABitmapImage, x As Double, y As Double, r As Double, ByRef color As RGBA)
		Call DrawFilledCircleBasicAlgorithm(canvas, x, y, r, color)
	End Sub


	Public Sub DrawFilledCircleMidpointAlgorithm(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, radius As Double, ByRef color As RGBA)
		Dim d, x, y As Double

		d = Floor((5 - radius*4)/4)
		x = 0
		y = radius

		
		While x <= y
			Call DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter + y, xCenter - x, yCenter + y, color)
			Call DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter - y, xCenter - x, yCenter - y, color)
			Call DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter + x, xCenter - y, yCenter + x, color)
			Call DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter - x, xCenter - y, yCenter - x, color)

			If d < 0
				d = d + 2*x + 1
			Else
				d = d + 2*(x - y) + 1
				y = y - 1
			End If
			x = x + 1
		End While
	End Sub


	Public Sub DrawFilledCircleBasicAlgorithm(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, radius As Double, ByRef color As RGBA)
		Dim pixels, a, da, dx, dy As Double

		' Place the circle in the center of the pixel.
		xCenter = Floor(xCenter) + 0.5
		yCenter = Floor(yCenter) + 0.5

		pixels = 2*Pi*radius

		' Below a radius of 10 pixels, over-compensate to get a smoother circle.
		If radius < 10
			pixels = pixels*10
		End If

		da = 2*Pi/pixels

		' Draw lines for a half-circle to fill an entire circle.
		a = 0
		While a < Pi
			dx = Cos(a)*radius
			dy = Sin(a)*radius

			' Floor to get the pixel coordinate.
			Call DrawVerticalLine1px(canvas, Floor(xCenter - dx), Floor(yCenter - dy), Floor(2*dy) + 1, color)
			a = a + da
		End While
	End Sub


	Public Sub DrawTriangle(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, height As Double, ByRef color As RGBA)
		Dim x1, y1, x2, y2, x3, y3 As Double

		x1 = Floor(xCenter + 0.5)
		y1 = Floor(Floor(yCenter + 0.5) - height)
		x2 = x1 - 2*height*Tan(Pi/6)
		y2 = Floor(y1 + 2*height)
		x3 = x1 + 2*height*Tan(Pi/6)
		y3 = Floor(y1 + 2*height)

		Call DrawLine1px(canvas, x1, y1, x2, y2, color)
		Call DrawLine1px(canvas, x1, y1, x3, y3, color)
		Call DrawLine1px(canvas, x2, y2, x3, y3, color)
	End Sub


	Public Sub DrawFilledTriangle(ByRef canvas As RGBABitmapImage, xCenter As Double, yCenter As Double, height As Double, ByRef color As RGBA)
		Dim i, offset, x1, y1 As Double

		x1 = Floor(xCenter + 0.5)
		y1 = Floor(Floor(yCenter + 0.5) - height)

		i = 0
		While i <= 2*height
			offset = Floor(i*Tan(Pi/6))
			Call DrawHorizontalLine1px(canvas, x1 - offset, y1 + i, 2*offset, color)
			i = i + 1
		End While
	End Sub


	Public Sub DrawLine(ByRef canvas As RGBABitmapImage, x1 As Double, y1 As Double, x2 As Double, y2 As Double, thickness As Double, ByRef color As RGBA)
		Call DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color)
	End Sub


	Public Sub DrawLineBresenhamsAlgorithmThick(ByRef canvas As RGBABitmapImage, x1 As Double, y1 As Double, x2 As Double, y2 As Double, thickness As Double, ByRef color As RGBA)
		Dim x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r As Double

		dx = x2 - x1
		dy = y2 - y1

		incX = Signx(dx)
		incY = Signx(dy)

		dx = Abs(dx)
		dy = Abs(dy)

		If dx > dy
			pdx = incX
			pdy = 0
			es = dy
			el = dx
		Else
			pdx = 0
			pdy = incY
			es = dx
			el = dy
		End If

		x = x1
		y = y1
		err = el/2

		If thickness >= 3
			r = thickness/2
			Call DrawCircle(canvas, x, y, r, color)
		ElseIf Floor(thickness) = 2
			Call DrawFilledRectangle(canvas, x, y, 2, 2, color)
		ElseIf Floor(thickness) = 1
			Call DrawPixel(canvas, x, y, color)
		End If

		t = 0
		While t < el
			err = err - es
			If err < 0
				err = err + el
				x = x + incX
				y = y + incY
			Else
				x = x + pdx
				y = y + pdy
			End If

			If thickness >= 3
				r = thickness/2
				Call DrawCircle(canvas, x, y, r, color)
			ElseIf Floor(thickness) = 2
				Call DrawFilledRectangle(canvas, x, y, 2, 2, color)
			ElseIf Floor(thickness) = 1
				Call DrawPixel(canvas, x, y, color)
			End If
			t = t + 1
		End While
	End Sub


	Public Sub DrawLineBresenhamsAlgorithm(ByRef canvas As RGBABitmapImage, x1 As Double, y1 As Double, x2 As Double, y2 As Double, ByRef color As RGBA)
		Dim x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t As Double

		dx = x2 - x1
		dy = y2 - y1

		incX = Signx(dx)
		incY = Signx(dy)

		dx = Abs(dx)
		dy = Abs(dy)

		If dx > dy
			pdx = incX
			pdy = 0
			es = dy
			el = dx
		Else
			pdx = 0
			pdy = incY
			es = dx
			el = dy
		End If

		x = x1
		y = y1
		err = el/2
		Call DrawPixel(canvas, x, y, color)

		t = 0
		While t < el
			err = err - es
			If err < 0
				err = err + el
				x = x + incX
				y = y + incY
			Else
				x = x + pdx
				y = y + pdy
			End If

			Call DrawPixel(canvas, x, y, color)
			t = t + 1
		End While
	End Sub


	Public Sub DrawLineBresenhamsAlgorithmThickPatterned(ByRef canvas As RGBABitmapImage, x1 As Double, y1 As Double, x2 As Double, y2 As Double, thickness As Double, ByRef pattern As Boolean (), ByRef offset As NumberReference, ByRef color As RGBA)
		Dim x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r As Double

		dx = x2 - x1
		dy = y2 - y1

		incX = Signx(dx)
		incY = Signx(dy)

		dx = Abs(dx)
		dy = Abs(dy)

		If dx > dy
			pdx = incX
			pdy = 0
			es = dy
			el = dx
		Else
			pdx = 0
			pdy = incY
			es = dx
			el = dy
		End If

		x = x1
		y = y1
		err = el/2

		offset.numberValue = (offset.numberValue + 1) Mod (pattern.Length*thickness)

		If pattern(Floor(offset.numberValue/thickness))
			If thickness >= 3
				r = thickness/2
				Call DrawCircle(canvas, x, y, r, color)
			ElseIf Floor(thickness) = 2
				Call DrawFilledRectangle(canvas, x, y, 2, 2, color)
			ElseIf Floor(thickness) = 1
				Call DrawPixel(canvas, x, y, color)
			End If
		End If

		t = 0
		While t < el
			err = err - es
			If err < 0
				err = err + el
				x = x + incX
				y = y + incY
			Else
				x = x + pdx
				y = y + pdy
			End If

			offset.numberValue = (offset.numberValue + 1) Mod (pattern.Length*thickness)

			If pattern(Floor(offset.numberValue/thickness))
				If thickness >= 3
					r = thickness/2
					Call DrawCircle(canvas, x, y, r, color)
				ElseIf Floor(thickness) = 2
					Call DrawFilledRectangle(canvas, x, y, 2, 2, color)
				ElseIf Floor(thickness) = 1
					Call DrawPixel(canvas, x, y, color)
				End If
			End If
			t = t + 1
		End While
	End Sub


	Public Function GetLinePattern5() As Boolean ()
		Dim pattern As Boolean ()

		pattern = New Boolean (19 - 1){}

		pattern(0) = true
		pattern(1) = true
		pattern(2) = true
		pattern(3) = true
		pattern(4) = true
		pattern(5) = true
		pattern(6) = true
		pattern(7) = true
		pattern(8) = true
		pattern(9) = true
		pattern(10) = false
		pattern(11) = false
		pattern(12) = false
		pattern(13) = true
		pattern(14) = true
		pattern(15) = true
		pattern(16) = false
		pattern(17) = false
		pattern(18) = false

		Return pattern
	End Function


	Public Function GetLinePattern4() As Boolean ()
		Dim pattern As Boolean ()

		pattern = New Boolean (13 - 1){}

		pattern(0) = true
		pattern(1) = true
		pattern(2) = true
		pattern(3) = true
		pattern(4) = true
		pattern(5) = true
		pattern(6) = true
		pattern(7) = true
		pattern(8) = true
		pattern(9) = true
		pattern(10) = false
		pattern(11) = false
		pattern(12) = false

		Return pattern
	End Function


	Public Function GetLinePattern3() As Boolean ()
		Dim pattern As Boolean ()

		pattern = New Boolean (13 - 1){}

		pattern(0) = true
		pattern(1) = true
		pattern(2) = true
		pattern(3) = true
		pattern(4) = true
		pattern(5) = true
		pattern(6) = false
		pattern(7) = false
		pattern(8) = false
		pattern(9) = true
		pattern(10) = true
		pattern(11) = false
		pattern(12) = false

		Return pattern
	End Function


	Public Function GetLinePattern2() As Boolean ()
		Dim pattern As Boolean ()

		pattern = New Boolean (4 - 1){}

		pattern(0) = true
		pattern(1) = true
		pattern(2) = false
		pattern(3) = false

		Return pattern
	End Function


	Public Function GetLinePattern1() As Boolean ()
		Dim pattern As Boolean ()

		pattern = New Boolean (8 - 1){}

		pattern(0) = true
		pattern(1) = true
		pattern(2) = true
		pattern(3) = true
		pattern(4) = true
		pattern(5) = false
		pattern(6) = false
		pattern(7) = false

		Return pattern
	End Function


	Public Function Blur(ByRef src As RGBABitmapImage, pixels As Double) As RGBABitmapImage
		Dim dst As RGBABitmapImage
		Dim x, y, w, h As Double

		w = ImageWidth(src)
		h = ImageHeight(src)
		dst = CreateImage(w, h, GetTransparent())

		x = 0
		While x < w
			y = 0
			While y < h
				Call SetPixel(dst, x, y, CreateBlurForPoint(src, x, y, pixels))
				y = y + 1
			End While
			x = x + 1
		End While

		Return dst
	End Function


	Public Function CreateBlurForPoint(ByRef src As RGBABitmapImage, x As Double, y As Double, pixels As Double) As RGBA
		Dim rgba As RGBA
		Dim i, j, countColor, countTransparent As Double
		Dim fromx, toxx, fromy, toy As Double
		Dim w, h As Double
		Dim alpha As Double

		w = src.x.Length
		h = src.x(0).y.Length

		rgba = New RGBA()
		rgba.r = 0
		rgba.g = 0
		rgba.b = 0
		rgba.a = 0

		fromx = x - pixels
		fromx = Max(fromx, 0)

		toxx = x + pixels
		toxx = Min(toxx, w - 1)

		fromy = y - pixels
		fromy = Max(fromy, 0)

		toy = y + pixels
		toy = Min(toy, h - 1)

		countColor = 0
		countTransparent = 0
		i = fromx
		While i < toxx
			j = fromy
			While j < toy
				alpha = src.x(i).y(j).a
				If alpha > 0
					rgba.r = rgba.r + src.x(i).y(j).r
					rgba.g = rgba.g + src.x(i).y(j).g
					rgba.b = rgba.b + src.x(i).y(j).b
					countColor = countColor + 1
				End If
				rgba.a = rgba.a + alpha
				countTransparent = countTransparent + 1
				j = j + 1
			End While
			i = i + 1
		End While

		If countColor > 0
			rgba.r = rgba.r/countColor
			rgba.g = rgba.g/countColor
			rgba.b = rgba.b/countColor
		Else
			rgba.r = 0
			rgba.g = 0
			rgba.b = 0
		End If

		If countTransparent > 0
			rgba.a = rgba.a/countTransparent
		Else
			rgba.a = 0
		End If

		Return rgba
	End Function


	Public Function CreateStringScientificNotationDecimalFromNumber(decimalx As Double) As Char ()
		Dim mantissaReference, exponentReference As StringReference
		Dim multiplier, inc As Double
		Dim exponent As Double
		Dim done, isPositive As Boolean
		Dim result As Char ()

		mantissaReference = New StringReference()
		exponentReference = New StringReference()
		result = New Char (0 - 1){}
		done = false
		exponent = 0

		If decimalx < 0
			isPositive = false
			decimalx = -decimalx
		Else
			isPositive = true
		End If

		If decimalx = 0
			done = true
		End If

		If Not done
			multiplier = 0
			inc = 0

			If decimalx < 1
				multiplier = 10
				inc = -1
			ElseIf decimalx >= 10
				multiplier = 0.1
				inc = 1
			Else
				done = true
			End If

			If Not done
				
				While decimalx >= 10 Or decimalx < 1
					decimalx = decimalx*multiplier
					exponent = exponent + inc
				End While
			End If
		End If

		CreateStringFromNumberWithCheck(decimalx, 10, mantissaReference)

		CreateStringFromNumberWithCheck(exponent, 10, exponentReference)

		If Not isPositive
			result = AppendString(result, "-".ToCharArray())
		End If

		result = AppendString(result, mantissaReference.stringx)
		result = AppendString(result, "e".ToCharArray())
		result = AppendString(result, exponentReference.stringx)

		Return result
	End Function


	Public Function CreateStringDecimalFromNumber(decimalx As Double) As Char ()
		Dim stringReference As StringReference

		stringReference = New StringReference()

		' This will succeed because base = 10.
		CreateStringFromNumberWithCheck(decimalx, 10, stringReference)

		Return stringReference.stringx
	End Function


	Public Function CreateStringFromNumberWithCheck(decimalx As Double, base As Double, ByRef stringReference As StringReference) As Boolean
		Dim stringx As Char ()
		Dim maximumDigits As Double
		Dim digitPosition As Double
		Dim hasPrintedPoint, isPositive As Boolean
		Dim i, d As Double
		Dim success As Boolean
		Dim characterReference As CharacterReference
		Dim c As Char

		isPositive = true

		If decimalx < 0
			isPositive = false
			decimalx = -decimalx
		End If

		If decimalx = 0
			stringReference.stringx = "0".ToCharArray()
			success = true
		Else
			characterReference = New CharacterReference()

			If IsInteger(base)
				success = true

				stringx = New Char (0 - 1){}

				maximumDigits = GetMaximumDigitsForBase(base)

				digitPosition = GetFirstDigitPosition(decimalx, base)

				decimalx = Round(decimalx*base ^ (maximumDigits - digitPosition - 1))

				hasPrintedPoint = false

				If Not isPositive
					stringx = AppendCharacter(stringx, "-"C)
				End If

				' Print leading zeros.
				If digitPosition < 0
					stringx = AppendCharacter(stringx, "0"C)
					stringx = AppendCharacter(stringx, "."C)
					hasPrintedPoint = true
					i = 0
					While i < -digitPosition - 1
						stringx = AppendCharacter(stringx, "0"C)
						i = i + 1
					End While
				End If

				' Print number.
				i = 0
				While i < maximumDigits And success
					d = Floor(decimalx/base ^ (maximumDigits - i - 1))

					If d >= base
						d = base - 1
					End If

					If Not hasPrintedPoint And digitPosition - i + 1 = 0
						If decimalx <> 0
							stringx = AppendCharacter(stringx, "."C)
						End If
						hasPrintedPoint = true
					End If

					If decimalx = 0 And hasPrintedPoint
					Else
						success = GetSingleDigitCharacterFromNumberWithCheck(d, base, characterReference)
						If success
							c = characterReference.characterValue
							stringx = AppendCharacter(stringx, c)
						End If
					End If

					If success
						decimalx = decimalx - d*base ^ (maximumDigits - i - 1)
					End If
					i = i + 1
				End While

				If success
					' Print trailing zeros.
					i = 0
					While i < digitPosition - maximumDigits + 1
						stringx = AppendCharacter(stringx, "0"C)
						i = i + 1
					End While

					stringReference.stringx = stringx
				End If
			Else
				success = false
			End If
		End If

		' Done
		Return success
	End Function


	Public Function GetMaximumDigitsForBase(base As Double) As Double
		Dim t As Double

		t = 10 ^ 15
		Return Floor(Log10(t)/Log10(base))
	End Function


	Public Function GetFirstDigitPosition(decimalx As Double, base As Double) As Double
		Dim power As Double
		Dim t As Double

		power = Ceiling(Log10(decimalx)/Log10(base))

		t = decimalx*base ^ (-power)
		If t < base And t >= 1
		ElseIf t >= base
			power = power + 1
		ElseIf t < 1
			power = power - 1
		End If

		Return power
	End Function


	Public Function GetSingleDigitCharacterFromNumberWithCheck(c As Double, base As Double, ByRef characterReference As CharacterReference) As Boolean
		Dim numberTable As Char ()
		Dim success As Boolean

		numberTable = GetDigitCharacterTable()

		If c < base Or c < numberTable.Length
			success = true
			characterReference.characterValue = numberTable(c)
		Else
			success = false
		End If

		Return success
	End Function


	Public Function GetDigitCharacterTable() As Char ()
		Dim numberTable As Char ()

		numberTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()

		Return numberTable
	End Function


	Public Function CreateNumberFromDecimalStringWithCheck(ByRef stringx As Char (), ByRef decimalReference As NumberReference, ByRef errorMessage As StringReference) As Boolean
		Return CreateNumberFromStringWithCheck(stringx, 10, decimalReference, errorMessage)
	End Function


	Public Function CreateNumberFromDecimalString(ByRef stringx As Char ()) As Double
		Dim doubleReference As NumberReference
		Dim stringReference As StringReference
		Dim number As Double

		doubleReference = CreateNumberReference(0)
		stringReference = CreateStringReference("".ToCharArray())
		CreateNumberFromStringWithCheck(stringx, 10, doubleReference, stringReference)
		number = doubleReference.numberValue

		doubleReference = Nothing
		stringReference = Nothing

		Return number
	End Function


	Public Function CreateNumberFromStringWithCheck(ByRef stringx As Char (), base As Double, ByRef numberReference As NumberReference, ByRef errorMessage As StringReference) As Boolean
		Dim success As Boolean
		Dim numberIsPositive, exponentIsPositive As BooleanReference
		Dim beforePoint, afterPoint, exponent As NumberArrayReference

		numberIsPositive = CreateBooleanReference(true)
		exponentIsPositive = CreateBooleanReference(true)
		beforePoint = New NumberArrayReference()
		afterPoint = New NumberArrayReference()
		exponent = New NumberArrayReference()

		If base >= 2 And base <= 36
			success = ExtractPartsFromNumberString(stringx, base, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessage)

			If success
				numberReference.numberValue = CreateNumberFromParts(base, numberIsPositive.booleanValue, beforePoint.numberArray, afterPoint.numberArray, exponentIsPositive.booleanValue, exponent.numberArray)
			End If
		Else
			success = false
			errorMessage.stringx = "Base must be from 2 to 36.".ToCharArray()
		End If

		Return success
	End Function


	Public Function CreateNumberFromParts(base As Double, numberIsPositive As Boolean, ByRef beforePoint As Double (), ByRef afterPoint As Double (), exponentIsPositive As Boolean, ByRef exponent As Double ()) As Double
		Dim n, i, p, e As Double

		n = 0

		i = 0
		While i < beforePoint.Length
			p = beforePoint(beforePoint.Length - i - 1)

			n = n + p*base ^ i
			i = i + 1
		End While

		i = 0
		While i < afterPoint.Length
			p = afterPoint(i)

			n = n + p*base ^ (-(i + 1))
			i = i + 1
		End While

		If exponent.Length > 0
			e = 0
			i = 0
			While i < exponent.Length
				p = exponent(exponent.Length - i - 1)

				e = e + p*base ^ i
				i = i + 1
			End While

			If Not exponentIsPositive
				e = -e
			End If

			n = n*base ^ e
		End If

		If Not numberIsPositive
			n = -n
		End If

		Return n
	End Function


	Public Function ExtractPartsFromNumberString(ByRef n As Char (), base As Double, ByRef numberIsPositive As BooleanReference, ByRef beforePoint As NumberArrayReference, ByRef afterPoint As NumberArrayReference, ByRef exponentIsPositive As BooleanReference, ByRef exponent As NumberArrayReference, ByRef errorMessages As StringReference) As Boolean
		Dim i As Double
		Dim success As Boolean

		i = 0

		If i < n.Length
			If n(i) = "-"C
				numberIsPositive.booleanValue = false
				i = i + 1
			ElseIf n(i) = "+"C
				numberIsPositive.booleanValue = true
				i = i + 1
			End If

			success = ExtractPartsFromNumberStringFromSign(n, base, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages)
		Else
			success = false
			errorMessages.stringx = "Number cannot have length zero.".ToCharArray()
		End If

		Return success
	End Function


	Public Function ExtractPartsFromNumberStringFromSign(ByRef n As Char (), base As Double, i As Double, ByRef beforePoint As NumberArrayReference, ByRef afterPoint As NumberArrayReference, ByRef exponentIsPositive As BooleanReference, ByRef exponent As NumberArrayReference, ByRef errorMessages As StringReference) As Boolean
		Dim success, done As Boolean
		Dim count, j As Double

		done = false
		count = 0
		
		While i + count < n.Length And Not done
			If CharacterIsNumberCharacterInBase(n(i + count), base)
				count = count + 1
			Else
				done = true
			End If
		End While

		If count >= 1
			beforePoint.numberArray = New Double (count - 1){}

			j = 0
			While j < count
				beforePoint.numberArray(j) = GetNumberFromNumberCharacterForBase(n(i + j), base)
				j = j + 1
			End While

			i = i + count

			If i < n.Length
				success = ExtractPartsFromNumberStringFromPointOrExponent(n, base, i, afterPoint, exponentIsPositive, exponent, errorMessages)
			Else
				afterPoint.numberArray = New Double (0 - 1){}
				exponent.numberArray = New Double (0 - 1){}
				success = true
			End If
		Else
			success = false
			errorMessages.stringx = "Number must have at least one number after the optional sign.".ToCharArray()
		End If

		Return success
	End Function


	Public Function ExtractPartsFromNumberStringFromPointOrExponent(ByRef n As Char (), base As Double, i As Double, ByRef afterPoint As NumberArrayReference, ByRef exponentIsPositive As BooleanReference, ByRef exponent As NumberArrayReference, ByRef errorMessages As StringReference) As Boolean
		Dim success, done As Boolean
		Dim count, j As Double

		If n(i) = "."C
			i = i + 1

			If i < n.Length
				done = false
				count = 0
				
				While i + count < n.Length And Not done
					If CharacterIsNumberCharacterInBase(n(i + count), base)
						count = count + 1
					Else
						done = true
					End If
				End While

				If count >= 1
					afterPoint.numberArray = New Double (count - 1){}

					j = 0
					While j < count
						afterPoint.numberArray(j) = GetNumberFromNumberCharacterForBase(n(i + j), base)
						j = j + 1
					End While

					i = i + count

					If i < n.Length
						success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages)
					Else
						exponent.numberArray = New Double (0 - 1){}
						success = true
					End If
				Else
					success = false
					errorMessages.stringx = "There must be at least one digit after the decimal point.".ToCharArray()
				End If
			Else
				success = false
				errorMessages.stringx = "There must be at least one digit after the decimal point.".ToCharArray()
			End If
		ElseIf base <= 14 And (n(i) = "e"C Or n(i) = "E"C)
			If i < n.Length
				success = ExtractPartsFromNumberStringFromExponent(n, base, i, exponentIsPositive, exponent, errorMessages)
				afterPoint.numberArray = New Double (0 - 1){}
			Else
				success = false
				errorMessages.stringx = "There must be at least one digit after the exponent.".ToCharArray()
			End If
		Else
			success = false
			errorMessages.stringx = "Expected decimal point or exponent symbol.".ToCharArray()
		End If

		Return success
	End Function


	Public Function ExtractPartsFromNumberStringFromExponent(ByRef n As Char (), base As Double, i As Double, ByRef exponentIsPositive As BooleanReference, ByRef exponent As NumberArrayReference, ByRef errorMessages As StringReference) As Boolean
		Dim success, done As Boolean
		Dim count, j As Double

		If base <= 14 And (n(i) = "e"C Or n(i) = "E"C)
			i = i + 1

			If i < n.Length
				If n(i) = "-"C
					exponentIsPositive.booleanValue = false
					i = i + 1
				ElseIf n(i) = "+"C
					exponentIsPositive.booleanValue = true
					i = i + 1
				End If

				If i < n.Length
					done = false
					count = 0
					
					While i + count < n.Length And Not done
						If CharacterIsNumberCharacterInBase(n(i + count), base)
							count = count + 1
						Else
							done = true
						End If
					End While

					If count >= 1
						exponent.numberArray = New Double (count - 1){}

						j = 0
						While j < count
							exponent.numberArray(j) = GetNumberFromNumberCharacterForBase(n(i + j), base)
							j = j + 1
						End While

						i = i + count

						If i = n.Length
							success = true
						Else
							success = false
							errorMessages.stringx = "There cannot be any characters past the exponent of the number.".ToCharArray()
						End If
					Else
						success = false
						errorMessages.stringx = "There must be at least one digit after the decimal point.".ToCharArray()
					End If
				Else
					success = false
					errorMessages.stringx = "There must be at least one digit after the exponent symbol.".ToCharArray()
				End If
			Else
				success = false
				errorMessages.stringx = "There must be at least one digit after the exponent symbol.".ToCharArray()
			End If
		Else
			success = false
			errorMessages.stringx = "Expected exponent symbol.".ToCharArray()
		End If

		Return success
	End Function


	Public Function GetNumberFromNumberCharacterForBase(c As Char, base As Double) As Double
		Dim numberTable As Char ()
		Dim i As Double
		Dim position As Double

		numberTable = GetDigitCharacterTable()
		position = 0

		i = 0
		While i < base
			If numberTable(i) = c
				position = i
			End If
			i = i + 1
		End While

		Return position
	End Function


	Public Function CharacterIsNumberCharacterInBase(c As Char, base As Double) As Boolean
		Dim numberTable As Char ()
		Dim i As Double
		Dim found As Boolean

		numberTable = GetDigitCharacterTable()
		found = false

		i = 0
		While i < base
			If numberTable(i) = c
				found = true
			End If
			i = i + 1
		End While

		Return found
	End Function


	Public Function StringToNumberArray(ByRef str As Char ()) As Double ()
		Dim numberArrayReference As NumberArrayReference
		Dim stringReference As StringReference
		Dim numbers As Double ()

		numberArrayReference = New NumberArrayReference()
		stringReference = New StringReference()

		StringToNumberArrayWithCheck(str, numberArrayReference, stringReference)

		numbers = numberArrayReference.numberArray

		numberArrayReference = Nothing
		stringReference = Nothing

		Return numbers
	End Function


	Public Function StringToNumberArrayWithCheck(ByRef str As Char (), ByRef numberArrayReference As NumberArrayReference, ByRef errorMessage As StringReference) As Boolean
		Dim numberStrings As StringReference ()
		Dim numbers As Double ()
		Dim i As Double
		Dim numberString, trimmedNumberString As Char ()
		Dim success As Boolean
		Dim numberReference As NumberReference

		numberStrings = SplitByString(str, ",".ToCharArray())

		numbers = New Double (numberStrings.Length - 1){}
		success = true
		numberReference = New NumberReference()

		i = 0
		While i < numberStrings.Length
			numberString = numberStrings(i).stringx
			trimmedNumberString = Trim(numberString)
			success = CreateNumberFromDecimalStringWithCheck(trimmedNumberString, numberReference, errorMessage)
			numbers(i) = numberReference.numberValue

			Call FreeStringReference(numberStrings(i))
			Erase trimmedNumberString 
			i = i + 1
		End While

		Erase numberStrings 
		numberReference = Nothing

		numberArrayReference.numberArray = numbers

		Return success
	End Function


	Public Function Negate(x As Double) As Double
		Return -x
	End Function


	Public Function Positive(x As Double) As Double
		Return +x
	End Function


	Public Function Factorial(x As Double) As Double
		Dim i, f As Double

		f = 1

		i = 2
		While i <= x
			f = f*i
			i = i + 1
		End While

		Return f
	End Function


	Public Function Roundx(x As Double) As Double
		Return Floor(x + 0.5)
	End Function


	Public Function BankersRound(x As Double) As Double
		Dim r As Double

		If Absolute(x - Truncatex(x)) = 0.5
			If Not DivisibleBy(Roundx(x), 2)
				r = Roundx(x) - 1
			Else
				r = Roundx(x)
			End If
		Else
			r = Roundx(x)
		End If

		Return r
	End Function


	Public Function Ceil(x As Double) As Double
		Return Ceiling(x)
	End Function


	Public Function Floorx(x As Double) As Double
		Return Floor(x)
	End Function


	Public Function Truncatex(x As Double) As Double
		Dim t As Double

		If x >= 0
			t = Floor(x)
		Else
			t = Ceiling(x)
		End If

		Return t
	End Function


	Public Function Absolute(x As Double) As Double
		Return Abs(x)
	End Function


	Public Function Logarithm(x As Double) As Double
		Return Log10(x)
	End Function


	Public Function NaturalLogarithm(x As Double) As Double
		Return Log(x)
	End Function


	Public Function Sinx(x As Double) As Double
		Return Sin(x)
	End Function


	Public Function Cosx(x As Double) As Double
		Return Cos(x)
	End Function


	Public Function Tanx(x As Double) As Double
		Return Tan(x)
	End Function


	Public Function Asinx(x As Double) As Double
		Return Asin(x)
	End Function


	Public Function Acosx(x As Double) As Double
		Return Acos(x)
	End Function


	Public Function Atanx(x As Double) As Double
		Return Atan(x)
	End Function


	Public Function Atan2x(y As Double, x As Double) As Double
		Dim a As Double

		' Atan2 is an invalid operation when x = 0 and y = 0, but this method does not return errors.
		a = 0

		If x > 0
			a = Atanx(y/x)
		ElseIf x < 0 And y >= 0
			a = Atanx(y/x) + Pi
		ElseIf x < 0 And y < 0
			a = Atanx(y/x) - Pi
		ElseIf x = 0 And y > 0
			a = Pi/2
		ElseIf x = 0 And y < 0
			a = -Pi/2
		End If

		Return a
	End Function


	Public Function Squareroot(x As Double) As Double
		Return Sqrt(x)
	End Function


	Public Function Expx(x As Double) As Double
		Return Exp(x)
	End Function


	Public Function DivisibleBy(a As Double, b As Double) As Boolean
		Return ((a Mod b) = 0)
	End Function


	Public Function Combinations(n As Double, k As Double) As Double
		Dim i, j, c As Double

		c = 1
		j = 1
		i = n - k + 1

		
		While i <= n
			c = c*i
			c = c/j

			i = i + 1
			j = j + 1
		End While

		Return c
	End Function


	Public Function Permutations(n As Double, k As Double) As Double
		Dim i, c As Double

		c = 1

		i = n - k + 1
		While i <= n
			c = c*i
			i = i + 1
		End While

		Return c
	End Function


	Public Function EpsilonCompare(a As Double, b As Double, epsilon As Double) As Boolean
		Return Abs(a - b) < epsilon
	End Function


	Public Function GreatestCommonDivisor(a As Double, b As Double) As Double
		Dim t As Double

		
		While b <> 0
			t = b
			b = a Mod b
			a = t
		End While

		Return a
	End Function


	Public Function GCDWithSubtraction(a As Double, b As Double) As Double
		Dim g As Double

		If a = 0
			g = b
		Else
			
			While b <> 0
				If a > b
					a = a - b
				Else
					b = b - a
				End If
			End While

			g = a
		End If

		Return g
	End Function


	Public Function IsInteger(a As Double) As Boolean
		Return (a - Floor(a)) = 0
	End Function


	Public Function GreatestCommonDivisorWithCheck(a As Double, b As Double, ByRef gcdReference As NumberReference) As Boolean
		Dim success As Boolean
		Dim gcd As Double

		If IsInteger(a) And IsInteger(b)
			gcd = GreatestCommonDivisor(a, b)
			gcdReference.numberValue = gcd
			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function LeastCommonMultiple(a As Double, b As Double) As Double
		Dim lcm As Double

		If a > 0 And b > 0
			lcm = Abs(a*b)/GreatestCommonDivisor(a, b)
		Else
			lcm = 0
		End If

		Return lcm
	End Function


	Public Function Signx(a As Double) As Double
		Dim s As Double

		If a > 0
			s = 1
		ElseIf a < 0
			s = -1
		Else
			s = 0
		End If

		Return s
	End Function


	Public Function Maxx(a As Double, b As Double) As Double
		Return Max(a, b)
	End Function


	Public Function Minx(a As Double, b As Double) As Double
		Return Min(a, b)
	End Function


	Public Function Power(a As Double, b As Double) As Double
		Return a ^ b
	End Function


	Public Function Gamma(x As Double) As Double
		Return LanczosApproximation(x)
	End Function


	Public Function LogGamma(x As Double) As Double
		Return Log(Gamma(x))
	End Function


	Public Function LanczosApproximation(z As Double) As Double
		Dim p As Double ()
		Dim i, y, t, x As Double

		p = New Double (8 - 1){}
		p(0) = 676.5203681218851
		p(1) = -1259.1392167224028
		p(2) = 771.32342877765313
		p(3) = -176.61502916214059
		p(4) = 12.507343278686905
		p(5) = -0.13857109526572012
		p(6) = 9.9843695780195716e-6
		p(7) = 1.5056327351493116e-7

		If z < 0.5
			y = Pi/(Sin(Pi*z)*LanczosApproximation(1 - z))
		Else
			z = z - 1
			x = 0.99999999999980993
			i = 0
			While i < p.Length
				x = x + p(i)/(z + i + 1)
				i = i + 1
			End While
			t = z + p.Length - 0.5
			y = Sqrt(2*Pi)*t ^ (z + 0.5)*Exp(-t)*x
		End If

		Return y
	End Function


	Public Function Beta(x As Double, y As Double) As Double
		Return Gamma(x)*Gamma(y)/Gamma(x + y)
	End Function


	Public Function Sinhx(x As Double) As Double
		Return (Exp(x) - Exp(-x))/2
	End Function


	Public Function Coshx(x As Double) As Double
		Return (Exp(x) + Exp(-x))/2
	End Function


	Public Function Tanhx(x As Double) As Double
		Return Sinhx(x)/Coshx(x)
	End Function


	Public Function Cot(x As Double) As Double
		Return 1/Tan(x)
	End Function


	Public Function Sec(x As Double) As Double
		Return 1/Cos(x)
	End Function


	Public Function Csc(x As Double) As Double
		Return 1/Sin(x)
	End Function


	Public Function Coth(x As Double) As Double
		Return Coshx(x)/Sinhx(x)
	End Function


	Public Function Sech(x As Double) As Double
		Return 1/Coshx(x)
	End Function


	Public Function Csch(x As Double) As Double
		Return 1/Sinhx(x)
	End Function


	Public Function Errorx(x As Double) As Double
		Dim y, t, tau, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10 As Double

		If x = 0
			y = 0
		ElseIf x < 0
			y = -Errorx(-x)
		Else
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

			t = 1/(1 + 0.5*Abs(x))

			tau = t*Exp(-x ^ 2 + c1 + t*(c2 + t*(c3 + t*(c4 + t*(c5 + t*(c6 + t*(c7 + t*(c8 + t*(c9 + t*c10)))))))))

			y = 1 - tau
		End If

		Return y
	End Function


	Public Function ErrorInverse(x As Double) As Double
		Dim y, a, t As Double

		a = (8*(Pi - 3))/(3*Pi*(4 - Pi))

		t = 2/(Pi*a) + Log(1 - x ^ 2)/2
		y = Signx(x)*Sqrt(Sqrt(t ^ 2 - Log(1 - x ^ 2)/a) - t)

		Return y
	End Function


	Public Function FallingFactorial(x As Double, n As Double) As Double
		Dim k, y As Double

		y = 1

		k = 0
		While k <= n - 1
			y = y*(x - k)
			k = k + 1
		End While

		Return y
	End Function


	Public Function RisingFactorial(x As Double, n As Double) As Double
		Dim k, y As Double

		y = 1

		k = 0
		While k <= n - 1
			y = y*(x + k)
			k = k + 1
		End While

		Return y
	End Function


	Public Function Hypergeometric(a As Double, b As Double, c As Double, z As Double, maxIterations As Double, precision As Double) As Double
		Dim y As Double

		If Abs(z) >= 0.5
			y = (1 - z) ^ (-a)*HypergeometricDirect(a, c - b, c, z/(z - 1), maxIterations, precision)
		Else
			y = HypergeometricDirect(a, b, c, z, maxIterations, precision)
		End If

		Return y
	End Function


	Public Function HypergeometricDirect(a As Double, b As Double, c As Double, z As Double, maxIterations As Double, precision As Double) As Double
		Dim y, yp, n As Double
		Dim done As Boolean

		y = 0
		done = false

		n = 0
		While n < maxIterations And Not done
			yp = RisingFactorial(a, n)*RisingFactorial(b, n)/RisingFactorial(c, n)*z ^ n/Factorial(n)
			If Abs(yp) < precision
				done = true
			End If
			y = y + yp
			n = n + 1
		End While

		Return y
	End Function


	Public Function BernouilliNumber(n As Double) As Double
		Return AkiyamaTanigawaAlgorithm(n)
	End Function


	Public Function AkiyamaTanigawaAlgorithm(n As Double) As Double
		Dim m, j, B As Double
		Dim A As Double ()

		A = New Double (n + 1 - 1){}

		m = 0
		While m <= n
			A(m) = 1/(m + 1)
			j = m
			While j >= 1
				A(j - 1) = j*(A(j - 1) - A(j))
				j = j - 1
			End While
			m = m + 1
		End While

		B = A(0)

		Erase A 

		Return B
	End Function


	Public Function aStringToNumberArray(ByRef stringx As Char ()) As Double ()
		Dim i As Double
		Dim arrayx As Double ()

		arrayx = New Double (stringx.Length - 1){}

		i = 0
		While i < stringx.Length
			arrayx(i) = Convert.ToInt16(stringx(i))
			i = i + 1
		End While
		Return arrayx
	End Function


	Public Function aNumberArrayToString(ByRef arrayx As Double ()) As Char ()
		Dim i As Double
		Dim stringx As Char ()

		stringx = New Char (arrayx.Length - 1){}

		i = 0
		While i < arrayx.Length
			stringx(i) = Convert.ToChar(Convert.ToInt64(arrayx(i)))
			i = i + 1
		End While
		Return stringx
	End Function


	Public Function aNumberArraysEqual(ByRef a As Double (), ByRef b As Double ()) As Boolean
		Dim equal As Boolean
		Dim i As Double

		equal = true
		If a.Length = b.Length
			i = 0
			While i < a.Length And equal
				If a(i) <> b(i)
					equal = false
				End If
				i = i + 1
			End While
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Function aBooleanArraysEqual(ByRef a As Boolean (), ByRef b As Boolean ()) As Boolean
		Dim equal As Boolean
		Dim i As Double

		equal = true
		If a.Length = b.Length
			i = 0
			While i < a.Length And equal
				If a(i) <> b(i)
					equal = false
				End If
				i = i + 1
			End While
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Function aStringsEqual(ByRef a As Char (), ByRef b As Char ()) As Boolean
		Dim equal As Boolean
		Dim i As Double

		equal = true
		If a.Length = b.Length
			i = 0
			While i < a.Length And equal
				If a(i) <> b(i)
					equal = false
				End If
				i = i + 1
			End While
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Sub aFillNumberArray(ByRef a As Double (), value As Double)
		Dim i As Double

		i = 0
		While i < a.Length
			a(i) = value
			i = i + 1
		End While
	End Sub


	Public Sub aFillString(ByRef a As Char (), value As Char)
		Dim i As Double

		i = 0
		While i < a.Length
			a(i) = value
			i = i + 1
		End While
	End Sub


	Public Sub aFillBooleanArray(ByRef a As Boolean (), value As Boolean)
		Dim i As Double

		i = 0
		While i < a.Length
			a(i) = value
			i = i + 1
		End While
	End Sub


	Public Function aFillNumberArrayRange(ByRef a As Double (), value As Double, from As Double, tox As Double) As Boolean
		Dim i, length As Double
		Dim success As Boolean

		If from >= 0 And from <= a.Length And tox >= 0 And tox <= a.Length And from <= tox
			length = tox - from
			i = 0
			While i < length
				a(from + i) = value
				i = i + 1
			End While

			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function aFillBooleanArrayRange(ByRef a As Boolean (), value As Boolean, from As Double, tox As Double) As Boolean
		Dim i, length As Double
		Dim success As Boolean

		If from >= 0 And from <= a.Length And tox >= 0 And tox <= a.Length And from <= tox
			length = tox - from
			i = 0
			While i < length
				a(from + i) = value
				i = i + 1
			End While

			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function aFillStringRange(ByRef a As Char (), value As Char, from As Double, tox As Double) As Boolean
		Dim i, length As Double
		Dim success As Boolean

		If from >= 0 And from <= a.Length And tox >= 0 And tox <= a.Length And from <= tox
			length = tox - from
			i = 0
			While i < length
				a(from + i) = value
				i = i + 1
			End While

			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function aCopyNumberArray(ByRef a As Double ()) As Double ()
		Dim i As Double
		Dim n As Double ()

		n = New Double (a.Length - 1){}

		i = 0
		While i < a.Length
			n(i) = a(i)
			i = i + 1
		End While

		Return n
	End Function


	Public Function aCopyBooleanArray(ByRef a As Boolean ()) As Boolean ()
		Dim i As Double
		Dim n As Boolean ()

		n = New Boolean (a.Length - 1){}

		i = 0
		While i < a.Length
			n(i) = a(i)
			i = i + 1
		End While

		Return n
	End Function


	Public Function aCopyString(ByRef a As Char ()) As Char ()
		Dim i As Double
		Dim n As Char ()

		n = New Char (a.Length - 1){}

		i = 0
		While i < a.Length
			n(i) = a(i)
			i = i + 1
		End While

		Return n
	End Function


	Public Function aCopyNumberArrayRange(ByRef a As Double (), from As Double, tox As Double, ByRef copyReference As NumberArrayReference) As Boolean
		Dim i, length As Double
		Dim n As Double ()
		Dim success As Boolean

		If from >= 0 And from <= a.Length And tox >= 0 And tox <= a.Length And from <= tox
			length = tox - from
			n = New Double (length - 1){}

			i = 0
			While i < length
				n(i) = a(from + i)
				i = i + 1
			End While

			copyReference.numberArray = n
			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function aCopyBooleanArrayRange(ByRef a As Boolean (), from As Double, tox As Double, ByRef copyReference As BooleanArrayReference) As Boolean
		Dim i, length As Double
		Dim n As Boolean ()
		Dim success As Boolean

		If from >= 0 And from <= a.Length And tox >= 0 And tox <= a.Length And from <= tox
			length = tox - from
			n = New Boolean (length - 1){}

			i = 0
			While i < length
				n(i) = a(from + i)
				i = i + 1
			End While

			copyReference.booleanArray = n
			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function aCopyStringRange(ByRef a As Char (), from As Double, tox As Double, ByRef copyReference As StringReference) As Boolean
		Dim i, length As Double
		Dim n As Char ()
		Dim success As Boolean

		If from >= 0 And from <= a.Length And tox >= 0 And tox <= a.Length And from <= tox
			length = tox - from
			n = New Char (length - 1){}

			i = 0
			While i < length
				n(i) = a(from + i)
				i = i + 1
			End While

			copyReference.stringx = n
			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function aIsLastElement(length As Double, index As Double) As Boolean
		Return index + 1 = length
	End Function


	Public Function aCreateNumberArray(length As Double, value As Double) As Double ()
		Dim arrayx As Double ()

		arrayx = New Double (length - 1){}
		Call aFillNumberArray(arrayx, value)

		Return arrayx
	End Function


	Public Function aCreateBooleanArray(length As Double, value As Boolean) As Boolean ()
		Dim arrayx As Boolean ()

		arrayx = New Boolean (length - 1){}
		Call aFillBooleanArray(arrayx, value)

		Return arrayx
	End Function


	Public Function aCreateString(length As Double, value As Char) As Char ()
		Dim arrayx As Char ()

		arrayx = New Char (length - 1){}
		Call aFillString(arrayx, value)

		Return arrayx
	End Function


	Public Sub aSwapElementsOfNumberArray(ByRef A As Double (), ai As Double, bi As Double)
		Dim tmp As Double

		tmp = A(ai)
		A(ai) = A(bi)
		A(bi) = tmp
	End Sub


	Public Sub aSwapElementsOfStringArray(ByRef A As StringArrayReference, ai As Double, bi As Double)
		Dim tmp As StringReference

		tmp = A.stringArray(ai)
		A.stringArray(ai) = A.stringArray(bi)
		A.stringArray(bi) = tmp
	End Sub


	Public Sub aReverseNumberArray(ByRef arrayx As Double ())
		Dim i As Double

		i = 0
		While i < arrayx.Length/2
			Call aSwapElementsOfNumberArray(arrayx, i, arrayx.Length - i - 1)
			i = i + 1
		End While
	End Sub


	Public Function CreateBooleanReference(value As Boolean) As BooleanReference
		Dim ref As BooleanReference

		ref = New BooleanReference()
		ref.booleanValue = value

		Return ref
	End Function


	Public Function CreateBooleanArrayReference(ByRef value As Boolean ()) As BooleanArrayReference
		Dim ref As BooleanArrayReference

		ref = New BooleanArrayReference()
		ref.booleanArray = value

		Return ref
	End Function


	Public Function CreateBooleanArrayReferenceLengthValue(length As Double, value As Boolean) As BooleanArrayReference
		Dim ref As BooleanArrayReference
		Dim i As Double

		ref = New BooleanArrayReference()
		ref.booleanArray = New Boolean (length - 1){}

		i = 0
		While i < length
			ref.booleanArray(i) = value
			i = i + 1
		End While

		Return ref
	End Function


	Public Sub FreeBooleanArrayReference(ByRef booleanArrayReference As BooleanArrayReference)
		Erase booleanArrayReference.booleanArray 
		booleanArrayReference = Nothing
	End Sub


	Public Function CreateCharacterReference(value As Char) As CharacterReference
		Dim ref As CharacterReference

		ref = New CharacterReference()
		ref.characterValue = value

		Return ref
	End Function


	Public Function CreateNumberReference(value As Double) As NumberReference
		Dim ref As NumberReference

		ref = New NumberReference()
		ref.numberValue = value

		Return ref
	End Function


	Public Function CreateNumberArrayReference(ByRef value As Double ()) As NumberArrayReference
		Dim ref As NumberArrayReference

		ref = New NumberArrayReference()
		ref.numberArray = value

		Return ref
	End Function


	Public Function CreateNumberArrayReferenceLengthValue(length As Double, value As Double) As NumberArrayReference
		Dim ref As NumberArrayReference
		Dim i As Double

		ref = New NumberArrayReference()
		ref.numberArray = New Double (length - 1){}

		i = 0
		While i < length
			ref.numberArray(i) = value
			i = i + 1
		End While

		Return ref
	End Function


	Public Sub FreeNumberArrayReference(ByRef numberArrayReference As NumberArrayReference)
		Erase numberArrayReference.numberArray 
		numberArrayReference = Nothing
	End Sub


	Public Function CreateStringReference(ByRef value As Char ()) As StringReference
		Dim ref As StringReference

		ref = New StringReference()
		ref.stringx = value

		Return ref
	End Function


	Public Function CreateStringReferenceLengthValue(length As Double, value As Char) As StringReference
		Dim ref As StringReference
		Dim i As Double

		ref = New StringReference()
		ref.stringx = New Char (length - 1){}

		i = 0
		While i < length
			ref.stringx(i) = value
			i = i + 1
		End While

		Return ref
	End Function


	Public Sub FreeStringReference(ByRef stringReference As StringReference)
		Erase stringReference.stringx 
		stringReference = Nothing
	End Sub


	Public Function CreateStringArrayReference(ByRef strings As StringReference ()) As StringArrayReference
		Dim ref As StringArrayReference

		ref = New StringArrayReference()
		ref.stringArray = strings

		Return ref
	End Function


	Public Function CreateStringArrayReferenceLengthValue(length As Double, ByRef value As Char ()) As StringArrayReference
		Dim ref As StringArrayReference
		Dim i As Double

		ref = New StringArrayReference()
		ref.stringArray = New StringReference (length - 1){}

		i = 0
		While i < length
			ref.stringArray(i) = CreateStringReference(value)
			i = i + 1
		End While

		Return ref
	End Function


	Public Sub FreeStringArrayReference(ByRef stringArrayReference As StringArrayReference)
		Dim i As Double

		i = 0
		While i < stringArrayReference.stringArray.Length
			stringArrayReference.stringArray(i) = Nothing
			i = i + 1
		End While
		Erase stringArrayReference.stringArray 
		stringArrayReference = Nothing
	End Sub


	Public Function DigitDataBase16() As Char ()
		Return "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe891412108153069c4ffffffffffffffffffffffffffffffffffffffff9409000000000000000049e7ffffffffffffffffffffffffffffffffff61000000000000000000000017ddffffffffffffffffffffffffffffff840000000573d3f5e5a62b00000028f0ffffffffffffffffffffffffffda04000008bcfffffffffff44200000073ffffffffffffffffffffffffff5700000088ffffffffffffffe812000008e3ffffffffffffffffffffffea02000015f9ffffffffffffffff8100000080ffffffffffffffffffffff9c00000072ffffffffffffffffffe40100002fffffffffffffffffffffff51000000b8ffffffffffffffffffff2a000000e2ffffffffffffffffffff21000001f0ffffffffffffffffffff65000000b3fffffffffffffffffff602000018ffffffffffffffffffffff8b0000008affffffffffffffffffd200000036ffffffffffffffffffffffa900000063ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffb500000057ffffffffffffffffffffffc900000046ffffffffffffffffffa90000005fffffffffffffffffffffffd20000003affffffffffffffffffa900000060ffffffffffffffffffffffd30000003affffffffffffffffffb400000057ffffffffffffffffffffffca00000046ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffd100000037ffffffffffffffffffffffa900000063fffffffffffffffffff602000019ffffffffffffffffffffff8b00000089ffffffffffffffffffff21000001f1ffffffffffffffffffff66000000b3ffffffffffffffffffff50000000b8ffffffffffffffffffff2a000000e1ffffffffffffffffffff9c00000073ffffffffffffffffffe40100002fffffffffffffffffffffffea02000015f9ffffffffffffffff8200000080ffffffffffffffffffffffff5700000088ffffffffffffffe812000008e2ffffffffffffffffffffffffda04000008bcfffffffffff44300000073ffffffffffffffffffffffffffff830000000674d3f6e6a72b00000028f0ffffffffffffffffffffffffffffff60000000000000000000000016ddfffffffffffffffffffffffffffffffffe9309000000000000000048e6ffffffffffffffffffffffffffffffffffffffe88f3f1f07132e68c3fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9d7b28e69441f02000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6a274c7095b9de64000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000affffffffffffffffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffd48b56271005142a5ea0f6ffffffffffffffffffffffffffffffffdb7c20000000000000000000001392feffffffffffffffffffffffffffff1f00000000000000000000000000004cf9ffffffffffffffffffffffffff1f0000003784c7e7f9e8b1480000000056ffffffffffffffffffffffffff1f015accffffffffffffffff9701000000b0ffffffffffffffffffffffff58caffffffffffffffffffffff770000003cfffffffffffffffffffffffffffffffffffffffffffffffffff107000002edffffffffffffffffffffffffffffffffffffffffffffffffff3a000000ccffffffffffffffffffffffffffffffffffffffffffffffffff4c000000baffffffffffffffffffffffffffffffffffffffffffffffffff32000000cbffffffffffffffffffffffffffffffffffffffffffffffffec05000002edffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffeb140000009affffffffffffffffffffffffffffffffffffffffffffffff520000002afbffffffffffffffffffffffffffffffffffffffffffffff8c00000003c7ffffffffffffffffffffffffffffffffffffffffffffffb30300000085ffffffffffffffffffffffffffffffffffffffffffffffc50a0000005dfeffffffffffffffffffffffffffffffffffffffffffffd2110000004efbffffffffffffffffffffffffffffffffffffffffffffdb1800000042f8ffffffffffffffffffffffffffffffffffffffffffffe21f00000039f3ffffffffffffffffffffffffffffffffffffffffffffe92600000030efffffffffffffffffffffffffffffffffffffffffffffee2e00000029eafffffffffffffffffffffffffffffffffffffffffffff33700000022e5fffffffffffffffffffffffffffffffffffffffffffff7410000001cdffffffffffffffffffffffffffffffffffffffffffffffb4c00000017d9fffffffffffffffffffffffffffffffffffffffffffffd5900000012d2ffffffffffffffffffffffffffffffffffffffffffffff680000000ecbffffffffffffffffffffffffffffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe2af8058392817060a1a3f74c8ffffffffffffffffffffffffffffffffeb0000000000000000000000000036cfffffffffffffffffffffffffffffeb000000000000000000000000000004a7ffffffffffffffffffffffffffeb00000f5a9dd0edfbf0ca841900000003c2ffffffffffffffffffffffffec3da8f9fffffffffffffffff0410000002bffffffffffffffffffffffffffffffffffffffffffffffffffee12000000cbffffffffffffffffffffffffffffffffffffffffffffffffff6900000090ffffffffffffffffffffffffffffffffffffffffffffffffff9600000078ffffffffffffffffffffffffffffffffffffffffffffffffff9a0000007effffffffffffffffffffffffffffffffffffffffffffffffff73000000a5fffffffffffffffffffffffffffffffffffffffffffffffff51b000009edfffffffffffffffffffffffffffffffffffffffffffffff7540000007efffffffffffffffffffffffffffffffffffffffffff3d3912400000055fcffffffffffffffffffffffffffffffffff1700000000000000001692feffffffffffffffffffffffffffffffffffff17000000000000002db8feffffffffffffffffffffffffffffffffffffff170000000000000000002bc3fffffffffffffffffffffffffffffffffffffffffffdf0cf922e00000003a5fffffffffffffffffffffffffffffffffffffffffffffffffd8700000007d1ffffffffffffffffffffffffffffffffffffffffffffffffff780000004ffffffffffffffffffffffffffffffffffffffffffffffffffff308000006f6ffffffffffffffffffffffffffffffffffffffffffffffffff3c000000d0ffffffffffffffffffffffffffffffffffffffffffffffffff4d000000c6ffffffffffffffffffffffffffffffffffffffffffffffffff35000000ddffffffffffffffffffffffffffffffffffffffffffffffffea0300000bf9ffffffffffffffffffffffffffffffffffffffffffffffff6200000054ffffffffffffffffffffff47bafefffffffffffffffffff56b00000002cbffffffffffffffffffffff0b001e71a9d7edfbf6e4ba771a000000007cffffffffffffffffffffffff0b0000000000000000000000000000017dffffffffffffffffffffffffff0b000000000000000000000000003cc8ffffffffffffffffffffffffffffe9b989593827160608162a5689dbffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffbd0100000000f3fffffffffffffffffffffffffffffffffffffffffffff3200000000000f3ffffffffffffffffffffffffffffffffffffffffffff69000000000000f3ffffffffffffffffffffffffffffffffffffffffffbf01000b0e000000f3fffffffffffffffffffffffffffffffffffffffff42100008e1f000000f3ffffffffffffffffffffffffffffffffffffffff6a000035fc1f000000f3ffffffffffffffffffffffffffffffffffffffc0010004d1ff1f000000f3fffffffffffffffffffffffffffffffffffff42200007affff1f000000f3ffffffffffffffffffffffffffffffffffff6c000026f7ffff1f000000f3ffffffffffffffffffffffffffffffffffc1010001c1ffffff1f000000f3fffffffffffffffffffffffffffffffff523000066ffffffff1f000000f3ffffffffffffffffffffffffffffffff6d000019f0ffffffff1f000000f3ffffffffffffffffffffffffffffffc2010000aeffffffffff1f000000f3fffffffffffffffffffffffffffff524000052ffffffffffff1f000000f3ffffffffffffffffffffffffffff6e00000fe6ffffffffffff1f000000f3ffffffffffffffffffffffffffc30200009affffffffffffff1f000000f3fffffffffffffffffffffffff62400003ffeffffffffffffff1f000000f3ffffffffffffffffffffffff70000008daffffffffffffffff1f000000f3fffffffffffffffffffffff602000086ffffffffffffffffff1f000000f3fffffffffffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f000008672f120514275997efffffffffffffffffffffffffffffffffff4f00000000000000000000000b73f6ffffffffffffffffffffffffffffff4f000000000000000000000000002bdeffffffffffffffffffffffffffff60538cbad2e7faf0d599370000000025ebffffffffffffffffffffffffffffffffffffffffffffffffa0090000005bffffffffffffffffffffffffffffffffffffffffffffffffffb100000001d2ffffffffffffffffffffffffffffffffffffffffffffffffff560000007effffffffffffffffffffffffffffffffffffffffffffffffffb80000003dffffffffffffffffffffffffffffffffffffffffffffffffffec00000022fffffffffffffffffffffffffffffffffffffffffffffffffffd00000011ffffffffffffffffffffffffffffffffffffffffffffffffffec00000022ffffffffffffffffffffffffffffffffffffffffffffffffffb80000003cffffffffffffffffffffffffffffffffffffffffffffffffff580000007dffffffffffffffffffffffffffffffffffffffffffffffffb301000000cfffffffffffffffffffffff4cb1fdffffffffffffffffffa40a00000058ffffffffffffffffffffffff17001a6ea9d7eefbf2d69b380000000024e8ffffffffffffffffffffffff1700000000000000000000000000002de0ffffffffffffffffffffffffff17000000000000000000000000127ef9ffffffffffffffffffffffffffffebba8a59372615050a1a3569a6f7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffca753915050d233866a3e0ffffffffffffffffffffffffffffffffffd13f0000000000000000000000f7ffffffffffffffffffffffffffffff9d07000000000000000000000000f7ffffffffffffffffffffffffffff9700000000469fdbf3f5da9e490100f7ffffffffffffffffffffffffffca0300000eb3ffffffffffffffffd84df8fffffffffffffffffffffffffa2d000007c8ffffffffffffffffffffffffffffffffffffffffffffffff9100000081ffffffffffffffffffffffffffffffffffffffffffffffffff28000010f6ffffffffffffffffffffffffffffffffffffffffffffffffc20000006affffffffffffffffffffffffffffffffffffffffffffffffff79000000b2ffffffffffffffffffffffffffffffffffffffffffffffffff43000000ebffeb903d1a0616306fc0ffffffffffffffffffffffffffffff0f000015ffa211000000000000000041dcfffffffffffffffffffffffff30000003087000000000000000000000013c6ffffffffffffffffffffffe30000000f00000055beeef7d8881000000017e6ffffffffffffffffffffd30000000000019dffffffffffffe12200000056ffffffffffffffffffffd100000000006effffffffffffffffce04000002dbffffffffffffffffffdd0000000006eaffffffffffffffffff550000008bffffffffffffffffffe90000000043ffffffffffffffffffffa90000004dfffffffffffffffffff80200000074ffffffffffffffffffffdb0000002cffffffffffffffffffff2200000088ffffffffffffffffffffef00000019ffffffffffffffffffff4d00000088ffffffffffffffffffffee0000001affffffffffffffffffff7e00000074ffffffffffffffffffffdb0000002dffffffffffffffffffffcd00000042ffffffffffffffffffffa900000052ffffffffffffffffffffff21000005e9ffffffffffffffffff5400000093ffffffffffffffffffffff8f0000006dffffffffffffffffcd04000007e6fffffffffffffffffffffff9220000019effffffffffffe1230000006cffffffffffffffffffffffffffc00600000056beeff8d888110000002af3ffffffffffffffffffffffffffffa603000000000000000000000026ddffffffffffffffffffffffffffffffffc8280000000000000000025deffffffffffffffffffffffffffffffffffffffab25a2a1106193b7ed7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff47000000000000000000000000000000000000f7ffffffffffffffffffff47000000000000000000000000000000000003faffffffffffffffffffff4700000000000000000000000000000000004afffffffffffffffffffffffffffffffffffffffffffffffffc1a000000adffffffffffffffffffffffffffffffffffffffffffffffffb300000015faffffffffffffffffffffffffffffffffffffffffffffffff5100000073ffffffffffffffffffffffffffffffffffffffffffffffffea05000000d6ffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffff2c0000009dffffffffffffffffffffffffffffffffffffffffffffffffc90000000cf3ffffffffffffffffffffffffffffffffffffffffffffffff6700000063fffffffffffffffffffffffffffffffffffffffffffffffff60f000000c6ffffffffffffffffffffffffffffffffffffffffffffffffa300000029ffffffffffffffffffffffffffffffffffffffffffffffffff410000008cffffffffffffffffffffffffffffffffffffffffffffffffdf01000005e9ffffffffffffffffffffffffffffffffffffffffffffffff7d00000052fffffffffffffffffffffffffffffffffffffffffffffffffd1e000000b5ffffffffffffffffffffffffffffffffffffffffffffffffb90000001bfcffffffffffffffffffffffffffffffffffffffffffffffff570000007bffffffffffffffffffffffffffffffffffffffffffffffffee07000001ddffffffffffffffffffffffffffffffffffffffffffffffff9300000042ffffffffffffffffffffffffffffffffffffffffffffffffff31000000a5ffffffffffffffffffffffffffffffffffffffffffffffffd000000010f7ffffffffffffffffffffffffffffffffffffffffffffffff6d0000006bfffffffffffffffffffffffffffffffffffffffffffffffff913000000ceffffffffffffffffffffffffffffffffffffffffffffffffa900000031ffffffffffffffffffffffffffffffffffffffffffffffffff4700000094ffffffffffffffffffffffffffffffffffffffffffffffffe302000008eeffffffffffffffffffffffffffffffffffffffffffffffff840000005afffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9a8602c13050c1d4882dfffffffffffffffffffffffffffffffffffffa918000000000000000000025eeeffffffffffffffffffffffffffffff780000000000000000000000000023e5ffffffffffffffffffffffffff9f0000000037a8e4faf1c66d0500000033fdfffffffffffffffffffffff81600000065fdffffffffffffc40a0000009fffffffffffffffffffffffb600000021faffffffffffffffff8d00000047ffffffffffffffffffffff820000007bffffffffffffffffffeb01000014ffffffffffffffffffffff6d000000a2ffffffffffffffffffff15000001fdffffffffffffffffffff76000000a2ffffffffffffffffffff14000007ffffffffffffffffffffffa10000007bffffffffffffffffffec01000033ffffffffffffffffffffffec08000022fbffffffffffffffff8e00000087ffffffffffffffffffffffff7d00000068fdffffffffffffc70b00001ef2fffffffffffffffffffffffffb5500000039aae5fbf2c87006000013d0fffffffffffffffffffffffffffffe93160000000000000000000153e3ffffffffffffffffffffffffffffffffffbd2e000000000000000780f0ffffffffffffffffffffffffffffffffce3500000000000000000000000e87fcffffffffffffffffffffffffffb3060000004fb2e6faf0cd82150000004ffaffffffffffffffffffffffda0b000004a9ffffffffffffffe93600000076ffffffffffffffffffffff5600000084ffffffffffffffffffe80e000005e2fffffffffffffffffff606000008f4ffffffffffffffffffff6f0000008dffffffffffffffffffcb00000039ffffffffffffffffffffffac0000005cffffffffffffffffffbc0000004affffffffffffffffffffffbe0000004dffffffffffffffffffcc00000039ffffffffffffffffffffffac0000005effffffffffffffffffea00000008f4ffffffffffffffffffff6e0000007cffffffffffffffffffff2f00000085ffffffffffffffffffe70d000000c1ffffffffffffffffffff9300000004a9ffffffffffffffe83400000028fcfffffffffffffffffffffa2d0000000050b2e7fbf2cd821400000002b8ffffffffffffffffffffffffe523000000000000000000000000000299fffffffffffffffffffffffffffff16605000000000000000000002cc5ffffffffffffffffffffffffffffffffffe88e542512040b1b3d72c1fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff8a259251008203f8be2ffffffffffffffffffffffffffffffffffffffa91d0000000000000000047ffaffffffffffffffffffffffffffffffff7b00000000000000000000000040f8ffffffffffffffffffffffffffff94000000004db9ecf7da8b1300000057ffffffffffffffffffffffffffdc050000008fffffffffffffe527000000acffffffffffffffffffffffff630000005fffffffffffffffffd406000025fbfffffffffffffffffffffb0c000002e0ffffffffffffffffff5f000000b2ffffffffffffffffffffc600000036ffffffffffffffffffffb50000005fffffffffffffffffffffa000000068ffffffffffffffffffffe700000011feffffffffffffffffff8d0000007cfffffffffffffffffffffb00000000dfffffffffffffffffff8c0000007cfffffffffffffffffffffb00000000b4ffffffffffffffffff9e00000069ffffffffffffffffffffe7000000008dffffffffffffffffffbe00000038ffffffffffffffffffffb6000000007bfffffffffffffffffff606000003e2ffffffffffffffffff62000000006fffffffffffffffffffff4f00000064ffffffffffffffffd8080000000062ffffffffffffffffffffc50000000096ffffffffffffe82b000000000064ffffffffffffffffffffff6c0000000051bbeff8dc8e1500001000000074fffffffffffffffffffffff94f0000000000000000000000288c00000084fffffffffffffffffffffffffd810b000000000000000052ea830000009fffffffffffffffffffffffffffffea8d471d090d2864c1ffff5b000000d4ffffffffffffffffffffffffffffffffffffffffffffffffff2100000dfdffffffffffffffffffffffffffffffffffffffffffffffffd900000052ffffffffffffffffffffffffffffffffffffffffffffffffff75000000b8ffffffffffffffffffffffffffffffffffffffffffffffffe30d000023fefffffffffffffffffffffffffffffffffffffffffffffff945000000b7ffffffffffffffffffffffffff7fa2fdffffffffffffffe8480000005effffffffffffffffffffffffffff63002080c4ecfae7c0740e00000034f4ffffffffffffffffffffffffffff6300000000000000000000000043f0ffffffffffffffffffffffffffffff6300000000000000000000118efdfffffffffffffffffffffffffffffffff4bb7f462b15040b25569ff4ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff".ToCharArray()
	End Function


	Public Sub DrawDigitCharacter(ByRef image As RGBABitmapImage, topx As Double, topy As Double, digit As Double)
		Dim x, y As Double
		Dim allCharData, colorChars As Char ()
		Dim colorReference As NumberReference
		Dim errorMessage As StringReference
		Dim color As RGBA

		colorReference = New NumberReference()
		errorMessage = New StringReference()
		color = New RGBA()

		colorChars = New Char (2 - 1){}

		allCharData = DigitDataBase16()

		y = 0
		While y < 37
			x = 0
			While x < 30
				colorChars(0) = allCharData(digit*30*37*2 + y*2*30 + x*2 + 0)
				colorChars(1) = allCharData(digit*30*37*2 + y*2*30 + x*2 + 1)

				Call ToUpperCase(colorChars)
				CreateNumberFromStringWithCheck(colorChars, 16, colorReference, errorMessage)
				color.r = colorReference.numberValue/255
				color.g = colorReference.numberValue/255
				color.b = colorReference.numberValue/255
				color.a = 1
				Call SetPixel(image, topx + x, topy + y, color)
				x = x + 1
			End While
			y = y + 1
		End While
	End Sub


	Public Function GetPixelFontData() As Char ()
		Return "0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000".ToCharArray()
	End Function


	Public Sub DrawAsciiCharacter(ByRef image As RGBABitmapImage, topx As Double, topy As Double, a As Char, ByRef color As RGBA)
		Dim index, x, y, row, pixel As Double
		Dim allCharData, charData, rowData As Char ()
		Dim rowReference As NumberReference
		Dim errorMessage As StringReference

		rowReference = New NumberReference()
		errorMessage = New StringReference()

		index = Convert.ToInt16(a)
		index = index - 32
		allCharData = GetPixelFontData()
		charData = Substring(allCharData, index*2*13, (index + 1)*2*13)

		y = 0
		While y < 13
			rowData = Substring(charData, y*2, (y + 1)*2)
			Call ToUpperCase(rowData)
			CreateNumberFromStringWithCheck(rowData, 16, rowReference, errorMessage)
			row = rowReference.numberValue
			x = 0
			While x < 8
				pixel = Floor(row/2 ^ x) Mod 2
				If pixel = 1
					Call DrawPixel(image, topx + 8 - 1 - x, topy + 13 - 1 - y, color)
				End If
				x = x + 1
			End While
			y = y + 1
		End While
	End Sub


	Public Function GetTextWidth(ByRef text As Char ()) As Double
		Dim charWidth, spacing, width As Double

		charWidth = 8
		spacing = 2

		If text.Length = 0
			width = 0
		Else
			width = text.Length*charWidth + (text.Length - 1)*spacing
		End If

		Return width
	End Function


	Public Function GetTextHeight(ByRef text As Char ()) As Double
		Return 13
	End Function


	Public Sub AssertFalse(b As Boolean, ByRef failures As NumberReference)
		If b
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertTrue(b As Boolean, ByRef failures As NumberReference)
		If Not b
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertEquals(a As Double, b As Double, ByRef failures As NumberReference)
		If a <> b
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertBooleansEqual(a As Boolean, b As Boolean, ByRef failures As NumberReference)
		If a <> b
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertCharactersEqual(a As Char, b As Char, ByRef failures As NumberReference)
		If a <> b
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertStringEquals(ByRef a As Char (), ByRef b As Char (), ByRef failures As NumberReference)
		If Not aStringsEqual(a, b)
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertNumberArraysEqual(ByRef a As Double (), ByRef b As Double (), ByRef failures As NumberReference)
		Dim i As Double

		If a.Length = b.Length
			i = 0
			While i < a.Length
				Call AssertEquals(a(i), b(i), failures)
				i = i + 1
			End While
		Else
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertBooleanArraysEqual(ByRef a As Boolean (), ByRef b As Boolean (), ByRef failures As NumberReference)
		Dim i As Double

		If a.Length = b.Length
			i = 0
			While i < a.Length
				Call AssertBooleansEqual(a(i), b(i), failures)
				i = i + 1
			End While
		Else
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Sub AssertStringArraysEqual(ByRef a As StringReference (), ByRef b As StringReference (), ByRef failures As NumberReference)
		Dim i As Double

		If a.Length = b.Length
			i = 0
			While i < a.Length
				Call AssertStringEquals(a(i).stringx, b(i).stringx, failures)
				i = i + 1
			End While
		Else
			failures.numberValue = failures.numberValue + 1
		End If
	End Sub


	Public Function ConvertToPNG(ByRef image As RGBABitmapImage) As Double ()
		Return ConvertToPNGWithOptions(image, 6, false, 0, 0.001)
	End Function


	Public Function ConvertToPNGGrayscale(ByRef image As RGBABitmapImage) As Double ()
		Return ConvertToPNGWithOptions(image, 0, false, 0, 0.001)
	End Function


	Public Function PysicsHeader(pixelsPerMeter As Double) As PHYS
		Dim phys As PHYS

		phys = New PHYS()

		phys.pixelsPerMeter = pixelsPerMeter

		Return phys
	End Function


	Public Function ConvertToPNGWithOptions(ByRef image As RGBABitmapImage, colorType As Double, setPhys As Boolean, pixelsPerMeter As Double, compressionLevel As Double) As Double ()
		Dim png As PNGImage
		Dim pngData, colorData As Double ()

		png = New PNGImage()

		png.signature = PNGSignature()

		png.ihdr = PNGHeader(image, colorType)

		png.physPresent = setPhys
		png.phys = PysicsHeader(pixelsPerMeter)

		If colorType = 6
			colorData = GetPNGColorData(image)
		Else
			colorData = GetPNGColorDataGreyscale(image)
		End If
		png.zlibStruct = ZLibCompressStaticHuffman(colorData, compressionLevel)

		pngData = PNGSerializeChunks(png)

		Return pngData
	End Function


	Public Function PNGSerializeChunks(ByRef png As PNGImage) As Double ()
		Dim length, i, chunkLength As Double
		Dim data As Double ()
		Dim position As NumberReference

		length = png.signature.Length + 12 + PNGHeaderLength() + 12 + PNGIDATLength(png) + 12
		If png.physPresent
			length = length + 4 + 4 + 1 + 12
		End If
		data = New Double (length - 1){}
		position = CreateNumberReference(0)

		' Signature
		i = 0
		While i < png.signature.Length
			Call WriteByte(data, png.signature(i), position)
			i = i + 1
		End While

		' Header
		chunkLength = PNGHeaderLength()
		Call Write4BytesBE(data, chunkLength, position)
		Call WriteStringBytes(data, "IHDR".ToCharArray(), position)
		Call Write4BytesBE(data, png.ihdr.Width, position)
		Call Write4BytesBE(data, png.ihdr.Height, position)
		Call WriteByte(data, png.ihdr.BitDepth, position)
		Call WriteByte(data, png.ihdr.ColourType, position)
		Call WriteByte(data, png.ihdr.CompressionMethod, position)
		Call WriteByte(data, png.ihdr.FilterMethod, position)
		Call WriteByte(data, png.ihdr.InterlaceMethod, position)
		Call Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4, chunkLength + 4), position)

		' pHYs
		If png.physPresent
			chunkLength = 4 + 4 + 1
			Call Write4BytesBE(data, chunkLength, position)
			Call WriteStringBytes(data, "pHYs".ToCharArray(), position)

			Call Write4BytesBE(data, png.phys.pixelsPerMeter, position)
			Call Write4BytesBE(data, png.phys.pixelsPerMeter, position)
			Call WriteByte(data, 1, position)
			' 1 = pixels per meter
			Call Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4, chunkLength + 4), position)
		End If

		' IDAT
		chunkLength = PNGIDATLength(png)
		Call Write4BytesBE(data, chunkLength, position)
		Call WriteStringBytes(data, "IDAT".ToCharArray(), position)
		Call WriteByte(data, png.zlibStruct.CMF, position)
		Call WriteByte(data, png.zlibStruct.FLG, position)
		i = 0
		While i < png.zlibStruct.CompressedDataBlocks.Length
			Call WriteByte(data, png.zlibStruct.CompressedDataBlocks(i), position)
			i = i + 1
		End While
		Call Write4BytesBE(data, png.zlibStruct.Adler32CheckValue, position)
		Call Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4, chunkLength + 4), position)

		' IEND
		chunkLength = 0
		Call Write4BytesBE(data, chunkLength, position)
		Call WriteStringBytes(data, "IEND".ToCharArray(), position)
		Call Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - 4, 4), position)

		Return data
	End Function


	Public Function PNGIDATLength(ByRef png As PNGImage) As Double
		Return 2 + png.zlibStruct.CompressedDataBlocks.Length + 4
	End Function


	Public Function PNGHeaderLength() As Double
		Return 4 + 4 + 1 + 1 + 1 + 1 + 1
	End Function


	Public Function GetPNGColorData(ByRef image As RGBABitmapImage) As Double ()
		Dim colordata As Double ()
		Dim length, x, y, nextx As Double
		Dim rgba As RGBA

		length = 4*ImageWidth(image)*ImageHeight(image) + ImageHeight(image)

		colordata = New Double (length - 1){}

		nextx = 0

		y = 0
		While y < ImageHeight(image)
			colordata(nextx) = 0
			nextx = nextx + 1
			x = 0
			While x < ImageWidth(image)
				rgba = image.x(x).y(y)
				colordata(nextx) = Roundx(rgba.r*255)
				nextx = nextx + 1
				colordata(nextx) = Roundx(rgba.g*255)
				nextx = nextx + 1
				colordata(nextx) = Roundx(rgba.b*255)
				nextx = nextx + 1
				colordata(nextx) = Roundx(rgba.a*255)
				nextx = nextx + 1
				x = x + 1
			End While
			y = y + 1
		End While

		Return colordata
	End Function


	Public Function GetPNGColorDataGreyscale(ByRef image As RGBABitmapImage) As Double ()
		Dim colordata As Double ()
		Dim length, x, y, nextx As Double
		Dim rgba As RGBA

		length = ImageWidth(image)*ImageHeight(image) + ImageHeight(image)

		colordata = New Double (length - 1){}

		nextx = 0

		y = 0
		While y < ImageHeight(image)
			colordata(nextx) = 0
			nextx = nextx + 1
			x = 0
			While x < ImageWidth(image)
				rgba = image.x(x).y(y)
				colordata(nextx) = Roundx(rgba.r*255)
				nextx = nextx + 1
				x = x + 1
			End While
			y = y + 1
		End While

		Return colordata
	End Function


	Public Function PNGHeader(ByRef image As RGBABitmapImage, colortype As Double) As IHDR
		Dim ihdr As IHDR

		ihdr = New IHDR()
		ihdr.Width = ImageWidth(image)
		ihdr.Height = ImageHeight(image)
		' Truecolour with alpha
		ihdr.BitDepth = 8
		ihdr.ColourType = colortype
		ihdr.FilterMethod = 0
		' None
		ihdr.CompressionMethod = 0
		' zlib
		ihdr.InterlaceMethod = 0
		' no interlace
		Return ihdr
	End Function


	Public Function PNGSignature() As Double ()
		Dim s As Double ()

		s = New Double (8 - 1){}
		s(0) = 137
		s(1) = 80
		s(2) = 78
		s(3) = 71
		s(4) = 13
		s(5) = 10
		s(6) = 26
		s(7) = 10

		Return s
	End Function


	Public Function PNGReadDataChunks(ByRef cs As Chunk ()) As Double ()
		Dim i, j, length, zlibpos As Double
		Dim c As Chunk
		Dim zlibData As Double ()

		length = 0
		i = 0
		While i < cs.Length
			c = cs(i)
			If aStringsEqual(c.type, "IDAT".ToCharArray())
				length = length + c.length
			End If
			i = i + 1
		End While

		zlibData = New Double (length - 1){}
		zlibpos = 0

		i = 0
		While i < cs.Length
			c = cs(i)
			If aStringsEqual(c.type, "IDAT".ToCharArray())
				j = 0
				While j < c.length
					zlibData(zlibpos) = c.data(j)
					zlibpos = zlibpos + 1
					j = j + 1
				End While
			End If
			i = i + 1
		End While

		Return zlibData
	End Function


	Public Function PNGReadHeader(ByRef image As RGBABitmapImage, ByRef cs As Chunk (), ByRef errorMessages As StringReference) As Boolean
		Dim i As Double
		Dim ihdr As IHDR
		Dim c As Chunk
		Dim position As NumberReference
		Dim n As RGBABitmapImage
		Dim success As Boolean

		position = CreateNumberReference(0)
		success = false

		i = 0
		While i < cs.Length
			c = cs(i)
			If aStringsEqual(c.type, "IHDR".ToCharArray())
				ihdr = New IHDR()

				ihdr.Width = Read4bytesBE(c.data, position)
				ihdr.Height = Read4bytesBE(c.data, position)
				ihdr.BitDepth = ReadByte(c.data, position)
				ihdr.ColourType = ReadByte(c.data, position)
				ihdr.CompressionMethod = ReadByte(c.data, position)
				ihdr.FilterMethod = ReadByte(c.data, position)
				ihdr.InterlaceMethod = ReadByte(c.data, position)

				n = CreateImage(ihdr.Width, ihdr.Height, GetTransparent())
				image.x = n.x

				If ihdr.ColourType = 6
					If ihdr.BitDepth = 8
						If ihdr.CompressionMethod = 0
							If ihdr.FilterMethod = 0
								If ihdr.InterlaceMethod = 0
									success = true
								Else
									success = false
									errorMessages.stringx = AppendString(errorMessages.stringx, "Interlace method not supported.".ToCharArray())
								End If
							Else
								success = false
								errorMessages.stringx = AppendString(errorMessages.stringx, "Filter method not supported.".ToCharArray())
							End If
						Else
							success = false
							errorMessages.stringx = AppendString(errorMessages.stringx, "Compression type not supported.".ToCharArray())
						End If
					Else
						success = false
						errorMessages.stringx = AppendString(errorMessages.stringx, "Bit depth not supported.".ToCharArray())
					End If
				Else
					success = false
					errorMessages.stringx = AppendString(errorMessages.stringx, "Color type not supported.".ToCharArray())
				End If
			End If
			i = i + 1
		End While

		Return success
	End Function


	Public Function PNGReadChunks(ByRef data As Double (), ByRef position As NumberReference) As Chunk ()
		Dim done As Boolean
		Dim prepos As Double
		Dim chunks As Double
		Dim c As Chunk
		Dim cs As Chunk ()
		Dim i As Double
		done = false
		prepos = position.numberValue
		chunks = 0
		While Not done
			c = PNGReadChunk(data, position)
			If aStringsEqual(c.type, "IEND".ToCharArray())
				done = true
			End If
			chunks = chunks + 1
		End While
		position.numberValue = prepos
		cs = New Chunk (chunks - 1){}
		i = 0
		While i < chunks
			cs(i) = PNGReadChunk(data, position)
			i = i + 1
		End While

		Return cs
	End Function


	Public Function PNGReadChunk(ByRef data As Double (), ByRef position As NumberReference) As Chunk
		Dim c As Chunk

		c = New Chunk()

		c.length = Read4bytesBE(data, position)
		c.type = New Char (4 - 1){}
		c.type(0) = Convert.ToChar(Convert.ToInt64(ReadByte(data, position)))
		c.type(1) = Convert.ToChar(Convert.ToInt64(ReadByte(data, position)))
		c.type(2) = Convert.ToChar(Convert.ToInt64(ReadByte(data, position)))
		c.type(3) = Convert.ToChar(Convert.ToInt64(ReadByte(data, position)))
		c.data = ReadXbytes(data, position, c.length)
		c.crc = Read4bytesBE(data, position)

		Return c
	End Function


	Public Sub WriteStringToStingStream(ByRef stream As Char (), ByRef index As NumberReference, ByRef src As Char ())
		Dim i As Double

		i = 0
		While i < src.Length
			stream(index.numberValue + i) = src(i)
			i = i + 1
		End While
		index.numberValue = index.numberValue + src.Length
	End Sub


	Public Sub WriteCharacterToStingStream(ByRef stream As Char (), ByRef index As NumberReference, src As Char)
		stream(index.numberValue) = src
		index.numberValue = index.numberValue + 1
	End Sub


	Public Sub WriteBooleanToStingStream(ByRef stream As Char (), ByRef index As NumberReference, src As Boolean)
		If src
			Call WriteStringToStingStream(stream, index, "true".ToCharArray())
		Else
			Call WriteStringToStingStream(stream, index, "false".ToCharArray())
		End If
	End Sub


	Public Function SubstringWithCheck(ByRef stringx As Char (), from As Double, tox As Double, ByRef stringReference As StringReference) As Boolean
		Dim success As Boolean

		If from >= 0 And from <= stringx.Length And tox >= 0 And tox <= stringx.Length And from <= tox
			stringReference.stringx = Substring(stringx, from, tox)
			success = true
		Else
			success = false
		End If

		Return success
	End Function


	Public Function Substring(ByRef stringx As Char (), from As Double, tox As Double) As Char ()
		Dim n As Char ()
		Dim i, length As Double

		length = tox - from

		n = New Char (length - 1){}

		i = from
		While i < tox
			n(i - from) = stringx(i)
			i = i + 1
		End While

		Return n
	End Function


	Public Function AppendString(ByRef s1 As Char (), ByRef s2 As Char ()) As Char ()
		Dim newString As Char ()

		newString = ConcatenateString(s1, s2)

		Erase s1 

		Return newString
	End Function


	Public Function ConcatenateString(ByRef s1 As Char (), ByRef s2 As Char ()) As Char ()
		Dim newString As Char ()
		Dim i As Double

		newString = New Char (s1.Length + s2.Length - 1){}

		i = 0
		While i < s1.Length
			newString(i) = s1(i)
			i = i + 1
		End While

		i = 0
		While i < s2.Length
			newString(s1.Length + i) = s2(i)
			i = i + 1
		End While

		Return newString
	End Function


	Public Function AppendCharacter(ByRef stringx As Char (), c As Char) As Char ()
		Dim newString As Char ()

		newString = ConcatenateCharacter(stringx, c)

		Erase stringx 

		Return newString
	End Function


	Public Function ConcatenateCharacter(ByRef stringx As Char (), c As Char) As Char ()
		Dim newString As Char ()
		Dim i As Double
		newString = New Char (stringx.Length + 1 - 1){}

		i = 0
		While i < stringx.Length
			newString(i) = stringx(i)
			i = i + 1
		End While

		newString(stringx.Length) = c

		Return newString
	End Function


	Public Function SplitByCharacter(ByRef toSplit As Char (), splitBy As Char) As StringReference ()
		Dim split As StringReference ()
		Dim stringToSplitBy As Char ()

		stringToSplitBy = New Char (1 - 1){}
		stringToSplitBy(0) = splitBy

		split = SplitByString(toSplit, stringToSplitBy)

		Erase stringToSplitBy 

		Return split
	End Function


	Public Function IndexOfCharacter(ByRef stringx As Char (), character As Char, ByRef indexReference As NumberReference) As Boolean
		Dim i As Double
		Dim found As Boolean

		found = false
		i = 0
		While i < stringx.Length And Not found
			If stringx(i) = character
				found = true
				indexReference.numberValue = i
			End If
			i = i + 1
		End While

		Return found
	End Function


	Public Function SubstringEqualsWithCheck(ByRef stringx As Char (), from As Double, ByRef substring As Char (), ByRef equalsReference As BooleanReference) As Boolean
		Dim success As Boolean

		If from < stringx.Length
			success = true
			equalsReference.booleanValue = SubstringEquals(stringx, from, substring)
		Else
			success = false
		End If

		Return success
	End Function


	Public Function SubstringEquals(ByRef stringx As Char (), from As Double, ByRef substring As Char ()) As Boolean
		Dim i As Double
		Dim equal As Boolean

		equal = true
		If stringx.Length - from >= substring.Length
			i = 0
			While i < substring.Length And equal
				If stringx(from + i) <> substring(i)
					equal = false
				End If
				i = i + 1
			End While
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Function IndexOfString(ByRef stringx As Char (), ByRef substring As Char (), ByRef indexReference As NumberReference) As Boolean
		Dim i As Double
		Dim found As Boolean

		found = false
		i = 0
		While i < stringx.Length - substring.Length + 1 And Not found
			If SubstringEquals(stringx, i, substring)
				found = true
				indexReference.numberValue = i
			End If
			i = i + 1
		End While

		Return found
	End Function


	Public Function ContainsCharacter(ByRef stringx As Char (), character As Char) As Boolean
		Dim i As Double
		Dim found As Boolean

		found = false
		i = 0
		While i < stringx.Length And Not found
			If stringx(i) = character
				found = true
			End If
			i = i + 1
		End While

		Return found
	End Function


	Public Function ContainsString(ByRef stringx As Char (), ByRef substring As Char ()) As Boolean
		Return IndexOfString(stringx, substring, New NumberReference())
	End Function


	Public Sub ToUpperCase(ByRef stringx As Char ())
		Dim i As Double

		i = 0
		While i < stringx.Length
			stringx(i) = charToUpperCase(stringx(i))
			i = i + 1
		End While
	End Sub


	Public Sub ToLowerCase(ByRef stringx As Char ())
		Dim i As Double

		i = 0
		While i < stringx.Length
			stringx(i) = charToLowerCase(stringx(i))
			i = i + 1
		End While
	End Sub


	Public Function EqualsIgnoreCase(ByRef a As Char (), ByRef b As Char ()) As Boolean
		Dim equal As Boolean
		Dim i As Double

		If a.Length = b.Length
			equal = true
			i = 0
			While i < a.Length And equal
				If charToLowerCase(a(i)) <> charToLowerCase(b(i))
					equal = false
				End If
				i = i + 1
			End While
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Function ReplaceString(ByRef stringx As Char (), ByRef toReplace As Char (), ByRef replaceWith As Char ()) As Char ()
		Dim result As Char ()
		Dim i As Double
		Dim equalsReference As BooleanReference
		Dim success As Boolean

		equalsReference = New BooleanReference()
		result = New Char (0 - 1){}

		i = 0
		While i < stringx.Length
			success = SubstringEqualsWithCheck(stringx, i, toReplace, equalsReference)
			If success
				success = equalsReference.booleanValue
			End If

			If success And toReplace.Length > 0
				result = ConcatenateString(result, replaceWith)
				i = i + toReplace.Length
			Else
				result = ConcatenateCharacter(result, stringx(i))
				i = i + 1
			End If
		End While

		Return result
	End Function


	Public Function ReplaceCharacter(ByRef stringx As Char (), toReplace As Char, replaceWith As Char) As Char ()
		Dim result As Char ()
		Dim i As Double

		result = New Char (0 - 1){}

		i = 0
		While i < stringx.Length
			If stringx(i) = toReplace
				result = ConcatenateCharacter(result, replaceWith)
			Else
				result = ConcatenateCharacter(result, stringx(i))
			End If
			i = i + 1
		End While

		Return result
	End Function


	Public Function Trim(ByRef stringx As Char ()) As Char ()
		Dim result As Char ()
		Dim i, lastWhitespaceLocationStart, lastWhitespaceLocationEnd As Double
		Dim firstNonWhitespaceFound As Boolean

		' Find whitepaces at the start.
		lastWhitespaceLocationStart = -1
		firstNonWhitespaceFound = false
		i = 0
		While i < stringx.Length And Not firstNonWhitespaceFound
			If charIsWhiteSpace(stringx(i))
				lastWhitespaceLocationStart = i
			Else
				firstNonWhitespaceFound = true
			End If
			i = i + 1
		End While

		' Find whitepaces at the end.
		lastWhitespaceLocationEnd = stringx.Length
		firstNonWhitespaceFound = false
		i = stringx.Length - 1
		While i >= 0 And Not firstNonWhitespaceFound
			If charIsWhiteSpace(stringx(i))
				lastWhitespaceLocationEnd = i
			Else
				firstNonWhitespaceFound = true
			End If
			i = i - 1
		End While

		If lastWhitespaceLocationStart < lastWhitespaceLocationEnd
			result = Substring(stringx, lastWhitespaceLocationStart + 1, lastWhitespaceLocationEnd)
		Else
			result = New Char (0 - 1){}
		End If

		Return result
	End Function


	Public Function StartsWith(ByRef stringx As Char (), ByRef start As Char ()) As Boolean
		Dim startsWithString As Boolean

		startsWithString = false
		If stringx.Length >= start.Length
			startsWithString = SubstringEquals(stringx, 0, start)
		End If

		Return startsWithString
	End Function


	Public Function EndsWith(ByRef stringx As Char (), ByRef endx As Char ()) As Boolean
		Dim endsWithString As Boolean

		endsWithString = false
		If stringx.Length >= endx.Length
			endsWithString = SubstringEquals(stringx, stringx.Length - endx.Length, endx)
		End If

		Return endsWithString
	End Function


	Public Function SplitByString(ByRef toSplit As Char (), ByRef splitBy As Char ()) As StringReference ()
		Dim split As StringReference ()
		Dim nextx As Char ()
		Dim i As Double
		Dim c As Char
		Dim n As StringReference

		split = New StringReference (0 - 1){}

		nextx = New Char (0 - 1){}
		i = 0
		While i < toSplit.Length
			c = toSplit(i)

			If SubstringEquals(toSplit, i, splitBy)
				n = New StringReference()
				n.stringx = nextx
				split = AddString(split, n)
				nextx = New Char (0 - 1){}
				i = i + splitBy.Length
			Else
				nextx = AppendCharacter(nextx, c)
				i = i + 1
			End If
		End While

		n = New StringReference()
		n.stringx = nextx
		split = AddString(split, n)

		Return split
	End Function


	Public Function StringIsBefore(ByRef a As Char (), ByRef b As Char ()) As Boolean
		Dim before, equal, done As Boolean
		Dim i As Double

		before = false
		equal = true
		done = false

		If a.Length = 0 And b.Length > 0
			before = true
		Else
			i = 0
			While i < a.Length And i < b.Length And Not done
				If a(i) <> b(i)
					equal = false
				End If
				If charCharacterIsBefore(a(i), b(i))
					before = true
				End If
				If charCharacterIsBefore(b(i), a(i))
					done = true
				End If
				i = i + 1
			End While

			If equal
				If a.Length < b.Length
					before = true
				End If
			End If
		End If

		Return before
	End Function


	Public Function ReadXbytes(ByRef data As Double (), ByRef position As NumberReference, length As Double) As Double ()
		Dim r As Double ()
		Dim i As Double

		r = New Double (length - 1){}

		i = 0
		While i < length
			r(i) = ReadByte(data, position)
			i = i + 1
		End While

		Return r
	End Function


	Public Function Read4bytesBE(ByRef data As Double (), ByRef position As NumberReference) As Double
		Dim r As Double

		r = 0
		r = r + 2 ^ 24*ReadByte(data, position)
		r = r + 2 ^ 16*ReadByte(data, position)
		r = r + 2 ^ 8*ReadByte(data, position)
		r = r + ReadByte(data, position)

		Return r
	End Function


	Public Function Read2bytesBE(ByRef data As Double (), ByRef position As NumberReference) As Double
		Dim r As Double

		r = 0
		r = r + 2 ^ 8*ReadByte(data, position)
		r = r + ReadByte(data, position)

		Return r
	End Function


	Public Function ReadByte(ByRef data As Double (), ByRef position As NumberReference) As Double
		Dim nextx As Double

		nextx = data(position.numberValue)
		position.numberValue = position.numberValue + 1

		Return nextx
	End Function


	Public Function Read4bytesLE(ByRef data As Double (), ByRef position As NumberReference) As Double
		Dim r As Double

		r = 0
		r = r + ReadByte(data, position)
		r = r + 2 ^ 8*ReadByte(data, position)
		r = r + 2 ^ 16*ReadByte(data, position)
		r = r + 2 ^ 24*ReadByte(data, position)

		Return r
	End Function


	Public Sub WriteByte(ByRef data As Double (), b As Double, ByRef position As NumberReference)
		data(position.numberValue) = b
		position.numberValue = position.numberValue + 1
	End Sub


	Public Sub Write2BytesLE(ByRef data As Double (), b As Double, ByRef position As NumberReference)
		data(position.numberValue) = Roundx(b Mod 2 ^ 8)
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Floor(b/2 ^ 8) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
	End Sub


	Public Sub Write4BytesLE(ByRef data As Double (), b As Double, ByRef position As NumberReference)
		data(position.numberValue) = Roundx(b Mod 2 ^ 8)
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Floor(b/2 ^ 8) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Floor(b/2 ^ 16) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Floor(b/2 ^ 24) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
	End Sub


	Public Sub Write2BytesBE(ByRef data As Double (), b As Double, ByRef position As NumberReference)
		data(position.numberValue) = Floor(b/2 ^ 8) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Roundx(b Mod 2 ^ 8)
		position.numberValue = position.numberValue + 1
	End Sub


	Public Sub Write4BytesBE(ByRef data As Double (), b As Double, ByRef position As NumberReference)
		data(position.numberValue) = Floor(b/2 ^ 24) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Floor(b/2 ^ 16) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Floor(b/2 ^ 8) Mod 2 ^ 8
		position.numberValue = position.numberValue + 1
		data(position.numberValue) = Roundx(b Mod 2 ^ 8)
		position.numberValue = position.numberValue + 1
	End Sub


	Public Sub WriteStringBytes(ByRef data As Double (), ByRef cs As Char (), ByRef position As NumberReference)
		Dim i, v As Double

		i = 0
		While i < cs.Length
			v = Convert.ToInt16(cs(i))
			Call WriteByte(data, v, position)
			i = i + 1
		End While
	End Sub


	Public Function MakeCRC32Table() As Double ()
		Dim c, n, k As Double
		Dim crcTable As Double ()

		crcTable = New Double (256 - 1){}

		n = 0
		While n < 256
			c = n
			k = 0
			While k < 8
				If Not DivisibleBy(c, 2)
					c = Xor4Byte(3988292384, Floor(c/2))
				Else
					c = Floor(c/2)
				End If
				k = k + 1
			End While
			crcTable(n) = c
			n = n + 1
		End While

		Return crcTable
	End Function


	Public Function UpdateCRC32(crc As Double, ByRef buf As Double (), ByRef crc_table As Double ()) As Double
		Dim n, index As Double

		n = 0
		While n < buf.Length
			index = And4Byte(Xor4Byte(crc, buf(n)), 2 ^ 8 - 1)
			crc = Xor4Byte(crc_table(index), Floor(crc/2 ^ 8))
			n = n + 1
		End While

		Return crc
	End Function


	Public Function CalculateCRC32(ByRef buf As Double ()) As Double
		Dim crcTable As Double ()
		Dim b32max, value As Double

		crcTable = MakeCRC32Table()

		b32max = 2 ^ 32 - 1
		value = UpdateCRC32(b32max, buf, crcTable)

		Return Xor4Byte(value, b32max)
	End Function


	Public Function CRC32OfInterval(ByRef data As Double (), from As Double, length As Double) As Double
		Dim crcBase As Double ()
		Dim i, crc As Double

		crcBase = New Double (length - 1){}

		i = 0
		While i < length
			crcBase(i) = data(from + i)
			i = i + 1
		End While

		crc = CalculateCRC32(crcBase)

		Erase crcBase 

		Return crc
	End Function


	Public Function ZLibCompressNoCompression(ByRef data As Double ()) As ZLIBStruct
		Dim zlibStruct As ZLIBStruct

		zlibStruct = New ZLIBStruct()

		zlibStruct.CMF = 120
		zlibStruct.FLG = 1
		zlibStruct.CompressedDataBlocks = DeflateDataNoCompression(data)
		zlibStruct.Adler32CheckValue = ComputeAdler32(data)

		Return zlibStruct
	End Function


	Public Function ZLibCompressStaticHuffman(ByRef data As Double (), level As Double) As ZLIBStruct
		Dim zlibStruct As ZLIBStruct

		zlibStruct = New ZLIBStruct()

		zlibStruct.CMF = 120
		zlibStruct.FLG = 1
		zlibStruct.CompressedDataBlocks = DeflateDataStaticHuffman(data, level)
		zlibStruct.Adler32CheckValue = ComputeAdler32(data)

		Return zlibStruct
	End Function


	Public Function AddNumber(ByRef list As Double (), a As Double) As Double ()
		Dim newlist As Double ()
		Dim i As Double

		newlist = New Double (list.Length + 1 - 1){}
		i = 0
		While i < list.Length
			newlist(i) = list(i)
			i = i + 1
		End While
		newlist(list.Length) = a
		
		Erase list 
		
		Return newlist
	End Function


	Public Sub AddNumberRef(ByRef list As NumberArrayReference, i As Double)
		list.numberArray = AddNumber(list.numberArray, i)
	End Sub


	Public Function RemoveNumber(ByRef list As Double (), n As Double) As Double ()
		Dim newlist As Double ()
		Dim i As Double

		newlist = New Double (list.Length - 1 - 1){}

		If n >= 0 And n < list.Length
			i = 0
			While i < list.Length
				If i < n
					newlist(i) = list(i)
				End If
				If i > n
					newlist(i - 1) = list(i)
				End If
				i = i + 1
			End While

			Erase list 
		Else
			Erase newlist 
		End If
		
		Return newlist
	End Function


	Public Function GetNumberRef(ByRef list As NumberArrayReference, i As Double) As Double
		Return list.numberArray(i)
	End Function


	Public Sub RemoveNumberRef(ByRef list As NumberArrayReference, i As Double)
		list.numberArray = RemoveNumber(list.numberArray, i)
	End Sub


	Public Function AddString(ByRef list As StringReference (), ByRef a As StringReference) As StringReference ()
		Dim newlist As StringReference ()
		Dim i As Double

		newlist = New StringReference (list.Length + 1 - 1){}

		i = 0
		While i < list.Length
			newlist(i) = list(i)
			i = i + 1
		End While
		newlist(list.Length) = a
		
		Erase list 
		
		Return newlist
	End Function


	Public Sub AddStringRef(ByRef list As StringArrayReference, ByRef i As StringReference)
		list.stringArray = AddString(list.stringArray, i)
	End Sub


	Public Function RemoveString(ByRef list As StringReference (), n As Double) As StringReference ()
		Dim newlist As StringReference ()
		Dim i As Double

		newlist = New StringReference (list.Length - 1 - 1){}

		If n >= 0 And n < list.Length
			i = 0
			While i < list.Length
				If i < n
					newlist(i) = list(i)
				End If
				If i > n
					newlist(i - 1) = list(i)
				End If
				i = i + 1
			End While

			Erase list 
		Else
			Erase newlist 
		End If
		
		Return newlist
	End Function


	Public Function GetStringRef(ByRef list As StringArrayReference, i As Double) As StringReference
		Return list.stringArray(i)
	End Function


	Public Sub RemoveStringRef(ByRef list As StringArrayReference, i As Double)
		list.stringArray = RemoveString(list.stringArray, i)
	End Sub


	Public Function AddBoolean(ByRef list As Boolean (), a As Boolean) As Boolean ()
		Dim newlist As Boolean ()
		Dim i As Double

		newlist = New Boolean (list.Length + 1 - 1){}
		i = 0
		While i < list.Length
			newlist(i) = list(i)
			i = i + 1
		End While
		newlist(list.Length) = a
		
		Erase list 
		
		Return newlist
	End Function


	Public Sub AddBooleanRef(ByRef list As BooleanArrayReference, i As Boolean)
		list.booleanArray = AddBoolean(list.booleanArray, i)
	End Sub


	Public Function RemoveBoolean(ByRef list As Boolean (), n As Double) As Boolean ()
		Dim newlist As Boolean ()
		Dim i As Double

		newlist = New Boolean (list.Length - 1 - 1){}

		If n >= 0 And n < list.Length
			i = 0
			While i < list.Length
				If i < n
					newlist(i) = list(i)
				End If
				If i > n
					newlist(i - 1) = list(i)
				End If
				i = i + 1
			End While

			Erase list 
		Else
			Erase newlist 
		End If
		
		Return newlist
	End Function


	Public Function GetBooleanRef(ByRef list As BooleanArrayReference, i As Double) As Boolean
		Return list.booleanArray(i)
	End Function


	Public Sub RemoveDecimalRef(ByRef list As BooleanArrayReference, i As Double)
		list.booleanArray = RemoveBoolean(list.booleanArray, i)
	End Sub


	Public Function CreateLinkedListString() As LinkedListStrings
		Dim ll As LinkedListStrings

		ll = New LinkedListStrings()
		ll.first = New LinkedListNodeStrings()
		ll.last = ll.first
		ll.last.endx = true

		Return ll
	End Function


	Public Sub LinkedListAddString(ByRef ll As LinkedListStrings, ByRef value As Char ())
		ll.last.endx = false
		ll.last.value = value
		ll.last.nextx = New LinkedListNodeStrings()
		ll.last.nextx.endx = true
		ll.last = ll.last.nextx
	End Sub


	Public Function LinkedListStringsToArray(ByRef ll As LinkedListStrings) As StringReference ()
		Dim arrayx As StringReference ()
		Dim length, i As Double
		Dim node As LinkedListNodeStrings

		node = ll.first

		length = LinkedListStringsLength(ll)

		arrayx = New StringReference (length - 1){}

		i = 0
		While i < length
			arrayx(i) = New StringReference()
			arrayx(i).stringx = node.value
			node = node.nextx
			i = i + 1
		End While

		Return arrayx
	End Function


	Public Function LinkedListStringsLength(ByRef ll As LinkedListStrings) As Double
		Dim l As Double
		Dim node As LinkedListNodeStrings

		l = 0
		node = ll.first
		
		While Not node.endx
			node = node.nextx
			l = l + 1
		End While

		Return l
	End Function


	Public Sub FreeLinkedListString(ByRef ll As LinkedListStrings)
		Dim node, prev As LinkedListNodeStrings

		node = ll.first

		
		While Not node.endx
			prev = node
			node = node.nextx
			prev = Nothing
		End While

		node = Nothing
	End Sub


	Public Function CreateLinkedListNumbers() As LinkedListNumbers
		Dim ll As LinkedListNumbers

		ll = New LinkedListNumbers()
		ll.first = New LinkedListNodeNumbers()
		ll.last = ll.first
		ll.last.endx = true

		Return ll
	End Function


	Public Function CreateLinkedListNumbersArray(length As Double) As LinkedListNumbers ()
		Dim lls As LinkedListNumbers ()
		Dim i As Double

		lls = New LinkedListNumbers (length - 1){}
		i = 0
		While i < lls.Length
			lls(i) = CreateLinkedListNumbers()
			i = i + 1
		End While

		Return lls
	End Function


	Public Sub LinkedListAddNumber(ByRef ll As LinkedListNumbers, value As Double)
		ll.last.endx = false
		ll.last.value = value
		ll.last.nextx = New LinkedListNodeNumbers()
		ll.last.nextx.endx = true
		ll.last = ll.last.nextx
	End Sub


	Public Function LinkedListNumbersLength(ByRef ll As LinkedListNumbers) As Double
		Dim l As Double
		Dim node As LinkedListNodeNumbers

		l = 0
		node = ll.first
		
		While Not node.endx
			node = node.nextx
			l = l + 1
		End While

		Return l
	End Function


	Public Function LinkedListNumbersIndex(ByRef ll As LinkedListNumbers, index As Double) As Double
		Dim i As Double
		Dim node As LinkedListNodeNumbers

		node = ll.first
		i = 0
		While i < index
			node = node.nextx
			i = i + 1
		End While

		Return node.value
	End Function


	Public Sub LinkedListInsertNumber(ByRef ll As LinkedListNumbers, index As Double, value As Double)
		Dim i As Double
		Dim node, tmp As LinkedListNodeNumbers

		If index = 0
			tmp = ll.first
			ll.first = New LinkedListNodeNumbers()
			ll.first.nextx = tmp
			ll.first.value = value
			ll.first.endx = false
		Else
			node = ll.first
			i = 0
			While i < index - 1
				node = node.nextx
				i = i + 1
			End While

			tmp = node.nextx
			node.nextx = New LinkedListNodeNumbers()
			node.nextx.nextx = tmp
			node.nextx.value = value
			node.nextx.endx = false
		End If
	End Sub


	Public Sub LinkedListSet(ByRef ll As LinkedListNumbers, index As Double, value As Double)
		Dim i As Double
		Dim node As LinkedListNodeNumbers

		node = ll.first
		i = 0
		While i < index
			node = node.nextx
			i = i + 1
		End While

		node.nextx.value = value
	End Sub


	Public Sub LinkedListRemoveNumber(ByRef ll As LinkedListNumbers, index As Double)
		Dim i As Double
		Dim node, prev As LinkedListNodeNumbers

		node = ll.first
		prev = ll.first

		i = 0
		While i < index
			prev = node
			node = node.nextx
			i = i + 1
		End While

		If index = 0
			ll.first = prev.nextx
		End If
		If Not prev.nextx.endx
			prev.nextx = prev.nextx.nextx
		End If
	End Sub


	Public Sub FreeLinkedListNumbers(ByRef ll As LinkedListNumbers)
		Dim node, prev As LinkedListNodeNumbers

		node = ll.first

		
		While Not node.endx
			prev = node
			node = node.nextx
			prev = Nothing
		End While

		node = Nothing
	End Sub


	Public Sub FreeLinkedListNumbersArray(ByRef lls As LinkedListNumbers ())
		Dim i As Double

		i = 0
		While i < lls.Length
			Call FreeLinkedListNumbers(lls(i))
			i = i + 1
		End While
		Erase lls 
	End Sub


	Public Function LinkedListNumbersToArray(ByRef ll As LinkedListNumbers) As Double ()
		Dim arrayx As Double ()
		Dim length, i As Double
		Dim node As LinkedListNodeNumbers

		node = ll.first

		length = LinkedListNumbersLength(ll)

		arrayx = New Double (length - 1){}

		i = 0
		While i < length
			arrayx(i) = node.value
			node = node.nextx
			i = i + 1
		End While

		Return arrayx
	End Function


	Public Function ArrayToLinkedListNumbers(ByRef arrayx As Double ()) As LinkedListNumbers
		Dim ll As LinkedListNumbers
		Dim i As Double

		ll = CreateLinkedListNumbers()

		i = 0
		While i < arrayx.Length
			Call LinkedListAddNumber(ll, arrayx(i))
			i = i + 1
		End While

		Return ll
	End Function


	Public Function LinkedListNumbersEqual(ByRef a As LinkedListNumbers, ByRef b As LinkedListNumbers) As Boolean
		Dim equal, done As Boolean
		Dim an, bn As LinkedListNodeNumbers

		an = a.first
		bn = b.first

		equal = true
		done = false
		
		While equal And Not done
			If an.endx = bn.endx
				If an.endx
					done = true
				ElseIf an.value = bn.value
					an = an.nextx
					bn = bn.nextx
				Else
					equal = false
				End If
			Else
				equal = false
			End If
		End While

		Return equal
	End Function


	Public Function CreateLinkedListCharacter() As LinkedListCharacters
		Dim ll As LinkedListCharacters

		ll = New LinkedListCharacters()
		ll.first = New LinkedListNodeCharacters()
		ll.last = ll.first
		ll.last.endx = true

		Return ll
	End Function


	Public Sub LinkedListAddCharacter(ByRef ll As LinkedListCharacters, value As Char)
		ll.last.endx = false
		ll.last.value = value
		ll.last.nextx = New LinkedListNodeCharacters()
		ll.last.nextx.endx = true
		ll.last = ll.last.nextx
	End Sub


	Public Function LinkedListCharactersToArray(ByRef ll As LinkedListCharacters) As Char ()
		Dim arrayx As Char ()
		Dim length, i As Double
		Dim node As LinkedListNodeCharacters

		node = ll.first

		length = LinkedListCharactersLength(ll)

		arrayx = New Char (length - 1){}

		i = 0
		While i < length
			arrayx(i) = node.value
			node = node.nextx
			i = i + 1
		End While

		Return arrayx
	End Function


	Public Function LinkedListCharactersLength(ByRef ll As LinkedListCharacters) As Double
		Dim l As Double
		Dim node As LinkedListNodeCharacters

		l = 0
		node = ll.first
		
		While Not node.endx
			node = node.nextx
			l = l + 1
		End While

		Return l
	End Function


	Public Sub FreeLinkedListCharacter(ByRef ll As LinkedListCharacters)
		Dim node, prev As LinkedListNodeCharacters

		node = ll.first

		
		While Not node.endx
			prev = node
			node = node.nextx
			prev = Nothing
		End While

		node = Nothing
	End Sub


	Public Function CreateDynamicArrayNumbers() As DynamicArrayNumbers
		Dim da As DynamicArrayNumbers

		da = New DynamicArrayNumbers()
		da.arrayx = New Double (10 - 1){}
		da.length = 0

		Return da
	End Function


	Public Function CreateDynamicArrayNumbersWithInitialCapacity(capacity As Double) As DynamicArrayNumbers
		Dim da As DynamicArrayNumbers

		da = New DynamicArrayNumbers()
		da.arrayx = New Double (capacity - 1){}
		da.length = 0

		Return da
	End Function


	Public Sub DynamicArrayAddNumber(ByRef da As DynamicArrayNumbers, value As Double)
		If da.length = da.arrayx.Length
			Call DynamicArrayNumbersIncreaseSize(da)
		End If

		da.arrayx(da.length) = value
		da.length = da.length + 1
	End Sub


	Public Sub DynamicArrayNumbersIncreaseSize(ByRef da As DynamicArrayNumbers)
		Dim newLength, i As Double
		Dim newArray As Double ()

		newLength = Round(da.arrayx.Length*3/2)
		newArray = New Double (newLength - 1){}

		i = 0
		While i < da.arrayx.Length
			newArray(i) = da.arrayx(i)
			i = i + 1
		End While

		Erase da.arrayx 

		da.arrayx = newArray
	End Sub


	Public Function DynamicArrayNumbersDecreaseSizeNecessary(ByRef da As DynamicArrayNumbers) As Boolean
		Dim needsDecrease As Boolean

		needsDecrease = false

		If da.length > 10
			needsDecrease = da.length <= Round(da.arrayx.Length*2/3)
		End If

		Return needsDecrease
	End Function


	Public Sub DynamicArrayNumbersDecreaseSize(ByRef da As DynamicArrayNumbers)
		Dim newLength, i As Double
		Dim newArray As Double ()

		newLength = Round(da.arrayx.Length*2/3)
		newArray = New Double (newLength - 1){}

		i = 0
		While i < newLength
			newArray(i) = da.arrayx(i)
			i = i + 1
		End While

		Erase da.arrayx 

		da.arrayx = newArray
	End Sub


	Public Function DynamicArrayNumbersIndex(ByRef da As DynamicArrayNumbers, index As Double) As Double
		Return da.arrayx(index)
	End Function


	Public Function DynamicArrayNumbersLength(ByRef da As DynamicArrayNumbers) As Double
		Return da.length
	End Function


	Public Sub DynamicArrayInsertNumber(ByRef da As DynamicArrayNumbers, index As Double, value As Double)
		Dim i As Double

		If da.length = da.arrayx.Length
			Call DynamicArrayNumbersIncreaseSize(da)
		End If

		i = da.length
		While i > index
			da.arrayx(i) = da.arrayx(i - 1)
			i = i - 1
		End While

		da.arrayx(index) = value

		da.length = da.length + 1
	End Sub


	Public Sub DynamicArraySet(ByRef da As DynamicArrayNumbers, index As Double, value As Double)
		da.arrayx(index) = value
	End Sub


	Public Sub DynamicArrayRemoveNumber(ByRef da As DynamicArrayNumbers, index As Double)
		Dim i As Double

		i = index
		While i < da.length - 1
			da.arrayx(i) = da.arrayx(i + 1)
			i = i + 1
		End While

		da.length = da.length - 1

		If DynamicArrayNumbersDecreaseSizeNecessary(da)
			Call DynamicArrayNumbersDecreaseSize(da)
		End If
	End Sub


	Public Sub FreeDynamicArrayNumbers(ByRef da As DynamicArrayNumbers)
		Erase da.arrayx 
		da = Nothing
	End Sub


	Public Function DynamicArrayNumbersToArray(ByRef da As DynamicArrayNumbers) As Double ()
		Dim arrayx As Double ()
		Dim i As Double

		arrayx = New Double (da.length - 1){}

		i = 0
		While i < da.length
			arrayx(i) = da.arrayx(i)
			i = i + 1
		End While

		Return arrayx
	End Function


	Public Function ArrayToDynamicArrayNumbersWithOptimalSize(ByRef arrayx As Double ()) As DynamicArrayNumbers
		Dim da As DynamicArrayNumbers
		Dim i As Double
		Dim c, n, newCapacity As Double

		'
		'         c = 10*(3/2)^n
		'         log(c) = log(10*(3/2)^n)
		'         log(c) = log(10) + log((3/2)^n)
		'         log(c) = 1 + log((3/2)^n)
		'         log(c) - 1 = log((3/2)^n)
		'         log(c) - 1 = n*log(3/2)
		'         n = (log(c) - 1)/log(3/2)
		'        
		c = arrayx.Length
		n = (Log(c) - 1)/Log(3/2)
		newCapacity = Floor(n) + 1

		da = CreateDynamicArrayNumbersWithInitialCapacity(newCapacity)

		i = 0
		While i < arrayx.Length
			da.arrayx(i) = arrayx(i)
			i = i + 1
		End While

		Return da
	End Function


	Public Function ArrayToDynamicArrayNumbers(ByRef arrayx As Double ()) As DynamicArrayNumbers
		Dim da As DynamicArrayNumbers

		da = New DynamicArrayNumbers()
		da.arrayx = aCopyNumberArray(arrayx)
		da.length = arrayx.Length

		Return da
	End Function


	Public Function DynamicArrayNumbersEqual(ByRef a As DynamicArrayNumbers, ByRef b As DynamicArrayNumbers) As Boolean
		Dim equal As Boolean
		Dim i As Double

		equal = true
		If a.length = b.length
			i = 0
			While i < a.length And equal
				If a.arrayx(i) <> b.arrayx(i)
					equal = false
				End If
				i = i + 1
			End While
		Else
			equal = false
		End If

		Return equal
	End Function


	Public Function DynamicArrayNumbersToLinkedList(ByRef da As DynamicArrayNumbers) As LinkedListNumbers
		Dim ll As LinkedListNumbers
		Dim i As Double

		ll = CreateLinkedListNumbers()

		i = 0
		While i < da.length
			Call LinkedListAddNumber(ll, da.arrayx(i))
			i = i + 1
		End While

		Return ll
	End Function


	Public Function LinkedListToDynamicArrayNumbers(ByRef ll As LinkedListNumbers) As DynamicArrayNumbers
		Dim da As DynamicArrayNumbers
		Dim i As Double
		Dim node As LinkedListNodeNumbers

		node = ll.first

		da = New DynamicArrayNumbers()
		da.length = LinkedListNumbersLength(ll)

		da.arrayx = New Double (da.length - 1){}

		i = 0
		While i < da.length
			da.arrayx(i) = node.value
			node = node.nextx
			i = i + 1
		End While

		Return da
	End Function


	Public Function AddCharacter(ByRef list As Char (), a As Char) As Char ()
		Dim newlist As Char ()
		Dim i As Double

		newlist = New Char (list.Length + 1 - 1){}
		i = 0
		While i < list.Length
			newlist(i) = list(i)
			i = i + 1
		End While
		newlist(list.Length) = a
		
		Erase list 
		
		Return newlist
	End Function


	Public Sub AddCharacterRef(ByRef list As StringReference, i As Char)
		list.stringx = AddCharacter(list.stringx, i)
	End Sub


	Public Function RemoveCharacter(ByRef list As Char (), n As Double) As Char ()
		Dim newlist As Char ()
		Dim i As Double

		newlist = New Char (list.Length - 1 - 1){}

		If n >= 0 And n < list.Length
			i = 0
			While i < list.Length
				If i < n
					newlist(i) = list(i)
				End If
				If i > n
					newlist(i - 1) = list(i)
				End If
				i = i + 1
			End While

			Erase list 
		Else
			Erase newlist 
		End If

		Return newlist
	End Function


	Public Function GetCharacterRef(ByRef list As StringReference, i As Double) As Char
		Return list.stringx(i)
	End Function


	Public Sub RemoveCharacterRef(ByRef list As StringReference, i As Double)
		list.stringx = RemoveCharacter(list.stringx, i)
	End Sub


	Public Function charToLowerCase(character As Char) As Char
		Dim toReturn As Char

		toReturn = character
		If character = "A"C
			toReturn = "a"C
		ElseIf character = "B"C
			toReturn = "b"C
		ElseIf character = "C"C
			toReturn = "c"C
		ElseIf character = "D"C
			toReturn = "d"C
		ElseIf character = "E"C
			toReturn = "e"C
		ElseIf character = "F"C
			toReturn = "f"C
		ElseIf character = "G"C
			toReturn = "g"C
		ElseIf character = "H"C
			toReturn = "h"C
		ElseIf character = "I"C
			toReturn = "i"C
		ElseIf character = "J"C
			toReturn = "j"C
		ElseIf character = "K"C
			toReturn = "k"C
		ElseIf character = "L"C
			toReturn = "l"C
		ElseIf character = "M"C
			toReturn = "m"C
		ElseIf character = "N"C
			toReturn = "n"C
		ElseIf character = "O"C
			toReturn = "o"C
		ElseIf character = "P"C
			toReturn = "p"C
		ElseIf character = "Q"C
			toReturn = "q"C
		ElseIf character = "R"C
			toReturn = "r"C
		ElseIf character = "S"C
			toReturn = "s"C
		ElseIf character = "T"C
			toReturn = "t"C
		ElseIf character = "U"C
			toReturn = "u"C
		ElseIf character = "V"C
			toReturn = "v"C
		ElseIf character = "W"C
			toReturn = "w"C
		ElseIf character = "X"C
			toReturn = "x"C
		ElseIf character = "Y"C
			toReturn = "y"C
		ElseIf character = "Z"C
			toReturn = "z"C
		End If

		Return toReturn
	End Function


	Public Function charToUpperCase(character As Char) As Char
		Dim toReturn As Char

		toReturn = character
		If character = "a"C
			toReturn = "A"C
		ElseIf character = "b"C
			toReturn = "B"C
		ElseIf character = "c"C
			toReturn = "C"C
		ElseIf character = "d"C
			toReturn = "D"C
		ElseIf character = "e"C
			toReturn = "E"C
		ElseIf character = "f"C
			toReturn = "F"C
		ElseIf character = "g"C
			toReturn = "G"C
		ElseIf character = "h"C
			toReturn = "H"C
		ElseIf character = "i"C
			toReturn = "I"C
		ElseIf character = "j"C
			toReturn = "J"C
		ElseIf character = "k"C
			toReturn = "K"C
		ElseIf character = "l"C
			toReturn = "L"C
		ElseIf character = "m"C
			toReturn = "M"C
		ElseIf character = "n"C
			toReturn = "N"C
		ElseIf character = "o"C
			toReturn = "O"C
		ElseIf character = "p"C
			toReturn = "P"C
		ElseIf character = "q"C
			toReturn = "Q"C
		ElseIf character = "r"C
			toReturn = "R"C
		ElseIf character = "s"C
			toReturn = "S"C
		ElseIf character = "t"C
			toReturn = "T"C
		ElseIf character = "u"C
			toReturn = "U"C
		ElseIf character = "v"C
			toReturn = "V"C
		ElseIf character = "w"C
			toReturn = "W"C
		ElseIf character = "x"C
			toReturn = "X"C
		ElseIf character = "y"C
			toReturn = "Y"C
		ElseIf character = "z"C
			toReturn = "Z"C
		End If

		Return toReturn
	End Function


	Public Function charIsUpperCase(character As Char) As Boolean
		Dim isUpper As Boolean

		isUpper = false
		If character = "A"C
			isUpper = true
		ElseIf character = "B"C
			isUpper = true
		ElseIf character = "C"C
			isUpper = true
		ElseIf character = "D"C
			isUpper = true
		ElseIf character = "E"C
			isUpper = true
		ElseIf character = "F"C
			isUpper = true
		ElseIf character = "G"C
			isUpper = true
		ElseIf character = "H"C
			isUpper = true
		ElseIf character = "I"C
			isUpper = true
		ElseIf character = "J"C
			isUpper = true
		ElseIf character = "K"C
			isUpper = true
		ElseIf character = "L"C
			isUpper = true
		ElseIf character = "M"C
			isUpper = true
		ElseIf character = "N"C
			isUpper = true
		ElseIf character = "O"C
			isUpper = true
		ElseIf character = "P"C
			isUpper = true
		ElseIf character = "Q"C
			isUpper = true
		ElseIf character = "R"C
			isUpper = true
		ElseIf character = "S"C
			isUpper = true
		ElseIf character = "T"C
			isUpper = true
		ElseIf character = "U"C
			isUpper = true
		ElseIf character = "V"C
			isUpper = true
		ElseIf character = "W"C
			isUpper = true
		ElseIf character = "X"C
			isUpper = true
		ElseIf character = "Y"C
			isUpper = true
		ElseIf character = "Z"C
			isUpper = true
		End If

		Return isUpper
	End Function


	Public Function charIsLowerCase(character As Char) As Boolean
		Dim isLower As Boolean

		isLower = false
		If character = "a"C
			isLower = true
		ElseIf character = "b"C
			isLower = true
		ElseIf character = "c"C
			isLower = true
		ElseIf character = "d"C
			isLower = true
		ElseIf character = "e"C
			isLower = true
		ElseIf character = "f"C
			isLower = true
		ElseIf character = "g"C
			isLower = true
		ElseIf character = "h"C
			isLower = true
		ElseIf character = "i"C
			isLower = true
		ElseIf character = "j"C
			isLower = true
		ElseIf character = "k"C
			isLower = true
		ElseIf character = "l"C
			isLower = true
		ElseIf character = "m"C
			isLower = true
		ElseIf character = "n"C
			isLower = true
		ElseIf character = "o"C
			isLower = true
		ElseIf character = "p"C
			isLower = true
		ElseIf character = "q"C
			isLower = true
		ElseIf character = "r"C
			isLower = true
		ElseIf character = "s"C
			isLower = true
		ElseIf character = "t"C
			isLower = true
		ElseIf character = "u"C
			isLower = true
		ElseIf character = "v"C
			isLower = true
		ElseIf character = "w"C
			isLower = true
		ElseIf character = "x"C
			isLower = true
		ElseIf character = "y"C
			isLower = true
		ElseIf character = "z"C
			isLower = true
		End If

		Return isLower
	End Function


	Public Function charIsLetter(character As Char) As Boolean
		Return charIsUpperCase(character) Or charIsLowerCase(character)
	End Function


	Public Function charIsNumber(character As Char) As Boolean
		Dim isNumberx As Boolean

		isNumberx = false
		If character = "0"C
			isNumberx = true
		ElseIf character = "1"C
			isNumberx = true
		ElseIf character = "2"C
			isNumberx = true
		ElseIf character = "3"C
			isNumberx = true
		ElseIf character = "4"C
			isNumberx = true
		ElseIf character = "5"C
			isNumberx = true
		ElseIf character = "6"C
			isNumberx = true
		ElseIf character = "7"C
			isNumberx = true
		ElseIf character = "8"C
			isNumberx = true
		ElseIf character = "9"C
			isNumberx = true
		End If

		Return isNumberx
	End Function


	Public Function charIsWhiteSpace(character As Char) As Boolean
		Dim isWhiteSpacex As Boolean

		isWhiteSpacex = false
		If character = " "C
			isWhiteSpacex = true
		ElseIf character = vbTab
			isWhiteSpacex = true
		ElseIf character = vblf
			isWhiteSpacex = true
		ElseIf character = vbcr
			isWhiteSpacex = true
		End If

		Return isWhiteSpacex
	End Function


	Public Function charIsSymbol(character As Char) As Boolean
		Dim isSymbolx As Boolean

		isSymbolx = false
		If character = "!"C
			isSymbolx = true
		ElseIf character = """"C
			isSymbolx = true
		ElseIf character = "#"C
			isSymbolx = true
		ElseIf character = "$"C
			isSymbolx = true
		ElseIf character = "%"C
			isSymbolx = true
		ElseIf character = "&"C
			isSymbolx = true
		ElseIf character = "'"C
			isSymbolx = true
		ElseIf character = "("C
			isSymbolx = true
		ElseIf character = ")"C
			isSymbolx = true
		ElseIf character = "*"C
			isSymbolx = true
		ElseIf character = "+"C
			isSymbolx = true
		ElseIf character = ","C
			isSymbolx = true
		ElseIf character = "-"C
			isSymbolx = true
		ElseIf character = "."C
			isSymbolx = true
		ElseIf character = "/"C
			isSymbolx = true
		ElseIf character = ":"C
			isSymbolx = true
		ElseIf character = ";"C
			isSymbolx = true
		ElseIf character = "<"C
			isSymbolx = true
		ElseIf character = "="C
			isSymbolx = true
		ElseIf character = ">"C
			isSymbolx = true
		ElseIf character = "?"C
			isSymbolx = true
		ElseIf character = "@"C
			isSymbolx = true
		ElseIf character = "["C
			isSymbolx = true
		ElseIf character = "\"C
			isSymbolx = true
		ElseIf character = "]"C
			isSymbolx = true
		ElseIf character = "^"C
			isSymbolx = true
		ElseIf character = "_"C
			isSymbolx = true
		ElseIf character = "`"C
			isSymbolx = true
		ElseIf character = "{"C
			isSymbolx = true
		ElseIf character = "|"C
			isSymbolx = true
		ElseIf character = "}"C
			isSymbolx = true
		ElseIf character = "~"C
			isSymbolx = true
		End If

		Return isSymbolx
	End Function


	Public Function charCharacterIsBefore(a As Char, b As Char) As Boolean
		Dim ad, bd As Double

		ad = Convert.ToInt16(a)
		bd = Convert.ToInt16(b)

		Return ad < bd
	End Function


	Public Function And4Byte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 32) - 1 and n2 >= 0 and n2 <= Math.pow(2, 32) - 1
				Return (Truncate(n1) and Truncate(n2)) and Math.pow(2, 32) - 1
			Else
				Return 0
			End If
	End Function


	Public Function And2Byte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 16) - 1 and n2 >= 0 and n2 <= Math.pow(2, 16) - 1
				Return (Truncate(n1) and Truncate(n2)) and Math.pow(2, 16) - 1
			Else
				Return 0
			End If
	End Function


	Public Function AndByte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 8) - 1 and n2 >= 0 and n2 <= Math.pow(2, 8) - 1
				Return (Truncate(n1) and Truncate(n2)) and Math.pow(2, 8) - 1
			Else
				Return 0
			End If
	End Function


	Public Function AndBytes(n1 As Double, n2 As Double, bytes As Double) As Double
		Dim byteVal, result, i As Double

		byteVal = 1
		result = 0

		If n1 >= 0 And n1 < 2 ^ (bytes*8) And n2 >= 0 And n2 < 2 ^ (bytes*8)
			n1 = Truncatex(n1)
			n2 = Truncatex(n2)
			bytes = Truncatex(bytes)

			i = 0
			While i < bytes*8
				If n1 Mod 2 = 1 And n2 Mod 2 = 1
					result = result + byteVal
				End If
				n1 = Floor(n1/2)
				n2 = Floor(n2/2)
				byteVal = byteVal*2
				i = i + 1
			End While
		End If

		Return result
	End Function


	Public Function Or4Byte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 32) - 1 and n2 >= 0 and n2 <= Math.pow(2, 32) - 1
				Return (Truncate(n1) or Truncate(n2)) and Math.pow(2, 32) - 1
			Else
				Return 0
			End If
	End Function


	Public Function Or2Byte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 16) - 1 and n2 >= 0 and n2 <= Math.pow(2, 16) - 1
				Return (Truncate(n1) or Truncate(n2)) and Math.pow(2, 16) - 1
			Else
				Return 0
			End If
	End Function


	Public Function OrByte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 8) - 1 and n2 >= 0 and n2 <= Math.pow(2, 8) - 1
				Return (Truncate(n1) or Truncate(n2)) and Math.pow(2, 8) - 1
			Else
				Return 0
			End If
	End Function


	Public Function OrBytes(n1 As Double, n2 As Double, bytes As Double) As Double
		Dim byteVal, result, i As Double

		byteVal = 1
		result = 0

		If n1 >= 0 And n1 < 2 ^ (bytes*8) And n2 >= 0 And n2 < 2 ^ (bytes*8)
			n1 = Truncatex(n1)
			n2 = Truncatex(n2)
			bytes = Truncatex(bytes)

			i = 0
			While i < bytes*8
				If n1 Mod 2 = 1 Or n2 Mod 2 = 1
					result = result + byteVal
				End If
				n1 = Floor(n1/2)
				n2 = Floor(n2/2)
				byteVal = byteVal*2
				i = i + 1
			End While
		End If

		Return result
	End Function


	Public Function Xor4Byte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 32) - 1 and n2 >= 0 and n2 <= Math.pow(2, 32) - 1
				Return (Truncate(n1) xor Truncate(n2)) and Math.pow(2, 32) - 1
			Else
				Return 0
			End If
	End Function


	Public Function Xor2Byte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 16) - 1 and n2 >= 0 and n2 <= Math.pow(2, 16) - 1
				Return (Truncate(n1) xor Truncate(n2)) and Math.pow(2, 16) - 1
			Else
				Return 0
			End If
	End Function


	Public Function XorByte(n1 As Double, n2 As Double) As Double
			If n1 >= 0 and n1 <= Math.pow(2, 8) - 1 and n2 >= 0 and n2 <= Math.pow(2, 8) - 1
				Return (Truncate(n1) xor Truncate(n2)) and Math.pow(2, 8) - 1
			Else
				Return 0
			End If
	End Function


	Public Function XorBytes(n1 As Double, n2 As Double, bytes As Double) As Double
		Dim byteVal, result, i As Double

		byteVal = 1
		result = 0

		If n1 >= 0 And n1 < 2 ^ (bytes*8) And n2 >= 0 And n2 < 2 ^ (bytes*8)
			n1 = Truncatex(n1)
			n2 = Truncatex(n2)
			bytes = Truncatex(bytes)

			i = 0
			While i < bytes*8
				If n1 Mod 2 <> n2 Mod 2
					result = result + byteVal
				End If
				n1 = Floor(n1/2)
				n2 = Floor(n2/2)
				byteVal = byteVal*2
				i = i + 1
			End While
		End If

		Return result
	End Function


	Public Function Not4Byte(b As Double) As Double
			If b >= 0 and b <= Math.pow(2, 32) - 1
				Return (not Convert.ToUInt32(Truncate(b))) and Math.pow(2, 32) - 1
			Else
				Return 0
			End If
	End Function


	Public Function Not2Byte(b As Double) As Double
			If b >= 0 and b <= Math.pow(2, 16) - 1
				Return (not Convert.ToUInt32(Truncate(b))) and Math.pow(2, 16) - 1
			Else
				Return 0
			End If
	End Function


	Public Function NotByte(b As Double) As Double
			If b >= 0 and b <= Math.pow(2, 8) - 1
				Return (not Convert.ToUInt32(Truncate(b))) and Math.pow(2, 8) - 1
			Else
				Return 0
			End If
	End Function


	Public Function NotBytes(b As Double, length As Double) As Double
		Dim result As Double

		result = 0

		If b >= 0 And b < 2 ^ (length*8)
			b = Truncatex(b)
			length = Truncatex(length)

			result = 2 ^ (length*8) - b - 1
		End If

		Return result
	End Function


	Public Function ShiftLeft4Byte(b As Double, amount As Double) As Double
			If b >= 0 and b <= Math.pow(2, 32) - 1 and amount >= 0 and amount <= Math.pow(2, 32) - 1
				Return (Truncate(b) << Truncate(amount)) and Math.pow(2, 32) - 1
			Else
				Return 0
			End If
	End Function


	Public Function ShiftLeft2Byte(b As Double, amount As Double) As Double
			If b >= 0 and b <= Math.pow(2, 16) - 1 and amount >= 0 and amount <= Math.pow(2, 16) - 1
				Return (Truncate(b) << Truncate(amount)) and Math.pow(2, 16) - 1
			Else
				Return 0
			End If
	End Function


	Public Function ShiftLeftByte(b As Double, amount As Double) As Double
			If b >= 0 and b <= Math.pow(2, 8) - 1 and amount >= 0 and amount <= Math.pow(2, 8) - 1
				Return (Truncate(b) << Truncate(amount)) and Math.pow(2, 8) - 1
			Else
				Return 0
			End If
	End Function


	Public Function ShiftLeftBytes(b As Double, amount As Double, length As Double) As Double
		Dim result As Double

		result = 0

		If b >= 0 And b < 2 ^ (length*8) And amount >= 0 And amount <= length*8
			b = Truncatex(b)
			amount = Truncatex(amount)

			result = b*2 ^ amount
		End If

		Return result
	End Function


	Public Function ShiftRight4Byte(b As Double, amount As Double) As Double
			If b >= 0 and b <= Math.pow(2, 32) - 1 and amount >= 0 and amount <= Math.pow(2, 32) - 1
				Return (Truncate(b) >> Truncate(amount)) and Math.pow(2, 32) - 1
			Else
				Return 0
			End If
	End Function


	Public Function ShiftRight2Byte(b As Double, amount As Double) As Double
			If b >= 0 and b <= Math.pow(2, 16) - 1 and amount >= 0 and amount <= Math.pow(2, 16) - 1
				Return (Truncate(b) >> Truncate(amount)) and Math.pow(2, 16) - 1
			Else
				Return 0
			End If
	End Function


	Public Function ShiftRightByte(b As Double, amount As Double) As Double
			If b >= 0 and b <= Math.pow(2, 8) - 1 and amount >= 0 and amount <= Math.pow(2, 8) - 1
				Return (Truncate(b) >> Truncate(amount)) and Math.pow(2, 8) - 1
			Else
				Return 0
			End If
	End Function


	Public Function ShiftRightBytes(b As Double, amount As Double, length As Double) As Double
		Dim result As Double

		result = 0

		If b >= 0 And b < 2 ^ (length*8) And amount >= 0 And amount <= length*8
			b = Truncatex(b)
			amount = Truncatex(amount)

			result = Truncatex(b/2 ^ amount)
		End If

		Return result
	End Function


	Public Function ReadNextBit(ByRef data As Double (), ByRef nextbit As NumberReference) As Double
		Dim bytenr, bitnumber, bit, b As Double

		bytenr = Floor(nextbit.numberValue/8)
		bitnumber = nextbit.numberValue Mod 8

		b = data(bytenr)

		bit = Floor(b/2 ^ bitnumber) Mod 2

		nextbit.numberValue = nextbit.numberValue + 1

		Return bit
	End Function


	Public Function BitExtract(b As Double, fromInc As Double, toInc As Double) As Double
		Return Floor(b/2 ^ fromInc) Mod 2 ^ (toInc + 1 - fromInc)
	End Function


	Public Function ReadBitRange(ByRef data As Double (), ByRef nextbit As NumberReference, length As Double) As Double
		Dim startbyte, endbyte As Double
		Dim startbit, endbit As Double
		Dim number, i As Double

		number = 0

		startbyte = Floor(nextbit.numberValue/8)
		endbyte = Floor((nextbit.numberValue + length)/8)

		startbit = nextbit.numberValue Mod 8
		endbit = (nextbit.numberValue + length - 1) Mod 8

		If startbyte = endbyte
			number = BitExtract(data(startbyte), startbit, endbit)
		End If

		nextbit.numberValue = nextbit.numberValue + length

		Return number
	End Function


	Public Sub SkipToBoundary(ByRef nextbit As NumberReference)
		Dim skip As Double

		skip = 8 - nextbit.numberValue Mod 8
		nextbit.numberValue = nextbit.numberValue + skip
	End Sub


	Public Function ReadNextByteBoundary(ByRef data As Double (), ByRef nextbit As NumberReference) As Double
		Dim bytenr, b As Double

		bytenr = Floor(nextbit.numberValue/8)
		b = data(bytenr)
		nextbit.numberValue = nextbit.numberValue + 8

		Return b
	End Function


	Public Function Read2bytesByteBoundary(ByRef data As Double (), ByRef nextbit As NumberReference) As Double
		Dim r As Double

		r = 0
		r = r + 2 ^ 8*ReadNextByteBoundary(data, nextbit)
		r = r + ReadNextByteBoundary(data, nextbit)

		Return r
	End Function


	Public Function ComputeAdler32(ByRef data As Double ()) As Double
		Dim a, b, m, i As Double

		a = 1
		b = 0
		m = 65521

		i = 0
		While i < data.Length
			a = (a + data(i)) Mod m
			b = (b + a) Mod m
			i = i + 1
		End While

		Return b*2 ^ 16 + a
	End Function


	Public Function DeflateDataStaticHuffman(ByRef data As Double (), level As Double) As Double ()
		Dim bytes As Double ()
		Dim currentBit As NumberReference
		Dim i As Double
		Dim copy As NumberArrayReference
		Dim code, length, compressedCode, lengthAdditionLength, distanceCode As NumberReference
		Dim distanceReference, lengthReference, lengthAddition As NumberReference
		Dim distanceAdditionReference, distanceAdditionLengthReference As NumberReference
		Dim bitReverseLookupTable As Double ()
		Dim match As BooleanReference

		code = CreateNumberReference(0)
		length = CreateNumberReference(0)
		compressedCode = CreateNumberReference(0)
		lengthAdditionLength = CreateNumberReference(0)
		distanceCode = CreateNumberReference(0)
		distanceReference = CreateNumberReference(0)
		lengthReference = CreateNumberReference(0)
		lengthAddition = CreateNumberReference(0)
		distanceAdditionReference = CreateNumberReference(0)
		distanceAdditionLengthReference = CreateNumberReference(0)
		match = New BooleanReference()

		bytes = New Double (Max(data.Length*2, 100) - 1){}
		Call aFillNumberArray(bytes, 0)
		currentBit = CreateNumberReference(0)

		bitReverseLookupTable = GenerateBitReverseLookupTable(9)

		' Final block
		Call AppendBitsToBytesRight(bytes, currentBit, 1, 1)
		' Fixed code
		Call AppendBitsToBytesRight(bytes, currentBit, 1, 2)

		i = 0
		While i < data.Length
			Call FindMatch(data, i, distanceReference, lengthReference, match, level)

			If match.booleanValue
				Call GetDeflateLengthCode(lengthReference.numberValue, compressedCode, lengthAddition, lengthAdditionLength)
				Call GetDeflateDistanceCode(distanceReference.numberValue, distanceCode, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable)
			End If

			If Not match.booleanValue
				Call GetDeflateStaticHuffmanCode(data(i), code, length, bitReverseLookupTable)
				Call AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)
				i = i + 1
			Else
				Call GetDeflateStaticHuffmanCode(compressedCode.numberValue, code, length, bitReverseLookupTable)
				Call AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)
				Call AppendBitsToBytesRight(bytes, currentBit, lengthAddition.numberValue, lengthAdditionLength.numberValue)
				Call AppendBitsToBytesRight(bytes, currentBit, distanceCode.numberValue, 5)
				Call AppendBitsToBytesRight(bytes, currentBit, distanceAdditionReference.numberValue, distanceAdditionLengthReference.numberValue)
				i = i + lengthReference.numberValue
			End If
		End While

		' Stop symbol
		Call GetDeflateStaticHuffmanCode(256, code, length, bitReverseLookupTable)
		Call AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue)

		copy = New NumberArrayReference()
		aCopyNumberArrayRange(bytes, 0, Ceiling(currentBit.numberValue/8), copy)
		Erase bytes 
		bytes = copy.numberArray

		Return bytes
	End Function


	Public Sub FindMatch(ByRef data As Double (), pos As Double, ByRef distanceReference As NumberReference, ByRef lengthReference As NumberReference, ByRef match As BooleanReference, level As Double)
		Dim i, j As Double
		Dim deflateMinMength, deflateMaxLength, deflateMaxDistance As Double
		Dim longest, maxLength, distanceForMax As Double
		Dim startDistance, matchLength As Double
		Dim done As Boolean

		deflateMinMength = 3
		deflateMaxLength = 258

		longest = Min(pos - 1, deflateMaxLength)
		longest = Min(data.Length - pos, longest)

		deflateMaxDistance = Floor(32768/10*level)

		startDistance = Min(pos, deflateMaxDistance)

		If longest >= deflateMinMength
			maxLength = 0
			distanceForMax = 0

			i = pos - 1
			While i >= pos - startDistance And maxLength <> longest
				matchLength = 0
				done = false
				j = 0
				While j < longest And Not done
					If data(i + j) = data(pos + j)
						matchLength = matchLength + 1
					Else
						done = true
					End If
					j = j + 1
				End While

				If matchLength >= deflateMinMength And matchLength > maxLength
					maxLength = matchLength
					distanceForMax = pos - i
				End If
				i = i - 1
			End While

			If maxLength >= deflateMinMength
				match.booleanValue = true
				lengthReference.numberValue = maxLength
				distanceReference.numberValue = distanceForMax
			Else
				match.booleanValue = false
			End If
		Else
			match.booleanValue = false
		End If
	End Sub


	Public Function GenerateBitReverseLookupTable(bits As Double) As Double ()
		Dim table As Double ()
		Dim i As Double

		table = New Double (2 ^ bits - 1){}

		i = 0
		While i < table.Length
			table(i) = ReverseBits(i, 32)
			i = i + 1
		End While

		Return table
	End Function


	Public Function ReverseBits(x As Double, bits As Double) As Double
		Dim b, bit, i As Double

		b = 0

		i = 0
		While i < bits
			b = ShiftLeft4Byte(b, 1)
			bit = And4Byte(x, 1)
			b = Or4Byte(b, bit)
			x = ShiftRight4Byte(x, 1)
			i = i + 1
		End While

		Return b
	End Function


	Public Function DeflateDataNoCompression(ByRef data As Double ()) As Double ()
		Dim deflated As Double ()
		Dim position As NumberReference
		Dim block, i, blocks, blocklength, maxblocksize As Double

		maxblocksize = 2 ^ 16 - 1
		blocks = Ceiling(data.Length/maxblocksize)

		position = CreateNumberReference(0)

		deflated = New Double ((1 + 4)*blocks + data.Length - 1){}

		block = 0
		While block < blocks
			If block + 1 = blocks
				Call WriteByte(deflated, 1, position)
			Else
				Call WriteByte(deflated, 0, position)
			End If
			blocklength = Min(data.Length - block*maxblocksize, maxblocksize)
			Call Write2BytesLE(deflated, blocklength, position)
			Call Write2BytesLE(deflated, Not2Byte(blocklength), position)

			i = 0
			While i < blocklength
				Call WriteByte(deflated, data(block*maxblocksize + i), position)
				i = i + 1
			End While
			block = block + 1
		End While

		Return deflated
	End Function


	Public Sub GetDeflateStaticHuffmanCode(b As Double, ByRef code As NumberReference, ByRef length As NumberReference, ByRef bitReverseLookupTable As Double ())
		Dim reversed As Double

		If b >= 0 And b <= 143
			code.numberValue = 48 + b
			length.numberValue = 8
		ElseIf b >= 144 And b <= 255
			code.numberValue = b - 144 + 400
			length.numberValue = 9
		ElseIf b >= 256 And b <= 279
			code.numberValue = b - 256 + 0
			length.numberValue = 7
		ElseIf b >= 280 And b <= 287
			code.numberValue = b - 280 + 192
			length.numberValue = 8
		End If

		reversed = bitReverseLookupTable(code.numberValue)
		code.numberValue = ShiftRight4Byte(reversed, 32 - length.numberValue)
	End Sub


	Public Sub GetDeflateLengthCode(length As Double, ByRef code As NumberReference, ByRef lengthAddition As NumberReference, ByRef lengthAdditionLength As NumberReference)
		If length >= 3 And length <= 10
			code.numberValue = 257 + length - 3
			lengthAdditionLength.numberValue = 0
		ElseIf length >= 11 And length <= 18
			code.numberValue = 265 + Floor((length - 11)/2)
			lengthAddition.numberValue = Floor((length - 11) Mod 2)
			lengthAdditionLength.numberValue = 1
		ElseIf length >= 19 And length <= 34
			code.numberValue = 269 + Floor((length - 19)/4)
			lengthAddition.numberValue = Floor((length - 19) Mod 4)
			lengthAdditionLength.numberValue = 2
		ElseIf length >= 35 And length <= 66
			code.numberValue = 273 + Floor((length - 35)/8)
			lengthAddition.numberValue = Floor((length - 35) Mod 8)
			lengthAdditionLength.numberValue = 3
		ElseIf length >= 67 And length <= 130
			code.numberValue = 277 + Floor((length - 67)/16)
			lengthAddition.numberValue = Floor((length - 67) Mod 16)
			lengthAdditionLength.numberValue = 4
		ElseIf length >= 131 And length <= 257
			code.numberValue = 281 + Floor((length - 131)/32)
			lengthAddition.numberValue = Floor((length - 131) Mod 32)
			lengthAdditionLength.numberValue = 5
		ElseIf length = 258
			code.numberValue = 285
			lengthAdditionLength.numberValue = 0
		End If
	End Sub


	Public Sub GetDeflateDistanceCode(distance As Double, ByRef code As NumberReference, ByRef distanceAdditionReference As NumberReference, ByRef distanceAdditionLengthReference As NumberReference, ByRef bitReverseLookupTable As Double ())
		Dim reversed As Double

		If distance >= 1 And distance <= 4
			code.numberValue = distance - 1
			distanceAdditionLengthReference.numberValue = 0
		ElseIf distance >= 5 And distance <= 8
			code.numberValue = 4 + Floor((distance - 5)/2)
			distanceAdditionReference.numberValue = Floor((distance - 5) Mod 2)
			distanceAdditionLengthReference.numberValue = 1
		ElseIf distance >= 9 And distance <= 16
			code.numberValue = 6 + Floor((distance - 9)/4)
			distanceAdditionReference.numberValue = Floor((distance - 9) Mod 4)
			distanceAdditionLengthReference.numberValue = 2
		ElseIf distance >= 17 And distance <= 32
			code.numberValue = 8 + Floor((distance - 17)/8)
			distanceAdditionReference.numberValue = Floor((distance - 17) Mod 8)
			distanceAdditionLengthReference.numberValue = 3
		ElseIf distance >= 33 And distance <= 64
			code.numberValue = 10 + Floor((distance - 33)/16)
			distanceAdditionReference.numberValue = Floor((distance - 33) Mod 16)
			distanceAdditionLengthReference.numberValue = 4
		ElseIf distance >= 65 And distance <= 128
			code.numberValue = 12 + Floor((distance - 65)/32)
			distanceAdditionReference.numberValue = Floor((distance - 65) Mod 32)
			distanceAdditionLengthReference.numberValue = 5
		ElseIf distance >= 129 And distance <= 256
			code.numberValue = 14 + Floor((distance - 129)/64)
			distanceAdditionReference.numberValue = Floor((distance - 129) Mod 64)
			distanceAdditionLengthReference.numberValue = 6
		ElseIf distance >= 257 And distance <= 512
			code.numberValue = 16 + Floor((distance - 257)/128)
			distanceAdditionReference.numberValue = Floor((distance - 257) Mod 128)
			distanceAdditionLengthReference.numberValue = 7
		ElseIf distance >= 513 And distance <= 1024
			code.numberValue = 18 + Floor((distance - 513)/256)
			distanceAdditionReference.numberValue = Floor((distance - 513) Mod 256)
			distanceAdditionLengthReference.numberValue = 8
		ElseIf distance >= 1025 And distance <= 2048
			code.numberValue = 20 + Floor((distance - 1025)/2 ^ 9)
			distanceAdditionReference.numberValue = Floor((distance - 1025) Mod 2 ^ 9)
			distanceAdditionLengthReference.numberValue = 9
		ElseIf distance >= 2049 And distance <= 4096
			code.numberValue = 22 + Floor((distance - 2049)/2 ^ 10)
			distanceAdditionReference.numberValue = Floor((distance - 2049) Mod 2 ^ 10)
			distanceAdditionLengthReference.numberValue = 10
		ElseIf distance >= 4097 And distance <= 8192
			code.numberValue = 24 + Floor((distance - 4097)/2 ^ 11)
			distanceAdditionReference.numberValue = Floor((distance - 4097) Mod 2 ^ 11)
			distanceAdditionLengthReference.numberValue = 11
		ElseIf distance >= 8193 And distance <= 16384
			code.numberValue = 26 + Floor((distance - 8193)/2 ^ 12)
			distanceAdditionReference.numberValue = Floor((distance - 8193) Mod 2 ^ 12)
			distanceAdditionLengthReference.numberValue = 12
		ElseIf distance >= 16385 And distance <= 32768
			code.numberValue = 28 + Floor((distance - 16385)/2 ^ 13)
			distanceAdditionReference.numberValue = Floor((distance - 16385) Mod 2 ^ 13)
			distanceAdditionLengthReference.numberValue = 13
		End If

		reversed = bitReverseLookupTable(code.numberValue)
		code.numberValue = ShiftRight4Byte(reversed, 32 - 5)
	End Sub


	Public Sub AppendBitsToBytesLeft(ByRef bytes As Double (), ByRef nextbit As NumberReference, data As Double, length As Double)
		Dim bytePos, bitPos, segment, part, remove As Double

		
		While length > 0
			bytePos = Truncatex(nextbit.numberValue/8)
			bitPos = nextbit.numberValue Mod 8

			If length < 8 - bitPos
				part = ShiftLeft4Byte(data, 8 - bitPos - length)

				bytes(bytePos) = Or4Byte(bytes(bytePos), part)

				nextbit.numberValue = nextbit.numberValue + length

				length = 0
			Else
				segment = 8 - bitPos

				part = ShiftRight4Byte(data, length - segment)
				bytes(bytePos) = Or4Byte(bytes(bytePos), part)
				nextbit.numberValue = nextbit.numberValue + segment

				remove = ShiftLeft4Byte(part, length - segment)
				data = Xor4Byte(data, remove)

				length = length - segment
			End If
		End While
	End Sub


	Public Sub AppendBitsToBytesRight(ByRef bytes As Double (), ByRef nextbit As NumberReference, data As Double, length As Double)
		Dim bytePos, bitPos, segment, part As Double
		Dim mask As Double

		
		While length > 0
			bytePos = Truncatex(nextbit.numberValue/8)
			bitPos = nextbit.numberValue Mod 8

			If length < 8 - bitPos
				part = ShiftLeft4Byte(data, bitPos)

				bytes(bytePos) = Or4Byte(bytes(bytePos), part)

				nextbit.numberValue = nextbit.numberValue + length

				length = 0
			Else
				segment = 8 - bitPos

				mask = 1
				mask = ShiftLeft4Byte(mask, segment)
				mask = mask - 1

				part = And4Byte(mask, data)
				part = ShiftLeft4Byte(part, bitPos)
				bytes(bytePos) = Or4Byte(bytes(bytePos), part)
				nextbit.numberValue = nextbit.numberValue + segment

				data = ShiftRight4Byte(data, segment)

				length = length - segment
			End If
		End While
	End Sub


End Module

