module internal FranceSSNValidation

open System
open FranceSSNGeneration
open FranceSSNParameters
open Util
open StringUtil
open CommonValidation
open Types.SSNTypes

let getCalculatedCs (ssn: string) =
    let genderNumber     = ssn |> substring GenderStart GenderLength
    let year             = ssn |> substring YearStart YearLength
    let month            = ssn |> substring MonthStart MonthLength
    let department       = ssn |> substring DepartmentStart DepartmentLength
    let commune          = ssn |> substring CommuneStart CommuneLength
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength
    
    generateChecksum genderNumber year month department commune individualNumber

let hasCorrectGenderNumber (ssn: string) = 
    let genderNumber = ssn |> substring GenderStart GenderLength |> int

    match genderNumber with
    | 0 | 1 | 2 | 7 | 8 -> Success ssn
    | _                 -> Failure InvalidGenderNumber

let hasCorrectDepartmentNumber (ssn: string) =
    let departmentNumber = ssn |> substring DepartmentStart DepartmentLength

    let isInt, parsedDepartmentNumber = Int32.TryParse departmentNumber

    match isInt with
    | true  ->
        match parsedDepartmentNumber with
        | 20 | 98                     -> Failure InvalidDepartmentNumber
        | Between 1 95                -> Success ssn
        | 97                          ->
            let threeDigitDepartmentNumber = ssn |> substring DepartmentStart (DepartmentLength + 1)
            let isTdnInt, parsedThreeDigitDepartmentNumber = Int32.TryParse threeDigitDepartmentNumber

            match isTdnInt, parsedThreeDigitDepartmentNumber with
            | true, Between 971 976 -> Success ssn
            | _                     -> Failure InvalidDepartmentNumber
        | _                           -> Failure InvalidDepartmentNumber
    | false ->
        match departmentNumber with
        | "2A" | "2B" -> Success ssn
        | _           -> Failure InvalidDepartmentNumber

let hasCorrectCommuneNumber (ssn: string) =
    let departmentNumber = ssn |> substring DepartmentStart DepartmentLength
    let communeNumber    = ssn |> substring CommuneStart    CommuneLength

    match departmentNumber with
    | "97" ->
        let twoDigitCommuneNumber = ssn |> substring (CommuneStart + 1) (CommuneLength - 1)
        let isInt, parsedTwoDigitCommuneNumber = Int32.TryParse twoDigitCommuneNumber

        match isInt, parsedTwoDigitCommuneNumber with
        | true, Between 1 90 -> Success ssn
        | _, _               -> Failure InvalidCommuneNumber
    | _    ->
        let isInt, parsedCommuneNumber = Int32.TryParse communeNumber

        match isInt, parsedCommuneNumber with
        | true, Between 1 990 -> Success ssn 
        | _, _                -> Failure InvalidCommuneNumber

let monthWhitelist = [ 20 ]

let validateSSNForFrance = 
    hasCorrectShape "^\d{7}[A|B|0-9]\d{7}$"
    >> bind hasCorrectGenderNumber
    >> bind hasCorrectDepartmentNumber
    >> bind hasCorrectCommuneNumber
    >> bind (hasCorrectYear YearStart YearLength)
    >> bind (hasCorrectMonthWithExtraWhitelist MonthStart MonthLength monthWhitelist)
    >> bind (hasIndividualNumber IndividualNumberStart IndividualNumberLength)
    >> bind (hasCorrectChecksum getCalculatedCs ChecksumStart ChecksumLength)
    >> toOutputResult
