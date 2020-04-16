Module Example1
	Sub Main(args As String())
		Dim image As RGBABitmapImage = CreateImage(800, 600, GetWhite())

		Dim xs as double() = {-2, -1, 0, 1, 2}
		Dim ys as double() = {2, -1, -2, -1, 2}

		Call DrawScatterPlot(image, xs, ys)

		Dim pngdata as double() = ConvertToPNG(image)
		Call WriteToFile(pngdata, "example1.png")
		Call DeleteImage(image)
	End Sub
End Module
