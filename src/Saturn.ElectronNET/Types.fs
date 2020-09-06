namespace Saturn.Electron.NET

open Microsoft.Extensions.DependencyInjection
open ElectronNET.API.Entities

[<AutoOpen>]
module Types =


    type ElectronConfig = {
        Menu : (ServiceProvider -> MenuItem) list
        ContextMenu : (ServiceProvider -> MenuItem) list
        BrowserWindowOption: BrowserWindowOptions option
    }
