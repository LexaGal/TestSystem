using System;
using System.Diagnostics;

namespace TestServer
{
    public class Runer
    {
        public bool TryGetResult(Process proc, string input, out string output)
        {
            proc.Start();
            proc.MaxWorkingSet = new IntPtr(100000);
            proc.StandardInput.WriteLine(input);
            if (!proc.WaitForExit(1000))
            {
                proc.Close();
                output = string.Empty;
                return false;
            }
            var tmp  = proc.StandardOutput.ReadToEnd();
            output = tmp.Remove(tmp.Length - 3, 2);
            return true;
        }
    }
}
