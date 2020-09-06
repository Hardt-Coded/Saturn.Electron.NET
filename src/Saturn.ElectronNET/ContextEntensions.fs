namespace Saturn.Electron.NET

open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open ElectronNET.API

[<AutoOpen>]
module ContextEntensions =

    
    type HttpContext with
        
        member this.ShowElectronContextMenu () =
            let bw = this.RequestServices.GetService<BrowserWindow>()
            Electron.Menu.ContextMenuPopup(bw)
            ()

        member this.ShowElectronContextMenu bw =
            Electron.Menu.ContextMenuPopup(bw)
            ()