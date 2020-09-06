module Server

open Giraffe
open Saturn

open Shared



open Saturn.Electron.NET
open ElectronNET.API.Entities
open FSharp.Control.Tasks.V2
open ElectronNET.API.Entities
open Microsoft.Extensions.DependencyInjection
open ElectronNET.API
open Microsoft.Extensions.Logging
open Saturn.Channels


module Channel =

    open Thoth.Json.Net

    /// Sends a message to a specific client by their socket ID.
    let sendElectronCommand (hub:Channels.ISocketHub) socketId (payload:ElectronCommands) = task {
        let payload = Encode.Auto.toString(0, payload)
        do! hub.SendMessageToClient "/channel" socketId "" payload
    }
    

    /// Sets up the channel to listen to clients.
    let channel = channel {
        join (fun ctx socketId ->
            task {
                ctx.GetLogger().LogInformation("Client has connected. They've been assigned socket Id: {socketId}", socketId)
                return Channels.Ok
            })
        handle "" (fun ctx clientInfo (message:Message<ElectronCommands>) ->
            task {
                printfn "%A" message
                //let command = message.Payload |> string |> Decode.Auto.unsafeFromString<ElectronCommands>
                let command = message.Payload
                // Here we handle any websocket client messages in a type-safe manner
                match command with
                | ShowContextMenu ->
                    printfn "Context Menu Open called!"
                    ctx.ShowElectronContextMenu()
            })
    }
    


[<AutoOpen>]
module ElectronSettings =

    let webPrefs =
        webPreferences {
            devTools true
            //zoomFactor 1
        }

    let windowOptions =
        electronWindowOptions {
            title "this is my window!"
            x 100
            y 200
            width 600
            height 400
            darkTheme false
            webPreferences webPrefs
        }


[<AutoOpen>]
module ElectonMenu =

    let myLabelMenu1 = labelMenu "myLabel1" (fun _ -> printfn "myLabel1 selected")

    let myLabelMenu2 = labelMenu "myLabel2" (fun _ -> printfn "myLabel2 selected")

    let myRadioMenu1 = radioMenu "myRadio1" (fun _ isChecked setCheckBox -> printfn "myRadio1 selected"; setCheckBox (isChecked |> not))

    let myRadioMenu2 = radioMenu "myRadio2" (fun _ isChecked setCheckBox -> printfn "myRadio2 selected"; setCheckBox (isChecked |> not))

    let myCheckMenu1 = checkMenu "myCheck1" true (fun _ isChecked setCheckBox -> printfn "myCheck1 selected"; setCheckBox (isChecked |> not))

    let myCheckMenu2 = checkMenu "myCheck2" false (fun _ isChecked setCheckBox -> printfn "myCheck2 selected"; setCheckBox (isChecked |> not))


    let menuCustomItem1 =
        menuitem {
            menutype MenuType.normal
            label "Make me bigger!"
            onclick (fun sp ->
                let browserWindow = sp.GetService<BrowserWindow>()
                ElectronNET.API.Electron.Menu.SetContextMenu(browserWindow, [| MenuItem(Label="Bla!") |])
                printfn "Action Called!"
                browserWindow.SetSize(600,700)
                ()
            )
        }

    


    let mySubMenu = subMenu "myMenu" [
        myLabelMenu1
        myLabelMenu2
        myRadioMenu1
        myRadioMenu2
        menuSperator
        myCheckMenu1
        myCheckMenu2
        menuCustomItem1
        devtoolMenu
    ]


let electronApp = 
    electron {
        menu [
            mySubMenu
        ]

        contextmenu [
            myLabelMenu1
            myLabelMenu2
            myRadioMenu1
            myRadioMenu2
            menuSperator
            myCheckMenu1
            myCheckMenu2
            menuCustomItem1
            devtoolMenu
        ]

        window_option windowOptions

    }

[<EntryPoint>]
let main args =
    

    let app =
        application {
            cli_arguments args
            use_electron 
                "http://0.0.0.0:8085" 
                electronApp
            no_router
            add_channel "/channel" Channel.channel
            memory_cache
            use_static "public"
            use_json_serializer (Thoth.Json.Giraffe.ThothSerializer())
            use_gzip
            
        }

    run app
    0