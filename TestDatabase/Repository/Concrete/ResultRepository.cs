using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        //public ResultRepository()
        //{
        //    ((IObjectContextAdapter)Context).ObjectContext.ObjectStateManager.ObjectStateManagerChanged +=
        //        (sender, args) => Debug.WriteLine("{0}, {1}", args.Action, args.Element.ToString());
        //}

        public override bool Edit(int id, Result value)
        {
            throw new System.NotImplementedException();
        }
    }
}