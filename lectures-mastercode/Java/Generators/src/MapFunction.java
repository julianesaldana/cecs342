public interface MapFunction<TIn, TOut> {
   TOut transform(TIn value);
}