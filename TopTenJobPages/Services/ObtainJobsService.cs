using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TopTenJobPages.Models;

namespace TopTenJobPages.Services
{
	public class ObtainJobsService
	{
		public ObtainJobsService()
		{
		}

		public List<LinkedInJob> obtenerLinkedinJobs()
		{
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.linkedin.com/jobs/search/?currentJobId=3739507869&distance=25.0&geoId=100876405&keywords=senior%20.net&origin=HISTORY");
            //Sacar ids de las url para recuperar las descripciones
            var elements = driver.FindElements(By.ClassName("base-card"));
            var doc = new HtmlDocument();
            WebClient client = new WebClient();
            var listCompany = new List<LinkedInJob>();
            foreach (var element in elements)
            {
                var id = obtenerLinkedId(element.GetAttribute("data-entity-urn"));
                var titleElement = element.FindElement(By.ClassName("base-search-card__title"));
                var title = titleElement.Text;
                var linkEmployment = element.FindElement(By.ClassName("base-card__full-link")).GetAttribute("href");
                var companyInfo = element.FindElement(By.ClassName("base-search-card__subtitle")).FindElement(By.ClassName("hidden-nested-link"));
                var company = companyInfo.Text;
                var linkCompany = companyInfo.GetAttribute("href");
                var linkedJob = new LinkedInJob
                {
                    Title = title,
                    Company = company,
                    LinkEmployment = linkEmployment,
                    LinkCompany = "nohay"
                };
                listCompany.Add(linkedJob);
            }
            driver.Quit();
            return listCompany;
        }

        static string obtenerLinkedId(string url)
        {
            Match match = Regex.Match(url, @"\d+");

            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return "";
            }
        }
    }
}

