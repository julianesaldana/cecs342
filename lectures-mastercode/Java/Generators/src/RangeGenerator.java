import java.util.Iterator;

public class RangeGenerator implements Iterable<Integer> {
   private int mStart, mEnd;
   
   public RangeGenerator(int start, int end) {
      mStart = start;
      mEnd = end;
   }
   
   public Iterator<Integer> iterator() {
      return new RangeIterator();
   }

   private class RangeIterator implements Iterator<Integer> {
      private int mCurrent;
      public RangeIterator() {
         mCurrent = mStart;
      }
      public boolean hasNext() {
         return mCurrent < mEnd;
      }
   
      public Integer next() {
         System.out.println("Generating " + mCurrent);
         return mCurrent++;
      }
   }
}