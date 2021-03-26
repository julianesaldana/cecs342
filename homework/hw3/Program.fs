/// Card representations.
// An "enum"-type union for card suit.
type CardSuit = 
    | Spades 
    | Clubs
    | Diamonds
    | Hearts

// Kinds: 1 = Ace, 2 = Two, ..., 11 = Jack, 12 = Queen, 13 = King.
type Card = {
    suit : CardSuit;
    kind : int
}


/// Game state records.
// One hand being played by the player: its cards, and a flag for whether it was doubled-down.
type PlayerHand = {
    cards: Card list; 
    doubled: bool
}

// All the hands being played by the player: the hands that are still being played (in the order the player must play them),
// and the hands that have been finished (stand or bust).
type PlayerState = {
    activeHands: PlayerHand list; 
    finishedHands: PlayerHand list
}

// The state of a single game of blackjack. Tracks the current deck, the player's hands, and the dealer's hand.
type GameState = {
    deck : Card list; 
    player : PlayerState; 
    dealer: Card list
}

// A log of results from many games of blackjack.
type GameLog = {
    playerWins : int;
    dealerWins : int;
    draws : int
}

/// Miscellaneous enums.
// Identifies whether the player or dealer is making some action.
type HandOwner = 
    | Player 
    | Dealer

// The different actions a player can take.
type PlayerAction = 
    | Hit
    | Stand
    | DoubleDown
    | Split

// The result of one hand that was played.
type HandResult = 
    | Win
    | Lose
    | Draw


// This global value can be used as a source of random integers by writing
// "rand.Next(i)", where i is the upper bound (exclusive) of the random range.
let rand = new System.Random()


// UTILITY METHODS

// Returns a string describing a card.
let cardToString card =
    // TODO: replace the following line with logic that converts the card's kind to a string.
    // Reminder: a 1 means "Ace", 11 means "Jack", 12 means "Queen", 13 means "King".
    // A "match" statement will be necessary. (The next function below is a hint.)
    let kind = string card.kind
    
    let actualKind = 
        match kind with
        | "1" -> "Ace"
        | "11" -> "Jack"
        | "12" -> "Queen"
        | "13" -> "King"
        | _ -> kind

    // "%A" can print any kind of object, and automatically converts a union (like CardSuit)
    // into a simple string.
    sprintf "%s of %A" actualKind card.suit


// Returns a string describing the cards in a hand.    
let handToString hand = 
    // TODO: replace the following line with statement(s) to build a string describing the given hand.
    // The string consists of the results of cardToString when called on each Card in the hand (a Card list),
    // separated by commas. You need to build this string yourself; the built-in "toString" methods for lists
    // insert semicolons and square brackets that I do not want.
    
    let result = 
        hand
        |> List.map (fun x -> cardToString(x))
        |> List.fold (fun elem acc -> match elem with
                                        | "" -> acc
                                        | _ -> acc + ", " + elem) ""
    sprintf "%s" result


    // Hint: transform each card in the hand to its cardToString representation. Then read the documentation
    // on String.concat.


    
// Returns the "value" of a card in a poker hand, where all three "face" cards are worth 10
// and an Ace has a value of 11.
let cardValue card =
    match card.kind with
    | 1 -> 11
    | 11 | 12 | 13 -> 10  // This matches 11, 12, or 13.
    | n -> n
    
    // Reminder: the result of the match will be returned


// Calculates the total point value of the given hand (Card list). 
// Find the sum of the card values of each card in the hand. If that sum
// exceeds 21, and the hand has aces, then some of those aces turn from 
// a value of 11 to a value of 1, and a new total is computed.
// TODO: fill in the marked parts of this function.
let handTotal hand =
    // TODO: modify the next line to calculate the sum of the card values of each
    // card in the list. Hint: List.map and List.sum. (Or, if you're slick, List.sumBy)
    
    let sum =
        hand
        |> List.map (fun kind -> cardValue kind)
        |> List.sum
        //|> List.sumBy (fun c -> c.kind)

    // TODO: modify the next line to count the number of aces in the hand.
    // Hint: List.filter and List.length. 

    let numAces = hand |> List.filter (fun c -> c.kind = 1) |> List.length

    // Adjust the sum if it exceeds 21 and there are aces.
    if sum <= 21 then
        // No adjustment necessary.
        sum
    else 
        // Find the max number of aces to use as 1 point instead of 11.
        let maxAces = (float sum - 21.0) / 10.0 |> ceil |> int
        // Remove 10 points per ace, depending on how many are needed.
        sum - (10 * (min numAces maxAces))


