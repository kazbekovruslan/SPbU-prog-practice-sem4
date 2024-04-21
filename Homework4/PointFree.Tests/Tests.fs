module PointFree.Tests

open PointFreeMultiplication
open NUnit.Framework
open FsCheck

[<Test>]
let MultiplicationTest =
    let check x list =
        List.map (fun y -> y * x) list = multiplyBy x list

    Check.Quick check
