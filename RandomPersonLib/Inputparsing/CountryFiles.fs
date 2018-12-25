module CountryFiles

open PersonData
open PostalCodeCityState

[<NoEquality;NoComparison>]
type CountryFiles = {
    generalData: PersonData
    addresses1: string[]
    postalCodeCityStates: PostalCodeCityState[]
}
