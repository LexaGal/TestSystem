namespace TestServer
{
    public interface ICompiler
    {
        bool TryCompile(string code, string outputAssembly);
    }
}