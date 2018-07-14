module internal NorwegianSSNParameters

[<Literal>]
let SsnLength = 11

[<Literal>]
let DateStart = 0

[<Literal>]
let DateLength = 6

[<Literal>]
let IndividualNumberStart = 6

[<Literal>]
let IndividualNumberLength = 3

[<Literal>]
let ChecksumStart = 6   // FIX: this seems wrong.

[<Literal>]
let ChecksumLength = 2
