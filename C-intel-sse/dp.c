#include "pbPlots.h"

// SSE4.2
#include <nmmintrin.h>

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
  __m128 C;
  __m128i X, Z;
  __m128 IF;

  if(x >= 0.0 && x < image->xLength && y >= 0.0 && y < image->yLength){
    int pixel = x + y * image->xLength;

    Z = _mm_setzero_si128(); // Z = 0
    IF = _mm_set_ps1(255);
    C = _mm_set_ps(color->r, color->g, color->b, color->a);  // 4xf32 [R, G, B, A]
    C = _mm_mul_ps(C, IF);                                   // 4xf32 [R, G, B, A] * 255
    C = _mm_round_ps(C, _MM_FROUND_TO_NEAREST_INT);          // round([R, G, B, A])
    X = _mm_cvtps_epi32(C);                            // Cast S -> D -- 4xu32 [R, G, B, A]
    X = _mm_packus_epi32(X, Z);                        // Cast D -> W -- 8xu16 [R, G, B, A, 0, 0, 0, 0]
    X = _mm_packus_epi16(X, Z);                        // Cast W -> B -- 16xu8 [R, G, B, A, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    image->pixels[pixel] = _mm_extract_epi32(X, 0);    // 4xu32 [RGBA, 0, 0, 0] [0]
  }
}
double Round(double x){
  return floor(x + 0.5);
}
void DrawPixel(RGBABitmapImage *image, double x, double y, RGBA *color){
  double ra, ga, ba, aa;
  double rb, gb, bb, ab;
  double ro, go, bo, ao;
  RGBA newColor;
  uint32_t oldColor;
  int pixel;
  int r, g, b, a, tf, to;
  double t1, t2;
  float f;
  __m128 A, B, O, O1, O2, AA, AB, AO, T1, T2;
  __m128i X, Y, Z;
  __m128 F, IF;

  f = 0.00392156862f;
  F = _mm_set_ps1(f); // F = {f, f, f, f} -- F = f
  IF = _mm_set_ps1(255); // IF = {255, 255, 255, 255} -- IF = 255
  Z = _mm_setzero_si128(); // Z = 0

  if(x >= 0.0 && x < image->xLength && y >= 0.0 && y < image->yLength){
    //ra = color->r;
    //ga = color->g;
    //ba = color->b;
    //aa = color->a;

    A = _mm_set_ps(color->r, color->g, color->b, color->a); // Results in (A, B, G, R) -- A = (color->r, color->g, color->b, color->a);

    pixel = x + y * image->xLength;
    oldColor = image->pixels[pixel];

    //rb = ((oldColor >> 24) & 0xFF) * f;
    //gb = ((oldColor >> 16) & 0xFF) * f;
    //bb = ((oldColor >> 8) & 0xFF) * f;
    //ab = ((oldColor >> 0) & 0xFF) * f;

    X = _mm_insert_epi32(Z, oldColor, 0);   // X[0] = oldColor 
    X = _mm_cvtepu8_epi16(X);               // Cast B -> W
    X = _mm_cvtepu16_epi32(X);              // Cast W -> D
    B = _mm_cvtepi32_ps(X);                 // Cast D -> S -- (A, B, G, R)
    B = _mm_mul_ps(B, F);                   // B = B / 255

    AA = _mm_set_ps1(color->a);
    tf = _mm_extract_ps(B, 0);
    AB = _mm_set_ps1(*(float*)&tf);

    //t1 = ab*(1.0 - aa);
    //ao = aa + t1;

    T1 = _mm_set_ps1(1.0f);
    T1 = _mm_sub_ps(T1, AA);
    T1 = _mm_mul_ps(AB, T1);
    AO = _mm_add_ps(AA, T1);

    //t2 = 1/ao;

    T2 = _mm_rcp_ps(AO);

    //ro = (ra*aa + rb*t1)*t2;
    //go = (ga*aa + gb*t1)*t2;
    //bo = (ba*aa + bb*t1)*t2;

    O1 = _mm_mul_ps(A, AA);
    O2 = _mm_mul_ps(B, T1);
    O = _mm_add_ps(O1, O2);
    O = _mm_mul_ps(O, T2);

    O = _mm_insert_ps(O, AO, 0); // X[0] = ao
   
    //r = Round(ro * 255);
    //g = Round(go * 255);
    //b = Round(bo * 255);
    //a = Round(ao * 255);

    O = _mm_mul_ps(O, IF);
    O = _mm_round_ps(O, _MM_FROUND_TO_NEAREST_INT);

    X = _mm_cvtps_epi32(O);                            // Cast S -> D
    X = _mm_packus_epi32(X, Z);                        // Cast D -> W
    X = _mm_packus_epi16(X, Z);                        // Cast W -> B
    image->pixels[pixel] = _mm_extract_epi32(X, 0);
  }
}
double CombineAlpha(double as, double ad){
  return as + ad*(1.0 - as);
}
double AlphaBlend(double cs, double as, double cd, double ad, double ao){
  return (cs*as + cd*ad*(1.0 - as))/ao;
}
