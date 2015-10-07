using System;
using Quartz;

namespace TestServer
{
    class Server:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
