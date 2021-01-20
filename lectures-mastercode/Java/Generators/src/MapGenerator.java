import java.util.Iterator;

public class MapGenerator<TIn, TOut> implements Iterable<TOut> {
   private MapFunction<TIn, TOut> mTransform;
   private Iterable<TIn> mSource;
   
   public MapGenerator(MapFunction<TIn, TOut> transform, Iterable<TIn> source) {
      mTransform = transform;
      mSource = source;
   }
   public Iterator<TOut> iterator() {
      return new MapIterator();
   }

   private class MapIterator implements Iterator<TOut> {
      private Iterator<TIn> mIter;

      public MapIterator() {
         mIter = mSource.iterator();
      }

      public boolean hasNext() {
         return mIter.hasNext();
      }
      
      public TOut next() {
         return mapNext();
      }
      
      private TOut mapNext() {
         TIn  next = mIter.next();
         TOut transformed = mTransform.transform(next);
         System.out.println("Transformed: " + next + " to " + transformed);
         return transformed;
      }
   }

}