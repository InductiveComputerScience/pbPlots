package tests;

import Plots.Graphics.*;
import Plots.Plots.*;
import references.references.*;

import static Plots.BarPlot.BarPlot.*;
import static Plots.Common.Common.*;
import static Plots.Graphics.Graphics.*;
import static Plots.Plots.Plots.*;
import static references.references.references.*;
import static testing.testing.testing.*;

public class tests {
    public static double test() {
        ScatterPlotSettings scatterPlotSettings;
        double z;
        double [] gridlines;
        NumberReference failures;
        StringArrayReference labels;
        NumberArrayReference labelPriorities;
        RGBABitmapImageReference imageReference;
        double [] xs, ys;

        failures = CreateNumberReference(0d);

        imageReference = CreateRGBABitmapImageReference();

        scatterPlotSettings = GetDefaultScatterPlotSettings();

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

        return failures.numberValue;
    }
}
