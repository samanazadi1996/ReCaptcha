using Sam.ReCaptcha.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddReCaptcha();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/", async (HttpContext httpContext) =>
{
    var service = httpContext.RequestServices.GetRequiredService<IImageGenerator>();

    byte[] imageBytes = service.GetBytes("");

    httpContext.Response.ContentType = "image/png";

    await httpContext.Response.Body.WriteAsync(imageBytes, 0, imageBytes.Length);
});
app.UseReCaptcha();
app.MapControllers();

app.Run();
