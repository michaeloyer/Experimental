# LoggingWebApp

Testing if a Web API can update a log level via the API and those changes will be recongized.

Status: Successful

To see it in action check the console in Visual Studio or however you are running it. Run the following commands in Powershell individually:

```Powershell
# Update the Minimum Log Level to 'Information'
Invoke-WebRequest https://localhost:44358/Logging/Information -Method Post
```
```Powershell
# Update the Minimum Log Level to 'Fatal'
Invoke-WebRequest https://localhost:44358/Logging/Fatal -Method Post
```
```Powershell
# Write logs using Serilog.ILogger
Invoke-WebRequest https://localhost:44358/Logging/Serilog
```
```Powershell
# Write logs using Microsoft.Extensions.Logging
Invoke-WebRequest https://localhost:44358/Logging/Microsoft
```