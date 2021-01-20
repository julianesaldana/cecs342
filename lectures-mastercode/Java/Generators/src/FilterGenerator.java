import java.util.function.Predicate;
import java.util.Iterator;
public class FilterGenerator<T> implements Iterable<T> {
   private Iterable<T> mSource;
   private Predicate<T> mPredicate;
   
   public FilterGenerator(Predicate<T> pred, Iterable<T> source) {
      mSource = source;
      mPredicate = pred;
   }

   public Iterator<T> iterator() {
      return new FilterIterator();
   }

   private class FilterIterator implements Iterator<T> {
      private T mNext;
      private Iterator<T> mIter;

      public FilterIterator() {
         mIter = mSource.iterator();
      }
   
      private T findNext() {
         while (mIter.hasNext()) {
            T temp = mIter.next();
            if (mPredicate.test(temp)) {
               return temp;
            }
         }
         return null;
      }

      public boolean hasNext() {
         if (mIter.hasNext() && mNext == null) {
            mNext = findNext();
         }
         return mNext != null;
      }
      
      public T next() {
         System.out.println("Filter found " + mNext);
         T temp = mNext;
         mNext = null;
         return temp;
      }
   }
}