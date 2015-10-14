using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace TestServer
{
    public class CSharpCompiler : ICompiler
    {
        public bool TryCompile(string code, string outputAssembly)
        {
            var providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v2.0" } };
            var provider = new CSharpCodeProvider(providerOptions);

            var compilerParams = new CompilerParameters
            {
                OutputAssembly = outputAssembly,
                GenerateExecutable = true
            };
            CompilerResults results = new CompilerResults(null);
            try
            {
                results = provider.CompileAssemblyFromSource(compilerParams, code);
            }
            catch (Exception e)
            {
                int i = 0;
            }
            return !results.Errors.HasErrors;
        }
    }
}
