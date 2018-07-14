﻿# Readme for RandomPersonCli

## Introduction

RandomPersonCli is a CLI tool that uses RandomPersonLib.
It generates random persons for a given nationality.

It can also validate SSNs.

## Usage

```
dotnet RandomPersonCli.dll [-m <I/L/T/V>] [-n <Danish/Finnish/Norwegian/Swedish>] [-a <n>] [-f <CSV/JSON/XML>]
                              [-o <path>]  [-s <path>]
```

-m: Mode. Either I (interactive), L (list), T (templated list) or V (validation).
-n: Nationality. Either Danish, Finnish, Norwegian or Swedish. Used in List or Template mode.
-a: Amount. Number of people to generate in List or Template mode.
-f: Format to print file in for use with List mode. CSV, JSON or XML.
-o: Path to output file, for use with List mode.
-s: Settings file path. Path to the settings file if non-default file is used.

Default: Interactive mode. If List or Template mode: 10 people, Norwegian nationality.

The options are case-sensitive.

### Examples

Open in interactive mode:

```dotnet RandomPersonCli -m I```

Generate 100 Swedish persons:

```dotnet RandomPersonCli -m L -n Swedish -a 100```

Generate 50 Danish persons:

```dotnet RandomPersonCli -m L -n Danish -a 50```

Generate 200 Norwegian persons in Template mode (template string read from Settings.json):

```dotnet RandomPersonCli -m T -n Norwegian -a 200```

## Options in Settings.json

```json
"Options": {
    "AnonymizeSSN": false,
    "Under18": false,
    "AddCountryCodeToPhoneNumber": false,
    "RemoveHyphenFromPhoneNumber": false,
    "RemoveSpaceFromPhoneNumber": true,
    "RemoveHyphenFromSSN": false,
    "BirthDate": {
        "SetYearManually": false,
        "SetUsingAge": false,
        "Low": 1900,
        "High": 2000
    },
	"Randomness": {
		"ManualSeed": false,
		"Seed": 1234
     }
}
```

`AnonymizeSSN` is set to true if the SSN should be anonymized (non-validating checksum).
Set it to false to generate real SSNs.

`Under18` is set to true if people under 18 years old should be generated (as well as over 18).
Set it to false to generate over-18s only. This setting is only active if `SetYearManually` is
false.

`AddCountryCodeToPhoneNumber` is set to true if country code should be added to the
telephone numbers.

`RemoveHyphenFromPhoneNumber` is set to true if hyphens should be excluded from telephone
numbers, for telephone numbers that usually include this.

`RemoveSpaceFromPhoneNumber` is set to true if space should be excluded from telephone
numbers, for telephone numbers that usually include this.

`RemoveHyphenFromSSN` is set to true if hyphens should be excluded from SSNs, for SSNs
that usually include this (e.g. Swedish and Danish).

In `BirthDate`:

`SetYearManually` is set to true if the birthdate should be between two different years than
the default values (which is between 1920 and now minus 18 years ago (or now if Under18 is true)).
The two different years are set in `Low` and `High`. This setting overrides `Under18`.

`SetUsingAge` is set to true if the `Low` and `High` values should be years of age instead of
years in general.

`Low`: Smallest year that the random person can be born in, if `SetYearManually` is set to true
and `SetUsingAge` is set to false. If `SetUsingAge` is true, this value is the lowest age of
the randomly generated person. E.g. `Low = 1900` means the random person can only be born after
1900, when `SetUsingAge = true`.

`High`: largest year that the random person can be born in, if `SetYearManually` is set to true
and `SetUsingAge` is set to false. If `SetUsingAge` is true, this value is the largest age of
the randomly generated person. E.g. `High = 2000` means the random person can only be born before
1900, when `SetUsingAge = true`.

In `Randomness`:

`ManualSeed`: is set to true if the random function should use a manual seed. The manual seed is
set in the `Seed` field. When using a manual seed, the generated values will be deterministic per
seed. I.e. RandomPerson will always generate the same values every time it's called.

`Seed`: The seed to use with the random function in .NET. Only used when `ManualSeed = true`.

## PrintOptions in Settings.json

```json
"PrintOptions": {
    "JsonPrintType": "ISO",
    "JsonPrettyPrint": true,
    "XmlPrettyPrint": false
}
```

`JsonPrintType` is used to determine the JSON output format when printing to a file in
List mode, when format is set to JSON. This mainly affects the birthdate, as this can be
in several different formats. Legal values are: "Microsoft" and "ISO".

Microsoft format: `Date(267573600000+0200)` <br />
ISO       format: `2018-05-01T10:02:57`

`JsonPrettyPrint` is used to set whether the JSON output should be pretty printed or not
when printing to a file in List mode. Set to true for pretty print, false otherwise.

`XmlPrettyPrint` is used to set whether the XML output should be pretty printed or not
when printing to a file in List mode. Set to true for pretty print, false otherwise.