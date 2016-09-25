using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LikeSweeper
{
    static class ElementParser
    {
        public static HtmlElementCollection ReturnAll(WebBrowser wb)
        {
            return wb.Document.All;
        }

        public static HtmlElementCollection ReturnTR(WebBrowser wb)
        {
            return wb.Document.GetElementsByTagName("TR");
        }

        public static HtmlElementCollection ReturnLinks(WebBrowser wb)
        {
            return wb.Document.GetElementsByTagName("a");
        }

        public static HtmlElementCollection ReturnItems(WebBrowser wb)
        {
            return wb.Document.GetElementsByTagName("LI");
        }

        public static void RemoveNextLike(WebBrowser wb, TextBox srcBoxOutput, int tCount)
        {
            try
            {
                HtmlElementCollection eleAll = ElementParser.ReturnTR(wb);

                HtmlElement content = eleAll[0].Children[1].Children[0];
                HtmlElement editLink = eleAll[0].Children[2].Children[0].Children[1].Children[0];




                bool tCountOdd;

                if (tCount % 2 != 0)
                {
                    tCountOdd = true;
                }
                else
                {
                    tCountOdd = false;
                }


                if (tCountOdd)
                {
                    editLink.InvokeMember("click");
                }
                else
                {
                    int linkCount = 0;
                    HtmlElementCollection allLinks = ElementParser.ReturnItems(wb);
                    foreach (HtmlElement link in allLinks)
                    {
                        linkCount++;
                        try
                        {
                            if (link.InnerText.Equals("\r\nUnlike") || link.InnerText.Equals("\r\nRemove Reaction"))
                            {
                                link.FirstChild.InvokeMember("click");
                            }

                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                srcBoxOutput.Text = editLink.InnerHtml;
            }
            catch
            {

            }
        }
    }
}
