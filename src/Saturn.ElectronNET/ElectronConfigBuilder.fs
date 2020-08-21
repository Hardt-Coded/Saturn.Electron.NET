namespace Saturn.Electron.NET

open ElectronNET.API.Entities


[<AutoOpen>]
module ElectronConfigBuilder =
    
    open Microsoft.Extensions.DependencyInjection

    type ElectronConfig = {
        Menu : (ServiceProvider -> MenuItem) list
        ContextMenu : (ServiceProvider -> MenuItem) list
        BrowserWindowOption: BrowserWindowOptions option
    }


    
    type ElectronBuilder () =
        
        member __.Yield(_) : ElectronConfig = {
            Menu = []
            ContextMenu = []
            BrowserWindowOption = None
        }

        [<CustomOperation("menu")>]
        member __.SetMenu(state:ElectronConfig, menuItems: (ServiceProvider -> MenuItem) list) =
            { state with
                Menu = menuItems
            }

        [<CustomOperation("contextmenu")>]
        member __.SetContextMenu(state:ElectronConfig, menuItems: (ServiceProvider -> MenuItem) list) =
            { state with
                ContextMenu = menuItems
            }

        [<CustomOperation("window_option")>]
        member __.SetWindowOption(state:ElectronConfig, windowOption: BrowserWindowOptions) =
            { state with BrowserWindowOption = Some windowOption }


        //member __SetThumbnailArea(state:ElectronConfig, value) =
        //    ElectronNET.API.Electron.Menu.
        //    state

        

    let electron = ElectronBuilder()

