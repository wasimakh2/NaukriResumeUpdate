using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class NaukriJobScrapper
    {
        

        public void  ScrapData()
        {
            // From Web
            var url = "https://www.naukri.com/wpf-jobs-in-noida?k=wpf&l=noida-1";
            WebDriverAutomation webDriverAutomation = new WebDriverAutomation();
            var html = webDriverAutomation.GetPageSource(url);
            webDriverAutomation.tearDown();
            //var web = new HtmlWeb();
            //var doc = web.Load(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);



            var findclasses = GetElements(doc.DocumentNode,"class","list").FirstOrDefault();

            var job_elems = GetElements(findclasses,"article","class","jobTuple bgWhite br4 mb-8").ToList();


            foreach (var item in job_elems)
            {
                HtmlNode URLITEM = GetElements(item,"a","class","title fw500 ellipsis").FirstOrDefault();

                string URL = URLITEM.Attributes["href"].Value;

                Console.WriteLine($"URL { URL }");
            }





        }

        private IEnumerable<HtmlNode> GetElements(HtmlNode doc,string Attribute,string AttributeValue)
        {
            return doc.Descendants().Where(
                            d => d.Attributes.Contains(Attribute)
                && d.Attributes[Attribute].Value.Equals(AttributeValue)
                );
        }
        private IEnumerable<HtmlNode> GetElements(HtmlNode doc, string ElementTagName,string Attribute, string AttributeValue)
        {
            return doc.Descendants(ElementTagName).Where(
                            d => d.Attributes.Contains(Attribute)
                && d.Attributes[Attribute].Value.Equals(AttributeValue)
                );
        }
    }
}
