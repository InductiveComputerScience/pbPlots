package Plots.Math;

import static java.lang.Math.*;
import static math.math.math.*;

public class Math {
    public static double RoundToDigits(double element, double digitsAfterPoint){
        return Round(element*pow(10d, digitsAfterPoint))/pow(10d, digitsAfterPoint);
    }
}
