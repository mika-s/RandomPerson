module CommonTemplatePrint

let cleanupValue (input: string) = input.Trim()

let rec removeLastParenthesis (input: string) = 
    let lastIdx = input.Length - 1
    
    let maybeCleaned = match input.[lastIdx] with
                       | '}' | ')' -> input.Remove(lastIdx)
                       | _         -> input
    
    let newLastIdx = maybeCleaned.Length - 1
    let lastCharacter = maybeCleaned.[newLastIdx]
    
    if (lastCharacter <> ')' && lastCharacter <> '}') then
        maybeCleaned
    else
        removeLastParenthesis (maybeCleaned)

let removeLastParenthesisFromArray (input: string[]) =
    let lastElemIdx = input.Length - 1
    let lastString = input.[lastElemIdx]
    
    let newArrayPart1 = input.[.. input.Length - 2]
    let newArrayPart2 = removeLastParenthesis (lastString) |> Array.create 1
    
    Array.concat [ newArrayPart1; newArrayPart2 ]
