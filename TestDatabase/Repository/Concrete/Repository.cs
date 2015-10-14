using System;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using TestDatabase.Context;
using TestDatabase.Repository.Abstract;
using TestDatabase.Transactions;

namespace TestDatabase.Repository.Concrete
{
    public abstract class Repository<T> : IRepository<T> where T: class
    {
        protected TestSystemDb Context = new TestSystemDb();
        //protected string ConnectionString = ConfigurationManager.ConnectionStrings["TestSystemDb"].ConnectionString;
        
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>().AsQueryable();
        }

        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual bool Add(T value)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    Context.Set<T>().Add(value);
                    Context.SaveChanges();
                    Dispose();
                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    Transaction.Current.Rollback();
                    return false;
                }
                return true;
            }
        }

        public abstract bool Edit(int id, T value);


        public virtual bool Delete(int id)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    T t = Context.Set<T>().Find(id);
                    if (t != null)
                    {
                        Context.Set<T>().Remove(t);
                        Context.SaveChanges();
                        Dispose();
                        transactionScope.Complete();
                    }
                }
                catch (Exception)
                {
                    Transaction.Current.Rollback();
                    return false;
                }
                return true;
            }
        }

        public virtual void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}