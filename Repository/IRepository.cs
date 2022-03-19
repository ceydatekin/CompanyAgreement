using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompanyAgreement.Repository
{
    public abstract class IRepository<T> where T : class
    {

        Models.Context context = ContextManager.GetContext();
        //Context context = Context.getNesne();
        //Context context = new Context();   

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().SingleOrDefault(predicate);
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
  
        public T Insert(T entity)
        {

           // context.Add(entity);
           //context.Add(entity);
          context.Set<T>().Add(entity);
            Save();
            return entity;

        }
        public void Update(T entity)
        {

        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            Save();
        }
        public void Save()
        {
            //context.SaveChanges();
            context.SaveChangesAsync();
            //Microsoft.Data.SqlClient.SqlException



        }

    }
}
