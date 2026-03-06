using NovaCore.Services.Interfaces;
using NovaCore.Services.Implementations;
using NovaCore.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddScoped<IChatGptService, ChatGptService>();
builder.Services.AddScoped<IVoiceService, VoiceService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
