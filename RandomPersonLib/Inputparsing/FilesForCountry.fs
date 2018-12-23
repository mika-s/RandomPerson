module FilesForCountry

open PersonData
open PostalCodeAndCity

[<NoEquality;NoComparison>]
type FilesForCountry = {
    generalData: PersonData
    addresses1: string[]
    postalCodesAndCities: PostalCodeAndCity[]
}
