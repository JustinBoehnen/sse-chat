using SseChat.ApiService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:7149")  // Blazor client URL
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMessageService, MessageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.UseCors("AllowBlazorClient");
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
