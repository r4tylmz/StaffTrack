using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StaffTrack.Core.Repositories;
using StaffTrack.Core.Services;
using StaffTrack.Core.UnitOfWorks;
using StaffTrack.Data;
using StaffTrack.Data.Repositories;
using StaffTrack.Data.UnitOfWorks;
using StaffTrack.Service.Services;

namespace StaffTrack.WebAPI
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

            services.AddCors(o=> o.AddPolicy("MyPolicy", builder =>{
                builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped(typeof(IService<>),typeof(Service<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IStaffActivityService, StaffActivityService>();
            services.AddScoped<IConstantService, ConstantService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"],o=> 
                    o.MigrationsAssembly("StaffTrack.Data"))
                
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "StaffTrack.WebAPI", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StaffTrack.WebAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}