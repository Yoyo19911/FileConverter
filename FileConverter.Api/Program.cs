using FileConverter.Infrastructure.Services;
using FileConverter.Application.Services;

#region Configure application
var builder = WebApplication.CreateBuilder(args);

var anyOriginCorsPolicy = "anyOriginCorsPolicy";
builder.Services.AddCors(options => options.AddPolicy(anyOriginCorsPolicy, policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

//add custom services
builder.Services.AddScoped<IHtmlToPdfConverter, HtmlToPdfConverter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(anyOriginCorsPolicy);
#endregion Configure application


//handle all exceptions and redirect to simple error page
app.UseExceptionHandler("/error");
app.MapGet("/error", () => Results.Problem());

//Method covnerts a html file body to a pdf file body and returns it as array of bytes
//Note: The pdf file body will be represented as a base64 string on the frontend side and needs to be converted before being used
app.MapPost("/html-to-pdf", (byte[] htmlFileBody, IHtmlToPdfConverter converter) 
    => converter.ConvertAsync(htmlFileBody));


app.Run();