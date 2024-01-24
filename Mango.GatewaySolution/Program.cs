using Mango.GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthentication();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange:true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();


app.MapGet("/", () => "Hello World!");
var configuration = new OcelotPipelineConfiguration
{
    AuthenticationMiddleware = async (cpt, est) =>
    {
        await est.Invoke();
    }
};

app.UseOcelot(configuration).GetAwaiter().GetResult();


app.Run();
