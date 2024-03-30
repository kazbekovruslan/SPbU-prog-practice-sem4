module Homework3.Tests

open NUnit.Framework
open FsUnit
open Homework3.LambdaInterpreter

let testCases () =
    []
    |> List.map (fun (expression, expected) -> TestCaseData(expression, expected))
