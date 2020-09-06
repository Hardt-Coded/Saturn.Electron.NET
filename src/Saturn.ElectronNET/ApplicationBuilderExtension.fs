namespace Saturn.Electron.NET

open Saturn
open ElectronNET.API
open ElectronNET.API.Entities
open FSharp.Control.Tasks.V2.ContextInsensitive
open Microsoft.AspNetCore.Hosting
open System
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Builder




[<AutoOpen>]
module ApplicationBuilderExtension =

    type Saturn.Application.ApplicationBuilder with
    
        [<CustomOperation("use_electron")>]
        member __.UseElectron(state:ApplicationState, 
                alternateUrl:string,
                config: ElectronConfig) =
            // check if is run by the electron cli
            let hasElectronParams =
                state.CliArguments
                |> Option.map (fun args -> args |> Array.exists (fun a -> a.Contains("ELECTRON",StringComparison.InvariantCultureIgnoreCase)))
                |> Option.defaultValue false


            let webHostConfig (cfg:IWebHostBuilder) =
                if hasElectronParams then
                    printfn "We have args"
                    printfn "%A" state.CliArguments
                    cfg.UseElectron(Option.toObj state.CliArguments)
                else
                    cfg.UseUrls(alternateUrl)


            let serviceConfig (services:IServiceCollection) =
                let addBrowserWindow () =
                    task {
                        if HybridSupport.IsElectronActive then
                            printfn "we are in!"
                            let option = 
                                match config.BrowserWindowOption with
                                | None ->
                                    BrowserWindowOptions()
                                | Some opt ->
                                    opt
                            option.Show <- false
                            let! browserWindow = Electron.WindowManager.CreateWindowAsync(option)
                            browserWindow.add_OnReadyToShow(fun () ->
                                
                                match config.Menu with
                                | [] ->
                                    ()
                                | menu ->
                                    let sp = services.BuildServiceProvider()
                                    let menuItemsWithSerivceCollection =
                                        menu
                                        |> List.map (fun f -> f sp )
                                        |> List.toArray

                                    Electron.Menu.SetApplicationMenu(menuItemsWithSerivceCollection)

                                match config.ContextMenu with
                                | [] ->
                                    ()
                                | contextmenu ->
                                    let sp = services.BuildServiceProvider()
                                    let contextMenuItemsWithSerivceCollection =
                                        contextmenu
                                        |> List.map (fun f -> f sp)
                                        |> List.toArray

                                    Electron.Menu.SetContextMenu(browserWindow,contextMenuItemsWithSerivceCollection)
                                    Electron.IpcMain.On("show-context-menu", fun args ->
                                        Electron.Menu.ContextMenuPopup(browserWindow)
                                    )
                                browserWindow.Show()
                            )
                            return services
                                .AddSingleton<BrowserWindow>(browserWindow)
                                .AddSingleton<ElectronConfig>(config)
                                
                        else
                            return services
                    }
                Task.Run<IServiceCollection>(fun _ -> task { return! addBrowserWindow () }).Result

            let appBuildConfig (app:IApplicationBuilder) =
                let browserWindow = app.ApplicationServices.GetService<BrowserWindow>()
                if (browserWindow |> isNull |> not) then
                
                    Task.Run<unit>(fun _ -> 
                        task { 
                            do! browserWindow.WebContents.Session.ClearCacheAsync()
                            //browserWindow.SetThumbnailClip
                        }) |> ignore
                
                

                app
            
            {
                state with
                    WebHostConfigs = webHostConfig::state.WebHostConfigs
                    ServicesConfig = serviceConfig::state.ServicesConfig
                    AppConfigs = appBuildConfig::state.AppConfigs
            }
            
