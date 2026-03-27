using IntelliTect.TestTools.TestFramework.Tests.TestData.Dependencies;
using IntelliTect.TestTools.TestFramework.Tests.TestData.TestBlocks;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace IntelliTect.TestTools.TestFramework.Tests.TestCaseTests
{
    public class FinallyExecutionTests
    {
        [Fact]
        public async Task NoExceptionsWhenAllBlocksAndFinallyBlocksPass()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddFinallyBlock<ExampleFinallyBlock>(true)
                .Build();

            // Act
            await tc.ExecuteAsync();

            // Assert
            Assert.True(tc.Passed, "Test case did not get marked as Passed when we expected it.");
        }

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
            var ex = await Assert.ThrowsAsync<AggregateException>(() => tc.ExecuteAsync());

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
            var ex = await Assert.ThrowsAsync<AggregateException>(() => tc.ExecuteAsync());

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
            await tc.ExecuteAsync();

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
            await Assert.ThrowsAsync<TestCaseException>(() => tc.ExecuteAsync());

            // Assert
            Assert.False(tc.Passed, "Test case did not get marked as Failed when we expected it.");
        }

        [Fact]
        public async Task NoExceptionsWhenAllBlocksAndAsyncFinallyBlocksPass()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddAsyncFinallyBlock<ExampleAsyncFinallyBlock>(true)
                .Build();

            // Act
            await tc.ExecuteAsync();

            // Assert
            Assert.True(tc.Passed, "Test case did not get marked as Passed when we expected it.");
        }

        [Fact]
        public async Task AsyncFinallyBlockThrowsExpectedExceptionWhenNotOverridingDefaultFinallyBehavior()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddAsyncFinallyBlock<ExampleAsyncFinallyBlock>()
                .Build();

            // Act
            var ex = await Assert.ThrowsAsync<AggregateException>(() => tc.ExecuteAsync());

            // Assert
            Assert.NotNull(ex.InnerExceptions);
            Assert.Single(ex.InnerExceptions);
            Assert.Contains("Test case succeeded",
                ex.Message,
                StringComparison.InvariantCultureIgnoreCase);
            Assert.True(tc.Passed, "Test case did not get marked as Passed when we expected it.");
        }

        [Fact]
        public async Task TestBlockAndAsyncFinallyBlockThrowsExpectedExceptionWhenNotOverridingDefaultFinallyBehavior()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(false)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddAsyncFinallyBlock<ExampleAsyncFinallyBlock>()
                .Build();

            // Act
            var ex = await Assert.ThrowsAsync<AggregateException>(() => tc.ExecuteAsync());

            // Assert
            Assert.NotNull(ex.InnerExceptions);
            Assert.Equal(2, ex.InnerExceptions.Count);
            Assert.Contains("Test case failed and finally blocks failed",
                ex.Message,
                StringComparison.InvariantCultureIgnoreCase);
            Assert.False(tc.Passed, "Test case did not get marked as Failed when we expected it.");
        }

        [Fact]
        public async Task AsyncFinallyBlockDoesNotThrowExceptionWhenOverridingDefaultFinallyBehavior()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddAsyncFinallyBlock<ExampleAsyncFinallyBlock>()
                .Build();
            tc.ThrowOnFinallyBlockException = false;

            // Act
            await tc.ExecuteAsync();

            // Assert
            Assert.True(tc.Passed, "Test case did not get marked as Passed when we expected it.");
        }

        [Fact]
        public async Task OnlyTestBlockThrowsExpectedExceptionWhenOverridingDefaultFinallyBehaviorWithAsyncFinallyBlock()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(false)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddAsyncFinallyBlock<ExampleAsyncFinallyBlock>()
                .Build();
            tc.ThrowOnFinallyBlockException = false;

            // Act
            await Assert.ThrowsAsync<TestCaseException>(() => tc.ExecuteAsync());

            // Assert
            Assert.False(tc.Passed, "Test case did not get marked as Failed when we expected it.");
        }

        [Fact]
        public async Task TestCasePassedIsSetTrueBeforeFinallyBlocksRun()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(true)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddFinallyBlock<ExampleBlockCheckingTestSuccess>()
                .Build();

            // Act
            await tc.ExecuteAsync();

            // Assert
            Assert.True(tc.Passed, "Test case did not get marked as Passed when we expected it.");
        }

        [Fact]
        public async Task TestCasePassedIsSetTrueEvenIfFinallyBlockFails()
        {
            // Arrange
            TestCase tc = new TestBuilder()
                .AddDependencyInstance(false)
                .AddTestBlock<ExampleTestBlockWithBoolReturn>()
                .AddFinallyBlock<ExampleBlockCheckingTestSuccess>()
                .Build();

            // Act
            AggregateException exception = await Assert.ThrowsAsync<AggregateException>(() => tc.ExecuteAsync());

            // Assert
            Assert.Equal(2, exception.InnerExceptions.Count);
            Assert.Equal(typeof(DivideByZeroException), exception.InnerException?.GetType());
            Assert.Equal(typeof(DivideByZeroException), exception.InnerExceptions[0].GetType());
            Assert.Equal(typeof(TrueException), exception.InnerExceptions[1].GetType());
            Assert.False(tc.Passed, "Test case was marked as Passed when we did not expected it.");
        }
    }
}
