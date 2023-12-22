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
	double t1, t2;
	float f;
	__m128 A, B, O;
	__m128i X, Y, Z;
	__m128 F;

	// Broadcast
  f = 0.00392156862f;
	//F = __builtin_ia32_vbroadcastss(&f);
	//F = _mm_insert_ps(F, f, 0);
	//F = _mm_insert_ps(F, f, 1);
	//F = _mm_insert_ps(F, f, 2);
	//F = _mm_insert_ps(F, f, 3);
	//F[0] = f;
	//F[1] = f;
	//F[2] = f;
	//F[3] = f;
	F = _mm_set_ps1(f);

	// Intel
	// https://www.intel.com/content/www/us/en/docs/intrinsics-guide/index.html
	// https://stackoverflow.com/questions/11759791/is-it-possible-to-cast-floats-directly-to-m128-if-they-are-16-byte-aligned

	// GCC
	// https://gcc.gnu.org/onlinedocs/gcc/Vector-Extensions.html
	// https://gcc.gnu.org/onlinedocs/gcc/x86-Built-in-Functions.html

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

		// byte[4] -> float[4]
		//B[0] = oldColor;
		//B[1] = 0;
		//_mm_unpacklo_epi8();
		//__builtin_ia32_cvtdq2ps();
		X = _mm_setzero_si128();                // X = All zeros
		X = _mm_insert_epi32(X, oldColor, 0);   // X[0] = oldColor 
		X = _mm_cvtepu8_epi16(X);               // Cast B -> W
		X = _mm_cvtepu16_epi32(X);              // Cast W -> D
		A = _mm_cvt_pi2ps(X);

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

    r = Round(newColor.r * 255);
    g = Round(newColor.g * 255);
    b = Round(newColor.b * 255);
    a = Round(newColor.a * 255);

    image->pixels[pixel] = (r << 24) | (g << 16) | (b << 8) | a;
  }
}
double CombineAlpha(double as, double ad){
  return as + ad*(1.0 - as);
}
double AlphaBlend(double cs, double as, double cd, double ad, double ao){
  return (cs*as + cd*ad*(1.0 - as))/ao;
}
