using System.Collections.Generic;
using System.Linq;
using TestDatabase.Entities;

namespace TestDatabase.Repository.Abstract
{
    public interface ICodeRepository : IRepository<Code>
    {
        IList<Code> ExtractAll();
    }
}