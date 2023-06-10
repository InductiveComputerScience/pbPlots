#include <stdlib.h>
#include <stdbool.h>
#include <math.h>
#include <string.h>
#include <wchar.h>
#include <stdint.h>

#define strparam(str) (str), wcslen(str)

#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif

#include <stdint.h>
#include <stdlib.h>
#include <stdbool.h>

void StartArenaAllocator();
void FreeAllocations();
void *Allocate(int64_t size, int64_t e);
void Free(void *addr);

struct RGBABitmapImageReference;
typedef struct RGBABitmapImageReference RGBABitmapImageReference;

struct Rectangle;
typedef struct Rectangle Rectangle;

struct ScatterPlotSeries;
typedef struct ScatterPlotSeries ScatterPlotSeries;

struct ScatterPlotSettings;
typedef struct ScatterPlotSettings ScatterPlotSettings;

struct BarPlotSeries;
typedef struct BarPlotSeries BarPlotSeries;

struct BarPlotSettings;
typedef struct BarPlotSettings BarPlotSettings;

struct RGBA;
typedef struct RGBA RGBA;

struct RGBABitmapImage;
typedef struct RGBABitmapImage RGBABitmapImage;

struct BooleanArrayReference;
typedef struct BooleanArrayReference BooleanArrayReference;

struct BooleanReference;
typedef struct BooleanReference BooleanReference;

struct CharacterReference;
typedef struct CharacterReference CharacterReference;

struct NumberArrayReference;
typedef struct NumberArrayReference NumberArrayReference;

struct NumberReference;
typedef struct NumberReference NumberReference;

struct StringArrayReference;
typedef struct StringArrayReference StringArrayReference;

struct StringReference;
typedef struct StringReference StringReference;

struct Chunk;
typedef struct Chunk Chunk;

struct IHDR;
typedef struct IHDR IHDR;

struct PHYS;
typedef struct PHYS PHYS;

struct PNGImage;
typedef struct PNGImage PNGImage;

struct DynamicArrayCharacters;
typedef struct DynamicArrayCharacters DynamicArrayCharacters;

struct LinkedListNodeStrings;
typedef struct LinkedListNodeStrings LinkedListNodeStrings;

struct LinkedListStrings;
typedef struct LinkedListStrings LinkedListStrings;

struct LinkedListNodeNumbers;
typedef struct LinkedListNodeNumbers LinkedListNodeNumbers;

struct LinkedListNumbers;
typedef struct LinkedListNumbers LinkedListNumbers;

struct LinkedListCharacters;
typedef struct LinkedListCharacters LinkedListCharacters;

struct LinkedListNodeCharacters;
typedef struct LinkedListNodeCharacters LinkedListNodeCharacters;

struct DynamicArrayNumbers;
typedef struct DynamicArrayNumbers DynamicArrayNumbers;

struct ByteArray;
typedef struct ByteArray ByteArray;

struct ByteArrayReference;
typedef struct ByteArrayReference ByteArrayReference;

struct ZLIBStruct;
typedef struct ZLIBStruct ZLIBStruct;

struct RGBABitmapImageReference{
  RGBABitmapImage *image;
};

struct Rectangle{
  double x1;
  double x2;
  double y1;
  double y2;
};

struct ScatterPlotSeries{
  _Bool linearInterpolation;
  wchar_t *pointType;
  size_t pointTypeLength;
  wchar_t *lineType;
  size_t lineTypeLength;
  double lineThickness;
  double *xs;
  size_t xsLength;
  double *ys;
  size_t ysLength;
  RGBA *color;
};

struct ScatterPlotSettings{
  ScatterPlotSeries **scatterPlotSeries;
  size_t scatterPlotSeriesLength;
  _Bool autoBoundaries;
  double xMax;
  double xMin;
  double yMax;
  double yMin;
  _Bool autoPadding;
  double xPadding;
  double yPadding;
  wchar_t *xLabel;
  size_t xLabelLength;
  wchar_t *yLabel;
  size_t yLabelLength;
  wchar_t *title;
  size_t titleLength;
  _Bool showGrid;
  RGBA *gridColor;
  _Bool xAxisAuto;
  _Bool xAxisTop;
  _Bool xAxisBottom;
  _Bool yAxisAuto;
  _Bool yAxisLeft;
  _Bool yAxisRight;
  double width;
  double height;
};

struct BarPlotSeries{
  double *ys;
  size_t ysLength;
  RGBA *color;
};

struct BarPlotSettings{
  double width;
  double height;
  _Bool autoBoundaries;
  double yMax;
  double yMin;
  _Bool autoPadding;
  double xPadding;
  double yPadding;
  wchar_t *title;
  size_t titleLength;
  _Bool showGrid;
  RGBA *gridColor;
  BarPlotSeries **barPlotSeries;
  size_t barPlotSeriesLength;
  wchar_t *yLabel;
  size_t yLabelLength;
  _Bool autoColor;
  _Bool grayscaleAutoColor;
  _Bool autoSpacing;
  double groupSeparation;
  double barSeparation;
  _Bool autoLabels;
  StringReference **xLabels;
  size_t xLabelsLength;
  _Bool barBorder;
};

struct RGBA{
  double r;
  double g;
  double b;
  double a;
};

struct RGBABitmapImage{
  uint32_t *pixels;
  size_t yLength;
  size_t xLength;
};

struct BooleanArrayReference{
  _Bool *booleanArray;
  size_t booleanArrayLength;
};

struct BooleanReference{
  _Bool booleanValue;
};

struct CharacterReference{
  wchar_t characterValue;
};

struct NumberArrayReference{
  double *numberArray;
  size_t numberArrayLength;
};

struct NumberReference{
  double numberValue;
};

