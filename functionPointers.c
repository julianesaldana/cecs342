#include <assert.h>

int square(int x) {
	return x * x;
}

int add(int x, int y) {
	return x + y;
}

void say_hello(int x) {
	printf("Hello with value %d", x);	//puts("hello") for string only, a char *
}

int main() {
	int x = 4;
	int squared = square(x);

	int (*fn) (int) = &square;

	// want a pointer to add
	// how do i declare it?
	int (*addptr) (int, int) = &add;

	// want a pointer to say_hello, how?
	void (*hello) (int) = &say_hello;	// if no int argument, cast to void
	hello(42);


	assert (16 == squared);
	assert (16 == fn(x));

	assert (5 == addptr(2,3));
	return 0;
}


///////////////////////////////////////////////////////////////

#include <stdio.h>
#include <assert.h>

struct Foo {
	void **data; // void pointer
}


int main() {
	int x = 42;
	void *data[1] = {&x};

	struct Foo foo;
	foo.data = data;

	//										void ** or void * []
	//				(pointer to an int)		+------+
	//												void *
	//												+---+
	//				int *
	//				+----------------------------------+
	// & -> int **
	int value = (int *)						foo.data[0];
	int value = *(foo.data[0]);

	return 0;
}



