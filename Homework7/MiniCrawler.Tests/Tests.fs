module MiniCrawler.Tests

open MiniCrawler
open FsUnit
open NUnit.Framework
open Moq
open System.Net.Http
open System.Net

[<Test>]
let ``extractLinks test`` () =
    let url = "https://example.com"

    async {
        use client = new HttpClient()
        let! htmlPage = downloadPageAsync (url, client)
        let actualLinks = extractLinks htmlPage
        let expectedLinks = [ "https://www.iana.org/domains/example" ]
        Assert.AreEqual(expectedLinks, actualLinks)
    }

[<Test>]
let ``downloadPages test`` () =
    let client = Mock<HttpClient>()
    let url = "https://example.com/"

    let response = new HttpResponseMessage(HttpStatusCode.OK)

    client
        .Setup(fun client ->
            client.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<System.Threading.CancellationToken>()))
        .ReturnsAsync(response)
        .Callback(fun (req: HttpRequestMessage) (ct: System.Threading.CancellationToken) ->
            if (req).RequestUri.AbsoluteUri = url then
                response.Content <- new StringContent("Example content")
            else
                response.Content <- new StringContent($"<html><body><a href=\"{url}\">Example</a></body></html>"))
    |> ignore

    let expected = [ (url, 15) ]

    let actual =
        Async.RunSynchronously(downloadPages ("https://fakeurl.com", client.Object))

    Assert.AreEqual(expected, actual)

[<Test>]
let ``downloadPages with 1 correct and 1 incorrect links`` () : unit =
    let client = Mock<HttpClient>()
    let url = "https://example.com/"
    let urlNotFould = "notFound.com"

    let response = new HttpResponseMessage(HttpStatusCode.OK)

    client
        .Setup(fun client ->
            client.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<System.Threading.CancellationToken>()))
        .ReturnsAsync(response)
        .Callback(fun (req: HttpRequestMessage) (ct: System.Threading.CancellationToken) ->
            if (req).RequestUri.AbsoluteUri = url then
                response.Content <- new StringContent("Example content")
            else
                response.Content <-
                    new StringContent(
                        $"<html><body><a href=\"{url}\">Example</a></body> <a href=\"{urlNotFould}\">Example</a></body></html>"
                    ))
    |> ignore

    let expected = [ (url, 15) ]

    let actual =
        Async.RunSynchronously(downloadPages ("https://fakeurl.com", client.Object))

    Assert.AreEqual(expected, actual)
