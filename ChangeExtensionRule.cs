using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class ChangeExtensionRule:Rules
    {
        public string ClassName => "ChangeExtensionRule";

        public List<string> KeyWord =>
            new List<string>() { ClassName, extension };

        public string Description =>
            $"Change Extension .{extension}";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 1)
            {
                result = new ChangeExtensionRule()
                {
                    extension = arguments[0]
                };

            }

            return result;
        }

        public string Process(string inputString, bool isFilename, ref int now)
        {
            string p = this.extension;
            string input = inputString;
            if (isFilename)
            {
                input = Path.GetFileNameWithoutExtension(inputString);
            }


            return input +'.'+ p;
        }


        public string extension { get; set; } = "";
    }
}
