module internal StringUtil

open System

let substring (startIndex: int) (length: int)   (str: string) = str.Substring(startIndex, length)
let insert    (startIndex: int) (value: string) (str: string) = str.Insert(startIndex, value)
let inline trim (str: string) = str.Trim()
let inline removeChar (charToRemove: string) (str: string) = str.Replace(charToRemove, String.Empty)
let lastChar (str: string) = str |> substring (str.Length - 1) 1

let uppercase (str: string) = str.ToUpperInvariant()
let lowercase (str: string) = str.ToLowerInvariant()

let capitalize (str: string) =
    if str.Length = 0 then str
    else uppercase str.[0..0] + str.[ 1 .. str.Length - 1 ]

let uncapitalize (str: string) =
    if str.Length = 0 then str
    else lowercase str.[0..0] + str.[ 1 .. str.Length - 1 ]

let firstUppercaseRestLowercase (str: string) =
    if str.Length = 0 then str
    else uppercase str.[0..0] + lowercase str.[ 1 .. str.Length - 1 ]
