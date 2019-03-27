using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeLike.CloudService;
using LifeLike.CloudService.CosmosDB;
using LifeLike.CloudService.MongoDB;
using LifeLike.CloudService.SqlDB;
using LifeLike.CloudService.TableStorage;
using LifeLike.Shared;
using LifeLike.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PGSUpskill.Extensions;
using CosmosUnitOfWork = LifeLike.CloudService.CosmosDB.CosmosUnitOfWork;

namespace PGSUpskill
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
            Config.SetupCloudService(services);


            //services.AddTransient<CloudTable>(provider => {

            //    var storageAccount = CloudStorageAccount.Parse(configuration["BlobStorage"]);
            //    var _tableClient = storageAccount.CreateCloudTableClient();

            //    var _table = _tableClient.GetTableReference("photos");
            //    _table.CreateIfNotExistsAsync().Wait();
            //    return _table;
            //});
            services.AddSwaggerSetting();
            services.AddMvc();

        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
