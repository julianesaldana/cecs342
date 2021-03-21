public class Demo {
   public static void main(String[] args) {
      System.out.println("Constructing range");
      RangeGenerator range = new RangeGenerator(0, 100000);
      System.out.println("Constructing filter");
      Iterable<Integer> filter =
         new FilterGenerator<>(
            (i) -> i % 5 == 0, 
            range);
      
      System.out.println("Constructing map");
      Iterable<Double> map =
         new MapGenerator<>(
            (i) -> Math.sqrt(i),
            filter);
            
      
      System.out.println("Iterating through sequence");
      for (Double x : new TakeGenerator<>(4, map)) {
         System.out.println("Sequence element: " + x);
      }

      System.out.println();
      System.out.println("Let's do it again!");
      Iterable<Double> second = 
         Seq.take(3, 
            Seq.map((i) -> Math.sqrt(i), 
               Seq.filter((i) -> i % 5 == 0,
                  Seq.range(0, 100000))));
      
      System.out.println(Seq.reduce((a, b) -> a + b, 0.0, second));
      
   }
}
