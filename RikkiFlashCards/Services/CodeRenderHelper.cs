using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{
    public static class CodeRenderHelper
    {
        public static Card EncodeCardForCode(Card card)
        {
            var htmle = HtmlEncoder.Create();
            card.Front = htmle.Encode(card.Front);
            card.Front = card.Front.Replace(htmle.Encode("<code>"), "<pre class='prettyprint lang-cs'>");
            card.Front = card.Front.Replace(htmle.Encode("</code>"), "</pre>");
            card.Back = htmle.Encode(card.Back);
            card.Back = card.Back.Replace(htmle.Encode("<code>"), "<pre class='prettyprint lang-cs'>");
            card.Back = card.Back.Replace(htmle.Encode("</code>"), "</pre>");
            card = RestoreNewLine(card);

            return card;
        }

        public static Card RestoreNewLine(Card card)
        {
            var htmle = HtmlEncoder.Create();
            card.Front = card.Front.Replace(htmle.Encode("\r\n"), "<br />");
            card.Back = card.Back.Replace(htmle.Encode("\r\n"), "<br />");

            return card;
        }

        public static Card DecodeCardForCode(Card card)
        {
            card.Front = card.Front.Replace("<pre class='prettyprint lang-cs'>", "<code>");
            card.Front = card.Front.Replace("</pre>", "</code>");
            card.Back = card.Back.Replace("<pre class='prettyprint lang-cs'>", "<code>");
            card.Back = card.Back.Replace("</pre>", "</code>");

            return card;
        }

    }
}
