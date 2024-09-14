
using Autofac.Core;
using AutoMapper;
using TrelloManagmentSystem.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrelloManagmentSystem.Data;

using TrelloManagmentSystem.Helpers;
using TrelloManagmentSystem.Middlewares;

namespace TrelloManagmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
             builder.AutoFacConfigration();
            builder.AddServicesDependencies(builder.Configuration);

            var app = builder.Build();
            MapperHelper.mapper = app.Services.GetService<IMapper>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
