using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Helpers
{
    public static class TagsHelper
    {
        public static string BuildSummaryClosingAllTags(string html)
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
            return currentSubstring + "...";
        }
    }
}
