module FilesForLanguage

open PersonData
open PostalCodeAndCity

type filesForLanguage = {
    generalData: PersonData
    addresses1: string[]
    postalCodesAndCities: PostalCodeAndCity[]
}
