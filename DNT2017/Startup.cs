using System;
using DNT2017.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DNT2017
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
            services.AddTransient<ITransientService, TransientService>()
                .AddScoped<IScopedService, ScopedService>()
                .AddSingleton<ISingletonService, SingletonService>()
                .AddSingleton<ISingletomInstanceService>(new SingletonService(Guid.Empty))
                .AddTransient<IBigService, BigService>();

            services.AddMvc();

            services.Configure<EmailMessageSettings>(Configuration.GetSection("EmailMessageSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Contact}/{action=Index}/{id?}");
            });
        }
    }
}
