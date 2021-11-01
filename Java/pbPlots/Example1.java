package pbPlots;

import static pbPlots.pbPlots.*;
import static pbPlots.SupportLib.*;

public class Example1{
	public static void main(String [] args){
		boolean success;
		StringReference errorMessage = new StringReference();
		RGBABitmapImageReference imageReference = CreateRGBABitmapImageReference();

		double [] xs = {-2, -1, 0, 1, 2};
		double [] ys = {2, -1, -2, -1, 2};

		success = DrawScatterPlot(imageReference, 600, 400, xs, ys, errorMessage);

		if(success) {
			double[] pngdata = ConvertToPNG(imageReference.image);
			WriteToFile(pngdata, "example1.png");
			DeleteImage(imageReference.image);
		}else{
			System.err.println(new String(errorMessage.string));
		}
	}
}
