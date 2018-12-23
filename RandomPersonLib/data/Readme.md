# Sources

The following sources are used for the data that is used to generate random persons.

## Generic

- [Password list](https://github.com/danielmiessler/SecLists/blob/master/Passwords/darkweb2017-top1000.txt)
- [National conventions for writing telephone numbers](https://en.wikipedia.org/wiki/National_conventions_for_writing_telephone_numbers)
- [Country codes and numbers in ISO-3166-1](https://en.wikipedia.org/wiki/ISO_3166-1)

## Denmark

- [Danish firstnames](https://www.dst.dk/da/Statistik/emner/befolkning-og-valg/navne/navne-til-nyfoedte)
- [Danish lastnames](https://ast.dk/born-familie/navne/navnelister/frie-efternavne)
- [Danish SSN rules](https://www.cpr.dk/media/17534/personnummeret-i-cpr.pdf)
- [Postal codes and cities in Denmark](https://www.postnord.dk/kundeservice/postnummerkort)
- [Telephone numbers in Denmark](https://en.wikipedia.org/wiki/Telephone_numbers_in_Denmark)

## Finland

- [Finnish firstnames](https://www.avoindata.fi/data/en_GB/dataset/none)
- [Finnish lastnames](https://www.avoindata.fi/data/en_GB/dataset/none)
- [Finnish SSN rules](https://en.wikipedia.org/wiki/National_identification_number#Finland)
- [Postal codes and cities in Finland](https://www.posti.fi/business/help-and-support/postal-code-services/postal-code-files.html) ([also this](http://www.posti.fi/webpcode/))
- [Telephone numbers in Finland](https://en.wikipedia.org/wiki/Telephone_numbers_in_Finland)

## Norway

- [Norwegian names](https://www.ssb.no/navn)
- [Syntax for SSNs](https://ehelse.no/standarder-kodeverk-og-referansekatalog/standarder-og-referansekatalog/identifikatorer-for-personer-syntaks-for-fodselsnummer-hjelpenummer-mv-his-10012010)
- [Streets in Oslo](http://www.norskegater.com/Oslo/)
- [Postal codes and cities in Norway](https://data.norge.no/data/posten-norge/postnummer-i-norge)
- [Telephone numbers in Norway](https://en.wikipedia.org/wiki/Telephone_numbers_in_Norway)

## Sweden

- [Swedish names](http://www.scb.se/hitta-statistik/statistik-efter-amne/befolkning/amnesovergripande-statistik/namnstatistik/)
- [Streets in Stockholm](https://www.svenskaplatser.se/Stockholm/)
- [Postal codes and cities in Sweden](http://download.geonames.org/export/zip/SE.zip)
- [Telephone numbers in Sweden](https://en.wikipedia.org/wiki/Telephone_numbers_in_Sweden)

The list of Swedish postal codes and cities is under the [Creative Commons Attribution 3.0 License](http://creativecommons.org/licenses/by/3.0/).

www.geonames.org

## The Netherlands

- [Dutch firstnames](https://www.meertens.knaw.nl/nvb/topnamen/land/Nederland/2014)
- [Dutch lastnames](https://github.com/digitalheir/family-names-in-the-netherlands)
- [Telephone numbers in The Netherlands](https://en.wikipedia.org/wiki/Telephone_numbers_in_the_Netherlands)
- [Postal codes in The Netherlands](https://en.wikipedia.org/wiki/Postal_codes_in_the_Netherlands)
- [List of postal codes in The Netherlands](http://download.geonames.org/export/zip/NL.zip)
- [List of addresses in The Netherlands](http://download.geonames.org/export/dump/NL.zip)

The list of Dutch postal codes, cities and addresses is under the [Creative Commons Attribution 3.0 License](http://creativecommons.org/licenses/by/3.0/).

www.geonames.org

## Iceland

- [Icelandic SSN rules](https://www.skra.is/english/individuals/me-and-my-family/my-registration/id-numbers/)
- [Icelandic names](https://www.island.is/mannanofn/leit-ad-nafni/)
- [Icelandic patronyms](https://en.ja.is/)
- [Streets in Iceland](https://www.postur.is/en/about-us/post-offices/post-codes/data-files/)
- [Postal codes and cities in Iceland](https://www.postur.is/en/about-us/post-offices/post-codes/data-files/)

Regex for extracting patronyms from yellow pages: `^.*?(\w+dóttir).*$` and  `^.*?(\w+son).*$`<br />
Replace with (with Notepad++): `\1`

# Misc

## Tools

Various tools that have been used to clean the data:

- [Convert to proper case](https://convertcase.net/)
- [Remove duplicate lines](https://textmechanic.com/text-tools/basic-text-tools/remove-duplicate-lines/)

## Other things

Save all data files as UTF8 without BOM. Notepad++ can be used to convert to this encoding.
