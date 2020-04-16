import pbPlots as pbPlots
import supportLib as supportLib


image = pbPlots.CreateImage(800, 600, pbPlots.GetWhite())

xs = [-2, -1, 0, 1, 2]
ys = [2, -1, -2, -1, 2]

pbPlots.DrawScatterPlot(image, xs, ys)

pngdata = pbPlots.ConvertToPNG(image)
supportLib.WriteToFile(pngdata, "example1.png")
pbPlots.DeleteImage(image)
