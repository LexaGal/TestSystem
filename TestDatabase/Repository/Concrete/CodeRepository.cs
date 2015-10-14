using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;
using TestDatabase.Transactions;

namespace TestDatabase.Repository.Concrete
{
    public class CodeRepository : Repository<Code>, ICodeRepository
    {
        public override bool Edit(int id, Code value)
        {
            throw new System.NotImplementedException();
        }

        public IList<Code> ExtractAll()
        {
            IList<Code> codes = null;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    codes = GetAll().ToList();
                    Context.Database.ExecuteSqlCommand("delete from Codes");
                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    Transaction.Current.Rollback();
                    return null;
                }
            }
            return codes;
        }
    }
}