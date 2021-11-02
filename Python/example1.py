import pbPlots as pbPlots
import supportLib as supportLib
import sys

imageReference = pbPlots.CreateRGBABitmapImageReference()

xs = [-2, -1, 0, 1, 2]
ys = [2, -1, -2, -1, 2]

errorMessage = pbPlots.StringReference()
success = pbPlots.DrawScatterPlot(imageReference, 800, 600, xs, ys, errorMessage)

if success:
    pngdata = pbPlots.ConvertToPNG(imageReference.image)
    supportLib.WriteToFile(pngdata, "example1.png")
    pbPlots.DeleteImage(imageReference.image)
else:
    sys.stderr.write("".join(errorMessage.string))