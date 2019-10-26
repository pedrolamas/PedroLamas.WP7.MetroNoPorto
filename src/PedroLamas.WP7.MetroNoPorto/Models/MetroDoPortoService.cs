using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace PedroLamas.WP7.MetroNoPorto.Models
{
    public class MetroDoPortoService : IMetroDoPortoService
    {
        private const string MetroDoPortoUrl = @"http://www.metrodoporto.pt";

        private static Regex SplittingRegex = new Regex(@"<td class=""alertaTitle alertaBorderRight""><b>", RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex ParsingRegex = new Regex(@"^(?<linha>[^<]{10,})</b></td>(?<resto>.*)", RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex ParsingRegex2 = new Regex(@"<td class=""(alertaText )?alerta(?<key>Estado|Detalhe)Box"">(?<value>.*?)</td>", RegexOptions.Singleline | RegexOptions.Compiled);

        private static Regex RemoveHtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        private string CreateRequestUrl()
        {
            return MetroDoPortoUrl;
        }

        private IMetroDoPortoLine[] GetResultFromResponseContent(string responseContent)
        {
            var linesData = SplittingRegex.Split(responseContent)
                .Select(x => ParsingRegex.Match(x))
                .Where(x => x.Success);

            var lines = new List<IMetroDoPortoLine>();

            foreach (var lineData in linesData)
            {
                var description = lineData.Groups["linha"].Value.Trim();
                var statusTitle = string.Empty;
                var statusDescription = string.Empty;

                foreach (Match match in ParsingRegex2.Matches(lineData.Groups["resto"].Value))
                {
                    switch (match.Groups["key"].Value)
                    {
                        case "Estado":
                            statusTitle = RemoveHtmlRegex.Replace(match.Groups["value"].Value, string.Empty).Trim();

                            break;

                        case "Detalhe":
                            statusDescription = RemoveHtmlRegex.Replace(match.Groups["value"].Value, string.Empty).Trim();

                            break;
                    }
                }

                lines.Add(new MetroDoPortoLine(lines.Count + 1, description, statusTitle, statusDescription));
            }

            return lines.ToArray();
        }

        public void GetStatus(Action<IMetroDoPortoStatusResult> callback)
        {
            var url = CreateRequestUrl();

            var request = HttpWebRequest.Create(url);

            request.BeginGetResponse(ar =>
            {
                try
                {
                    var response = (HttpWebResponse)request.EndGetResponse(ar);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseContent;

                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            responseContent = streamReader.ReadToEnd();
                        }

                        var lines = GetResultFromResponseContent(responseContent);

                        callback(new MetroDoPortoStatusResult(lines));
                    }
                    else
                    {
                        throw new Exception(string.Format("Http Error: ({0}) {1}",
                            response.StatusCode,
                            response.StatusDescription));
                    }
                }
                catch (Exception ex)
                {
                    callback(new MetroDoPortoStatusResult(ex));
                }
            }, null);
        }
    }
}