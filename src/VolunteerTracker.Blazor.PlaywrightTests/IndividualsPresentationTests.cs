using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using VolunteerTracker.Tests.Common;

namespace VolunteerTracker.Blazor.PlaywrightTests;

[TestClass]
public class IndividualsPresentationTests : PageTest
{
    
    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task IndividualsTableHasHeadersVisible()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Actions" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Last Name" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "First Name" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Location" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Phone" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Email" })).ToBeVisibleAsync();
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task IndividualsTableHasFilters()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Expect(Page.GetByRole(AriaRole.Textbox).First).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Textbox).Nth(1)).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Textbox).Nth(2)).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Textbox).Nth(3)).ToBeVisibleAsync();
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task IndividualsTableHasRowButtons()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Expect(Page.Locator("tbody tr:nth-child(1) td:nth-child(1)").GetByRole(AriaRole.Button).Nth(0)).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(1) td:nth-child(1)").GetByRole(AriaRole.Button).Nth(1)).ToBeVisibleAsync();
    }


    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task IndividualsTableHasDataOnLoad()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Expect(Page.Locator("tbody tr:nth-child(1) td:nth-child(2)")).ToContainTextAsync("Abbott");
        await Expect(Page.Locator("tbody tr:nth-child(1) td:nth-child(3)")).ToContainTextAsync("Kiarra");
        await Expect(Page.Locator("tbody tr:nth-child(1) td:nth-child(4)")).ToContainTextAsync("19889 Mattie FieldsJansville, Colorado, 52244");
        await Expect(Page.Locator("tbody tr:nth-child(1) td:nth-child(5)")).ToContainTextAsync("1-999-281-5133");
        await Expect(Page.Locator("tbody tr:nth-child(1) td:nth-child(6)")).ToContainTextAsync("Kiarra95@hotmail.com");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task IndividualsLoadTenRows()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Expect(Page.Locator("tbody tr:nth-child(1)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(2)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(3)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(4)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(5)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(6)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(7)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(8)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(9)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(10)")).ToBeVisibleAsync();
        await Expect(Page.Locator("tbody tr:nth-child(11)")).Not.ToBeVisibleAsync();
    }


    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task IndividualsTableHasPagination()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "2 3 4 5" })).ToBeVisibleAsync();
    }
}

//TODO: View shows all data

[TestClass]
public class IndividualPresentationTests : PageTest
{
    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task CanAddNewIndividual()
    {
        //Add
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
        await Page.GetByLabel("Title").ClickAsync();
        await Page.GetByLabel("Title").FillAsync("Mr");
        await Page.GetByLabel("Title").PressAsync("Tab");
        await Page.GetByLabel("First Name").FillAsync("Cheddar");
        await Page.GetByLabel("First Name").PressAsync("Tab");
        await Page.GetByLabel("Middle Name").FillAsync("A");
        await Page.GetByLabel("Middle Name").PressAsync("Tab");
        await Page.GetByLabel("Last Name").FillAsync("Warren");
        await Page.GetByLabel("Last Name").PressAsync("Tab");
        await Page.GetByLabel("Suffix").FillAsync("Jr");
        await Page.GetByLabel("Suffix").PressAsync("Tab");
        await Page.GetByLabel("Address 1").FillAsync("1234 Main St");
        await Page.GetByLabel("Address 1").PressAsync("Tab");
        await Page.GetByLabel("Address 2").FillAsync("Ste 2");
        await Page.GetByLabel("Address 2").PressAsync("Tab");
        await Page.GetByLabel("City").FillAsync("Provo");
        await Page.GetByLabel("City").PressAsync("Tab");
        await Page.GetByLabel("State").FillAsync("Virginia");
        await Page.GetByLabel("State").PressAsync("Tab");
        await Page.GetByLabel("Zip").FillAsync("33525");
        await Page.GetByLabel("Zip").PressAsync("Tab");
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByRole(AriaRole.Button).ClickAsync();
        await Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Type").SelectOptionAsync(new[] { "Mobile" });
        await Page.GetByLabel("Number").ClickAsync();
        await Page.GetByLabel("Number").FillAsync("2315236998");
        await Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByRole(AriaRole.Button).ClickAsync();
        await Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Type").SelectOptionAsync(new[] { "Work" });
        await Page.GetByLabel("Address", new() { Exact = true }).ClickAsync();
        await Page.GetByLabel("Address", new() { Exact = true }).FillAsync("CArmstrong@gmail.com");
        await Page.GetByLabel("Notes").ClickAsync();
        await Page.GetByLabel("Notes").FillAsync("Notes for Chaz");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        
        //Refresh data and target new entry
        await Page.GotoAsync("https://localhost:7231/individuals"); 
        await Page.GetByRole(AriaRole.Textbox).First.ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).First.FillAsync("Warren");
        await Page.GetByRole(AriaRole.Row, new() { Name = "  Warren Cheddar 1234" }).GetByRole(AriaRole.Button).Nth(1).ClickAsync();

        //Verify data
        await Expect(Page.GetByLabel("Title")).ToHaveValueAsync("Mr");
        await Expect(Page.GetByLabel("First Name")).ToHaveValueAsync("Cheddar");
        await Expect(Page.GetByLabel("Middle Name")).ToHaveValueAsync("A");
        await Expect(Page.GetByLabel("Last Name")).ToHaveValueAsync("Warren");
        await Expect(Page.GetByLabel("Suffix")).ToHaveValueAsync("Jr");
        
        await Expect(Page.GetByLabel("Address 1")).ToHaveValueAsync("1234 Main St");
        await Expect(Page.GetByLabel("Address 2")).ToHaveValueAsync("Ste 2");
        await Expect(Page.GetByLabel("City")).ToHaveValueAsync("Provo");
        await Expect(Page.GetByLabel("State")).ToHaveValueAsync("Virginia");
        await Expect(Page.GetByLabel("Zip")).ToHaveValueAsync("33525");
        
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Type")).ToHaveValueAsync("Mobile");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Number")).ToHaveValueAsync("2315236998");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Main")).ToBeCheckedAsync();

        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Type")).ToHaveValueAsync("Work");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Address")).ToHaveValueAsync("CArmstrong@gmail.com");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Main")).ToBeCheckedAsync();

        await Expect(Page.GetByLabel("Notes")).ToHaveValueAsync("Notes for Chaz");

    }
}


    //TODO: Can Edit Each field, including add / delete child entities
    //TODO: Required fields are enforced

