module internal UsaSSNValidation

open CommonValidation
open UsaSSNParameters
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectAreaNumber (ssn: string) = 
    let areaNumber = ssn |> substring AreaNumberStart AreaNumberLength |> int

    match areaNumber with
    | x when x = 0    -> Failure WrongAreaNumber
    | x when x = 666  -> Failure WrongAreaNumber
    | x when 900 <= x -> Failure WrongAreaNumber
    | _               -> Success ssn

let hasCorrectGroupNumber (ssn: string) =
    let groupNumber = ssn |> substring GroupNumberStart GroupNumberLength |> int

    match groupNumber with
    | x when x = 0  -> Failure WrongGroupNumber
    | _             -> Success ssn

let hasCorrectSerialNumber (ssn: string) =
    let serialNumber = ssn |> substring SerialNumberStart SerialNumberLength |> int

    match serialNumber with
    | x when x = 0  -> Failure WrongSerialNumber
    | _             -> Success ssn

let validateSSNForUSA =
    hasCorrectShape "^\d{3}-\d{2}-\d{4}$"
    >> bind hasCorrectAreaNumber
    >> bind hasCorrectGroupNumber
    >> bind hasCorrectSerialNumber
    >> toOutputResult
