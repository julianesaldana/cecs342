int max (int a, int b) {
	return a >= b ? a : b;	// returns the larger integer
	// if a >= b return a, else return b
}

int main() {
	int (*pFunc) (int int);	// pFunc is a pointer to a function that takes 2 int arguments and returns int

	pFunc = max;	// pFunc now points to the real function "max"
	printf ("%d", pFunc(5, 10));	// dereference pFunc, use its return value
}