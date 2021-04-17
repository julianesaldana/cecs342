#include <stdio.h>
#include <stdlib.h>

struct Block {
    // vvv known as the HEADER of the block vvv
    int block_size; // # of bytes in the data section
    struct Block *next_block;   // in C, you have to use "stuct Block" as the type;
};

const int overhead_size = sizeof(struct Block);
const int pointer_size = sizeof(void*);
struct Block *free_head;

void my_initialize_heap(int size) {
    free_head = malloc(size + 8 + 8);   // size plus header information, which is 8 and 8, taken from lecture
    free_head->block_size = size;
    free_head->next_block = NULL;
}

void* my_alloc(int size) {

}

void my_free(void *data) {

}

int main() {
    printf("overhead_size = %d\n", overhead_size);
    printf("pointer_size = %d", pointer_size);
    return 0;
}