using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NieR_Text_Tool
{
    public static class TXT
    {
        public static string ToTXT(byte[] bytes)
        {
            string str = Encoding.UTF8.GetString(bytes);
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\n", "<LF>").Replace("\t", "<TAB>");
            }
            str = string.Join("\r\n", lines);
            Dictionary<string, byte[]> gameCode = GameCode.GetGameCode();
            foreach (KeyValuePair<string, byte[]> entry in gameCode)
            {
                str = str.Replace(Encoding.UTF8.GetString(entry.Value), entry.Key);
            }
            return str;
        }
        public static byte[] ToGameFormat(string input)
        {
            string str = input.Replace("<LF>", "\n").Replace("<TAB>", "\t");
            Dictionary<string, byte[]> gameCode = GameCode.GetGameCode();
            foreach (KeyValuePair<string, byte[]> entry in gameCode)
            {
                str = str.Replace(entry.Key, Encoding.UTF8.GetString(entry.Value));
            }
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
