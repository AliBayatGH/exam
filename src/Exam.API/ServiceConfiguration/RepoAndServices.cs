using Exam.Application.Interfaces;
using Exam.Application.Services;
using Exam.Domain.Interfaces;
using Exam.Infrastructure.Repositories;

namespace Exam.API.ServiceConfiguration
{
    public static class RepoAndServices
    {
        public static void AddServicesAndRepos(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUserService, UserService>();
            


        }
    }
}