// FUNCTIONS THAT CREATE OR UPDATE GAME STATES

// Creates a new, unshuffled deck of 52 cards.
// A function with no parameters is indicated by () in the parameter list. It is also invoked
// with () as the argument.
let makeDeck () =
    // Make a deck by calling this anonymous function 52 times, each time incrementing
    // the parameter 'i' by 1.
    // The Suit of a card is found by dividing i by 13, so the first 13 cards are Spades.
    // The Kind of a card is the modulo of (i+1) and 13. 
    List.init 52 (fun i -> let s = match i / 13 with
                                   | 0 -> Spades
                                   | 1 -> Clubs
                                   | 2 -> Diamonds
                                   | 3 -> Hearts
                           {suit = s; kind = i % 13 + 1})


// Shuffles a list by converting it to an array, doing an in-place Fisher-Yates 
// shuffle, then converting back to a list.
// Don't worry about this.
let shuffleDeck (deck : Card list) =
    let arr = List.toArray deck

    let swap (a: _[]) x y =
        let tmp = a.[x]
        a.[x] <- a.[y]
        a.[y] <- tmp
    
    Array.iteri (fun i _ -> swap arr i (rand.Next(i, Array.length arr))) arr
    Array.toList arr


// Creates a new game state using the given deck, dealing 2 cards to the player and dealer.
let newGame (deck : Card list) =
    // Construct the starting hands for player and dealer.
    let playerCards = [deck.Head ; List.item 2 deck] // First and third cards.
    let dealerCards = [deck.Tail.Head ; List.item 3 deck] // Second and fourth.

    // Return a fresh game state.
    {deck = List.skip 4 deck;
    // the initial player has only one active hand.
     player = {activeHands = [{cards = playerCards; doubled = false}]; finishedHands = []}
     dealer = dealerCards}


// Given a current game state and an indication of which player is "hitting", deal one
// card from the deck and add it to the given person's hand. Return the new game state.
let hit handOwner gameState = 
    let topCard = List.head gameState.deck
    let newDeck = List.tail gameState.deck
    
    // Updating the dealer's hand is easy.
    if handOwner = Dealer then
        let newDealerHand = topCard :: gameState.dealer
        // Return a new game state with the updated deck and dealer hand.
        {gameState with deck = newDeck;
                        dealer = newDealerHand}
    else
        let isDoubled = gameState.player.activeHands.Head.doubled
        let updatedPlayerHand = {cards = topCard :: gameState.player.activeHands.Head.cards; doubled = isDoubled}
        let newActiveHands = updatedPlayerHand :: gameState.player.activeHands.Tail // removes the old non-updated hand and replaces it with the updated hand
        let updatedPlayerState = {activeHands = newActiveHands; finishedHands = gameState.player.finishedHands}
        {gameState with deck = newDeck;
                        player = updatedPlayerState}    

        // TODO: updating the player is trickier. We are always working with the player's first
        // active hand. Create a new first hand by adding the top card to that hand's card list.
        // Then update the player's active hands so that the new first hand is head of the list; and the
        //     other (unchanged) active hands follow it.
        // Then construct the new game state with the updated deck and updated player.

        // TODO: this is just so the code compiles; fix it.
        //gameState


