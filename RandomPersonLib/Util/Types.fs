module internal Types

    module SSNTypes =
        type SSNValidationErrorMessage =
        | WrongShape
        | WrongDate
        | WrongIndividualNumber
        | WrongChecksum
        | WrongCenturyNumber
        | WrongAreaNumber
        | WrongGroupNumber
        | WrongSerialNumber

        type SSNValidationResult<'T> =
        | Success of 'T
        | Failure of SSNValidationErrorMessage

    module PANTypes =
        type PANValidationErrorMessage =
        | WrongShape
        | WrongChecksum

        type PANValidationResult<'T> =
        | Success of 'T
        | Failure of PANValidationErrorMessage
