module HttpClient

open System
open System.Net.Http
open System.Threading

let private httpMessageHandler =
    new SocketsHttpHandler(
        PooledConnectionLifetime=TimeSpan.FromMinutes(float 2),
        UseCookies=false)
    :> HttpMessageHandler

let private getHttpClient() = new HttpClient(httpMessageHandler, disposeHandler=false)

let responseToStream (response:HttpResponseMessage) =
    use response = response
    use content = response.Content
    content.ReadAsStreamAsync() |> Async.AwaitTask |> Async.RunSynchronously
    
let responseToString (response:HttpResponseMessage) =
    use response = response
    use content = response.Content
    content.ReadAsStringAsync() |> Async.AwaitTask |> Async.RunSynchronously
    
let send method (uri:string) (cancellationToken:CancellationToken) =
    async {
        use request = new HttpRequestMessage(method, uri)
        use client = getHttpClient()
        let! response = client.SendAsync(request, cancellationToken) |> Async.AwaitTask
        return response |> HttpResult.ofResponse
    }

let get = send HttpMethod.Get

let getString url token =
    get url token
    |> Async.RunSynchronously
    |> HttpResult.bind responseToString

let getStream url token = 
    get url token
    |> Async.RunSynchronously
    |> HttpResult.bind responseToStream

let post = send HttpMethod.Post
let put = send HttpMethod.Put
let delete = send HttpMethod.Delete
