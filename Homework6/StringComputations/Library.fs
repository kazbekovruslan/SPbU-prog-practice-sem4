module StringComputations

type StringComputationsBuilder() =
    member this.Bind(x: string, f: int -> Result<int, string>) =
        match System.Int32.TryParse x with
        | (true, n) -> f n
        | (false, _) -> Error "Error"

    member this.Return(x: int) = Ok x

let calculate = StringComputationsBuilder()
