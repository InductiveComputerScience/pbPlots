import pbPlots as pbPlots
import supportLib as supportLib
import sys

series = pbPlots.GetDefaultScatterPlotSeriesSettings()
series.xs = [-2, -1, 0, 1, 2]
series.ys = [2, -1, -2, -1, 2]
series.linearInterpolation = True
series.lineType = "dashed"
series.lineThickness = 2
series.color = pbPlots.GetGray(0.3)

settings = pbPlots.GetDefaultScatterPlotSettings()
settings.width = 800
settings.height = 600
settings.autoBoundaries = True
settings.autoPadding = True
settings.title = "x^2 - 2"
settings.xLabel = "X axis"
settings.yLabel = "Y axis"
settings.scatterPlotSeries = [series]

imageReference = pbPlots.CreateRGBABitmapImageReference()
errorMessage = pbPlots.StringReference()
success = pbPlots.DrawScatterPlotFromSettings(imageReference, settings, errorMessage)

if success:
    pngdata = pbPlots.ConvertToPNG(imageReference.image)
    supportLib.WriteToFile(pngdata, "example2.png")
    pbPlots.DeleteImage(imageReference.image)
else:
    sys.stderr.write("".join(errorMessage.string))
