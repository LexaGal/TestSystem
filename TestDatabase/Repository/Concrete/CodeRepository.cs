using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class CodeRepository : Repository<Code>, ICodeRepository
    {
        public override bool Edit(int id, Code value)
        {
            throw new System.NotImplementedException();
        }
    }
}