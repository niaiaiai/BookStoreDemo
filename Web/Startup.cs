using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyAspNetCore.Extensions;
using MyCore.DependencyInjection;
using System;
using System.IO;
using System.Security.Cryptography;

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
            services.Create();

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

            string keyPublic = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "rsa_public_key.pem"));
            //RSAParameters keyParameters = JsonSerializer.Deserialize<RSAParameters>(keyPublic);
            //var rsaKey = new RsaSecurityKey(keyParameters);
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(keyPublic.ToCharArray());

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
                    options.TokenValidationParameters.IssuerSigningKey = new RsaSecurityKey(rsa);
                });

            //services.AddSingleton<IHealthCheckPublisher, BookStoreContextHealthCheckPublisher>();
            services.AddHealthChecks().AddDbContextCheck<BookStoreContext>();
            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Predicate = ch => ch.FailureStatus == HealthStatus.Unhealthy;
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

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                AllowCachingResponses = false
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
