module Orchestration.Web

open Giraffe.HttpHandlers
open Giraffe.Common
open Giraffe.Tasks
open Giraffe.HttpContextExtensions
open Microsoft.AspNetCore.Http

let putTunnel next (ctx : HttpContext) = 
    task {
        let! tunnel = ctx.BindModel<string>()

        do! Application.registerTunnel tunnel

        return! json "" next ctx
    }

let root<'a> =
    choose [
        PUT >=> route "/tunnel" >=> putTunnel
        setStatusCode 404 >=> text "Not Found"
    ]
