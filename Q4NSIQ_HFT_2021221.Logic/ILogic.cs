using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface ILogic<TEntity> where TEntity : class
    {
        void Create(TEntity obj);
        void Delete(int id);
        TEntity Read(int id);
        IEnumerable<TEntity> ReadAll();
        void Update(TEntity obj);
    }
}