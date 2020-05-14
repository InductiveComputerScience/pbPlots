package Plots.Plots;

import RGBABitmapImage.RGBABitmapImage.*;

public class ScatterPlotSettings {
    public ScatterPlotSeries scatterPlotSeries[];
    public boolean autoBoundaries;
    public double xMax, xMin, yMax, yMin;
    public boolean autoPadding;
    public double xPadding, yPadding;

    public char [] yLabel;
    public char [] xLabel;
    public char [] title;
    public boolean showGrid;
    public RGBA gridColor;
    public boolean xAxisAuto;
    public boolean xAxisTop;
    public boolean xAxisBottom;
    public boolean yAxisAuto;
    public boolean yAxisLeft;
    public boolean yAxisRight;
    public double width;
    public double height;
}
