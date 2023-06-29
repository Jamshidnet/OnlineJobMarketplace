using OnlineJobMarketplace.Infrustructure;
using OnlineJobMarketplace.Application;
using OnlineJobMarketplace.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddLazyCache();
        builder.Services.AddControllers();
        builder.Services.AddApplicationService();
        builder.Services.AddInfrastructureService(builder.Configuration);
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
        app.UseCustomMiddleware();
        app.UseHttpsRedirection();
       
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}