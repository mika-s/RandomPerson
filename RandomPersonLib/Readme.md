# Readme for RandomPersonLib

## Introduction

This is a .NET library that creates random personal data. It is originally
written in F# but can also be used with C# and other .NET languages. The
library uses .NET Standard and can therefore be used with .NET Framework
and .NET Core.

It can create people with the following generated data:

- First name
- Last name
- Address (Address 1, 2, postal code and city)
- Nationality
- Gender
- Birth date
- SSN
- Email
- Password
- Mobile phone
- Home phone

for the following countries:

- Denmark
- Finland
- Iceland
- Norway
- Sweden

The data is generated using real-life data from the mentioned countries.
The SSNs that are generated are also real, unless specified to be false.

The library can also validate SSNs for the countries listed above.

## How to use RandomPersonLib

### In C#

Add RandomPersonLib.dll to the project's references. Add FSharp.Core.dll version 4.5.0 to the references
using NuGet.

#### Example 1

Generate an IEnumerable of people and print to the console.

```cs
IRandomPerson randomPerson = new RandomPerson();

var people = randomPerson.CreatePersonList(100, Nationality.Danish);

foreach (var person in people)
    Console.WriteLine(person.FirstName);
```

#### Example 2

Create an option object and set a few of them to true. Generate one
Person and print the mobile phone number to the console.

```cs
IRandomPerson randomPerson = new RandomPerson();

RandomPersonOptions options = new RandomPersonOptions
{
    AddCountryCodeToPhoneNumber = true,
    RemoveSpaceFromPhoneNumber = true
};

Person person = randomPerson.CreatePerson(Nationality.Norwegian, options);

Console.WriteLine(person.MobilePhone);
```

#### Example 3

Validate an SSN for a Swedish person.

```cs
IRandomPerson randomPerson = new RandomPerson();

bool isLegalSSN = randomPerson.ValidateSSN(Nationality.Swedish, "950204-12345");

Console.WriteLine(isLegalSSN);
```

### In F#

#### Example 1

Generate one Danish person and print his/hers name.

```fs
let randomPerson = RandomPerson()

let printPersonsName (person: Person) = printfn "Name: %s %s" person.FirstName personLastName

randomPerson.CreatePerson(Nationality.Danish)
|> printPersonsName
```

#### Example 2

Generate a list of 15 Swedish people.

```fs
let randomPerson = RandomPerson()

let amount = 15
let nationality = Nationality.Swedish

let people = randomPerson.CreatePersonList(amount, nationality)
```

## API

### CreatePerson ()

*CreatePerson (nationality: Nationality) -> Person*

or

*CreatePerson (nationality: Nationality, options: RandomPersonOptions) -> Person*

Creates a random person given a nationality. The return value is a Person object
with the random data. Nationality is an enum. options is an optional object with
options. Default settings are used if options is not provided.

### CreatePersonList ()

*CreatePersonList (amount: int, nationality: Nationality) -> Person list*

or

*CreatePersonList (amount: int, nationality: Nationality, options: RandomPersonOptions) -> Person list*

Creates a list of random people given a nationality. The return value is a Person object
with the random data. Nationality is an enum. options is an optional object with
options. Default settings are used if options is not provided.

### CreatePersonTemplatedList () =

*CreatePersonTemplatedList (amount: int, nationality: Nationality, outputString: string) -> string list*

or

*CreatePersonTemplatedList (amount: int, nationality: Nationality, outputString: string, options: RandomPersonOptions) -> string list*

Creates a list of strings. Nationality is an enum. options is an optional object with
options. Default settings are used if options is not provided. The string will follow
the format of *outputString* and replace the following variables with generated values:

- `#{SSN}`
- `#{Email}`
- `#{Password}`
- `#{FirstName}`
- `#{LastName}`
- `#{Address1}`
- `#{Address2}`
- `#{PostalCode}`
- `#{City}`
- `#{Nationality}`
- `#{BirthDate}`
- `#{Gender}`
- `#{MobilePhone}`
- `#{HomePhone}`

The following methods can be chained to the variable replacements:

- `ToLower()`: All lowercaps.
- `ToUpper()`: All uppercaps.

You can change the gender values if you don't want them to be "Male" or "Female":

- `#{Gender('Mann', 'Kvinne')}`

The male string is the first argument to `Gender` and the female string is the second argument.

You can also change the birthdate values to a given date format:

- `#{BirthDate('ddMMyy')}`
- `#{BirthDate('MMMM dd', 'da-DK')}`

The datetime formats are the same as in the official Microsoft [documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings).
The culture info can be passed as an optional second parameter. The current culture is
used if a culture is not provided. The culture info has to be in exactly the same format
as in the documentation linked to above.

