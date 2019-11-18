module internal Types

    module SSNTypes =
        type SSNValidationErrorMessage =
        | InvalidShape
        | InvalidDate
        | InvalidYear
        | InvalidMonth
        | InvalidIndividualNumber
        | InvalidChecksum
        | InvalidCenturyNumber
        | InvalidAreaNumber
        | InvalidGroupNumber
        | InvalidSerialNumber
        | InvalidGenderNumber
        | InvalidDepartmentNumber
        | InvalidCommuneNumber

        type SSNValidationResult<'T> =
        | Success of 'T
        | Failure of SSNValidationErrorMessage

    module PANTypes =
        type PANValidationErrorMessage =
        | InvalidShape
        | InvalidChecksum

        type PANValidationResult<'T> =
        | Success of 'T
        | Failure of PANValidationErrorMessage
