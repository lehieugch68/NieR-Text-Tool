using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NieR_Text_Tool
{
    public class GameCode
    {
        private static Dictionary<string, byte[]> _Instance;
        private static Dictionary<string, byte[]> Instance()
        {
            Dictionary<string, byte[]> gameCode = new Dictionary<string, byte[]>();
            gameCode.Add("{01C280}", new byte[] { 0x1, 0xC2, 0x80 });
            gameCode.Add("{01C299}", new byte[] { 0x1, 0xC2, 0x99 });
            gameCode.Add("{01C29C}", new byte[] { 0x1, 0xC2, 0x9C });
            gameCode.Add("{01C297}", new byte[] { 0x1, 0xC2, 0x97 });
            gameCode.Add("{01C298}", new byte[] { 0x1, 0xC2, 0x98 });
            gameCode.Add("{01C286}", new byte[] { 0x1, 0xC2, 0x86 });
            gameCode.Add("{017E}", new byte[] { 0x1, 0x7E });
            gameCode.Add("{01C285}", new byte[] { 0x1, 0xC2, 0x85 });
            gameCode.Add("{01C2B6}", new byte[] { 0x1, 0xC2, 0xB6 });
            gameCode.Add("{01C2B5}", new byte[] { 0x1, 0xC2, 0xB5 });
            gameCode.Add("{01C28A}", new byte[] { 0x1, 0xC2, 0x8A });
            gameCode.Add("{017F}", new byte[] { 0x1, 0x7F });
            gameCode.Add("{017D}", new byte[] { 0x1, 0x7D });
            gameCode.Add("{0166}", new byte[] { 0x1, 0x66 });
            gameCode.Add("{0167}", new byte[] { 0x1, 0x67 });
            gameCode.Add("{0168}", new byte[] { 0x1, 0x68 });
            gameCode.Add("{016A}", new byte[] { 0x1, 0x6A });
            gameCode.Add("{0169}", new byte[] { 0x1, 0x69 });
            gameCode.Add("{016B}", new byte[] { 0x1, 0x6B });
            gameCode.Add("{0164}", new byte[] { 0x1, 0x64 });
            gameCode.Add("{0165}", new byte[] { 0x1, 0x65 });
            gameCode.Add("{016C}", new byte[] { 0x1, 0x6C });
            gameCode.Add("{016D}", new byte[] { 0x1, 0x6D });
            gameCode.Add("{01C283}", new byte[] { 0x1, 0xC2, 0x83 });
            gameCode.Add("{01C284}", new byte[] { 0x1, 0xC2, 0x84 });
            gameCode.Add("{01C287}", new byte[] { 0x1, 0xC2, 0x87 });
            gameCode.Add("{01C288}", new byte[] { 0x1, 0xC2, 0x88 });
            gameCode.Add("{01C289}", new byte[] { 0x1, 0xC2, 0x89 });
            gameCode.Add("{01C28B}", new byte[] { 0x1, 0xC2, 0x8B });
            gameCode.Add("{01C282}", new byte[] { 0x1, 0xC2, 0x82 });
            gameCode.Add("{01C281}", new byte[] { 0x1, 0xC2, 0x81 });
            gameCode.Add("{01C2A3}", new byte[] { 0x1, 0xC2, 0xA3 });
            gameCode.Add("{01C290}", new byte[] { 0x1, 0xC2, 0x90 });
            gameCode.Add("{01C291}", new byte[] { 0x1, 0xC2, 0x91 });
            gameCode.Add("{01C292}", new byte[] { 0x1, 0xC2, 0x92 });
            gameCode.Add("{01C293}", new byte[] { 0x1, 0xC2, 0x93 });
            gameCode.Add("{01C2A4}", new byte[] { 0x1, 0xC2, 0xA4 });
            gameCode.Add("{01C28C}", new byte[] { 0x1, 0xC2, 0x8C });
            gameCode.Add("{01C28D}", new byte[] { 0x1, 0xC2, 0x8D });
            gameCode.Add("{01C28E}", new byte[] { 0x1, 0xC2, 0x8E });
            gameCode.Add("{01C28F}", new byte[] { 0x1, 0xC2, 0x8F });
            gameCode.Add("{01C2A8}", new byte[] { 0x1, 0xC2, 0xA8 });
            return gameCode;

        }
        public static Dictionary<string, byte[]> GetGameCode()
        {
            if (_Instance == null)
            {
                _Instance = Instance();
            }
            return _Instance;
        }
    }
}
