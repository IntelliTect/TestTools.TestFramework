using IntelliTect.TestTools.TestFramework.Tests.TestData.TestBlocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntelliTect.TestTools.TestFramework.Tests.TestCaseTests
{
    public class MultipleDependencyTests : TestBase
    {
        [Fact]
        public void ReturnDuplicateTypesDoesNotThrow()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddTestBlock<ExampleTestBlockWithBoolReturn>(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>(true)
                .Build();

            // Act
            tc.Execute();

            // Assert
            Assert.True(tc.Passed);
        }

        [Fact]
        public void FetchByObjectInstanceForMultipleDependencies()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance("Testing")
                .AddDependencyInstance(1234)
                .AddTestBlock<ExampleTestBlockWithMultipleDependencies>()
                .Build();

            // Act
            tc.Execute();

            // Assert
            Assert.True(tc.Passed);
        }

        [Fact]
        public async Task ReturnMultipleObjectsStoresEachObjectSeparately()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddTestBlock<ExampleBlockWithMultipleReturns>(true)
                .AddTestBlock<ExampleTestBlockWithExecuteArg>()
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .Build();

            // Act
            await tc.ExecuteAsync();

            // Assert
            Assert.True(tc.Passed);
        }

        [Fact]
        public async Task ReturnMultipleObjectsExecutesSubsequentTestBlocks()
        {
            // Arrange

            TestCase tc = new TestBuilder()
                .AddTestBlock<ExampleBlockWithMultipleReturns>(false)
                .AddTestBlock<ExampleTestBlockWithExecuteArg>()
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .Build();

            // Act
            var result = await Assert.ThrowsAsync<TestCaseException>(tc.ExecuteAsync);

            // Assert
            Assert.False(tc.Passed);
            Assert.NotNull(result.InnerException);
            Assert.True(result.InnerException.GetType() == typeof(DivideByZeroException));
        }
    }
}
