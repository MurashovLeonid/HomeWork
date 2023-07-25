using Microsoft.EntityFrameworkCore;
using Quartz;
using SomeApp.Exstensions;
using SomeApp.Infrastructure.Implementation.EFCore.Contexts;
using SomeApp.Infrastructure.Implementation.EFCore.RepositoryImplementations;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;
using SomeApp.UseCases.Commands.TaskRows.CreateTaskRow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

#region Quartz

builder.Services.ConfigureQuartz();

builder.Services.AddQuartzHostedService(cfg=> cfg.WaitForJobsToComplete = true);

#endregion

#region Interfaces

builder.Services.AddTransient<IRepository, Repository>();

#endregion

#region Contexts

builder.Services.AddDbContext<ApplicationDbContext>(opts=> opts.UseNpgsql(builder.Configuration.GetConnectionString("NpgSqlConnection")));

#endregion

#region MediatR

builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(typeof(CreateTaskRowHandler).Assembly));

#endregion




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

app.Run();
