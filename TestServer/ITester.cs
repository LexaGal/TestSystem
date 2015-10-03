using System.Collections.Generic;
using System.Diagnostics;

namespace TestServer
{
    public interface ITester
    {
        void Test(Process proc, Dictionary<string, string> testCases,
            out Result result);
    }
}
