using Microsoft.Toolkit.Mvvm.ComponentModel;
using Q4NSIQ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        //GenericModel have to use these to not to recreate them all the time from scratch when we change model.
        public RestCollection<Movie> movies;
        public RestCollection<Movie> Movies { get { return this.movies; } set { this.movies = value; } }

        public RestCollection<MovieHall> movieHalls;
        public RestCollection<MovieHall> MovieHalls { get { return this.movieHalls; } set { this.movieHalls = value; } }

        public RestCollection<Seats> seats;
        public RestCollection<Seats> Seats { get { return this.seats; } set { this.seats = value; } }

        public RestCollection<Showtime> showtimes;
        public RestCollection<Showtime> Showtimes { get { return this.showtimes; } set { this.showtimes = value; } }

        public RestCollection<Staff> staffs;
        public RestCollection<Staff> Staffs { get { return this.staffs; } set { this.staffs = value; } }

        public RestCollection<Ticket> tickets;
        public RestCollection<Ticket> Tickets { get { return this.tickets; } set { this.tickets = value; } }

        private string url;
        public string Url
        {
            get { return url; }
            private set { url = value; }
        }

        private string hub;
        public string Hub
        {
            get { return hub; }
            private set { hub = value; }
        }

        public object GenericViewModel { get; private set; }

        private void createGenericViewModel(object menuObj)
        {
            switch (menuObj as string)
            {
                case "Movie":
                    GenericViewModel = new GenericViewModel<Movie>(ref movies, this.url, this.hub);
                    break;
                case "MovieHall":
                    GenericViewModel = new GenericViewModel<MovieHall>(ref movieHalls, this.url, this.hub);
                    break;
                case "Seats":
                    GenericViewModel = new GenericViewModel<Seats>(ref seats, this.url, this.hub);
                    break;
                case "Showtime":
                    GenericViewModel = new GenericViewModel<Showtime>(ref showtimes, this.url, this.hub);
                    break;
                case "Staff":
                    GenericViewModel = new GenericViewModel<Staff>(ref staffs, this.url, this.hub);
                    break;
                case "Ticket":
                    GenericViewModel = new GenericViewModel<Ticket>(ref tickets, this.url, this.hub);
                    break;
            }
        }

        public MainWindowViewModel()
        {
            this.url = "http://localhost:17133/";
            this.hub = "hub";
        }
    }
}
