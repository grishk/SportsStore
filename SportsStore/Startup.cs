using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Data;

namespace SportsStore
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config) => Configuration = config;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddMvc();
            services.AddAutofac();
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDistributedSqlServerCache(options => {
                options.ConnectionString = connectionString;
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });
            services.AddSession(options => {
                options.Cookie.Name = "SportsStore.Session";
                options.IdleTimeout = System.TimeSpan.FromHours(48);
                options.Cookie.HttpOnly = false;
            });
            return BootstrapIoC(services);//services.AddSingleton<IProductRepository, DataProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }

        private AutofacServiceProvider BootstrapIoC(IServiceCollection services)
        {
            // Create your builder.
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);



            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly).AsImplementedInterfaces().AssignableTo<IRepository>().Except<IRepository>();
            

            AutofacContainer = builder.Build();

            // However, if you want BOTH services (not as common)
            // you can say so:
            //builder.RegisterType<SomeType>().AsSelf().As<IService>();

            return new AutofacServiceProvider(AutofacContainer);
        }
    }
}
