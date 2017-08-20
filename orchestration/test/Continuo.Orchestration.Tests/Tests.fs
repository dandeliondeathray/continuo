module Tests

open System
open FsCheck.Xunit
open Swensen.Unquote

[<Property>]
let ``My test`` () =
    2 + 1 =! 3
