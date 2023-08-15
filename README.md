# PlaywrightPlayground

Welcome to my PlaywrightPlayground! This repository is a testament to my journey in learning Playwright, and serves as a platform for showcasing my solutions to different test automation practice exercises.
It also demonstrates the various capabilities of Playwright.

While the solutions provided are my own, I believe that there are multiple ways to approach these exercises. 
I welcome any alternative solutions, questions, or general feedback you may have. Feel free to open an issue to start a discussion.

## Table of contents

- [Contributions](#contributions)
- ...

## Visual Studio Tips
Before starting coding it is good to have your IDE setup to fit your preferences and the project you will be working on. In this section I'll describe how I've prepared my IDE to optimize my workflow.

### Item templates
When working with Page Object Models it's a great time saver to create templates for them. In the project I'm using very simple tamplate:
```cs
// Page Object Model template
internal class AddRemovePage : BasePage
{
    public AddRemovePage(IPage page) : base(page){}
}
```
To setup your own templates you can fallow this [IAmTimCorey](https://youtu.be/3uYN3mDFP-o) tutorial.

### Namespace declarations
In the project I'm using File Scoped Namespaces introduced in C#10. Namesapces are set to be Block scoped by default in VS2022, but you can change it in **Tools** -> **Options** -> **Text Editor** -> **C#** -> **Code Style**. Look for Namespace declarations under Code block preferences section and set it to File scoped.

### Line length guideline
For better code readability I prefer to stick with a rule of lines no longer than 80 characters. It is not a set in stone value, but I try to fallow it if possible.
To setup a guideline I recommend Visual Studio extension Editor Guidelines by Paul Harrington.

### Test run settings
To configure test enviroment in Visual Studio you can add test.runsettings file into your solution. For example, configuration setting playwright to run in HEADED mode (with a browser visible) you can use this configuration.

```xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <RunConfiguration>
    <EnvironmentVariables>
      <HEADED>1</HEADED>
    </EnvironmentVariables>
  </RunConfiguration>
</RunSettings>
```

If Visual Studio doesn't detect your configuration automatically go to **Test** -> **Configure Run Settings** -> **Select Solution Wide runsettings file** and choose your file manually.

## Guides

As I'm learning Playwright and good testing practices I'll be descrabing it all in this readme. I hope it will serve as a fast start for people wanting to get into automation or as a good quick refresher for me from the future. When studying Playwright I'm using mix of [Official Documentation](https://playwright.dev/dotnet/docs/intro), Stackoverflow, ChatGpt, Youtube. If a concept is unclear to you I suggest you try to find answers there. Starting a discussion in the Issues tab is always welcome aswell.

### What to know before starting with this guide

Presented here is my first Playwright project. I have to note tho that I have previous experience in Programming, Software Developement and Testing. 
I'll try do go in detail into anything that I've learned about Playwright or testing in general and present the best practices in this guide. I will hovever assume that you have some programming knowladge.

### Codegen
Playwright code generator is a great tool for speeding up the process of automation. It allows you to interact with the page as a user would and generates the code to later simulate performed interactions.
When accessing it for the first time fallow those steps:
1. Make sure playwright is added to your project.
2. Compile the project.
3. Open PowerShell and navigate to your project \bin\Debug\net7.0\ (change the net version if necessery)
4. Execute .\playwright install (to install all the necessery webdrivers)
5. Execute .\playwright codegen

When you get it up and running it's pretty straightfoward from there. Iteract with the page and copy the generated parts you need to your codebase.

### Page Object Models
Page Object Model is the most important Design Pattern in test automation. It allows to separate test logic from the code operating the page. It also makes the code that allows for page interaction more reusable, and the tests more readable. Every tested page in this project is represented by the specific page object class. Some more complex pages may be composed of different components. Page objects will reflect that by also being composed of a smaller object models. 

Each Page is a child of a BasePage class. When starting the project the BasePage only holds the IPage object. When developing the tests and page objects the base class may get extended with methods that are not specific to a ceratain page, like refreshing the page or moving the mouse out of viewport. It's a good place to note - don't develop the page objects more than you need. Only add the functionality you're going to use in tests. First think of what you actually need, don't write a function for every page element - it's a waste of time.

```cs
internal class BasePage
{
    protected readonly IPage page;

    public BasePage(IPage page)
    {
         this.page = page;
    }
}
```

Each page object model class has the same structure. From the top:
 - private properties and methods identifying elements of the page.
 - constructor
 - public methods allowing for page interaction
 - private helper methods

```cs
internal class AddRemovePage : BasePage
{
    private ILocator AddButton =>
        page.GetByRole(AriaRole.Button, new() { Name = "Add Element" });

    private ILocator DeleteButton(int index) =>
        page.GetByRole(AriaRole.Button, new() { Name = "Delete" }).Nth(index);

    public AddRemovePage(IPage page) : base(page){}

    public async Task ClickAddButton()
    {
        await AddButton.ClickAsync();
    }

    public async Task ClickDeleteButton(int index)
    {
        await DeleteButton(index).ClickAsync();
    }
}
```
### Getting property of the element with Playwright

If you come from a selenium background your first instinct for getting a property of the element might be using the *GetAttributeAsync* method. It however won't get you any properties. For that you should use *EvaluateAsync<string>* such as:

```cs
    var propertyValue = await image(index).EvaluateAsync<string>($"element => element.{propertyName}");
```

### Screenshot on failure

For additional information about the failure we can instruct Playwright to take a screenshot. To achive that we can use TearDown method in a BaseTest class.

```cs
[TearDown]
public async Task TearDown()
{
    if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
    {
        var testName = TestContext.CurrentContext.Test.MethodName ?? "MISSING_TEST_NAME";
        var screenshotName = testName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";
        await Page.ScreenshotAsync(new()
        {
            Path = Path.Combine(Globals.GetScreenshotFolderPath(), screenshotName),
            FullPage = true
        });
    }
}
```

First we check if the test failed. If so, we get the test method name (you can also use TestName if you have one), we add the DateTime and use ScreenshotAsync to take a screenshot.
To make sure the tests run on different machines we generate the screenshot folder path using a static method that generates the path based on Environment.CurrentDirectory.

```cs
public static string GetScreenshotFolderPath()
{
    var workingDirectory = Environment.CurrentDirectory;
    var parentPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
    return Path.Combine(parentPath, "Screenshots" );
}
```

## Contributions

Your suggestions and contributions are always welcome! If you know of any good sites that offer QA or automation exercises, or if you have any general suggestions, don't hesitate to let me know. 

## Acknowledgements 

Thank you to all the awsome creators that provide these exercises, making it possible for people like me to learn, practice, and improve automation skills. 
