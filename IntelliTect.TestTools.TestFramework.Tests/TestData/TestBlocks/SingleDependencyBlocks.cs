using IntelliTect.TestTools.TestFramework.Tests.TestData.Dependencies;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntelliTect.TestTools.TestFramework.Tests.TestData.TestBlocks
{
    public class ExampleTestBlockWithExecuteArg : TestBlock
    {
        public void Execute(string input)
        {
            Assert.Equal("Testing", input);
        }
    }

    public class ExampleTestBlockWithProperty : TestBlock
    {
        public string? Input { get; set; }

        public void Execute()
        {
            Assert.Equal("Testing", Input);
        }
    }

    public class ExampleTestBlockWithConstructor : TestBlock
    {
        public ExampleTestBlockWithConstructor(string input)
        {
            Input = input;
        }

        private string Input { get; }

        public void Execute()
        {
            Assert.Equal("Testing", Input);
        }
    }

    public class ExampleTestBlockWithStringReturn : TestBlock
    {
        public string Execute()
        {
            return "Testing";
        }
    }

    public class ExampleTestBlockWithBoolReturn : TestBlock
    {
        public bool Execute(bool arg)
        {
            if (!arg)
            {
                throw new DivideByZeroException("Test failure");
            }
            else
            {
                return false;
            }
        }
    }

    public class SomeTestBlock : ITestBlock
    {
        public ITestCaseLogger? Log { get; }

        public bool Execute(SomeDependency dep)
        {
            Assert.NotNull(dep);
            return true;
        }
    }

    public class PlaywrightError : TestBlock
    {
        public async Task Execute()
        {
            await Task.Delay(1);
            throw new InvalidOperationException("Oops");
            //using IPlaywright pw = await Playwright.CreateAsync();
            //IAPIRequestContext context = await pw.APIRequest.NewContextAsync();
            //await context.GetAsync("http://bad.url.com");
            // Playwright exception thrown for ENOTFOUND
        }
    }
}
