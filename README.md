# BlazorSignalrOrleans
This project is a simple example of Blazor and Microsoft Orleans integration via SignalR for real-time communication in .NET 7.

## How Does It Work?
The ASP.NET backend uses Orleans Observers to listen for messages from Orleans Grains and forwards the messages to the Blazor front-end using a SignalR connection.
A hosted service in the ASP.NET backend is used to (re)subscribe the Observer every 3 minutes, while the Observer subscribes with a 5 minute timeout.
Identity is used in this example just to show how messages can be tied to a specific identity.

## Configuration
The ASP.NET backend project (BlazorSignalrOrleans) has a "BundledMode" switch in it's appsettings.json. When it's set to "true" the Silo will be started alongside the
backend. This is made for development and debugging purposes just for this example. In ideal deployments this would be set to "false" and the Silo would be started as
a separate application.

## What Is It Good Foor
This example demonstrated how Grains can process data and forward it in real-time to Blazor clients. Example usage would be a chat application, browser games, betting
software or anything that needs a constant stream of data from backend to frontend in combination with distributed computing.

## Next Steps
A logical next-step for usage would be configuring Orleans so it works trully distributed with data streaming between grains and forwarding to Blazor clients. However,
that is outside of scope for this example and you should look into Orleans documentation for that functionality.

## Source
There is a branch for a .NET 5 version of this example but it's not using observers and the implementation was pretty naive. Left here just for archive and in case
anyone wants to look at it.

## Links
* ASP.NET Core - https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0
* Blazor - https://learn.microsoft.com/en-us/aspnet/core/blazor/?WT.mc_id=dotnet-35129-website&view=aspnetcore-7.0
* SignalR - https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction?WT.mc_id=dotnet-35129-website&view=aspnetcore-7.0
* Microsoft Orleans - https://learn.microsoft.com/en-us/dotnet/orleans/overview

## License
Copyright (c) 2020-2022 Daniel Ferenc

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