module LocalNetwork


open System.Collections.Generic


type OS(name: string, infectionProbability: float) =
    member val Name = name with get
    member val InfectionProbability = infectionProbability with get


type PC(os: OS, isInfected: bool) =
    member val System = os with get
    member val IsInfected = isInfected with get, set


type LocalNetwork(pcsDict: Dictionary<PC, PC list>) =
    let pcs = pcsDict.Keys |> Seq.toList

    member private this.GetInfectedPcsWithUninfectedNeighbours() =
        pcs
        |> List.filter (fun pc -> pc.IsInfected && pcsDict[pc] |> List.exists (fun pc -> not pc.IsInfected))

    member private this.Step() =
        let infectedPcsWithUninfectedNeighbours =
            this.GetInfectedPcsWithUninfectedNeighbours()

        let newlyInfectedPcs =
            infectedPcsWithUninfectedNeighbours
            |> List.map (fun pc -> pcsDict[pc])
            |> List.concat
            |> List.map (fun pc ->
                if System.Random().NextDouble() < pc.System.InfectionProbability then
                    Some pc
                else
                    None)
            |> List.choose id

        for pc in newlyInfectedPcs do
            pc.IsInfected <- true

        printfn "----------"
        pcs |> List.map (fun pc -> printfn "  %s: %b" pc.System.Name pc.IsInfected)

    member this.Run() =
        while this.GetInfectedPcsWithUninfectedNeighbours().Length > 0 do
            this.Step() |> ignore
