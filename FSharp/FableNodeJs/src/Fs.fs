module Fs

open Fable.Core

[<ImportDefault("fs")>]
let fs: Node.Fs.IExports = jsNative
