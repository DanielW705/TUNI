using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TUNIWEB.ClassValidation;
using TUNIWEB.Hubs;
using TUNIWEB.Models;
using TUNIWEB.Models.UserCase;
using TUNIWEB.Models.UsersCase;
using TUNIWEB.Utilities;

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
            string cadenaDeConexion = Configuration.GetConnectionString("TUNIConnectionString") ?? throw new InvalidOperationException("No existe una manera de conectarse a la base de datos");
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


            

            services.AddDbContext<TUNIDbContext>(options => options.UseSqlServer(cadenaDeConexion));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //Agregamos inyeccion de dependencias para las cookies
            services.AddScoped<SignInCookieUseCase>();
            //Agregamos inyeccion de dependencia para asegurar la uthorizacion del usuario
            services.AddScoped<UserAuthenticationCase>();
            //Agregamos inyeccion de dependencia para asegurar que las contraseñas no se repitan
            services.AddScoped<PasswordIsInUseCase>();
            //Agregamos inyeccion de dependencia para login de usuario
            services.AddScoped<LoginUserCase>();
            //Agregamos inyeccion de dependencia para agregar un usuario
            services.AddScoped<AddUserCase>();
            //Agregamos inyeccion de dependencia para agregar los datos obligatorios del Alumno
            services.AddScoped<AddDatosObligatoriosUserCase>();
            //Agregamos inyeccion de dependencia para ver el perfil del alumno
            services.AddScoped<GetUserProfileCase>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TUNIDbContext bd)
        {
            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            CreateTemporalFiles._hostingEnvironment = env;
            using(IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                TUNIDbContext context = scope.ServiceProvider.GetRequiredService<TUNIDbContext>();
                context.Database.Migrate();
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
                    template: "{controller=Log}/{action=LogIn}/{id?}");
            });
            Operaciones.inicializar(bd);
        }
    }
}
