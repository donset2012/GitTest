using System;
using OpenQA.Selenium;

namespace TestingWebsite
{
    class Navigate
    {
        //ClickUSD
        public static void ClickUSD(IWebDriver driver)
        {
            IWebElement usd;
            try
            {
                usd = driver.FindElement(By.XPath(@".//div[2]/div[2]/div[1]/a[1]"));
                usd.Click();
                System.Threading.Thread.Sleep(2 * 1000);
                usd = driver.FindElement(By.LinkText("USD $"));
                usd.Click();
            }
            catch
            {
                Console.WriteLine("Failed to click on the element 'USD $'");
                Logger.Log.Error("Failed to click on the element 'USD $'");
            }
        }


        //Search
        public static void Search(IWebDriver driver)
        {
            IWebElement search;
            try
            {
                search = driver.FindElement(By.ClassName("ui-autocomplete-input"));
                search.Click();
                System.Threading.Thread.Sleep(2 * 1000);
                search.SendKeys("dress.");
                search.Submit();
            }
            catch
            {
                Console.WriteLine("Failed to search");
                Logger.Log.Error("Failed to search");

            }
        }


        //Sort
        public static void Sort(IWebDriver driver)
        {
            IWebElement sort;
            try
            {
                sort = driver.FindElement(By.XPath(@".//section[1]/div[1]/div[1]/section[1]/section[1]/div[1]/div[1]/div[2]/div[1]/div[1]/a[1]"));
                sort.Click();
                System.Threading.Thread.Sleep(1 * 1000);
                sort = driver.FindElement(By.XPath(@".//section[1]/div[1]/div[1]/section[1]/section[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/a[5]"));
                sort.Click();
            }
            catch
            {
                Console.WriteLine("Failed to sort");
                Logger.Log.Error("Failed to sort");
            }
        }
    }
}
