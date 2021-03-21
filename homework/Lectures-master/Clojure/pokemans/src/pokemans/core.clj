(ns pokemans.core)

;; In this application, two Pokemans fight to the death.


;; A Pokeman has a name, a level, a number of hit points, strength, and defense,
;; and the "power" of their attack ability.
(defn make-pokeman [name level hit-points strength defense ability-power]
  {:name name
   :level level
   :hit-points hit-points
   :strength strength
   :defense defense
   :ability-power ability-power})


;; Selectors for the fields of a Pokeman.
(defn name [pokeman]
  (:name pokeman))

(defn level [pokeman]
  (:level pokeman))

(defn hit-points [pokeman]
  (:hit-points pokeman))

(defn strength [pokeman]
  (:strength pokeman))

(defn defense [pokeman]
  (:defense pokeman))

(defn ability-power [pokeman]
  (:ability-power pokeman))

;; The state of the application: for two Pokemans to battle, we must track
;; the two Pokemans themselves, plus an indicator of whose turn it is to attack.
(defn make-state [pokeman-1 pokeman-2 turn]
  {:pokeman-1 pokeman-1
   :pokeman-2 pokeman-2
   :turn turn})

;; Selectors for game states
(defn current-turn [game-state]
  (:turn game-state))

(defn attacker [game-state]
  ((:turn game-state) game-state))

(defn pokeman-1? [turn]
  (= turn :pokeman-1))

(defn defender [game-state]
  (let [defender-key (if (pokeman-1? (current-turn game-state))
                       :pokeman-2
                       :pokeman-1)]
    (defender-key game-state)))

;; attack: calculates the damage dealt by the attacker to the defender. Returns
;; a new defender object with adjusted hitpoints.
(defn attack [attacker defender]
  ;; formula: damage = 2 + ((level*2/5 + 2) * ability-power * (attack / defense) / 50)
  (let [ad-ratio (/ (strength attacker) (defense defender))
        ability-pow (ability-power attacker)
        damage (int (+ 2 (/ (* (+ 2 (/ 5 (* 2 (level attacker))))
                               ability-pow
                               ad-ratio)
                            50)))
        new-hp (max (- (hit-points defender) damage)
                    0)]
    ;; Construct and return the new defender
    (make-pokeman (name defender)
                  (level defender)
                  new-hp
                  (strength defender)
                  (defense defender)
                  (ability-power defender))))

;; Cause the Pokeman whose turn it is to attack the other Pokeman. Return the
;; new game state.
(defn fight [game-state]
  (let [attacker (attacker game-state)
        defender (defender game-state)
        damaged-defender (attack attacker defender)
        p1? (pokeman-1? (current-turn game-state))
        new-p1 (if p1?
                 attacker
                 damaged-defender)
        new-p2 (if (not p1?)
                 attacker
                 damaged-defender)]
    (make-state new-p1 new-p2 (if p1? :pokeman-2 :pokeman-1))))

;; Play one round of the game. Current Pokeman attacks the other; if the defender
;; does not die, recursively play another round with the sides switched.
(defn play-round [game-state]
  (let [attack (attacker game-state)
        defend (defender game-state)
        new-state (fight game-state)]

    (println (str (name attack) " (" (hit-points attack) ") attacks "
                  (name defend) " (" (hit-points (attacker new-state)) ")"))

    (if (pos? (hit-points (attacker new-state)))
      (play-round new-state)
      (println (str (name defend) " died!")))))


(defn -main [& args]
  (let [game (make-state
               (make-pokeman "Jolteon" 50 100 100 40 120)
               (make-pokeman "Togekiss" 50 150 60 100 40)
               :pokeman-1)]
    (play-round game)))