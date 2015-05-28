using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models.Repositories.AbstractInterfaces
{
   /* public partial class GridMastercontrol<TEntity> : UserControl where TEntity : class*/
    public abstract class AbstractIRepository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected readonly ApplicationDbContext ApplicationDbContext;

        protected AbstractIRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }


        public IEnumerable<TEntity> GetDatabase()
        {
            return ApplicationDbContext.Set<TEntity>().ToList();
        }

        public bool IsDatabaseEmpty()
        {
            return ApplicationDbContext.Set<TEntity>().Any();
        }

        public int Create(TEntity Object)
        {
            ApplicationDbContext.Set<TEntity>().Add(Object);
            ApplicationDbContext.Entry(Object).State = EntityState.Added;
            ApplicationDbContext.SaveChanges();
            IContextEntity entity = (IContextEntity)Object;//herpderp im not proud, but this is the current solution. 
            return entity.Id;
        }

        public TEntity Read(int? id)
        {
            return ApplicationDbContext.Set<TEntity>().Find(id);
        }

        public void Update(TEntity Object)
        {
            ApplicationDbContext.Entry(Object).State = EntityState.Modified;
            ApplicationDbContext.SaveChanges();
        }

        public void Delete(TEntity Object)
        {
            ApplicationDbContext.Entry(Object).State = EntityState.Deleted;
            ApplicationDbContext.SaveChanges();
        }
    }
}