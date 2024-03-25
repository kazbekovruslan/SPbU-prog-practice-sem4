module Homework2.ParsingTree

type Operator =
    | Plus
    | Minus
    | Multiplication
    | Modulo

type 'a ParseTree = 
    | Operand of 'a
    | Operation of 'a ParseTree * Operator * 'a ParseTree

let rec calculate parseTree = 
    match parseTree with
    | Operand value -> value
    | Operation (l, op, r) ->
        match op with 
        | Plus -> (calculate l) + (calculate r)
        | Minus -> (calculate l) - (calculate r)
        | Multiplication -> (calculate l) * (calculate r)
        | Modulo -> (calculate l) % (calculate r)