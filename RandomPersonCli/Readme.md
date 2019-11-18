# Readme for RandomPersonCli

## Introduction

RandomPersonCli is a CLI tool that uses RandomPersonLib.
It generates random persons for a given country.

It can also validate SSNs and creditcard PANs.

## Usage

```
NAME

    RandomPersonCli - generate random personal information

SYNOPSIS

dotnet RandomPersonCli.dll [-m (I|L|T|VP [<PAN>]|VS [<SSN>])]
                           [-c (Denmark|Finland|France|Iceland|Netherlands|Norway|Sweden|USA)]
                           [-a (n)] [-f (CSV|JSON|XML)] [--caf (true|false)]
                           [-o (path)] [-s (path)]

DESCRIPTION

    RandomPersonCli will generate random personal information, such as name,
    address, SSN, gender, email and so on. It has various modes, can generate
    information for different countries and write to CSV, XML and JSON files,
    as well as straight to the console.

OPTIONS

-m, --mode
    Either I (interactive), L (list), T (templated list), VS (validate SSN) or
    VP (validate PAN). Validate PAN mode can take PAN as optional input,
    otherwise it's using interactive validation. Validate SSN mode can take SSN
    as optional input, otherwise it's using interactive validation.

-c, --country
    Either Denmark, Finland, France, Iceland, Netherlands, Norway, Sweden or USA.
    Used in List or Template mode.

-a, --amount
    Number of people to generate in List or Template mode.

-f, --filetype
    File format to use when printing to file in List mode.
    Will print to the console if not specified.

--caf
    Print to both console and file at the same time if true. Only used when -f
    is specified. False is default.

-o, --output
    Path to output file when printing to file in List mode.

-s, --settings
    Path to the settings file if non-default file is used.


Default:
    Interactive mode.
    If List or Template mode: 10 people, Norway as country.
    If Validation mode with SSN supplied as argument: Norway as country.

The options are case-sensitive.
```

You might want to change encoding for the console to UTF8: `chcp 65001` (for Windows).

### Examples

Open in interactive mode:

```dotnet RandomPersonCli -m I```

Generate 100 Swedish persons:

```dotnet RandomPersonCli -m L -c Sweden -a 100```

Generate 50 Danish persons, print to JSON file called test_people.json:

```dotnet RandomPersonCli -m L -c Denmark -a 50 -f JSON -o test_people.json```

Generate 200 Norwegian persons in Template mode (template string read from Settings.json):

```dotnet RandomPersonCli -m T -c Norway -a 200```

## Options in Settings.json

```json
"Options": {
    "AnonymizeSSN": false,
    "Under18": false,
    "AddCountryCodeToPhoneNumber": false,
    "RemoveHyphenFromPhoneNumber": false,
    "RemoveSpaceFromPhoneNumber": true,
    "RemoveHyphenFromSSN": false,
	"RemoveSpacesFromPAN": false,
	"UseColonsInMacAddress": false,
    "UseUppercaseInMacAddress": false,
	"Creditcard": {
        "CardIssuer": "Visa",
        "PinLength": 4
      },
    "BirthDate": {
      "BirthDateMode": "DefaultCalendarYearRange",
      "Low": 1900,
      "High": 1950,
      "ManualBirthDate": "2018-05-03""
    },
    "Randomness": {
        "ManualSeed": false,
        "Seed": 1234
    }
}
```
### Top-level settings

#### AnonymizeSSN
Is set to true if the SSN should be anonymized (non-validating checksum).
Set it to false to generate real SSNs.

#### Under18
Is set to true if people under 18 years old should be generated (as well as over 18).
Set it to false to generate over-18s only. This setting is only active if `SetYearRangeManually` is
false.

#### AddCountryCodeToPhoneNumber
Is set to true if country code should be added to the telephone numbers.

#### RemoveHyphenFromPhoneNumber
Is set to true if hyphens should be excluded from telephone numbers, for telephone numbers that usually
include this.

#### RemoveSpaceFromPhoneNumber
Is set to true if space should be excluded from telephone numbers, for telephone numbers that usually
include this.

