Module Example2
	Sub Main2(args As String())
		Dim image As RGBABitmapImage = CreateImage(800, 600, GetWhite())

		Dim series As ScatterPlotSeries = GetDefaultScatterPlotSeriesSettings()
		series.xs = {-2, -1, 0, 1, 2}
		series.ys = {2, -1, -2, -1, 2}
		series.linearInterpolation = true
		series.lineType = "dashed"
		series.lineThickness = 2
		series.color = GetGray(0.3)

		Dim settings As ScatterPlotSettings = GetDefaultScatterPlotSettings()
		settings.canvas = CreateImage(800, 600, GetWhite())
		settings.autoBoundaries = true
		settings.autoPadding = true
		settings.title = "x^2 - 2"
		settings.xLabel = "X axis"
		settings.yLabel = "Y axis"
		settings.scatterPlotSeries = {series}

		Call DrawScatterPlotFromSettings(settings)

		Dim pngdata as double() = ConvertToPNG(settings.canvas)
		Call WriteToFile(pngdata, "example2.png")
		Call DeleteImage(settings.canvas)
	End Sub
End Module
