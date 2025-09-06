using Infrastructure;
using Presentation.Endpoints;
using Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
          .WithOrigins("http://localhost:4242")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
    });
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "Open API v1");
    });
}

app.UseCors("AllowAngular");
await app.UseMigrationsAndSeedAsync();
app.MapEndpoints();
app.MapHub<DiscussionHub>("/hubs/discussion");
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.Run();
