using API_SBI.Entities;
using API_SBI.Repository;
using API_SBI.Repository.IRepository;
using API_SBI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;



namespace API_SBI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // Este método es llamado por el tiempo de ejecución. Utilice este método para agregar servicios al contenedor.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IServerPost, ServerPostRepository>();
            services.AddCors();
            services.AddScoped<ServerPostService>();
            services.AddSingleton<IServerPost, ServerPostRepository>();
            services.AddTransient<ServerPost>();
            services.AddTransient<IServerPost, ServerPostRepository>();
            services.AddMemoryCache();

            //Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
               mc.AddProfile(new Mappers.Mappers());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_SBI", Version = "v1" });
            });
        }


        //Este método es llamado por el tiempo de ejecución.Utilice este método para configurar la canalización de solicitudes HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_SBI v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
