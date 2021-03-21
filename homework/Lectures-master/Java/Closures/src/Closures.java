import java.util.function.Function;

class Closures {
    public static void main(String[] args) {
        Function<Integer, Integer> adder = getAdder(5);

        System.out.println(adder.apply(5));
    }

    // Returns a function that adds 2*x to its parameter.
    public static Function<Integer, Integer> getAdder(int x) {
        int z = x * 2;
        // Same issue as in F#... when this anonymous function gets returned,
        // z (and x) will no longer be in scope, and won't exist.... right?
        return (i) -> i + z;
    }
















    // SECRET FORBIDDEN KNOWLEDGE
    public static Function<Integer, Integer> getAdder2(int x) {
        int z = x * 2;
        return new MyAdder(z);
    }
}