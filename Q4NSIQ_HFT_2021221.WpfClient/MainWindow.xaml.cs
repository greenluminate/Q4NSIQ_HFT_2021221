using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q4NSIQ_HFT_2021221.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //int index = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tc_menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            //(DataContext as MainWindowViewModel).SelectedObjectString = (sender as TextBlock).Text;
            //(DataContext as MainWindowViewModel).SelectedObjectString = (DataContext as MainWindowViewModel).ClassNames[index++];
            //        "<DataTemplate>" +
            //"<DataTemplate.Resources>" +
            //    "<local:MainWindowViewModel x:Key=\"main\"/>" +
            //"</DataTemplate.Resources>" +
            //"<ListBox x:Name=\"lb_records\" ItemsSource=\"{Binding Source={StaticResource main}, Path=GenericViewModel.Entities}\" SelectedItem=\"{Binding Source={StaticResource main}, Path=GenericViewModel.SelectedEntitiy}\">" +
            //    "<ListBox.ItemTemplate>" +
            //        "<DataTemplate>" +
            //            "<StackPanel>";

            string templateString =
                "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">" +
                    "<ListBox x:Name=\"lb_records\" ItemsSource=\"{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Window}}, Path=DataContext.GenericViewModel.Entities }\" SelectedItem=\"{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Window}}, Path=DataContext.GenericViewModel.SelectedEntity}\">" +
                        "<ListBox.ItemTemplate>" +
                            "<DataTemplate>" +
                                "<StackPanel>";

            //List<PropertyInfo> properties = (DataContext as MainWindowViewModel).GenericViewModel.GetType().GetProperties().ToList();
            List<PropertyInfo> properties = (List<PropertyInfo>)((DataContext as MainWindowViewModel).GenericViewModel.GetType().GetMethod("GetTModelProperties").Invoke((DataContext as MainWindowViewModel).GenericViewModel, null));

            properties.ForEach(prop => templateString += $"<Label Content=\"{{Binding { prop.Name }}}\"></Label>");

            templateString +=
                                "</StackPanel>" +
                            "</DataTemplate>" +
                        "</ListBox.ItemTemplate>" +
                    "</ListBox>" +
                "</DataTemplate>";
            //ParserContext pc = new ParserContext();
            //pc.XmlnsDictionary.Add("", @"http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            //pc.XmlnsDictionary.Add("x", @"http://schemas.microsoft.com/winfx/2006/xaml\");
            //pc.XmlnsDictionary.Add("d", @"http://schemas.microsoft.com/expression/blend/2008\");
            //pc.XmlnsDictionary.Add("mc", @"http://schemas.openxmlformats.org/markup-compatibility/2006\");
            //pc.XmlnsDictionary.Add("local", "clr-namespace:Q4NSIQ_HFT_2021221.WpfClient;assembly=\"Q4NSIQ_HFT_2021221\"");
            //xmlns:local=\"clr-namespace:Q4NSIQ_HFT_2021221.WpfClient\"

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(templateString));
            //var template = (DataTemplate)XamlReader.Load(ms, pc);
            var template = (DataTemplate)XamlReader.Load(ms);
            tc_menu.ContentTemplate = template;
            ;
        }
    }
}
