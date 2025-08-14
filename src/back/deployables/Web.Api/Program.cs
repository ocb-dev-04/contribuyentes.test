using Global.Sources.Constants;
using Presentation.Midllewares;
using Scalar.AspNetCore;
using Web.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceCollection(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.CheckMigrations();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

#if DEBUG
app.UseCors(GeneralConstants.FrontEndPortCorsName);
#endif

app.MapControllers();

await app.SeedData();

app.Run();