#### RemoveHyphenFromSSN
Is set to true if hyphens should be excluded from SSNs, for SSNs that usually include this (e.g.
Swedish and Danish).

#### RemoveSpacesFromPAN
If this is set to true, the PANs that are generated will not include spaces. E.g.
XXXX YYYY ZZZZ QQQQ becomes XXXXYYYYZZZZQQQQ. If this is set to false, the PANs will contain spaces.

#### UseColonsInMacAddress
Set to true if the MAC addresses that are generated will use colons rather than hyphens.

#### UseUppercaseInMacAddress
Set to true if the MAC addresses that are generated will use uppercase letters for the A to F hexadecimal letters.

### In Creditcard

#### CreditCard.CardIssuer

`CardIssuer` decides what type of credit card PAN that will be generated. It's an enum of one of these values:

- "AmericanExpress"
- "DinersClub"
- "Discover"
- "MasterCard"
- "Visa"

It's written as a string.

#### CreditCard.PinLength
Length of the generated PIN code, as an integer.

### In BirthDate

#### BirthDateMode

`BirthDateMode` decides how the birthdate should be calculated. It can be one of these:

- "DefaultCalendarYearRange": will randomly generate a birthdate between 1920 and 2000.
- "ManualCalendarYearRange": will randomly generate a birthdate between Low and High (see below).
- "ManualAgeRange": will randomly generate the birthdate using Low and High as age of the person.
- "Manual": will use the birthdate given in ManualBirthDate (see below).

#### Low

`Low` is the lowest year that is used when generating a random birthdate and BirthDateMode is
DefaultCalendarYearRange. If BirthDateMode, then it's the lowest age of the generated person.

#### High

`High` is the highest year that is used when generating a random birthdate and BirthDateMode is
DefaultCalendarYearRange. If BirthDateMode, then it's the highest age of the generated person.

#### ManualBirthDate

`ManualBirthDate` is the birthdate of the generated person if BirthDateMode is Manual. The format
is "yyyy-MM-dd".

#### "RandomnessOptions.ManualSeed": bool

`ManualSeed`: is set to true if the random function should use a manual seed. The manual seed is
set in the `Seed` field. When using a manual seed, the generated values will be deterministic per
seed. I.e. RandomPerson will always generate the same values every time it's called.

### In Randomness:

#### ManualSeed
Is set to true if the random function should use a manual seed. The manual seed is
set in the `Seed` field. When using a manual seed, the generated values will be deterministic per
seed. I.e. RandomPerson will always generate the same values every time it's called.

#### Seed
The seed to use with the random function in .NET. Only used when `ManualSeed = true`.

## PrintOptions in Settings.json

```json
"PrintOptions": {
    "CsvDateFormat": "dd-MM-yyyy",
    "CsvGenderMale": "Man",
    "CsvGenderFemale": "Woman",
    "JsonDateTimeType": "ISO",
    "JsonPrettyPrint": true,
    "XmlPrettyPrint": false
}
```

#### CsvDateFormat
Is used to determine how dates should be printed when printing to a file in List mode,
when format is set to CSV. This mainly affects the birthdate. The format has to follow the
.NET documentation.

#### CsvGenderMale
The string that is printed for males as Gender when printing to a file in List mode,
when format is set to CSV.

#### CsvGenderFemale
The string that is printed for females as Gender when printing to a file in List mode,
when format is set to CSV.

#### JsonDateType
Is used to determine the JSON output format when printing to a file in
List mode, when format is set to JSON. This mainly affects the birthdate, as this can be
in several different formats. Legal values are: "Microsoft" and "ISO".

| Format           | Example                 |
|------------------|-------------------------|
| Microsoft format | Date(267573600000+0200) |
| ISO format       | 2018-05-01T10:02:57     |

#### JsonPrettyPrint
Is used to set whether the JSON output should be pretty printed or not
when printing to a file in List mode. Set to true for pretty print, false otherwise.

#### XmlPrettyPrint
Is used to set whether the XML output should be pretty printed or not
when printing to a file in List mode. Set to true for pretty print, false otherwise.
