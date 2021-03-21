public class CommissionEmployee extends Employee {
    private double amountOfSales;
    private double commissionRate;

    public double getWages() {
        return 40_000 + amountOfSales * commissionRate;
    }

    public boolean dueForPromotion() {
        return commissionRate > 0.10 && amountOfSales > 100_000;
    }
}