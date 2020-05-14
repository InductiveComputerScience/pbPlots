package Plots.Colors;

import RGBABitmapImage.RGBABitmapImage.RGBA;

import static Graphics2D.colors.colors.CreateRGBColor;

public class Colors {
    public static RGBA[] Get8HighContrastColors() {
        RGBA[] colors;
        colors = new RGBA[8];
        colors[0] = CreateRGBColor(3d/256d, 146d/256d, 206d/256d);
        colors[1] = CreateRGBColor(253d/256d, 83d/256d, 8d/256d);
        colors[2] = CreateRGBColor(102d/256d, 176d/256d, 50d/256d);
        colors[3] = CreateRGBColor(208d/256d, 234d/256d, 43d/256d);
        colors[4] = CreateRGBColor(167d/256d, 25d/256d, 75d/256d);
        colors[5] = CreateRGBColor(254d/256d, 254d/256d, 51d/256d);
        colors[6] = CreateRGBColor(134d/256d, 1d/256d, 175d/256d);
        colors[7] = CreateRGBColor(251d/256d, 153d/256d, 2d/256d);
        return colors;
    }
}
