using OX.Cryptography.ECC;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Net.Http;
using OX.SmartContract;
using OX.Wallets;
using OX.Wallets.NEP6;
using OX.MetaMask;
using AntDesign.ProLayout;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using System.Threading;

namespace OX.Tablet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); ;
            services.AddAntDesign();
            services.AddHttpContextAccessor();
            //services.AddBlazoredLocalStorage();   // local storage
            //services.AddBlazoredLocalStorage(config =>
            //    config.JsonSerializerOptions.WriteIndented = true);  // local storage
            services.AddMetaMaskBlazor();
            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(sp.GetService<NavigationManager>().BaseUri)
            });
            services.Configure<ProSettings>(x =>
            {
                x.Title = "OX";
                x.NavTheme = "realDark";
                x.Layout = "side";
                x.PrimaryColor = "daybreak";
                x.ContentWidth = "Fluid";
                x.HeaderHeight = 64;
            });
            //services.Configure<ProSettings>(Configuration.GetSection("ProSettings"));
            //services.AddSingleton<IStateDispatch, StateDispatcher>();
            //services.Configure<RazorViewEngineOptions>(o => {
            //    o.ViewLocationExpanders.Add(new CustomViewLocationExpander());
            //});
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

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseOXAuthentication();
            //app.UseCheckMobileBrowser();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                //endpoints.MapHub<StateHub>("/statehub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
    public static class WebHostHelper
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseUrls($"http://*");
                   webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                   webBuilder.UseStartup<Startup>();
                   webBuilder.UseKestrel(options =>
                   {
                       options.ListenAnyIP(80);
                       var path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\obw.pfx";
                       bool ok = true;
                       X509Certificate2 cert = default;
                       try
                       {
                           cert = new X509Certificate2(path, "qazqwe");
                       }
                       catch (Exception e)
                       {
                           ok = false;
                       }
                       if (ok)
                       {
                           options.Listen(System.Net.IPAddress.Any, 443, listenOptions =>
                           {
                               listenOptions.UseHttps(cert);
                           });
                       }
                   });

               });
        public static void StartWeb(CancellationToken token)
        {
            CreateHostBuilder(null).Build().RunAsync(token);
        }
    }
}
