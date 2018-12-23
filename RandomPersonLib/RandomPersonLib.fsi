namespace RandomPersonLib

[<Interface>]
type IRandomPerson =
    abstract member CreatePerson: Country -> Person
    abstract member CreatePerson: Country * RandomPersonOptions -> Person
    abstract member CreatePeople: int * Country -> Person seq
    abstract member CreatePeople: int * Country * RandomPersonOptions -> Person seq
    abstract member CreatePeopleTemplated: int * Country * string -> string seq
    abstract member CreatePeopleTemplated: int * Country * string * RandomPersonOptions -> string seq
    abstract member ValidateSSN: Country * string -> bool

type RandomPerson =
    new : unit -> RandomPerson
    member CreatePerson: Country -> Person
    member CreatePerson: Country * RandomPersonOptions -> Person
    member CreatePeople: int * Country -> Person list
    member CreatePeople: int * Country * RandomPersonOptions -> Person list
    member CreatePeopleTemplated: int * Country * string -> string list
    member CreatePeopleTemplated: int * Country * string * RandomPersonOptions -> string list
    member ValidateSSN: Country * string -> bool

    interface IRandomPerson
