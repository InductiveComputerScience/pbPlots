import pbPlots as pbPlots
import supportLib as supportLib

series = pbPlots.GetDefaultScatterPlotSeriesSettings()
series.xs = [-2, -1, 0, 1, 2]
series.ys = [2, -1, -2, -1, 2]
series.linearInterpolation = True
series.lineType = "dashed"
series.lineThickness = 2
series.color = pbPlots.GetGray(0.3)

settings = pbPlots.GetDefaultScatterPlotSettings()
settings.canvas = pbPlots.CreateImage(800, 600, pbPlots.GetWhite())
settings.autoBoundaries = True
settings.autoPadding = True
settings.title = "x^2 - 2"
settings.xLabel = "X axis"
settings.yLabel = "Y axis"
settings.scatterPlotSeries = [series]

pbPlots.DrawScatterPlotFromSettings(settings)

pngdata = pbPlots.ConvertToPNG(settings.canvas)
supportLib.WriteToFile(pngdata, "example2.png")
pbPlots.DeleteImage(settings.canvas)