// Take the dealer's turn by repeatedly taking a single action, hit or stay, until 
// the dealer busts or stays.
let rec dealerTurn gameState =
    let dealer = gameState.dealer
    let score = handTotal dealer

    printfn "Dealer's hand: %s; %d points" (handToString dealer) score
    
    // Dealer rules: must hit if score < 17.
    if score > 21 then
        printfn "Dealer busts!"
        // The game state is unchanged because we did not hit. 
        // The dealer does not get to take another action.
        gameState
    elif score < 17 then
        printfn "Dealer hits"
        // The game state is changed; the result of "hit" is used to build the new state.
        // The dealer gets to take another action using the new state.
        gameState
        |> hit Dealer
        |> dealerTurn
    else
        // The game state is unchanged because we did not hit. 
        // The dealer does not get to take another action.
        printfn "Dealer must stay"
        gameState
        
let activeToInactive gameState = 
    let toInactive = gameState.player.activeHands.Head
    let newActive = gameState.player.activeHands.Tail
    let newPlayerState = {activeHands = newActive; finishedHands = toInactive :: gameState.player.finishedHands}
    {gameState with player = newPlayerState}

let setDoubled (gameState : GameState) =
    let newPlayerHand = {cards = gameState.player.activeHands.Head.cards; doubled = true}
    let newPlayerState = {activeHands = newPlayerHand :: gameState.player.activeHands.Tail; finishedHands = gameState.player.finishedHands}
    {gameState with player = newPlayerState}



// Take the player's turn by repeatedly taking a single action until they bust or stand.
let rec playerTurn (playerStrategy : GameState->PlayerAction) (gameState : GameState) =
    // TODO: code this method using dealerTurn as a guide. Follow the same standard
    // of printing output. This function must return the new game state after the player's
    // turn has finished, like dealerTurn.

    // Unlike the dealer, the player gets to make choices about whether they will hit or stay.
    // The "elif score < 17" code from dealerTurn is inappropriate; in its place, we will
    // allow a "strategy" to decide whether to hit. A "strategy" is a function that accepts
    // the current game state and returns true if the player should hit, and false otherwise.
    // playerTurn must call that function (the parameter playerStrategy) to decide whether
    // to hit or stay.
    let playerState = gameState.player

    if playerState.activeHands.IsEmpty then
        // A player with no active hands cannot take an action.
        gameState
    else
        let playerCards = gameState.player.activeHands.Head.cards
        let score = handTotal playerCards
        let topHand = playerState.activeHands.Head

        printfn "Player's hand: %s; %d points" (handToString playerCards) score

        if score > 21 then  // handles busts
            printfn "Player busts!"
            gameState 
            |> activeToInactive // newly created function, located above playerTurn
            |> playerTurn playerStrategy
        else
            match playerStrategy gameState with
                | Hit ->
                    gameState
                    |> hit Player
                    |> playerTurn playerStrategy
                | Stand ->
                    printfn "Points: %d" (handTotal playerState.activeHands.Head.cards)

                    gameState 
                    |> activeToInactive 
                    |> playerTurn playerStrategy

                | DoubleDown ->
                    let result = 
                        gameState
                        |> hit Player
                        |> setDoubled

                    printfn "Doubling down, Player's hand: %s; %d points" (handToString result.player.activeHands.Head.cards) (handTotal result.player.activeHands.Head.cards)
                    
                    result
                    |> activeToInactive
                    |> playerTurn playerStrategy
                | Split ->
                    printfn "Splitting.."
                    let firstSplitHand = {cards = [gameState.player.activeHands.Head.cards.Head]; doubled = false}
                    let secondSplitHand = {cards = [gameState.player.activeHands.Head.cards.Tail.Head]; doubled = false}

                    let firstSplitGameState = {deck = gameState.deck; player = {activeHands = [firstSplitHand]; finishedHands = gameState.player.finishedHands}; dealer = gameState.dealer}
                    let firstSplitGameStateUpdated = firstSplitGameState |> hit Player
                    
                    let secondSplitGameState = {deck = firstSplitGameStateUpdated.deck; player = {activeHands = [secondSplitHand]; finishedHands = firstSplitGameStateUpdated.player.finishedHands}; dealer = firstSplitGameStateUpdated.dealer}
                    let secondSplitGameStateUpdated = secondSplitGameState |> hit Player    // only care about activeHands, deck

                    let trailing = secondSplitGameStateUpdated.player.activeHands.Head :: gameState.player.activeHands.Tail

                    {deck = secondSplitGameStateUpdated.deck; player = {activeHands = firstSplitGameStateUpdated.player.activeHands.Head :: trailing; finishedHands = secondSplitGameStateUpdated.player.finishedHands}; dealer = secondSplitGameStateUpdated.dealer}
                    |> playerTurn playerStrategy

               

        // The next line is just so the code compiles. Remove it when you code the function.
        // TODO: print the player's first active hand. Call the strategy to get a PlayerAction.
        // Create a new game state based on that action. Recurse if the player can take another action 
        // after their chosen one, or return the game state if they cannot.
        

        // Remove this when you're ready; it's just so the code compiles.
        //gameState
                        


