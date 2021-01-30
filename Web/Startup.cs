using Application;
using AutoMapper;
using Domain;
using IdentityModel;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyRepositories;
using MyRepositories.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            //ContainerBuilder containerBuilder = new();
            //containerBuilder.RegisterType<GetBookValidationInterceptor>();
            //containerBuilder.RegisterType<Class1>().As<Interface1>().InterceptedBy(typeof(GetBookValidationInterceptor)).EnableInterfaceInterceptors();
            //var container = containerBuilder.Build();

            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.WithOrigins("http://localhost:8080", "http://localhost:8081")
                            .WithHeaders("x-requested-with", "content-type", "authorization")
                            .AllowAnyMethod()
                            .AllowCredentials();
                });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContextPool<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BookStoreConnection")));

            var authorizationSettings = Configuration.GetSection("Authorization");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = authorizationSettings.GetValue<string>("Authority");
                    options.RequireHttpsMetadata = false; //生产环境注释掉这个
                    options.TokenValidationParameters.ValidIssuer = authorizationSettings.GetValue<string>("Authority");
                    options.TokenValidationParameters.ValidAudience = authorizationSettings.GetValue<string>("Audience");
                    options.TokenValidationParameters.RequireExpirationTime = true;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);
                });

            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(BookStoreRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(BookStoreRepository<>));
            services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(BookStoreRepository<,>));
            services.AddScoped(typeof(IRepository<,>), typeof(BookStoreRepository<,>));

            services.AddUnitOfWork<BookStoreContext>();
            services.AddDomainServices().AddApplicationServices();

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
            }
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();

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
