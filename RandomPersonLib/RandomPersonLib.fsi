namespace RandomPersonLib

[<Interface>]
/// <summary>Interface for RandomPerson for use with C#.</summary>
type IRandomPerson =
    /// <summary>Create a Person object given a country.</summary>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <returns>A Person object with random data.</returns>
    abstract member CreatePerson: country: Country -> Person

    /// <summary>Create a Person object given a country and an options object.</summary>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A Person object with random data.</returns>
    abstract member CreatePerson: country: Country * options: RandomPersonOptions -> Person

    /// <summary>Create a list of Person objects given a country.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <returns>A list of Person object with random data.</returns>
    abstract member CreatePeople: amount: int * country: Country -> Person seq

    /// <summary>Create a list of Person objects given a country and an options object.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of Person object with random data.</returns>
    abstract member CreatePeople: amount: int * country: Country * options: RandomPersonOptions -> Person seq

    /// Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <returns>A list of strings containing random data.</returns>
    abstract member CreatePeopleTemplated: amount: int * country: Country * string -> string seq

    /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the person to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of strings containing random data.</returns>
    abstract member CreatePeopleTemplated: amount: int * country: Country * outputString: string * options: RandomPersonOptions -> string seq

[<Interface>]
/// <summary>Interface for ValidatePerson for use with C#.</summary>
type IValidatePerson =

    /// <summary>Validate a primary account number (PAN) for a credit card.</summary>
    /// <param name="pan">The PAN to validate.</param>
    /// <returns>Tuple, with first value true if valid PAN, false otherwise; second value is the error message if false.</returns>
    abstract member ValidatePAN: string -> bool * string

    /// <summary>Validate an SSN for a given country.</summary>
    /// <param name="country">The country of the person to validate SSN for.</param>
    /// <param name="ssn">The SSN to validate.</param>
    /// <returns>Tuple, with first value true if valid SSN, false otherwise; second value is the error message if false.</returns>
    abstract member ValidateSSN: Country * string -> bool * string

/// <summary>A service class for generating random persons or validating SSNs.</summary>
type RandomPerson =
    new : unit -> RandomPerson

    /// <summary>Create a Person object given a country.</summary>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <returns>A Person object with random data.</returns>
    member CreatePerson: Country -> Person

    /// <summary>Create a Person object given a country and an options object.</summary>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A Person object with random data.</returns>
    member CreatePerson: Country * RandomPersonOptions -> Person

    /// <summary>Create a list of Person objects given a country.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <returns>A list of Person object with random data.</returns>
    member CreatePeople: int * Country -> Person list

    /// <summary>Create a list of Person objects given a country and an options object.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of Person object with random data.</returns>
    member CreatePeople: int * Country * RandomPersonOptions -> Person list

    /// Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <returns>A list of strings containing random data.</returns>
    member CreatePeopleTemplated: int * Country * string -> string list

    /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the person to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of strings containing random data.</returns>
    member CreatePeopleTemplated: int * Country * string * RandomPersonOptions -> string list

    interface IRandomPerson

/// <summary>A service class for validating PANs and SSNs.</summary>
type ValidatePerson =
    new : unit -> ValidatePerson

    /// <summary>Validate a primary account number (PAN) for a credit card.</summary>
    /// <param name="pan">The PAN to validate.</param>
    /// <returns>Tuple, with first value true if valid PAN, false otherwise; second value is the error message if false.</returns>
    member ValidatePAN: string -> bool * string

    /// <summary>Validate an SSN for a given country.</summary>
    /// <param name="country">The country of the person to validate SSN for.</param>
    /// <param name="ssn">The SSN to validate.</param>
    /// <returns>Tuple, with first value true if valid SSN, false otherwise; second value is the error message if false.</returns>
    member ValidateSSN: Country * string -> bool * string

    interface IValidatePerson

/// <summary>An utility class for generating random data.</summary>
[<AbstractClass; Sealed>]
type RandomPersonUtil =

    /// <summary>Generate a random integer between two integers. Inclusive on min, exlusive on max.</summary>
    /// <param name="min">Minimum integer that can be generated.</param>
    /// <param name="max">Maximum integer that can be generated + 1.</param>
    /// <returns>An integer between min and max.</returns>
    static member RandomIntBetween: int * int -> int

    /// <summary>Generate a random integer between two integers, with steps. Inclusive on min, exlusive on max.</summary>
    /// <param name="min">Minimum integer that can be generated.</param>
    /// <param name="step">The step size</param>
    /// <param name="max">Maximum integer that can be generated + 1.</param>
    /// <returns>An integer between min and max, rounded to a step.</returns>
    static member RandomIntBetween: int * int * int -> int

    /// <summary>Generate a random float between two float. Inclusive on min, exlusive on max.</summary>
    /// <param name="min">Minimum float that can be generated.</param>
    /// <param name="max">Maximum float that can be generated + 1.</param>
    /// <returns>A float between min and max.</returns>
    static member RandomFloatBetween: float * float -> float

    /// <summary>Generate a random float between two float, with steps. Inclusive on min, exlusive on max.</summary>
    /// <param name="min">Minimum float that can be generated.</param>
    /// <param name="step">The step size</param>
    /// <param name="max">Maximum integer that can be generated + 1.</param>
    /// <returns>A float between min and max, rounded to a step.</returns>
    static member RandomFloatBetween: float * float * float -> float

    /// <summary>Generate a random integer using a normal distribution.</summary>
    /// <param name="mean">The mean of the normal distribution.</param>
    /// <param name="std">The standard deviation of the normal distribution.</param>
    /// <returns>A random integer from a normal distribution.</returns>
    static member GenerateNormallyDistributedInt: int * int -> int

    /// <summary>Generate a random float using a normal distribution.</summary>
    /// <param name="mean">The mean of the normal distribution.</param>
    /// <param name="std">The standard deviation of the normal distribution.</param>
    /// <returns>A random float from a normal distribution.</returns>
    static member GenerateNormallyDistributedFloat: float * float -> float
