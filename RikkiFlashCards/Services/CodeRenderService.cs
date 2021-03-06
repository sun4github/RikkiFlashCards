﻿using AnkiFlashCards.Models.Domain;
using RikkiFlashCards.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{

    public enum CardColors
    {
        crimson,
        royalblue
    }
    public class CodeRenderService : ICodeRenderService
    {
        
        private const string onScreenWhileEditing_LinkRegex_Pattern = @"(?<entireLink>(?<openLink>\[link\])(?<hyperLink>https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.*[a-zA-Z0-9()]{0,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*))(?<closeLink>\[/link\]))";
        private const string onScreenWhileViewing_MainLinkRegex_Replacement = @"<a href=""${hyperLink}"" target=""_blank"" style='font-family: cursive; font-stretch: condensed; font-size: 85%; color:darkorchid;'>${hyperLink}</a>";

        private const string onScreenWhileEditing_BoldRegex_Pattern = @"(?<entireBoldWord>(?<openBold>\[bold\])(?<boldText>[-a-zA-Z0-9,&@:%._\+~#=\s\<\>\(\)\{\}&\?\[\]]{1,256})(?<closeBold>\[/bold\]))";


        public Card EncodeCardContentForReadonlyView(Card card)
        {
            List<string> linksInCardContent;
            List<string> boldWordsInCardContent;

            var htmle = HtmlEncoder.Create();


            linksInCardContent = findCustomLinks(card.Front);
            boldWordsInCardContent = findCustomBoldWords(card.Front);
            card.Front = htmle.Encode(card.Front);
            card.Front = card.Front.Replace(htmle.Encode("<code>"), "<pre class='prettyprint lang-cs'>");
            card.Front = card.Front.Replace(htmle.Encode("</code>"), "</pre>");

            card.Front = ReplaceCustomMarkupWithHTMLMarkup(card.Front, linksInCardContent, htmle, CardColors.crimson, AlterCustomLinksToHyperlinks);
            card.Front = ReplaceCustomMarkupWithHTMLMarkup(card.Front, boldWordsInCardContent, htmle, CardColors.crimson, AlterCustomBoldWordsToBoldTags);


            linksInCardContent = findCustomLinks(card.Back);
            boldWordsInCardContent = findCustomBoldWords(card.Back);
            card.Back = htmle.Encode(card.Back);
            card.Back = card.Back.Replace(htmle.Encode("<code>"), "<pre class='prettyprint lang-cs'>");
            card.Back = card.Back.Replace(htmle.Encode("</code>"), "</pre>");
            
            card.Back = ReplaceCustomMarkupWithHTMLMarkup(card.Back, linksInCardContent, htmle, CardColors.royalblue, AlterCustomLinksToHyperlinks);
            card.Back = ReplaceCustomMarkupWithHTMLMarkup(card.Back, boldWordsInCardContent, htmle, CardColors.royalblue, AlterCustomBoldWordsToBoldTags);

            card = RestoreNewLine(card);
            return card;
        }

        private string ReplaceCustomMarkupWithHTMLMarkup(string CardContent, List<string> markupContent, HtmlEncoder htmle, CardColors cardColor, Func<string,HtmlEncoder, CardColors,string> AlterCustomMarkupToHTMLMarkup)
        {

            foreach (var markup in markupContent)
            {
                CardContent = CardContent.Replace(htmle.Encode(markup), markup);
            }
            return AlterCustomMarkupToHTMLMarkup(CardContent, htmle, cardColor);
            
        }

        private List<string> findCustomLinks(string cardContent)
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

        private List<string> findCustomBoldWords(string cardContent)
        {
            var boldWords = new List<string>();
            Match matchCollection;
            try
            {
                matchCollection = Regex.Match(cardContent, onScreenWhileEditing_BoldRegex_Pattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                TimeSpan.FromSeconds(1));
                while (matchCollection.Success)
                {
                    boldWords.Add(matchCollection.Groups["entireBoldWord"].Value);
                    matchCollection = matchCollection.NextMatch();
                }
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("The matching operation timed out.");
            }
            return boldWords;
        }

        private string AlterCustomLinksToHyperlinks(string cardContent, HtmlEncoder htmlEncoder, CardColors cardColor)
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

        private string AlterCustomBoldWordsToBoldTags(string cardContent, HtmlEncoder htmlEncoder, CardColors cardColor)
        {
            var modifiedCardContent = string.Empty;

            Match matchCollection;
            try
            {
                matchCollection = Regex.Match(cardContent, onScreenWhileEditing_BoldRegex_Pattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                TimeSpan.FromSeconds(1));

                if(matchCollection.Success)
                {
                    modifiedCardContent = cardContent;
                    while (matchCollection.Success)
                    {
                        var boldText_Replacement = matchCollection.Groups["boldText"].Value;
                        boldText_Replacement = boldText_Replacement.Replace("<", htmlEncoder.Encode("<"));
                        boldText_Replacement = boldText_Replacement.Replace(">", htmlEncoder.Encode(">"));
                        boldText_Replacement = String.Concat($"<b style='font-family: ui-sans-serif; font-stretch: expanded; font-size: 125%; color:{nameof(cardColor)};'>", boldText_Replacement, "</b>");
                        modifiedCardContent = modifiedCardContent.Replace($"[bold]{matchCollection.Groups["boldText"].Value}[/bold]", boldText_Replacement);

                        matchCollection = matchCollection.NextMatch();
                    }
                }
                else
                {
                    modifiedCardContent = cardContent;
                }
                
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("The matching operation timed out.");
            }
            return modifiedCardContent;
        }

        public Card RestoreNewLine(Card card)
        {
            var htmle = HtmlEncoder.Create();
            card.Front = card.Front.Replace(htmle.Encode("\r\n"), "<br />");
            card.Back = card.Back.Replace(htmle.Encode("\r\n"), "<br />");

            return card;
        }
    }
}
