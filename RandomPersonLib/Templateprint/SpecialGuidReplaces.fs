module internal SpecialGuidReplaces

open System
open System.Text.RegularExpressions

let replaceGuidWithoutFormat (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let guid = Guid.NewGuid().ToString()
        regex.Replace(remaining, guid, 1)
    | false -> remaining

let replaceGuidWithFormat (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let format = matching.Groups.[1].Value
        let guid = Guid.NewGuid().ToString(format)
        regex.Replace(remaining, guid, 1)
    | false -> remaining

let performSpecialGuidReplaces (stringToDoReplaces: string) =
    let guidWithoutFormatRegex = Regex "#{GUID\(\s?\)}"
    let guidWithFormatRegex    = Regex "#{GUID\(\s?'([NDBPX])'\s?\)}"
    
    let rec loop (remaining: string) =

        let modified = remaining
                       |> replaceGuidWithoutFormat guidWithoutFormatRegex
                       |> replaceGuidWithFormat    guidWithFormatRegex

        let isMoreRemaining =
            (guidWithoutFormatRegex.Match modified).Success ||
            (guidWithFormatRegex.Match    modified).Success

        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
