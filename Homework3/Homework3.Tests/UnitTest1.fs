module Homework3.Tests

open NUnit.Framework
open FsUnit
open Homework3.LambdaInterpreter

let testCases () =
    [ Var "x", Var "x"
      Application((LambdaAbstraction("x", Var "x"), Var "z")), Var "z"
      LambdaAbstraction("x", Var "a"), LambdaAbstraction("x", Var "a")
      Application(
          LambdaAbstraction("x", Var "y"),
          Application(
              LambdaAbstraction("x", LambdaAbstraction("x", Var "x")),
              LambdaAbstraction("x", Application(Var "x", Var "x"))
          )
      ),
      Var "y" ]
    |> List.map (fun (expression, result) -> TestCaseData(expression, result))


[<TestCaseSource(nameof (testCases))>]
let reduceTests expression expected =
    reduce expression |> should equal expected
