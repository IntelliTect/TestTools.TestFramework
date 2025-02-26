namespace IntelliTect.TestTools.TestFramework
{
    public class TestBlock : ITestBlock
    {
        public ITestCaseLogger? Log { get; set; }

        public virtual void PreBlockExecution()
        {
            throw new System.NotImplementedException();
        }

        public virtual void PostBlockExecution()
        {
            throw new System.NotImplementedException();
        }
    }
}