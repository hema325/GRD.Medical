using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);


//builder.WebHost.UseUrls("https://192.168.1.104:7295");  //test from other devices

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(b =>
    {
        b.AllowAnyOrigin();
        b.AllowAnyMethod();
        b.AllowAnyHeader();
    });
});
builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseInfrastructure();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

await app.Services.InitialiseDBAsync();

app.Run();
