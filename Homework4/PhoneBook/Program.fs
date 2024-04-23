module Program

open PhoneBook

let printOptions () =
    System.Console.WriteLine(
        """
    Commands:
      exit: Exit
      add: Add contact
      find phone: Find phone number by name
      find name: Find name by phone number
      list: See list of all contacts
      save: Save phonebook in file
      load: Load phonebook from file
    """
    )

let rec loop phonebook =
    printf "> "
    let input = System.Console.ReadLine().Trim()

    match input with
    | "exit" -> ()
    | "add" ->
        printf "Name: "
        let name = System.Console.ReadLine().Trim()
        printf "Phone Number: "
        let phoneNumber = System.Console.ReadLine().Trim()

        loop (
            addEntry
                { Name = name
                  PhoneNumber = phoneNumber }
                phonebook
        )
    | "find phone" ->
        printf "Name: "
        let name = System.Console.ReadLine().Trim()

        match findPhoneByName name phonebook with
        | Some entry -> printf "Phone Number: %s\n" entry.PhoneNumber
        | None -> printf "Name not found.\n"

        loop phonebook
    | "find name" ->
        printf "Phone Number: "
        let phoneNumber = System.Console.ReadLine().Trim()

        match findNameByPhone phoneNumber phonebook with
        | Some entry -> printf "Name: %s\n" entry.Name
        | None -> printf "Phone number not found.\n"

        loop phonebook
    | "list" ->
        phonebook |> List.iter (fun e -> printf "%s: %s\n" e.Name e.PhoneNumber)
        loop phonebook
    | "save" ->
        printf "Filename: "
        let filename = System.Console.ReadLine().Trim()
        saveToFile filename phonebook
        loop phonebook
    | "load" ->
        printf "Filename: "
        let filename = System.Console.ReadLine().Trim()
        let newPhonebook = readFromFile filename
        loop newPhonebook
    | _ ->
        printf "Invalid command.\n"
        loop phonebook

[<EntryPoint>]
let main argv =
    printOptions ()
    let emptyPhonebook = []
    loop emptyPhonebook
    0
