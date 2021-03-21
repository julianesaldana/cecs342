(ns learning.core
  (:gen-class))
; Highlight core operations in Clojure


(defn -main
  [& args]
  
  ;; (let [*binding* *value*] ...) -- creates a name for a value limited in scope -- a "local variable"
  (let [x 5]
    ;; (if *condition* *true-clause* *false-clause (optional)*)
    ;; conditional evaluation
    (println (if (= x 2) "Yes" "No")) ; evaluates to?

    ;; (cond *condition1* *true1* *condition2* *true2* ... :else *else*)
    ;; more flexible if-elseif-elseif-else pattern
    (println (cond
               (= x 2) "First"
               (= x 4) "Second"
               (>= x 4) "Third"
               :else "Else"))

    ;; (when *condition* *clause*)
    ;; "If" without an "else".
    ;; What does this evaluate to when the condition is false?
    ;; Remember, every expression must evaluate to a value...
    (println (when (= x 5) "When!"))

    ;; (and *condition1* *condition2* ...)
    ;; logical and
    (println (when (and (< 0 x) (< x 11)) "And!"))
    ;; likewise or, not
  ) ;; this ends the "let" -- now x is not in scope
) ;; this ends the main



(-main nil)
