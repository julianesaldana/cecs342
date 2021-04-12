% Simple facts.

number(pikachu, 25).
evolves(pikachu, raichu).
evolves(charmander, charmeleon).
evolves(charmeleon, charizard).
evolves(eevee, jolteon).
evolves(eevee, flareon).
evolves(eevee, vaporeon).

% Slightly more complex facts.

move(thunderbolt, electric, special, 90).
move(thunderpunch, electric, physical, 75).
learns(pikachu, thunderbolt, level(36)). % Pikachu learns Thunderbolt at level 36.
learns(pikachu, thunderpunch, tm(5)).


% Simple rules.

sibling(X, Y) :- evolves(Parent, X), evolves(Parent, Y), X \= Y. % the comma means "and". "\=" means "does not unify".

canUseItem(Pokemon, tm(X)) :- learns(Pokemon, _, tm(X)). % _ is "don't care", yet again.


% A rule with multiple clauses.
descendent(X, Y) :- evolves(Y, X).
descendent(X, Y) :- evolves(Y, Z), descendent(X, Z). % This one is recursive!!



% LAB CHALLENGES

% updating evolve facts
evolves(pikachu, raichu, item(thunderStone)).
evolves(charmander, charmeleon, level(16)).
evolves(charmeleon, charizard, level(36)).
evolves(eevee, jolteon, item(thunderStone)).
evolves(eevee, flareon, item(fireStone)).
evolves(eeve, vapoeron, item(waterStone)).

% adding type-effectiveness to move types

ineffective(fire, water).
ineffective(fire, fire).
effective(fire, grass).
normal(fire, electric).
normal(fire, ground).

effective(water, fire).
ineffective(water, water).
ineffective(water, grass).
normal(water, electric).
effective(water, ground).

ineffective(grass, fire).
effective(grass, water).
ineffective(grass, grass).
normal(grass, electric).
effective(grass, ground).

normal(electric, fire).
effective(electric, water).
ineffective(electric, grass).
ineffective(electric, electric).
immune(electric, ground).

effective(ground, fire).
normal(ground, water).
ineffective(ground, grass).
effective(ground, electric).
normal(ground, ground).

% adding rules for damage amount done by attack of one type against a target of a given type
damageMultiplier(MoveType, TargetType, 2.0) :- effective(MoveType, TargetType). % a move does 2x damage against a target if it is effective against that target.
damageMultiplier(MoveType, TargetType, 0.5) :- ineffective(MoveType, TargetType).
damageMultiplier(MoveType, TargetType, 0.0) :- immune(MoveType, TargetType).
damageMultiplier(MoveType, TargetType, 1.0) :- normal(MoveType, TargetType).