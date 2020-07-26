using System.IO;
using System.Text;

namespace savescrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            string path = args[0];

            if (path == "")
                return;

            string file_name = Path.GetFileNameWithoutExtension(path);

            if (file_name != "savegame")
                return;

            string file_ext = Path.GetExtension(path);

            if (file_ext.ToLower() == ".json")
            {
                Coder coder = new Coder();
                coder.JsonToSaveFile(path);
            }
            
            if (file_ext.ToLower() == "")
            {
                Decoder decoder = new Decoder();
                decoder.SaveFileToJson(path);
            }            
        }
    }
}
