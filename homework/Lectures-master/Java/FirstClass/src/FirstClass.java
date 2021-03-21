// Java sort-of has first class functions, using interfaces.
class FirstClass {

    // First, define an interface with a single method matching the signature of the
    // function you want to use.
    interface IntIntIntFunction {
        // a function on int->int->int.
        int apply(int a, int b);
    }

    // Next define the actual function.
    static int min(int a, int b) {
        return a < b ? a : b;
    }

    public static void main(String[] args) {
        // Next, create a variable of the interface, assigned to the function.
        // In Java, the :: is for scope resolution: it means, 
        // "the min function of the class named FirstClass"
       IntIntIntFunction f = FirstClass::min;

       // f is really an object, not a method; so we can't use () to call it.
       // Instead we call the one method of that interface; in this case, apply.
       System.out.println(f.apply(10, 5));

       // For dynamic-created functions at run time, we use lambda syntax:
       f = (a, b) -> a > b ? a : b; // now f is "max".
       System.out.println(f.apply(10, 5));
    }
}