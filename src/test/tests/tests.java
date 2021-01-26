package tests;

import Plots.BarPlot.BarPlotSeries;
import Plots.BarPlot.BarPlotSettings;
import Plots.Graphics.*;
import Plots.Plots.*;
import RGBABitmapImage.RGBABitmapImage.RGBABitmapImage;
import references.references.*;

import static Graphics2D.colors.colors.*;
import static Plots.BarPlot.BarPlot.*;
import static Plots.Common.Common.*;
import static Plots.Graphics.Graphics.*;
import static Plots.Plots.Plots.*;
//import static Regression.Regression.Regression.OrdinaryLeastSquaresWithIntercept;
import static java.lang.Math.*;
import static numbers.StringToNumber.StringToNumber.StringToNumberArray;
import static references.references.references.*;
import static testing.testing.testing.*;

public class tests {
    public static double test() {
        double z;
        double [] gridlines;
        NumberReference failures;
        StringArrayReference labels;
        NumberArrayReference labelPriorities;
        RGBABitmapImageReference imageReference;
        double [] xs, ys;

        failures = CreateNumberReference(0d);

        imageReference = CreateRGBABitmapImageReference();

        labels = new StringArrayReference();
        labelPriorities = new NumberArrayReference();

        z = 10d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 11d, failures);

