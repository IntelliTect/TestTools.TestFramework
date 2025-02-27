
TestFramework
==========

IntelliTect's TestFramework helps manage test dependencies and data sharing in a highly visible and easy-to-read way. It adds a layer between your unit test method and executing code to manage the things that, in complex projects, often get abstracted away out of visibility. The goal is to provide a way to manage those dependencies without hiding them, so even people new to the project can start writing tests quickly. Under the covers, it's using dependency injection to inject test data and dependencies into test blocks only when needed. Test Framework uses a builder pattern to help facilitate highly composable tests with minimal extraneous code.

Current implementation is in C#, however the pattern is language agnostic and could be implemented in other languages.

Design
-----
TestFramework works within existing unit test frameworks and runners to help keep dependencies visible and easily reusable. You still execute your tests using NUnit, xUnit, etc. Your tests still run against a class, database connection, rest client, UI driver, etc.

Usage
-----
To start, you need some executable code. Your executable code needs to live in TestBlocks. A test block is any code that derives from the TestBlock class OR implements the ITestBlock interface and has an Execute() method:
```
using IntelliTect.TestTools.TestFramework;

namespace ExampleTests.TestBlocks;

internal class VerifyTrueisTrue : ITestBlock
{
   public void Execute()
   {
      Assert.True(true);
   }
}

```

Next, in your unit test method instantiate a new TestBuilder:
```
[Fact]
public void Test1()
{
   TestBuilder builder = new();
}
```
This builder object is then used to manage everything about the test and constituent test blocks. In simple cases, you can add your test block and execute without any further setup The whole unit test looks like:
```
[Fact]
public void Test1()
{
   TestBuilder builder = new();

   builder.AddTestBlock<TestBlocks.VerifyTrueisTrue>()

   TestCase test = builder.Build()
   test.ExecuteTestCase();
}
```
Each test block can:
- Be standalone
- Have dependencies injected from external sources
- Have dependencies injected from other test blocks
- Create or modify dependencies for other test blocks

This is a simple example, and frankly would be easier as its own unit test. TestFramework shines, though, when dependencies start to become complex. While agnostic to unit, database, API, or UI testing, particularly complex API and UI testing is where we most commonly implement Test Framework. If we were to convert this to do something more interesting, we can start to see how to configure tests for different scenarios in a more interesting fashion.

If we take a more complex example, in this case still a relatively simple Selenium test, you can compose something like this:

```
[Fact]
public void Test1()
{
   var expectedResult = new SiteStatus
   {
      IsHeaderAvailable = true,
      IsBodyAvailable = true
   };

   TestBuilder builder = new();
   builder
      .AddLogger<NewLogger>()
      .AddDependencyService<IWebDriver>(new WebDriverFactory("Chrome").Driver)
      .AddDependencyService<Harness.IntelliTectWebpage>()
      .AddTestBlock<TestBlocks.NavigateToWebsite>()
      .AddTestBlock<TestBlocks.VerifyWebsiteBodyIsDisplayed>(expectedResult)
      .ExecuteTestCase();
}
```

This takes care of needing to have code in the test to wire up the logger, Selenium / WebDriver, and will produce errors if you (for example) add a test black that needs a dependency that isn't supplied by the test. This gives you an easy way to move complex code out of your tests and expose human-readable hooks for composing tests. 

See the full code here: [Example Tests](https://github.com/IntelliTect/TestTools.TestFramework/tree/update-docs/ExampleTests/ExampleTests)
