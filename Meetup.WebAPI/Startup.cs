using Meetups.Aplication;
using Meetups.Aplication.Common.Mapping;
using Meetups.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Meetup.WebAPI
{
    //configuration of the app
    public class Startup
    {
        public IConfiguration Configuration { get; }//?

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(configure =>
            {
                configure.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));// info about current assembly
                configure.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
            });
            services.AddApplication();
            services.AddPersistance(Configuration);// get configuration through constructor and pass to the mrthod
            services.AddControllers();
            services.AddCors(options => 
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // we indicate here what the application will use (the order matters)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(configuration =>
            {
                configuration.RoutePrefix = string.Empty;
                configuration.SwaggerEndpoint("swagger/v1/swagger.json", "Meetups API"); //default adress
            });
            app.UseRouting();
            app.UseHttpsRedirection();// change http to https
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();// �������� ������ ����� ������� , ����� ������� ������� �� �������� ������������ � �� ������
            });
        }
    }
}