struct StringArrayReference{
  StringReference **stringArray;
  size_t stringArrayLength;
};

struct StringReference{
  wchar_t *string;
  size_t stringLength;
};

struct Chunk{
  double length;
  wchar_t *type;
  size_t typeLength;
  ByteArray *data;
  double crc;
};

struct IHDR{
  double Width;
  double Height;
  double BitDepth;
  double ColourType;
  double CompressionMethod;
  double FilterMethod;
  double InterlaceMethod;
};

struct PHYS{
  double pixelsPerMeter;
};

struct PNGImage{
  double *signature;
  size_t signatureLength;
  IHDR *ihdr;
  ZLIBStruct *zlibStruct;
  _Bool physPresent;
  PHYS *phys;
};

struct DynamicArrayCharacters{
  wchar_t *array;
  size_t arrayLength;
  double length;
};

struct LinkedListNodeStrings{
  _Bool end;
  wchar_t *value;
  size_t valueLength;
  LinkedListNodeStrings *next;
};

struct LinkedListStrings{
  LinkedListNodeStrings *first;
  LinkedListNodeStrings *last;
};

struct LinkedListNodeNumbers{
  LinkedListNodeNumbers *next;
  _Bool end;
  double value;
};

struct LinkedListNumbers{
  LinkedListNodeNumbers *first;
  LinkedListNodeNumbers *last;
};

struct LinkedListCharacters{
  LinkedListNodeCharacters *first;
  LinkedListNodeCharacters *last;
};

struct LinkedListNodeCharacters{
  _Bool end;
  wchar_t value;
  LinkedListNodeCharacters *next;
};

struct DynamicArrayNumbers{
  double *array;
  size_t arrayLength;
  double length;
};

struct ByteArray{
  uint8_t *bytes;
  size_t bytesLength;
};

struct ByteArrayReference{
  ByteArray *bytes;
};

struct ZLIBStruct{
  double CMF;
  double CM;
  double CINFO;
  double FLG;
  double FCHECK;
  double FDICT;
  double FLEVEL;
  ByteArray *CompressedDataBlocks;
  double Adler32CheckValue;
};

_Bool Loess(double *xs, size_t xsLength, double *ys, size_t ysLength, double bandwidth, double robustnessIters, double accuracy, NumberArrayReference *resultXs, StringReference *errorMessage);
_Bool Lowess(double *xs, size_t xsLength, double *ys, size_t ysLength, double *weights, size_t weightsLength, double bandwidth, double robustnessIters, double accuracy, NumberArrayReference *resultXs, StringReference *errorMessage);
void RearrangeArray(double *as, size_t asLength, double *indexes, size_t indexesLength);
void AssignNumberArray(double *as, size_t asLength, double *bs, size_t bsLength);
double FindNextNonZeroElement(double *array, size_t arrayLength, double offset);
double Tricube(double x);

_Bool CropLineWithinBoundary(NumberReference *x1Ref, NumberReference *y1Ref, NumberReference *x2Ref, NumberReference *y2Ref, double xMin, double xMax, double yMin, double yMax);
double IncrementFromCoordinates(double x1, double y1, double x2, double y2);
double InterceptFromCoordinates(double x1, double y1, double x2, double y2);

RGBA **Get8HighContrastColors(size_t *returnArrayLength);

void DrawFilledRectangleWithBorder(RGBABitmapImage *image, double x, double y, double w, double h, RGBA *borderColor, RGBA *fillColor);
RGBABitmapImageReference *CreateRGBABitmapImageReference();

_Bool RectanglesOverlap(Rectangle *r1, Rectangle *r2);
Rectangle *CreateRectangle(double x1, double y1, double x2, double y2);
void CopyRectangleValues(Rectangle *rd, Rectangle *rs);

void DrawXLabelsForPriority(double p, double xMin, double oy, double xMax, double xPixelMin, double xPixelMax, NumberReference *nextRectangle, RGBA *gridLabelColor, RGBABitmapImage *canvas, double *xGridPositions, size_t xGridPositionsLength, StringArrayReference *xLabels, NumberArrayReference *xLabelPriorities, Rectangle **occupied, size_t occupiedLength, _Bool textOnBottom);
void DrawYLabelsForPriority(double p, double yMin, double ox, double yMax, double yPixelMin, double yPixelMax, NumberReference *nextRectangle, RGBA *gridLabelColor, RGBABitmapImage *canvas, double *yGridPositions, size_t yGridPositionsLength, StringArrayReference *yLabels, NumberArrayReference *yLabelPriorities, Rectangle **occupied, size_t occupiedLength, _Bool textOnLeft);
double *ComputeGridLinePositions(size_t *returnArrayLength, double cMin, double cMax, StringArrayReference *labels, NumberArrayReference *priorities);
double MapYCoordinate(double y, double yMin, double yMax, double yPixelMin, double yPixelMax);
double MapXCoordinate(double x, double xMin, double xMax, double xPixelMin, double xPixelMax);
double MapXCoordinateAutoSettings(double x, RGBABitmapImage *image, double *xs, size_t xsLength);
double MapYCoordinateAutoSettings(double y, RGBABitmapImage *image, double *ys, size_t ysLength);
double MapXCoordinateBasedOnSettings(double x, ScatterPlotSettings *settings);
double MapYCoordinateBasedOnSettings(double y, ScatterPlotSettings *settings);
double GetDefaultPaddingPercentage();

void DrawText(RGBABitmapImage *canvas, double x, double y, wchar_t *text, size_t textLength, RGBA *color);
void DrawTextUpwards(RGBABitmapImage *canvas, double x, double y, wchar_t *text, size_t textLength, RGBA *color);

