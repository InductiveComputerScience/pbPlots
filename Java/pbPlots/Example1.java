package pbPlots;

import static pbPlots.pbPlots.*;
import static pbPlots.SupportLib.*;

public class Example1{
	public static void main(String [] args){
		RGBABitmapImageReference imageReference = CreateRGBABitmapImageReference();

		double [] xs = {-2, -1, 0, 1, 2};
		double [] ys = {2, -1, -2, -1, 2};

		DrawScatterPlot(imageReference, 600, 400, xs, ys);
		double[] pngdata = ConvertToPNG(imageReference.image);
		WriteToFile(pngdata, "example1.png");
		DeleteImage(imageReference.image);
	}
}
