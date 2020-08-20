# ADTool [![Nuget](https://img.shields.io/nuget/v/adtool)](https://www.nuget.org/packages/ADTool/) ![Master Release Pipeline](https://github.com/bduman/adtool/workflows/Master%20Release%20Pipeline/badge.svg)
Compare your assemblies

# Installation

`dotnet tool install --global ADTool`

# Usage

```
$ adtool -h

Usage: ADTool [command] [options]

Options:
  --version     Show version information
  -?|-h|--help  Show help information

Commands:
  compare       Compare 2 assembly

Run 'ADTool [command] -?|-h|--help' for more information about a command.

$ adtool compare -h
Compare 2 assembly

Usage: ADTool compare [options] <FirstAssemblyPath> <SecondAssemblyPath>

Arguments:
  FirstAssemblyPath         First Assembly Path
  SecondAssemblyPath        Second Assembly Path

Options:
  -o|--Output               Output
  -t|--WithOutAssemblyTags  WithoutAssemblyTags
  -?|-h|--help              Show help information
```
