using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using TestDatabase.Entities;
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

                        List<Code> codes = codeRepository.ExtractAll().ToList();
                        codes.ForEach(item =>
                        {
                            if (!compiler.TryCompile(item.Text, @"F://test.exe"))
                            {
                                resultRepository.Add(new TestDatabase.Entities.Result(item.Id, "compilation error"));
                            }
                            else
                            {
                                var tests = testRepository.GetTestsByTaskId(item.TaskId).ToList();
                                for (var i = 0; i < tests.Count; i++)
                                {
                                    var proc = processBuilder.Create(@"F://test.exe");
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
                                    catch (Exception e)
                                    {
                                        resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                            "out of memory on test " + (i + 1).ToString()));
                                        return;
                                    }

                                    if (output == tests[i].Output) continue;
                                    resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                        "wrong answer on test " + (i + 1).ToString()));
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
