using System;
using System.Diagnostics;

namespace TestServer
{
    public class Runer
    {
        public bool TryGetResult(Process proc, string input, out string output)
        {
            proc.Start();
            proc.MaxWorkingSet = new IntPtr(1000000);
            proc.StandardInput.WriteLine(input);
            if (!proc.WaitForExit(10000))
            {
                proc.Close();
                output = string.Empty;

                return false;
            }
            output = proc.StandardOutput.ReadToEnd();
            return true;
        }
    }
}
