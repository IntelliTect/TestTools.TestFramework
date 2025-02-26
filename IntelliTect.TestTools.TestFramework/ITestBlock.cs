namespace IntelliTect.TestTools.TestFramework
{
    public interface ITestBlock
    {
        ITestCaseLogger? Log { get; }
        public void PreBlockExecution();
        public void PostBlockExecution();
        //public virtual void Test()
        //{
        //    Log?.Info("Testing!");
        //}
    }
}