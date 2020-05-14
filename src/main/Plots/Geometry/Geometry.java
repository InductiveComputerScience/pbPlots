package Plots.Geometry;

import references.references.*;

import static java.lang.Math.*;

public class Geometry {
    public static boolean CropLineWithinBoundary(NumberReference x1Ref, NumberReference y1Ref, NumberReference x2Ref, NumberReference y2Ref, double xMin, double xMax, double yMin, double yMax) {
        double x1, y1, x2, y2;
        boolean success, p1In, p2In;
        double dx, dy, f1, f2, f3, f4, f;

        x1 = x1Ref.numberValue;
        y1 = y1Ref.numberValue;
        x2 = x2Ref.numberValue;
        y2 = y2Ref.numberValue;

        p1In = x1 >= xMin && x1 <= xMax && y1 >= yMin && y1 <= yMax;
        p2In = x2 >= xMin && x2 <= xMax && y2 >= yMin && y2 <= yMax;

        if(p1In && p2In){
            success = true;
        }else if(!p1In && p2In){
            dx = x1 - x2;
            dy = y1 - y2;

            if(dx != 0d) {
                f1 = (xMin - x2) / dx;
                f2 = (xMax - x2) / dx;
            }else{
                f1 = 1d;
                f2 = 1d;
            }
            if(dy != 0d) {
                f3 = (yMin - y2)/dy;
                f4 = (yMax - y2)/dy;
            }else{
                f3 = 1d;
                f4 = 1d;
            }

            if(f1 < 0d){f1 = 1d;}
            if(f2 < 0d){f2 = 1d;}
            if(f3 < 0d){f3 = 1d;}
            if(f4 < 0d){f4 = 1d;}

            f = min(f1, min(f2, min(f3, f4)));

            x1 = x2 + f*dx;
            y1 = y2 + f*dy;

            success = true;
        }else if(p1In && !p2In){
            dx = x2 - x1;
            dy = y2 - y1;

            if(dx != 0d) {
                f1 = (xMin - x1) / dx;
                f2 = (xMax - x1) / dx;
            }else{
                f1 = 1d;
                f2 = 1d;
            }
            if(dy != 0d) {
                f3 = (yMin - y1) / dy;
                f4 = (yMax - y1) / dy;
            }else{
                f3 = 1d;
                f4 = 1d;
            }

            if(f1 < 0d){f1 = 1d;}
            if(f2 < 0d){f2 = 1d;}
            if(f3 < 0d){f3 = 1d;}
            if(f4 < 0d){f4 = 1d;}

            f = min(f1, min(f2, min(f3, f4)));

            x2 = x1 + f*dx;
            y2 = y1 + f*dy;

            success = true;
        }else{
            success = false;
        }

        x1Ref.numberValue = x1;
        y1Ref.numberValue = y1;
        x2Ref.numberValue = x2;
        y2Ref.numberValue = y2;

        return success;
    }

    public static double IncrementFromCoordinates(double x1, double y1, double x2, double y2) {
        return (x2 - x1)/(y2 - y1);
    }

    public static double InterceptFromCoordinates(double x1, double y1, double x2, double y2) {
        double a, b;

        a = IncrementFromCoordinates(x1, y1, x2, y2);
        b = y1 - a*x1;

        return b;
    }
}
