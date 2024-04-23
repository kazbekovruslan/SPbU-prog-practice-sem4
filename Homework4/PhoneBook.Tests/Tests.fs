module PhoneBook.Tests

open NUnit.Framework
open FsUnit
open PhoneBook

[<Test>]
let ``add entry to phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let newEntry = { Name = "Jack"; PhoneNumber = "9012" }

    let actual = addEntry newEntry phonebook

    actual
    |> should
        equal
        [ { Name = "Jack"; PhoneNumber = "9012" }
          { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

[<Test>]
let ``find phone by name in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findPhoneByName "John" phonebook

    actual |> should equal (Some { Name = "John"; PhoneNumber = "1234" })

[<Test>]
let ``find name by phone in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findNameByPhone "5678" phonebook

    actual |> should equal (Some { Name = "Jane"; PhoneNumber = "5678" })

[<Test>]
let ``save phonebook to file`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let filename = "test_phonebook.txt"

    saveToFile filename phonebook

    let actual = System.IO.File.ReadAllText(filename)

    actual |> should equal "John,1234\nJane,5678\n"

[<Test>]
let ``read phonebook from file`` () =
    let expected =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let filename = "test_phonebook.txt"

    System.IO.File.WriteAllText(filename, "John,1234\nJane,5678\n")

    let actual = readFromFile filename

    actual |> should equal expected