ScatterPlotSettings *GetDefaultScatterPlotSettings();
ScatterPlotSeries *GetDefaultScatterPlotSeriesSettings();
_Bool DrawScatterPlot(RGBABitmapImageReference *canvasReference, double width, double height, double *xs, size_t xsLength, double *ys, size_t ysLength, StringReference *errorMessage);
_Bool DrawScatterPlotFromSettings(RGBABitmapImageReference *canvasReference, ScatterPlotSettings *settings, StringReference *errorMessage);
void ComputeBoundariesBasedOnSettings(ScatterPlotSettings *settings, Rectangle *boundaries);
_Bool ScatterPlotFromSettingsValid(ScatterPlotSettings *settings, StringReference *errorMessage);

BarPlotSettings *GetDefaultBarPlotSettings();
BarPlotSeries *GetDefaultBarPlotSeriesSettings();
RGBABitmapImage *DrawBarPlotNoErrorCheck(double width, double height, double *ys, size_t ysLength);
_Bool DrawBarPlot(RGBABitmapImageReference *canvasReference, double width, double height, double *ys, size_t ysLength, StringReference *errorMessage);
_Bool DrawBarPlotFromSettings(RGBABitmapImageReference *canvasReference, BarPlotSettings *settings, StringReference *errorMessage);
_Bool BarPlotSettingsIsValid(BarPlotSettings *settings, StringReference *errorMessage);

double GetMinimum(double *data, size_t dataLength);
double GetMaximum(double *data, size_t dataLength);

double RoundToDigits(double element, double digitsAfterPoint);

double test();
void TestMapping(NumberReference *failures);
void TestMapping2(NumberReference *failures);
void ExampleRegression(RGBABitmapImageReference *image);
void ExampleRegression2(RGBABitmapImageReference *image);
void BarPlotExample(RGBABitmapImageReference *imageReference);

RGBA *GetBlack();
RGBA *GetWhite();
RGBA *GetTransparent();
RGBA *GetGray(double percentage);
RGBA *CreateRGBColor(double r, double g, double b);
RGBA *CreateRGBAColor(double r, double g, double b, double a);

RGBABitmapImage *CreateImage(double w, double h, RGBA *color);
void DeleteImage(RGBABitmapImage *image);
double ImageWidth(RGBABitmapImage *image);
double ImageHeight(RGBABitmapImage *image);
void SetPixel(RGBABitmapImage *image, double x, double y, RGBA *color);
void DrawPixel(RGBABitmapImage *image, double x, double y, RGBA *color);
double CombineAlpha(double as, double ad);
double AlphaBlend(double cs, double as, double cd, double ad, double ao);
void DrawHorizontalLine1px(RGBABitmapImage *image, double x, double y, double length, RGBA *color);
void DrawVerticalLine1px(RGBABitmapImage *image, double x, double y, double height, RGBA *color);
void DrawRectangle1px(RGBABitmapImage *image, double x, double y, double width, double height, RGBA *color);
void DrawImageOnImage(RGBABitmapImage *dst, RGBABitmapImage *src, double topx, double topy);
void DrawLine1px(RGBABitmapImage *image, double x0, double y0, double x1, double y1, RGBA *color);
void XiaolinWusLineAlgorithm(RGBABitmapImage *image, double x0, double y0, double x1, double y1, RGBA *color);
double OneMinusFractionalPart(double x);
double FractionalPart(double x);
RGBA *SetBrightness(RGBA *color, double newBrightness);
void DrawQuadraticBezierCurve(RGBABitmapImage *image, double x0, double y0, double cx, double cy, double x1, double y1, RGBA *color);
void QuadraticBezierPoint(double x0, double y0, double cx, double cy, double x1, double y1, double t, NumberReference *x, NumberReference *y);
void DrawCubicBezierCurve(RGBABitmapImage *image, double x0, double y0, double c0x, double c0y, double c1x, double c1y, double x1, double y1, RGBA *color);
void CubicBezierPoint(double x0, double y0, double c0x, double c0y, double c1x, double c1y, double x1, double y1, double t, NumberReference *x, NumberReference *y);
RGBABitmapImage *CopyImage(RGBABitmapImage *image);
RGBA *GetImagePixel(RGBABitmapImage *image, double x, double y);
RGBA GetImagePixelStruct(RGBABitmapImage *image, double x, double y);
void HorizontalFlip(RGBABitmapImage *img);
void DrawFilledRectangle(RGBABitmapImage *image, double x, double y, double w, double h, RGBA *color);
RGBABitmapImage *RotateAntiClockwise90Degrees(RGBABitmapImage *image);
void DrawCircle(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color);
void BresenhamsCircleDrawingAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color);
void DrawCircleMidpointAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color);
void DrawCircleBasicAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color);
void DrawFilledCircle(RGBABitmapImage *canvas, double x, double y, double r, RGBA *color);
void DrawFilledCircleMidpointAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color);
void DrawFilledCircleBasicAlgorithm(RGBABitmapImage *canvas, double xCenter, double yCenter, double radius, RGBA *color);
void DrawTriangle(RGBABitmapImage *canvas, double xCenter, double yCenter, double height, RGBA *color);
void DrawFilledTriangle(RGBABitmapImage *canvas, double xCenter, double yCenter, double height, RGBA *color);
void DrawLine(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, double thickness, RGBA *color);
void DrawLineBresenhamsAlgorithmThick(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, double thickness, RGBA *color);
void DrawLineBresenhamsAlgorithm(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, RGBA *color);
void DrawLineBresenhamsAlgorithmThickPatterned(RGBABitmapImage *canvas, double x1, double y1, double x2, double y2, double thickness, _Bool *pattern, size_t patternLength, NumberReference *offset, RGBA *color);

