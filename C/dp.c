#include "pbPlots.h"

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

	for(int i = 0; i < w*h; i++){
		image->pixels[i] = c;
	}

	//__stosd(image->pixels, c, w*h);

  return image;
}
void SetPixel(RGBABitmapImage *image, double x, double y, RGBA *color){
  if(x >= 0.0 && x < ImageWidth(image) && y >= 0.0 && y < ImageHeight(image)){
    int pixel = x + y * ImageWidth(image);
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
void DrawPixel(RGBABitmapImage *image, double x, double y, RGBA *color){
  double ra, ga, ba, aa;
  double rb, gb, bb, ab;
  double ro, go, bo, ao;
  RGBA newColor;
  uint32_t oldColor;
  int pixel;

  double f = 0.00392156862;

  if(x >= 0.0 && x < ImageWidth(image) && y >= 0.0 && y < ImageHeight(image)){
    ra = color->r;
    ga = color->g;
    ba = color->b;
    aa = color->a;

    pixel = x + y * ImageWidth(image);
    oldColor  = image->pixels[pixel];
    rb = ((oldColor >> 24) & 0xFF) * f;
    gb = ((oldColor >> 16) & 0xFF) * f;
    bb = ((oldColor >> 8) & 0xFF) * f;
    ab = ((oldColor >> 0) & 0xFF) * f;

    double t1 = ab*(1.0 - aa);

    ao = aa + t1;

    double t2 = 1/ao;

    ro = (ra*aa + rb*t1)*t2;
    go = (ga*aa + gb*t1)*t2;
    bo = (ba*aa + bb*t1)*t2;
    
    newColor.r = ro;
    newColor.g = go;
    newColor.b = bo;
    newColor.a = ao;

    int r = Round(newColor.r * 255);
    int g = Round(newColor.g * 255);
    int b = Round(newColor.b * 255);
    int a = Round(newColor.a * 255);

    image->pixels[pixel] = (r << 24) | (g << 16) | (b << 8) | a;
  }
}
double CombineAlpha(double as, double ad){
  return as + ad*(1.0 - as);
}
double AlphaBlend(double cs, double as, double cd, double ad, double ao){
  return (cs*as + cd*ad*(1.0 - as))/ao;
}
