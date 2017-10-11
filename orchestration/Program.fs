module Orchestration.App

open System
open System.IO
open System.Collections.Generic
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe.HttpHandlers
open Giraffe.Middleware

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------
    
let configureApp (app : IApplicationBuilder) =
    app.UseGiraffeErrorHandler errorHandler
    app.UseGiraffe Web.root

let configureServices (services : IServiceCollection) = ()

let configureLogging (builder : ILoggingBuilder) =
    builder
        .AddConsole()
        .AddDebug() |> ignore

[<EntryPoint>]
let main argv =
    match argv with
    | [| port |] ->
        WebHostBuilder()
            .UseKestrel()
            .UseIISIntegration()
            .Configure(Action<IApplicationBuilder> configureApp)
            .ConfigureServices(configureServices)
            .ConfigureLogging(configureLogging)
            .UseUrls(sprintf "http://0.0.0.0:%s" port)
            .Build()
            .Run()
    | _ ->
        printfn "Usage: ./orchestration <port>"
    0