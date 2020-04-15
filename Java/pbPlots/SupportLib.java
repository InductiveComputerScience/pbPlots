package pbPlots;

import java.awt.image.BufferedImage;

import pbPlots.pbPlots.*;
import static pbPlots.pbPlots.*;
import static java.lang.Math.*;
import java.io.FileOutputStream;

public class SupportLib {
    public static BufferedImage ConvertRGBABitmapImageToJavaBufferedImage(RGBABitmapImage image) {
        BufferedImage bi = new BufferedImage(image.x.length, image.x[0].y.length, BufferedImage.TYPE_INT_ARGB);

        for(int i = 0; i < image.x.length; i++){
            for(int j = 0; j < image.x[0].y.length; j++){
                int color = getColor(image.x[i].y[j]);
                bi.setRGB(i, j, color);
            }
        }

        return bi;
    }

    public static int getColor(RGBA rgba) {
        int r = (int)round(rgba.r * 255d); // red component 0...255
        int g = (int)round(rgba.g * 255d); // green component 0...255
        int b = (int)round(rgba.b * 255d); // blue component 0...255
        int a = (int)round(rgba.a * 255d); // alpha (transparency) component 0...255
        int col = (a << 24) | (r << 16) | (g << 8) | b;
        return col;
    }

    public static RGBABitmapImage ConvertJavaBufferedImageToRGBABitmapImage(BufferedImage image) {
        int x, y;
        RGBABitmapImage img = CreateImage(image.getWidth(), image.getHeight(), GetBlack());

        for(y = 0; y < image.getHeight(); y++){
            for(x = 0; x < image.getWidth(); x++){
                int rgb = image.getRGB(x, y);
                img.x[x].y[y].r = ((rgb >> 16) & 0xFF)/255d;
                img.x[x].y[y].g = ((rgb >> 8) & 0xFF)/255d;
                img.x[x].y[y].b = ((rgb >> 0) & 0xFF)/255d;
                img.x[x].y[y].a = 1;
            }
        }

        return img;
    }

    public static void WriteToFile(double [] data, String filename) {
        try{
            FileOutputStream fos = new FileOutputStream(filename);
            fos.write(DoubleArrayToByteArray(data));
        }catch(Exception e){
            e.printStackTrace();
        }
    }

    public static byte[] DoubleArrayToByteArray(double[] data) {
        byte [] bytes = new byte [data.length];
        for(int i = 0; i < data.length; i++){
            bytes[i] = DoubleToByte(data[i]);
        }
        return bytes;
    }

    public static byte DoubleToByte(double d) {
        return (byte)d;
    }
}
