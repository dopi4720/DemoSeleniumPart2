using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSeleniumPart2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<StableCoin> StableCoinList = new List<StableCoin>();
            ChromeDriverService cService = ChromeDriverService.CreateDefaultService();
            cService.HideCommandPromptWindow = true;
            ChromeDriver driver = new ChromeDriver(cService);

            driver.Navigate().GoToUrl("https://coinmarketcap.com/vi/view/stablecoin/");

            //Find table -> tbody -> tr tag
            var tr_Tags = driver.FindElement(By.CssSelector("table.h7vnx2-2.czTsgW.cmc-table"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"));
            for (int i = 0; i < tr_Tags.Count; i++)
            {
                tr_Tags = driver.FindElement(By.CssSelector("table.h7vnx2-2.czTsgW.cmc-table"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"));
                var t = tr_Tags[i];
                StableCoin stableCoin = new StableCoin();
                var CellsOfName = t.FindElements(By.TagName("td"))[2]
                    .FindElements(By.TagName("p"));
                while (CellsOfName.Count == 0)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, 500)");
                    tr_Tags = driver.FindElement(By.CssSelector("table.h7vnx2-2.czTsgW.cmc-table"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"));
                    t = tr_Tags[i];
                    CellsOfName = t.FindElements(By.TagName("td"))[2]
                    .FindElements(By.TagName("p"));
                }

                stableCoin.Name = CellsOfName[0].Text + " - " + CellsOfName[1].Text;
                stableCoin.Price = t.FindElements(By.TagName("td"))[3]
                    .FindElement(By.TagName("span")).Text;

                stableCoin._1Hour = (t.FindElements(By.TagName("td"))[4]
                    .FindElements(By.ClassName("icon-Caret-up")).Count != 0 ? "" : "-")
                    + t.FindElements(By.TagName("td"))[4]
                    .FindElement(By.TagName("span")).Text;

                stableCoin._24Hour = (t.FindElements(By.TagName("td"))[5]
                    .FindElements(By.ClassName("icon-Caret-up")).Count != 0 ? "" : "-")
                    + t.FindElements(By.TagName("td"))[5]
                    .FindElement(By.TagName("span")).Text;

                stableCoin._7Day = (t.FindElements(By.TagName("td"))[6]
                    .FindElements(By.ClassName("icon-Caret-up")).Count != 0 ? "" : "-")
                    + t.FindElements(By.TagName("td"))[6]
                    .FindElement(By.TagName("span")).Text;

                stableCoin.VHTT = t.FindElements(By.TagName("td"))[7].FindElements(By.TagName("p")).Count != 0 ?
                    t.FindElements(By.TagName("td"))[7].FindElement(By.TagName("p")).Text : "";
                stableCoin.KhoiLuong = t.FindElements(By.TagName("td"))[8]
                    .FindElements(By.TagName("p")).Count != 0 ? t.FindElements(By.TagName("td"))[8]
                    .FindElement(By.TagName("p")).Text : "";
                StableCoinList.Add(stableCoin);
            }
        }
    }
}
