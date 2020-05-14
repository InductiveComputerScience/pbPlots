package Plots.BarPlot;

import static BasicPixelFont.BasicPixelFont.BasicPixelFont.GetTextWidth;
import static Graphics2D.Graphics2D.Graphics2D.*;
import static Graphics2D.colors.colors.*;
import static Plots.Arrays.Arrays.*;
import static Plots.Colors.Colors.*;
import static Plots.Common.Common.*;
import static Plots.Graphics.Graphics.*;
import static Plots.Rectangles.Rectangles.*;
import static Plots.TextDrawing.TextDrawing.DrawText;
import static Plots.TextDrawing.TextDrawing.DrawTextUpwards;
import static java.lang.Math.*;
import static math.math.math.Round;
import static numbers.NumberToString.NumberToString.CreateStringDecimalFromNumber;
import static references.references.references.*;

import Plots.Graphics.RGBABitmapImageReference;
import Plots.Plots.*;
import RGBABitmapImage.RGBABitmapImage.*;
import references.references.*;

public class BarPlot {
    public static BarPlotSettings GetDefaultBarPlotSettings(){
        BarPlotSettings settings;

        settings = new BarPlotSettings();

        settings.width = 800d;
        settings.height = 600d;
        settings.autoBoundaries = true;
        settings.yMax = 0d;
        settings.yMin = 0d;
        settings.autoPadding = true;
        settings.xPadding = 0d;
        settings.yPadding = 0d;
        settings.title = "".toCharArray();
        settings.yLabel = "".toCharArray();
        settings.barPlotSeries = new BarPlotSeries[0];
        settings.showGrid = true;
        settings.gridColor = GetGray(0.1);
        settings.autoColor = true;
        settings.grayscaleAutoColor = false;
        settings.autoSpacing = true;
        settings.groupSeparation = 0d;
        settings.barSeparation = 0d;
        settings.autoLabels = true;
        settings.xLabels = new StringReference [0];
        /*settings.autoLabels = false;
        settings.xLabels = new StringReference [5];
        settings.xLabels[0] = CreateStringReference("may 20".toCharArray());
        settings.xLabels[1] = CreateStringReference("jun 20".toCharArray());
        settings.xLabels[2] = CreateStringReference("jul 20".toCharArray());
        settings.xLabels[3] = CreateStringReference("aug 20".toCharArray());
        settings.xLabels[4] = CreateStringReference("sep 20".toCharArray());*/
        settings.barBorder = false;

        return settings;
    }

    public static BarPlotSeries GetDefaultBarPlotSeriesSettings(){
        BarPlotSeries series;

        series = new BarPlotSeries();

        series.ys = new double[0];
        series.color = GetBlack();

        return series;
    }

    public static RGBABitmapImage DrawBarPlot(double width, double height, double[] ys) {
        BarPlotSettings settings;
        RGBABitmapImageReference canvasReference;

        settings = GetDefaultBarPlotSettings();

        settings.barPlotSeries = new BarPlotSeries[1];
        settings.barPlotSeries[0] = GetDefaultBarPlotSeriesSettings();
        delete(settings.barPlotSeries[0].ys);
        settings.barPlotSeries[0].ys = ys;
        canvasReference = new RGBABitmapImageReference();
        settings.width = width;
        settings.height = height;

        DrawBarPlotFromSettings(canvasReference, settings);

        return canvasReference.image;
    }

    public static void delete(Object o){
    }

    public static boolean DrawBarPlotFromSettings(RGBABitmapImageReference canvasReference, BarPlotSettings settings){
        double xPadding, yPadding;
        double xPixelMin, yPixelMin, yPixelMax, xPixelMax;
        double xLengthPixels, yLengthPixels;
        double s, n, y, x, w, h, yMin, yMax, b, i, py, yValue;
        RGBA [] colors;
        double [] ys, yGridPositions;
        double yTop, yBottom, ss, bs, yLength;
        double groupSeparation, barSeparation, barWidth, textwidth;
        StringArrayReference yLabels;
        NumberArrayReference yLabelPriorities;
        Rectangle [] occupied;
        NumberReference nextRectangle;
        RGBA gridLabelColor, barColor;
        char [] label;
        boolean success;
        RGBABitmapImage canvas;

        success = BarPlotSettingsIsValid(settings);

        if(success){

        canvas = CreateImage(settings.width, settings.height, GetWhite());

        ss = settings.barPlotSeries.length;
        gridLabelColor = GetGray(0.5);

        // padding
        if(settings.autoPadding) {
            xPadding = floor(GetDefaultPaddingPercentage() * ImageWidth(canvas));
            yPadding = floor(GetDefaultPaddingPercentage() * ImageHeight(canvas));
        }else{
            xPadding = settings.xPadding;
            yPadding = settings.yPadding;
        }

        // Draw title
        DrawText(canvas, floor(ImageWidth(canvas)/2d - GetTextWidth(settings.title)/2d), floor(yPadding/3d), settings.title, GetBlack());
        DrawTextUpwards(canvas, 10d, floor(ImageHeight(canvas) / 2d - GetTextWidth(settings.yLabel)/2d), settings.yLabel, GetBlack());

        // min and max
        if(settings.autoBoundaries) {
            if (ss >= 1d) {
                yMax = GetMaximum(settings.barPlotSeries[0].ys);
                yMin = min(0d, GetMinimum(settings.barPlotSeries[0].ys));

                for (s = 0d; s < ss; s = s + 1d) {
                    yMax = max(yMax, GetMaximum(settings.barPlotSeries[(int) s].ys));
                    yMin = min(yMin, GetMinimum(settings.barPlotSeries[(int) s].ys));
                }
            } else {
                yMax = 10d;
                yMin = 0d;
            }
        }else{
            yMin = settings.yMin;
            yMax = settings.yMax;
        }
        yLength = yMax - yMin;

        // boundaries
        xPixelMin = xPadding;
        yPixelMin = yPadding;
        xPixelMax = ImageWidth(canvas) - xPadding;
        yPixelMax = ImageHeight(canvas) - yPadding;
        xLengthPixels = xPixelMax - xPixelMin;
        yLengthPixels = yPixelMax - yPixelMin;

        // Draw boundary.
        DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor);

        // Draw grid lines.
        yLabels = new StringArrayReference();
        yLabelPriorities = new NumberArrayReference();
        yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities);

