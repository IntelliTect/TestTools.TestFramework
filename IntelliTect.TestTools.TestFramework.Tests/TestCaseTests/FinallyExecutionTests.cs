using IntelliTect.TestTools.TestFramework.Tests.TestData.Dependencies;
using IntelliTect.TestTools.TestFramework.Tests.TestData.TestBlocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntelliTect.TestTools.TestFramework.Tests.TestCaseTests
{
    public class FinallyExecutionTests
    {

        [Fact]
        public async Task FinallyBlockThrowsExpectedExceptionWhenNotOverridingDefaultFinallyBehavior()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddFinallyBlock<ExampleFinallyBlock>()
                .Build();

            // Act
            var ex = await Assert.ThrowsAsync<AggregateException>(() => tc.Execute());

            // Assert
            Assert.NotNull(ex.InnerExceptions);
            Assert.Single(ex.InnerExceptions);
            Assert.Contains("Test case succeeded", 
                ex.Message, 
                StringComparison.InvariantCultureIgnoreCase);
            Assert.True(tc.Passed, "Test case did not get marked as Passed when we expected it.");
        }

        [Fact]
        public async Task TestBlockAndFinallyBlockThrowsExpectedExceptionWhenNotOverridingDefaultFinallyBehavior()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(false)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddFinallyBlock<ExampleFinallyBlock>()
                .Build();

            // Act
            var ex = await Assert.ThrowsAsync<AggregateException>(() => tc.Execute());

            // Assert
            Assert.NotNull(ex.InnerExceptions);
            Assert.Equal(2, ex.InnerExceptions.Count);
            Assert.Contains("Test case failed and finally blocks failed",
                ex.Message,
                StringComparison.InvariantCultureIgnoreCase);
            Assert.False(tc.Passed, "Test case did not get marked as Failed when we expected it.");
        }

        [Fact]
        public async Task FinallyBlockDoesNotThrowExceptionWhenOverridingDefaultFinallyBehavior()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddFinallyBlock<ExampleFinallyBlock>()
                .Build();
            tc.ThrowOnFinallyBlockException = false;

            // Act
            await tc.Execute();

            // Assert
            Assert.True(tc.Passed, "Test case did not get marked as Passed when we expected it.");
        }

        [Fact]
        public async Task OnlyTestBlockThrowsExpectedExceptionWhenOverridingDefaultFinallyBehavior()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(false)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddFinallyBlock<ExampleFinallyBlock>()
                .Build();
            tc.ThrowOnFinallyBlockException = false;

            // Act
            await Assert.ThrowsAsync<TestCaseException>(() => tc.Execute());

            // Assert
            Assert.False(tc.Passed, "Test case did not get marked as Failed when we expected it.");
        }
    }
}
