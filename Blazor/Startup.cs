using System.Security.Claims;
using Blazor.Authentication;
using Blazor.Model;
using Blazor.Util;
using Blazored.Modal;
using Domain.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocketsT1_T2.Tier1;

namespace Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IJSRuntime, JSRuntime>();
            services.AddRazorPages();
            services.AddServerSideBlazor();

           services.AddScoped<IClient,Client>();
            
            services.AddScoped<IAudioTestModel,AudioTestModel>();
            services.AddScoped<IPlayModel, PlayModel>();
            services.AddScoped<CircuitHandler, CircuitHandlerService>();
            services.AddScoped<ISongSearchModel, SongSearchModel>();
            services.AddBlazoredModal();
            services.AddScoped<IUserModel, UserModel>();
            
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddAuthorization(options => {
                options.AddPolicy("MustBeAdmin",  a => 
                    a.RequireAuthenticatedUser().RequireClaim("Domain", "viaAdmin.dk"));
         
                options.AddPolicy("MustBeStudent",  a => 
                    a.RequireAuthenticatedUser().RequireClaim("Domain", "viaStudent.dk"));
            
                options.AddPolicy("SecurityLevel4",  a => 
                    a.RequireAuthenticatedUser().RequireClaim("Level", "4","5"));
            
                options.AddPolicy("MustBeTeacher",  a => 
                    a.RequireAuthenticatedUser().RequireClaim("Role", "Teacher"));
            
                options.AddPolicy("SecurityLevel2", policy =>
                    policy.RequireAuthenticatedUser().RequireAssertion(context => {
                        Claim levelClaim = context.User.FindFirst(claim => claim.Type.Equals("Level"));
                        if (levelClaim == null) return false;
                        return int.Parse(levelClaim.Value) >= 2;
                    }));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}