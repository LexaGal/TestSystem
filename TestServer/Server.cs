using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            StringBuilder testResultsBuilder = new StringBuilder();
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
                                resultRepository.Add(new TestDatabase.Entities.Result(item.Id, "Compilation error",
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
                                                    testResultsBuilder.AppendLine(
                                                        string.Format(
                                                            "Out of time on test {0}: \n[Input:\n {1}, \nOutput: \n{2}]",
                                                            (i + 1).ToString(), tests[i].Input, tests[i].Output));

                                                    resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                                        string.Format("Out of time on test {0}|{1}", (i + 1).ToString(),
                                                            testResultsBuilder), item.TaskId, item.UserId));
                                                    
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        testResultsBuilder.AppendLine(
                                            string.Format("Out of memory on test {0}: \n[Input:\n {1}, \nOutput: \n{2}]",
                                                (i + 1).ToString(), tests[i].Input, tests[i].Output));

                                        resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                            string.Format("Out of memory on test {0}|{1}", (i + 1).ToString(),
                                                testResultsBuilder), item.TaskId, item.UserId));

                                        return;
                                    }

                                    if (output == tests[i].Output)
                                    {
                                        testResultsBuilder.AppendLine(
                                            string.Format("Test {0}: [\n[Input:\n {1}, \nOutput: \n{2}] passed successfully",
                                                (i + 1).ToString(), tests[i].Input, tests[i].Output));
                                        
                                        continue;
                                    }

                                    testResultsBuilder.AppendLine(
                                        string.Format("Wrong answer on test {0}: \n[Input:\n {1}, \nOutput: \n{2}]",
                                            (i + 1).ToString(), tests[i].Input, tests[i].Output));

                                    resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                        string.Format("Wrong answer on test {0}|{1}", (i + 1).ToString(),
                                            testResultsBuilder), item.TaskId, item.UserId));

                                    return;
                                }
                            }

                            testResultsBuilder.AppendLine("All tests passed successfully");

                            resultRepository.Add(new TestDatabase.Entities.Result(item.Id,
                                string.Format("Success|{0}", testResultsBuilder), item.TaskId, item.UserId));
                        });
                    }
                }
            }
        }
    }
}
