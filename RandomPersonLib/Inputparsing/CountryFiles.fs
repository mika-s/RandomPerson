module CountryFiles

open PersonData
open PostalCodeAndCity

[<NoEquality;NoComparison>]
type CountryFiles = {
    generalData: PersonData
    addresses1: string[]
    postalCodesAndCities: PostalCodeAndCity[]
}
