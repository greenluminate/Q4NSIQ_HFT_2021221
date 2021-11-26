using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            var properties = type.GetProperties();

            bool objIsCreatable = true;
            int i = 0;
            while (objIsCreatable && i < properties.Length)
            {
                var property = properties[i];
                var attributes = property.GetCustomAttributes(false).Where(attr => attr.GetType().FullName.Contains("ValidationAttribute")).ToArray();
                var propertyValue = property.GetValue(obj);

                //Ezt megnézni elég-e. Tehát áttenni emnuhelperbe
                //objIsCreatable = attributes[0].GetType().GetCustomAttribute<ValidationAttribute>().IsValid(propertyValue);

                int j = 0;
                while (objIsCreatable && j < attributes.Length)
                {
                    var attribute = attributes[j];
                    string atrrName = attribute.GetType().Name;

                    if (atrrName.Contains("RegularExpression"))
                    {
                        /*var pattern = attribute.GetType().GetCustomAttribute<RegularExpressionAttribute>().Pattern;
                        bool isMatched = Regex.IsMatch(propertyValue.ToString(), pattern);
                        objIsCreatable = isMatched;*/

                        objIsCreatable = attribute.GetType().GetCustomAttribute<RegularExpressionAttribute>().IsValid(propertyValue);
                    }
                    else if (atrrName.Contains("Required"))
                    {
                        /*bool isNotEmpty = propertyValue != null;
                        isNotEmpty = propertyValue.GetType().Name.ToLower() == "string" ? (string)propertyValue != "" : true;
                        objIsCreatable = isNotEmpty;*/

                        objIsCreatable = attribute.GetType().GetCustomAttribute<RequiredAttribute>().IsValid(propertyValue);
                        objIsCreatable = propertyValue.GetType().Name.ToLower() == "string" ? (string)propertyValue != "" : true;
                    }
                    else if (atrrName.Contains("StringLength"))
                    {
                        objIsCreatable = attribute.GetType().GetCustomAttribute<StringLengthAttribute>().IsValid(propertyValue);
                    }
                    j++;

                }
                i++;

            }

            if (objIsCreatable)
            {
                repo.Create(obj);
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
            repo.Update(obj);
        }

        public void Delete(int id)
        {
            if (Read(id) != null)
            {
                repo.Delete(id);
            }
        }
    }
}
