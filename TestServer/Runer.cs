using System;
using System.Diagnostics;

namespace TestServer
{
    public class Runer
    {
        public bool TryGetResult(Process proc, string input, out string output, int memory, int time)
        {
            proc.Start();
            //proc.MaxWorkingSet = new IntPtr(memory);
            proc.StandardInput.WriteLine(input);
            if (!proc.WaitForExit(time))
            {
                proc.Close();
                output = string.Empty;
                return false;
            }
            string tmp  = proc.StandardOutput.ReadToEnd();
            
            output = tmp;
            if (tmp.Length > 2)
            {
                output = tmp.Remove(tmp.Length - 2, 2);
            }
            return true;
        }
    }
}
