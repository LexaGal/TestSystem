using MvcTestSystem.Models;
using MvcTestSystem.Repository.Abstract;

namespace MvcTestSystem.Repository.Concrete
{
    public class CodeRepository : Repository<Code>, ICodeRepository
    {
        public override bool Edit(int id, Code value)
        {
            throw new System.NotImplementedException();
        }
    }
}