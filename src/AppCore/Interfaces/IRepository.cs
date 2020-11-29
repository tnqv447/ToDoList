using System.Collections.Generic;
using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IRepository<T> where T:class
    {
         IList<T> GetAll();
        T GetBy(int id);
        T Add(User source, T entity);
        void Update(User source, T entity);
        void Delete(User source, T entity);

        bool Exists(int id);
    }
}