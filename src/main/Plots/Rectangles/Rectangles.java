package Plots.Rectangles;

import Plots.Plots.Rectangle;

public class Rectangles {
    public static boolean RectanglesOverlap(Rectangle r1, Rectangle r2) {
        boolean overlap;

        overlap = false;

        overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2);
        overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2);
        overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2);
        overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2);

        return overlap;
    }

    public static Rectangle CreateRectangle(double x1, double y1, double x2, double y2) {
        Rectangle r;
        r = new Rectangle();
        r.x1 = x1;
        r.y1 = y1;
        r.x2 = x2;
        r.y2 = y2;
        return r;
    }

    public static void CopyRectangleValues(Rectangle rd, Rectangle rs) {
        rd.x1 = rs.x1;
        rd.y1 = rs.y1;
        rd.x2 = rs.x2;
        rd.y2 = rs.y2;
    }
}
