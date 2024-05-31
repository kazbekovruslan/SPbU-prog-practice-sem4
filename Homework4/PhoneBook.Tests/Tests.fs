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
let ``add entry with existing name to phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let newEntry = { Name = "Jane"; PhoneNumber = "9999" }

    let actual = addEntry newEntry phonebook

    actual
    |> should
        equal
        [ { Name = "Jane"; PhoneNumber = "9999" }
          { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

[<Test>]
let ``find phone by name in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findPhoneByName "John" phonebook

    actual |> should equal ([ { Name = "John"; PhoneNumber = "1234" } ])

[<Test>]
let ``find phone by existing name in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findPhoneByName "John" phonebook

    actual |> should equal ([ { Name = "John"; PhoneNumber = "1234" } ])

[<Test>]
let ``find phone by non existing name in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findPhoneByName "Jack" phonebook

    Assert.That(actual, Is.Empty)

[<Test>]
let ``find name by phone in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findNameByPhone "5678" phonebook

    actual |> should equal ([ { Name = "Jane"; PhoneNumber = "5678" } ])

[<Test>]
let ``find name by existing phone in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findNameByPhone "5678" phonebook

    actual |> should equal ([ { Name = "Jane"; PhoneNumber = "5678" } ])

[<Test>]
let ``find name by non existing phone in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "5678" } ]

    let actual = findNameByPhone "9999" phonebook

    Assert.That(actual, Is.Empty)

[<Test>]
let ``find name by existing duplicate phone in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "Jane"; PhoneNumber = "1234" } ]

    let actual = findNameByPhone "1234" phonebook

    actual
    |> should
        equal
        ([ { Name = "John"; PhoneNumber = "1234" }
           { Name = "Jane"; PhoneNumber = "1234" } ])

[<Test>]
let ``find phone by existing duplicate name in phonebook`` () =
    let phonebook =
        [ { Name = "John"; PhoneNumber = "1234" }
          { Name = "John"; PhoneNumber = "5678" } ]

    let actual = findPhoneByName "John" phonebook

    actual
    |> should
        equal
        ([ { Name = "John"; PhoneNumber = "1234" }
           { Name = "John"; PhoneNumber = "5678" } ])

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
