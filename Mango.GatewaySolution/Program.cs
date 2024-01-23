using Mango.GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthentication();

builder.Services.AddOcelot();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");
var configuration = new OcelotPipelineConfiguration
{
    AuthenticationMiddleware = async (cpt, est) =>
    {
        await est.Invoke();
    }
};

app.UseOcelot(configuration).Wait();


app.Run();
