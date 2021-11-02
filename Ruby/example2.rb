require "./pbPlots.rb"
require "./supportLib.rb"

series = GetDefaultScatterPlotSeriesSettings()
series.xs = [-2, -1, 0, 1, 2]
series.ys = [2, -1, -2, -1, 2]
series.linearInterpolation = true
series.lineType = "dashed"
series.lineThickness = 2
series.color = GetGray(0.3)

settings = GetDefaultScatterPlotSettings()
settings.width = 800
settings.height = 600
settings.autoBoundaries = true
settings.autoPadding = true
settings.title = "x^2 - 2"
settings.xLabel = "X axis"
settings.yLabel = "Y axis"
settings.scatterPlotSeries = [series]

imageReference = CreateRGBABitmapImageReference()
errorMessage = StringReference.new
success = DrawScatterPlotFromSettings(imageReference, settings, errorMessage)

if success
    pngdata = ConvertToPNG(imageReference.image)
    WriteToFile(pngdata, "example2.png")
    DeleteImage(imageReference.image)
else
    STDERR.puts(errorMessage.string.join(''))
end
