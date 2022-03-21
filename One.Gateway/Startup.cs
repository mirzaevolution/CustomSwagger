using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;

namespace One.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication()
                   .AddJwtBearer("Bearer", options =>
                   {
                       options.Authority = Configuration["IdentityServer:BaseAddress"];
                       options.Audience = Configuration["IdentityServer:Audience"];
                       options.TokenValidationParameters.NameClaimType = "name";
                       options.TokenValidationParameters.RoleClaimType = "role";
                   });

            services.AddSwaggerForOcelot(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
                opt.OAuthAppName(Configuration.GetValue<string>("One.Gateway"));
                opt.OAuthClientId(Configuration.GetValue<string>("IdentityServer:ClientId"));
                opt.OAuthClientSecret(Configuration.GetValue<string>("IdentityServer:ClientSecret"));
                opt.OAuthUsePkce();
            }).UseOcelot()
             .Wait();
        }
    }
}
