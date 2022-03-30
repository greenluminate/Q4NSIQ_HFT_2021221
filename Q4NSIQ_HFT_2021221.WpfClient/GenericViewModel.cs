using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Q4NSIQ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Q4NSIQ_HFT_2021221.WpfClient
{
    public class GenericViewModel<T> : ObservableRecipient where T : class
    {
        private RestCollection<T> entities;
        public RestCollection<T> Entities { get { return entities; } set { this.entities = value; OnPropertyChanged(); } }

        private T selectedEntitiy;
        public T SelectedEntitiy
        {
            get { return selectedEntitiy; }
            set
            {
                if (value != null)
                {
                    /// <summary>
                    /// It fills the selected entity with copyed values.
                    /// </summary>
                    var properties = GetTModelProperties();
                    selectedEntitiy = (T)Activator.CreateInstance(typeof(T));
                    properties.ForEach(prop => prop.SetValue(selectedEntitiy, selectedEntitiy.GetType().GetProperty(prop.Name).GetValue(value)));

                    OnPropertyChanged();
                    (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public GenericViewModel(string url, string hub)
        {
            Entities = new RestCollection<T>(url, null, hub);

            CreateCommand = new RelayCommand(
            () => { AddEntityToRestColelction(); });
            UpdateCommand = new RelayCommand(
            () => { Entities.Update(SelectedEntitiy); OnPropertyChanged(); },
            () => { return SelectedEntitiy != null; }
            );
            DeleteCommand = new RelayCommand(
            () => { Entities.Delete(getReflexId()); OnPropertyChanged(); },
            () => { return SelectedEntitiy != null; }
            );

            //Maybe it will be necessary to set a selected entity:
            //selectedEntitiy = entities.GetEnumerator().Current;
            //selectedEntitiy = (T)Activator.CreateInstance(typeof(T));
        }

        private int getReflexId()
        {
            return (int)selectedEntitiy
                        .GetType()
                        .GetProperties()
                        .Where(prop => prop.Name.Contains("Id"))
                        .FirstOrDefault()
                        .GetValue(selectedEntitiy);
        }

        public void AddEntityToRestColelction()
        {
            T newEntity = (T)Activator.CreateInstance(typeof(T));
            var properties = GetTModelProperties();
            properties.ForEach(prop => prop.SetValue(newEntity, selectedEntitiy.GetType().GetProperty(prop.Name).GetValue(selectedEntitiy)));

            Entities.Add(newEntity);
        }

        /// <summary>
        /// It collects everything from the current model class what is needed by dynamic xaml.cs templates.
        /// </summary>
        public List<PropertyInfo> GetTModelProperties()
        {
            Type type = typeof(T);
            var propertiesAll = type.GetProperties();
            var properties = propertiesAll.Where(p => !p.PropertyType.AssemblyQualifiedName.Contains("ICollection") &&
                                                      !p.PropertyType.AssemblyQualifiedName.Contains(".Models")).ToList();
            return properties;
        }

    }
}
