module internal SwedenSSNParameters

type SSNParams = {
    SsnLength: int
    DateStart: int
    DateLength: int
    DateFormat: string
    IndividualNumberStart: int
    IndividualNumberLength: int
    ChecksumStart: int
    ChecksumLength: int
}

let oldSsnParams = {
    SsnLength = 11;
    DateStart = 0;
    DateLength = 6;
    DateFormat = "yyMMdd";
    IndividualNumberStart = 7;
    IndividualNumberLength = 3;
    ChecksumStart = 10;
    ChecksumLength = 1;
}

let newSsnParams = {
    SsnLength = 13;
    DateStart = 0;
    DateLength = 8;
    DateFormat = "yyyyMMdd";
    IndividualNumberStart = 9;
    IndividualNumberLength = 3;
    ChecksumStart = 12;
    ChecksumLength = 1
}
