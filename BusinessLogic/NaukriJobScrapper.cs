using DataAccessLayer;
using HtmlAgilityPack;
using NaukriResumeUpdate;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class NaukriJobScrapper
    {
        public static UserDetails userDetails = new UserDetails();
        public string Jobkeysearch { get; set; } = userDetails.jobkeysearch;
        public string Joblocation { get; set; } = userDetails.joblocation;
        private readonly WebDriverAutomation webDriverAutomation = new();
        private HtmlDocument doc = new();
        
        public void ScrapData(int pageNumber)
        {
            try
            {
                // From Web
                string url = $"https://www.naukri.com/wpf-jobs-in-noida?k=" + HttpUtility.UrlEncode($"{Jobkeysearch}") + $"&l={Joblocation}-{pageNumber}";

                var html = webDriverAutomation.GetPageSource(url);

                doc.LoadHtml(html);

                var findclasses = GetElements(doc.DocumentNode, "class", "list").FirstOrDefault();

                var job_elems = GetElements(findclasses, "article", "class", "jobTuple bgWhite br4 mb-8").ToList();

                foreach (var item in job_elems)
                {
                    string datajobid = item.Attributes["data-job-id"].Value;
                    HtmlNode URLITEM = GetElements(item, "a", "class", "title fw500 ellipsis").FirstOrDefault();

                    string URL = URLITEM.Attributes["href"].Value;
                    string Title = URLITEM.Attributes["title"].Value;
                    string Company = GetElements(item, "a", "class", "subTitle ellipsis fleft").FirstOrDefault().Attributes["title"].Value;

                    string rating_span = GetElements(item, "span", "class", "starRating fleft dot").FirstOrDefault() is null ? "" : GetElements(item, "span", "class", "starRating fleft dot").FirstOrDefault().InnerText;

                    if (string.IsNullOrEmpty(rating_span)) continue;

                    HtmlNode Exp = GetElements(item, "li", "class", "fleft grey-text br2 placeHolderLi experience").FirstOrDefault();
                    string Experience = GetElements(Exp, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault() is null ? "" : GetElements(Exp, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault().InnerText;
                    HtmlNode Sal = GetElements(item, "li", "class", "fleft grey-text br2 placeHolderLi salary").FirstOrDefault();

                    string Salary = GetElements(Sal, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault() is null ? "" : GetElements(Sal, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault().InnerText;

                    HtmlNode Loc = GetElements(item, "li", "class", "fleft grey-text br2 placeHolderLi location").FirstOrDefault();
                    string Location = GetElements(Loc, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault() is null ? "" : GetElements(Loc, "span", "class", "ellipsis fleft fs12 lh16").FirstOrDefault().InnerText;

                    HtmlNode Hist = GetElements(item, "div", "class", "type br2 fleft grey").FirstOrDefault() ?? GetElements(item, "div", "class", "type br2 fleft green").FirstOrDefault();

                    string Post_History = GetElements(Hist, "span", "class", "fleft fw500").FirstOrDefault() is null ? "" : GetElements(Hist, "span", "class", "fleft fw500").FirstOrDefault().InnerText;

                    DataAccessLayer.Entity.NaukriJobDetail naukriJobDetail = new()
                    {
                        Title = Title,
                        URL = URL,
                        Company = Company,
                        Ratings = rating_span,
                        Experience = Experience,
                        Location = Location,
                        Salary = Salary,
                        Job_Post_History = Post_History,
                        DataJobId = datajobid
                    };

                    using DataAccessContext dataAccessContext = new();

                    DataAccessLayer.Entity.NaukriJobDetail DataObject = dataAccessContext.NaukriJobDetails.Where(x => x.DataJobId == naukriJobDetail.DataJobId).FirstOrDefault();

                    if (DataObject != null) continue;

                    SaveJobDetail(dataAccessContext, naukriJobDetail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CloseBrowser()
        {
            webDriverAutomation.TearDown();
        }

        private void SaveJobDetail(DataAccessContext dataAccessContext, DataAccessLayer.Entity.NaukriJobDetail naukriJobDetail)
        {
            try
            {
                dataAccessContext.NaukriJobDetails.Add(naukriJobDetail);
                dataAccessContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error::{ ex.Message }");
            }
        }

        private IEnumerable<HtmlNode> GetElements(HtmlNode doc, string Attribute, string AttributeValue)
        {
            return doc.Descendants().Where(
                            d => d.Attributes.Contains(Attribute)
                && d.Attributes[Attribute].Value.Equals(AttributeValue)
                );
        }

        private IEnumerable<HtmlNode> GetElements(HtmlNode doc, string ElementTagName, string Attribute, string AttributeValue)
        {
            return doc.Descendants(ElementTagName).Where(
                            d => d.Attributes.Contains(Attribute)
                && d.Attributes[Attribute].Value.Equals(AttributeValue)
                );
        }
    }
}