        if(settings.showGrid) {
            // Y-grid
            for (i = 0; i < yGridPositions.length; i = i + 1d) {
                y = yGridPositions[(int) i];
                py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
                DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
            }
        }

        // Draw origin.
        if(yMin < 0d && yMax > 0d){
            py = MapYCoordinate(0d, yMin, yMax, yPixelMin, yPixelMax);
            DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
        }

        // Labels
        occupied = new Rectangle[(int)(yLabels.stringArray.length)];
        for(i = 0d; i < occupied.length; i = i + 1d){
            occupied[(int)i] = CreateRectangle(0d, 0d, 0d, 0d);
        }
        nextRectangle = CreateNumberReference(0d);

        for(i = 1d; i <= 5d; i = i + 1d){
            DrawYLabelsForPriority(i, yMin, xPixelMin, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, true);
        }

        // Draw bars.
        if(settings.autoColor) {
            if(!settings.grayscaleAutoColor) {
                colors = Get8HighContrastColors();
            }else{
                colors = new RGBA [(int)ss];
                if(ss > 1d) {
                    for (i = 0d; i < ss; i = i + 1d) {
                        colors[(int) i] = GetGray(0.7 - (i / ss) * 0.7);
                    }
                }else{
                    colors[0] = GetGray(0.5);
                }
            }
        }else{
            colors = new RGBA [0];
        }

        // distances
        bs = settings.barPlotSeries[0].ys.length;

        if(settings.autoSpacing) {
            groupSeparation = ImageWidth(canvas) * 0.05;
            barSeparation = ImageWidth(canvas) * 0.005;
        }else{
            groupSeparation = settings.groupSeparation;
            barSeparation = settings.barSeparation;
        }

        barWidth = (xLengthPixels - groupSeparation*(bs - 1d) - barSeparation*(bs * (ss - 1d))) / (bs * ss);

        // Draw bars.
        b = 0d;
        for(n = 0d; n < bs; n = n + 1d) {
            for(s = 0d; s < ss; s = s + 1d){
                ys = settings.barPlotSeries[(int) s].ys;

                yValue = ys[(int) n];

                yBottom = MapYCoordinate(yValue, yMin, yMax, yPixelMin, yPixelMax);
                yTop = MapYCoordinate(0d, yMin, yMax, yPixelMin, yPixelMax);

                x = xPixelMin + n * (groupSeparation + ss * barWidth) + s * (barWidth) + b * barSeparation;
                w = barWidth;

                if(yValue >= 0d) {
                    y = yBottom;
                    h = yTop - y;
                }else{
                    y = yTop;
                    h = yBottom - yTop;
                }

                // Cut at boundaries.
                if(y < yPixelMin && y + h > yPixelMax){
                    y = yPixelMin;
                    h = yPixelMax - yPixelMin;
                }else if(y < yPixelMin){
                    y = yPixelMin;
                    if(yValue >= 0d) {
                        h = yTop - y;
                    }else{
                        h = yBottom - y;
                    }
                }else if(y + h > yPixelMax){
                    h = yPixelMax - y;
                }

                // Get color
                if(settings.autoColor){
                    barColor = colors[(int) s];
                }else{
                    barColor = settings.barPlotSeries[(int) s].color;
                }

                // Draw
                if(settings.barBorder) {
                    DrawFilledRectangleWithBorder(canvas, Round(x), Round(y), Round(w), Round(h), GetBlack(), barColor);
                }else{
                    DrawFilledRectangle(canvas, Round(x), Round(y), Round(w), Round(h), barColor);
                }

                b = b + 1d;
            }
            b = b - 1d;
        }

        // x-labels
        for(n = 0d; n < bs; n = n + 1d){
            if(settings.autoLabels) {
                label = CreateStringDecimalFromNumber(n + 1d);
            }else{
                label = settings.xLabels[(int)n].string;
            }

            textwidth = GetTextWidth(label);

            x = xPixelMin + (n + 0.5)*(ss*barWidth + (ss-1d)*barSeparation) + n*groupSeparation - textwidth/2d;

            DrawText(canvas, floor(x), ImageHeight(canvas) - yPadding + 20d, label, gridLabelColor);

            b = b + 1d;
        }

        canvasReference.image = canvas;

        }

        return success;
    }

    public static boolean BarPlotSettingsIsValid(BarPlotSettings settings) {
        boolean success, lengthSet;
        BarPlotSeries series;
        double i, width, height, length;

        success = true;

        // Check series lengths.
        lengthSet = false;
        length = 0d;
        for(i = 0d; i < settings.barPlotSeries.length; i = i + 1d){
            series = settings.barPlotSeries[(int) i];

            if(!lengthSet){
                length = series.ys.length;
                lengthSet = true;
            }else{
                if(length != series.ys.length){
                    success = false;
                }
            }
        }

        // Check bounds.
        if(!settings.autoBoundaries){
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

        // Check spacing
        if(!settings.autoSpacing) {
            if(settings.groupSeparation < 0d){
                success = false;
            }
            if(settings.barSeparation < 0d){
                success = false;
            }
        }

        return success;
    }
}





