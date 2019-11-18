# RandomPerson

## Introduction

RandomPerson is a .NET library and CLI tool that creates random personal data. It is originally
written in F# but can also be used with C# and other .NET languages. The library uses .NET Standard and can
therefore be used with .NET Framework and .NET Core. The CLI tool is made in .NET core and can be used on
several platforms.

It supports creating personal data for the following countries:

- Denmark
- Finland
- France
- Iceland
- Netherlands
- Norway
- Sweden
- USA

Check the RandomPersonLib folder for more information regarding the library. <br />
Check the RandomPersonCli folder for more information regarding the CLI tool.

## Features

### Generate person objects containing the following information:

* First name
* Last name
* Address (Address 1, 2, postal code and city)
* Nationality
* Gender
* Birth date
* SSN
* Email
* Password
* MAC address
* Mobile phone
* Home phone
* Credit card details (PAN, PIN, expiry and CVV)
* Country names, codes, number.
* TLD

The data is based on real values from the mentioned countries. I.e. the SSN, telephone numbers, etc.
will be valid. SSN can be chosen to be fake. Birth date can be completely random, within an age range,
within a calendar year range or fixed.

![List mode for Norwegian](./Assets/Images/list%20mode%20-%20usa.png)

### Output to file

In addition to outputting to the console, RandomPersonCli can output the data to file in JSON, XML and CSV format.

![List mode to file for Swedish](./Assets/Images/list%20mode%20to%20JSON%20-%20swedish.png)

### Generate templated strings

A string with special template values can be supplied to RandomPerson. The special template values will be replaced
with the randomly generated values.

This can, for example, be used for creating SQL insert statements that create test data for use in development
or testing:

`"INSERT INTO Customers (FirstName, LastName, Address1, Address2, PostalCode, City, Phone, Level) VALUES
('#{FirstName}', '#{LastName}', '#{Address1}', '#{Address2}', '#{PostalCode}', '#{City}', '#{MobilePhone}', #{Random(int, 3, 10)})"`

will generate

`"INSERT INTO Customers (FirstName, LastName, Address1, Address2, PostalCode, City, Phone, Level) VALUES
('Lars', 'Olsen', 'Dybviksgata 13', '', '1234', 'Gokk', '12345678', 7)`

RandomPerson can also generate random integers and float between two given numbers, as well as random strings from a
list. It can also generate normal distributed random numbers, given mean and standard deviation.

### Options

RandomPerson has options for:

- Deterministic randomization can be enabled if wanted, meaning you can get the same set of people for every
  generated list of people.
- Setting a range for birth dates in either absolute values (birth year) or relative values (age). It can also be fixed.
  This will also affect the generated SSN.
- Anonymization of SSNs.
- Output format for phone numbers (e.g. with country code or not), SSNs and PANs.

### Validate PANs and SSNs

RandomPerson can validate PAN for credit cards and SSNs for the countries listed above.
