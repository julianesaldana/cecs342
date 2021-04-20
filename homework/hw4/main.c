#include <stdlib.h>
#include <stdio.h>
#include <stdbool.h>

struct Block {
    int block_size;
    struct Block *next_block;
};

// global variables
const int overhead_size = sizeof(struct Block);
const int pointer_size = sizeof(void *);
struct Block *free_head;

void my_initialize_heap(int size) {
    free_head = (struct Block *) malloc(size);
    free_head->block_size = size - overhead_size;
    free_head->next_block = NULL;
}

void *my_alloc(int size) {
    if (size < 1)   // size must be positive integer value
        return 0;

    int neededSize = 0;
    if (size % pointer_size)
        neededSize = size + pointer_size - (size % pointer_size);  // turning size into a multiple of pointer size

    struct Block *curr = free_head;
    struct Block *prev = NULL;

    while (curr != NULL) {
        if (curr->block_size <= neededSize) {  // checking if the block has enough space
            // will keep looking for a block with minimum space required
            prev = curr;
            curr = curr->next_block;
        } else {
            int leftover = curr->block_size - neededSize;    // leftover space

            if (leftover < (overhead_size + pointer_size)) { // checking if leftover is big enough for split
                // if leftover space is NOT enough for overhead + pointer
                if (curr == free_head)
                    free_head = curr->next_block;   // set next block as head
                else {
                    prev->next_block = curr->next_block;    // practically removing current block since it doesnt have enough space
                }
                curr->next_block = NULL;    // removed the block that wasn't big enough to store
            } else {    // if leftover space IS enough for overhead + pointer
                struct Block *newBlock = (struct Block *) (((char *) curr) + overhead_size + neededSize);    // intializing new block and pointing to where it will start
                newBlock->block_size = leftover;
                newBlock->next_block = curr->next_block;    // put new block in between, rid of current block

                curr->block_size = neededSize; // reducing size of original block to match allocation request
                curr->next_block = newBlock;    // putting newBlock in between, may need to comment out and switch to below VVV

//                if (curr == free_head)   // if current block is the head, set new block to head
//                    free_head = newBlock;
//                else {
//                    prev->next_block = newBlock;
//                    curr->next_block = prev;
//                }
            }

            return (void *) ((char *) curr + overhead_size);

        }
    }
    return 0; // returns 0 if there are no blocks available for the given size
}


void my_free(void *data) {
    struct Block *curr = (struct Block *) ((char *) data - overhead_size);
    curr->next_block = free_head;
    free_head = curr;
}

int maxOfBoth(int a, int b) {
    if (a > b)
        return a;
    else
        return b;
}

int main() {
    int option;
    my_initialize_heap(1000);

    bool run = true;
    while (run) {
        printf("Enter 1-5:");
        scanf("%d", &option);

        if (option == 1) {
            void *int1 = my_alloc(sizeof(int));
            printf("Address of  first: %p \n", int1);

            my_free(int1);

            void *int2 = my_alloc(sizeof(int));
            printf("Address of second: %p \n", int2);

        } else if (option == 2) {
            void *int1 = my_alloc(sizeof(int));
            void *int2 = my_alloc(sizeof(int));
            printf("Address of  first: %p \n", int1);
            printf("Address of second: %p \n", int2);

            int max = maxOfBoth(sizeof(int), pointer_size);
            printf("They are %d apart\n", overhead_size + max);
        } else if (option == 3) {
            void *int1 = my_alloc(sizeof(int));
            void *int2 = my_alloc(sizeof(int));
            void *int3 = my_alloc(sizeof(int));
            printf("Address of  first: %p \n", int1);
            printf("Address of second: %p \n", int2);
            printf("Address of  third: %p \n", int3);

            my_free(int2);

            void *array1 = my_alloc(2 * sizeof(double));
            printf("Address of array1: %p \n", array1);

            void *alloc4 = my_alloc(sizeof(int));
            printf("Address of fourth: %p \n", alloc4);
        } else if (option == 4) {
            void *char1 = my_alloc(sizeof(char));
            void *int1 = my_alloc(sizeof(int));
            printf("Address of char: %p \n", char1);
            printf("Address of  int: %p \n", int1);


            int max = maxOfBoth(sizeof(int), pointer_size);
            printf("They are %d apart\n", overhead_size + max);
        } else if (option == 5) {
            void *array1 = my_alloc(80 * sizeof(int));
            void *int1 = my_alloc(sizeof(int));
            printf("Int should be at this address:   %p  \n",
                   ((char *) array1) + overhead_size + (80 * sizeof(int)));
            printf("Address of int: %p \n", int1);
            my_free(array1);
            printf("Address of int after deleted array: %p \n", int1);

//            void *array1 = my_alloc(80 * sizeof(int));
//            void *int1 = my_alloc(sizeof(int));
//
//            printf("Address of array: %p \n", array1);
//            printf("Address of   int: %p \n", int1);
//            printf("They are %d apart\n", int1 - array1);
//
//            printf("Address of int =  %p\n", array1 + overhead_size + 80 * sizeof(int));
//
//            my_free(array1);
//
//            printf("Address of int after freeing array: %p \n", int1);
        } else
            run = false;
    }
    return 0;
}