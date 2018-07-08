﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestingWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://prestashop-automation.qatestlab.com.ua/ru/");
            Check check = new Check();
            Check.CheckCurrency(driver);
            Navigate navigate = new Navigate();
            Navigate.ClickUSD(driver);
            Navigate.Search(driver);
            System.Threading.Thread.Sleep(2 * 1000);
            Check.CheckGoods(driver);
            Check.CheckPriceUSD(driver);
            System.Threading.Thread.Sleep(1 * 1000);
            Navigate.Sort(driver);
            System.Threading.Thread.Sleep(1 * 1000);
            Check.CheckSort(driver);
            Check.CheckDiscount(driver);
        }
    }

    public class Check
    {
        public static void CheckCurrency(IWebDriver driver)
        {
            string Currency = driver.FindElement(By.XPath(@".//div[2]/div[2]/div[1]/span[2]")).Text;
            Currency = Regex.Replace(Currency, @"\w", "", RegexOptions.Compiled);
            Currency = Currency.Trim();
            List<IWebElement> a = driver.FindElements(By.ClassName("price")).ToList();
            for (int i = 0; i < a.Count; i++)
            {
                string s = a[i].Text;
                s = Regex.Replace(s, @"\d", "", RegexOptions.Compiled);
                s = s.Trim(',', ' ');
                if (s == Currency)
                {
                    Console.WriteLine("Checked currency of the " + (i + 1) + " stuff");
                }
                else
                {
                    Console.WriteLine("Not Checked");
                }
            }
            Console.WriteLine("\r\n");
        }
        public static void CheckGoods(IWebDriver driver)
        {
            string goods = driver.FindElements(By.ClassName("thumbnail-container")).Count.ToString();
            string goods2 = driver.FindElement(By.XPath(@".//section[1]/section[1]/div[1]/div[1]/div[1]/p[1]")).Text;
            goods2 = Regex.Replace(goods2, @"\D", "", RegexOptions.Compiled);
            goods2 = goods2.Trim();
            if (goods == goods2)
            {
                Console.WriteLine("Товаров: " + goods + "\r\n");
            }
            else
            {
                Console.WriteLine("Can't check goods" + "\r\n");
            }
        }
        public static void CheckPriceUSD(IWebDriver driver)
        {
                string Currency = driver.FindElement(By.XPath(@".//div[2]/div[2]/div[1]/span[2]")).Text;
                Currency = Regex.Replace(Currency, @"\w", "", RegexOptions.Compiled);
                Currency = Currency.Trim();
         
            List<IWebElement> a = driver.FindElements(By.ClassName("price")).ToList();
            for (int i = 0; i < a.Count; i++)
            {
                string s = a[i].Text;
                s = Regex.Replace(s, @"\d", "", RegexOptions.Compiled);
                s = s.Trim(',', ' ');
                if (s == Currency)
                {
                    Console.WriteLine("The currency of the " + (i + 1) + " stuff: " + s);
                }
                else
                {
                    Console.WriteLine("The currency doesn't match");
                }
            }
            Console.WriteLine("\r\n");
        }
        public static void CheckSort(IWebDriver driver)
        {
            List<IWebElement> price = driver.FindElements(By.ClassName("price")).ToList();

            for (int i = 1; i < price.Count; i++)
            {
                string first = price[i].Text;
                first = Regex.Replace(first, @"[^0-9,]", "");
                first = first.Trim(' ');
                string second = price[i - 1].Text;
                second = Regex.Replace(second, @"[^0-9,]", "");
                second = second.Trim(' ');
                try
                {
                    first = driver.FindElement(By.CssSelector("#js-product-list > div.products.row > article:nth-child(" + (i + 1) + ") > div > div.product-description > div > span.regular-price")).Text;
                    first = Regex.Replace(first, @"[^0-9,]", "");
                    first = first.Trim(' ');
                }
                catch { }
                try
                {
                    second = driver.FindElement(By.CssSelector("#js-product-list > div.products.row > article:nth-child(" + i + ") > div > div.product-description > div > span.regular-price")).Text;
                    second = Regex.Replace(second, @"[^0-9,]", "");
                    second = second.Trim(' ');
                }
                catch { }
                double bigger = Convert.ToDouble(second);
                double smaller = Convert.ToDouble(first);
                if (bigger >= smaller)
                {
                    Console.WriteLine(bigger.ToString("0.00") + " >= " + smaller.ToString("0.00") + " = " + "Good");
                }
                else
                {
                    Console.WriteLine(bigger.ToString("0.00") + " >= " + smaller.ToString("0.00") + " = " + "Bad");
                }
            }
            Console.WriteLine("\r\n");
        }
        public static void CheckDiscount(IWebDriver driver)
        {
            List<IWebElement> full_price = driver.FindElements(By.ClassName("price")).ToList();

            for (int i = 0; i < full_price.Count; i++)
            {
                try
                {
                    string regular_price = driver.FindElement(By.CssSelector("#js-product-list > div.products.row > article:nth-child(" + i + ") > div > div.product-description > div > span.regular-price")).Text;
                    regular_price = Regex.Replace(regular_price, @"[^0-9,]", "");
                    regular_price = regular_price.Trim(' ');
                    if (regular_price != "")
                    {
                        string price = driver.FindElement(By.CssSelector("#js-product-list > div.products.row > article:nth-child(" + i + ") > div > div.product-description > div > span.price")).Text;
                        price = Regex.Replace(price, @"[^0-9,]", "");
                        price = price.Trim(' ');
                        string discount = driver.FindElement(By.CssSelector("#js-product-list > div.products.row > article:nth-child(" + i + ") > div > div.product-description > div > span.discount-percentage")).Text;
                        discount = Regex.Replace(discount, @"[^0-9,]", "");
                        discount = discount.Trim(' ');

                        double calculate;
                        double dregular_price = Convert.ToDouble(regular_price);
                        double dprice = Convert.ToDouble(price);
                        double ddiscount = Convert.ToDouble(discount);
                        calculate = Math.Round(dregular_price - (dregular_price * ddiscount) / 100, 2);
                        if (calculate == dprice)
                        {
                            Console.WriteLine("Цена товара: " + regular_price + " скидка: " + discount + "%" + " скидочная цена: " + calculate);
                        }
                        else { Console.WriteLine("Скидка отображается неверно"); }
                    }

                }
                catch { }
            }
            Console.WriteLine("\r\n");
        }
    }

    public class Navigate
    {
        public static void ClickUSD(IWebDriver driver)
        {
            IWebElement usd;
            usd = driver.FindElement(By.XPath(@".//div[2]/div[2]/div[1]/a[1]"));
            usd.Click();
            System.Threading.Thread.Sleep(2 * 1000);
            usd = driver.FindElement(By.LinkText("USD $"));
            usd.Click();
        }
        public static void Search(IWebDriver driver)
        {
            IWebElement search;
            search = driver.FindElement(By.ClassName("ui-autocomplete-input"));
            search.Click();
            System.Threading.Thread.Sleep(2 * 1000);
            search.SendKeys("dress.");
            search.Submit();
        }
        public static void Sort(IWebDriver driver)
        {
            IWebElement sort = driver.FindElement(By.XPath(@".//section[1]/div[1]/div[1]/section[1]/section[1]/div[1]/div[1]/div[2]/div[1]/div[1]/a[1]"));
            sort.Click();
            System.Threading.Thread.Sleep(1 * 1000);
            sort = driver.FindElement(By.XPath(@".//section[1]/div[1]/div[1]/section[1]/section[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/a[5]"));
            sort.Click();
        }
    }
}

