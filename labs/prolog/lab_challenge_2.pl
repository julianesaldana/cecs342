contains([X|_], X). % if head is equal to item X
contains([_|T], X) :- contains(T, X).   % recursive definition, checks head AND tail
