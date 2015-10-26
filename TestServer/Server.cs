using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;
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
                                resultRepository.Add(new TestDatabase.Entities.Result(item.Id, "compilation error",
                                    item.TaskId, item.UserId));
                            }
                            else
                            {
                                var tests = testRepository.GetTestsByTaskId(item.TaskId).ToList();
                                for (var i = 0; i < tests.Count; i++)
                                {
                                    var proc = processBuilder.Create(@"F://test.exe");
                                    string output = null;
                                    try
                                    {
                                        using (ITaskRepository taskRepository = new TaskRepository())
                                        {
                                            Task task = taskRepository.Get(item.TaskId);
                                            if (task != null)
                                            {
                                                int memory = (int) task.Memory*1024*1024;

                                                int time = (int) task.Time*1000;

                                                if (!runer.TryGetResult(proc, tests[i].Input, out output, memory, time))
                                                {
                                                    resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                                        "out of time on test " + (i + 1).ToString(), item.TaskId,
                                                        item.UserId));
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                            "out of memory on test " + (i + 1).ToString(), item.TaskId, item.UserId));
                                        return;
                                    }

                                    if (output == tests[i].Output) continue;
                                    resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                        "wrong answer on test " + (i + 1).ToString(), item.TaskId, item.UserId));
                                    return;
                                }
                            }

                            resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                "success", item.TaskId, item.UserId));
                        });
                    }
                }
            }
        }
    }
}
