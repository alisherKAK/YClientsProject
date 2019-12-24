using System.Collections.Generic;

namespace SaveTime.AbstractModels
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        void Delete(T item);
        void Delete(int? id);
        void Update(T item);
        IEnumerable<T> GetAll();
        T Get(T item);
        T Get(int? id);
    }
}
