package Plots.TextDrawing;

import RGBABitmapImage.RGBABitmapImage.*;

import static BasicPixelFont.BasicPixelFont.BasicPixelFont.*;
import static Graphics2D.Graphics2D.Graphics2D.*;
import static Graphics2D.colors.colors.*;

public class TextDrawing {
    public static void DrawText(RGBABitmapImage canvas, double x, double y, char[] text, RGBA color) {
        double i, charWidth, spacing;

        charWidth = 8d;
        spacing = 2d;

        for(i = 0d; i < text.length; i = i + 1d) {
            DrawAsciiCharacter(canvas, x + i*(charWidth + spacing), y, text[(int)i], color);
        }
    }

    public static void DrawTextUpwards(RGBABitmapImage canvas, double x, double y, char [] text, RGBA color) {
        RGBABitmapImage buffer, rotated;

        buffer = CreateImage(GetTextWidth(text), GetTextHeight(text), GetTransparent());
        DrawText(buffer, 0d, 0d, text, color);
        rotated = RotateAntiClockwise90Degrees(buffer);
        DrawImageOnImage(canvas, rotated, x, y);
        DeleteImage(buffer);
        DeleteImage(rotated);
    }
}
