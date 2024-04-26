module PriorityQueue

type PriorityQueue<'a>() =
    let mutable data = []

    member this.Enqueue(x: 'a, priority: int) =
        let rec insert (x: 'a * int) (xs: ('a * int) list) =
            match xs with
            | [] -> [ x ]
            | (y, p) :: ys when snd x < p -> x :: xs
            | (y, p) :: ys -> (y, p) :: (insert x ys)

        data <- insert (x, priority) data

    member this.Dequeue() =
        match data with
        | [] -> raise (System.InvalidOperationException("Queue is empty"))
        | (x, _) :: xs ->
            data <- xs
            x

    member this.Peek() =
        match data with
        | [] -> raise (System.InvalidOperationException("Queue is empty"))
        | (x, _) :: _ -> x
