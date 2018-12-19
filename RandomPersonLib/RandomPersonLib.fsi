namespace RandomPersonLib

[<Interface>]
type IRandomPerson =
    abstract member CreatePerson: Nationality -> Person
    abstract member CreatePerson: Nationality * RandomPersonOptions -> Person
    abstract member CreatePeople: int * Nationality -> Person seq
    abstract member CreatePeople: int * Nationality * RandomPersonOptions -> Person seq
    abstract member CreatePeopleTemplated: int * Nationality * string -> string seq
    abstract member CreatePeopleTemplated: int * Nationality * string * RandomPersonOptions -> string seq
    abstract member ValidateSSN: Nationality * string -> bool

type RandomPerson =
    new : unit -> RandomPerson
    member CreatePerson: Nationality -> Person
    member CreatePerson: Nationality * RandomPersonOptions -> Person
    member CreatePeople: int * Nationality -> Person list
    member CreatePeople: int * Nationality * RandomPersonOptions -> Person list
    member CreatePeopleTemplated: int * Nationality * string -> string list
    member CreatePeopleTemplated: int * Nationality * string * RandomPersonOptions -> string list
    member ValidateSSN: Nationality * string -> bool

    interface IRandomPerson
