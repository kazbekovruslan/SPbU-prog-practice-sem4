module LocalNetwork

type OS(name: string, infectionProbability: float) =
    member val Name = name with get
    member val InfectionProbability = infectionProbability with get


type PC(os: OS, isInfected: bool) =
    member val System = os with get
    member val IsInfected = isInfected with get, set


type LocalNetwork(pcs: PC array) =
    let adjacencyMatrix = Array2D.zeroCreate pcs.Length pcs.Length

    member this.AddConnection(pc1: PC, pc2: PC) =
        let index1 = Array.findIndex (fun p -> p = pc1) pcs
        let index2 = Array.findIndex (fun p -> p = pc2) pcs
        adjacencyMatrix.[index1, index2] <- 1
        adjacencyMatrix.[index2, index1] <- 1

    member private this.Step() =
        let infectedPcs = pcs |> Array.filter (fun pc -> pc.IsInfected)

        let newlyInfectedPcs =
            infectedPcs
            |> Array.collect (fun pc ->
                pcs
                |> Array.indexed
                |> Array.filter (fun (i, p) ->
                    adjacencyMatrix.[Array.findIndex (fun p' -> p' = pc) pcs, i] = 1
                    && not p.IsInfected)
                |> Array.map (fun (i, p) ->
                    if System.Random().NextDouble() < p.System.InfectionProbability then
                        Some p
                    else
                        None)
                |> Array.choose id)

        for pc in newlyInfectedPcs do
            pc.IsInfected <- true

        printfn "----------"
        pcs |> Array.iter (fun pc -> printfn "  %s: %b" pc.System.Name pc.IsInfected)

    member this.Run() =
        while pcs |> Array.exists (fun pc -> not pc.IsInfected) do
            this.Step()



let windows = OS("Windows", 0.8)
let linux = OS("Linux", 0.3)
let macos = OS("macOS", 0.2)

let pc1 = PC(windows, false)
let pc2 = PC(linux, false)
let pc3 = PC(macos, false)
let pc4 = PC(windows, false)
let pc5 = PC(linux, false)

let network = LocalNetwork([| pc1; pc2; pc3; pc4; pc5 |])

network.AddConnection(pc1, pc2)
network.AddConnection(pc1, pc3)
network.AddConnection(pc2, pc4)
network.AddConnection(pc3, pc5)

pc1.IsInfected <- true
network.Run()
