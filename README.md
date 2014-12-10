Seq.Client.Portable
===================

A portable (WP/iOS/Android) sink for Serilog that writes events over HTTP/S to Seq. In time it's hoped that this will feed back into the "official" Seq client that ships as _Serilog.Sinks.Seq_.

The primary goal of this package is to provide great development-time logging, although there's nothing technically preventing this from being used on deployed devices.

Usage
-----

Install from [NuGet](https://nuget.org/packages/seq.client.portable):

```powershell
Install-Package Seq.Client.Portable
```

Configure:

```csharp
Log.Logger = new LoggerConfiguration()
  .WriteTo.SeqPortable("http://169.254.80.80:5341")
  .CreateLogger();
```

Log:

```csharp
Log.Information("Hello Seq!");
```

Development-time Logging for Mobile Apps
----------------------------------------

Most mobile device emulators use a fixed IP address to identify the host machine that the emulator is running on.

Here's one possible configuration:

```csharp
var serverUrl = Device.OnPlatform(
  WinPhone: "http://169.254.80.80:5341",
  Android: "http://10.0.2.2:5341",
  iOS: "http://my-seq.example.com");
```

Known Issues
------------

Serilog's still has some known issues on iOS, namely, `Nullable<T>` properties can't currently be represented, and dictionary values are not able to be serialized. There's an [open issue covering this](https://github.com/serilog/serilog/pull/125).


