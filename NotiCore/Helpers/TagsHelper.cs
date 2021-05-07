using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Helpers
{
    public static class TagsHelper
    {
        public static string FormatImages(this string html)
        {
            var style = "<img style=\"max-width: 100% !important; border-radius: 16px; margin-right: auto !important; margin-left: auto !important; display:block !important; height: auto!important;\"";
            if (html.Contains("<img"))
            {
               html = html.Replace("<img", style);
            }
            return html;
        }
        public static string BuildSummaryClosingAllTags(this string html)
        {
            var currentSubstring = html.Substring(0, 350);
            if (html.Contains("<"))
            {
                var areOpenTags = 0;
                for (int i = 0; i < html.Length; i++)
                {
                    var limitReached = i > 350;
                    if (i + 1 != html.Length)
                    {
                        var character = html[i];
                        if (character.Equals('<') && html[i + 1] != '/') 
                        {
                            areOpenTags += 1; 
                        }

                        if (((character.Equals('<') && html[i + 1] == '/')) || (character.Equals('/') && html[i + 1] == '>'))
                            areOpenTags -= 1;

                        if (areOpenTags <= 0 && limitReached)
                        {
                            currentSubstring = html.Substring(0, i);
                            break;
                        }
                    }
                }

            }
            if (string.IsNullOrEmpty(AvoidCustomTagRules(currentSubstring)))
                return "";

            return AvoidCustomTagRules(currentSubstring) + "...";
        }

        private static string AvoidCustomTagRules(string html)
        {
            var custom = "<div class=\"feedflare\">";
            if (html.Contains(custom))
            {
                html = html.Substring(0, html.IndexOf(custom));
            }
            return html;
        }
    }
}
