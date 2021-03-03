#include <stdio.h>
#include <stdlib.h>


struct Employee {
    void **vtable;
    int age;
};

int GetAge(struct Employee *employee) {
    return employee->age;
}

/////////////////////////////////////////////////////

struct HourlyEmployee {
    void **vtable;
    int age;
    double hourly_rate;
    double hours;
};

void Construct_Hourly(struct HourlyEmployee *employee) {
    employee->age = 0;
    employee->hourly_rate = 0;
    employee->hours = 0;
}

void Speak_Hourly(struct Employee *employee) {
    printf("I work for %.2f dollars per hour :(", ((struct HourlyEmployee *) employee)->hourly_rate);
}

double GetPay_Hourly(struct Employee *employee) {
    return ((struct HourlyEmployee *) employee)->hourly_rate * ((struct HourlyEmployee *) employee)->hours;
}

////////////////////////////////////////////////////////

struct CommissionEmployee {
    void **vtable;
    int age;
    double sales_amount;
};

void Construct_Commission(struct CommissionEmployee *employee) {
    employee->age = 0;
    employee->sales_amount = 0;
}

void Speak_Commission(struct Employee *employee) {
    printf("I make commission on %.2f dollars in sales!", ((struct CommissionEmployee *) employee)->sales_amount);
}

double GetPay_Commission(struct Employee *employee) {
    return (((struct CommissionEmployee *) employee)->sales_amount * 0.10) + 40000;
}

//////////////////////////////////////////////////////////////////

struct SeniorSalesman {
    void **vtable;
    int age;
    double sales_amount;
};

void Construct_Senior(struct SeniorSalesman *employee) {
    employee->age = 0;
    employee->sales_amount = 0;
}

void Speak_Senior(struct Employee *employee) {
    printf("I make commission on %.2f dollars in sales!", ((struct SeniorSalesman *) employee)->sales_amount);
}

double GetPay_Senior(struct Employee *employee) {
    double sales_amount = ((struct SeniorSalesman *) employee)->sales_amount;
    return (GetAge(employee) >= 40) ? (sales_amount * 0.25 + 50000) : (sales_amount * 0.20 + 50000);
}

///////////////////////////////////////////////////////////////////////////

void *Vtable_Hourly[2] = {Speak_Hourly, GetPay_Hourly};
void *Vtable_Commission[2] = {Speak_Commission, GetPay_Commission};
void *Vtable_Senior[2] = {Speak_Senior, GetPay_Senior};

int main() {
    return 0;
}
