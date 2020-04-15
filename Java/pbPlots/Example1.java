package pbPlots;

import static pbPlots.pbPlots.*;
import static pbPlots.SupportLib.*;

public class Example1{
	public static void main(String [] args){
		RGBABitmapImage image = CreateImage(600, 400, GetWhite());

		double [] xs = {-2, -1, 0, 1, 2};
		double [] ys = {2, -1, -2, -1, 2};

		DrawScatterPlot(image, xs, ys);
		double[] pngdata = ConvertToPNG(image);
		WriteToFile(pngdata, "example1.png");
		DeleteImage(image);
	}
}
