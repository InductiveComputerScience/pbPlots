// Downloaded from https://repo.progsbase.com - Code Developed Using progsbase.

using static System.Math;

public class RGBABitmapImageReference{
	public RGBABitmapImage image;
}
public class Rectangle{
	public double x1;
	public double x2;
	public double y1;
	public double y2;
}
public class ScatterPlotSeries{
	public bool linearInterpolation;
	public char [] pointType;
	public char [] lineType;
	public double lineThickness;
	public double [] xs;
	public double [] ys;
	public RGBA color;
}
public class ScatterPlotSettings{
	public ScatterPlotSeries [] scatterPlotSeries;
	public bool autoBoundaries;
	public double xMax;
	public double xMin;
	public double yMax;
	public double yMin;
	public bool autoPadding;
	public double xPadding;
	public double yPadding;
	public char [] xLabel;
	public char [] yLabel;
	public char [] title;
	public bool showGrid;
	public RGBA gridColor;
	public bool xAxisAuto;
	public bool xAxisTop;
	public bool xAxisBottom;
	public bool yAxisAuto;
	public bool yAxisLeft;
	public bool yAxisRight;
	public double width;
	public double height;
}
public class BarPlotSeries{
	public double [] ys;
	public RGBA color;
}
public class BarPlotSettings{
	public double width;
	public double height;
	public bool autoBoundaries;
	public double yMax;
	public double yMin;
	public bool autoPadding;
	public double xPadding;
	public double yPadding;
	public char [] title;
	public bool showGrid;
	public RGBA gridColor;
	public BarPlotSeries [] barPlotSeries;
	public char [] yLabel;
	public bool autoColor;
	public bool grayscaleAutoColor;
	public bool autoSpacing;
	public double groupSeparation;
	public double barSeparation;
	public bool autoLabels;
	public StringReference [] xLabels;
	public bool barBorder;
}
public class RGBA{
	public double r;
	public double g;
	public double b;
	public double a;
}
public class RGBABitmap{
	public RGBA [] y;
}
public class RGBABitmapImage{
	public RGBABitmap [] x;
}
public class BooleanArrayReference{
	public bool [] booleanArray;
}
public class BooleanReference{
	public bool booleanValue;
}
public class CharacterReference{
	public char characterValue;
}
public class NumberArrayReference{
	public double [] numberArray;
}
public class NumberReference{
	public double numberValue;
}
public class StringArrayReference{
	public StringReference [] stringArray;
}
public class StringReference{
	public char [] stringx;
}
public class Chunk{
	public double length;
	public char [] type;
	public double [] data;
	public double crc;
}
public class IHDR{
	public double Width;
	public double Height;
	public double BitDepth;
	public double ColourType;
	public double CompressionMethod;
	public double FilterMethod;
	public double InterlaceMethod;
}
public class PHYS{
	public double pixelsPerMeter;
}
public class PNGImage{
	public double [] signature;
	public IHDR ihdr;
	public ZLIBStruct zlibStruct;
	public bool physPresent;
	public PHYS phys;
}
public class ZLIBStruct{
	public double CMF;
	public double CM;
	public double CINFO;
	public double FLG;
	public double FCHECK;
	public double FDICT;
	public double FLEVEL;
	public double [] CompressedDataBlocks;
	public double Adler32CheckValue;
}
public class LinkedListNodeStrings{
	public bool end;
	public char [] value;
	public LinkedListNodeStrings next;
}
public class LinkedListStrings{
	public LinkedListNodeStrings first;
	public LinkedListNodeStrings last;
}
public class LinkedListNodeNumbers{
	public LinkedListNodeNumbers next;
	public bool end;
	public double value;
}
public class LinkedListNumbers{
	public LinkedListNodeNumbers first;
	public LinkedListNodeNumbers last;
}
public class LinkedListCharacters{
	public LinkedListNodeCharacters first;
	public LinkedListNodeCharacters last;
}
public class LinkedListNodeCharacters{
	public bool end;
	public char value;
	public LinkedListNodeCharacters next;
}
public class DynamicArrayNumbers{
	public double [] array;
	public double length;
}
public class pbPlots{
	public static bool CropLineWithinBoundary(NumberReference x1Ref, NumberReference y1Ref, NumberReference x2Ref, NumberReference y2Ref, double xMin, double xMax, double yMin, double yMax){
		double x1, y1, x2, y2;
		bool success, p1In, p2In;
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

			if(dx != 0d){
				f1 = (xMin - x2)/dx;
				f2 = (xMax - x2)/dx;
			}else{
				f1 = 1d;
				f2 = 1d;
			}
			if(dy != 0d){
				f3 = (yMin - y2)/dy;
				f4 = (yMax - y2)/dy;
			}else{
				f3 = 1d;
				f4 = 1d;
			}

			if(f1 < 0d){
				f1 = 1d;
			}
			if(f2 < 0d){
				f2 = 1d;
			}
			if(f3 < 0d){
				f3 = 1d;
			}
			if(f4 < 0d){
				f4 = 1d;
			}

			f = Min(f1, Min(f2, Min(f3, f4)));

			x1 = x2 + f*dx;
			y1 = y2 + f*dy;

			success = true;
		}else if(p1In && !p2In){
			dx = x2 - x1;
			dy = y2 - y1;

			if(dx != 0d){
				f1 = (xMin - x1)/dx;
				f2 = (xMax - x1)/dx;
			}else{
				f1 = 1d;
				f2 = 1d;
			}
			if(dy != 0d){
				f3 = (yMin - y1)/dy;
				f4 = (yMax - y1)/dy;
			}else{
				f3 = 1d;
				f4 = 1d;
			}

			if(f1 < 0d){
				f1 = 1d;
			}
			if(f2 < 0d){
				f2 = 1d;
			}
			if(f3 < 0d){
				f3 = 1d;
			}
			if(f4 < 0d){
				f4 = 1d;
			}

			f = Min(f1, Min(f2, Min(f3, f4)));

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


	public static double IncrementFromCoordinates(double x1, double y1, double x2, double y2){
		return (x2 - x1)/(y2 - y1);
	}


	public static double InterceptFromCoordinates(double x1, double y1, double x2, double y2){
		double a, b;

		a = IncrementFromCoordinates(x1, y1, x2, y2);
		b = y1 - a*x1;

		return b;
	}


	public static RGBA [] Get8HighContrastColors(){
		RGBA [] colors;
		colors = new RGBA [8];
		colors[0] = CreateRGBColor(3d/256d, 146d/256d, 206d/256d);
		colors[1] = CreateRGBColor(253d/256d, 83d/256d, 8d/256d);
		colors[2] = CreateRGBColor(102d/256d, 176d/256d, 50d/256d);
		colors[3] = CreateRGBColor(208d/256d, 234d/256d, 43d/256d);
		colors[4] = CreateRGBColor(167d/256d, 25d/256d, 75d/256d);
		colors[5] = CreateRGBColor(254d/256d, 254d/256d, 51d/256d);
		colors[6] = CreateRGBColor(134d/256d, 1d/256d, 175d/256d);
		colors[7] = CreateRGBColor(251d/256d, 153d/256d, 2d/256d);
		return colors;
	}


	public static void DrawFilledRectangleWithBorder(RGBABitmapImage image, double x, double y, double w, double h, RGBA borderColor, RGBA fillColor){
		if(h > 0d && w > 0d){
			DrawFilledRectangle(image, x, y, w, h, fillColor);
			DrawRectangle1px(image, x, y, w, h, borderColor);
		}
	}


	public static RGBABitmapImageReference CreateRGBABitmapImageReference(){
		RGBABitmapImageReference reference;

		reference = new RGBABitmapImageReference();
		reference.image = new RGBABitmapImage();
		reference.image.x = new RGBABitmap [0];

		return reference;
	}


	public static bool RectanglesOverlap(Rectangle r1, Rectangle r2){
		bool overlap;

		overlap = false;

		overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2);
		overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y1 >= r1.y1 && r2.y1 <= r1.y2);
		overlap = overlap || (r2.x1 >= r1.x1 && r2.x1 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2);
		overlap = overlap || (r2.x2 >= r1.x1 && r2.x2 <= r1.x2 && r2.y2 >= r1.y1 && r2.y2 <= r1.y2);

