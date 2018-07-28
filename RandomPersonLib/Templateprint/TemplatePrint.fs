module internal TemplatePrint

open OrdinaryReplaces
open SpecialGenderReplaces
open SpecialBirthDateReplaces
open RandomReplaces
open RandomPersonLib

let printForTemplateMode (originalOutput: string) (person: Person) =
    originalOutput
    |> performOrdinaryReplaces person
    |> performRandomReplaces
    |> performSpecialGenderReplaces person.Gender
    |> performSpecialBirthDateReplaces person.BirthDate
