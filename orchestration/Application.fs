module Orchestration.Application

let registerTunnel tunnel =async {
    printfn "Registered tunnel endpoint: %s" tunnel
    return ()
}