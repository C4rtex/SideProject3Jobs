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
        private readonly string linkLinkedIn = "https://www.linkedin.com/";
        private readonly string linkGlassDoor = "https://www.glassdoor.com";
        private readonly string linkFlexjobs = "https://www.flexjobs.com";

        public List<GlassDoorJob> obtenerGlassDoorJobs()
        {
            return new List<GlassDoorJob>();
        }

        public List<FlexJobsJob> obtenerIndeedJobs()
        {
            return new List<FlexJobsJob>();
        }

        public List<LinkedInJob> obtenerLinkedinJobs()
		{
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.linkedin.com/jobs/search/?currentJobId=3739507869&distance=25.0&geoId=100876405&keywords=senior%20.net&origin=HISTORY");
            //Sacar ids de las url para recuperar las descripciones
            var elements = driver.FindElements(By.ClassName("base-card"));
            //var doc = new HtmlDocument();
            //WebClient client = new WebClient();
            var listCompany = new List<LinkedInJob>();
            foreach (var element in elements)
            {
                var id = getLinkedId(element.GetAttribute("data-entity-urn"));
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
                    LinkCompany = linkCompany
                };
                listCompany.Add(linkedJob);
            }
            driver.Quit();
            return listCompany;
        }

        private bool logInLinkedIn(IWebDriver driver)
        {
            bool entro = false;
            driver.Navigate().GoToUrl(linkLinkedIn);
            var inputEmail = driver.FindElement(By.XPath("//*[@id=\"session_key\"]"));
            var inputContrasena = driver.FindElement(By.XPath("//*[@id=\"session_password\"]"));
            var buttonLogin = driver.FindElement(By.XPath("//*[@id=\"main-content\"]/section[1]/div/div/form/div[2]/button"));
            inputEmail.SendKeys("email");
            inputContrasena.SendKeys("contrasena");
            buttonLogin.Click();
            return entro;
        } 

        private string getLinkedId(string url)
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

