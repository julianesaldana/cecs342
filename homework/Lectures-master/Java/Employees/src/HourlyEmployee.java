public class HourlyEmployee extends Employee {
    private double hourlyWage;
    private double hoursWorked;

    public double getWages() {
        return hourlyWage * hoursWorked;
    }    

    public boolean dueForPromotion() {
        return hoursWorked >= 50;
    }
}