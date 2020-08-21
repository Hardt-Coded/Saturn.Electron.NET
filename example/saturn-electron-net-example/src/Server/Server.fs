module Server

open Giraffe
open Saturn

open Shared

let webApp =
    router {
        get Route.hello (json "Hello from SAFE!")
    }

open Saturn.Electron.NET
open ElectronNET.API.Entities
open FSharp.Control.Tasks.V2
open ElectronNET.API.Entities

let windowOptions =
    electronWindowOptions {
        title "this is my window!"
        x 100
        y 200
        width 500
        height 300
        darkTheme false
    }


open Microsoft.Extensions.DependencyInjection
open ElectronNET.API

let menuItem1 =
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


let electronApp = 
    electron {
        menu [
            menuItem1
        ]

        contextmenu [
            menuItem1
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
            use_router webApp
            memory_cache
            use_static "public"
            use_json_serializer (Thoth.Json.Giraffe.ThothSerializer())
            use_gzip
            
        }

    run app
    0