﻿using Newtonsoft.Json;
using OpenQA.Selenium;

namespace ExampleTests.Harness
{
    public class IntelliTectWebpage : BasePage
    {
        public IntelliTectWebpage(IWebDriver driver) : base(driver)
        {
            HomePage = new HomePage(Driver);
            Blogs = new Blogs(Driver);
        }

        [JsonIgnore]
        public HomePage HomePage { get; }
        [JsonIgnore]
        public Blogs Blogs { get; }
    }
}
