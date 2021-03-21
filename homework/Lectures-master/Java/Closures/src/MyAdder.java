import java.util.function.Function;

public class MyAdder implements Function<Integer, Integer> {
    private int z;

    public MyAdder(int x) {
        this.z = x;
    }

    public Integer apply(Integer t) {
        return t + z;
    }
}
