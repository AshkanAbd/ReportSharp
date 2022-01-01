# ReportSharp-1.0.5:

## Description:

Crash and information reporter for dotnet.   
The package that reports request, crashes, exceptions and any other data you want.    
Compatible with any service such as email, discord, telegram and etc. All you need to do is implementing report for it.   
You can easily config and use it in your ASP.Net projects.   
If you like it you can [support this project](#donation)

## Concepts:

#### Request and RequestReporter:

Incoming requests to your web application. they can be reported with `RequestReporters`.    
Also you can access `RequestReporters` with `ServiceProvider.GetServices<IRequestReporter>()`
and report them with `ReportRequest` method.

#### Exception and ExceptionReporter:

Any exception that occurs in request pipeline can be reported with `ExceptionReporters`.   
Pipeline exceptions will be detected even if you use `UseExceptionHandler`.   
For other exceptions that handled by `try-catch` blocks or non-pipeline exceptions, you can access `ExceptionReporter` with `ServiceProvider.GetServices<IExceptionReporter>()`
and report them with `ReportException` method.

#### Data and DataReporter:

If you want to report any other data or log that is important for you, you can use `DataReporter`.You can access `DataReporter` with `ServiceProvider.GetServices<IDataReporter>()`
and report them with `ReportData` method.

#### Services that can be reported by:

You can use any service for reporting.   
Use [this list](#implemented-reporters) to check if the service has been implemented.   
If the service you want to use has not been implemented, you can implement it with [this how-to](#how-to-implement-new-reporters) and let me know to update [this list](#implemented-reporters).

#### WatchdogPrefix:

Prefix of routes that ReportSharp should report them with `RequestReporter` and `ExceptionReporter`.   
You can use this feature to customize ReportSharp.  
**Note**: if you have direct access to `RequestReporter` or `ExceptionReporters` from `ServiceProvider`, Requests or exceptions will be reported even if they are not match with watchdog prefix

## Dependencies:

Dotnet Core 3.1 or later

## Usage:

#### Dotnet 5 or below:

1) Add following lines to `ConfigureServices` method in `Startup` class:

```c#
services.AddReportSharp(options => {
    options.ConfigReportSharp(configBuilder => 
        configBuilder.SetWatchdogPrefix("/")
        // The url prefix that ReportSharp ExceptionReporters and RequestReporter should report them 
    );
    options.AddRequestReporter(() => new TheRequestReporterThatYouWantToUse());
    options.AddExceptionReporter(() => new TheExceptionReporterThatYouWantToUse());
    options.AddDataReporter(() => new TheDataReporterThatYouWantToUse());
    options.AddReporter<Reporter,TheReporterThatYouWantToUse>(() => new TheReporterThatYouWantToUse());
});
```

##### Note: if you want to it for all reporters, you can use only `AddReporter` method.

2) Add following lines to `Configure` method in `Startup` class:

```c#
app.UseReportSharp(configure => {
    configure.UseReportSharpMiddleware<ReportSharpMiddleware>();
});
```

### Dotnet 6 or later:

1) Add following lines to `services` section, before `builder.Build()` line:

```c#
services.AddReportSharp(options => {
    options.ConfigReportSharp(configBuilder => 
        configBuilder.SetWatchdogPrefix("/")
        // The url prefix that ReportSharp ExceptionReporters and RequestReporter should report them 
    );
    options.AddRequestReporter(() => new TheRequestReporterThatYouWantToUse());
    options.AddExceptionReporter(() => new TheExceptionReporterThatYouWantToUse());
    options.AddDataReporter(() => new TheDataReporterThatYouWantToUse());
    options.AddReporter<Reporter,TheReporterThatYouWantToUse>(() => new TheReporterThatYouWantToUse());
});
```

##### Note: if you want to it for all reporters, you can use only `AddReporter` method.

2) Add following lines to `Configure` section, after `builder.Build()` line:

```c#
app.UseReportSharp(configure => {
    configure.UseReportSharpMiddleware<ReportSharpMiddleware>();
});
```

## Implemented reporters:

[ReportSharp.DatabaseReporter](https://www.nuget.org/packages/ReportSharp.DatabaseReporter/) for database.   
[ReportSharp.DiscordReporter](https://www.nuget.org/packages/ReportSharp.DiscordReporter/) for discord.  
[ReportSharp.Api](https://www.nuget.org/packages/ReportSharp.Api/) implements apis for [ReportSharp.DatabaseReporter](https://www.nuget.org/packages/ReportSharp.DatabaseReporter/).

#### Note: If you have implemented new a reporter, you can let me know with a new issue for this repository.

## How to implement new reporters:

#### To create a new reporter to Test service you need to do following step:

##### **1.** Create `TestReporter` class.

##### **2.** Implement one or more of following interfaces in `TestReporter`.

##### **2.1.** For **RequestReporters** implement `IRequestReporter` interface in `TestReporter` class.

##### **2.2.** Implement how `TestReporter` reports requests in `ReportRequest` method.

##### **3.1.** For **ExceptionReporters** implement `IExceptionReporter` interface in `TestReporter` class.

##### **3.2.** Implement how `TestReporter` reports exceptions in `ReportException` method.

##### **4.1.** For **DataReporters** implement `IDataReporter` interface in `TestReporter` class.

##### **4.2.** Implement how `TestReporter` reports data in `ReportData` method.

##### **5.** Create `TestReportOptionsBuilder` class.

##### **6.** Implement one or more of following interfaces in `TestReportOptionsBuilder`.

##### **6.1.** If `IRequestReporter` is implemented in `TestReporter`, Implement `IRequestReporterOptionsBuilder<TestReporter>`.

##### **6.2.** If `IExceptionReporter` is implemented in `TestReporter`, Implement `IExceptionReporterOptionsBuilder<TestReporter>`.

##### **6.3.** If `IDataReporter` is implemented in `TestReporter`, Implement `IDataReporterOptionsBuilder<TestReporter>`.

##### **7.** Add `TestService` implementation to `serviceCollection` inside `Build` method.

##### **8.** Add `TestReporter` to `ReportSharp` as a reporter.

##### **8.1.** If `IRequestReporter` is implemented in `TestReporter`, use `reportSharpOptionsBuilder.AddRequestReporter(() => new TestReportOptionsBuilder())`

##### **8.2.** If `IExceptionReporter` is implemented in `TestReporter`, use `reportSharpOptionsBuilder.AddExceptionReporter(() => new TestReportOptionsBuilder())`

##### **8.3.** If `IDataReporter` is implemented in `TestReporter`, use `reportSharpOptionsBuilder.AddDataReporter(() => new TestReportOptionsBuilder())`

##### **8.4.** If `IRequestReporter`, `IExceptionReporter` and `IDataReporter` is implemented, you can use `reportSharpOptionsBuilder.AddReporter<TestReporter, TestReportOptionsBuilder>(() => new TestReportOptionsBuilder)`.

## Donation:

#### If you like it, you can support me with `USDT`:

1) `TJ57yPBVwwK8rjWDxogkGJH1nF3TGPVq98` for `USDT TRC20`
2) `0x743379201B80dA1CB680aC08F54b058Ac01346F1` for `USDT ERC20`
