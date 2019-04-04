using Autofac;
using Autofac.Extensions.DependencyInjection;
using LifeLike.CloudService;
using LifeLike.ElasticSearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PGSUpskill.Extensions;
using System;

namespace PGSUpskill
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get;  set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerSetting();
            services.SetupBackground();
            services.AddMvc().AddControllersAsServices();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new CloudModule());
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new ElasticSearchModule());
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwaggerSetting();
            var option = new RewriteOptions().AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
