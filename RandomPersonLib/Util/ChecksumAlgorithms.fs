module internal ChecksumAlgorithms

open Util

let luhn (numbersStr: string) (weights: int array) =
    let sumTheDigits (product: int) =
        let productAsString = sprintf "%d" product

        intFromChar productAsString.[0] + intFromChar productAsString.[1]

    let luhnMap (w: int) (n: int) =
        let product = n * w

        match product with
        | p when p >= 10 -> sumTheDigits product
        | _              -> product

    let numbers = intArrayFromString numbersStr
    let sum = (weights, numbers) ||> Array.map2 luhnMap |> Array.sum |> sprintf "%d"
    let tenMinusLastDigit = 10 - intFromChar sum.[sum.Length - 1] |> sprintf "%d"

    string(tenMinusLastDigit.[tenMinusLastDigit.Length - 1])
