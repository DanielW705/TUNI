using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TUNIWEB.ClassValidation;
using TUNIWEB.Hubs;
using TUNIWEB.Models;

namespace TUNIWEB
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Log/nvoLogIn";
                    options.Cookie.Name = "validado";
                });
            services.AddDbContext<BB>(options =>
             {
                 options.UseSqlServer(Configuration.GetConnectionString("BB"));
             });
            services.Configure<ReCAPTCHASSettings>(Configuration.GetSection("GooglereCAPTCHA"));
            services.AddTransient<GooglereCaptchaService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            /*  services.AddIdentity<UsuarioAlumno, IdentityRole>(options =>
              {
                  options.Password.RequiredLength = 10;
                  options.Password.RequiredUniqueChars = 3;
                  options.Password.RequireLowercase = true;
                  options.Password.RequireDigit = true;
                  options.Password.RequireNonAlphanumeric = false;
              }).AddEntityFrameworkStores<BB>();*/
            services.AddSignalR();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BB bd)
        {
            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                Secure = CookieSecurePolicy.None
            });
            app.UseAuthentication();
            app.UseSignalR(x => x.MapHub<ChatHubs>("/chatHub"));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Log}/{action=nvoLogIn}/{id?}");
            });
            Operaciones.inicializar(bd);
        }
    }
}
