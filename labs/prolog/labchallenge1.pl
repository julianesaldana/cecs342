% Simple facts.

number(pikachu, 25).
% evolves(pikachu, raichu).
% evolves(charmander, charmeleon).
% evolves(charmeleon, charizard).
% evolves(eevee, jolteon).
% evolves(eevee, flareon).
% evolves(eevee, vaporeon).

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
evolves(eevee, vapoeron, item(waterStone)).

% adding type-effectiveness to move types

immune(electric, ground).

ineffective(fire, fire).
ineffective(fire, water).
ineffective(water, water).
ineffective(water, grass).
ineffective(grass, grass).
ineffective(grass, fire).
ineffective(electric, grass).
ineffective(electric, electric).
ineffective(ground, grass).

effective(water, fire).
effective(water, ground).
effective(grass, water).
effective(grass, ground).
effective(electric, water).
effective(ground, fire).
effective(ground, electric).
effective(fire, grass).

% adding rules for damage amount done by attack of one type against a target of a given type
damageMultiplier(MoveType, TargetType, 2.0) :- effective(MoveType, TargetType). % a move does 2x damage against a target if it is effective against that target.
damageMultiplier(MoveType, TargetType, 0.5) :- ineffective(MoveType, TargetType).
damageMultiplier(MoveType, TargetType, 0.0) :- immune(MoveType, TargetType).
damageMultiplier(_, _, 1.0).

% What does pikachu evolve into?
% evolves(pikachu, X)

% Is Charizard a descendent of Charmander?
% descendent(charizard, charmander)

% Who is a sibling of Jolteon?
% sibling(jolteon, Variable)

% What TMs can Pikachu use?
% canUseItem(pikachu, tm(X))

% damageMultiplier(water, fly, X)


effective(poison, grass).
ineffective(poison, poison).
ineffective(poison, ground).
ineffective(poison, rock).
ineffective(poison, ghost).
immune(poison, steel).
effective(poison, fairy).