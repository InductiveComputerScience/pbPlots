package Plots.Plots;

import Plots.Graphics.RGBABitmapImageReference;
import RGBABitmapImage.RGBABitmapImage.*;
import references.references.*;

import static BasicPixelFont.BasicPixelFont.BasicPixelFont.*;
import static Graphics2D.Graphics2D.Graphics2D.*;
import static Graphics2D.LinePatterns.LinePatterns.*;
import static Graphics2D.colors.colors.*;
import static Plots.Arrays.Arrays.*;
import static Plots.Common.Common.*;
import static Plots.Geometry.Geometry.*;
import static Plots.Rectangles.Rectangles.*;
import static Plots.TextDrawing.TextDrawing.*;
import static aarrays.arrays.arrays.*;
import static java.lang.Math.*;
import static math.math.math.*;
import static references.references.references.*;

public class Plots {
    public static ScatterPlotSettings GetDefaultScatterPlotSettings(){
        ScatterPlotSettings settings;

        settings = new ScatterPlotSettings();

        settings.autoBoundaries = true;
        settings.xMax = 0d;
        settings.xMin = 0d;
        settings.yMax = 0d;
        settings.yMin = 0d;
        settings.autoPadding = true;
        settings.xPadding = 0d;
        settings.yPadding = 0d;
        settings.title = "".toCharArray();
        settings.xLabel = "".toCharArray();
        settings.yLabel = "".toCharArray();
        settings.scatterPlotSeries = new ScatterPlotSeries[0];
        settings.showGrid = true;
        settings.gridColor = GetGray(0.1);
        settings.xAxisAuto = true;
        settings.xAxisTop = false;
        settings.xAxisBottom = false;
        settings.yAxisAuto = true;
        settings.yAxisLeft = false;
        settings.yAxisRight = false;

        return settings;
    }

    public static ScatterPlotSeries GetDefaultScatterPlotSeriesSettings(){
        ScatterPlotSeries series;

        series = new ScatterPlotSeries();

        series.linearInterpolation = true;
        series.pointType = "pixels".toCharArray();
        series.lineType = "solid".toCharArray();
        series.lineThickness = 1d;
        series.xs = new double[0];
        series.ys = new double[0];
        series.color = GetBlack();

        return series;
    }

    public static void DrawScatterPlot(RGBABitmapImageReference canvasReference, double width, double height, double[] xs, double[] ys) {
        ScatterPlotSettings settings;

        settings = GetDefaultScatterPlotSettings();

        settings.width = width;
        settings.height = height;
        settings.scatterPlotSeries = new ScatterPlotSeries[1];
        settings.scatterPlotSeries[0] = GetDefaultScatterPlotSeriesSettings();
        delete(settings.scatterPlotSeries[0].xs);
        settings.scatterPlotSeries[0].xs = xs;
        delete(settings.scatterPlotSeries[0].ys);
        settings.scatterPlotSeries[0].ys = ys;

        DrawScatterPlotFromSettings(canvasReference, settings);
    }

    public static boolean DrawScatterPlotFromSettings(RGBABitmapImageReference canvasReference, ScatterPlotSettings settings){
        double xMin, xMax, yMin, yMax, xLength, yLength, i, x, y, xPrev, yPrev, px, py, pxPrev, pyPrev, originX, originY, p, l, plot;
        Rectangle boundaries;
        double xPadding, yPadding, originXPixels, originYPixels;
        double xPixelMin, yPixelMin, xPixelMax, yPixelMax, xLengthPixels, yLengthPixels, axisLabelPadding;
        NumberReference nextRectangle, x1Ref, y1Ref, x2Ref, y2Ref, patternOffset;
        boolean prevSet, success;
        RGBA gridLabelColor;
        RGBABitmapImage canvas;
        double [] xs, ys;
        boolean linearInterpolation;
        ScatterPlotSeries sp;
        double [] xGridPositions, yGridPositions;
        StringArrayReference xLabels, yLabels;
        NumberArrayReference xLabelPriorities, yLabelPriorities;
        Rectangle occupied [];
        boolean [] linePattern;
        boolean originXInside, originYInside, textOnLeft, textOnBottom;
        double originTextX, originTextY, originTextXPixels, originTextYPixels, side;

        canvas = CreateImage(settings.width, settings.height, GetWhite());
        patternOffset = CreateNumberReference(0d);

        success = ScatterPlotFromSettingsValid(settings);

        if(success){

        boundaries = new Rectangle();
        ComputeBoundariesBasedOnSettings(settings, boundaries);
        xMin = boundaries.x1;
        yMin = boundaries.y1;
        xMax = boundaries.x2;
        yMax = boundaries.y2;

        // If zero, set to defaults.
        if(xMin - xMax == 0){
            xMin = 0;
            xMax = 10;
        }

        if(yMin - yMax == 0){
            yMin = 0;
            yMax = 10;
        }

        xLength = xMax - xMin;
        yLength = yMax - yMin;

        if(settings.autoPadding) {
            xPadding = floor(GetDefaultPaddingPercentage() * settings.width);
            yPadding = floor(GetDefaultPaddingPercentage() * settings.height);
        }else{
            xPadding = settings.xPadding;
            yPadding = settings.yPadding;
        }

        // Draw title
        DrawText(canvas, floor(settings.width/2d - GetTextWidth(settings.title)/2d), floor(yPadding/3d), settings.title, GetBlack());

        // Draw grid
        xPixelMin = xPadding;
        yPixelMin = yPadding;
        xPixelMax = settings.width - xPadding;
        yPixelMax = settings.height - yPadding;
        xLengthPixels = xPixelMax - xPixelMin;
        yLengthPixels = yPixelMax - yPixelMin;
        DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor);

        gridLabelColor = GetGray(0.5);

        xLabels = new StringArrayReference();
        xLabelPriorities = new NumberArrayReference();
        yLabels = new StringArrayReference();
        yLabelPriorities = new NumberArrayReference();
        xGridPositions = ComputeGridLinePositions(xMin, xMax, xLabels, xLabelPriorities);
        yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities);

