using API;

var builder = WebApplication.CreateBuilder(args);
builder.AddDIServices();

var app = builder.Build();
app.AddMiddleware();
app.Run();