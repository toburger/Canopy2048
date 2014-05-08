#r @"packages/FAKE/tools/FakeLib.dll"
open Fake 
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open System

// Information about the project are used
//  - for version and project name in generated AssemblyInfo file
//  - by the generated NuGet package 
//  - to run tests and to publish documentation on GitHub gh-pages
//  - for documentation, you also need to edit info in "docs/tools/generate.fsx"

// The name of the project 
// (used by attributes in AssemblyInfo, name of a NuGet package and directory in 'src')
let project = "Canopy2048"

// Short summary of the project
// (used as description in AssemblyInfo and as a short summary for NuGet package)
let summary = ""

// Longer description of the project
// (used as a description for NuGet package; line breaks are automatically cleaned up)
let description = """"""
// List of author names (for NuGet package)
let authors = [ "Mathias Brandewinder"; "Tobias Burger" ]
// Tags for your project (for NuGet package)
let tags = "canopy, 2048"

// File system information 
// (<solutionFile>.sln is built during the building process)
let solutionFile  = "Canopy2048"
// Pattern specifying assemblies to be tested using NUnit
let testAssemblies = ""

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted 
let gitHome = "https://github.com/toburger/Canopy2048"
// The name of the project on GitHub
let gitName = "Canopy2048"

// Read additional information from the release notes document
Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

// --------------------------------------------------------------------------------------
// Clean build results & restore NuGet packages

Target "RestorePackages" RestorePackages

Target "Clean" (fun _ ->
    //CleanDirs ["bin"; "temp"]
    !! (solutionFile + "*.sln")
    |> MSBuildRelease "" "Clean"
    |> ignore
)

// --------------------------------------------------------------------------------------
// Build library & test project

Target "Build" (fun _ ->
    !! (solutionFile + "*.sln")
    |> MSBuildRelease "" "Rebuild"
    |> ignore
)

Target "Release" DoNothing

// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

Target "All" DoNothing

"Clean"
  ==> "RestorePackages"
  ==> "Build"
  ==> "All"

"All" 
  ==> "Release"

RunTargetOrDefault "All"
