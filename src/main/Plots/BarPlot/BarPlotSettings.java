package Plots.BarPlot;

import RGBABitmapImage.RGBABitmapImage.*;
import references.references.StringReference;

public class BarPlotSettings {
    public double width, height;
    public boolean autoBoundaries;
    public double yMax, yMin;
    public boolean autoPadding;
    public double xPadding, yPadding;
    public char [] title;
    public boolean showGrid;
    public RGBA gridColor;
    public BarPlotSeries barPlotSeries[];
    public char [] yLabel;
    public boolean autoColor;
    public boolean grayscaleAutoColor;
    public boolean autoSpacing;
    public double groupSeparation;
    public double barSeparation;
    public boolean autoLabels;
    public StringReference [] xLabels;
    public boolean barBorder;
}
