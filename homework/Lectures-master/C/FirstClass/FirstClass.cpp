#include <iostream>

// C has function pointers, but we don't consider those to be "first class".

int max(int a, int b) {
	return a > b ? a : b;
}

int main() {
	int (*fp)(int, int) = max; // you've seen this syntax before. ugly!
	printf("max: %d\n", fp(10, 5));

	printf("max: %d\n", applyFunction(max, 10, 5));

	int r = getFunction()(19, 5);
	return 0;
}

// We can pass function pointers as parameters:
int applyFunction(int (*f)(int, int), int a, int b) {
	return f(a, b);
}

// And return them from functions.
int (*getFunction())(int, int) {
/*               ^^ this is the parameter list to getFunction
^^^ says that getFunction returns a function returning int
                     ^^^^^^^^ says that the function returned by getFunction takes 2 int parameters.
*/
	return max;
}

// But we can't create a function at run time.