using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Q4NSIQ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Q4NSIQ_HFT_2021221.WpfClient
{
    public class GenericViewModel<T> : ObservableRecipient
    {
        private RestCollection<T> entities;
        public RestCollection<T> Entities { get { return entities; } set { this.entities = value; } }

        private T selectedEntity;
        public T SelectedEntitiy
        {
            get { return selectedEntity; }
            set
            {
                if (value != null)
                {
                    /// <summary>
                    /// It fills the selected entity with copyed values.
                    /// </summary>
                    var properties = GetTModelProperties();
                    selectedEntity = (T)Activator.CreateInstance(typeof(T));
                    properties.ForEach(prop => prop.SetValue(selectedEntity, selectedEntity.GetType().GetProperty(prop.Name).GetValue(value)));

                    OnPropertyChanged();
                    (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public GenericViewModel(ref RestCollection<T> entities, string url, string hub)
        {
            this.Entities = entities;

            CreateCommand = new RelayCommand(
            () => { AddEntityToRestColelction(); });
            UpdateCommand = new RelayCommand(
            () => { Entities.Update(SelectedEntitiy); OnPropertyChanged(); },
            () => { return SelectedEntitiy != null; }
            );
            DeleteCommand = new RelayCommand(
            () =>
            {
                Entities.Delete(getReflexId());
                OnPropertyChanged();
                //Entities.Update(SelectedEntitiy = (T)Entities.GetEnumerator().Current);
            },
            () => { return SelectedEntitiy != null; }
            );

            selectedEntity = (T)Entities.GetEnumerator().Current;
        }

        private int getReflexId()
        {
            return (int)selectedEntity
                        .GetType()
                        .GetProperties()
                        .Where(prop => prop.Name.Contains("Id"))
                        .FirstOrDefault()
                        .GetValue(selectedEntity);
        }

        public void AddEntityToRestColelction()
        {
            T newEntity = (T)Activator.CreateInstance(typeof(T));
            var properties = GetTModelProperties();
            properties.ForEach(prop => prop.SetValue(newEntity, !prop.Name.Contains("Id") ? selectedEntity.GetType().GetProperty(prop.Name).GetValue(selectedEntity) : null));

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
