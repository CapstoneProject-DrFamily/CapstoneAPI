using AutoMapper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Services;
using Capstone_API_V2.UnitOfWork;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Capstone_API_V2
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
            services.AddDbContext<FamilyDoctorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Capstone_DB")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IHealthRecordService, HealthRecordService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IAuthenticatedService, AuthenticatedService>();
            services.AddScoped<IExaminationHistoryService, ExaminationHistoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IFamilyDoctorService, FamilyDoctorService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            _ = services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Capstone API", Version = "v1.0" });
            });
            services.AddControllers().AddNewtonsoftJson( o => { o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    //.AllowCredentials();
                });
            });

            var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), "Key", "capstoneproject-5c703-firebase-adminsdk-r3xw0-f495eac594.json");
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(pathToKey)
            });

            var tokenValue = Configuration.GetSection("AppSettings:Token").Value;
            var url = Configuration.GetSection("AppSettings:Url").Value;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   var firebaseProject = Configuration.GetSection("AppSettings:FirebaseProject").Value;
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.Authority = "https://securetoken.google.com/" + firebaseProject;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidAudience = firebaseProject,
                       ValidIssuer = "https://securetoken.google.com/" + firebaseProject,
                       ValidateLifetime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValue))
                   };
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(UI =>
            {
                UI.SwaggerEndpoint("/swagger/v1.0/swagger.json", "V1.1");
            });
        }
    }
}