let oneGame playerStrategy gameState =
    // TODO: print the first card in the dealer's hand to the screen, because the Player can see
    // one card from the dealer's hand in order to make their decisions.
    
    printfn "Dealer is showing: %s" (cardToString gameState.dealer.Head)

    // TODO: play the game! First the player gets their turn. The dealer then takes their turn,
    // using the state of the game after the player's turn finished.
    printfn "Player's turn"
    let state = playerTurn playerStrategy gameState

    printfn "\nDealer's turn"
    let nextState = dealerTurn state

    let dealerScore = handTotal nextState.dealer

    if dealerScore > 21 then
        let busts =
            nextState.player.finishedHands
            |> List.filter (fun x -> 21 < handTotal x.cards)
            |> List.map (fun x -> if x.doubled then 2 else 1)
            |> List.sum
        let wins =
            nextState.player.finishedHands
            |> List.map (fun x -> if x.doubled then 2 else 1)
            |> List.sum
        {playerWins = wins - busts; dealerWins = busts; draws = 0}
    else
       let wins =
            nextState.player.finishedHands
            |> List.filter (fun x -> handTotal x.cards > dealerScore && handTotal x.cards <= 21)
            |> List.map (fun x -> if x.doubled then 2 else 1)
            |> List.sum
       let busts =
            nextState.player.finishedHands
            |> List.filter (fun x -> handTotal x.cards < dealerScore || handTotal x.cards > 21)
            |> List.map (fun x -> if x.doubled then 2 else 1)
            |> List.sum
       let playerDraws = (nextState.player.activeHands |> List.filter (fun x -> handTotal x.cards = dealerScore) |> List.length) + (nextState.player.finishedHands |> List.filter (fun x -> handTotal x.cards = dealerScore) |> List.length)
       {playerWins = wins; dealerWins = busts; draws = playerDraws}


// Plays n games using the given playerStrategy, and returns the combined game log.
let manyGames n playerStrategy =
    //// TODO: run oneGame with the playerStrategy n times, and accumulate the result. 
    //// If you're slick, you won't do any recursion yourself. Instead read about List.init, 
    //// and then consider List.reduce.

    //// TODO: this is a "blank" GameLog. Return something more appropriate.
    //// let gameResults = {playerWins = 0; dealerWins = 0; draws = 0}
    List.init n (fun i -> oneGame playerStrategy (makeDeck() |> shuffleDeck |> newGame))
    |> List.reduce (fun a b -> {playerWins = a.playerWins + b.playerWins; dealerWins = a.dealerWins + b.dealerWins; draws = a.draws + b.draws})

        
// PLAYER STRATEGIES
// Returns a list of legal player actions given their current hand.
let legalPlayerActions playerHand =
    let legalActions = [Hit; Stand; DoubleDown; Split]
    // One boolean entry for each action; True if the corresponding action can be taken at this time.
    let requirements = [
        handTotal playerHand < 21; 
        true; 
        playerHand.Length = 2;
        playerHand.Length = 2 && cardValue playerHand.Head = cardValue playerHand.Tail.Head
    ]

    List.zip legalActions requirements // zip the actions with the boolean results of whether they're legal
    |> List.filter (fun (_, req) -> req) // if req is true, the action can be taken
    |> List.map (fun (act, _) -> act) // return the actions whose req was true


