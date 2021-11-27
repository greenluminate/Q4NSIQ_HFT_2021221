using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class Logic<TEntity> : ILogic<TEntity> where TEntity : class
    {
        internal IRepository<TEntity> repo;
        public Logic(IRepository<TEntity> repo)
        {
            this.repo = repo;
        }

        public void Create(TEntity obj)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() is null).ToArray();

            bool objIsCreatable = true;
            int i = 0;
            var property = properties[i];
            var propertyValue = property.GetValue(obj);
            while (objIsCreatable && i < properties.Length)
            {
                property = properties[i];
                var attributes = property.GetCustomAttributes(false).Where(attr => attr.GetType().BaseType.FullName.Contains("ValidationAttribute")).ToArray();
                propertyValue = property.GetValue(obj);

                int j = 0;
                while (objIsCreatable && j < attributes.Length)
                {
                    objIsCreatable = (attributes[j] as ValidationAttribute).IsValid(propertyValue);

                    j++;
                }

                i++;
            }

            if (objIsCreatable)
            {
                repo.Create(obj);
            }
            else
            {
                throw new ArgumentException($"Given parameter is inadequate! {property.Name}: {propertyValue}");
            }
        }

        public TEntity Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<TEntity> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(TEntity obj)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() is null).ToArray();

            bool objIsCreatable = true;
            int i = 0;
            var property = properties[i];
            var propertyValue = property.GetValue(obj);
            while (objIsCreatable && i < properties.Length)
            {
                property = properties[i];
                var attributes = property.GetCustomAttributes(false).Where(attr => attr.GetType().BaseType.FullName.Contains("ValidationAttribute")).ToArray();
                propertyValue = property.GetValue(obj);

                int j = 0;
                while (objIsCreatable && j < attributes.Length)
                {
                    objIsCreatable = (attributes[j] as ValidationAttribute).IsValid(propertyValue);

                    j++;
                }

                i++;
            }

            if (objIsCreatable)
            {
                repo.Update(obj);
            }
            else
            {
                throw new ArgumentException($"Given parameter is inadequate! {property.Name}: {propertyValue}");
            }
        }

        public void Delete(int id)
        {
            if (Read(id) != null)
            {
                try
                {
                    repo.Delete(id);
                }
                catch (Exception)
                {
                    throw new Exception($"You can not delete a record that is strictly bound with other records.");
                }
            }
            else
            {
                throw new ArgumentNullException($"There is no record matching the specified data (ID: {id}) to delete.");
            }
        }
    }
}
