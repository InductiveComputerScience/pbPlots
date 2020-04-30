require "./pbPlots.rb"
require "./supportLib.rb"

imageReference = CreateRGBABitmapImageReference()

xs = [-2, -1, 0, 1, 2]
ys = [2, -1, -2, -1, 2]

DrawScatterPlot(imageReference, 800, 600, xs, ys)

pngdata = ConvertToPNG(imageReference.image)
WriteToFile(pngdata, "example1.png")
DeleteImage(imageReference.image)
