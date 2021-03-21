(ns guns-and-ships.core)

;; Play a game like "Gorillas" (MS-DOS), Gunbound, etc.
;; A target is randomly placed up to 1000m away. The user chooses an angle
;; and gunpowder amount to use when firing their cannon.
;; 1g of gunpowder == 15 m/s of cannon velocity.
;; Calculate the distance traveled by the cannonball, and count a "hit"
;; if the distance is within 1m of the target.

;; Generates a random distance from 0 to max-distance.
(defn random-target [max-distance]
  (rand max-distance))

;; Asks the user to input the cannon angle.
(defn get-angle []
  (println "Fire the cannon at what angle?")
  (flush)
  (Double/parseDouble (read-line)))

;; Asks the user to input the gunpowder amount.
(defn get-gunpowder []
  (println "Fire the cannon with what amount of gunpowder?")
  (flush)
  (Double/parseDouble (read-line)))

;; Converts gunpowder amount to meters-per-second.
(defn gunpowder-to-mps [gunpowder]
  ;; 1g gunpowder = 15 m/s
  (* gunpowder 15))

;; Uses projectile physics to determine the distance traveled by a projectile
;; fired at the given angle with the given amount of gunpowder.
(defn calculate-distance [angle gunpowder]
  (let [v (gunpowder-to-mps gunpowder)
        v-squared (* v v)
        two-theta (* 2 angle)
        radians (Math/toRadians two-theta)
        G 9.8]
    ;; formula: v^2*sin(2*theta) / G
    (/ (* v-squared (Math/sin radians)) G)))

;; A cannonball hits the target if it is within 1 m.
(defn hit? [target cannonball]
  (let [diff (- target cannonball)]
    (<= -1 diff 1)))


;; User gets a single shot to hit the target, since we can't mutate variables!
(defn play-one-shot []
  ;; Choose a distance for the target.
  (let [target (random-target 1000)]
    (println "The target is " target "m away. Do not throw away your shot!")

    ;; Get the user's input and calculate their distance.
    (let [angle (get-angle)
          gunpowder (get-gunpowder)
          distance (calculate-distance angle gunpowder)]

      ;; Check for a hit.
      (if (hit? target distance)
        (println "Great job, you hit the target!")
        ;; We missed -- tell how far off.
        (let [diff (- distance target)]
          (println "You suck! You were"
                   (Math/abs diff) "m too"
                   (if (pos? diff) "far." "short.")))))))






;; We want the user to keep taking shots until they hit the target. We'll use recursion to
;; loop the "take a shot" logic, but we'll need the target distance and (for fun) a log of
;; all previous shots to be passed to each recursive call. We'll package these into a "game state"
;; object.
(defn make-state [target-distance shot-log]
  ;; We will *secretly* use a hashmap to store the game state data.
  {:target-distance target-distance,
   :shot-log shot-log})

;; Selectors for game states.
(defn target-distance [game-state]
  (:target-distance game-state))

(defn shot-log [game-state]
  (:shot-log game-state))

;; Because our game-state implementation is "private", we don't want other people cons-ing
;; onto the shot log. So this "constructor" makes a new shot log given an old log and a new shot.
(defn add-log [old-log new-shot]
  (cons new-shot old-log))


;; Plays one round of the game. Given a game state (with a target distance and a log of previous shots),
;; input a single shot from the user. If the shot is a hit, terminate the game and return the game state.
;; Otherwise, recursively call one-round with the new game state; the recursive call will eventually
;; return the final game state, including the log of all shots taken.
(defn one-round [game-state]
  (let [target (target-distance game-state)
        angle (get-angle)
        gunpowder (get-gunpowder)
        distance (calculate-distance angle gunpowder)
        new-state (make-state target (add-log (shot-log game-state) distance))]
    (if (hit? target distance)
      (do (println "Great job, you hit the target!")
          ;; Return the game state, which includes the updated game log.
          new-state)
      (let [diff (- distance target)]
        (println "You were"
                 (Math/abs diff) "m too"
                 (if (pos? diff) "far." "short."))
        ;; Recurse to the next round with the mutated game state.
        (one-round new-state)))))


;; one-round assumes a game state has already been created. This is the real
;; "entry point" of the game.
(defn play-game []
  ;; Generate and print the target distance.
  (let [distance (random-target 1000)]
    (println "The target is" distance "m away.")
    ;; Call one-round, which will recurse until the user hits the target.
    (let [result-state (one-round (make-state distance nil))]
      ;; Print the results.
      (println "It only took you"
               (count (shot-log result-state))
               "shots to hit the target!"))))












;; On a REPL, you will need to uncomment the last line of the file for this main to run.
(defn -main [& args]
  (play-one-shot))

;;(-main)