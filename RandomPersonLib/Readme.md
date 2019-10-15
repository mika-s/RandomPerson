# Readme for RandomPersonLib

## Introduction

This is a .NET library that creates random personal data. It is originally written in F# but can also
be used with C# and other .NET languages. The library uses .NET Standard and can, therefore, be used
with .NET Framework and .NET Core.

It can create people with the following generated data:

- First name
- Last name
- Address (Address 1, 2, postal code, city and state)
- Country
- Gender
- Birthdate
- SSN
- Email
- Password
- Mobile phone
- Home phone
- Credit card details (PAN, PIN, expiry and CVV)
- Country names, codes and number
- TLD

for the following countries:

- Denmark
- Finland
- Iceland
- Netherlands
- Norway
- Sweden
- USA

The data is generated using real-life data from the mentioned countries.
The SSNs that are generated are also real, unless specified to be false.

The library can validate SSNs for the countries listed above and PANs (credit
card numbers).

## How to use RandomPersonLib

### In C#

Add RandomPersonLib.dll to the project's references. Add FSharp.Core.dll version 4.5.4 to the references
using NuGet. Make sure the *data* folder is in the same folder as the referenced RandomPersonLib.dll
when running the program. If you want IntelliSense to contain XML documentation you have to have
RandomPersonLib.XML in the same folder as well.

#### Example 1

Generate an IEnumerable of people and print to the console.

```cs
IRandomPerson randomPerson = new RandomPerson();

var people = randomPerson.CreatePeople(100, Country.Denmark);

foreach (var person in people)
    Console.WriteLine(person.FirstName);
```

#### Example 2

Create an option object and set two options to true. Generate a
Person and print the mobile phone number to the console.

```cs
IRandomPerson randomPerson = new RandomPerson();

RandomPersonOptions options = new RandomPersonOptions
{
    AddCountryCodeToPhoneNumber = true,
    RemoveSpaceFromPhoneNumber = true
};

Person person = randomPerson.CreatePerson(Country.Norway, options);

Console.WriteLine(person.MobilePhone);
```

#### Example 3

Validate an SSN for a Swedish person.

```cs
IValidatePerson validatePerson = new ValidatePerson();

bool isLegalSSN = validatePerson.ValidateSSN(Country.Sweden, "950204-12345");

Console.WriteLine(isLegalSSN);
```

### In F#

#### Example 1

Generate one Danish person and print his/hers name.

```fs
let randomPerson = RandomPerson()

let printPersonName (person: Person) = printfn "Name: %s %s" person.FirstName person.LastName

randomPerson.CreatePerson(Country.Denmark)
|> printPersonName
```

#### Example 2

Generate a list of 15 Swedish people.

```fs
let randomPerson = RandomPerson()

let amount = 15
let country = Country.Sweden

let people = randomPerson.CreatePeople(amount, country)
```

## API

### CreatePerson ()

*CreatePerson (country: Country) -> Person*

or

*CreatePerson (country: Country, options: RandomPersonOptions) -> Person*

Creates a random person given a country. The return value is a Person object
with the random data. Country is an enum. options is an optional object with
options. Default settings are used if options is not provided.

### CreatePeople ()

*CreatePeople (amount: int, country: Country) -> Person list*

or

*CreatePeople (amount: int, country: Country, options: RandomPersonOptions) -> Person list*

Creates a list of random people given a country. The return value is a Person object
with the random data. Country is an enum. options is an optional object with
options. Default settings are used if options is not provided.

### CreatePeopleTemplated () =

*CreatePeopleTemplated (amount: int, country: Country, outputString: string) -> string list*

or

*CreatePeopleTemplated (amount: int, country: Country, outputString: string, options: RandomPersonOptions) -> string list*

Creates a list of strings. Country is an enum. options is an optional object with
options. Default settings are used if options is not provided.

#### Ordinary template variables

The string will follow the format of *outputString* and replace the following
variables with generated values:

