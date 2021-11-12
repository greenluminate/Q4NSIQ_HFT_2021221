using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            repo.Create(obj);
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
            repo.Delete(id);
        }
    }
}
