// Name: Julian Saldana
// StudentID: 018462169
// Assignment: Homework 4 - Original Heapster
// Due Date: April 21, 2021 at start of lecture

#include <stdlib.h>
#include <math.h>

struct Block {
    int block_size;
    struct Block *next_block;
};

const int overhead_size = sizeof(struct Block);
const int pointer_size = sizeof(void *);
struct Block *free_head;

void my_initialize_heap(int size) {
    free_head = (struct Block *) malloc(size);
    free_head->block_size = size - overhead_size;
    free_head->next_block = NULL;
}

void *my_alloc(int size) {
    if (size < 1)   // checking if size is a positive integer value
        return NULL;

    int multipleSize = pointer_size * (int) (ceil((double) size / (double) pointer_size));

    struct Block *curr = free_head;
    struct Block *prev = NULL;

    while (curr != NULL) {
        if (curr->block_size < multipleSize) {  // will keep looking if size is too small
            prev = curr;
            curr = curr->next_block;
        } else {    // size is large enough
            if (curr->block_size < multipleSize + overhead_size + pointer_size) {   // block not splittable, it will use the block as is and unlink it
                if (curr == free_head) {
                    free_head = curr->next_block;
                } else {
                    prev->next_block = curr->next_block;
                }
                curr->next_block = NULL;
            } else {    // block is splittable
                struct Block *newBlockLocation = (struct Block *) ((char *) curr + multipleSize + overhead_size);   // creating new block and specifying attributes
                newBlockLocation->block_size = curr->block_size - multipleSize - overhead_size;
                newBlockLocation->next_block = curr->next_block;

                if (curr == free_head)  // unlinking if curr block was the head
                    free_head = newBlockLocation;
                else {  // unlinking curr if not head
                    newBlockLocation->next_block = curr->next_block;
                    prev->next_block = newBlockLocation;
                }

                curr->block_size = multipleSize;    // curr block changes to the new split size
            }
            return (void *) ((char *) curr + overhead_size);
        }
    }
    return NULL;
}

void my_free(void *data) {
    struct Block *curr = (struct Block *) ((char *) data - overhead_size);
    curr->next_block = free_head;
    free_head = curr;
}