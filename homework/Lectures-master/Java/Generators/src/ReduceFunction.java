public interface ReduceFunction<TIn, TResult> {
	TResult apply(TResult old, TIn next);
}