// Weapon types
type MeleeWeapon = {damage : int}
type RangedWeapon = {power: int; minimumRange: int}
type Weapon = 
    | Sword of MeleeWeapon
    | Bow of RangedWeapon


// Heroes
type Hero = {name : string; strength : int; defense: int ; hitPoints : int ; weapon : Weapon; position : int; movement : int} 

let mitama = {name = "Mitama"; strength = 10; defense = 8; hitPoints = 30; 
              weapon = Bow {power = 5; minimumRange = 2};
              position = 6; movement = 1}

let severa = {name = "Severa"; strength = 10; defense = 8; hitPoints = 30; 
              weapon = Sword {damage = 8};
              position = 1 ; movement = 2}


// Moves a hero by the given amount, returning a new copy of the moved hero.
let move hero amount =
    {hero with position = hero.position + amount}


// Returns true if the hero is in range to attack the target, depending on their weapon.
let canAttack hero target =
    match hero.weapon with
    | Sword s -> abs (hero.position - target.position) = 1
    | Bow b -> abs (hero.position - target.position) >= b.minimumRange
    

// Cause a hero to attack a target, returning a new copy of the target after damage is dealt.
let attack hero target =
    // Damage formula: weaponPower + hero's strength - target's defense
    let weaponPower = 
        match hero.weapon with
        | Sword s -> s.damage
        | Bow b -> b.power

    if not (canAttack hero target) then
        // can't actually attack the target, so return the original value.
        target
    else
        // we can attack the target. determine the amount of damage.
        let damage = max 0 (weaponPower + hero.strength - target.defense)
        // return a copy of the target with modified hit points.
        {target with hitPoints = target.hitPoints - damage}
        

// Returns a simplified string for a hero, only showing their name, HP, and position.
let heroToString hero =
    // sprintf is like printf, but returns a string instead of printing to console.
    sprintf "{%s: %d HitPoints, @Position %d}" hero.name hero.hitPoints hero.position


// Simulate a battle between h1 and h2. The winner is returned.
let rec battle h1 h2 =
    // a Hero with a Bow will attack if they are at least their minimumRange away, otherwise they will move away
    //      from the opponent.
    // a Hero with a Sword will attack if they are next to the opponent, otherwise they will move towards the
    //      opponent and then attack if possible.
    // In the end, return the winner.
    printfn "Ready.... fight! \n %O vs %O" (heroToString h1) (heroToString h2)

    // Decide on an action to take by the given hero vs the given opponent.
    // BOTH the hero and the opponent are returned as a tuple, in that order.
    let takeAction hero opponent =
        match hero.weapon with
        | Bow b -> 
            // Attack if minimumRange is good, otherwise move 1 square.
            if canAttack hero opponent then
                printfn "%s attacks %s with a bow" hero.name opponent.name 
                (hero, attack hero opponent)
            else
                printfn "%s moves %d" hero.name hero.movement
                (move hero hero.movement, opponent)
        | Sword s -> 
            // Attack if next to the opponent.
            if canAttack hero opponent then
                printfn "%s attacks %s with a sword" hero.name opponent.name
                (hero, attack hero opponent)
            else
                // Otherwise, move closer to the opponent, stopping 1 position short of them.
                let distanceMoved = min hero.movement (opponent.position - hero.position - 1)
                printfn "%s moves %d" hero.name distanceMoved
                let moved = move hero distanceMoved

                // If we didn't use all our movement "points", then we can also attack.
                if distanceMoved < hero.movement && canAttack moved opponent then
                    printfn "%s attacks %s with a sword after moving" hero.name opponent.name
                    (moved, attack moved opponent)
                else
                    (moved, opponent)
    
    // Have h1 take an action first. The result is the new h1 and h2.
    let (act1, act2) = takeAction h1 h2 

    // If the new h2 is not dead.
    if act2.hitPoints > 0 then
        // Then let h2 take an action.
        let (final2, final1) = takeAction act2 act1

        // If the final h1 is dead, then h2 is the winner. 
        if final1.hitPoints = 0 then
            final2
        else
            // Otherwise, do the next round in the battle and return that result.
            battle final1 final2
    else
        // Otherwise, the new h1 is the winner.
        act1
        

[<EntryPoint>]
let main argv = 
    battle mitama severa
    |> heroToString
    |> printfn "Winner: %O"
    0
