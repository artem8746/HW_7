
using Microsoft.AspNetCore.Components;
using ToDoListWebAPI_HW.Models;
using ToDoListWebAPI_HW.Services;

namespace ToDoListWebAPI_HW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDataRegisterService, DataRegisterService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("api/targets", (HttpContext requestDelegate) =>
            {
                var service = requestDelegate.RequestServices.GetService<IDataRegisterService>();
                var targets = service.GetTargets();

                if (targets == null)
                    return Results.NoContent();

                return Results.Ok(targets);
            })
                .WithName("GetTargets")
                .WithOpenApi();

            app.MapPost("api/targets", (HttpContext requestDelegate, CreateTargetRequest request) =>
            {
                var service = requestDelegate.RequestServices.GetService<IDataRegisterService>();
                
                var target = new Target
                {
                    Id = service.GetLastId() + 1,
                    TargetValue = request.TargetValue,
                    Description = request.Description,
                    IsCompleted = false
                };

                service.Add(target);

                return Results.Ok(target);
            })
                .WithName("CreateTarget")
                .WithOpenApi();

            app.MapPatch("api/targets/{id:int}", (HttpContext requestDelegate, ChangeTargetRequest request, int id) =>
            {
                var service = requestDelegate.RequestServices.GetService<IDataRegisterService>();

                var target = service.ChangeTarget(request, id);

                if (target is not null)
                    return Results.Ok(target);
                               
                return Results.BadRequest(request);
            })
                .WithName("ChangeTask")
                .WithOpenApi();

            app.MapDelete("api/targets/{id:int}", (HttpContext requestDelegate, int id) =>
            {
                var service = requestDelegate.RequestServices.GetService<IDataRegisterService>();

                var target = service.DeleteTarget(id);
                if (target is not null)
                {
                    return Results.Ok(target);
                }

                return Results.BadRequest();
            })
                .WithName("DeleteTarget")
                .WithOpenApi();
            
            app.MapGet("api/targets/{id:int}", (HttpContext requestDelegate, int id) =>
            {
                var service = requestDelegate.RequestServices.GetService<IDataRegisterService>();

                var target = service.GetTarget(id);

                if (target is not null)
                    return Results.Ok(target);

                return Results.BadRequest();
            })
                .WithName("GetTarget")
                .WithOpenApi();

            app.Run();
        }
    }
}