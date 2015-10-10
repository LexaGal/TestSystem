using System.Diagnostics;

namespace TestServer
{
    public class ProcessBuilder
    {
        public Process Create(string addres)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = addres,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };
            return proc;
        }
    }
}
