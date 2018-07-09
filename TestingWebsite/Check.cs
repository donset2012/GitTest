using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;


namespace TestingWebsite
{
    class Check
    {  
        //CheckCurrency
        public static void CheckCurrency(IWebDriver driver)
        {
            try
            {
                List<IWebElement> all = driver.FindElements(By.ClassName("price")).ToList();
                string Currency = driver.FindElement(By.XPath(@".//div[2]/div[2]/div[1]/span[2]")).Text;
                Currency = Regex.Replace(Currency, @"\w", "", RegexOptions.Compiled);
                Currency = Currency.Trim();
                for (int i = 0; i < all.Count; i++)
                {
                    string s = all[i].Text;
                    s = Regex.Replace(s, @"\d", "", RegexOptions.Compiled);
                    s = s.Trim(',', ' ');
                    if (s == Currency)
                    {
                        Console.WriteLine("Checked the currency of the " + (i + 1) + " stuff");
                    }
                    else
                    {
                        Console.WriteLine("Not Checked");
                        Logger.Log.Warn("Can't check the currency");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Faild to check the currency");
                Logger.Log.Error("Faild to check the currency");
            }
            Console.WriteLine("\r\n");
        }


        //CheckStuff
        public static void CheckStuff(IWebDriver driver)
        {
            try
            {
                string stuff = driver.FindElements(By.ClassName("thumbnail-container")).Count.ToString();
                string stuff2 = driver.FindElement(By.XPath(@".//section[1]/section[1]/div[1]/div[1]/div[1]/p[1]")).Text;
                stuff2 = Regex.Replace(stuff2, @"\D", "", RegexOptions.Compiled);
                stuff2 = stuff2.Trim();
                if (stuff == stuff2 && stuff != "")
                {
                    Console.WriteLine("Товаров: " + stuff + " - True" + "\r\n");
                }
                else
                {
                    Console.WriteLine("Can't check the stuff" + "\r\n");
                    Logger.Log.Warn("Can't check the stuff");
                }
            }
            catch
            {
                Console.WriteLine("Faild to check the stuff" + "\r\n");
                Logger.Log.Error("Faild to check the stuff");
            }
        }


        //CheckPriceUSD
        public static void CheckPriceUSD(IWebDriver driver)
        {
            try
            {
                List<IWebElement> all = driver.FindElements(By.ClassName("price")).ToList();
                string Currency = driver.FindElement(By.XPath(@".//div[2]/div[2]/div[1]/span[2]")).Text;
                Currency = Regex.Replace(Currency, @"\w", "", RegexOptions.Compiled);
                Currency = Currency.Trim();


                for (int i = 0; i < all.Count; i++)
                {
                    string s = all[i].Text;
                    s = Regex.Replace(s, @"\d", "", RegexOptions.Compiled);
                    s = s.Trim(',', ' ');
                    if (s == Currency)
                    {
                        Console.WriteLine("The currency of the " + (i + 1) + " stuff: " + s);
                    }
                    else
                    {
                        Console.WriteLine("The currency doesn't match");
                        Logger.Log.Warn("The currency doesn't match");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Faild to check the currency");
                Logger.Log.Error("Faild to check the currency");
            }
            Console.WriteLine("\r\n");
        }


        //CheckSort
        public static void CheckSort(IWebDriver driver)
        {
            try
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
                        Logger.Log.Error("Failed to check the sorting");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to check the sorting");
                Logger.Log.Error("Failed to check the sorting");
            }
            Console.WriteLine("\r\n");
        }


        //CheckDiscount
        public static void CheckDiscount(IWebDriver driver)
        {
            try
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
                            else
                            {
                                Console.WriteLine("Discount displayed incorrectly");
                                Logger.Log.Error("Discount displayed incorrectly");
                            }
                        }


                    }
                    catch { }
                }
            }
            catch
            {
                Console.WriteLine("Faild to check the discount");
                Logger.Log.Error("Faild to check the discount");
            }
            Console.WriteLine("\r\n");
        }
    }
}

