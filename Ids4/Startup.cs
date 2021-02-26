using Ids4.Data;
using Ids4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyAspNetCore.Extensions;
using MyRepositories.Repositories;

namespace Ids4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string[] origins = Configuration.GetValue<string>("Cors:Origins").Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.WithOrigins(origins)
                            .WithHeaders("x-requested-with", "content-type")
                            .AllowAnyMethod()
                            .AllowCredentials();
                });
            });

            services.AddDbContextPool<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Ids4Connection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddIdentityServer()
                .AddConfigurationStore<Ids4ConfigurationDbContext>(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("Ids4Connection"));
                })
                .AddOperationalStore<Ids4PersistedGrantDbContext>(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("Ids4Connection"));
                })
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential();

            services.AddScoped<IDataSeed, Ids4DataSeed<Ids4ConfigurationDbContext, Ids4PersistedGrantDbContext>>();
            services.AddScoped<IDataSeed, IdentityUserDataSeed<ApplicationContext>>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
                app.UseCors("MyPolicy");
            }
            app.UserIdentityServerOrigin(Configuration.GetValue<string>("IdentityServerOrigin"));
            app.UseHttpsRedirection();

            app.UseDataInit();
            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
