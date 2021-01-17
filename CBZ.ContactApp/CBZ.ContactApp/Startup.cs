using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter.Deserialization;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Collections.Generic;
using System.Linq;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using Serilog;

namespace CBZ.ContactApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContactDbContext>( builder =>
            {
                if (builder == null) throw new ArgumentNullException(nameof(builder));
                builder.UseNpgsql(Configuration.GetConnectionString("contactDb"));
            });
            services.AddControllers();
            services.AddOData(opt =>
            {
                opt.AddModel("v1", GetEdmModel()).Select().Filter().Expand().Count().OrderBy();
                opt.EnableAttributeRouting = true;
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }
            app.UseODataBatching();
            
            app.UseSerilogRequestLogging();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                if (endpoints == null) throw new ArgumentNullException(nameof(endpoints));
                endpoints.MapControllers();
            });
            
        }
        
        private IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "Ext";
            
            //Contact
            //EntitySet
            var contactsEntitySetSet= builder.EntitySet<Contact>("Contacts");
            contactsEntitySetSet.EntityType.HasKey(e => e.Id);
            //EntityType
            var contactsEntityType= builder.EntityType<Contact>();
            //Functions
            var getContactsByNameSurname = contactsEntityType.Collection.Function("ByNameSurname");
            getContactsByNameSurname.Parameter<string>("name");
            getContactsByNameSurname.Parameter<string>("surname");
            getContactsByNameSurname.ReturnsFromEntitySet<Contact>("Contacts");
            
            //Info
            //EntitySet
            var infosEntitySetSet= builder.EntitySet<Info>("Infos");
            infosEntitySetSet.EntityType.HasKey(e => e.ContactId);
            infosEntitySetSet.EntityType.HasKey(e => e.InfoTypeId);
            
            //InfoType
            //EntitySet
            var infoTypesEntitySetSet= builder.EntitySet<InfoType>("InfoTypes");
            infoTypesEntitySetSet.EntityType.HasKey(e => e.Id);

            //ReportRequest
            //EntitySet
            var reportRequestsEntitySet= builder.EntitySet<ReportRequest>("ReportRequest");
            reportRequestsEntitySet.EntityType.HasKey(e => e.Id);

            // builder.EntitySet<InfoType>("InfoTypes");
            // builder.EntitySet<ReportRequest>("ReportRequests");
            // builder.EntitySet<ReportState>("ReportState");
            return builder.GetEdmModel();
        }
    }
}
