module Orchestration.Web

open Giraffe.HttpHandlers
open Giraffe.Common
open Giraffe.Tasks
open Giraffe.HttpContextExtensions
open Microsoft.AspNetCore.Http

let openTunnel next (ctx : HttpContext) = 
    task {
        let! tunnel = ctx.BindModel<string>()

        do! Application.openTunnel tunnel

        return! setStatusCode 204 next ctx
    }

let root<'a> =
    choose [
        route "/tunnel" >=> choose [
            POST >=> openTunnel
            setStatusCode 405 >=> text "Method not supported"
        ]
        setStatusCode 404 >=> text "Not Found"
    ]
