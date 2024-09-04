using Azure.Identity;
using Microsoft.Extensions.Azure;
using ServiceBus.Application.Extensions;
using ServiceBus.Application.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureClients(o =>
{
    o.AddServiceBusClient(builder.Configuration.GetSection("ServiceBusConfiguration"))
     .WithCredential(new DefaultAzureCredential(new DefaultAzureCredentialOptions()
     {
         TenantId = "53e27a50-b5e0-4516-aeb3-194e0f31d7a4",
         ExcludeAzureCliCredential = true,
         ExcludeAzureDeveloperCliCredential = true,
         ExcludeAzurePowerShellCredential = true,
         ExcludeEnvironmentCredential = true,
         ExcludeInteractiveBrowserCredential = true,
         ExcludeSharedTokenCacheCredential = true,
         ExcludeVisualStudioCodeCredential = true,
         ExcludeWorkloadIdentityCredential = true,
     }));
});
builder.Services.AddApplicationServices();

builder.Services.AddOptions<ServiceBusConfiguration>()
                .Bind(builder.Configuration.GetSection("ServiceBusConfiguration"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
