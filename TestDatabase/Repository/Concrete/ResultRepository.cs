using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        public override bool Edit(int id, Result value)
        {
            throw new System.NotImplementedException();
        }

        public Result GetLastResultByCodeId(int id)
        {
            List<Result> list = new List<Result>();
            foreach (var result in GetAll())
            {
                list.Add(result);
            }
            return list.FirstOrDefault(r => r.CodeId == id);
        }
    }
}