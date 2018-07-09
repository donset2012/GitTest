using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace TestingWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger.InitLogger();
            IWebDriver driver;
            Logger.Log.Info("Launched Browser Chrome");
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            Logger.Log.Info("Going to URL http://prestashop-automation.qatestlab.com.ua/ru/");
            driver.Navigate().GoToUrl("http://prestashop-automation.qatestlab.com.ua/ru/");

            Logger.Log.Info("Check of the currency");
            Check check = new Check();
            Check.CheckCurrency(driver);

            Logger.Log.Info("Click on the USD button");
            Navigate navigate = new Navigate();
            Navigate.ClickUSD(driver);

            Logger.Log.Info("Search for the stuff by entering 'dress.' in the search field");
            Navigate.Search(driver);
            System.Threading.Thread.Sleep(2 * 1000);

            Logger.Log.Info("Check of the stuff");
            Check.CheckStuff(driver);

            Logger.Log.Info("Check of the price in USD");
            Check.CheckPriceUSD(driver);
            System.Threading.Thread.Sleep(1 * 1000);

            Logger.Log.Info("Sort by price from high to low");
            Navigate.Sort(driver);
            System.Threading.Thread.Sleep(1 * 1000);

            Logger.Log.Info("Check sort");
            Check.CheckSort(driver);

            Logger.Log.Info("Check discount");
            Check.CheckDiscount(driver);
        }
    } 
}
