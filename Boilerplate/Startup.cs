using System;
using System.Text;
using Boilerplate.Models;
using Boilerplate.SQLite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

// mysql://b25203c37bb5a4:f45e5c0a@us-cdbr-iron-east-05.cleardb.net/heroku_966c0696892a23d?reconnect=true

//     "IdentityConnection": "Data source=us-cdbr-iron-east-02.cleardb.net;database=heroku_4814ee10c387aab;user id=b2f72e34023ddb;Password=3ccdc0b2;",

namespace Boilerplate {
  public class Startup {
    public Startup (IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.

    public void ConfigureServices (IServiceCollection services) {
      services.AddDbContext<DatabaseContext> (options => options.UseMySql (Configuration.GetConnectionString ("IdentityConnection")));
      services.AddDbContext<AuthenticationContext> (options => options.UseMySql (Configuration.GetConnectionString ("IdentityConnection")));
      services.Configure<ApplicationSettings> (Configuration.GetSection ("ApplicationSettings"));
      services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

      services.AddDefaultIdentity<ApplicationUser> ()
        .AddEntityFrameworkStores<AuthenticationContext> ()
        .AddDefaultTokenProviders ();
      services.Configure<IdentityOptions> (options => {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;

      });

      //jwt Authentication

      var key = Encoding.UTF8.GetBytes (Configuration["ApplicationSettings:JWT_Secret"].ToString ());;
      services.AddAuthentication (x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer (x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey (key),
          ValidateIssuer = false,
          ValidateAudience = false,
          ClockSkew = TimeSpan.Zero
        };
      });

      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles (configuration => {
        configuration.RootPath = "ClientApp/dist";
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

      app.Use (async (ctx, next) => {
        await next ();
        if (ctx.Response.StatusCode == 204) {
          ctx.Response.ContentLength = 0;
        }
      });
      if (env.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
      } else {
        app.UseExceptionHandler ("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts ();
      }

      app.UseCors (builder =>
        builder.WithOrigins (
          "https://localhost:5001")
        .AllowAnyHeader ()
        .AllowAnyMethod ());
      app.UseAuthentication ();
      app.UseHttpsRedirection ();
      app.UseStaticFiles ();
      app.UseSpaStaticFiles ();

      app.UseMvc (
        routes => {
          routes.MapRoute (
            name: "default",
            template: "{controller}/{action=Index}/{id?}");
        });

      app.UseSpa (spa => {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        if (env.IsDevelopment ()) {
          spa.Options.SourcePath = "ClientApp";
          spa.UseAngularCliServer (npmScript: "start");
        }
      });
    }
  }
}
