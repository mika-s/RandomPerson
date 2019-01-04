module internal CliEnums

type Mode =
| Interactive = 0
| List        = 1
| Template    = 2
| ValidateSSN = 3
| ValidatePAN = 4

type OutputType =
| Console        = 0
| File           = 1
| ConsoleAndFile = 2

type FileFormat =
| CSV  = 0
| JSON = 1
| XML  = 2
