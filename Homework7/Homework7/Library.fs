module MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

let downloadPageAsync (url: string) =
    async {
        try
            use client = new HttpClient()
            let! html = client.GetStringAsync(url) |> Async.AwaitTask
            return html
        with ex ->
            printfn "Failed to download %s: %s" url ex.Message
            return ""
    }

let extractLinks (html: string) =
    let pattern = @"<a\s+(?:[^>]*?\s+)?href=([""'])(http[^""']+)\1"
    let matches = Regex.Matches(html, pattern)
    [ for m in matches -> m.Groups.[2].Value ]

let downloadPages (url: string) =
    async {
        let! mainPageHtml = downloadPageAsync url
        let linksFromMainPage = extractLinks mainPageHtml

        let downloadTasks =
            linksFromMainPage
            |> List.map (fun link ->
                async {
                    let! html = downloadPageAsync link
                    return (link, html.Length)
                })

        let! results = Async.Parallel downloadTasks
        return results |> List.ofArray
    }

let printSizes (results: (string * int) list) =
    results |> List.iter (fun (link, size) -> printfn "%s — %d" link size)

let downloadAndPrintSizes (url: string) =
    async {
        let! results = downloadPages url
        printSizes results
    }
