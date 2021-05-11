# Cleverbit Coding Task Task Template

This template should be used for coding tasks of Cleverbit.

Three projects are included in this solution:
- Cleverbit.CodingTask.Host: A .NET Core 3.1 Web Application
- Cleverbit.CodingTask.Data: A .NET Core 3.1 class library which includes the first implementation of DB Context and User table.
- Cleverbit.CodingTask.Utilities: A .NET Core 3.1 class library which includes the Hash Service.

Database initialization has been implemented and configured in startup.

Basic authentication has been implemented and wired to the User table in DbContext.

An example API controller has been implemented as PingController which includes two GET methods, one without Authorization and the other one with Authorization.

By default, db connection string is configured for SQL Express. This can be changed in appSettings.Development.json .

Following users are provisioned during startup:

|UserName|Password|
|-|-|
|User1|Password1|
|User2|Password2|
|User3|Password3|
|User4|Password4|

Example AJAX calls to ping APIs (with and without Authorization) are present under Cleverbit.CodingTask.Host/Views/index.html