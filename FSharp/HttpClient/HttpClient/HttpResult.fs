module HttpResult

open System.Net.Http
 
type HttpResult<'T> =
    | Success of 'T
    | Fail of string
    
let ofResponse (response:HttpResponseMessage) =
    if (response.IsSuccessStatusCode)
    then Success response
    else Fail response.ReasonPhrase
    
let bind f = function
    | Success t -> Success (f t)
    | Fail message -> Fail message
