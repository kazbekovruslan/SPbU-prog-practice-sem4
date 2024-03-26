module Homework2.ParseTree.Test

open NUnit.Framework
open FsUnit
open Homework2.ParseTree

let testCases () =
    [ Operation(Operation(Operand(5), Plus, Operand(4)), Minus, Operation(Operand(-1), Plus, Operand(-2))), 12
      Operand(1), 1
      Operand(-1), -1 ]
    |> List.map (fun (expression, expected) -> TestCaseData(expression, expected))


[<TestCaseSource(nameof (testCases))>]
let ParseTreeTest expression expected =
    calculate expression |> should equal expected
