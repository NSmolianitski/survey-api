using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (connectionString is null)
            throw new NullReferenceException("Database connection string is null.");

        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        builder.AddRepositories();
        builder.AddServices();
    }

    private static void AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IResultRepository, ResultRepository>();
        builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IInterviewService, InterviewService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
    }
}