import pbPlots as pbPlots
import supportLib as supportLib


imageReference = pbPlots.CreateRGBABitmapImageReference()

xs = [-2, -1, 0, 1, 2]
ys = [2, -1, -2, -1, 2]

pbPlots.DrawScatterPlot(imageReference, 800, 600, xs, ys)

pngdata = pbPlots.ConvertToPNG(imageReference.image)
supportLib.WriteToFile(pngdata, "example1.png")
pbPlots.DeleteImage(imageReference.image)
