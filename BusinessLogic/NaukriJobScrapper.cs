using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class NaukriJobScrapper
    {
        

        public void  ScrapData(int pageNumber)
        {
            // From Web
            string url = $"https://www.naukri.com/wpf-jobs-in-noida?k=wpf&l=noida-{pageNumber}";
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
                string Title = URLITEM.Attributes["title"].Value;
                string Company= GetElements(item, "a", "class", "subTitle ellipsis fleft").FirstOrDefault().Attributes["title"].Value;


                string rating_span = GetElements(item, "span","class", "starRating fleft dot").FirstOrDefault() is null?"": GetElements(item, "span", "class", "starRating fleft dot").FirstOrDefault().InnerText;

                if (rating_span == "") continue;

                HtmlNode Exp = GetElements(item, "li", "class", "fleft grey-text br2 placeHolderLi experience").FirstOrDefault();
                string Experience= GetElements(Exp, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault() is null ? "" : GetElements(Exp, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault().InnerText;
                HtmlNode Sal= GetElements(item, "li", "class", "fleft grey-text br2 placeHolderLi salary").FirstOrDefault();

                string Salary= GetElements(Sal, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault() is null ? "" : GetElements(Sal, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault().InnerText;

                HtmlNode Loc= GetElements(item, "li", "class", "fleft grey-text br2 placeHolderLi location").FirstOrDefault();
                string Location= GetElements(Loc, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault() is null ? "" : GetElements(Loc, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault().InnerText;

                HtmlNode Hist= GetElements(item, "div", "class", "type br2 fleft grey").FirstOrDefault();

                if (Hist==null)
                {
                    Hist = GetElements(item, "div", "class", "type br2 fleft green").FirstOrDefault();
                }

                string Post_History= GetElements(Hist, "span", "class", "fleft fw500").FirstOrDefault() is null ? "" : GetElements(Hist, "span", "class", "fleft fw500").FirstOrDefault().InnerText;





                //Console.WriteLine($"URL { URL }");
                //Console.WriteLine($"Title { Title }");
                //Console.WriteLine($"Company { Company }");
                //Console.WriteLine($"rating_span { rating_span }");

                DataAccessLayer.Entity.NaukriJobDetail naukriJobDetail = new DataAccessLayer.Entity.NaukriJobDetail
                {
                    Title = Title,
                    URL = URL,
                    Company= Company,
                    Ratings= rating_span,
                    Experience=Experience,
                    Location=Location,
                    Salary=Salary,
                    Job_Post_History=Post_History
                };


                using (DataAccessLayer.DataAccessContext dataAccessContext=new DataAccessLayer.DataAccessContext())
                {
                    dataAccessContext.NaukriJobDetails.Add(naukriJobDetail);

                    dataAccessContext.SaveChanges();
                }



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