`#{SSN}` <br />
`#{Email}` <br />
`#{Password}` <br />
`#{FirstName}` <br />
`#{LastName}` <br />
`#{Address1}` <br />
`#{Address2}` <br />
`#{PostalCode}` <br />
`#{City}` <br />
`#{Country}` <br />
`#{BirthDate}` <br />
`#{Gender}` <br />
`#{MobilePhone}` <br />
`#{HomePhone}` <br />
`#{PIN}` <br />
`#{PAN}` <br />
`#{Expiry}` <br />
`#{CVV}` <br />
`#{CountryNameEnglish}` <br />
`#{CountryNameNative}` <br />
`#{CountryNameNativeAlternative1}` <br />
`#{CountryNameNativeAlternative2}` <br />
`#{CountryCode2}` <br />
`#{CountryCode3}` <br />
`#{CountryNumber}` <br />
`#{TLD}`

PIN, PAN, Expiry and CVV are related to creditcards. PAN is the "creditcard number".

##### Special rules for Gender

You can change the gender values if you don't want them to be "Male" or "Female":

`#{Gender('Mann', 'Kvinne')}`

The male string is the first argument to `Gender` and the female string is the second argument.

##### Special rules for Birthdate

You can also change the birthdate values to a given date format:

`#{BirthDate('ddMMyy')}` <br />
`#{BirthDate('MMMM dd', 'da-DK')}`

The datetime formats are the same as in the official Microsoft [documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings).
The culture info can be passed as an optional second parameter. The current culture is
used if a culture is not provided. The culture info has to be in exactly the same format
as in the documentation linked to above.

#### Special template functions and variables

##### Formatting

The following methods can be chained to the variable replacements:

`ToLower()`: All lowercaps. <br />
`ToUpper()`: All uppercaps. <br />
`FirstToUpperRestLower()`: First letter lowercase, rest lowercase. <br />
`Capitalize()`: First letter uppercase, rest unchanged. <br />
`Uncapitalize()`: First letter lowercase, rest unchanged.

##### GUID

`#{GUID()}` <br />
`#{GUID(format)}` <br />

