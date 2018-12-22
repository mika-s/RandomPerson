module PostalCodeAndCity

/// A class representing postal code and city of a person's address.
[<NoEquality;NoComparison>]
type PostalCodeAndCity(postalCode: string, city: string) =
    member __.PostalCode = postalCode
    member __.City = city
