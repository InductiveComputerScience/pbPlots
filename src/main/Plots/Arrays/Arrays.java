package Plots.Arrays;

import static java.lang.Math.*;

public class Arrays {
    public static double GetMinimum(double[] data) {
        double i, minimum;

        minimum = data[0];
        for(i = 0d; i < data.length; i = i + 1d){
            minimum = min(minimum, data[(int)i]);
        }

        return minimum;
    }

    public static double GetMaximum(double[] data) {
        double i, maximum;

        maximum = data[0];
        for(i = 0d; i < data.length; i = i + 1d){
            maximum = max(maximum, data[(int)i]);
        }

        return maximum;
    }
}
