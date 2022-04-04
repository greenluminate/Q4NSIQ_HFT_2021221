using Microsoft.Toolkit.Mvvm.ComponentModel;
using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace Q4NSIQ_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel Self { get { return this; } private set { } }
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
        public List<string> ClassNames
        {
            get { return new List<string> { "Movie", "MovieHall", "Seats", "Showtime", "Staff", "Ticket" }; }
            private set { }
        }

        private object selectedObjectString;
        public object SelectedObjectString//Selected Menu -> ELidnTjuk manuálisan mint a parancssorosnál a kieg modellben és kész onna ngenerikus, csak dynamic load kell ,de az amúgy is kell innek küldük a zadatokat a xaml.cs pedig megeszi amit megkap és felépíti a viewt.
        {
            get
            {
                return selectedObjectString;
            }
            set
            {
                SetProperty(ref selectedObjectString, value);
                createGenericViewModel(value);
                OnPropertyChanged();
            }
        }

        public object genericViewModel;
        public object GenericViewModel { get { return genericViewModel; } private set { this.genericViewModel = value; OnPropertyChanged(); } }

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

        public List<DataTemplate> TabControlContentTemplates { get; set; }

        public MainWindowViewModel()
        {
            this.url = "http://localhost:17133/";
            this.hub = "hub";

            movies = new RestCollection<Movie>(Url, null, Hub);
            movieHalls = new RestCollection<MovieHall>(Url, null, Hub);
            seats = new RestCollection<Seats>(Url, null, Hub);
            showtimes = new RestCollection<Showtime>(Url, null, Hub);
            staffs = new RestCollection<Staff>(Url, null, Hub);
            tickets = new RestCollection<Ticket>(Url, null, Hub);

            TabControlContentTemplates = new List<DataTemplate>();
            TabControlContentTemplateSetter();
        }

        private void TabControlContentTemplateSetter()
        {
            ClassNames.ForEach(name =>
            {
                createGenericViewModel(name);
                TemplateCreator((List<PropertyInfo>)GenericViewModel.GetType().GetMethod("GetTModelProperties").Invoke(GenericViewModel, null));
            });
        }

        private void TemplateCreator(List<PropertyInfo> properties)
        {//Template in templaet string
            string templateString =
                "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">" +
                    "<ListBox x:Name=\"lb_records\" ItemsSource=\"{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Window}}, Path=DataContext.GenericViewModel.Entities }\" SelectedItem=\"{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Window}}, Path=DataContext.GenericViewModel.SelectedEntitiy}\">" +
                        "<ListBox.ItemTemplate>" +
                            "<DataTemplate>" +
                                "<StackPanel>";

            properties.ForEach(prop => templateString += $"<Label Content=\"{{Binding { prop.Name }}}\"></Label>");//itt tölteni template in templatet.

            templateString +=
                                "</StackPanel>" +
                                //Ide a template in templatet, ha kséz
                            "</DataTemplate>" +
                        "</ListBox.ItemTemplate>" +
                    "</ListBox>" +
                "</DataTemplate>";

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(templateString));

            TabControlContentTemplates.Add((DataTemplate)XamlReader.Load(ms));
        }
    }
}
