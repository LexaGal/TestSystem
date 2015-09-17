using System.Linq;
using MvcTestSystem.Repository.Abstract;

namespace MvcTestSystem.Repository.Concrete
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
            Context.Set<T>().Add(value);
            Context.SaveChanges();
            Dispose();
            return true;
        }

        public abstract bool Edit(int id, T value);
        
        public virtual bool Delete(int id)
        {
            T t = Context.Set<T>().Find(id);
            if (t != null)
            {
                Context.Set<T>().Remove(t);
                Context.SaveChanges();
                Dispose();
                return true;
            }
            return false;
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