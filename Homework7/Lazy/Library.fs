module Lazy

open System

type ILazy<'a> =
    abstract member Get: unit -> 'a

type SingleThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutable value: Result<'a, exn> option = None

    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some(Ok v) -> v
            | Some(Error ex) -> raise ex
            | None ->
                try
                    let v = supplier ()
                    value <- Some(Ok v)
                    v
                with ex ->
                    value <- Some(Error ex)
                    raise ex

type ThreadSafeLazy<'a>(supplier: unit -> 'a) =
    let mutable value: Result<'a, exn> option = None
    let syncObj = obj ()

    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some(Ok v) -> v
            | Some(Error ex) -> raise ex
            | None ->
                lock syncObj (fun () ->
                    match value with
                    | Some(Ok v) -> v
                    | Some(Error ex) -> raise ex
                    | None ->
                        try
                            let v = supplier ()
                            value <- Some(Ok v)
                            v
                        with ex ->
                            value <- Some(Error ex)
                            raise ex)

type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable value: Result<'a, exn> option = None

    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some(Ok v) -> v
            | Some(Error ex) -> raise ex
            | None ->
                try
                    let result = supplier ()

                    match System.Threading.Interlocked.CompareExchange(&value, Some(Ok result), None) with
                    | Some(Ok v) -> v
                    | Some(Error ex) -> raise ex
                    | None -> result
                with ex ->
                    System.Threading.Interlocked.CompareExchange(&value, Some(Error ex), None)
                    |> ignore

                    raise ex
