using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TariffComparison;
using TariffComparison.DbModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<ProductContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    var dbPath = Path.Join(path, "products.db");

    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.UseExceptionHandler(appError =>
{
    appError.Run(context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            logger.LogError($"Something went wrong: {contextFeature.Error}");
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return Task.CompletedTask;
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<ProductContext>())
{
    context.Database.EnsureCreated();
}

app.Run();
