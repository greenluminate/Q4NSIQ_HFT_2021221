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
        public RestCollection<Movie> Movies;

        public RestCollection<MovieHall> MovieHalls;

        public RestCollection<Seats> Seats;

        public RestCollection<Showtime> Showtimes;

        public RestCollection<Staff> Staffs;

        public RestCollection<Ticket> Tickets;

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
        public object SelectedObjectString
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
                    GenericViewModel = new GenericViewModel<Movie>(Movies, this.url, this.hub);
                    break;
                case "MovieHall":
                    GenericViewModel = new GenericViewModel<MovieHall>(MovieHalls, this.url, this.hub);
                    break;
                case "Seats":
                    GenericViewModel = new GenericViewModel<Seats>(Seats, this.url, this.hub);
                    break;
                case "Showtime":
                    GenericViewModel = new GenericViewModel<Showtime>(Showtimes, this.url, this.hub);
                    break;
                case "Staff":
                    GenericViewModel = new GenericViewModel<Staff>(Staffs, this.url, this.hub);
                    break;
                case "Ticket":
                    GenericViewModel = new GenericViewModel<Ticket>(Tickets, this.url, this.hub);
                    break;
            }
        }

        public List<DataTemplate> TabControlContentTemplates { get; set; }

        public MainWindowViewModel()
        {
            this.url = "http://localhost:17133/";
            this.hub = "hub";

            Movies = new RestCollection<Movie>(Url, null, Hub);
            MovieHalls = new RestCollection<MovieHall>(Url, null, Hub);
            Seats = new RestCollection<Seats>(Url, null, Hub);
            Showtimes = new RestCollection<Showtime>(Url, null, Hub);
            Staffs = new RestCollection<Staff>(Url, null, Hub);
            Tickets = new RestCollection<Ticket>(Url, null, Hub);

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
        {
            string editorContentStack = "<ScrollViewer Grid.Column=\"1\"> <StackPanel>";
            string recordTemplate =
                "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">" +
                    "<Grid>" +
                        "<Grid.ColumnDefinitions>" +
                            "<ColumnDefinition Width=\"1*\"/>" +
                            "<ColumnDefinition Width=\"1*\"/>" +
                        "</Grid.ColumnDefinitions>" +
                        "<ListBox x:Name=\"lb_records\" Grid.Column=\"0\" ItemsSource=\"{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Window}}, Path=DataContext.GenericViewModel.Entities }\" SelectedItem=\"{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Window}}, Path=DataContext.GenericViewModel.SelectedEntitiy}\" HorizontalContentAlignment=\"Stretch\">" +
                            "<ListBox.ItemTemplate>" +
                                "<DataTemplate>" +
                                    "<Border BorderBrush=\"Gray\" BorderThickness=\"0,0,0,1\">" +
                                        "<StackPanel>";

            properties.ForEach(prop =>
            {
                editorContentStack += $"<Label Content=\"{prop.Name}\" Background=\"LightBlue\" Padding=\"10\"/>";
                editorContentStack += $"<TextBox Text=\"{{Binding RelativeSource={{RelativeSource FindAncestor, AncestorType={{x:Type Window}}}}, Path=DataContext.GenericViewModel.SelectedEntitiy.{prop.Name}}}\" Padding=\"10\" Margin=\"0,0,0,20\"/>";
                recordTemplate += $"<Label Content=\"{{Binding { prop.Name }}}\"></Label>";
            });

            editorContentStack += $"<Button Content=\"Create Movie\" Command=\"{{Binding RelativeSource={{RelativeSource FindAncestor, AncestorType={{x:Type Window}}}}, Path=DataContext.GenericViewModel.CreateCommand}}\" Margin=\"10\" Padding=\"10\"/>";
            editorContentStack += $"<Button Content=\"Delete Movie\" Command=\"{{Binding RelativeSource={{RelativeSource FindAncestor, AncestorType={{x:Type Window}}}}, Path=DataContext.GenericViewModel.DeleteCommand}}\" Margin=\"10\" Padding=\"10\"/>";
            editorContentStack += $"<Button Content=\"Update Movie\" Command=\"{{Binding RelativeSource={{RelativeSource FindAncestor, AncestorType={{x:Type Window}}}}, Path=DataContext.GenericViewModel.UpdateCommand}}\" Margin=\"10\" Padding=\"10\"/>";
            editorContentStack += "</StackPanel> </ScrollViewer>";

            recordTemplate += "</StackPanel>" +
                                        "</Border>" +
                                    "</DataTemplate>" +
                                "</ListBox.ItemTemplate>" +
                            "</ListBox>" +
                        $"{editorContentStack}" +
                    "</Grid>" +
                "</DataTemplate>";

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(recordTemplate));

            TabControlContentTemplates.Add((DataTemplate)XamlReader.Load(ms));
        }
    }
}
