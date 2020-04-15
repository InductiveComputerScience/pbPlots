g++ -c example1.cpp -std=c++98 -O3 -march=native
g++ -c example2.cpp -std=c++98 -O3 -march=native
g++ example1.o -lm -o example1
g++ example2.o -lm -o example2
strip example1
strip example2
