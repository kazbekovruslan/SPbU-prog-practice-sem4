module Homework5.Tests

open LocalNetwork
open NUnit.Framework
open FsUnit
open System.Collections.Generic

[<Test>]
let ``All net should be infected if all computers with 100 probability of infection`` () =
    let badOS = OS("BadOS", 1)
    let pc1 = PC(badOS, true)
    let pc2 = PC(badOS, false)
    let pc3 = PC(badOS, false)

    let net = new Dictionary<PC, PC list>()
    net.Add(pc1, [ pc2 ])
    net.Add(pc2, [ pc1; pc3 ])
    net.Add(pc3, [ pc2 ])

    let network = LocalNetwork(net)
    network.Run()

    Assert.That(pc1.IsInfected && pc2.IsInfected && pc3.IsInfected)

[<Test>]
let ``All net should be uninfected if all computers with 0 probability of infection`` () =
    let goodOS = OS("GoodOS", 0)
    let pc1 = PC(goodOS, false)
    let pc2 = PC(goodOS, false)
    let pc3 = PC(goodOS, false)

    let net = new Dictionary<PC, PC list>()
    net.Add(pc1, [ pc2 ])
    net.Add(pc2, [ pc1; pc3 ])
    net.Add(pc3, [ pc2 ])

    let network = LocalNetwork(net)
    network.Run()

    Assert.That(not pc1.IsInfected && not pc2.IsInfected && not pc3.IsInfected)

[<Test>]
let ``Infection should spread right`` () =
    let someOS = OS("SomeOS", 0.5)
    let otherOS = OS("OtherOS", 0.9)
    let pc1 = PC(someOS, true)
    let pc2 = PC(someOS, false)
    let pc3 = PC(otherOS, false)
    let pc4 = PC(otherOS, false)

    let net = new Dictionary<PC, PC list>()
    net.Add(pc1, [ pc2; pc4 ])
    net.Add(pc2, [ pc1; pc3 ])
    net.Add(pc3, [ pc2 ])
    net.Add(pc4, [ pc1 ])

    let network = LocalNetwork(net)
    network.Run()

    Assert.That(pc1.IsInfected && pc2.IsInfected && pc3.IsInfected && pc4.IsInfected)

[<Test>]
let ``Unconnected pcs shouldn't be infected after spreading`` () =
    let badOS = OS("BadOS", 1)
    let pc1 = PC(badOS, true)
    let pc2 = PC(badOS, false)
    let pc3 = PC(badOS, false)
    let pc4 = PC(badOS, false)

    let net = new Dictionary<PC, PC list>()
    net.Add(pc1, [ pc2 ])
    net.Add(pc2, [ pc1; pc3 ])
    net.Add(pc3, [ pc2 ])
    net.Add(pc4, [])

    let network = LocalNetwork(net)
    network.Run()

    Assert.That(pc1.IsInfected && pc2.IsInfected && pc3.IsInfected && not pc4.IsInfected)
