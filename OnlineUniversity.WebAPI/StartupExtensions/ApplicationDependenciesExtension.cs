using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OnlineUniversity.Application;
using OnlineUniversity.Application.Interfaces;
using OnlineUniversity.Application.Mappings;
using OnlineUniversity.Data.EFCore;
using OnlineUniversity.Domain.Commands;
using OnlineUniversity.Domain.Commands.Interfaces;
using OnlineUniversity.Domain.Repository;
using OnlineUniversity.Infrastructure;
using OnlineUniversity.Infrastructure.Interfaces;
using System;

namespace OnlineUniversity.WebAPI.StartupExtensions
{
    public static class ApplicationDependenciesExtension
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<ILecturerRepository, LecturerRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseStatisticsRepository, CourseStatisticsRepository>();

            services.AddScoped<ILecturerApplication, LecturerApplication>();
            services.AddScoped<ICourseApplication, CourseApplication>();
            services.AddScoped<ISignUpToCourseApplication, SignUpToCourseApplication>();
            services.AddScoped<ICourseStatisticsApplication, CourseStatisticsApplication>();

            services.AddScoped<ISignUpToCourseCommandHandler, SignUpToCourseCommandHandler>();
            services.AddScoped<IUpdateCourseStatisticsCommandHandler, UpdateCourseStatisticsCommandHandler>();

            services.AddAutoMapper(typeof(CourseMapping), typeof(LecturerMapping), typeof(StudentMapping), typeof(CourseStatisticsMapping));

            services.AddDbContext<DataContext>(options =>
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                options.UseInMemoryDatabase(databaseName: "OnlineUniversity_db")
            );
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(f =>
                {
                    string name = f.Name;

                    if (name.EndsWith("Dto"))
                        name = name.Replace("Dto", string.Empty);

                    return name;
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Online University API",
                    Contact = new OpenApiContact
                    {
                        Name = "Anderson Davanse",
                        Email = "andervanse@gmail",
                        Url = new Uri("https://github.com/andervanse"),
                    }
                });
            });

            return services;
        }
    }
}
