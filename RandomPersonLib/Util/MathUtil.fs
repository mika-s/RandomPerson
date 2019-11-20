module internal MathUtil

open System

let modulo (x: int) (y: int) = y % x
let isOdd  (x: int) = x % 2 <> 0
let isEven (x: int) = x % 2 =  0
let roundToNearest (rounding: float) (x: float) = Math.Round(x / rounding) * rounding
