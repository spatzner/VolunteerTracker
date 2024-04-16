using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using VolunteerTracker.Tests.Common;

namespace VolunteerTracker.Blazor.PlaywrightTests.Tests;

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

//TODO: Can Edit Each field, including add / delete child entities
//TODO: Required fields are enforced

