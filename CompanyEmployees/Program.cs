using CompanyEmployees.ActionFilters;
using CompanyEmployees.Controllers;
using CompanyEmployees.Extensions;
using Contracts;
using Entities.DataTransferObjects;
using FluentAssertions.Common;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.AddAuthorization();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true;
})
 .AddXmlDataContractSerializerFormatters();


builder.Services.AddControllers(conf =>
{
    conf.RespectBrowserAcceptHeader = true;
    conf.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
  .AddXmlDataContractSerializerFormatters()
  .AddCustomCSVFormatter();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    builder.Services.AddScoped<ValidationFilterAttribute>();
    builder.Services.AddScoped<ValidateCompanyExistsAttribute>();
    builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
});

var nlogPath = Directory.GetCurrentDirectory() + "\\nlog.config";
LogManager.Setup().LoadConfigurationFromFile(nlogPath);

var app = builder.Build();

app.UseStaticFiles();
app.UseCors("Cors Policy");
app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();