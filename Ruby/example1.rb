require "./pbPlots.rb"
require "./supportLib.rb"

imageReference = CreateRGBABitmapImageReference()

xs = [-2, -1, 0, 1, 2]
ys = [2, -1, -2, -1, 2]

errorMessage = StringReference.new
success = DrawScatterPlot(imageReference, 800, 600, xs, ys, errorMessage)

if success
    pngdata = ConvertToPNG(imageReference.image)
    WriteToFile(pngdata, "example1.png")
    DeleteImage(imageReference.image)
else
    STDERR.puts(errorMessage.string.join(''))
end
