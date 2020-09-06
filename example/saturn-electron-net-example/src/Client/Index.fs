module Index

open Elmish
open Thoth.Fetch
open Browser.Types
open System
open Shared

type WsSender = ElectronCommands -> unit
type BroadcastMode = ViaWebSocket | ViaHTTP
type ConnectionState =
    | DisconnectedFromServer | ConnectedToServer of WsSender | Connecting
    member this.IsConnected =
        match this with
        | ConnectedToServer _ -> true
        | DisconnectedFromServer | Connecting -> false



type Model =
    { 
        ConnectionState : ConnectionState
        Hello: string 
    }

type Msg =
    | ElectronCommandRecieved of ElectronCommands
    | ConnectionChange of ConnectionState
    | OpenContextMenu



module Channel =
    open Browser.WebSocket

    

    let inline decode<'a> x = x |> unbox<string> |> Thoth.Json.Decode.Auto.unsafeFromString<'a>
    let buildWsSender (ws:WebSocket) : WsSender =
        fun (message:ElectronCommands) ->
            let message = {| Topic = ""; Ref = ""; Payload = message |}
            let message = Thoth.Json.Encode.Auto.toString(0, message)
            ws.send message

    let subscription _ =
        let sub dispatch =
            /// Handles push messages from the server and relays them into Elmish messages.
            let onWebSocketMessage (msg:MessageEvent) =
                let msg = msg.data |> decode<{| Payload : string |}>
                msg.Payload
                |> decode<ElectronCommands>
                |> ElectronCommandRecieved
                |> dispatch

            /// Continually tries to connect to the server websocket.
            let rec connect () =
                let ws = WebSocket.Create "ws://localhost:8001/channel"
                ws.onmessage <- onWebSocketMessage
                ws.onopen <- (fun ev ->
                    dispatch (ConnectionChange (ConnectedToServer (buildWsSender ws)))
                    printfn "WebSocket opened")
                ws.onclose <- (fun ev ->
                    dispatch (ConnectionChange DisconnectedFromServer)
                    printfn "WebSocket closed. Retrying connection"
                    promise {
                        do! Promise.sleep 2000
                        dispatch (ConnectionChange Connecting)
                        connect() })

            connect()

        Cmd.ofSub sub



let init() =
    let model : Model =
        { 
            Hello = "Hello!" 
            ConnectionState = DisconnectedFromServer
        }
    model, Cmd.none


let update msg model =
    match msg with
    | ConnectionChange status ->
        { model with ConnectionState = status }, Cmd.none

    | OpenContextMenu ->
        match model.ConnectionState with
        | ConnectedToServer sender -> sender (ElectronCommands.ShowContextMenu)
        | _ -> ()
        model, Cmd.none

    | ElectronCommandRecieved cmd ->
        { model with Hello = cmd.ToString() }, Cmd.none


open Fable.React
open Fable.React.Props

let view model dispatch =
    div [ Style [ TextAlign TextAlignOptions.Center; Padding 40 ] ] [
        div [] [
            img [ Src "favicon.png" ]
            h1 [] [ str "Saturn.Electron.Net Example" ]
            h2 [] [ str model.Hello ]
            h2 [] [ str "Connection State:" ]
            h2 [] [ str (model.ConnectionState.ToString()) ]
            button [
                OnClick (fun _ -> dispatch OpenContextMenu)
            ] [ str "Open ContextMenu" ]
        ]
    ]

