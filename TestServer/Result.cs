namespace TestServer
{
    public class Result
    {
        public Result(bool isPassed, FailReason failReason = FailReason.NoFailes, int failedTestNumber = -1)
        {
            IsPassed = isPassed;
            FailReason = failReason;
            FailedTestNumber = failedTestNumber;
        }

        public bool IsPassed { get; }

        public FailReason FailReason { get; }

        public int FailedTestNumber { get; }
    }
}
