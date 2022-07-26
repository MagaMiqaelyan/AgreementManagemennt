using System;
using System.Collections.Generic;

namespace AgreementManagementTask.Services
{
    public interface IService<T>
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        T Get(int id);
        IEnumerable<T> GetAll(Func<T, bool> filter = null, params IncludedType[] includedTypes);
        T FirstOrDefault(Func<T, bool> filter = null, params IncludedType[] includedTypes);
    }

    public enum IncludedType
    {
        None, 
        Product,
        ProductGroup,
        User
    }
}
