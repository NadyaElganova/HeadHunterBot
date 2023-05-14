using HeadHunterBot;
using Microsoft.Playwright;

HeadHunterClient headHunterClient = new HeadHunterClient();
var password = Environment.GetEnvironmentVariable("Password_HH");
var login = "elganova-nadya@mail.ru";
await headHunterClient.AuthorizationHeadHunter(login,password);

Console.ReadKey();
