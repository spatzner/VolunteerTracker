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

[TestClass]
public class IndividualsActionTests : PageTest
{
    //TODO: remove ignore
    [TestMethod]
    [TestCategory(TestType.Integration)]
    [Ignore]
    public async Task CanAddNewIndividual()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
        await Page.GetByLabel("Title").ClickAsync();
        await Page.GetByLabel("Title").FillAsync("Mr");
        await Page.GetByLabel("Title").PressAsync("Tab");
        await Page.GetByLabel("First Name").FillAsync("Cheddar");
        await Page.GetByLabel("First Name").PressAsync("Tab");
        await Page.GetByLabel("Middle Name").FillAsync("A");
        await Page.GetByLabel("Middle Name").PressAsync("Tab");
        await Page.GetByLabel("Last Name").FillAsync("Armstrong");
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
        await Page.GetByLabel("Number").ClickAsync();
        await Page.GetByLabel("Number").FillAsync("2315236998");
        await Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByRole(AriaRole.Button).ClickAsync();
        await Page.GetByLabel("Address", new() { Exact = true }).ClickAsync();
        await Page.GetByLabel("Address", new() { Exact = true }).FillAsync("CArmstrong@gmail.com");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        await Expect(Page.Locator("tbody")).ToContainTextAsync("Armstrong");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Cheddar");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("1234 Main St, Ste 2Provo, Virginia, 33525");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("231-523-6998");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("CArmstrong@gmail.com");

    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByLastNameIsBeginsWith()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByRole(AriaRole.Textbox).First.ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).First.FillAsync("bart");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Bartell");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Bartoletti");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Barton");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByFirstNameIsBeginsWith()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByRole(AriaRole.Textbox).Nth(1).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).Nth(1).FillAsync("Gl");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Glen");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Gloria");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Glenna"); 
    }
    
    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByPhoneNumberIsBeginsWithAndSearchesSecondaryNumbersToo()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByRole(AriaRole.Textbox).Nth(2).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).Nth(2).FillAsync("350");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("350-386-7619 x3792");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("350-241-9664");
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).Nth(2).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Dialog)).ToContainTextAsync("Home: 350-960-8912 x8815");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByEmailIsBeginsWithAndSearchesSecondaryEmailsToo()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByRole(AriaRole.Textbox).Nth(3).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).Nth(3).FillAsync("marci");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Marcia_Von@yahoo.com");
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).First.ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Dialog)).ToContainTextAsync("Work: Marcia.Frami78@yahoo.com");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task LastPageButtonGoesToLastPage()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.Locator("nav > ul > li:nth-child(10) > .page-link").ClickAsync(); 
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Zulauf");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("100");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task FirstPageButtonGoesToFirstPage()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.Locator("nav > ul > li:nth-child(10) > .page-link").ClickAsync();
        await Page.Locator(".page-link").First.ClickAsync();
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("1");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Abbott");
    }
    
    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task NextPageButtonGoesToNextPage()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.Locator("li:nth-child(9) > .page-link").ClickAsync();
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Aufderhar");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("2");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task PreviousPageButtonGoesToPreviousPage()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByText("3", new() { Exact = true }).ClickAsync();
        await Page.Locator("li:nth-child(2) > .page-link").ClickAsync();
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Aufderhar");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("2");
    }
    
    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task ClickingPageNumberTakesYouToThatPage()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByText("4", new() { Exact = true }).ClickAsync();
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Barton");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("4");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task PagingShowCurrentRecordsOutOfTotal()
    {
        await Page.GotoAsync("https://localhost:7231/individuals");
        await Page.GetByText("4", new() { Exact = true }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Article)).ToContainTextAsync("31 - 40 of 997 items");
    }
}

    //TODO: View shows all data
    //TODO: Can Edit Each field, including add / delete child entities
    //TODO: Required fields are enforced

