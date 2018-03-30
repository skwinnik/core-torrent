# ASP.NET Core Torrent Client

## Steps to start
This project requires DevExpress Bootstrap ASP.NET Core components. The components are paid, however a [trial version is available](https://community.devexpress.com/blogs/aspnet/archive/2018/03/05/now-available-the-devexpress-nuget-package-portal-nuget-devexpress-com.aspx)
1. Add a package source to this project to obtain required NuGet packages - https://nuget.devexpress.com/
2. Install Transmission to your machine (either Raspberry Pi or any other PC that supports ASP.NET Core and Transmission), and specify its RPC address in the appsettings.json:
```
  "TransmissionHostUrl": "http://127.0.0.1:9091/transmission/rpc/"
```
Note that "localhost" may work incorrectly or slow due to [this issue](https://github.com/dotnet/corefx/issues/24104), so you may want to use 127.0.0.1 instead.