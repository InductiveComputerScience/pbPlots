gcc -c example1.c -std=c99 -O3 -march=native
gcc -c example2.c -std=c99 -O3 -march=native
gcc example1.o -lm -o example1
gcc example2.o -lm -o example2
strip example1
strip example2
