using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NieR_Text_Tool
{
    public static class PACK
    {
        #region Structure
        public struct PackHeader
        {
            public int Magic;
            public int Version;
            public int PackSize;
            public int UnkDataOffset;
            public int Unk;
            public int NameCount;
            public int NameTableOffset;
            public int Table1FileCount;
            public int Table1Offset;
            public int Table1RealOffset;
            public int Table2FileCount;
            public int Table2Offset;
            public int Table2RealOffset;
        }
        public struct PackEntry
        {
            public int NameHash;
            public int NameOffset;
            public int RealNameOffset;
            public int DataLength;
            public int DataOffset;
            public int RealDataOffset;
            public int Unk;
            public string FileName;
            public byte[] FileData;
        }
        #endregion
        public static PackHeader ReadHeader(ref BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            PackHeader header = new PackHeader();
            header.Magic = reader.ReadInt32();
            if (header.Magic != 0x4B434150) throw new Exception("Unsupported file type."); 
            header.Version = reader.ReadInt32();
            header.PackSize = reader.ReadInt32();
            header.UnkDataOffset = reader.ReadInt32();
            header.Unk = reader.ReadInt32();
            header.NameCount = reader.ReadInt32();
            header.NameTableOffset = reader.ReadInt32();
            header.Table1FileCount = reader.ReadInt32();
            header.Table1Offset = reader.ReadInt32();
            header.Table1RealOffset = (int)(reader.BaseStream.Position - 4) + header.Table1Offset;
            header.Table2FileCount = reader.ReadInt32();
            header.Table2Offset = reader.ReadInt32();
            header.Table2RealOffset = (int)(reader.BaseStream.Position - 4) + header.Table2Offset;
            return header;
        }
        public static string ReadString(ref BinaryReader reader)
        {
            StringBuilder str = new StringBuilder();
            byte[] ch = reader.ReadBytes(1);
            while (ch[0] != 0 && reader.BaseStream.Position < reader.BaseStream.Length)
            {
                str.Append(Encoding.ASCII.GetString(ch));
                ch = reader.ReadBytes(1);
            }
            return str.ToString();
        }
        public static PackEntry[] ReadEntries(ref BinaryReader reader, PackHeader header)
        {
            List<PackEntry> result = new List<PackEntry>();
            reader.BaseStream.Position = header.Table2RealOffset;
            for (int i = 0; i < header.Table2FileCount; i++)
            {
                PackEntry entry = new PackEntry();
                entry.NameHash = reader.ReadInt32();
                entry.NameOffset = reader.ReadInt32();
                entry.RealNameOffset = (int)(reader.BaseStream.Position - 4) + entry.NameOffset;
                entry.DataLength = reader.ReadInt32();
                entry.DataOffset = reader.ReadInt32();
                entry.RealDataOffset = (int)(reader.BaseStream.Position - 4) + entry.DataOffset;
                entry.Unk = reader.ReadInt32();
                long temp = reader.BaseStream.Position;
                reader.BaseStream.Position = entry.RealNameOffset;
                entry.FileName = ReadString(ref reader);
                reader.BaseStream.Position = entry.RealDataOffset;
                entry.FileData = reader.ReadBytes(entry.DataLength);
                reader.BaseStream.Position = temp;
                result.Add(entry);
            }
            return result.ToArray();
        }
        private static byte[] Padding(int len)
        {
            byte[] padding = new byte[len];
            for (int i = 0; i < len; i++)
            {
                if (i < 8) padding[i] = 0x26;
                else padding[i] = 0x40;
            }
            return padding;
        }
        public static void Unpack(string file, string des)
        {
            BinaryReader reader = new BinaryReader(File.OpenRead(file));
            PackHeader header = ReadHeader(ref reader);
            PackEntry[] files = ReadEntries(ref reader, header);
            foreach (PackEntry entry in files)
            {
                string filePath = Path.Combine(des, $"{entry.FileName}.csv");
                string data = CSV.ToCSV(entry.FileData);
                if (!Directory.Exists(des)) Directory.CreateDirectory(des);
                File.WriteAllText(filePath, data, Encoding.UTF8);
                Console.WriteLine($">> {Path.GetFileName(filePath)}");
            }
            reader.Close();
        }
        public static void Repack(string file, string dir, string outFile)
        {
            MemoryStream input = new MemoryStream(File.ReadAllBytes(file));
            BinaryReader reader = new BinaryReader(input);
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            PackHeader header = ReadHeader(ref reader);
            PackEntry[] entries = ReadEntries(ref reader, header);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            writer.Write(reader.ReadBytes(header.Table2RealOffset));
            long tocPos = writer.BaseStream.Position;
            writer.Write(new byte[0x14 * header.Table2FileCount]);
            for (int i = 0; i < entries.Length; i++)
            {
                string filePath = Path.Combine(dir, $"{entries[i].FileName}.csv");
                if (File.Exists(filePath))
                {
                    entries[i].FileData = CSV.ToGameFormat(filePath);
                    entries[i].DataLength = entries[i].FileData.Length;
                    Console.WriteLine($"<< {Path.GetFileName(filePath)}");
                }
                entries[i].RealDataOffset = (int)writer.BaseStream.Position;
                writer.Write(entries[i].FileData);
                entries[i].RealNameOffset = (int)writer.BaseStream.Position;
                writer.Write(Encoding.ASCII.GetBytes(entries[i].FileName));
                writer.Write(new byte());
                long temp = writer.BaseStream.Position;
                writer.BaseStream.Position = tocPos + (0x14 * i);
                writer.Write(entries[i].NameHash);
                writer.Write(entries[i].RealNameOffset - (int)writer.BaseStream.Position);
                writer.Write(entries[i].DataLength);
                writer.Write(entries[i].RealDataOffset - (int)writer.BaseStream.Position);
                writer.Write(entries[i].Unk);
                writer.BaseStream.Position = temp;
            }
            writer.BaseStream.Seek(8, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Write((int)writer.BaseStream.Length);
            File.WriteAllBytes(outFile, stream.ToArray());
            reader.Close();
            writer.Close();
            input.Close();
            stream.Close();
        }
    }
}
