using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
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
            
            services.AddScoped<IRepository<Contact>, ContactRepository>();
            services.AddScoped<IRepository<Info>, InfoRepository>();
            services.AddScoped<IRepository<InfoType>, InfoTypeRepository>();
            services.AddScoped<IRepository<ReportRequest>, ReportRequestRepository>();
            services.AddScoped<IRepository<ReportState>, ReportStateRepository>();
            
            services.AddDbContext<ContactDbContext>( builder =>
            {
                if (builder == null) throw new ArgumentNullException(nameof(builder));
                builder.UseNpgsql(Configuration.GetConnectionString("contactDb"),npgsqlOptionsAction: optionsBuilder =>
                {
                    optionsBuilder.EnableRetryOnFailure(
                        10,
                        TimeSpan.FromSeconds(30),
                        null);
                } );
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
            
            app.UseSerilogRequestLogging();

            app.UseODataBatching();

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
            var infosEntitySet= builder.EntitySet<Info>("Infos");
            infosEntitySet.EntityType.HasKey(e => new {e.ContactId,e.InfoTypeId});

            //InfoType
            //EntitySet
            var infoTypesEntitySetSet= builder.EntitySet<InfoType>("InfoTypes");
            infoTypesEntitySetSet.EntityType.HasKey(e => e.Id);

            //ReportRequest
            //EntitySet
            var reportRequestsEntitySet= builder.EntitySet<ReportRequest>("ReportRequests");
            reportRequestsEntitySet.EntityType.HasKey(e => e.Id);
            
            //ReportState
            //EntitySet
            var reportStatesEntitySet= builder.EntitySet<ReportState>("ReportStates");
            reportStatesEntitySet.EntityType.HasKey(e => e.Id);

            //Report
            //EntitySet
            var reportsEntitySet= builder.EntitySet<Report>("Reports");
            reportStatesEntitySet.EntityType.HasKey(e => e.Id);
            
            return builder.GetEdmModel();
        }
    }
}
