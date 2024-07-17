using Microsoft.Extensions.Azure;
using Azure.Identity;
using Azure.Storage.Blobs;
using AzureBlobTutApi;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("appsettings.json").Build();
builder.Configuration.AddEnvironmentVariables();

var AzureSetting = builder.Configuration.GetSection("AppSettings:AzureSettings").Get<AzureSettings>();
// Add services to the container.

Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", AzureSetting.AZURE_CLIENT_SECRET);
Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", AzureSetting.AZURE_CLIENT_ID);
Environment.SetEnvironmentVariable("AZURE_TENANT_ID", AzureSetting.AZURE_TENANT_ID);

   Uri blobUri = new Uri(AzureSetting.AZURE_BLOB_URL);
builder.Services.AddAzureClients(builder =>
{
    builder.AddBlobServiceClient(blobUri);
    builder.UseCredential(new DefaultAzureCredential());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
