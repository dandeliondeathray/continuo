module Orchestration.Application

let openTunnel tunnel = async {
    printfn "Registered tunnel endpoint: %s" tunnel
    return ()
}