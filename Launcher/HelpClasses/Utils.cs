using Launcher.Controls.Update;
using System.IO;
using System.Net;
using System.Text;

namespace Launcher.HelpClasses
{
    internal class Utilities
    {

        public static readonly string[] LANGS = { "enUS", "esMX", "ptBR", "deDE", "enGB", "esES", "frFR", "itIT", "ruRU", "koKR", "zhTW", "zhCN" };

        public class ReamlistUtils
        {
            private static void ClearReadOnlyAttributes(string filePath)
            {
                var attributes = File.GetAttributes(filePath);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes = attributes & ~FileAttributes.ReadOnly;
                    File.SetAttributes(filePath, attributes);
                }
            }

            private static void WriteRealmlistContent(string realmlistPath, string realmlistContent)
            {
                if (!File.Exists(realmlistPath))
                {
                    return;
                }

                ClearReadOnlyAttributes(realmlistPath);

                using (var writer = new StreamWriter(realmlistPath))
                {
                    writer.WriteLine(realmlistContent);
                }
            }

            public static void WriteVanillaRealmlist(string gamePath, string realmlistContent)
            {
                var realmlistPath = Path.Combine(gamePath, "realmlist.wtf");
                WriteRealmlistContent(realmlistPath, realmlistContent);
            }

            public static void WriteLocalizedRealmlist(string gamePath, string realmlistContent)
            {
                // thanks to https://github.com/pangolp for idea and basic implementation multilang universal realmlist handling
                foreach (var lang in LANGS)
                {
                    var realmlistPath = Path.Combine(gamePath, $@"Data\{lang}\realmlist.wtf");
                    WriteRealmlistContent(realmlistPath, realmlistContent);
                }
            }

            public static void WritePandariaRealmlist(string gamePath, string realmlistContent)
            {
                var realmlistPath = Path.Combine(gamePath, @"WTF\config.wtf");
                if (!File.Exists(realmlistPath))
                {
                    return;
                }

                ClearReadOnlyAttributes(realmlistPath);

                var builder = new StringBuilder();
                using (var reader = new StreamReader(realmlistPath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        builder.AppendLine(line.ToLower().Contains("set realmlist") ? realmlistContent : line);
                    }
                        
                }

                using (var writer = new StreamWriter(realmlistPath))
                {
                    writer.Write(builder.ToString());
                }
            }
        }

        public class Updater
        {
            public static string GetPath(string gamePath, string item)
            {
                // thanks to https://github.com/pangolp for idea and basic implementation multilang universal patch handling
                foreach (var lang in LANGS)
                {
                    if (item.ToLower().EndsWith(".mpq") && item.Contains($@"-{lang}-"))
                    {
                        return Path.Combine(gamePath, $@"Data\{lang}\{item}");
                    }
                }

                return item.ToLower().EndsWith(".mpq")
                    ? Path.Combine(gamePath, $@"Data\{item}")
                    : Path.Combine(gamePath, item);
            }

            public static string DetectSize(long value)
            {
                var amount = SpeedUnitsExtension.GetFormattedSize(value, "{0:0.00}");
                var abbr = SpeedUnitsExtension.GetUnitAbbr(value);
                return $"{amount}{abbr}";
            }

            public static string DetectSize(long value, out int speedType)
            {
                var units = SpeedUnitsExtension.GetUnit(value);
                switch (units)
                {
                    case SpeedUnits.GBit:
                        speedType = 2;
                        break;
                    case SpeedUnits.MBit:
                        speedType = 1;
                        break;
                    default:
                        speedType = 0;
                        break;
                }
                return SpeedUnitsExtension.GetFormattedSize(value, "{0:0}");
            }
        }

        public class Network
        {
            /// <summary>
            /// Internet connection checker
            /// </summary>
            /// <returns>Returns true if connection exist</returns>
            public static bool IsInternetConnectionAvailable()
            {
                try
                {
                    using (var client = new WebClient())
                    using (client.OpenRead("https://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
