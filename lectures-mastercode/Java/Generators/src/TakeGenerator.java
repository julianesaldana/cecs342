import java.util.Iterator;

public class TakeGenerator<T> implements Iterable<T> {
	private int mCount;
	private Iterable<T> mSource;
	
	public TakeGenerator(int count, Iterable<T> source) {
		mCount = count;
		mSource = source;
	}
	
	public Iterator<T> iterator() {
		return new TakeIterator();
	}
	
	private class TakeIterator implements Iterator<T> {
		private int mCurrent;
		private Iterator<T> mIter;
		
		public TakeIterator() {
			mIter = mSource.iterator();

		}
		public boolean hasNext() {
			return mCurrent < mCount && mIter.hasNext();
		}
		
		public T next() {
			mCurrent++;
			return mIter.next();
		}
	}
}