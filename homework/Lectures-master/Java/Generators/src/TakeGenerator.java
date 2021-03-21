import java.util.Iterator;

public class TakeGenerator<T> implements Iterable<T> {
	private int mTakeCount;
	private Iterable<T> mSource;
	
	public TakeGenerator(int count, Iterable<T> source) {
		mTakeCount = count;
		mSource = source;
	}
	
	public Iterator<T> iterator() {
		return new TakeIterator();
	}
	
	private class TakeIterator implements Iterator<T> {
		private int mTaken;
		private Iterator<T> mIterator;
		
		public TakeIterator() {
			mIterator = mSource.iterator();
			mTaken = 0;
		}
		
		public boolean hasNext() {
			return mTaken < mTakeCount && mIterator.hasNext();
		}
		
		public T next() {
			mTaken++;
			return mIterator.next();
		}
	}
}