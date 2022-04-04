using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Q4NSIQ_HFT_2021221.WpfClient
{
    public class TabContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            MainWindowViewModel mwvm = (container.GetType().GetProperty("Parent").GetValue(container) as Border).DataContext as MainWindowViewModel;

            return mwvm.TabControlContentTemplates[mwvm.ClassNames.IndexOf((string)item)];
        }
    }
}
