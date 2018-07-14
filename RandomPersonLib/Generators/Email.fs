module internal Email

open System

let generateEmailAddress (random: Random) (emailAddresses: string[]) (firstName: string) (lastName: string) (birthDate: DateTime) = 
    let randomNumber = random.Next(emailAddresses.Length)
    let domain = emailAddresses.[randomNumber]

    let localpart = match random.Next(0, 100) with
                    | r when 0 <= r && r <= 10  -> firstName
                    | r when 10 < r && r <= 25  -> lastName
                    | r when 25 < r && r <= 30  -> firstName + "." + lastName.ToLower()
                    | r when 30 < r && r <= 35  -> firstName + birthDate.Year.ToString()
                    | r when 35 < r && r <= 40  -> lastName + birthDate.Year.ToString()
                    | r when 40 < r && r <= 45  -> firstName + lastName
                    | r when 45 < r && r <= 50  -> firstName.[0].ToString () + lastName
                    | r when 50 < r && r <= 55  -> firstName + lastName.[0].ToString ()
                    | r when 55 < r && r <= 60  -> firstName.Substring(0, 2) + lastName
                    | r when 60 < r && r <= 65  -> firstName + lastName.Substring(0, 2)
                    | r when 65 < r && r <= 70  -> firstName + birthDate.Year.ToString().Substring(2, 2)
                    | r when 70 < r && r <= 75  -> lastName + birthDate.Year.ToString().Substring(2, 2)
                    | r when 75 < r && r <= 100 -> firstName + random.Next(0, 20).ToString()
                    | _ -> invalidOp "Outside legal random range"

    let cleanLocalpart = localpart.ToLower()
                                  .Replace(" ", "")
                                  .Replace("æ", "ae")
                                  .Replace("ø", "oe")
                                  .Replace("å", "aa")
                                  .Replace("ä", "ae")
                                  .Replace("ö", "oe")
                                  .Replace("ü", "u")
                                  .Replace("ï", "i")

    cleanLocalpart + "@" + domain
