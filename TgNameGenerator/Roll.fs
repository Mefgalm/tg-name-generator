module TgNameGenerator.Roll

open System
let random = Random()

let range f t = random.Next(f, t + 1)

let d4() = range 1 4

let d6() = range 1 6

let d8() = range 1 8

let d10() = range 1 10

let d12() = range 1 12

let d20() = range 1 20

let (<!>) x (f: unit -> int) = List.init x (fun _ -> f())

let (|Rare|Standard|Often|) x =
    match (range 1 6) with
    | 1 -> Rare
    | 2 -> Often
    | 3 -> Often
    | 4 -> Standard
    | 5 -> Often
    | 6 -> Rare
    | _ -> failwith "not ok :("

let shouldReRollRare() = (range 1 10) < 8

let shouldReRollOfter() = (range 1 10) >= 8

let sd6() =
    let rec loop roll =
        match roll with
        | Rare when shouldReRollRare() -> loop (d6())
        | Often when shouldReRollOfter() -> loop (d6())
        | _ -> roll
        
    loop (d6())