_Bool *GetLinePattern5(size_t *returnArrayLength);
_Bool *GetLinePattern4(size_t *returnArrayLength);
_Bool *GetLinePattern3(size_t *returnArrayLength);
_Bool *GetLinePattern2(size_t *returnArrayLength);
_Bool *GetLinePattern1(size_t *returnArrayLength);

RGBABitmapImage *Blur(RGBABitmapImage *src, double pixels);
RGBA *CreateBlurForPoint(RGBABitmapImage *src, double x, double y, double pixels);

wchar_t *CreateStringScientificNotationDecimalFromNumber(size_t *returnArrayLength, double decimal);
wchar_t *CreateStringDecimalFromNumber(size_t *returnArrayLength, double decimal);
_Bool CreateStringFromNumberWithCheck(double decimal, double base, StringReference *stringReference);
double GetMaximumDigitsForBase(double base);
double GetFirstDigitPosition(double decimal, double base);
_Bool GetSingleDigitCharacterFromNumberWithCheck(double c, double base, CharacterReference *characterReference);
wchar_t *GetDigitCharacterTable(size_t *returnArrayLength);

_Bool CreateNumberFromDecimalStringWithCheck(wchar_t *string, size_t stringLength, NumberReference *decimalReference, StringReference *errorMessage);
double CreateNumberFromDecimalString(wchar_t *string, size_t stringLength);
_Bool CreateNumberFromStringWithCheck(wchar_t *string, size_t stringLength, double base, NumberReference *numberReference, StringReference *errorMessage);
double CreateNumberFromParts(double base, _Bool numberIsPositive, double *beforePoint, size_t beforePointLength, double *afterPoint, size_t afterPointLength, _Bool exponentIsPositive, double *exponent, size_t exponentLength);
_Bool ExtractPartsFromNumberString(wchar_t *n, size_t nLength, double base, BooleanReference *numberIsPositive, NumberArrayReference *beforePoint, NumberArrayReference *afterPoint, BooleanReference *exponentIsPositive, NumberArrayReference *exponent, StringReference *errorMessages);
double GetNumberFromNumberCharacterForBase(wchar_t c, double base);
_Bool CharacterIsNumberCharacterInBase(wchar_t c, double base);
double *StringToNumberArray(size_t *returnArrayLength, wchar_t *str, size_t strLength);
_Bool StringToNumberArrayWithCheck(wchar_t *str, size_t strLength, NumberArrayReference *numberArrayReference, StringReference *errorMessage);

void QuickSortStrings(StringArrayReference *list);
void QuickSortStringsBounds(StringArrayReference *A, double lo, double hi);
double QuickSortStringsPartition(StringArrayReference *A, double lo, double hi);
double *QuickSortStringsWithIndexes(size_t *returnArrayLength, StringArrayReference *A);
void QuickSortStringsBoundsWithIndexes(StringArrayReference *A, double *indexes, size_t indexesLength, double lo, double hi);
double QuickSortStringsPartitionWithIndexes(StringArrayReference *A, double *indexes, size_t indexesLength, double lo, double hi);

void QuickSortNumbers(double *list, size_t listLength);
void QuickSortNumbersBounds(double *A, size_t ALength, double lo, double hi);
double QuickSortNumbersPartition(double *A, size_t ALength, double lo, double hi);
double *QuickSortNumbersWithIndexes(size_t *returnArrayLength, double *A, size_t ALength);
void QuickSortNumbersBoundsWithIndexes(double *A, size_t ALength, double *indexes, size_t indexesLength, double lo, double hi);
double QuickSortNumbersPartitionWithIndexes(double *A, size_t ALength, double *indexes, size_t indexesLength, double lo, double hi);

double Negate(double x);
double Positive(double x);
double Factorial(double x);
double Round(double x);
double BankersRound(double x);
double Ceil(double x);
double Floor(double x);
double Truncate(double x);
double Absolute(double x);
double Logarithm(double x);
double NaturalLogarithm(double x);
double Sin(double x);
double Cos(double x);
double Tan(double x);
double Asin(double x);
double Acos(double x);
double Atan(double x);
double Atan2(double y, double x);
double Squareroot(double x);
double Exp(double x);
_Bool DivisibleBy(double a, double b);
double Combinations(double n, double k);
double Permutations(double n, double k);
_Bool EpsilonCompare(double a, double b, double epsilon);
double GreatestCommonDivisor(double a, double b);
double GCDWithSubtraction(double a, double b);
_Bool IsInteger(double a);
_Bool GreatestCommonDivisorWithCheck(double a, double b, NumberReference *gcdReference);
double LeastCommonMultiple(double a, double b);
double Sign(double a);
double Max(double a, double b);
double Min(double a, double b);
double Power(double a, double b);
double Gamma(double x);
double LogGamma(double x);
double LanczosApproximation(double z);
double Beta(double x, double y);
double Sinh(double x);
double Cosh(double x);
double Tanh(double x);
double Cot(double x);
double Sec(double x);
double Csc(double x);
double Coth(double x);
double Sech(double x);
double Csch(double x);
double Error(double x);
double ErrorInverse(double x);
double FallingFactorial(double x, double n);
double RisingFactorial(double x, double n);
double Hypergeometric(double a, double b, double c, double z, double maxIterations, double precision);
double HypergeometricDirect(double a, double b, double c, double z, double maxIterations, double precision);
double BernouilliNumber(double n);
double AkiyamaTanigawaAlgorithm(double n);

