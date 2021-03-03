#include <stdio.h>
#include <stdlib.h>

void *Vtable_Hourly[2], *Vtable_Commission[2], *Vtable_Senior[2];

typedef struct Employee {
    void **vtable;
    int age;
} Employee;

int GetAge(struct Employee *employee) {
    return employee->age;
}

/////////////////////////////////////////////////////

typedef struct HourlyEmployee {
    void **vtable;
    int age;
    double hourly_rate;
    double hours;
} HourlyEmployee;

//void Construct_Hourly(struct HourlyEmployee *employee) {
//    employee->vtable = Vtable_Hourly;
//    employee->age = 0;
//    employee->hourly_rate = 0;
//    employee->hours = 0;
//}

void Construct_Hourly(struct HourlyEmployee *employee) {
    employee->vtable = Vtable_Hourly;
    employee->age = 0;
    employee->hourly_rate = 0;
    employee->hours = 0;
}

void Speak_Hourly(struct Employee *employee) {
    printf("I work for %.2f dollars per hour :(\n", ((struct HourlyEmployee *) employee)->hourly_rate);
}

double GetPay_Hourly(struct Employee *employee) {
    return ((struct HourlyEmployee *) employee)->hourly_rate * ((struct HourlyEmployee *) employee)->hours;
}

////////////////////////////////////////////////////////

typedef struct CommissionEmployee {
    void **vtable;
    int age;
    double sales_amount;
} CommissionEmployee;

void Construct_Commission(struct CommissionEmployee *employee) {
    employee->vtable = Vtable_Commission;
    employee->age = 0;
    employee->sales_amount = 0;
}

void Speak_Commission(struct Employee *employee) {
    printf("I make commission on %.2f dollars in sales!\n", ((struct CommissionEmployee *) employee)->sales_amount);
}

double GetPay_Commission(struct Employee *employee) {
    return (((struct CommissionEmployee *) employee)->sales_amount * 0.10) + 40000;
}

//////////////////////////////////////////////////////////////////

typedef struct SeniorSalesman {
    void **vtable;
    int age;
    double sales_amount;
} SeniorSalesman;

void Construct_Senior(struct SeniorSalesman *employee) {
    employee->vtable = Vtable_Senior;
    employee->age = 0;
    employee->sales_amount = 0;
}

void Speak_Senior(struct Employee *employee) {
    printf("I make commission on %.2f dollars in sales!\n", ((struct SeniorSalesman *) employee)->sales_amount);
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
    int run = 1;
    while (run) {
        struct Employee *employee;

        printf("Choose the type of employee:\n1) Hourly\n2) Commission\n3) Senior\n");
        int type; scanf("%d", &type);

        printf("Enter the age of the employee:\n");
        int age; scanf("%d", &age);

        if (type == 1) {
            HourlyEmployee *h = (HourlyEmployee *) malloc(sizeof(HourlyEmployee));
            printf("Enter pay rate and hours:\n");
            double hourly_rate, hours; scanf("%lf %lf", &hourly_rate, &hours);
            Construct_Hourly(h);
            h->age = age; h->hourly_rate = hourly_rate; h->hours = hours;

            employee = h;

            ((void (*)(struct Employee*))Vtable_Hourly[0])((struct Employee *)employee);
            double pay = ((double (*)(struct Employee*))Vtable_Hourly[1])((struct Employee *)employee);
            printf("I got paid %.2f\n", pay);

        } else if (type == 2) {
            CommissionEmployee *c = (CommissionEmployee *) malloc(sizeof(CommissionEmployee));
            printf("Enter amount in sales:\n");
            double sales_amount; scanf("%lf", &sales_amount);
            Construct_Commission(c);
            c->age = age; c->sales_amount = sales_amount;

            employee = c;

            ((void (*)(struct Employee*))Vtable_Commission[0])((struct Employee *)employee);
            double pay = ((double (*)(struct Employee*))Vtable_Commission[1])((struct Employee *)employee);
            printf("I got paid %.2f\n", pay);

        } else {
            SeniorSalesman *s = (SeniorSalesman *) malloc(sizeof(SeniorSalesman));
            printf("Enter amount in sales:\n");
            double sales_amount; scanf("%lf", &sales_amount);
            Construct_Senior(s);
            s->age = age; s->sales_amount = sales_amount;

            employee = s;

            ((void (*)(struct Employee*))Vtable_Senior[0])((struct Employee *)employee);
            double pay = ((double (*)(struct Employee*))Vtable_Senior[1])((struct Employee *)employee);
            printf("I got paid %.2f\n", pay);
        }
        printf("\nEnter 0 if you'd like to quit:\n");
        scanf("%d", &run);
        printf("\n");
    }
    return 0;
}

