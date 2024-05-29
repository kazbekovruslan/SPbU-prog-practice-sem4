module BracketSequence

let braces = [ ('(', ')'); ('[', ']'); ('{', '}') ]

// without another declaring braces in function I have nullReferenceException
let isOpeningBracket x =
    let braces = [ ('(', ')'); ('[', ']'); ('{', '}') ]
    braces |> List.exists (fun (opening, _) -> opening = x)

let isClosingBracket x =
    let braces = [ ('(', ')'); ('[', ']'); ('{', '}') ]
    braces |> List.exists (fun (_, closing) -> closing = x)

let isBracePair x y =
    let braces = [ ('(', ')'); ('[', ']'); ('{', '}') ]
    List.contains (x, y) braces

let isBracketSequenceCorrect (str: string) =
    let rec helper (stack, seq) =
        match seq with
        | [] -> List.isEmpty stack
        | first :: seqTail when isOpeningBracket first -> helper ((first :: stack), seqTail)
        | first :: seqTail when isClosingBracket first ->
            match stack with
            | [] -> false
            | top :: stackTail ->
                if isBracePair top first then
                    helper (stackTail, seqTail)
                else
                    false
        | _ :: seqTail -> helper (stack, seqTail)

    helper ([], Seq.toList str)



let str1 = "[a]"

let isBalanced1 = isBracketSequenceCorrect str1 // true


printfn "%A" $"{isBalanced1}"
