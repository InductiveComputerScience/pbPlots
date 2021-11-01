package pbPlots;

import static pbPlots.pbPlots.*;
import static pbPlots.SupportLib.*;

public class Example2{
	public static void main(String [] args){
		boolean success;
		StringReference errorMessage = new StringReference();
		ScatterPlotSeries series = GetDefaultScatterPlotSeriesSettings();
		series.xs = new double [] {-2, -1, 0, 1, 2};
		series.ys = new double [] {2, -1, -2, -1, 2};
		series.linearInterpolation = true;
		series.lineType = "dashed".toCharArray();
		series.lineThickness = 2d;
		series.color = GetGray(0.3);

		ScatterPlotSettings settings = GetDefaultScatterPlotSettings();
		settings.width = 600;
		settings.height = 400;
		settings.autoBoundaries = true;
		settings.autoPadding = true;
		settings.title = "x^2 - 2".toCharArray();
		settings.xLabel = "X axis".toCharArray();
		settings.yLabel = "Y axis".toCharArray();
		settings.scatterPlotSeries = new ScatterPlotSeries [] {series};

		RGBABitmapImageReference imageReference = CreateRGBABitmapImageReference();
		success = DrawScatterPlotFromSettings(imageReference, settings, errorMessage);

		if(success) {
			double[] pngdata = ConvertToPNG(imageReference.image);
			WriteToFile(pngdata, "example2.png");
			DeleteImage(imageReference.image);
		}else{
			System.err.println(new String(errorMessage.string));
		}
	}
}
