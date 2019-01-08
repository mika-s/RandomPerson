module internal SwedenSSNValidation

open System.Text.RegularExpressions
open CommonValidation
open SwedenSSNGeneration
open SwedenSSNParameters
open Util
open StringUtil

let (|OldSSNForSweden|NewSSNForSweden|NotSSN|) (ssn: string) =
    let regexMatchOld = Regex.Match(ssn, "^\d{6}-\d{4}$")
    let regexMatchNew = Regex.Match(ssn, "^\d{8}-\d{4}$")

    match (regexMatchOld.Success, regexMatchNew.Success) with
    | (_, true) -> NewSSNForSweden
    | (true, _) -> OldSSNForSweden
    | _         -> NotSSN

let getCalculatedCs (p: SSNParams) (ssn: string) =
    let birthDate = match p.SsnLength with
                    | Equals oldSsnParams.SsnLength -> ssn |> substring p.DateStart p.DateLength
                    | Equals newSsnParams.SsnLength -> ssn |> substring (p.DateStart + 2) (p.DateLength - 2)   // omit two first digits in the date
                    | _  -> invalidOp "Wrong SSN length in parameters."

    let individualNumber = ssn |> substring p.IndividualNumberStart p.IndividualNumberLength

    generateChecksum (birthDate + individualNumber)

let validateSSNForSwedenGivenParams (p: SSNParams) =
    hasDate p.DateFormat p.DateStart p.DateLength
    >> bind (hasIndividualNumber p.IndividualNumberStart p.IndividualNumberLength)
    >> bind (hasCorrectChecksum (getCalculatedCs p) p.ChecksumStart p.ChecksumLength)
    >> toOutputResult

let validateSSNForSweden (ssn: string) =
    match ssn with 
    | OldSSNForSweden -> validateSSNForSwedenGivenParams oldSsnParams ssn
    | NewSSNForSweden -> validateSSNForSwedenGivenParams newSsnParams ssn
    | NotSSN          -> (false, "The shape is wrong.")
