module Square

let square n =
    String.init n (fun lineIndex ->
        if lineIndex = 0 || lineIndex = n - 1 then
            String.init n (fun _ -> "*") + "\n"
        else
            "*" + String.init (n - 2) (fun _ -> " ") + "*" + "\n")
