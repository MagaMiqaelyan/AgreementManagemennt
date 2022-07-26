using AgreementManagementTask.DataBase;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AgreementManagementTask.Services
{
    public abstract class ServiceBase<T> : IService<T> where T : class
    {
        protected readonly DbSet<T> DbSet;
        protected readonly AgreementDbContext AgreementDbContext;
        public ServiceBase(AgreementDbContext agreementDbContext)
        {
            AgreementDbContext = agreementDbContext;
            DbSet = agreementDbContext.Set<T>();
        }

        public abstract void Update(T entity);

        public void Add(T entity)
        {
            DbSet.Add(entity);
            AgreementDbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
            AgreementDbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Func<T, bool> filter = null, params IncludedType[] includedTypes)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            foreach (var includeProp in includedTypes)
            {
                query = query.Include(includeProp.ToString());
            }

            return query;
        }

        public T FirstOrDefault(Func<T, bool> filter = null, params IncludedType[] includedTypes)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            foreach (var includeProp in includedTypes)
            {
                query = query.Include(includeProp.ToString());
            }

            return query.FirstOrDefault();
        }
    }
}
