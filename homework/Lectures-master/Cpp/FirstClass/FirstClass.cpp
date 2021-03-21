#include <iostream>
#include <functional>

int max(int a, int b) { return a > b ? a : b; }

int applyIntFunction(int (*f)(int, int), int a, int b) {
	return f(a, b);
}

int main() {
	// C++ supports C-style function pointers...
	int (*fp)(int, int) = max;
	std::cout << fp(10, 5) << std::endl;


	// It also supports the creation of new anonymous functions at runtime, through the
	// auto 'lambda' type.
	auto fAnon = [](int a, int b) {
		return a < b ? a : b;
	};
	std::cout << fAnon(10, 5) << std::endl;

	return 0;
}