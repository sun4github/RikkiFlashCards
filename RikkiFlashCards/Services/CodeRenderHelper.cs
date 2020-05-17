using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{
    public static class CodeRenderHelper
    {
        
        private const string onScreenWhileEditing_LinkRegex_Pattern = @"(?<entireLink>(?<openLink>\[link\])(?<hyperLink>https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.*[a-zA-Z0-9()]{0,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*))(?<closeLink>\[/link\]))";
        private const string onScreenWhileViewing_MainLinkRegex_Replacement = @"<a href=""${hyperLink}"" target=""_blank"">${hyperLink}</a>";
        
        public static Card EncodeCardContentForReadonlyView(Card card)
        {
            List<string> linksInCardContent;

            var htmle = HtmlEncoder.Create();
            linksInCardContent = findCustomLinks(card.Front);
            card.Front = htmle.Encode(card.Front);            
            card.Front = card.Front.Replace(htmle.Encode("<code>"), "<pre class='prettyprint lang-cs'>");
            card.Front = card.Front.Replace(htmle.Encode("</code>"), "</pre>");
            foreach(var linkInCard in linksInCardContent)
            {
                card.Front = card.Front.Replace(htmle.Encode(linkInCard), linkInCard);
            }
            card.Front = AlterCustomLinkToHyperlink(card.Front);


            linksInCardContent = findCustomLinks(card.Back);
            card.Back = htmle.Encode(card.Back);
            card.Back = card.Back.Replace(htmle.Encode("<code>"), "<pre class='prettyprint lang-cs'>");
            card.Back = card.Back.Replace(htmle.Encode("</code>"), "</pre>");
            foreach (var linkInCard in linksInCardContent)
            {
                card.Back = card.Back.Replace(htmle.Encode(linkInCard), linkInCard);
            }
            card.Back = AlterCustomLinkToHyperlink(card.Back);

            card = RestoreNewLine(card);
            return card;
        }

        private static List<string> findCustomLinks(string cardContent)
        {
            var linksInContent = new List<string>();
            Match matchCollection;
            try
            {
                matchCollection = Regex.Match(cardContent, onScreenWhileEditing_LinkRegex_Pattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                TimeSpan.FromSeconds(1));
                while (matchCollection.Success)
                {
                    linksInContent.Add(matchCollection.Groups["entireLink"].Value);
                    matchCollection = matchCollection.NextMatch();
                }
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("The matching operation timed out.");
            }
            return linksInContent;
        }

        private static string AlterCustomLinkToHyperlink(string cardContent)
        {
            var modifiedCardContent = string.Empty;
            try
            {
                modifiedCardContent = Regex.Replace(cardContent, onScreenWhileEditing_LinkRegex_Pattern, onScreenWhileViewing_MainLinkRegex_Replacement);
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("The matching operation timed out.");
            }
            return modifiedCardContent;
        }

        public static Card RestoreNewLine(Card card)
        {
            var htmle = HtmlEncoder.Create();
            card.Front = card.Front.Replace(htmle.Encode("\r\n"), "<br />");
            card.Back = card.Back.Replace(htmle.Encode("\r\n"), "<br />");

            return card;
        }
    }
}
