module BracketSequence.Tests

open BracketSequence
open NUnit.Framework
open FsUnit

let testCases () =
    [ "", true
      "(){}[]", true
      "([{}])", true
      "((])", false
      "({[]", false
      "[]})", false
      "[a]", true
      "a(b[c]d)e", true
      "f(g{h]i)j", false
      "!@#$%^&*(){}[]", true
      "abc(def[ghi", false
      "jkl)mno}pqr]", false
      "(", false
      ")", false ]
    |> List.map (fun (input, expected) -> TestCaseData(input, expected))

[<TestCaseSource(nameof (testCases))>]
let bracketSequenceTests input expected =
    isBracketSequenceCorrect input |> should equal expected
