using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.App;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain.App.Identity;
using Domain.Base.App.EF;
using ee.itcollege.kaande.pitsariina.Contracts.DAL.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Helpers;

namespace WebApp
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
            services.Configure<IdentityOptions>(options => {
                
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
                
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                
                // User settings
                options.User.RequireUniqueEmail = true;
            });
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MsSqlConnection")));

            // add a scoped dependency
            services.AddScoped<IUserNameProvider, UserNameProvider>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBLL, AppBLL>();

            
            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false) 
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            
            // makes httpcontext injectable - needed to resolve username in dal layer
            services.AddHttpContextAccessor();

            
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            
            // =============== JWT support ===============
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            /*
            services.Configure<RequestLocalizationOptions>(options =>
            {
                // TODO: Move to configuration
                var supportedCultures = new[]
                {
                    new CultureInfo("en-GB"),
                    new CultureInfo("et-EE"),
                    new CultureInfo("ru-RU"),
                };
                // datetime and currency support
                options.SupportedCultures = supportedCultures;
                // UI translated strings
                options.SupportedUICultures = supportedCultures;
                // if nothing is found, use this
                options.DefaultRequestCulture = new RequestCulture("en-GB","en-GB");
            });
            */
            
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                // options.DefaultApiVersion = new ApiVersion(1, 0);
                // options.AssumeDefaultVersionWhenUnspecified
            });

            services.AddApiVersioning(options => { options.ReportApiVersions = true; });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            UpdateDatabase(app, env, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            var defaultCulture = new CultureInfo("en-GB");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);
            
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseCors("CorsAllowAll");
            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach ( var description in provider.ApiVersionDescriptions )
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant() );
                    }
                } );
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
        private static void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env,
            IConfiguration configuration)
        {
            // give me the scoped services (everyhting created by it will be closed at the end of service scope life).
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();

            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
            var logger = serviceScope.ServiceProvider.GetService<ILogger<Startup>>();
            
            if (configuration["AppDataInitialization:DropDatabase"] == "True")
            {
                Console.WriteLine("DropDatabase");
                DataInitializers.DeleteDatabase(ctx);
            }
            
            if (configuration["AppDataInitialization:MigrateDatabase"] == "True")
            {
                Console.WriteLine("MigrateDatabase");
                DataInitializers.MigrateDatabase(ctx);
            }
            
            if (configuration["AppDataInitialization:SeedIdentity"] == "True")
            {
                Console.WriteLine("SeedIdentity");
                DataInitializers.SeedIdentity(userManager, roleManager);
            }
            
            if (configuration.GetValue<bool>("AppDataInitialization:SeedData"))
            {
                Console.WriteLine("SeedData");
                DataInitializers.SeedData(ctx);
            }
            
            // ctx.Database.Migrate();
        }
    }
}