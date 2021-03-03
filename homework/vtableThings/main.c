#include <stdio.h>

struct Employee {
    void **vtable;
    int age;
};

struct HourlyEmployee {
    void **vtable;
    int age;
    double hourly_rate;
    double hours;
};

struct CommissionEmployee {
    void **vtable;
    int age;
    double hourly_rate;
    double hours;
    double sales_amount;
};

struct SeniorSalesman {
    void **vtable;
    int age;
    double hourly_rate;
    double hours;
    double sales_amount;
};

void Construct_Hourly() {

}

void Construct_Commission() {

}

void Construct_Senior() {
    
}

int main() {
    struct Employee *ptr;
    printf("Enter the number of the option you'd like to select:\n1) Hourly Employee\n2) Commission Employee\n3) Senior Salesman\n");
    int employeeType;
    scanf("%d", &employeeType);

//    if (employeeType == 1) {
//        struct HourlyEmployee *h = (HourlyEmployee*) malloc (sizeof(HourlyEmployee));
//    } else if (employeeType == 2) {
//        struct CommissionEmployee *h = (CommissionEmployee*) malloc (sizeof(CommissionEmployee));
//    } else {
//        struct SeniorSalesman *h = (SeniorSalesman*) malloc (sizeof(SeniorSalesman));
//    }

    printf("How old is the employee?\n");
    int age;
    scanf("%d", &age);

    if (employeeType == 1) {
        printf("Enter employee's pay rate and hours:\n");
        float payRate;
        int hours;
        scanf("%f %d", &payRate, &hours);
    } else {
        printf("Enter the employee's amount of sales:\n");
        int sales;
        scanf("%d", &sales);
    }
    return 0;
}