        z = 9d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 19d, failures);

        z = 8d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 17d, failures);

        z = 7d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 15d, failures);

        z = 6d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 13d, failures);

        z = 5d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 21d, failures);

        z = 4d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 17d, failures);

        z = 3d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 31d, failures);

        z = 2d;
        gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
        AssertEquals(gridlines.length, 21d, failures);

        xs = new double[5];
        xs[0] = -2d;
        xs[1] = -1d;
        xs[2] = 0d;
        xs[3] = 1d;
        xs[4] = 2d;
        ys = new double[5];
        ys[0] = 2d;
        ys[1] = -1d;
        ys[2] = -2d;
        ys[3] = -1d;
        ys[4] = 2d;
        DrawScatterPlot(imageReference, 800d, 600d, xs, ys);

        imageReference.image = DrawBarPlot(800d, 600d, ys);

        TestMapping(failures);
        TestMapping2(failures);

        return failures.numberValue;
    }

    public static void TestMapping(NumberReference failures) {
        ScatterPlotSeries series;
        ScatterPlotSettings settings;
        RGBABitmapImageReference imageReference;
        double x1, y1;

        series = GetDefaultScatterPlotSeriesSettings();

        series.xs = new double [5];
        series.xs[0] = -2;
        series.xs[1] = -1;
        series.xs[2] = 0;
        series.xs[3] = 1;
        series.xs[4] = 2;
        series.ys = new double [5];
        series.ys[0] = -2;
        series.ys[1] = -1;
        series.ys[2] = -2;
        series.ys[3] = -1;
        series.ys[4] = 2;
        series.linearInterpolation = true;
        series.lineType = "dashed".toCharArray();
        series.lineThickness = 2d;
        series.color = GetGray(0.3);

        settings = GetDefaultScatterPlotSettings();
        settings.width = 600;
        settings.height = 400;
        settings.autoBoundaries = true;
        settings.autoPadding = true;
        settings.title = "x^2 - 2".toCharArray();
        settings.xLabel = "X axis".toCharArray();
        settings.yLabel = "Y axis".toCharArray();
        settings.scatterPlotSeries = new ScatterPlotSeries [1];
        settings.scatterPlotSeries[0] = series;

        imageReference = CreateRGBABitmapImageReference();
        DrawScatterPlotFromSettings(imageReference, settings);

        x1 = MapXCoordinateAutoSettings(-1d, imageReference.image, series.xs);
        y1 = MapYCoordinateAutoSettings(-1d, imageReference.image, series.ys);

        AssertEquals(x1, 180d, failures);
        AssertEquals(y1, 280d, failures);
    }

    public static void TestMapping2(NumberReference failures) {
        double[] xs, ys, xs2, ys2;
        double i, x, y, w, h, xMin, xMax, yMin, yMax;
        RGBABitmapImageReference canvasReference;
        ScatterPlotSettings settings;
        double points;
        double x1, y1;

        points = 300d;
        w = 600d * 2d;
        h = 300d * 2d;
        xMin = 0d;
        xMax = 150d;
        yMin = 0d;
        yMax = 1d;

        xs = new double [(int)points];
        ys = new double [(int)points];
        xs2 = new double [(int)points];
        ys2 = new double [(int)points];

        for(i = 0d; i < points; i = i + 1d){
            x = xMin + (xMax - xMin)/(points - 1d) * i; // points - 1d is to ensure both extremeties are included.

            y = x / (x + 7d);

            xs[(int)i] = x;
            ys[(int)i] = y;

            y = 1.4 * x / (x + 7d) * (1d - (atan((x/1.5 - 30d)/5d)/1.6 + 1d)/2d);

            xs2[(int)i] = x;
            ys2[(int)i] = y;
        }

        settings = GetDefaultScatterPlotSettings();

        settings.scatterPlotSeries = new ScatterPlotSeries[2];
        settings.scatterPlotSeries[0] = new ScatterPlotSeries();
        settings.scatterPlotSeries[0].xs = xs;
        settings.scatterPlotSeries[0].ys = ys;
        settings.scatterPlotSeries[0].linearInterpolation = true;
        settings.scatterPlotSeries[0].lineType = "solid".toCharArray();
        settings.scatterPlotSeries[0].lineThickness = 3d;
        settings.scatterPlotSeries[0].color = CreateRGBColor(1d, 0d, 0d);
        settings.scatterPlotSeries[1] = new ScatterPlotSeries();
        settings.scatterPlotSeries[1].xs = xs2;
        settings.scatterPlotSeries[1].ys = ys2;
        settings.scatterPlotSeries[1].linearInterpolation = true;
        settings.scatterPlotSeries[1].lineType = "solid".toCharArray();
        settings.scatterPlotSeries[1].lineThickness = 3d;
        settings.scatterPlotSeries[1].color = CreateRGBColor(0d, 0d, 1d);
        settings.autoBoundaries = false;
        settings.xMin = xMin;
        settings.xMax = xMax;
        settings.yMin = yMin;
        settings.yMax = yMax;
        settings.yLabel = "".toCharArray();
        settings.xLabel = "Features".toCharArray();
        settings.title = "".toCharArray();
        settings.width = w;
        settings.height = h;

        canvasReference = CreateRGBABitmapImageReference();

        DrawScatterPlotFromSettings(canvasReference, settings);

        x1 = MapXCoordinateBasedOnSettings(27d, settings);
        y1 = MapYCoordinateBasedOnSettings(1d, settings);

        AssertEquals(floor(x1), 292d, failures);
        AssertEquals(y1, 60d, failures);
    }

    public static void ExampleRegression(RGBABitmapImageReference image){
        char [] xsStr, ysStr;
        double [] xs, ys, xs2, ys2;

        ScatterPlotSettings settings;

        xsStr = "20.1, 7.1, 16.1, 14.9, 16.7, 8.8, 9.7, 10.3, 22, 16.2, 12.1, 10.3, 14.5, 12.4, 9.6, 12.2, 10.8, 14.7, 19.7, 11.2, 10.1, 11, 12.2, 9.2, 23.5, 9.4, 15.3, 9.6, 11.1, 5.3, 7.8, 25.3, 16.5, 12.6, 12, 11.5, 17.1, 11.2, 12.2, 10.6, 19.9, 14.5, 15.5, 17.4, 8.4, 10.3, 10.2, 12.5, 16.7, 8.5, 12.2".toCharArray();
        ysStr = "31.5, 18.9, 35, 31.6, 22.6, 26.2, 14.1, 24.7, 44.8, 23.2, 31.4, 17.7, 18.4, 23.4, 22.6, 16.4, 21.4, 26.5, 31.7, 11.9, 20, 12.5, 18, 14.2, 37.6, 22.2, 17.8, 18.3, 28, 8.1, 14.7, 37.8, 15.7, 28.6, 11.7, 20.1, 30.1, 18.2, 17.2, 19.6, 29.2, 17.3, 28.2, 38.2, 17.8, 10.4, 19, 16.8, 21.5, 15.9, 17.7".toCharArray();

        xs = StringToNumberArray(xsStr);
        ys = StringToNumberArray(ysStr);

        settings = GetDefaultScatterPlotSettings();

        settings.scatterPlotSeries = new ScatterPlotSeries[2];
        settings.scatterPlotSeries[0] = new ScatterPlotSeries();
        settings.scatterPlotSeries[0].xs = xs;
        settings.scatterPlotSeries[0].ys = ys;
        settings.scatterPlotSeries[0].linearInterpolation = false;
        settings.scatterPlotSeries[0].pointType = "dots".toCharArray();
        settings.scatterPlotSeries[0].color = CreateRGBColor(1d, 0d, 0d);

        //OrdinaryLeastSquaresWithIntercept();

        xs2 = new double [2];
        ys2 = new double [2];

        xs2[0] = 5d;
        ys2[0] = 12d;
        xs2[1] = 25d;
        ys2[1] = 39d;

        settings.scatterPlotSeries[1] = new ScatterPlotSeries();
        settings.scatterPlotSeries[1].xs = xs2;
        settings.scatterPlotSeries[1].ys = ys2;
        settings.scatterPlotSeries[1].linearInterpolation = true;
        settings.scatterPlotSeries[1].lineType = "solid".toCharArray();
        settings.scatterPlotSeries[1].lineThickness = 2d;
        settings.scatterPlotSeries[1].color = CreateRGBColor(0d, 0d, 1d);

        settings.autoBoundaries = true;
        settings.yLabel = "".toCharArray();
        settings.xLabel = "".toCharArray();
        settings.title = "".toCharArray();
        settings.width = 600d;
        settings.height = 400d;

        DrawScatterPlotFromSettings(image, settings);
    }

    public static void BarPlotExample(RGBABitmapImageReference imageReference) {
        double [] ys1, ys2, ys3;
        BarPlotSettings settings;

        ys1 = StringToNumberArray("1, 2, 3, 4, 5".toCharArray());
        ys2 = StringToNumberArray("5, 4, 3, 2, 1".toCharArray());
        ys3 = StringToNumberArray("10, 2, 4, 3, 4".toCharArray());

        settings = GetDefaultBarPlotSettings();

        settings.autoBoundaries = true;
        //settings.yMax;
        //settings.yMin;
        settings.autoPadding = true;
        //settings.xPadding;
        //settings.yPadding;
        settings.title = "title".toCharArray();
        settings.showGrid = true;
        settings.gridColor = GetGray(0.1);
        settings.yLabel = "y label".toCharArray();
        settings.autoColor = true;
        settings.grayscaleAutoColor = false;
        settings.autoSpacing = true;
        //settings.groupSeparation;
        //settings.barSeparation;
        settings.autoLabels = false;
        settings.xLabels = new StringReference[5];
        settings.xLabels[0] = CreateStringReference("may 20".toCharArray());
        settings.xLabels[1] = CreateStringReference("jun 20".toCharArray());
        settings.xLabels[2] = CreateStringReference("jul 20".toCharArray());
        settings.xLabels[3] = CreateStringReference("aug 20".toCharArray());
        settings.xLabels[4] = CreateStringReference("sep 20".toCharArray());
        //settings.colors;
        settings.barBorder = true;

        settings.barPlotSeries = new BarPlotSeries[3];
        settings.barPlotSeries[0] = GetDefaultBarPlotSeriesSettings();
        settings.barPlotSeries[0].ys = ys1;
        settings.barPlotSeries[1] = GetDefaultBarPlotSeriesSettings();
        settings.barPlotSeries[1].ys = ys2;
        settings.barPlotSeries[2] = GetDefaultBarPlotSeriesSettings();
        settings.barPlotSeries[2].ys = ys3;

        DrawBarPlotFromSettings(imageReference, settings);
    }
}

































