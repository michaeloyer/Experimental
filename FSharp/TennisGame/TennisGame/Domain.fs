module TennisGame.Domain

type Score =
    | Love
    | Fifteen
    | Thirty
    | Forty
    | Advantage

type Outcome =
    | OnGoing of Score * Score
    | GameReceiver
    | GameServer

type Match = Score * Score

let startMatch() = Match(Love, Love)

let scoreServer gameMatch =
    match gameMatch with
    | Forty, Forty -> OnGoing (Advantage, Forty)
    | Advantage, _ -> GameServer
    | _, Advantage -> OnGoing (Forty, Forty)
    | Love, score -> OnGoing (Fifteen, score)
    | Fifteen, score -> OnGoing (Thirty, score)
    | Thirty, score -> OnGoing (Forty, score)
    | Forty, _ -> GameServer

let scoreReceiver gameMatch =
    match gameMatch with
    | _, Advantage -> GameReceiver
    | Forty, Forty -> OnGoing (Forty, Advantage)
    | Advantage, _ -> OnGoing (Forty, Forty)
    | score, Love -> OnGoing (score, Fifteen)
    | score, Fifteen -> OnGoing (score, Thirty)
    | score, Thirty -> OnGoing (score, Forty)
    | _, Forty -> GameReceiver

let getScore outcome =
    match outcome with
    | GameServer -> "Victory Server"
    | GameReceiver -> "Victory Receiver"
    | OnGoing (Advantage, _) -> "Adv. Server"
    | OnGoing (_, Advantage) -> "Adv. Receiver"
    | OnGoing (Forty, Forty) -> "Deuce"
    | OnGoing (score1, score2) -> $"{score1}-{score2}"
