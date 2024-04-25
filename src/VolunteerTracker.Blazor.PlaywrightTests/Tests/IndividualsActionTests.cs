using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using VolunteerTracker.Tests.Common;

namespace VolunteerTracker.Blazor.PlaywrightTests.Tests;

[TestClass]
public class IndividualsActionTests : PageTest
{
    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByLastNameIsBeginsWith()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByRole(AriaRole.Textbox).First.ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).First.FillAsync("bart");

        await Expect(Page.Locator("tbody > tr > td:nth-child(2)").GetByText("Bartell")).Not.ToHaveCountAsync(0);
        await Expect(Page.Locator("tbody > tr > td:nth-child(2)").GetByText("Bartoletti")).Not.ToHaveCountAsync(0);
        await Expect(Page.Locator("tbody > tr > td:nth-child(2)").GetByText("Barton")).Not.ToHaveCountAsync(0);
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByFirstNameIsBeginsWith()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByRole(AriaRole.Textbox).Nth(1).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).Nth(1).FillAsync("gl");

        await Expect(Page.Locator("tbody > tr > td:nth-child(3)").GetByText("Glen")).Not.ToHaveCountAsync(0);
        await Expect(Page.Locator("tbody > tr > td:nth-child(3)").GetByText("Gloria")).Not.ToHaveCountAsync(0);
        await Expect(Page.Locator("tbody > tr > td:nth-child(3)").GetByText("Glenna")).Not.ToHaveCountAsync(0);
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByPhoneNumberIsBeginsWithAndSearchesSecondaryNumbersToo()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByRole(AriaRole.Textbox).Nth(2).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).Nth(2).FillAsync("350");

        await Expect(Page.Locator("tbody > tr > td:nth-child(5)").GetByText("350-386-7619 x3792")).Not.ToHaveCountAsync(0);
        await Expect(Page.Locator("tbody > tr > td:nth-child(5)").GetByText("350-241-9664")).Not.ToHaveCountAsync(0);
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).Nth(2).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Dialog)).ToContainTextAsync("Home: 350-960-8912 x8815");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task SearchByEmailIsBeginsWithAndSearchesSecondaryEmailsToo()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByRole(AriaRole.Textbox).Nth(3).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).Nth(3).FillAsync("marci");

        await Expect(Page.Locator("tbody > tr > td:nth-child(6)").GetByText("Marcia_Von@yahoo.com")).Not.ToHaveCountAsync(0);
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).First.ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Dialog)).ToContainTextAsync("Work: Marcia.Frami78@yahoo.com");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task LastPageButtonGoesToLastPage()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.Locator("nav > ul > li:nth-child(10) > .page-link").ClickAsync();
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Zulauf");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("100");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task FirstPageButtonGoesToFirstPage()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.Locator("nav > ul > li:nth-child(10) > .page-link").ClickAsync();
        await Page.Locator(".page-link").First.ClickAsync();
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("1");
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Abbott");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task NextPageButtonGoesToNextPage()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.Locator("li:nth-child(9) > .page-link").ClickAsync();
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Aufderhar");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("2");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task PreviousPageButtonGoesToPreviousPage()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByText("3", new() { Exact = true }).ClickAsync();
        await Page.Locator("li:nth-child(2) > .page-link").ClickAsync();
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Aufderhar");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("2");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task ClickingPageNumberTakesYouToThatPage()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByText("4", new() { Exact = true }).ClickAsync();
        await Expect(Page.Locator("tbody")).ToContainTextAsync("Barton");
        await Expect(Page.GetByLabel("Pagination links").GetByRole(AriaRole.Button)).ToContainTextAsync("4");
    }

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task PagingShowCurrentRecordsOutOfTotal()
    {
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByText("4", new() { Exact = true }).ClickAsync();
#pragma warning disable SYSLIB1045
        await Expect(Page.GetByRole(AriaRole.Article)).ToContainTextAsync(new Regex(@"\d+ - \d+ of \d+ items"));
    }
}