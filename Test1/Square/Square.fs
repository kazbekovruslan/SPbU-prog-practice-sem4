module Square

// Returns a string representation of a hollow square with side length n
let square n =
    String.init n (fun lineIndex ->
        if lineIndex = 0 || lineIndex = n - 1 then
            String.init n (fun _ -> "*") + "\n"
        else
            "*" + String.init (n - 2) (fun _ -> " ") + "*" + "\n")
