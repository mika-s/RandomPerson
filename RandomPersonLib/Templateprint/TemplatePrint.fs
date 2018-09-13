module internal TemplatePrint

open OrdinaryReplaces
open SpecialBirthDateReplaces
open SpecialDateReplaces
open SpecialGenderReplaces
open SpecialGuidReplaces
open RandomReplaces
open RandomPersonLib

let printForTemplateMode (originalOutput: string) (person: Person) =
    originalOutput
    |> performOrdinaryReplaces person
    |> performRandomReplaces
    |> performSpecialBirthDateReplaces person.BirthDate
    |> performSpecialDateReplaces
    |> performSpecialGenderReplaces person.Gender
    |> performSpecialGuidReplaces
