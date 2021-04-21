// Name: Julian Saldana
// StudentID: 018462169
// Assignment: Homework 4 - Original Heapster
// Due Date: April 21, 2021 at start of lecture

#include <stdio.h>
#include <stdbool.h>
#include "hw4.c"

int maxOfBoth(int a, int b) {
    if (a > b)
        return a;
    else
        return b;
}

// FIXME: addresses of variables were converted to decimals from hexadecimal in order to easily determine location distance from one another
int main() {
    int option;
    my_initialize_heap(1000);

    bool run = true;
    while (run) {
        printf("\n-----------\n");
        printf("Enter 1-5:");
        scanf("%d", &option);
        if (option == 1) {
            void *int1 = my_alloc(sizeof(int));
            printf("Address of  first int: %d \n", int1);

            my_free(int1);

            void *int2 = my_alloc(sizeof(int));
            printf("Address of second int: %d \n", int2);

            printf("They should be the same.\n");

        } else if (option == 2) {
            void *int1 = my_alloc(sizeof(int));
            void *int2 = my_alloc(sizeof(int));

            printf("Address of  first: %d \n", int1);
            printf("Address of second: %d \n", int2);

            int max = maxOfBoth(sizeof(int), pointer_size);
            printf("They should be %d apart\n", overhead_size + max);
        } else if (option == 3) {
            void *int1 = my_alloc(sizeof(int));
            void *int2 = my_alloc(sizeof(int));
            void *int3 = my_alloc(sizeof(int));
            printf("Address of  first int: %d \n", int1);
            printf("Address of second int: %d \n", int2);
            printf("Address of  third int: %d \n\n", int3);

            printf("Freeing second int...\n\n");
            my_free(int2);

            void *array1 = my_alloc(2 * sizeof(double));
            printf("Address of array1: %d \n", array1);

            void *alloc4 = my_alloc(sizeof(int));
            printf("Address of fourth: %d \n", alloc4);
        } else if (option == 4) {
            void *char1 = my_alloc(sizeof(char));
            void *int1 = my_alloc(sizeof(int));
            printf("Address of char: %d \n", char1);
            printf("Address of  int: %d \n", int1);


            int max = maxOfBoth(sizeof(int), pointer_size);
            printf("They should be %d apart\n", overhead_size + max);
        } else if (option == 5) {
            void *array1 = my_alloc(80 * sizeof(int));
            void *int1 = my_alloc(sizeof(int));
            printf("Address of int should be here: %d\n", (uintptr_t) array1 + overhead_size + 80 * sizeof(int));
            printf("Actual address of int: %d\n", int1);
            my_free(array1);
            printf("Address of int after deleted array: %d\n", int1);
        } else
            run = false;
    }
    return 0;
}