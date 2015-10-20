using System;
using System.Diagnostics;

namespace TestServer
{
    public class Runer
    {
        public bool TryGetResult(Process proc, string input, out string output)
        {
            proc.Start();
            //proc.MaxWorkingSet = new IntPtr(10000);
            proc.StandardInput.WriteLine(input);
            if (!proc.WaitForExit(1000))
            {
                proc.Close();
                output = string.Empty;
                return false;
            }
            string tmp  = proc.StandardOutput.ReadToEnd();
            
            output = tmp;
            if (tmp.Length > 2)
            {
                output = tmp.Remove(tmp.Length - 3, 2);
            }
            
            return true;
        }
    }
}
