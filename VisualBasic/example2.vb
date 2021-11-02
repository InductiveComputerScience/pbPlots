Module Example2
	Sub Main2(args As String())
		Dim series As ScatterPlotSeries = GetDefaultScatterPlotSeriesSettings()
		series.xs = {-2, -1, 0, 1, 2}
		series.ys = {2, -1, -2, -1, 2}
		series.linearInterpolation = true
		series.lineType = "dashed"
		series.lineThickness = 2
		series.color = GetGray(0.3)

		Dim settings As ScatterPlotSettings = GetDefaultScatterPlotSettings()
		settings.width = 800
		settings.height = 600
		settings.autoBoundaries = true
		settings.autoPadding = true
		settings.title = "x^2 - 2"
		settings.xLabel = "X axis"
		settings.yLabel = "Y axis"
		settings.scatterPlotSeries = {series}

		Dim imageReference As RGBABitmapImageReference = CreateRGBABitmapImageReference()
        Dim errorMessage As StringReference = New StringReference()
        Dim success As Boolean = DrawScatterPlotFromSettings(imageReference, settings, errorMessage)

        If success
            Dim pngdata as double() = ConvertToPNG(imageReference.image)
            Call WriteToFile(pngdata, "example2.png")
            Call DeleteImage(imageReference.image)
        Else
            Console.Error.WriteLine(errorMessage.stringx)
        End If
	End Sub
End Module