*Example 1:*

`outputString = "First name: #{FirstName}\nLast name: #{LastName}"`

could generate

`"First name: Test\nLast name: Person"`

*Example 2:*

`outputString = "Address: #{Address1.ToUpper()}"`

could generate

`"Address: TEST STREET 213"`

The following functions are also available:

- `#{Random(int, min, max)}`
- `#{Random(int, min, step, max)}`
- `#{Random(float, min, max)}`
- `#{Random(float, min, step, max)}`
- `#{Random(float:X, min, max)}`
- `#{Random(float:X, min, step, max)}`
- `#{Random(switch, one, two, ...)}`

For int and float:
A random number is generated with the arguments as settings. type is either int
or float. min is the minimum random value and max is the maximum random value.
Both lower and upper bounds are inclusive.

The third parameter is step size if four parameters are given. E.g.
`#{Random(int, 0, 2000, 10000)}` can generate 0, 2000, 4000, 6000, 8000 or 10000.

For switch:
One of the values in the list, after "switch", are chosen randomly. Use `\`, rather
than just a `,`, if the comma should be a part of the chosen value.

Example for int:

`#{Random(int, 0, 2)}`

Example for float:

`#{Random(float, 18.0, 60.0)}` or `#{Random(float:2, 18.0, 60.0)}`

The number after `:` defines the amount of numbers after the decimal. Default is 3 numbers.

Example for switch:

`#{Random(switch, 'true', 'false')}`

There is a 50/50 chance of generating either true or false.

### ValidateSSN ()

*ValidateSSN (nationality: Nationality, ssn: string) -> bool*

Validates an SSN given a nationality and an SSN.

### Options

All default values are false if the options object is not provided.

#### "AnonymizeSSN": boolean

If this is set to true, the SSNs that are generated will be fake.
If this is set to false, the SSNs that are generated will be real and the checksum correct.

#### "Under18": boolean

If this is set to true, the birthdate (and therefore SSN) that are generated can include
people under 18. If this is set to false, the person that is generated will always be
older than 18 years.

#### "AddCountryCodeToPhoneNumber": boolean

If this is set to true, the phone numbers generated will include the country code. E.g.
+4790000000. If this is set to false, the phone number will not include country code. E.g. 90000000.

#### "RemoveHyphenFromPhoneNumber": boolean

If this is set to true, the phone numbers generated will not include hyphens (for phone
numbers that usually include them). E.g. 555-1234 becomes 5551234. If this is set to false,
the phone numbers can contain hyphens.

#### "RemoveSpaceFromPhoneNumber": boolean

If this is set to true, the phone numbers generated will not include space (for phone
numbers that usually include them). E.g. 12 34 56 78 becomes 12345678. If this is set to false,
the phone numbers can contain space.

#### "RemoveHyphenFromSSN": boolean

If this is et to true, the SSNs that are generated will not include hyphens (for SSNs that
usually include them). E.g. XXXXXX-YYYY becomes XXXXXXYYYY. If this is set to false, the
SSNs can contain hyphens.

#### "BirthDateOptions.SetYearManually": boolean

`SetYearManually` is set to true if the birthdate should be between two different years than
the default values (which is between 1920 and now minus 18 years ago (or now if Under18 is true)).
The two different years are set in `Low` and `High`. This setting overrides `Under18`.

#### "BirthDateOptions.SetUsingAge": boolean

`SetUsingAge` is set to true if the `Low` and `High` values should be years of age instead of
years in general.

#### "BirthDateOptions.Low": int

`Low`: Smallest year that the random person can be born in, if `SetYearManually` is set to true
and `SetUsingAge` is set to false. If `SetUsingAge` is true, this value is the lowest age of
the randomly generated person. E.g. `Low = 1900` means the random person can only be born after
1900, when `SetUsingAge = true`.

#### "BirthDateOptions.High": int

`High`: largest year that the random person can be born in, if `SetYearManually` is set to true
and `SetUsingAge` is set to false. If `SetUsingAge` is true, this value is the largest age of
the randomly generated person. E.g. `High = 2000` means the random person can only be born before
1900, when `SetUsingAge = true`.

#### "RandomnessOptions.ManualSeed": boolean

`ManualSeed`: is set to true if the random function should use a manual seed. The manual seed is
set in the `Seed` field. When using a manual seed, the generated values will be deterministic per
seed. I.e. RandomPerson will always generate the same values every time it's called.

#### "RandomnessOptions.Seed": int

The seed to use with the random function in .NET. Only used when `ManualSeed = true`.
