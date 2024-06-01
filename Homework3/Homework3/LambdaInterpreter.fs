module Homework3.LambdaInterpreter

type LambdaTerm =
    | Var of string
    | Application of LambdaTerm * LambdaTerm
    | LambdaAbstraction of string * LambdaTerm

let rec getFV term =
    match term with
    | Var x -> Set.singleton (x)
    | Application(term1, term2) -> Set.union (getFV term1) (getFV term2)
    | LambdaAbstraction(x, term) -> getFV term |> Set.remove x

let isFV var term = getFV (term) |> Set.contains var

let rec renameVar usedVars =
    let rec changeVar v =
        if Set.contains v usedVars then changeVar v + "'" else v

    changeVar "x"

let rec substitute var term substTerm =
    match term with
    | Var x when x = var -> substTerm
    | Var _ -> term
    | Application(term1, term2) -> Application(substitute var term1 substTerm, substitute var term2 substTerm)
    | LambdaAbstraction(x, _) when x = var -> term
    | LambdaAbstraction(x, t) when isFV x substTerm |> not || isFV var t |> not ->
        LambdaAbstraction(x, substitute var t substTerm)
    | LambdaAbstraction(x, t) ->
        let newVar = Set.union (getFV t) (getFV substTerm) |> renameVar
        LambdaAbstraction(newVar, substitute var (substitute x t (Var(newVar))) substTerm)

let rec reduce term =
    match term with
    | Var x -> term
    | LambdaAbstraction(var, t) -> LambdaAbstraction(var, reduce t)
    | Application(term1, term2) ->
        match term1 with
        | LambdaAbstraction(var, t) -> substitute var t term2
        | _ -> Application(reduce term1, reduce term2)
