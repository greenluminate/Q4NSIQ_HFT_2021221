using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal CinemaDbContext db;
        internal DbSet<TEntity> dbSet;

        public Repository(CinemaDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<TEntity>();
        }

        public void Create(TEntity obj)
        {
            dbSet.Add(obj);
            db.SaveChanges();
        }

        public TEntity Read(int id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<TEntity> ReadAll()
        {
            return dbSet.AsQueryable();
        }

        public void Update(TEntity obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            dbSet.Remove(Read(id));
            db.SaveChanges();
        }
    }
}
