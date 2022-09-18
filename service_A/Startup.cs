using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Dapper;
using service_A.Models;
using service_A.Models.SubDivision;
using service_A.Logic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace service_A
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

            //services.AddCors(options => options.AddPolicy("AllowLocalhost44321", builder => builder
            //        .WithOrigins("https://www.gg.com")
            //        .AllowAnyHeader()
            //        .AllowAnyMethod())
            //   );

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //       .AddJwtBearer(options =>
            //       {
            //           options.RequireHttpsMetadata = false;
            //           options.TokenValidationParameters = new TokenValidationParameters
            //           {
            //                   // укзывает, будет ли валидироваться издатель при валидации токена
            //                   ValidateIssuer = false,

            //                   // будет ли валидироваться потребитель токена
            //                   ValidateAudience = false,


            //                   // установка ключа безопасности
            //                   IssuerSigningKey = AuthOp.GetSymmetricSecurityKey(),
            //                   // валидация ключа безопасности
            //                   ValidateIssuerSigningKey = true,
            //           };
            //       });
            services.AddControllers();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<SubDivisionRepository>();
            builder.RegisterType<ALogic>();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            builder.Register(context => new SqlDbConnectionFactory(connectionString)).As<IDbConnectionFactory>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         
            app.UseHttpsRedirection();
   
            app.UseRouting();
            app.UseCors("AllowLocalhost44321");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