		return overlap;
	}


	public static Rectangle CreateRectangle(double x1, double y1, double x2, double y2){
		Rectangle r;
		r = new Rectangle();
		r.x1 = x1;
		r.y1 = y1;
		r.x2 = x2;
		r.y2 = y2;
		return r;
	}


	public static void CopyRectangleValues(Rectangle rd, Rectangle rs){
		rd.x1 = rs.x1;
		rd.y1 = rs.y1;
		rd.x2 = rs.x2;
		rd.y2 = rs.y2;
	}


	public static void DrawXLabelsForPriority(double p, double xMin, double oy, double xMax, double xPixelMin, double xPixelMax, NumberReference nextRectangle, RGBA gridLabelColor, RGBABitmapImage canvas, double [] xGridPositions, StringArrayReference xLabels, NumberArrayReference xLabelPriorities, Rectangle [] occupied, bool textOnBottom){
		bool overlap, currentOverlaps;
		double i, j, x, px, padding;
		char [] text;
		Rectangle r;

		r = new Rectangle();
		padding = 10d;

		overlap = false;
		for(i = 0d; i < xLabels.stringArray.Length; i = i + 1d){
			if(xLabelPriorities.numberArray[(int)(i)] == p){

				x = xGridPositions[(int)(i)];
				px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
				text = xLabels.stringArray[(int)(i)].stringx;

				r.x1 = Floor(px - GetTextWidth(text)/2d);
				if(textOnBottom){
					r.y1 = Floor(oy + 5d);
				}else{
					r.y1 = Floor(oy - 20d);
				}
				r.x2 = r.x1 + GetTextWidth(text);
				r.y2 = r.y1 + GetTextHeight(text);

				/* Add padding*/
				r.x1 = r.x1 - padding;
				r.y1 = r.y1 - padding;
				r.x2 = r.x2 + padding;
				r.y2 = r.y2 + padding;

				currentOverlaps = false;

				for(j = 0d; j < nextRectangle.numberValue; j = j + 1d){
					currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[(int)(j)]);
				}

				if(!currentOverlaps && p == 1d){
					DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor);

					CopyRectangleValues(occupied[(int)(nextRectangle.numberValue)], r);
					nextRectangle.numberValue = nextRectangle.numberValue + 1d;
				}

				overlap = overlap || currentOverlaps;
			}
		}
		if(!overlap && p != 1d){
			for(i = 0d; i < xGridPositions.Length; i = i + 1d){
				x = xGridPositions[(int)(i)];
				px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);

				if(xLabelPriorities.numberArray[(int)(i)] == p){
					text = xLabels.stringArray[(int)(i)].stringx;

					r.x1 = Floor(px - GetTextWidth(text)/2d);
					if(textOnBottom){
						r.y1 = Floor(oy + 5d);
					}else{
						r.y1 = Floor(oy - 20d);
					}
					r.x2 = r.x1 + GetTextWidth(text);
					r.y2 = r.y1 + GetTextHeight(text);

					DrawText(canvas, r.x1, r.y1, text, gridLabelColor);

					CopyRectangleValues(occupied[(int)(nextRectangle.numberValue)], r);
					nextRectangle.numberValue = nextRectangle.numberValue + 1d;
				}
			}
		}
	}


	public static void DrawYLabelsForPriority(double p, double yMin, double ox, double yMax, double yPixelMin, double yPixelMax, NumberReference nextRectangle, RGBA gridLabelColor, RGBABitmapImage canvas, double [] yGridPositions, StringArrayReference yLabels, NumberArrayReference yLabelPriorities, Rectangle [] occupied, bool textOnLeft){
		bool overlap, currentOverlaps;
		double i, j, y, py, padding;
		char [] text;
		Rectangle r;

		r = new Rectangle();
		padding = 10d;

		overlap = false;
		for(i = 0d; i < yLabels.stringArray.Length; i = i + 1d){
			if(yLabelPriorities.numberArray[(int)(i)] == p){

				y = yGridPositions[(int)(i)];
				py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
				text = yLabels.stringArray[(int)(i)].stringx;

				if(textOnLeft){
					r.x1 = Floor(ox - GetTextWidth(text) - 10d);
				}else{
					r.x1 = Floor(ox + 10d);
				}
				r.y1 = Floor(py - 6d);
				r.x2 = r.x1 + GetTextWidth(text);
				r.y2 = r.y1 + GetTextHeight(text);

				/* Add padding*/
				r.x1 = r.x1 - padding;
				r.y1 = r.y1 - padding;
				r.x2 = r.x2 + padding;
				r.y2 = r.y2 + padding;

				currentOverlaps = false;

				for(j = 0d; j < nextRectangle.numberValue; j = j + 1d){
					currentOverlaps = currentOverlaps || RectanglesOverlap(r, occupied[(int)(j)]);
				}

				/* Draw labels with priority 1 if they do not overlap anything else.*/
				if(!currentOverlaps && p == 1d){
					DrawText(canvas, r.x1 + padding, r.y1 + padding, text, gridLabelColor);

					CopyRectangleValues(occupied[(int)(nextRectangle.numberValue)], r);
					nextRectangle.numberValue = nextRectangle.numberValue + 1d;
				}

				overlap = overlap || currentOverlaps;
			}
		}
		if(!overlap && p != 1d){
			for(i = 0d; i < yGridPositions.Length; i = i + 1d){
				y = yGridPositions[(int)(i)];
				py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);

				if(yLabelPriorities.numberArray[(int)(i)] == p){
					text = yLabels.stringArray[(int)(i)].stringx;

					if(textOnLeft){
						r.x1 = Floor(ox - GetTextWidth(text) - 10d);
					}else{
						r.x1 = Floor(ox + 10d);
					}
					r.y1 = Floor(py - 6d);
					r.x2 = r.x1 + GetTextWidth(text);
					r.y2 = r.y1 + GetTextHeight(text);

					DrawText(canvas, r.x1, r.y1, text, gridLabelColor);

					CopyRectangleValues(occupied[(int)(nextRectangle.numberValue)], r);
					nextRectangle.numberValue = nextRectangle.numberValue + 1d;
				}
			}
		}
	}


	public static double [] ComputeGridLinePositions(double cMin, double cMax, StringArrayReference labels, NumberArrayReference priorities){
		double [] positions;
		double cLength, p, pMin, pMax, pInterval, pNum, i, num, rem, priority, mode;

		cLength = cMax - cMin;

		p = Floor(Log10(cLength));
		pInterval = Pow(10d, p);
		/* gives 10-1 lines for 100-10 diff*/
		pMin = Ceiling(cMin/pInterval)*pInterval;
		pMax = Floor(cMax/pInterval)*pInterval;
		pNum = Roundx((pMax - pMin)/pInterval + 1d);

		mode = 1d;

		if(pNum <= 3d){
			p = Floor(Log10(cLength) - 1d);
			/* gives 100-10 lines for 100-10 diff*/
			pInterval = Pow(10d, p);
			pMin = Ceiling(cMin/pInterval)*pInterval;
			pMax = Floor(cMax/pInterval)*pInterval;
			pNum = Roundx((pMax - pMin)/pInterval + 1d);

			mode = 4d;
		}else if(pNum <= 6d){
			p = Floor(Log10(cLength));
			pInterval = Pow(10d, p)/4d;
			/* gives 40-5 lines for 100-10 diff*/
			pMin = Ceiling(cMin/pInterval)*pInterval;
			pMax = Floor(cMax/pInterval)*pInterval;
			pNum = Roundx((pMax - pMin)/pInterval + 1d);

			mode = 3d;
		}else if(pNum <= 10d){
			p = Floor(Log10(cLength));
			pInterval = Pow(10d, p)/2d;
			/* gives 20-3 lines for 100-10 diff*/
			pMin = Ceiling(cMin/pInterval)*pInterval;
			pMax = Floor(cMax/pInterval)*pInterval;
			pNum = Roundx((pMax - pMin)/pInterval + 1d);

			mode = 2d;
		}

		positions = new double [(int)(pNum)];
		labels.stringArray = new StringReference [(int)(pNum)];
		priorities.numberArray = new double [(int)(pNum)];

		for(i = 0d; i < pNum; i = i + 1d){
			num = pMin + pInterval*i;
			positions[(int)(i)] = num;

			/* Always print priority 1 labels. Only draw priority 2 if they can all be drawn. Then, only draw priority 3 if they can all be drawn.*/
			priority = 1d;

			/* Prioritize x.25, x.5 and x.75 lower.*/
			if(mode == 2d || mode == 3d){
				rem = Abs((double)Round(num/Pow(10d, p - 2d)))%100d;

				priority = 1d;
				if(rem == 50d){
					priority = 2d;
				}else if(rem == 25d || rem == 75d){
					priority = 3d;
				}
			}

			/* Prioritize x.1-x.4 and x.6-x.9 lower*/
			if(mode == 4d){
				rem = Abs(Roundx(num/Pow(10d, p)))%10d;

				priority = 1d;
				if(rem == 1d || rem == 2d || rem == 3d || rem == 4d || rem == 6d || rem == 7d || rem == 8d || rem == 9d){
					priority = 2d;
				}
			}

			/* 0 has lowest priority.*/
			if(EpsilonCompare(num, 0d, Pow(10d, p - 5d))){
				priority = 3d;
			}

			priorities.numberArray[(int)(i)] = priority;

			/* The label itself.*/
			labels.stringArray[(int)(i)] = new StringReference();
			if(p < 0d){
				if(mode == 2d || mode == 3d){
					num = RoundToDigits(num, -(p - 1d));
				}else{
					num = RoundToDigits(num, -p);
				}
			}
			labels.stringArray[(int)(i)].stringx = CreateStringDecimalFromNumber(num);
		}

		return positions;
	}


	public static double MapYCoordinate(double y, double yMin, double yMax, double yPixelMin, double yPixelMax){
		double yLength, yPixelLength;

		yLength = yMax - yMin;
		yPixelLength = yPixelMax - yPixelMin;

		y = y - yMin;
		y = y*yPixelLength/yLength;
		y = yPixelLength - y;
		y = y + yPixelMin;
		return y;
	}


	public static double MapXCoordinate(double x, double xMin, double xMax, double xPixelMin, double xPixelMax){
		double xLength, xPixelLength;

		xLength = xMax - xMin;
		xPixelLength = xPixelMax - xPixelMin;

		x = x - xMin;
		x = x*xPixelLength/xLength;
		x = x + xPixelMin;
		return x;
	}


	public static double MapXCoordinateAutoSettings(double x, RGBABitmapImage image, double [] xs){
		return MapXCoordinate(x, GetMinimum(xs), GetMaximum(xs), GetDefaultPaddingPercentage()*ImageWidth(image), (1d - GetDefaultPaddingPercentage())*ImageWidth(image));
	}


	public static double MapYCoordinateAutoSettings(double y, RGBABitmapImage image, double [] ys){
		return MapYCoordinate(y, GetMinimum(ys), GetMaximum(ys), GetDefaultPaddingPercentage()*ImageHeight(image), (1d - GetDefaultPaddingPercentage())*ImageHeight(image));
	}


	public static double MapXCoordinateBasedOnSettings(double x, ScatterPlotSettings settings){
		double xMin, xMax, xPadding, xPixelMin, xPixelMax;
		Rectangle boundaries;

		boundaries = new Rectangle();
		ComputeBoundariesBasedOnSettings(settings, boundaries);
		xMin = boundaries.x1;
		xMax = boundaries.x2;

		if(settings.autoPadding){
			xPadding = Floor(GetDefaultPaddingPercentage()*settings.width);
		}else{
			xPadding = settings.xPadding;
		}

		xPixelMin = xPadding;
		xPixelMax = settings.width - xPadding;

		return MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
	}


	public static double MapYCoordinateBasedOnSettings(double y, ScatterPlotSettings settings){
		double yMin, yMax, yPadding, yPixelMin, yPixelMax;
		Rectangle boundaries;

		boundaries = new Rectangle();
		ComputeBoundariesBasedOnSettings(settings, boundaries);
		yMin = boundaries.y1;
		yMax = boundaries.y2;

		if(settings.autoPadding){
			yPadding = Floor(GetDefaultPaddingPercentage()*settings.height);
		}else{
			yPadding = settings.yPadding;
		}

		yPixelMin = yPadding;
		yPixelMax = settings.height - yPadding;

		return MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
	}


	public static double GetDefaultPaddingPercentage(){
		return 0.10;
	}


	public static void DrawText(RGBABitmapImage canvas, double x, double y, char [] text, RGBA color){
		double i, charWidth, spacing;

		charWidth = 8d;
		spacing = 2d;

		for(i = 0d; i < text.Length; i = i + 1d){
			DrawAsciiCharacter(canvas, x + i*(charWidth + spacing), y, text[(int)(i)], color);
		}
	}


	public static void DrawTextUpwards(RGBABitmapImage canvas, double x, double y, char [] text, RGBA color){
		RGBABitmapImage buffer, rotated;

		buffer = CreateImage(GetTextWidth(text), GetTextHeight(text), GetTransparent());
		DrawText(buffer, 0d, 0d, text, color);
		rotated = RotateAntiClockwise90Degrees(buffer);
		DrawImageOnImage(canvas, rotated, x, y);
		DeleteImage(buffer);
		DeleteImage(rotated);
	}


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
		settings.title = "".ToCharArray();
		settings.xLabel = "".ToCharArray();
		settings.yLabel = "".ToCharArray();
		settings.scatterPlotSeries = new ScatterPlotSeries [0];
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
		series.pointType = "pixels".ToCharArray();
		series.lineType = "solid".ToCharArray();
		series.lineThickness = 1d;
		series.xs = new double [0];
		series.ys = new double [0];
		series.color = GetBlack();

		return series;
	}


	public static void DrawScatterPlot(RGBABitmapImageReference canvasReference, double width, double height, double [] xs, double [] ys){
		ScatterPlotSettings settings;

		settings = GetDefaultScatterPlotSettings();

		settings.width = width;
		settings.height = height;
		settings.scatterPlotSeries = new ScatterPlotSeries [1];
		settings.scatterPlotSeries[0] = GetDefaultScatterPlotSeriesSettings();
		delete(settings.scatterPlotSeries[0].xs);
		settings.scatterPlotSeries[0].xs = xs;
		delete(settings.scatterPlotSeries[0].ys);
		settings.scatterPlotSeries[0].ys = ys;

		DrawScatterPlotFromSettings(canvasReference, settings);
	}


	public static bool DrawScatterPlotFromSettings(RGBABitmapImageReference canvasReference, ScatterPlotSettings settings){
		double xMin, xMax, yMin, yMax, xLength, yLength, i, x, y, xPrev, yPrev, px, py, pxPrev, pyPrev, originX, originY, p, l, plot;
		Rectangle boundaries;
		double xPadding, yPadding, originXPixels, originYPixels;
		double xPixelMin, yPixelMin, xPixelMax, yPixelMax, xLengthPixels, yLengthPixels, axisLabelPadding;
		NumberReference nextRectangle, x1Ref, y1Ref, x2Ref, y2Ref, patternOffset;
		bool prevSet, success;
		RGBA gridLabelColor;
		RGBABitmapImage canvas;
		double [] xs, ys;
		bool linearInterpolation;
		ScatterPlotSeries sp;
		double [] xGridPositions, yGridPositions;
		StringArrayReference xLabels, yLabels;
		NumberArrayReference xLabelPriorities, yLabelPriorities;
		Rectangle [] occupied;
		bool [] linePattern;
		bool originXInside, originYInside, textOnLeft, textOnBottom;
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

			if(settings.autoPadding){
				xPadding = Floor(GetDefaultPaddingPercentage()*settings.width);
				yPadding = Floor(GetDefaultPaddingPercentage()*settings.height);
			}else{
				xPadding = settings.xPadding;
				yPadding = settings.yPadding;
			}

			/* Draw title*/
			DrawText(canvas, Floor(settings.width/2d - GetTextWidth(settings.title)/2d), Floor(yPadding/3d), settings.title, GetBlack());

			/* Draw grid*/
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

			if(settings.showGrid){
				/* X-grid*/
				for(i = 0d; i < xGridPositions.Length; i = i + 1d){
					x = xGridPositions[(int)(i)];
					px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
					DrawLine1px(canvas, px, yPixelMin, px, yPixelMax, settings.gridColor);
				}

				/* Y-grid*/
				for(i = 0d; i < yGridPositions.Length; i = i + 1d){
					y = yGridPositions[(int)(i)];
					py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
					DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
				}
			}

			/* Compute origin information.*/
			originYInside = yMin < 0d && yMax > 0d;
			originY = 0d;
			if(settings.xAxisAuto){
				if(originYInside){
					originY = 0d;
				}else{
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
			if(settings.yAxisAuto){
				if(originXInside){
					originX = 0d;
				}else{
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

			/* Labels*/
			occupied = new Rectangle [(int)(xLabels.stringArray.Length + yLabels.stringArray.Length)];
			for(i = 0d; i < occupied.Length; i = i + 1d){
				occupied[(int)(i)] = CreateRectangle(0d, 0d, 0d, 0d);
			}
			nextRectangle = CreateNumberReference(0d);

			/* x labels*/
			for(i = 1d; i <= 5d; i = i + 1d){
				textOnBottom = true;
				if(!settings.xAxisAuto && settings.xAxisTop){
					textOnBottom = false;
				}
				DrawXLabelsForPriority(i, xMin, originYPixels, xMax, xPixelMin, xPixelMax, nextRectangle, gridLabelColor, canvas, xGridPositions, xLabels, xLabelPriorities, occupied, textOnBottom);
			}

			/* y labels*/
			for(i = 1d; i <= 5d; i = i + 1d){
				textOnLeft = true;
				if(!settings.yAxisAuto && settings.yAxisRight){
					textOnLeft = false;
				}
				DrawYLabelsForPriority(i, yMin, originXPixels, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, textOnLeft);
			}

			/* Draw origin line axis titles.*/
			axisLabelPadding = 20d;

			/* x origin line*/
			if(originYInside){
				DrawLine1px(canvas, Roundx(xPixelMin), Roundx(originYPixels), Roundx(xPixelMax), Roundx(originYPixels), GetBlack());
			}

			/* y origin line*/
			if(originXInside){
				DrawLine1px(canvas, Roundx(originXPixels), Roundx(yPixelMin), Roundx(originXPixels), Roundx(yPixelMax), GetBlack());
			}

			/* Draw origin axis titles.*/
			DrawTextUpwards(canvas, 10d, Floor(originTextYPixels - GetTextWidth(settings.yLabel)/2d), settings.yLabel, GetBlack());
			DrawText(canvas, Floor(originTextXPixels - GetTextWidth(settings.xLabel)/2d), yPixelMax + axisLabelPadding, settings.xLabel, GetBlack());

			/* X-grid-markers*/
			for(i = 0d; i < xGridPositions.Length; i = i + 1d){
				x = xGridPositions[(int)(i)];
				px = MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax);
				p = xLabelPriorities.numberArray[(int)(i)];
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

			/* Y-grid-markers*/
			for(i = 0d; i < yGridPositions.Length; i = i + 1d){
				y = yGridPositions[(int)(i)];
				py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
				p = yLabelPriorities.numberArray[(int)(i)];
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

			/* Draw points*/
			for(plot = 0d; plot < settings.scatterPlotSeries.Length; plot = plot + 1d){
				sp = settings.scatterPlotSeries[(int)(plot)];

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
					for(i = 0d; i < xs.Length; i = i + 1d){
						x = xs[(int)(i)];
						y = ys[(int)(i)];

						if(prevSet){
							x1Ref.numberValue = xPrev;
							y1Ref.numberValue = yPrev;
							x2Ref.numberValue = x;
							y2Ref.numberValue = y;

							success = CropLineWithinBoundary(x1Ref, y1Ref, x2Ref, y2Ref, xMin, xMax, yMin, yMax);

							if(success){
								pxPrev = Floor(MapXCoordinate(x1Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax));
								pyPrev = Floor(MapYCoordinate(y1Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax));
								px = Floor(MapXCoordinate(x2Ref.numberValue, xMin, xMax, xPixelMin, xPixelMax));
								py = Floor(MapYCoordinate(y2Ref.numberValue, yMin, yMax, yPixelMin, yPixelMax));

								if(aStringsEqual(sp.lineType, "solid".ToCharArray()) && sp.lineThickness == 1d){
									DrawLine1px(canvas, pxPrev, pyPrev, px, py, sp.color);
								}else if(aStringsEqual(sp.lineType, "solid".ToCharArray())){
									DrawLine(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, sp.color);
								}else if(aStringsEqual(sp.lineType, "dashed".ToCharArray())){
									linePattern = GetLinePattern1();
									DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
								}else if(aStringsEqual(sp.lineType, "dotted".ToCharArray())){
									linePattern = GetLinePattern2();
									DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
								}else if(aStringsEqual(sp.lineType, "dotdash".ToCharArray())){
									linePattern = GetLinePattern3();
									DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
								}else if(aStringsEqual(sp.lineType, "longdash".ToCharArray())){
									linePattern = GetLinePattern4();
									DrawLineBresenhamsAlgorithmThickPatterned(canvas, pxPrev, pyPrev, px, py, sp.lineThickness, linePattern, patternOffset, sp.color);
								}else if(aStringsEqual(sp.lineType, "twodash".ToCharArray())){
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
					for(i = 0d; i < xs.Length; i = i + 1d){
						x = xs[(int)(i)];
						y = ys[(int)(i)];

						if(x > xMin && x < xMax && y > yMin && y < yMax){

							x = Floor(MapXCoordinate(x, xMin, xMax, xPixelMin, xPixelMax));
							y = Floor(MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax));

							if(aStringsEqual(sp.pointType, "crosses".ToCharArray())){
								DrawPixel(canvas, x, y, sp.color);
								DrawPixel(canvas, x + 1d, y, sp.color);
								DrawPixel(canvas, x + 2d, y, sp.color);
								DrawPixel(canvas, x - 1d, y, sp.color);
								DrawPixel(canvas, x - 2d, y, sp.color);
								DrawPixel(canvas, x, y + 1d, sp.color);
								DrawPixel(canvas, x, y + 2d, sp.color);
								DrawPixel(canvas, x, y - 1d, sp.color);
								DrawPixel(canvas, x, y - 2d, sp.color);
							}else if(aStringsEqual(sp.pointType, "circles".ToCharArray())){
								DrawCircle(canvas, x, y, 3d, sp.color);
							}else if(aStringsEqual(sp.pointType, "dots".ToCharArray())){
								DrawFilledCircle(canvas, x, y, 3d, sp.color);
							}else if(aStringsEqual(sp.pointType, "triangles".ToCharArray())){
								DrawTriangle(canvas, x, y, 3d, sp.color);
							}else if(aStringsEqual(sp.pointType, "filled triangles".ToCharArray())){
								DrawFilledTriangle(canvas, x, y, 3d, sp.color);
							}else if(aStringsEqual(sp.pointType, "pixels".ToCharArray())){
								DrawPixel(canvas, x, y, sp.color);
							}
						}
					}
				}
			}

			DeleteImage(canvasReference.image);
			canvasReference.image = canvas;
		}

		return success;
	}


	public static void ComputeBoundariesBasedOnSettings(ScatterPlotSettings settings, Rectangle boundaries){
		ScatterPlotSeries sp;
		double plot, xMin, xMax, yMin, yMax;

		if(settings.scatterPlotSeries.Length >= 1d){
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

		if(!settings.autoBoundaries){
			xMin = settings.xMin;
			xMax = settings.xMax;
			yMin = settings.yMin;
			yMax = settings.yMax;
		}else{
			for(plot = 1d; plot < settings.scatterPlotSeries.Length; plot = plot + 1d){
				sp = settings.scatterPlotSeries[(int)(plot)];

				xMin = Min(xMin, GetMinimum(sp.xs));
				xMax = Max(xMax, GetMaximum(sp.xs));
				yMin = Min(yMin, GetMinimum(sp.ys));
				yMax = Max(yMax, GetMaximum(sp.ys));
			}
		}

		boundaries.x1 = xMin;
		boundaries.y1 = yMin;
		boundaries.x2 = xMax;
		boundaries.y2 = yMax;
	}


	public static bool ScatterPlotFromSettingsValid(ScatterPlotSettings settings){
		bool success, found;
		ScatterPlotSeries series;
		double i;

		success = true;

		/* Check axis placement.*/
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

		/* Check series lengths.*/
		for(i = 0d; i < settings.scatterPlotSeries.Length; i = i + 1d){
			series = settings.scatterPlotSeries[(int)(i)];
			if(series.xs.Length != series.ys.Length){
				success = false;
			}
			if(series.xs.Length == 0d){
				success = false;
			}
			if(series.linearInterpolation && series.xs.Length == 1d){
				success = false;
			}
		}

		/* Check bounds.*/
		if(!settings.autoBoundaries){
			if(settings.xMin >= settings.xMax){
				success = false;
			}
			if(settings.yMin >= settings.yMax){
				success = false;
			}
		}

		/* Check padding.*/
		if(!settings.autoPadding){
			if(2d*settings.xPadding >= settings.width){
				success = false;
			}
			if(2d*settings.yPadding >= settings.height){
				success = false;
			}
		}

		/* Check width and height.*/
		if(settings.width < 0d){
			success = false;
		}
		if(settings.height < 0d){
			success = false;
		}

		/* Check point types.*/
		for(i = 0d; i < settings.scatterPlotSeries.Length; i = i + 1d){
			series = settings.scatterPlotSeries[(int)(i)];

			if(series.lineThickness < 0d){
				success = false;
			}

			if(!series.linearInterpolation){
				/* Point type.*/
				found = false;
				if(aStringsEqual(series.pointType, "crosses".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.pointType, "circles".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.pointType, "dots".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.pointType, "triangles".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.pointType, "filled triangles".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.pointType, "pixels".ToCharArray())){
					found = true;
				}
				if(!found){
					success = false;
				}
			}else{
				/* Line type.*/
				found = false;
				if(aStringsEqual(series.lineType, "solid".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.lineType, "dashed".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.lineType, "dotted".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.lineType, "dotdash".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.lineType, "longdash".ToCharArray())){
					found = true;
				}else if(aStringsEqual(series.lineType, "twodash".ToCharArray())){
					found = true;
				}

				if(!found){
					success = false;
				}
			}
		}

		return success;
	}


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
		settings.title = "".ToCharArray();
		settings.yLabel = "".ToCharArray();
		settings.barPlotSeries = new BarPlotSeries [0];
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

		series.ys = new double [0];
		series.color = GetBlack();

		return series;
	}


	public static RGBABitmapImage DrawBarPlot(double width, double height, double [] ys){
		BarPlotSettings settings;
		RGBABitmapImageReference canvasReference;

		settings = GetDefaultBarPlotSettings();

		settings.barPlotSeries = new BarPlotSeries [1];
		settings.barPlotSeries[0] = GetDefaultBarPlotSeriesSettings();
		delete(settings.barPlotSeries[0].ys);
		settings.barPlotSeries[0].ys = ys;
		canvasReference = new RGBABitmapImageReference();
		settings.width = width;
		settings.height = height;

		DrawBarPlotFromSettings(canvasReference, settings);

		return canvasReference.image;
	}


	public static bool DrawBarPlotFromSettings(RGBABitmapImageReference canvasReference, BarPlotSettings settings){
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
		bool success;
		RGBABitmapImage canvas;

		success = BarPlotSettingsIsValid(settings);

		if(success){

			canvas = CreateImage(settings.width, settings.height, GetWhite());

			ss = settings.barPlotSeries.Length;
			gridLabelColor = GetGray(0.5);

			/* padding*/
			if(settings.autoPadding){
				xPadding = Floor(GetDefaultPaddingPercentage()*ImageWidth(canvas));
				yPadding = Floor(GetDefaultPaddingPercentage()*ImageHeight(canvas));
			}else{
				xPadding = settings.xPadding;
				yPadding = settings.yPadding;
			}

			/* Draw title*/
			DrawText(canvas, Floor(ImageWidth(canvas)/2d - GetTextWidth(settings.title)/2d), Floor(yPadding/3d), settings.title, GetBlack());
			DrawTextUpwards(canvas, 10d, Floor(ImageHeight(canvas)/2d - GetTextWidth(settings.yLabel)/2d), settings.yLabel, GetBlack());

			/* min and max*/
			if(settings.autoBoundaries){
				if(ss >= 1d){
					yMax = GetMaximum(settings.barPlotSeries[0].ys);
					yMin = Min(0d, GetMinimum(settings.barPlotSeries[0].ys));

					for(s = 0d; s < ss; s = s + 1d){
						yMax = Max(yMax, GetMaximum(settings.barPlotSeries[(int)(s)].ys));
						yMin = Min(yMin, GetMinimum(settings.barPlotSeries[(int)(s)].ys));
					}
				}else{
					yMax = 10d;
					yMin = 0d;
				}
			}else{
				yMin = settings.yMin;
				yMax = settings.yMax;
			}
			yLength = yMax - yMin;

			/* boundaries*/
			xPixelMin = xPadding;
			yPixelMin = yPadding;
			xPixelMax = ImageWidth(canvas) - xPadding;
			yPixelMax = ImageHeight(canvas) - yPadding;
			xLengthPixels = xPixelMax - xPixelMin;
			yLengthPixels = yPixelMax - yPixelMin;

			/* Draw boundary.*/
			DrawRectangle1px(canvas, xPixelMin, yPixelMin, xLengthPixels, yLengthPixels, settings.gridColor);

			/* Draw grid lines.*/
			yLabels = new StringArrayReference();
			yLabelPriorities = new NumberArrayReference();
			yGridPositions = ComputeGridLinePositions(yMin, yMax, yLabels, yLabelPriorities);

			if(settings.showGrid){
				/* Y-grid*/
				for(i = 0d; i < yGridPositions.Length; i = i + 1d){
					y = yGridPositions[(int)(i)];
					py = MapYCoordinate(y, yMin, yMax, yPixelMin, yPixelMax);
					DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
				}
			}

			/* Draw origin.*/
			if(yMin < 0d && yMax > 0d){
				py = MapYCoordinate(0d, yMin, yMax, yPixelMin, yPixelMax);
				DrawLine1px(canvas, xPixelMin, py, xPixelMax, py, settings.gridColor);
			}

			/* Labels*/
			occupied = new Rectangle [(int)(yLabels.stringArray.Length)];
			for(i = 0d; i < occupied.Length; i = i + 1d){
				occupied[(int)(i)] = CreateRectangle(0d, 0d, 0d, 0d);
			}
			nextRectangle = CreateNumberReference(0d);

			for(i = 1d; i <= 5d; i = i + 1d){
				DrawYLabelsForPriority(i, yMin, xPixelMin, yMax, yPixelMin, yPixelMax, nextRectangle, gridLabelColor, canvas, yGridPositions, yLabels, yLabelPriorities, occupied, true);
			}

			/* Draw bars.*/
			if(settings.autoColor){
				if(!settings.grayscaleAutoColor){
					colors = Get8HighContrastColors();
				}else{
					colors = new RGBA [(int)(ss)];
					if(ss > 1d){
						for(i = 0d; i < ss; i = i + 1d){
							colors[(int)(i)] = GetGray(0.7 - (i/ss)*0.7);
						}
					}else{
						colors[0] = GetGray(0.5);
					}
				}
			}else{
				colors = new RGBA [0];
			}

			/* distances*/
			bs = settings.barPlotSeries[0].ys.Length;

			if(settings.autoSpacing){
				groupSeparation = ImageWidth(canvas)*0.05;
				barSeparation = ImageWidth(canvas)*0.005;
			}else{
				groupSeparation = settings.groupSeparation;
				barSeparation = settings.barSeparation;
			}

			barWidth = (xLengthPixels - groupSeparation*(bs - 1d) - barSeparation*(bs*(ss - 1d)))/(bs*ss);

			/* Draw bars.*/
			b = 0d;
			for(n = 0d; n < bs; n = n + 1d){
				for(s = 0d; s < ss; s = s + 1d){
					ys = settings.barPlotSeries[(int)(s)].ys;

					yValue = ys[(int)(n)];

					yBottom = MapYCoordinate(yValue, yMin, yMax, yPixelMin, yPixelMax);
					yTop = MapYCoordinate(0d, yMin, yMax, yPixelMin, yPixelMax);

					x = xPixelMin + n*(groupSeparation + ss*barWidth) + s*(barWidth) + b*barSeparation;
					w = barWidth;

					if(yValue >= 0d){
						y = yBottom;
						h = yTop - y;
					}else{
						y = yTop;
						h = yBottom - yTop;
					}

					/* Cut at boundaries.*/
					if(y < yPixelMin && y + h > yPixelMax){
						y = yPixelMin;
						h = yPixelMax - yPixelMin;
					}else if(y < yPixelMin){
						y = yPixelMin;
						if(yValue >= 0d){
							h = yTop - y;
						}else{
							h = yBottom - y;
						}
					}else if(y + h > yPixelMax){
						h = yPixelMax - y;
					}

					/* Get color*/
					if(settings.autoColor){
						barColor = colors[(int)(s)];
					}else{
						barColor = settings.barPlotSeries[(int)(s)].color;
					}

					/* Draw*/
					if(settings.barBorder){
						DrawFilledRectangleWithBorder(canvas, Roundx(x), Roundx(y), Roundx(w), Roundx(h), GetBlack(), barColor);
					}else{
						DrawFilledRectangle(canvas, Roundx(x), Roundx(y), Roundx(w), Roundx(h), barColor);
					}

					b = b + 1d;
				}
				b = b - 1d;
			}

			/* x-labels*/
			for(n = 0d; n < bs; n = n + 1d){
				if(settings.autoLabels){
					label = CreateStringDecimalFromNumber(n + 1d);
				}else{
					label = settings.xLabels[(int)(n)].stringx;
				}

				textwidth = GetTextWidth(label);

				x = xPixelMin + (n + 0.5)*(ss*barWidth + (ss - 1d)*barSeparation) + n*groupSeparation - textwidth/2d;

				DrawText(canvas, Floor(x), ImageHeight(canvas) - yPadding + 20d, label, gridLabelColor);

				b = b + 1d;
			}

			canvasReference.image = canvas;
		}

		return success;
	}


	public static bool BarPlotSettingsIsValid(BarPlotSettings settings){
		bool success, lengthSet;
		BarPlotSeries series;
		double i, width, height, length;

		success = true;

		/* Check series lengths.*/
		lengthSet = false;
		length = 0d;
		for(i = 0d; i < settings.barPlotSeries.Length; i = i + 1d){
			series = settings.barPlotSeries[(int)(i)];

			if(!lengthSet){
				length = series.ys.Length;
				lengthSet = true;
			}else if(length != series.ys.Length){
				success = false;
			}
		}

		/* Check bounds.*/
		if(!settings.autoBoundaries){
			if(settings.yMin >= settings.yMax){
				success = false;
			}
		}

		/* Check padding.*/
		if(!settings.autoPadding){
			if(2d*settings.xPadding >= settings.width){
				success = false;
			}
			if(2d*settings.yPadding >= settings.height){
				success = false;
			}
		}

		/* Check width and height.*/
		if(settings.width < 0d){
			success = false;
		}
		if(settings.height < 0d){
			success = false;
		}

		/* Check spacing*/
		if(!settings.autoSpacing){
			if(settings.groupSeparation < 0d){
				success = false;
			}
			if(settings.barSeparation < 0d){
				success = false;
			}
		}

		return success;
	}


	public static double GetMinimum(double [] data){
		double i, minimum;

		minimum = data[0];
		for(i = 0d; i < data.Length; i = i + 1d){
			minimum = Min(minimum, data[(int)(i)]);
		}

		return minimum;
	}


	public static double GetMaximum(double [] data){
		double i, maximum;

		maximum = data[0];
		for(i = 0d; i < data.Length; i = i + 1d){
			maximum = Max(maximum, data[(int)(i)]);
		}

		return maximum;
	}


	public static double RoundToDigits(double element, double digitsAfterPoint){
		return Roundx(element*Pow(10d, digitsAfterPoint))/Pow(10d, digitsAfterPoint);
	}


	public static double test(){
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
		AssertEquals(gridlines.Length, 11d, failures);

		z = 9d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 19d, failures);

		z = 8d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 17d, failures);

		z = 7d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 15d, failures);

		z = 6d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 13d, failures);

		z = 5d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 21d, failures);

		z = 4d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 17d, failures);

		z = 3d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 31d, failures);

		z = 2d;
		gridlines = ComputeGridLinePositions(-z/2d, z/2d, labels, labelPriorities);
		AssertEquals(gridlines.Length, 21d, failures);

		xs = new double [5];
		xs[0] = -2d;
		xs[1] = -1d;
		xs[2] = 0d;
		xs[3] = 1d;
		xs[4] = 2d;
		ys = new double [5];
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


	public static void TestMapping(NumberReference failures){
		ScatterPlotSeries series;
		ScatterPlotSettings settings;
		RGBABitmapImageReference imageReference;
		double x1, y1;

		series = GetDefaultScatterPlotSeriesSettings();

		series.xs = new double [5];
		series.xs[0] = -2d;
		series.xs[1] = -1d;
		series.xs[2] = 0d;
		series.xs[3] = 1d;
		series.xs[4] = 2d;
		series.ys = new double [5];
		series.ys[0] = -2d;
		series.ys[1] = -1d;
		series.ys[2] = -2d;
		series.ys[3] = -1d;
		series.ys[4] = 2d;
		series.linearInterpolation = true;
		series.lineType = "dashed".ToCharArray();
		series.lineThickness = 2d;
		series.color = GetGray(0.3);

		settings = GetDefaultScatterPlotSettings();
		settings.width = 600d;
		settings.height = 400d;
		settings.autoBoundaries = true;
		settings.autoPadding = true;
		settings.title = "x^2 - 2".ToCharArray();
		settings.xLabel = "X axis".ToCharArray();
		settings.yLabel = "Y axis".ToCharArray();
		settings.scatterPlotSeries = new ScatterPlotSeries [1];
		settings.scatterPlotSeries[0] = series;

		imageReference = CreateRGBABitmapImageReference();
		DrawScatterPlotFromSettings(imageReference, settings);

		x1 = MapXCoordinateAutoSettings(-1d, imageReference.image, series.xs);
		y1 = MapYCoordinateAutoSettings(-1d, imageReference.image, series.ys);

		AssertEquals(x1, 180d, failures);
		AssertEquals(y1, 280d, failures);
	}


	public static void TestMapping2(NumberReference failures){
		double [] xs, ys, xs2, ys2;
		double i, x, y, w, h, xMin, xMax, yMin, yMax;
		RGBABitmapImageReference canvasReference;
		ScatterPlotSettings settings;
		double points;
		double x1, y1;

		points = 300d;
		w = 600d*2d;
		h = 300d*2d;
		xMin = 0d;
		xMax = 150d;
		yMin = 0d;
		yMax = 1d;

		xs = new double [(int)(points)];
		ys = new double [(int)(points)];
		xs2 = new double [(int)(points)];
		ys2 = new double [(int)(points)];

		for(i = 0d; i < points; i = i + 1d){
			x = xMin + (xMax - xMin)/(points - 1d)*i;
			/* points - 1d is to ensure both extremeties are included.*/
			y = x/(x + 7d);

			xs[(int)(i)] = x;
			ys[(int)(i)] = y;

			y = 1.4*x/(x + 7d)*(1d - (Atan((x/1.5 - 30d)/5d)/1.6 + 1d)/2d);

			xs2[(int)(i)] = x;
			ys2[(int)(i)] = y;
		}

		settings = GetDefaultScatterPlotSettings();

		settings.scatterPlotSeries = new ScatterPlotSeries [2];
		settings.scatterPlotSeries[0] = new ScatterPlotSeries();
		settings.scatterPlotSeries[0].xs = xs;
		settings.scatterPlotSeries[0].ys = ys;
		settings.scatterPlotSeries[0].linearInterpolation = true;
		settings.scatterPlotSeries[0].lineType = "solid".ToCharArray();
		settings.scatterPlotSeries[0].lineThickness = 3d;
		settings.scatterPlotSeries[0].color = CreateRGBColor(1d, 0d, 0d);
		settings.scatterPlotSeries[1] = new ScatterPlotSeries();
		settings.scatterPlotSeries[1].xs = xs2;
		settings.scatterPlotSeries[1].ys = ys2;
		settings.scatterPlotSeries[1].linearInterpolation = true;
		settings.scatterPlotSeries[1].lineType = "solid".ToCharArray();
		settings.scatterPlotSeries[1].lineThickness = 3d;
		settings.scatterPlotSeries[1].color = CreateRGBColor(0d, 0d, 1d);
		settings.autoBoundaries = false;
		settings.xMin = xMin;
		settings.xMax = xMax;
		settings.yMin = yMin;
		settings.yMax = yMax;
		settings.yLabel = "".ToCharArray();
		settings.xLabel = "Features".ToCharArray();
		settings.title = "".ToCharArray();
		settings.width = w;
		settings.height = h;

		canvasReference = CreateRGBABitmapImageReference();

		DrawScatterPlotFromSettings(canvasReference, settings);

		x1 = MapXCoordinateBasedOnSettings(27d, settings);
		y1 = MapYCoordinateBasedOnSettings(1d, settings);

		AssertEquals(Floor(x1), 292d, failures);
		AssertEquals(y1, 60d, failures);
	}


	public static RGBA GetBlack(){
		RGBA black;
		black = new RGBA();
		black.a = 1d;
		black.r = 0d;
		black.g = 0d;
		black.b = 0d;
		return black;
	}


	public static RGBA GetWhite(){
		RGBA white;
		white = new RGBA();
		white.a = 1d;
		white.r = 1d;
		white.g = 1d;
		white.b = 1d;
		return white;
	}


	public static RGBA GetTransparent(){
		RGBA transparent;
		transparent = new RGBA();
		transparent.a = 0d;
		transparent.r = 0d;
		transparent.g = 0d;
		transparent.b = 0d;
		return transparent;
	}


	public static RGBA GetGray(double percentage){
		RGBA black;
		black = new RGBA();
		black.a = 1d;
		black.r = 1d - percentage;
		black.g = 1d - percentage;
		black.b = 1d - percentage;
		return black;
	}


	public static RGBA CreateRGBColor(double r, double g, double b){
		RGBA color;
		color = new RGBA();
		color.a = 1d;
		color.r = r;
		color.g = g;
		color.b = b;
		return color;
	}


	public static RGBA CreateRGBAColor(double r, double g, double b, double a){
		RGBA color;
		color = new RGBA();
		color.a = a;
		color.r = r;
		color.g = g;
		color.b = b;
		return color;
	}


	public static RGBABitmapImage CreateImage(double w, double h, RGBA color){
		RGBABitmapImage image;
		double i, j;

		image = new RGBABitmapImage();
		image.x = new RGBABitmap [(int)(w)];
		for(i = 0d; i < w; i = i + 1d){
			image.x[(int)(i)] = new RGBABitmap();
			image.x[(int)(i)].y = new RGBA [(int)(h)];
			for(j = 0d; j < h; j = j + 1d){
				image.x[(int)(i)].y[(int)(j)] = new RGBA();
				SetPixel(image, i, j, color);
			}
		}

		return image;
	}


	public static void DeleteImage(RGBABitmapImage image){
		double i, j, w, h;

		w = ImageWidth(image);
		h = ImageHeight(image);

		for(i = 0d; i < w; i = i + 1d){
			for(j = 0d; j < h; j = j + 1d){
				delete(image.x[(int)(i)].y[(int)(j)]);
			}
			delete(image.x[(int)(i)]);
		}
		delete(image);
	}


	public static double ImageWidth(RGBABitmapImage image){
		return image.x.Length;
	}


	public static double ImageHeight(RGBABitmapImage image){
		double height;

		if(ImageWidth(image) == 0d){
			height = 0d;
		}else{
			height = image.x[0].y.Length;
		}

		return height;
	}


	public static void SetPixel(RGBABitmapImage image, double x, double y, RGBA color){
		if(x >= 0d && x < ImageWidth(image) && y >= 0d && y < ImageHeight(image)){
			image.x[(int)(x)].y[(int)(y)].a = color.a;
			image.x[(int)(x)].y[(int)(y)].r = color.r;
			image.x[(int)(x)].y[(int)(y)].g = color.g;
			image.x[(int)(x)].y[(int)(y)].b = color.b;
		}
	}


	public static void DrawPixel(RGBABitmapImage image, double x, double y, RGBA color){
		double ra, ga, ba, aa;
		double rb, gb, bb, ab;
		double ro, go, bo, ao;

		if(x >= 0d && x < ImageWidth(image) && y >= 0d && y < ImageHeight(image)){
			ra = color.r;
			ga = color.g;
			ba = color.b;
			aa = color.a;

			rb = image.x[(int)(x)].y[(int)(y)].r;
			gb = image.x[(int)(x)].y[(int)(y)].g;
			bb = image.x[(int)(x)].y[(int)(y)].b;
			ab = image.x[(int)(x)].y[(int)(y)].a;

			ao = CombineAlpha(aa, ab);

			ro = AlphaBlend(ra, aa, rb, ab, ao);
			go = AlphaBlend(ga, aa, gb, ab, ao);
			bo = AlphaBlend(ba, aa, bb, ab, ao);

			image.x[(int)(x)].y[(int)(y)].r = ro;
			image.x[(int)(x)].y[(int)(y)].g = go;
			image.x[(int)(x)].y[(int)(y)].b = bo;
			image.x[(int)(x)].y[(int)(y)].a = ao;
		}
	}


	public static double CombineAlpha(double asx, double ad){
		return asx + ad*(1d - asx);
	}


	public static double AlphaBlend(double cs, double asx, double cd, double ad, double ao){
		return (cs*asx + cd*ad*(1d - asx))/ao;
	}


	public static void DrawHorizontalLine1px(RGBABitmapImage image, double x, double y, double length, RGBA color){
		double i;

		for(i = 0d; i < length; i = i + 1d){
			DrawPixel(image, x + i, y, color);
		}
	}


	public static void DrawVerticalLine1px(RGBABitmapImage image, double x, double y, double height, RGBA color){
		double i;

		for(i = 0d; i < height; i = i + 1d){
			DrawPixel(image, x, y + i, color);
		}
	}


	public static void DrawRectangle1px(RGBABitmapImage image, double x, double y, double width, double height, RGBA color){
		DrawHorizontalLine1px(image, x, y, width + 1d, color);
		DrawVerticalLine1px(image, x, y + 1d, height + 1d - 1d, color);
		DrawVerticalLine1px(image, x + width, y + 1d, height + 1d - 1d, color);
		DrawHorizontalLine1px(image, x + 1d, y + height, width + 1d - 2d, color);
	}


	public static void DrawImageOnImage(RGBABitmapImage dst, RGBABitmapImage src, double topx, double topy){
		double y, x;

		for(y = 0d; y < ImageHeight(src); y = y + 1d){
			for(x = 0d; x < ImageWidth(src); x = x + 1d){
				if(topx + x >= 0d && topx + x < ImageWidth(dst) && topy + y >= 0d && topy + y < ImageHeight(dst)){
					DrawPixel(dst, topx + x, topy + y, src.x[(int)(x)].y[(int)(y)]);
				}
			}
		}
	}


	public static void DrawLine1px(RGBABitmapImage image, double x0, double y0, double x1, double y1, RGBA color){
		XiaolinWusLineAlgorithm(image, x0, y0, x1, y1, color);
	}


	public static void XiaolinWusLineAlgorithm(RGBABitmapImage image, double x0, double y0, double x1, double y1, RGBA color){
		bool steep;
		double x, t, dx, dy, g, xEnd, yEnd, xGap, xpxl1, ypxl1, intery, xpxl2, ypxl2, olda;

		olda = color.a;

		steep = Abs(y1 - y0) > Abs(x1 - x0);

		if(steep){
			t = x0;
			x0 = y0;
			y0 = t;

			t = x1;
			x1 = y1;
			y1 = t;
		}
		if(x0 > x1){
			t = x0;
			x0 = x1;
			x1 = t;

			t = y0;
			y0 = y1;
			y1 = t;
		}

		dx = x1 - x0;
		dy = y1 - y0;
		g = dy/dx;

		if(dx == 0d){
			g = 1d;
		}

		xEnd = Roundx(x0);
		yEnd = y0 + g*(xEnd - x0);
		xGap = OneMinusFractionalPart(x0 + 0.5);
		xpxl1 = xEnd;
		ypxl1 = Floor(yEnd);
		if(steep){
			DrawPixel(image, ypxl1, xpxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
			DrawPixel(image, ypxl1 + 1d, xpxl1, SetBrightness(color, FractionalPart(yEnd)*xGap));
		}else{
			DrawPixel(image, xpxl1, ypxl1, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
			DrawPixel(image, xpxl1, ypxl1 + 1d, SetBrightness(color, FractionalPart(yEnd)*xGap));
		}
		intery = yEnd + g;

		xEnd = Roundx(x1);
		yEnd = y1 + g*(xEnd - x1);
		xGap = FractionalPart(x1 + 0.5);
		xpxl2 = xEnd;
		ypxl2 = Floor(yEnd);
		if(steep){
			DrawPixel(image, ypxl2, xpxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
			DrawPixel(image, ypxl2 + 1d, xpxl2, SetBrightness(color, FractionalPart(yEnd)*xGap));
		}else{
			DrawPixel(image, xpxl2, ypxl2, SetBrightness(color, OneMinusFractionalPart(yEnd)*xGap));
			DrawPixel(image, xpxl2, ypxl2 + 1d, SetBrightness(color, FractionalPart(yEnd)*xGap));
		}

		if(steep){
			for(x = xpxl1 + 1d; x <= xpxl2 - 1d; x = x + 1d){
				DrawPixel(image, Floor(intery), x, SetBrightness(color, OneMinusFractionalPart(intery)));
				DrawPixel(image, Floor(intery) + 1d, x, SetBrightness(color, FractionalPart(intery)));
				intery = intery + g;
			}
		}else{
			for(x = xpxl1 + 1d; x <= xpxl2 - 1d; x = x + 1d){
				DrawPixel(image, x, Floor(intery), SetBrightness(color, OneMinusFractionalPart(intery)));
				DrawPixel(image, x, Floor(intery) + 1d, SetBrightness(color, FractionalPart(intery)));
				intery = intery + g;
			}
		}

		color.a = olda;
	}


	public static double OneMinusFractionalPart(double x){
		return 1d - FractionalPart(x);
	}


	public static double FractionalPart(double x){
		return x - Floor(x);
	}


	public static RGBA SetBrightness(RGBA color, double newBrightness){
		color.a = newBrightness;
		return color;
	}


	public static void DrawQuadraticBezierCurve(RGBABitmapImage image, double x0, double y0, double cx, double cy, double x1, double y1, RGBA color){
		double t, dt, dx, dy;
		NumberReference xs, ys, xe, ye;

		dx = Abs(x0 - x1);
		dy = Abs(y0 - y1);

		dt = 1d/Sqrt(Pow(dx, 2d) + Pow(dy, 2d));

		xs = new NumberReference();
		ys = new NumberReference();
		xe = new NumberReference();
		ye = new NumberReference();

		QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, 0d, xs, ys);
		for(t = dt; t <= 1d; t = t + dt){
			QuadraticBezierPoint(x0, y0, cx, cy, x1, y1, t, xe, ye);
			DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color);
			xs.numberValue = xe.numberValue;
			ys.numberValue = ye.numberValue;
		}

		delete(xs);
		delete(ys);
		delete(xe);
		delete(ye);
	}


	public static void QuadraticBezierPoint(double x0, double y0, double cx, double cy, double x1, double y1, double t, NumberReference x, NumberReference y){
		x.numberValue = Pow(1d - t, 2d)*x0 + (1d - t)*2d*t*cx + Pow(t, 2d)*x1;
		y.numberValue = Pow(1d - t, 2d)*y0 + (1d - t)*2d*t*cy + Pow(t, 2d)*y1;
	}


	public static void DrawCubicBezierCurve(RGBABitmapImage image, double x0, double y0, double c0x, double c0y, double c1x, double c1y, double x1, double y1, RGBA color){
		double t, dt, dx, dy;
		NumberReference xs, ys, xe, ye;

		dx = Abs(x0 - x1);
		dy = Abs(y0 - y1);

		dt = 1d/Sqrt(Pow(dx, 2d) + Pow(dy, 2d));

		xs = new NumberReference();
		ys = new NumberReference();
		xe = new NumberReference();
		ye = new NumberReference();

		CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, 0d, xs, ys);
		for(t = dt; t <= 1d; t = t + dt){
			CubicBezierPoint(x0, y0, c0x, c0y, c1x, c1y, x1, y1, t, xe, ye);
			DrawLine1px(image, xs.numberValue, ys.numberValue, xe.numberValue, ye.numberValue, color);
			xs.numberValue = xe.numberValue;
			ys.numberValue = ye.numberValue;
		}

		delete(xs);
		delete(ys);
		delete(xe);
		delete(ye);
	}


	public static void CubicBezierPoint(double x0, double y0, double c0x, double c0y, double c1x, double c1y, double x1, double y1, double t, NumberReference x, NumberReference y){
		x.numberValue = Pow(1d - t, 3d)*x0 + Pow(1d - t, 2d)*3d*t*c0x + (1d - t)*3d*Pow(t, 2d)*c1x + Pow(t, 3d)*x1;

		y.numberValue = Pow(1d - t, 3d)*y0 + Pow(1d - t, 2d)*3d*t*c0y + (1d - t)*3d*Pow(t, 2d)*c1y + Pow(t, 3d)*y1;
	}


	public static RGBABitmapImage CopyImage(RGBABitmapImage image){
		RGBABitmapImage copy;
		double i, j;

		copy = CreateImage(ImageWidth(image), ImageHeight(image), GetTransparent());

		for(i = 0d; i < ImageWidth(image); i = i + 1d){
			for(j = 0d; j < ImageHeight(image); j = j + 1d){
				SetPixel(copy, i, j, image.x[(int)(i)].y[(int)(j)]);
			}
		}

		return copy;
	}


	public static RGBA GetImagePixel(RGBABitmapImage image, double x, double y){
		return image.x[(int)(x)].y[(int)(y)];
	}


	public static void HorizontalFlip(RGBABitmapImage img){
		double y, x;
		double tmp;
		RGBA c1, c2;

		for(y = 0d; y < ImageHeight(img); y = y + 1d){
			for(x = 0d; x < ImageWidth(img)/2d; x = x + 1d){
				c1 = img.x[(int)(x)].y[(int)(y)];
				c2 = img.x[(int)(ImageWidth(img) - 1d - x)].y[(int)(y)];

				tmp = c1.a;
				c1.a = c2.a;
				c2.a = tmp;

				tmp = c1.r;
				c1.r = c2.r;
				c2.r = tmp;

				tmp = c1.g;
				c1.g = c2.g;
				c2.g = tmp;

				tmp = c1.b;
				c1.b = c2.b;
				c2.b = tmp;
			}
		}
	}


	public static void DrawFilledRectangle(RGBABitmapImage image, double x, double y, double w, double h, RGBA color){
		double i, j;

		for(i = 0d; i < w; i = i + 1d){
			for(j = 0d; j < h; j = j + 1d){
				SetPixel(image, x + i, y + j, color);
			}
		}
	}


	public static RGBABitmapImage RotateAntiClockwise90Degrees(RGBABitmapImage image){
		RGBABitmapImage rotated;
		double x, y;

		rotated = CreateImage(ImageHeight(image), ImageWidth(image), GetBlack());

		for(y = 0d; y < ImageHeight(image); y = y + 1d){
			for(x = 0d; x < ImageWidth(image); x = x + 1d){
				SetPixel(rotated, y, ImageWidth(image) - 1d - x, GetImagePixel(image, x, y));
			}
		}

		return rotated;
	}


	public static void DrawCircle(RGBABitmapImage canvas, double xCenter, double yCenter, double radius, RGBA color){
		DrawCircleBasicAlgorithm(canvas, xCenter, yCenter, radius, color);
	}


	public static void BresenhamsCircleDrawingAlgorithm(RGBABitmapImage canvas, double xCenter, double yCenter, double radius, RGBA color){
		double x, y, delta;

		y = radius;
		x = 0d;

		delta = 3d - 2d*radius;
		for(; y >= x; x = x + 1d){
			DrawLine1px(canvas, xCenter + x, yCenter + y, xCenter + x, yCenter + y, color);
			DrawLine1px(canvas, xCenter + x, yCenter - y, xCenter + x, yCenter - y, color);
			DrawLine1px(canvas, xCenter - x, yCenter + y, xCenter - x, yCenter + y, color);
			DrawLine1px(canvas, xCenter - x, yCenter - y, xCenter - x, yCenter - y, color);

			DrawLine1px(canvas, xCenter - y, yCenter + x, xCenter - y, yCenter + x, color);
			DrawLine1px(canvas, xCenter - y, yCenter - x, xCenter - y, yCenter - x, color);
			DrawLine1px(canvas, xCenter + y, yCenter + x, xCenter + y, yCenter + x, color);
			DrawLine1px(canvas, xCenter + y, yCenter - x, xCenter + y, yCenter - x, color);

			if(delta < 0d){
				delta = delta + 4d*x + 6d;
			}else{
				delta = delta + 4d*(x - y) + 10d;
				y = y - 1d;
			}
		}
	}


	public static void DrawCircleMidpointAlgorithm(RGBABitmapImage canvas, double xCenter, double yCenter, double radius, RGBA color){
		double d, x, y;

		d = Floor((5d - radius*4d)/4d);
		x = 0d;
		y = radius;

		for(; x <= y; x = x + 1d){
			DrawPixel(canvas, xCenter + x, yCenter + y, color);
			DrawPixel(canvas, xCenter + x, yCenter - y, color);
			DrawPixel(canvas, xCenter - x, yCenter + y, color);
			DrawPixel(canvas, xCenter - x, yCenter - y, color);
			DrawPixel(canvas, xCenter + y, yCenter + x, color);
			DrawPixel(canvas, xCenter + y, yCenter - x, color);
			DrawPixel(canvas, xCenter - y, yCenter + x, color);
			DrawPixel(canvas, xCenter - y, yCenter - x, color);

			if(d < 0d){
				d = d + 2d*x + 1d;
			}else{
				d = d + 2d*(x - y) + 1d;
				y = y - 1d;
			}
		}
	}


	public static void DrawCircleBasicAlgorithm(RGBABitmapImage canvas, double xCenter, double yCenter, double radius, RGBA color){
		double pixels, a, da, dx, dy;

		/* Place the circle in the center of the pixel.*/
		xCenter = Floor(xCenter) + 0.5;
		yCenter = Floor(yCenter) + 0.5;

		pixels = 2d*PI*radius;

		/* Below a radius of 10 pixels, over-compensate to get a smoother circle.*/
		if(radius < 10d){
			pixels = pixels*10d;
		}

		da = 2d*PI/pixels;

		for(a = 0d; a < 2d*PI; a = a + da){
			dx = Cos(a)*radius;
			dy = Sin(a)*radius;

			/* Floor to get the pixel coordinate.*/
			DrawPixel(canvas, Floor(xCenter + dx), Floor(yCenter + dy), color);
		}
	}


	public static void DrawFilledCircle(RGBABitmapImage canvas, double x, double y, double r, RGBA color){
		DrawFilledCircleBasicAlgorithm(canvas, x, y, r, color);
	}


	public static void DrawFilledCircleMidpointAlgorithm(RGBABitmapImage canvas, double xCenter, double yCenter, double radius, RGBA color){
		double d, x, y;

		d = Floor((5d - radius*4d)/4d);
		x = 0d;
		y = radius;

		for(; x <= y; x = x + 1d){
			DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter + y, xCenter - x, yCenter + y, color);
			DrawLineBresenhamsAlgorithm(canvas, xCenter + x, yCenter - y, xCenter - x, yCenter - y, color);
			DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter + x, xCenter - y, yCenter + x, color);
			DrawLineBresenhamsAlgorithm(canvas, xCenter + y, yCenter - x, xCenter - y, yCenter - x, color);

			if(d < 0d){
				d = d + 2d*x + 1d;
			}else{
				d = d + 2d*(x - y) + 1d;
				y = y - 1d;
			}
		}
	}


	public static void DrawFilledCircleBasicAlgorithm(RGBABitmapImage canvas, double xCenter, double yCenter, double radius, RGBA color){
		double pixels, a, da, dx, dy;

		/* Place the circle in the center of the pixel.*/
		xCenter = Floor(xCenter) + 0.5;
		yCenter = Floor(yCenter) + 0.5;

		pixels = 2d*PI*radius;

		/* Below a radius of 10 pixels, over-compensate to get a smoother circle.*/
		if(radius < 10d){
			pixels = pixels*10d;
		}

		da = 2d*PI/pixels;

		/* Draw lines for a half-circle to fill an entire circle.*/
		for(a = 0d; a < PI; a = a + da){
			dx = Cos(a)*radius;
			dy = Sin(a)*radius;

			/* Floor to get the pixel coordinate.*/
			DrawVerticalLine1px(canvas, Floor(xCenter - dx), Floor(yCenter - dy), Floor(2d*dy) + 1d, color);
		}
	}


	public static void DrawTriangle(RGBABitmapImage canvas, double xCenter, double yCenter, double height, RGBA color){
		double x1, y1, x2, y2, x3, y3;

		x1 = Floor(xCenter + 0.5);
		y1 = Floor(Floor(yCenter + 0.5) - height);
		x2 = x1 - 2d*height*Tan(PI/6d);
		y2 = Floor(y1 + 2d*height);
		x3 = x1 + 2d*height*Tan(PI/6d);
		y3 = Floor(y1 + 2d*height);

		DrawLine1px(canvas, x1, y1, x2, y2, color);
		DrawLine1px(canvas, x1, y1, x3, y3, color);
		DrawLine1px(canvas, x2, y2, x3, y3, color);
	}


	public static void DrawFilledTriangle(RGBABitmapImage canvas, double xCenter, double yCenter, double height, RGBA color){
		double i, offset, x1, y1;

		x1 = Floor(xCenter + 0.5);
		y1 = Floor(Floor(yCenter + 0.5) - height);

		for(i = 0d; i <= 2d*height; i = i + 1d){
			offset = Floor(i*Tan(PI/6d));
			DrawHorizontalLine1px(canvas, x1 - offset, y1 + i, 2d*offset, color);
		}
	}


	public static void DrawLine(RGBABitmapImage canvas, double x1, double y1, double x2, double y2, double thickness, RGBA color){
		DrawLineBresenhamsAlgorithmThick(canvas, x1, y1, x2, y2, thickness, color);
	}


	public static void DrawLineBresenhamsAlgorithmThick(RGBABitmapImage canvas, double x1, double y1, double x2, double y2, double thickness, RGBA color){
		double x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r;

		dx = x2 - x1;
		dy = y2 - y1;

		incX = Sign(dx);
		incY = Sign(dy);

		dx = Abs(dx);
		dy = Abs(dy);

		if(dx > dy){
			pdx = incX;
			pdy = 0d;
			es = dy;
			el = dx;
		}else{
			pdx = 0d;
			pdy = incY;
			es = dx;
			el = dy;
		}

		x = x1;
		y = y1;
		err = el/2d;

		if(thickness >= 3d){
			r = thickness/2d;
			DrawCircle(canvas, x, y, r, color);
		}else if(Floor(thickness) == 2d){
			DrawFilledRectangle(canvas, x, y, 2d, 2d, color);
		}else if(Floor(thickness) == 1d){
			DrawPixel(canvas, x, y, color);
		}

		for(t = 0d; t < el; t = t + 1d){
			err = err - es;
			if(err < 0d){
				err = err + el;
				x = x + incX;
				y = y + incY;
			}else{
				x = x + pdx;
				y = y + pdy;
			}

			if(thickness >= 3d){
				r = thickness/2d;
				DrawCircle(canvas, x, y, r, color);
			}else if(Floor(thickness) == 2d){
				DrawFilledRectangle(canvas, x, y, 2d, 2d, color);
			}else if(Floor(thickness) == 1d){
				DrawPixel(canvas, x, y, color);
			}
		}
	}


	public static void DrawLineBresenhamsAlgorithm(RGBABitmapImage canvas, double x1, double y1, double x2, double y2, RGBA color){
		double x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t;

		dx = x2 - x1;
		dy = y2 - y1;

		incX = Sign(dx);
		incY = Sign(dy);

		dx = Abs(dx);
		dy = Abs(dy);

		if(dx > dy){
			pdx = incX;
			pdy = 0d;
			es = dy;
			el = dx;
		}else{
			pdx = 0d;
			pdy = incY;
			es = dx;
			el = dy;
		}

		x = x1;
		y = y1;
		err = el/2d;
		DrawPixel(canvas, x, y, color);

		for(t = 0d; t < el; t = t + 1d){
			err = err - es;
			if(err < 0d){
				err = err + el;
				x = x + incX;
				y = y + incY;
			}else{
				x = x + pdx;
				y = y + pdy;
			}

			DrawPixel(canvas, x, y, color);
		}
	}


	public static void DrawLineBresenhamsAlgorithmThickPatterned(RGBABitmapImage canvas, double x1, double y1, double x2, double y2, double thickness, bool [] pattern, NumberReference offset, RGBA color){
		double x, y, dx, dy, incX, incY, pdx, pdy, es, el, err, t, r;

		dx = x2 - x1;
		dy = y2 - y1;

		incX = Sign(dx);
		incY = Sign(dy);

		dx = Abs(dx);
		dy = Abs(dy);

		if(dx > dy){
			pdx = incX;
			pdy = 0d;
			es = dy;
			el = dx;
		}else{
			pdx = 0d;
			pdy = incY;
			es = dx;
			el = dy;
		}

		x = x1;
		y = y1;
		err = el/2d;

		offset.numberValue = (offset.numberValue + 1d)%(pattern.Length*thickness);

		if(pattern[(int)(Floor(offset.numberValue/thickness))]){
			if(thickness >= 3d){
				r = thickness/2d;
				DrawCircle(canvas, x, y, r, color);
			}else if(Floor(thickness) == 2d){
				DrawFilledRectangle(canvas, x, y, 2d, 2d, color);
			}else if(Floor(thickness) == 1d){
				DrawPixel(canvas, x, y, color);
			}
		}

		for(t = 0d; t < el; t = t + 1d){
			err = err - es;
			if(err < 0d){
				err = err + el;
				x = x + incX;
				y = y + incY;
			}else{
				x = x + pdx;
				y = y + pdy;
			}

			offset.numberValue = (offset.numberValue + 1d)%(pattern.Length*thickness);

			if(pattern[(int)(Floor(offset.numberValue/thickness))]){
				if(thickness >= 3d){
					r = thickness/2d;
					DrawCircle(canvas, x, y, r, color);
				}else if(Floor(thickness) == 2d){
					DrawFilledRectangle(canvas, x, y, 2d, 2d, color);
				}else if(Floor(thickness) == 1d){
					DrawPixel(canvas, x, y, color);
				}
			}
		}
	}


	public static bool [] GetLinePattern5(){
		bool [] pattern;

		pattern = new bool [19];

		pattern[0] = true;
		pattern[1] = true;
		pattern[2] = true;
		pattern[3] = true;
		pattern[4] = true;
		pattern[5] = true;
		pattern[6] = true;
		pattern[7] = true;
		pattern[8] = true;
		pattern[9] = true;
		pattern[10] = false;
		pattern[11] = false;
		pattern[12] = false;
		pattern[13] = true;
		pattern[14] = true;
		pattern[15] = true;
		pattern[16] = false;
		pattern[17] = false;
		pattern[18] = false;

		return pattern;
	}


	public static bool [] GetLinePattern4(){
		bool [] pattern;

		pattern = new bool [13];

		pattern[0] = true;
		pattern[1] = true;
		pattern[2] = true;
		pattern[3] = true;
		pattern[4] = true;
		pattern[5] = true;
		pattern[6] = true;
		pattern[7] = true;
		pattern[8] = true;
		pattern[9] = true;
		pattern[10] = false;
		pattern[11] = false;
		pattern[12] = false;

		return pattern;
	}


	public static bool [] GetLinePattern3(){
		bool [] pattern;

		pattern = new bool [13];

		pattern[0] = true;
		pattern[1] = true;
		pattern[2] = true;
		pattern[3] = true;
		pattern[4] = true;
		pattern[5] = true;
		pattern[6] = false;
		pattern[7] = false;
		pattern[8] = false;
		pattern[9] = true;
		pattern[10] = true;
		pattern[11] = false;
		pattern[12] = false;

		return pattern;
	}


	public static bool [] GetLinePattern2(){
		bool [] pattern;

		pattern = new bool [4];

		pattern[0] = true;
		pattern[1] = true;
		pattern[2] = false;
		pattern[3] = false;

		return pattern;
	}


	public static bool [] GetLinePattern1(){
		bool [] pattern;

		pattern = new bool [8];

		pattern[0] = true;
		pattern[1] = true;
		pattern[2] = true;
		pattern[3] = true;
		pattern[4] = true;
		pattern[5] = false;
		pattern[6] = false;
		pattern[7] = false;

		return pattern;
	}


	public static RGBABitmapImage Blur(RGBABitmapImage src, double pixels){
		RGBABitmapImage dst;
		double x, y, w, h;

		w = ImageWidth(src);
		h = ImageHeight(src);
		dst = CreateImage(w, h, GetTransparent());

		for(x = 0d; x < w; x = x + 1d){
			for(y = 0d; y < h; y = y + 1d){
				SetPixel(dst, x, y, CreateBlurForPoint(src, x, y, pixels));
			}
		}

		return dst;
	}


	public static RGBA CreateBlurForPoint(RGBABitmapImage src, double x, double y, double pixels){
		RGBA rgba;
		double i, j, countColor, countTransparent;
		double fromx, tox, fromy, toy;
		double w, h;
		double alpha;

		w = src.x.Length;
		h = src.x[0].y.Length;

		rgba = new RGBA();
		rgba.r = 0d;
		rgba.g = 0d;
		rgba.b = 0d;
		rgba.a = 0d;

		fromx = x - pixels;
		fromx = Max(fromx, 0d);

		tox = x + pixels;
		tox = Min(tox, w - 1d);

		fromy = y - pixels;
		fromy = Max(fromy, 0d);

		toy = y + pixels;
		toy = Min(toy, h - 1d);

		countColor = 0d;
		countTransparent = 0d;
		for(i = fromx; i < tox; i = i + 1d){
			for(j = fromy; j < toy; j = j + 1d){
				alpha = src.x[(int)(i)].y[(int)(j)].a;
				if(alpha > 0d){
					rgba.r = rgba.r + src.x[(int)(i)].y[(int)(j)].r;
					rgba.g = rgba.g + src.x[(int)(i)].y[(int)(j)].g;
					rgba.b = rgba.b + src.x[(int)(i)].y[(int)(j)].b;
					countColor = countColor + 1d;
				}
				rgba.a = rgba.a + alpha;
				countTransparent = countTransparent + 1d;
			}
		}

		if(countColor > 0d){
			rgba.r = rgba.r/countColor;
			rgba.g = rgba.g/countColor;
			rgba.b = rgba.b/countColor;
		}else{
			rgba.r = 0d;
			rgba.g = 0d;
			rgba.b = 0d;
		}

		if(countTransparent > 0d){
			rgba.a = rgba.a/countTransparent;
		}else{
			rgba.a = 0d;
		}

		return rgba;
	}


	public static char [] CreateStringScientificNotationDecimalFromNumber(double decimalx){
		StringReference mantissaReference, exponentReference;
		double multiplier, inc;
		double exponent;
		bool done, isPositive;
		char [] result;

		mantissaReference = new StringReference();
		exponentReference = new StringReference();
		result = new char [0];
		done = false;
		exponent = 0d;

		if(decimalx < 0d){
			isPositive = false;
			decimalx = -decimalx;
		}else{
			isPositive = true;
		}

		if(decimalx == 0d){
			done = true;
		}

		if(!done){
			multiplier = 0d;
			inc = 0d;

			if(decimalx < 1d){
				multiplier = 10d;
				inc = -1d;
			}else if(decimalx >= 10d){
				multiplier = 0.1;
				inc = 1d;
			}else{
				done = true;
			}

			if(!done){
				for(; decimalx >= 10d || decimalx < 1d; ){
					decimalx = decimalx*multiplier;
					exponent = exponent + inc;
				}
			}
		}

		CreateStringFromNumberWithCheck(decimalx, 10d, mantissaReference);

		CreateStringFromNumberWithCheck(exponent, 10d, exponentReference);

		if(!isPositive){
			result = AppendString(result, "-".ToCharArray());
		}

		result = AppendString(result, mantissaReference.stringx);
		result = AppendString(result, "e".ToCharArray());
		result = AppendString(result, exponentReference.stringx);

		return result;
	}


	public static char [] CreateStringDecimalFromNumber(double decimalx){
		StringReference stringReference;

		stringReference = new StringReference();

		/* This will succeed because base = 10.*/
		CreateStringFromNumberWithCheck(decimalx, 10d, stringReference);

		return stringReference.stringx;
	}


	public static bool CreateStringFromNumberWithCheck(double decimalx, double basex, StringReference stringReference){
		char [] stringx;
		double maximumDigits;
		double digitPosition;
		bool hasPrintedPoint, isPositive;
		double i, d;
		bool success;
		CharacterReference characterReference;
		char c;

		isPositive = true;

		if(decimalx < 0d){
			isPositive = false;
			decimalx = -decimalx;
		}

		if(decimalx == 0d){
			stringReference.stringx = "0".ToCharArray();
			success = true;
		}else{
			characterReference = new CharacterReference();

			if(IsInteger(basex)){
				success = true;

				stringx = new char [0];

				maximumDigits = GetMaximumDigitsForBase(basex);

				digitPosition = GetFirstDigitPosition(decimalx, basex);

				decimalx = (double)Round(decimalx*Pow(basex, maximumDigits - digitPosition - 1d));

				hasPrintedPoint = false;

				if(!isPositive){
					stringx = AppendCharacter(stringx, '-');
				}

				/* Print leading zeros.*/
				if(digitPosition < 0d){
					stringx = AppendCharacter(stringx, '0');
					stringx = AppendCharacter(stringx, '.');
					hasPrintedPoint = true;
					for(i = 0d; i < -digitPosition - 1d; i = i + 1d){
						stringx = AppendCharacter(stringx, '0');
					}
				}

				/* Print number.*/
				for(i = 0d; i < maximumDigits && success; i = i + 1d){
					d = Floor(decimalx/Pow(basex, maximumDigits - i - 1d));

					if(d >= basex){
						d = basex - 1d;
					}

					if(!hasPrintedPoint && digitPosition - i + 1d == 0d){
						if(decimalx != 0d){
							stringx = AppendCharacter(stringx, '.');
						}
						hasPrintedPoint = true;
					}

					if(decimalx == 0d && hasPrintedPoint){
					}else{
						success = GetSingleDigitCharacterFromNumberWithCheck(d, basex, characterReference);
						if(success){
							c = characterReference.characterValue;
							stringx = AppendCharacter(stringx, c);
						}
					}

					if(success){
						decimalx = decimalx - d*Pow(basex, maximumDigits - i - 1d);
					}
				}

				if(success){
					/* Print trailing zeros.*/
					for(i = 0d; i < digitPosition - maximumDigits + 1d; i = i + 1d){
						stringx = AppendCharacter(stringx, '0');
					}

					stringReference.stringx = stringx;
				}
			}else{
				success = false;
			}
		}

		/* Done*/
		return success;
	}


	public static double GetMaximumDigitsForBase(double basex){
		double t;

		t = Pow(10d, 15d);
		return Floor(Log10(t)/Log10(basex));
	}


	public static double GetFirstDigitPosition(double decimalx, double basex){
		double power;
		double t;

		power = Ceiling(Log10(decimalx)/Log10(basex));

		t = decimalx*Pow(basex, -power);
		if(t < basex && t >= 1d){
		}else if(t >= basex){
			power = power + 1d;
		}else if(t < 1d){
			power = power - 1d;
		}

		return power;
	}


	public static bool GetSingleDigitCharacterFromNumberWithCheck(double c, double basex, CharacterReference characterReference){
		char [] numberTable;
		bool success;

		numberTable = GetDigitCharacterTable();

		if(c < basex || c < numberTable.Length){
			success = true;
			characterReference.characterValue = numberTable[(int)(c)];
		}else{
			success = false;
		}

		return success;
	}


	public static char [] GetDigitCharacterTable(){
		char [] numberTable;

		numberTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

		return numberTable;
	}


	public static bool CreateNumberFromDecimalStringWithCheck(char [] stringx, NumberReference decimalReference, StringReference errorMessage){
		return CreateNumberFromStringWithCheck(stringx, 10d, decimalReference, errorMessage);
	}


	public static double CreateNumberFromDecimalString(char [] stringx){
		NumberReference doubleReference;
		StringReference stringReference;
		double number;

		doubleReference = CreateNumberReference(0d);
		stringReference = CreateStringReference("".ToCharArray());
		CreateNumberFromStringWithCheck(stringx, 10d, doubleReference, stringReference);
		number = doubleReference.numberValue;

		delete(doubleReference);
		delete(stringReference);

		return number;
	}


	public static bool CreateNumberFromStringWithCheck(char [] stringx, double basex, NumberReference numberReference, StringReference errorMessage){
		bool success;
		BooleanReference numberIsPositive, exponentIsPositive;
		NumberArrayReference beforePoint, afterPoint, exponent;

		numberIsPositive = CreateBooleanReference(true);
		exponentIsPositive = CreateBooleanReference(true);
		beforePoint = new NumberArrayReference();
		afterPoint = new NumberArrayReference();
		exponent = new NumberArrayReference();

		if(basex >= 2d && basex <= 36d){
			success = ExtractPartsFromNumberString(stringx, basex, numberIsPositive, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessage);

			if(success){
				numberReference.numberValue = CreateNumberFromParts(basex, numberIsPositive.booleanValue, beforePoint.numberArray, afterPoint.numberArray, exponentIsPositive.booleanValue, exponent.numberArray);
			}
		}else{
			success = false;
			errorMessage.stringx = "Base must be from 2 to 36.".ToCharArray();
		}

		return success;
	}


	public static double CreateNumberFromParts(double basex, bool numberIsPositive, double [] beforePoint, double [] afterPoint, bool exponentIsPositive, double [] exponent){
		double n, i, p, e;

		n = 0d;

		for(i = 0d; i < beforePoint.Length; i = i + 1d){
			p = beforePoint[(int)(beforePoint.Length - i - 1d)];

			n = n + p*Pow(basex, i);
		}

		for(i = 0d; i < afterPoint.Length; i = i + 1d){
			p = afterPoint[(int)(i)];

			n = n + p*Pow(basex, -(i + 1d));
		}

		if(exponent.Length > 0d){
			e = 0d;
			for(i = 0d; i < exponent.Length; i = i + 1d){
				p = exponent[(int)(exponent.Length - i - 1d)];

				e = e + p*Pow(basex, i);
			}

			if(!exponentIsPositive){
				e = -e;
			}

			n = n*Pow(basex, e);
		}

		if(!numberIsPositive){
			n = -n;
		}

		return n;
	}


	public static bool ExtractPartsFromNumberString(char [] n, double basex, BooleanReference numberIsPositive, NumberArrayReference beforePoint, NumberArrayReference afterPoint, BooleanReference exponentIsPositive, NumberArrayReference exponent, StringReference errorMessages){
		double i;
		bool success;

		i = 0d;

		if(i < n.Length){
			if(n[(int)(i)] == '-'){
				numberIsPositive.booleanValue = false;
				i = i + 1d;
			}else if(n[(int)(i)] == '+'){
				numberIsPositive.booleanValue = true;
				i = i + 1d;
			}

			success = ExtractPartsFromNumberStringFromSign(n, basex, i, beforePoint, afterPoint, exponentIsPositive, exponent, errorMessages);
		}else{
			success = false;
			errorMessages.stringx = "Number cannot have length zero.".ToCharArray();
		}

		return success;
	}


	public static bool ExtractPartsFromNumberStringFromSign(char [] n, double basex, double i, NumberArrayReference beforePoint, NumberArrayReference afterPoint, BooleanReference exponentIsPositive, NumberArrayReference exponent, StringReference errorMessages){
		bool success, done;
		double count, j;

		done = false;
		count = 0d;
		for(; i + count < n.Length && !done; ){
			if(CharacterIsNumberCharacterInBase(n[(int)(i + count)], basex)){
				count = count + 1d;
			}else{
				done = true;
			}
		}

		if(count >= 1d){
			beforePoint.numberArray = new double [(int)(count)];

			for(j = 0d; j < count; j = j + 1d){
				beforePoint.numberArray[(int)(j)] = GetNumberFromNumberCharacterForBase(n[(int)(i + j)], basex);
			}

			i = i + count;

			if(i < n.Length){
				success = ExtractPartsFromNumberStringFromPointOrExponent(n, basex, i, afterPoint, exponentIsPositive, exponent, errorMessages);
			}else{
				afterPoint.numberArray = new double [0];
				exponent.numberArray = new double [0];
				success = true;
			}
		}else{
			success = false;
			errorMessages.stringx = "Number must have at least one number after the optional sign.".ToCharArray();
		}

		return success;
	}


	public static bool ExtractPartsFromNumberStringFromPointOrExponent(char [] n, double basex, double i, NumberArrayReference afterPoint, BooleanReference exponentIsPositive, NumberArrayReference exponent, StringReference errorMessages){
		bool success, done;
		double count, j;

		if(n[(int)(i)] == '.'){
			i = i + 1d;

			if(i < n.Length){
				done = false;
				count = 0d;
				for(; i + count < n.Length && !done; ){
					if(CharacterIsNumberCharacterInBase(n[(int)(i + count)], basex)){
						count = count + 1d;
					}else{
						done = true;
					}
				}

				if(count >= 1d){
					afterPoint.numberArray = new double [(int)(count)];

					for(j = 0d; j < count; j = j + 1d){
						afterPoint.numberArray[(int)(j)] = GetNumberFromNumberCharacterForBase(n[(int)(i + j)], basex);
					}

					i = i + count;

					if(i < n.Length){
						success = ExtractPartsFromNumberStringFromExponent(n, basex, i, exponentIsPositive, exponent, errorMessages);
					}else{
						exponent.numberArray = new double [0];
						success = true;
					}
				}else{
					success = false;
					errorMessages.stringx = "There must be at least one digit after the decimal point.".ToCharArray();
				}
			}else{
				success = false;
				errorMessages.stringx = "There must be at least one digit after the decimal point.".ToCharArray();
			}
		}else if(basex <= 14d && (n[(int)(i)] == 'e' || n[(int)(i)] == 'E')){
			if(i < n.Length){
				success = ExtractPartsFromNumberStringFromExponent(n, basex, i, exponentIsPositive, exponent, errorMessages);
				afterPoint.numberArray = new double [0];
			}else{
				success = false;
				errorMessages.stringx = "There must be at least one digit after the exponent.".ToCharArray();
			}
		}else{
			success = false;
			errorMessages.stringx = "Expected decimal point or exponent symbol.".ToCharArray();
		}

		return success;
	}


	public static bool ExtractPartsFromNumberStringFromExponent(char [] n, double basex, double i, BooleanReference exponentIsPositive, NumberArrayReference exponent, StringReference errorMessages){
		bool success, done;
		double count, j;

		if(basex <= 14d && (n[(int)(i)] == 'e' || n[(int)(i)] == 'E')){
			i = i + 1d;

			if(i < n.Length){
				if(n[(int)(i)] == '-'){
					exponentIsPositive.booleanValue = false;
					i = i + 1d;
				}else if(n[(int)(i)] == '+'){
					exponentIsPositive.booleanValue = true;
					i = i + 1d;
				}

				if(i < n.Length){
					done = false;
					count = 0d;
					for(; i + count < n.Length && !done; ){
						if(CharacterIsNumberCharacterInBase(n[(int)(i + count)], basex)){
							count = count + 1d;
						}else{
							done = true;
						}
					}

					if(count >= 1d){
						exponent.numberArray = new double [(int)(count)];

						for(j = 0d; j < count; j = j + 1d){
							exponent.numberArray[(int)(j)] = GetNumberFromNumberCharacterForBase(n[(int)(i + j)], basex);
						}

						i = i + count;

						if(i == n.Length){
							success = true;
						}else{
							success = false;
							errorMessages.stringx = "There cannot be any characters past the exponent of the number.".ToCharArray();
						}
					}else{
						success = false;
						errorMessages.stringx = "There must be at least one digit after the decimal point.".ToCharArray();
					}
				}else{
					success = false;
					errorMessages.stringx = "There must be at least one digit after the exponent symbol.".ToCharArray();
				}
			}else{
				success = false;
				errorMessages.stringx = "There must be at least one digit after the exponent symbol.".ToCharArray();
			}
		}else{
			success = false;
			errorMessages.stringx = "Expected exponent symbol.".ToCharArray();
		}

		return success;
	}


	public static double GetNumberFromNumberCharacterForBase(char c, double basex){
		char [] numberTable;
		double i;
		double position;

		numberTable = GetDigitCharacterTable();
		position = 0d;

		for(i = 0d; i < basex; i = i + 1d){
			if(numberTable[(int)(i)] == c){
				position = i;
			}
		}

		return position;
	}


	public static bool CharacterIsNumberCharacterInBase(char c, double basex){
		char [] numberTable;
		double i;
		bool found;

		numberTable = GetDigitCharacterTable();
		found = false;

		for(i = 0d; i < basex; i = i + 1d){
			if(numberTable[(int)(i)] == c){
				found = true;
			}
		}

		return found;
	}


	public static double [] StringToNumberArray(char [] str){
		NumberArrayReference numberArrayReference;
		StringReference stringReference;
		double [] numbers;

		numberArrayReference = new NumberArrayReference();
		stringReference = new StringReference();

		StringToNumberArrayWithCheck(str, numberArrayReference, stringReference);

		numbers = numberArrayReference.numberArray;

		delete(numberArrayReference);
		delete(stringReference);

		return numbers;
	}


	public static bool StringToNumberArrayWithCheck(char [] str, NumberArrayReference numberArrayReference, StringReference errorMessage){
		StringReference [] numberStrings;
		double [] numbers;
		double i;
		char [] numberString, trimmedNumberString;
		bool success;
		NumberReference numberReference;

		numberStrings = SplitByString(str, ",".ToCharArray());

		numbers = new double [(int)(numberStrings.Length)];
		success = true;
		numberReference = new NumberReference();

		for(i = 0d; i < numberStrings.Length; i = i + 1d){
			numberString = numberStrings[(int)(i)].stringx;
			trimmedNumberString = Trim(numberString);
			success = CreateNumberFromDecimalStringWithCheck(trimmedNumberString, numberReference, errorMessage);
			numbers[(int)(i)] = numberReference.numberValue;

			FreeStringReference(numberStrings[(int)(i)]);
			delete(trimmedNumberString);
		}

		delete(numberStrings);
		delete(numberReference);

		numberArrayReference.numberArray = numbers;

		return success;
	}


	public static double Negate(double x){
		return -x;
	}


	public static double Positive(double x){
		return +x;
	}


	public static double Factorial(double x){
		double i, f;

		f = 1d;

		for(i = 2d; i <= x; i = i + 1d){
			f = f*i;
		}

		return f;
	}


	public static double Roundx(double x){
		return Floor(x + 0.5);
	}


	public static double BankersRound(double x){
		double r;

		if(Absolute(x - Truncate(x)) == 0.5){
			if(!DivisibleBy(Roundx(x), 2d)){
				r = Roundx(x) - 1d;
			}else{
				r = Roundx(x);
			}
		}else{
			r = Roundx(x);
		}

		return r;
	}


	public static double Ceil(double x){
		return Ceiling(x);
	}


	public static double Floorx(double x){
		return Floor(x);
	}


	public static double Truncate(double x){
		double t;

		if(x >= 0d){
			t = Floor(x);
		}else{
			t = Ceiling(x);
		}

		return t;
	}


	public static double Absolute(double x){
		return Abs(x);
	}


	public static double Logarithm(double x){
		return Log10(x);
	}


	public static double NaturalLogarithm(double x){
		return Log(x);
	}


	public static double Sinx(double x){
		return Sin(x);
	}


	public static double Cosx(double x){
		return Cos(x);
	}


	public static double Tanx(double x){
		return Tan(x);
	}


	public static double Asinx(double x){
		return Asin(x);
	}


	public static double Acosx(double x){
		return Acos(x);
	}


	public static double Atanx(double x){
		return Atan(x);
	}


	public static double Atan2(double y, double x){
		double a;

		/* Atan2 is an invalid operation when x = 0 and y = 0, but this method does not return errors.*/
		a = 0d;

		if(x > 0d){
			a = Atanx(y/x);
		}else if(x < 0d && y >= 0d){
			a = Atanx(y/x) + PI;
		}else if(x < 0d && y < 0d){
			a = Atanx(y/x) - PI;
		}else if(x == 0d && y > 0d){
			a = PI/2d;
		}else if(x == 0d && y < 0d){
			a = -PI/2d;
		}

		return a;
	}


	public static double Squareroot(double x){
		return Sqrt(x);
	}


	public static double Expx(double x){
		return Exp(x);
	}


	public static bool DivisibleBy(double a, double b){
		return ((a%b) == 0d);
	}


	public static double Combinations(double n, double k){
		double i, j, c;

		c = 1d;
		j = 1d;
		i = n - k + 1d;

		for(; i <= n; ){
			c = c*i;
			c = c/j;

			i = i + 1d;
			j = j + 1d;
		}

		return c;
	}


	public static double Permutations(double n, double k){
		double i, c;

		c = 1d;

		for(i = n - k + 1d; i <= n; i = i + 1d){
			c = c*i;
		}

		return c;
	}


	public static bool EpsilonCompare(double a, double b, double epsilon){
		return Abs(a - b) < epsilon;
	}


	public static double GreatestCommonDivisor(double a, double b){
		double t;

		for(; b != 0d; ){
			t = b;
			b = a%b;
			a = t;
		}

		return a;
	}


	public static double GCDWithSubtraction(double a, double b){
		double g;

		if(a == 0d){
			g = b;
		}else{
			for(; b != 0d; ){
				if(a > b){
					a = a - b;
				}else{
					b = b - a;
				}
			}

			g = a;
		}

		return g;
	}


	public static bool IsInteger(double a){
		return (a - Floor(a)) == 0d;
	}


	public static bool GreatestCommonDivisorWithCheck(double a, double b, NumberReference gcdReference){
		bool success;
		double gcd;

		if(IsInteger(a) && IsInteger(b)){
			gcd = GreatestCommonDivisor(a, b);
			gcdReference.numberValue = gcd;
			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static double LeastCommonMultiple(double a, double b){
		double lcm;

		if(a > 0d && b > 0d){
			lcm = Abs(a*b)/GreatestCommonDivisor(a, b);
		}else{
			lcm = 0d;
		}

		return lcm;
	}


	public static double Sign(double a){
		double s;

		if(a > 0d){
			s = 1d;
		}else if(a < 0d){
			s = -1d;
		}else{
			s = 0d;
		}

		return s;
	}


	public static double Maxx(double a, double b){
		return Max(a, b);
	}


	public static double Minx(double a, double b){
		return Min(a, b);
	}


	public static double Power(double a, double b){
		return Pow(a, b);
	}


	public static double Gamma(double x){
		return LanczosApproximation(x);
	}


	public static double LogGamma(double x){
		return Log(Gamma(x));
	}


	public static double LanczosApproximation(double z){
		double [] p;
		double i, y, t, x;

		p = new double [8];
		p[0] = 676.5203681218851;
		p[1] = -1259.1392167224028;
		p[2] = 771.32342877765313;
		p[3] = -176.61502916214059;
		p[4] = 12.507343278686905;
		p[5] = -0.13857109526572012;
		p[6] = 9.9843695780195716e-6;
		p[7] = 1.5056327351493116e-7;

		if(z < 0.5){
			y = PI/(Sin(PI*z)*LanczosApproximation(1d - z));
		}else{
			z = z - 1d;
			x = 0.99999999999980993;
			for(i = 0d; i < p.Length; i = i + 1d){
				x = x + p[(int)(i)]/(z + i + 1d);
			}
			t = z + p.Length - 0.5;
			y = Sqrt(2d*PI)*Pow(t, z + 0.5)*Exp(-t)*x;
		}

		return y;
	}


	public static double Beta(double x, double y){
		return Gamma(x)*Gamma(y)/Gamma(x + y);
	}


	public static double Sinh(double x){
		return (Exp(x) - Exp(-x))/2d;
	}


	public static double Cosh(double x){
		return (Exp(x) + Exp(-x))/2d;
	}


	public static double Tanh(double x){
		return Sinh(x)/Cosh(x);
	}


	public static double Cot(double x){
		return 1d/Tan(x);
	}


	public static double Sec(double x){
		return 1d/Cos(x);
	}


	public static double Csc(double x){
		return 1d/Sin(x);
	}


	public static double Coth(double x){
		return Cosh(x)/Sinh(x);
	}


	public static double Sech(double x){
		return 1d/Cosh(x);
	}


	public static double Csch(double x){
		return 1d/Sinh(x);
	}


	public static double Error(double x){
		double y, t, tau, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10;

		if(x == 0d){
			y = 0d;
		}else if(x < 0d){
			y = -Error(-x);
		}else{
			c1 = -1.26551223;
			c2 = +1.00002368;
			c3 = +0.37409196;
			c4 = +0.09678418;
			c5 = -0.18628806;
			c6 = +0.27886807;
			c7 = -1.13520398;
			c8 = +1.48851587;
			c9 = -0.82215223;
			c10 = +0.17087277;

			t = 1d/(1d + 0.5*Abs(x));

			tau = t*Exp(-Pow(x, 2d) + c1 + t*(c2 + t*(c3 + t*(c4 + t*(c5 + t*(c6 + t*(c7 + t*(c8 + t*(c9 + t*c10)))))))));

			y = 1d - tau;
		}

		return y;
	}


	public static double ErrorInverse(double x){
		double y, a, t;

		a = (8d*(PI - 3d))/(3d*PI*(4d - PI));

		t = 2d/(PI*a) + Log(1d - Pow(x, 2d))/2d;
		y = Sign(x)*Sqrt(Sqrt(Pow(t, 2d) - Log(1d - Pow(x, 2d))/a) - t);

		return y;
	}


	public static double FallingFactorial(double x, double n){
		double k, y;

		y = 1d;

		for(k = 0d; k <= n - 1d; k = k + 1d){
			y = y*(x - k);
		}

		return y;
	}


	public static double RisingFactorial(double x, double n){
		double k, y;

		y = 1d;

		for(k = 0d; k <= n - 1d; k = k + 1d){
			y = y*(x + k);
		}

		return y;
	}


	public static double Hypergeometric(double a, double b, double c, double z, double maxIterations, double precision){
		double y;

		if(Abs(z) >= 0.5){
			y = Pow(1d - z, -a)*HypergeometricDirect(a, c - b, c, z/(z - 1d), maxIterations, precision);
		}else{
			y = HypergeometricDirect(a, b, c, z, maxIterations, precision);
		}

		return y;
	}


	public static double HypergeometricDirect(double a, double b, double c, double z, double maxIterations, double precision){
		double y, yp, n;
		bool done;

		y = 0d;
		done = false;

		for(n = 0d; n < maxIterations && !done; n = n + 1d){
			yp = RisingFactorial(a, n)*RisingFactorial(b, n)/RisingFactorial(c, n)*Pow(z, n)/Factorial(n);
			if(Abs(yp) < precision){
				done = true;
			}
			y = y + yp;
		}

		return y;
	}


	public static double BernouilliNumber(double n){
		return AkiyamaTanigawaAlgorithm(n);
	}


	public static double AkiyamaTanigawaAlgorithm(double n){
		double m, j, B;
		double [] A;

		A = new double [(int)(n + 1d)];

		for(m = 0d; m <= n; m = m + 1d){
			A[(int)(m)] = 1d/(m + 1d);
			for(j = m; j >= 1d; j = j - 1d){
				A[(int)(j - 1d)] = j*(A[(int)(j - 1d)] - A[(int)(j)]);
			}
		}

		B = A[0];

		delete(A);

		return B;
	}


	public static double [] aStringToNumberArray(char [] stringx){
		double i;
		double [] array;

		array = new double [(int)(stringx.Length)];

		for(i = 0d; i < stringx.Length; i = i + 1d){
			array[(int)(i)] = stringx[(int)(i)];
		}
		return array;
	}


	public static char [] aNumberArrayToString(double [] array){
		double i;
		char [] stringx;

		stringx = new char [(int)(array.Length)];

		for(i = 0d; i < array.Length; i = i + 1d){
			stringx[(int)(i)] = (char)(array[(int)(i)]);
		}
		return stringx;
	}


	public static bool aNumberArraysEqual(double [] a, double [] b){
		bool equal;
		double i;

		equal = true;
		if(a.Length == b.Length){
			for(i = 0d; i < a.Length && equal; i = i + 1d){
				if(a[(int)(i)] != b[(int)(i)]){
					equal = false;
				}
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static bool aBooleanArraysEqual(bool [] a, bool [] b){
		bool equal;
		double i;

		equal = true;
		if(a.Length == b.Length){
			for(i = 0d; i < a.Length && equal; i = i + 1d){
				if(a[(int)(i)] != b[(int)(i)]){
					equal = false;
				}
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static bool aStringsEqual(char [] a, char [] b){
		bool equal;
		double i;

		equal = true;
		if(a.Length == b.Length){
			for(i = 0d; i < a.Length && equal; i = i + 1d){
				if(a[(int)(i)] != b[(int)(i)]){
					equal = false;
				}
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static void aFillNumberArray(double [] a, double value){
		double i;

		for(i = 0d; i < a.Length; i = i + 1d){
			a[(int)(i)] = value;
		}
	}


	public static void aFillString(char [] a, char value){
		double i;

		for(i = 0d; i < a.Length; i = i + 1d){
			a[(int)(i)] = value;
		}
	}


	public static void aFillBooleanArray(bool [] a, bool value){
		double i;

		for(i = 0d; i < a.Length; i = i + 1d){
			a[(int)(i)] = value;
		}
	}


	public static bool aFillNumberArrayRange(double [] a, double value, double from, double to){
		double i, length;
		bool success;

		if(from >= 0d && from <= a.Length && to >= 0d && to <= a.Length && from <= to){
			length = to - from;
			for(i = 0d; i < length; i = i + 1d){
				a[(int)(from + i)] = value;
			}

			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static bool aFillBooleanArrayRange(bool [] a, bool value, double from, double to){
		double i, length;
		bool success;

		if(from >= 0d && from <= a.Length && to >= 0d && to <= a.Length && from <= to){
			length = to - from;
			for(i = 0d; i < length; i = i + 1d){
				a[(int)(from + i)] = value;
			}

			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static bool aFillStringRange(char [] a, char value, double from, double to){
		double i, length;
		bool success;

		if(from >= 0d && from <= a.Length && to >= 0d && to <= a.Length && from <= to){
			length = to - from;
			for(i = 0d; i < length; i = i + 1d){
				a[(int)(from + i)] = value;
			}

			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static double [] aCopyNumberArray(double [] a){
		double i;
		double [] n;

		n = new double [(int)(a.Length)];

		for(i = 0d; i < a.Length; i = i + 1d){
			n[(int)(i)] = a[(int)(i)];
		}

		return n;
	}


	public static bool [] aCopyBooleanArray(bool [] a){
		double i;
		bool [] n;

		n = new bool [(int)(a.Length)];

		for(i = 0d; i < a.Length; i = i + 1d){
			n[(int)(i)] = a[(int)(i)];
		}

		return n;
	}


	public static char [] aCopyString(char [] a){
		double i;
		char [] n;

		n = new char [(int)(a.Length)];

		for(i = 0d; i < a.Length; i = i + 1d){
			n[(int)(i)] = a[(int)(i)];
		}

		return n;
	}


	public static bool aCopyNumberArrayRange(double [] a, double from, double to, NumberArrayReference copyReference){
		double i, length;
		double [] n;
		bool success;

		if(from >= 0d && from <= a.Length && to >= 0d && to <= a.Length && from <= to){
			length = to - from;
			n = new double [(int)(length)];

			for(i = 0d; i < length; i = i + 1d){
				n[(int)(i)] = a[(int)(from + i)];
			}

			copyReference.numberArray = n;
			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static bool aCopyBooleanArrayRange(bool [] a, double from, double to, BooleanArrayReference copyReference){
		double i, length;
		bool [] n;
		bool success;

		if(from >= 0d && from <= a.Length && to >= 0d && to <= a.Length && from <= to){
			length = to - from;
			n = new bool [(int)(length)];

			for(i = 0d; i < length; i = i + 1d){
				n[(int)(i)] = a[(int)(from + i)];
			}

			copyReference.booleanArray = n;
			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static bool aCopyStringRange(char [] a, double from, double to, StringReference copyReference){
		double i, length;
		char [] n;
		bool success;

		if(from >= 0d && from <= a.Length && to >= 0d && to <= a.Length && from <= to){
			length = to - from;
			n = new char [(int)(length)];

			for(i = 0d; i < length; i = i + 1d){
				n[(int)(i)] = a[(int)(from + i)];
			}

			copyReference.stringx = n;
			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static bool aIsLastElement(double length, double index){
		return index + 1d == length;
	}


	public static double [] aCreateNumberArray(double length, double value){
		double [] array;

		array = new double [(int)(length)];
		aFillNumberArray(array, value);

		return array;
	}


	public static bool [] aCreateBooleanArray(double length, bool value){
		bool [] array;

		array = new bool [(int)(length)];
		aFillBooleanArray(array, value);

		return array;
	}


	public static char [] aCreateString(double length, char value){
		char [] array;

		array = new char [(int)(length)];
		aFillString(array, value);

		return array;
	}


	public static void aSwapElementsOfNumberArray(double [] A, double ai, double bi){
		double tmp;

		tmp = A[(int)(ai)];
		A[(int)(ai)] = A[(int)(bi)];
		A[(int)(bi)] = tmp;
	}


	public static void aSwapElementsOfStringArray(StringArrayReference A, double ai, double bi){
		StringReference tmp;

		tmp = A.stringArray[(int)(ai)];
		A.stringArray[(int)(ai)] = A.stringArray[(int)(bi)];
		A.stringArray[(int)(bi)] = tmp;
	}


	public static void aReverseNumberArray(double [] array){
		double i;

		for(i = 0d; i < array.Length/2d; i = i + 1d){
			aSwapElementsOfNumberArray(array, i, array.Length - i - 1d);
		}
	}


	public static BooleanReference CreateBooleanReference(bool value){
		BooleanReference refx;

		refx = new BooleanReference();
		refx.booleanValue = value;

		return refx;
	}


	public static BooleanArrayReference CreateBooleanArrayReference(bool [] value){
		BooleanArrayReference refx;

		refx = new BooleanArrayReference();
		refx.booleanArray = value;

		return refx;
	}


	public static BooleanArrayReference CreateBooleanArrayReferenceLengthValue(double length, bool value){
		BooleanArrayReference refx;
		double i;

		refx = new BooleanArrayReference();
		refx.booleanArray = new bool [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			refx.booleanArray[(int)(i)] = value;
		}

		return refx;
	}


	public static void FreeBooleanArrayReference(BooleanArrayReference booleanArrayReference){
		delete(booleanArrayReference.booleanArray);
		delete(booleanArrayReference);
	}


	public static CharacterReference CreateCharacterReference(char value){
		CharacterReference refx;

		refx = new CharacterReference();
		refx.characterValue = value;

		return refx;
	}


	public static NumberReference CreateNumberReference(double value){
		NumberReference refx;

		refx = new NumberReference();
		refx.numberValue = value;

		return refx;
	}


	public static NumberArrayReference CreateNumberArrayReference(double [] value){
		NumberArrayReference refx;

		refx = new NumberArrayReference();
		refx.numberArray = value;

		return refx;
	}


	public static NumberArrayReference CreateNumberArrayReferenceLengthValue(double length, double value){
		NumberArrayReference refx;
		double i;

		refx = new NumberArrayReference();
		refx.numberArray = new double [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			refx.numberArray[(int)(i)] = value;
		}

		return refx;
	}


	public static void FreeNumberArrayReference(NumberArrayReference numberArrayReference){
		delete(numberArrayReference.numberArray);
		delete(numberArrayReference);
	}


	public static StringReference CreateStringReference(char [] value){
		StringReference refx;

		refx = new StringReference();
		refx.stringx = value;

		return refx;
	}


	public static StringReference CreateStringReferenceLengthValue(double length, char value){
		StringReference refx;
		double i;

		refx = new StringReference();
		refx.stringx = new char [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			refx.stringx[(int)(i)] = value;
		}

		return refx;
	}


	public static void FreeStringReference(StringReference stringReference){
		delete(stringReference.stringx);
		delete(stringReference);
	}


	public static StringArrayReference CreateStringArrayReference(StringReference [] strings){
		StringArrayReference refx;

		refx = new StringArrayReference();
		refx.stringArray = strings;

		return refx;
	}


	public static StringArrayReference CreateStringArrayReferenceLengthValue(double length, char [] value){
		StringArrayReference refx;
		double i;

		refx = new StringArrayReference();
		refx.stringArray = new StringReference [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			refx.stringArray[(int)(i)] = CreateStringReference(value);
		}

		return refx;
	}


	public static void FreeStringArrayReference(StringArrayReference stringArrayReference){
		double i;

		for(i = 0d; i < stringArrayReference.stringArray.Length; i = i + 1d){
			delete(stringArrayReference.stringArray[(int)(i)]);
		}
		delete(stringArrayReference.stringArray);
		delete(stringArrayReference);
	}


	public static char [] DigitDataBase16(){
		return "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe891412108153069c4ffffffffffffffffffffffffffffffffffffffff9409000000000000000049e7ffffffffffffffffffffffffffffffffff61000000000000000000000017ddffffffffffffffffffffffffffffff840000000573d3f5e5a62b00000028f0ffffffffffffffffffffffffffda04000008bcfffffffffff44200000073ffffffffffffffffffffffffff5700000088ffffffffffffffe812000008e3ffffffffffffffffffffffea02000015f9ffffffffffffffff8100000080ffffffffffffffffffffff9c00000072ffffffffffffffffffe40100002fffffffffffffffffffffff51000000b8ffffffffffffffffffff2a000000e2ffffffffffffffffffff21000001f0ffffffffffffffffffff65000000b3fffffffffffffffffff602000018ffffffffffffffffffffff8b0000008affffffffffffffffffd200000036ffffffffffffffffffffffa900000063ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffb500000057ffffffffffffffffffffffc900000046ffffffffffffffffffa90000005fffffffffffffffffffffffd20000003affffffffffffffffffa900000060ffffffffffffffffffffffd30000003affffffffffffffffffb400000057ffffffffffffffffffffffca00000046ffffffffffffffffffc00000004effffffffffffffffffffffc100000052ffffffffffffffffffd100000037ffffffffffffffffffffffa900000063fffffffffffffffffff602000019ffffffffffffffffffffff8b00000089ffffffffffffffffffff21000001f1ffffffffffffffffffff66000000b3ffffffffffffffffffff50000000b8ffffffffffffffffffff2a000000e1ffffffffffffffffffff9c00000073ffffffffffffffffffe40100002fffffffffffffffffffffffea02000015f9ffffffffffffffff8200000080ffffffffffffffffffffffff5700000088ffffffffffffffe812000008e2ffffffffffffffffffffffffda04000008bcfffffffffff44300000073ffffffffffffffffffffffffffff830000000674d3f6e6a72b00000028f0ffffffffffffffffffffffffffffff60000000000000000000000016ddfffffffffffffffffffffffffffffffffe9309000000000000000048e6ffffffffffffffffffffffffffffffffffffffe88f3f1f07132e68c3fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9d7b28e69441f02000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6300000000000000000000afffffffffffffffffffffffffffffffffffff6a274c7095b9de64000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000afffffffffffffffffffffffffffffffffffffffffffffffffff67000000affffffffffffffffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bfffffffffffffffffffffffff7000000000000000000000000000000003bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffd48b56271005142a5ea0f6ffffffffffffffffffffffffffffffffdb7c20000000000000000000001392feffffffffffffffffffffffffffff1f00000000000000000000000000004cf9ffffffffffffffffffffffffff1f0000003784c7e7f9e8b1480000000056ffffffffffffffffffffffffff1f015accffffffffffffffff9701000000b0ffffffffffffffffffffffff58caffffffffffffffffffffff770000003cfffffffffffffffffffffffffffffffffffffffffffffffffff107000002edffffffffffffffffffffffffffffffffffffffffffffffffff3a000000ccffffffffffffffffffffffffffffffffffffffffffffffffff4c000000baffffffffffffffffffffffffffffffffffffffffffffffffff32000000cbffffffffffffffffffffffffffffffffffffffffffffffffec05000002edffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffeb140000009affffffffffffffffffffffffffffffffffffffffffffffff520000002afbffffffffffffffffffffffffffffffffffffffffffffff8c00000003c7ffffffffffffffffffffffffffffffffffffffffffffffb30300000085ffffffffffffffffffffffffffffffffffffffffffffffc50a0000005dfeffffffffffffffffffffffffffffffffffffffffffffd2110000004efbffffffffffffffffffffffffffffffffffffffffffffdb1800000042f8ffffffffffffffffffffffffffffffffffffffffffffe21f00000039f3ffffffffffffffffffffffffffffffffffffffffffffe92600000030efffffffffffffffffffffffffffffffffffffffffffffee2e00000029eafffffffffffffffffffffffffffffffffffffffffffff33700000022e5fffffffffffffffffffffffffffffffffffffffffffff7410000001cdffffffffffffffffffffffffffffffffffffffffffffffb4c00000017d9fffffffffffffffffffffffffffffffffffffffffffffd5900000012d2ffffffffffffffffffffffffffffffffffffffffffffff680000000ecbffffffffffffffffffffffffffffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffef0000000000000000000000000000000000008bffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe2af8058392817060a1a3f74c8ffffffffffffffffffffffffffffffffeb0000000000000000000000000036cfffffffffffffffffffffffffffffeb000000000000000000000000000004a7ffffffffffffffffffffffffffeb00000f5a9dd0edfbf0ca841900000003c2ffffffffffffffffffffffffec3da8f9fffffffffffffffff0410000002bffffffffffffffffffffffffffffffffffffffffffffffffffee12000000cbffffffffffffffffffffffffffffffffffffffffffffffffff6900000090ffffffffffffffffffffffffffffffffffffffffffffffffff9600000078ffffffffffffffffffffffffffffffffffffffffffffffffff9a0000007effffffffffffffffffffffffffffffffffffffffffffffffff73000000a5fffffffffffffffffffffffffffffffffffffffffffffffff51b000009edfffffffffffffffffffffffffffffffffffffffffffffff7540000007efffffffffffffffffffffffffffffffffffffffffff3d3912400000055fcffffffffffffffffffffffffffffffffff1700000000000000001692feffffffffffffffffffffffffffffffffffff17000000000000002db8feffffffffffffffffffffffffffffffffffffff170000000000000000002bc3fffffffffffffffffffffffffffffffffffffffffffdf0cf922e00000003a5fffffffffffffffffffffffffffffffffffffffffffffffffd8700000007d1ffffffffffffffffffffffffffffffffffffffffffffffffff780000004ffffffffffffffffffffffffffffffffffffffffffffffffffff308000006f6ffffffffffffffffffffffffffffffffffffffffffffffffff3c000000d0ffffffffffffffffffffffffffffffffffffffffffffffffff4d000000c6ffffffffffffffffffffffffffffffffffffffffffffffffff35000000ddffffffffffffffffffffffffffffffffffffffffffffffffea0300000bf9ffffffffffffffffffffffffffffffffffffffffffffffff6200000054ffffffffffffffffffffff47bafefffffffffffffffffff56b00000002cbffffffffffffffffffffff0b001e71a9d7edfbf6e4ba771a000000007cffffffffffffffffffffffff0b0000000000000000000000000000017dffffffffffffffffffffffffff0b000000000000000000000000003cc8ffffffffffffffffffffffffffffe9b989593827160608162a5689dbffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffbd0100000000f3fffffffffffffffffffffffffffffffffffffffffffff3200000000000f3ffffffffffffffffffffffffffffffffffffffffffff69000000000000f3ffffffffffffffffffffffffffffffffffffffffffbf01000b0e000000f3fffffffffffffffffffffffffffffffffffffffff42100008e1f000000f3ffffffffffffffffffffffffffffffffffffffff6a000035fc1f000000f3ffffffffffffffffffffffffffffffffffffffc0010004d1ff1f000000f3fffffffffffffffffffffffffffffffffffff42200007affff1f000000f3ffffffffffffffffffffffffffffffffffff6c000026f7ffff1f000000f3ffffffffffffffffffffffffffffffffffc1010001c1ffffff1f000000f3fffffffffffffffffffffffffffffffff523000066ffffffff1f000000f3ffffffffffffffffffffffffffffffff6d000019f0ffffffff1f000000f3ffffffffffffffffffffffffffffffc2010000aeffffffffff1f000000f3fffffffffffffffffffffffffffff524000052ffffffffffff1f000000f3ffffffffffffffffffffffffffff6e00000fe6ffffffffffff1f000000f3ffffffffffffffffffffffffffc30200009affffffffffffff1f000000f3fffffffffffffffffffffffff62400003ffeffffffffffffff1f000000f3ffffffffffffffffffffffff70000008daffffffffffffffff1f000000f3fffffffffffffffffffffff602000086ffffffffffffffffff1f000000f3fffffffffffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbfffffffffffffff3000000000000000000000000000000000000000000cbffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffff1f000000f3ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000000000000000000000000002fffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f00000fffffffffffffffffffffffffffffffffffffffffffffffffffff4f000008672f120514275997efffffffffffffffffffffffffffffffffff4f00000000000000000000000b73f6ffffffffffffffffffffffffffffff4f000000000000000000000000002bdeffffffffffffffffffffffffffff60538cbad2e7faf0d599370000000025ebffffffffffffffffffffffffffffffffffffffffffffffffa0090000005bffffffffffffffffffffffffffffffffffffffffffffffffffb100000001d2ffffffffffffffffffffffffffffffffffffffffffffffffff560000007effffffffffffffffffffffffffffffffffffffffffffffffffb80000003dffffffffffffffffffffffffffffffffffffffffffffffffffec00000022fffffffffffffffffffffffffffffffffffffffffffffffffffd00000011ffffffffffffffffffffffffffffffffffffffffffffffffffec00000022ffffffffffffffffffffffffffffffffffffffffffffffffffb80000003cffffffffffffffffffffffffffffffffffffffffffffffffff580000007dffffffffffffffffffffffffffffffffffffffffffffffffb301000000cfffffffffffffffffffffff4cb1fdffffffffffffffffffa40a00000058ffffffffffffffffffffffff17001a6ea9d7eefbf2d69b380000000024e8ffffffffffffffffffffffff1700000000000000000000000000002de0ffffffffffffffffffffffffff17000000000000000000000000127ef9ffffffffffffffffffffffffffffebba8a59372615050a1a3569a6f7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffca753915050d233866a3e0ffffffffffffffffffffffffffffffffffd13f0000000000000000000000f7ffffffffffffffffffffffffffffff9d07000000000000000000000000f7ffffffffffffffffffffffffffff9700000000469fdbf3f5da9e490100f7ffffffffffffffffffffffffffca0300000eb3ffffffffffffffffd84df8fffffffffffffffffffffffffa2d000007c8ffffffffffffffffffffffffffffffffffffffffffffffff9100000081ffffffffffffffffffffffffffffffffffffffffffffffffff28000010f6ffffffffffffffffffffffffffffffffffffffffffffffffc20000006affffffffffffffffffffffffffffffffffffffffffffffffff79000000b2ffffffffffffffffffffffffffffffffffffffffffffffffff43000000ebffeb903d1a0616306fc0ffffffffffffffffffffffffffffff0f000015ffa211000000000000000041dcfffffffffffffffffffffffff30000003087000000000000000000000013c6ffffffffffffffffffffffe30000000f00000055beeef7d8881000000017e6ffffffffffffffffffffd30000000000019dffffffffffffe12200000056ffffffffffffffffffffd100000000006effffffffffffffffce04000002dbffffffffffffffffffdd0000000006eaffffffffffffffffff550000008bffffffffffffffffffe90000000043ffffffffffffffffffffa90000004dfffffffffffffffffff80200000074ffffffffffffffffffffdb0000002cffffffffffffffffffff2200000088ffffffffffffffffffffef00000019ffffffffffffffffffff4d00000088ffffffffffffffffffffee0000001affffffffffffffffffff7e00000074ffffffffffffffffffffdb0000002dffffffffffffffffffffcd00000042ffffffffffffffffffffa900000052ffffffffffffffffffffff21000005e9ffffffffffffffffff5400000093ffffffffffffffffffffff8f0000006dffffffffffffffffcd04000007e6fffffffffffffffffffffff9220000019effffffffffffe1230000006cffffffffffffffffffffffffffc00600000056beeff8d888110000002af3ffffffffffffffffffffffffffffa603000000000000000000000026ddffffffffffffffffffffffffffffffffc8280000000000000000025deffffffffffffffffffffffffffffffffffffffab25a2a1106193b7ed7ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff47000000000000000000000000000000000000f7ffffffffffffffffffff47000000000000000000000000000000000003faffffffffffffffffffff4700000000000000000000000000000000004afffffffffffffffffffffffffffffffffffffffffffffffffc1a000000adffffffffffffffffffffffffffffffffffffffffffffffffb300000015faffffffffffffffffffffffffffffffffffffffffffffffff5100000073ffffffffffffffffffffffffffffffffffffffffffffffffea05000000d6ffffffffffffffffffffffffffffffffffffffffffffffff8d00000039ffffffffffffffffffffffffffffffffffffffffffffffffff2c0000009dffffffffffffffffffffffffffffffffffffffffffffffffc90000000cf3ffffffffffffffffffffffffffffffffffffffffffffffff6700000063fffffffffffffffffffffffffffffffffffffffffffffffff60f000000c6ffffffffffffffffffffffffffffffffffffffffffffffffa300000029ffffffffffffffffffffffffffffffffffffffffffffffffff410000008cffffffffffffffffffffffffffffffffffffffffffffffffdf01000005e9ffffffffffffffffffffffffffffffffffffffffffffffff7d00000052fffffffffffffffffffffffffffffffffffffffffffffffffd1e000000b5ffffffffffffffffffffffffffffffffffffffffffffffffb90000001bfcffffffffffffffffffffffffffffffffffffffffffffffff570000007bffffffffffffffffffffffffffffffffffffffffffffffffee07000001ddffffffffffffffffffffffffffffffffffffffffffffffff9300000042ffffffffffffffffffffffffffffffffffffffffffffffffff31000000a5ffffffffffffffffffffffffffffffffffffffffffffffffd000000010f7ffffffffffffffffffffffffffffffffffffffffffffffff6d0000006bfffffffffffffffffffffffffffffffffffffffffffffffff913000000ceffffffffffffffffffffffffffffffffffffffffffffffffa900000031ffffffffffffffffffffffffffffffffffffffffffffffffff4700000094ffffffffffffffffffffffffffffffffffffffffffffffffe302000008eeffffffffffffffffffffffffffffffffffffffffffffffff840000005afffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff9a8602c13050c1d4882dfffffffffffffffffffffffffffffffffffffa918000000000000000000025eeeffffffffffffffffffffffffffffff780000000000000000000000000023e5ffffffffffffffffffffffffff9f0000000037a8e4faf1c66d0500000033fdfffffffffffffffffffffff81600000065fdffffffffffffc40a0000009fffffffffffffffffffffffb600000021faffffffffffffffff8d00000047ffffffffffffffffffffff820000007bffffffffffffffffffeb01000014ffffffffffffffffffffff6d000000a2ffffffffffffffffffff15000001fdffffffffffffffffffff76000000a2ffffffffffffffffffff14000007ffffffffffffffffffffffa10000007bffffffffffffffffffec01000033ffffffffffffffffffffffec08000022fbffffffffffffffff8e00000087ffffffffffffffffffffffff7d00000068fdffffffffffffc70b00001ef2fffffffffffffffffffffffffb5500000039aae5fbf2c87006000013d0fffffffffffffffffffffffffffffe93160000000000000000000153e3ffffffffffffffffffffffffffffffffffbd2e000000000000000780f0ffffffffffffffffffffffffffffffffce3500000000000000000000000e87fcffffffffffffffffffffffffffb3060000004fb2e6faf0cd82150000004ffaffffffffffffffffffffffda0b000004a9ffffffffffffffe93600000076ffffffffffffffffffffff5600000084ffffffffffffffffffe80e000005e2fffffffffffffffffff606000008f4ffffffffffffffffffff6f0000008dffffffffffffffffffcb00000039ffffffffffffffffffffffac0000005cffffffffffffffffffbc0000004affffffffffffffffffffffbe0000004dffffffffffffffffffcc00000039ffffffffffffffffffffffac0000005effffffffffffffffffea00000008f4ffffffffffffffffffff6e0000007cffffffffffffffffffff2f00000085ffffffffffffffffffe70d000000c1ffffffffffffffffffff9300000004a9ffffffffffffffe83400000028fcfffffffffffffffffffffa2d0000000050b2e7fbf2cd821400000002b8ffffffffffffffffffffffffe523000000000000000000000000000299fffffffffffffffffffffffffffff16605000000000000000000002cc5ffffffffffffffffffffffffffffffffffe88e542512040b1b3d72c1fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff8a259251008203f8be2ffffffffffffffffffffffffffffffffffffffa91d0000000000000000047ffaffffffffffffffffffffffffffffffff7b00000000000000000000000040f8ffffffffffffffffffffffffffff94000000004db9ecf7da8b1300000057ffffffffffffffffffffffffffdc050000008fffffffffffffe527000000acffffffffffffffffffffffff630000005fffffffffffffffffd406000025fbfffffffffffffffffffffb0c000002e0ffffffffffffffffff5f000000b2ffffffffffffffffffffc600000036ffffffffffffffffffffb50000005fffffffffffffffffffffa000000068ffffffffffffffffffffe700000011feffffffffffffffffff8d0000007cfffffffffffffffffffffb00000000dfffffffffffffffffff8c0000007cfffffffffffffffffffffb00000000b4ffffffffffffffffff9e00000069ffffffffffffffffffffe7000000008dffffffffffffffffffbe00000038ffffffffffffffffffffb6000000007bfffffffffffffffffff606000003e2ffffffffffffffffff62000000006fffffffffffffffffffff4f00000064ffffffffffffffffd8080000000062ffffffffffffffffffffc50000000096ffffffffffffe82b000000000064ffffffffffffffffffffff6c0000000051bbeff8dc8e1500001000000074fffffffffffffffffffffff94f0000000000000000000000288c00000084fffffffffffffffffffffffffd810b000000000000000052ea830000009fffffffffffffffffffffffffffffea8d471d090d2864c1ffff5b000000d4ffffffffffffffffffffffffffffffffffffffffffffffffff2100000dfdffffffffffffffffffffffffffffffffffffffffffffffffd900000052ffffffffffffffffffffffffffffffffffffffffffffffffff75000000b8ffffffffffffffffffffffffffffffffffffffffffffffffe30d000023fefffffffffffffffffffffffffffffffffffffffffffffff945000000b7ffffffffffffffffffffffffff7fa2fdffffffffffffffe8480000005effffffffffffffffffffffffffff63002080c4ecfae7c0740e00000034f4ffffffffffffffffffffffffffff6300000000000000000000000043f0ffffffffffffffffffffffffffffff6300000000000000000000118efdfffffffffffffffffffffffffffffffff4bb7f462b15040b25569ff4ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff".ToCharArray();
	}


	public static void DrawDigitCharacter(RGBABitmapImage image, double topx, double topy, double digit){
		double x, y;
		char [] allCharData, colorChars;
		NumberReference colorReference;
		StringReference errorMessage;
		RGBA color;

		colorReference = new NumberReference();
		errorMessage = new StringReference();
		color = new RGBA();

		colorChars = new char [2];

		allCharData = DigitDataBase16();

		for(y = 0d; y < 37d; y = y + 1d){
			for(x = 0d; x < 30d; x = x + 1d){
				colorChars[0] = allCharData[(int)(digit*30d*37d*2d + y*2d*30d + x*2d + 0d)];
				colorChars[1] = allCharData[(int)(digit*30d*37d*2d + y*2d*30d + x*2d + 1d)];

				ToUpperCase(colorChars);
				CreateNumberFromStringWithCheck(colorChars, 16d, colorReference, errorMessage);
				color.r = colorReference.numberValue/255d;
				color.g = colorReference.numberValue/255d;
				color.b = colorReference.numberValue/255d;
				color.a = 1d;
				SetPixel(image, topx + x, topy + y, color);
			}
		}
	}


	public static char [] GetPixelFontData(){
		return "0000000000000000000000000000001818000018181818181818000000000000000000363636360000006666ff6666ff666600000000187eff1b1f7ef8d8ff7e1800000e1bdb6e30180c76dbd87000007fc6cfd87070d8cccc6c38000000000000000000181c0c0e00000c1830303030303030180c000030180c0c0c0c0c0c0c183000000000995a3cff3c5a990000000000181818ffff1818180000000030181c1c00000000000000000000000000ffff000000000000000038380000000000000000006060303018180c0c0606030300003c66c3e3f3dbcfc7c3663c00007e181818181818187838180000ffc0c06030180c0603e77e00007ee70303077e070303e77e00000c0c0c0c0cffcc6c3c1c0c00007ee7030307fec0c0c0c0ff00007ee7c3c3c7fec0c0c0e77e000030303030180c06030303ff00007ee7c3c3e77ee7c3c3e77e00007ee70303037fe7c3c3e77e00000038380000383800000000000030181c1c00001c1c0000000000060c183060c06030180c0600000000ffff00ffff0000000000006030180c0603060c183060000018000018180c0603c3c37e00003f60cfdbd3ddc37e0000000000c3c3c3c3ffc3c3c3663c180000fec7c3c3c7fec7c3c3c7fe00007ee7c0c0c0c0c0c0c0e77e0000fccec7c3c3c3c3c3c7cefc0000ffc0c0c0c0fcc0c0c0c0ff0000c0c0c0c0c0c0fcc0c0c0ff00007ee7c3c3cfc0c0c0c0e77e0000c3c3c3c3c3ffc3c3c3c3c300007e1818181818181818187e00007ceec606060606060606060000c3c6ccd8f0e0f0d8ccc6c30000ffc0c0c0c0c0c0c0c0c0c00000c3c3c3c3c3c3dbffffe7c30000c7c7cfcfdfdbfbf3f3e3e300007ee7c3c3c3c3c3c3c3e77e0000c0c0c0c0c0fec7c3c3c7fe00003f6edfdbc3c3c3c3c3663c0000c3c6ccd8f0fec7c3c3c7fe00007ee70303077ee0c0c0e77e000018181818181818181818ff00007ee7c3c3c3c3c3c3c3c3c30000183c3c6666c3c3c3c3c3c30000c3e7ffffdbdbc3c3c3c3c30000c366663c3c183c3c6666c300001818181818183c3c6666c30000ffc0c060307e0c060303ff00003c3030303030303030303c00030306060c0c18183030606000003c0c0c0c0c0c0c0c0c0c3c000000000000000000c3663c18ffff00000000000000000000000000000000000000001838307000007fc3c37f03c37e000000000000fec3c3c3c3fec0c0c0c0c000007ec3c0c0c0c37e0000000000007fc3c3c3c37f030303030300007fc0c0fec3c37e0000000000003030303030fc303030331e7ec303037fc3c3c37e000000000000c3c3c3c3c3c3fec0c0c0c000001818181818181800001800386c0c0c0c0c0c0c0c00000c000000c6ccf8f0d8ccc6c0c0c0c000007e181818181818181818780000dbdbdbdbdbdbfe000000000000c6c6c6c6c6c6fc0000000000007cc6c6c6c6c67c00000000c0c0c0fec3c3c3c3fe000000000303037fc3c3c3c37f000000000000c0c0c0c0c0e0fe000000000000fe03037ec0c07f0000000000001c3630303030fc3030300000007ec6c6c6c6c6c6000000000000183c3c6666c3c3000000000000c3e7ffdbc3c3c3000000000000c3663c183c66c300000000c0606030183c6666c3000000000000ff6030180c06ff0000000000000f18181838f0381818180f181818181818181818181818180000f01818181c0f1c181818f0000000000000068ff160000000".ToCharArray();
	}


	public static void DrawAsciiCharacter(RGBABitmapImage image, double topx, double topy, char a, RGBA color){
		double index, x, y, row, pixel;
		char [] allCharData, charData, rowData;
		NumberReference rowReference;
		StringReference errorMessage;

		rowReference = new NumberReference();
		errorMessage = new StringReference();

		index = a;
		index = index - 32d;
		allCharData = GetPixelFontData();
		charData = Substring(allCharData, index*2d*13d, (index + 1d)*2d*13d);

		for(y = 0d; y < 13d; y = y + 1d){
			rowData = Substring(charData, y*2d, (y + 1d)*2d);
			ToUpperCase(rowData);
			CreateNumberFromStringWithCheck(rowData, 16d, rowReference, errorMessage);
			row = rowReference.numberValue;
			for(x = 0d; x < 8d; x = x + 1d){
				pixel = Floor(row/Pow(2d, x))%2d;
				if(pixel == 1d){
					DrawPixel(image, topx + 8d - 1d - x, topy + 13d - 1d - y, color);
				}
			}
		}
	}


	public static double GetTextWidth(char [] text){
		double charWidth, spacing, width;

		charWidth = 8d;
		spacing = 2d;

		if(text.Length == 0d){
			width = 0d;
		}else{
			width = text.Length*charWidth + (text.Length - 1d)*spacing;
		}

		return width;
	}


	public static double GetTextHeight(char [] text){
		return 13d;
	}


	public static void AssertFalse(bool b, NumberReference failures){
		if(b){
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertTrue(bool b, NumberReference failures){
		if(!b){
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertEquals(double a, double b, NumberReference failures){
		if(a != b){
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertBooleansEqual(bool a, bool b, NumberReference failures){
		if(a != b){
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertCharactersEqual(char a, char b, NumberReference failures){
		if(a != b){
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertStringEquals(char [] a, char [] b, NumberReference failures){
		if(!aStringsEqual(a, b)){
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertNumberArraysEqual(double [] a, double [] b, NumberReference failures){
		double i;

		if(a.Length == b.Length){
			for(i = 0d; i < a.Length; i = i + 1d){
				AssertEquals(a[(int)(i)], b[(int)(i)], failures);
			}
		}else{
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertBooleanArraysEqual(bool [] a, bool [] b, NumberReference failures){
		double i;

		if(a.Length == b.Length){
			for(i = 0d; i < a.Length; i = i + 1d){
				AssertBooleansEqual(a[(int)(i)], b[(int)(i)], failures);
			}
		}else{
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static void AssertStringArraysEqual(StringReference [] a, StringReference [] b, NumberReference failures){
		double i;

		if(a.Length == b.Length){
			for(i = 0d; i < a.Length; i = i + 1d){
				AssertStringEquals(a[(int)(i)].stringx, b[(int)(i)].stringx, failures);
			}
		}else{
			failures.numberValue = failures.numberValue + 1d;
		}
	}


	public static double [] ConvertToPNG(RGBABitmapImage image){
		return ConvertToPNGWithOptions(image, 6d, false, 0d, 0.001);
	}


	public static double [] ConvertToPNGGrayscale(RGBABitmapImage image){
		return ConvertToPNGWithOptions(image, 0d, false, 0d, 0.001);
	}


	public static PHYS PysicsHeader(double pixelsPerMeter){
		PHYS phys;

		phys = new PHYS();

		phys.pixelsPerMeter = pixelsPerMeter;

		return phys;
	}


	public static double [] ConvertToPNGWithOptions(RGBABitmapImage image, double colorType, bool setPhys, double pixelsPerMeter, double compressionLevel){
		PNGImage png;
		double [] pngData, colorData;

		png = new PNGImage();

		png.signature = PNGSignature();

		png.ihdr = PNGHeader(image, colorType);

		png.physPresent = setPhys;
		png.phys = PysicsHeader(pixelsPerMeter);

		if(colorType == 6d){
			colorData = GetPNGColorData(image);
		}else{
			colorData = GetPNGColorDataGreyscale(image);
		}
		png.zlibStruct = ZLibCompressStaticHuffman(colorData, compressionLevel);

		pngData = PNGSerializeChunks(png);

		return pngData;
	}


	public static double [] PNGSerializeChunks(PNGImage png){
		double length, i, chunkLength;
		double [] data;
		NumberReference position;

		length = png.signature.Length + 12d + PNGHeaderLength() + 12d + PNGIDATLength(png) + 12d;
		if(png.physPresent){
			length = length + 4d + 4d + 1d + 12d;
		}
		data = new double [(int)(length)];
		position = CreateNumberReference(0d);

		/* Signature*/
		for(i = 0d; i < png.signature.Length; i = i + 1d){
			WriteByte(data, png.signature[(int)(i)], position);
		}

		/* Header*/
		chunkLength = PNGHeaderLength();
		Write4BytesBE(data, chunkLength, position);
		WriteStringBytes(data, "IHDR".ToCharArray(), position);
		Write4BytesBE(data, png.ihdr.Width, position);
		Write4BytesBE(data, png.ihdr.Height, position);
		WriteByte(data, png.ihdr.BitDepth, position);
		WriteByte(data, png.ihdr.ColourType, position);
		WriteByte(data, png.ihdr.CompressionMethod, position);
		WriteByte(data, png.ihdr.FilterMethod, position);
		WriteByte(data, png.ihdr.InterlaceMethod, position);
		Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4d, chunkLength + 4d), position);

		/* pHYs*/
		if(png.physPresent){
			chunkLength = 4d + 4d + 1d;
			Write4BytesBE(data, chunkLength, position);
			WriteStringBytes(data, "pHYs".ToCharArray(), position);

			Write4BytesBE(data, png.phys.pixelsPerMeter, position);
			Write4BytesBE(data, png.phys.pixelsPerMeter, position);
			WriteByte(data, 1d, position);
			/* 1 = pixels per meter*/
			Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4d, chunkLength + 4d), position);
		}

		/* IDAT*/
		chunkLength = PNGIDATLength(png);
		Write4BytesBE(data, chunkLength, position);
		WriteStringBytes(data, "IDAT".ToCharArray(), position);
		WriteByte(data, png.zlibStruct.CMF, position);
		WriteByte(data, png.zlibStruct.FLG, position);
		for(i = 0d; i < png.zlibStruct.CompressedDataBlocks.Length; i = i + 1d){
			WriteByte(data, png.zlibStruct.CompressedDataBlocks[(int)(i)], position);
		}
		Write4BytesBE(data, png.zlibStruct.Adler32CheckValue, position);
		Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - chunkLength - 4d, chunkLength + 4d), position);

		/* IEND*/
		chunkLength = 0d;
		Write4BytesBE(data, chunkLength, position);
		WriteStringBytes(data, "IEND".ToCharArray(), position);
		Write4BytesBE(data, CRC32OfInterval(data, position.numberValue - 4d, 4d), position);

		return data;
	}


	public static double PNGIDATLength(PNGImage png){
		return 2d + png.zlibStruct.CompressedDataBlocks.Length + 4d;
	}


	public static double PNGHeaderLength(){
		return 4d + 4d + 1d + 1d + 1d + 1d + 1d;
	}


	public static double [] GetPNGColorData(RGBABitmapImage image){
		double [] colordata;
		double length, x, y, next;
		RGBA rgba;

		length = 4d*ImageWidth(image)*ImageHeight(image) + ImageHeight(image);

		colordata = new double [(int)(length)];

		next = 0d;

		for(y = 0d; y < ImageHeight(image); y = y + 1d){
			colordata[(int)(next)] = 0d;
			next = next + 1d;
			for(x = 0d; x < ImageWidth(image); x = x + 1d){
				rgba = image.x[(int)(x)].y[(int)(y)];
				colordata[(int)(next)] = Roundx(rgba.r*255d);
				next = next + 1d;
				colordata[(int)(next)] = Roundx(rgba.g*255d);
				next = next + 1d;
				colordata[(int)(next)] = Roundx(rgba.b*255d);
				next = next + 1d;
				colordata[(int)(next)] = Roundx(rgba.a*255d);
				next = next + 1d;
			}
		}

		return colordata;
	}


	public static double [] GetPNGColorDataGreyscale(RGBABitmapImage image){
		double [] colordata;
		double length, x, y, next;
		RGBA rgba;

		length = ImageWidth(image)*ImageHeight(image) + ImageHeight(image);

		colordata = new double [(int)(length)];

		next = 0d;

		for(y = 0d; y < ImageHeight(image); y = y + 1d){
			colordata[(int)(next)] = 0d;
			next = next + 1d;
			for(x = 0d; x < ImageWidth(image); x = x + 1d){
				rgba = image.x[(int)(x)].y[(int)(y)];
				colordata[(int)(next)] = Roundx(rgba.r*255d);
				next = next + 1d;
			}
		}

		return colordata;
	}


	public static IHDR PNGHeader(RGBABitmapImage image, double colortype){
		IHDR ihdr;

		ihdr = new IHDR();
		ihdr.Width = ImageWidth(image);
		ihdr.Height = ImageHeight(image);
		/* Truecolour with alpha*/
		ihdr.BitDepth = 8d;
		ihdr.ColourType = colortype;
		ihdr.FilterMethod = 0d;
		/* None*/
		ihdr.CompressionMethod = 0d;
		/* zlib*/
		ihdr.InterlaceMethod = 0d;
		/* no interlace*/
		return ihdr;
	}


	public static double [] PNGSignature(){
		double [] s;

		s = new double [8];
		s[0] = 137d;
		s[1] = 80d;
		s[2] = 78d;
		s[3] = 71d;
		s[4] = 13d;
		s[5] = 10d;
		s[6] = 26d;
		s[7] = 10d;

		return s;
	}


	public static double [] PNGReadDataChunks(Chunk [] cs){
		double i, j, length, zlibpos;
		Chunk c;
		double [] zlibData;

		length = 0d;
		for(i = 0d; i < cs.Length; i = i + 1d){
			c = cs[(int)(i)];
			if(aStringsEqual(c.type, "IDAT".ToCharArray())){
				length = length + c.length;
			}
		}

		zlibData = new double [(int)(length)];
		zlibpos = 0d;

		for(i = 0d; i < cs.Length; i = i + 1d){
			c = cs[(int)(i)];
			if(aStringsEqual(c.type, "IDAT".ToCharArray())){
				for(j = 0d; j < c.length; j = j + 1d){
					zlibData[(int)(zlibpos)] = c.data[(int)(j)];
					zlibpos = zlibpos + 1d;
				}
			}
		}

		return zlibData;
	}


	public static bool PNGReadHeader(RGBABitmapImage image, Chunk [] cs, StringReference errorMessages){
		double i;
		IHDR ihdr;
		Chunk c;
		NumberReference position;
		RGBABitmapImage n;
		bool success;

		position = CreateNumberReference(0d);
		success = false;

		for(i = 0d; i < cs.Length; i = i + 1d){
			c = cs[(int)(i)];
			if(aStringsEqual(c.type, "IHDR".ToCharArray())){
				ihdr = new IHDR();

				ihdr.Width = Read4bytesBE(c.data, position);
				ihdr.Height = Read4bytesBE(c.data, position);
				ihdr.BitDepth = ReadByte(c.data, position);
				ihdr.ColourType = ReadByte(c.data, position);
				ihdr.CompressionMethod = ReadByte(c.data, position);
				ihdr.FilterMethod = ReadByte(c.data, position);
				ihdr.InterlaceMethod = ReadByte(c.data, position);

				n = CreateImage(ihdr.Width, ihdr.Height, GetTransparent());
				image.x = n.x;

				if(ihdr.ColourType == 6d){
					if(ihdr.BitDepth == 8d){
						if(ihdr.CompressionMethod == 0d){
							if(ihdr.FilterMethod == 0d){
								if(ihdr.InterlaceMethod == 0d){
									success = true;
								}else{
									success = false;
									errorMessages.stringx = AppendString(errorMessages.stringx, "Interlace method not supported.".ToCharArray());
								}
							}else{
								success = false;
								errorMessages.stringx = AppendString(errorMessages.stringx, "Filter method not supported.".ToCharArray());
							}
						}else{
							success = false;
							errorMessages.stringx = AppendString(errorMessages.stringx, "Compression type not supported.".ToCharArray());
						}
					}else{
						success = false;
						errorMessages.stringx = AppendString(errorMessages.stringx, "Bit depth not supported.".ToCharArray());
					}
				}else{
					success = false;
					errorMessages.stringx = AppendString(errorMessages.stringx, "Color type not supported.".ToCharArray());
				}
			}
		}

		return success;
	}


	public static Chunk [] PNGReadChunks(double [] data, NumberReference position){
		bool done;
		double prepos;
		double chunks;
		Chunk c;
		Chunk [] cs;
		double i;
		done = false;
		prepos = position.numberValue;
		for(chunks = 0d; !done; chunks = chunks + 1d){
			c = PNGReadChunk(data, position);
			if(aStringsEqual(c.type, "IEND".ToCharArray())){
				done = true;
			}
		}
		position.numberValue = prepos;
		cs = new Chunk [(int)(chunks)];
		for(i = 0d; i < chunks; i = i + 1d){
			cs[(int)(i)] = PNGReadChunk(data, position);
		}

		return cs;
	}


	public static Chunk PNGReadChunk(double [] data, NumberReference position){
		Chunk c;

		c = new Chunk();

		c.length = Read4bytesBE(data, position);
		c.type = new char [4];
		c.type[0] = (char)(ReadByte(data, position));
		c.type[1] = (char)(ReadByte(data, position));
		c.type[2] = (char)(ReadByte(data, position));
		c.type[3] = (char)(ReadByte(data, position));
		c.data = ReadXbytes(data, position, c.length);
		c.crc = Read4bytesBE(data, position);

		return c;
	}


	public static void WriteStringToStingStream(char [] stream, NumberReference index, char [] src){
		double i;

		for(i = 0d; i < src.Length; i = i + 1d){
			stream[(int)(index.numberValue + i)] = src[(int)(i)];
		}
		index.numberValue = index.numberValue + src.Length;
	}


	public static void WriteCharacterToStingStream(char [] stream, NumberReference index, char src){
		stream[(int)(index.numberValue)] = src;
		index.numberValue = index.numberValue + 1d;
	}


	public static void WriteBooleanToStingStream(char [] stream, NumberReference index, bool src){
		if(src){
			WriteStringToStingStream(stream, index, "true".ToCharArray());
		}else{
			WriteStringToStingStream(stream, index, "false".ToCharArray());
		}
	}


	public static bool SubstringWithCheck(char [] stringx, double from, double to, StringReference stringReference){
		bool success;

		if(from >= 0d && from <= stringx.Length && to >= 0d && to <= stringx.Length && from <= to){
			stringReference.stringx = Substring(stringx, from, to);
			success = true;
		}else{
			success = false;
		}

		return success;
	}


	public static char [] Substring(char [] stringx, double from, double to){
		char [] n;
		double i, length;

		length = to - from;

		n = new char [(int)(length)];

		for(i = from; i < to; i = i + 1d){
			n[(int)(i - from)] = stringx[(int)(i)];
		}

		return n;
	}


	public static char [] AppendString(char [] s1, char [] s2){
		char [] newString;

		newString = ConcatenateString(s1, s2);

		delete(s1);

		return newString;
	}


	public static char [] ConcatenateString(char [] s1, char [] s2){
		char [] newString;
		double i;

		newString = new char [(int)(s1.Length + s2.Length)];

		for(i = 0d; i < s1.Length; i = i + 1d){
			newString[(int)(i)] = s1[(int)(i)];
		}

		for(i = 0d; i < s2.Length; i = i + 1d){
			newString[(int)(s1.Length + i)] = s2[(int)(i)];
		}

		return newString;
	}


	public static char [] AppendCharacter(char [] stringx, char c){
		char [] newString;

		newString = ConcatenateCharacter(stringx, c);

		delete(stringx);

		return newString;
	}


	public static char [] ConcatenateCharacter(char [] stringx, char c){
		char [] newString;
		double i;
		newString = new char [(int)(stringx.Length + 1d)];

		for(i = 0d; i < stringx.Length; i = i + 1d){
			newString[(int)(i)] = stringx[(int)(i)];
		}

		newString[(int)(stringx.Length)] = c;

		return newString;
	}


	public static StringReference [] SplitByCharacter(char [] toSplit, char splitBy){
		StringReference [] split;
		char [] stringToSplitBy;

		stringToSplitBy = new char [1];
		stringToSplitBy[0] = splitBy;

		split = SplitByString(toSplit, stringToSplitBy);

		delete(stringToSplitBy);

		return split;
	}


	public static bool IndexOfCharacter(char [] stringx, char character, NumberReference indexReference){
		double i;
		bool found;

		found = false;
		for(i = 0d; i < stringx.Length && !found; i = i + 1d){
			if(stringx[(int)(i)] == character){
				found = true;
				indexReference.numberValue = i;
			}
		}

		return found;
	}


	public static bool SubstringEqualsWithCheck(char [] stringx, double from, char [] substring, BooleanReference equalsReference){
		bool success;

		if(from < stringx.Length){
			success = true;
			equalsReference.booleanValue = SubstringEquals(stringx, from, substring);
		}else{
			success = false;
		}

		return success;
	}


	public static bool SubstringEquals(char [] stringx, double from, char [] substring){
		double i;
		bool equal;

		equal = true;
		if(stringx.Length - from >= substring.Length){
			for(i = 0d; i < substring.Length && equal; i = i + 1d){
				if(stringx[(int)(from + i)] != substring[(int)(i)]){
					equal = false;
				}
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static bool IndexOfString(char [] stringx, char [] substring, NumberReference indexReference){
		double i;
		bool found;

		found = false;
		for(i = 0d; i < stringx.Length - substring.Length + 1d && !found; i = i + 1d){
			if(SubstringEquals(stringx, i, substring)){
				found = true;
				indexReference.numberValue = i;
			}
		}

		return found;
	}


	public static bool ContainsCharacter(char [] stringx, char character){
		double i;
		bool found;

		found = false;
		for(i = 0d; i < stringx.Length && !found; i = i + 1d){
			if(stringx[(int)(i)] == character){
				found = true;
			}
		}

		return found;
	}


	public static bool ContainsString(char [] stringx, char [] substring){
		return IndexOfString(stringx, substring, new NumberReference());
	}


	public static void ToUpperCase(char [] stringx){
		double i;

		for(i = 0d; i < stringx.Length; i = i + 1d){
			stringx[(int)(i)] = charToUpperCase(stringx[(int)(i)]);
		}
	}


	public static void ToLowerCase(char [] stringx){
		double i;

		for(i = 0d; i < stringx.Length; i = i + 1d){
			stringx[(int)(i)] = charToLowerCase(stringx[(int)(i)]);
		}
	}


	public static bool EqualsIgnoreCase(char [] a, char [] b){
		bool equal;
		double i;

		if(a.Length == b.Length){
			equal = true;
			for(i = 0d; i < a.Length && equal; i = i + 1d){
				if(charToLowerCase(a[(int)(i)]) != charToLowerCase(b[(int)(i)])){
					equal = false;
				}
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static char [] ReplaceString(char [] stringx, char [] toReplace, char [] replaceWith){
		char [] result;
		double i;
		BooleanReference equalsReference;
		bool success;

		equalsReference = new BooleanReference();
		result = new char [0];

		for(i = 0d; i < stringx.Length; ){
			success = SubstringEqualsWithCheck(stringx, i, toReplace, equalsReference);
			if(success){
				success = equalsReference.booleanValue;
			}

			if(success && toReplace.Length > 0d){
				result = ConcatenateString(result, replaceWith);
				i = i + toReplace.Length;
			}else{
				result = ConcatenateCharacter(result, stringx[(int)(i)]);
				i = i + 1d;
			}
		}

		return result;
	}


	public static char [] ReplaceCharacter(char [] stringx, char toReplace, char replaceWith){
		char [] result;
		double i;

		result = new char [0];

		for(i = 0d; i < stringx.Length; i = i + 1d){
			if(stringx[(int)(i)] == toReplace){
				result = ConcatenateCharacter(result, replaceWith);
			}else{
				result = ConcatenateCharacter(result, stringx[(int)(i)]);
			}
		}

		return result;
	}


	public static char [] Trim(char [] stringx){
		char [] result;
		double i, lastWhitespaceLocationStart, lastWhitespaceLocationEnd;
		bool firstNonWhitespaceFound;

		/* Find whitepaces at the start.*/
		lastWhitespaceLocationStart = -1d;
		firstNonWhitespaceFound = false;
		for(i = 0d; i < stringx.Length && !firstNonWhitespaceFound; i = i + 1d){
			if(charIsWhiteSpace(stringx[(int)(i)])){
				lastWhitespaceLocationStart = i;
			}else{
				firstNonWhitespaceFound = true;
			}
		}

		/* Find whitepaces at the end.*/
		lastWhitespaceLocationEnd = stringx.Length;
		firstNonWhitespaceFound = false;
		for(i = stringx.Length - 1d; i >= 0d && !firstNonWhitespaceFound; i = i - 1d){
			if(charIsWhiteSpace(stringx[(int)(i)])){
				lastWhitespaceLocationEnd = i;
			}else{
				firstNonWhitespaceFound = true;
			}
		}

		if(lastWhitespaceLocationStart < lastWhitespaceLocationEnd){
			result = Substring(stringx, lastWhitespaceLocationStart + 1d, lastWhitespaceLocationEnd);
		}else{
			result = new char [0];
		}

		return result;
	}


	public static bool StartsWith(char [] stringx, char [] start){
		bool startsWithString;

		startsWithString = false;
		if(stringx.Length >= start.Length){
			startsWithString = SubstringEquals(stringx, 0d, start);
		}

		return startsWithString;
	}


	public static bool EndsWith(char [] stringx, char [] end){
		bool endsWithString;

		endsWithString = false;
		if(stringx.Length >= end.Length){
			endsWithString = SubstringEquals(stringx, stringx.Length - end.Length, end);
		}

		return endsWithString;
	}


	public static StringReference [] SplitByString(char [] toSplit, char [] splitBy){
		StringReference [] split;
		char [] next;
		double i;
		char c;
		StringReference n;

		split = new StringReference [0];

		next = new char [0];
		for(i = 0d; i < toSplit.Length; ){
			c = toSplit[(int)(i)];

			if(SubstringEquals(toSplit, i, splitBy)){
				n = new StringReference();
				n.stringx = next;
				split = AddString(split, n);
				next = new char [0];
				i = i + splitBy.Length;
			}else{
				next = AppendCharacter(next, c);
				i = i + 1d;
			}
		}

		n = new StringReference();
		n.stringx = next;
		split = AddString(split, n);

		return split;
	}


	public static bool StringIsBefore(char [] a, char [] b){
		bool before, equal, done;
		double i;

		before = false;
		equal = true;
		done = false;

		if(a.Length == 0d && b.Length > 0d){
			before = true;
		}else{
			for(i = 0d; i < a.Length && i < b.Length && !done; i = i + 1d){
				if(a[(int)(i)] != b[(int)(i)]){
					equal = false;
				}
				if(charCharacterIsBefore(a[(int)(i)], b[(int)(i)])){
					before = true;
				}
				if(charCharacterIsBefore(b[(int)(i)], a[(int)(i)])){
					done = true;
				}
			}

			if(equal){
				if(a.Length < b.Length){
					before = true;
				}
			}
		}

		return before;
	}


	public static double [] ReadXbytes(double [] data, NumberReference position, double length){
		double [] r;
		double i;

		r = new double [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			r[(int)(i)] = ReadByte(data, position);
		}

		return r;
	}


	public static double Read4bytesBE(double [] data, NumberReference position){
		double r;

		r = 0d;
		r = r + Pow(2d, 24d)*ReadByte(data, position);
		r = r + Pow(2d, 16d)*ReadByte(data, position);
		r = r + Pow(2d, 8d)*ReadByte(data, position);
		r = r + ReadByte(data, position);

		return r;
	}


	public static double Read2bytesBE(double [] data, NumberReference position){
		double r;

		r = 0d;
		r = r + Pow(2d, 8d)*ReadByte(data, position);
		r = r + ReadByte(data, position);

		return r;
	}


	public static double ReadByte(double [] data, NumberReference position){
		double next;

		next = data[(int)(position.numberValue)];
		position.numberValue = position.numberValue + 1d;

		return next;
	}


	public static double Read4bytesLE(double [] data, NumberReference position){
		double r;

		r = 0d;
		r = r + ReadByte(data, position);
		r = r + Pow(2d, 8d)*ReadByte(data, position);
		r = r + Pow(2d, 16d)*ReadByte(data, position);
		r = r + Pow(2d, 24d)*ReadByte(data, position);

		return r;
	}


	public static void WriteByte(double [] data, double b, NumberReference position){
		data[(int)(position.numberValue)] = b;
		position.numberValue = position.numberValue + 1d;
	}


	public static void Write2BytesLE(double [] data, double b, NumberReference position){
		data[(int)(position.numberValue)] = Roundx(b%Pow(2d, 8d));
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 8d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
	}


	public static void Write4BytesLE(double [] data, double b, NumberReference position){
		data[(int)(position.numberValue)] = Roundx(b%Pow(2d, 8d));
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 8d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 16d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 24d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
	}


	public static void Write2BytesBE(double [] data, double b, NumberReference position){
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 8d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Roundx(b%Pow(2d, 8d));
		position.numberValue = position.numberValue + 1d;
	}


	public static void Write4BytesBE(double [] data, double b, NumberReference position){
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 24d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 16d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Floor(b/Pow(2d, 8d))%Pow(2d, 8d);
		position.numberValue = position.numberValue + 1d;
		data[(int)(position.numberValue)] = Roundx(b%Pow(2d, 8d));
		position.numberValue = position.numberValue + 1d;
	}


	public static void WriteStringBytes(double [] data, char [] cs, NumberReference position){
		double i, v;

		for(i = 0d; i < cs.Length; i = i + 1d){
			v = cs[(int)(i)];
			WriteByte(data, v, position);
		}
	}


	public static double [] MakeCRC32Table(){
		double c, n, k;
		double [] crcTable;

		crcTable = new double [256];

		for(n = 0d; n < 256d; n = n + 1d){
			c = n;
			for(k = 0d; k < 8d; k = k + 1d){
				if(!DivisibleBy(c, 2d)){
					c = Xor4Byte(3988292384d, Floor(c/2d));
				}else{
					c = Floor(c/2d);
				}
			}
			crcTable[(int)(n)] = c;
		}

		return crcTable;
	}


	public static double UpdateCRC32(double crc, double [] buf, double [] crc_table){
		double n, index;

		for(n = 0d; n < buf.Length; n = n + 1d){
			index = And4Byte(Xor4Byte(crc, buf[(int)(n)]), Pow(2d, 8d) - 1d);
			crc = Xor4Byte(crc_table[(int)(index)], Floor(crc/Pow(2d, 8d)));
		}

		return crc;
	}


	public static double CalculateCRC32(double [] buf){
		double [] crcTable;
		double b32max, value;

		crcTable = MakeCRC32Table();

		b32max = Pow(2d, 32d) - 1d;
		value = UpdateCRC32(b32max, buf, crcTable);

		return Xor4Byte(value, b32max);
	}


	public static double CRC32OfInterval(double [] data, double from, double length){
		double [] crcBase;
		double i, crc;

		crcBase = new double [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			crcBase[(int)(i)] = data[(int)(from + i)];
		}

		crc = CalculateCRC32(crcBase);

		delete(crcBase);

		return crc;
	}


	public static ZLIBStruct ZLibCompressNoCompression(double [] data){
		ZLIBStruct zlibStruct;

		zlibStruct = new ZLIBStruct();

		zlibStruct.CMF = 120d;
		zlibStruct.FLG = 1d;
		zlibStruct.CompressedDataBlocks = DeflateDataNoCompression(data);
		zlibStruct.Adler32CheckValue = ComputeAdler32(data);

		return zlibStruct;
	}


	public static ZLIBStruct ZLibCompressStaticHuffman(double [] data, double level){
		ZLIBStruct zlibStruct;

		zlibStruct = new ZLIBStruct();

		zlibStruct.CMF = 120d;
		zlibStruct.FLG = 1d;
		zlibStruct.CompressedDataBlocks = DeflateDataStaticHuffman(data, level);
		zlibStruct.Adler32CheckValue = ComputeAdler32(data);

		return zlibStruct;
	}


	public static double [] AddNumber(double [] list, double a){
		double [] newlist;
		double i;

		newlist = new double [(int)(list.Length + 1d)];
		for(i = 0d; i < list.Length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.Length)] = a;
		
		delete(list);
		
		return newlist;
	}


	public static void AddNumberRef(NumberArrayReference list, double i){
		list.numberArray = AddNumber(list.numberArray, i);
	}


	public static double [] RemoveNumber(double [] list, double n){
		double [] newlist;
		double i;

		newlist = new double [(int)(list.Length - 1d)];

		if(n >= 0d && n < list.Length){
			for(i = 0d; i < list.Length; i = i + 1d){
				if(i < n){
					newlist[(int)(i)] = list[(int)(i)];
				}
				if(i > n){
					newlist[(int)(i - 1d)] = list[(int)(i)];
				}
			}

			delete(list);
		}else{
			delete(newlist);
		}
		
		return newlist;
	}


	public static double GetNumberRef(NumberArrayReference list, double i){
		return list.numberArray[(int)(i)];
	}


	public static void RemoveNumberRef(NumberArrayReference list, double i){
		list.numberArray = RemoveNumber(list.numberArray, i);
	}


	public static StringReference [] AddString(StringReference [] list, StringReference a){
		StringReference [] newlist;
		double i;

		newlist = new StringReference [(int)(list.Length + 1d)];

		for(i = 0d; i < list.Length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.Length)] = a;
		
		delete(list);
		
		return newlist;
	}


	public static void AddStringRef(StringArrayReference list, StringReference i){
		list.stringArray = AddString(list.stringArray, i);
	}


	public static StringReference [] RemoveString(StringReference [] list, double n){
		StringReference [] newlist;
		double i;

		newlist = new StringReference [(int)(list.Length - 1d)];

		if(n >= 0d && n < list.Length){
			for(i = 0d; i < list.Length; i = i + 1d){
				if(i < n){
					newlist[(int)(i)] = list[(int)(i)];
				}
				if(i > n){
					newlist[(int)(i - 1d)] = list[(int)(i)];
				}
			}

			delete(list);
		}else{
			delete(newlist);
		}
		
		return newlist;
	}


	public static StringReference GetStringRef(StringArrayReference list, double i){
		return list.stringArray[(int)(i)];
	}


	public static void RemoveStringRef(StringArrayReference list, double i){
		list.stringArray = RemoveString(list.stringArray, i);
	}


	public static bool [] AddBoolean(bool [] list, bool a){
		bool [] newlist;
		double i;

		newlist = new bool [(int)(list.Length + 1d)];
		for(i = 0d; i < list.Length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.Length)] = a;
		
		delete(list);
		
		return newlist;
	}


	public static void AddBooleanRef(BooleanArrayReference list, bool i){
		list.booleanArray = AddBoolean(list.booleanArray, i);
	}


	public static bool [] RemoveBoolean(bool [] list, double n){
		bool [] newlist;
		double i;

		newlist = new bool [(int)(list.Length - 1d)];

		if(n >= 0d && n < list.Length){
			for(i = 0d; i < list.Length; i = i + 1d){
				if(i < n){
					newlist[(int)(i)] = list[(int)(i)];
				}
				if(i > n){
					newlist[(int)(i - 1d)] = list[(int)(i)];
				}
			}

			delete(list);
		}else{
			delete(newlist);
		}
		
		return newlist;
	}


	public static bool GetBooleanRef(BooleanArrayReference list, double i){
		return list.booleanArray[(int)(i)];
	}


	public static void RemoveDecimalRef(BooleanArrayReference list, double i){
		list.booleanArray = RemoveBoolean(list.booleanArray, i);
	}


	public static LinkedListStrings CreateLinkedListString(){
		LinkedListStrings ll;

		ll = new LinkedListStrings();
		ll.first = new LinkedListNodeStrings();
		ll.last = ll.first;
		ll.last.end = true;

		return ll;
	}


	public static void LinkedListAddString(LinkedListStrings ll, char [] value){
		ll.last.end = false;
		ll.last.value = value;
		ll.last.next = new LinkedListNodeStrings();
		ll.last.next.end = true;
		ll.last = ll.last.next;
	}


	public static StringReference [] LinkedListStringsToArray(LinkedListStrings ll){
		StringReference [] array;
		double length, i;
		LinkedListNodeStrings node;

		node = ll.first;

		length = LinkedListStringsLength(ll);

		array = new StringReference [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			array[(int)(i)] = new StringReference();
			array[(int)(i)].stringx = node.value;
			node = node.next;
		}

		return array;
	}


	public static double LinkedListStringsLength(LinkedListStrings ll){
		double l;
		LinkedListNodeStrings node;

		l = 0d;
		node = ll.first;
		for(; !node.end; ){
			node = node.next;
			l = l + 1d;
		}

		return l;
	}


	public static void FreeLinkedListString(LinkedListStrings ll){
		LinkedListNodeStrings node, prev;

		node = ll.first;

		for(; !node.end; ){
			prev = node;
			node = node.next;
			delete(prev);
		}

		delete(node);
	}


	public static LinkedListNumbers CreateLinkedListNumbers(){
		LinkedListNumbers ll;

		ll = new LinkedListNumbers();
		ll.first = new LinkedListNodeNumbers();
		ll.last = ll.first;
		ll.last.end = true;

		return ll;
	}


	public static LinkedListNumbers [] CreateLinkedListNumbersArray(double length){
		LinkedListNumbers [] lls;
		double i;

		lls = new LinkedListNumbers [(int)(length)];
		for(i = 0d; i < lls.Length; i = i + 1d){
			lls[(int)(i)] = CreateLinkedListNumbers();
		}

		return lls;
	}


	public static void LinkedListAddNumber(LinkedListNumbers ll, double value){
		ll.last.end = false;
		ll.last.value = value;
		ll.last.next = new LinkedListNodeNumbers();
		ll.last.next.end = true;
		ll.last = ll.last.next;
	}


	public static double LinkedListNumbersLength(LinkedListNumbers ll){
		double l;
		LinkedListNodeNumbers node;

		l = 0d;
		node = ll.first;
		for(; !node.end; ){
			node = node.next;
			l = l + 1d;
		}

		return l;
	}


	public static double LinkedListNumbersIndex(LinkedListNumbers ll, double index){
		double i;
		LinkedListNodeNumbers node;

		node = ll.first;
		for(i = 0d; i < index; i = i + 1d){
			node = node.next;
		}

		return node.value;
	}


	public static void LinkedListInsertNumber(LinkedListNumbers ll, double index, double value){
		double i;
		LinkedListNodeNumbers node, tmp;

		if(index == 0d){
			tmp = ll.first;
			ll.first = new LinkedListNodeNumbers();
			ll.first.next = tmp;
			ll.first.value = value;
			ll.first.end = false;
		}else{
			node = ll.first;
			for(i = 0d; i < index - 1d; i = i + 1d){
				node = node.next;
			}

			tmp = node.next;
			node.next = new LinkedListNodeNumbers();
			node.next.next = tmp;
			node.next.value = value;
			node.next.end = false;
		}
	}


	public static void LinkedListSet(LinkedListNumbers ll, double index, double value){
		double i;
		LinkedListNodeNumbers node;

		node = ll.first;
		for(i = 0d; i < index; i = i + 1d){
			node = node.next;
		}

		node.next.value = value;
	}


	public static void LinkedListRemoveNumber(LinkedListNumbers ll, double index){
		double i;
		LinkedListNodeNumbers node, prev;

		node = ll.first;
		prev = ll.first;

		for(i = 0d; i < index; i = i + 1d){
			prev = node;
			node = node.next;
		}

		if(index == 0d){
			ll.first = prev.next;
		}
		if(!prev.next.end){
			prev.next = prev.next.next;
		}
	}


	public static void FreeLinkedListNumbers(LinkedListNumbers ll){
		LinkedListNodeNumbers node, prev;

		node = ll.first;

		for(; !node.end; ){
			prev = node;
			node = node.next;
			delete(prev);
		}

		delete(node);
	}


	public static void FreeLinkedListNumbersArray(LinkedListNumbers [] lls){
		double i;

		for(i = 0d; i < lls.Length; i = i + 1d){
			FreeLinkedListNumbers(lls[(int)(i)]);
		}
		delete(lls);
	}


	public static double [] LinkedListNumbersToArray(LinkedListNumbers ll){
		double [] array;
		double length, i;
		LinkedListNodeNumbers node;

		node = ll.first;

		length = LinkedListNumbersLength(ll);

		array = new double [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			array[(int)(i)] = node.value;
			node = node.next;
		}

		return array;
	}


	public static LinkedListNumbers ArrayToLinkedListNumbers(double [] array){
		LinkedListNumbers ll;
		double i;

		ll = CreateLinkedListNumbers();

		for(i = 0d; i < array.Length; i = i + 1d){
			LinkedListAddNumber(ll, array[(int)(i)]);
		}

		return ll;
	}


	public static bool LinkedListNumbersEqual(LinkedListNumbers a, LinkedListNumbers b){
		bool equal, done;
		LinkedListNodeNumbers an, bn;

		an = a.first;
		bn = b.first;

		equal = true;
		done = false;
		for(; equal && !done; ){
			if(an.end == bn.end){
				if(an.end){
					done = true;
				}else if(an.value == bn.value){
					an = an.next;
					bn = bn.next;
				}else{
					equal = false;
				}
			}else{
				equal = false;
			}
		}

		return equal;
	}


	public static LinkedListCharacters CreateLinkedListCharacter(){
		LinkedListCharacters ll;

		ll = new LinkedListCharacters();
		ll.first = new LinkedListNodeCharacters();
		ll.last = ll.first;
		ll.last.end = true;

		return ll;
	}


	public static void LinkedListAddCharacter(LinkedListCharacters ll, char value){
		ll.last.end = false;
		ll.last.value = value;
		ll.last.next = new LinkedListNodeCharacters();
		ll.last.next.end = true;
		ll.last = ll.last.next;
	}


	public static char [] LinkedListCharactersToArray(LinkedListCharacters ll){
		char [] array;
		double length, i;
		LinkedListNodeCharacters node;

		node = ll.first;

		length = LinkedListCharactersLength(ll);

		array = new char [(int)(length)];

		for(i = 0d; i < length; i = i + 1d){
			array[(int)(i)] = node.value;
			node = node.next;
		}

		return array;
	}


	public static double LinkedListCharactersLength(LinkedListCharacters ll){
		double l;
		LinkedListNodeCharacters node;

		l = 0d;
		node = ll.first;
		for(; !node.end; ){
			node = node.next;
			l = l + 1d;
		}

		return l;
	}


	public static void FreeLinkedListCharacter(LinkedListCharacters ll){
		LinkedListNodeCharacters node, prev;

		node = ll.first;

		for(; !node.end; ){
			prev = node;
			node = node.next;
			delete(prev);
		}

		delete(node);
	}


	public static DynamicArrayNumbers CreateDynamicArrayNumbers(){
		DynamicArrayNumbers da;

		da = new DynamicArrayNumbers();
		da.array = new double [10];
		da.length = 0d;

		return da;
	}


	public static DynamicArrayNumbers CreateDynamicArrayNumbersWithInitialCapacity(double capacity){
		DynamicArrayNumbers da;

		da = new DynamicArrayNumbers();
		da.array = new double [(int)(capacity)];
		da.length = 0d;

		return da;
	}


	public static void DynamicArrayAddNumber(DynamicArrayNumbers da, double value){
		if(da.length == da.array.Length){
			DynamicArrayNumbersIncreaseSize(da);
		}

		da.array[(int)(da.length)] = value;
		da.length = da.length + 1d;
	}


	public static void DynamicArrayNumbersIncreaseSize(DynamicArrayNumbers da){
		double newLength, i;
		double [] newArray;

		newLength = (double)Round(da.array.Length*3d/2d);
		newArray = new double [(int)(newLength)];

		for(i = 0d; i < da.array.Length; i = i + 1d){
			newArray[(int)(i)] = da.array[(int)(i)];
		}

		delete(da.array);

		da.array = newArray;
	}


	public static bool DynamicArrayNumbersDecreaseSizeNecessary(DynamicArrayNumbers da){
		bool needsDecrease;

		needsDecrease = false;

		if(da.length > 10d){
			needsDecrease = da.length <= (double)Round(da.array.Length*2d/3d);
		}

		return needsDecrease;
	}


	public static void DynamicArrayNumbersDecreaseSize(DynamicArrayNumbers da){
		double newLength, i;
		double [] newArray;

		newLength = (double)Round(da.array.Length*2d/3d);
		newArray = new double [(int)(newLength)];

		for(i = 0d; i < newLength; i = i + 1d){
			newArray[(int)(i)] = da.array[(int)(i)];
		}

		delete(da.array);

		da.array = newArray;
	}


	public static double DynamicArrayNumbersIndex(DynamicArrayNumbers da, double index){
		return da.array[(int)(index)];
	}


	public static double DynamicArrayNumbersLength(DynamicArrayNumbers da){
		return da.length;
	}


	public static void DynamicArrayInsertNumber(DynamicArrayNumbers da, double index, double value){
		double i;

		if(da.length == da.array.Length){
			DynamicArrayNumbersIncreaseSize(da);
		}

		for(i = da.length; i > index; i = i - 1d){
			da.array[(int)(i)] = da.array[(int)(i - 1d)];
		}

		da.array[(int)(index)] = value;

		da.length = da.length + 1d;
	}


	public static void DynamicArraySet(DynamicArrayNumbers da, double index, double value){
		da.array[(int)(index)] = value;
	}


	public static void DynamicArrayRemoveNumber(DynamicArrayNumbers da, double index){
		double i;

		for(i = index; i < da.length - 1d; i = i + 1d){
			da.array[(int)(i)] = da.array[(int)(i + 1d)];
		}

		da.length = da.length - 1d;

		if(DynamicArrayNumbersDecreaseSizeNecessary(da)){
			DynamicArrayNumbersDecreaseSize(da);
		}
	}


	public static void FreeDynamicArrayNumbers(DynamicArrayNumbers da){
		delete(da.array);
		delete(da);
	}


	public static double [] DynamicArrayNumbersToArray(DynamicArrayNumbers da){
		double [] array;
		double i;

		array = new double [(int)(da.length)];

		for(i = 0d; i < da.length; i = i + 1d){
			array[(int)(i)] = da.array[(int)(i)];
		}

		return array;
	}


	public static DynamicArrayNumbers ArrayToDynamicArrayNumbersWithOptimalSize(double [] array){
		DynamicArrayNumbers da;
		double i;
		double c, n, newCapacity;

		/*
         c = 10*(3/2)^n
         log(c) = log(10*(3/2)^n)
         log(c) = log(10) + log((3/2)^n)
         log(c) = 1 + log((3/2)^n)
         log(c) - 1 = log((3/2)^n)
         log(c) - 1 = n*log(3/2)
         n = (log(c) - 1)/log(3/2)
        */
		c = array.Length;
		n = (Log(c) - 1d)/Log(3d/2d);
		newCapacity = Floor(n) + 1d;

		da = CreateDynamicArrayNumbersWithInitialCapacity(newCapacity);

		for(i = 0d; i < array.Length; i = i + 1d){
			da.array[(int)(i)] = array[(int)(i)];
		}

		return da;
	}


	public static DynamicArrayNumbers ArrayToDynamicArrayNumbers(double [] array){
		DynamicArrayNumbers da;

		da = new DynamicArrayNumbers();
		da.array = aCopyNumberArray(array);
		da.length = array.Length;

		return da;
	}


	public static bool DynamicArrayNumbersEqual(DynamicArrayNumbers a, DynamicArrayNumbers b){
		bool equal;
		double i;

		equal = true;
		if(a.length == b.length){
			for(i = 0d; i < a.length && equal; i = i + 1d){
				if(a.array[(int)(i)] != b.array[(int)(i)]){
					equal = false;
				}
			}
		}else{
			equal = false;
		}

		return equal;
	}


	public static LinkedListNumbers DynamicArrayNumbersToLinkedList(DynamicArrayNumbers da){
		LinkedListNumbers ll;
		double i;

		ll = CreateLinkedListNumbers();

		for(i = 0d; i < da.length; i = i + 1d){
			LinkedListAddNumber(ll, da.array[(int)(i)]);
		}

		return ll;
	}


	public static DynamicArrayNumbers LinkedListToDynamicArrayNumbers(LinkedListNumbers ll){
		DynamicArrayNumbers da;
		double i;
		LinkedListNodeNumbers node;

		node = ll.first;

		da = new DynamicArrayNumbers();
		da.length = LinkedListNumbersLength(ll);

		da.array = new double [(int)(da.length)];

		for(i = 0d; i < da.length; i = i + 1d){
			da.array[(int)(i)] = node.value;
			node = node.next;
		}

		return da;
	}


	public static char [] AddCharacter(char [] list, char a){
		char [] newlist;
		double i;

		newlist = new char [(int)(list.Length + 1d)];
		for(i = 0d; i < list.Length; i = i + 1d){
			newlist[(int)(i)] = list[(int)(i)];
		}
		newlist[(int)(list.Length)] = a;
		
		delete(list);
		
		return newlist;
	}


	public static void AddCharacterRef(StringReference list, char i){
		list.stringx = AddCharacter(list.stringx, i);
	}


	public static char [] RemoveCharacter(char [] list, double n){
		char [] newlist;
		double i;

		newlist = new char [(int)(list.Length - 1d)];

		if(n >= 0d && n < list.Length){
			for(i = 0d; i < list.Length; i = i + 1d){
				if(i < n){
					newlist[(int)(i)] = list[(int)(i)];
				}
				if(i > n){
					newlist[(int)(i - 1d)] = list[(int)(i)];
				}
			}

			delete(list);
		}else{
			delete(newlist);
		}

		return newlist;
	}


	public static char GetCharacterRef(StringReference list, double i){
		return list.stringx[(int)(i)];
	}


	public static void RemoveCharacterRef(StringReference list, double i){
		list.stringx = RemoveCharacter(list.stringx, i);
	}


	public static char charToLowerCase(char character){
		char toReturn;

		toReturn = character;
		if(character == 'A'){
			toReturn = 'a';
		}else if(character == 'B'){
			toReturn = 'b';
		}else if(character == 'C'){
			toReturn = 'c';
		}else if(character == 'D'){
			toReturn = 'd';
		}else if(character == 'E'){
			toReturn = 'e';
		}else if(character == 'F'){
			toReturn = 'f';
		}else if(character == 'G'){
			toReturn = 'g';
		}else if(character == 'H'){
			toReturn = 'h';
		}else if(character == 'I'){
			toReturn = 'i';
		}else if(character == 'J'){
			toReturn = 'j';
		}else if(character == 'K'){
			toReturn = 'k';
		}else if(character == 'L'){
			toReturn = 'l';
		}else if(character == 'M'){
			toReturn = 'm';
		}else if(character == 'N'){
			toReturn = 'n';
		}else if(character == 'O'){
			toReturn = 'o';
		}else if(character == 'P'){
			toReturn = 'p';
		}else if(character == 'Q'){
			toReturn = 'q';
		}else if(character == 'R'){
			toReturn = 'r';
		}else if(character == 'S'){
			toReturn = 's';
		}else if(character == 'T'){
			toReturn = 't';
		}else if(character == 'U'){
			toReturn = 'u';
		}else if(character == 'V'){
			toReturn = 'v';
		}else if(character == 'W'){
			toReturn = 'w';
		}else if(character == 'X'){
			toReturn = 'x';
		}else if(character == 'Y'){
			toReturn = 'y';
		}else if(character == 'Z'){
			toReturn = 'z';
		}

		return toReturn;
	}


	public static char charToUpperCase(char character){
		char toReturn;

		toReturn = character;
		if(character == 'a'){
			toReturn = 'A';
		}else if(character == 'b'){
			toReturn = 'B';
		}else if(character == 'c'){
			toReturn = 'C';
		}else if(character == 'd'){
			toReturn = 'D';
		}else if(character == 'e'){
			toReturn = 'E';
		}else if(character == 'f'){
			toReturn = 'F';
		}else if(character == 'g'){
			toReturn = 'G';
		}else if(character == 'h'){
			toReturn = 'H';
		}else if(character == 'i'){
			toReturn = 'I';
		}else if(character == 'j'){
			toReturn = 'J';
		}else if(character == 'k'){
			toReturn = 'K';
		}else if(character == 'l'){
			toReturn = 'L';
		}else if(character == 'm'){
			toReturn = 'M';
		}else if(character == 'n'){
			toReturn = 'N';
		}else if(character == 'o'){
			toReturn = 'O';
		}else if(character == 'p'){
			toReturn = 'P';
		}else if(character == 'q'){
			toReturn = 'Q';
		}else if(character == 'r'){
			toReturn = 'R';
		}else if(character == 's'){
			toReturn = 'S';
		}else if(character == 't'){
			toReturn = 'T';
		}else if(character == 'u'){
			toReturn = 'U';
		}else if(character == 'v'){
			toReturn = 'V';
		}else if(character == 'w'){
			toReturn = 'W';
		}else if(character == 'x'){
			toReturn = 'X';
		}else if(character == 'y'){
			toReturn = 'Y';
		}else if(character == 'z'){
			toReturn = 'Z';
		}

		return toReturn;
	}


	public static bool charIsUpperCase(char character){
		bool isUpper;

		isUpper = false;
		if(character == 'A'){
			isUpper = true;
		}else if(character == 'B'){
			isUpper = true;
		}else if(character == 'C'){
			isUpper = true;
		}else if(character == 'D'){
			isUpper = true;
		}else if(character == 'E'){
			isUpper = true;
		}else if(character == 'F'){
			isUpper = true;
		}else if(character == 'G'){
			isUpper = true;
		}else if(character == 'H'){
			isUpper = true;
		}else if(character == 'I'){
			isUpper = true;
		}else if(character == 'J'){
			isUpper = true;
		}else if(character == 'K'){
			isUpper = true;
		}else if(character == 'L'){
			isUpper = true;
		}else if(character == 'M'){
			isUpper = true;
		}else if(character == 'N'){
			isUpper = true;
		}else if(character == 'O'){
			isUpper = true;
		}else if(character == 'P'){
			isUpper = true;
		}else if(character == 'Q'){
			isUpper = true;
		}else if(character == 'R'){
			isUpper = true;
		}else if(character == 'S'){
			isUpper = true;
		}else if(character == 'T'){
			isUpper = true;
		}else if(character == 'U'){
			isUpper = true;
		}else if(character == 'V'){
			isUpper = true;
		}else if(character == 'W'){
			isUpper = true;
		}else if(character == 'X'){
			isUpper = true;
		}else if(character == 'Y'){
			isUpper = true;
		}else if(character == 'Z'){
			isUpper = true;
		}

		return isUpper;
	}


	public static bool charIsLowerCase(char character){
		bool isLower;

		isLower = false;
		if(character == 'a'){
			isLower = true;
		}else if(character == 'b'){
			isLower = true;
		}else if(character == 'c'){
			isLower = true;
		}else if(character == 'd'){
			isLower = true;
		}else if(character == 'e'){
			isLower = true;
		}else if(character == 'f'){
			isLower = true;
		}else if(character == 'g'){
			isLower = true;
		}else if(character == 'h'){
			isLower = true;
		}else if(character == 'i'){
			isLower = true;
		}else if(character == 'j'){
			isLower = true;
		}else if(character == 'k'){
			isLower = true;
		}else if(character == 'l'){
			isLower = true;
		}else if(character == 'm'){
			isLower = true;
		}else if(character == 'n'){
			isLower = true;
		}else if(character == 'o'){
			isLower = true;
		}else if(character == 'p'){
			isLower = true;
		}else if(character == 'q'){
			isLower = true;
		}else if(character == 'r'){
			isLower = true;
		}else if(character == 's'){
			isLower = true;
		}else if(character == 't'){
			isLower = true;
		}else if(character == 'u'){
			isLower = true;
		}else if(character == 'v'){
			isLower = true;
		}else if(character == 'w'){
			isLower = true;
		}else if(character == 'x'){
			isLower = true;
		}else if(character == 'y'){
			isLower = true;
		}else if(character == 'z'){
			isLower = true;
		}

		return isLower;
	}


	public static bool charIsLetter(char character){
		return charIsUpperCase(character) || charIsLowerCase(character);
	}


	public static bool charIsNumber(char character){
		bool isNumberx;

		isNumberx = false;
		if(character == '0'){
			isNumberx = true;
		}else if(character == '1'){
			isNumberx = true;
		}else if(character == '2'){
			isNumberx = true;
		}else if(character == '3'){
			isNumberx = true;
		}else if(character == '4'){
			isNumberx = true;
		}else if(character == '5'){
			isNumberx = true;
		}else if(character == '6'){
			isNumberx = true;
		}else if(character == '7'){
			isNumberx = true;
		}else if(character == '8'){
			isNumberx = true;
		}else if(character == '9'){
			isNumberx = true;
		}

		return isNumberx;
	}


	public static bool charIsWhiteSpace(char character){
		bool isWhiteSpacex;

		isWhiteSpacex = false;
		if(character == ' '){
			isWhiteSpacex = true;
		}else if(character == '\t'){
			isWhiteSpacex = true;
		}else if(character == '\n'){
			isWhiteSpacex = true;
		}else if(character == '\r'){
			isWhiteSpacex = true;
		}

		return isWhiteSpacex;
	}


	public static bool charIsSymbol(char character){
		bool isSymbolx;

		isSymbolx = false;
		if(character == '!'){
			isSymbolx = true;
		}else if(character == '\"'){
			isSymbolx = true;
		}else if(character == '#'){
			isSymbolx = true;
		}else if(character == '$'){
			isSymbolx = true;
		}else if(character == '%'){
			isSymbolx = true;
		}else if(character == '&'){
			isSymbolx = true;
		}else if(character == '\''){
			isSymbolx = true;
		}else if(character == '('){
			isSymbolx = true;
		}else if(character == ')'){
			isSymbolx = true;
		}else if(character == '*'){
			isSymbolx = true;
		}else if(character == '+'){
			isSymbolx = true;
		}else if(character == ','){
			isSymbolx = true;
		}else if(character == '-'){
			isSymbolx = true;
		}else if(character == '.'){
			isSymbolx = true;
		}else if(character == '/'){
			isSymbolx = true;
		}else if(character == ':'){
			isSymbolx = true;
		}else if(character == ';'){
			isSymbolx = true;
		}else if(character == '<'){
			isSymbolx = true;
		}else if(character == '='){
			isSymbolx = true;
		}else if(character == '>'){
			isSymbolx = true;
		}else if(character == '?'){
			isSymbolx = true;
		}else if(character == '@'){
			isSymbolx = true;
		}else if(character == '['){
			isSymbolx = true;
		}else if(character == '\\'){
			isSymbolx = true;
		}else if(character == ']'){
			isSymbolx = true;
		}else if(character == '^'){
			isSymbolx = true;
		}else if(character == '_'){
			isSymbolx = true;
		}else if(character == '`'){
			isSymbolx = true;
		}else if(character == '{'){
			isSymbolx = true;
		}else if(character == '|'){
			isSymbolx = true;
		}else if(character == '}'){
			isSymbolx = true;
		}else if(character == '~'){
			isSymbolx = true;
		}

		return isSymbolx;
	}


	public static bool charCharacterIsBefore(char a, char b){
		double ad, bd;

		ad = a;
		bd = b;

		return ad < bd;
	}


	public static double And4Byte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFFFFFFFU && n2 >= 0d && n2 <= (double)0xFFFFFFFFU){
			return (uint)n1 & (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double And2Byte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFFFU && n2 >= 0d && n2 <= (double)0xFFFFU){
			return (uint)n1 & (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double AndByte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFU && n2 >= 0d && n2 <= (double)0xFFU){
			return (uint)n1 & (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double AndBytes(double n1, double n2, double bytes){
		double byteVal, result, i;

		byteVal = 1d;
		result = 0d;

		if(n1 >= 0d && n1 < Pow(2d, bytes*8d) && n2 >= 0d && n2 < Pow(2d, bytes*8d)){
			n1 = Truncate(n1);
			n2 = Truncate(n2);
			bytes = Truncate(bytes);

			for(i = 0d; i < bytes*8d; i = i + 1d){
				if(n1%2d == 1d && n2%2d == 1d){
					result = result + byteVal;
				}
				n1 = Floor(n1/2d);
				n2 = Floor(n2/2d);
				byteVal = byteVal*2d;
			}
		}

		return result;
	}


	public static double Or4Byte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFFFFFFFU && n2 >= 0d && n2 <= (double)0xFFFFFFFFU){
			return (uint)n1 | (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double Or2Byte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFFFU && n2 >= 0d && n2 <= (double)0xFFFFU){
			return (uint)n1 | (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double OrByte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFU && n2 >= 0d && n2 <= (double)0xFFU){
			return (uint)n1 | (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double OrBytes(double n1, double n2, double bytes){
		double byteVal, result, i;

		byteVal = 1d;
		result = 0d;

		if(n1 >= 0d && n1 < Pow(2d, bytes*8d) && n2 >= 0d && n2 < Pow(2d, bytes*8d)){
			n1 = Truncate(n1);
			n2 = Truncate(n2);
			bytes = Truncate(bytes);

			for(i = 0d; i < bytes*8d; i = i + 1d){
				if(n1%2d == 1d || n2%2d == 1d){
					result = result + byteVal;
				}
				n1 = Floor(n1/2d);
				n2 = Floor(n2/2d);
				byteVal = byteVal*2d;
			}
		}

		return result;
	}


	public static double Xor4Byte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFFFFFFFU && n2 >= 0d && n2 <= (double)0xFFFFFFFFU){
			return (uint)n1 ^ (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double Xor2Byte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFFFU && n2 >= 0d && n2 <= (double)0xFFFFU){
			return (uint)n1 ^ (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double XorByte(double n1, double n2){
		if(n1 >= 0d && n1 <= (double)0xFFU && n2 >= 0d && n2 <= (double)0xFFU){
			return (uint)n1 ^ (uint)n2;
		}else{
			return 0d;
		}
	}


	public static double XorBytes(double n1, double n2, double bytes){
		double byteVal, result, i;

		byteVal = 1d;
		result = 0d;

		if(n1 >= 0d && n1 < Pow(2d, bytes*8d) && n2 >= 0d && n2 < Pow(2d, bytes*8d)){
			n1 = Truncate(n1);
			n2 = Truncate(n2);
			bytes = Truncate(bytes);

			for(i = 0d; i < bytes*8d; i = i + 1d){
				if(n1%2d != n2%2d){
					result = result + byteVal;
				}
				n1 = Floor(n1/2d);
				n2 = Floor(n2/2d);
				byteVal = byteVal*2d;
			}
		}

		return result;
	}


	public static double Not4Byte(double b){
		if(b >= 0d && b <= (double)0xFFFFFFFFL){
			return ~(uint)b & 0xFFFFFFFFL;
		}else{
			return 0d;
		}
	}


	public static double Not2Byte(double b){
		if(b >= 0d && b <= (double)0xFFFFL){
			return ~(uint)b & 0xFFFFL;
		}else{
			return 0d;
		}
	}


	public static double NotByte(double b){
		if(b >= 0d && b <= (double)0xFFL){
			return ~(uint)b & 0xFFL;
		}else{
			return 0d;
		}
	}


	public static double NotBytes(double b, double length){
		double result;

		result = 0d;

		if(b >= 0d && b < Pow(2d, length*8d)){
			b = Truncate(b);
			length = Truncate(length);

			result = Pow(2d, length*8d) - b - 1d;
		}

		return result;
	}


	public static double ShiftLeft4Byte(double b, double amount){
		if(b >= 0d && b <= (double)0xFFFFFFFFU && amount >= 0d && amount < (double)32){
			return (uint)b << (int)amount;
		}else{
			return 0d;
		}
	}


	public static double ShiftLeft2Byte(double b, double amount){
		if(b >= 0d && b <= (double)0xFFFFU && amount >= 0d && amount < (double)16){
			return (uint)b << (int)amount;
		}else{
			return 0d;
		}
	}


	public static double ShiftLeftByte(double b, double amount){
		if(b >= 0d && b <= (double)0xFFU && amount >= 0d && amount < (double)8){
			return (uint)b << (int)amount;
		}else{
			return 0d;
		}
	}


	public static double ShiftLeftBytes(double b, double amount, double length){
		double result;

		result = 0d;

		if(b >= 0d && b < Pow(2d, length*8d) && amount >= 0d && amount <= length*8d){
			b = Truncate(b);
			amount = Truncate(amount);

			result = b*Pow(2d, amount);
		}

		return result;
	}


	public static double ShiftRight4Byte(double b, double amount){
		if(b >= 0d && b <= (double)0xFFFFFFFFU && amount >= 0d && amount < (double)32){
			return (uint)b >> (int)amount;
		}else{
			return 0d;
		}
	}


	public static double ShiftRight2Byte(double b, double amount){
		if(b >= 0d && b <= (double)0xFFFFU && amount >= 0d && amount < (double)16){
			return (uint)b >> (int)amount;
		}else{
			return 0d;
		}
	}


	public static double ShiftRightByte(double b, double amount){
		if(b >= 0d && b <= (double)0xFFU && amount >= 0d && amount < (double)8){
			return (uint)b >> (int)amount;
		}else{
			return 0d;
		}
	}


	public static double ShiftRightBytes(double b, double amount, double length){
		double result;

		result = 0d;

		if(b >= 0d && b < Pow(2d, length*8d) && amount >= 0d && amount <= length*8d){
			b = Truncate(b);
			amount = Truncate(amount);

			result = Truncate(b/Pow(2d, amount));
		}

		return result;
	}


	public static double ReadNextBit(double [] data, NumberReference nextbit){
		double bytenr, bitnumber, bit, b;

		bytenr = Floor(nextbit.numberValue/8d);
		bitnumber = nextbit.numberValue%8d;

		b = data[(int)(bytenr)];

		bit = Floor(b/Pow(2d, bitnumber))%2d;

		nextbit.numberValue = nextbit.numberValue + 1d;

		return bit;
	}


	public static double BitExtract(double b, double fromInc, double toInc){
		return Floor(b/Pow(2d, fromInc))%Pow(2d, toInc + 1d - fromInc);
	}


	public static double ReadBitRange(double [] data, NumberReference nextbit, double length){
		double startbyte, endbyte;
		double startbit, endbit;
		double number, i;

		number = 0d;

		startbyte = Floor(nextbit.numberValue/8d);
		endbyte = Floor((nextbit.numberValue + length)/8d);

		startbit = nextbit.numberValue%8d;
		endbit = (nextbit.numberValue + length - 1d)%8d;

		if(startbyte == endbyte){
			number = BitExtract(data[(int)(startbyte)], startbit, endbit);
		}

		nextbit.numberValue = nextbit.numberValue + length;

		return number;
	}


	public static void SkipToBoundary(NumberReference nextbit){
		double skip;

		skip = 8d - nextbit.numberValue%8d;
		nextbit.numberValue = nextbit.numberValue + skip;
	}


	public static double ReadNextByteBoundary(double [] data, NumberReference nextbit){
		double bytenr, b;

		bytenr = Floor(nextbit.numberValue/8d);
		b = data[(int)(bytenr)];
		nextbit.numberValue = nextbit.numberValue + 8d;

		return b;
	}


	public static double Read2bytesByteBoundary(double [] data, NumberReference nextbit){
		double r;

		r = 0d;
		r = r + Pow(2d, 8d)*ReadNextByteBoundary(data, nextbit);
		r = r + ReadNextByteBoundary(data, nextbit);

		return r;
	}


	public static double ComputeAdler32(double [] data){
		double a, b, m, i;

		a = 1d;
		b = 0d;
		m = 65521d;

		for(i = 0d; i < data.Length; i = i + 1d){
			a = (a + data[(int)(i)])%m;
			b = (b + a)%m;
		}

		return b*Pow(2d, 16d) + a;
	}


	public static double [] DeflateDataStaticHuffman(double [] data, double level){
		double [] bytes;
		NumberReference currentBit;
		double i;
		NumberArrayReference copy;
		NumberReference code, length, compressedCode, lengthAdditionLength, distanceCode;
		NumberReference distanceReference, lengthReference, lengthAddition;
		NumberReference distanceAdditionReference, distanceAdditionLengthReference;
		double [] bitReverseLookupTable;
		BooleanReference match;

		code = CreateNumberReference(0d);
		length = CreateNumberReference(0d);
		compressedCode = CreateNumberReference(0d);
		lengthAdditionLength = CreateNumberReference(0d);
		distanceCode = CreateNumberReference(0d);
		distanceReference = CreateNumberReference(0d);
		lengthReference = CreateNumberReference(0d);
		lengthAddition = CreateNumberReference(0d);
		distanceAdditionReference = CreateNumberReference(0d);
		distanceAdditionLengthReference = CreateNumberReference(0d);
		match = new BooleanReference();

		bytes = new double [(int)(Max(data.Length*2d, 100d))];
		aFillNumberArray(bytes, 0d);
		currentBit = CreateNumberReference(0d);

		bitReverseLookupTable = GenerateBitReverseLookupTable(9d);

		/* Final block*/
		AppendBitsToBytesRight(bytes, currentBit, 1d, 1d);
		/* Fixed code*/
		AppendBitsToBytesRight(bytes, currentBit, 1d, 2d);

		for(i = 0d; i < data.Length; ){
			FindMatch(data, i, distanceReference, lengthReference, match, level);

			if(match.booleanValue){
				GetDeflateLengthCode(lengthReference.numberValue, compressedCode, lengthAddition, lengthAdditionLength);
				GetDeflateDistanceCode(distanceReference.numberValue, distanceCode, distanceAdditionReference, distanceAdditionLengthReference, bitReverseLookupTable);
			}

			if(!match.booleanValue){
				GetDeflateStaticHuffmanCode(data[(int)(i)], code, length, bitReverseLookupTable);
				AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue);
				i = i + 1d;
			}else{
				GetDeflateStaticHuffmanCode(compressedCode.numberValue, code, length, bitReverseLookupTable);
				AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue);
				AppendBitsToBytesRight(bytes, currentBit, lengthAddition.numberValue, lengthAdditionLength.numberValue);
				AppendBitsToBytesRight(bytes, currentBit, distanceCode.numberValue, 5d);
				AppendBitsToBytesRight(bytes, currentBit, distanceAdditionReference.numberValue, distanceAdditionLengthReference.numberValue);
				i = i + lengthReference.numberValue;
			}
		}

		/* Stop symbol*/
		GetDeflateStaticHuffmanCode(256d, code, length, bitReverseLookupTable);
		AppendBitsToBytesRight(bytes, currentBit, code.numberValue, length.numberValue);

		copy = new NumberArrayReference();
		aCopyNumberArrayRange(bytes, 0d, Ceiling(currentBit.numberValue/8d), copy);
		delete(bytes);
		bytes = copy.numberArray;

		return bytes;
	}


	public static void FindMatch(double [] data, double pos, NumberReference distanceReference, NumberReference lengthReference, BooleanReference match, double level){
		double i, j;
		double deflateMinMength, deflateMaxLength, deflateMaxDistance;
		double longest, maxLength, distanceForMax;
		double startDistance, matchLength;
		bool done;

		deflateMinMength = 3d;
		deflateMaxLength = 258d;

		longest = Min(pos - 1d, deflateMaxLength);
		longest = Min(data.Length - pos, longest);

		deflateMaxDistance = Floor(32768d/10d*level);

		startDistance = Min(pos, deflateMaxDistance);

		if(longest >= deflateMinMength){
			maxLength = 0d;
			distanceForMax = 0d;

			for(i = pos - 1d; i >= pos - startDistance && maxLength != longest; i = i - 1d){
				matchLength = 0d;
				done = false;
				for(j = 0d; j < longest && !done; j = j + 1d){
					if(data[(int)(i + j)] == data[(int)(pos + j)]){
						matchLength = matchLength + 1d;
					}else{
						done = true;
					}
				}

				if(matchLength >= deflateMinMength && matchLength > maxLength){
					maxLength = matchLength;
					distanceForMax = pos - i;
				}
			}

			if(maxLength >= deflateMinMength){
				match.booleanValue = true;
				lengthReference.numberValue = maxLength;
				distanceReference.numberValue = distanceForMax;
			}else{
				match.booleanValue = false;
			}
		}else{
			match.booleanValue = false;
		}
	}


	public static double [] GenerateBitReverseLookupTable(double bits){
		double [] table;
		double i;

		table = new double [(int)(Pow(2d, bits))];

		for(i = 0d; i < table.Length; i = i + 1d){
			table[(int)(i)] = ReverseBits(i, 32d);
		}

		return table;
	}


	public static double ReverseBits(double x, double bits){
		double b, bit, i;

		b = 0d;

		for(i = 0d; i < bits; i = i + 1d){
			b = ShiftLeft4Byte(b, 1d);
			bit = And4Byte(x, 1d);
			b = Or4Byte(b, bit);
			x = ShiftRight4Byte(x, 1d);
		}

		return b;
	}


	public static double [] DeflateDataNoCompression(double [] data){
		double [] deflated;
		NumberReference position;
		double block, i, blocks, blocklength, maxblocksize;

		maxblocksize = Pow(2d, 16d) - 1d;
		blocks = Ceiling(data.Length/maxblocksize);

		position = CreateNumberReference(0d);

		deflated = new double [(int)((1d + 4d)*blocks + data.Length)];

		for(block = 0d; block < blocks; block = block + 1d){
			if(block + 1d == blocks){
				WriteByte(deflated, 1d, position);
			}else{
				WriteByte(deflated, 0d, position);
			}
			blocklength = Min(data.Length - block*maxblocksize, maxblocksize);
			Write2BytesLE(deflated, blocklength, position);
			Write2BytesLE(deflated, Not2Byte(blocklength), position);

			for(i = 0d; i < blocklength; i = i + 1d){
				WriteByte(deflated, data[(int)(block*maxblocksize + i)], position);
			}
		}

		return deflated;
	}


	public static void GetDeflateStaticHuffmanCode(double b, NumberReference code, NumberReference length, double [] bitReverseLookupTable){
		double reversed;

		if(b >= 0d && b <= 143d){
			code.numberValue = 48d + b;
			length.numberValue = 8d;
		}else if(b >= 144d && b <= 255d){
			code.numberValue = b - 144d + 400d;
			length.numberValue = 9d;
		}else if(b >= 256d && b <= 279d){
			code.numberValue = b - 256d + 0d;
			length.numberValue = 7d;
		}else if(b >= 280d && b <= 287d){
			code.numberValue = b - 280d + 192d;
			length.numberValue = 8d;
		}

		reversed = bitReverseLookupTable[(int)(code.numberValue)];
		code.numberValue = ShiftRight4Byte(reversed, 32d - length.numberValue);
	}


	public static void GetDeflateLengthCode(double length, NumberReference code, NumberReference lengthAddition, NumberReference lengthAdditionLength){
		if(length >= 3d && length <= 10d){
			code.numberValue = 257d + length - 3d;
			lengthAdditionLength.numberValue = 0d;
		}else if(length >= 11d && length <= 18d){
			code.numberValue = 265d + Floor((length - 11d)/2d);
			lengthAddition.numberValue = Floor((length - 11d)%2d);
			lengthAdditionLength.numberValue = 1d;
		}else if(length >= 19d && length <= 34d){
			code.numberValue = 269d + Floor((length - 19d)/4d);
			lengthAddition.numberValue = Floor((length - 19d)%4d);
			lengthAdditionLength.numberValue = 2d;
		}else if(length >= 35d && length <= 66d){
			code.numberValue = 273d + Floor((length - 35d)/8d);
			lengthAddition.numberValue = Floor((length - 35d)%8d);
			lengthAdditionLength.numberValue = 3d;
		}else if(length >= 67d && length <= 130d){
			code.numberValue = 277d + Floor((length - 67d)/16d);
			lengthAddition.numberValue = Floor((length - 67d)%16d);
			lengthAdditionLength.numberValue = 4d;
		}else if(length >= 131d && length <= 257d){
			code.numberValue = 281d + Floor((length - 131d)/32d);
			lengthAddition.numberValue = Floor((length - 131d)%32d);
			lengthAdditionLength.numberValue = 5d;
		}else if(length == 258d){
			code.numberValue = 285d;
			lengthAdditionLength.numberValue = 0d;
		}
	}


	public static void GetDeflateDistanceCode(double distance, NumberReference code, NumberReference distanceAdditionReference, NumberReference distanceAdditionLengthReference, double [] bitReverseLookupTable){
		double reversed;

		if(distance >= 1d && distance <= 4d){
			code.numberValue = distance - 1d;
			distanceAdditionLengthReference.numberValue = 0d;
		}else if(distance >= 5d && distance <= 8d){
			code.numberValue = 4d + Floor((distance - 5d)/2d);
			distanceAdditionReference.numberValue = Floor((distance - 5d)%2d);
			distanceAdditionLengthReference.numberValue = 1d;
		}else if(distance >= 9d && distance <= 16d){
			code.numberValue = 6d + Floor((distance - 9d)/4d);
			distanceAdditionReference.numberValue = Floor((distance - 9d)%4d);
			distanceAdditionLengthReference.numberValue = 2d;
		}else if(distance >= 17d && distance <= 32d){
			code.numberValue = 8d + Floor((distance - 17d)/8d);
			distanceAdditionReference.numberValue = Floor((distance - 17d)%8d);
			distanceAdditionLengthReference.numberValue = 3d;
		}else if(distance >= 33d && distance <= 64d){
			code.numberValue = 10d + Floor((distance - 33d)/16d);
			distanceAdditionReference.numberValue = Floor((distance - 33d)%16d);
			distanceAdditionLengthReference.numberValue = 4d;
		}else if(distance >= 65d && distance <= 128d){
			code.numberValue = 12d + Floor((distance - 65d)/32d);
			distanceAdditionReference.numberValue = Floor((distance - 65d)%32d);
			distanceAdditionLengthReference.numberValue = 5d;
		}else if(distance >= 129d && distance <= 256d){
			code.numberValue = 14d + Floor((distance - 129d)/64d);
			distanceAdditionReference.numberValue = Floor((distance - 129d)%64d);
			distanceAdditionLengthReference.numberValue = 6d;
		}else if(distance >= 257d && distance <= 512d){
			code.numberValue = 16d + Floor((distance - 257d)/128d);
			distanceAdditionReference.numberValue = Floor((distance - 257d)%128d);
			distanceAdditionLengthReference.numberValue = 7d;
		}else if(distance >= 513d && distance <= 1024d){
			code.numberValue = 18d + Floor((distance - 513d)/256d);
			distanceAdditionReference.numberValue = Floor((distance - 513d)%256d);
			distanceAdditionLengthReference.numberValue = 8d;
		}else if(distance >= 1025d && distance <= 2048d){
			code.numberValue = 20d + Floor((distance - 1025d)/Pow(2d, 9d));
			distanceAdditionReference.numberValue = Floor((distance - 1025d)%Pow(2d, 9d));
			distanceAdditionLengthReference.numberValue = 9d;
		}else if(distance >= 2049d && distance <= 4096d){
			code.numberValue = 22d + Floor((distance - 2049d)/Pow(2d, 10d));
			distanceAdditionReference.numberValue = Floor((distance - 2049d)%Pow(2d, 10d));
			distanceAdditionLengthReference.numberValue = 10d;
		}else if(distance >= 4097d && distance <= 8192d){
			code.numberValue = 24d + Floor((distance - 4097d)/Pow(2d, 11d));
			distanceAdditionReference.numberValue = Floor((distance - 4097d)%Pow(2d, 11d));
			distanceAdditionLengthReference.numberValue = 11d;
		}else if(distance >= 8193d && distance <= 16384d){
			code.numberValue = 26d + Floor((distance - 8193d)/Pow(2d, 12d));
			distanceAdditionReference.numberValue = Floor((distance - 8193d)%Pow(2d, 12d));
			distanceAdditionLengthReference.numberValue = 12d;
		}else if(distance >= 16385d && distance <= 32768d){
			code.numberValue = 28d + Floor((distance - 16385d)/Pow(2d, 13d));
			distanceAdditionReference.numberValue = Floor((distance - 16385d)%Pow(2d, 13d));
			distanceAdditionLengthReference.numberValue = 13d;
		}

		reversed = bitReverseLookupTable[(int)(code.numberValue)];
		code.numberValue = ShiftRight4Byte(reversed, 32d - 5d);
	}


	public static void AppendBitsToBytesLeft(double [] bytes, NumberReference nextbit, double data, double length){
		double bytePos, bitPos, segment, part, remove;

		for(; length > 0d; ){
			bytePos = Truncate(nextbit.numberValue/8d);
			bitPos = nextbit.numberValue%8d;

			if(length < 8d - bitPos){
				part = ShiftLeft4Byte(data, 8d - bitPos - length);

				bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);

				nextbit.numberValue = nextbit.numberValue + length;

				length = 0d;
			}else{
				segment = 8d - bitPos;

				part = ShiftRight4Byte(data, length - segment);
				bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);
				nextbit.numberValue = nextbit.numberValue + segment;

				remove = ShiftLeft4Byte(part, length - segment);
				data = Xor4Byte(data, remove);

				length = length - segment;
			}
		}
	}


	public static void AppendBitsToBytesRight(double [] bytes, NumberReference nextbit, double data, double length){
		double bytePos, bitPos, segment, part;
		double mask;

		for(; length > 0d; ){
			bytePos = Truncate(nextbit.numberValue/8d);
			bitPos = nextbit.numberValue%8d;

			if(length < 8d - bitPos){
				part = ShiftLeft4Byte(data, bitPos);

				bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);

				nextbit.numberValue = nextbit.numberValue + length;

				length = 0d;
			}else{
				segment = 8d - bitPos;

				mask = 1d;
				mask = ShiftLeft4Byte(mask, segment);
				mask = mask - 1d;

				part = And4Byte(mask, data);
				part = ShiftLeft4Byte(part, bitPos);
				bytes[(int)(bytePos)] = Or4Byte(bytes[(int)(bytePos)], part);
				nextbit.numberValue = nextbit.numberValue + segment;

				data = ShiftRight4Byte(data, segment);

				length = length - segment;
			}
		}
	}


	public static void delete(System.Object objectx){
		// C# has garbage collection.
	}
}

