using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace LeituraPagina
{
    public partial class Default : System.Web.UI.Page
    {
        private int _itemCounter = 0;
        Dictionary<string, int> _words = new Dictionary<string, int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void GetWords(string page)
        {
            string pattern = "<*?>(.*?)<\\/";
            MatchCollection matches = Regex.Matches(page, pattern);
            foreach (Match m in matches)
            {
                string[] separatingStrings = { ">", "</" };
                string[] readLines = m.Value.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in readLines)
                {
                    if (!string.IsNullOrEmpty(line.Replace("<br", "").Replace(" ", "")) && !line.Replace("<br", "").Contains("<"))
                    {
                        string[] readWords = line.Split(' ');
                        foreach (string word in readWords)
                        {
                            InsertWord(word);
                        }
                    }
                }
            }
            EscreveNaPagina(Get10MoreWords());
        }

        private void InsertWord(string word)
        {
            word = word.Trim().Replace("<br", "");
            if (!string.IsNullOrEmpty(word))
            {
                if (_words.ContainsKey(word))
                    _words[word]++;
                else
                    _words.Add(word, 1);
            }
        }

        private void EscreveNaPagina(List<KeyValuePair<string, int>> list)
        {
            HiddenData.Value = "";
            HiddenLabel.Value = "";
            tbody_words.InnerHtml = "";
            foreach (var k in list)
            {
                tbody_words.InnerHtml += string.Format(@"<tr><td>{0}</td><td>{1}</td></tr>", k.Key, k.Value);
                HiddenData.Value += (string.IsNullOrEmpty(HiddenData.Value) ? "" : ",") + k.Value;
                HiddenLabel.Value += (string.IsNullOrEmpty(HiddenLabel.Value) ? "" : ",") + string.Format("'{0}'", k.Key);
            }
            HiddenData.Value = "[" + HiddenData.Value + "]";
            HiddenLabel.Value = "[" + HiddenLabel.Value + "]";
        }

        private List<KeyValuePair<string, int>> Get10MoreWords()
        {
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(_words);
            myList.Sort(
                delegate (KeyValuePair<string, int> firstPair, KeyValuePair<string, int> nextPair)
                {
                    return nextPair.Value.CompareTo(firstPair.Value);
                }
            );
            return myList.GetRange(0, myList.Count >= 10 ? 10 : myList.Count);
        }

        private string GetPage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }

        private void GetImagesFromDiv(string s)
        {
            GetRegexMatchesImage(s, @"<div.*url\s*(\s*(?:""(?<1>[^""]*)""|(?<1>\S+))\s*)>", "div");
        }

        private void GetImages(string s)
        {
            GetRegexMatchesImage(s, @"img.*src\s*=\s*(?:""(?<1>[^""]*)""|(?<1>\S+))", "img");
        }

        private void GetRegexMatchesImage(string s, string pattern, string tipo)
        {
            MatchCollection matches = Regex.Matches(s, pattern);

            if (matches.Count > 0)
            {
                foreach (Match m in matches)
                {
                    if (m.Value.Contains("+") || m.Value.Contains("src=\"\""))
                        continue;

                    buttons.InnerHtml += GetButtom();
                    itens.InnerHtml += GetImage(m.Value, tipo);
                }
            }
        }

        private string GetImage(string value, string tipo)
        {
            string div = "";
            if (tipo.Equals("div"))
            {
                string[] separatingStrings = { "url(", ")" };
                string[] url = value.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                if (url.Length >= 3)
                {
                    div = string.Format(@"<img class='d-block w-100' src='{0}' />", url[1]);
                }
            }
            return string.Format(@" <div class='carousel-item {0}'>
                                    {1}
                                </div>",
                                    (string.IsNullOrEmpty(itens.InnerHtml.Trim()) ? "active" : ""),
                                    tipo.Equals("img") ? string.Format("<{0}/>", value) : div);
        }

        private string GetButtom()
        {
            return string.Format(@"<li data-target='#carousel' data-slide-to='{0}' {1}></li>",
                (_itemCounter),
                ((_itemCounter++) == 0 ? "class='active'" : ""));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            buttons.InnerHtml = "";
            itens.InnerHtml = "";
            string page = GetPage(urlPage.Value);
            GetImages(page);
            GetImagesFromDiv(page);
            GetWords(page);
        }
    }
}