using System;
using Xunit;

namespace IntelliTect.TestTools.TestFramework.Tests;

public class TestDataTests
{
    [Fact]
    public void BlockDataT1T2ThrowsExceptionOnDuplicateTypes()
    {
        var exception = Assert.Throws<TypeInitializationException>(() => 
        {
            var _ = new BlockData<int, int>(1, 2);
        });
        Assert.NotNull(exception.InnerException);
        Assert.Equal(typeof(InvalidOperationException), exception.InnerException.GetType());
        Assert.Equal("Duplicate type found: Int32 appears multiple times. BlockData must use different types to avoid unexpected behavior by the TestCase DI Container.",
            exception.InnerException.Message);
    }

    [Fact]
    public void BlockDataT1T2T3ThrowsExceptionOnDuplicateTypes()
    {
        var exception = Assert.Throws<TypeInitializationException>(() =>
        {
            var _ = new BlockData<int, bool, int>(1, true, 2);
        });
        Assert.NotNull(exception.InnerException);
        Assert.Equal(typeof(InvalidOperationException), exception.InnerException.GetType());
        Assert.Equal("Duplicate type found: Int32 appears multiple times. BlockData must use different types to avoid unexpected behavior by the TestCase DI Container.",
            exception.InnerException.Message);
    }

    [Fact]
    public void BlockDataT1T2T3T4ThrowsExceptionOnDuplicateTypes()
    {
        var exception = Assert.Throws<TypeInitializationException>(() =>
        {
            var _ = new BlockData<bool, double, int, int>(false, 0.5, 1, 2);
        });
        Assert.NotNull(exception.InnerException);
        Assert.Equal(typeof(InvalidOperationException), exception.InnerException.GetType());
        Assert.Equal("Duplicate type found: Int32 appears multiple times. BlockData must use different types to avoid unexpected behavior by the TestCase DI Container.",
            exception.InnerException.Message);
    }

    [Fact]
    public void BlockDataT1T2DoesNotThrowExceptionOnWithUniqueTypes()
    {
        BlockData<int, bool> blockData = new(1, true);
        Assert.Equal(2, blockData.Data.Count);
    }

    [Fact]
    public void BlockDataT1T2T3DoesNotThrowExceptionOnWithUniqueTypes()
    {
        BlockData<ArgumentException, Exception, bool> blockData = new(new ArgumentException(), new Exception(), true);
        Assert.Equal(3, blockData.Data.Count);
    }

    [Fact]
    public void BlockDataT1T2T3T4DoesNotThrowExceptionOnWithUniqueTypes()
    {
        BlockData<int, bool, string, double> blockData = new(1, true, "", 0.5);
        Assert.Equal(4, blockData.Data.Count);
    }
}
