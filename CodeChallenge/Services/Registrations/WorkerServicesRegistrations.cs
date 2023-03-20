using CodeChallenge.DAL;
using CodeChallenge.DAL.Interfaces;
using CodeChallenge.IServices;

namespace CodeChallenge.Services.Registrations
{
    public static class WorkerServicesRegistrations
    {
        public static void RegisterWorkerServices(this IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IMovieRepository, MovieRepository>();

            services.AddTransient<IDirectorService, DirectorService>();
            services.AddTransient<IDirectorRepository, DirectorRepository>();
        }
    }
}
