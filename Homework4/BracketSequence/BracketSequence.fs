module BracketSequence

let isBracketSequenceCorrect (str: string) =
    let bracketPairs = [ ('(', ')'); ('[', ']'); ('{', '}') ]
    let stack = System.Collections.Generic.Stack<char>()

    str
    |> Seq.forall (fun c ->
        match c with
        | '('
        | '['
        | '{' ->
            stack.Push(c)
            true
        | ')'
        | ']'
        | '}' ->
            if stack.Count = 0 then
                false
            else
                let expected =
                    bracketPairs
                    |> List.tryFind (fun pair -> fst pair = stack.Peek())
                    |> Option.map snd

                match expected with
                | Some expectedChar when expectedChar = c ->
                    stack.Pop()
                    true
                | _ -> false
        | _ -> true)
    && stack.Count = 0
