using DAL;
using DAL.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CRUD_WebApp_SQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/jcm.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddDbContext<JcmContext>(options =>
            {
                var cxnStr = builder.Configuration.GetConnectionString("JcmOrdersDBConnectionString");
                options.UseSqlServer(cxnStr, sqlOptions => 
                sqlOptions.MigrationsAssembly(typeof(JcmContext).Assembly.GetName().Name)
                    .EnableRetryOnFailure(1));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(AutomapperProfiles));

            builder.Services.AddScoped<IJCM_Repo, JCM_Repo>(); 

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

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
        }
    }
}


/* Todo
 * 
 *  Bulk Insert
 *  100K line test file
 *  implement Validations w/ Rules Engine - see https://github.com/microsoft/RulesEngine
 * 
 * 
 */