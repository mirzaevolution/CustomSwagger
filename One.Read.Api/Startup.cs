using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

namespace One.Read.Api
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "One.Read.Api", Version = "v1" });
                c.AddSecurityDefinition("OneApi.Oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{Configuration["IdentityServer:BaseAddress"]}/connect/authorize"),
                            TokenUrl = new Uri($"{Configuration["IdentityServer:BaseAddress"]}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "One.Read","Read Api" }
                            }
                        }
                    }
                });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);


                c.IncludeXmlComments(xmlFilePath);
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            services.AddCors(c =>
            {
                c.AddPolicy("Cors", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                });
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   options.Authority = Configuration["IdentityServer:BaseAddress"];
                   options.Audience = Configuration["IdentityServer:Audience"];
                   options.TokenValidationParameters.NameClaimType = "name";
                   options.TokenValidationParameters.RoleClaimType = "role";
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

            app.UseRouting();
            app.UseCors("Cors");
            app.UseSwagger(config =>
            {
                config.RouteTemplate = "/OneApi/Read/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/OneApi/Read/swagger/v1/swagger.json", "One.Read.Api");
                c.RoutePrefix = "OneApi/Read/swagger";
                c.OAuthClientId(Configuration["IdentityServer:ClientId"]);
                c.OAuthClientSecret(Configuration["IdentityServer:ClientSecret"]);
                c.OAuthAppName("One.Read.Api v1");
                c.OAuthUsePkce();

            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("Cors");
            });
        }
    }
}
