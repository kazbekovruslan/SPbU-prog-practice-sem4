module Lazy.Tests

open Lazy
open NUnit.Framework
open FsUnit

let mutable counter = 0

let lazyConstructors =
    [ (fun f -> SingleThreadedLazy f :> ILazy<obj>)
      (fun f -> ThreadSafeLazy f :> ILazy<obj>)
      (fun f -> LockFreeLazy f :> ILazy<obj>) ]
    |> List.map (fun f -> TestCaseData(f))

let multiThreadLazyConstructors =
    [ (fun f -> ThreadSafeLazy f :> ILazy<obj>)
      (fun f -> LockFreeLazy f :> ILazy<obj>) ]
    |> List.map (fun f -> TestCaseData(f))

[<TestCaseSource(nameof lazyConstructors)>]
let ``Value should compute only once`` (lazyConstructor: (unit -> obj) -> ILazy<obj>) =
    let supplier () =
        counter <- counter + 1
        obj ()

    counter <- 0

    let lazyObject = lazyConstructor supplier
    let firstCallValue = lazyObject.Get()
    let secondCallValue = lazyObject.Get()

    Assert.That(counter, Is.EqualTo(1))


[<TestCaseSource(nameof lazyConstructors)>]
let ``Computed value should be same for several Gets`` (lazyConstructor: (unit -> obj) -> ILazy<obj>) =
    let supplier () = 42 :> obj

    let lazyObject = lazyConstructor supplier
    let firstCallValue = lazyObject.Get()
    let secondCallValue = lazyObject.Get()

    Assert.That(firstCallValue, Is.EqualTo(secondCallValue))


[<TestCaseSource(nameof lazyConstructors)>]
let ``Exception in supplier should be thrown`` (lazyConstructor: (unit -> obj) -> ILazy<obj>) =
    let lazyObject = lazyConstructor (fun () -> raise (System.Exception()))
    Assert.Throws<System.Exception>(fun () -> lazyObject.Get() |> ignore) |> ignore


[<TestCaseSource(nameof multiThreadLazyConstructors)>]
let ``Multithread lazies test`` (lazyConstructor: (unit -> obj) -> ILazy<obj>) =
    let supplier () = 42 :> obj

    let lazyObject = lazyConstructor supplier
    let amountOfThreads = 8

    let tasksArray =
        Seq.init amountOfThreads (fun _ -> async { return lazyObject.Get() })

    tasksArray
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.forall (fun object -> object = 42)
    |> should be True
