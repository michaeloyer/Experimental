module Test

open Fs

let test() =
    let path = """dist/Output.txt"""
    fs.writeFile(path, "Writing file Successful!!\n", fun _ ->
        fs.appendFileSync(path, "Writing Sync Text after successful write\n")
    )

