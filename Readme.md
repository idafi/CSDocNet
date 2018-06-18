# CSDocNet

**CSDocNet** is a quick, lightweight documentation builder for C# assembiles.

It simply reads generated XML comment files, pairs them with reflected type information from the referenced assembly, and uses them to output [MSDN](https://msdn.microsoft.com)-style documentation, in Markdown format. (HTML building is feasible, but not currently a short-term goal.)

## Prerequisites

* CSDocNet's core libraries target .NET Standard `2.0`, and should thus be compatible with any modern .NET platform.
* The entry point executable multitargets .NET Core `2.0`, and .NET Framework `4.7.1`.
* The project is built using [.NET Core `2.x`](https://dotnet.github.io)'s `csproj` system.

To target .NET Core, all you'll need to build is a .NET Core `2.x` SDK installation. To target .NET Framework, you'll also need to install the .NET Framework `4.7.1` SDK. Both SDKs can be acquired from [Microsoft's website](https://www.microsoft.com/net/download/).

## Building

To build, simply build the solution file using either the `dotnet` CLI tool (`dotnet build`), or the Visual Studio / VS Code installation of your choice.

## Testing

To run unit tests, execute `dotnet test src/Test/Test.CSDocNet` from the command line.

## Running

To run from the .NET Core entry point, execute `dotnet build CSDocNet.dll`, passing the XML comment files as arguments.

To run from the .NET Framework entry point, either execute `CSDocNet.exe`, passing the XML comment files as arguments, or drag your XML files onto the executable.

You probably shouldn't, though, as the project isn't yet complete.
