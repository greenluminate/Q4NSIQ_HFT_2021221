using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Repository;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Endpoint.Services;

namespace Q4NSIQ_HFT_2021221.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region GenericLogicTransient
            services.AddTransient<ILogic<Movie>, Logic<Movie>>();
            services.AddTransient<ILogic<MovieHall>, Logic<MovieHall>>();
            services.AddTransient<ILogic<Seats>, Logic<Seats>>();
            services.AddTransient<ILogic<Showtime>, Logic<Showtime>>();
            services.AddTransient<ILogic<Staff>, Logic<Staff>>();
            services.AddTransient<ILogic<Ticket>, Logic<Ticket>>();
            #endregion

            #region GenericRepositoryTransient
            services.AddTransient<IRepository<Movie>, Repository<Movie>>();
            services.AddTransient<IRepository<MovieHall>, Repository<MovieHall>>();
            services.AddTransient<IRepository<Seats>, Repository<Seats>>();
            services.AddTransient<IRepository<Showtime>, Repository<Showtime>>();
            services.AddTransient<IRepository<Staff>, Repository<Staff>>();
            services.AddTransient<IRepository<Ticket>, Repository<Ticket>>();
            #endregion

            #region LogicTransient
            services.AddTransient<IMovieLogic, MovieLogic>();
            services.AddTransient<IMovieHallLogic, MovieHallLogic>();
            services.AddTransient<ISeatsLogic, SeatsLogic>();
            services.AddTransient<IShowtimeLogic, ShowtimeLogic>();
            services.AddTransient<IStaffLogic, StaffLogic>();
            services.AddTransient<ITicketLogic, TicketLogic>();
            #endregion

            #region RepositoryTransient
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieHallRepository, MovieHallRepository>();
            services.AddTransient<ISeatsRepository, SeatsRepository>();
            services.AddTransient<IShowtimeRepository, ShowtimeRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            #endregion

            services.AddTransient<CinemaDbContext, CinemaDbContext>();

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:7172"));

            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });

        }
    }
}
