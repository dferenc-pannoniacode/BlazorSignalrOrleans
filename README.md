# BlazorSignalrOrleans
A simple example of Blazor WASM (ASP.NET Core Hosted), SignalR, Microsoft Orleans and Identity authentication and authorization integration in .NET 5.

## Purpose
This project is a small demonstration of how to integrate Blazor WASM (ASP.NET Core Hosted), SignalR, Microsoft Orleans and Identity authentication and authorization in .NET 5.
There is a switch in appsettings.json "BundledMode" which controls if the silo is run from the ASP.NET application or should be run separately/standalone. I know there is Orleans integration
directly with ASP.NET host but I don't think that is a good way to go regarding scaling.

One SignalR hub is implemented which receives a message from a chat page on the Blazor app and calls an Orleans Grain which does some work with it and returns a result which is
then forwarded to the chat page as a response. The idea is behind this that the ASP.NET application itself is kind of a "switch" that routes "traffic" towards proper Grains.

Check out the technology specific documentation on more info and ideas on how to use these technologies in your own projects.

## To Do
* Integrate Entity Framework with Grains
* Add state persistence to Grains
* More ideas might come to my mind later...

## Links
* ASP.NET Core - https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-5.0
* Blazor - https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-5.0
* SignalR - https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0
* Microsoft Orleans - https://dotnet.github.io/orleans/docs/index.html
* Identity - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-5.0

## License
Copyright (c) 2020 Daniel Ferenc

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
