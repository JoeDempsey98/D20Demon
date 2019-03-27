using System;
using NReco.ImageGenerator;
using System.IO;
using DiscordDnDBot.Resources;
using DiscordDnDBot.Core.UserAccounts;

namespace DiscordDnDBot.Core.LevelingSystem
{
    internal static class UserLevelDisplayImage
    {
        //private static string directory = "Resources/Images/";
        private static string file = "Resources/Images/testImage.html";
        //private static string unformattedHtml = File.ReadAllText(directory + file);

        public static byte[] HtmlToJpeg(UserAccount user)
        {
            string userName = user.username;
            string userXp = "XP: " + user.XP.ToString();
            string userLvl = "Level: " + user.lvl.ToString();
            
            HtmlBuilder.BuildNewDoc(userName, userXp, userLvl);
            var convert = new HtmlToImageConverter
            {
                Width = 200,
                Height = 150
            };
            var bytes = convert.GenerateImageFromFile(file, ImageFormat.Jpeg);
            return bytes;
        }
    }
}
