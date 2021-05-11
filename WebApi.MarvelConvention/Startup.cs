using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService;
using ApplicationService.Interfaces;
using Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.DbEntities;
using WebApi.MarvelConvention.Dto;

namespace WebApi.MarvelConvention
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
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IConventionService, ConventionService>();
            services.AddScoped<IConventionsRepository, ConventionsRepository>();
            services.AddScoped<IUserManagementRepository, UserManagementRepository>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddHttpContextAccessor();

            services.AddDbContext<MarvelConventionDbContext>(options => options.UseSqlServer(@"Server=.\sqlexpress;Database=MarvelConvention;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
                .AddJwtBearer(options =>    
                {    
                    options.RequireHttpsMetadata = false;    
                    options.SaveToken = true;    
                    options.TokenValidationParameters = new TokenValidationParameters    
                    {    
                        ValidateIssuer = true,    
                        ValidateAudience = true,    
                        ValidateLifetime = true,    
                        ValidateIssuerSigningKey = true,    
                        ValidIssuer = Configuration["Jwt:Issuer"],    
                        ValidAudience = Configuration["Jwt:Audience"],    
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),    
                        ClockSkew = TimeSpan.Zero    
                    };    
                    services.AddCors();    
                });    
    
            services.AddAuthorization(config =>    
            {    
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());    
                config.AddPolicy(Policies.Participant, Policies.ParticipantPolicy());    
            });    

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
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
