require "./pbPlots.rb"
require "./supportLib.rb"

image = CreateImage(800, 600, GetWhite())

xs = [-2, -1, 0, 1, 2]
ys = [2, -1, -2, -1, 2]

DrawScatterPlot(image, xs, ys)

pngdata = ConvertToPNG(image)
WriteToFile(pngdata, "example1.png")
DeleteImage(image)
