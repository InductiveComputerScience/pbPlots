package Plots.Common;

import Plots.Plots.*;
import RGBABitmapImage.RGBABitmapImage.*;
import references.references.*;

import static BasicPixelFont.BasicPixelFont.BasicPixelFont.*;
import static Graphics2D.Graphics2D.Graphics2D.*;
import static Plots.Arrays.Arrays.*;
import static Plots.Math.Math.*;
import static Plots.Rectangles.Rectangles.*;
import static Plots.TextDrawing.TextDrawing.*;
import static java.lang.Math.*;
import static math.math.math.*;
import static numbers.NumberToString.NumberToString.*;

public class Common {
    public static void DrawXLabelsForPriority(double p, double xMin, double oy, double xMax, double xPixelMin, double xPixelMax, NumberReference nextRectangle, RGBA gridLabelColor, RGBABitmapImage canvas, double[] xGridPositions, StringArrayReference xLabels, NumberArrayReference xLabelPriorities, Rectangle[] occupied, boolean textOnBottom) {
        boolean overlap, currentOverlaps;
        double i, j, x, px, padding;
        char[] text;
        Rectangle r;

        r = new Rectangle();
        padding = 10d;

        overlap = false;
        for(i = 0d; i < xLabels.stringArray.length; i = i + 1d){
            if(xLabelPriorities.numberArray[(int)i] == p){

                x = xGridPositions[(int)i];
                px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
                text = xLabels.stringArray[(int) i].string;

                r.x1 = floor(px - GetTextWidth(text)/2d);
                if(textOnBottom){
                    r.y1 = floor(oy + 5d);
                }else{
                    r.y1 = floor(oy - 20d);
                }
                r.x2 = r.x1 + GetTextWidth(text);
                r.y2 = r.y1 + GetTextHeight(text);

                // Add padding
                r.x1 = r.x1 - padding;
                r.y1 = r.y1 - padding;
                r.x2 = r.x2 + padding;
                r.y2 = r.y2 + padding;

                currentOverlaps = false;

                for(j = 0d; j < nextRectangle.numberValue; j = j + 1d){
                    currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[(int)j]);
                }

                if(!currentOverlaps && p == 1d){
                    DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor);

                    CopyRectangleValues(occupied[(int)nextRectangle.numberValue], r);
                    nextRectangle.numberValue = nextRectangle.numberValue + 1d;
                }

                overlap = overlap || currentOverlaps;
            }
        }
        if(!overlap && p != 1d){
            for(i = 0d; i < xGridPositions.length; i = i + 1d){
                x = xGridPositions[(int)i];
                px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);

                if(xLabelPriorities.numberArray[(int)i] == p) {
                    text = xLabels.stringArray[(int) i].string;

                    r.x1 = floor(px - GetTextWidth(text)/2d);
                    if(textOnBottom){
                        r.y1 = floor(oy + 5d);
                    }else{
                        r.y1 = floor(oy - 20d);
                    }
                    r.x2 = r.x1 + GetTextWidth(text);
                    r.y2 = r.y1 + GetTextHeight(text);

                    DrawText(canvas, r.x1, r.y1, text, gridLabelColor);

                    CopyRectangleValues(occupied[(int)nextRectangle.numberValue], r);
                    nextRectangle.numberValue = nextRectangle.numberValue + 1d;
                }
            }
        }
    }

    public static void DrawYLabelsForPriority(double p, double yMin, double ox, double yMax, double yPixelMin, double yPixelMax, NumberReference nextRectangle, RGBA gridLabelColor, RGBABitmapImage canvas, double[] yGridPositions, StringArrayReference yLabels, NumberArrayReference yLabelPriorities, Rectangle[] occupied, boolean textOnLeft) {
        boolean overlap, currentOverlaps;
        double i, j, y, py, padding;
        char[] text;
        Rectangle r;

        r = new Rectangle();
        padding = 10d;

        overlap = false;
        for(i = 0d; i < yLabels.stringArray.length; i = i + 1d){
            if(yLabelPriorities.numberArray[(int)i] == p){

                y = yGridPositions[(int)i];
                py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
                text = yLabels.stringArray[(int) i].string;

                if(textOnLeft) {
                    r.x1 = floor(ox - GetTextWidth(text) - 10d);
                }else{
                    r.x1 = floor(ox + 10d);
                }
                r.y1 = floor(py - 6d);
                r.x2 = r.x1 + GetTextWidth(text);
                r.y2 = r.y1 + GetTextHeight(text);

                // Add padding
                r.x1 = r.x1 - padding;
                r.y1 = r.y1 - padding;
                r.x2 = r.x2 + padding;
                r.y2 = r.y2 + padding;

                currentOverlaps = false;

                for(j = 0d; j < nextRectangle.numberValue; j = j + 1d){
                    currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[(int)j]);
                }

                // Draw labels with priority 1 if they do not overlap anything else.
                if(!currentOverlaps && p == 1d){
                    DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor);

                    CopyRectangleValues(occupied[(int)nextRectangle.numberValue], r);
                    nextRectangle.numberValue = nextRectangle.numberValue + 1d;
                }

                overlap = overlap || currentOverlaps;
            }
        }
        if(!overlap && p != 1d){
            for(i = 0d; i < yGridPositions.length; i = i + 1d){
                y = yGridPositions[(int)i];
                py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);

                if(yLabelPriorities.numberArray[(int)i] == p) {
                    text = yLabels.stringArray[(int) i].string;

                    if(textOnLeft) {
                        r.x1 = floor(ox - GetTextWidth(text) - 10d);
                    }else{
                        r.x1 = floor(ox + 10d);
                    }
                    r.y1 = floor(py - 6d);
                    r.x2 = r.x1 + GetTextWidth(text);
                    r.y2 = r.y1 + GetTextHeight(text);

                    DrawText(canvas, r.x1, r.y1, text, gridLabelColor);

                    CopyRectangleValues(occupied[(int)nextRectangle.numberValue], r);
                    nextRectangle.numberValue = nextRectangle.numberValue + 1d;
                }
            }
        }
    }

    public static double[] ComputeGridLinePositions(double cMin, double cMax, StringArrayReference labels, NumberArrayReference priorities) {
        double[] positions;
        double cLength, p, pMin, pMax, pInterval, pNum, i, num, rem, priority, mode;

        cLength = cMax - cMin;

        p = floor(log10(cLength));
        pInterval = pow(10d, p); // gives 10-1 lines for 100-10 diff
        pMin = ceil(cMin / pInterval)*pInterval;
        pMax = floor(cMax / pInterval)*pInterval;
        pNum = Round((pMax - pMin)/pInterval + 1d);

        mode = 1;

        if(pNum <= 3d){
            p = floor(log10(cLength) - 1d); // gives 100-10 lines for 100-10 diff
            pInterval = pow(10d, p);
            pMin = ceil(cMin / pInterval)*pInterval;
            pMax = floor(cMax / pInterval)*pInterval;
            pNum = Round((pMax - pMin)/pInterval + 1d);

            mode = 4d;
        }else if(pNum <= 6d){
            p = floor(log10(cLength));
            pInterval = pow(10d, p)/4d; // gives 40-5 lines for 100-10 diff
            pMin = ceil(cMin / pInterval)*pInterval;
            pMax = floor(cMax / pInterval)*pInterval;
            pNum = Round((pMax - pMin)/pInterval + 1d);

            mode = 3d;
        }else if(pNum <= 10d){
            p = floor(log10(cLength));
            pInterval = pow(10d, p)/2d; // gives 20-3 lines for 100-10 diff
            pMin = ceil(cMin / pInterval)*pInterval;
            pMax = floor(cMax / pInterval)*pInterval;
            pNum = Round((pMax - pMin)/pInterval + 1d);

            mode = 2d;
        }

        positions = new double [(int)pNum];
        labels.stringArray = new StringReference[(int)pNum];
        priorities.numberArray = new double [(int)pNum];

        for(i = 0d; i < pNum; i = i + 1d){
            num = pMin + pInterval * i;
            positions[(int)i] = num;

            // Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn.
            priority = 1d;

            // Prioritize x.25, x.5 and x.75 lower.
            if(mode == 2d || mode == 3d) {
                rem = abs(round(num / pow(10d, p - 2d))) % 100d;

                priority = 1d;
                if (rem == 50d) {
                    priority = 2d;
                } else if (rem == 25d || rem == 75d) {
                    priority = 3d;
                }
            }

            // Prioritize x.1-x.4 and x.6-x.9 lower
            if(mode == 4d) {
                rem = abs(Round(num / pow(10d, p))) % 10d;

                priority = 1d;
                if (rem == 1d || rem == 2d || rem == 3d || rem == 4d || rem == 6d || rem == 7d || rem == 8d || rem == 9d) {
                    priority = 2d;
                }
            }

            // 0 has lowest priority.
            if(EpsilonCompare(num, 0d, pow(10d, p-5d))){
                priority = 3d;
            }

            priorities.numberArray[(int)i] = priority;

            // The label itself.
            labels.stringArray[(int)i] = new StringReference();
            if(p < 0d) {
                if(mode == 2d || mode == 3d) {
                    num = RoundToDigits(num, -(p - 1d));
                }else{
                    num = RoundToDigits(num, -p);
                }
            }
            labels.stringArray[(int) i].string = CreateStringDecimalFromNumber(num);
        }

        return positions;
    }

    public static double MapYCoordinate(double y, double yMin, double yMax, double yPixelMin, double yPixelMax) {
        double yLength, yPixelLength;

        yLength = yMax - yMin;
        yPixelLength = yPixelMax - yPixelMin;

        y = y - yMin;
        y = y * yPixelLength/yLength;
        y = yPixelLength - y;
        y = y + yPixelMin;
        return y;
    }

    public static double MapXCoordinate(double x, double xMin, double xMax, double xPixelMin, double xPixelMax) {
        double xLength, xPixelLength;

        xLength = xMax - xMin;
        xPixelLength = xPixelMax - xPixelMin;

        x = x - xMin;
        x = x * xPixelLength/xLength;
        x = x + xPixelMin;
        return x;
    }

    public static double MapXCoordinateAutoSettings(double x, RGBABitmapImage image, double[] xs) {
        return MapXCoordinate(x, GetMinimum(xs), GetMaximum(xs) - GetMinimum(xs), GetDefaultPaddingPercentage() * ImageWidth(image), (1d - GetDefaultPaddingPercentage()) * ImageWidth(image));
    }

    public static double MapYCoordinateAutoSettings(double y, RGBABitmapImage image, double[] ys) {
        return MapYCoordinate(y, GetMinimum(ys), GetMaximum(ys), GetDefaultPaddingPercentage() * ImageHeight(image), (1d - GetDefaultPaddingPercentage())*ImageHeight(image));
    }

    public static double GetDefaultPaddingPercentage() {
        return 0.10d;
    }
}
