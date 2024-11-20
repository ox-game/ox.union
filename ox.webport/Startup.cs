using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Components.Authorization;
using OX.Wallets;
using Microsoft.AspNetCore.Mvc.Razor;

namespace OX.WebPort
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
            //services.AddRazorPages();
            //services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); ;
            services.AddHttpContextAccessor();
          
            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(sp.GetService<NavigationManager>().BaseUri)
            });
            services.AddControllers();           
            //services.Configure<RazorViewEngineOptions>(o => {
            //    o.ViewLocationExpanders.Add(new CustomViewLocationExpander());
            //});
            services.AddResponseCaching();
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
            app.UseResponseCaching();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseOXAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(name: "Default", url: "api/{ApiBoxName}/{ApiModuleName}/{ApiActionName}/{arg=}", defaults: new { controller = "PrintCard", action = "DownJobCardRar", arg = UrlParameter.Optional });
                //endpoints.MapBlazorHub();
                //endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
