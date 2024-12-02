using Microsoft.AspNetCore.Server.Kestrel.Core;
using TaskManager.Dal.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5213, o => o.Protocols = HttpProtocols.Http2);
});
var app = builder.Build();

app.MigrateUp();
app.Run();