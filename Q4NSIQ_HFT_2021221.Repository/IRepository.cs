using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity obj);
        void Delete(int id);
        TEntity Read(int id);
        IQueryable<TEntity> ReadAll();
        void Update(TEntity obj);
    }
}