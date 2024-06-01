module MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

let downloadPageAsync (url: string, client: HttpClient) =
    async {
        try
            let httpRequest = new HttpRequestMessage(HttpMethod.Get, url)

            let! response =
                client.SendAsync(httpRequest, System.Threading.CancellationToken.None)
                |> Async.AwaitTask

            let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
            return content
        with ex ->
            printfn "Failed to download %s: %s" url ex.Message
            return ""
    }

let extractLinks (html: string) =
    let pattern = @"<a\s+(?:[^>]*?\s+)?href=([""'])(http[^""']+)\1"
    let matches = Regex.Matches(html, pattern)
    [ for m in matches -> m.Groups.[2].Value ]

let downloadPages (url: string, client: HttpClient) =
    async {
        let! mainPageHtml = downloadPageAsync (url, client)
        let linksFromMainPage = extractLinks mainPageHtml

        let downloadTasks =
            linksFromMainPage
            |> List.map (fun link ->
                async {
                    let! html = downloadPageAsync (link, client)
                    return (link, html.Length)
                })

        let! results = Async.Parallel downloadTasks
        return results |> List.ofArray
    }

let printSizes (results: (string * int) list) =
    results |> List.iter (fun (link, size) -> printfn "%s — %d" link size)

let downloadAndPrintSizes (url: string) =
    async {
        use client = new HttpClient()
        let! results = downloadPages (url, client)
        printSizes results
    }
