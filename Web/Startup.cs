using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyAspNetCore.Extensions;
using MyCore.DependencyInjection;
using System;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjectionBase dependencyInjectionBase = new(services);

            //ContainerBuilder containerBuilder = new();
            //containerBuilder.RegisterType<GetBookValidationInterceptor>();
            //containerBuilder.RegisterType<Class1>().As<Interface1>().InterceptedBy(typeof(GetBookValidationInterceptor)).EnableInterfaceInterceptors();
            //var container = containerBuilder.Build();
            string[] origins = Configuration.GetValue<string>("Cors:Origins").Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.WithOrigins(origins)
                            .WithHeaders("x-requested-with", "content-type", "authorization")
                            .AllowAnyMethod()
                            .AllowCredentials();
                });
            });

            var authorizationSettings = Configuration.GetSection("Authorization");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = authorizationSettings.GetValue<string>("Authority");
                    options.RequireHttpsMetadata = authorizationSettings.GetValue<bool>("RequireHttpsMetadata");
                    options.MetadataAddress = $"{authorizationSettings.GetValue<string>("Authority")}/.well-known/openid-configuration";
                    options.TokenValidationParameters.ValidIssuer = authorizationSettings.GetValue<string>("Authority");
                    options.TokenValidationParameters.ValidAudience = authorizationSettings.GetValue<string>("Audience");
                    options.TokenValidationParameters.RequireExpirationTime = true;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);
                });

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
            
            app.UseHttpsRedirection();
            app.UseDataInit();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
