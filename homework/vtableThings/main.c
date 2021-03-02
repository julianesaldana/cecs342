#include <stdio.h>

struct Employee {
    void** vtable;
    int age;
};

struct HourlyEmployee {
    void** vtable;
    int age;
    double hourly_rate;
    double hours;
};

struct CommissionEmployee {
    void** vtable;
    int age;
    double hourly_rate;
    double hours;
    double sales_amount;
};

int GetAge(struct Employee temp) {
    return temp.age;
}

//void Speak_Hourly(struct Employee *ptr) {
//    ptr = (struct HourlyEmployee*) ptr;
//
//}



int main() {
    struct Employee temp;
    temp.age = 10;
    printf("%d", temp.age);
    return 0;
}
