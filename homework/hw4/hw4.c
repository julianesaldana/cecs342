#include <stdio.h>

struct Block {
    int block_size; // # of bytes in the data section
    struct Block *next_block;   // in C, you have to use "stuct Block" as the type;
};

// void my_initialize_heap(int size) {
    
// }

const int overhead_size = sizeof(struct Block);
const int pointer_size = sizeof(void*);
struct Block *free_head;


int main() {
    printf("overhead_size = %d\n", overhead_size);
    printf("pointer_size = %d", pointer_size);
    return 0;
}