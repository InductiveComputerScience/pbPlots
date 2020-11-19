gcc -c pbPlots.c -std=c99 -O3 -march=native
gcc -c supportLib.c -std=c99 -O3 -march=native
gcc -c example1.c -std=c99 -O3 -march=native
gcc -c example2.c -std=c99 -O3 -march=native
gcc -c example3.c -std=c99 -O3 -march=native
gcc -c example4.c -std=c99 -O3 -march=native
gcc example1.o pbPlots.o supportLib.o -lm -o example1
gcc example2.o pbPlots.o supportLib.o -lm -o example2
gcc example3.o pbPlots.o supportLib.o -lm -o example3
gcc example4.o pbPlots.o supportLib.o -lm -o example4
strip example1
strip example2
strip example3
strip example4
./example1
./example2
./example3
./example4