        if(settings.showGrid) {
            // X-grid
            for (i = 0d; i < xGridPositions.length; i = i + 1d) {
                x = xGridPositions[(int) i];
                px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
                DrawLine1px(canvas, px, yPixelMin, px, yPixelMax, settings.gridColor);
            }

            // Y-grid
            for (i = 0; i < yGridPositions.length; i = i + 1d) {
                y = yGridPositions[(int) i];
                py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
                DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
            }
        }

        // Compute origin information.
        originYInside = yMin < 0d && yMax > 0d;
        originY = 0d;
        if(settings.xAxisAuto) {
            if (originYInside) {
                originY = 0d;
            } else {
                originY = yMin;
            }
        }else{
            if(settings.xAxisTop){
                originY = yMax;
            }
            if(settings.xAxisBottom){
                originY = yMin;
            }
        }
        originYPixels = MapYCoordinate(originY, yMin, yMax, yPixelMin, yPixelMax);

        originXInside = xMin < 0d && xMax > 0d;
        originX = 0d;
        if(settings.yAxisAuto) {
            if (originXInside) {
                originX = 0d;
            } else {
                originX = xMin;
            }
        }else{
            if(settings.yAxisLeft){
                originX = xMin;
            }
            if(settings.yAxisRight){
                originX = xMax;
            }
        }
        originXPixels = MapXCoordinate(originX, xMin, xMax, xPixelMin, xPixelMax);

        if(originYInside){
            originTextY = 0d;
        }else{
            originTextY = yMin + yLength/2d;
        }
        originTextYPixels = MapYCoordinate(originTextY, yMin, yMax, yPixelMin, yPixelMax);

        if(originXInside){
            originTextX = 0d;
        }else{
            originTextX = xMin + xLength/2d;
        }
        originTextXPixels = MapXCoordinate(originTextX, xMin, xMax, xPixelMin, xPixelMax);

        // Labels
        occupied = new Rectangle[(int)(xLabels.stringArray.length + yLabels.stringArray.length)];
        for(i = 0d; i < occupied.length; i = i + 1d){
            occupied[(int)i] = CreateRectangle(0d, 0d, 0d, 0d);
        }
        nextRectangle = CreateNumberReference(0d);

        // x labels
        for(i = 1d; i <= 5d; i = i + 1d){
            textOnBottom = true;
            if(!settings.xAxisAuto && settings.xAxisTop){
                textOnBottom = false;
            }
            DrawXLabelsForPriority(i, xMin, originYPixels, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied, textOnBottom);
        }

        // y labels
        for(i = 1d; i <= 5d; i = i + 1d){
            textOnLeft = true;
            if(!settings.yAxisAuto && settings.yAxisRight){
                textOnLeft = false;
            }
            DrawYLabelsForPriority(i, yMin, originXPixels, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, textOnLeft);
        }

        // Draw origin line axis titles.
        axisLabelPadding = 20d;

        // x origin line
        if(originYInside){
            DrawLine1px(canvas, Round(xPixelMin), Round(originYPixels), Round(xPixelMax), Round(originYPixels), GetBlack());
        }

        // y origin line
        if(originXInside){
            DrawLine1px(canvas, Round(originXPixels), Round(yPixelMin), Round(originXPixels), Round(yPixelMax), GetBlack());
        }

        // Draw origin axis titles.
        DrawTextUpwards(canvas, 10d, floor(originTextYPixels - GetTextWidth(settings.yLabel)/2d), settings.yLabel, GetBlack());
        DrawText(canvas, floor(originTextXPixels - GetTextWidth(settings.xLabel)/2d), yPixelMax + axisLabelPadding, settings.xLabel, GetBlack());

