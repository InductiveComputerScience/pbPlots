Module Example1
	Sub Main(args As String())
		Dim imageReference As RGBABitmapImageReference = CreateRGBABitmapImageReference()

		Dim xs as double() = {-2, -1, 0, 1, 2}
		Dim ys as double() = {2, -1, -2, -1, 2}

		Call DrawScatterPlot(imageReference, 800, 600, xs, ys)

		Dim pngdata as double() = ConvertToPNG(imageReference.image)
		Call WriteToFile(pngdata, "example1.png")
		Call DeleteImage(imageReference.image)
	End Sub
End Module
