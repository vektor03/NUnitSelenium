using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace NUnitSelenium.pages
{
    class VeeamCareersPage
    {
        public readonly IWebDriver driver;

        private IWebElement countrySelection;
        public IWebElement CountrySelection
        {
            get
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(countrySelection);
                return countrySelection;
            }
        }

        private IWebElement languageSelection;
        public IWebElement LanguageSelection
        {
            get
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(languageSelection);
                return languageSelection;
            }
        }

        public IWebElement NumberOfVacanciesText
        {
            get
            {
                Thread.Sleep(3000);
                return driver.FindElement(By.XPath("//h3[contains(.,' jobs found')]"));
            }
        }

        const string webAdress = "https://careers.veeam.com/";

        public VeeamCareersPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(webAdress);

            countrySelection = driver.FindElement(By.ClassName("vacancies-filter-select"));
            languageSelection = driver.FindElement(By.Id("language"));
        }  

        /// <summary>
        /// Элемент с названием страны из выпадающего списка
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public IWebElement CountryToSet(string country)
        {
            IWebElement CToSet = driver.FindElement(By.CssSelector("span[data-value='" + country+ "'][class^='selecter-item']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView()", CToSet);

            return CToSet;
        }

        /// <summary>
        /// Элемент с названием языка из выпадающего списка
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public IWebElement LanguageToSet(string language)
        {
            IWebElement LToSet = driver.FindElement(By.XPath("//fieldset/label[contains(.,'" + language + "')] | //*[class='controls-checkbox']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView()", LToSet);

            return LToSet;
        }
    }
}