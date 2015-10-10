using System;
using System.Linq;
using Quartz;
using TestDatabase.Repository.Concrete;

namespace TestServer
{
    internal class Server : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (var testRepository = new TestRepository())
            {
                using (var codeRepository = new CodeRepository())
                {
                    using (var resultRepository = new ResultRepository())
                    {
                        ICompiler compiler = new CSharpCompiler();
                        var processBuilder = new ProcessBuilder();
                        var runer = new Runer();
                        var code = codeRepository.GetAll().ToList();
                        code.ForEach(item =>
                        {
                            if (!compiler.TryCompile(item.Text, "test.exe"))
                            {
                                resultRepository.Add(new TestDatabase.Entities.Result(item.Id, "compilation error"));
                            }
                            else
                            {
                                var tests = testRepository.GeTestsByTaskId(item.TaskId).ToList();
                                for (var i = 0; i < tests.Count; i++)
                                {
                                    var proc = processBuilder.Create("test.exe");
                                    string output;
                                    try
                                    {
                                        if (!runer.TryGetResult(proc, tests[i].Input, out output))
                                        {
                                            resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                                "out of time on test " + (i + 1).ToString()));
                                            return;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                            "out of memory on test " + (i + 1).ToString()));
                                        return;
                                    }

                                    if (output == tests[i].Output) continue;
                                    resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                        "wrong ansver on test " + (i + 1).ToString()));
                                    return;
                                }

                                resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                    "success"));
                            }
                        });
                    }
                }
            }
        }
    }
}
