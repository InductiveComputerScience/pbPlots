g++ -c pbPlots.cpp -std=c++98 -O3 -march=native
g++ -c supportLib.cpp -std=c++98 -O3 -march=native
g++ -c example1-c++98.cpp -std=c++98 -O3 -march=native
g++ -c example2-c++98.cpp -std=c++98 -O3 -march=native
g++ -c example2.cpp -std=c++11 -O3 -march=native
g++ -c example1.cpp -std=c++11 -O3 -march=native
g++ -c example2.cpp -std=c++11 -O3 -march=native
g++ -c example3.cpp -std=c++11 -O3 -march=native
g++ example1.o pbPlots.o supportLib.o -lm -o example1
g++ example2.o pbPlots.o supportLib.o -lm -o example2
g++ example3.o pbPlots.o supportLib.o -lm -o example3
strip example1
strip example2
strip example3
./example1
./example2
./example3