This will create a [GUID](https://en.wikipedia.org/wiki/Universally_unique_identifier).
`format` is an optional argument to format the GUID. Please see the offical [documentation](https://docs.microsoft.com/en-us/dotnet/api/system.guid.tostring).

##### Date

`#{Date('now')}` <br />
`#{Date('now', dateformat)}` <br />
`#{Date('now', dateformat, culture)}` <br />
`#{Date(X)}` <br />
`#{Date(X, dateformat)}` <br />
`#{Date(X, dateformat, culture)}` <br />

Inserts the current date when using 'now'. A number, X, can also be provided to add or substract
number of days from the current date. dateformat and culture is like for BirthDate and has
to follow the official Microsoft documentation.

##### Random integers, floats and element in list

`#{Random(int, min, max)}` <br />
`#{Random(int, min, step, max)}` <br />
`#{Random(float, min, max)}` <br />
`#{Random(float, min, step, max)}` <br />
`#{Random(float:X, min, max)}` <br />
`#{Random(float:X, min, step, max)}` <br />
`#{Random(switch, 'one', 'two', ...)}` <br />

For `int` and `float`:
A random number is generated with the arguments as settings. Type is either int
or float. `min` is the minimum random value and `max` is the maximum random value.
Both lower and upper bounds are inclusive.

The third parameter is step size if four parameters are given. E.g.
`#{Random(int, 0, 2000, 10000)}` can generate 0, 2000, 4000, 6000, 8000 or 10000.

The number after `:` defines the amount of numbers after the decimal. Default is 3 numbers. This is optional.

For `switch`:
One of the values in the list, after "switch", are chosen randomly. Use single quotes
around the arguments that should be picked randomly.


##### Random integers and floats, normal distributed

`#{Random(nd_int, mean, std)}` <br />
`#{Random(nd_int, mean, std, rounding)}` <br />
`#{Random(nd_float, mean, std)}` <br />
`#{Random(nd_float, mean, std, rounding)}` <br />
`#{Random(nd_float:X, mean, std)}` <br />
`#{Random(nd_float:X, mean, std, rounding)}` <br />

A random number, either int or float, is generated using [normal distribution](https://en.wikipedia.org/wiki/Normal_distribution).
The mean and standard deviation have to be provided as arguments. The rounding is an optional argument that
can be used to round the generated number.

The number after `:` defines the amount of numbers after the decimal. Default is 3 numbers. This is optional.

#### Examples

*Example:*

`outputString = "First name: #{FirstName}\nLast name: #{LastName}"`

could generate

`"First name: Test\nLast name: Person"`

*Example:*

`outputString = "Address: #{Address1.ToUpper()}"`

could generate

`"Address: TEST STREET 213"`

*Example:*

`outputString = "Ten days ago: #{Date(-10, 'dd-MM-yyyy')}"`

could generate

`"Ten days ago: 03-09-2018"`

*Example for random int:*

`#{Random(int, 0, 2)}`

*Example for random float:*

`#{Random(float, 18.0, 60.0)}` or `#{Random(float:2, 18.0, 60.0)}`

*Example for switch:*

`#{Random(switch, 'true', 'false')}`

There is a 50/50 chance of generating either true or false.

### ValidateSSN ()

*ValidateSSN (country: Country, ssn: string) -> bool * string*

Validates an SSN given a country and an SSN. The return value is a tuple with
the bool representing whether the SSN is valid or not, and the string is the
error message if the SSN is invalid. Given a valid SSN the string will be empty.

### ValidatePAN ()

*ValidatePAN (pan: string) -> bool * string*

Validates an PAN. The return value is a tuple with the bool representing whether
the PAN is valid or not, and the string is the error message if the PAN is invalid.
Given a valid PAN the string will be empty.

### Options

All default values are false if the options object is not provided.

#### "AnonymizeSSN": bool

If this is set to true, the SSNs that are generated will be fake.
If this is set to false, the SSNs that are generated will be real and the checksum correct.

#### "Under18": bool

If this is set to true, the birthdate (and therefore SSN) that are generated can include
people under 18. If this is set to false, the person that is generated will always be
older than 18 years.

#### "AddCountryCodeToPhoneNumber": bool

If this is set to true, the phone numbers generated will include the country code. E.g.
+4790000000. If this is set to false, the phone number will not include country code. E.g. 90000000.

#### "RemoveHyphenFromPhoneNumber": bool

If this is set to true, the phone numbers generated will not include hyphens (for phone
numbers that usually include them). E.g. 555-1234 becomes 5551234. If this is set to false,
the phone numbers can contain hyphens.

#### "RemoveSpaceFromPhoneNumber": bool

If this is set to true, the phone numbers generated will not include space (for phone
numbers that usually include them). E.g. 12 34 56 78 becomes 12345678. If this is set to false,
the phone numbers can contain space.

#### "RemoveHyphenFromSSN": bool

If this is et to true, the SSNs that are generated will not include hyphens (for SSNs that
usually include them). E.g. XXXXXX-YYYY becomes XXXXXXYYYY. If this is set to false, the
SSNs can contain hyphens.

#### "RemoveSpacesFromPAN": bool

If this is et to true, the PANs that are generated will not include spaces. E.g.
XXXX YYYY ZZZZ QQQQ becomes XXXXYYYYZZZZQQQQ. If this is set to false, the PANs will contain
spaces.

#### "BirthDateOptions.BirthDateMode": enum

`BirthDateMode` decides how the birthdate should be calculated. It can be one of these:

- DefaultCalendarYearRange: will randomly generate a birthdate between 1920 and 2000.
- ManualCalendarYearRange: will randomly generate a birthdate between Low and High (see below).
- ManualAgeRange: will randomly generate the birthdate using Low and High as age of the person.
- Manual: will use the birthdate given in ManualBirthDate (see below).

#### "BirthDateOptions.Low": int

`Low` is the lowest year that is used when generating a random birthdate and BirthDateMode is
DefaultCalendarYearRange. If BirthDateMode, then it's the lowest age of the generated person.

#### "BirthDateOptions.High": int

`High` is the highest year that is used when generating a random birthdate and BirthDateMode is
DefaultCalendarYearRange. If BirthDateMode, then it's the highest age of the generated person.

#### "BirthDateOptions.ManualBirthDate": DateTime

`ManualBirthDate` is the birthdate of the generated person if BirthDateMode is Manual.

#### "RandomnessOptions.ManualSeed": bool

`ManualSeed`: is set to true if the random function should use a manual seed. The manual seed is
set in the `Seed` field. When using a manual seed, the generated values will be deterministic per
seed. I.e. RandomPerson will always generate the same values every time it's called.

#### "RandomnessOptions.Seed": int

The seed to use with the random function in .NET. Only used when `ManualSeed = true`.
