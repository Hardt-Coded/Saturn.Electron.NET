#r @"C:\Users\Dieselmeister\.nuget\packages\electronnet.api\9.31.2\lib\netcoreapp3.1\ElectronNET.API.dll"


open ElectronNET.API.Entities
open System 

let toLowerFirstLetter (str:string) =
    match str |> Seq.toList with
    | [] ->
        ""
    | head::tail ->
        let tail = String(tail |> List.toArray)
        (head |> string).ToLower() + tail
    

let properties = 
    typeof<BrowserWindowOptions>.GetProperties()
    |> Array.map (fun pi ->
        let propertyName = pi.Name
        let opName = propertyName |> toLowerFirstLetter
        sprintf """
        [<CustomOperation("%s")>]
        member __.Set%s(state:BrowserWindowOptions, value)=
            state.%s <- value
            state

        """ opName propertyName propertyName
        
    )
    |> Array.iter (fun i -> printfn "%s" i)



typeof<MenuItem>.GetProperties()
|> Array.map (fun pi ->
    let propertyName = pi.Name
    let opName = propertyName |> toLowerFirstLetter
    sprintf """
    [<CustomOperation("%s")>]
    member __.Set%s(state:MenuItem, value)=
        state.%s <- value
        state""" opName propertyName propertyName
    
)
|> Array.iter (fun i -> printfn "%s" i)
