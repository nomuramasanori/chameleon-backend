using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using Npgsql;

namespace SampleApplication
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
            //for react
            services.AddCors(option => option.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddTransient<IDbConnection>(db =>
            new NpgsqlConnection(Configuration.GetConnectionString("postgres")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // 設定をクラスにバインドできるようにする
            services.Configure<Chameleon.ApplicationManager>(Configuration.GetSection("Chameleon"));
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
                app.UseHsts();
            }

            //for react
            app.UseCors();


            // TODO: 画像のダウンロードに必要だが・・
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute("areas", "{area}/{controller=Api}/{action}/{id?}");
            });
        }
    }
}
