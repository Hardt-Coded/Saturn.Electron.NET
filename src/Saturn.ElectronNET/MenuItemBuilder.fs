namespace Saturn.Electron.NET

open ElectronNET.API.Entities
open Microsoft.Extensions.DependencyInjection


[<AutoOpen>]
module MenuItemBuilder =

    type MenuItemState = {
        MenuItem:MenuItem
        Action: (ServiceProvider -> unit) option
    }

    type MenuItemBuilder() =
        
        member __.Yield(_) = {
            MenuItem = new MenuItem()
            Action = None
        }

        [<CustomOperation("onclick")>]
        member __.SetClick(state:MenuItemState, action : ServiceProvider -> unit)=
            { state with Action = Some action }

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

        [<CustomOperation("checked")>]
        member __.SetChecked(state:MenuItemState, value)=
            state.MenuItem.Checked <- value
            state

        [<CustomOperation("submenu")>]
        member __.SetSubmenu(state:MenuItemState, value)=
            state.MenuItem.Submenu <- value
            state

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

                state.MenuItem
            
            


    let menuitem = MenuItemBuilder()



