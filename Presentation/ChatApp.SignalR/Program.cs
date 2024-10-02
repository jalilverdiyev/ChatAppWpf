using System.Text.Json.Serialization;
using ChatApp.Persistence;
using ChatApp.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterPersistanceServices();
builder.Services.AddSignalR().AddJsonProtocol(o =>
{
	o.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();


app.MapHub<ChatHub>("/chat");

app.Run();