double *aStringToNumberArray(size_t *returnArrayLength, wchar_t *string, size_t stringLength);
wchar_t *aNumberArrayToString(size_t *returnArrayLength, double *array, size_t arrayLength);
_Bool aNumberArraysEqual(double *a, size_t aLength, double *b, size_t bLength);
_Bool aBooleanArraysEqual(_Bool *a, size_t aLength, _Bool *b, size_t bLength);
_Bool aStringsEqual(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength);
void aFillNumberArray(double *a, size_t aLength, double value);
void aFillString(wchar_t *a, size_t aLength, wchar_t value);
void aFillBooleanArray(_Bool *a, size_t aLength, _Bool value);
_Bool aFillNumberArrayRange(double *a, size_t aLength, double value, double from, double to);
_Bool aFillBooleanArrayRange(_Bool *a, size_t aLength, _Bool value, double from, double to);
_Bool aFillStringRange(wchar_t *a, size_t aLength, wchar_t value, double from, double to);
double *aCopyNumberArray(size_t *returnArrayLength, double *a, size_t aLength);
_Bool *aCopyBooleanArray(size_t *returnArrayLength, _Bool *a, size_t aLength);
wchar_t *aCopyString(size_t *returnArrayLength, wchar_t *a, size_t aLength);
_Bool aCopyNumberArrayRange(double *a, size_t aLength, double from, double to, NumberArrayReference *copyReference);
_Bool aCopyBooleanArrayRange(_Bool *a, size_t aLength, double from, double to, BooleanArrayReference *copyReference);
_Bool aCopyStringRange(wchar_t *a, size_t aLength, double from, double to, StringReference *copyReference);
_Bool aIsLastElement(double length, double index);
double *aCreateNumberArray(size_t *returnArrayLength, double length, double value);
_Bool *aCreateBooleanArray(size_t *returnArrayLength, double length, _Bool value);
wchar_t *aCreateString(size_t *returnArrayLength, double length, wchar_t value);
void aSwapElementsOfNumberArray(double *A, size_t ALength, double ai, double bi);
void aSwapElementsOfStringArray(StringArrayReference *A, double ai, double bi);
void aReverseNumberArray(double *array, size_t arrayLength);


BooleanReference *CreateBooleanReference(_Bool value);
BooleanArrayReference *CreateBooleanArrayReference(_Bool *value, size_t valueLength);
BooleanArrayReference *CreateBooleanArrayReferenceLengthValue(double length, _Bool value);
void FreeBooleanArrayReference(BooleanArrayReference *booleanArrayReference);
CharacterReference *CreateCharacterReference(wchar_t value);
NumberReference *CreateNumberReference(double value);
NumberArrayReference *CreateNumberArrayReference(double *value, size_t valueLength);
NumberArrayReference *CreateNumberArrayReferenceLengthValue(double length, double value);
void FreeNumberArrayReference(NumberArrayReference *numberArrayReference);
StringReference *CreateStringReference(wchar_t *value, size_t valueLength);
StringReference *CreateStringReferenceLengthValue(double length, wchar_t value);
void FreeStringReference(StringReference *stringReference);
StringArrayReference *CreateStringArrayReference(StringReference **strings, size_t stringsLength);
StringArrayReference *CreateStringArrayReferenceLengthValue(double length, wchar_t *value, size_t valueLength);
void FreeStringArrayReference(StringArrayReference *stringArrayReference);

wchar_t *DigitDataBase16(size_t *returnArrayLength);
void DrawDigitCharacter(RGBABitmapImage *image, double topx, double topy, double digit);

wchar_t *GetPixelFontData(size_t *returnArrayLength);
void DrawAsciiCharacter(RGBABitmapImage *image, double topx, double topy, wchar_t a, RGBA *color);
double GetTextWidth(wchar_t *text, size_t textLength);
double GetTextHeight(wchar_t *text, size_t textLength);

void AssertFalse(_Bool b, NumberReference *failures);
void AssertTrue(_Bool b, NumberReference *failures);
void AssertEquals(double a, double b, NumberReference *failures);
void AssertBooleansEqual(_Bool a, _Bool b, NumberReference *failures);
void AssertCharactersEqual(wchar_t a, wchar_t b, NumberReference *failures);
void AssertStringEquals(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength, NumberReference *failures);
void AssertNumberArraysEqual(double *a, size_t aLength, double *b, size_t bLength, NumberReference *failures);
void AssertBooleanArraysEqual(_Bool *a, size_t aLength, _Bool *b, size_t bLength, NumberReference *failures);
void AssertStringArraysEqual(StringReference **a, size_t aLength, StringReference **b, size_t bLength, NumberReference *failures);

ByteArray *ConvertToPNG(RGBABitmapImage *image);
ByteArray *ConvertToPNGGrayscale(RGBABitmapImage *image);
PHYS *PysicsHeader(double pixelsPerMeter);
ByteArray *ConvertToPNGWithOptions(RGBABitmapImage *image, double colorType, _Bool setPhys, double pixelsPerMeter, double compressionLevel);
ByteArray *PNGSerializeChunks(PNGImage *png);
double PNGIDATLength(PNGImage *png);
double PNGHeaderLength();
ByteArray *GetPNGColorData(RGBABitmapImage *image);
ByteArray *GetPNGColorDataGreyscale(RGBABitmapImage *image);
IHDR *PNGHeader(RGBABitmapImage *image, double colortype);
double *PNGSignature(size_t *returnArrayLength);
double *PNGReadDataChunks(size_t *returnArrayLength, Chunk **cs, size_t csLength);
_Bool PNGReadHeader(RGBABitmapImage *image, Chunk **cs, size_t csLength, StringReference *errorMessages);
Chunk **PNGReadChunks(size_t *returnArrayLength, ByteArray *data, NumberReference *position);
Chunk *PNGReadChunk(ByteArray *data, NumberReference *position);

