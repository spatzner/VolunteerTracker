using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using VolunteerTracker.Tests.Common;

namespace VolunteerTracker.Blazor.PlaywrightTests.Tests;

#pragma warning disable CA1861

[TestClass]
public class IndividualActionTests : PageTest
{
    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task CanAddNewIndividual()
    {
        //Add
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "" })).ToBeVisibleAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync(); 
        await Expect(Page.GetByText("Edit Individual Title First")).ToBeVisibleAsync();
        await Page.GetByLabel("Title").FillAsync("Mr");
        await Page.GetByLabel("First Name").FillAsync("Cheddar");
        await Page.GetByLabel("Middle Name").FillAsync("A");
        await Page.GetByLabel("Last Name").FillAsync("Warren");
        await Page.GetByLabel("Suffix").FillAsync("Jr");
        await Page.GetByLabel("Address 1").FillAsync("1234 Main St");
        await Page.GetByLabel("Address 2").FillAsync("Ste 2");
        await Page.GetByLabel("City").FillAsync("Provo");
        await Page.GetByLabel("State").FillAsync("Virginia");
        await Page.GetByLabel("Zip").FillAsync("33525");

        await Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Type").SelectOptionAsync(new[] { "Mobile" });
        await Page.GetByLabel("Number").FillAsync("2315236998");
        await Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Type").SelectOptionAsync(new[] { "Work" });
        await Page.GetByLabel("Address", new() { Exact = true }).FillAsync("CArmstrong@gmail.com");
        await Page.GetByLabel("Notes").FillAsync("Notes for Chaz");
        
        //Save
        await Page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        //Refresh data and target new entry
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByRole(AriaRole.Textbox).First.FillAsync("Warren");
        await Expect(Page.Locator("tbody > tr > td:nth-child(2)").GetByText("Warren")).ToBeVisibleAsync();
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

    [TestMethod]
    [TestCategory(TestType.Integration)]
    public async Task CanEditIndividual()
    {
        //Open Edit Window
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Expect(Page.GetByRole(AriaRole.Textbox).First).ToBeVisibleAsync();
        await Page.GetByRole(AriaRole.Textbox).First.FillAsync("VonCheddar");
        await Expect(Page.Locator("tbody > tr > td:nth-child(2)").GetByText("VonCheddar")).ToBeVisibleAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
        await Expect(Page.GetByText("Edit Individual Title First")).ToBeVisibleAsync();

        //Edit fields
        await Page.GetByLabel("Title").FillAsync("Mrs");
        await Page.GetByLabel("First Name").FillAsync("Janet");
        await Page.GetByLabel("Middle Name").FillAsync("A");
        await Page.GetByLabel("Last Name").FillAsync("VanCheddar");
        await Page.GetByLabel("Suffix").FillAsync("MBA");
        await Page.GetByLabel("Address 1").FillAsync("66100 S Elm St");
        await Page.GetByLabel("Address 2").FillAsync("Apt 4");
        await Page.GetByLabel("City").FillAsync("Reno");
        await Page.GetByLabel("State").FillAsync("Nevada");
        await Page.GetByLabel("Zip").FillAsync("33667");
        await Page.GetByLabel("Type").First.SelectOptionAsync(new[] { "Home" });
        await Page.GetByLabel("Number").First.FillAsync("2315236998");
        await Page.GetByLabel("Type").Nth(1).SelectOptionAsync(new[] { "Fax" });
        await Page.GetByLabel("Number").Nth(1).FillAsync("2315236622");
        await Page.GetByLabel("Type").Nth(2).SelectOptionAsync(new[] { "Home" });
        await Page.GetByLabel("Address").Nth(2).FillAsync("JVanCheddar@raikko.org");
        await Page.GetByLabel("Type").Nth(3).SelectOptionAsync(new[] { "Work" });
        await Page.GetByLabel("Address").Nth(3).FillAsync("JanetLikesPlanets@gmail.com");
        await Page.GetByLabel("Notes").FillAsync("Janet is a very nice person.");
        
        //save
        await Page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        //Refresh data and target edited entry
        await Page.GotoAsync(BlazorEndpoints.Individuals);
        await Page.GetByRole(AriaRole.Textbox).First.FillAsync("VanCheddar");
        await Expect(Page.Locator("tbody > tr > td:nth-child(2)").GetByText("VanCheddar")).ToBeVisibleAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();

        //Verify data
        await Expect(Page.GetByLabel("Title")).ToHaveValueAsync("Mrs");
        await Expect(Page.GetByLabel("First Name")).ToHaveValueAsync("Janet");
        await Expect(Page.GetByLabel("Middle Name")).ToHaveValueAsync("A");
        await Expect(Page.GetByLabel("Last Name")).ToHaveValueAsync("VanCheddar");
        await Expect(Page.GetByLabel("Suffix")).ToHaveValueAsync("MBA");

        await Expect(Page.GetByLabel("Address 1")).ToHaveValueAsync("66100 S Elm St");
        await Expect(Page.GetByLabel("Address 2")).ToHaveValueAsync("Apt 4");
        await Expect(Page.GetByLabel("City")).ToHaveValueAsync("Reno");
        await Expect(Page.GetByLabel("State")).ToHaveValueAsync("Nevada");
        await Expect(Page.GetByLabel("Zip")).ToHaveValueAsync("33667");


        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Main").Nth(1)).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Type").First).ToHaveValueAsync("Home");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Number").First).ToHaveValueAsync("2315236998");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Main").First).Not.ToBeCheckedAsync();

        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Type").Nth(1)).ToHaveValueAsync("Fax");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Number").Nth(1)).ToHaveValueAsync("2315236622");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Phone" }).GetByLabel("Main").Nth(1)).ToBeCheckedAsync();


        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Main").Nth(1)).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Type").First).ToHaveValueAsync("Home");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Address").First).ToHaveValueAsync("JVanCheddar@raikko.org");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Main").First).ToBeCheckedAsync();

        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Type").Nth(1)).ToHaveValueAsync("Work");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Address").Nth(1)).ToHaveValueAsync("JanetLikesPlanets@gmail.com");
        await Expect(Page.GetByRole(AriaRole.Group, new() { Name = "Email" }).GetByLabel("Main").Nth(1)).Not.ToBeCheckedAsync();


        await Expect(Page.GetByLabel("Notes")).ToHaveValueAsync("Janet is a very nice person.");
    }
}