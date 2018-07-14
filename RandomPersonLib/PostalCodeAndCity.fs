module PostalCodeAndCity

/// A class representing postal code and city of a person's address.
type PostalCodeAndCity(postalCode: string, city: string) =
    member this.PostalCode = postalCode
    member this.City = city

