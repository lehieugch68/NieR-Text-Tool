using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NieR_Text_Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "NieR Replicant Text Tool by LeHieu - VietHoaGame";
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-e":
                        if (args.Length >= 3)
                        {
                            PACK.Unpack(args[1], args[2]);
                        }
                        else
                        {
                            Console.WriteLine("-> Type \"-e [Input File] [Extract Folder]\" to extract the files.");
                        }    
                        break;
                    case "-i":
                        if (args.Length >= 4)
                        {
                            PACK.Repack(args[1], args[2], args[3]);
                            Console.WriteLine($"\n>> {args[3]}");
                        }
                        else
                        {
                            Console.WriteLine("-> Type \"-i [Input File] [Extracted Folder] [Output File]\" to re-import the files.");
                        }
                        break;
                    default:
                        Help();
                        break;
                }
            }
            else
            {
                Help();
            }
        }
        static void Help()
        {
            Console.WriteLine("Usage:\n-> Type \"-e [Input File] [Extract Folder]\" to extract the files.\n-> Type \"-i [Input File] [Extracted Folder] [Output File]\" to re-import the files.");
        }
    }
}