// Get a nice printable string to describe an action.
let actionToString = function
    | Hit -> "(H)it"
    | Stand -> "(S)tand"
    | DoubleDown -> "(D)ouble down"
    | Split -> "S(p)lit"

// This strategy shows a list of actions to the user and then reads their choice from the keyboard.
let rec interactivePlayerStrategy gameState =
    let playerHand = gameState.player.activeHands.Head
    let legalActions = legalPlayerActions playerHand.cards

    legalActions
    |> List.map actionToString
    |> String.concat ", "
    |> printfn "What do you want to do? %s" 

    let answer = System.Console.ReadLine()
    // Return true if they entered "y", false otherwise.
    match answer.ToLower() with
    | "h" when List.contains Hit legalActions -> Hit
    | "s" -> Stand
    | "d" when List.contains DoubleDown legalActions -> DoubleDown
    | "p" when List.contains Split legalActions -> Split
    | _ -> printfn "Please choose one of the available options, dummy."
           interactivePlayerStrategy gameState

let inactivePlayerStrategy gameState =
    Stand

let greedyPlayerStrategy gameState = 
    let playerHand = gameState.player.activeHands.Head.cards
    if handTotal playerHand < 21 then
        Hit
    else
        Stand
    

let coinFlipPlayerStrategy gameState=
    let playerHand = gameState.player.activeHands.Head.cards
    if rand.Next(1) = 0 then
        Hit
    else
        Stand

let basicPlayerStrategy gameState = 
    let playerHand = gameState.player.activeHands.Head.cards
    let dealerFirstCard = gameState.dealer.Head
    
    let numOfAces = playerHand |> List.filter (fun n -> n.kind = 1) |> List.length

    if handTotal playerHand = 11 then
        DoubleDown
    elif playerHand.Head.kind = 5 && playerHand.Tail.Head.kind = 5 then
        DoubleDown
    elif handTotal playerHand = 10 then
        if cardValue dealerFirstCard = 10 || cardValue dealerFirstCard = 11 then
            Hit
        else
            DoubleDown
    elif handTotal playerHand = 9 then
        if cardValue dealerFirstCard = 2 || cardValue dealerFirstCard > 7 then
            Hit
        else
            DoubleDown
    elif cardValue playerHand.Head = cardValue playerHand.Tail.Head then
        if handTotal playerHand = 20 then
            Stand
        else
            Split
    else
        if cardValue dealerFirstCard >= 2 && cardValue dealerFirstCard <= 6 then
            if handTotal playerHand > 12 then
                Stand
            else
                Hit
        elif cardValue dealerFirstCard >= 7 && cardValue dealerFirstCard <= 10 then
            if handTotal playerHand < 16 then 
                Hit
            else
                Stand
        elif cardValue dealerFirstCard = 11 then
            if handTotal playerHand <= 16 && numOfAces >= 1 then
                Hit
            elif handTotal playerHand <= 11 then
                Hit
            else
                Stand
        else
            Stand


//open MyBlackjack

[<EntryPoint>]
let main argv =
    //makeDeck() 
    //|> shuffleDeck
    //|> newGame
    //|> oneGame interactivePlayerStrategy
    //|> printfn "%A"

    //manyGames 1000 basicPlayerStrategy
    //|> printfn "%A"
    //printfn "1000 games of basicPlayerStrategy"


    //manyGames 1000 inactivePlayerStrategy
    //|> printfn "%A"
    //printfn "1000 games of inactivePlayerStrategy"

    //manyGames 1000 greedyPlayerStrategy
    //|> printfn "%A"
    //printfn "1000 games of greedyPlayerStrategy"


    manyGames 1000 coinFlipPlayerStrategy
    |> printfn "%A"
    printfn "1000 games of coinFlipPlayerStrategy"

    0 // return an integer exit code