void WriteStringToStingStream(wchar_t *stream, size_t streamLength, NumberReference *index, wchar_t *src, size_t srcLength);
void WriteCharacterToStingStream(wchar_t *stream, size_t streamLength, NumberReference *index, wchar_t src);
void WriteBooleanToStingStream(wchar_t *stream, size_t streamLength, NumberReference *index, _Bool src);

_Bool SubstringWithCheck(wchar_t *string, size_t stringLength, double from, double to, StringReference *stringReference);
wchar_t *Substring(size_t *returnArrayLength, wchar_t *string, size_t stringLength, double from, double to);
wchar_t *AppendString(size_t *returnArrayLength, wchar_t *s1, size_t s1Length, wchar_t *s2, size_t s2Length);
wchar_t *ConcatenateString(size_t *returnArrayLength, wchar_t *s1, size_t s1Length, wchar_t *s2, size_t s2Length);
wchar_t *AppendCharacter(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t c);
wchar_t *ConcatenateCharacter(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t c);
StringReference **SplitByCharacter(size_t *returnArrayLength, wchar_t *toSplit, size_t toSplitLength, wchar_t splitBy);
_Bool IndexOfCharacter(wchar_t *string, size_t stringLength, wchar_t character, NumberReference *indexReference);
_Bool SubstringEqualsWithCheck(wchar_t *string, size_t stringLength, double from, wchar_t *substring, size_t substringLength, BooleanReference *equalsReference);
_Bool SubstringEquals(wchar_t *string, size_t stringLength, double from, wchar_t *substring, size_t substringLength);
_Bool IndexOfString(wchar_t *string, size_t stringLength, wchar_t *substring, size_t substringLength, NumberReference *indexReference);
_Bool ContainsCharacter(wchar_t *string, size_t stringLength, wchar_t character);
_Bool ContainsString(wchar_t *string, size_t stringLength, wchar_t *substring, size_t substringLength);
void ToUpperCase(wchar_t *string, size_t stringLength);
void ToLowerCase(wchar_t *string, size_t stringLength);
_Bool EqualsIgnoreCase(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength);
wchar_t *ReplaceString(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t *toReplace, size_t toReplaceLength, wchar_t *replaceWith, size_t replaceWithLength);
wchar_t *ReplaceCharacterToNew(size_t *returnArrayLength, wchar_t *string, size_t stringLength, wchar_t toReplace, wchar_t replaceWith);
void ReplaceCharacter(wchar_t *string, size_t stringLength, wchar_t toReplace, wchar_t replaceWith);
wchar_t *Trim(size_t *returnArrayLength, wchar_t *string, size_t stringLength);
_Bool StartsWith(wchar_t *string, size_t stringLength, wchar_t *start, size_t startLength);
_Bool EndsWith(wchar_t *string, size_t stringLength, wchar_t *end, size_t endLength);
StringReference **SplitByString(size_t *returnArrayLength, wchar_t *toSplit, size_t toSplitLength, wchar_t *splitBy, size_t splitByLength);
_Bool StringIsBefore(wchar_t *a, size_t aLength, wchar_t *b, size_t bLength);

double *AddNumber(size_t *returnArrayLength, double *list, size_t listLength, double a);
void AddNumberRef(NumberArrayReference *list, double i);
double *RemoveNumber(size_t *returnArrayLength, double *list, size_t listLength, double n);
double GetNumberRef(NumberArrayReference *list, double i);
void RemoveNumberRef(NumberArrayReference *list, double i);

StringReference **AddString(size_t *returnArrayLength, StringReference **list, size_t listLength, StringReference *a);
void AddStringRef(StringArrayReference *list, StringReference *i);
StringReference **RemoveString(size_t *returnArrayLength, StringReference **list, size_t listLength, double n);
StringReference *GetStringRef(StringArrayReference *list, double i);
void RemoveStringRef(StringArrayReference *list, double i);


DynamicArrayCharacters *CreateDynamicArrayCharacters();
DynamicArrayCharacters *CreateDynamicArrayCharactersWithInitialCapacity(double capacity);
void DynamicArrayAddCharacter(DynamicArrayCharacters *da, wchar_t value);
void DynamicArrayCharactersIncreaseSize(DynamicArrayCharacters *da);
_Bool DynamicArrayCharactersDecreaseSizeNecessary(DynamicArrayCharacters *da);
void DynamicArrayCharactersDecreaseSize(DynamicArrayCharacters *da);
double DynamicArrayCharactersIndex(DynamicArrayCharacters *da, double index);
double DynamicArrayCharactersLength(DynamicArrayCharacters *da);
void DynamicArrayInsertCharacter(DynamicArrayCharacters *da, double index, wchar_t value);
_Bool DynamicArrayCharacterSet(DynamicArrayCharacters *da, double index, wchar_t value);
void DynamicArrayRemoveCharacter(DynamicArrayCharacters *da, double index);
void FreeDynamicArrayCharacters(DynamicArrayCharacters *da);
wchar_t *DynamicArrayCharactersToArray(size_t *returnArrayLength, DynamicArrayCharacters *da);
DynamicArrayCharacters *ArrayToDynamicArrayCharactersWithOptimalSize(wchar_t *array, size_t arrayLength);
DynamicArrayCharacters *ArrayToDynamicArrayCharacters(wchar_t *array, size_t arrayLength);
_Bool DynamicArrayCharactersEqual(DynamicArrayCharacters *a, DynamicArrayCharacters *b);
LinkedListCharacters *DynamicArrayCharactersToLinkedList(DynamicArrayCharacters *da);
DynamicArrayCharacters *LinkedListToDynamicArrayCharacters(LinkedListCharacters *ll);

