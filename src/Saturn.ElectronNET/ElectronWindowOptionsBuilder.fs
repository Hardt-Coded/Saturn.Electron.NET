namespace Saturn.Electron.NET

open ElectronNET.API.Entities

[<AutoOpen>]
module ElectronWindowOptionsBuilder =

    

    type ElectronWindowOptionsBuilder() =
        member __.Yield(_) = new BrowserWindowOptions()

        [<CustomOperation("width")>]
        member __.SetWidth(state:BrowserWindowOptions, value)=
            state.Width <- value
            state

        [<CustomOperation("height")>]
        member __.SetHeight(state:BrowserWindowOptions, value)=
            state.Height <- value
            state

        [<CustomOperation("x")>]
        member __.SetX(state:BrowserWindowOptions, value)=
            state.X <- value
            state

        [<CustomOperation("y")>]
        member __.SetY(state:BrowserWindowOptions, value)=
            state.Y <- value
            state

        [<CustomOperation("useContentSize")>]
        member __.SetUseContentSize(state:BrowserWindowOptions, value)=
            state.UseContentSize <- value
            state

        [<CustomOperation("center")>]
        member __.SetCenter(state:BrowserWindowOptions, value)=
            state.Center <- value
            state

        [<CustomOperation("minWidth")>]
        member __.SetMinWidth(state:BrowserWindowOptions, value)=
            state.MinWidth <- value
            state

        [<CustomOperation("minHeight")>]
        member __.SetMinHeight(state:BrowserWindowOptions, value)=
            state.MinHeight <- value
            state

        [<CustomOperation("maxWidth")>]
        member __.SetMaxWidth(state:BrowserWindowOptions, value)=
            state.MaxWidth <- value
            state

        [<CustomOperation("maxHeight")>]
        member __.SetMaxHeight(state:BrowserWindowOptions, value)=
            state.MaxHeight <- value
            state

        [<CustomOperation("resizable")>]
        member __.SetResizable(state:BrowserWindowOptions, value)=
            state.Resizable <- value
            state

        [<CustomOperation("movable")>]
        member __.SetMovable(state:BrowserWindowOptions, value)=
            state.Movable <- value
            state

        [<CustomOperation("minimizable")>]
        member __.SetMinimizable(state:BrowserWindowOptions, value)=
            state.Minimizable <- value
            state

        [<CustomOperation("maximizable")>]
        member __.SetMaximizable(state:BrowserWindowOptions, value)=
            state.Maximizable <- value
            state

        [<CustomOperation("closable")>]
        member __.SetClosable(state:BrowserWindowOptions, value)=
            state.Closable <- value
            state

        [<CustomOperation("focusable")>]
        member __.SetFocusable(state:BrowserWindowOptions, value)=
            state.Focusable <- value
            state

        [<CustomOperation("alwaysOnTop")>]
        member __.SetAlwaysOnTop(state:BrowserWindowOptions, value)=
            state.AlwaysOnTop <- value
            state

        [<CustomOperation("fullscreen")>]
        member __.SetFullscreen(state:BrowserWindowOptions, value)=
            state.Fullscreen <- value
            state

        [<CustomOperation("fullscreenable")>]
        member __.SetFullscreenable(state:BrowserWindowOptions, value)=
            state.Fullscreenable <- value
            state

        [<CustomOperation("skipTaskbar")>]
        member __.SetSkipTaskbar(state:BrowserWindowOptions, value)=
            state.SkipTaskbar <- value
            state

        [<CustomOperation("kiosk")>]
        member __.SetKiosk(state:BrowserWindowOptions, value)=
            state.Kiosk <- value
            state

        [<CustomOperation("title")>]
        member __.SetTitle(state:BrowserWindowOptions, value)=
            state.Title <- value
            state

        [<CustomOperation("icon")>]
        member __.SetIcon(state:BrowserWindowOptions, value)=
            state.Icon <- value
            state

        [<CustomOperation("show")>]
        member __.SetShow(state:BrowserWindowOptions, value)=
            state.Show <- value
            state

        [<CustomOperation("frame")>]
        member __.SetFrame(state:BrowserWindowOptions, value)=
            state.Frame <- value
            state

        [<CustomOperation("modal")>]
        member __.SetModal(state:BrowserWindowOptions, value)=
            state.Modal <- value
            state

        [<CustomOperation("acceptFirstMouse")>]
        member __.SetAcceptFirstMouse(state:BrowserWindowOptions, value)=
            state.AcceptFirstMouse <- value
            state

        [<CustomOperation("disableAutoHideCursor")>]
        member __.SetDisableAutoHideCursor(state:BrowserWindowOptions, value)=
            state.DisableAutoHideCursor <- value
            state

        [<CustomOperation("autoHideMenuBar")>]
        member __.SetAutoHideMenuBar(state:BrowserWindowOptions, value)=
            state.AutoHideMenuBar <- value
            state

        [<CustomOperation("enableLargerThanScreen")>]
        member __.SetEnableLargerThanScreen(state:BrowserWindowOptions, value)=
            state.EnableLargerThanScreen <- value
            state

        [<CustomOperation("backgroundColor")>]
        member __.SetBackgroundColor(state:BrowserWindowOptions, value)=
            state.BackgroundColor <- value
            state

        [<CustomOperation("hasShadow")>]
        member __.SetHasShadow(state:BrowserWindowOptions, value)=
            state.HasShadow <- value
            state

        [<CustomOperation("darkTheme")>]
        member __.SetDarkTheme(state:BrowserWindowOptions, value)=
            state.DarkTheme <- value
            state

        [<CustomOperation("transparent")>]
        member __.SetTransparent(state:BrowserWindowOptions, value)=
            state.Transparent <- value
            state

        [<CustomOperation("type")>]
        member __.SetType(state:BrowserWindowOptions, value)=
            state.Type <- value
            state

        [<CustomOperation("titleBarStyle")>]
        member __.SetTitleBarStyle(state:BrowserWindowOptions, value)=
            state.TitleBarStyle <- value
            state

        [<CustomOperation("fullscreenWindowTitle")>]
        member __.SetFullscreenWindowTitle(state:BrowserWindowOptions, value)=
            state.FullscreenWindowTitle <- value
            state

        [<CustomOperation("thickFrame")>]
        member __.SetThickFrame(state:BrowserWindowOptions, value)=
            state.ThickFrame <- value
            state

        [<CustomOperation("vibrancy")>]
        member __.SetVibrancy(state:BrowserWindowOptions, value)=
            state.Vibrancy <- value
            state

        [<CustomOperation("zoomToPageWidth")>]
        member __.SetZoomToPageWidth(state:BrowserWindowOptions, value)=
            state.ZoomToPageWidth <- value
            state

        [<CustomOperation("tabbingIdentifier")>]
        member __.SetTabbingIdentifier(state:BrowserWindowOptions, value)=
            state.TabbingIdentifier <- value
            state

        [<CustomOperation("webPreferences")>]
        member __.SetWebPreferences(state:BrowserWindowOptions, value)=
            state.WebPreferences <- value
            state

    let electronWindowOptions = ElectronWindowOptionsBuilder() 