using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunterBot
{
    public class HeadHunterClient
    {
        public HeadHunterClient(){}
        public async Task AuthorizationHeadHunter(string login, string password)
        {
            using var playwright = await Playwright.CreateAsync();
            await using IBrowser browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    SlowMo = 100,
                    Timeout = 10000,
                });

            BrowserNewContextOptions device = playwright.Devices["Desktop Chrome"];
            await using IBrowserContext context = await browser.NewContextAsync(device);
            IPage page = await context.NewPageAsync();

            await page.GotoAsync("https://hh.ru/account/login");
            await page.ClickAsync("button[data-qa='expand-login-by-password']");
            await page.TypeAsync("input[data-qa='login-input-username']", login);
            await page.TypeAsync("input[data-qa='login-input-password']", password);
            await page.ClickAsync("button[data-qa='account-login-submit']");
            var incorrectPassword = await page.IsVisibleAsync("div[data-qa='account-login-error']");
            if (incorrectPassword)
            {
                throw new Exception("Incorrect password");
            }
        }
    }
}