_Bool *AddBoolean(size_t *returnArrayLength, _Bool *list, size_t listLength, _Bool a);
void AddBooleanRef(BooleanArrayReference *list, _Bool i);
_Bool *RemoveBoolean(size_t *returnArrayLength, _Bool *list, size_t listLength, double n);
_Bool GetBooleanRef(BooleanArrayReference *list, double i);
void RemoveDecimalRef(BooleanArrayReference *list, double i);


LinkedListStrings *CreateLinkedListString();
void LinkedListAddString(LinkedListStrings *ll, wchar_t *value, size_t valueLength);
StringReference **LinkedListStringsToArray(size_t *returnArrayLength, LinkedListStrings *ll);
double LinkedListStringsLength(LinkedListStrings *ll);
void FreeLinkedListString(LinkedListStrings *ll);


LinkedListNumbers *CreateLinkedListNumbers();
LinkedListNumbers **CreateLinkedListNumbersArray(size_t *returnArrayLength, double length);
void LinkedListAddNumber(LinkedListNumbers *ll, double value);
double LinkedListNumbersLength(LinkedListNumbers *ll);
double LinkedListNumbersIndex(LinkedListNumbers *ll, double index);
void LinkedListInsertNumber(LinkedListNumbers *ll, double index, double value);
void LinkedListSet(LinkedListNumbers *ll, double index, double value);
void LinkedListRemoveNumber(LinkedListNumbers *ll, double index);
void FreeLinkedListNumbers(LinkedListNumbers *ll);
void FreeLinkedListNumbersArray(LinkedListNumbers **lls, size_t llsLength);
double *LinkedListNumbersToArray(size_t *returnArrayLength, LinkedListNumbers *ll);
LinkedListNumbers *ArrayToLinkedListNumbers(double *array, size_t arrayLength);
_Bool LinkedListNumbersEqual(LinkedListNumbers *a, LinkedListNumbers *b);

LinkedListCharacters *CreateLinkedListCharacter();
void LinkedListAddCharacter(LinkedListCharacters *ll, wchar_t value);
wchar_t *LinkedListCharactersToArray(size_t *returnArrayLength, LinkedListCharacters *ll);
double LinkedListCharactersLength(LinkedListCharacters *ll);
void FreeLinkedListCharacter(LinkedListCharacters *ll);
void LinkedListCharactersAddString(LinkedListCharacters *ll, wchar_t *str, size_t strLength);



DynamicArrayNumbers *CreateDynamicArrayNumbers();
DynamicArrayNumbers *CreateDynamicArrayNumbersWithInitialCapacity(double capacity);
void DynamicArrayAddNumber(DynamicArrayNumbers *da, double value);
void DynamicArrayNumbersIncreaseSize(DynamicArrayNumbers *da);
_Bool DynamicArrayNumbersDecreaseSizeNecessary(DynamicArrayNumbers *da);
void DynamicArrayNumbersDecreaseSize(DynamicArrayNumbers *da);
double DynamicArrayNumbersIndex(DynamicArrayNumbers *da, double index);
double DynamicArrayNumbersLength(DynamicArrayNumbers *da);
void DynamicArrayInsertNumber(DynamicArrayNumbers *da, double index, double value);
_Bool DynamicArrayNumberSet(DynamicArrayNumbers *da, double index, double value);
void DynamicArrayRemoveNumber(DynamicArrayNumbers *da, double index);
void FreeDynamicArrayNumbers(DynamicArrayNumbers *da);
double *DynamicArrayNumbersToArray(size_t *returnArrayLength, DynamicArrayNumbers *da);
DynamicArrayNumbers *ArrayToDynamicArrayNumbersWithOptimalSize(double *array, size_t arrayLength);
DynamicArrayNumbers *ArrayToDynamicArrayNumbers(double *array, size_t arrayLength);
_Bool DynamicArrayNumbersEqual(DynamicArrayNumbers *a, DynamicArrayNumbers *b);
LinkedListNumbers *DynamicArrayNumbersToLinkedList(DynamicArrayNumbers *da);
DynamicArrayNumbers *LinkedListToDynamicArrayNumbers(LinkedListNumbers *ll);
double DynamicArrayNumbersIndexOf(DynamicArrayNumbers *arr, double n, BooleanReference *foundReference);
_Bool DynamicArrayNumbersIsInArray(DynamicArrayNumbers *arr, double n);

wchar_t *AddCharacter(size_t *returnArrayLength, wchar_t *list, size_t listLength, wchar_t a);
void AddCharacterRef(StringReference *list, wchar_t i);
wchar_t *RemoveCharacter(size_t *returnArrayLength, wchar_t *list, size_t listLength, double n);
wchar_t GetCharacterRef(StringReference *list, double i);
void RemoveCharacterRef(StringReference *list, double i);

ByteArray *ReadXbytes(ByteArray *data, NumberReference *position, double length);
double Read4bytesBE(ByteArray *data, NumberReference *position);
double Read2bytesBE(ByteArray *data, NumberReference *position);
double ReadByte(ByteArray *data, NumberReference *position);
double Read4bytesLE(ByteArray *data, NumberReference *position);
void WriteByte(ByteArray *data, double b, NumberReference *position);
void Write2BytesLE(ByteArray *data, double b, NumberReference *position);
void Write4BytesLE(ByteArray *data, double b, NumberReference *position);
void Write2BytesBE(ByteArray *data, double b, NumberReference *position);
void Write4BytesBE(ByteArray *data, double b, NumberReference *position);
void WriteStringBytes(ByteArray *data, wchar_t *cs, size_t csLength, NumberReference *position);
double BytesRound(double x);
double *ByteArrayToNumberArray(size_t *returnArrayLength, ByteArray *src);
ByteArray *NumberArrayToByteArray(double *src, size_t srcLength);
_Bool ByteArraysEqual(ByteArray *a, ByteArray *b);
ByteArray *CopyByteArray(ByteArray *a);
double ByteArrayLength(ByteArray *response);
ByteArray *CreateAndFillByteArray(double length, double value);
ByteArray *CreateByteArray(double length);
void SetByte(ByteArray *array, double index, double value);
double GetByte(ByteArray *array, double index);
void AssertByteArraysEqual(ByteArray *a, ByteArray *b, NumberReference *failures);
void FreeByteArray(ByteArray *byteArray);
_Bool CopyByteArrayRange(ByteArray *a, double from, double to, ByteArray *b);

