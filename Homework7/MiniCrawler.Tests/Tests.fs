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

[<Test>]
let ``downloadPages test`` () =
    let url = "https://se.math.spbu.ru"

    async {
        let! actual = downloadPages url
        printfn "%A" actual

        let expected =
            [ ("https://spbu.ru/", 112326)
              ("https://oops.math.spbu.ru/SE/alumni", 49175)
              ("https://ru.wikipedia.org/wiki/%D0%A2%D0%B5%D1%80%D0%B5%D1%85%D0%BE%D0%B2,_%D0%90%D0%BD%D0%B4%D1%80%D0%B5%D0%B9_%D0%9D%D0%B8%D0%BA%D0%BE%D0%BB%D0%B0%D0%B5%D0%B2%D0%B8%D1%87",
               102683)
              ("https://ru.wikipedia.org/wiki/%D0%A2%D0%B5%D1%80%D0%B5%D1%85%D0%BE%D0%B2,_%D0%90%D0%BD%D0%B4%D1%80%D0%B5%D0%B9_%D0%9D%D0%B8%D0%BA%D0%BE%D0%BB%D0%B0%D0%B5%D0%B2%D0%B8%D1%87",
               102683)
              ("https://www.acm.org/binaries/content/assets/education/curricula-recommendations/cc2005-march06final.pdf",
               758040)
              ("https://t.me/sysprog_admission", 12477)
              ("https://oops.math.spbu.ru/SE", 23408)
              ("https://oops.math.spbu.ru/SE/alumni", 49175) ]

        Assert.AreEqual(expected, actual)
    }
