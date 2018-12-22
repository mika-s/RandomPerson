module FilesForLanguage

open PersonData
open PostalCodeAndCity

[<NoEquality;NoComparison>]
type FilesForLanguage = {
    generalData: PersonData
    addresses1: string[]
    postalCodesAndCities: PostalCodeAndCity[]
}
