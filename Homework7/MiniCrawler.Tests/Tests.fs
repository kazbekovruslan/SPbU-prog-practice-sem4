module MiniCrawler.Tests

open MiniCrawler
open FsUnit
open NUnit.Framework

[<Test>]
let ``extractLinks should test`` () =
    let url = "https://example.com"

    async {
        let! htmlPage = downloadPageAsync url
        let actualLinks = extractLinks htmlPage
        let expectedLinks = [ "https://www.iana.org/domains/example" ]
        Assert.AreEqual(expectedLinks, actualLinks)
    }
