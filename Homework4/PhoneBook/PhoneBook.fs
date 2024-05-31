module PhoneBook

type PhoneEntry = { Name: string; PhoneNumber: string }

let addEntry (entry: PhoneEntry) (phonebook: PhoneEntry list) = entry :: phonebook

let findPhoneByName (name: string) (phonebook: PhoneEntry list) =
    phonebook |> List.filter (fun e -> e.Name = name)

let findNameByPhone (phoneNumber: string) (phonebook: PhoneEntry list) =
    phonebook |> List.filter (fun e -> e.PhoneNumber = phoneNumber)

let saveToFile (filename: string) (phonebook: PhoneEntry list) =
    let data = phonebook |> List.map (fun e -> sprintf "%s,%s\n" e.Name e.PhoneNumber)
    System.IO.File.WriteAllText(filename, String.concat "" data)

let readFromFile (filename: string) =
    try
        System.IO.File.ReadAllLines(filename)

        |> Array.map (fun line ->
            let parts = line.Split(',')

            { Name = parts.[0]
              PhoneNumber = parts.[1] })
        |> List.ofArray
    with :? System.IO.FileNotFoundException ->
        []
