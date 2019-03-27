using System.Xml;

namespace DiscordDnDBot.Resources
{
    internal static class HtmlBuilder
    {
        private static string path = "Resources/Images/testImage.html";
        private static string styleFormatting =
            @"body {
        background-size: 200px 150px;
        background-image: url(""https://wallpapercave.com/wp/1Xklfvu.jpg"");
    }
    h1{
        max-width: 200px;
        max-height: 150px;
        color: white;
        text-shadow: 10px;
        font-size: 20px;
    }
    p {
        max-width: 200px;
        max-height: 50px;
        color: white;
        text-shadow: 10px;
        font-size: 15px;
    }";

        internal static void BuildNewDoc(string name, params string[] input)
        {
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t",
                ConformanceLevel = ConformanceLevel.Fragment
            };
            using (var writer = XmlWriter.Create(path, settings))
            {
                writer.WriteElementString("style", styleFormatting);
                writer.WriteElementString("h1", name);
                foreach (string s in input)
                {
                    writer.WriteElementString("p", s);
                }
                writer.Flush();
            }
        }
    }
}
