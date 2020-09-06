namespace Saturn.Electron.NET

[<AutoOpen>]
module WebPreferencesBuilder =

    open ElectronNET.API.Entities

    type WebPreferencesBuilder () =
        
        member __.Yield(_) =
            new WebPreferences()

        [<CustomOperation("devTools")>]
        member __.SetDevTools(state:WebPreferences, value)=
            state.DevTools <- value
            state

        [<CustomOperation("nodeIntegration")>]
        member __.SetNodeIntegration(state:WebPreferences, value)=
            state.NodeIntegration <- value
            state

        [<CustomOperation("nodeIntegrationInWorker")>]
        member __.SetNodeIntegrationInWorker(state:WebPreferences, value)=
            state.NodeIntegrationInWorker <- value
            state

        [<CustomOperation("preload")>]
        member __.SetPreload(state:WebPreferences, value)=
            state.Preload <- value
            state

        [<CustomOperation("sandbox")>]
        member __.SetSandbox(state:WebPreferences, value)=
            state.Sandbox <- value
            state

        [<CustomOperation("partition")>]
        member __.SetPartition(state:WebPreferences, value)=
            state.Partition <- value
            state

        [<CustomOperation("zoomFactor")>]
        member __.SetZoomFactor(state:WebPreferences, value)=
            state.ZoomFactor <- value
            state

        [<CustomOperation("javascript")>]
        member __.SetJavascript(state:WebPreferences, value)=
            state.Javascript <- value
            state

        [<CustomOperation("webSecurity")>]
        member __.SetWebSecurity(state:WebPreferences, value)=
            state.WebSecurity <- value
            state

        [<CustomOperation("allowRunningInsecureContent")>]
        member __.SetAllowRunningInsecureContent(state:WebPreferences, value)=
            state.AllowRunningInsecureContent <- value
            state

        [<CustomOperation("images")>]
        member __.SetImages(state:WebPreferences, value)=
            state.Images <- value
            state

        [<CustomOperation("textAreasAreResizable")>]
        member __.SetTextAreasAreResizable(state:WebPreferences, value)=
            state.TextAreasAreResizable <- value
            state

        [<CustomOperation("webgl")>]
        member __.SetWebgl(state:WebPreferences, value)=
            state.Webgl <- value
            state

        [<CustomOperation("webaudio")>]
        member __.SetWebaudio(state:WebPreferences, value)=
            state.Webaudio <- value
            state

        [<CustomOperation("plugins")>]
        member __.SetPlugins(state:WebPreferences, value)=
            state.Plugins <- value
            state

        [<CustomOperation("experimentalFeatures")>]
        member __.SetExperimentalFeatures(state:WebPreferences, value)=
            state.ExperimentalFeatures <- value
            state

        [<CustomOperation("experimentalCanvasFeatures")>]
        member __.SetExperimentalCanvasFeatures(state:WebPreferences, value)=
            state.ExperimentalCanvasFeatures <- value
            state

        [<CustomOperation("scrollBounce")>]
        member __.SetScrollBounce(state:WebPreferences, value)=
            state.ScrollBounce <- value
            state

        [<CustomOperation("enableBlinkFeatures")>]
        member __.SetEnableBlinkFeatures(state:WebPreferences, value)=
            state.EnableBlinkFeatures <- value
            state

        [<CustomOperation("disableBlinkFeatures")>]
        member __.SetDisableBlinkFeatures(state:WebPreferences, value)=
            state.DisableBlinkFeatures <- value
            state

        [<CustomOperation("defaultFontFamily")>]
        member __.SetDefaultFontFamily(state:WebPreferences, value)=
            state.DefaultFontFamily <- value
            state

        [<CustomOperation("defaultFontSize")>]
        member __.SetDefaultFontSize(state:WebPreferences, value)=
            state.DefaultFontSize <- value
            state

        [<CustomOperation("defaultMonospaceFontSize")>]
        member __.SetDefaultMonospaceFontSize(state:WebPreferences, value)=
            state.DefaultMonospaceFontSize <- value
            state

        [<CustomOperation("minimumFontSize")>]
        member __.SetMinimumFontSize(state:WebPreferences, value)=
            state.MinimumFontSize <- value
            state

        [<CustomOperation("defaultEncoding")>]
        member __.SetDefaultEncoding(state:WebPreferences, value)=
            state.DefaultEncoding <- value
            state

        [<CustomOperation("backgroundThrottling")>]
        member __.SetBackgroundThrottling(state:WebPreferences, value)=
            state.BackgroundThrottling <- value
            state

        [<CustomOperation("offscreen")>]
        member __.SetOffscreen(state:WebPreferences, value)=
            state.Offscreen <- value
            state

        [<CustomOperation("contextIsolation")>]
        member __.SetContextIsolation(state:WebPreferences, value)=
            state.ContextIsolation <- value
            state

        [<CustomOperation("nativeWindowOpen")>]
        member __.SetNativeWindowOpen(state:WebPreferences, value)=
            state.NativeWindowOpen <- value
            state

        [<CustomOperation("webviewTag")>]
        member __.SetWebviewTag(state:WebPreferences, value)=
            state.WebviewTag <- value
            state

    let webPreferences = WebPreferencesBuilder()



