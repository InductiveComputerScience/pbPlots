#include "pbPlots.h"

#include <immintrin.h>

void stosd(void *p, long long count, long long val);

RGBABitmapImage *CreateImage(double w, double h, RGBA *color){
  RGBABitmapImage *image;
  double i, j;
	int c, r, g, b, a;

  image = (RGBABitmapImage *)Allocate(sizeof(RGBABitmapImage), 1);
  image->pixels = (uint32_t*)Allocate(sizeof(uint32_t) * w * h, 1);
  image->xLength = w;
  image->yLength = h;

  r = Round(color->r * 255);
  g = Round(color->g * 255);
  b = Round(color->b * 255);
  a = Round(color->a * 255);

  c = (r << 24) | (g << 16) | (b << 8) | a;

	stosd(image->pixels, w*h, c);

  return image;
}
void SetPixel(RGBABitmapImage *image, double x, double y, RGBA *color){
  if(x >= 0.0 && x < image->xLength && y >= 0.0 && y < image->yLength){
    int pixel = x + y * image->xLength;
    int r = Round(color->r * 255);
    int g = Round(color->g * 255);
    int b = Round(color->b * 255);
    int a = Round(color->a * 255);

    image->pixels[pixel] = (r << 24) | (g << 16) | (b << 8) | a;
  }
}
double Round(double x){
  return floor(x + 0.5);
}

//typedef double v2df __attribute__ ((vector_size (2)));

void DrawPixel(RGBABitmapImage *image, double x, double y, RGBA *color){
  double ra, ga, ba, aa;
  double rb, gb, bb, ab;
  double ro, go, bo, ao;
  RGBA newColor;
  uint32_t oldColor;
  int pixel;
	int r, g, b, a;
	double t1, t2, f;
	__m128d A, B, C;

  f = 0.00392156862;

	// https://www.intel.com/content/www/us/en/docs/intrinsics-guide/index.html

  if(x >= 0.0 && x < image->xLength && y >= 0.0 && y < image->yLength){
    ra = color->r;
    ga = color->g;
    ba = color->b;
    aa = color->a;

    pixel = x + y * image->xLength;
    oldColor  = image->pixels[pixel];
    rb = ((oldColor >> 24) & 0xFF) * f;
    gb = ((oldColor >> 16) & 0xFF) * f;
    bb = ((oldColor >> 8) & 0xFF) * f;
    ab = ((oldColor >> 0) & 0xFF) * f;

    t1 = ab*(1.0 - aa);

    ao = aa + t1;

    t2 = 1/ao;

    ro = (ra*aa + rb*t1)*t2;
    go = (ga*aa + gb*t1)*t2;
    bo = (ba*aa + bb*t1)*t2;
    
    newColor.r = ro;
    newColor.g = go;
    newColor.b = bo;
    newColor.a = ao;

    //r = Round(newColor.r * 255);
    //g = Round(newColor.g * 255);
    //b = Round(newColor.b * 255);
    //a = Round(newColor.a * 255);

		A[0] = 0.0;
		A[1] = 0.0;
		B[0] = newColor.r * 255;
		B[1] = 0.0;
		C = _mm_round_sd(A, B, _MM_FROUND_TO_NEAREST_INT);
		r = C[0];
		B[0] = newColor.g * 255;
		C = _mm_round_sd(A, B, _MM_FROUND_TO_NEAREST_INT);
		g = C[0];
		B[0] = newColor.b * 255;
		C = _mm_round_sd(A, B, _MM_FROUND_TO_NEAREST_INT);
		b = C[0];
		B[0] = newColor.a * 255;
		C = _mm_round_sd(A, B, _MM_FROUND_TO_NEAREST_INT);
		a = C[0];

    image->pixels[pixel] = (r << 24) | (g << 16) | (b << 8) | a;
  }
}
double CombineAlpha(double as, double ad){
  return as + ad*(1.0 - as);
}
double AlphaBlend(double cs, double as, double cd, double ad, double ao){
  return (cs*as + cd*ad*(1.0 - as))/ao;
}