wchar_t *BytesToTextBase16(size_t *returnArrayLength, double *bytes, size_t bytesLength);
double *TextToBytesBase16(size_t *returnArrayLength, wchar_t *string, size_t stringLength);
void FreeBase64ByteCombinations(StringReference **comb, size_t combLength);
StringReference **GenerateBase16ByteCombinations(size_t *returnArrayLength);

double *MakeCRC32Table(size_t *returnArrayLength);
double UpdateCRC32(double crc, ByteArray *buf, double *crc_table, size_t crc_tableLength);
double CalculateCRC32(ByteArray *buf);
double CRC32OfInterval(ByteArray *data, double from, double length);

ZLIBStruct *ZLibCompressNoCompression(ByteArray *data);
ZLIBStruct *ZLibCompressStaticHuffman(ByteArray *data, double level);

wchar_t charToLowerCase(wchar_t character);
wchar_t charToUpperCase(wchar_t character);
_Bool charIsUpperCase(wchar_t character);
_Bool charIsLowerCase(wchar_t character);
_Bool charIsLetter(wchar_t character);
_Bool charIsNumber(wchar_t character);
_Bool charIsWhiteSpace(wchar_t character);
_Bool charIsSymbol(wchar_t character);
_Bool charCharacterIsBefore(wchar_t a, wchar_t b);
wchar_t charDecimalDigitToCharacter(double digit);
double charCharacterToDecimalDigit(wchar_t c);

double And4Byte(double n1, double n2);
double And2Byte(double n1, double n2);
double AndByte(double n1, double n2);
double AndBytes(double n1, double n2, double bytes);
double Or4Byte(double n1, double n2);
double Or2Byte(double n1, double n2);
double OrByte(double n1, double n2);
double OrBytes(double n1, double n2, double bytes);
double Xor4Byte(double n1, double n2);
double Xor2Byte(double n1, double n2);
double XorByte(double n1, double n2);
double XorBytes(double n1, double n2, double bytes);
double Not4Byte(double b);
double Not2Byte(double b);
double NotByte(double b);
double NotBytes(double b, double length);
double ShiftLeft4Byte(double b, double amount);
double ShiftLeft2Byte(double b, double amount);
double ShiftLeftByte(double b, double amount);
double ShiftLeftBytes(double b, double amount, double length);
double ShiftRight4Byte(double b, double amount);
double ShiftRight2Byte(double b, double amount);
double ShiftRightByte(double b, double amount);
double ShiftRightBytes(double b, double amount, double length);
double RotateLeft4Byte(double w, double n);
double RotateRight4Bytes(double w, double n);

_Bool *CreateBooleanArrayFromNumber(size_t *returnArrayLength, double w, double size);
double BooleanArrayToNumber(_Bool *bits, size_t bitsLength);
_Bool *BooleanAnd(size_t *returnArrayLength, _Bool *a, size_t aLength, _Bool *b, size_t bLength);
_Bool *BooleanXor(size_t *returnArrayLength, _Bool *a, size_t aLength, _Bool *b, size_t bLength);
_Bool *BooleanNot(size_t *returnArrayLength, _Bool *a, size_t aLength);
_Bool *ShiftBitsRight4Byte(size_t *returnArrayLength, _Bool *w, size_t wLength, double n);

double ReadNextBit(double *data, size_t dataLength, NumberReference *nextbit);
double BitExtract(double b, double fromInc, double toInc);
double ReadBitRange(double *data, size_t dataLength, NumberReference *nextbit, double length);
void SkipToBoundary(NumberReference *nextbit);
double ReadNextByteBoundary(double *data, size_t dataLength, NumberReference *nextbit);
double Read2bytesByteBoundary(double *data, size_t dataLength, NumberReference *nextbit);

double ComputeAdler32(ByteArray *data);

ByteArray *Pack(ByteArray *data, double level);
ByteArray *Unpack(ByteArray *data);

ByteArray *DeflateDataStaticHuffman(ByteArray *data, double level);
void FindMatch(ByteArray *data, double pos, NumberReference *distanceReference, NumberReference *lengthReference, BooleanReference *match, double level);
double *GenerateBitReverseLookupTable(size_t *returnArrayLength, double bits);
double ReverseBits(double x, double bits);
ByteArray *DeflateDataNoCompression(ByteArray *data);
void GetDeflateStaticHuffmanCode(double b, NumberReference *code, NumberReference *length, double *bitReverseLookupTable, size_t bitReverseLookupTableLength);
void GetDeflateLengthCode(double length, NumberReference *code, NumberReference *lengthAddition, NumberReference *lengthAdditionLength);
void GetDeflateDistanceCode(double distance, NumberReference *code, NumberReference *distanceAdditionReference, NumberReference *distanceAdditionLengthReference, double *bitReverseLookupTable, size_t bitReverseLookupTableLength);
void AppendBitsToBytesLeft(ByteArray *bytes, NumberReference *nextbit, double data, double length);
void AppendBitsToBytesRight(ByteArray *bytes, NumberReference *nextbit, double data, double length);
