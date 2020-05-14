package Plots.Graphics;

import RGBABitmapImage.RGBABitmapImage.*;

import static Graphics2D.Graphics2D.Graphics2D.*;

public class Graphics {
    public static void DrawFilledRectangleWithBorder(RGBABitmapImage image, double x, double y, double w, double h, RGBA borderColor, RGBA fillColor){
        if(h > 0d && w > 0d) {
            DrawFilledRectangle(image, x, y, w, h, fillColor);
            DrawRectangle1px(image, x, y, w, h, borderColor);
        }
    }

    public static RGBABitmapImageReference CreateRGBABitmapImageReference() {
        RGBABitmapImageReference reference;

        reference = new RGBABitmapImageReference();
        reference.image = new RGBABitmapImage();
        reference.image.x = new RGBABitmap[0];

        return reference;
    }
}
