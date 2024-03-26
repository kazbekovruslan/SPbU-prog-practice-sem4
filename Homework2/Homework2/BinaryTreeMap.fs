module Homework2.BinaryTreeMap

type 'a Tree =
    | Node of 'a * 'a Tree * 'a Tree
    | Leaf

let rec binTreeMap tree func =
    match tree with
    | Leaf -> Leaf
    | Node (value, l, r) -> Node (func value, binTreeMap l func, binTreeMap r func)