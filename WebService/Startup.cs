using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Formatters;
using DataAccessLayer;

namespace WebService
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
            //The RespectBrowserAcceptHeader is set false by default, we want to be true.
            //We also set XMLFormatSerializer, default is only JSON
            services.AddMvc(options => {
                options.RespectBrowserAcceptHeader = true;
            }).AddXmlSerializerFormatters();

            //Add a DataService instance parameters to all Controllers constructors.
            //The DataService provided must be compliant with the IDataService interface.
            services.AddSingleton<IDataService, DataService>();
            //Go To DAL/DataService.cs to understand
            //After understanding the DAL part, you should give a look to WebService/Controllers
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
