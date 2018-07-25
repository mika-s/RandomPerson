module internal TemplatePrint

open OrdinaryReplaces
open SpecialReplaces
open RandomPersonLib

let printForTemplateMode (originalOutput: string) (person: Person) =
    parseOrdinaryReplaces originalOutput person
    |> parseSpecialReplaces
