module Homework2.BinaryTreeMap.Test

open NUnit.Framework
open FsUnit
open Homework2.BinaryTreeMap

let testCases () =
    [ (fun x -> 0), Node(1, Node(2, Leaf, Leaf), Leaf), Node(0, Node(0, Leaf, Leaf), Leaf)
      (fun x -> x + 1), Node(1, Leaf, Node(2, Leaf, Leaf)), Node(2, Leaf, Node(3, Leaf, Leaf))
      (fun x -> x + 1), Leaf, Leaf ]
    |> List.map (fun (func, tree, expected) -> TestCaseData(func, tree, expected))


// [<TestCaseSource(nameof (testCases))>]
// let BinaryTreeMapTest func tree expected =
//     binTreeMap tree func |> should equal expected
