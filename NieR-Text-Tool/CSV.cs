using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace NieR_Text_Tool
{
    public static class CSV
    {
        public static string ToCSV(byte[] input)
        {
            string str = Encoding.UTF8.GetString(input);
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<string> strings = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Replace("\n", "<LF>").Split(new string[] { "\t" }, StringSplitOptions.None).Select(entry => $"\"{entry.Replace("\"", "\"\"")}\"").ToArray();
                strings.Add(string.Join(",", line));
            }
            string result = string.Join("\r\n", strings.ToArray());
            Dictionary<string, byte[]> gameCode = GameCode.GetGameCode();
            foreach (KeyValuePair<string, byte[]> entry in gameCode)
            {
                result = result.Replace(Encoding.UTF8.GetString(entry.Value), entry.Key);
            }
            return result;
        }

        public static byte[] ToGameFormat(string input)
        {
            List<string> strings = new List<string>();
            using (TextFieldParser parser = new TextFieldParser(input))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.TrimWhiteSpace = false;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    strings.Add(string.Join("\t", fields));
                }
            }
            string result = string.Join("\r\n", strings.ToArray());
            result = result.Replace("<LF>", "\n");
            Dictionary<string, byte[]> gameCode = GameCode.GetGameCode();
            foreach (KeyValuePair<string, byte[]> entry in gameCode)
            {
                result = result.Replace(entry.Key, Encoding.UTF8.GetString(entry.Value));
            }
            return Encoding.UTF8.GetBytes(result);
        }
    }
}
