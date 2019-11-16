using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnitSelenium.pages;
using System.Threading;
//using SeleniumExtras.PageObjects;

namespace NUnitSelenium
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [OneTimeTearDown]
        public void Close()
        {
            driver.Close();
            driver.Quit();
        }

        [TestCase("Russian Federation", "Russian", 22)]
        [TestCase("Romania", "English", 32)]
        public void NumberOfVacanciesCheck(string country ,string language, int jobsExpected)
        {
            VeeamCareersPage careersPage = new VeeamCareersPage(driver);

            SetCountry(careersPage, country);
            SetLanguage(careersPage, language);

            int jobsFound = FindJobsNumber(careersPage);
            Assert.AreEqual(jobsExpected, jobsFound, "Количество вакансий не совпадает");
        }

        static void SetCountry(VeeamCareersPage careersPage, string country)
        {
            careersPage.CountrySelection.Click();
            IWebElement CountryToSet = careersPage.CountryToSet(country);
            CountryToSet.Click();
        }

        static void SetLanguage(VeeamCareersPage careersPage, string language)
        {
            careersPage.LanguageSelection.Click();
            IWebElement LanguageToSet = careersPage.LanguageToSet(language);
            LanguageToSet.Click();
            Thread.Sleep(3000);
            careersPage.LanguageSelection.Click();
        }

        static int FindJobsNumber(VeeamCareersPage careersPage)
        {
            string jobsString = careersPage.NumberOfVacanciesText.Text;
            string[] words = jobsString.Split(new char[] { ' ' });
            int jobsFromSite = Convert.ToInt32(words[0]);

            return jobsFromSite;
        }
    }
}