using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ProjectHub
{
    public static class StringExtensions
    {
        public static string ToStringWithLinks(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            return value.ResolveProjectLinks();
        }

        private static string ResolveProjectLinks(this string value)
        {
            var regex = new Regex(@"(^@|(?<=\s)@[\w\.\-\[\]]+)");
            var matches = regex.Matches(value)
                .OfType<Match>().Select(m => m.Groups[0].Value).Distinct();
            foreach (var match in matches)
            {
                string href = "#";
                string text = match;
                var idStart = match.IndexOf("[", StringComparison.Ordinal);
                if (idStart > 0)
                {
                    var idStop = match.IndexOf("]", idStart, StringComparison.Ordinal);
                    var id = idStop > idStart+1 
                        ? match.Substring(idStart + 1, idStop - idStart)
                        : string.Empty;
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        var helper = new UrlHelper();
                        href = helper.Action("Details", "Project", new {id = id});
                    }
                    text = match.Substring(0, idStart);
                }
                value = value.Replace(match, $"<a href=\"{href}\">{text}</a>");
            }
            return value;
        }

        private static string ResolveTopicLinks(this string value)
        {
            var regex = new Regex(@"(^#|(?<=\s)#[\w\.\-\[\]]+)");
            var matches = regex.Matches(value)
                .OfType<Match>().Select(m => m.Groups[0].Value).Distinct();
            foreach (var match in matches)
            {
                string href = "#";
                string text = match;
                var idStart = match.IndexOf("[", StringComparison.Ordinal);
                if (idStart > 0)
                {
                    var idStop = match.IndexOf("]", idStart, StringComparison.Ordinal);
                    var id = idStop > idStart + 1
                        ? match.Substring(idStart + 1, idStop - idStart)
                        : string.Empty;
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        var helper = new UrlHelper();
                        href = helper.Action("Details", "Topic", new { id = id });
                    }
                    text = match.Substring(0, idStart);
                }
                value = value.Replace(match, $"<a href=\"{href}\">{text}</a>");
            }
            return value;
        }
    }
}