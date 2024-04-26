module PriorityQueue

// Represents a Priority Queue data structure
type PriorityQueue<'a>() =
    let mutable data = []

    // Adds an element to the priority queue with the given priority
    member this.Enqueue(x: 'a, priority: int) =
        let rec insert (x: 'a * int) (xs: ('a * int) list) =
            match xs with
            | [] -> [ x ]
            | (y, p) :: ys when snd x < p -> x :: xs
            | (y, p) :: ys -> (y, p) :: (insert x ys)

        data <- insert (x, priority) data

    // Removes and returns the element with the highest priority
    member this.Dequeue() =
        match data with
        | [] -> raise (System.InvalidOperationException("Queue is empty"))
        | (x, _) :: xs ->
            data <- xs
            x

    // Returns the element with the highest priority without removing it
    member this.Peek() =
        match data with
        | [] -> raise (System.InvalidOperationException("Queue is empty"))
        | (x, _) :: _ -> x
