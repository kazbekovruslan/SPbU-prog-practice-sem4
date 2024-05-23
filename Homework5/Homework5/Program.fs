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



let windows = OS("Windows", 0.8)
let linux = OS("Linux", 0.3)
let macos = OS("macOS", 0.2)

let pc1 = PC(windows, false)
let pc2 = PC(linux, false)
let pc3 = PC(macos, false)
let pc4 = PC(windows, false)
let pc5 = PC(linux, false)
let pc6 = PC(linux, false)

let net = new Dictionary<PC, PC list>()
net.Add(pc1, [ pc2; pc3 ])
net.Add(pc2, [ pc4 ])
net.Add(pc3, [ pc5 ])
net.Add(pc4, [ pc2 ])
net.Add(pc5, [ pc3 ])
net.Add(pc6, [])

let network = LocalNetwork(net)

pc1.IsInfected <- true
network.Run()