        // X-grid-markers
        for(i = 0d; i < xGridPositions.length; i = i + 1d){
            x = xGridPositions[(int)i];
            px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
            p = xLabelPriorities.numberArray[(int)i];
            l = 1d;
            if(p == 1d){
                l = 8d;
            }else if(p == 2d){
                l = 3d;
            }
            side = -1d;
            if(!settings.xAxisAuto && settings.xAxisTop){
                side = 1d;
            }
            DrawLine1px(canvas, px, originYPixels, px, originYPixels + side*l, GetBlack());
        }

        // Y-grid-markers
        for(i = 0d; i < yGridPositions.length; i = i + 1d){
            y = yGridPositions[(int)i];
            py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
            p = yLabelPriorities.numberArray[(int)i];
            l = 1d;
            if(p == 1d){
                l = 8d;
            }else if(p == 2d){
                l = 3d;
            }
            side = 1d;
            if(!settings.yAxisAuto && settings.yAxisRight){
                side = -1d;
            }
            DrawLine1px(canvas, originXPixels, py, originXPixels + side*l, py, GetBlack());
        }

        // Draw points
        for(plot = 0d; plot < settings.scatterPlotSeries.length; plot = plot + 1d){
            sp = settings.scatterPlotSeries[(int)plot];

            xs = sp.xs;
            ys = sp.ys;
            linearInterpolation = sp.linearInterpolation;

            x1Ref = new NumberReference();
            y1Ref = new NumberReference();
            x2Ref = new NumberReference();
            y2Ref = new NumberReference();
            if(linearInterpolation){
                prevSet = false;
                xPrev = 0d;
                yPrev = 0d;
                for(i = 0; i < xs.length; i = i + 1d){
                    x = xs[(int)i];
                    y = ys[(int)i];

                    if (prevSet) {
                        x1Ref.numberValue = xPrev;
                        y1Ref.numberValue = yPrev;
                        x2Ref.numberValue = x;
                        y2Ref.numberValue = y;

                        success = CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax);

                        if(success) {
                            pxPrev = floor(MapXCoordinate(x1Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax));
                            pyPrev = floor(MapYCoordinate(y1Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax));
                            px = floor(MapXCoordinate(x2Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax));
                            py = floor(MapYCoordinate(y2Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax));

                            if(aStringsEqual(sp.lineType, "solid".toCharArray()) && sp.lineThickness == 1d) {
                                DrawLine1px(canvas, pxPrev, pyPrev, px, py, sp.color);
                            }else if(aStringsEqual(sp.lineType, "solid".toCharArray())) {
                                DrawLine(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, sp.color);
                            }else if(aStringsEqual(sp.lineType, "dashed".toCharArray())){
                                linePattern = GetLinePattern1();
                                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
                            }else if(aStringsEqual(sp.lineType, "dotted".toCharArray())){
                                linePattern = GetLinePattern2();
                                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
                            }else if(aStringsEqual(sp.lineType, "dotdash".toCharArray())){
                                linePattern = GetLinePattern3();
                                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
                            }else if(aStringsEqual(sp.lineType, "longdash".toCharArray())){
                                linePattern = GetLinePattern4();
                                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
                            }else if(aStringsEqual(sp.lineType, "twodash".toCharArray())){
                                linePattern = GetLinePattern5();
                                DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
                            }
                        }
                    }

                    prevSet = true;
                    xPrev = x;
                    yPrev = y;
                }
            }else{
                for(i = 0; i < xs.length; i = i + 1d){
                    x = xs[(int)i];
                    y = ys[(int)i];

                    if(x > xMin && x < xMax && y > yMin && y < yMax) {

                        x = floor(MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax));
                        y = floor(MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax));

                        if (aStringsEqual(sp.pointType, "crosses".toCharArray())) {
                            DrawPixel(canvas, x, y, sp.color);
                            DrawPixel(canvas, x + 1d, y, sp.color);
                            DrawPixel(canvas, x + 2d, y, sp.color);
                            DrawPixel(canvas, x - 1d, y, sp.color);
                            DrawPixel(canvas, x - 2d, y, sp.color);
                            DrawPixel(canvas, x, y + 1d, sp.color);
                            DrawPixel(canvas, x, y + 2d, sp.color);
                            DrawPixel(canvas, x, y - 1d, sp.color);
                            DrawPixel(canvas, x, y - 2d, sp.color);
                        } else if (aStringsEqual(sp.pointType, "circles".toCharArray())) {
                            DrawCircle(canvas, x, y, 3d, sp.color);
                        } else if (aStringsEqual(sp.pointType, "dots".toCharArray())) {
                            DrawFilledCircle(canvas, x, y, 3d, sp.color);
                        } else if (aStringsEqual(sp.pointType, "triangles".toCharArray())) {
                            DrawTriangle(canvas, x, y, 3d, sp.color);
                        } else if (aStringsEqual(sp.pointType, "filled triangles".toCharArray())) {
                            DrawFilledTriangle(canvas, x, y, 3d, sp.color);
                        } else if (aStringsEqual(sp.pointType, "pixels".toCharArray())) {
                            DrawPixel(canvas, x, y, sp.color);
                        }
                    }
                }
            }
        }

        canvasReference.image = canvas;

        }

        return success;
    }

    public static void ComputeBoundariesBasedOnSettings(ScatterPlotSettings settings, Rectangle boundaries){
        ScatterPlotSeries sp;
        double plot, xMin, xMax, yMin, yMax;

        if(settings.scatterPlotSeries.length >= 1d) {
            xMin = GetMinimum(settings.scatterPlotSeries[0].xs);
            xMax = GetMaximum(settings.scatterPlotSeries[0].xs);
            yMin = GetMinimum(settings.scatterPlotSeries[0].ys);
            yMax = GetMaximum(settings.scatterPlotSeries[0].ys);
        }else{
            xMin = -10d;
            xMax = 10d;
            yMin = -10d;
            yMax = 10d;
        }

        if (!settings.autoBoundaries) {
            xMin = settings.xMin;
            xMax = settings.xMax;
            yMin = settings.yMin;
            yMax = settings.yMax;
        }else{
            for(plot = 1d; plot < settings.scatterPlotSeries.length; plot = plot + 1d) {
                sp = settings.scatterPlotSeries[(int) plot];

                xMin = min(xMin, GetMinimum(sp.xs));
                xMax = max(xMax, GetMaximum(sp.xs));
                yMin = min(yMin, GetMinimum(sp.ys));
                yMax = max(yMax, GetMaximum(sp.ys));
            }
        }

        boundaries.x1 = xMin;
        boundaries.y1 = yMin;
        boundaries.x2 = xMax;
        boundaries.y2 = yMax;
    }

    public static boolean ScatterPlotFromSettingsValid(ScatterPlotSettings settings) {
        boolean success, found;
        ScatterPlotSeries series;
        double i;

        success = true;

        // Check axis placement.
        if(!settings.xAxisAuto){
            if(settings.xAxisTop && settings.xAxisBottom){
                success = false;
            }
            if(!settings.xAxisTop && !settings.xAxisBottom){
                success = false;
            }
        }

        if(!settings.yAxisAuto){
            if(settings.yAxisLeft && settings.yAxisRight){
                success = false;
            }
            if(!settings.yAxisLeft && !settings.yAxisRight){
                success = false;
            }
        }

        // Check series lengths.
        for(i = 0d; i < settings.scatterPlotSeries.length; i = i + 1d){
            series = settings.scatterPlotSeries[(int) i];
            if(series.xs.length != series.ys.length){
                success = false;
            }
            if(series.xs.length == 0d){
                success = false;
            }
            if(series.linearInterpolation && series.xs.length == 1d){
                success = false;
            }
        }

        // Check bounds.
        if(!settings.autoBoundaries){
            if(settings.xMin >= settings.xMax){
                success = false;
            }
            if(settings.yMin >= settings.yMax){
                success = false;
            }
        }

        // Check padding.
        if(!settings.autoPadding) {
            if (2d * settings.xPadding >= settings.width) {
                success = false;
            }
            if (2d * settings.yPadding >= settings.height) {
                success = false;
            }
        }

        // Check width and height.
        if (settings.width < 0d){
            success = false;
        }
        if (settings.height < 0d) {
            success = false;
        }

        // Check point types.
        for(i = 0d; i < settings.scatterPlotSeries.length; i = i + 1d) {
            series = settings.scatterPlotSeries[(int) i];

            if(series.lineThickness < 0d){
                success = false;
            }

            if(!series.linearInterpolation){
                // Point type.
                found = false;
                if (aStringsEqual(series.pointType, "crosses".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.pointType, "circles".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.pointType, "dots".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.pointType, "triangles".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.pointType, "filled triangles".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.pointType, "pixels".toCharArray())) {
                    found = true;
                }
                if (!found) {
                    success = false;
                }
            }else{
                // Line type.
                found = false;
                if (aStringsEqual(series.lineType, "solid".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.lineType, "dashed".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.lineType, "dotted".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.lineType, "dotdash".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.lineType, "longdash".toCharArray())) {
                    found = true;
                } else if (aStringsEqual(series.lineType, "twodash".toCharArray())) {
                    found = true;
                }

                if (!found) {
                    success = false;
                }
            }
        }

        return success;
    }

    public static void delete(Object o){
        // Java has garbage collection.
    }
}
