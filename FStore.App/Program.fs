// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open FStore.S3

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

let rec repl(ctx: S3Context) =
    printfn "Enter a command:"
    printf "> "
    let input = Console.ReadLine()
    match input.ToLower() with
    | "upload" ->
        printfn "Enter file path:"
        printf "> "
        let filePath = Console.ReadLine()
        printfn "Enter bucket name:"
        printf "> "
        let bucketName = Console.ReadLine()
        printfn "Enter key:"
        printf "> "
        let key = Console.ReadLine()
        ctx.UploadObject(bucketName, key, filePath) |> Async.RunSynchronously
        repl(ctx)
    | "download" ->
        printfn "Enter file path:"
        printf "> "
        let filePath = Console.ReadLine()
        printfn "Enter bucket name:"
        printf "> "
        let bucketName = Console.ReadLine()
        printfn "Enter key:"
        printf "> "
        let key = Console.ReadLine()
        ctx.DownloadObject(bucketName, key, filePath, false) |> Async.RunSynchronously
        repl(ctx)
    | "help" ->
        printfn "Commands:"
        printfn "\tupload"
        printfn "\tdownload"
        printfn "\texit"
        repl(ctx)
    | "exit" ->
        printfn "Exiting..."
    | _ ->
        printfn "Error: unknown command."
    
    
    
[<EntryPoint>]
let main argv =
    match argv.Length > 0 with
    | true ->
        
        let message = from "F#" // Call the function
        printfn "Hello world %s" message
        0 // return an integer exit code
    | false ->
        printfn "No args, running in REPL mode."
        printfn "Enter configuration path:"
        printf "> "
        let configPath = Console.ReadLine()
        match S3Context.Create(configPath) with
        | Ok ctx ->
            repl ctx
            0
        | Error e ->
            printfn $"Error creating context: '{e}'"
            1