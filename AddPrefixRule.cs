using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class AddPrefixRule:Rules
    {
        public string ClassName => "AddPrefixRule";

        public List<string> KeyWord =>
            new List<string>() { ClassName, prefix };

        public string Description =>
            $"Add '{prefix}' to Prefix";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 1)
            {
                result = new AddPrefixRule()
                {
                    prefix = arguments[0]
                };

            }

            return result;
        }

        public string Process(string inputString, bool isFilename, ref int now)
        {
            string p=this.prefix;
            string input = inputString;
            string extension = "";
            if (isFilename)
            {
                input = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, input.Length);
            }


            return p + input+extension;
        }


        public string prefix { get; set; } ="";


    }
}
