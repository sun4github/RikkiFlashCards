using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{
    public static class HtmlHelperExtensions
    {
       
        public static HtmlString ReplaceNewLineWithBr(this IHtmlContent content)
        {
            var encoder = HtmlEncoder.Default;

            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, encoder);
                var cleanedString = writer.ToString().Replace(encoder.Encode("\r\n"), encoder.Encode("<br>"));
                return new HtmlString(cleanedString) ;
            }
        }

    }
}
