namespace Saturn.Electron.NET

open ElectronNET.API.Entities
open Microsoft.Extensions.DependencyInjection
open ElectronNET.API
open System


[<AutoOpen>]
module MenuItemBuilder =

    type IsChecked = bool

    type SetCheckState = (bool -> unit)

    

    type MenuItemState = {
        MenuItem: MenuItem
        Action: (ServiceProvider -> unit) option
        SubMenuItems: (ServiceProvider -> MenuItem list) option
        CheckAction: (ServiceProvider -> IsChecked -> SetCheckState -> unit) option
    }

    

    type MenuItemBuilder() =
        
        member __.Yield(_) = 
            let mi = MenuItem()
            let t = typeof<MenuItem>
            // yes damn, the ID setter is "Internal"
            t.GetProperty("Id").SetValue(mi,Guid.NewGuid().ToString())
            {
                MenuItem = mi
                Action = None
                SubMenuItems = None
                CheckAction = None
            }

        [<CustomOperation("onclick")>]
        member __.SetClick(state:MenuItemState, action : ServiceProvider -> unit)=
            { state with Action = Some action }


        /// serviceProvider -> isChecked -> setChecked -> unit
        [<CustomOperation("oncheckAction")>]
        member __.SetCheckAction(state:MenuItemState, action : ServiceProvider -> IsChecked -> SetCheckState -> unit)=
            { state with CheckAction = Some action }



        [<CustomOperation("role")>]
        member __.SetRole(state:MenuItemState, value)=
            state.MenuItem.Role <- value
            state

        [<CustomOperation("menutype")>]
        member __.SetType(state:MenuItemState, value)=
            state.MenuItem.Type <- value
            state

        [<CustomOperation("label")>]
        member __.SetLabel(state:MenuItemState, value)=
            state.MenuItem.Label <- value
            state

        [<CustomOperation("sublabel")>]
        member __.SetSublabel(state:MenuItemState, value)=
            state.MenuItem.Sublabel <- value
            state

        [<CustomOperation("accelerator")>]
        member __.SetAccelerator(state:MenuItemState, value)=
            state.MenuItem.Accelerator <- value
            state

        [<CustomOperation("icon")>]
        member __.SetIcon(state:MenuItemState, value)=
            state.MenuItem.Icon <- value
            state

        [<CustomOperation("enabled")>]
        member __.SetEnabled(state:MenuItemState, value)=
            state.MenuItem.Enabled <- value
            state

        [<CustomOperation("visible")>]
        member __.SetVisible(state:MenuItemState, value)=
            state.MenuItem.Visible <- value
            state

        [<CustomOperation("ischecked")>]
        member __.SetChecked(state:MenuItemState, value)=
            state.MenuItem.Checked <- value
            state

        [<CustomOperation("submenu")>]
        member __.SetSubmenu(state:MenuItemState, value:(ServiceProvider -> MenuItem) list) =
            let subItems serviceProvider =
                value
                |> List.map (fun i -> i serviceProvider)
            
            { state with SubMenuItems = Some subItems }

        [<CustomOperation("position")>]
        member __.SetPosition(state:MenuItemState, value)=
            state.MenuItem.Position <- value
            state

        member __.Run(state) =
            fun serviceProvider ->
                match state.Action with
                | None ->
                    ()
                | Some action ->
                    state.MenuItem.Click <- System.Action(fun () -> action serviceProvider)

                match state.SubMenuItems with
                | None ->
                    ()
                | Some subMenuItems ->
                    state.MenuItem.Submenu <- (subMenuItems serviceProvider |> List.toArray)

                match state.CheckAction with
                | None ->
                    ()
                | Some checkAction ->
                    let setChecked b =
                        state.MenuItem.Checked <- b
                        let electronConfig = serviceProvider.GetService<ElectronConfig>()

                        
                        match electronConfig.Menu with
                        | [] ->
                            ()
                        | menu ->
                            let browserWindow = serviceProvider.GetService<ElectronNET.API.BrowserWindow>()
                            let menuItemsWithSerivceCollection =
                                menu
                                |> List.map (fun f -> f serviceProvider)
                                |> List.toArray

                            Electron.Menu.SetApplicationMenu(menuItemsWithSerivceCollection)

                            
                            
                        match electronConfig.ContextMenu with
                        | [] ->
                            ()
                        | contextmenu ->
                            let browserWindow = serviceProvider.GetService<ElectronNET.API.BrowserWindow>()
                            let contextMenuItemsWithSerivceCollection =
                                contextmenu
                                |> List.map (fun f -> f serviceProvider)
                                |> List.toArray

                            Electron.Menu.SetContextMenu(browserWindow,contextMenuItemsWithSerivceCollection)

                            

                    state.MenuItem.Click <- (System.Action(fun () -> checkAction serviceProvider state.MenuItem.Checked setChecked))
                    
                state.MenuItem
            
            


    let menuitem = MenuItemBuilder()


    let labelMenu l action = 
        menuitem { 
            label l
            onclick action
            menutype MenuType.normal
        }

    let subMenu l sm =
        menuitem {
            label l
            submenu sm
            menutype MenuType.submenu
        }

    let menuSperator =
        menuitem {
            menutype MenuType.separator
            
        }


    let checkMenu l isChecked action =
        menuitem { 
            label l
            oncheckAction action
            menutype MenuType.checkbox
            ischecked isChecked
        }


    let radioMenu l action =
        menuitem { 
            label l
            oncheckAction action
            menutype MenuType.radio
        }

    let devtoolMenu =
        menuitem {
            label "DevTools"
            menutype MenuType.normal
            role MenuRole.toggledevtools

        }



