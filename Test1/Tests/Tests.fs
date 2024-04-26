module Tests

open NUnit.Framework
open FsUnit
open FibEvenSum
open Square
open PriorityQueue

[<Test>]
let ``Sum of Fibonacci even numbers under 1 million should be equal 1089154 `` () =
    let fibEvenSum = fibEvenSum
    fibEvenSum |> should equal 1089154

[<Test>]
let ``Square of size 1`` () = square 1 |> should equal "*\n"

[<Test>]
let ``Square of size 3`` () =
    square 3 |> should equal "***\n* *\n***\n"

[<Test>]
let ``Square of size 5`` () =
    square 5 |> should equal "*****\n*   *\n*   *\n*   *\n*****\n"

[<Test>]
let ``Empty square`` () = square 0 |> should equal ""

[<Test>]
let ``Enqueue elements with priority`` () =
    let pq = PriorityQueue<string>()
    pq.Enqueue("a", 3)
    pq.Enqueue("b", 1)
    pq.Enqueue("c", 2)
    pq.Dequeue() |> should equal "b"
    pq.Dequeue() |> should equal "c"
    pq.Dequeue() |> should equal "a"

[<Test>]
let ``Dequeue from empty queue should throw exception`` () =
    let pq = PriorityQueue()

    Assert.Throws<System.InvalidOperationException>(fun () -> pq.Dequeue())
    |> ignore

[<Test>]
let ``Peek at element with highest priority`` () =
    let pq = PriorityQueue<string>()
    pq.Enqueue("a", 3)
    pq.Enqueue("b", 1)
    pq.Peek() |> should equal "b"
    pq.Dequeue() |> should equal "b"
    pq.Peek() |> should equal "a"

[<Test>]
let ``Peek into empty queue throws exception`` () =
    let pq = PriorityQueue()
    Assert.Throws<System.InvalidOperationException>(fun () -> pq.Peek()) |> ignore
