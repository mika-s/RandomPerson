module internal CliEnums

type Mode =
| Interactive = 0
| List        = 1
| Template    = 2
| Validation  = 3

type OutputType =
| Console = 0
| File    = 1

type FileFormat =
| CSV  = 0
| JSON = 1
| XML  = 2
