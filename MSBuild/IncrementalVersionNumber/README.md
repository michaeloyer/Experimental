# Incremental Version Number

This project is just to show how to automatically increment the semver build number (1.0.x) when `dotnet pack` is called. All configuration is being done in the [fsproj](IncrementalVersionNumber.fsproj) file, but any dotnet project file works, or this could be moved into a `.targets` file if preferred.

## When do I use this?

Primarily in CI/CD scenarios where you have your own custom nuget server. This will allow developers to make changes to their library code without having to always remember to increment a version number in source or in their pipeline somewhere. This will work great if you have a private server many people will be making updates to and semver details will end up bogging down your team's flow, **but** is probably not ideal if you are trying to strictly follow semver in a publicly consumed package.

## Requirements

You'll need to use this in a script that has access to the nuget feed the package lives in. As long as your script is able to query for the latest nuget package version and pass that into `dotnet pack /p:CurrentVersion=x.x.x` MSBuild will be able to handle the rest.

## Table of Versions

### Table Headers
* **Project Version**: (`<Version>x.x<Version>`) set in your project file (or `VersionPrefix` if you prefer to work with Pre release packages)
* **Command Version**: `dotnet pack /p:CurrentVersion=x.x.x` (Avoid versions like x.x, though it does work because that translates to x.x.-1)
* **Output Version**:  (`ProjectName.x.x.x.nupkg`)

| Project Version | Command Version | Output Version |
|-----------------|-----------------|----------------|
| 1.0             | 1.0.0           | 1.0.1          |
| 1.0             | 1.0.1           | 1.0.2          |
| 1.0             | 1.1.0           | 1.1.1          |
| 1.0             | 2.0.0           | 2.0.1          |
| 1.0             | 2.1.0           | 2.1.1          |
| 1.0             | (unspecified)   | 1.0.0          |
| 1.1             | 1.0.1           | 1.1.0          |
| 2.0             | 1.0.1           | 2.0.0          |
