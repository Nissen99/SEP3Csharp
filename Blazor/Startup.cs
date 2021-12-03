using Blazor.Authentication;
using Blazor.Model.AlbumModel;
using Blazor.Model.ArtistModel;
using Blazor.Model.AudioTestModel;
using Blazor.Model.PlaylistManageModel;
using Blazor.Model.PlaylistModel;
using Blazor.Model.PlayModel;
using Blazor.Model.SongManagerModel;
using Blazor.Model.SongSearchModel;
using Blazor.Model.UserModel;
using Blazor.Util;
using Blazored.Modal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocketsT1_T2.Tier1;
using Syncfusion.Blazor;

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
            services.AddScoped<IArtistModel, ArtistModel>();
            services.AddScoped<IAlbumModel, AlbumModel>();
            services.AddScoped<ISongManageModel, SongManageModel>();
            services.AddScoped<IPlayListModel, PlayListModel>();
            services.AddScoped<IPlaylistManageModel, PlaylistManageModel>();
            


            services.AddBlazoredModal();
            services.AddSyncfusionBlazor();
            services.AddScoped<IUserModel, UserModel>();
            
            
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeLoggedIn", a =>
                    a.RequireAuthenticatedUser().RequireClaim("Role", "StandardUser", "Admin"));
                options.AddPolicy("MustBeAdmin",  a => 
                    a.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));
                
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