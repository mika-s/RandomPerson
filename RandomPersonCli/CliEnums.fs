module internal CliEnums

type Mode =
| Interactive
| List
| Template
| ValidateSSN
| ValidatePAN

type OutputType =
| Console
| File
| ConsoleAndFile

type FileFormat =
| CSV
| JSON
| XML

type BirthDateMode =
| DefaultCalendarYearRange
| ManualCalendarYearRange
| ManualAgeRange
| Manual
